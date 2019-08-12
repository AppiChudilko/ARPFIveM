using System;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Business
{
    public class Shop : BaseScript
    {
        //public static Vector3 ShopElPos = new Vector3(-658.8007f, -857.2805f, 24.50002f);
        public static Vector3 ShopElPos = new Vector3(-657.2524f, -857.301f, 24.49003f);
        public static Vector3 ShopElPos1 = new Vector3(1133.834f, -473.1605f, 65.76524f);
        
        public static void LoadAll()
        {
            for (int i = 0; i < Main.Shops.Length / 4; i++)
            {
                Vector3 shopPos = new Vector3((float) Main.Shops[i, 0], (float) Main.Shops[i, 1], (float) Main.Shops[i, 2]);

                var blip = World.CreateBlip(shopPos);
                blip.Sprite = (BlipSprite) 52;
                blip.Name = Lang.GetTextToPlayer("_lang_59");
                blip.IsShortRange = true;
                blip.Scale = 0.8f;
                
                Managers.Checkpoint.Create(blip.Position, 1.4f, "shop:menu");
                Managers.Marker.Create(blip.Position - new Vector3(0, 0, 1), 1f, 1f, Managers.Marker.Blue.R, Managers.Marker.Blue.G, Managers.Marker.Blue.B, Managers.Marker.Blue.A);
            }
            
            
            var blipEl = World.CreateBlip(ShopElPos);
            blipEl.Sprite = (BlipSprite) 521;
            blipEl.Name = "Магазин электроники Digital Den";
            blipEl.IsShortRange = true;
            blipEl.Scale = 0.8f;
            
            Managers.Checkpoint.Create(blipEl.Position, 1.4f, "shop:menu");
            Managers.Marker.Create(blipEl.Position - new Vector3(0, 0, 1), 1f, 1f, Managers.Marker.Blue.R, Managers.Marker.Blue.G, Managers.Marker.Blue.B, Managers.Marker.Blue.A);
            
            
            blipEl = World.CreateBlip(ShopElPos1);
            blipEl.Sprite = (BlipSprite) 521;
            blipEl.Name = "Магазин электроники Digital Den";
            blipEl.IsShortRange = true;
            blipEl.Scale = 0.8f;
            
            Managers.Checkpoint.Create(blipEl.Position, 1.4f, "shop:menu");
            Managers.Marker.Create(blipEl.Position - new Vector3(0, 0, 1), 1f, 1f, Managers.Marker.Blue.R, Managers.Marker.Blue.G, Managers.Marker.Blue.B, Managers.Marker.Blue.A);
        }

        public static async void CheckPosForOpenMenu()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            int shopId = GetShopIdInRadius(playerPos, 2f);
            if (shopId == -1)
            {
                if (Main.GetDistanceToSquared(playerPos, ShopElPos) < 2f)
                    MenuList.ShowElectroShopMenu(120);
                if (Main.GetDistanceToSquared(playerPos, ShopElPos1) < 2f)
                    MenuList.ShowElectroShopMenu(126);
                return;
            }
            MenuList.ShowShopMenu(shopId, await Business.GetPrice(shopId));
        }

        public static async void Buy(int item, int price, int shopId, int count = 1)
        {
            await User.GetAllData();
            
            if (User.GetMoneyWithoutSync() < price * count)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                return;
            }

            int plId = User.GetServerId();

            switch (item)
            {
                case 8:
                    if (User.Data.phone_code > 0 || User.Data.phone > 0)
                    {
                        Notification.SendWithTime("~r~У Вас уже есть телефон");
                        return;
                    }

                    Random rand = new Random();
                    
                    User.Data.phone_code = 555;
                    User.Data.phone = rand.Next(10000, 999999);

                    Sync.Data.Set(plId, "phone_code", User.Data.phone_code);
                    Sync.Data.Set(plId, "phone", User.Data.phone);
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_80", $"{User.Data.phone_code}-{User.Data.phone}"));

                    User.RemoveMoney(price);
                    Business.AddMoney(shopId, price);
                    break;
                case 7:
                    if (User.Data.item_clock)
                    {
                        Managers.Inventory.UnEquipItem(7);
                        await Delay(1000);
                    }

                    User.Data.item_clock = true;
                    Sync.Data.Set(plId, "item_clock", User.Data.item_clock);
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_81"));

                    User.RemoveMoney(price);
                    Business.AddMoney(shopId, price);
                    break;
                case 47:

                    if (User.Data.is_buy_walkietalkie)
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_82"));
                        break;
                    }

                    User.Data.is_buy_walkietalkie = true;
                    User.Data.walkietalkie_num = "70";
                    Sync.Data.Set(plId, "is_buy_walkietalkie", true);
                    Sync.Data.Set(plId, "walkietalkie_num", "70");
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_83"));

                    User.RemoveMoney(price);
                    Business.AddMoney(shopId, price);
                    break;
                default:
                    int amount = await Managers.Inventory.GetInvAmount(User.Data.id, InventoryTypes.Player);

                    if (amount + Inventory.GetItemAmountById(item) > User.Amount)
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_60"));
                        return;
                    }

                    if (item == 155 && !User.Data.allow_marg)
                    {
                        Notification.SendWithTime("~r~У Вас нет рецепта");
                        return;
                    }
                    
                    User.Data.allow_marg = false;
                    Sync.Data.Set(plId, "allow_marg", false);
                    
                    Managers.Inventory.AddItemServer(item, count, InventoryTypes.Player, User.Data.id, item == 155 ? 10 : 1, -1, -1, -1);
                    
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_84", Inventory.GetItemNameById(item), count));
                    User.RemoveMoney(price * count);
                    Business.AddMoney(shopId, price * count);
                    
                    Managers.Inventory.UpdateAmount(User.Data.id, InventoryTypes.Player);
                    break;
            }
        }

        public static int GetShopIdInRadius(Vector3 pos, float radius)
        {
            for (int i = 0; i < Main.Shops.Length / 4; i++)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3((float) Main.Shops[i, 0], (float) Main.Shops[i, 1], (float) Main.Shops[i, 2])) < radius)
                    return Convert.ToInt32(Main.Shops[i, 3]);
            }
            return -1;
        }

        public static double[,] GetShopInRadius(Vector3 pos, float radius)
        {
            for (int i = 0; i < Main.Shops.Length / 4; i++)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3((float) Main.Shops[i, 0], (float) Main.Shops[i, 1], (float) Main.Shops[i, 2])) < radius)
                    return Main.Shops;
            }
            return null;
        }

        public static Vector3 FindNearest(Vector3 pos)
        {
            var shopPosPrew = new Vector3((float) Main.Shops[0, 0], (float) Main.Shops[0, 1], (float) Main.Shops[0, 2]);
            for (int i = 0; i < Main.Shops.Length / 4; i++)
            {
                var shopPos = new Vector3((float) Main.Shops[i, 0], (float) Main.Shops[i, 1], (float) Main.Shops[i, 2]);
                if(Main.GetDistanceToSquared(shopPos, pos) < Main.GetDistanceToSquared(shopPosPrew, pos))
                    shopPosPrew = shopPos;
            }
            return shopPosPrew;
        }
    }
}