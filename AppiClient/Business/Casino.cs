using System;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;
using Notification = Client.Managers.Notification;

namespace Client.Business
{
    public class Casino : BaseScript
    {
        public static Vector3 CasinoPos = new Vector3(-2065.634f, -1024.353f, 10.90894f);
        
        public static double[,] Markers =
        {
            { -2066.328, -1023.091, 10.90927, 165.2046, 91, 0 },
            { -2067.252, -1022.86, 10.90958, 153.4625, 91, 0 },
            { -2068.007, -1022.56, 10.90983, 160.3423, 91, 0 },
            { -2068.716, -1022.336, 10.91006, 159.9575, 91, 0 },
            { -2070.468, -1019.982, 10.91083, 85.3426, 91, 0 },
            { -2070.175, -1019.145, 10.91083, 68.83111, 91, 0 },
            { -2067.116, -1024.918, 10.90931, 338.4271, 91, 1 },
            { -2067.793, -1024.703, 10.90956, 342.6817, 91, 1 },
            { -2068.647, -1024.432, 10.90985, 2.437016, 91, 1 },
            { -2069.347, -1024.209, 10.91008, 336.2635, 91, 1 },
            { -2072.063, -1024.95, 10.91082, 63.71762, 91, 1 },
            { -2072.295, -1025.618, 10.91083, 65.85417, 91, 1 },
        };

        public static bool IsStart = false;
        
        public static void LoadAll()
        {
            var blip = World.CreateBlip(CasinoPos);
            blip.Sprite = (BlipSprite) 605;
            blip.Name = Lang.GetTextToPlayer("_lang_48");
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            for (int i = 0; i < Markers.Length / 6; i++)
            {
                Vector3 shopPos = new Vector3((float) Markers[i, 0], (float) Markers[i, 1], (float) Markers[i, 2]);
                
                Managers.Checkpoint.Create(shopPos, 1.2f, "show:menu");
                Managers.Marker.Create(shopPos, 0.7f, 0.7f, Managers.Marker.Blue.R, Managers.Marker.Blue.G, Managers.Marker.Blue.B, Managers.Marker.Blue.A);
            }
        }
        
        public static async void Start()
        {
            if (IsStart && IsCasino())
            {
                Stop();
                return;
            }
            
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            
            for (int i = 0; i < Markers.Length / 6; i++)
            {
                Vector3 gumPos = new Vector3((float) Markers[i, 0], (float) Markers[i, 1], (float) Markers[i, 2]);
                if (Main.GetDistanceToSquared(playerPos, gumPos) > 1.2f) continue;
                
                IsStart = true;
                User.PedRotation((float) Markers[i, 3]);
                SetEntityCoords(GetPlayerPed(-1), gumPos.X, gumPos.Y, gumPos.Z, true, false, false, true);

                await Delay(1000);
                
                User.PlayScenario("PROP_HUMAN_SEAT_BENCH");
                MenuList.ShowCasinoRateMenu(Convert.ToInt32(Markers[i, 4]), Convert.ToInt32(Markers[i, 5]));
            }
        }

        public static bool IsCasino()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            for (int i = 0; i < Markers.Length / 6; i++)
            {
                Vector3 gumPos = new Vector3((float) Markers[i, 0], (float) Markers[i, 1], (float) Markers[i, 2]);
                if (Main.GetDistanceToSquared(playerPos, gumPos) > 0.9f) continue;
                return true;
            }
            return false;
        }

        public static void Stop()
        {
            IsStart = false;
            User.PlayScenario("PROP_HUMAN_SEAT_BENCH");
            User.StopScenario();
            MenuList.HideMenu();
        }

        public static async void StartCombo(int casinoId, int rate)
        {
            Screen.LoadingPrompt.Show("Крутим барабан...");
            await Delay(10000);
            Screen.LoadingPrompt.Hide();
            
            var rand = new Random();
            var number1 = rand.Next(9);
            var number2 = rand.Next(9);
            var number3 = rand.Next(9);
            
            if ((number1 == number2 || number2 == number3 || number1 == number3) && rand.Next(3) == 0)
            {
                number1 = rand.Next(9);
                number2 = rand.Next(9);
                number3 = rand.Next(9);
            }
            
            Notification.SendWithTime($"~g~Выпало число ~y~{number1}{number2}{number3}");
            
            if (number1 == number2 && number2 == number3)
            {
                rate = rate * 3;
                Notification.SendWithTime($"~g~Ваш выигрыш умножен на 3: ${rate:#,#}.");
                User.AddCashMoney(rate);
                Business.RemoveMoney(casinoId, rate);
            }
            else if (number1 == number2 || number2 == number3 || number1 == number3)
            {
                rate = rate * 2;
                Notification.SendWithTime($"~g~Ваш выигрыш умножен на 2: ${rate:#,#}.");
                User.AddCashMoney(rate);
                Business.RemoveMoney(casinoId, rate);
            }
            else
            {
                User.RemoveCashMoney(rate);
                Business.AddMoney(casinoId, rate);
                Notification.SendWithTime($"~r~Вы проиграли ${rate}");
            }
            await Delay(500);
            MenuList.ShowCasinoRateMenu(casinoId, 0);
        }
        
        public static async void StartRulet(int casinoId, int rate, int idx)
        {
            switch (idx)
            {
                case 0:
                    Notification.SendWithTime("~b~Вы сделали ставку на ~y~Red");
                    break;
                case 1:
                    Notification.SendWithTime("~b~Вы сделали ставку на ~y~Black");
                    break;
                default:
                    Notification.SendWithTime("~b~Вы сделали ставку на ~y~Zero");
                    break;
            }
            
            Screen.LoadingPrompt.Show("Крутим рулетку...");
            await Delay(10000);
            Screen.LoadingPrompt.Hide();
            
            var rand = new Random();
            var number = rand.Next(36);
            await Delay(500);
            MenuList.ShowCasinoRateMenu(casinoId, 1);
            
            if (number % 2 == 0)
            {
                if (idx == 1 && rand.Next(4) > 1)
                    number++;
            }
            else
            {
                if (idx == 0 && rand.Next(4) > 1)
                    number++;
            }
            
            if (number == 0)
            {
                Notification.SendWithTime("~g~Выпало ~y~Zero");

                if (idx == 2)
                {
                    rate = rate * 5;
                    Notification.SendWithTime($"~g~Ваш выигрыш умножен на 5: ${rate:#,#}.");
                    User.AddCashMoney(rate);
                    Business.RemoveMoney(casinoId, rate);
                    return;
                }
                
                User.RemoveCashMoney(rate);
                Business.AddMoney(casinoId, rate);
                
                Notification.SendWithTime($"~r~Вы проиграли ${rate}");
            }
            else if (number % 2 == 0)
            {
                Notification.SendWithTime("~g~Выпал ~y~Black");
                if (idx == 1)
                {
                    rate = Convert.ToInt32(rate * 1.5);
                    Notification.SendWithTime($"~g~Ваш выигрыш умножен на 1.5: ${rate:#,#}.");
                    User.AddCashMoney(rate);
                    Business.RemoveMoney(casinoId, rate);
                    return;
                }
                
                User.RemoveCashMoney(rate);
                Business.AddMoney(casinoId, rate);
                
                Notification.SendWithTime($"~r~Вы проиграли ${rate}");
            }
            else
            {
                Notification.SendWithTime("~g~Выпал ~y~Red");
                if (idx == 0)
                {
                    rate = Convert.ToInt32(rate * 1.5);
                    Notification.SendWithTime($"~g~Ваш выигрыш умножен на 1.5: ${rate:#,#}.");
                    User.AddCashMoney(rate);
                    Business.RemoveMoney(casinoId, rate);
                    return;
                }
                
                User.RemoveCashMoney(rate);
                Business.AddMoney(casinoId, rate);
                
                Notification.SendWithTime($"~r~Вы проиграли ${rate}");
            }
        }
    }
}