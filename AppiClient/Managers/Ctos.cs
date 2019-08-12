using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using Client.Business;
using Client.Vehicle;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Ctos : BaseScript
    {
        public static bool IsBlackoutConnect = false;
        private static bool _isBlackout = false;
        
        private static readonly List<string> _elecBox = new List<string>
        {
            "prop_elecbox_01a",
            "prop_elecbox_01b",
            "prop_elecbox_02a",
            "prop_elecbox_02b",
            "prop_elecbox_03a",
            "prop_elecbox_04a",
            "prop_elecbox_05a",
            "prop_elecbox_06a",
            "prop_elecbox_07a",
            "prop_elecbox_08",
            "prop_elecbox_08b",
            "prop_elecbox_09",
            "prop_elecbox_11",
            "prop_elecbox_24",
            "prop_elecbox_24b",
            "prop_elecbox_11"
        };
        
        private static readonly List<string> _trafficLight = new List<string>
        {
            "prop_trafficdiv_01",
            "prop_trafficdiv_02",
            "prop_traffic_01a",
            "prop_traffic_01b",
            "prop_traffic_01d",
            "prop_traffic_02a",
            "prop_traffic_02b",
            "prop_traffic_03a",
            "prop_traffic_03b",
            "prop_traffic_lightset_01",
            "prop_traffic_rail_1a",
            "prop_traffic_rail_2",
            "prop_traffic_rail_3",
            "prop_plant_palm_01a",
            "prop_plant_palm_01b",
            "prop_plant_palm_01c",
            "prop_traffic_01a"
        };
        
        private static readonly List<string> _streetLight = new List<string>
        {
            "prop_streetlight_01",
            "prop_streetlight_01b",
            "prop_streetlight_02",
            "prop_streetlight_03",
            "prop_streetlight_03b",
            "prop_streetlight_03c",
            "prop_streetlight_03d",
            "prop_streetlight_03e",
            "prop_streetlight_04",
            "prop_streetlight_05",
            "prop_streetlight_05_b",
            "prop_streetlight_07a",
            "prop_streetlight_07b",
            "prop_streetlight_08",
            "prop_streetlight_09",
            "prop_streetlight_10",
            "prop_streetlight_11a",
            "prop_streetlight_11b",
            "prop_streetlight_11c",
            "prop_streetlight_12a",
            "prop_streetlight_12b",
            "prop_streetlight_12c",
            "prop_streetlight_14a",
            "prop_streetlight_15a",
            "prop_streetlight_16a",
            "prop_streetlight_01"
        };
        
        public static double[,] Network =
        {
            { -338.7884, -579.6178, 48.09489 },
            { -293.0222, -632.1178, 47.43132 },
            { -269.2281, -962.775, 143.5142 },
            { 98.88757, -870.8663, 136.9165 },
            { 580.1769, 89.59447, 117.3308 },
            { 423.6479, 15.56825, 151.9242 },
            { 424.9219, 18.586, 151.931 },
            { 551.9955, -28.19887, 93.86244 },
            { 305.863, -284.8494, 68.29829 },
            { 299.488, -313.9493, 68.29829 },
            { 1240.899, -1090.095, 44.35722 },
            { -418.4464, -2804.495, 14.80695 },
            { 802.3354, -2996.213, 27.36875 },
            { 548.3521, -2219.756, 67.94666 },
            { -701.2187, 58.91474, 68.68575 },
            { -696.7746, 208.6952, 139.7731 },
            { -769.8155, 255.006, 134.7385 },
            { -1918.884, -3028.625, 22.61429 },
            { -1039.817, -2385.444, 27.40255 },
            { -1590.373, -3212.547, 28.6604 },
            { -1311.997, -2624.589, 36.11582 },
            { -991.5846, -2774.019, 48.31227 },
            { -556.7017, -119.8519, 50.98835 },
            { -619.0831, -106.5815, 51.01202 },
            { -1152.408, -443.9738, 42.89137 },
            { -1156.081, -498.8079, 49.32043 },
            { -1290.007, -445.2428, 106.4711 },
            { -770.0829, -786.3356, 83.82861 },
            { -824.3132, -719.1805, 120.2517 },
            { -598.8342, -917.809, 35.84408 },
            { -678.5171, -717.0078, 54.09795 },
            { -669.458, -804.2544, 31.8844 },
            { -1463.988, -526.1229, 83.58365 },
            { -1525.904, -596.7999, 66.52119 },
            { -1375.134, -465.2585, 83.51427 },
            { -1711.984, 478.334, 127.1892 },
            { 202.6934, 1204, 230.2588 },
            { 217.0646, 1140.443, 230.2588 },
            { 668.7827, 590.3213, 136.9934 },
            { 722.2471, 562.2682, 134.2943 },
            { 838.1705, 510.1091, 138.6649 },
            { 773.1747, 575.3554, 138.4155 },
            { 735.4507, 231.9995, 145.1368 },
            { 450.932, 5566.451, 795.442 },
            { -449.0599, 6019.923, 35.56564 },
            { -142.5559, 6286.784, 39.26382 },
            { -368.0471, 6105.006, 38.42902 },
            { 2796.773, 5992.872, 354.989 },
            { 3460.883, 3653.532, 51.16711 },
            { 3614.592, 3636.562, 51.16711 },
            { -2180.794, 3252.703, 54.3309 },
            { -2124.381, 3219.853, 54.3309 },
            { -2050.939, 3178.414, 54.3309 },
            { 1858.295, 3694.042, 37.91168 },
            { 1695.486, 3614.863, 37.79684 },
            { 1692.829, 2532.073, 60.33785 },
            { 1692.829, 2647.942, 60.33785 },
            { 1824.353, 2574.386, 60.56225 },
            { 1407.908, 2117.489, 104.1011 },
            { -214.6158, -744.6461, 219.4428 },
            { -166.7245, -590.6718, 199.0783 },
            { 124.2959, -654.8749, 261.8616 },
            { 149.2771, -769.0092, 261.8616 },
            { 253.297, -3145.925, 39.40688 },
            { 207.652, -3145.925, 39.41451 },
            { 207.652, -3307.397, 39.51926 },
            { 247.3365, -3307.397, 39.52404 },
            { 484.2856, -2178.582, 40.25116 },
            { -150.321, -150.2459, 96.1528 },
            { -202.9684, -327.1913, 65.04893 },
            { -1913.77, -3031.85, 22.58777 },
            { -1042.578, -2390.227, 27.40255 },
            { -1583.461, -3216.81, 28.63388 },
            { -1308.23, -2626.368, 36.0893 },
            { -984.6726, -2778.282, 48.28575 },
            { -1167.27, -575.0267, 40.19548 },
            { -928.5076, -383.1334, 135.2698 },
            { -902.8115, -443.0529, 170.8185 },
            { -2311.601, 335.4441, 187.6049 },
            { -2214.416, 342.206, 198.1012 },
            { -2234.355, 187.0235, 193.6015 },
            { 2792.246, 5996.045, 355.1923 },
            { 3459.178, 3659.834, 51.19159 },
            { 3615.938, 3642.95, 51.19159 },
        };
        
        
        public static float UserNetwork = 1;
        
        private static string _command = "";
        private static bool _isConnectConsole = false;
        private static bool _isProxy = false;
        private static string _isConnectIp = "";
        
        //private static bool _isDownloadSearch = false;
        //private static bool _isDownloadBlackoutPy = false;
        
        public Ctos()
        {
            EventHandlers.Add("ARP:Blackout", new Action(BlackoutEvent));
            EventHandlers.Add("ARP:BlackoutStop", new Action(BlackoutStopEvent));
            
            Tick += ConsoleTimer;
            Tick += UpdateNetworkTimer;
        }
        
        public static Vector3 FindNearestNetwork(Vector3 pos)
        {
            var shopPosPrew = new Vector3((float) Network[0, 0], (float) Network[0, 1], (float) Network[0, 2]);
            for (int i = 0; i < Network.Length / 3; i++)
            {
                var shopPos = new Vector3((float) Network[i, 0], (float) Network[i, 1], (float) Network[i, 2]);
                if (Main.GetDistanceToSquared(shopPos, pos) < Main.GetDistanceToSquared(shopPosPrew, pos))
                    shopPosPrew = shopPos;
            }
            return shopPosPrew;
        }
        public static async Task<Vector3> FindNearestNetworkHasNetwork(Vector3 pos)
        {
            var shopPosPrew = new Vector3((float) Network[0, 0], (float) Network[0, 1], (float) Network[0, 2]);
            for (int i = 0; i < Network.Length / 3; i++)
            {
                var shopPos = new Vector3((float) Network[i, 0], (float) Network[i, 1], (float) Network[i, 2]);
                if (!(Main.GetDistanceToSquared(shopPos, pos) < Main.GetDistanceToSquared(shopPosPrew, pos))) continue;
                if (!await Client.Sync.Data.Has(-1, "DisableNetwork" + i))
                    shopPosPrew = shopPos;
            }
            return shopPosPrew;
        }
        
        public static int FindNearestNetworkId(Vector3 pos)
        {
            int id = 0;
            var shopPosPrew = new Vector3((float) Network[0, 0], (float) Network[0, 1], (float) Network[0, 2]);
            for (int i = 0; i < Network.Length / 3; i++)
            {
                var shopPos = new Vector3((float) Network[i, 0], (float) Network[i, 1], (float) Network[i, 2]);
                if (!(Main.GetDistanceToSquared(shopPos, pos) < Main.GetDistanceToSquared(shopPosPrew, pos))) continue;
                id = i;
                shopPosPrew = shopPos;
            }
            return id;
        }

        private static async Task UpdateNetworkTimer()
        {
            await Delay(5000);
            if (User.GetNetwork() > 1)
                Client.Sync.Data.Reset(User.GetServerId(), "disableNetwork");
            else
                Client.Sync.Data.Set(User.GetServerId(), "disableNetwork", true);
        }
        
        public static void AddConsoleMessage(string msg)
        {
            TriggerServerEvent("ARPPhone:AddConsoleMessage", msg);
        }

        private static async Task ConsoleTimer()
        {
            await Delay(500);
            if (User.IsLogin() && _command != "")
            {
                string[] cmd = _command.Split(' ');

                switch (cmd[0])
                {
                    case "connect":
                    case "ssh":
                        if (cmd.Length != 2)
                        {
                            Notification.Send("~r~Incorrect number of parameters\n~b~Use:~s~ ssh IP\n~b~Example:~s~ ssh 127.0.0.1");
                            AddConsoleMessage("<span class=\"red-text\">Incorrect number of parameters<br>Use: ssh IP<br>Example: ssh 127.0.0.1</span>");
                            break;
                        }
                        if (cmd[1] == "127.0.0.1")
                        {
                            Notification.Send("~r~Error. It's localhost.");
                            AddConsoleMessage("<span class=\"red-text\">It's localhost.</span>");
                            break;
                        }
                        if (cmd[1] == "54.37.128.202")
                        {
                            Notification.Send($"~r~New IP: {User.Data.ip_last}");
                            AddConsoleMessage($"<span class=\"red-text\">New IP: {User.Data.ip_last}</span>");
                            break;
                        }
                        if (cmd[1] == User.Data.ip_last || cmd[1] == "11.11.11.11" || cmd[1] == "47.24.39.182" || cmd[1] == "218.108.149.173")
                        {
                            Notification.Send($"~b~Connecting to {cmd[1]}...");
                            AddConsoleMessage($"<span class=\"grey-text\">Connecting to {cmd[1]}...</span>");
                            await Delay(1000);

                            if (cmd[1] == "11.11.11.11" && User.Data.mp0_watchdogs < 20)
                            {
                                Notification.Send($"~r~Hacking attempt (( Ваш навык слишком мал ))");
                                Dispatcher.SendEms("Хакерская атака", $"Попытка проникновения в бд правительства. CardID: {User.Data.id}");
                                break;
                            }
                            
                            if (User.Data.phone_code != 403 && (cmd[1] == "47.24.39.182" || cmd[1] == "218.108.149.173"))
                            {
                                Notification.Send("~r~Your version Kali Linux is old");
                                AddConsoleMessage($"<span class=\"red-text\">Your version Kali Linux is old</span>");
                                break;
                            }
                            
                            AddConsoleMessage($"<span class=\"grey-text\">Log in to the system...</span>");
                            Notification.Send($"~b~Log in to the system...");
                            await Delay(2000);
                            AddConsoleMessage($"<span class=\"green-text\">Сonnected successfully</span>");
                            Notification.Send("~g~Сonnected successfully");
                        
                            _isConnectConsole = true;
                            _isConnectIp = cmd[1];
                        
                            Client.Sync.Data.Set(User.Data.id, "isConnectConsole", true);
                            Client.Sync.Data.Set(User.Data.id, "connectIp", cmd[1]);
                            break;
                        }
                        Notification.Send("~r~Access denied");
                        break;
                    case "checkconnect":
                        if (cmd.Length != 2)
                        {
                            Notification.Send("~r~Incorrect number of parameters\n~b~Use:~s~ checkconnect ID");
                            break;
                        }

                        bool isUserConnect = await Client.Sync.Data.Has(Convert.ToInt32(cmd[1]), "isConnectConsole");
                        Notification.Send($"~g~Connected: {isUserConnect}");
                        if (isUserConnect)
                            Notification.Send($"~g~Connect IP: {await Client.Sync.Data.Has(Convert.ToInt32(cmd[1]), "connectIp")}");
                        
                        break;
                    case "disconnect":
                    case "exit":
                        Notification.Send("~g~Success");
                        Client.Sync.Data.Reset(User.Data.id, "isConnectConsole");
                        Client.Sync.Data.Reset(User.Data.id, "connectIp");
                        _isConnectConsole = false;
                        _isConnectIp = "";
                        break;
                    case "wget":
                        if (!_isConnectConsole)
                        {
                            Notification.Send("~r~You need connected to the server (ssh IP)");
                            break;
                        }
                        
                        if (cmd.Length != 3)
                        {
                            Notification.Send("~r~Incorrect number of parameters\n~b~Use:~s~ wget IP FILENAME");
                            break;
                        }
                        if (cmd[1] == "54.37.128.202")
                        {
                            if (cmd[2] == "search.sh" || cmd[2] == "atmbackdoor.sh" || cmd[2] == "getuserinfo.sh" || cmd[2] == "phone777.sh" || cmd[2] == "phone555.sh" || cmd[2] == "phone404.sh" || cmd[2] == "sportcar.sh" || cmd[2] == "elcar.sh")
                                await DownloadFile(cmd[2]);
                            break;
                        }
                        if (cmd[1] == "218.108.149.173")
                        {
                            if (cmd[2] == "search.py" || cmd[2] == "fsociety.py")
                                await DownloadFile(cmd[2]);
                            break;
                        }
                        Notification.Send("~r~Access denied");
                        break;
                    case "bash":
                        if (!_isConnectConsole)
                        {
                            Notification.Send("~r~You need connected to the server (ssh IP)");
                            break;
                        }
                        if (cmd.Length < 2)
                        {
                            Notification.Send("~r~Incorrect number of parameters\n~b~Use:~s~ bash FILENAME PARAMS");
                            break;
                        }
                        if (cmd[1] == "getuserinfo.sh" && await Client.Sync.Data.Has(GetHashKey(_isConnectIp), "getuserinfo.sh"))
                        {
                            if (cmd.Length == 3)
                            {
                                if (cmd[2] == "help")
                                {
                                    Notification.Send("~g~Use: bash getuserinfo.sh ID\nOr use: bash getuserinfo.sh PHONE_CODE PHONE_NUMBER\nFor first connecting to 11.11.11.11");
                                    break;
                                }
                                if (_isConnectIp == "11.11.11.11")
                                {
                                    if (User.Data.mp0_watchdogs < 95)
                                    {
                                        Dispatcher.SendEms("Хакерская атака", $"Взлом личных данных. CardID: {User.Data.id}");
                                        
                                        Client.Sync.Data.Set(User.GetServerId(), "wanted_level", 10);
                                        Client.Sync.Data.Set(User.GetServerId(), "wanted_reason", "Взлом личных данных");

                                        TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "Вы в розыске", User.GetServerId());
                    
                                        Notification.SendPictureToDep($"Выдан розыск {User.PlayerIdList[User.GetServerId().ToString()]}. Уровень: 10", "Диспетчер", "Система", "CHAR_CALL911", Notification.TypeChatbox);
                                    }

                                    if (await ExecuteFile(cmd[1], 500))
                                    {
                                        User.AddWatchDogs(60, 10);
                                        TriggerServerEvent("ARP:SendPlayerShowPassByHacker", Convert.ToInt32(cmd[2]));
                                    }
                                    else
                                    {
                                        _command = "";
                                    }
                                    break;
                                }
                            }
                            if (cmd.Length == 4)
                            {
                                if (_isConnectIp == "11.11.11.11")
                                {
                                    if (User.Data.mp0_watchdogs < 95)
                                    {
                                        Dispatcher.SendEms("Хакерская атака", $"Взлом личных данных. CardID: {User.Data.id}");
                                        
                                        Client.Sync.Data.Set(User.GetServerId(), "wanted_level", 10);
                                        Client.Sync.Data.Set(User.GetServerId(), "wanted_reason", "Взлом личных данных");

                                        TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "Вы в розыске", User.GetServerId());
                    
                                        Notification.SendPictureToDep($"Выдан розыск {User.PlayerIdList[User.GetServerId().ToString()]}. Уровень: 10", "Диспетчер", "Система", "CHAR_CALL911", Notification.TypeChatbox);
                                    }
                                    
                                    await ExecuteFile(cmd[1], 500);
                                    if (Convert.ToInt32(cmd[2]) == 0 || Convert.ToInt32(cmd[3]) == 0) break;
                                    TriggerServerEvent("ARP:SendPlayerShowPassByHackerByPhone", Convert.ToInt32(cmd[2]), Convert.ToInt32(cmd[3]));
                                    
                                    User.AddWatchDogs(60, 10);
                                    break;
                                }
                            }
                            Notification.Send("~g~User not found");
                            break;
                        }
                        if (cmd[1] == "sportcar.sh" && await Client.Sync.Data.Has(GetHashKey(_isConnectIp), "sportcar.sh"))
                        {
                            if (cmd.Length == 3)
                            {
                                if (cmd[2] == "help")
                                {
                                    Notification.Send("~g~Use: bash sportcar.sh");
                                    break;
                                }
                            }

                            CitizenFX.Core.Vehicle v = Main.FindNearestVehicle();
                            if (v == null)
                            {
                                Notification.SendWithTime("~r~Нужно быть рядом с машиной");
                                return;
                            }
                    
                            if (VehInfo.Get(v.Model.Hash).ClassName != "Super")
                            {
                                Notification.SendWithTime("~r~Это должен быть спорткар");
                                return;
                            }
                            
                            var rand = new Random();
                            if (User.Data.mp0_watchdogs < 30)
                            {
                                v.StartAlarm();
                                Dispatcher.SendEms("Хакерская атака", $"Взлом спорткара {v.DisplayName} с номера: {User.Data.phone_code}-{User.Data.phone}");
                            }
                            if (User.Data.mp0_watchdogs < 70 && rand.Next(2) == 0)
                            {
                                v.StartAlarm();
                                Dispatcher.SendEms("Хакерская атака", $"Взлом спорткара {v.DisplayName} с номера: {User.Data.phone_code}-{User.Data.phone}");
                            }
							
                            User.AddWatchDogs(20, 10);
                            await ExecuteFile(cmd[1]);
                            
                            v.LockStatus = VehicleLockStatus.Unlocked;
                            Notification.SendWithTime("~g~Success. Vehicle unlocked");
                            break;
                        }
                        if (cmd[1] == "elcar.sh" && await Client.Sync.Data.Has(GetHashKey(_isConnectIp), "elcar.sh"))
                        {
                            if (cmd.Length == 3)
                            {
                                if (cmd[2] == "help")
                                {
                                    Notification.Send("~g~Use: bash elcar.sh");
                                    break;
                                }
                            }

                            CitizenFX.Core.Vehicle v = Main.FindNearestVehicle();
                            if (v == null)
                            {
                                Notification.SendWithTime("~r~Нужно быть рядом с машиной");
                                return;
                            }
                    
                            if (VehInfo.Get(v.Model.Hash).FuelMinute != 0)
                            {
                                Notification.SendWithTime("~r~Это должен быть электрокар");
                                return;
                            }
                            
                            var rand = new Random();
                            if (User.Data.mp0_watchdogs < 30)
                            {
                                v.StartAlarm();
                                Dispatcher.SendEms("Хакерская атака", $"Взлом электрокара {v.DisplayName} с номера: {User.Data.phone_code}-{User.Data.phone}");
                            }
                            if (User.Data.mp0_watchdogs < 70 && rand.Next(3) == 0)
                            {
                                v.StartAlarm();
                                Dispatcher.SendEms("Хакерская атака", $"Взлом электрокара {v.DisplayName} с номера: {User.Data.phone_code}-{User.Data.phone}");
                            }

                            User.AddWatchDogs(10, 5);
                            await ExecuteFile(cmd[1]);
                    
                            v.LockStatus = VehicleLockStatus.Unlocked;
                            Notification.SendWithTime("~g~Success. Vehicle unlocked");
                            break;
                        }
                        if (cmd[1] == "atmbackdoor.sh" && await Client.Sync.Data.Has(GetHashKey(_isConnectIp), "atmbackdoor.sh"))
                        {
                            if (cmd.Length == 3)
                            {
                                if (cmd[2] == "help")
                                {
                                    Notification.Send("~g~Use: bash atmbackdoor.sh");
                                    break;
                                }
                            }

                            if (Client.Sync.Data.HasLocally(User.GetServerId(), "atmTimeout"))
                            {
                                Notification.Send("~r~Time out");
                                break;
                            }

                            if (!Client.Sync.Data.HasLocally(User.GetServerId(), "backdoor"))
                            {
                                Notification.Send("~r~No network");
                                break;
                            }
                            
                            Client.Sync.Data.ResetLocally(User.GetServerId(), "backdoor");

                            Client.Sync.Data.SetLocally(User.GetServerId(), "atmTimeout", 2);
                            
                            User.PlayPhoneAnimation();
                            await DownloadFile("AtmInfo.so", 500);
                            
                            var rand = new Random();
                            if (User.Data.mp0_watchdogs < 50)
                                Dispatcher.SendEms("Хакерская атака", $"Взлом банкомата с номера: {User.Data.phone_code}-{User.Data.phone}");
                            if (User.Data.mp0_watchdogs < 80 && rand.Next(2) == 0)
                                Dispatcher.SendEms("Хакерская атака", $"Взлом банкомата с номера: {User.Data.phone_code}-{User.Data.phone}");
                            if (User.Data.mp0_watchdogs < 95 && rand.Next(3) == 0)
                                Dispatcher.SendEms("Хакерская атака", $"Взлом банкомата с номера: {User.Data.phone_code}-{User.Data.phone}");
                            if (User.Data.mp0_watchdogs < 101 && rand.Next(4) == 0)
                                Dispatcher.SendEms("Хакерская атака", $"Взлом банкомата с номера: {User.Data.phone_code}-{User.Data.phone}");
							
                            User.AddWatchDogs(60, 10);
                            await ExecuteFile("AtmInfo.so", 100);
                            
                            var money = rand.Next(100, 200) * User.Bonus;
                            if (User.Data.mp0_watchdogs > 80 && rand.Next(3) == 0)
                                money = rand.Next(200, 400) * User.Bonus;
                            
                            User.AddBankMoney(money);

                            Bank.SendSmsBankOperation($"Зачисление средств ${money}");
                            break;
                        }
                        if (cmd[1] == "search.sh" && await Client.Sync.Data.Has(GetHashKey(_isConnectIp), "search.sh"))
                        {
                            int searchRadius = 100;
                            if (cmd.Length == 3)
                            {
                                if (cmd[2] == "help")
                                {
                                    Notification.Send("~g~Use: bash search.sh RADIUS");
                                    break;
                                }
                                searchRadius = Convert.ToInt32(cmd[2]);
                            }

                            if (searchRadius > 100 || searchRadius < 1)
                            {
                                Notification.Send("~r~Radius values: 1 to 100");
                                break;
                            }

                            MenuList.ShowHackerSearchList(searchRadius);
                            break;
                        }
                        if (cmd[1] == "phone555.sh" && await Client.Sync.Data.Has(GetHashKey(_isConnectIp), "phone555.sh"))
                        {
                            if (cmd.Length == 3)
                            {
                                if (cmd[2] == "help")
                                {
                                    Notification.Send("~g~Use: bash phone555.sh PHONE_PREFIX PHONE_NUMBER");
                                    break;
                                }
                                Notification.Send("~r~Incorrect number of parameters\n~b~Use:~s~ bash FILENAME PARAMS");
                                break;
                            }

                            if (cmd.Length == 4)
                            {
                                if (Convert.ToInt32(cmd[2]) == 555)
                                {
                                    var rand = new Random();
                                    if (User.Data.mp0_watchdogs < 30 && rand.Next(2) == 0)
                                        Dispatcher.SendEms("Хакерская атака", $"Взлом телефона с номера: {User.Data.phone_code}-{User.Data.phone}");
                                    if (User.Data.mp0_watchdogs < 80 && rand.Next(3) == 0)
                                        Dispatcher.SendEms("Хакерская атака", $"Взлом телефона с номера: {User.Data.phone_code}-{User.Data.phone}");
                                    
                                    User.AddWatchDogs(30, 10);
                                    await ExecuteFile(cmd[1]);
                                    
                                    TriggerServerEvent("ARP:OpenSmsListMenu", Convert.ToInt32(cmd[2]) + "-" + Convert.ToInt32(cmd[3]));
                                    break;
                                }
                                Dispatcher.SendEms("Хакерская атака", $"Взлом телефона с номера: {User.Data.phone_code}-{User.Data.phone}");
                                Notification.Send("~r~Hacking attempt");
                                break;
                            }
                            Notification.Send("~r~Incorrect number of parameters\n~b~Use:~s~ bash FILENAME PARAMS");
                            break;
                        }
                        if (cmd[1] == "phone777.sh" && await Client.Sync.Data.Has(GetHashKey(_isConnectIp), "phone777.sh"))
                        {
                            if (cmd.Length == 3)
                            {
                                if (cmd[2] == "help")
                                {
                                    Notification.Send("~g~Use: bash phone777.sh PHONE_PREFIX PHONE_NUMBER");
                                    break;
                                }
                                Notification.Send("~r~Incorrect number of parameters\n~b~Use:~s~ bash FILENAME PARAMS");
                                break;
                            }

                            if (cmd.Length == 4)
                            {
                                if (Convert.ToInt32(cmd[2]) == 777)
                                {
                                    var rand = new Random();
                                    if (User.Data.mp0_watchdogs < 30 && rand.Next(2) == 0)
                                        Dispatcher.SendEms("Хакерская атака", $"Взлом телефона с номера: {User.Data.phone_code}-{User.Data.phone}");
                                    if (User.Data.mp0_watchdogs < 80 && rand.Next(3) == 0)
                                        Dispatcher.SendEms("Хакерская атака", $"Взлом телефона с номера: {User.Data.phone_code}-{User.Data.phone}");
                                    
                                    User.AddWatchDogs(30, 10);
                                    await ExecuteFile(cmd[1]);
                                    
                                    TriggerServerEvent("ARP:OpenSmsListMenu", Convert.ToInt32(cmd[2]) + "-" + Convert.ToInt32(cmd[3]));
                                    break;
                                }
                                Dispatcher.SendEms("Хакерская атака", $"Взлом телефона с номера: {User.Data.phone_code}-{User.Data.phone}");
                                Notification.Send("~r~Hacking attempt");
								
                                break;
                            }
                            Notification.Send("~r~Incorrect number of parameters\n~b~Use:~s~ bash FILENAME PARAMS");
                            break;
                        }
                        if (cmd[1] == "phone404.sh" && await Client.Sync.Data.Has(GetHashKey(_isConnectIp), "phone404.sh"))
                        {
                            if (cmd.Length == 3)
                            {
                                if (cmd[2] == "help")
                                {
                                    Notification.Send("~g~Use: bash phone404.sh PHONE_PREFIX PHONE_NUMBER");
                                    break;
                                }
                                Notification.Send("~r~Incorrect number of parameters\n~b~Use:~s~ bash FILENAME PARAMS");
                                break;
                            }

                            if (cmd.Length == 4)
                            {
                                if (Convert.ToInt32(cmd[2]) == 404)
                                {
                                    var rand = new Random();
                                    if (User.Data.mp0_watchdogs < 30 && rand.Next(2) == 0)
                                        Dispatcher.SendEms("Хакерская атака", $"Взлом телефона с номера: {User.Data.phone_code}-{User.Data.phone}");
                                    if (User.Data.mp0_watchdogs < 80 && rand.Next(3) == 0)
                                        Dispatcher.SendEms("Хакерская атака", $"Взлом телефона с номера: {User.Data.phone_code}-{User.Data.phone}");
                                    
                                    User.AddWatchDogs(30, 10);
                                    await ExecuteFile(cmd[1], 50);
                                    
                                    TriggerServerEvent("ARP:OpenSmsListMenu", Convert.ToInt32(cmd[2]) + "-" + Convert.ToInt32(cmd[3]));
                                    break;
                                }
                                Dispatcher.SendEms("Хакерская атака", $"Взлом телефона с номера: {User.Data.phone_code}-{User.Data.phone}");
                                Notification.Send("~r~Hacking attempt");
                                break;
                            }
                            Notification.Send("~r~Incorrect number of parameters\n~b~Use:~s~ bash FILENAME PARAMS");
                            break;
                        }
                        Notification.Send("~r~File not found, maybe you wanna download? Use: wget IP FILENAME");
                        break;
                    case "ls":
                        if (!_isConnectConsole)
                        {
                            Notification.Send("~r~You need connected to the server (ssh IP)");
                            break;
                        }
                        MenuList.ShowHackerServerFilesList(_isConnectIp);
                        break;
                    case "python":
                        if (!_isConnectConsole)
                        {
                            Notification.Send("~r~You need connected to the server (ssh IP)");
                            break;
                        }
                        if (User.Data.phone_code != 403)
                        {
                            Notification.Send("Your version Kali Linux is old");
                            return;
                        }
                        if (cmd.Length < 2)
                        {
                            Notification.Send("~r~Incorrect number of parameters\n~b~Use:~s~ python FILENAME PARAMS");
                            break;
                        }
                        if (cmd[1] == "search.py" && await Client.Sync.Data.Has(GetHashKey(_isConnectIp), "search.py"))
                        {
                            int searchRadius = 100;
                            if (cmd.Length == 3)
                            {
                                if (cmd[2] == "help")
                                {
                                    Notification.Send("~g~Use: bash search.py RADIUS");
                                    break;
                                }
                                searchRadius = Convert.ToInt32(cmd[2]);
                            }

                            if (searchRadius > 500 || searchRadius < 1)
                            {
                                Notification.Send("~r~Radius values: 1 to 500");
                                break;
                            }

                            MenuList.ShowHackerSearchPyList(searchRadius);
                            break;
                        }
                        Notification.Send("~r~File not found, maybe you wanna download? Use: wget IP FILENAME");
                        break;
                    case "xdman":
                        if (!_isConnectConsole)
                        {
                            Notification.Send("~r~You need connected to the server (ssh IP)");
                            break;
                        }
                        if (User.Data.phone_code != 403)
                        {
                            Notification.Send("Your version Kali Linux is old");
                            return;
                        }
                        if (cmd.Length != 2)
                        {
                            Notification.Send("~r~Incorrect number of parameters\n~b~Use:~s~ xdman IP");
                            break;
                        }
                        if (cmd[1] == "54.37.128.202")
                        {
                            await DownloadFile("search.sh");
                            await DownloadFile("phone777.sh");
                            await DownloadFile("phone555.sh");
                            await DownloadFile("phone404.sh");
                            await DownloadFile("elcar.sh");
                            await DownloadFile("sportcar.sh");
                            await DownloadFile("getuserinfo.sh");
                            await DownloadFile("atmbackdoor.sh");
                        }
                        else if (cmd[1] == "218.108.149.173")
                        {
                            await DownloadFile("search.py");
                            //await DownloadFile("fsociety.py");
                        }
                        else
                        {
                            var data = await Client.Sync.Data.GetAll(GetHashKey(cmd[1]));
                            if (data == null)
                            {
                                Notification.Send("~r~System error, try again");
                                return;
                            }
                            foreach (var itemList in (IDictionary<String, Object>) data)
                                await DownloadFile(itemList.Key);
                        }
                        break;
                    case "proxy":
                        if (!_isConnectConsole)
                        {
                            Notification.Send("~r~You need connected to the server (ssh IP)");
                            break;
                        }
                        if (User.Data.phone_code != 403)
                        {
                            Notification.Send("Your version Kali Linux is old");
                            return;
                        }
                        if (cmd.Length != 2)
                        {
                            Notification.Send("~r~Incorrect number of parameters\n~b~Use:~s~ proxy IP");
                            break;
                        }
                        if (!ValidateIPv4(cmd[1]))
                        {
                            Notification.Send("IP is not valid");
                            return;
                        }
                        _isProxy = true;
                        Client.Sync.Data.Set(User.Data.id, "connectIp", cmd[1]);
                        break;
                    case "help":
                        if (User.Data.phone_code == 403)
                            Notification.Send("~g~Aviable commands:~s~\nssh IP\nexit\ncheckconnect ID\nbash PARAMS\nwget IP FILENAME\nls\npython PARAMS\nxdman IP\nproxy IP");
                        else
                            Notification.Send("~g~Aviable commands:~s~\nssh IP\nexit\ncheckconnect ID\nbash PARAMS\nwget IP FILENAME\nls");
                        break;
                    default:
                        Notification.Send("~r~Command not found");
                        break;
                }

                _command = "";
            }
        }
        
        public static bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
                return false;

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
                return false;

            byte tempForParsing;
            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        public static void ExecuteCommand(string command)
        {
            _command = command;
        }

        public static List<string> GetTrafficLightList()
        {
            return _trafficLight;
        }

        public static List<string> GetStreetLightList()
        {
            return _streetLight;
        }

        public static List<string> GetElecboxList()
        {
            return _elecBox;
        }

        public static void HackPhoneInRadius(float radius = 50f)
        {
            HackPhoneInRadius(GetEntityCoords(GetPlayerPed(-1), true), radius);
        }
        
        public static void HackPhoneInRadius(Vector3 position, float radius = 50f)
        {
            foreach (var ped in Main.GetPedListOnRadius(position, radius))
                HackPhone(ped);
        }
        
        public static async void HackPhone(CitizenFX.Core.Ped ped)
        {
            var rand = new Random();

            if (ped.IsInVehicle())
                return;

            string scenario = "WORLD_HUMAN_STAND_MOBILE";

            switch (rand.Next(3))
            {
                case 0:
                    scenario = "WORLD_HUMAN_TOURIST_MOBILE";
                    break;
                case 1:
                    scenario = "WORLD_HUMAN_STAND_MOBILE_UPRIGHT";
                    break;
            }
            
            ped.Task.StartScenario(scenario, ped.Position);
            
            await Delay(rand.Next(10000, 20000));
                
            ped.Task.ClearAll();
        }

        public static void HackPhoneBankCardInRadius(float radius = 50f)
        {
            HackPhoneBankCardInRadius(GetEntityCoords(GetPlayerPed(-1), true), radius);
        }

        public static async void HackPhoneBankCardInRadius(Vector3 position, float radius = 50f)
        {
            foreach (var ped in Main.GetPedListOnRadius(position, radius))
            {
                if (ped.IsInVehicle())
                    continue;
                int downloadStatus = 0;
                while (downloadStatus < 99)
                {
                    Screen.LoadingPrompt.Show($"Processing data {++downloadStatus}%");
                    await Delay(100);
                }
				await Delay(1000);
				Screen.LoadingPrompt.Hide();
                HackPhoneBankCard(ped, false);
            }
        }

        public static async void HackPhoneBankCard(CitizenFX.Core.Ped ped, bool isShowProcess = true)
        {
            if (User.IsAnimal(ped.Model.Hash)) return;
            var rand = new Random();
            string scenario = "WORLD_HUMAN_STAND_MOBILE";

            switch (rand.Next(3))
            {
                case 0:
                    scenario = "WORLD_HUMAN_TOURIST_MOBILE";
                    break;
                case 1:
                    scenario = "WORLD_HUMAN_STAND_MOBILE_UPRIGHT";
                    break;
            }
                
            ped.Task.StartScenario(scenario, ped.Position);

            if (isShowProcess)
            {
                int downloadStatus = 0;
                while (downloadStatus < 99)
                {
                    Screen.LoadingPrompt.Show($"Processing data {++downloadStatus}%");
                    await Delay(100);
                }
            }
           
            var money = rand.Next(15, 15) * User.Bonus;
            if (User.Data.mp0_watchdogs > 98)
                money = rand.Next(30, 35) * User.Bonus;
                
            User.AddBankMoney(money);
            Bank.SendSmsBankOperation($"Зачисление средств ${money}");
            
            if (User.Data.mp0_watchdogs < 30)
                Dispatcher.SendEms("Хакерская атака", $"Взлом банковских карт с телефона {User.Data.phone_code}-{User.Data.phone}");
            if (User.Data.mp0_watchdogs < 50 && rand.Next(2) == 0)
                Dispatcher.SendEms("Хакерская атака", $"Взлом банковских карт с телефона {User.Data.phone_code}-{User.Data.phone}");
            if (User.Data.mp0_watchdogs < 90 && rand.Next(3) == 0)
                Dispatcher.SendEms("Хакерская атака", $"Взлом банковских карт с телефона {User.Data.phone_code}-{User.Data.phone}");
            if (User.Data.mp0_watchdogs < 101 && rand.Next(5) == 0)
                Dispatcher.SendEms("Хакерская атака", $"Взлом банковских карт с телефона {User.Data.phone_code}-{User.Data.phone}");

            await Delay(1000);
            Screen.LoadingPrompt.Hide();
            await Delay(rand.Next(10000, 20000));
                
            ped.Task.ClearAll();
        }

        public static async void HackBlackout()
        {
            if (await Client.Sync.Data.Has(-9999, "IsBlackoutTimeOut"))
            {
                Notification.Send("~g~Error, try again later");
                return;
            }
            if (await IsBlackout()) return;
            Shared.TriggerEventToAllPlayers("ARP:Blackout");
        }

        public static async Task<bool> ExecuteFile(string fileName, int delay = 10)
        {
            int downloadStatus = 0;
            while (downloadStatus < 99)
            {
                if (User.GetNetwork() < 40)
                {
                    Screen.LoadingPrompt.Show("~r~Problem with network");
                    await Delay(2000);
                    Screen.LoadingPrompt.Hide();
                    return false;
                }
                Screen.LoadingPrompt.Show($"Processing {fileName} {++downloadStatus}%");
                await Delay(delay);
            }
            await Delay(1000);
            Screen.LoadingPrompt.Hide();
            return true;
        }

        public static async Task<bool> DownloadFile(string fileName, int delay = 10)
        {
            int downloadStatus = 0;
            while (downloadStatus < 99)
            {
                if (User.GetNetwork() < 40)
                {
                    Screen.LoadingPrompt.Show("~r~Problem with network");
                    await Delay(2000);
                    Screen.LoadingPrompt.Hide();
                    return false;
                }
                Screen.LoadingPrompt.Show($"Downloading {fileName} {++downloadStatus}%");
                await Delay(delay);
            }
            await Delay(1000);
            Screen.LoadingPrompt.Hide();
            
            Notification.Send("~g~You have downloaded " + fileName);
            Client.Sync.Data.Set(GetHashKey(_isConnectIp), fileName, true);
            return true;
        }

        public static async void ResetAtmTimeout()
        {
            await Delay(180000);
            Client.Sync.Data.Reset(User.GetServerId(), "atmTimeout");
        }

        public static void ElecboxDestroy(float radius = 150f)
        {
            ItemsDestroy(_elecBox, GetEntityCoords(GetPlayerPed(-1), true), radius);
        }

        public static void ElecboxDestroy(Vector3 position, float radius = 150f)
        {
            ItemsDestroy(_elecBox, position, radius);
        }

        public static void TrafficLightDestroy(float radius = 150f)
        {
            ItemsDestroy(_trafficLight, GetEntityCoords(GetPlayerPed(-1), true), radius);
        }

        public static void TrafficLightDestroy(Vector3 position, float radius = 150f)
        {
            ItemsDestroy(_trafficLight, position, radius);
        }

        public static void StreetLightDestroy(float radius = 150f)
        {
            ItemsDestroy(_streetLight, GetEntityCoords(GetPlayerPed(-1), true), radius);
        }

        public static void StreetLightDestroy(Vector3 position, float radius = 150f)
        {
            ItemsDestroy(_streetLight, position, radius);
        }

        public static void ElecboxExplode(float radius = 150f, ExplosionType type = ExplosionType.Bullet)
        {
            ItemsExplode(_elecBox, GetEntityCoords(GetPlayerPed(-1), true), type, radius);
        }

        public static void ElecboxExplode(Vector3 position, float radius = 150f, ExplosionType type = ExplosionType.Bullet)
        {
            ItemsExplode(_elecBox, position, type, radius);
        }

        public static void TrafficLightExplode(float radius = 150f, ExplosionType type = ExplosionType.Bullet)
        {
            ItemsExplode(_trafficLight, GetEntityCoords(GetPlayerPed(-1), true), type, radius);
        }

        public static void TrafficLightExplode(Vector3 position, float radius = 150f, ExplosionType type = ExplosionType.Bullet)
        {
            ItemsExplode(_trafficLight, position, type, radius);
        }

        public static void StreetLightExplode(float radius = 150f, ExplosionType type = ExplosionType.Bullet)
        {
            ItemsExplode(_streetLight, GetEntityCoords(GetPlayerPed(-1), true), type, radius);
        }
        
        public static void StreetLightExplode(Vector3 position, float radius = 150f, ExplosionType type = ExplosionType.Bullet)
        {
            ItemsExplode(_streetLight, position, type, radius);
        }

        public static void ItemsExplode(List<string> itemList, Vector3 position, ExplosionType type = ExplosionType.Bullet, float radius = 150f)
        {
            foreach (var item in Main.GetObjListOnRadius(position, radius))
            {
                foreach (string t in itemList)
                {
                    if (item.Model.Hash != GetHashKey(t) && item.Model.Hash != (uint) GetHashKey(t)) continue;
                    var entityPos = item.Position;
                    AddExplosion(entityPos.X, entityPos.Y, entityPos.Z, (int) type, 1.0f, true, false, 0);
                }
            }
        }

        public static void ItemsDestroy(List<string> itemList, Vector3 position, float radius = 150f)
        {
            var list = Main.GetObjListOnRadius(position, radius);
            for (int i = 0; i < 4; i++)
            {
                foreach (var item in list)
                {
                    foreach (string t in itemList)
                    {
                        if (item.Model.Hash != GetHashKey(t) && item.Model.Hash != (uint) GetHashKey(t)) continue;
                        var entityPos = item.Position;
                        AddExplosion(entityPos.X, entityPos.Y, entityPos.Z, (int) ExplosionType.Bullet, 0.1f, false, false, 0);
                    }
                }
            }
        }

        public static void SetBlackout(bool bl = true)
        {
            _isBlackout = bl;
            World.Blackout = bl;
            IsBlackoutConnect = bl;
        }

        public static async void BlackoutEvent()
        {
            if (await Client.Sync.Data.Has(-9999, "IsBlackoutTimeOut")) return;
            BlackoutStart();
            
            foreach (var ped in Main.GetPedListOnRadius(1000f))
                ped.DrivingStyle = DrivingStyle.IgnoreLights;
            
            await Delay(30000);
            
            foreach (var ped in Main.GetPedListOnRadius(1000f))
                ped.DrivingStyle = DrivingStyle.IgnoreLights;
            
            await Delay(30000);
            
            foreach (var ped in Main.GetPedListOnRadius(1000f))
                ped.DrivingStyle = DrivingStyle.IgnoreLights;
            
            await Delay(30000);
            
            foreach (var ped in Main.GetPedListOnRadius(1000f))
                ped.DrivingStyle = DrivingStyle.IgnoreLights;
            
            await Delay(30000);
            
            foreach (var ped in Main.GetPedListOnRadius(1000f))
                ped.DrivingStyle = DrivingStyle.IgnoreLights;
            
            await Delay(30000);
            
            foreach (var ped in Main.GetPedListOnRadius(1000f))
                ped.DrivingStyle = DrivingStyle.IgnoreLights;
            
            await Delay(30000);
            
            foreach (var ped in Main.GetPedListOnRadius(1000f))
                ped.DrivingStyle = DrivingStyle.IgnoreLights;
            
            await Delay(30000);
            
            foreach (var ped in Main.GetPedListOnRadius(1000f))
                ped.DrivingStyle = DrivingStyle.IgnoreLights;
            
            await Delay(30000);
            
            foreach (var ped in Main.GetPedListOnRadius(1000f))
                ped.DrivingStyle = DrivingStyle.IgnoreLights;
            
            BlackoutStop();
        }
        
        public static void BlackoutStopEvent()
        {
            if (!IsBlackoutConnect) return;
            IsBlackoutConnect = false;
            BlackoutStop();
        }
        
        public static async Task<bool> IsBlackout(bool withoutCache = false)
        {
            if (withoutCache)
                return await Client.Sync.Data.Has(-9999, "IsBlackout");
            return _isBlackout;
        }
        
        public static async void BlackoutStart()
        {
            TriggerEvent("CTOSBlackoutStart");
            Client.Sync.Data.Set(-9999, "IsBlackout", true);
            Client.Sync.Data.Set(-9999, "IsBlackoutTimeOut", true);
            _isBlackout = true;
            
            foreach (var ped in Main.GetPedListOnRadius())
                ped.DrivingStyle = DrivingStyle.IgnoreLights;
            
            World.Blackout = true;
            await Delay(100);
            World.Blackout = false;
            await Delay(100);
            World.Blackout = true;
            await Delay(100);
            World.Blackout = false;
            await Delay(100);
            World.Blackout = true;
            await Delay(100);
            World.Blackout = false;
            await Delay(200);
            World.Blackout = true;
            await Delay(700);
            World.Blackout = false;
            await Delay(700);
            World.Blackout = true;
        }
        
        public static async void BlackoutStop()
        {
            Shared.TriggerEventToAllPlayers("ARP:BlackoutStop");
            TriggerEvent("CTOSBlackoutStop");
            
            World.Blackout = false;
            await Delay(100);
            World.Blackout = true;
            await Delay(700);
            World.Blackout = false;
            await Delay(700);
            World.Blackout = true;
            await Delay(200);
            World.Blackout = false;
            await Delay(100);
            World.Blackout = true;
            await Delay(100);
            World.Blackout = false;
            await Delay(100);
            World.Blackout = true;
            await Delay(100);
            World.Blackout = false;
            await Delay(200);
            World.Blackout = true;
            await Delay(200);
            World.Blackout = false;
            
            Client.Sync.Data.Reset(-9999, "IsBlackout");
            _isBlackout = false;
            
            foreach (var ped in Main.GetPedListOnRadius())
                ped.DrivingStyle = DrivingStyle.Normal;
        }
    }
}