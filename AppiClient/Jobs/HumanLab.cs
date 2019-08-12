using System;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Jobs
{
    public class HumanLab : BaseScript
    {
        public static bool IsProcess = false;
        
        public static readonly double[,] WaterCheckPos =
        {
            {-2147.697, -500.8055, 1.808812},
            {-1802.926, -979.5959, 0.8247299},
            {-1529.48, -1170.853, 0.6652265},
            {-1150.43, -1883.995, 0.8556018},
            {-951.2028, -984.1022, 0.2137202},
            {-785.668, -1493.263, 0.5745819},
            {-113.2224, -1879.044, 0.6524521},
            {1215.647, -2708.443, 0.6189926},
            {1109.928, -1220.048, 15.36792},
            {663.9535, -498.1472, 15.08265},
            {1100.557, -555.3084, 55.89379},
            {1264.158, -1045.235, 38.68134},
            {1098.827, -160.9295, 53.86881},
            {1928.864, 410.9628, 161.0442},
            {2837.639, -679.6146, 0.5003721},
            {29.41303, 871.0053, 196.5249},
            {-180.1949, 795.683, 196.4767},
            {-3116.456, 438.9633, 0.9076679},
            {-3225.826, 1353.027, 0.7466202},
            {-1500.024, 1574.277, 105.0456},
            {-1651.97, 2579.359, 0.5195942},
            {-2090.691, 2612.525, 0.4330854},
            {-1245.869, 2664.031, 0.4098645},
            {-415.5812, 2944.076, 13.85313},
            {297.7439, 3553.998, 30.04643},
            {-170.1436, 4137.855, 30.58113},
            {-215.3174, 4327.854, 30.36332},
            {-870.7251, 4432.679, 15.36371},
            {-1657.555, 4464.896, 0.3635531},
            {-1879.15, 4783.202, 0.9304351},
            {-3194.069, 3262.599, 0.3009984},
            {-1005.672, 6275.037, 1.211535},
            {-127.4178, 6736.716, 0.7080706},
            {146.1358, 7097.081, 0.4930558},
            {1508.745, 6639.454, 1.264099},
            {2588.054, 6141.128, 162.1035},
            {3366.186, 5193.819, 0.1716362},
            {3842.051, 4489.926, 0.8470399},
            {2434.03, 4618.897, 29.06884},
            {2119.215, 4575.23, 30.61998},
            {2166.003, 3830.607, 30.69661},
            {1580.655, 3914.799, 30.49529},
            {1413.794, 4258.563, 30.6244},
            {706.6923, 4139.002, 30.32208},
            {-1691.388, -207.5315, 56.70388}
        };

        public static readonly double[,] GroundCheckPos =
        {
            {1673.238, -2497.599, 79.63357},
            {1387.134, -1945.667, 65.68669},
            {2004.425, -886.1641, 79.07301},
            {2730.614, -740.934, 20.75201},
            {1650.889, -64.17523, 164.6689},
            {2169.16, 128.5417, 228.2811},
            {1976.624, 905.296, 223.8133},
            {-2076.867, -126.9384, 36.53727},
            {-2303.513, 545.1491, 182.4584},
            {-2236.82, 1044.593, 208.015},
            {-3130.185, 1343.642, 20.21685},
            {-2820.715, 2249.65, 29.77978},
            {-2362.491, 2787.569, 2.682229},
            {-2270.325, 4322.655, 43.0285},
            {-1512.462, 4239.353, 65.27418},
            {-1216.76, 4444.553, 29.99684},
            {-1133.724, 4660.337, 243.7695},
            {-274.677, 4686.053, 236.7642},
            {-483.1308, 5619.704, 64.67578},
            {72.52802, 7049.892, 15.5201},
            {821.9828, 6449.445, 31.53106},
            {1624.15, 6655.603, 23.72502},
            {202.1006, 5278.594, 610.1451},
            {1392.455, 5536.185, 466.5204},
            {1600.266, 5804.852, 415.5432},
            {1674.373, 5143.856, 150.8606},
            {3364.964, 5454.675, 17.09648},
            {3623.534, 4518.104, 38.99737},
            {2442.344, 4397.227, 34.90043},
            {1540.602, 4520.084, 59.09164},
            {167.5663, 4399.423, 78.29996},
            {-393.0041, 4380.564, 54.61767},
            {-144.3843, 2916.276, 40.98077},
            {-1257.691, 2500.003, 29.30817},
            {-2061.354, 1984.752, 197.8799},
            {-1339.939, 728.8558, 185.5612},
            {-315.0531, 1296.85, 345.9303},
            {-229.5341, 2153.169, 146.8271},
            {591.9678, 2092.038, 86.7029},
            {866.6752, 1194.328, 345.6783},
            {1110.347, 734.1995, 156.8002},
            {2427.27, 2006.669, 84.59879},
            {2953.969, 2786.999, 41.49084},
            {2397.269, 3685.833, 56.83162},
            {1379.197, 2642.405, 47.51794},
            {1094.303, 3243.898, 37.71872}
        };

        public static void Start()
        {
            if (IsProcess)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_109"));
                return;
            }
            
            Random random = new Random();
            
            if (User.IsJobScienceWater())
            {
                var r = random.Next(44);
                var pos = new Vector3((float) WaterCheckPos[r, 0], (float) WaterCheckPos[r, 1], (float) WaterCheckPos[r, 2] - 1);
                
                Managers.Checkpoint.CreateWithMarker(pos, 1f, 1f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:hlab:work");
                Managers.Blip.Create(pos);
                User.SetWaypoint(pos.X, pos.Y);
                IsProcess = true;
            }
            if (User.IsJobScienceGround())
            {
                var r = random.Next(45);
                var pos = new Vector3((float) GroundCheckPos[r, 0], (float) GroundCheckPos[r, 1], (float) GroundCheckPos[r, 2] - 1);
                
                Managers.Checkpoint.CreateWithMarker(pos, 1f, 1f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:hlab:work");
                Managers.Blip.Create(pos);
                User.SetWaypoint(pos.X, pos.Y);
                IsProcess = true;
            }
        }

        public static async void WorkProcess()
        {
            Notification.SendWithTime("~y~Вы начали брать пробу");
            Managers.Blip.Delete();
            
            User.PlayScenario("WORLD_HUMAN_GARDENER_PLANT");
            User.Freeze(PlayerId(), true);
            
            await Delay(200);
            User.IsBlockAnimation = true;

            await Delay(30000);
                
            IsProcess = false;
            User.IsBlockAnimation = false;
            User.Freeze(PlayerId(), false);
            User.StopScenario();
            
            Sync.Data.SetLocally(User.GetServerId(), "hlab", true);
        }

        public static void DropInCar(CitizenFX.Core.Vehicle veh)
        {
            if (!Sync.Data.HasLocally(User.GetServerId(), "hlab"))
            {
                Notification.SendWithTime("~r~У Вас нет пробы на руках. Нужно её собрать.");
                return;
            }

            if (Sync.Data.HasLocally(VehToNet(veh.Handle), "veh:halbLoad"))
            {
                int count = (int) Sync.Data.GetLocally(VehToNet(veh.Handle), "veh:halbLoad");
                
                if (veh.Model.Hash == 121658888 && count >= 20)
                {
                    Notification.SendWithTime("~r~В машине нет места");
                    return;
                }
                if (veh.Model.Hash == 914654722 && count >= 5)
                {
                    Notification.SendWithTime("~r~В машине нет места");
                    return;
                }
                
                Sync.Data.SetLocally(VehToNet(veh.Handle), "veh:halbLoad", ++count);
                Notification.SendWithTime("~g~Вы положили пробу в транспорт.");
            }
            else
            {
                Sync.Data.SetLocally(VehToNet(veh.Handle), "veh:halbLoad", 1);
                Notification.SendWithTime("~g~Вы положили пробу в транспорт.");
            }
            
            Sync.Data.ResetLocally(User.GetServerId(), "hlab");
        }

        public static void UnloadCar(CitizenFX.Core.Vehicle veh)
        {
            var pos = GetEntityCoords(GetPlayerPed(-1), true);
            if (Main.GetDistanceToSquared(new Vector3(3616.693f, 3730.078f, 28.6901f), pos) > 10f)
            {
                Notification.SendWithTime("~r~Вы слишком далеко от места разгрузки");
                User.SetWaypoint(3616.693f, 3730.078f);
                Managers.Checkpoint.CreateWithMarker(new Vector3(3616.693f, 3730.078f, 27.6901f), 10f, 5f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "checkpoint:withdelete");
                return;
            }
            
            if (!Sync.Data.HasLocally(VehToNet(veh.Handle), "veh:halbLoad"))
            {
                Notification.SendWithTime("~r~Транспорт пуст");
                return;
            }
            
            int count = (int) Sync.Data.GetLocally(VehToNet(veh.Handle), "veh:halbLoad");
                
            if (count == 0)
            {
                Notification.SendWithTime("~r~Транспорт пуст");
                return;
            }
                
            var rand = new Random();
            if (User.Data.job == "swater")
                User.GiveJobMoney(300 * count + rand.Next(200));
            else
                User.GiveJobMoney(250 * count + rand.Next(200));
            Sync.Data.SetLocally(VehToNet(veh.Handle), "veh:halbLoad", 0);
        }
    }
}