using CitizenFX.Core;
using System;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Jobs
{
    public class Bus : BaseScript
    {
        private static bool _isBus1 = false;
        private static bool _isBus2 = false;
        private static bool _isBus3 = false;
        private static int _currentCheckpoint = 0;
        private static int _currentId = 0;
        
        public static double[,] Bus1 =
        {
            { -20.77613, -1355.558, 28.17333 },
            { -69.27248, -1364.598, 28.39273 },
            {-207.2123, -1411.102, 30.23419},
            {-250.3917, -1419.969, 30.24708},
            {-351.4041, -1419.679, 28.43443},
            {-434.4893, -1412.981, 28.23767},
            {-526.2911, -1114.002, 20.93598},
            {-534.7128, -985.7481, 22.2819},
            {-502.8471, -864.3864, 29.00174},
            {-609.1118, -834.1956, 24.54251},
            {-647.7816, -869.95, 23.4784},
            {-646.2023, -935.5083, 21.39142},
            {-751.9155, -1095.719, 9.76653},
            {-885.2305, -1171.75, 3.808795},
            {-930.7425, -1198.454, 4.070882},
            {-1049.229, -1266.24, 5.215746},
            {-1138.747, -1304.63, 4.08339},
            {-1203.084, -1191.843, 6.607671},
            {-1240.451, -1080.135, 7.41134},
            {-1277.571, -946.8057, 10.30568},
            {-1349.633, -813.9242, 17.31865},
            {-1412.26, -761.7822, 21.47782 },
            {-1518.48, -688.4073, 27.45993},
            {-1622.958, -596.4943, 32.06582},
            {-1586.303, -534.6346, 34.38402},
            {-1477.075, -463.451, 34.40791},
            {-1429.307, -435.4835, 34.76416},
            {-1401.365, -416.0498, 35.48946},
            {-1320.327, -367.8933, 35.67199},
            {-1090.98, -279.3097, 36.6939},
            {-1014.448, -244.1829, 36.64638},
            {-912.8623, -268.655, 39.57436},
            {-826.0239, -316.0187, 36.73166},
            {-756.6213, -346.8496, 34.85471},
            {-665.9775, -374.3257, 33.63932},
            {-548.4927, -376.5024, 34.062},
            {-315.4498, -406.8911, 29.03227},
            {-224.8235, -436.4268, 29.54648},
            {-249.4325, -635.1298, 32.57001},
            {-203.4997, -696.7413, 32.83302},
            {-152.6661, -713.5783, 33.6908},
            {1.240081, -761.6959, 31.01256},
            {143.6725, -807.9731, 30.20995},
            {231.8147, -691.0505, 35.4769},
            {246.5911, -646.074, 38.61546},
            {303.3329, -492.8282, 42.35616},
            {316.535, -416.0796, 43.94266},
            {347.8625, -309.5445, 51.84609},
            {349.7827, -298.8471, 52.67729},
            {398.3643, -156.267, 63.35838},
            {517.5294, 38.56313, 92.93687},
            {665.2781, 20.48015, 83.85236},
            {770.9596, -44.92016, 79.91208},
            {968.2475, -177.0578, 72.06339},
            {991.9081, -190.6512, 70.63889},
            {1211.547, -348.5237, 68.12844},
            {1182.876, -442.515, 65.7378},
            {1179.259, -488.382, 64.65414},
            {1174.542, -612.1591, 62.73099},
            {1196.08, -738.2855, 57.70032},
            {1178.955, -820.3583, 54.49027},
            {1150.809, -927.1675, 47.87659},
            {1008.341, -985.3448, 41.27326},
            {826.1581, -999.9065, 25.36019},
            {808.5326, -1001.976, 24.18585},
            {419.95, -1038.981, 28.68525},
            {321.5047, -1036.647, 28.11785}//67
        };

        public static double[,] Bus2 =
        { { -925.1628, -2320.734, 19.11593},
            { -1032.885, -2729.676, 19.11244 },
            { -809.0793, -2466.783, 12.85843 },
            { -686.7441, -2128.663, 12.62766},
            { -347.187, -2111.682, 23.03776 },
            { -168.0055, -2108.106, 23.79468 },
            { 117.9054, -2044.946, 17.37168 },
            { 215.6207, -1954.056, 20.51428 },
            { 267.7746, -1891.401, 25.65105 },
            { 365.6594, -1775.07, 28.16373 },
            { 454.3903, -1661.197, 28.29699 },
            {350.7339, -1532.453, 28.30629},
            {257.7349, -1453.839, 28.33812},
            {182.2527, -1404.326, 28.34013},
            {-22.58355, -1355.378, 28.16117}//15
            
        };

        public static double[,] Bus3 =
        {
            { 387.0436, -672.6332, 28.04908 },
            { 1556.16, 881.731, 76.29194 },
            { 2430.731, 2860.752, 47.84104 },
            { 1959.371, 2981.862, 44.56105},
            { 1092.098, 2691.49, 37.61154 } ,
            {395.4369, 2671.72, 43.16005},
            {302.9107, 2644.307, 43.35554},
            {223.9788, 3070.557, 41.10066},
            {1249.81, 3532.767, 34.02944},
            {1591.604, 3662.07, 33.30909},
            {1661.828, 3565.755, 34.36056},
            {1934.406, 3704.664, 31.28149},
            {2025.247, 3757.067, 31.12759},
            {2053.286, 3731.671, 31.79866},
            {2507.79, 4118.888, 37.31491},
            {1678.677, 4823.556, 40.80423},
            {1954.646, 5138.808, 42.20303},
            {2594.617, 5100.798, 42.58412},
            {2626.005, 5108.932, 43.64918},
            {1655.054, 6414.708, 28.02375},
            {165.1584, 6548.34, 30.7405},
            {89.17739, 6596.896, 30.35389},
            {-161.4769, 6383.953, 30.15224},
            {-291.3061, 6247.273, 30.24008},
            {-436.7479, 6056.877, 30.19872},
            {-413.9703, 5980.956, 30.43074},
            {-937.0167, 5426.759, 36.7452},
            {-1529.167, 4995.497, 61.18305},
            {-2229.287, 4323.936, 47.58266},
            {-2498.996, 3608.509, 13.14034},
            {-2729.138, 2298.377, 17.5265},
            {-3118.411, 1186.622, 19.18515},
            {-3015.445, 335.9182, 13.41847},
            {-2203.097, -356.1289, 11.98739},
            {-1837.839, -603.1777, 10.20072},
            {245.5144, -550.312, 41.75318},
            {251.5672, -573.588, 42.02026},
            {182.4826, -791.8699, 30.3394},
            {261.0313, -856.171, 28.23261},
            {386.5411, -860.2405, 28.16551},
            {247.6562, -583.2525, 42.94083},
            {468.184, -604.3215, 27.32677}//42
        };

        

        public static void Start(int busType)
        {
            if (_isBus1 || _isBus2 || _isBus3)
                Stop();
            
            switch (busType)
            {
                case 1:
                    if (_isBus1) break;
                    _isBus1 = true;
                    _currentId = 0;

                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_119"));
                    var pickup1Pos = new Vector3((float) Bus1[_currentId, 0], (float) Bus1[_currentId, 1],
                        (float) Bus1[_currentId, 2]);
                    _currentCheckpoint = Managers.Checkpoint.CreateWithMarker(pickup1Pos, 3f, 4f, Marker.Red.R,
                        Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:bus1:work");
                    _currentId++;
                    
                    User.SetWaypoint(pickup1Pos.X, pickup1Pos.Y);
                    
                    Managers.Blip.Create(pickup1Pos);
                    break;
                case 2:
                    if(_isBus2) break;
                    _isBus2 = true;
                    _currentId = 0;
                    
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_119"));
                    var pickup2Pos = new Vector3((float) Bus2[_currentId, 0], (float) Bus2[_currentId, 1], (float) Bus2[_currentId, 2]);
                    _currentCheckpoint = Managers.Checkpoint.CreateWithMarker(pickup2Pos, 3f, 4f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:bus2:work");
                    _currentId++;
                    
                    User.SetWaypoint(pickup2Pos.X, pickup2Pos.Y);
                    Managers.Blip.Create(pickup2Pos);
                    
                    break;
                case 3:
                    if(_isBus3) break;
                    _isBus3 = true;
                    _currentId = 0;
                    
                    
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_119"));
                    var pickup3Pos = new Vector3((float) Bus3[_currentId, 0], (float) Bus3[_currentId, 1], (float) Bus3[_currentId, 2]);
                    _currentCheckpoint = Managers.Checkpoint.CreateWithMarker(pickup3Pos, 3f, 4f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:bus3:work");
                    _currentId++;

                    User.SetWaypoint(pickup3Pos.X, pickup3Pos.Y);
                    Managers.Blip.Create(pickup3Pos);
                    break;
            }
        }

        public static async void NextCheckpoint()
        {
            if (_isBus1)
            {
                if (_currentId == 1 || _currentId == 11 || _currentId == 18 || _currentId == 27 ||
                    _currentId == 31 || _currentId == 34 || _currentId == 40 || _currentId == 44 ||
                    _currentId == 48 || _currentId == 54 || _currentId == 57 || _currentId == 64 ||
                    _currentId == 67)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_120"));
                    User.Freeze(PlayerId(), true);
                    await Delay(10000);
                    User.Freeze(PlayerId(), false);

                }
            }
            if (_isBus2)
            {
                if (_currentId == 2 || _currentId == 15)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_120"));
                    User.Freeze(PlayerId(), true);
                    await Delay(10000);
                    User.Freeze(PlayerId(), false);

                }
            }
            if (_isBus3)
            {
                if (_currentId == 2 || _currentId == 4 || _currentId == 5 || _currentId == 6 ||
                    _currentId == 8 || _currentId == 12 || _currentId == 16 || _currentId == 20 ||
                    _currentId == 23 || _currentId == 25 || _currentId == 27 || _currentId == 28 ||
                    _currentId == 29 || _currentId == 30 || _currentId == 31 || _currentId == 32 ||
                    _currentId == 33 ||_currentId == 35 || _currentId == 37 || _currentId == 42)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_120"));
                    User.Freeze(PlayerId(), true);
                    await Delay(10000);
                    User.Freeze(PlayerId(), false);

                }
            }
            if (IsPedInAnyVehicle(PlayerPedId(), true))
            {
                
                var veh = GetVehiclePedIsUsing(PlayerPedId());
                
                
                if (GetPedInVehicleSeat(veh, -1) == PlayerPedId())
                {
                    var v = new CitizenFX.Core.Vehicle(veh);

                    switch (v.Model.Hash)
                    {
                        case -713569950:
                            if (!_isBus1)
                            {
                                Stop();
                                break;

                            }



                            if (_currentId >= 67)
                            {
                                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_121"));
                                var rand = new Random();
                                int number = rand.Next(600,900);
                                User.GiveJobMoney(number);

                                Managers.Blip.Delete();
                                Managers.Checkpoint.DeleteWithMarker(_currentCheckpoint);

                                _isBus1 = false;
                                _currentCheckpoint = 0;
                                _currentId = 0;
                                User.Freeze(PlayerId(), false);
                                break;
                            }

                            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_122"));
                            var pickup1Pos = new Vector3((float) Bus1[_currentId, 0], (float) Bus1[_currentId, 1],
                                (float) Bus1[_currentId, 2]);
                            _currentCheckpoint = Managers.Checkpoint.CreateWithMarker(pickup1Pos, 3f, 4f, Marker.Red.R,
                                Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:bus1:work");
                            _currentId++;

                            User.SetWaypoint(pickup1Pos.X, pickup1Pos.Y);
                            User.Freeze(PlayerId(), false);

                            Managers.Blip.Create(pickup1Pos);
                            break;
                       case 1283517198:
                            if (!_isBus2)
                            {
                                Stop();
                                break;

                            }



                            if (_currentId >= 15)
                            {
                                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_121"));
                                var rand = new Random();
                                int number = rand.Next(300,400);
                                User.GiveJobMoney(number);
                                
                                Managers.Blip.Delete();
                                Managers.Checkpoint.DeleteWithMarker(_currentCheckpoint);

                                _isBus1 = false;
                                _currentCheckpoint = 0;
                                _currentId = 0;
                                User.Freeze(PlayerId(), false);
                                break;
                            }
                           Notification.SendWithTime(Lang.GetTextToPlayer("_lang_122"));
                            var pickup2Pos = new Vector3((float) Bus2[_currentId, 0], (float) Bus2[_currentId, 1],
                                (float) Bus2[_currentId, 2]);
                            _currentCheckpoint = Managers.Checkpoint.CreateWithMarker(pickup2Pos, 3f, 4f, Marker.Red.R,
                                Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:bus1:work");
                            _currentId++;
                           
                            User.SetWaypoint(pickup2Pos.X, pickup2Pos.Y);
                            User.Freeze(PlayerId(), false);

                            Managers.Blip.Create(pickup2Pos);
                            break;
                        case -2072933068:
                            if(!_isBus3) 
                            {
                                Stop();
                                break;
                            }
                            
                            if (_currentId >= 42)
                            {
                                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_121"));
                                
                                Managers.Blip.Delete();
                                Managers.Checkpoint.DeleteWithMarker(_currentCheckpoint);
                                
                                var rand = new Random();
                                int number = rand.Next(1200,1800);
                                User.GiveJobMoney(number);
                                
                                _isBus3 = false;
                                _currentCheckpoint = 0;
                                _currentId = 0;
                                User.Freeze(PlayerId(), false);
                                break;
                            }
                            
                            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_122"));
                            var pickup3Pos = new Vector3((float) Bus3[_currentId, 0], (float) Bus3[_currentId, 1], (float) Bus3[_currentId, 2]);
                            _currentCheckpoint = Managers.Checkpoint.CreateWithMarker(pickup3Pos, 3f, 4f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:bus3:work");
                            _currentId++;
                            
                            User.Freeze(PlayerId(), false);
                            User.SetWaypoint(pickup3Pos.X, pickup3Pos.Y);
                            
                            Managers.Blip.Create(pickup3Pos);
                            break;
                        default:
                            Stop();
                            break;
                    }
                }
                else
                    Stop();
            }
            else
                Stop();
        }

        public static void Stop()
        {
            Managers.Blip.Delete();
            
            Managers.Checkpoint.DeleteWithMarker(_currentCheckpoint);
            User.Freeze(PlayerId(), false);
            _isBus1 = false;
            _isBus2 = false;
            _isBus3 = false;
            _currentCheckpoint = 0;
            _currentId = 0;
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_123"));
        }
    }
}