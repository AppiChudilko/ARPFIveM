using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;

namespace Client.Managers
{
    public class Condo : BaseScript
    {
        public static List<CondoInfoGlobalData> CondoGlobalDataList = new List<CondoInfoGlobalData>();
        
        public static double[,] HouseInts =
        {
            { 0, 0, 0 }, //0
            { 1972.724, 3816.522, 32.4 }, //1
            { 1397.666, 1163.96, 113.3336 }, //2
            { -1150.642, -1520.649, 9.63273 }, //3
            { 346.6588, -1012.286, -100.19624 }, //4
            { -110.2899, -14.17893, 69.51956 }, //5
            { 1274.026, -1719.583, 53.77145 }, //6
            { -1908.656, -572.6918, 18.10678 }, //7
            { 265.9925, -1007.13, -101.9903 }, //8
            { 151.2914, -1007.358, -100 }, //9
            { -859.8812, 691.0566, 151.8607 }, //10
            { -888.1979, -451.5262, 94.46114 }, //11
            { -611.2705, 58.77193, 97.20025 }, //12
            { -781.8554, 318.5241, 186.9488 }, //13
            { 938.6196, -539.2518, 42.63986 } //14
        };
        
        public Condo()
        {
            EventHandlers.Add("ARP:AddCondoGlobalDataList", new Action<dynamic>(AddCondoGlobalDataList));
            EventHandlers.Add("ARP:UpdateClientCondoInfo", new Action<int, string, int>(UpdateCondoInfo));
        }
        
        public static void AddCondoGlobalDataList(dynamic data)
        {
            Debug.WriteLine("START LOAD HOUSES");
            
            CondoGlobalDataList.Clear();

            /*for (int i = 0; i < HouseWater.Length / 4; i++)
            {
                var pos = new Vector3((float) HouseWater[i, 0], (float) HouseWater[i, 1], (float) HouseWater[i, 2]);
                Checkpoint.Create(pos, 1.4f, "house:water");
                Marker.Create(pos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            }*/
            
            for (int i = 0; i < HouseInts.Length / 3; i++)
            {
                var pos = new Vector3((float) HouseInts[i, 0], (float) HouseInts[i, 1], (float) HouseInts[i, 2]);
                Checkpoint.Create(pos, 1.4f, "house");
                Marker.Create(pos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            }
            
            var localData = (IList<Object>) data;
            foreach (var item in localData)
            {
                try
                {
                    var hInfo = new CondoInfoGlobalData();
                    var localItem = (IDictionary<String, Object>) item;
                
                    foreach (var property in typeof(CondoInfoGlobalData).GetProperties())
                        property.SetValue(hInfo, localItem[property.Name], null);

                    Checkpoint.Create(new Vector3(hInfo.x, hInfo.y, hInfo.z), 1.4f, "house");
                    Marker.Create(new Vector3(hInfo.x, hInfo.y, hInfo.z), 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
                    
                    /*var blip = World.CreateBlip(new Vector3(hInfo.x, hInfo.y, hInfo.z));
                    blip.Sprite = (BlipSprite) 40;
                    //blip.Name = "Апартаменты";
                    blip.IsShortRange = true;
                    blip.Scale = 0.4f; //86*/
                
                    CondoGlobalDataList.Add(hInfo);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString(), "");
                    throw;
                }
            }

            Main.FinishLoad();
            
            Debug.WriteLine($"FINISH LOAD CONDO ({CondoGlobalDataList.Count})");
        }

        public static int GetHouseInteriorInRadiusOfPosition(Vector3 pos, float radius)
        {
            foreach(var h in CondoGlobalDataList)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3(h.int_x, h.int_y, h.int_z)) <= radius && User.GetPlayerVirtualWorld() == h.id + 10000)
                    return CondoGlobalDataList.IndexOf(h);
            }
            return -1;
        }

        public static List<CondoInfoGlobalData> GetHouseListInRadiusOfPosition(Vector3 pos, float radius)
        {
            return CondoGlobalDataList.Where(h => Main.GetDistanceToSquared(pos, new Vector3(h.x, h.y, h.z)) <= radius).ToList();
        }
        
        public static CondoInfoGlobalData GetRandomHouseInLosSantos()
        {
            var rand = new Random();
            string[] addressList =
            {
                "Чумаш",
                "Каньон Бэнхэм",
                "Ричман",
                "Рокфорд-Хиллз",
                "Вайнвуд Хиллз",
                "Западный Вайнвуд",
                "Центральный Вайнвуд",
                "Альта",
                "Буртон",
                "Дель Пьеро",
                "Пуэрто Дель Сол",
                "Каналы Веспучи",
                "Миррор-Парк",
                "Южный Лос-Сантос"
            };

            var list = (from h in CondoGlobalDataList let address = h.address where addressList.Any(address.Contains) select h).ToList();
            return list.ElementAt(rand.Next(list.Count));
        }

        public static int GetHouseInRadiusOfPosition(Vector3 pos, float radius)
        {
            foreach(var h in CondoGlobalDataList)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3(h.x, h.y, h.z)) <= radius)
                    return CondoGlobalDataList.IndexOf(h);
            }
            return -1;
        }

        public static int GetHouseIdFromPosition(Vector3 pos)
        {
            foreach(var h in CondoGlobalDataList)
            {
                if (h.x == pos.X && h.y == pos.Y && h.z == pos.Z)
                    return CondoGlobalDataList.IndexOf(h);
            }
            return -1;
        }
        
        public static CondoInfoGlobalData GetHouseFromId(int id)
        {
            return CondoGlobalDataList.FirstOrDefault(h => h.id == id);
        }

        public static async void MenuEnterHouse(CondoInfoGlobalData h)
        {
            h = await GetAllData(h.id);

            if (h.id == 0)
            {
                Notification.SendWithTime("~r~Произошла ошибка, попробуйте еще раз");
                return;
            }
            
            if (h.id_user > 0)
            {
                MenuList.ShowCondoOutMenu(h);
            }
            else
                MenuList.ShowCondoBuyMenu(h);
        }

        public static async void MenuExitHouse(CondoInfoGlobalData h)
        {
            h = await GetAllData(h.id);
            if (User.GetPlayerVirtualWorld() == h.id + 10000)
                MenuList.ShowCondoInMenu(h);
        }

        public static async void EnterHouse(CondoInfoGlobalData h)
        {
            if ((int) await Client.Sync.Data.Get(300000 + h.id, "pin") > 0)
            {
                int pass = Convert.ToInt32(await Menu.GetUserInput("Пароль", null, 5));
                if (pass == h.pin)
                {
                    MenuList.HideMenu();
                    User.SetVirtualWorld(h.id + 10000);
                    User.Teleport(new Vector3(h.int_x, h.int_y, h.int_z));
                }
                else
                {
                    Notification.SendWithTime("~r~Вы не верно ввели пароль");
                }
            }
            else
            {
                MenuList.HideMenu();
                User.SetVirtualWorld(h.id + 10000);
                User.Teleport(new Vector3(h.int_x, h.int_y, h.int_z));
            }
            //SetEntityCoords(GetPlayerPed(-1), ToFloat(Convert.ToInt32(h.int_x)), ToFloat(Convert.ToInt32(h.int_y)), ToFloat(Convert.ToInt32(h.int_z)), true, false, false, true);
        }

        public static void ExitHouse(CondoInfoGlobalData h)
        {
            MenuList.HideMenu();
            User.SetVirtualWorld(0);
            User.Teleport(new Vector3(h.x, h.y, h.z));
            //SetEntityCoords(GetPlayerPed(-1), ToFloat(Convert.ToInt32(h.x)), ToFloat(Convert.ToInt32(h.y)), ToFloat(Convert.ToInt32(h.z)), true, false, false, true);
        }

        public static void UpdateCondoInfo(int id, string userName, int userId)
        {
            if (CondoGlobalDataList.Count < 1) return;
            
            var index = CondoGlobalDataList.IndexOf(GetHouseFromId(id));

            CondoGlobalDataList[index].name_user = userName;
            CondoGlobalDataList[index].id_user = userId;
        }

        public static async Task<CondoInfoGlobalData> GetAllData(int id)
        {
            var data = await Client.Sync.Data.GetAll(300000 + id);
            if (data == null) return new CondoInfoGlobalData();
            
            var hData = new CondoInfoGlobalData();
            var localData = (IDictionary<String, Object>) data;
            foreach (var property in typeof(CondoInfoGlobalData).GetProperties())
                property.SetValue(hData, localData[property.Name], null);

            return hData;
        }

        public static async void SellToUser(int serverId, int hId, string name, int price)
        {
            var hData = await GetAllData(hId);
            if (hData.id_user != User.Data.id)
            {
                Notification.SendWithTime("~r~Сожителям запрещено продавать дом");
                return;
            }
            
            if ((int) await Client.Sync.Data.Get(serverId, "condo_id") > 0)
            {
                Notification.SendWithTime("~r~У игрока уже есть дом");
                return;
            }
            
            Shared.TriggerEventToPlayer(serverId, "ARP:SellHouseToUserShowMenu", User.GetServerId(), hId, name, price);
            Notification.SendWithTime("~g~Вы предложили игроку купить дом");
        }

        public static async void AcceptBuy(int serverId, int hId, int price)
        {
            if (price > User.GetMoneyWithoutSync())
            {
                Notification.SendWithTime("~g~У Вас нет достаточной суммы");
                return;
            }
            if ((int) await Client.Sync.Data.Get(300000 + hId, "id_user") == 0)
            {
                Notification.SendWithTime("~g~Дом невозможно купить");
                return;
            }
            
            Client.Sync.Data.Set(User.GetServerId(), "condo_id", hId);
            User.Data.condo_id = hId;

            //await User.GetAllData();
            
            TriggerServerEvent("ARP:UpdateCondoInfo", User.Data.rp_name, User.Data.id, hId);
            
            User.RemoveMoney(price);
            Shared.TriggerEventToPlayer(serverId, "ARP:AcceptBuyHouseToUser", price);
            User.SaveAccount();
                
            Notification.SendWithTime("~g~Вы купили дом");
        }

        public static async void AcceptBuyHouseToUser(int price)
        {
            Client.Sync.Data.Set(User.GetServerId(), "condo_id", 0);
            User.Data.condo_id = 0;

            //await User.GetAllData();
            
            User.AddMoney(price);
            User.SaveAccount();
                
            Notification.SendWithTime("~g~Вы продали дом");
        }

        public static async void BuyHouse(CondoInfoGlobalData h)
        {
            await User.GetAllData();
            var hData = await GetAllData(h.id);
            var playerId = User.GetServerId();

            if (hData.id == 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_35"));
                return;
            }
            
            if (hData.id_user != 0)
            {
                Notification.SendWithTime("~r~Недвижимость уже куплена");
                return;
            }
            
            if (User.Data.condo_id != 0)
            {
                Notification.SendWithTime("~r~У Вас уже есть недвижимость");
                return;
            }
            
            if (User.GetMoneyWithoutSync() < hData.price)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_2"));
                return;
            }
            
            Client.Sync.Data.Set(playerId, "condo_id", hData.id);
            
            await Delay(200);

            if (await Client.Sync.Data.Get(playerId, "condo_id") == 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_32"));
                return;
            }
            
            User.RemoveMoney(hData.price);
            Coffer.AddMoney(hData.price);

            User.Data.condo_id = hData.id;
            
            Main.SaveLog("BuySellCondo", $"[BUY] {User.Data.id} {User.Data.rp_name} | {hData.price} | {User.Data.condo_id}");

            
            Notification.SendWithTime("~g~Поздравляем с покупкой недвижимости");

            if (User.Data.reg_status != 3)
            {
                Client.Sync.Data.Set(playerId, "reg_time", 372);
                Client.Sync.Data.Set(playerId, "reg_status", 2);
            }
            
            TriggerServerEvent("ARP:UpdateCondoInfo", User.Data.rp_name, User.Data.id, hData.id);

            User.SaveAccount();
            MenuList.HideMenu();

            await Delay(10000);
            await User.GetAllData();
        }

        public static async void SellHouse(CondoInfoGlobalData h)
        {
            if (Screen.LoadingPrompt.IsActive)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_34"));
                return;
            }
            
            Client.Sync.Data.ShowSyncMessage = false;
            Screen.LoadingPrompt.Show("Обработка запроса, подождите");
            
            await User.GetAllData();
            var hData = await GetAllData(h.id);
            var playerId = User.GetServerId();

            if (hData.id == 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_35"));
                return;
            }
            
            if (hData.id_user != User.Data.id)
            {
                Notification.SendWithTime("~r~Сожителям запрещено продавать дом");
                return;
            }
            
            if (User.Data.condo_id == 0)
            {
                Notification.SendWithTime("~r~У Вас нет недвижимости");
                return;
            }
            
            Client.Sync.Data.Set(playerId, "condo_id", 0);

            await Delay(200);

            if (await Client.Sync.Data.Get(playerId, "condo_id") != 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_32"));
                return;
            }
            
            var nalog = hData.price * (100 - Coffer.GetNalog()) / 100;
            
            User.AddMoney(nalog);
            Coffer.RemoveMoney(nalog);
            
            Main.SaveLog("BuySellCondo", $"[SELL] {User.Data.id} {User.Data.rp_name} | {nalog} | {User.Data.condo_id}");

            User.Data.condo_id = 0;
            Notification.SendWithTime($"~g~{Coffer.GetNalog()}%\n~s~Получено: ~g~${nalog:#,#}" );

            if (User.Data.reg_status != 3)
            {
                Client.Sync.Data.Set(playerId, "reg_time", 28);
                Client.Sync.Data.Set(playerId, "reg_status", 1);
            }
            
            TriggerServerEvent("ARP:UpdateCondoInfo", "", 0, hData.id);

            User.SaveAccount();
            MenuList.HideMenu();
            await User.GetAllData();
            
            Screen.LoadingPrompt.Hide();
            Client.Sync.Data.ShowSyncMessage = true;
        }
    }
}

public class CondoInfoGlobalData
{
    public int id { get; set; }
    public string address { get; set; }
    public int price { get; set; }
    public string name_user { get; set; }
    public int id_user { get; set; }
    public int pin { get; set; }
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float int_x { get; set; }
    public float int_y { get; set; }
    public float int_z { get; set; }
}