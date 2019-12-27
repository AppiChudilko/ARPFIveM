using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using Client.Vehicle;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Vehicle : BaseScript
    {
        public static List<VehicleInfoGlobalData> VehicleInfoGlobalDataList = new List<VehicleInfoGlobalData>();
        public static int VehicleFind = -1;
        public static int VehicleFindDouble = -1;
        public static float LoadSquare = 300f;
        
        public static float AutoPilotSpeed = 60f;
        public static float MaxSpeed = 100f;
        public static float MaxCurrentSpeed = 100f;
        
        public static int LastSpeed = 0;
        public static float LastHealthSpeed = 0;
        public static float LastHealthEngSpeed = 0;

        public static VehicleInfoGlobalData CurrentVehicle = new VehicleInfoGlobalData();

        private static bool _vIsDisableControl = false;
        private static int _vehCountSave = 0;
        
        public Vehicle()
        {
            EventHandlers.Add("ARP:AddVehicleInfoGlobalDataList", new Action<dynamic>(AddVehicleInfoGlobalDataList));
            EventHandlers.Add("ARP:AddOneVehicleInfoGlobalDataList", new Action<dynamic>(AddOneVehicleInfoGlobalDataList));
            EventHandlers.Add("ARP:UpdateClientVehId", new Action<int, int>(UpdateClientVehId));
            EventHandlers.Add("ARP:UpdateClientVehInfo", new Action<int, float, float, float, float, float>(UpdateClientVehInfo));
            EventHandlers.Add("ARP:UpdateClientVehPark", new Action<int, float, float, float, float>(UpdateClientVehPark));
            EventHandlers.Add("ARP:SpawnClientVehicle", new Action<int>(SpawnClientVehicle));
            EventHandlers.Add("ARP:UpdateClientSellVehInfo", new Action<int, string, int>(UpdateClientSellVehInfo));
            EventHandlers.Add("ARP:EnableLeftIndicatorVehicle", new Action<int, bool>(EnableLeftIndicatorVehicle));
            EventHandlers.Add("ARP:EnableRightIndicatorVehicle", new Action<int, bool>(EnableRightIndicatorVehicle));
            EventHandlers.Add("ARP:SetSirenSoundVehicle", new Action<int, bool>(SetSirenSoundVehicle));
            EventHandlers.Add("ARP:RepairVehicle", new Action<int>(RepairVehicle));
            
            Tick += VehicleSpawner;
            Tick += VehicleChecker;
            Tick += VehicleCheckerSync;
            Tick += VehicleCheckerRot;
            Tick += VehicleCheckerTick;

            var rand = new Random();
            LoadSquare = 200f + rand.Next(150);
        }
        
        public static void UpdateClientVehId(int id, int netid)
        {
            if (VehicleInfoGlobalDataList.Count < 1) 
                return;
            
            if (HasVehicleId(id))
                VehicleInfoGlobalDataList[id].NetId = netid;
            
            Debug.WriteLine($"UPDATE NET ID: {netid} - {id}", "");
        }
        
        public static void UpdateClientVehInfo(int id, float fuel, float x, float y, float z, float rot)
        {
            bool isFind = false;
            foreach (var item in VehicleInfoGlobalDataList)
            {
                if (item.VehId == id)
                    isFind = true;
            }
            
            if (!isFind || VehicleInfoGlobalDataList.Count < 1)
                return;
            
            if (IsPedInAnyVehicle(PlayerPedId(), true))
            {
                var veh = GetVehiclePedIsUsing(PlayerPedId());
                if (GetPedInVehicleSeat(veh, -1) == PlayerPedId())
                {
                    if (VehicleInfoGlobalDataList[id].Number == GetVehicleNumber(veh))
                        return;
                }
            }

            VehicleInfoGlobalDataList[id].Fuel = fuel;
            VehicleInfoGlobalDataList[id].CurrentPosX = x;
            VehicleInfoGlobalDataList[id].CurrentPosY = y;
            VehicleInfoGlobalDataList[id].CurrentPosZ = z;
            VehicleInfoGlobalDataList[id].CurrentRotZ = rot;
        }
        
        public static void UpdateClientVehPark(int id, float x, float y, float z, float rot)
        {
            VehicleInfoGlobalDataList[id].x_park = x;
            VehicleInfoGlobalDataList[id].y_park = y;
            VehicleInfoGlobalDataList[id].z_park = z;
            VehicleInfoGlobalDataList[id].rot_park = rot;
        }
        
        public static void SpawnClientVehicle(int id)
        {
            SpawnVehicle(id);
        }
        
        public static int NetToVehicle(int entity)
        {
            return NetworkDoesEntityExistWithNetworkId(entity) ? NetToVeh(entity) : -1;
        }
        
        public static void AddOneVehicleInfoGlobalDataList(dynamic data)
        {
            var localData = (IDictionary<String, Object>) data;
            VehicleInfoGlobalData vehInfo = new VehicleInfoGlobalData();
            
            foreach (var property in typeof(VehicleInfoGlobalData).GetProperties())
                property.SetValue(vehInfo, localData[property.Name], null);
            
            VehicleInfoGlobalDataList.Add(vehInfo);
        }
        
        public static void AddVehicleInfoGlobalDataList(dynamic data)
        {
            Debug.WriteLine("START LOAD CARS");
            
            VehicleInfoGlobalDataList.Clear();
            
            var localData = (IList<Object>) data;

            foreach (var item in localData)
            {
                VehicleInfoGlobalData vehInfo = new VehicleInfoGlobalData();
                var localItem = (IDictionary<String, Object>) item;
                
                foreach (var property in typeof(VehicleInfoGlobalData).GetProperties())
                    property.SetValue(vehInfo, localItem[property.Name], null);
                
                VehicleInfoGlobalDataList.Add(vehInfo);
            }
            Debug.WriteLine($"FINISH LOAD CARS ({VehicleInfoGlobalDataList.Count})");
        }

        public static bool CanSpawn(VehicleInfoGlobalData vehData)
        {
            if (HasNumberOfStreamer(vehData.Number) || vehData.IsVisible == false) return false;
            
            var vehPos = new Vector3(vehData.CurrentPosX, vehData.CurrentPosY, vehData.CurrentPosZ);

            bool canSpawn = true;
            foreach (var v in Main.GetVehicleListOnRadius())
            {
                /*if (!NetworkDoesEntityExistWithNetworkId(vehData.NetId) && VehToNet(v.Handle) == vehData.NetId)
                    canSpawn = false;*/
                if (Main.GetDistanceToSquared(v.Position, vehPos) < 4f || GetVehicleNumber(v.Handle) == vehData.Number)
                    canSpawn = false;
            }

            return canSpawn;
        }

        public static async void SpawnVehicle(int id)
        {
            VehicleInfoGlobalData vehData = await GetAllData(id);
            //vehData = await GetAllData(id);

            await Delay(4000);
            uint vehicleHash = (uint) vehData.Hash;

            if (!CanSpawn(vehData))
                return;
                    
            if (!await Main.LoadModel(vehicleHash))
                return;
            
            var veh = CreateVehicle(vehicleHash, vehData.CurrentPosX, vehData.CurrentPosY, vehData.CurrentPosZ, vehData.CurrentRotZ, true, false);

            var vehicle = new CitizenFX.Core.Vehicle(veh);
            
            VehicleInfoGlobalDataList[vehData.VehId].NetId = VehToNet(veh);
            TriggerServerEvent("ARP:UpdateVehNetId", vehData.VehId, VehToNet(veh));
            
            SetEngineStatus(vehicle, vehData.EngineStatus);
            SetLockStatus(vehicle, vehData.LockStatus);
            
            ClearVehicleCustomPrimaryColour(vehicle.Handle);
            ClearVehicleCustomSecondaryColour(vehicle.Handle);
            SetVehicleLivery(vehicle.Handle, vehData.Livery);
            SetVehicleColours(vehicle.Handle, vehData.color1, vehData.color2);
            SetVehicleExtraColours(vehicle.Handle, 0, 0);
            
            SetVehicleNumberPlateText(vehicle.Handle, vehData.Number);
            SetVehicleNumberPlateTextIndex(vehicle.Handle, vehData.StyleNumber);

            SetVehicleHasBeenOwnedByPlayer(veh, true);
            SetNetworkIdCanMigrate(NetworkGetNetworkIdFromEntity(veh), true);
            //SetEntityAsMissionEntity(veh, true, true);
            
            //vehicle.MarkAsNoLongerNeeded();
            
            SetModelAsNoLongerNeeded((uint) vehicle.Model.Hash);
            
            Main.SaveLog("LSCTEST", "LSC TEST " + vehData.upgrade);

            if (vehData.upgrade != "")
                LoadCarMod(vehicle, vehData.upgrade);
            
            Debug.WriteLine($"SPAWNED: {vehData.VehId} - {vehData.Number} - {vehData.DisplayName} - {VehToNet(veh)}", "");
        }

        public static async void SpawnVehicleByVehData(VehicleInfoGlobalData vehData)
        {
            if (await Client.Sync.Data.Has(110000 + vehData.VehId, "sell_price"))
            {
                if ((int) await Client.Sync.Data.Get(110000 + vehData.VehId, "sell_price") > 0)
                {
                    Notification.SendWithTime("~r~Уберите транспорт с продажи в PREMIUM DELUXE MOTORSPORT");
                    return;
                }
            }
            
            vehData = await GetAllData(vehData.VehId);
            uint vehicleHash = (uint) vehData.Hash;
                    
            if (!await Main.LoadModel(vehicleHash))
                return;
            
            if (vehData.IsUserOwner)
            {
                vehData.CurrentPosX = vehData.x_park;
                vehData.CurrentPosY = vehData.y_park;
                vehData.CurrentPosZ = vehData.z_park;
                vehData.CurrentRotZ = vehData.rot_park;

                if (vehData.x_park == 0)
                {
                    vehData.CurrentPosX = vehData.x;
                    vehData.CurrentPosY = vehData.y;
                    vehData.CurrentPosZ = vehData.z;
                    vehData.CurrentRotZ = vehData.rot;
                }
            }
                    
            var veh = CreateVehicle(vehicleHash, vehData.CurrentPosX, vehData.CurrentPosY, vehData.CurrentPosZ, vehData.CurrentRotZ, true, false);

            var vehicle = new CitizenFX.Core.Vehicle(veh);
            
            VehicleInfoGlobalDataList[vehData.VehId].NetId = VehToNet(veh);
            TriggerServerEvent("ARP:UpdateVehNetId", vehData.VehId, VehToNet(veh));
            
            SetEngineStatus(vehicle, vehData.EngineStatus);
            SetLockStatus(vehicle, vehData.LockStatus);

            ClearVehicleCustomPrimaryColour(vehicle.Handle);
            ClearVehicleCustomSecondaryColour(vehicle.Handle);

            vehicle.BodyHealth = vehicle.BodyHealth - vehData.SBody * 200;
            vehicle.EngineHealth = vehicle.EngineHealth - vehData.SEngine * 20;
            for (int i = 0; i < vehicle.Wheels.Count; i++)
                SetVehicleWheelHealth(vehicle.Handle, i, vehData.SWhBkl * 200);
            
            if (vehData.SBody == 4)
                foreach (var door in vehicle.Doors)
                    door.Break();

            if (vehData.SSuspension > 3)
            {
                SetVehicleHandlingFloat(vehicle.Handle, "CHandlingData", "fTractionCurveMax", 0.9f);
                SetVehicleHandlingFloat(vehicle.Handle, "CHandlingData", "fSuspensionReboundDamp", 0.4f);
                SetVehicleHandlingFloat(vehicle.Handle, "CHandlingData", "fSuspensionCompDamp", 0.4f);
                
                /*
                SetVehicleHandlingFloat(vehicle.Handle, "CHandlingData", "fTractionCurveMax", 2.1f);
                SetVehicleHandlingFloat(vehicle.Handle, "CHandlingData", "fSuspensionReboundDamp", 0.7f);
                SetVehicleHandlingFloat(vehicle.Handle, "CHandlingData", "fSuspensionCompDamp", 0.7f);
                */
            }
            
            //if (vehData.Livery >= 0)
            SetVehicleLivery(vehicle.Handle, vehData.Livery);
            
            SetVehicleColours(vehicle.Handle, vehData.color1, vehData.color2);
            SetVehicleExtraColours(vehicle.Handle, 0, 0);
            
            SetVehicleNumberPlateText(vehicle.Handle, vehData.Number);
            SetVehicleNumberPlateTextIndex(vehicle.Handle, vehData.StyleNumber);
            
            SetVehicleHasBeenOwnedByPlayer(veh, true);
            SetNetworkIdCanMigrate(NetworkGetNetworkIdFromEntity(veh), true);
            //SetEntityAsMissionEntity(veh, true, true);
            
            //vehicle.MarkAsNoLongerNeeded();
            
            SetModelAsNoLongerNeeded((uint) vehicle.Model.Hash);

            if (vehData.ClassName == "Emergency")
            {
                SetVehicleModKit(vehicle.Handle, 0);
                SetVehicleMod(veh, 11, 2, false);
                SetVehicleMod(veh, 12, 2, false);
                SetVehicleMod(veh, 13, 3, false);
                SetVehicleMod(veh, 18, 0, false);
                SetVehicleMod(veh, 16, 2, false);
            }

            if (vehData.upgrade != "")
                LoadCarMod(vehicle, vehData.upgrade);
            
            Debug.WriteLine($"SPAWNED: {vehData.VehId} - {vehData.Number} - {vehData.DisplayName} - {VehToNet(veh)}", "");
        }

        public static async Task<CitizenFX.Core.Vehicle> SpawnByName(string name, Vector3 coords, float rot, bool userInCar = true)
        {
            var vehicleHash = (uint) GetHashKey(name);
               
            if (!await Main.LoadModel(vehicleHash))
                return null;
                    
            var veh = CreateVehicle(vehicleHash, coords.X, coords.Y, coords.Z + 1f, rot, true, false);
                    
            CitizenFX.Core.Vehicle vehicle = new CitizenFX.Core.Vehicle(veh)
            {
                IsEngineRunning = true
            };
                    
            if (userInCar)
                new CitizenFX.Core.Ped(PlayerPedId()).SetIntoVehicle(vehicle, VehicleSeat.Driver);
            
            SetVehicleOnGroundProperly(vehicle.Handle);
            //vehicle.MarkAsNoLongerNeeded();
            SetModelAsNoLongerNeeded((uint) vehicle.Model.Hash);
            return vehicle;
        }


        public static void Engine(CitizenFX.Core.Vehicle veh)
        {
            if (veh.EngineHealth > 950.0)
                veh.IsDriveable = veh.IsEngineRunning = !veh.IsEngineRunning;
            else if (veh.EngineHealth > 500.0)
            {
                var rand = new Random();
                if (rand.Next(2) == 0)
                    veh.IsDriveable = veh.IsEngineRunning = !veh.IsEngineRunning;
                else
                {
                    Chat.SendDoCommand("транспорт не заводится");
                    Notification.SendWithTime("~r~Проблемы с двигателем, вам нужен рем. комплект");
                    Notification.SendWithTime("~r~Либо попробуйте еще раз");
                }
            }
            else
            {
                Chat.SendDoCommand("транспорт не заводится");
                Notification.SendWithTime("~r~Проблемы с двигателем, вам нужен рем. комплект");
                Notification.SendWithTime("~r~Либо попробуйте еще раз");
            }
        }
        /*public static void EngineCityBee(CitizenFX.Core.Vehicle veh)
        {
            if (User.Data.phone_code == 0 || User.Data.phone == 0)
            {
                Notification.SendWithTime("~r~У Вас должен быть телефон");
                return;
            }
            if (veh.EngineHealth > 500.0)
                veh.IsDriveable = veh.IsEngineRunning = !veh.IsEngineRunning;
            
            else
            {
                Chat.SendDoCommand("транспорт не заводится");
                Notification.SendWithTime("~r~Проблемы с двигателем, вам нужен рем. комплект");
                return;
            }

            if (veh.IsEngineRunning)
            {
                Notification.SendWithTime("~b~Поездка началась");
                Notification.SendWithTime("~b~Тариф: ~w~10$");
            }
            else if (!veh.IsEngineRunning)
            {
                Notification.SendWithTime("~g~Поездка прекращена");
            }
        }*/
        


        public static int GetVehicleIdByNetId(int netid)
        {
            foreach (var item in VehicleInfoGlobalDataList)
                if (netid == item.NetId) return item.VehId;
            return -1;
        }

        public static int GetVehicleIdByNumber(string number)
        {
            number = number.Replace(" ", string.Empty);
            foreach (var item in VehicleInfoGlobalDataList)
                if (String.Equals(number, item.Number, StringComparison.CurrentCultureIgnoreCase)) return item.VehId;
            return -1;
        }

        public static string GetVehicleNumber(int handle)
        {
            if (!NetworkGetEntityIsNetworked(handle)) 
                return "NULLCAR___";
            
            string number = GetVehicleNumberPlateText(handle);
            if (IsStringNullOrEmpty(number))
                return "NULLCAR___";

            number = number.Replace(" ", string.Empty);
            return !IsStringNullOrEmpty(number) ? number : "NULLCAR___";
        }

        public static CitizenFX.Core.Vehicle GetVehicleByNumber(string number)
        {
            return Main.GetVehicleListOnRadius().FirstOrDefault(v => GetVehicleNumber(v.Handle) == number);
        }

        public static bool HasVehicleId(int vehId)
        {
            return VehicleInfoGlobalDataList.Any(item => vehId == item.VehId);
        }
        
        public static string GetTypeHealth(int id)
        {
            switch (id)
            {
                case 0:
                    return "~g~Отличное";
                case 1:
                    return "~g~Хорошее";
                case 2:
                    return "~y~Среднее";
                case 3:
                    return "~o~Плохое";
                default:
                    return "~r~Очень плохое";
            }
        }
        
        public static string CheckOil(float val)
        {
            switch (CheckOilInt(Convert.ToInt32(val)))
            {
                case 0:
                    return "~g~Замена не требуется";
            }
            return "~r~Требуется замена";
        }
        
        public static int CheckOilInt(int val)
        {
            return val > 10000 ? 1 : 0;
        }
        
        public static void LockStatus(CitizenFX.Core.Vehicle vehicle)
        {
            if (vehicle.LockStatus == VehicleLockStatus.Locked)
            {
                SetLockStatus(vehicle, false);
                Notification.SendWithTime("Вы ~g~открыли~w~ " + vehicle.DisplayName);
            }
            else
            {
                SetLockStatus(vehicle, true);
                Notification.SendWithTime("Вы ~r~закрыли~w~ " + vehicle.DisplayName);
            }
        }
        
        public static void EngineStatus(CitizenFX.Core.Vehicle vehicle)
        {
            if (vehicle.IsEngineRunning)
            {
                SetEngineStatus(vehicle, true);
                Notification.SendWithTime("Вы ~g~завели~w~ " + vehicle.DisplayName);
            }
            else
            {
                SetEngineStatus(vehicle, false);
                Notification.SendWithTime("Вы ~r~заглушили~w~ " + vehicle.DisplayName);
            }
        }
        
        public static void SetLockStatus(CitizenFX.Core.Vehicle vehicle, bool lockStatus)
        {
            vehicle.LockStatus = !lockStatus ? VehicleLockStatus.Unlocked : VehicleLockStatus.Locked;
        }
        
        public static void SetEngineStatus(CitizenFX.Core.Vehicle vehicle, bool engineStatus)
        {
            vehicle.IsDriveable = vehicle.IsEngineRunning = engineStatus;
        }
        
        public static void EnableDriveable(CitizenFX.Core.Vehicle vehicle, bool enable)
        {
            vehicle.IsDriveable = enable;
        }
        
        public static void Repair(CitizenFX.Core.Vehicle vehicle)
        {
            vehicle.Repair();
            vehicle.Health = 1000;
            Shared.TriggerEventToAllPlayers("ARP:RepairVehicle", VehToNet(vehicle.Handle));
        }
        
        private static void RepairVehicle(int netId)
        {
            if (!NetworkDoesEntityExistWithNetworkId(netId))
                return;
            var v = new CitizenFX.Core.Vehicle(NetToVehicle(netId));
            v.Repair();
            v.Health = 1000;
        }
        
        private static void AutoPenalty(int netId)
        {
            
        }
        
        public static void DisableSirenSound(CitizenFX.Core.Vehicle vehicle, bool disable)
        {
            DisableVehicleImpactExplosionActivation(vehicle.Handle, disable);
            Shared.TriggerEventToAllPlayers("ARP:SetSirenSoundVehicle", VehToNet(vehicle.Handle), disable);
        }
        
        private static void SetSirenSoundVehicle(int netId, bool disable)
        {
            if (!NetworkDoesEntityExistWithNetworkId(netId))
                return;
            var v = new CitizenFX.Core.Vehicle(NetToVehicle(netId));
            DisableVehicleImpactExplosionActivation(v.Handle, disable);
        }
        
        public static void EnableRightIndicator(CitizenFX.Core.Vehicle vehicle, bool enable)
        {
            SetVehicleIndicatorLights(vehicle.Handle, 0, enable);
            Shared.TriggerEventToAllPlayers("ARP:EnableRightIndicatorVehicle", VehToNet(vehicle.Handle), enable);
        }
        
        private static void EnableRightIndicatorVehicle(int netId, bool enable)
        {
            if (!NetworkDoesEntityExistWithNetworkId(netId))
                return;
            var v = new CitizenFX.Core.Vehicle(NetToVehicle(netId));
            SetVehicleIndicatorLights(v.Handle, 0, enable);
        }
        
        public static void EnableLeftIndicator(CitizenFX.Core.Vehicle vehicle, bool enable)
        {
            SetVehicleIndicatorLights(vehicle.Handle, 1, enable);
            Shared.TriggerEventToAllPlayers("ARP:EnableLeftIndicatorVehicle", VehToNet(vehicle.Handle), enable);
        }
        
        private static void EnableLeftIndicatorVehicle(int netId, bool enable)
        {
            if (!NetworkDoesEntityExistWithNetworkId(netId))
                return;
            var v = new CitizenFX.Core.Vehicle(NetToVehicle(netId));
            SetVehicleIndicatorLights(v.Handle, 1, enable);
        }
        
        public static async void LoadCarMod(CitizenFX.Core.Vehicle vehicle, string upgrade)
        {
            await Delay(5000);
            
            if (IsStringNullOrEmpty(upgrade))
                return;
            
            SetVehicleNeonLightEnabled(vehicle.Handle, 0, false);
            SetVehicleNeonLightEnabled(vehicle.Handle, 1, false);
            SetVehicleNeonLightEnabled(vehicle.Handle, 2, false);
            SetVehicleNeonLightEnabled(vehicle.Handle, 3, false);

            var vId = GetVehicleIdByNumber(GetVehicleNumberPlateText(vehicle.Handle));
            try
            {
                if (VehicleInfoGlobalDataList[vId].id_user > 0 && VehicleInfoGlobalDataList[vId].neon_type > 0)
                {
                    SetVehicleNeonLightsColour(vehicle.Handle, VehicleInfoGlobalDataList[vId].neon_r, VehicleInfoGlobalDataList[vId].neon_g, VehicleInfoGlobalDataList[vId].neon_b);
                    SetVehicleNeonLightEnabled(vehicle.Handle, 0, true);
                    SetVehicleNeonLightEnabled(vehicle.Handle, 1, true);
                    SetVehicleNeonLightEnabled(vehicle.Handle, 2, true);
                    SetVehicleNeonLightEnabled(vehicle.Handle, 3, true);
                }
            }
            catch (Exception e)
            {
                Main.SaveLog("LSCEX", $"NEON|{User.Data.rp_name} {e}");
                Debug.WriteLine("Error neon.");
            }
            
            SetVehicleModKit(vehicle.Handle, 0);
            try
            {
                for (int i = 0; i < 70; i++)
                    RemoveVehicleMod(vehicle.Handle, i);

                dynamic upgradeData = await Main.FromJson(upgrade);
                
                Debug.WriteLine(upgradeData.GetType().ToString());

                if (upgradeData.GetType().ToString() == "bool")
                {
                    LoadCarMod(vehicle, upgrade);
                    return;
                }
                
                foreach (var item in (IDictionary<String, Object>) upgradeData)
                {
                    if (item.Key == "Knife")
                        return;
                    if(Convert.ToInt32(item.Value) < 0) continue;
                    Debug.WriteLine($"LSC {Convert.ToInt32(item.Key)}:{Convert.ToInt32(item.Value)}");
                    SetVehicleMod(vehicle.Handle, Convert.ToInt32(item.Key), Convert.ToInt32(item.Value), false);
                    
                    if (Convert.ToInt32(item.Key) == 69)
                        SetVehicleWindowTint(vehicle.Handle, Convert.ToInt32(item.Value));
                }
            }
            catch(Exception e)
            {
                Main.SaveLog("LSCEX", $"{User.Data.rp_name} {e}");
                Debug.WriteLine("Error tunning.");
                LoadCarMod(vehicle, upgrade);
                
                for (int i = 0; i < 70; i++)
                    RemoveVehicleMod(vehicle.Handle, i);
            }
        }

        public static void UpdateClientSellVehInfo(int id, string userName, int userId)
        {
            if (userId == 0)
                VehicleInfoGlobalDataList[id] = new VehicleInfoGlobalData {IsVisible = false};
            else
            {
                VehicleInfoGlobalDataList[id].id_user = userId;
                VehicleInfoGlobalDataList[id].user_name = userName;
            }
        }

        public static VehicleInfoGlobalData GetVehicleById(int id)
        {
            return VehicleInfoGlobalDataList.FirstOrDefault(item => item.id == id);
        }

        public static async void SwitchAutopilot(CitizenFX.Core.Vehicle veh)
        {
            if (World.WaypointPosition.X == 0 && World.WaypointPosition.Y == 0)
            {
                Notification.SendWithTime("~r~Для начала установите маркер на карте");
                return;
            }
            
            if (!Client.Sync.Data.HasLocally(User.GetServerId(), "autopilot"))
            {
                TaskVehicleDriveToCoordLongrange(GetPlayerPed(-1), veh.Handle, World.WaypointPosition.X,
                    World.WaypointPosition.Y, World.GetGroundHeight(World.WaypointPosition), AutoPilotSpeed, 786603,
                    20.0f);
                
                /*await Delay(5000);
                
                TaskVehicleDriveToCoord(GetPlayerPed(-1), veh.Handle, World.WaypointPosition.X,
                    World.WaypointPosition.Y,
                    World.GetGroundHeight(World.WaypointPosition), AutoPilotSpeed, 1, 0, 786603, 1.0f, 1);*/

                Client.Sync.Data.SetLocally(User.GetServerId(), "autopilot", true);
                Notification.SendWithTime("~g~Автопилот активирован");
            }
            else
            {
                ClearPedTasks(GetPlayerPed(-1));
                Client.Sync.Data.ResetLocally(User.GetServerId(), "autopilot");
                Notification.SendWithTime("~g~Автопилот деактивирован");
            }
        }

        public static bool IsValidVehicle(int veh)
        {
            var model = (uint) GetEntityModel(veh);
            return IsThisModelACar(model) || IsThisModelABike(model) || IsThisModelAQuadbike(model);
        }

        public static void CheckVehicleDamage()
        {
            var ped = GetPlayerPed(-1);
            if (!DoesEntityExist(ped) || User.IsDead()) return;
            if (!IsPedSittingInAnyVehicle(ped)) return;
            var vehicle = GetVehiclePedIsIn(ped, false);
            var v = new CitizenFX.Core.Vehicle(vehicle);
            
            if (LastSpeed > 190 && LastSpeed > UI.GetCurrentSpeed() + 120f)
            {
                SetEngineStatus(v, false);
                v.EngineHealth = 100;
                
                foreach (var item in VehicleInfoGlobalDataList)
                {
                    if (item.Number != GetVehicleNumber(vehicle)) continue;
                    VehicleInfoGlobalDataList[item.VehId].SBody = 4;
                    VehicleInfoGlobalDataList[item.VehId].SEngine = 4;
                    VehicleInfoGlobalDataList[item.VehId].SSuspension = 4;
                    VehicleInfoGlobalDataList[item.VehId].SWhBkl = 4;
                }
                
                SetEntityHealth(GetPlayerPed(-1), 0);
            }
            else if (LastSpeed > 130 && LastSpeed > UI.GetCurrentSpeed() + 100f)
            {
                SetEngineStatus(v, false);
                v.EngineHealth = 300;
                SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) - 80);
                
                var rand = new Random();
                foreach (var item in VehicleInfoGlobalDataList)
                {
                    if (item.Number != GetVehicleNumber(vehicle)) continue;
                    
                    if (rand.Next(3) == 0 && VehicleInfoGlobalDataList[item.VehId].SBody < 1)
                        VehicleInfoGlobalDataList[item.VehId].SBody += 1;
                    if (rand.Next(6) == 0 && VehicleInfoGlobalDataList[item.VehId].SEngine < 1)
                        VehicleInfoGlobalDataList[item.VehId].SEngine += 1;
                    if (rand.Next(6) == 0 && VehicleInfoGlobalDataList[item.VehId].SSuspension < 1)
                        VehicleInfoGlobalDataList[item.VehId].SSuspension += 1;
                    if (rand.Next(3) == 0 && VehicleInfoGlobalDataList[item.VehId].SWhBkl < 1)
                        VehicleInfoGlobalDataList[item.VehId].SWhBkl += 1;
                }
            }
            else if (LastSpeed > 130 && LastSpeed > UI.GetCurrentSpeed() + 50f)
            {
                if (LastHealthSpeed > v.BodyHealth + 50)
                {
                    SetEngineStatus(v, false);
                }
                if (LastHealthSpeed > v.BodyHealth + 130)
                {
                    SetEngineStatus(v, false);
                    v.EngineHealth = v.EngineHealth - 300;
                    SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) - 30);
                }
                    
                var rand = new Random();
                foreach (var item in VehicleInfoGlobalDataList)
                {
                    if (item.Number != GetVehicleNumber(vehicle)) continue;
                    
                    if (rand.Next(4) == 0 && VehicleInfoGlobalDataList[item.VehId].SBody < 1)
                        VehicleInfoGlobalDataList[item.VehId].SBody += 1;
                    if (rand.Next(10) == 0 && VehicleInfoGlobalDataList[item.VehId].SEngine < 1)
                        VehicleInfoGlobalDataList[item.VehId].SEngine += 1;
                    if (rand.Next(10) == 0 && VehicleInfoGlobalDataList[item.VehId].SSuspension < 1)
                        VehicleInfoGlobalDataList[item.VehId].SSuspension += 1;
                    if (rand.Next(4) == 0 && VehicleInfoGlobalDataList[item.VehId].SWhBkl < 1)
                        VehicleInfoGlobalDataList[item.VehId].SWhBkl += 1;
                }
            }
            else if (LastSpeed > 100)
            {
                if (LastHealthSpeed > v.BodyHealth + 50)
                {
                    SetEngineStatus(v, false);
                }
                if (LastHealthSpeed > v.BodyHealth + 100)
                {
                    SetEngineStatus(v, false);
                    v.EngineHealth = v.EngineHealth - 120;
                }
            }

            LastSpeed = UI.GetCurrentSpeed();
            LastHealthSpeed = v.BodyHealth;
        }

        public static async Task<VehicleInfoGlobalData> GetAllData(int id)
        {
            try
            {
                var data = await Client.Sync.Data.GetAll(110000 + id);
                if (data == null) return new VehicleInfoGlobalData();

                var vData = new VehicleInfoGlobalData();
                var localData = (IDictionary<String, Object>) data;
                foreach (var property in typeof(VehicleInfoGlobalData).GetProperties())
                    property.SetValue(vData, localData[property.Name], null);

                foreach (var item in VehicleInfoGlobalDataList) {
                    if (item.VehId == vData.VehId)
                    {
                        VehicleInfoGlobalDataList[item.VehId].Fuel = vData.Fuel;
                        VehicleInfoGlobalDataList[item.VehId].SBody = vData.SBody;
                        VehicleInfoGlobalDataList[item.VehId].SCandle = vData.Fuel;
                        VehicleInfoGlobalDataList[item.VehId].SEngine = vData.SEngine;
                        VehicleInfoGlobalDataList[item.VehId].SMp = vData.Fuel;
                        VehicleInfoGlobalDataList[item.VehId].SOil = vData.Fuel;
                        VehicleInfoGlobalDataList[item.VehId].SSuspension = vData.SSuspension;
                        VehicleInfoGlobalDataList[item.VehId].SWhBkl = vData.SWhBkl;
                        VehicleInfoGlobalDataList[item.VehId].IsUserOwner = vData.IsUserOwner;
                        VehicleInfoGlobalDataList[item.VehId].user_name = vData.user_name;
                        VehicleInfoGlobalDataList[item.VehId].id_user = vData.id_user;
                        VehicleInfoGlobalDataList[item.VehId].upgrade = vData.upgrade;
                    }
                }
                
                return vData;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"VEHICLE DATA: {e}");
                throw;
            }
            return new VehicleInfoGlobalData();
        }

        public static async void SellCar(int carId)
        {
            if (Screen.LoadingPrompt.IsActive)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_34"));
                return;
            }
            
            Client.Sync.Data.ShowSyncMessage = false;
            CitizenFX.Core.UI.Screen.LoadingPrompt.Show("Обработка запроса, подождите");
            
            var vehItem = GetVehicleById(carId);
            
            if (await Client.Sync.Data.Has(110000 + vehItem.VehId, "sell_price"))
            {
                if ((int) await Client.Sync.Data.Get(110000 + vehItem.VehId, "sell_price") > 0)
                {
                    Notification.SendWithTime("~r~Уберите транспорт с продажи в PREMIUM DELUXE MOTORSPORT");
                    return;
                }
            }

            if (!vehItem.IsVisible)
            {
                Notification.SendWithTime("~r~Уберите транспорт с продажи в PREMIUM DELUXE MOTORSPORT");
                Client.Sync.Data.ShowSyncMessage = true;
                CitizenFX.Core.UI.Screen.LoadingPrompt.Hide();
                return;
            }
            
            await User.GetAllData();
            await Delay(2000);

            if (!await Client.Sync.Data.Has(110000 + vehItem.VehId, "id"))
            {
                Notification.SendWithTime("~r~Ошибка обработки запроса");
                Client.Sync.Data.ShowSyncMessage = true;
                CitizenFX.Core.UI.Screen.LoadingPrompt.Hide();
                return;
            }
            
            var vData = await GetAllData(vehItem.VehId);
            var playerId = User.GetServerId();
            
            if (vData.id_user != User.Data.id || vData.Hash == 0) {
                Notification.SendWithTime("~r~Ошибка обработки запроса");
                Client.Sync.Data.ShowSyncMessage = true;
                CitizenFX.Core.UI.Screen.LoadingPrompt.Hide();
                return;
            }

            if (User.Data.car_id1 == carId)
            {
                Client.Sync.Data.Set(playerId, "car_id1", 0);
                await Delay(200);
                if (await Client.Sync.Data.Get(playerId, "car_id1") != 0)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_32"));
                    return;
                }
            }

            if (User.Data.car_id2 == carId)
            {
                Client.Sync.Data.Set(playerId, "car_id2", 0);
                await Delay(200);
                if (await Client.Sync.Data.Get(playerId, "car_id2") != 0)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_32"));
                    return;
                }
            }

            if (User.Data.car_id3 == carId)
            {
                Client.Sync.Data.Set(playerId, "car_id3", 0);
                await Delay(200);
                if (await Client.Sync.Data.Get(playerId, "car_id3") != 0)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_32"));
                    return;
                }
            }

            if (User.Data.car_id4 == carId)
            {
                Client.Sync.Data.Set(playerId, "car_id4", 0);
                await Delay(200);
                if (await Client.Sync.Data.Get(playerId, "car_id4") != 0)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_32"));
                    return;
                }
            }

            if (User.Data.car_id5 == carId)
            {
                Client.Sync.Data.Set(playerId, "car_id5", 0);
                await Delay(200);
                if (await Client.Sync.Data.Get(playerId, "car_id5") != 0)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_32"));
                    return;
                }
            }

            if (User.Data.car_id6 == carId)
            {
                Client.Sync.Data.Set(playerId, "car_id6", 0);
                await Delay(200);
                if (await Client.Sync.Data.Get(playerId, "car_id6") != 0)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_32"));
                    return;
                }
            }

            if (User.Data.car_id7 == carId)
            {
                Client.Sync.Data.Set(playerId, "car_id7", 0);
                await Delay(200);
                if (await Client.Sync.Data.Get(playerId, "car_id7") != 0)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_32"));
                    return;
                }
            }

            if (User.Data.car_id8 == carId)
            {
                Client.Sync.Data.Set(playerId, "car_id8", 0);
                await Delay(200);
                if (await Client.Sync.Data.Get(playerId, "car_id8") != 0)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_32"));
                    return;
                }
            }
            
            await User.GetAllData();
            
            var nalog = vData.price * (100 - (Coffer.GetNalog() + 20)) / 100;

            var vh = GetVehicleByNumber(vData.Number);
            if (vh != null)
                vh.Delete();
            
            if (NetworkDoesEntityExistWithNetworkId(vData.NetId))
            {
                var vh1 = new CitizenFX.Core.Vehicle(NetToVehicle(vData.NetId));
                vh1.Delete();
            }
            
            User.AddMoney(nalog);
            Coffer.RemoveMoney(nalog);
            
            Notification.SendWithTime($"Налог: ~g~{(Coffer.GetNalog() + 20)}%\n~s~Получено: ~g~${nalog:#,#}" );
            TriggerServerEvent("ARP:UpdateSellCarInfo", "", 0, carId);
            
            Main.SaveLog("SellCar", $"ID: {vData.id}, NAME: {vData.DisplayName}, PRICE: {vData.price}, USERNAME: {User.Data.rp_name}");

            User.SaveAccount();
            MenuList.HideMenu();
            
            await Delay(10000);
            await User.GetAllData();
            
            Client.Sync.Data.ShowSyncMessage = true;
            CitizenFX.Core.UI.Screen.LoadingPrompt.Hide();
        }

        public static void SellToUser(int serverId, int vId, string number, string displayName, int price)
        {
            Shared.TriggerEventToPlayer(serverId, "ARP:SellVehicleToUserShowMenu", User.GetServerId(), vId, number, displayName, price);
            Notification.SendWithTime("~g~Вы предложили игроку купить транспорт");
        }

        public static async void CheckSellToUser(int serverId, int vId, int price)
        {
            if (price > User.GetMoneyWithoutSync())
            {
                Notification.SendWithTime("~g~У Вас нет достаточной суммы");
                return;
            }

            if (User.Data.car_id1 == 0)
            {
                AcceptBuyCar(serverId, vId, price);
                return;
            }

            if (User.Data.car_id2 == 0)
            {
                if (User.Data.id_house > 0 || User.Data.apartment_id > 0 || User.Data.condo_id > 0)
                {
                    AcceptBuyCar(serverId, vId, price);
                    return;
                }
                Notification.SendWithTime("~r~Нужно иметь дом или квартиру");
                return;
            }

            if (User.Data.car_id3 == 0)
            {
                if (User.Data.id_house > 0)
                {
                    var hData = await House.GetAllData(User.Data.id_house);
                    if (hData.price >= 1000000)
                    {
                        AcceptBuyCar(serverId, vId, price);
                        return;
                    }
                    Notification.SendWithTime("~r~Стоимость дома должна быть выше $1,000,000");
                    return;
                }
                Notification.SendWithTime("~r~Нужно иметь дом");
                return;
            }

            if (User.Data.car_id4 == 0)
            {
                if (User.Data.id_house > 0)
                {
                    var hData = await House.GetAllData(User.Data.id_house);
                    if (hData.price >= 2500000)
                    {
                        AcceptBuyCar(serverId, vId, price);
                        return;
                    }
                    Notification.SendWithTime("~r~Стоимость дома должна быть выше $2,500,000");
                    return;
                }
                Notification.SendWithTime("~r~Нужно иметь дом");
                return;
            }

            if (User.Data.car_id5 == 0)
            {
                if (User.Data.id_house > 0)
                {
                    var hData = await House.GetAllData(User.Data.id_house);
                    if (hData.price >= 5000000)
                    {
                        AcceptBuyCar(serverId, vId, price);
                        return;
                    }
                    Notification.SendWithTime("~r~Стоимость дома должна быть выше $5,000,000");
                    return;
                }
                Notification.SendWithTime("~r~Нужно иметь дом");
                return;
            }

            if (User.Data.car_id6 == 0 || User.Data.car_id7 == 0 || User.Data.car_id8 == 0)
            {
                Notification.SendWithTime("~r~6, 7, 8 слот доступны за Appi Coins");
                return;
            }
        }

        public static async void AcceptBuyCar(int serverId, int vId, int price)
        {
            if (User.Data.car_id1 == 0)
            {
                Client.Sync.Data.Set(User.GetServerId(), "car_id1", vId);
                User.Data.car_id1 = vId;
            }
            else if (User.Data.car_id2 == 0)
            {
                Client.Sync.Data.Set(User.GetServerId(), "car_id2", vId);
                User.Data.car_id2 = vId;
            }
            else if (User.Data.car_id3 == 0)
            {
                Client.Sync.Data.Set(User.GetServerId(), "car_id3", vId);
                User.Data.car_id3 = vId;
            }
            else if (User.Data.car_id4 == 0)
            {
                Client.Sync.Data.Set(User.GetServerId(), "car_id4", vId);
                User.Data.car_id4 = vId;
            }
            else if (User.Data.car_id5 == 0)
            {
                Client.Sync.Data.Set(User.GetServerId(), "car_id5", vId);
                User.Data.car_id5 = vId;
            }
            else if (User.Data.car_id6 == 0)
            {
                Client.Sync.Data.Set(User.GetServerId(), "car_id6", vId);
                User.Data.car_id6 = vId;
            }
            else if (User.Data.car_id7 == 0)
            {
                Client.Sync.Data.Set(User.GetServerId(), "car_id7", vId);
                User.Data.car_id7 = vId;
            }
            else if (User.Data.car_id8 == 0)
            {
                Client.Sync.Data.Set(User.GetServerId(), "car_id8", vId);
                User.Data.car_id8 = vId;
            }
            
            //await User.GetAllData();
            
            TriggerServerEvent("ARP:UpdateSellCarInfo", User.Data.rp_name, User.Data.id, vId);
            
            User.RemoveMoney(price);
            Shared.TriggerEventToPlayer(serverId, "ARP:AcceptSellToUser", User.GetServerId(), vId, price);
            User.SaveAccount();
            
            Main.SaveLog("SellCarToUser", $"ID: {vId}, BUY NAME: {User.Data.rp_name}, PRICE: {price}");
            
            Notification.SendWithTime("~g~Вы купили транспорт");
        }

        public static void UpdateVehicleNumber(int serverId, int vId, int vHandle, string newNumber)
        {
            if (serverId == User.GetServerId())
            {
                foreach (var v in Main.GetVehicleListOnRadius())
                {
                    if (v.Handle == vHandle)
                        SetVehicleNumberPlateText(vHandle, newNumber);
                }
            }
            
            try
            {
                VehicleInfoGlobalDataList[vId].Number = newNumber;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                throw;
            }
        }

        public static async void AcceptSellToUser(int serverId, int vId, int price)
        {
            if (User.Data.car_id1 == vId)
            {
                Client.Sync.Data.Set(User.GetServerId(), "car_id1", 0);
                User.Data.car_id1 = 0;
            }
            else if (User.Data.car_id2 == vId)
            {
                Client.Sync.Data.Set(User.GetServerId(), "car_id2", 0);
                User.Data.car_id2 = 0;
            }
            else if (User.Data.car_id3 == vId)
            {
                Client.Sync.Data.Set(User.GetServerId(), "car_id3", 0);
                User.Data.car_id3 = 0;
            }
            else if (User.Data.car_id4 == vId)
            {
                Client.Sync.Data.Set(User.GetServerId(), "car_id4", 0);
                User.Data.car_id4 = 0;
            }
            else if (User.Data.car_id5 == vId)
            {
                Client.Sync.Data.Set(User.GetServerId(), "car_id5", 0);
                User.Data.car_id5 = 0;
            }
            else if (User.Data.car_id6 == vId)
            {
                Client.Sync.Data.Set(User.GetServerId(), "car_id6", 0);
                User.Data.car_id6 = 0;
            }
            else if (User.Data.car_id7 == vId)
            {
                Client.Sync.Data.Set(User.GetServerId(), "car_id7", 0);
                User.Data.car_id7 = 0;
            }
            else if (User.Data.car_id8 == vId)
            {
                Client.Sync.Data.Set(User.GetServerId(), "car_id8", 0);
                User.Data.car_id8 = 0;
            }
            
            //await User.GetAllData();
            
            Main.SaveLog("SellCarToUser", $"ID: {vId}, SELL NAME: {User.Data.rp_name}, PRICE: {price}");
            
            User.AddMoney(price);
            User.SaveAccount();
            
            Notification.SendWithTime("~g~Вы продали свой транспорт");
        }

        public static bool HasNumberOfStreamer(string number)
        {
            return Main.GetVehicleListOnRadius().Any(v => GetVehicleNumber(v.Handle) == number);
        }

        public static void FindDobule(string number, int hash)
        {
            int i = 0;

            foreach (var v in Main.GetVehicleListOnRadius())
            {
                if (GetVehicleNumber(v.Handle) != number && v.Model.Hash != hash) continue;
                i++;
                if (i > 1)
                    v.Delete();
            }
        }
        
        private static async Task VehicleSpawner()
        {
            /*await Delay(10000);
            
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            
            foreach (var item in VehicleInfoGlobalDataList)
            {
                if (!(Main.GetDistanceToSquared(new Vector3(item.CurrentPosX, item.CurrentPosY, item.CurrentPosZ), playerPos) < LoadSquare)) continue;
                
                if (item.NetId != -1)
                {
                    if (NetworkDoesEntityExistWithNetworkId(item.NetId) || HasNumberOfStreamer(item.Number)) continue;
                    TriggerServerEvent("ARP:SpawnServerVehicle", item.VehId, GetPlayerServerId(PlayerId()));
                }
                else
                    TriggerServerEvent("ARP:SpawnServerVehicle", item.VehId, GetPlayerServerId(PlayerId()));

                //FindDobule(item.Number, item.Hash);
            }*/
        }
        
        private static async Task VehicleChecker()
        {
            await Delay(1000);
            
            _vIsDisableControl = CheckIsDisableControl();
            
            if (IsPedInAnyVehicle(PlayerPedId(), true))
            {
                var veh = GetVehiclePedIsUsing(PlayerPedId());
                if (GetPedInVehicleSeat(veh, -1) == PlayerPedId())
                {
                    var vehicle = new CitizenFX.Core.Vehicle(veh);

                    //SetEntityAsMissionEntity(veh, true, true);
                    foreach (var item in VehicleInfoGlobalDataList)
                    {
                        if (item.Number != GetVehicleNumber(veh)) continue;
                        
                        if (item.IsUserOwner)
                        {
                            VehicleInfoGlobalDataList[item.VehId].CurrentPosX = vehicle.Position.X;
                            VehicleInfoGlobalDataList[item.VehId].CurrentPosY = vehicle.Position.Y;
                            VehicleInfoGlobalDataList[item.VehId].CurrentPosZ = vehicle.Position.Z;
                            VehicleInfoGlobalDataList[item.VehId].CurrentRotZ = vehicle.Heading;
                        }
                        
                        if (vehicle.IsEngineRunning == false) continue;
                        if (VehicleInfoGlobalDataList[item.VehId].Fuel <= 0)
                        {
                            vehicle.FuelLevel = 0;
                            vehicle.IsEngineRunning = false;
                            Client.Sync.Data.Set(110000 + item.VehId, "EngineStatus", false);
                            continue; 
                        }
                        
                        var velocity = GetEntityVelocity(veh);
                        var speed = Math.Sqrt(velocity.X * velocity.X +
                                              velocity.Y * velocity.Y +
                                              velocity.Z * velocity.Z);
                        
                        var speedMph = Math.Round(speed * 2.23693629);
             
        
                        var fuelMinute = VehicleInfoGlobalDataList[item.VehId].FuelMinute;
                        var fuel = VehicleInfoGlobalDataList[item.VehId].Fuel;
        
                        if (speedMph < 1)
                        {
                            var result = fuel - fuelMinute * 0.01 / 300;
                            VehicleInfoGlobalDataList[item.VehId].Fuel = (float) (result < 1 ? 0 : result);
                        }
                        else if (speedMph > 0 && speedMph < 61)
                        {
                            var result = fuel - fuelMinute * 1.5 / 710;
                            VehicleInfoGlobalDataList[item.VehId].Fuel = (float) (result < 1 ? 0 : result);
                        }
                        else if (speedMph > 60 && speedMph < 101)
                        {
                            var result = fuel - fuelMinute * 0.75 / 480;
                            VehicleInfoGlobalDataList[item.VehId].Fuel = (float) (result < 1 ? 0 : result);
                        }
                        else
                        {
                            var result = fuel - fuelMinute * 0.75 / 240;
                            VehicleInfoGlobalDataList[item.VehId].Fuel = (float) (result < 1 ? 0 : result);
                        }

                        vehicle.FuelLevel = VehicleInfoGlobalDataList[item.VehId].Fuel;
                        
                        float mpSec = (float) (speedMph / 3600);
                        VehicleInfoGlobalDataList[item.VehId].SOil += mpSec;
                        VehicleInfoGlobalDataList[item.VehId].SCandle += mpSec;
                        VehicleInfoGlobalDataList[item.VehId].SMp += mpSec;
                    }
                }
            }
        }
        
        private static async Task VehicleCheckerSync()
        {
            await Delay(10000);
            
            if (IsPedInAnyVehicle(PlayerPedId(), true))
            {
                var veh = GetVehiclePedIsUsing(PlayerPedId());
                if (GetPedInVehicleSeat(veh, -1) == PlayerPedId())
                {
                    foreach (var item in VehicleInfoGlobalDataList)
                    {
                        if (item.Number != GetVehicleNumber(veh)) continue;
                        var vehicle = new CitizenFX.Core.Vehicle(veh);

                        if (item.SEngine == 4 && UI.GetCurrentSpeed() > 40)
                        {
                            if (!vehicle.IsEngineRunning) continue;
                            vehicle.IsEngineRunning = false;
                            Client.Sync.Data.Set(110000 + item.VehId, "EngineStatus", false);
                            Notification.SendWithTime("~r~Транспорт заглох");
                            continue; 
                        }

                        var rand = new Random();

                        if (item.SEngine > 2)
                        {
                            if (rand.Next(5) <= item.SEngine)
                            {
                                if (!vehicle.IsEngineRunning) continue;
                                vehicle.IsEngineRunning = false;
                                Client.Sync.Data.Set(110000 + item.VehId, "EngineStatus", false);
                                Notification.SendWithTime("~r~Транспорт заглох");
                                continue; 
                            }
                        }
                       
                        if (item.SOil > 15000)
                        {
                            if (rand.Next(5 - item.SEngine) == 0)
                            {
                                if (!vehicle.IsEngineRunning) continue;
                                vehicle.IsEngineRunning = false;
                                Client.Sync.Data.Set(110000 + item.VehId, "EngineStatus", false);
                                Notification.SendWithTime("~r~Транспорт заглох");
                                continue; 
                            }

                            if (item.SEngine < 4 && rand.Next(10) == 0 && UI.GetCurrentSpeed() > 70)
                            {
                                item.SEngine++;
                                Client.Sync.Data.Set(110000 + item.VehId, "SEngine", item.SEngine);
                                VehicleInfoGlobalDataList[item.VehId].SEngine = item.SEngine;
                            }
                        }
                        
                        if (item.SSuspension > 3)
                        {
                            SetVehicleHandlingFloat(vehicle.Handle, "CHandlingData", "fTractionCurveMax", 0.9f);
                            SetVehicleHandlingFloat(vehicle.Handle, "CHandlingData", "fSuspensionReboundDamp", 0.4f);
                            SetVehicleHandlingFloat(vehicle.Handle, "CHandlingData", "fSuspensionCompDamp", 0.4f);
                        }
                        
                        if (item.IsUserOwner)
                        {
                            Client.Sync.Data.Set(110000 + item.VehId, "CurrentPosX", vehicle.Position.X);
                            Client.Sync.Data.Set(110000 + item.VehId, "CurrentPosY", vehicle.Position.Y);
                            Client.Sync.Data.Set(110000 + item.VehId, "CurrentPosZ", vehicle.Position.Z);
                            
                            Client.Sync.Data.Set(110000 + item.VehId, "SBody", VehicleInfoGlobalDataList[item.VehId].SBody);
                            Client.Sync.Data.Set(110000 + item.VehId, "SCandle", VehicleInfoGlobalDataList[item.VehId].SCandle);
                            Client.Sync.Data.Set(110000 + item.VehId, "SEngine", VehicleInfoGlobalDataList[item.VehId].SEngine);
                            Client.Sync.Data.Set(110000 + item.VehId, "SMp", VehicleInfoGlobalDataList[item.VehId].SMp);
                            Client.Sync.Data.Set(110000 + item.VehId, "SSuspension", VehicleInfoGlobalDataList[item.VehId].SSuspension);
                            Client.Sync.Data.Set(110000 + item.VehId, "SOil", VehicleInfoGlobalDataList[item.VehId].SOil);
                            Client.Sync.Data.Set(110000 + item.VehId, "SWhBkl", VehicleInfoGlobalDataList[item.VehId].SWhBkl);
                            
                            _vehCountSave++;
                            if (_vehCountSave > 2)
                            {
                                _vehCountSave = 0;
                                TriggerServerEvent("ARP:SaveVehicle", item.VehId);
                            }
                        }
                        
                        Client.Sync.Data.Set(110000 + item.VehId, "Fuel", VehicleInfoGlobalDataList[item.VehId].Fuel);
                    }
                }
            }
        }
        
        public static bool CheckIsDisableControl()
        {
            var ped = GetPlayerPed(-1);
            if (!DoesEntityExist(ped) || User.IsDead()) return false;
            if (!IsPedSittingInAnyVehicle(ped)) return false;
            var vehicle = GetVehiclePedIsIn(ped, false);
            if (GetPedInVehicleSeat(vehicle, -1) != ped) return false;
            var v = new CitizenFX.Core.Vehicle(vehicle); 
            string className = VehInfo.GetClassName(v.Model.Hash);
            if (className == "Helicopters" || className == "Planes" || className == "Boats" ||
                className == "Motorcycles" || className == "Cycles") return false;
            return v.Rotation.X > 90 || v.Rotation.X < -90 || v.Rotation.Y > 90 || v.Rotation.Y < -90 || v.IsInAir;
        }
        
        private static async Task VehicleCheckerTick()
        {
            await Delay(1200);
            CheckVehicleDamage();
        }
        
        private static async Task VehicleCheckerRot()
        {
            if (_vIsDisableControl)
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
                //DisableControlAction(0, 75, true); //disable exit vehicle
                //DisableControlAction(27, 75, true); //disable exit vehicle
                DisableControlAction(0, 32, true); //move (w)
                DisableControlAction(0, 34, true); //move (a)
                DisableControlAction(0, 33, true); //move (s)
                DisableControlAction(0, 35, true); //move (d)
                DisableControlAction(0, 59, true);
                DisableControlAction(0, 60, true);
            }
        }
    }
}