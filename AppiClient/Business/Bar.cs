using System;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Business
{
    public class Bar : BaseScript
    {
        public static void LoadAll()
        {
            for (int i = 0; i < Main.Bars.Length / 4; i++)
            {
                Vector3 shopPos = new Vector3((float) Main.Bars[i, 0], (float) Main.Bars[i, 1], (float) Main.Bars[i, 2]);

                if ((int) Main.Bars[i, 3] == 122)
                {
                    var blip = World.CreateBlip(new Vector3(4.723007f, 220.3487f, 106.7251f));
                    blip.Sprite = (BlipSprite) 614;
                    blip.Name = Lang.GetTextToPlayer("_lang_14");
                    blip.IsShortRange = true;
                    blip.Scale = 0.8f;
                }
                else
                {
                    var blip = World.CreateBlip(shopPos);
                    if ((int) Main.Bars[i, 3] == 49)
                        blip.Sprite = (BlipSprite) 121;
                    else if ((int) Main.Bars[i, 3] == 53)
                        blip.Sprite = (BlipSprite) 614;
                    else if ((int) Main.Bars[i, 3] == 80)
                        blip.Sprite = (BlipSprite) 102;
                    else
                        blip.Sprite = (BlipSprite) 93;

                    blip.Name = Lang.GetTextToPlayer("_lang_14");
                    blip.IsShortRange = true;
                    blip.Scale = 0.8f;
                }
                
                Managers.Checkpoint.Create(shopPos, 1.4f, "bar:menu");
                Managers.Marker.Create(shopPos, 1f, 1f, Managers.Marker.Blue.R, Managers.Marker.Blue.G, Managers.Marker.Blue.B, Managers.Marker.Blue.A);
            }
        }

        public static async void CheckPosForOpenMenu()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            int shopId = GetBarIdInRadius(playerPos, 2f);
            if (shopId == -1) return;
            MenuList.ShowBarMenu(shopId, await Business.GetPrice(shopId));
        }

        public static async void Buy(string item, int price, int shopId)
        {
            await User.GetAllData();
            
            if (User.GetMoneyWithoutSync() < price)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                return;
            }

            switch (item)
            {
                case "water":
                    Chat.SendMeCommand(Lang.GetTextToPlayer("_lang_16"));
                    User.AddWaterLevel(100);
                    User.RemoveMoney(price);
                    Business.AddMoney(shopId, price);
            
                    User.PlayDrinkAnimation();
                    break;
                case "cola":
                    Chat.SendMeCommand(Lang.GetTextToPlayer("_lang_17"));
                    User.AddWaterLevel(20);
                    User.RemoveMoney(price);
                    Business.AddMoney(shopId, price);
            
                    User.PlayDrinkAnimation();
                    break;
                case "limonad":
                    Chat.SendMeCommand(Lang.GetTextToPlayer("_lang_18"));
                    User.AddWaterLevel(20);
                    User.RemoveMoney(price);
                    Business.AddMoney(shopId, price);
            
                    User.PlayDrinkAnimation();
                    break;
                case "bear":
                    Chat.SendMeCommand(Lang.GetTextToPlayer("_lang_19"));
                    User.RemoveMoney(price);
                    Business.AddMoney(shopId, price);
                    
                    User.AddDrunkLevel(2);
            
                    User.PlayDrinkAnimation();
                    break;
                case "vodka":
                    Chat.SendMeCommand(Lang.GetTextToPlayer("_lang_20"));
                    User.RemoveMoney(price);
                    Business.AddMoney(shopId, price);
                    
                    User.AddDrunkLevel(4);
            
                    User.PlayDrinkAnimation();
                    break;
                case "whishkey":
                    Chat.SendMeCommand(Lang.GetTextToPlayer("_lang_21"));
                    User.RemoveMoney(price);
                    Business.AddMoney(shopId, price);
                    
                    User.AddDrunkLevel(5);
            
                    User.PlayDrinkAnimation();
                    break;
            }
        }

        public static int GetBarIdInRadius(Vector3 pos, float radius)
        {
            for (int i = 0; i < Main.Bars.Length / 4; i++)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3((float) Main.Bars[i, 0], (float) Main.Bars[i, 1], (float) Main.Bars[i, 2])) < radius)
                    return Convert.ToInt32(Main.Bars[i, 3]);
            }
            return -1;
        }

        public static double[,] GetBarInRadius(Vector3 pos, float radius)
        {
            for (int i = 0; i < Main.Bars.Length / 4; i++)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3((float) Main.Bars[i, 0], (float) Main.Bars[i, 1], (float) Main.Bars[i, 2])) < radius)
                    return Main.Bars;
            }
            return null;
        }

        public static Vector3 FindNearest(Vector3 pos)
        {
            var shopPosPrew = new Vector3((float) Main.Bars[0, 0], (float) Main.Bars[0, 1], (float) Main.Bars[0, 2]);
            for (int i = 0; i < Main.Bars.Length / 4; i++)
            {
                var shopPos = new Vector3((float) Main.Bars[i, 0], (float) Main.Bars[i, 1], (float) Main.Bars[i, 2]);
                if(Main.GetDistanceToSquared(shopPos, pos) < Main.GetDistanceToSquared(shopPosPrew, pos))
                    shopPosPrew = shopPos;
            }
            return shopPosPrew;
        }
    }
}