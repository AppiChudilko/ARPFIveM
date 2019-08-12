using System;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Business
{
    public class CarNumber : BaseScript
    {
        public static double[,] NumberMarkers =
        {
            { -38.85731, -1082.644, 25.42234, 86, -33.4645, -1089.276, 26.00986 }, //Simon / Premium Delux Motorsport
            { 548.5602, -172.7322, 53.48133, 87, 538.6563, -176.844, 54.06823 }, //Vinewood / Rent Exotic
        };
        
        public static void CreatePickups()
        {
            for (var i = 0; i < NumberMarkers.Length / 7; i++)
            {
                Marker.Create(new Vector3((float) NumberMarkers[i, 0], (float) NumberMarkers[i, 1], (float) NumberMarkers[i, 2]), 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
                Managers.Checkpoint.Create(new Vector3((float) NumberMarkers[i, 0], (float) NumberMarkers[i, 1], (float) NumberMarkers[i, 2]), 1.4f, "show:menu");
                
                var blip = World.CreateBlip(new Vector3((float) NumberMarkers[i, 0], (float) NumberMarkers[i, 1], (float) NumberMarkers[i, 2]));
                blip.Sprite = (BlipSprite) 225;
                blip.Color = (BlipColor) 68;
                blip.Name = Lang.GetTextToPlayer("_lang_39");
                blip.IsShortRange = true;
                blip.Scale = 0.4f;
                
                Managers.Checkpoint.Create(new Vector3((float) NumberMarkers[i, 4], (float) NumberMarkers[i, 5], (float) NumberMarkers[i, 6]), 4f, "car:set:number");
            }
        }

        public static void CheckPosPickups()
        {
            for (var i = 0; i < NumberMarkers.Length / 7; i++)
            {
                var plPos = GetEntityCoords(GetPlayerPed(-1), true);
                if (!(Main.GetDistanceToSquared(
                          new Vector3((float) NumberMarkers[i, 0], (float) NumberMarkers[i, 1],
                              (float) NumberMarkers[i, 2]),
                          plPos) < 2f)) continue;
                
                MenuList.ShowSetCarNumberMenu(Convert.ToInt32(NumberMarkers[i, 3]));
                return;
            }
        }
        
        public static void ShowVehicleRepairList(int bizzId)
        {
            var vehList = Main.GetVehicleListOnRadius(new Vector3((float) NumberMarkers[0, 4], (float) NumberMarkers[0, 5], (float) NumberMarkers[0, 6]), 4f);
            switch (bizzId)
            {
                case 86:
                    MenuList.ShowRepairList(bizzId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 87:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) NumberMarkers[1, 4], (float) NumberMarkers[1, 5], (float) NumberMarkers[1, 6]), 4f);
                    MenuList.ShowRepairList(bizzId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
            }
        }

        public static void ShowVehicleNumberList(int bizzId)
        {
            var vehList = Main.GetVehicleListOnRadius(new Vector3((float) NumberMarkers[0, 4], (float) NumberMarkers[0, 5], (float) NumberMarkers[0, 6]), 4f);
            switch (bizzId)
            {
                case 86:
                    MenuList.ShowVehicleNumberList(bizzId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 87:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) NumberMarkers[1, 4], (float) NumberMarkers[1, 5], (float) NumberMarkers[1, 6]), 4f);
                    MenuList.ShowVehicleNumberList(bizzId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
            }
        }

        public static void RepairVeh(string number, int lscId)
        {
            CitizenFX.Core.Vehicle veh = Managers.Vehicle.GetVehicleByNumber(number);
            if (veh != null && veh.Handle > 0)
            {
                if (User.GetMoneyWithoutSync() < 200)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_25"));
                    return;
                }
                
                User.RemoveMoney(200);
                Managers.Vehicle.Repair(veh);
                Business.AddMoney(lscId, 200);
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_40"));
            }
            else
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_41"));
            }
        }

        public static void SetNumber(string number, int lscId, string newNumber)
        {
            int vehId = Managers.Vehicle.GetVehicleIdByNumber(number);
            var vehHandle = Managers.Vehicle.GetVehicleByNumber(number);
            if (vehId == -1)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_42"));
                return;
            }

            newNumber = newNumber.ToUpper();

            if (newNumber.Length < 4)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_43"));
                return;
            }

            if (!NumberIsValid(newNumber))
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_44"));
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_45"));
                return;
            }
            
            if (User.GetMoneyWithoutSync() < 40000)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_25"));
                return;
            }
            
            TriggerServerEvent("ARP:SetCarNumber", vehId, lscId, vehHandle.Handle, newNumber, GetHashKey(newNumber), GetHashKey(Managers.Vehicle.GetVehicleNumber(vehHandle.Handle)));
        }

        public static bool NumberIsValid(string number)
        {
            number = number.ToUpper();
            const string chars = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            foreach (char t1 in number)
            {
                bool isValid = false;
                foreach (char t in chars)
                {
                    if (t1 == t)
                        isValid = true;
                }

                if (!isValid)
                    return false;
            }

            return true;
        }
    }
}