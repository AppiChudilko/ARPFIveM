using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Jail : BaseScript
    {
        public static readonly Vector3 JailCenter = new Vector3(1707.69f, 2546.69f, 45.56f);
        public static readonly Vector3 JailCop = new Vector3(1707.69f, 2546.69f, 45.56f);
        //public static readonly Vector3 JailCop = new Vector3(460.464f, -994.4287f, 24.91487f);
        public static readonly Vector3 JailFree = new Vector3(1849.444f, 2601.747f, 45.60717f);
        public static readonly Vector3 JailCopFree = new Vector3(439.5873f, -982.1817f, 30.6896f);
        
        public static int Warrning = 0;

        public Jail()
        {
            EventHandlers.Add("ARP:JailPlayer", new Action<int>(JailPlayerScene));
            EventHandlers.Add("ARP:JailFreePlayer", new Action(JailFreePlayer));
            EventHandlers.Add("ARP:UnjailPlayer", new Action(UnjailPlayer));
            
            Tick += SecTimer;
        }
        
        private static async Task SecTimer()
        {
            await Delay(1000);
            if (User.IsLogin())
            {
                if (User.Data.jail_time > 1)
                {
                    User.Data.jail_time--;
                    Client.Sync.Data.Set(User.GetServerId(), "jail_time", User.Data.jail_time);

                    if (IsPedInAnyVehicle(GetPlayerPed(-1), true))
                    {
                        var veh = GetVehiclePedIsUsing(PlayerPedId());
                        if (Client.Vehicle.VehInfo.GetClassName(new CitizenFX.Core.Vehicle(veh).Model.Hash) !=
                            "Emergency")
                        {
                            var coords = GetEntityCoords(GetPlayerPed(-1), true);
                            if (Main.GetDistanceToSquared(coords, JailCenter) > 200f)
                            {
                                Warrning++;

                                if (Warrning > 90)
                                {
                                    JailPlayer(User.Data.jail_time + 300);
                                    Notification.SendWithTime("~r~Попытка сбежать. +300 сек тюрьмы");
                                }
                            }
                        }
                    }
                    else
                    {
                        var coords = GetEntityCoords(GetPlayerPed(-1), true);
                        if (Main.GetDistanceToSquared(coords, JailCenter) > 200f)
                        {
                            Warrning++;
                            if (Warrning > 90)
                            {
                                JailPlayer(User.Data.jail_time + 300);
                                Notification.SendWithTime("~r~Попытка сбежать. +300 сек тюрьмы");
                            }
                        }
                    }
                }
                if (User.Data.jail_time == 1)
                {
                    User.Data.jail_time--;
                    JailFreePlayer();
                }
            }
        }
        
        public static void UnjailPlayer()
        {
            Notification.SendWithTime("~g~Вас досрочно выпустили из тюрьмы");
            JailFreePlayer();
        }
        
        public static async void JailFreePlayer()
        {
            if ((int) await Client.Sync.Data.Get(User.GetServerId(), "jail_time") > User.Data.jail_time + 10)
            {
                User.Kick("Pidaras dont use CHEAT ENGINE");
                Main.SaveLog("UseCheatEngine", $"{User.Data.rp_name} ({User.Data.id} | JAIL_TIME)");
                return;
            }
            
            User.Data.jail_time = 0;
            Client.Sync.Data.Set(User.GetServerId(), "jail_time", User.Data.jail_time);
            Client.Sync.Data.ResetLocally(User.GetServerId(), "jailedCop");

            await Delay(500);
            
            Notification.SendWithTime("~g~Вы отплатили свой долг. Теперь вы свободны.");
            User.Teleport(Client.Sync.Data.HasLocally(User.GetServerId(), "jailedCop") ? JailCopFree : JailFree);
            Characher.UpdateCloth();
        }

        public static async void JailPlayerScene(int sec)
        {
            await UI.ShowLoadDisplayAwait();
            
            int wantedLevel = (int) await Client.Sync.Data.Get(User.GetServerId(), "wanted_level");
            int countSupportVeh = 0;
            
            /*if (wantedLevel == 10)
                countSupportVeh = 5;
            else if (wantedLevel > 7)
                countSupportVeh = 4;
            else if (wantedLevel > 5)
                countSupportVeh = 3;
            else if (wantedLevel > 2)
                countSupportVeh = 2;*/
            
            if (wantedLevel > 5)
                countSupportVeh = 2;
            
            string[] skins = {"s_f_y_cop_01", "s_m_y_cop_01", "s_m_y_cop_01", "s_m_y_cop_01", "s_m_y_cop_01", "s_m_y_cop_01", "s_m_y_cop_01"};
            string[] cars = {"scoutpol", "police", "police3", "police2", "scoutpol", "scoutpol", "police3"};
            var rand = new Random();

            if (rand.Next(3) == 0)
            {
                skins = new[] {"s_f_y_sheriff_01", "s_m_y_sheriff_01", "s_m_y_sheriff_01", "s_m_y_sheriff_01", "s_f_y_sheriff_01", "s_m_y_sheriff_01", "s_m_y_sheriff_01"};
                cars = new[] {"sheriff2", "sheriff", "sheriff2", "sheriff", "sheriff", "sheriff2", "sheriff"};
            }

            if (wantedLevel == 10)
            {
                skins = new[] {"s_m_m_fibsec_01", "s_m_m_fibsec_01", "s_m_m_fibsec_01", "s_m_m_fiboffice_01", "s_m_m_fiboffice_02", "s_m_m_fiboffice_02", "s_m_m_fiboffice_02"};
                cars = new[] {"scoutpol2", "scoutpol2", "fbi2", "fbi2", "fbi2", "fbi2", "fbi2"};
            }
            else if (wantedLevel > 5)
                cars = new[] {"scoutpol2", "scoutpol2", "police4", "police4", "fbi", "fbi2", "scoutpol2"};
            
            uint vehicleHash = (uint) GetHashKey(cars[rand.Next(0, 6)]);
            uint pHash = (uint) GetHashKey(skins[rand.Next(0, 6)]);
            if (!await Main.LoadModel(vehicleHash))
            {
                JailPlayerScene(sec);
                return;
            }
            if (!await Main.LoadModel(pHash))
            {
                JailPlayerScene(sec);
                return;
            }
            
            var taxiCar = CreateVehicle(vehicleHash, 494.2763f, -1026.028f, 27.6905f, 180.5473f, true, false);
            var taxiDriver = CreatePedInsideVehicle(taxiCar, 6, pHash, -1, true, false);
            
            if (wantedLevel == 10)
                SetVehicleColours(taxiCar, 0, 0);
            
            pHash = (uint) GetHashKey(skins[rand.Next(0, 6)]);
            if (!await Main.LoadModel(pHash))
                return;
            var taxiPassanger = CreatePedInsideVehicle(taxiCar, 6, pHash, 0, true, false);
            new CitizenFX.Core.Vehicle(taxiCar).LockStatus = VehicleLockStatus.StickPlayerInside;
            
            var groupHandle = GetPlayerGroup(PlayerPedId());
            SetGroupSeparationRange(groupHandle, 10f);
            SetPedNeverLeavesGroup(taxiDriver, true);
            SetPedNeverLeavesGroup(taxiPassanger, true);
            
            SetPedCanBeTargetted(taxiDriver, true);
            SetPedCanBeTargettedByPlayer(taxiDriver, GetPlayerPed(-1), true);
            SetCanAttackFriendly(taxiDriver, false, false);
            TaskSetBlockingOfNonTemporaryEvents(taxiDriver, true);
            SetBlockingOfNonTemporaryEvents(taxiDriver, true);
            
            SetPedCanBeTargetted(taxiPassanger, true);
            SetPedCanBeTargettedByPlayer(taxiPassanger, GetPlayerPed(-1), true);
            SetCanAttackFriendly(taxiPassanger, false, false);
            TaskSetBlockingOfNonTemporaryEvents(taxiPassanger, true);
            SetBlockingOfNonTemporaryEvents(taxiPassanger, true);
            
            SetPedIntoVehicle(PlayerPedId(), taxiCar, 2);
            JailPlayerWithoutTeleport(sec);
            
            TaskVehicleDriveToCoordLongrange(taxiDriver, taxiCar, 1830.489f, 2603.093f, 45.8891f, 20f, DriveTypes.Normal, 40.0f);
            await UI.HideLoadDisplayAwait();

            var vehs = new List<int>();
            var peds = new List<int>();

            int prevVehicle = taxiCar;

            for (int i = 0; i < countSupportVeh; i++)
            {
                vehicleHash = (uint) GetHashKey(cars[rand.Next(0, 6)]);
                pHash = (uint) GetHashKey(skins[rand.Next(0, 6)]);
                if (!await Main.LoadModel(vehicleHash) && !await Main.LoadModel(pHash))
                    continue;
                
                var supportCar = CreateVehicle(vehicleHash, 485.138f, -1070.594f, 28.5766f, 283.2761f, true, false);
                var supportDriver = CreatePedInsideVehicle(supportCar, 6, pHash, -1, true, false);
                var supportPassanger = CreatePedInsideVehicle(supportCar, 6, pHash, 0, true, false);
                new CitizenFX.Core.Vehicle(supportCar).LockStatus = VehicleLockStatus.StickPlayerInside;
                
                if (wantedLevel == 10)
                    SetVehicleColours(supportCar, 0, 0);
                
                //TaskVehicleEscort(supportDriver, supportCar, prevVehicle, -1, 20f, DriveTypes.Normal, 10.0f, 0, 5f);
                TaskVehicleFollow(supportDriver, supportCar, prevVehicle, 20f, DriveTypes.Normal, 3);
                prevVehicle = supportCar;
                vehs.Add(supportCar);
                peds.Add(supportDriver);
                peds.Add(supportPassanger);
                await Delay(4000);
            }

            int stopWarrning = 0;
            
            while (IsPedInAnyVehicle(GetPlayerPed(-1), true))
            {
                var v = new CitizenFX.Core.Vehicle(taxiCar);
                if (v.Speed < 2)
                {
                    stopWarrning++;
                    if (stopWarrning >= 12)
                    {
                        stopWarrning = 0;
                        Dispatcher.SendEms("Код 2", "Тюремный конвой запрашивает поддержку");
                    }
                }
                else
                    stopWarrning = 0;

                if (Vehicle.CheckIsDisableControl() || v.Health < 680)
                {
                    stopWarrning++;
                    if (stopWarrning >= 4)
                    {
                        stopWarrning = 0;
                        Dispatcher.SendEms("Код 3", "Тюремный конвой запрашивает поддержку");
                        new CitizenFX.Core.Vehicle(taxiCar).LockStatus = VehicleLockStatus.Unlocked;
                    }
                }

                if (new CitizenFX.Core.Ped(taxiDriver).IsDead || new CitizenFX.Core.Ped(taxiPassanger).IsDead)
                {
                    stopWarrning++;
                    if (stopWarrning >= 4)
                    {
                        stopWarrning = 0;
                        Dispatcher.SendEms("Код 0", "Тюремный конвой запрашивает поддержку, офицер убит");
                        new CitizenFX.Core.Vehicle(taxiCar).LockStatus = VehicleLockStatus.Unlocked;
                    }
                }
                
                if (User.Data.jail_time > 0 && Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3(1830.489f, 2603.093f, 45.8891f)) < 80f)
                    TeleportToJail(sec);
                await Delay(5000);
            }

            ClearPedTasks(taxiDriver);
            TaskVehicleDriveWander(taxiDriver, taxiCar, 17.0f, DriveTypes.Normal);
            new CitizenFX.Core.Ped(taxiDriver).MarkAsNoLongerNeeded();
            new CitizenFX.Core.Ped(taxiPassanger).MarkAsNoLongerNeeded();
            new CitizenFX.Core.Vehicle(taxiCar).MarkAsNoLongerNeeded();

            foreach (var v in vehs)
                new CitizenFX.Core.Vehicle(v).MarkAsNoLongerNeeded();
            foreach (var p in peds)
                new CitizenFX.Core.Ped(p).MarkAsNoLongerNeeded();
        }

        public static void JailPlayer(int sec)
        {
            TeleportToJail(sec);
            JailPlayerWithoutTeleport(sec);
        }

        public static async void TeleportToJail(int sec)
        {
            User.UnTie();
            
            if (await Client.Sync.Data.Has(User.GetServerId(), "isCuff"))
            {
                SetEnableHandcuffs(GetPlayerPed(-1), false);
                Client.Sync.Data.Reset(User.GetServerId(), "isCuff");
                Client.Sync.Data.ResetLocally(User.GetServerId(), "isCuff");
                User.StopAnimation();
                User.IsBlockAnimation = false;
                //Freeze(PlayerId(), false);
            }
            
            Client.Sync.Data.ResetLocally(User.GetServerId(), "jailedCop");
            if (sec < 1200)
            {
                Client.Sync.Data.SetLocally(User.GetServerId(), "jailedCop", true);
                User.Teleport(JailCop, 10000);
                NetworkResurrectLocalPlayer(JailCop.X, JailCop.Y, JailCop.Z, 0, true, false);
            }
            else
            {
                User.Teleport(JailCenter);
                NetworkResurrectLocalPlayer(JailCenter.X, JailCenter.Y, JailCenter.Z, 0, true, false);
            }
        }

        public static void JailPlayerWithoutTeleport(int sec)
        {
            User.Data.jail_time = sec;
            User.Data.wanted_level = 0;
            Client.Sync.Data.Set(User.GetServerId(), "jail_time", sec);
            Client.Sync.Data.Set(User.GetServerId(), "wanted_level", 0);
            
            RemoveAllPedWeapons(GetPlayerPed(-1), false);
            User.RemoveWeapons();
            Random rand = new Random();
            
            Warrning = 0;
            
            if (User.Skin.SEX == 1)
            {
                SetPedComponentVariation(GetPlayerPed(-1), 4, 3, 15, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 6, 5, 0, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 8, 60, 500, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 11, 0, 0, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 3, 0, 0, 2);
            }
            else
            {
                if (rand.Next(2) == 0)
                {
                    SetPedComponentVariation(GetPlayerPed(-1), 3, 15, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 11, 56, 1, 2);
                }
                else
                {
                    SetPedComponentVariation(GetPlayerPed(-1), 3, 0, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 11, 56, 0, 2);
                }

                SetPedComponentVariation(GetPlayerPed(-1), 4, 7, 15, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 8, 60, 500, 2);
                SetPedComponentVariation(GetPlayerPed(-1), 6, 6, rand.Next(2), 2);
            }
        }
    }
}