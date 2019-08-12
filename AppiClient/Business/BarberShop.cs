using System;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Business
{
    public class BarberShop : BaseScript
    {
        public static void LoadAll()
        {
            for (int i = 0; i < Main.BarberShops.Length / 4; i++)
            {
                Vector3 shopPos = new Vector3((float) Main.BarberShops[i, 0], (float) Main.BarberShops[i, 1], (float) Main.BarberShops[i, 2]);

                var blip = World.CreateBlip(shopPos);
                blip.Sprite = (BlipSprite) 71;
                blip.Name = "Парикмахерская";
                blip.IsShortRange = true;
                blip.Scale = 0.8f;
                
                Managers.Checkpoint.Create(blip.Position, 1.4f, "shop:menu");
                Managers.Marker.Create(blip.Position, 1f, 1f, Managers.Marker.Blue.R, Managers.Marker.Blue.G, Managers.Marker.Blue.B, Managers.Marker.Blue.A);
            }
        }

        public static void CheckPosForOpenMenu()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            int shopId = GetShopIdInRadius(playerPos, 2f);
            if (shopId == -1) return;
            MenuList.ShowBarberShopMenu(shopId);
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
                        Managers.Inventory.UnEquipItem(8);
                        await Delay(2000);
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
                    Managers.Inventory.AddItemServer(item, count, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                    
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_84", Inventory.GetItemNameById(item), count));
                    User.RemoveMoney(price * count);
                    Business.AddMoney(shopId, price * count);
                    
                    Managers.Inventory.UpdateAmount(User.Data.id, InventoryTypes.Player);
                    break;
            }
        }

        public static int GetShopIdInRadius(Vector3 pos, float radius)
        {
            for (int i = 0; i < Main.BarberShops.Length / 4; i++)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3((float) Main.BarberShops[i, 0], (float) Main.BarberShops[i, 1], (float) Main.BarberShops[i, 2])) < radius)
                    return Convert.ToInt32(Main.BarberShops[i, 3]);
            }
            return -1;
        }

        public static double[,] GetShopInRadius(Vector3 pos, float radius)
        {
            for (int i = 0; i < Main.BarberShops.Length / 4; i++)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3((float) Main.BarberShops[i, 0], (float) Main.BarberShops[i, 1], (float) Main.BarberShops[i, 2])) < radius)
                    return Main.BarberShops;
            }
            return null;
        }

        public static Vector3 FindNearest(Vector3 pos)
        {
            var shopPosPrew = new Vector3((float) Main.BarberShops[0, 0], (float) Main.BarberShops[0, 1], (float) Main.BarberShops[0, 2]);
            for (int i = 0; i < Main.BarberShops.Length / 4; i++)
            {
                var shopPos = new Vector3((float) Main.BarberShops[i, 0], (float) Main.BarberShops[i, 1], (float) Main.BarberShops[i, 2]);
                if(Main.GetDistanceToSquared(shopPos, pos) < Main.GetDistanceToSquared(shopPosPrew, pos))
                    shopPosPrew = shopPos;
            }
            return shopPosPrew;
        }
    }
}