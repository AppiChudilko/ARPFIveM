using System;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Business
{
    public class Custom : BaseScript
    {
        protected static Camera Camera;
        
        public static double[,] LscCarPos =
        {
            { -1159.827, -2015.182, 12.16598, 338.3167 },
            { -330.8568, -137.6985, 38.00612, 95.85743 },
            { 732.1998, -1088.71, 21.15658, 89.10553 },
            { -222.6972, -1329.915, 29.87796, 269.8108 },
            { 1174.876, 2640.67, 36.7454, 0.5306945 },
            { 110.3291, 6626.977, 30.7735, 223.695 },
            { -147.4434, -599.0691, 166.0058, 315.3235 },
            { 481.2153, -1317.698, 28.09073, 296.715 },
        };
        
        public static double[,] LscCarColorPos =
        {
            { -1167.36, -2013.42, 11.63059, 136.2973 },
            { -327.3558, -144.5778, 38.04641, 250.0263 },
            { 735.6607, -1072.729, 21.2193, 179.3472 },
            { -206.2765, -1323.342, 29.87665, 2.203119 },
            { 1182.65, 2638.49, 36.78132, 357.8441 },
            { 103.6963, 6622.596, 30.81484, 224.0517 },
            { -151.0918, -594.4427, 166.0052, 308.8605 },
            { 478.4267, -1308.524, 28.01912, 27.1145 },
        };
        
        public static double[,] CamColorPos =
        {
            { -1167.856, -2016.76, 13.53096 },
            { -323.593, -143.7437, 39.26034 },
            { 733.7505, -1069.566, 22.43295 },
            { -209.2587, -1319.221, 31.09041 },
            { 1184.5, 2635.391, 38.0 },
            { 100.3815, 6623.296, 32.12886 },
            { -146.051, -593.9824, 167.5002 },
            { 475.9341, -1307.055, 29.43324 },
        };
        
        public static double[,] CamPos1 =
        {
            { -1155.691, -2012.395, 13.18026 },
            { -1160.24, -2020.572, 13.18034 },
            { -1163.919, -2015.856, 13.98026 },
        };
        
        public static double[,] CamPos2 =
        {
            { -335.2385, -136.069, 39.00966 },
            { -326.8082, -134.7203, 39.00966 },
            { -328.3979, -140.4191, 39.80966 },
        };
        
        public static double[,] CamPos3 =
        {
            { 727.5853, -1091.216, 22.16918 },
            { 732.2767, -1084.753, 22.169 },
            { 736.9069, -1090.952, 22.98864 },
        };
        
        public static double[,] CamPos4 =
        {
            { -217.821, -1327.9, 30.89041 },
            { -223.2449, -1333.283, 30.8904 },
            { -226.2647, -1326.762, 31.3904 },
        };
        
        public static double[,] CamPos5 =
        {
            { 1177.862, 2644.075, 37.78613 },
            { 1177.083, 2636.541, 37.75381 },
            { 1172.015, 2641.024, 38.29303 },
        };
        
        public static double[,] CamPos6 =
        {
            { 111.6643, 6621.887, 31.78725 },
            { 106.7364, 6627.241, 31.78723 },
            { 112.0298, 6629.307, 32.28724 },
        };
        
        public static double[,] CamPos7 =
        {
            { -145.8006, -594.2851, 167.0002 },
            { -144.9173, -602.0816, 167.5002 },
            { -152.5041, -600.7098, 167.0002 },
        };
        
        public static double[,] CamPos8 =
        {
            { 482.9536, -1314.375, 29.20051 },
            { 478.4819, -1314.576, 29.60341 },
            { 477.1504, -1319.856, 29.20595 },
        };

        public static readonly double[,] LscPickupPos =
        {
            { -1148.878, -2000.123, 12.18026, 14 }, //LSIA
            { -347.0815, -133.3432, 38.00966, 54 }, //Burton
            { 726.0679, -1071.613, 27.31101, 55 }, //La Mesa
            { -207.0201, -1331.493, 33.89437, 56 }, //Benny
            { 1187.764, 2639.15, 37.43521, 57 }, //Senora
            { 101.0262, 6618.267, 31.43771, 71 }, //Paleto
            { -146.2072, -584.2731, 166.0002, 121 }, //Arcadius
            { 472.2666, -1310.529, 28.22178, 123 }, //Mission Row
        };
        
        private static readonly List<int>_countSlots = new List<int>();
        public static bool IsOpenMenu = false;
        public static bool IsOpenColorMenu = false;
        public static int MenuId = 0;
        public static int CamPos = 0;

        public static void CreatePickups()
        {
            for (var i = 0; i < LscPickupPos.Length / 4; i++)
            {
                Marker.Create(new Vector3((float) LscPickupPos[i, 0], (float) LscPickupPos[i, 1], (float) LscPickupPos[i, 2]), 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
                Managers.Checkpoint.Create(new Vector3((float) LscPickupPos[i, 0], (float) LscPickupPos[i, 1], (float) LscPickupPos[i, 2]), 1.4f, "show:menu");
            }
            
            for (var i = 0; i < LscCarPos.Length / 4; i++)
                Managers.Checkpoint.CreateWithMarker(new Vector3((float) LscCarPos[i, 0], (float) LscCarPos[i, 1], (float) LscCarPos[i, 2]), 4f, 0.3f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A, "lsc:info:tun");
            
            for (var i = 0; i < LscCarColorPos.Length / 4; i++)
                Managers.Checkpoint.CreateWithMarker(new Vector3((float) LscCarColorPos[i, 0], (float) LscCarColorPos[i, 1], (float) LscCarColorPos[i, 2]), 4f, 0.3f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A, "lsc:info:clr");
        }

        public static void CheckPosPickups()
        {
            for (var i = 0; i < LscPickupPos.Length / 4; i++)
            {
                var plPos = GetEntityCoords(GetPlayerPed(-1), true);
                if (!(Main.GetDistanceToSquared(
                          new Vector3((float) LscPickupPos[i, 0], (float) LscPickupPos[i, 1],
                              (float) LscPickupPos[i, 2]),
                          plPos) < 2f)) continue;
                
                MenuList.ShowLscMenu(Convert.ToInt32(LscPickupPos[i, 3]));
                MenuId = Convert.ToInt32(LscPickupPos[i, 3]);
                return;
            }
        }

        public static void CameraNext()
        {
            switch (MenuId)
            {
                case 14:
                    CamPos++;
                    if (CamPos > 2) CamPos = 0;
                    Camera.Position = new Vector3((float) CamPos1[CamPos, 0], (float) CamPos1[CamPos, 1], (float) CamPos1[CamPos, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 54:
                    CamPos++;
                    if (CamPos > 2) CamPos = 0;
                    Camera.Position = new Vector3((float) CamPos2[CamPos, 0], (float) CamPos2[CamPos, 1], (float) CamPos2[CamPos, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 55:
                    CamPos++;
                    if (CamPos > 2) CamPos = 0;
                    Camera.Position = new Vector3((float) CamPos3[CamPos, 0], (float) CamPos3[CamPos, 1], (float) CamPos3[CamPos, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 56:
                    CamPos++;
                    if (CamPos > 2) CamPos = 0;
                    Camera.Position = new Vector3((float) CamPos4[CamPos, 0], (float) CamPos4[CamPos, 1], (float) CamPos4[CamPos, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 57:
                    CamPos++;
                    if (CamPos > 2) CamPos = 0;
                    Camera.Position = new Vector3((float) CamPos5[CamPos, 0], (float) CamPos5[CamPos, 1], (float) CamPos5[CamPos, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;   
                case 71:
                    CamPos++;
                    if (CamPos > 2) CamPos = 0;
                    Camera.Position = new Vector3((float) CamPos6[CamPos, 0], (float) CamPos6[CamPos, 1], (float) CamPos6[CamPos, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;  
                case 121:
                    CamPos++;
                    if (CamPos > 2) CamPos = 0;
                    Camera.Position = new Vector3((float) CamPos7[CamPos, 0], (float) CamPos7[CamPos, 1], (float) CamPos7[CamPos, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break; 
                case 123:
                    CamPos++;
                    if (CamPos > 2) CamPos = 0;
                    Camera.Position = new Vector3((float) CamPos8[CamPos, 0], (float) CamPos8[CamPos, 1], (float) CamPos8[CamPos, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;      
            }
        }

        public static void RepairVeh(string number, int lscId)
        {
            CitizenFX.Core.Vehicle veh = Managers.Vehicle.GetVehicleByNumber(number);;
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

        public static async void UpgradeVeh(int slot, int index, int price, int lscId, CitizenFX.Core.Vehicle veh)
        {
            var vehId = Managers.Vehicle.GetVehicleIdByNumber(Managers.Vehicle.GetVehicleNumber(veh.Handle));
            if (vehId == -1)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_52"));
                return;
            }
            
            var vehItem = await Managers.Vehicle.GetAllData(vehId);
            if (!vehItem.IsUserOwner)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_52"));
                return;
            }

            if (await User.GetMoney() < price)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                return;
            }

            if (slot == 99)
            {
                Sync.Data.Set(vehId + 110000, "Livery", index);
                Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Livery = index;
                
                User.RemoveMoney(price);
                Business.AddMoney(lscId, price);
                TriggerServerEvent("ARP:SaveVehicle", vehItem.VehId);
                
                Notification.SendSubtitle(Lang.GetTextToPlayer("_lang_53", $"{price:#,#}"));
                return;
            }

            if (slot == 998)
            {
                if (index == 0)
                {
                    Sync.Data.Set(vehId + 110000, "neon_type", 0);
                    Managers.Vehicle.VehicleInfoGlobalDataList[vehId].neon_type = 0;
                
                    User.RemoveMoney(price / 2);
                    Business.AddMoney(lscId, price / 2);
                    TriggerServerEvent("ARP:SaveVehicle", vehItem.VehId);
                
                    Notification.SendSubtitle(Lang.GetTextToPlayer("_lang_56", $"{(price/2):#,#}"));
                }
                else
                {
                    Sync.Data.Set(vehId + 110000, "neon_type", 1);
                    Managers.Vehicle.VehicleInfoGlobalDataList[vehId].neon_type = 1;
                
                    User.RemoveMoney(price);
                    Business.AddMoney(lscId, price);
                    TriggerServerEvent("ARP:SaveVehicle", vehItem.VehId);
                
                    Notification.SendSubtitle(Lang.GetTextToPlayer("_lang_53", $"{price:#,#}"));
                }
                return;
            }

            if (slot == 999)
            {
                if (index == 0)
                {
                    Sync.Data.Set(vehId + 110000, "neon_type", 0);
                    Managers.Vehicle.VehicleInfoGlobalDataList[vehId].neon_type = 0;
                
                    User.RemoveMoney(price / 2);
                    Business.AddMoney(lscId, price / 2);
                    TriggerServerEvent("ARP:SaveVehicle", vehItem.VehId);
                
                    Notification.SendSubtitle(Lang.GetTextToPlayer("_lang_56", $"{(price/2):#,#}"));
                }
                else
                {
                    Sync.Data.Set(vehId + 110000, "neon_type", 2);
                    Managers.Vehicle.VehicleInfoGlobalDataList[vehId].neon_type = 2;
                
                    User.RemoveMoney(price);
                    Business.AddMoney(lscId, price);
                    TriggerServerEvent("ARP:SaveVehicle", vehItem.VehId);
                
                    Notification.SendSubtitle(Lang.GetTextToPlayer("_lang_53", $"{price:#,#}"));
                }
                return;
            }

            if (index == 0)
            {
                
                if (User.GetMoneyWithoutSync() < price / 2)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_25"));
                    return;
                }
                
                RemoveVehicleMod(veh.Handle, slot);
                        
                try
                {
                    var jsonUpgrade = (IDictionary<String, Object>) await Main.FromJson(vehItem.upgrade);
                    if (jsonUpgrade.ContainsKey("Knife"))
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_54"));
                        return;
                    }
                    jsonUpgrade[slot.ToString()] = -1;
                    string json = await Main.ToJson(jsonUpgrade);
                    Sync.Data.Set(vehId + 110000, "upgrade", json);
                    Managers.Vehicle.VehicleInfoGlobalDataList[vehId].upgrade = json;
                
                    User.RemoveMoney(price / 2);
                    Business.AddMoney(lscId, price / 2);
                    TriggerServerEvent("ARP:SaveVehicle", vehItem.VehId);
                }
                catch (Exception e)
                {
                    if (IsStringNullOrEmpty(vehItem.upgrade))
                    {
                        /*var jsonFormat = await Main.ToJson(GetVehicleModSlotsFormat(veh));
                        Sync.Data.Set(vehId + 110000, "upgrade", jsonFormat);
                        Managers.Vehicle.VehicleInfoGlobalDataList[vehId].upgrade = jsonFormat;*/
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_54"));
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_35"));
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_55"));
                        
                        Main.SaveLog("LSCEXC", $"{User.Data.rp_name} | {e} | {vehItem.upgrade}");
                        return;
                    }

                    var jsonUpgrade = (IDictionary<String, Object>) await Main.FromJson(vehItem.upgrade);
                    if (jsonUpgrade.ContainsKey("Knife"))
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_54"));
                        return;
                    }
                    jsonUpgrade[slot.ToString()] = -1;
                    string json = await Main.ToJson(jsonUpgrade);
                    Sync.Data.Set(vehId + 110000, "upgrade", json);
                    Managers.Vehicle.VehicleInfoGlobalDataList[vehId].upgrade = json;
                
                    Main.SaveLog("LSCEXC", $"{User.Data.rp_name} | {e} | {vehItem.upgrade}");
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_54"));
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_35"));
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_55"));
                
                    User.RemoveMoney(price / 2);
                    Business.AddMoney(lscId, price / 2);
                    TriggerServerEvent("ARP:SaveVehicle", vehItem.VehId);
                }
                
                Notification.SendSubtitle(Lang.GetTextToPlayer("_lang_56", $"{(price / 2):#,#}"));
                Main.SaveLog("LSC", User.Data.rp_name + ", id: " + vehItem.id + " remove slot: " + slot);
                return;
            }
            
            if (slot == 11 || slot == 12 || slot == 13)
                price = price * index;
            
            index = --index;

            if (User.GetMoneyWithoutSync() < price)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                return;
            }
            
            try
            {
                var jsonUpgrade = (IDictionary<String, Object>) await Main.FromJson(vehItem.upgrade);
                if (jsonUpgrade.ContainsKey("Knife"))
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_54"));
                    return;
                }
                jsonUpgrade[slot.ToString()] = index;
                string json = await Main.ToJson(jsonUpgrade);
                Sync.Data.Set(vehId + 110000, "upgrade", json);
                Managers.Vehicle.VehicleInfoGlobalDataList[vehId].upgrade = json;
                
                User.RemoveMoney(price);
                Business.AddMoney(lscId, price);
                TriggerServerEvent("ARP:SaveVehicle", vehItem.VehId);
            }
            catch (Exception e)
            {
                if (IsStringNullOrEmpty(vehItem.upgrade))
                {
                    /*var jsonFormat = await Main.ToJson(GetVehicleModSlotsFormat(veh));
                    Sync.Data.Set(vehId + 110000, "upgrade", jsonFormat);
                    Managers.Vehicle.VehicleInfoGlobalDataList[vehId].upgrade = jsonFormat;*/
                    
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_54"));
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_35"));
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_55"));
                        
                    Main.SaveLog("LSCEXC", $"{User.Data.rp_name} | {e} | {vehItem.upgrade}");
                    return;
                }

                var jsonUpgrade = (IDictionary<String, Object>) await Main.FromJson(vehItem.upgrade);
                if (jsonUpgrade.ContainsKey("Knife"))
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_54"));
                    return;
                }
                jsonUpgrade[slot.ToString()] = index;
                string json = await Main.ToJson(jsonUpgrade);
                Sync.Data.Set(vehId + 110000, "upgrade", json);
                Managers.Vehicle.VehicleInfoGlobalDataList[vehId].upgrade = json;
                
                Main.SaveLog("LSCEXC", $"{User.Data.rp_name} | {e} | {vehItem.upgrade}");
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_54"));
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_35"));
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_55"));
                
                User.RemoveMoney(price);
                Business.AddMoney(lscId, price);
                TriggerServerEvent("ARP:SaveVehicle", vehItem.VehId);
            }
                
            Notification.SendSubtitle(Lang.GetTextToPlayer("_lang_53", $"{price:#,#}"));
            Main.SaveLog("LSC", User.Data.rp_name + ", id: " + vehItem.id + " add slot: " + slot);
        }

        public static async void Color1Veh(int idx, int price, int lscId, CitizenFX.Core.Vehicle veh)
        {
            MenuList.HideMenu();
            CloseMenuWithRemoveMod(veh);
            
            var vehId = Managers.Vehicle.GetVehicleIdByNumber(Managers.Vehicle.GetVehicleNumber(veh.Handle));
            if (vehId == -1)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_52"));
                return;
            }
            
            var vehItem = await Managers.Vehicle.GetAllData(vehId);
            
            if (User.GetMoneyWithoutSync() < price)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                return;
            }
            
            Sync.Data.Set(vehId + 110000, "color1", idx);
            Managers.Vehicle.VehicleInfoGlobalDataList[vehId].color1 = idx;
                
            User.RemoveMoney(price);
            Business.AddMoney(lscId, price);
            TriggerServerEvent("ARP:SaveVehicle", vehItem.VehId);
            Notification.SendSubtitle(Lang.GetTextToPlayer("_lang_57", $"{price:#,#}"));
            
            SetVehicleColours(veh.Handle, idx, vehItem.color2);
        }
        
        public static async void Color2Veh(int idx, int price, int lscId, CitizenFX.Core.Vehicle veh)
        {
            MenuList.HideMenu();
            CloseMenuWithRemoveMod(veh);
            
            var vehId = Managers.Vehicle.GetVehicleIdByNumber(Managers.Vehicle.GetVehicleNumber(veh.Handle));
            if (vehId == -1)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_52"));
                return;
            }
            
            var vehItem = await Managers.Vehicle.GetAllData(vehId);

            if (User.GetMoneyWithoutSync() < price)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                return;
            }
            
            Sync.Data.Set(vehId + 110000, "color2", idx);
            Managers.Vehicle.VehicleInfoGlobalDataList[vehId].color2 = idx;
                
            User.RemoveMoney(price);
            Business.AddMoney(lscId, price);
            TriggerServerEvent("ARP:SaveVehicle", vehItem.VehId);
            Notification.SendSubtitle(Lang.GetTextToPlayer("_lang_57", $"{price:#,#}"));
            
            SetVehicleColours(veh.Handle, vehItem.color1, idx);
        }
        
        public static async void ColorNeonVeh(int price, int lscId, CitizenFX.Core.Vehicle veh)
        {
            MenuList.HideMenu();
            CloseMenuWithRemoveMod(veh);
            
            var vehId = Managers.Vehicle.GetVehicleIdByNumber(Managers.Vehicle.GetVehicleNumber(veh.Handle));
            if (vehId == -1)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_52"));
                return;
            }
            
            var vehItem = await Managers.Vehicle.GetAllData(vehId);

            if (User.GetMoneyWithoutSync() < price)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                return;
            }

            if (vehItem.neon_type < 1)
            {
                Notification.SendWithTime("~r~Для начала купите неон");
                return;
            }
            
            int r = Convert.ToInt32(await Menu.GetUserInput("R", "", 3));
            int g = Convert.ToInt32(await Menu.GetUserInput("G", "", 3));
            int b = Convert.ToInt32(await Menu.GetUserInput("B", "", 3));

            SetVehicleNeonLightsColour(veh.Handle, r, g, b);
            SetVehicleNeonLightEnabled(veh.Handle, 0, true);
            SetVehicleNeonLightEnabled(veh.Handle, 1, true);
            SetVehicleNeonLightEnabled(veh.Handle, 2, true);
            SetVehicleNeonLightEnabled(veh.Handle, 3, true);
                                
            Sync.Data.Set(vehId + 110000, "neon_r", r);
            Managers.Vehicle.VehicleInfoGlobalDataList[vehId].neon_r = r;
                                
            Sync.Data.Set(vehId + 110000, "neon_g", g);
            Managers.Vehicle.VehicleInfoGlobalDataList[vehId].neon_g = g;
                                
            Sync.Data.Set(vehId + 110000, "neon_b", b);
            Managers.Vehicle.VehicleInfoGlobalDataList[vehId].neon_b = b;
                
            User.RemoveMoney(price);
            Business.AddMoney(lscId, price);
            TriggerServerEvent("ARP:SaveVehicle", vehItem.VehId);
            Notification.SendSubtitle(Lang.GetTextToPlayer("_lang_57", $"{price:#,#}"));
        }

        public static async void TunningVeh(string number, int lscId)
        {
            CitizenFX.Core.Vehicle veh = Managers.Vehicle.GetVehicleByNumber(number);
            var vehId = Managers.Vehicle.GetVehicleIdByNumber(number);

            if (vehId == -1)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_52"));
                return;
            }
            
            var vehItem = await Managers.Vehicle.GetAllData(vehId);
            
            if (!vehItem.IsUserOwner)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_52"));
                return;
            }
            
            if (Vehicle.VehInfo.GetClassName(veh.Model.Hash) == "Cycles" || Vehicle.VehInfo.GetClassName(veh.Model.Hash) == "Helicopters" || Vehicle.VehInfo.GetClassName(veh.Model.Hash) == "Planes" || Vehicle.VehInfo.GetClassName(veh.Model.Hash) == "Boats")
            {
                Notification.SendWithTime("Нельзя тюнинговать велосипеды, вертолеты, самолеты и лодки");
                return;
            }
            if (Vehicle.VehInfo.GetClassName(veh.Model.Hash) == "Commercials")
            {
                Notification.SendWithTime("Нельзя тюнинговать коммерчесский транспорт");
                return;
            }
            
            UI.ShowLoadDisplay();

            CamPos = 0;
            IsOpenMenu = true;
            FreezeEntityPosition(veh.Handle, true);
            
            switch (lscId)
            {
                case 14:
                    veh.Position = new Vector3((float) LscCarPos[0, 0], (float) LscCarPos[0, 1], (float) LscCarPos[0, 2]);
                    veh.Heading = (float) LscCarPos[0, 3];
                    
                    Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                    Camera.IsActive = true;
                    Camera.PointAt(new Vector3((float) LscCarPos[0, 0], (float) LscCarPos[0, 1], (float) LscCarPos[0, 2]));
                    Camera.Position = new Vector3((float) CamPos1[CamPos, 0], (float) CamPos1[CamPos, 1], (float) CamPos1[CamPos, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 54:
                    veh.Position = new Vector3((float) LscCarPos[1, 0], (float) LscCarPos[1, 1], (float) LscCarPos[1, 2]);
                    veh.Heading = (float) LscCarPos[1, 3];
                    
                    Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                    Camera.IsActive = true;
                    Camera.PointAt(new Vector3((float) LscCarPos[1, 0], (float) LscCarPos[1, 1], (float) LscCarPos[1, 2]));
                    Camera.Position = new Vector3((float) CamPos2[CamPos, 0], (float) CamPos2[CamPos, 1], (float) CamPos2[CamPos, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 55:
                    veh.Position = new Vector3((float) LscCarPos[2, 0], (float) LscCarPos[2, 1], (float) LscCarPos[2, 2]);
                    veh.Heading = (float) LscCarPos[2, 3];
                    
                    Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                    Camera.IsActive = true;
                    Camera.PointAt(new Vector3((float) LscCarPos[2, 0], (float) LscCarPos[2, 1], (float) LscCarPos[2, 2]));
                    Camera.Position = new Vector3((float) CamPos3[CamPos, 0], (float) CamPos3[CamPos, 1], (float) CamPos3[CamPos, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 56:
                    veh.Position = new Vector3((float) LscCarPos[3, 0], (float) LscCarPos[3, 1], (float) LscCarPos[3, 2]);
                    veh.Heading = (float) LscCarPos[3, 3];
                    
                    Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                    Camera.IsActive = true;
                    Camera.PointAt(new Vector3((float) LscCarPos[3, 0], (float) LscCarPos[3, 1], (float) LscCarPos[3, 2]));
                    Camera.Position = new Vector3((float) CamPos4[CamPos, 0], (float) CamPos4[CamPos, 1], (float) CamPos4[CamPos, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 57:
                    veh.Position = new Vector3((float) LscCarPos[4, 0], (float) LscCarPos[4, 1], (float) LscCarPos[4, 2]);
                    veh.Heading = (float) LscCarPos[4, 3];
                    
                    Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                    Camera.IsActive = true;
                    Camera.PointAt(new Vector3((float) LscCarPos[4, 0], (float) LscCarPos[4, 1], (float) LscCarPos[4, 2]));
                    Camera.Position = new Vector3((float) CamPos5[CamPos, 0], (float) CamPos5[CamPos, 1], (float) CamPos5[CamPos, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 71:
                    veh.Position = new Vector3((float) LscCarPos[5, 0], (float) LscCarPos[5, 1], (float) LscCarPos[5, 2]);
                    veh.Heading = (float) LscCarPos[5, 3];
                    
                    Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                    Camera.IsActive = true;
                    Camera.PointAt(new Vector3((float) LscCarPos[5, 0], (float) LscCarPos[5, 1], (float) LscCarPos[5, 2]));
                    Camera.Position = new Vector3((float) CamPos6[CamPos, 0], (float) CamPos6[CamPos, 1], (float) CamPos6[CamPos, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 121:
                    veh.Position = new Vector3((float) LscCarPos[6, 0], (float) LscCarPos[6, 1], (float) LscCarPos[6, 2]);
                    veh.Heading = (float) LscCarPos[6, 3];
                    
                    Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                    Camera.IsActive = true;
                    Camera.PointAt(new Vector3((float) LscCarPos[6, 0], (float) LscCarPos[6, 1], (float) LscCarPos[6, 2]));
                    Camera.Position = new Vector3((float) CamPos7[CamPos, 0], (float) CamPos7[CamPos, 1], (float) CamPos7[CamPos, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 123:
                    veh.Position = new Vector3((float) LscCarPos[7, 0], (float) LscCarPos[7, 1], (float) LscCarPos[7, 2]);
                    veh.Heading = (float) LscCarPos[7, 3];
                    
                    Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                    Camera.IsActive = true;
                    Camera.PointAt(new Vector3((float) LscCarPos[7, 0], (float) LscCarPos[7, 1], (float) LscCarPos[7, 2]));
                    Camera.Position = new Vector3((float) CamPos8[CamPos, 0], (float) CamPos8[CamPos, 1], (float) CamPos8[CamPos, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
            }
            
            MenuList.ShowLscTunningMenu(lscId, GetVehicleModSlots(veh), _countSlots, veh);
            UI.HideLoadDisplay();
        }

        public static async void ColorVeh(string number, int lscId)
        {
            CitizenFX.Core.Vehicle veh = Managers.Vehicle.GetVehicleByNumber(number);
            var vehId = Managers.Vehicle.GetVehicleIdByNumber(number);

            if (vehId == -1)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_52"));
                return;
            }
            
            var vehItem = await Managers.Vehicle.GetAllData(vehId);
            
            if (!vehItem.IsUserOwner)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_52"));
                return;
            }
            
            UI.ShowLoadDisplay();

            IsOpenColorMenu = true;
            FreezeEntityPosition(veh.Handle, true);
            
            switch (lscId)
            {
                case 14:
                    veh.Position = new Vector3((float) LscCarColorPos[0, 0], (float) LscCarColorPos[0, 1], (float) LscCarColorPos[0, 2]);
                    veh.Heading = (float) LscCarColorPos[0, 3];
                    
                    Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                    Camera.IsActive = true;
                    Camera.PointAt(new Vector3((float) LscCarColorPos[0, 0], (float) LscCarColorPos[0, 1], (float) LscCarColorPos[0, 2]));
                    Camera.Position = new Vector3((float) CamColorPos[0, 0], (float) CamColorPos[0, 1], (float) CamColorPos[0, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 54:
                    veh.Position = new Vector3((float) LscCarColorPos[1, 0], (float) LscCarColorPos[1, 1], (float) LscCarColorPos[1, 2]);
                    veh.Heading = (float) LscCarColorPos[1, 3];
                    
                    Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                    Camera.IsActive = true;
                    Camera.PointAt(new Vector3((float) LscCarColorPos[1, 0], (float) LscCarColorPos[1, 1], (float) LscCarColorPos[1, 2]));
                    Camera.Position = new Vector3((float) CamColorPos[1, 0], (float) CamColorPos[1, 1], (float) CamColorPos[1, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 55:
                    veh.Position = new Vector3((float) LscCarColorPos[2, 0], (float) LscCarColorPos[2, 1], (float) LscCarColorPos[2, 2]);
                    veh.Heading = (float) LscCarColorPos[2, 3];
                    
                    Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                    Camera.IsActive = true;
                    Camera.PointAt(new Vector3((float) LscCarColorPos[2, 0], (float) LscCarColorPos[2, 1], (float) LscCarColorPos[2, 2]));
                    Camera.Position = new Vector3((float) CamColorPos[2, 0], (float) CamColorPos[2, 1], (float) CamColorPos[2, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 56:
                    veh.Position = new Vector3((float) LscCarColorPos[3, 0], (float) LscCarColorPos[3, 1], (float) LscCarColorPos[3, 2]);
                    veh.Heading = (float) LscCarColorPos[3, 3];
                    
                    Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                    Camera.IsActive = true;
                    Camera.PointAt(new Vector3((float) LscCarColorPos[3, 0], (float) LscCarColorPos[3, 1], (float) LscCarColorPos[3, 2]));
                    Camera.Position = new Vector3((float) CamColorPos[3, 0], (float) CamColorPos[3, 1], (float) CamColorPos[3, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 57:
                    veh.Position = new Vector3((float) LscCarColorPos[4, 0], (float) LscCarColorPos[4, 1], (float) LscCarColorPos[4, 2]);
                    veh.Heading = (float) LscCarColorPos[4, 3];
                    
                    Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                    Camera.IsActive = true;
                    Camera.PointAt(new Vector3((float) LscCarColorPos[4, 0], (float) LscCarColorPos[4, 1], (float) LscCarColorPos[4, 2]));
                    Camera.Position = new Vector3((float) CamColorPos[4, 0], (float) CamColorPos[4, 1], (float) CamColorPos[4, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 71:
                    veh.Position = new Vector3((float) LscCarColorPos[5, 0], (float) LscCarColorPos[5, 1], (float) LscCarColorPos[5, 2]);
                    veh.Heading = (float) LscCarColorPos[5, 3];
                    
                    Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                    Camera.IsActive = true;
                    Camera.PointAt(new Vector3((float) LscCarColorPos[5, 0], (float) LscCarColorPos[5, 1], (float) LscCarColorPos[5, 2]));
                    Camera.Position = new Vector3((float) CamColorPos[5, 0], (float) CamColorPos[5, 1], (float) CamColorPos[5, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 121:
                    veh.Position = new Vector3((float) LscCarColorPos[6, 0], (float) LscCarColorPos[6, 1], (float) LscCarColorPos[6, 2]);
                    veh.Heading = (float) LscCarColorPos[6, 3];
                    
                    Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                    Camera.IsActive = true;
                    Camera.PointAt(new Vector3((float) LscCarColorPos[6, 0], (float) LscCarColorPos[6, 1], (float) LscCarColorPos[6, 2]));
                    Camera.Position = new Vector3((float) CamColorPos[6, 0], (float) CamColorPos[6, 1], (float) CamColorPos[6, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
                case 123:
                    veh.Position = new Vector3((float) LscCarColorPos[7, 0], (float) LscCarColorPos[7, 1], (float) LscCarColorPos[7, 2]);
                    veh.Heading = (float) LscCarColorPos[7, 3];
                    
                    Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                    Camera.IsActive = true;
                    Camera.PointAt(new Vector3((float) LscCarColorPos[7, 0], (float) LscCarColorPos[7, 1], (float) LscCarColorPos[7, 2]));
                    Camera.Position = new Vector3((float) CamColorPos[7, 0], (float) CamColorPos[7, 1], (float) CamColorPos[7, 2]);
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    break;
            }
            
            MenuList.ShowLscColorMenu(lscId, veh);
            UI.HideLoadDisplay();
        }

        public static void CloseMenu()
        {            
            UI.ShowLoadDisplay();

            CamPos = 0;
            IsOpenMenu = false;
            IsOpenColorMenu = false;
            RenderScriptCams(false, true, 500, true, true);
            
            UI.HideLoadDisplay();
        }
        
        

        public static async void CloseMenuWithRemoveMod(CitizenFX.Core.Vehicle veh)
        {            
            UI.ShowLoadDisplay();
            
            CamPos = 0;
            IsOpenMenu = false;
            IsOpenColorMenu = false;
            RenderScriptCams(false, true, 500, true, true);
            FreezeEntityPosition(veh.Handle, false);
            
            var vehId = Managers.Vehicle.GetVehicleIdByNumber(Managers.Vehicle.GetVehicleNumber(veh.Handle));
            var vehItem = await Managers.Vehicle.GetAllData(vehId);
            SetVehicleColours(veh.Handle, vehItem.color1, vehItem.color2);
            SetVehicleLivery(veh.Handle, vehItem.Livery);
            Managers.Vehicle.LoadCarMod(veh, vehItem.upgrade);
            
            UI.HideLoadDisplay();
        }

        public static void ShowVehicleRepairList(int lscId)
        {
            var vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[0, 0], (float) LscCarPos[0, 1], (float) LscCarPos[0, 2]), 4f);
            switch (lscId)
            {
                case 14:
                    MenuList.ShowLscRepairList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 54:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[1, 0], (float) LscCarPos[1, 1], (float) LscCarPos[1, 2]), 4f);
                    MenuList.ShowLscRepairList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 55:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[2, 0], (float) LscCarPos[2, 1], (float) LscCarPos[2, 2]), 4f);
                    MenuList.ShowLscRepairList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 56:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[3, 0], (float) LscCarPos[3, 1], (float) LscCarPos[3, 2]), 4f);
                    MenuList.ShowLscRepairList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 57:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[4, 0], (float) LscCarPos[4, 1], (float) LscCarPos[4, 2]), 4f);
                    MenuList.ShowLscRepairList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 71:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[5, 0], (float) LscCarPos[5, 1], (float) LscCarPos[5, 2]), 4f);
                    MenuList.ShowLscRepairList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 121:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[6, 0], (float) LscCarPos[6, 1], (float) LscCarPos[6, 2]), 4f);
                    MenuList.ShowLscRepairList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 123:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[7, 0], (float) LscCarPos[7, 1], (float) LscCarPos[7, 2]), 4f);
                    MenuList.ShowLscRepairList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
            }
        }

        public static void ShowVehicleNumberList(int lscId)
        {
            var vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[0, 0], (float) LscCarPos[0, 1], (float) LscCarPos[0, 2]), 4f);
            switch (lscId)
            {
                case 14:
                    MenuList.ShowVehicleNumberList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 54:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[1, 0], (float) LscCarPos[1, 1], (float) LscCarPos[1, 2]), 4f);
                    MenuList.ShowVehicleNumberList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 55:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[2, 0], (float) LscCarPos[2, 1], (float) LscCarPos[2, 2]), 4f);
                    MenuList.ShowVehicleNumberList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 56:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[3, 0], (float) LscCarPos[3, 1], (float) LscCarPos[3, 2]), 4f);
                    MenuList.ShowVehicleNumberList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 57:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[4, 0], (float) LscCarPos[4, 1], (float) LscCarPos[4, 2]), 4f);
                    MenuList.ShowVehicleNumberList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 71:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[5, 0], (float) LscCarPos[5, 1], (float) LscCarPos[5, 2]), 4f);
                    MenuList.ShowVehicleNumberList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 121:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[6, 0], (float) LscCarPos[6, 1], (float) LscCarPos[6, 2]), 4f);
                    MenuList.ShowVehicleNumberList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 123:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[7, 0], (float) LscCarPos[7, 1], (float) LscCarPos[7, 2]), 4f);
                    MenuList.ShowVehicleNumberList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
            }
        }

        public static void ShowVehicleTunningList(int lscId)
        {
            var vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[0, 0], (float) LscCarPos[0, 1], (float) LscCarPos[0, 2]), 4f);
            switch (lscId)
            {
                case 14:
                    MenuList.ShowLscTunList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 54:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[1, 0], (float) LscCarPos[1, 1], (float) LscCarPos[1, 2]), 4f);
                    MenuList.ShowLscTunList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 55:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[2, 0], (float) LscCarPos[2, 1], (float) LscCarPos[2, 2]), 4f);
                    MenuList.ShowLscTunList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 56:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[3, 0], (float) LscCarPos[3, 1], (float) LscCarPos[3, 2]), 4f);
                    MenuList.ShowLscTunList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 57:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[4, 0], (float) LscCarPos[4, 1], (float) LscCarPos[4, 2]), 4f);
                    MenuList.ShowLscTunList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 71:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[5, 0], (float) LscCarPos[5, 1], (float) LscCarPos[5, 2]), 4f);
                    MenuList.ShowLscTunList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 121:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[6, 0], (float) LscCarPos[6, 1], (float) LscCarPos[6, 2]), 4f);
                    MenuList.ShowLscTunList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 123:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarPos[7, 0], (float) LscCarPos[7, 1], (float) LscCarPos[7, 2]), 4f);
                    MenuList.ShowLscTunList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
            }
        }

        public static void ShowVehicleColorList(int lscId)
        {
            var vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarColorPos[0, 0], (float) LscCarColorPos[0, 1], (float) LscCarColorPos[0, 2]), 5f);
            switch (lscId)
            {
                case 14:
                    MenuList.ShowLscClrList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 54:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarColorPos[1, 0], (float) LscCarColorPos[1, 1], (float) LscCarColorPos[1, 2]), 5f);
                    MenuList.ShowLscClrList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 55:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarColorPos[2, 0], (float) LscCarColorPos[2, 1], (float) LscCarColorPos[2, 2]), 5f);
                    MenuList.ShowLscClrList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 56:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarColorPos[3, 0], (float) LscCarColorPos[3, 1], (float) LscCarColorPos[3, 2]), 5f);
                    MenuList.ShowLscClrList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 57:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarColorPos[4, 0], (float) LscCarColorPos[4, 1], (float) LscCarColorPos[4, 2]), 5f);
                    MenuList.ShowLscClrList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 71:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarColorPos[5, 0], (float) LscCarColorPos[5, 1], (float) LscCarColorPos[5, 2]), 5f);
                    MenuList.ShowLscClrList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 121:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarColorPos[6, 0], (float) LscCarColorPos[6, 1], (float) LscCarColorPos[6, 2]), 5f);
                    MenuList.ShowLscClrList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
                case 123:
                    vehList = Main.GetVehicleListOnRadius(new Vector3((float) LscCarColorPos[7, 0], (float) LscCarColorPos[7, 1], (float) LscCarColorPos[7, 2]), 5f);
                    MenuList.ShowLscClrList(lscId, vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                    break;
            }
        }
        
        public static List<int> GetVehicleModSlots(CitizenFX.Core.Vehicle vehicle)
        {
            List<int> availableslots = new List<int>();
            _countSlots.Clear();

            SetVehicleModKit(vehicle.Handle, 0);
            for (var i = 0; i < 76; i++)
            {
                if (GetNumVehicleMods(vehicle.Handle, i) <= 0) continue;
                availableslots.Add(i);
                _countSlots.Add(GetNumVehicleMods(vehicle.Handle, i));
            }
            return availableslots;
        }

        public static Dictionary<int, int> GetVehicleModSlotsFormat(CitizenFX.Core.Vehicle vehicle)
        {
            SetVehicleModKit(vehicle.Handle, 0);
            Dictionary<int, int> availableslots = new Dictionary<int, int>();
            for (var i = 0; i < 76; i++)
                if (GetNumVehicleMods(vehicle.Handle, i) > 0)
                    availableslots.Add(i, -1);
            return availableslots;
        }
    }
}