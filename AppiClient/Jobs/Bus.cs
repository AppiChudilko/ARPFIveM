using CitizenFX.Core;
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
            { -1032.266, -2724.416, 12.65254 },
            { 189.878, -1988.402, 17.70164 },
            { 146.5654, -1734.431, 28.08748 },
            { -214.3812, -1003.525, 28.17548 },
            { -72.5507, -618.2736, 35.09362 },
            { -503.8881, 20.96746, 43.68846 },
            { -1191.871, -270.6994, 36.61627 },
            { -1619.696, -531.4001, 33.41887 },
            { -1231.549, -1134.148, 6.699632 },
            { -658.9761, -1400.086, 9.50183 },
            { -556.3972, -1753.336, 20.77087 } //10
        };

        public static double[,] Bus2 =
        {
            { -1031.312, -2725.107, 12.64634 },
            { -214.3812, -1003.525, 28.17548 } //1
        };

        public static double[,] Bus3 =
        {
            { -214.3812, -1003.525, 28.17548 },
            { 2761.002, 4618.438, 43.94569 },
            { -216.0822, 6172.684, 30.2277 },
            { -2275.199, 4255.438, 42.92985 },
            { -3014.472, 368.9521, 13.75097 } //4
        };

        public static void Start(int busType)
        {
            if (_isBus1 || _isBus2 || _isBus3)
                Stop();
            
            switch (busType)
            {
                case 1:
                    if(_isBus1) break;
                    _isBus1 = true;
                    _currentId = 0;
                    
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_119"));
                    var pickup1Pos = new Vector3((float) Bus1[_currentId, 0], (float) Bus1[_currentId, 1], (float) Bus1[_currentId, 2]);
                    _currentCheckpoint = Managers.Checkpoint.CreateWithMarker(pickup1Pos, 3f, 4f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:bus1:work");
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
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_120"));
            User.Freeze(PlayerId(), true);
            await Delay(10000);
            
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

                            if (_currentId > 10)
                            {
                                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_121"));
                                
                                User.GiveJobMoney(200);
                                
                                Managers.Blip.Delete();
                                Managers.Checkpoint.DeleteWithMarker(_currentCheckpoint);
                                
                                _isBus1 = false;
                                _currentCheckpoint = 0;
                                _currentId = 0;
                                User.Freeze(PlayerId(), false);
                                break;
                            }
                    
                            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_122"));
                            var pickup1Pos = new Vector3((float) Bus1[_currentId, 0], (float) Bus1[_currentId, 1], (float) Bus1[_currentId, 2]);
                            _currentCheckpoint = Managers.Checkpoint.CreateWithMarker(pickup1Pos, 3f, 4f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:bus1:work");
                            _currentId++;
                    
                            User.SetWaypoint(pickup1Pos.X, pickup1Pos.Y);
                            User.Freeze(PlayerId(), false);
                            
                            Managers.Blip.Create(pickup1Pos);
                            break;
                        case 1283517198:
                            if(!_isBus2)
                            {
                                Stop();
                                break;
                            }
                            
                            if (_currentId > 1)
                            {
                                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_121"));
                                
                                User.GiveJobMoney(70);
                                
                                Managers.Blip.Delete();
                                Managers.Checkpoint.DeleteWithMarker(_currentCheckpoint);
                                
                                _isBus2 = false;
                                _currentCheckpoint = 0;
                                _currentId = 0;
                                User.Freeze(PlayerId(), false);
                                break;
                            }
                    
                            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_122"));
                            var pickup2Pos = new Vector3((float) Bus2[_currentId, 0], (float) Bus2[_currentId, 1], (float) Bus2[_currentId, 2]);
                            _currentCheckpoint = Managers.Checkpoint.CreateWithMarker(pickup2Pos, 3f, 4f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "job:bus2:work");
                            _currentId++;
                            
                            User.Freeze(PlayerId(), false);
                            User.SetWaypoint(pickup2Pos.X, pickup2Pos.Y);
                            
                            Managers.Blip.Create(pickup2Pos);
                            break;
                        case -2072933068:
                            if(!_isBus3) 
                            {
                                Stop();
                                break;
                            }
                            
                            if (_currentId > 4)
                            {
                                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_121"));
                                
                                Managers.Blip.Delete();
                                Managers.Checkpoint.DeleteWithMarker(_currentCheckpoint);
                                
                                User.GiveJobMoney(365);

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