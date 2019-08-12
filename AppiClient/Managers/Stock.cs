using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;

namespace Client.Managers
{
    public class Stock : BaseScript
    {
        public static List<StockInfoGlobalData> StockGlobalDataList = new List<StockInfoGlobalData>();
        public static Dictionary<string, CitizenFX.Core.Blip> StockBlipList = new Dictionary<string, CitizenFX.Core.Blip>();
        
        public static readonly Vector3 PcPos = new Vector3(1088.792f, -3101.406f, -39.96338f);
        public static readonly Vector3 StockPos = new Vector3(1095.231f, -3098.371f, -39.99991f);
        public static readonly Vector3 ExitPos = new Vector3(1104.422f, -3099.484f, -39.99992f);
        
        public Stock()
        {
            EventHandlers.Add("ARP:AddStockGlobalDataList", new Action<dynamic>(AddStockGlobalDataList));
            EventHandlers.Add("ARP:UpdateClientStockInfo", new Action<int, string, int>(UpdateStockInfo));
        }
        
        public static void AddStockGlobalDataList(dynamic data)
        {
            Debug.WriteLine("START LOAD HOUSES");
            
            StockGlobalDataList.Clear();
            
            Checkpoint.Create(PcPos, 1.4f, "show:menu");
            Marker.Create(PcPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            
            Checkpoint.Create(StockPos, 1.4f, "show:menu");
            Marker.Create(StockPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            
            Checkpoint.Create(ExitPos, 1.4f, "show:menu");
            Marker.Create(ExitPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);

            var localData = (IList<Object>) data;
            foreach (var item in localData)
            {
                try
                {
                    var hInfo = new StockInfoGlobalData();
                    var localItem = (IDictionary<String, Object>) item;
                
                    foreach (var property in typeof(StockInfoGlobalData).GetProperties())
                        property.SetValue(hInfo, localItem[property.Name], null);

                    var pos = new Vector3(hInfo.x, hInfo.y, hInfo.z);

                    /*var blip = World.CreateBlip(new Vector3(hInfo.x, hInfo.y, hInfo.z));
                    blip.Sprite = (BlipSprite) 50;
                    blip.Name = "";
                    blip.IsShortRange = true;
                    blip.Scale = 0.4f; //86
                    
                    StockBlipList.Add(hInfo.id.ToString(), blip);*/
                    Checkpoint.Create(pos, 1.4f, "show:menu");
                    Marker.Create(pos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
                
                    StockGlobalDataList.Add(hInfo);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString(), "");
                    throw;
                }
            }

            Main.FinishLoad();
            
            Debug.WriteLine($"FINISH LOAD HOUSES ({StockGlobalDataList.Count})");
        }

        public static List<StockInfoGlobalData> GetStockListInRadiusOfPosition(Vector3 pos, float radius)
        {
            return StockGlobalDataList.Where(h => Main.GetDistanceToSquared(pos, new Vector3(h.x, h.y, h.z)) <= radius).ToList();
        }

        public static int GetStockInRadiusOfPosition(Vector3 pos, float radius)
        {
            foreach(var h in StockGlobalDataList)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3(h.x, h.y, h.z)) <= radius)
                    return StockGlobalDataList.IndexOf(h);
            }
            return -1;
        }

        public static int GetStockInteriorInRadiusOfPosition(Vector3 pos, float radius)
        {
            foreach(var h in StockGlobalDataList)
            {
                if (Main.GetDistanceToSquared(pos, ExitPos) <= radius && User.GetPlayerVirtualWorld()-50000 == h.id)
                    return StockGlobalDataList.IndexOf(h);
            }
            return -1;
        }

        public static int GetStockIdFromPosition(Vector3 pos)
        {
            foreach(var h in StockGlobalDataList)
            {
                if (h.x == pos.X && h.y == pos.Y && h.z == pos.Z)
                    return StockGlobalDataList.IndexOf(h);
            }
            return -1;
        }
        
        public static StockInfoGlobalData GetStockFromId(int id)
        {
            return StockGlobalDataList.FirstOrDefault(h => h.id == id);
        }

        public static async void MenuEnterStock(StockInfoGlobalData h)
        {
            h = await GetAllData(h.id);

            if (h.id == 0)
            {
                Notification.SendWithTime("~r~Произошла ошибка, попробуйте еще раз");
                return;
            }
            
            if (h.user_id != 0)
                MenuList.ShowStockBuyOwnerMenu(h);
            else
                MenuList.ShowStockBuyMenu(h);
        }

        public static async void MenuExitStock(StockInfoGlobalData h)
        {
            h = await GetAllData(h.id);
            if (User.GetPlayerVirtualWorld() == 50000 + h.id)
                MenuList.ShowStockInMenu(h);
        }

        public static async void EnterStock(StockInfoGlobalData h)
        {
            MenuList.HideMenu();

            if (Client.Sync.Data.HasLocally(User.GetServerId(), "HasPassError"))
            {
                Notification.SendWithTime("~r~Таймаут на ввод пароля, пожалуйста попробуйте снова");
                return;
            }

            if (User.IsAdmin(4))
            {
                Notification.SendWithTime($"~g~Пароль: ~s~{h.pin1}");
                Notification.SendWithTime($"~g~Сейф 1: ~s~{h.pin2}");
                Notification.SendWithTime($"~g~Сейф 2: ~s~{h.pin3}");
            }

            if (User.Data.id == h.user_id)
            {
                User.SetVirtualWorld(50000 + h.id);
                User.Teleport(ExitPos);
                return;
            }

            int pass = Convert.ToInt32(await Menu.GetUserInput("Пароль", null, 5));
            if (pass == h.pin1)
            {
                User.SetVirtualWorld(50000 + h.id);
                User.Teleport(ExitPos);
                Main.AddStockLog(User.Data.rp_name, "Успешно введён пароль", h.id);
            }
            else
            {
                Client.Sync.Data.SetLocally(User.GetServerId(), "HasPassError", true);
                Notification.SendWithTime("~r~Вы не верно ввели пароль");
                Main.AddStockLog(User.Data.rp_name, $"Ошибка ввода пароля ({pass})", h.id);
                await Delay(1000);
                Client.Sync.Data.ResetLocally(User.GetServerId(), "HasPassError");
            }
        }

        public static void ExitStock(StockInfoGlobalData h)
        {
            MenuList.HideMenu();
            User.SetVirtualWorld(0);
            User.Teleport(new Vector3(h.x, h.y, h.z));
        }

        public static void UpdateStockInfo(int id, string userName, int userId)
        {
            if (StockGlobalDataList.Count < 1) return;
            var index = StockGlobalDataList.IndexOf(GetStockFromId(id));
            StockGlobalDataList[index].user_name = userName;
            StockGlobalDataList[index].user_id = userId;
        }

        public static async Task<StockInfoGlobalData> GetAllData(int id)
        {
            var data = await Client.Sync.Data.GetAll(200000 + id);
            if (data == null) return new StockInfoGlobalData();
            
            var hData = new StockInfoGlobalData();
            var localData = (IDictionary<String, Object>) data;
            foreach (var property in typeof(StockInfoGlobalData).GetProperties())
                property.SetValue(hData, localData[property.Name], null);

            return hData;
        }

        public static void Save(int id)
        {
            TriggerServerEvent("ARP:SaveStock", id);
        }

        public static async void SellToUser(int serverId, int hId, string name, int price)
        {
            var hData = await GetAllData(hId);
            if (hData.user_id != User.Data.id)
            {
                Notification.SendWithTime("~r~Сожителям запрещено продавать дом");
                return;
            }
            
            if ((int) await Client.Sync.Data.Get(serverId, "stock_id") > 0)
            {
                Notification.SendWithTime("~r~У игрока уже есть склад");
                return;
            }
            
            Shared.TriggerEventToPlayer(serverId, "ARP:SellStockToUserShowMenu", User.GetServerId(), hId, name, price);
            Notification.SendWithTime("~g~Вы предложили игроку купить склад");
        }

        public static async void AcceptBuy(int serverId, int hId, int price)
        {
            if (price > User.GetMoneyWithoutSync())
            {
                Notification.SendWithTime("~g~У Вас нет достаточной суммы");
                return;
            }
            if ((int) await Client.Sync.Data.Get(200000 + hId, "user_id") == 0)
            {
                Notification.SendWithTime("~g~Склад невозможно купить");
                return;
            }
            
            Client.Sync.Data.Set(User.GetServerId(), "stock_id", hId);
            User.Data.stock_id = hId;

            //await User.GetAllData();
            
            TriggerServerEvent("ARP:UpdateStockInfo", User.Data.rp_name, User.Data.id, hId);
            
            User.RemoveMoney(price);
            Shared.TriggerEventToPlayer(serverId, "ARP:AcceptBuyStockToUser", price);
            User.SaveAccount();
                
            Notification.SendWithTime("~g~Вы купили склад");
        }

        public static async void AcceptBuyStockToUser(int price)
        {
            Client.Sync.Data.Set(User.GetServerId(), "stock_id", 0);
            User.Data.stock_id = 0;

            //await User.GetAllData();
            
            User.AddMoney(price);
            User.SaveAccount();
                
            Notification.SendWithTime("~g~Вы продали склад");
        }

        public static async void BuyStock(StockInfoGlobalData h)
        {
            await User.GetAllData();
            var hData = await GetAllData(h.id);
            var playerId = User.GetServerId();

            if (hData.id == 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_35"));
                return;
            }
            
            if (hData.user_id != 0)
            {
                Notification.SendWithTime("~r~Недвижимость уже куплена");
                return;
            }
            
            if (User.Data.stock_id != 0)
            {
                Notification.SendWithTime("~r~У Вас уже есть недвижимость");
                return;
            }
            
            if (User.GetMoneyWithoutSync() < hData.price)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_2"));
                return;
            }
            
            Client.Sync.Data.Set(playerId, "stock_id", hData.id);
            
            await Delay(200);

            if (await Client.Sync.Data.Get(playerId, "stock_id") == 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_32"));
                return;
            }
            
            User.RemoveMoney(hData.price);
            Coffer.AddMoney(hData.price);

            User.Data.stock_id = hData.id;
            
            Main.SaveLog("BuySellStock", $"[BUY] {User.Data.id} {User.Data.rp_name} | {hData.price} | {User.Data.stock_id}");

            Notification.SendWithTime($"~g~Поздравляем с покупкой. Пароль: ~s~{hData.pin1}");

            if (User.Data.reg_status != 3)
            {
                Client.Sync.Data.Set(playerId, "reg_time", 372);
                Client.Sync.Data.Set(playerId, "reg_status", 2);
            }
            
            TriggerServerEvent("ARP:UpdateStockInfo", User.Data.rp_name, User.Data.id, hData.id);

            User.SaveAccount();
            MenuList.HideMenu();

            await Delay(10000);
            await User.GetAllData();
        }

        public static async void SellStock(StockInfoGlobalData h)
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
            
            if (User.Data.stock_id == 0)
            {
                Notification.SendWithTime("~r~У Вас нет недвижимости");
                return;
            }
            
            Client.Sync.Data.Set(playerId, "stock_id", 0);

            await Delay(200);

            if (await Client.Sync.Data.Get(playerId, "stock_id") != 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_32"));
                return;
            }
            
            var nalog = hData.price * (100 - Coffer.GetNalog()) / 100;
            
            User.AddMoney(nalog);
            Coffer.RemoveMoney(nalog);
            
            Main.SaveLog("BuySellStock", $"[SELL] {User.Data.id} {User.Data.rp_name} | {nalog} | {User.Data.stock_id}");

            User.Data.stock_id = 0;
            Notification.SendWithTime($"~g~{Coffer.GetNalog()}%\n~s~Получено: ~g~${nalog:#,#}" );
            
            TriggerServerEvent("ARP:UpdateStockInfo", "", 0, hData.id);

            User.SaveAccount();
            MenuList.HideMenu();
            await User.GetAllData();
            
            Screen.LoadingPrompt.Hide();
            Client.Sync.Data.ShowSyncMessage = true;
        }
    }
}

public class StockInfoGlobalData
{
    public int id { get; set; }
    public string address { get; set; }
    public int price { get; set; }
    public string user_name { get; set; }
    public int user_id { get; set; }
    public int pin1 { get; set; }
    public int pin2 { get; set; }
    public int pin3 { get; set; }
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
}