using System;
using CitizenFX.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Business;
using Client.Vehicle;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Inventory : BaseScript
    {
        public static readonly List<InventoryData> ObjectList = new List<InventoryData>();
        public static float LoadRange = 400f;
        public static int VehicleFind = -1;
        public static int CurrentItem = -1;
       
        /*
            -Кинуть
            -Положить
            -Взять
            -Передать
            
            Хранить в ТС, на земле, в игроке, в сумке
        */
	    
        public Inventory()
        {
            Tick += Draw;
            EventHandlers.Add("ARP:CloseItemMenu", new Action<int>(CloseItemMenu));
            EventHandlers.Add("ARP:OnDropItem", new Action<int, int, int, float, float, float>(OnDropItem));
            EventHandlers.Add("ARP:OnTakeItem", new Action<int>(OnTakeItem));
            EventHandlers.Add("ARP:Inventory:GetInfoItem", new Action<int, int, int, int, int, int, int, int>(GetInfoItemFromServer));
            EventHandlers.Add("ARP:Inventory:AddWorldList", new Action<dynamic>(AddWorldList));
        }
        
        public static void AddWorldList(dynamic data)
        {
            Debug.WriteLine("START LOAD INVLIST");
            
            ObjectList.Clear();
            
            var localData = (IList<Object>) data;
            foreach (var item in localData)
            {
                try
                {
                    var oInfo = new InventoryWorldData();
                    var localItem = (IDictionary<String, Object>) item;
                    foreach (var property in typeof(InventoryWorldData).GetProperties())
                        property.SetValue(oInfo, localItem[property.Name], null);

                    InventoryData objData = new InventoryData
                    {
                        Id = oInfo.id,
                        ItemId = oInfo.itemId,
                        Prop = null,
                        Model = Client.Inventory.GetItemHashById(oInfo.itemId),
                        Pos = new Vector3(oInfo.x, oInfo.y, oInfo.z),
                        Rot = new Vector3(oInfo.rotx, oInfo.roty, oInfo.rotz),
                        IsCreate = false
                    };
                    ObjectList.Add(objData);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString(), "");
                    throw;
                }
            }

            Main.FinishLoad();
            
            Debug.WriteLine($"FINISH LOAD INVLIST ({ObjectList.Count})");
        }
        
        public static void CloseItemMenu(int id)
        {
            if (id != CurrentItem) return;
            MenuList.HideMenu();
            CurrentItem = -1;
        }
        
        public static void EquipCloth(int id, int itemId, int prefix, int number, int keyId)
        {
            if (prefix != User.Skin.SEX)
            {
                Notification.SendWithTime(prefix == 1
                    ? "~r~Вы не можете на себя надеть женскую одежду"
                    : "~r~Вы не можете на себя надеть мужскую одежду");
                return;
            }
            dynamic[,] clothList = User.Skin.SEX == 1 ? Cloth.ClothF : Cloth.ClothM;
            Cloth.Buy(0, (int) clothList[keyId, 1], (int) clothList[keyId, 2], number, (int) clothList[keyId, 4], (int) clothList[keyId, 5], (int) clothList[keyId, 6], (int) clothList[keyId, 7], 0, true);
            Characher.UpdateCloth();
            DeleteItemServer(id);
           
        }
        
        public static void EquipProp(int id, int itemId, int prefix, int number, int keyId)
        {
            if (prefix != User.Skin.SEX)
            {
                Notification.SendWithTime(prefix == 1
                    ? "~r~Вы не можете на себя надеть женскую одежду"
                    : "~r~Вы не можете на себя надеть мужскую одежду");
                return;
            }

            dynamic[,] clothList = User.Skin.SEX == 1 ? Cloth.PropF : Cloth.PropM;
            Cloth.BuyProp(0, (int) clothList[keyId, 1], (int) clothList[keyId, 2], number, 0, true);
            Characher.UpdateCloth();
            DeleteItemServer(id);
        }

        public static async void EquipItem(int id, int itemId, int prefix, int number, int keyId, int countItems)
        {
            Shared.TriggerEventToAllPlayers("ARP:CloseItemMenu", id);
            switch (itemId)
            {
                case 265:
                {
                    if (User.Data.torso != 15)
                    {
                        Notification.SendWithTime("~r~У вас уже экипированы торс");
                        return;
                    }

                    EquipCloth(id, itemId, prefix, number, keyId);
                    break;
                }
                case 266:
                {
                    if (User.Skin.SEX == 0) {
                        if (User.Data.leg != 61)
                        {
                            Notification.SendWithTime("~r~У вас уже экипированы штаны");
                            return;
                        }
                    }
                    else {
                        if (User.Data.leg != 15)
                        {
                            Notification.SendWithTime("~r~У вас уже экипированы штаны");
                            return;
                        }
                    }

                    EquipCloth(id, itemId, prefix, number, keyId);
                    break;
                }
                case 267:
                {
                    if (User.Skin.SEX == 0) {
                        if (User.Data.foot != 34)
                        {
                            Notification.SendWithTime("~r~У вас уже экипирована обувь");
                            return;
                        }
                    }
                    else {
                        if (User.Data.foot != 35)
                        {
                            Notification.SendWithTime("~r~У вас уже экипирована обувь");
                            return;
                        }
                    }

                    EquipCloth(id, itemId, prefix, number, keyId);
                    break;
                }
                case 268:
                {
                    if (User.Data.accessorie > 0)
                    {
                        Notification.SendWithTime("~r~У вас уже экипирован аксессуар");
                        return;
                    }

                    EquipCloth(id, itemId, prefix, number, keyId);
                    break;
                }
                case 269:
                {
                    if (User.Data.hat >= 0)
                    {
                        Notification.SendWithTime("~r~У вас уже экипирована шапка");
                        return;
                    }

                    EquipProp(id, itemId, prefix, number, keyId);
                    break;
                }
                case 270:
                {
                    if (User.Data.glasses >= 0)
                    {
                        Notification.SendWithTime("~r~У вас уже экипированы очки");
                        return;
                    }

                    EquipProp(id, itemId, prefix, number, keyId);
                    break;
                }
                case 271:
                {
                    if (User.Data.ear >= 0)
                    {
                        Notification.SendWithTime("~r~У вас уже экипированы серёжки");
                        return;
                    }

                    EquipProp(id, itemId, prefix, number, keyId);
                    break;
                }
                case 272:
                {
                    if (User.Data.watch >= 0)
                    {
                        Notification.SendWithTime("~r~У вас уже экипированы часы");
                        return;
                    }

                    EquipProp(id, itemId, prefix, number, keyId);
                    break;
                }
                case 273:
                {
                    if (User.Data.bracelet >= 0)
                    {
                        Notification.SendWithTime("~r~У вас уже экипирован браслет");
                        return;
                    }

                    EquipProp(id, itemId, prefix, number, keyId);
                    break;
                }
                case 7:
                    if (User.Data.item_clock)
                    {
                        UnEquipItem(itemId);
                        await Delay(1000);
                    }
                    
                    User.Data.item_clock = true;
                    Client.Sync.Data.Set(User.GetServerId(), "item_clock", true);
                    Chat.SendMeCommand("надел часы");
                    
                    DeleteItemServer(id);
                    break;
                case 8:
                    if (User.Data.phone_code != 0)
                    {
                        UnEquipItem(itemId);
                        await Delay(1000);
                    }
                    
                    User.Data.phone = number;
                    Client.Sync.Data.Set(User.GetServerId(), "phone", number);
                    User.Data.phone_code = prefix;
                    Client.Sync.Data.Set(User.GetServerId(), "phone_code", prefix);
                    
                    Notification.SendWithTime("~g~Вы экипировали телефон");
                    DeleteItemServer(id);
                    break;
                case 42:
                    if (User.Data.business_id != 0)
                    {
                        UnEquipItem(itemId);
                        await Delay(1000);
                    }
                    
                    User.Data.business_id = keyId;
                    Client.Sync.Data.Set(User.GetServerId(), "business_id", keyId);
                    
                    Notification.SendWithTime("~g~Вы экипировали ключи от офиса");
                    DeleteItemServer(id);
                    break;
                case 43:
                    if (User.Data.id_house != 0)
                    {
                        UnEquipItem(itemId);
                        await Delay(1000);
                    }
                    
                    User.Data.id_house = keyId;
                    Client.Sync.Data.Set(User.GetServerId(), "id_house", keyId);
                    
                    Notification.SendWithTime("~g~Вы экипировали ключи от дома");
                    DeleteItemServer(id);
                    break;
                case 44:
                    if (User.Data.apartment_id != 0)
                    {
                        UnEquipItem(itemId);
                        await Delay(1000);
                    }
                    
                    User.Data.apartment_id = keyId;
                    Client.Sync.Data.Set(User.GetServerId(), "apartment_id", keyId);
                    
                    Notification.SendWithTime("~g~Вы экипировали ключи от квартиры");
                    DeleteItemServer(id);
                    break;
                case 47:
                    if (User.Data.is_buy_walkietalkie)
                    {
                        UnEquipItem(itemId);
                        await Delay(1000);
                    }
                    Client.Sync.Data.Set(User.GetServerId(), "is_buy_walkietalkie", true);
                    User.Data.is_buy_walkietalkie = true;
                    Client.Sync.Data.Set(User.GetServerId(), "walkietalkie_num", User.Data.walkietalkie_num);
                    Chat.SendMeCommand("надел рацию");
                    Shared.TriggerEventToAllPlayers("ARP:PeerRadioUnmute", User.Data.id, User.Data.walkietalkie_num);
                    TriggerEvent("ARPSound:RadioOn");
                    
                    DeleteItemServer(id);
                    break;
                case 50:
                    if (User.Data.bank_prefix != 0)
                    {
                        UnEquipItem(itemId);
                        await Delay(1000);
                    }

                    User.Data.bank_number = number;
                    Client.Sync.Data.Set(User.GetServerId(), "bank_number", number);
                    User.Data.bank_prefix = prefix;
                    Client.Sync.Data.Set(User.GetServerId(), "bank_prefix", prefix);
                    User.AddBankMoney(countItems);
                    Notification.SendWithTime("~g~Вы экипировали банковскую карту");
                    
                    //LOG
                    Main.SaveLog("money", $"[EKIP-BANKCARD] {User.Data.rp_name} {User.Data.bank_prefix}-{User.Data.bank_number} {countItems}$");
                    
                    DeleteItemServer(id);
                    break;
                case int n when (n <= 136 && n >= 54 ):
                    foreach(uint hash in Enum.GetValues(typeof(WeaponHash)))
                    {
                        string name = Enum.GetName(typeof(WeaponHash), hash);
                        if (!String.Equals(name, Client.Inventory.GetItemNameHashById(n), StringComparison.CurrentCultureIgnoreCase)) continue;
                        User.GiveWeapon((uint) hash, 0, false, true);
                        
                        Notification.SendWithTime("~g~Вы экипировали оружие");
                        DeleteItemServer(id);
                        break;
                    }
                    break;
                case 138:
                    User.AddCashMoney(1);
                    Notification.SendWithTime("~g~Вы положили $1 в кошелёк");
                    DeleteItemServer(id);
                    break;
                case 139:
                    User.AddCashMoney(100);
                    Notification.SendWithTime("~g~Вы положили $100 в кошелёк");
                    DeleteItemServer(id);
                    break;
                case 140:
                case 141:
                    User.AddCashMoney(countItems);
                    Notification.SendWithTime($"~g~Вы положили ${countItems} в кошелёк");
                    DeleteItemServer(id);
                    break;
                case 27:
                case 28:
                case 29:
                case 30:
                case 146:
                case 147:
                case 148:
                case 149:
                case 150:
                case 151:
                case 152:
                case 153:
                    AddAmmo(itemId, countItems);
                    Notification.SendWithTime("~g~Вы экипировали патроны");
                    DeleteItemServer(id);
                    break;
            }
        }

        public static async void UnEquipItem(int itemId, int countItems = 0, int type = 1, int toId = 0)
        {
            int amount = await GetInvAmount(User.Data.id, InventoryTypes.Player);
            if (Client.Inventory.GetItemAmountById(itemId) + amount > User.Amount)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_60"));
                return;
            }
            
            switch (itemId)
            {
                case 265:
                {
                    dynamic[,] cloth = User.Skin.SEX == 1 ? Cloth.ClothF : Cloth.ClothM;
                    for (int i = 0; i < cloth.Length; i++)
                    {
                        if ((int) cloth[i, 1] != 11) continue;
                        if ((int) cloth[i, 2] != User.Data.body) continue;
                        
                        AddItemServer(265, 1, InventoryTypes.Player, User.Data.id, 1, User.Skin.SEX, User.Data.body_color, i);
                        
                        if (User.Skin.SEX == 0)
                        {
                            Client.Sync.Data.Set(User.GetServerId(), "torso", 15);
                            Client.Sync.Data.Set(User.GetServerId(), "torso_color", 0);
                            Client.Sync.Data.Set(User.GetServerId(), "body", 0);
                            Client.Sync.Data.Set(User.GetServerId(), "body_color", 240);
                            Client.Sync.Data.Set(User.GetServerId(), "parachute", 0);
                            Client.Sync.Data.Set(User.GetServerId(), "parachute_color", 240);
                            Client.Sync.Data.Set(User.GetServerId(), "decal", 0);
                            Client.Sync.Data.Set(User.GetServerId(), "decal_color", 0);
                        }
                        else
                        {
                            Client.Sync.Data.Set(User.GetServerId(), "torso", 15);
                            Client.Sync.Data.Set(User.GetServerId(), "torso_color", 0);
                            Client.Sync.Data.Set(User.GetServerId(), "body", 15);
                            Client.Sync.Data.Set(User.GetServerId(), "body_color", 0);
                            Client.Sync.Data.Set(User.GetServerId(), "parachute", 0);
                            Client.Sync.Data.Set(User.GetServerId(), "parachute_color", 240);
                            Client.Sync.Data.Set(User.GetServerId(), "decal", 0);
                            Client.Sync.Data.Set(User.GetServerId(), "decal_color", 0);
                        }
                        Characher.UpdateCloth();
                        break;
                    }
                    break;
                }
                case 266:
                {
                    dynamic[,] cloth = User.Skin.SEX == 1 ? Cloth.ClothF : Cloth.ClothM;
                    for (int i = 0; i < cloth.Length / 12; i++)
                    {
                        if ((int) cloth[i, 1] != 4) continue;
                        if ((int) cloth[i, 2] != User.Data.leg) continue;
                        if ( User.Skin.SEX == 0 && User.Data.leg == 61) continue;
                        if ( User.Skin.SEX == 1 && User.Data.leg == 15) continue;
                        AddItemServer(itemId, 1, InventoryTypes.Player, User.Data.id, 1, User.Skin.SEX, User.Data.leg_color, i);
                        
                        if (User.Skin.SEX == 0)
                        {
                            Client.Sync.Data.Set(User.GetServerId(), "leg", 61);
                            Client.Sync.Data.Set(User.GetServerId(), "leg_color", 13);
                        }
                        else
                        {
                            Client.Sync.Data.Set(User.GetServerId(), "leg", 15);
                            Client.Sync.Data.Set(User.GetServerId(), "leg_color", 0);
                        }
                        Characher.UpdateCloth();
                        return;
                    }
                    break;
                }
                case 267:
                {
                    dynamic[,] cloth = User.Skin.SEX == 1 ? Cloth.ClothF : Cloth.ClothM;
                    for (int i = 0; i < cloth.Length / 12; i++)
                    {
                        if ((int) cloth[i, 1] != 6) continue;
                        if ((int) cloth[i, 2] != User.Data.foot) continue;
                        AddItemServer(itemId, 1, InventoryTypes.Player, User.Data.id, 1, User.Skin.SEX, User.Data.foot_color, i);
                        
                        if (User.Skin.SEX == 0)
                        {
                            Client.Sync.Data.Set(User.GetServerId(), "foot", 34);
                            Client.Sync.Data.Set(User.GetServerId(), "foot_color", 0);
                        }
                        else
                        {
                            Client.Sync.Data.Set(User.GetServerId(), "foot", 35);
                            Client.Sync.Data.Set(User.GetServerId(), "foot_color", 0);
                        }
                        Characher.UpdateCloth();
                        return;
                    }
                    break;
                }
                case 268:
                {
                    dynamic[,] cloth = User.Skin.SEX == 1 ? Cloth.ClothF : Cloth.ClothM;
                    for (int i = 0; i < cloth.Length / 12; i++)
                    {
                        if ((int) cloth[i, 1] != 7) continue;
                        if ((int) cloth[i, 2] != User.Data.accessorie) continue;
                        AddItemServer(itemId, 1, InventoryTypes.Player, User.Data.id, 1, User.Skin.SEX, User.Data.accessorie_color, i);
                        
                        Client.Sync.Data.Set(User.GetServerId(), "accessorie", 0);
                        Client.Sync.Data.Set(User.GetServerId(), "accessorie_color", 0);
                        Characher.UpdateCloth();
                        return;
                    }
                    break;
                }
                case 269:
                {
                    dynamic[,] cloth = User.Skin.SEX == 1 ? Cloth.PropF : Cloth.PropM;
                    for (int i = 0; i < cloth.Length / 6; i++)
                    {
                        if ((int) cloth[i, 1] != 0) continue;
                        if ((int) cloth[i, 2] != User.Data.hat) continue;
                        AddItemServer(itemId, 1, InventoryTypes.Player, User.Data.id, 1, User.Skin.SEX, User.Data.hat_color, i);
                        
                        Client.Sync.Data.Set(User.GetServerId(), "hat", -1);
                        Client.Sync.Data.Set(User.GetServerId(), "hat_color", -1);
                        Characher.UpdateCloth();
                        return;
                    }
                    break;
                }
                case 270:
                {
                    dynamic[,] cloth = User.Skin.SEX == 1 ? Cloth.PropF : Cloth.PropM;
                    for (int i = 0; i < cloth.Length / 6; i++)
                    {
                        if ((int) cloth[i, 1] != 1) continue;
                        if ((int) cloth[i, 2] != User.Data.glasses) continue;
                        AddItemServer(itemId, 1, InventoryTypes.Player, User.Data.id, 1, User.Skin.SEX, User.Data.glasses_color, i);
                        
                        Client.Sync.Data.Set(User.GetServerId(), "glasses", -1);
                        Client.Sync.Data.Set(User.GetServerId(), "glasses_color", -1);
                        Characher.UpdateCloth();
                        return;
                    }
                    break;
                }
                case 271:
                {
                    dynamic[,] cloth = User.Skin.SEX == 1 ? Cloth.PropF : Cloth.PropM;
                    for (int i = 0; i < cloth.Length / 6; i++)
                    {
                        if ((int) cloth[i, 1] != 2) continue;
                        if ((int) cloth[i, 2] != User.Data.ear) continue;
                        AddItemServer(itemId, 1, InventoryTypes.Player, User.Data.id, 1, User.Skin.SEX, User.Data.ear_color, i);
                        
                        Client.Sync.Data.Set(User.GetServerId(), "ear", -1);
                        Client.Sync.Data.Set(User.GetServerId(), "ear_color", -1);
                        Characher.UpdateCloth();
                        return;
                    }
                    break;
                }
                case 272:
                {
                    dynamic[,] cloth = User.Skin.SEX == 1 ? Cloth.PropF : Cloth.PropM;
                    for (int i = 0; i < cloth.Length / 6; i++)
                    {
                        if ((int) cloth[i, 1] != 6) continue;
                        if ((int) cloth[i, 2] != User.Data.watch) continue;
                        AddItemServer(itemId, 1, InventoryTypes.Player, User.Data.id, 1, User.Skin.SEX, User.Data.watch_color, i);
                        
                        Client.Sync.Data.Set(User.GetServerId(), "watch", -1);
                        Client.Sync.Data.Set(User.GetServerId(), "watch_color", -1);
                        Characher.UpdateCloth();
                        return;
                    }
                    break;
                }
                case 273:
                {
                    dynamic[,] cloth = User.Skin.SEX == 1 ? Cloth.PropF : Cloth.PropM;
                    for (int i = 0; i < cloth.Length / 6; i++)
                    {
                        if ((int) cloth[i, 1] != 7) continue;
                        if ((int) cloth[i, 2] != User.Data.bracelet) continue;
                        AddItemServer(itemId, 1, InventoryTypes.Player, User.Data.id, 1, User.Skin.SEX, User.Data.bracelet_color, i);
                        
                        Client.Sync.Data.Set(User.GetServerId(), "bracelet", -1);
                        Client.Sync.Data.Set(User.GetServerId(), "bracelet_color", -1);
                        Characher.UpdateCloth();
                        return;
                    }
                    break;
                }
                case 7:
                    AddItemServer(7, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                    
                    User.Data.item_clock = false;
                    Client.Sync.Data.Set(User.GetServerId(), "item_clock", false);
                    Chat.SendMeCommand("снял часы");
                    break;
                case 8:
                    AddItemServer(8, 1, InventoryTypes.Player, User.Data.id, 1, User.Data.phone_code, User.Data.phone, -1);
                    
                    User.Data.phone = 0;
                    Client.Sync.Data.Set(User.GetServerId(), "phone", 0);
                    User.Data.phone_code = 0;
                    Client.Sync.Data.Set(User.GetServerId(), "phone_code", 0);
                    
                    Notification.SendWithTime("~g~Вы убрали телефон");
                    break;
                case 42:
                    AddItemServer(42, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, User.Data.business_id);
                    
                    User.Data.business_id = 0;
                    Client.Sync.Data.Set(User.GetServerId(), "business_id", 0);
                    
                    Notification.SendWithTime("~g~Вы убрали ключи от офиса");
                    break;
                case 43:
                    AddItemServer(43, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, User.Data.id_house);
                    
                    User.Data.id_house = 0;
                    Client.Sync.Data.Set(User.GetServerId(), "id_house", 0);
                    
                    Notification.SendWithTime("~g~Вы убрали ключи от дома");
                    break;
                case 44:
                    AddItemServer(44, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, User.Data.apartment_id);
                    
                    User.Data.apartment_id = 0;
                    Client.Sync.Data.Set(User.GetServerId(), "apartment_id", 0);
                    
                    Notification.SendWithTime("~g~Вы убрали ключи от квартиры");
                    break;
                case 47:
                    AddItemServer(47, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                    
                    User.Data.is_buy_walkietalkie = false;
                    Client.Sync.Data.Set(User.GetServerId(), "is_buy_walkietalkie", false);
                    Notification.SendWithTime("~g~Вы убрали рацию");
                    Voice.SetRadioEnable(false);
                    User.StopAnimation();
                    break;
                case 50:
                    AddItemServer(50, 1, InventoryTypes.Player, User.Data.id, User.Data.money_bank, User.Data.bank_prefix, User.Data.bank_number, -1);
                    
                    //LOG
                    Main.SaveLog("money", $"[DEEKIP-BANKCARD] {User.Data.rp_name} {User.Data.bank_prefix}-{User.Data.bank_number} {User.Data.money_bank}$");
                    
                    User.Data.bank_number = 0;
                    Client.Sync.Data.Set(User.GetServerId(), "bank_number", 0);
                    User.Data.bank_prefix = 0;
                    Client.Sync.Data.Set(User.GetServerId(), "bank_prefix", 0);
                    
                    User.SetBankMoney(0);
                    
                    Notification.SendWithTime("~g~Вы убрали банковскую карту");

                    break;
                case int n when (n <= 136 && n >= 54 ):
                    if (type == InventoryTypes.StockGang)
                    {
                        foreach (uint hash in Enum.GetValues(typeof(WeaponHash)))
                        {
                            string name = Enum.GetName(typeof(WeaponHash), hash);
                            if (!String.Equals(name, Client.Inventory.GetItemNameHashById(n),
                                StringComparison.CurrentCultureIgnoreCase)) continue;

                            AddItemServer(n, 1, InventoryTypes.StockGang, 2, 1, -1, -1, -1);
                            int pt = GetAmmoInPedWeapon(GetPlayerPed(-1), hash);
                            RemoveWeaponFromPed(GetPlayerPed(-1), hash);
                            break;
                        }
                    }
                    else if (type == 9999)
                    {
                        foreach (uint hash in Enum.GetValues(typeof(WeaponHash)))
                        {
                            string name = Enum.GetName(typeof(WeaponHash), hash);
                            if (!String.Equals(name, Client.Inventory.GetItemNameHashById(n),
                                StringComparison.CurrentCultureIgnoreCase)) continue;

                            AddItemServer(n, 1, InventoryTypes.Player, toId, 1, -1, -1, -1);
                            int pt = GetAmmoInPedWeapon(GetPlayerPed(-1), hash);
                            RemoveWeaponFromPed(GetPlayerPed(-1), hash);
                            break;
                        }
                    }
                    else
                    {
                        foreach(uint hash in Enum.GetValues(typeof(WeaponHash)))
                        {
                            string name = Enum.GetName(typeof(WeaponHash), hash);
                            if (!String.Equals(name, Client.Inventory.GetItemNameHashById(n), StringComparison.CurrentCultureIgnoreCase)) continue;
                   
                            AddItemServer(n, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                            int pt = GetAmmoInPedWeapon(GetPlayerPed(-1), hash);
                            RemoveWeaponFromPed(GetPlayerPed(-1), hash);
                            Notification.SendWithTime("~g~Вы убрали оружие");
                            break;
                        }
                    }
                    break;
                case 138:
                    if (await User.GetCashMoney() < 1)
                    {
                        Notification.SendWithTime("~r~У Вас нет налички");
                        return;
                    }
                    AddItemServer(138, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                    User.RemoveCashMoney(1);
                    Notification.SendWithTime("~g~Вы убрали $1 в инвентарь");
                    break;
                case 139:
                    if (await User.GetCashMoney() < 100)
                    {
                        Notification.SendWithTime("~r~У Вас нет налички");
                        return;
                    }
                    AddItemServer(138, 1, InventoryTypes.Player, User.Data.id, 100, -1, -1, -1);
                    User.RemoveCashMoney(100);
                    Notification.SendWithTime("~g~Вы убрали $100 в инвентарь");
                    break;
                case 140:
                case 141:

                    int money = Convert.ToInt32(await Menu.GetUserInput("Кол-во", null, 10));
                    
                    if (await User.GetCashMoney() < money)
                    {
                        Notification.SendWithTime("~r~У Вас нет столько налички");
                        return;
                    }
                    if (money < 0)
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                        return;
                    }
                    
                    int moneyFull = money;
                    while (moneyFull > 0)
                    {
                        if (moneyFull == 1)
                        {
                            AddItemServer(138, 1, InventoryTypes.Player, User.Data.id, moneyFull, -1, -1, -1);
                            moneyFull = 0;
                        }
                        else if (moneyFull == 100)
                        {
                            AddItemServer(139, 1, InventoryTypes.Player, User.Data.id, moneyFull, -1, -1, -1);
                            moneyFull = 0;
                        }
                        else if (moneyFull <= 10000)
                        {
                            AddItemServer(140, 1, InventoryTypes.Player, User.Data.id, moneyFull, -1, -1, -1);
                            moneyFull = 0;
                        }
                        else if (moneyFull <= 30000)
                        {
                            AddItemServer(141, 1, InventoryTypes.Player, User.Data.id, moneyFull, -1, -1, -1);
                            moneyFull = 0;
                        }
                        else
                        {
                            AddItemServer(141, 1, InventoryTypes.Player, User.Data.id, 30000, -1, -1, -1);
                            moneyFull = moneyFull - 30000;
                        }
                    }
                    
                    User.RemoveCashMoney(money);
                    
                    Notification.SendWithTime($"~g~Вы убрали ${money:#,#} в инвентарь");

                    await Delay(2000);
                    
                    if (User.Data.money == money)
                        User.RemoveCashMoney(money);
                    break;
                case 27:
                case 28:
                case 29:
                case 30:
                case 146:
                case 147:
                case 148:
                case 149:
                case 150:
                case 151:
                case 152:
                case 153:
                    
                    if (type == 9999)
                    {
                        int ptFull = countItems;
                        while (ptFull > 0)
                        {
                            if (ptFull <= AmmoItemIdToMaxCount(itemId))
                            {
                                AddItemServer(itemId, 1, InventoryTypes.Player, toId, ptFull, -1, -1, -1);
                                ptFull = 0;
                            }
                            else
                            {
                                AddItemServer(itemId, 1, InventoryTypes.Player, toId, AmmoItemIdToMaxCount(itemId), -1, -1, -1);
                                ptFull = ptFull - AmmoItemIdToMaxCount(itemId);
                            }
                        }
                    
                        RemoveAllAmmo(itemId);
                    }
                    else
                    {
                    
                        int ptFull = countItems;
                        while (ptFull > 0)
                        {
                            if (ptFull <= AmmoItemIdToMaxCount(itemId))
                            {
                                AddItemServer(itemId, 1, InventoryTypes.Player, User.Data.id, ptFull, -1, -1, -1);
                                ptFull = 0;
                            }
                            else
                            {
                                AddItemServer(itemId, 1, InventoryTypes.Player, User.Data.id, AmmoItemIdToMaxCount(itemId), -1, -1, -1);
                                ptFull = ptFull - AmmoItemIdToMaxCount(itemId);
                            }
                        }

                        /*int countPt = Convert.ToInt32(countItems / AmmoItemIdToMaxCount(itemId));
                                
                        int ptSum = 0;
                        for (int i = 0; i < countItems; i++)
                        {
                            ptSum += Convert.ToInt32(countPt / countItems);
                            AddItemServer(itemId, 1, InventiryTypes.Player, User.Data.id, Convert.ToInt32(countPt / countItems), -1, -1, -1);
                            Debug.WriteLine($"countPt:{Convert.ToInt32(countPt / countItems)}");
                        }
    
                        if (countPt % AmmoItemIdToMaxCount(itemId) > 0)
                        {
                            int endPt = countPt - ptSum;
                            AddItemServer(itemId, 1, InventiryTypes.Player, User.Data.id, endPt, -1, -1, -1); 
                            Debug.WriteLine($"endPt:{endPt}");
                        }*/
                    
                        RemoveAllAmmo(itemId);
                        Notification.SendWithTime($"~g~Вы убрали патроны в инвентарь");
                    }
                    break;
            }
                    
            UpdateAmount(User.Data.id, InventoryTypes.Player);
        }

        public static async void UseItem(int id, int itemId, int use)
        {
            switch (itemId)
            {
                case 275:
                {
                    Grab.GrabGrSix();
                    break;
                }
                case 276:
                {
                    TriggerEvent("ARPbinocular:open", true);
                    break;
                }
                
                case 0:
                {
                    var pPos = GetEntityCoords(GetPlayerPed(-1), true);
                    var player = Main.GetPlayerOnRadius(pPos, 1.5f);
                    if (player == null)
                    {
                        Notification.SendWithTime("~r~Рядом с вами никого нет");
                        return;
                    }

                    if (await Client.Sync.Data.Has(player.ServerId, "isTie"))
                    {
                        Notification.SendWithTime("~y~Игрок уже связан");
                        //Shared.TriggerEventToPlayer(player.ServerId, "ARP:UnTie");
                        //Notification.SendWithTime("~y~Вы развязали игрока");
                        //Chat.SendMeCommand("развязал человека рядом");
                        //AddItemServer(itemId, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                    }
                    else
                    {
                        if (!await Client.Sync.Data.Has(player.ServerId, "isKnockout"))
                        {
                            Notification.SendWithTime("~r~Игрок должен быть в нокауте");
                            return;
                        }
                        
                        Main.SaveLog("GangBang", $"[TIE] {User.Data.rp_name} - {User.PlayerIdList[player.ServerId.ToString()]} | {pPos.X} {pPos.Y} {pPos.Z}");
                        
                        Shared.TriggerEventToPlayer(player.ServerId, "ARP:Tie");
                        Notification.SendWithTime("~y~Вы связали игрока");
                        Chat.SendMeCommand("связал человека рядом");
                        DeleteItemServer(id);
                    }
                    break;
                }
                case 1:
                {
                    var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1.5f);
                    if (player == null)
                    {
                        Notification.SendWithTime("~r~Рядом с вами никого нет");
                        return;
                    }
                    if (await Client.Sync.Data.Has(player.ServerId, "isTieBandage"))
                    {
                        Shared.TriggerEventToPlayer(player.ServerId, "ARP:UnTieBandage");
                        Notification.SendWithTime("~y~Вы сняли мешок с головы");
                        Chat.SendMeCommand("снял мешок с головы человеку рядом");
                        AddItemServer(itemId, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                    }
                    else
                    {
                        if (!await Client.Sync.Data.Has(player.ServerId, "isTie"))
                        {
                            Notification.SendWithTime("~r~Игрок должен быть связан");
                            return;
                        }
                    
                        Shared.TriggerEventToPlayer(player.ServerId, "ARP:TieBandage");
                        Notification.SendWithTime("~y~Вы надели мешок на голову");
                        Chat.SendMeCommand("надел мешок на голову человеку рядом");
                        DeleteItemServer(id);
                    }
                    break;
                }
                case 253:
                {
                    Chat.SendCommand("/diceHASH");
                    break;
                }
                case 251:
                {
                    if (!UI.IsPlayerInOcean())
                    {
                        Notification.SendWithTime("~r~Вы должны быть в океане");
                        break;
                    }
                    if (IsPedSwimming(GetPlayerPed(-1)))
                    {
                        Notification.SendWithTime("~r~Вы не должны быть в воде");
                        break;
                    }
                    if (IsPedInAnyVehicle(GetPlayerPed(-1), true))
                    {
                        Notification.SendWithTime("~r~Вы не должны быть в транспорте");
                        break;
                    }
                    if (User.IsBlockAnimation)
                    {
                        Notification.SendWithTime("~r~Вы уже рыбачите");
                        break;
                    }
                    
                    User.PlayScenario("WORLD_HUMAN_STAND_FISHING");
                    User.IsBlockAnimation = true;
                    await Delay(20000);
                    
                    var rand = new Random();

                    if (rand.Next(0, 2) == 0)
                    {
                        if (rand.Next(0, 3) == 0)
                            TakeNewItem(241);
                        else if (rand.Next(0, 3) == 0)
                            TakeNewItem(243);
                        else if (rand.Next(0, 2) == 0)
                            TakeNewItem(244);
                        else
                            TakeNewItem(245);
                    }
                    else
                        TakeNewItem(242);
                    
                    User.IsBlockAnimation = false;
                    User.StopScenario();
                    break;
                }
                case 2:
                {
                    Chat.SendMeCommand("употребил кокаин");
                    SetEntityHealth(GetPlayerPed(-1), 200);
                    DeleteItemServer(id);
                    
                    User.AddDrugCocaLevel(2);
                    User.PlayDrugAnimation();
                    break;
                }
                case 158:
                {
                    Chat.SendMeCommand("употребил амфетамин");
                    SetEntityHealth(GetPlayerPed(-1), 200);
                    DeleteItemServer(id);
                    
                    User.AddDrugAmfLevel(2);
                    User.PlayDrugAnimation();
                    break;
                }
                case 159:
                {
                    Chat.SendMeCommand("употребил DMT");
                    SetEntityHealth(GetPlayerPed(-1), 200);
                    DeleteItemServer(id);
                    
                    User.AddDrugDmtLevel(2);
                    User.PlayDrugAnimation();
                    break;
                }
                case 160:
                {
                    Chat.SendMeCommand("употребил мефедрон");
                    SetEntityHealth(GetPlayerPed(-1), 200);
                    DeleteItemServer(id);
                    
                    User.AddDrugMefLevel(2);
                    User.PlayDrugAnimation();
                    break;
                }
                case 161:
                {
                    Chat.SendMeCommand("употребил кетамин");
                    SetEntityHealth(GetPlayerPed(-1), 200);
                    DeleteItemServer(id);
                    
                    User.AddDrugMefLevel(2);
                    User.PlayDrugAnimation();
                    break;
                }
                case 162:
                {
                    Chat.SendMeCommand("употребил LSD");
                    SetEntityHealth(GetPlayerPed(-1), 200);
                    DeleteItemServer(id);
                    
                    User.AddDrugMefLevel(2);
                    User.PlayDrugAnimation();
                    break;
                }
                case 3:
                {
                    Chat.SendMeCommand("употребил марихуану");
                    if (GetEntityHealth(GetPlayerPed(-1)) <= 190)
                        SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) + 10);
                    else
                        SetEntityHealth(GetPlayerPed(-1), 200);
                    DeleteItemServer(id);
                    
                    User.AddDrugMargLevel(1);
                    break;
                }
                case 4:
                {
                    var rand = new Random();
                    CitizenFX.Core.Vehicle v = Main.FindNearestVehicle();
                    var veh = Main.FindNearestVehicle();
                    var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1.5f);
                    if(use == 1)
                    {
                            if (v == null)
                            {
                                Notification.SendWithTime("~r~Нужно быть рядом с машиной");
                                return;
                            }

                            if (VehInfo.Get(v.Model.Hash).FuelMinute == 0)
                            {
                                Notification.SendWithTime("~r~Электрокары можно взломать только с Kali Linux");
                                return;
                            }

                            if (VehInfo.Get(v.Model.Hash).ClassName == "Super")
                            {
                                Notification.SendWithTime("~r~Спорткары можно взломать только с Kali Linux");
                                return;
                            }

                            if (VehInfo.Get(v.Model.Hash).ClassName == "Helicopters" ||
                                VehInfo.Get(v.Model.Hash).ClassName == "Planes")
                            {
                                Notification.SendWithTime("~r~Вы не можете взломать это транспортное средство");
                                return;
                            }

                            if (v.LockStatus == VehicleLockStatus.Unlocked)
                            {
                                Notification.SendWithTime("~r~Транспорт уже открыт");
                                return;
                            }

                            foreach (var p in Main.GetPedListOnRadius(100f))
                            {
                                if (User.IsAnimal(p.Model.Hash)) continue;
                                if (rand.Next(2) != 0) continue;
                                p.Task.StartScenario("WORLD_HUMAN_STAND_MOBILE", p.Position);
                                await Delay(10000);
                                if (p.IsDead) break;
                                Dispatcher.SendEms("Код 2",
                                    $"Возможен угон ТС.\nНомера: ~y~{Vehicle.GetVehicleNumber(v.Handle)}\n~s~Модель: ~y~{v.DisplayName}");
                                p.Task.ClearAll();
                                break;
                            }
                            
                            Chat.SendMeCommand("ковыряется отмычкой в замке автомобиля");
                            User.PlayAnimation("mp_arresting", "a_uncuff", 8);
                            await Delay(2500);


                            if (rand.Next(0, 3) == 1)
                            {
                                SetVehicleSiren(veh.Handle, true);
                                Shared.TriggerEventToAllPlayers("ARP:SetSirenSoundVehicle", VehToNet(veh.Handle), true);
                                v.LockStatus = VehicleLockStatus.Unlocked;
                                Notification.SendWithTime("~g~Вы открыли транспорт");
                                StartVehicleAlarm(veh.Handle);
                            }
                            else
                            {
                                Notification.SendWithTime("~g~Вы сломали отмычку");
                                StartVehicleAlarm(veh.Handle);
                                DeleteItemServer(id);
                                SetVehicleSiren(veh.Handle, true);
                                Shared.TriggerEventToAllPlayers("ARP:SetSirenSoundVehicle", VehToNet(veh.Handle), true);
                            }

                            break;
                    }
                    
                    if (player == null)
                    { 
                        Notification.SendWithTime("~r~Рядом с вами никого нет");
                        return;
                    }
                    
                    Chat.SendMeCommand("ковыряется отмычкой в замке");
                    User.PlayAnimation("mp_arresting", "a_uncuff", 8);
                    await Delay(2500);
                    
                    if (await Client.Sync.Data.Has(player.ServerId, "isCuff"))
                    {
                        if (rand.Next(0, 10) == 1)
                        {
                            Shared.Cuff(player.ServerId);
                            Notification.SendWithTime("~y~Вскрыли замки в наручниках");
                            Chat.SendMeCommand("снял наручники с человека рядом");
                            AddItemServer(40, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                            return;
                        }
                        else
                        {
                            Notification.SendWithTime("~g~Вы сломали отмычку");
                            DeleteItemServer(id);
                            return;
                        }
                                
                    }
                            
                    Notification.SendWithTime("~y~Человек не в наручниках");
                    break;
                }

                case 5:
                {
                    if (IsPedInAnyVehicle(PlayerPedId(), true))
                    {
                        Notification.SendWithTime("~r~Вы должны находиться около открытого капота");
                        return;
                    }
                    CitizenFX.Core.Vehicle v = Main.FindNearestVehicle();
                    if (v == null)
                    {
                        Notification.SendWithTime("~r~Нужно быть рядом с машиной");
                        return;
                    }

                    foreach (var item in Vehicle.VehicleInfoGlobalDataList)
                    {
                        if (item.Number != Vehicle.GetVehicleNumber(v.Handle)) continue;
                        Vehicle.VehicleInfoGlobalDataList[item.VehId].SOil = 0;
                        Client.Sync.Data.Set(110000 + item.VehId, "SOil", 0);
                        TriggerServerEvent("ARP:SaveVehicle", item.VehId);
                    }
                
                    Notification.SendWithTime("~g~Вы залили масло в транспорт");
                    DeleteItemServer(id);
                    break;
                }
                case 6:
                {
                    if (IsPedInAnyVehicle(PlayerPedId(), true))
                    {
                        Notification.SendWithTime("~r~Вы должны находиться около открытого капота");
                        return;
                    }
                    CitizenFX.Core.Vehicle v = Main.FindNearestVehicle();
                    if (v == null)
                    {
                        Notification.SendWithTime("~r~Нужно быть рядом с машиной");
                        return;
                    }
                    
                    /*if (v.Health < 750.0) //309 сука строка -------
                    {
                        Notification.SendWithTime("~r~Вы не можете сами починить авто, вызывайте механика");
                        return;
                    }*/
                    if (v.EngineHealth >= 999)
                    {
                        Notification.SendWithTime("~r~Автомобиль не поврежден");
                        return;
                    }
                    /*if (!v.Doors[VehicleDoorIndex.Hood].IsOpen)
                    {
                        Notification.SendWithTime("~r~Капот должен быть открыт");
                        return;
                    }*/

                    foreach (var item in Vehicle.VehicleInfoGlobalDataList)
                    {
                        if (item.Number != Vehicle.GetVehicleNumber(v.Handle)) continue;
                        if (item.SEngine < 2)
                        {
                            Vehicle.VehicleInfoGlobalDataList[item.VehId].SEngine = 0;
                            Client.Sync.Data.Set(110000 + item.VehId, "SEngine", 0);
                            TriggerServerEvent("ARP:SaveVehicle", item.VehId);
                            Notification.SendWithTime("~g~Вы успешно починили двигатель");
                        }
                    }

                    float sum = v.EngineHealth + 200;
                    if (sum >= 1000)
                        sum = 1000;

                    v.EngineHealth = sum;
                    v.Health = Convert.ToInt32(sum);
                
                    Notification.SendWithTime("~g~Вы успешно починили авто");
                    DeleteItemServer(id);
                    break;
                }
                case 7:
                {
                    Notification.Send($"~g~Ваш ID:~s~ {User.Data.id}\n~g~Время:~s~ {DateTime.Now:dd/MM/yyyy} {DateTime.Now:HH:mm}");
                    if (User.Data.jail_time > 0)
                        Notification.Send($"~g~Время в тюрьме:~s~ {User.Data.jail_time} сек.");
                    if (User.IsMuted())
                        Notification.Send($"~g~Время окончания мута:~s~ {Main.UnixTimeStampToDateTime(User.Data.date_mute)}");
                    break;
                }
                case 9:
                {
                    if (IsPedInAnyVehicle(PlayerPedId(), true))
                    {
                        Notification.SendWithTime("~r~Вы должны находиться около транспорта");
                        return;
                    }
                    CitizenFX.Core.Vehicle v = Main.FindNearestVehicle();
                    if (v == null)
                    {
                        Notification.SendWithTime("~r~Нужно быть рядом с машиной");
                        return;
                    }
                    var vehId = Managers.Vehicle.GetVehicleIdByNumber(Vehicle.GetVehicleNumber(v.Handle));

                    if (Managers.Vehicle.VehicleInfoGlobalDataList[vehId].FullFuel < Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Fuel + 10)
                    {
                        Notification.SendWithTime("~r~Полный бак");
                        return;
                    }

                    if (Managers.Vehicle.VehicleInfoGlobalDataList[vehId].FullFuel < Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Fuel + 10)
                    {
                        Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Fuel = Managers.Vehicle.VehicleInfoGlobalDataList[vehId].FullFuel;
                        Notification.SendWithTime("~g~Вы залили полный бак");
                        return;
                    }
            
                    Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Fuel += 10;
                    Notification.SendWithTime("~g~Вы заправили авто на 10л.");
                    DeleteItemServer(id);
                    break;
                }
                case 63:
                {
                    if (IsPedInAnyVehicle(PlayerPedId(), true))
                    {
                        Notification.SendWithTime("~r~Вы в автомобиле");
                        return;
                    }
                    
                    var ped = Main.FindNearestPed();
                    if (!User.IsAnimal(ped.Model.Hash))
                    {
                        Notification.SendWithTime("~r~Это должно быть животное");
                        break;
                    }
                    if (ped.Handle == 0)
                    {
                        Notification.SendWithTime("~r~Рядом с вами нет животного");
                        break;
                    }
                    if (ped.IsAlive)
                    {
                        Notification.SendWithTime("~r~Животное должно быть мертвое");
                        break;
                    }
                    
                    var pPos = GetEntityCoords(GetPlayerPed(-1), true);
                    if (Main.GetPlayerListOnRadius(pPos, 4f).Count >= 2)
                    {
                        Notification.SendWithTime("~r~Рядом с вами кто-то есть");
                        return;
                    }

                    if (new Random().Next(5) == 0)
                        Dispatcher.SendEms("Код 2", "Возможное браконьерство", false);
                    
                    User.PlayScenario("CODE_HUMAN_MEDIC_TEND_TO_DEAD");
                    await Delay(10000);
                    User.StopScenario();

                    if (ped.Model.Hash == GetHashKey("a_c_boar"))
                    {
                        Chat.SendMeCommand("разрезал животное");
                        TakeNewItem(223);
                        ped.Delete();
                    }
                    else if (ped.Model.Hash == GetHashKey("a_c_chickenhawk"))
                    {
                        Chat.SendMeCommand("разрезал птицу");
                        TakeNewItem(224);
                        ped.Delete();
                    }
                    else if (ped.Model.Hash == GetHashKey("a_c_cow"))
                    {
                        Chat.SendMeCommand("разрезал животное");
                        TakeNewItem(225);
                        ped.Delete();
                    }
                    else if (ped.Model.Hash == GetHashKey("a_c_cormorant"))
                    {
                        Chat.SendMeCommand("разрезал птицу");
                        TakeNewItem(226);
                        ped.Delete();
                    }
                    else if (ped.Model.Hash == GetHashKey("a_c_deer"))
                    {
                        Chat.SendMeCommand("разрезал животное");
                        TakeNewItem(227);
                        ped.Delete();
                    }
                    else if (ped.Model.Hash == GetHashKey("a_c_hen"))
                    {
                        Chat.SendMeCommand("разрезал птицу");
                        TakeNewItem(228);
                        ped.Delete();
                    }
                    else if (ped.Model.Hash == GetHashKey("a_c_pig"))
                    {
                        Chat.SendMeCommand("разрезал животное");
                        TakeNewItem(229);
                        ped.Delete();
                    }
                    else if (ped.Model.Hash == GetHashKey("a_c_rabbit_01"))
                    {
                        Chat.SendMeCommand("разрезал животное");
                        TakeNewItem(230);
                        ped.Delete();
                    }
                    else if (ped.Model.Hash == GetHashKey("a_c_rat"))
                    {
                        Chat.SendMeCommand("разрезал животное");
                        TakeNewItem(231);
                        ped.Delete();
                    }
                    else
                    {
                        Notification.SendWithTime("~r~Этот тип не подходит для еды");
                    }
                    
                    break;
                }
                case 232:
                case 234:
                case 236:
                case 238:
                {
                    User.AddEatLevel(800);
                    Chat.SendMeCommand("съедает мясо");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 246:
                case 247:
                case 248:
                case 249:
                case 250:
                {
                    User.AddEatLevel(850);
                    Chat.SendMeCommand("съедает рыбу");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 237:
                case 239:
                {
                    User.AddEatLevel(500);
                    Chat.SendMeCommand("съедает мясо");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 240:
                {
                    User.AddEatLevel(100);
                    Chat.SendMeCommand("съедает мясо");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 233:
                {
                    User.AddEatLevel(1500);
                    Chat.SendMeCommand("съедает мясо");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 235:
                {
                    User.AddEatLevel(1000);
                    Chat.SendMeCommand("съедает мясо");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 10:
                {
                    User.AddEatLevel(40);
                    Chat.SendMeCommand("съедает жвачку");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 11:
                {
                    User.RemoveWaterLevel(10);
                    User.AddEatLevel(190);
                    Chat.SendMeCommand("съедает батончик");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 12:
                {
                    User.RemoveWaterLevel(20);
                    User.AddEatLevel(160);
                    Chat.SendMeCommand("съедает чипсы");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 13:
                {
                    User.RemoveWaterLevel(5);
                    User.AddEatLevel(320);
                    Chat.SendMeCommand("съедает роллы");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 14:
                {
                    User.RemoveWaterLevel(7);
                    User.AddEatLevel(380);
                    Chat.SendMeCommand("съедает гамбургер");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 15:
                {
                    User.RemoveWaterLevel(5);
                    User.AddEatLevel(420);
                    Chat.SendMeCommand("съедает салат");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 16:
                {
                    User.RemoveWaterLevel(10);
                    User.AddEatLevel(550);
                    Chat.SendMeCommand("съедает пиццу");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 17:
                {
                    User.RemoveWaterLevel(8);
                    User.AddEatLevel(780);
                    Chat.SendMeCommand("съедает жаркое");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 18:
                {
                    User.RemoveWaterLevel(10);
                    User.AddEatLevel(850);
                    Chat.SendMeCommand("съедает кесадильи");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 19:
                {
                    User.RemoveWaterLevel(10);
                    User.AddEatLevel(1100);
                    Chat.SendMeCommand("съедает фрикасе");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 20:
                {
                    User.AddHealthLevel(5);
                    User.AddWaterLevel(20);
                    User.AddEatLevel(220);
                    Chat.SendMeCommand("съедает фрукты");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 32:
                {
                    User.AddWaterLevel(100);
                    User.AddEatLevel(900);
                    Chat.SendMeCommand("съедает сухпаёк");
                    DeleteItemServer(id);
                    User.PlayEatAnimation();
                    break;
                }
                case 21:
                {
                    User.AddWaterLevel(100);
                    Chat.SendMeCommand("выпивает бутылку воды");
                    DeleteItemServer(id);
                    User.PlayDrinkAnimation();
                    break;
                }
                case 22:
                {
                    if (User.GetTempLevel() < 35.9)
                        User.AddTempLevel((float) 0.9);
                    User.AddWaterLevel(95);
                    Chat.SendMeCommand("выпивает стакан кофе");
                    DeleteItemServer(id);
                    User.PlayDrinkAnimation();
                    break;
                }
                case 23:
                {
                    if (User.GetTempLevel() < 35.9)
                        User.AddTempLevel((float) 1.2);
                    User.AddWaterLevel(95);
                    Chat.SendMeCommand("выпивает стакан чая");
                    DeleteItemServer(id);
                    User.PlayDrinkAnimation();
                    break;
                }
                case 24:
                {
                    User.AddWaterLevel(70);
                    Chat.SendMeCommand("выпивает бутылку лимонада");
                    DeleteItemServer(id);
                    User.PlayDrinkAnimation();
                    break;
                }
                case 25:
                {
                    User.AddWaterLevel(55);
                    Chat.SendMeCommand("выпивает банку кока-колы");
                    DeleteItemServer(id);
                    User.PlayDrinkAnimation();
                    break;
                }
                case 26:
                {
                    User.AddWaterLevel(110);
                    Chat.SendMeCommand("выпивает банку энергетика");
                    DeleteItemServer(id);
                    User.PlayDrinkAnimation();
                    break;
                }
                case 31:
                {
                    var pPos = GetEntityCoords(GetPlayerPed(-1), true);
                    var player = Main.GetPlayerOnRadius(pPos, 1.2f);
                    if (player == null)
                    {
                        Notification.SendWithTime("~r~Рядом с вами никого нет");
                        return;
                    }
                    Main.SaveLog("GangBang", $"[ANDRENALINE] {User.Data.rp_name} - {User.PlayerIdList[player.ServerId.ToString()]} | {pPos.X} {pPos.Y} {pPos.Z}");
                    Shared.TriggerEventToPlayer(player.ServerId, "ARP:UseAdrenalin");
                    Chat.SendMeCommand("сделал инъекцию адреналина");
                    DeleteItemServer(id);
                    break;
                }
                case 40:
                {
                    var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1.2f);
                    if (player == null)
                    {
                        Notification.SendWithTime("~r~Рядом с вами никого нет");
                        return;
                    }
                    User.PlayAnimation("mp_arresting", "a_uncuff", 8);
                    Shared.Cuff(player.ServerId);
                    DeleteItemServer(id);
                    break;
                }
                case 215:
                {
                    Chat.SendMeCommand("использует аптечку"); 
                    // SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) + 40);
                    if (GetEntityHealth(GetPlayerPed(-1)) <= 160)
                        SetEntityHealth(GetPlayerPed(-1), GetEntityHealth(GetPlayerPed(-1)) + 40);
                    else
                        SetEntityHealth(GetPlayerPed(-1), 200);
                    DeleteItemServer(id);
                    break;
                }
                case 221:
                {
                    Chat.SendMeCommand("употребил таблетку");
                    
                    User.SetDrugAmfLevel(0);
                    User.SetDrugCocaLevel(0);
                    User.SetDrugDmtLevel(0);
                    User.SetDrugKetLevel(0);
                    User.SetDrugLsdLevel(0);
                    User.SetDrugMargLevel(0);
                    User.SetDrugMefLevel(0);
                    User.SetDrunkLevel(0);
                
                    StopAllScreenEffects();
                    User.PlayDrugAnimation();
                    
                    DeleteItemServer(id);
                    break;
                }
            }
        }
        
        public static void DropItem(int id, int itemId, Vector3 pos, bool isDrop)
        {
            CitizenFX.Core.Ped ped = new CitizenFX.Core.Ped(GetPlayerPed(-1));
            
            if (itemId == 194 || itemId == 198)
            {
                Shared.TriggerEventToAllPlayers("ARP:OnDropItem", User.Data.id, id, itemId, pos.X - Sin(ped.Rotation.Z) * (1),pos.Y + Cos(ped.Rotation.Z) * (1) , pos.Z - 0.95);
                return;
            }
            
            if (itemId == 195 || itemId == 196 || itemId == 197 || itemId == 199 || itemId == 203 || itemId == 202 || itemId == 200 || itemId == 201)
            {
                Shared.TriggerEventToAllPlayers("ARP:OnDropItem", User.Data.id, id, itemId, pos.X - Sin(ped.Rotation.Z) * (0.5),pos.Y + Cos(ped.Rotation.Z) * (0.5) , pos.Z - 0.95);
                return;
            }
            
            if (isDrop)
            {
                Shared.TriggerEventToAllPlayers("ARP:OnDropItem", User.Data.id, id, itemId, pos.X - Sin(ped.Rotation.Z) * (0.25),pos.Y + Cos(ped.Rotation.Z) * (0.25) , pos.Z);
                return;
            }
            

            
            Shared.TriggerEventToAllPlayers("ARP:OnDropItem", User.Data.id, id, itemId, pos.X - Sin(ped.Rotation.Z) * (0.25),pos.Y + Cos(ped.Rotation.Z) * (0.25) , pos.Z - 0.95);
        }

        public static async void TakeNewItem(int itemId, int count = 1)
        {
            int amount = await GetInvAmount(User.Data.id, InventoryTypes.Player);
            int amountMax = await GetInvAmountMax(User.Data.id, InventoryTypes.Player);
            if (Client.Inventory.GetItemAmountById(itemId) + amount > amountMax)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_60"));
                return;
            }
            
            AddItemServer(itemId, 1, InventoryTypes.Player, User.Data.id, count, -1, -1, -1);
            UpdateAmount(User.Data.id, InventoryTypes.Player);
            Notification.SendWithTime($"~b~Вы взяли \"{Client.Inventory.GetItemNameById(itemId)}\"");
            Chat.SendMeCommand($"взял \"{Client.Inventory.GetItemNameById(itemId)}\"");
        }

        public static async void TakeItem(int id, int itemId, int ownerType, bool notify = true)
        {
            int amount = await GetInvAmount(User.Data.id, InventoryTypes.Player);
            int amountMax = await GetInvAmountMax(User.Data.id, InventoryTypes.Player);
            if (Client.Inventory.GetItemAmountById(itemId) + amount > amountMax)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_60"));
                return;
            }
            
            Shared.TriggerEventToAllPlayers("ARP:CloseItemMenu", id);

            if (ownerType == InventoryTypes.World)
            {
                Shared.TriggerEventToAllPlayers("ARP:OnTakeItem", id);
                User.PlayAnimation("pickup_object","pickup_low", 8);
            }

            if (ownerType == InventoryTypes.StockGang)
                Main.AddFractionGunLog(User.Data.rp_name, $"TAKE: {Client.Inventory.GetItemNameById(itemId)}", User.Data.fraction_id);
            if (ownerType == InventoryTypes.UserStock)
                Main.AddStockLog(User.Data.rp_name, $"TAKE: {Client.Inventory.GetItemNameById(itemId)}", User.GetPlayerVirtualWorld() - 50000);
            
            UpdateItemOwnerServer(id, InventoryTypes.Player, User.Data.id);
            UpdateAmount(User.Data.id, InventoryTypes.Player);

            if (!notify) return;
            Notification.SendWithTime($"~g~Вы взяли \"{Client.Inventory.GetItemNameById(itemId)}\"");
            Chat.SendMeCommand($"взял \"{Client.Inventory.GetItemNameById(itemId)}\"");
        }

        public static async void GiveItem(int id, int itemId, int playerId, bool notify = true)
        {
            int amount = await GetInvAmount(playerId, InventoryTypes.Player);
            int amountMax = await GetInvAmountMax(playerId, InventoryTypes.Player);
            if (Client.Inventory.GetItemAmountById(itemId) + amount > amountMax)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_60"));
                return;
            }
            
            Shared.TriggerEventToAllPlayers("ARP:UserPlayAnimationToAll", playerId, "mp_common","givetake2_a", 8);
            User.PlayAnimation("mp_common","givetake1_a", 8);
            
            UpdateItemOwnerServer(id, InventoryTypes.Player, playerId);
            UpdateAmount(playerId, InventoryTypes.Player);
            UpdateAmount(User.Data.id, InventoryTypes.Player);

            if (!notify) return;
            Notification.SendWithTime($"~g~Вы передали \"{Client.Inventory.GetItemNameById(itemId)}\" игроку");
            Chat.SendMeCommand($"передал \"{Client.Inventory.GetItemNameById(itemId)}\" человеку рядом");
        }

        public static async void DropItemToVehicle(int id, int itemId, string number, bool notify = true)
        {
            int vId = ConvertNumberToHash(number);
            int amount = await GetInvAmount(vId, InventoryTypes.Vehicle);
            int amountMax = await GetInvAmountMax(vId, InventoryTypes.Vehicle);
            if (Client.Inventory.GetItemAmountById(itemId) + amount > amountMax)
            {
                Notification.SendWithTime("~r~В багажнике нет места");
                return;
            }
            
            UpdateItemOwnerServer(id, InventoryTypes.Vehicle, vId);
            UpdateAmount(vId, InventoryTypes.Vehicle);
            UpdateAmount(User.Data.id, InventoryTypes.Player);

            if (!notify) return;
            Notification.SendWithTime($"~g~Вы положили \"{Client.Inventory.GetItemNameById(itemId)}\" в багажник");
            Chat.SendMeCommand($"положил \"{Client.Inventory.GetItemNameById(itemId)}\" в багажник");
        }

        public static async void DropItemToStockGang(int id, int itemId, int ownerId, bool notify = true)
        {
            int amount = await GetInvAmount(ownerId, InventoryTypes.StockGang);
            int amountMax = await GetInvAmountMax(ownerId, InventoryTypes.StockGang);
            if (Client.Inventory.GetItemAmountById(itemId) + amount > amountMax)
            {
                Notification.SendWithTime("~r~На складе нет места");
                return;
            }
            
            Main.AddFractionGunLog(User.Data.rp_name, $"DROP: {Client.Inventory.GetItemNameById(itemId)}", User.Data.fraction_id);
            
            UpdateItemOwnerServer(id, InventoryTypes.StockGang, ownerId);
            UpdateAmount(ownerId, InventoryTypes.StockGang);
            UpdateAmount(User.Data.id, InventoryTypes.Player);

            if (!notify) return;
            Notification.SendWithTime($"~g~Вы положили \"{Client.Inventory.GetItemNameById(itemId)}\" на склад");
            Chat.SendMeCommand($"положил \"{Client.Inventory.GetItemNameById(itemId)}\" на склад");
        }

        public static async void DropItemToFridge(int id, int itemId, int ownerId, bool notify = true)
        {
            int amount = await GetInvAmount(ownerId, InventoryTypes.Fridge);
            int amountMax = await GetInvAmountMax(ownerId, InventoryTypes.Fridge);
            if (Client.Inventory.GetItemAmountById(itemId) + amount > amountMax)
            {
                Notification.SendWithTime("~r~В холодильнике нет места");
                return;
            }
            
            UpdateItemOwnerServer(id, InventoryTypes.Fridge, ownerId);
            UpdateAmount(ownerId, InventoryTypes.Fridge);
            UpdateAmount(User.Data.id, InventoryTypes.Player);

            if (!notify) return;
            Notification.SendWithTime($"~g~Вы положили \"{Client.Inventory.GetItemNameById(itemId)}\" в холодильник");
            Chat.SendMeCommand($"положил \"{Client.Inventory.GetItemNameById(itemId)}\" в холодильник");
        }

        public static async void DropItemToUserStock(int id, int itemId, int ownerId, bool notify = true)
        {
            int amount = await GetInvAmount(ownerId, InventoryTypes.UserStock);
            int amountMax = await GetInvAmountMax(ownerId, InventoryTypes.UserStock);
            if (Client.Inventory.GetItemAmountById(itemId) + amount > amountMax)
            {
                Notification.SendWithTime("~r~На складе нет места");
                return;
            }
            int ownId = User.GetPlayerVirtualWorld() - 50000;
            
            Main.AddStockLog(User.Data.rp_name, $"DROP: {Client.Inventory.GetItemNameById(itemId)}", ownId);

            UpdateItemOwnerServer(id, InventoryTypes.UserStock+ownerId, ownId);
            UpdateAmount(ownerId, InventoryTypes.UserStock);
            UpdateAmount(User.Data.id, InventoryTypes.Player);

            if (!notify) return;
            Notification.SendWithTime($"~g~Вы положили \"{Client.Inventory.GetItemNameById(itemId)}\" на склад");
            Chat.SendMeCommand($"положил \"{Client.Inventory.GetItemNameById(itemId)}\" на склад");
        }

        public static async void TakeDrugItem(int id, int itemId, int countItems, bool notify = true, int takeCount = 1)
        {
            countItems = countItems - takeCount;
            
            int amount = await GetInvAmount(User.Data.id, InventoryTypes.Player);
            int amountMax = await GetInvAmountMax(User.Data.id, InventoryTypes.Player);

            int newItemId = 2;

            switch (itemId)
            {
                case 142:
                case 144:
                case 154:
                case 156:
                    if (takeCount == 1)
                        newItemId = 2;
                    if (takeCount == 10)
                        newItemId = 154;
                    if (takeCount == 50)
                        newItemId = 156;
                    break;
                case 143:
                case 145:
                case 155:
                case 157:
                    if (takeCount == 1)
                        newItemId = 3;
                    if (takeCount == 10)
                        newItemId = 155;
                    if (takeCount == 50)
                        newItemId = 157;
                    break;
                case 163:
                case 164:
                case 171:
                case 176:
                    if (takeCount == 1)
                        newItemId = 158;
                    if (takeCount == 10)
                        newItemId = 171;
                    if (takeCount == 50)
                        newItemId = 176;
                    break;
                case 165:
                case 166:
                case 172:
                case 177:
                    if (takeCount == 1)
                        newItemId = 159;
                    if (takeCount == 10)
                        newItemId = 172;
                    if (takeCount == 50)
                        newItemId = 177;
                    break;
                case 167:
                case 168:
                case 173:
                case 178:
                    if (takeCount == 1)
                        newItemId = 160;
                    if (takeCount == 10)
                        newItemId = 173;
                    if (takeCount == 50)
                        newItemId = 178;
                    break;
                case 169:
                case 174:
                case 179:
                    if (takeCount == 1)
                        newItemId = 161;
                    if (takeCount == 10)
                        newItemId = 174;
                    if (takeCount == 50)
                        newItemId = 179;
                    break;
                case 170:
                case 175:
                case 180:
                    if (takeCount == 1)
                        newItemId = 162;
                    if (takeCount == 10)
                        newItemId = 175;
                    if (takeCount == 50)
                        newItemId = 180;
                    break;
            }
            
            if (Client.Inventory.GetItemAmountById(newItemId) + amount > amountMax)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_60"));
                return;
            }
            
            MenuList.HideMenu();
            
            AddItemServer(newItemId, 1, InventoryTypes.Player, User.Data.id, takeCount, -1, -1, -1);
            UpdateAmount(User.Data.id, InventoryTypes.Player);

            if (countItems <= 0)
                DeleteItemServer(id);
            else
            {
                UpdateItemCountServer(id, countItems);
                GetInfoItem(id);
            }

            if (!notify) return;
            Notification.SendWithTime($"~g~Вы взяли \"{Client.Inventory.GetItemNameById(newItemId)}\"");
            Chat.SendMeCommand($"взял {takeCount}гр наркотиков");
        }

        public static async Task<int> GetInvAmount(int id, int type)
        {
            //return (int) await Client.Sync.Data.Get(id, "invAmount:" + type);
            if (Client.Sync.Data.HasLocally(id, "invAmount:" + type))
                return (int) Client.Sync.Data.GetLocally(id, "invAmount:" + type);
            
            if (await Client.Sync.Data.Has(id, "invAmount:" + type))
                return (int) await Client.Sync.Data.Get(id, "invAmount:" + type);
            
            UpdateAmount(id, type);
            await Delay(1000);
            
            return (int) await Client.Sync.Data.Get(id, "invAmount:" + type);
        }

        public static void SetInvAmount(int id, int type, int data)
        {
            Client.Sync.Data.Set(id, "invAmount:" + type, data);
        }

        public static async Task<int> GetInvAmountMax(int id, int type)
        {
            if (Client.Sync.Data.HasLocally(id, "invAmountMax:" + type))
                return (int) Client.Sync.Data.GetLocally(id, "invAmountMax:" + type);
            
            if (await Client.Sync.Data.Has(id, "invAmountMax:" + type))
                return (int) await Client.Sync.Data.Get(id, "invAmountMax:" + type);
            
            UpdateAmountMax(id, type);
            await Delay(1000);
            
            return (int) await Client.Sync.Data.Get(id, "invAmountMax:" + type);
        }

        public static void SetInvAmountMax(int id, int type, int data)
        {
            Client.Sync.Data.Set(id, "invAmountMax:" + type, data);
            Client.Sync.Data.SetLocally(id, "invAmountMax:" + type, data);
        }

        public static int AmmoTypeToAmmo(int type)
        {
            switch (type)
            {
                case 357983224: //... / PipeBomb
                    return -1;
                case 1003688881: //... / Grenade
                    return -1;
                case -435287898: //... / SmokeGrenade
                    return -1;
                case -1356724057: //... / ProximityMine
                    return -1;
                case -1686864220: //... / BZGas
                    return -1;
                case 1359393852: //... / FireExtinguisher
                    return -1;
                case 1411692055: //... / StickyBomb
                    return -1;
                case 1446246869: //... / Molotov
                    return -1;
                case -899475295: //... / PetrolCan
                    return -1;
                case -6986138: //... / Ball
                    return -1;
                case -2112339603: //... / Snowball
                    return -1;
                case 1808594799: //... / Flare
                    return -1;
                case 1173416293: //... / FlareGun
                    return 147;
                case -1339118112: //... / StunGun
                    return -1;
                case -1356599793: //... / Firework
                    return 148;
                case 1742569970: //... / RPG
                    return 149;
                case 2034517757: //... / Railgun
                    return 150;
                case -1726673363: //... / HomingLauncher
                    return 152;
                case 1003267566: //... / CompactGrenadeLauncher
                    return 151;
                case 1285032059: //12.7mm / SniperGun
                    return 146;
                case -1878508229: //18.5mm / Shotguns
                    return 28;
                case 218444191: //5.56mm / AssaultRifles
                    return 30;
                case 1950175060: //9mm / Handguns
                    return 27;
                case 1820140472: //9mm / MiniMG
                    return 153;
                case 1788949567: //7.62mm / MG
                case -1614428030: //7.62mm / MiniGun
                    return 29;
                default:
                    return -1;
            }
        }

        public static int AmmoItemIdToMaxCount(int type)
        {
            switch (type)
            {
                case 147:
                    return 10;
                case 148: //... / Firework
                    return 1;
                case 149: //... / RPG
                    return 1;
                case 150: //... / Railgun
                    return 10;
                case -152: //... / HomingLauncher
                    return 1;
                case 151: //... / CompactGrenadeLauncher
                    return 10;
                case 146: //12.7mm / SniperGun
                    return 60;
                case 28: //18.5mm
                    return 120;
                case 30: //5.56mm
                    return 260;
                case 27: //9mm
                    return 140;
                case 153: //9mm
                    return 140;
                case 29: //7.62mm
                    return 130;
                default:
                    return 1;
            }
        }

        public static void AddAmmo(int type, int count)
        {
            switch (type)
            {
                case 147:
                    AddAmmoToPed(GetPlayerPed(-1), (uint) WeaponHash.FlareGun, count);
                    return;
                case 148: //... / Firework
                    AddAmmoToPed(GetPlayerPed(-1), (uint) WeaponHash.Firework, count);
                    return;
                case 149: //... / RPG
                    AddAmmoToPed(GetPlayerPed(-1), (uint) WeaponHash.RPG, count);
                    return;
                case 150: //... / Railgun
                    AddAmmoToPed(GetPlayerPed(-1), (uint) WeaponHash.Railgun, count);
                    return;
                case -152: //... / HomingLauncher
                    AddAmmoToPed(GetPlayerPed(-1), (uint) WeaponHash.HomingLauncher, count);
                    return;
                case 151: //... / CompactGrenadeLauncher
                    AddAmmoToPed(GetPlayerPed(-1), (uint) WeaponHash.CompactGrenadeLauncher, count);
                    return;
                case 146: //12.7mm / SniperGun
                    AddAmmoToPed(GetPlayerPed(-1), (uint) WeaponHash.MarksmanRifle, count);
                    return;
                case 28: //18.5mm
                    AddAmmoToPed(GetPlayerPed(-1), (uint) WeaponHash.AssaultShotgun, count);
                    return;
                case 30: //5.56mm
                    AddAmmoToPed(GetPlayerPed(-1), (uint) WeaponHash.AssaultRifle, count);
                    return;
                case 27: //9mm
                    AddAmmoToPed(GetPlayerPed(-1), (uint) WeaponHash.Pistol, count);
                    return;
                case 153: //9mm
                    AddAmmoToPed(GetPlayerPed(-1), (uint) WeaponHash.SMG, count);
                    return;
                case 29: //7.62mm
                    AddAmmoToPed(GetPlayerPed(-1), (uint) WeaponHash.MG, count);
                    return;
            }
        }

        public static void RemoveAllAmmo(int type)
        {
            switch (type)
            {
                case 147:
                    SetPedAmmo(GetPlayerPed(-1), (uint) WeaponHash.FlareGun, 0);
                    return;
                case 148: //... / Firework
                    SetPedAmmo(GetPlayerPed(-1), (uint) WeaponHash.Firework, 0);
                    return;
                case 149: //... / RPG
                    SetPedAmmo(GetPlayerPed(-1), (uint) WeaponHash.RPG, 0);
                    return;
                case 150: //... / Railgun
                    SetPedAmmo(GetPlayerPed(-1), (uint) WeaponHash.Railgun, 0);
                    return;
                case -152: //... / HomingLauncher
                    SetPedAmmo(GetPlayerPed(-1), (uint) WeaponHash.HomingLauncher, 0);
                    return;
                case 151: //... / CompactGrenadeLauncher
                    SetPedAmmo(GetPlayerPed(-1), (uint) WeaponHash.CompactGrenadeLauncher, 0);
                    return;
                case 146: //12.7mm / SniperGun
                    SetPedAmmo(GetPlayerPed(-1), (uint) WeaponHash.MarksmanRifle, 0);
                    return;
                case 28: //18.5mm
                    SetPedAmmo(GetPlayerPed(-1), (uint) WeaponHash.AssaultShotgun, 0);
                    return;
                case 30: //5.56mm
                    SetPedAmmo(GetPlayerPed(-1), (uint) WeaponHash.AssaultRifle, 0);
                    return;
                case 27: //9mm
                    SetPedAmmo(GetPlayerPed(-1), (uint) WeaponHash.Pistol, 0);
                    return;
                case 153: //9mm
                    SetPedAmmo(GetPlayerPed(-1), (uint) WeaponHash.SMG, 0);
                    return;
                case 29: //7.62mm
                    SetPedAmmo(GetPlayerPed(-1), (uint) WeaponHash.MG, 0);
                    return;
            }
        }

        public static int ConvertNumberToHash(string number)
        {
            /*number = number.Replace("A", "10"); 
            number = number.Replace("B", "11"); 
            number = number.Replace("C", "12"); 
            number = number.Replace("D", "13"); 
            number = number.Replace("F", "14");
            number = number.Replace("G", "15");
            number = number.Replace("H", "16");
            number = number.Replace("I", "17");
            number = number.Replace("J", "18");
            number = number.Replace("K", "19"); 
            number = number.Replace("L", "20"); 
            number = number.Replace("M", "21"); 
            number = number.Replace("N", "22"); 
            number = number.Replace("O", "23"); 
            number = number.Replace("P", "24"); 
            number = number.Replace("Q", "25"); 
            number = number.Replace("R", "26"); 
            number = number.Replace("S", "27");
            number = number.Replace("T", "28");
            number = number.Replace("U", "29");
            number = number.Replace("V", "30");
            number = number.Replace("W", "31");
            number = number.Replace("X", "32");
            number = number.Replace("Y", "33");
            number = number.Replace("Z", "34");*/
            //return Convert.ToInt32(number);
            return Convert.ToInt32(GetHashKey(number.ToUpper()));
        }

        public static CitizenFX.Core.Vehicle FindVehicle(int hash)
        {
            return Main.GetVehicleListOnRadius().FirstOrDefault(v => ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)) == hash);
        }

        public static void UpdateAmountMax(int id, int type)
        {
            if (type == InventoryTypes.VehicleNpc ||
                type == InventoryTypes.VehicleOwner ||
                type == InventoryTypes.VehicleServer ||
                type == InventoryTypes.Vehicle)
            {
                CitizenFX.Core.Vehicle v = FindVehicle(id);
                if (v == null)
                    return;
                SetInvAmountMax(id, type, Client.Vehicle.VehInfo.Get(v.Model.Hash).Stock);
            }
            else
            {
                int invAmountMax = User.Amount;
                if (type == InventoryTypes.World)
                    invAmountMax = -1;
                else if (type == InventoryTypes.Apartment)
                    invAmountMax = 200000;
                else if (type == InventoryTypes.House)
                    invAmountMax = 200000;
                else if (type == InventoryTypes.Player)
                    invAmountMax = User.Amount;
                else if (type == InventoryTypes.Bag)
                    invAmountMax = 60000;
                else if (type == InventoryTypes.StockGang)
                    invAmountMax = 21000000;
                else if (type == InventoryTypes.Fridge)
                    invAmountMax = Main.GetKitchenAmount();
                else if (type == 12 || type == 13)
                    invAmountMax = 750000;
                else if (type >= 14 && type <= 17)
                    invAmountMax = 1100000;
                else if (type >= 18 && type <= 22 || type == 11)
                    invAmountMax = 950000;
                SetInvAmountMax(id, type, invAmountMax);
            }
        }

        public static void UpdateAmount(int id, int type)
        {
            TriggerServerEvent("ARP:Inventory:UpdateAmount", id, type);
        }

        public static void AddItemServer(int itemId, int count, int ownerType, int ownerId, int countItems, int prefix, int number, int keyId)
        {
            TriggerServerEvent("ARP:Inventory:AddItem", itemId, count, ownerType, ownerId, countItems, prefix, number, keyId);
        }

        public static void UpdateItemServer(int id, int itemId, int ownerType, int ownerId, int countItems, int prefix, int number, int keyId)
        {
            TriggerServerEvent("ARP:Inventory:UpdateItem", id, itemId, ownerType, ownerId, countItems, prefix, number, keyId);
        }

        public static void UpdateItemOwnerServer(int id, int ownerType, int ownerId)
        {
            TriggerServerEvent("ARP:Inventory:UpdateItemOwner", id, ownerType, ownerId);
        }

        public static void UpdateItemCountServer(int id, int count)
        {
            TriggerServerEvent("ARP:Inventory:UpdateItemCount", id, count);
        }

        public static void AddItemPosServer(int itemId, Vector3 pos, Vector3 rot, int count, int ownerType, int ownerId)
        {
            TriggerServerEvent("ARP:Inventory:AddItemPos", itemId, pos.X, pos.Y, pos.Z, rot.X, rot.Y, rot.Z, count, ownerType, ownerId);
        }

        public static void UpdateItemPosServer(int id, int itemId, Vector3 pos, Vector3 rot, int ownerType, int ownerId)
        {
            TriggerServerEvent("ARP:Inventory:UpdateItemPos", id, itemId, pos.X, pos.Y, pos.Z, rot.X, rot.Y, rot.Z, ownerType, ownerId);
        }

        public static void DeleteItemServer(int id)
        {
            TriggerServerEvent("ARP:Inventory:DeleteItem", id);
        }

        public static void GetInfoItem(int id)
        {
            TriggerServerEvent("ARP:Inventory:GetInfoItem", id);
        }

        public static void GetItemList(int ownerId, int ownerType)
        {
            TriggerServerEvent("ARP:Inventory:GetItemList", ownerId, ownerType);
        }

        public static void GetItemListInRadius(Vector3 pos)
        {
            TriggerServerEvent("ARP:Inventory:GetItemListInWorld", pos.X, pos.Y);
        }

        public static void CookFood(int ownerId)
        {
            TriggerServerEvent("ARP:Inventory:CookFood", ownerId);
            Chat.SendMeCommand("готовит еду");
            Notification.SendWithTime("~g~Вы приготовили всю еду");
        }

        public static void AddInWorldItem(int id, int itemId, Vector3 pos, Vector3 rot)
        {
            InventoryData objData = new InventoryData
            {
                Id = id,
                ItemId = id,
                Prop = null,
                Model = Client.Inventory.GetItemHashById(itemId),
                Pos = pos,
                Rot = rot,
                IsCreate = false
            };
            ObjectList.Add(objData);
        }

        public static void UpdateInWorldItem(int id, int itemId, Vector3 pos, Vector3 rot)
        {
            DeleteInWorldItem(id);
            AddInWorldItem(id, itemId, pos, rot);
        }

        public static void DeleteInWorldItem(int id)
        {
            foreach (var item in ObjectList)
            {
                if (id != item.Id) continue;
                if (item.IsCreate && item.Prop != null)
                    item.Prop.Delete();

                ObjectList.Remove(item);
                return;
            }
        }

        public static void Find(int id)
        {
            foreach (var item in ObjectList)
            {
                if (id != item.Id) continue;
                if (item.IsCreate && item.Prop != null)
                    item.Prop.Delete();

                ObjectList.Remove(item);
                return;
            }
        }

        protected static void GetInfoItemFromServer(int id, int itemId, int ownerType, int ownerId, int countItems, int prefix, int number, int keyId)
        {
            MenuList.ShowToPlayerInfoItemMenu(id, prefix, number, keyId, itemId, ownerType, ownerId, countItems);
        }

        protected static void OnTakeItem(int id)
        {
            Client.Sync.Data.SetLocally(User.GetServerId(), "TakeItem" + id, true);
            /*int idx = 0;
            foreach (var item in ObjectList)
            {
                if (item.Id == id)
                {
                    try
                    {
                        item.Prop.Delete();
                        item.Prop = null;
                        ObjectList[idx] = null;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("DELETE INV TAKE OBJECT: " + e.ToString());
                    }
                }
                idx++;
            }*/
        }

        protected static void OnDropItem(int userId, int id, int itemId, float x, float y, float z)
        {
            OnDropItemAsync(userId, id, itemId, x, y, z);
        }

        protected static async void OnDropItemAsync(int userId, int id, int itemId, float x, float y, float z)
        {
            var itemPos = new Vector3(x, y, z);
            if (Main.GetDistanceToSquared(itemPos, GetEntityCoords(GetPlayerPed(-1), true)) > 50f)
                return;
            
            var model = Client.Inventory.GetItemHashById(itemId);
            var prop = await Objects.CreateObjectLocally(model, itemPos, new Vector3(0, 0, 0), true);		    

            if (itemId == 195 || itemId == 196 || itemId == 197 || itemId == 198 || itemId == 199 )
                SetEntityHeading(prop.Handle, GetEntityRotationVelocity(GetPlayerPed(-1)).Z - 90);
            
            prop.HasGravity = true;
            prop.ApplyForce(itemPos + new Vector3(0, 0, 0.1f), new Vector3(), ForceType.ForceNoRot);
                
            
            await Delay(1500);
            
            InventoryData objData = new InventoryData
            {
                Id = id,
                ItemId = itemId,
                Prop = prop,
                Model = model,
                Pos = prop.Position,
                Rot = prop.Rotation,
                IsCreate = true
            };
            ObjectList.Add(objData);

            if (userId == User.Data.id)
                UpdateItemPosServer(id, itemId, prop.Position, prop.Rotation, InventoryTypes.World, 0);
        }

        private static async Task Draw()
        {
            await Delay(500);
            var pos = GetEntityCoords(GetPlayerPed(-1), true);
            
            foreach (var item in ObjectList)
            {
                if (item == null) continue;
                if (item.IsDelete) continue;
                
                if (Client.Sync.Data.HasLocally(User.GetServerId(), "TakeItem" + item.Id))
                {
                    if (item.IsCreate || item.Prop != null)
                    {
                        try
                        {
                            item.Prop.Delete();
                            item.Prop = null;
                            item.IsCreate = false;
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine("DELETE INV OBJECT1: " + e.ToString());
                        }
                    }
                    item.IsDelete = true;
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "TakeItem" + item.Id);
                    continue;
                }
                
                if (Main.GetDistanceToSquared(pos, item.Pos) > LoadRange)
                {
                    if (!item.IsCreate) continue;
                    if (item.Prop == null) continue;
                    if (item.Prop.Model.Hash == 0) continue;
                    try
                    {
                        item.Prop.Delete();
                        item.Prop = null;
                        item.IsCreate = false;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("DELETE INV OBJECT1: " + e.ToString());
                    }
                }
                else
                {
                    if (!item.IsCreate)
                    {
                        if (item.Prop != null) continue;
                        
                        try
                        {
                            item.Prop = await Objects.CreateObjectLocally(item.Model, item.Pos, item.Rot, true);
                            
                            item.Prop.HasGravity = true;
                            item.Prop.ApplyForce(item.Pos + new Vector3(0, 0, 0.1f), new Vector3(), ForceType.ForceNoRot);

                            item.IsCreate = true;
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine($"ERROR CREATE INV OBJECT: {Convert.ToInt64(item.Model)}|{item.Pos.X}|{item.Pos.Y}|{item.Pos.Z}");
                        }
                    }
                    else
                    {
                        if (item.Prop == null) continue;
                        if (!item.Prop.IsDead) continue;	
                        
                        try
                        {
                            item.Prop.Delete();
                            item.Prop = null;
                            item.IsCreate = false;
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine("DELETE OBJECT1: " + e.ToString());
                        }
                    }
                }
            } 
        }
    }
}

public class InventoryData
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public Prop Prop { get; set; }
    public Model Model { get; set; }
    public Vector3 Pos { get; set; }
    public Vector3 Rot { get; set; }
    public int OwnerType { get; set; }
    public int OwnerId { get; set; }
    public int Count { get; set; }
    public bool IsCreate { get; set; }
    public bool IsDelete { get; set; }
}

public class InventoryTypes
{
    public static int World => 0;
    public static int Player => 1;
    public static int VehicleOwner => 2;
    public static int VehicleServer => 3;
    public static int VehicleNpc => 4;
    public static int House => 5;
    public static int Apartment => 6;
    public static int Bag => 7;
    public static int Vehicle => 8;
    public static int StockGang => 9;
    public static int Fridge => 10;
    public static int UserStock => 11;
}

public class InventoryWorldData
{
    public int id { get; set; }
    public int itemId { get; set; }
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float rotx { get; set; }
    public float roty { get; set; }
    public float rotz { get; set; }
}