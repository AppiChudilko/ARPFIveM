using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

/*
public class LspdUnitTypes
{
    public static int Standart => 0;
    public static int HightWay => 1;
    public static int Detective => 2;
    public static int Fib => 3;
    public static int Swat => 4;
    public static int Hrt => 5;
    public static int ParkRanger => 6;
    public static int Sheriff => 7;
    public static int AirSupportDivision => 8;
}
*/

namespace Client.Managers
{
    public class PedAi : BaseScript
    {
        public static Vector3 OutPos;
        public static int PedFind = -1;

        public static int[] VehiclesStandart = {-1627000575, 2046537925, 1977528411, 1977528411, -595004596, 2046537925, -1973172295, -2046537925, -2046537925 };
        public static string[] PedStandart = {"s_f_y_cop_01", "s_m_y_cop_01", "s_m_y_cop_01", "s_m_y_cop_01", "s_m_y_cop_01" };
        
        public static int[] VehiclesHightWay = {1912215274, 1912215274, 1912215274, 1912215274 };
        public static string[] PedHighWay = {"s_m_y_hwaycop_01", "s_m_y_hwaycop_01", "s_m_y_hwaycop_01", "s_m_y_hwaycop_01", "s_m_y_hwaycop_01" };
        
        public static int[] VehiclesSheriff = {-1683328900, 1922257928, -1683328900, 1922257928 };
        public static string[] PedSheriff = {"s_f_y_sheriff_01", "s_m_y_sheriff_01", "s_m_y_sheriff_01", "s_m_y_sheriff_01", "s_m_y_sheriff_01" };
        
        public static int[] VehiclesDetective = {-595004596, 1127131465, -1973172295, -1973172295, -1973172295 };
        public static string[] PedDetective = {"cs_fbisuit_01", "cs_fbisuit_01" };
        
        public static int[] VehiclesFib = {-595004596, 1127131465, -1973172295, -1647941228, -1647941228 };
        public static string[] PedFib = {"s_m_m_fibsec_01", "s_m_m_fibsec_01", "s_m_m_fibsec_01", "s_m_m_fiboffice_01", "s_m_m_fiboffice_02", "s_m_m_fiboffice_02" };
        
        public static int[] VehiclesSwat = {-1205689942, 456714581, 456714581, 456714581 };
        public static string[] PedSwat = {"s_m_y_swat_01", "s_m_y_swat_01" };
        
        public static int[] VehiclesMedic = {1171614426, 1171614426 };
        public static string[] PedMedic = {"s_m_m_paramedic_01", "s_m_m_paramedic_01" };
        
        public static int[] VehiclesBallas = {-682211828, -1790546981, -1743316013, 2006918058, 525509695, -682211828, -682211828 };
        public static string[] PedBallas = {"g_m_y_ballaeast_01", "g_m_y_ballaorig_01", "g_f_y_ballas_01", "ig_ballasog", "csb_ballasog", "g_m_y_ballasout_01", "g_m_y_ballasout_01" };
        
        public static int[] VehiclesMara = {-1205801634, -825837129, -2040426790, -2040426790, -808831384, 1923400478, -682211828, -682211828 };
        public static string[] PedMara = {"g_m_y_salvaboss_01", "g_m_y_salvagoon_01", "g_m_y_salvagoon_02", "g_m_y_salvagoon_03", "g_m_y_salvagoon_03" };
        
        public static int[] VehiclesInvaderCiv = { 524266990, -1324550758, -2107990196, -1529242755, -986944621, -1116818112, 2134119907, -1993175239, -121446169, -350899544, -1728685474, 196747873, -566387422, -1259134696, -1405937764, -1372848492, -988501280, 223240013, -2078554704, -1848730848, 566387422 };
        
        public static int[] VehiclesWinterCiv = {-1807623979, -1241712818, 1933662059, -748008636, 734217681, 1132262048, -1807623979 };
        public static string[] PedWinterCiv = {"a_m_m_salton_01", "s_f_y_shop_low", "s_m_y_strvend_01", "a_f_y_vinewood_02", "a_m_y_vinewood_03", "a_m_y_vinewood_03" };
        
        public static void Add(float x, float y, float z, int type, int timeout = 15, bool isSirenActive = false, bool toCoord = true)
        {
            var rand = new Random();
            GetNthClosestVehicleNode(x, y, z, 300 + rand.Next(300), ref OutPos, 0, 0, 0);

            if (type == UnitTypes.Standart)
            {
                CreateVehicleWithPed(VehiclesStandart[rand.Next(8)], PedStandart[rand.Next(5)], 2, 453432689, OutPos, new Vector3(x, y, z), type, timeout, isSirenActive, 6, -1, -1, toCoord);
            }
            else if (type == UnitTypes.HightWay)
            {
                CreateVehicleWithPed(VehiclesHightWay[rand.Next(4)], PedHighWay[rand.Next(5)], 2, 453432689, OutPos, new Vector3(x, y, z), type, timeout, isSirenActive, 6, -1, -1, toCoord);
            }
            else if (type == UnitTypes.Sheriff)
            {
                CreateVehicleWithPed(VehiclesSheriff[rand.Next(4)], PedSheriff[rand.Next(5)], 2, 453432689, OutPos, new Vector3(x, y, z), type, timeout, isSirenActive, 6, -1, -1, toCoord);
            }
            else if (type == UnitTypes.Detective)
            {
                CreateVehicleWithPed(VehiclesDetective[rand.Next(5)], PedDetective[0], 1, 453432689, OutPos, new Vector3(x, y, z), type, timeout, isSirenActive, 6, -1, -1, toCoord);
            }
            else if (type == UnitTypes.Fib)
            {
                CreateVehicleWithPed(VehiclesFib[rand.Next(5)], PedFib[rand.Next(6)], 7, 453432689, OutPos, new Vector3(x, y, z), type, timeout, isSirenActive, 6, -1, -1, toCoord);
            }
            else if (type == UnitTypes.Swat)
            {
                CreateVehicleWithPed(VehiclesSwat[rand.Next(3)], PedSwat[0], 7, 453432689, OutPos, new Vector3(x, y, z), type, timeout, isSirenActive, 6, -1, -1, toCoord);
            }
            else if (type == UnitTypes.Medic)
            {
                CreateVehicleWithPed(VehiclesMedic[0], PedMedic[0], 2, 0, OutPos, new Vector3(x, y, z), type, timeout, isSirenActive, 6, -1, -1, toCoord);
            }
            else if (type == UnitTypes.Ballas)
            {
                GetNthClosestVehicleNode(x, y, z, 300, ref OutPos, 0, 0, 0);
                CreateVehicleWithPed(VehiclesBallas[rand.Next(6)], PedBallas[rand.Next(7)], 4, (uint) WeaponHash.MicroSMG, OutPos, new Vector3(x, y, z), type, timeout, isSirenActive, 22, 145, 145, toCoord);
            }
            else if (type == UnitTypes.Mara)
            {
                GetNthClosestVehicleNode(x, y, z, 300, ref OutPos, 0, 0, 0);
                CreateVehicleWithPed(VehiclesMara[rand.Next(7)], PedMara[rand.Next(5)], 4, (uint) WeaponHash.MicroSMG, OutPos, new Vector3(x, y, z), type, timeout, isSirenActive, 22, 65, 65, toCoord);
            }
            else if (type == UnitTypes.WinterCiv)
            {
                GetNthClosestVehicleNode(x, y, z, 300, ref OutPos, 0, 0, 0);
                CreateVehicleWithPed(VehiclesWinterCiv[rand.Next(6)], PedWinterCiv[rand.Next(5)], 1, (uint) WeaponHash.Ball, OutPos, new Vector3(x, y, z), type, timeout, isSirenActive, 26, -1, -1, toCoord);
            }
            else if (type == UnitTypes.InvaderCiv)
            {
                GetNthClosestVehicleNode(x, y, z, 300, ref OutPos, 0, 0, 0);
                CreateVehicleWithPed(VehiclesInvaderCiv[rand.Next(20)], PedWinterCiv[rand.Next(5)], 1, (uint) WeaponHash.Ball, OutPos, new Vector3(x, y, z), type, timeout, isSirenActive, 26, -1, -1, toCoord);
            }
        }
        
        public static async void SendCode(int codeType, bool sendNotification = true, int timeout = 15, int unit = 0)
        {
            var rand = new Random();
            
            if (CodeList.Code0 == codeType)
            {
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                for (int i = 0; i < 2 + rand.Next(2); i++)
                {
                    await Delay(3000);
                    Add(pos.X, pos.Y, pos.Z, unit, timeout, true);
                }
                
                if (sendNotification)
                    Notification.SendPicture("Поддержка в пути", "Диспетчер", "Код 0", "CHAR_CALL911", Notification.TypeChatbox);
            }
            else if (CodeList.Code1 == codeType)
            {
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                for (int i = 0; i < 3 + rand.Next(3); i++)
                {
                    await Delay(3000);
                    Add(pos.X, pos.Y, pos.Z, unit, timeout, true);
                }
                
                if (sendNotification)
                    Notification.SendPicture("Поддержка в пути", "Диспетчер", "Код 1", "CHAR_CALL911", Notification.TypeChatbox);
            }
            else if (CodeList.Code2 == codeType)
            {
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                for (int i = 0; i < 2 + rand.Next(1); i++)
                {
                    await Delay(3000);
                    Add(pos.X, pos.Y, pos.Z, unit, timeout);
                }
                
                if (sendNotification)
                    Notification.SendPicture("Поддержка в пути", "Диспетчер", "Код 2", "CHAR_CALL911", Notification.TypeChatbox);
            }
            else if (CodeList.Code3 == codeType)
            {
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                for (int i = 0; i < 2 + rand.Next(2); i++)
                {
                    await Delay(3000);
                    Add(pos.X, pos.Y, pos.Z, unit, timeout, true);
                }
                
                if (sendNotification)
                    Notification.SendPicture("Поддержка в пути", "Диспетчер", "Код 3", "CHAR_CALL911", Notification.TypeChatbox);
            }
            else if (99 == codeType)
            {
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                for (int i = 0; i < (int) Client.Sync.Data.GetLocally(User.GetServerId(), "hightSapdType"); i++)
                {
                    await Delay(5000);
                    Add(pos.X, pos.Y, pos.Z, unit, timeout, false, false);
                }
            }
            else if (100 == codeType)
            {
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                await Delay(5000);
                Add(pos.X, pos.Y, pos.Z, unit, timeout, false, false);
            }
            else if (CodeList.Code4 == codeType)
            {
                foreach (var p in Main.GetPedListOnRadius())
                {
                    if (!Client.Sync.Data.HasLocally(p.Handle, "lspdUnitTimeout")) continue;
                    if ((int) Client.Sync.Data.GetLocally(p.Handle, "lspdUnitTimeout") > 0)
                        Client.Sync.Data.SetLocally(p.Handle, "lspdUnitTimeout", 3);
                }
                
                if (sendNotification)
                    Notification.SendPicture("Поддержка не требуется", "Диспетчер", "Код 4", "CHAR_CALL911", Notification.TypeChatbox);
            }
        }
        
        public static async void SendGangUnit(int codeType, int timeout = 15, int unit = 0)
        {
            var rand = new Random();
            
            if (CodeList.Code0 == codeType)
            {
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                
                Add(pos.X, pos.Y, pos.Z, unit, timeout, true);
                /*for (int i = 0; i < 2 + rand.Next(2); i++)
                {
                    await Delay(3000);
                    Add(pos.X, pos.Y, pos.Z, unit, timeout, true);
                }*/
                
                Notification.Send("~g~Поддержка в пути");
            }
            else if (CodeList.Code1 == codeType)
            {
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                for (int i = 0; i < 3 + rand.Next(3); i++)
                {
                    await Delay(3000);
                    Add(pos.X, pos.Y, pos.Z, unit, timeout, true);
                }
                
                Notification.Send("~g~Поддержка в пути");
            }
            else if (CodeList.Code4 == codeType)
            {
                foreach (var p in Main.GetPedListOnRadius())
                {
                    if (!Client.Sync.Data.HasLocally(p.Handle, "lspdUnitTimeout")) continue;
                    if ((int) Client.Sync.Data.GetLocally(p.Handle, "lspdUnitTimeout") > 0)
                        Client.Sync.Data.SetLocally(p.Handle, "lspdUnitTimeout", 3);
                }
                
                Notification.Send("~r~Поддержка больше не требуется");
            }
        }

        private static async void CreateVehicleWithPed(int hash, string pedHash, int countPed, uint weaponHash, Vector3 pos, Vector3 toPos, int type = 0, int timeout = 10, bool isSirenActive = false, int pedType = 6, int color1 = -1, int color2 = -1, bool toCoord = true)
        {
            uint vehicleHash = (uint) hash;
            uint pHash = (uint) GetHashKey(pedHash);

            if (!await Main.LoadModel(vehicleHash))
                return;
            if (!await Main.LoadModel(pHash))
                return;
                    
            var veh = CreateVehicle(vehicleHash, pos.X, pos.Y, pos.Z + 1f, 0, true, false); 
            CitizenFX.Core.Vehicle vehicle = new CitizenFX.Core.Vehicle(veh)
            {
                IsEngineRunning = true
            };

            if (color1 >= 0)
                SetVehicleColours(vehicle.Handle, color1, 0);

            if (color1 >= 0 && color2 >= 0)
                SetVehicleColours(vehicle.Handle, color1, color2);
            
            SetVehicleOnGroundProperly(vehicle.Handle);
            SetVehicleFixed(vehicle.Handle);
            
            /*var blip = AddBlipForEntity(vehicle.Handle);
            SetBlipColour(blip, 29);
            SetBlipFlashes(blip, true);*/

            int driver = CreatePedInsideVehicle(vehicle.Handle, pedType, pHash, -1, true, false);;
            
            SetEntityAsMissionEntity(driver, true, true);
            if (weaponHash == 0)
                GiveWeaponToPed(driver, weaponHash, 200, false, true);
            
            Client.Sync.Data.SetLocally(driver, "lspdUnitToCoord", toPos);
            Client.Sync.Data.SetLocally(driver, "lspdUnitTimeout", timeout);
            Client.Sync.Data.SetLocally(driver, "lspdUnit", true);
            //Client.Sync.Data.SetLocally(driver, "lspdBlip", blip);
            
            if (isSirenActive)
                SetVehicleSiren(vehicle.Handle, true);

            if (countPed > 1)
            {
                var rand = new Random();
                if (type == UnitTypes.Standart)
                {
                    pedHash = PedStandart[rand.Next(5)];
                }
                else if (type == UnitTypes.HightWay)
                {
                    pedHash = PedHighWay[rand.Next(5)];
                }
                else if (type == UnitTypes.Fib)
                {
                    pedHash = PedFib[rand.Next(6)];
                }
                else if (type == UnitTypes.Ballas)
                {
                    pedHash = PedBallas[rand.Next(7)];
                }
                else if (type == UnitTypes.Mara)
                {
                    pedHash = PedMara[rand.Next(5)];
                }
                        
                pHash = (uint) GetHashKey(pedHash);
                if (!await Main.LoadModel(pHash))
                    return;
                
                if (countPed >= GetVehicleMaxNumberOfPassengers(vehicle.Handle))
                {
                    for (int i = 0; i < GetVehicleMaxNumberOfPassengers(vehicle.Handle) - 1; i++)
                    {
                        var ped = CreatePedInsideVehicle(vehicle.Handle, pedType, pHash, i, true, false);
                        SetEntityAsMissionEntity(ped, true, true);
                        
                        if (weaponHash != 0)
                            GiveWeaponToPed(ped, weaponHash, 200, false, true);
                    }
                }
                else
                {
                    for (int i = 0; i < countPed - 1; i++)
                    {
                        var ped = CreatePedInsideVehicle(vehicle.Handle, pedType, pHash, i, true, false);
                        SetEntityAsMissionEntity(ped, true, true);
                        
                        if (weaponHash != 0)
                            GiveWeaponToPed(ped, weaponHash, 200, false, true);
                    }
                }
            }

            if (toCoord)
            {
                if (isSirenActive)
                    TaskVehicleDriveToCoord(driver, vehicle.Handle, toPos.X, toPos.Y, toPos.Z, 40f, 1, 0,
                        DriveTypes.Rushed, 1.0f, 1);
                else
                    TaskVehicleDriveToCoord(driver, vehicle.Handle, toPos.X, toPos.Y, toPos.Z, 17f, 1, 0,
                        DriveTypes.Normal, 1.0f, 1);
            }
            else
            {
                TaskVehicleDriveWander(driver, vehicle.Handle, 17.0f, DriveTypes.Normal);
                SetVehicleSiren(veh, false);
                            
                new CitizenFX.Core.Ped(driver).MarkAsNoLongerNeeded();
                new CitizenFX.Core.Vehicle(veh).MarkAsNoLongerNeeded();
            }
        }

        public static async void CreateTrains()
        {
            if (Main.IsSpawnTrain) return;
            Main.IsSpawnTrain = true;
            //DeleteAllTrains();

            var requestModels = new List<string> {"freight", "freightcar", "freightgrain", "freightcont1", "freightcont2", "tankercar", "freighttrailer", "metrotrain", "s_m_m_lsmetro_01"};
            foreach (var model in requestModels)
                await Main.LoadModel((uint) GetHashKey(model));

            var rand = new Random();
            
            await Delay(rand.Next(2000, 180000));
            
            var train = CreateMissionTrain(rand.Next(20), 537.0f, -1324.1f, 29.1f, false);
            var ped = CreatePedInsideVehicle(train, 26, (uint) GetHashKey("s_m_m_lsmetro_01"), -1, true, true);
            SetEntityAsMissionEntity(train, true, true);
            SetEntityAsMissionEntity(ped, true, true);
            
            train = CreateMissionTrain(24, 40.2f, -1201.3f, 31.0f, true);
            ped = CreatePedInsideVehicle(train, 26, (uint) GetHashKey("s_m_m_lsmetro_01"), -1, true, true);
            SetEntityAsMissionEntity(train, true, true);
            SetEntityAsMissionEntity(ped, true, true);
            await Delay(90000);
            
            train = CreateMissionTrain(24, 40.2f, -1201.3f, 31.0f, true);
            ped = CreatePedInsideVehicle(train, 26, (uint) GetHashKey("s_m_m_lsmetro_01"), -1, true, true);
            SetEntityAsMissionEntity(train, true, true);
            SetEntityAsMissionEntity(ped, true, true);
            await Delay(90000);
        }
    }
}

public class UnitTypes
{
    public static int Standart => 0;
    public static int HightWay => 1;
    public static int Detective => 2;
    public static int Fib => 3;
    public static int Swat => 4;
    public static int Hrt => 5;
    public static int ParkRanger => 6;
    public static int Sheriff => 7;
    public static int AirSupportDivision => 8;
    public static int Medic => 9;
    public static int Groove => 10;
    public static int Ballas => 11;
    public static int Vagos => 12;
    public static int Aztec => 13;
    public static int Mara => 14;
    public static int FractionUnkown => 15;
    public static int Civ => 16;
    public static int WinterCiv => 17;
    public static int InvaderCiv => 18;
}

public class CodeList
{
    public static int Code0 => 0; //Необходима немедленная поддержка
    public static int Code1 => 1; //Офицер в бедственном положении
    public static int Code2 => 2; //Приоритетный вызов\n(без сирен/стробоскопов)
    public static int Code3 => 3; //Срочный вызов\n(сирены, стробоскопы)
    public static int Code4 => 4; //Помощь не требуется.\nВсе спокойно
}

public class DriveTypes
{
    public static int Normal => 786603;
    public static int Rushed => 1074528293;
    public static int IgnoreLights => 2883621;
    public static int AvoidTraffic => 786468;
}

enum PedTypes
{
    PED_TYPE_PLAYER_0,				// michael
    PED_TYPE_PLAYER_1,				// franklin
    PED_TYPE_NETWORK_PLAYER,			// mp character
    PED_TYPE_PLAYER_2,				// trevor
    PED_TYPE_CIVMALE,
    PED_TYPE_CIVFEMALE,
    PED_TYPE_COP,
    PED_TYPE_GANG_ALBANIAN,
    PED_TYPE_GANG_BIKER_1,
    PED_TYPE_GANG_BIKER_2,
    PED_TYPE_GANG_ITALIAN,
    PED_TYPE_GANG_RUSSIAN,
    PED_TYPE_GANG_RUSSIAN_2,
    PED_TYPE_GANG_IRISH,
    PED_TYPE_GANG_JAMAICAN,
    PED_TYPE_GANG_AFRICAN_AMERICAN,
    PED_TYPE_GANG_KOREAN,
    PED_TYPE_GANG_CHINESE_JAPANESE,
    PED_TYPE_GANG_PUERTO_RICAN,
    PED_TYPE_DEALER,
    PED_TYPE_MEDIC,
    PED_TYPE_FIREMAN,
    PED_TYPE_CRIMINAL,
    PED_TYPE_BUM,
    PED_TYPE_PROSTITUTE,
    PED_TYPE_SPECIAL,
    PED_TYPE_MISSION,
    PED_TYPE_SWAT,
    PED_TYPE_ANIMAL,
    PED_TYPE_ARMY
};