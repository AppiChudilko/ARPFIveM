using System;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Business
{
    public class Gun : BaseScript
    {
        public static void LoadAll()
        {
            for (int i = 0; i < Main.GunShops.Length / 4; i++)
            {
                Vector3 shopPos = new Vector3((float) Main.GunShops[i, 0], (float) Main.GunShops[i, 1], (float) Main.GunShops[i, 2]);

                var blip = World.CreateBlip(shopPos);
                blip.Sprite = (BlipSprite) 110;
                blip.Name = Lang.GetTextToPlayer("_lang_66");
                blip.IsShortRange = true;
                blip.Scale = 0.8f;
                
                Managers.Checkpoint.Create(blip.Position, 1.4f, "shop:menu");
                Managers.Marker.Create(blip.Position - new Vector3(0, 0, 1), 1f, 1f, Managers.Marker.Blue.R, Managers.Marker.Blue.G, Managers.Marker.Blue.B, Managers.Marker.Blue.A);
            }
        }

        public static async void CheckPosForOpenMenu()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            int shopId = GetShopIdInRadius(playerPos, 2f);
            if (shopId == -1) return;
            MenuList.ShowGunShopMenu(shopId, await Business.GetPrice(shopId));
        }

        public static async void Buy(int item, int price, int count, int shopId)
        {
            await User.GetAllData();

            if (User.GetMoneyWithoutSync() < price)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                return;
            }
            
            int amount = await Managers.Inventory.GetInvAmount(User.Data.id, InventoryTypes.Player);
            if (Client.Inventory.GetItemAmountById(item) + amount > User.Amount)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_60"));
                return;
            }

            switch (item)
            {
                case int n when (n <= 136 && n >= 54 ):
                    
                    foreach(uint hash in Enum.GetValues(typeof(WeaponHash)))
                    {
                        string name = Enum.GetName(typeof(WeaponHash), hash);
                        if (!String.Equals(name, Client.Inventory.GetItemNameHashById(n), StringComparison.CurrentCultureIgnoreCase)) continue;
                        
                        Managers.Inventory.AddItemServer(n, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);

                        User.RemoveMoney(price);
                        Business.AddMoney(shopId, price);
                        break;
                    }
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
                case 276:

                    int ptFull = count;
                    while (ptFull > 0)
                    {
                        if (ptFull <= Managers.Inventory.AmmoItemIdToMaxCount(item))
                        {
                            Managers.Inventory.AddItemServer(item, 1, InventoryTypes.Player, User.Data.id, ptFull, -1, -1, -1);
                            ptFull = 0;
                        }
                        else
                        {
                            Managers.Inventory.AddItemServer(item, 1, InventoryTypes.Player, User.Data.id, Managers.Inventory.AmmoItemIdToMaxCount(item), -1, -1, -1);
                            ptFull = ptFull - Managers.Inventory.AmmoItemIdToMaxCount(item);
                        }
                    }
                    
                    User.RemoveMoney(price);
                    Business.AddMoney(shopId, price);
                    break;
                   
            }
            
            Managers.Inventory.UpdateAmount(User.Data.id, InventoryTypes.Player);
        }

        public static int GetShopIdInRadius(Vector3 pos, float radius)
        {
            for (int i = 0; i < Main.GunShops.Length / 4; i++)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3((float) Main.GunShops[i, 0], (float) Main.GunShops[i, 1], (float) Main.GunShops[i, 2])) < radius)
                    return Convert.ToInt32(Main.GunShops[i, 3]);
            }
            return -1;
        }

        public static double[,] GetShopInRadius(Vector3 pos, float radius)
        {
            for (int i = 0; i < Main.GunShops.Length / 4; i++)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3((float) Main.GunShops[i, 0], (float) Main.GunShops[i, 1], (float) Main.GunShops[i, 2])) < radius)
                    return Main.GunShops;
            }
            return null;
        }

        public static Vector3 FindNearest(Vector3 pos)
        {
            var shopPosPrew = new Vector3((float) Main.GunShops[0, 0], (float) Main.GunShops[0, 1], (float) Main.GunShops[0, 2]);
            for (int i = 0; i < Main.GunShops.Length / 4; i++)
            {
                var shopPos = new Vector3((float) Main.GunShops[i, 0], (float) Main.GunShops[i, 1], (float) Main.GunShops[i, 2]);
                if(Main.GetDistanceToSquared(shopPos, pos) < Main.GetDistanceToSquared(shopPosPrew, pos))
                    shopPosPrew = shopPos;
            }
            return shopPosPrew;
        }
    }
}