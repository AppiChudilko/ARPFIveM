using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using Client.Vehicle;
using static CitizenFX.Core.Native.API;
using static Client.MenuList;

namespace Client.Managers
{
    public class Timer : BaseScript
    {
        public static int PedFind = -1;
        
        public static int EntityFleeca = 0;
        public static int EntityOther1 = 0;
        public static int EntityOther2 = 0;
        public static int EntityOther3 = 0;

        public static int EntityHeal = 0;
        
        public static Vector3 OutPos;
        public static bool IsDisableClipset;
        
        public static float WeatherTemp = -99;
        
        public Timer()
        {
            Tick += Tick100Timer;
            Tick += SecTimer;
            Tick += SecAtmTimer;
            Tick += Min60Timer;
            Tick += Min30Timer;
            Tick += Min10Timer;
            Tick += Min5Timer;
            Tick += Min3Timer;
            Tick += Min1Timer;
            Tick += Sec10Timer;
            Tick += SetTick;
            Tick += SetTickLight;
            Tick += Set1Tick;
            Tick += SetTickCuff;
            Tick += SetTickHandSup;
            Tick += SetTickHidehud;
            Tick += SetTickCheckId;
            Tick += SetTimerFindNetwork;
        }

        private static async Task Tick100Timer()
        {
            await Delay(10);
            if (WeatherTemp > -99)
                Weather.Temp = WeatherTemp;
            
            UI.DrawAdditionalHud();
            
            if (MenuList.UiMenu != null)
            {
                if (MenuList.UiMenu.Visible)
                {
                    if (User.IsDead())
                    {
                        Notification.SendWithTime("Нужна помощь? Нажмите на..");
                        Notification.SendWithTime("~y~Y-Чтобы задать вопрос.");
                        Notification.SendWithTime("~r~U-Чтобы оставить жалобу.");
                        MenuList.HideMenu();
                    }
                }
            }
        }

        private static async Task Min1Timer()
        {
            await Delay(1500 * 60 * 1);
            
            foreach (var p in Main.GetPedListOnRadius())
            {
                if (p.IsDead && !p.IsPlayer)
                    p.MarkAsNoLongerNeeded();
            }
            
            if (!User.IsGos())
            {
                if (IsPedInAnyVehicle(PlayerPedId(), true))
                {
                    var veh = new CitizenFX.Core.Vehicle(GetVehiclePedIsUsing(PlayerPedId()));
                    if (GetPedInVehicleSeat(veh.Handle, -1) == PlayerPedId() &&
                        VehInfo.GetClassName(veh.Model.Hash) == "Emergency")
                    {
                        Dispatcher.SendEms("Код 0", $"Угон служебного ТС\nНомера: ~y~{GetVehicleNumberPlateText(veh.Handle)}");
                        //Client.Sync.Data.SetLocally(User.GetServerId(), "GrabCash", 1);
                                
                        //SetPlayerWantedLevel(Game.Player.Handle, 5, false);
                        //SetPlayerWantedLevelNow(Game.Player.Handle, false);
                                
                        //PedAi.SendCode(100, false, 15, UnitTypes.Sheriff);
                        
                        Client.Sync.Data.Set(User.GetServerId(), "wanted_level", 10);
                        Client.Sync.Data.Set(User.GetServerId(), "wanted_reason", "Угон служебного транспорта");
                    }
                }
            }
            
            var plPos = GetEntityCoords(GetPlayerPed(-1), true);
            
            if (IsPedInAnyVehicle(PlayerPedId(), true))
            {
                var veh = new CitizenFX.Core.Vehicle(GetVehiclePedIsUsing(PlayerPedId()));
                if (veh.Model.IsBike || veh.Model.IsBoat || veh.Model.IsBicycle)
                    User.UpdateLevel();
            }
            else if (User.GetPlayerVirtualWorld() == 0 && GetInteriorAtCoords(plPos.X, plPos.Y, plPos.Z) == 0)
                User.UpdateLevel();

            if (Client.Sync.Data.HasLocally(User.GetServerId(), "atmTimeout"))
            {
                Client.Sync.Data.SetLocally(User.GetServerId(), "atmTimeout", (int) Client.Sync.Data.GetLocally(User.GetServerId(), "atmTimeout") - 1);
                if ((int) Client.Sync.Data.GetLocally(User.GetServerId(), "atmTimeout") < 1)
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "atmTimeout");
            }

            if (User.GetDrunkLevel() >= 0)
            {
                if (User.GetDrunkLevel() >= 10 && !Main.GetScreenEffectIsActive("ChopVision"))
                    StartScreenEffect("ChopVision", 0, true);
                User.RemoveDrunkLevel(3);

                if (User.GetDrunkLevel() > 50)
                {
                    IsDisableClipset = true;
                    User.SetPlayerNonStaticClipset("MOVE_M@DRUNK@VERYDRUNK");
                    SetPedToRagdoll(GetPlayerPed(-1), 5000, 5000, 0, false, false, false);
                }
                else if (User.GetDrunkLevel() > 100)
                {
                    SetEntityHealth(GetPlayerPed(-1), 0);
                    Notification.SendWithTime("~r~Вы перепили и вырубились, скоро приедет скорая Вас откачивать");
                }
                else
                {
                    IsDisableClipset = true;
                    User.SetPlayerNonStaticClipset("MOVE_M@DRUNK@SLIGHTLYDRUNK");
                }
            }
            if (User.GetDrunkLevel() <= 0 && Main.GetScreenEffectIsActive("ChopVision"))
            {
                IsDisableClipset = false;
                StopScreenEffect("ChopVision");
            }

            if (User.GetDrugMargLevel() > 0)
            {
                if (User.GetDrugMargLevel() > 2 && !Main.GetScreenEffectIsActive("ChopVision"))
                    StartScreenEffect("ChopVision", 0, true);
                User.RemoveDrugMargLevel(1);
            }    
            if (User.GetDrugMargLevel() <= 0 && Main.GetScreenEffectIsActive("ChopVision"))
                StopScreenEffect("ChopVision");

            if (User.GetDrugDrunkLevel() > 0)
            {
                if (User.GetDrugDrunkLevel() > 13)
                {
                    SetEntityHealth(GetPlayerPed(-1), 0);
                    Notification.SendWithTime("~r~Передозировка, скоро приедет скорая Вас откачивать");
                }
                
                if (User.GetDrugAmfLevel() > 0)
                {
                    if (!Main.GetScreenEffectIsActive("DrugsMichaelAliensFightIn"))
                        StartScreenEffect("DrugsMichaelAliensFightIn", 0, true);

                    User.RemoveDrugAmfLevel(1);
                }
                if (User.GetDrugAmfLevel() <= 0 && Main.GetScreenEffectIsActive("DrugsMichaelAliensFightIn"))
                {
                    StopScreenEffect("DrugsMichaelAliensFightIn");
                    StartScreenEffect("DrugsMichaelAliensFightOut", 0, false);
                    await Delay(10000);
                    StopScreenEffect("DrugsMichaelAliensFightOut");
                }

                if (User.GetDrugCocaLevel() > 0)
                {
                    if (!Main.GetScreenEffectIsActive("DrugsTrevorClownsFightIn"))
                        StartScreenEffect("DrugsTrevorClownsFightIn", 0, true);

                    User.RemoveDrugCocaLevel(1);
                }
                if (User.GetDrugCocaLevel() <= 0 && Main.GetScreenEffectIsActive("DrugsTrevorClownsFightIn"))
                {
                    StopScreenEffect("DrugsTrevorClownsFightIn");
                    StartScreenEffect("DrugsTrevorClownsFightOut", 0, false);
                    await Delay(10000);
                    StopScreenEffect("DrugsTrevorClownsFightOut");
                }

                if (User.GetDrugLsdLevel() > 0)
                {
                    if (!Main.GetScreenEffectIsActive("DrugsDrivingIn"))
                        StartScreenEffect("DrugsDrivingIn", 0, true);

                    User.RemoveDrugLsdLevel(1);
                }
                if (User.GetDrugLsdLevel() <= 0 && Main.GetScreenEffectIsActive("DrugsDrivingIn"))
                {
                    StopScreenEffect("DrugsDrivingIn");
                    StartScreenEffect("DrugsDrivingOut", 0, false);
                    await Delay(10000);
                    StopScreenEffect("DrugsDrivingOut");
                }

                if (User.GetDrugMefLevel() > 0)
                {
                    if (!Main.GetScreenEffectIsActive("PeyoteEndIn"))
                        StartScreenEffect("PeyoteEndIn", 0, true);

                    User.RemoveDrugMefLevel(1);
                }
                if (User.GetDrugMefLevel() <= 0 && Main.GetScreenEffectIsActive("PeyoteEndIn"))
                {
                    StopScreenEffect("PeyoteEndIn");
                    StartScreenEffect("PeyoteEndOut", 0, false);
                    await Delay(10000);
                    StopScreenEffect("PeyoteEndOut");
                }

                if (User.GetDrugDmtLevel() > 0)
                {
                    if (!Main.GetScreenEffectIsActive("DMT_flight"))
                        StartScreenEffect("DMT_flight", 0, true);

                    User.RemoveDrugDmtLevel(1);
                }
                if (User.GetDrugDmtLevel() <= 0 && Main.GetScreenEffectIsActive("DMT_flight"))
                    StopScreenEffect("DMT_flight");

                if (User.GetDrugKsanLevel() > 0)
                {
                    if (!Main.GetScreenEffectIsActive("Rampage"))
                        StartScreenEffect("Rampage", 0, true);

                    User.RemoveDrugKsanLevel(1);
                }
                if (User.GetDrugKsanLevel() <= 0 && Main.GetScreenEffectIsActive("Rampage"))
                    StopScreenEffect("Rampage");

                int count = 0;
                foreach (var p in Main.GetPedListOnRadius())
                {
                    if (p.Model.Hash == GetHashKey("a_m_y_acult_02") ||
                        p.Model.Hash == GetHashKey("s_m_m_movalien_01") ||
                        p.Model.Hash == GetHashKey("s_m_m_movspace_01") || p.Model.Hash == GetHashKey("a_c_boar") ||
                        p.Model.Hash == GetHashKey("u_m_y_imporage"))
                    {
                        p.MarkAsNoLongerNeeded();
                        count++;

                        if (User.GetDrugDrunkLevel() < 5)
                            p.Delete();
                        else
                            count++;
                    }
                }

                if (User.GetDrugDrunkLevel() > 5)
                {
                    SetPedToRagdoll(GetPlayerPed(-1), 5000, 5000, 0, false, false, false);

                    if (count < 20)
                    {
                        var rand = new Random();

                        for (int i = 0; i < 10; i++)
                        {
                            string[] skins = {"a_m_y_acult_02", "s_m_m_movalien_01", "s_m_m_movspace_01", "a_c_boar", "u_m_y_imporage", "u_m_y_imporage", "u_m_y_imporage"};
                            var pos = GetEntityCoords(GetPlayerPed(-1), true);
                            uint spawnModel = (uint) GetHashKey(skins[rand.Next(0, 6)]);

                            if (!await Main.LoadModel(spawnModel))
                                return;
                            
                            var ped = CreatePed(6, spawnModel, pos.X, pos.Y, pos.Z + 5, 0, false, false);
                            TaskChatToPed(ped, GetPlayerPed(-1), 16, 0, 0, 0, 0, 0);
                        }
                    }
                }
            }
        }

        private static async Task Min5Timer()
        {
            await Delay(1000 * 60 * 5);

            /*if (Weather.CurrentWeather != "XMAS")
            {
                var vHandle = FindFirstVehicle(ref VehFind);
                do
                {
                    var v = new CitizenFX.Core.Vehicle(VehFind);
                    if (v.Model.Hash == -214455498 || v.Model.Hash == -748008636 || v.Model.Hash == 1933662059 || v.Model.Hash == -1241712818 || v.Model.Hash == -1807623979 || v.Model.Hash == 1132262048)
                        v.MarkAsNoLongerNeeded();

                } while (FindNextVehicle(vHandle, ref PedFind));
                EndFindVehicle(vHandle);
            }*/
            
        }

        private static async Task Min3Timer()
        {
            await Delay(1000 * 60 * 3);
            
            if (IsPedRunning(GetPlayerPed(-1)) && User.Data.mp0_stamina < 99)
            {
                User.Data.mp0_stamina++;
                Client.Sync.Data.Set(User.GetServerId(), "mp0_stamina", User.Data.mp0_stamina);
            }
            
            if (IsPedSwimming(GetPlayerPed(-1)) && User.Data.mp0_lung_capacity < 99)
            {
                User.Data.mp0_lung_capacity++;
                Client.Sync.Data.Set(User.GetServerId(), "mp0_lung_capacity", User.Data.mp0_lung_capacity);
            }
            
            if (IsPedSwimmingUnderWater(GetPlayerPed(-1)) && User.Data.mp0_lung_capacity < 99)
            {
                User.Data.mp0_lung_capacity++;
                User.Data.mp0_lung_capacity++;
                User.Data.mp0_lung_capacity++;
        
                if (User.Data.mp0_lung_capacity >= 99)
                    User.Data.mp0_lung_capacity = 99;
                
                Client.Sync.Data.Set(User.GetServerId(), "mp0_lung_capacity", User.Data.mp0_lung_capacity);
            }
            
            if (User.Data.mp0_wheelie_ability < 99)
            {
                if (IsPedInAnyVehicle(PlayerPedId(), true))
                {
                    var veh = GetVehiclePedIsUsing(PlayerPedId());
                    if (GetPedInVehicleSeat(veh, -1) == PlayerPedId() && !IsEntityInAir(veh) && UI.GetCurrentSpeed() > 10)
                    {
                        User.Data.mp0_wheelie_ability++;
                        Client.Sync.Data.Set(User.GetServerId(), "mp0_wheelie_ability", User.Data.mp0_wheelie_ability);
                    }
                }
            }
            
            if (User.Data.mp0_flying_ability < 99)
            {
                if (IsPedInAnyVehicle(PlayerPedId(), true))
                {
                    var veh = GetVehiclePedIsUsing(PlayerPedId());
                    if (GetPedInVehicleSeat(veh, -1) == PlayerPedId() && IsEntityInAir(veh))
                    {
                        User.Data.mp0_flying_ability++;
                        Client.Sync.Data.Set(User.GetServerId(), "mp0_flying_ability", User.Data.mp0_flying_ability);
                    }
                }
            }
        }

        private static async Task Sec10Timer()
        {
            await Delay(1000 * 10);

            if (User.IsLogin())
            {
                if (IsPedShooting(GetPlayerPed(-1)) && User.Data.mp0_shooting_ability < 99)
                {
                    User.Data.mp0_shooting_ability++;
                    Client.Sync.Data.Set(User.GetServerId(), "mp0_shooting_ability", User.Data.mp0_shooting_ability);
                }
          
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                Client.Sync.Data.Set(User.Data.id, "qposX", pos.X);
                Client.Sync.Data.Set(User.Data.id, "qposY", pos.Y);
                Client.Sync.Data.Set(User.Data.id, "qposZ", pos.Z);
                Client.Sync.Data.Set(User.Data.id, "qrot", GetEntityHeading(GetPlayerPed(-1)));
                Client.Sync.Data.Set(User.Data.id, "qvw", User.GetPlayerVirtualWorld());
                
                
                foreach (uint hash in Enum.GetValues(typeof(WeaponHash)))
                {
                    if (!HasPedGotWeapon(GetPlayerPed(-1), hash, false) && Client.Sync.Data.HasLocally(0, hash.ToString()))
                    {
                        Client.Sync.Data.ResetLocally(0, hash.ToString());
                        Client.Sync.Data.Reset(User.GetServerId(), hash.ToString());
                    }
                }
            }
        }

        private static async Task Min10Timer()
        {
            await Delay(1000 * 60 * 10);
            
            if (Client.Sync.Data.HasLocally(User.GetServerId(), "hightSapd"))
                PedAi.SendCode(99, false);
            
            if (Weather.CurrentWeather == "XMAS")
                PedAi.SendCode(100, false, 10, UnitTypes.WinterCiv);
        }
        
        private static async Task Min30Timer()
        {
            await Delay(1000 * 60 * 30);
            PedAi.SendCode(100, false, 15, UnitTypes.InvaderCiv);
        }
        
        private static async Task Min60Timer()
        {
            await Delay(1000 * 60 * 60);

            if (!User.IsRpAnim)
            {
                if (User.Data.mp0_stamina > 0)
                {
                    User.Data.mp0_stamina--;
                    Client.Sync.Data.Set(User.GetServerId(), "mp0_stamina", User.Data.mp0_stamina);
                }
            
                if (User.Data.mp0_lung_capacity > 0)
                {
                    User.Data.mp0_lung_capacity--;
                    Client.Sync.Data.Set(User.GetServerId(), "mp0_lung_capacity", User.Data.mp0_lung_capacity);
                }
            
                if (User.Data.mp0_lung_capacity > 0)
                {
                    User.Data.mp0_lung_capacity--;
                    Client.Sync.Data.Set(User.GetServerId(), "mp0_lung_capacity", User.Data.mp0_lung_capacity);
                }
            
                if (User.Data.mp0_wheelie_ability > 0)
                {
                    User.Data.mp0_wheelie_ability--;
                    Client.Sync.Data.Set(User.GetServerId(), "mp0_wheelie_ability", User.Data.mp0_wheelie_ability);
                }
            }
        }

        private static async Task SecAtmTimer()
        {
            await Delay(2000);
            
            var plPos = GetEntityCoords(GetPlayerPed(-1), true);

            Model fleeca = 506770882;
            Model other1 = -1126237515;
            Model other2 = -1364697528;
            Model other3 = -870868698;

            Model heal1 = 0;

            Model fire1 = -1065766299;
            Model fire2 = -1350614541;
            Model fire3 = 690464963;
            
            EntityFleeca = GetClosestObjectOfType(plPos.X, plPos.Y, plPos.Z, 0.68f, (uint) fleeca.Hash, false, false, false);            
            EntityOther1 = GetClosestObjectOfType(plPos.X, plPos.Y, plPos.Z, 0.68f, (uint) other1.Hash, false, false, false);
            EntityOther2 = GetClosestObjectOfType(plPos.X, plPos.Y, plPos.Z, 0.68f, (uint) other2.Hash, false, false, false);
            EntityOther3 = GetClosestObjectOfType(plPos.X, plPos.Y, plPos.Z, 0.68f, (uint) other3.Hash, false, false, false);  
            
            EntityHeal = GetClosestObjectOfType(plPos.X, plPos.Y, plPos.Z, 0.68f, (uint) heal1.Hash, false, false, false);  

            
            var entityFire1 = GetClosestObjectOfType(plPos.X, plPos.Y, plPos.Z, 5f, (uint) fire1.Hash, false, false, false);
            var entityFire2 = GetClosestObjectOfType(plPos.X, plPos.Y, plPos.Z, 5f, (uint) fire2.Hash, false, false, false);
            var entityFire3 = GetClosestObjectOfType(plPos.X, plPos.Y, plPos.Z, 5f, (uint) fire3.Hash, false, false, false);

            if (entityFire1 != 0)
            {
                float distance = Main.GetDistanceToSquared(GetEntityCoords(entityFire1, true), plPos);
                Weather.Temp = Weather.TempServer + (float) Math.Round(((distance * 2) - 10f) / -1, 1);
            }
            else if (entityFire2 != 0)
            {
                float distance = Main.GetDistanceToSquared(GetEntityCoords(entityFire2, true), plPos);
                Weather.Temp = Weather.TempServer + (float) Math.Round(((distance * 2) - 10f) / -1, 1);
            }
            else if (entityFire3 != 0)
            {
                float distance = Main.GetDistanceToSquared(GetEntityCoords(entityFire3, true), plPos);
                Weather.Temp = Weather.TempServer + (float) Math.Round(((distance * 2) - 10f) / -1, 1);
            }
            else if (plPos.Z >= 110)
                Weather.Temp = Weather.TempServer - (float) Math.Round((plPos.Z - 110) / 50, 1);
            else
                WeatherTemp = -99;

            if (WeatherTemp > -99)
                WeatherTemp = Weather.Temp;
            
            if (User.Data.bank_prefix > 0 && (EntityFleeca != 0 || EntityOther1 != 0 || EntityOther2 != 0 || EntityOther3 != 0))
                Notification.Send("Нажмите ~b~E~s~ чтобы открыть меню банкомата");
        }

        private static async Task SecTimer()
        {
            await Delay(1500);

            if (User.IsAuth)
            {
                User.HealthCheck();
            }

            if (User.GetVipStatus() == "none" && User.Data.last_login < User.Data.date_reg + 604800)
                User.Data.vip_status = "Light";


            foreach (uint hash in Enum.GetValues(typeof(WeaponHash)))
            {
                string name = Enum.GetName(typeof(WeaponHash), hash);
                if (!HasPedGotWeapon(GetPlayerPed(-1), hash, false)) continue;
                if (!Client.Sync.Data.HasLocally(0, hash.ToString()) && name != "Unarmed")
                    RemoveWeaponFromPed(GetPlayerPed(-1), hash);
            }
            
            //NetworkSetVoiceChannel(User.GetPlayerVirtualWorld());
            /*NetworkSetTalkerProximity(5f);
            switch (User.Voice)
            {
                case 0:
                    NetworkSetTalkerProximity(2f);
                    break;
                case 2:
                    NetworkSetTalkerProximity(10f);
                    break;
            }*/
            
            if (User.EnableRagdoll)
                SetPedToRagdoll(GetPlayerPed(-1), 1490, 1490, 0, false, false, false); 
            
            Main.UpdatePedListCache();
            Main.UpdateVehicleListCache();
            
            StatSetInt((uint) GetHashKey("MP0_STAMINA"), User.Data.mp0_stamina, true);
            StatSetInt((uint) GetHashKey("MP0_STRENGTH"), User.Data.mp0_strength, true);
            StatSetInt((uint) GetHashKey("MP0_LUNG_CAPACITY"), User.Data.mp0_lung_capacity, true);
            StatSetInt((uint) GetHashKey("MP0_WHEELIE_ABILITY"), User.Data.mp0_wheelie_ability, true);
            StatSetInt((uint) GetHashKey("MP0_FLYING_ABILITY"), User.Data.mp0_flying_ability, true);
            StatSetInt((uint) GetHashKey("MP0_SHOOTING_ABILITY"), User.Data.mp0_shooting_ability, true);
            StatSetInt((uint) GetHashKey("MP0_STEALTH_ABILITY"), User.Data.mp0_stealth_ability, true);

            SetPedAccuracy(GetPlayerPed(-1), User.Data.mp0_shooting_ability);

            User.Amount = User.Data.mp0_strength * 100 + 45100;
            
            if (User.GetEatLevel() < 250)
            {
                IsDisableClipset = true;
                //User.SetPlayerNonStaticClipset("move_heist_lester");
            }
            else if (User.GetDrunkLevel() > 50)
            {
                IsDisableClipset = true;
                User.SetPlayerNonStaticClipset("MOVE_M@DRUNK@VERYDRUNK");
                SetPedToRagdoll(GetPlayerPed(-1), 5000, 5000, 0, false, false, false);
            }
            else if (User.GetDrunkLevel() > 100)
            {
                SetEntityHealth(GetPlayerPed(-1), 0);
                Notification.SendWithTime("~r~Вы перепили и вырубились, скоро приедет скорая Вас откачивать");
            }
            else if(User.GetDrunkLevel() >= 5)
            {
                IsDisableClipset = true;
                User.SetPlayerNonStaticClipset("MOVE_M@DRUNK@SLIGHTLYDRUNK");
            }
            else if(User.GetDrunkLevel() < 5)
                IsDisableClipset = false;

            if (!IsDisableClipset)
                User.SetPlayerCurrentClipset();
             
            if (User.Data.money < -10000 || User.Data.money_bank < -10000)
                TriggerServerEvent("ARP:BanPlayerByServer");
             
            if ((User.Data.money > 1000000 || User.Data.money_bank > 1000000) && User.Data.age == 18 && User.Data.exp_age < 30)
                TriggerServerEvent("ARP:BanPlayerByServer");
            
            if (Client.Sync.Data.HasLocally(User.GetServerId(), "autopilot"))
            {
                if (World.WaypointPosition.X == 0 && World.WaypointPosition.Y == 0)
                {
                    ClearPedTasks(GetPlayerPed(-1));
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "autopilot");

                    var ped = GetPlayerPed(-1);
                    
                    if (!IsPedSittingInAnyVehicle(ped)) return;
                    var vehicle = GetVehiclePedIsIn(ped, false);
                    if (GetPedInVehicleSeat(vehicle, -1) != ped) return;
                    
                    TaskVehicleTempAction(PedFind, vehicle, 27, 8000);
                    await Delay(8000);
                    
                    Notification.SendWithTime("~g~Вы достигли конечной точки маршрута");
                    Notification.SendWithTime("~g~Автопилот деактивирован");
                    ClearPedTasks(GetPlayerPed(-1));
                    return;
                }
            }
            
            if (Client.Sync.Data.HasLocally(User.GetServerId(), "isCuff") || Client.Sync.Data.HasLocally(User.GetServerId(), "isTie"))
            {
                if (!IsEntityPlayingAnim(GetPlayerPed(-1), "mp_arresting", "idle", 3))
                {
                    User.IsBlockAnimation = false;
                    SetEnableHandcuffs(GetPlayerPed(-1), true);
                    User.PlayAnimation("mp_arresting", "idle");
                    User.IsBlockAnimation = true;
                }
                if (!IsEntityPlayingAnim(GetPlayerPed(-1), "mp_arresting", "idle", 49))
                {
                    User.IsBlockAnimation = false;
                    SetEnableHandcuffs(GetPlayerPed(-1), true);
                    User.PlayAnimation("mp_arresting", "idle");
                    User.IsBlockAnimation = true;
                }
            }
            
            /*var relationshipList = new List<string>
            {
                "PLAYER",
                "CIVMALE",
                "CIVFEMALE",
                "COP",
                "SECURITY_GUARD",
                "PRIVATE_SECURITY",
                "FIREMAN",
                "GANG_1",
                "GANG_2",
                "GANG_9",
                "GANG_10",
                "AMBIENT_GANG_LOST",
                "AMBIENT_GANG_MEXICAN",
                "AMBIENT_GANG_FAMILY",
                "AMBIENT_GANG_BALLAS",
                "AMBIENT_GANG_MARABUNTE",
                "AMBIENT_GANG_CULT",
                "AMBIENT_GANG_SALVA",
                "AMBIENT_GANG_WEICHENG",
                "AMBIENT_GANG_HILLBILLY",
                "DEALER",
                "HATES_PLAYER",
                "HEN",
                "WILD_ANIMAL",
                "SHARK",
                "COUGAR",
                "NO_RELATIONSHIP",
                "SPECIAL",
                "MISSION2",
                "MISSION3",
                "MISSION4",
                "MISSION5",
                "MISSION6",
                "MISSION7",
                "MISSION8",
                "ARMY",
                "GUARD_DOG",
                "AGGRESSIVE_INVESTIGATE",
                "MEDIC",
                "CAT",
            };
            
            SetPedAsCop(GetPlayerPed(-1), false);

            foreach (var item in relationshipList)
            {
                SetRelationshipBetweenGroups(RelationshipTypes.Neutral, (uint) GetHashKey("PLAYER"), (uint) GetHashKey(item));
                SetRelationshipBetweenGroups(RelationshipTypes.Neutral, (uint) GetHashKey(item), (uint) GetHashKey("PLAYER"));
            }*/
            
            //AddRelationshipGroup()
            
            SetRelationshipBetweenGroups(RelationshipTypes.Companion, (uint) GetHashKey("PLAYER"), (uint) GetHashKey("PLAYER"));
            SetPedAsCop(GetPlayerPed(-1), false);
            
            if (User.IsGos())
            {
                SetPedAsCop(GetPlayerPed(-1), true);
                SetRelationshipBetweenGroups(RelationshipTypes.Companion, (uint) GetHashKey("PLAYER"), (uint) GetHashKey("COP"));
                SetRelationshipBetweenGroups(RelationshipTypes.Companion, (uint) GetHashKey("COP"), (uint) GetHashKey("PLAYER"));
                
                SetRelationshipBetweenGroups(RelationshipTypes.Like, (uint) GetHashKey("PLAYER"), (uint) GetHashKey("CIVMALE"));
                SetRelationshipBetweenGroups(RelationshipTypes.Like, (uint) GetHashKey("CIVMALE"), (uint) GetHashKey("PLAYER"));
                
                SetRelationshipBetweenGroups(RelationshipTypes.Like, (uint) GetHashKey("PLAYER"), (uint) GetHashKey("CIVFEMALE"));
                SetRelationshipBetweenGroups(RelationshipTypes.Like, (uint) GetHashKey("CIVFEMALE"), (uint) GetHashKey("PLAYER"));
                
                SetRelationshipBetweenGroups(RelationshipTypes.Companion, (uint) GetHashKey("PLAYER"), (uint) GetHashKey("ARMY"));
                SetRelationshipBetweenGroups(RelationshipTypes.Companion, (uint) GetHashKey("ARMY"), (uint) GetHashKey("PLAYER"));
                
                SetRelationshipBetweenGroups(RelationshipTypes.Companion, (uint) GetHashKey("PLAYER"), (uint) GetHashKey("MEDIC"));
                SetRelationshipBetweenGroups(RelationshipTypes.Companion, (uint) GetHashKey("MEDIC"), (uint) GetHashKey("PLAYER"));
                
                SetRelationshipBetweenGroups(RelationshipTypes.Companion, (uint) GetHashKey("PLAYER"), (uint) GetHashKey("FIREMAN"));
                SetRelationshipBetweenGroups(RelationshipTypes.Companion, (uint) GetHashKey("FIREMAN"), (uint) GetHashKey("PLAYER"));
            }
            else if (User.IsBallas())
            {
                SetRelationshipBetweenGroups(RelationshipTypes.Companion, (uint) GetHashKey("PLAYER"), (uint) GetHashKey("AMBIENT_GANG_BALLAS"));
                SetRelationshipBetweenGroups(RelationshipTypes.Companion, (uint) GetHashKey("AMBIENT_GANG_BALLAS"), (uint) GetHashKey("PLAYER"));
                
                SetRelationshipBetweenGroups(RelationshipTypes.Dislike, (uint) GetHashKey("PLAYER"), (uint) GetHashKey("AMBIENT_GANG_MARABUNTE"));
                SetRelationshipBetweenGroups(RelationshipTypes.Dislike, (uint) GetHashKey("AMBIENT_GANG_MARABUNTE"), (uint) GetHashKey("PLAYER"));
                
                SetRelationshipBetweenGroups(RelationshipTypes.Dislike, (uint) GetHashKey("PLAYER"), (uint) GetHashKey("AMBIENT_GANG_FAMILY"));
                SetRelationshipBetweenGroups(RelationshipTypes.Dislike, (uint) GetHashKey("AMBIENT_GANG_FAMILY"), (uint) GetHashKey("PLAYER"));
            }
            else if (User.IsMara())
            {
                SetRelationshipBetweenGroups(RelationshipTypes.Companion, (uint) GetHashKey("PLAYER"), (uint) GetHashKey("AMBIENT_GANG_MARABUNTE"));
                SetRelationshipBetweenGroups(RelationshipTypes.Companion, (uint) GetHashKey("AMBIENT_GANG_MARABUNTE"), (uint) GetHashKey("PLAYER"));
                
                SetRelationshipBetweenGroups(RelationshipTypes.Dislike, (uint) GetHashKey("PLAYER"), (uint) GetHashKey("AMBIENT_GANG_BALLAS"));
                SetRelationshipBetweenGroups(RelationshipTypes.Dislike, (uint) GetHashKey("AMBIENT_GANG_BALLAS"), (uint) GetHashKey("PLAYER"));
                
                SetRelationshipBetweenGroups(RelationshipTypes.Dislike, (uint) GetHashKey("PLAYER"), (uint) GetHashKey("AMBIENT_GANG_FAMILY"));
                SetRelationshipBetweenGroups(RelationshipTypes.Dislike, (uint) GetHashKey("AMBIENT_GANG_FAMILY"), (uint) GetHashKey("PLAYER"));
            }

            foreach (var p in Main.GetPedListOnRadius())
            {
                if (!p.IsPlayer)
                    SetPedDropsWeaponsWhenDead(p.Handle, false);
                
                /*
                    "PLAYER",
                    "CIVMALE",
                    "CIVFEMALE",
                    "COP",
                    "SECURITY_GUARD",
                    "PRIVATE_SECURITY",
                    "FIREMAN",
                    "GANG_1",
                    "GANG_2",
                    "GANG_9",
                    "GANG_10",
                    "AMBIENT_GANG_LOST",
                    "AMBIENT_GANG_MEXICAN",
                    "AMBIENT_GANG_FAMILY",
                    "AMBIENT_GANG_BALLAS",
                    "AMBIENT_GANG_MARABUNTE",
                    "AMBIENT_GANG_CULT",
                    "AMBIENT_GANG_SALVA",
                    "AMBIENT_GANG_WEICHENG",
                    "AMBIENT_GANG_HILLBILLY",
                    "DEALER",
                    "HATES_PLAYER",
                    "HEN",
                    "WILD_ANIMAL",
                    "SHARK",
                    "COUGAR",
                    "NO_RELATIONSHIP",
                    "SPECIAL",
                    "MISSION2",
                    "MISSION3",
                    "MISSION4",
                    "MISSION5",
                    "MISSION6",
                    "MISSION7",
                    "MISSION8",
                    "ARMY",
                    "GUARD_DOG",
                    "AGGRESSIVE_INVESTIGATE",
                    "MEDIC",
                    "CAT",
                */

                if (Client.Sync.Data.HasLocally(PedFind, "lspdUnitToCoord"))
                {
                    if (Main.GetDistanceToSquared((Vector3) Client.Sync.Data.GetLocally(PedFind, "lspdUnitToCoord"), p.Position) < 50f)
                    {
                        ClearPedTasks(PedFind);
                        //new CitizenFX.Core.Blip(Client.Sync.Data.GetLocally(PedFind, "lspdBlip")).Delete();
                        var vehicle = GetVehiclePedIsIn(PedFind, false);
                        //TaskVehiclePark(PedFind, vehicle, World.WaypointPosition.X, World.WaypointPosition.Y, World.GetGroundHeight(World.WaypointPosition), 0, 0, 100, false);
                        TaskVehicleTempAction(PedFind, vehicle, 27, 15000);
                        await Delay(15000);
                        TaskVehicleTempAction(PedFind, vehicle, 27, 15000);
                        Client.Sync.Data.ResetLocally(PedFind, "lspdUnitToCoord");
                    }
                }
                if (Client.Sync.Data.HasLocally(PedFind, "lspdUnitTimeout"))
                {
                    if ((int) Client.Sync.Data.GetLocally(PedFind, "lspdUnitTimeout") > 0)
                    {
                        Client.Sync.Data.SetLocally(PedFind, "lspdUnitTimeout", (int) Client.Sync.Data.GetLocally(PedFind, "lspdUnitTimeout") - 1);
                        if ((int) Client.Sync.Data.GetLocally(PedFind, "lspdUnitTimeout") == 0)
                        {
                            var veh = GetVehiclePedIsUsing(PedFind);
                            TaskVehicleDriveWander(PedFind, veh, 17.0f, DriveTypes.Normal);
                            SetVehicleSiren(veh, false);
                            Client.Sync.Data.ResetLocally(PedFind, "lspdUnitTimeout");
                            
                            p.MarkAsNoLongerNeeded();
                            new CitizenFX.Core.Vehicle(veh).MarkAsNoLongerNeeded();
                        }
                    }    
                }
                
                /*if ((p.IsInvincible || !p.IsVisible) && !p.IsPlayer)
                    p.Delete();*/
                
                /*if (
                    p.Model.Hash == 368603149 || 
                    p.Model.Hash == 1581098148 || 
                    p.Model.Hash == -1920001264 || 
                    p.Model.Hash == -1699520669
                )
                    p.Delete();*/

            }

            foreach (var v in Main.GetVehicleListOnRadius())
            {
                if (v.Model.Hash == (int) VehicleHash.Rhino || v.Model.Hash == (int) VehicleHash.CargoPlane)
                    v.Delete();
            }
        }
        
        private static async Task SetTick()
        {
            SetParkedVehicleDensityMultiplierThisFrame(0.5f);
            SetVehicleDensityMultiplierThisFrame(User.GetPlayerVirtualWorld() > 0 ? 0f : 0.5f);
        
            SetPlayerHealthRechargeMultiplier(PlayerId(), 0);
            
            var ped = GetPlayerPed(-1);
            if (IsPedBeingStunned(ped, 0))
            {
                var rand = new Random();
                if (User.Data.mp0_strength > 95 && rand.Next(3) > 0)
                    SetPedMinGroundTimeForStungun(ped, 1000);
                else if (User.Data.mp0_strength > 70 && rand.Next(2) > 0)
                    SetPedMinGroundTimeForStungun(ped, 6000);
                else if (User.Data.mp0_strength > 50 && rand.Next(2) > 0)
                    SetPedMinGroundTimeForStungun(ped, 12000);
                else
                    SetPedMinGroundTimeForStungun(ped, 30000);
            }

            if (GetSelectedPedWeapon(PlayerPedId()) == GetHashKey("WEAPON_SNOWBALL"))
                SetPlayerWeaponDamageModifier(PlayerId(), 0.0f);
        }
        
        private static async Task SetTickLight()
        {
            var plPos = GetEntityCoords(GetPlayerPed(-1), true);
            if (GetInteriorAtCoords(plPos.X, plPos.Y, plPos.Z) != 0)
            {
                DrawLightWithRange(291.9079f, -1348.883f, 27.03455f, 255, 255, 255, 20.0f, 0.5f);
                DrawLightWithRange(279.4622f, -1337.024f, 27.03455f, 255, 255, 255, 20.0f, 0.5f);
                DrawLightWithRange(272.779f, -1341.37f, 27.03455f, 255, 255, 255, 20.0f, 0.5f);
                DrawLightWithRange(264.4822f, -1360.97f, 27.03455f, 255, 255, 255, 20.0f, 0.5f);
                DrawLightWithRange(253.408f, -1364.389f, 27.03455f, 255, 255, 255, 20.0f, 0.5f);
                DrawLightWithRange(254.8074f, -1349.439f, 27.03455f, 255, 255, 255, 20.0f, 0.5f);
                DrawLightWithRange(240.4855f, -1368.784f, 32.28351f, 255, 255, 255, 20.0f, 0.5f);
                DrawLightWithRange(247.7051f, -1366.653f, 32.34088f, 255, 255, 255, 20.0f, 0.5f);
                DrawLightWithRange(257.9836f, -1358.863f, 41.80476f, 255, 255, 255, 20.0f, 0.5f);
                DrawLightWithRange(255.0098f, -1383.685f, 42.01367f, 255, 255, 255, 20.0f, 0.5f);
                DrawLightWithRange(230.9255f, -1367.348f, 42.03852f, 255, 255, 255, 20.0f, 0.5f);
                DrawLightWithRange(243.6069f, -1366.777f, 26.78872f, 255, 255, 255, 20.0f, 0.5f);
            }
        }
        
        public static int FindSeat(int vehicle)
        {
            if (AreAnyVehicleSeatsFree(vehicle))
            {
                for (int i = 0; i < GetVehicleMaxNumberOfPassengers(vehicle) - 1; i++)
                {
                    i++;
                    if (IsVehicleSeatFree(vehicle, i))
                        return i;
                }
            }
            return 999;
        }
        
        private static async Task Set1Tick()
        {
            await Delay(1);
            
            var veh = GetVehiclePedIsTryingToEnter(PlayerPedId());
            if (DoesEntityExist(veh))
            {
                var ped = GetPedInVehicleSeat(veh, -1);
                
                if (GetVehicleDoorLockStatus(veh) == 7)
                    SetVehicleDoorsLocked(veh, 2);
                
                if (DoesEntityExist(ped))
                {
                    if (Client.Sync.Data.HasLocally(ped, "isTaxi"))
                    {
                        var seat = FindSeat(veh);
                        if (seat != 999)
                        {
                            DoScreenFadeOut(500);
                            while (IsScreenFadingOut())
                                await Delay(1);
                
                            await Delay(500);

                            var plPed = GetPlayerPed(-1);
                                
                            SetPedIntoVehicle(plPed, veh, seat);

                            await Delay(500);
                
                            DoScreenFadeIn(500);
                            while (IsScreenFadingIn())
                                await Delay(1);
                        }
                
                        /*var seat = FindSeat(veh);
                        if (seat != 999)
                        {
                            SetPedCanBeDraggedOut(ped, false);
                            SetVehicleDoorsLocked(veh, 1);
                            SetPedGroupMemberPassengerIndex(GetPlayerPed(-1), seat);
                            TaskEnterVehicle(GetPlayerPed(-1), veh, 10000, seat, 1.0f, 1, 0);
                        }*/
                    }
                    else if (!IsPedAPlayer(ped))
                        SetPedCanBeDraggedOut(ped, false);
                }
            }
        }
        
        private static async Task SetTickCuff()
        {
            if (IsEntityPlayingAnim(GetPlayerPed(-1), "mp_arresting", "idle", 49))
            {
                DisableControlAction(0, 21, true); //disable sprint
                DisableControlAction(0, 24, true); //disable attack
                DisableControlAction(0, 25, true); //disable aim
                DisableControlAction(0, 47, true); //disable weapon
                DisableControlAction(0, 58, true); //disable weapon
                DisableControlAction(0, 263, true); //disable melee
                DisableControlAction(0, 264, true); //disable melee
                DisableControlAction(0, 257, true); //disable melee
                DisableControlAction(0, 140, true); //disable melee
                DisableControlAction(0, 141, true); //disable melee
                DisableControlAction(0, 142, true); //disable melee
                DisableControlAction(0, 143, true); //disable melee
                DisableControlAction(0, 75, true); //disable exit vehicle
                DisableControlAction(27, 75, true); //disable exit vehicle
            }
            if (IsEntityPlayingAnim(GetPlayerPed(-1), "mp_arresting", "idle", 3))
            {
                DisableControlAction(0, 21, true); //disable sprint
                DisableControlAction(0, 24, true); //disable attack
                DisableControlAction(0, 25, true); //disable aim
                DisableControlAction(0, 47, true); //disable weapon
                DisableControlAction(0, 58, true); //disable weapon
                DisableControlAction(0, 263, true); //disable melee
                DisableControlAction(0, 264, true); //disable melee
                DisableControlAction(0, 257, true); //disable melee
                DisableControlAction(0, 140, true); //disable melee
                DisableControlAction(0, 141, true); //disable melee
                DisableControlAction(0, 142, true); //disable melee
                DisableControlAction(0, 143, true); //disable melee
                DisableControlAction(0, 75, true); //disable exit vehicle
                DisableControlAction(27, 75, true); //disable exit vehicle
            }
        }
        
        private static async Task SetTickHandSup()
        {
            if (IsEntityPlayingAnim(GetPlayerPed(-1), "random@arrests@busted", "idle_a", 9))
            {
                DisableControlAction(0, 21, true); //disable sprint
                DisableControlAction(0, 24, true); //disable attack
                DisableControlAction(0, 25, true); //disable aim
                DisableControlAction(0, 47, true); //disable weapon
                DisableControlAction(0, 58, true); //disable weapon
                DisableControlAction(0, 263, true); //disable melee
                DisableControlAction(0, 264, true); //disable melee
                DisableControlAction(0, 257, true); //disable melee
                DisableControlAction(0, 140, true); //disable melee
                DisableControlAction(0, 141, true); //disable melee
                DisableControlAction(0, 142, true); //disable melee
                DisableControlAction(0, 143, true); //disable melee
                DisableControlAction(0, 75, true); //disable exit vehicle
                DisableControlAction(27, 75, true); //disable exit vehicle
            }
            if (IsEntityPlayingAnim(GetPlayerPed(-1), "random@arrests@busted", "idle_a", 3))
            {
                DisableControlAction(0, 21, true); //disable sprint
                DisableControlAction(0, 24, true); //disable attack
                DisableControlAction(0, 25, true); //disable aim
                DisableControlAction(0, 47, true); //disable weapon
                DisableControlAction(0, 58, true); //disable weapon
                DisableControlAction(0, 263, true); //disable melee
                DisableControlAction(0, 264, true); //disable melee
                DisableControlAction(0, 257, true); //disable melee
                DisableControlAction(0, 140, true); //disable melee
                DisableControlAction(0, 141, true); //disable melee
                DisableControlAction(0, 142, true); //disable melee
                DisableControlAction(0, 143, true); //disable melee
                DisableControlAction(0, 75, true); //disable exit vehicle
                DisableControlAction(27, 75, true); //disable exit vehicle
            }
        }
        
        private static async Task SetTickHidehud()
        {
            HideHudComponentThisFrame(1); // Wanted Stars
            HideHudComponentThisFrame(2); // Weapon Icon
            HideHudComponentThisFrame(3); // Cash
            HideHudComponentThisFrame(4); // MP Cash
            //HideHudComponentThisFrame(6); // Vehicle Name
            HideHudComponentThisFrame(7); // Area Name
            //HideHudComponentThisFrame(8);// Vehicle Class
            HideHudComponentThisFrame(9); // Street Name
            HideHudComponentThisFrame(13); // Cash Change
            HideHudComponentThisFrame(17); // Save Game
            HideHudComponentThisFrame(20); // Weapon Stats

            if (User.Data.mp0_shooting_ability < 70)
                HideHudComponentThisFrame(14);
        }
        
        private static async Task SetTickCheckId()
        {
            if (User.IsLogin() && Game.CurrentInputMode == InputMode.MouseAndKeyboard && !Menu.IsShowInput)
            {
                if (Game.IsControlPressed(0, (Control) 118) ||
                    Game.IsControlPressed(0, (Control) 118) ||
                    Game.IsControlPressed(0, (Control) 166) ||
                    Game.IsControlPressed(0, (Control) 166))
                {
                    foreach (var p in new PlayerList())
                    {
                        if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                        if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                        if (!(Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(p.Handle), true),
                                  GetEntityCoords(GetPlayerPed(-1), true)) < 10f)) continue;
                        var entity = new MyEntity(GetPlayerPed(p.Handle));
                        UI.DrawText3D(entity.Position + new Vector3(0, 0, 0.6f),
                            User.PlayerIdList[p.ServerId.ToString()].ToString());
                    }
                }

                if ((Game.IsControlPressed(0, (Control) 127) || Game.IsControlPressed(0, (Control) 127)) &&
                    User.IsAdmin())
                {
                    foreach (var p in new PlayerList())
                    {
                        if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                        if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                        if (!(Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(p.Handle), true),
                                  GetEntityCoords(GetPlayerPed(-1), true)) < 300f)) continue;
                        var entity = new MyEntity(GetPlayerPed(p.Handle));
                        UI.DrawText3D(entity.Position + new Vector3(0, 0, 0.6f),
                            User.PlayerIdList[p.ServerId.ToString()].ToString());
                    }
                }
            }
        }
        
        private static async Task SetTimerFindNetwork()
        {
            await Delay(1000);

            if (!await Ctos.IsBlackout() && User.Data.phone_code > 0)
            {
                var plPos = GetEntityCoords(GetPlayerPed(-1), true);
                var pos = Ctos.FindNearestNetwork(plPos);
                
                /*if (await Client.Sync.Data.Has(-1, "DisableNetwork" + Ctos.FindNearestNetworkId(plPos)))
                {
                    CitizenFX.Core.UI.Screen.LoadingPrompt.Show("Поиск ближайшей вышки связи...");
                    pos = await Ctos.FindNearestNetworkHasNetwork(plPos);
                    CitizenFX.Core.UI.Screen.LoadingPrompt.Hide();
                }*/

                var distance = Main.GetDistanceToSquared(pos, plPos);
                
                if (plPos.Z < 270 && plPos.Z > 0)
                {
                    if (distance <= 1000)
                        Ctos.UserNetwork = 1;
                    else if (distance > 1000 && distance < 1500)
                    {
                        float distanceNetwork = (500 - (distance - 1000)) / 5.0f;
                        Ctos.UserNetwork = distanceNetwork / 100f;
                    }
                    else
                        Ctos.UserNetwork = 0;
                }
                else if (plPos.Z < 450 && plPos.Z >= 270)
                {
                    float distanceNetwork = (180 - (plPos.Z - 270)) / 1.8f;
                    Ctos.UserNetwork = distanceNetwork / 100f;
                }
                else if (plPos.Z >= 450)
                    Ctos.UserNetwork = 0;
            }
            else
                Ctos.UserNetwork = 0;
        }
    }
}

public class RelationshipTypes
{
    public static int Companion => 0;
    public static int Respect => 1;
    public static int Like => 2;
    public static int Neutral => 3;
    public static int Dislike => 4;
    public static int Hate => 5;
    public static int Pedestrians => 255;
}