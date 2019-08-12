using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;
using Notification = Client.Managers.Notification;

namespace Client.Business
{
    public class Business : BaseScript
    {
        /*
        TypeList.Add("Банк"); //0
        TypeList.Add("Магазин 24/7"); //1
        TypeList.Add("Магазин одежды"); //2
        TypeList.Add("Автомастерская"); //3
        TypeList.Add("Пункт аренды"); //4
        TypeList.Add("Заправка"); //5
        TypeList.Add("Парикмахерская"); //6
        TypeList.Add("Развлечение"); //7
        TypeList.Add("Сотовые операторы"); //8
        TypeList.Add("Юридические компании"); //9
        TypeList.Add("Офис"); //10
        */
        
        public static readonly Vector3 BusinessOfficePos = new Vector3(-140.7121f, -617.3683f, 167.8204f);
        public static readonly Vector3 BusinessMotorPos = new Vector3(-138.6593f, -592.6267f, 166.0002f);
        public static readonly Vector3 BusinessStreetPos = new Vector3(-116.8427f, -604.7336f, 35.28074f);
        public static readonly Vector3 BusinessGaragePos = new Vector3(-155.6696f, -577.3766f, 31.42448f);
        public static readonly Vector3 BusinessRoofPos = new Vector3(-136.6686f, -596.3055f, 205.9157f);
        public static readonly Vector3 BusinessBotPos = new Vector3(-139.2922f, -631.5964f, 167.8204f);
        
        public static BusinessData CurrentData = new BusinessData();
        
        public static string[] InteriorList =
        {
            "ex_dt1_02_office_02b", 
            "ex_dt1_02_office_02c",
            "ex_dt1_02_office_02a",
            "ex_dt1_02_office_01a",
            "ex_dt1_02_office_01b",
            "ex_dt1_02_office_01c",
            "ex_dt1_02_office_03a",
            "ex_dt1_02_office_03b",
            "ex_dt1_02_office_03c"
        };
        
        public static string[] TypeList =
        {
            "Банк", 
            "Магазин 24/7",
            "Магазин одежды",
            "Автомастерская",
            "Пункт аренды",
            "Заправка",
            "Парикмахерская",
            "Развлечение",
            "Услуги",
            "Юридические компании",
            "Офис",
            "Магазин оружия",
            "Тату салон",
            "Разное"
        };

        public Business()
        {
            
        }

        public static async void SellToUser(int serverId, int bId, string name, int price)
        {
            if ((int) await Sync.Data.Get(serverId, "business_id") > 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_22"));
                return;
            }
            
            if (!(bool) await Sync.Data.Get(serverId, "biz_lic"))
            {
                Notification.SendWithTime("~r~У игрока нет лицензии на бизнес");
                return;
            }
            
            Shared.TriggerEventToPlayer(serverId, "ARP:SellBusinessToUserShowMenu", User.GetServerId(), bId, name, price);
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_23"));
        }

        public static async void AcceptBuy(int serverId, int bId, int price)
        {
            if ((int) await Sync.Data.Get(-20000 + bId, "user_id") == 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_24"));
                return;
            }
            
            if (price > User.GetMoneyWithoutSync())
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_25"));
                return;
            }
            
            if (User.Data.age < 21 && Main.ServerName != "SunFlower")
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_26"));
                return;
            }
            
            if (!User.Data.biz_lic && Main.ServerName != "SunFlower")
            {
                Notification.SendWithTime("~r~У Вас нет лицензии на бизнес");
                return;
            }
            
            Sync.Data.Set(User.GetServerId(), "business_id", bId);
            User.Data.business_id = bId;
            
            Sync.Data.Set(-20000 + bId, "user_id", User.Data.id);
            Sync.Data.Set(-20000 + bId, "user_name", User.Data.rp_name);

            //await User.GetAllData();
            
            User.RemoveMoney(price);
            Shared.TriggerEventToPlayer(serverId, "ARP:AcceptBuyBusinessToUser", price);
            User.SaveAccount();
                
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_27"));
        }

        public static async void AcceptBuyBusinessToUser(int price)
        {
            Sync.Data.Set(User.GetServerId(), "business_id", 0);
            User.Data.business_id = 0;

            //await User.GetAllData();
            
            User.AddMoney(price);
            User.SaveAccount();
                
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_28"));
        }

        public static async void LoadInterior(int id)
        {
            foreach (string t in InteriorList)
                RemoveIpl(t);

            await Delay(200);
            RequestIpl(InteriorList[id]);
            
            var pos = GetEntityCoords(GetPlayerPed(-1), true);
            CitizenFX.Core.Native.API.LoadInterior(GetInteriorAtCoords(pos.X, pos.Y, pos.Z));
            SetEntityCoords(GetPlayerPed(-1), pos.X, pos.Y, pos.Z, true, false, false, true);
        }

        public static void BuyInterior(int bId, int id)
        {
            if (User.Data.money_bank < 50000)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_2"));
                return;
            }
            
            Sync.Data.Set(-20000 + bId, "interior", id);
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_29"));
            User.RemoveBankMoney(50000);
            Coffer.AddMoney(50000);
            Save(bId);
        }

        public static async void Buy(int bId)
        {
            CurrentData = await GetAllData(bId);
            
            if (User.Data.age < 21 && Main.ServerName != "SunFlower")
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_26"));
                return;
            }
            
            if (!User.Data.biz_lic && Main.ServerName != "SunFlower")
            {
                Notification.SendWithTime("~r~У Вас нет лицензии на бизнес");
                return;
            }
            
            if (User.Data.money_bank < CurrentData.price)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_2"));
                return;
            }
            
            if (CurrentData.user_id != 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_30"));
                return;
            }
            
            if (User.Data.business_id != 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_31"));
                return;
            }
            
            Sync.Data.Set(User.GetServerId(), "business_id", CurrentData.id);
            
            await Delay(200);

            if (await Sync.Data.Get(User.GetServerId(), "business_id") == 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_32"));
                return;
            }
            
            
            User.RemoveBankMoney(CurrentData.price);
            Coffer.AddMoney(CurrentData.price + CurrentData.bank);
            Sync.Data.Set(-20000 + bId, "bank", 0);
            User.Data.business_id = CurrentData.id;
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_33"));
            
            Main.SaveLog("BuySellBizz", $"[BUY] {User.Data.id} {User.Data.rp_name} | {CurrentData.price} | {User.Data.business_id}");

            Sync.Data.Set(-20000 + bId, "user_id", User.Data.id);
            Sync.Data.Set(-20000 + bId, "user_name", User.Data.rp_name);
            User.SaveAccount();
            Save(CurrentData.id);
        }

        public static async void Sell(int bId)
        {
            if (Screen.LoadingPrompt.IsActive)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_34"));
                return;
            }
            
            Client.Sync.Data.ShowSyncMessage = false;
            Screen.LoadingPrompt.Show(Lang.GetTextToPlayer("_lang_36"));
            
            CurrentData = await GetAllData(bId);
            
            if (CurrentData.id == 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_35"));
                return;
            }
            
            if (User.Data.business_id == 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_37"));
                return;
            }
            
            Sync.Data.Set(User.GetServerId(), "business_id", 0);
            
            await Delay(200);

            if (await Sync.Data.Get(User.GetServerId(), "business_id") != 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_32"));
                return;
            }
            
            var nalog = CurrentData.price * (100 - Coffer.GetNalog()) / 100;
            
            Main.SaveLog("BuySellBizz", $"[SELL] {User.Data.id} {User.Data.rp_name} | {nalog} | {User.Data.business_id}");
            
            User.AddBankMoney(nalog);
            Coffer.RemoveMoney(nalog);

            User.Data.business_id = 0;
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_38", nalog));

            Sync.Data.Set(-20000 + bId, "user_id", 0);
            Sync.Data.Set(-20000 + bId, "user_name", "");
            User.SaveAccount();
            Save(CurrentData.id);
            
            Screen.LoadingPrompt.Hide();
            Client.Sync.Data.ShowSyncMessage = true;
        }

        public static async Task<BusinessData> GetAllData(int id)
        {
            var data = await Sync.Data.GetAll(-20000 + id);
            if (data == null) return new BusinessData();
            
            var bData = new BusinessData();
            var localData = (IDictionary<String, Object>) data;
            foreach (var property in typeof(BusinessData).GetProperties())
            {
                if (localData.ContainsKey(property.Name))
                    property.SetValue(bData, localData[property.Name], null);
            }

            return bData;
        }
        
        public static void Save(int id)
        {
            TriggerServerEvent("ARP:SaveBusiness", id);
        }

        public static async void AddMoney(int id, int count)
        {
            Sync.Data.Set(-20000 + id, "bank", await GetMoney(id) + count);
        }

        public static async void RemoveMoney(int id, int count)
        {
            Sync.Data.Set(-20000 + id, "bank", await GetMoney(id) - count);
        }

        public static async Task<int> GetMoney(int id)
        {
            return (int) await Sync.Data.Get(-20000 + id, "bank");
        }

        public static void SetName(int id, string name)
        {
            Sync.Data.Set(-20000 + id, "name", Main.RemoveQuotes(name));
        }
        
        public static void SetPriceBuyCard1(int id, int num)
        {
            Sync.Data.Set(-20000 + id, "price_card1", num);
        }

        public static async Task<int> GetPriceCard1(int id)
        {
            CurrentData = await GetAllData(id);
            return CurrentData.price_card1 == 0 ? 10 : CurrentData.price_card1;
        }

        public static void SetPrice(int id, int num)
        {
            Sync.Data.Set(-20000 + id, "price_product", num+1);
        }

        public static void SetTarif(int id, int num)
        {
            Sync.Data.Set(-20000 + id, "tarif", num);
        }

        public static async Task<int> GetPrice(int id)
        {
            CurrentData = await GetAllData(id);
            return CurrentData.price_product == 0 ? 1 : CurrentData.price_product;
        }

        public static async void Enter(int id)
        {
            CurrentData = await GetAllData(id);
            if (CurrentData.id == 0)
            {
                Notification.SendWithTime("~r~Business error #1");
                return;
            }
            LoadInterior(CurrentData.interior);
            User.Teleport(BusinessOfficePos);
            User.SetVirtualWorld(CurrentData.id);
        }

        public static void Exit()
        {
            User.Teleport(BusinessStreetPos);
            User.SetVirtualWorld(0);
        }

        public static void Exit(Vector3 pos)
        {
            User.Teleport(pos);
            User.SetVirtualWorld(0);
        }
    }
}

public class BusinessData
{
    public int id {get;set;}
    public string name {get;set;}
    public int price {get;set;}
    public string user_name {get;set;}
    public int user_id {get;set;}
    public int bank {get;set;}
    public int type {get;set;}
    public int price_product {get;set;}
    public int price_card1 {get;set;}
    public int price_card2 {get;set;}
    public int tarif {get;set;}
    public int interior {get;set;}
}