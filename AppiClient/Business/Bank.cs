using System;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Business
{
    public class Bank : BaseScript
    {
        public static double[,] Banks =
        {
            { 253.4611, 220.7204, 106.2865, 108 },
            { 251.749, 221.4658, 106.2865, 108 },
            { 248.3227, 222.5736, 106.2867, 108 },
            { 246.4875, 223.2582, 106.2867, 108 },
            { 243.1434, 224.4678, 106.2868, 108 },
            { 241.1435, 225.0419, 106.2868, 108 },
            { 148.5, -1039.971, 29.37775, 1 },
            { 1175.054, 2706.404, 38.09407, 1 },
            { -1212.83, -330.3573, 37.78702, 1 },
            { 314.3541, -278.5519, 54.17077, 1 },
            { -2962.951, 482.8024, 15.7031, 1 },
            { -350.6871, -49.60739, 49.04258, 1 },
            { -111.1722, 6467.846, 31.62671, 2 },
            { -113.3064, 6469.969, 31.62672, 2 }
        };
        
        public static void LoadAll()
        {
            for (int i = 0; i < Banks.Length / 4; i++)
            {
                Vector3 bankPos = new Vector3((float) Banks[i, 0], (float) Banks[i, 1], (float) Banks[i, 2]);                
                Managers.Checkpoint.Create(bankPos, 1.4f, "show:menu");
                Managers.Marker.Create(bankPos - new Vector3(0, 0, 1), 1f, 1f, Managers.Marker.Blue.R, Managers.Marker.Blue.G, Managers.Marker.Blue.B, Managers.Marker.Blue.A);
            }
        }

        public static async void Widthdraw(bool isAtm = false)
        {
            if (isAtm)
            {
                int moneyAtm = Convert.ToInt32(await Menu.GetUserInput(Lang.GetTextToPlayer("_lang_1"), null, 5));
                if (moneyAtm < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                    
                if (await User.GetBankMoney() < moneyAtm)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_2"));
                    return;
                }
                    
                if (moneyAtm > 10000)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_3"));
                    return;
                }
                
                User.RemoveBankMoney(moneyAtm);
                User.AddCashMoney(moneyAtm);
                SendSmsBankOperation(Lang.GetTextToPlayer("_lang_4", moneyAtm));
                return;
            }
            
            int money = Convert.ToInt32(await Menu.GetUserInput(Lang.GetTextToPlayer("_lang_1"), null, 10));
            if (money < 1)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                return;
            }
                    
            if (await User.GetBankMoney() < money)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_2"));
                return;
            }
                
            User.RemoveBankMoney(money);
            User.AddCashMoney(money);
            SendSmsBankOperation(Lang.GetTextToPlayer("_lang_4", money));
        }

        public static async void Deposit(bool isAtm = false)
        {            
            if (isAtm)
            {
                int moneyAtm = Convert.ToInt32(await Menu.GetUserInput(Lang.GetTextToPlayer("_lang_1"), null, 5));
                if (moneyAtm < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                    
                if (await User.GetCashMoney() < moneyAtm)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_5"));
                    return;
                }
                    
                if (moneyAtm > 10000)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_6"));
                    return;
                }
                
                User.RemoveCashMoney(moneyAtm);
                User.AddBankMoney(moneyAtm);
                SendSmsBankOperation(Lang.GetTextToPlayer("_lang_7", moneyAtm));
                return;
            }
            
            int money = Convert.ToInt32(await Menu.GetUserInput(Lang.GetTextToPlayer("_lang_1"), null, 10));
            if (money < 1)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                return;
            }
                    
            if (await User.GetCashMoney() < money)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_5"));
                return;
            }
                
            User.RemoveCashMoney(money);
            User.AddBankMoney(money);
            SendSmsBankOperation(Lang.GetTextToPlayer("_lang_7", money));
        }

        public static void NewCard(int bankId, int price)
        {
            if (User.Data.money < price)
            {
                Managers.Notification.SendWithTime(Lang.GetTextToPlayer("_lang_5"));
                return;
            }
            
            if (User.Data.bank_prefix > 0)
            {
                Managers.Notification.SendWithTime(Lang.GetTextToPlayer("_lang_8"));
                return;
            }
            
            Random rand = new Random();
            int prefix = 1111;

            switch (bankId)
            {
                case 1:
                    prefix = 2222;
                    break;
                case 2:
                    prefix = 3333;
                    break;
                case 108:
                    prefix = 4444;
                    break;
            }

            int plId = User.GetServerId();

            User.Data.bank_number = rand.Next(10000, 9999999);
            User.Data.bank_prefix = prefix;
            
            Sync.Data.Set(plId, "bank_prefix", prefix);
            Sync.Data.Set(plId, "bank_number", User.Data.bank_number);
            
            SendSmsBankOperationCreateCard();
            
            User.RemoveCashMoney(price);
            
            if (bankId == 0)
                Coffer.AddMoney(price);
            else
                Business.AddMoney(bankId, price);
        }

        public static async void CloseCard()
        {
            int plId = User.GetServerId();
            
            SendSmsBankOperationCloseCard();

            User.Data.bank_number = 0;
            User.Data.bank_prefix = 0;
            
            Sync.Data.Set(plId, "bank_prefix", 0);
            Sync.Data.Set(plId, "bank_number", 0);

            int money = await User.GetBankMoney();
            User.RemoveBankMoney(money);
            User.AddCashMoney(money);
        }

        public static void SendSmsBankOperation(string text)
        {
            switch (User.Data.bank_prefix)
            {
                case 1111:
                    Managers.Notification.SendPicture(text, "~r~Maze~s~ Bank", Lang.GetTextToPlayer("_lang_9"), "CHAR_BANK_MAZE", Managers.Notification.TypeChatbox);
                    break;
                case 2222:
                    Managers.Notification.SendPicture(text, "~g~Fleeca~s~ Bank", Lang.GetTextToPlayer("_lang_9"), "CHAR_BANK_FLEECA", Managers.Notification.TypeChatbox);
                    break;
                case 3333:
                    Managers.Notification.SendPicture(text, "~b~Blaine~s~ Bank", Lang.GetTextToPlayer("_lang_9"), "CHAR_STEVE_TREV_CONF", Managers.Notification.TypeChatbox);
                    break;
                case 4444:
                    Managers.Notification.SendPicture(text, "~o~Pacific~s~ Bank", Lang.GetTextToPlayer("_lang_9"), "CHAR_STEVE_MIKE_CONF", Managers.Notification.TypeChatbox);
                    break;
            }
        }

        public static void SendSmsBankOperationCreateCard()
        {
            switch (User.Data.bank_prefix)
            {
                case 1111:
                    Managers.Notification.SendPicture(Lang.GetTextToPlayer("_lang_12"), "~r~Maze~s~ Bank", Lang.GetTextToPlayer("_lang_10"), "CHAR_BANK_MAZE", Managers.Notification.TypeChatbox);
                    break;
                case 2222:
                    Managers.Notification.SendPicture(Lang.GetTextToPlayer("_lang_12"), "~g~Fleeca~s~ Bank", Lang.GetTextToPlayer("_lang_10"), "CHAR_BANK_FLEECA", Managers.Notification.TypeChatbox);
                    break;
                case 3333:
                    Managers.Notification.SendPicture(Lang.GetTextToPlayer("_lang_12"), "~b~Blaine~s~ Bank", Lang.GetTextToPlayer("_lang_10"), "CHAR_STEVE_TREV_CONF", Managers.Notification.TypeChatbox);
                    break;
                case 4444:
                    Managers.Notification.SendPicture(Lang.GetTextToPlayer("_lang_12"), "~o~Pacific~s~ Bank", Lang.GetTextToPlayer("_lang_10"), "CHAR_STEVE_MIKE_CONF", Managers.Notification.TypeChatbox);
                    break;
            }
        }

        public static void SendSmsBankOperationCloseCard()
        {
            switch (User.Data.bank_prefix)
            {
                case 1111:
                    Managers.Notification.SendPicture(Lang.GetTextToPlayer("_lang_13"), "~r~Maze~s~ Bank", Lang.GetTextToPlayer("_lang_11"), "CHAR_BANK_MAZE", Managers.Notification.TypeChatbox);
                    break;
                case 2222:
                    Managers.Notification.SendPicture(Lang.GetTextToPlayer("_lang_13"), "~g~Fleeca~s~ Bank", Lang.GetTextToPlayer("_lang_11"), "CHAR_BANK_FLEECA", Managers.Notification.TypeChatbox);
                    break;
                case 3333:
                    Managers.Notification.SendPicture(Lang.GetTextToPlayer("_lang_13"), "~b~Blaine~s~ Bank", Lang.GetTextToPlayer("_lang_11"), "CHAR_STEVE_TREV_CONF", Managers.Notification.TypeChatbox);
                    break;
                case 4444:
                    Managers.Notification.SendPicture(Lang.GetTextToPlayer("_lang_13"), "~o~Pacific~s~ Bank", Lang.GetTextToPlayer("_lang_11"), "CHAR_STEVE_MIKE_CONF", Managers.Notification.TypeChatbox);
                    break;
            }
        }
        
        public static void CheckPosForOpenMenu()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            int bankId = GetBankIdInRadius(playerPos, 2f);
            if (bankId == -1) return;
            MenuList.ShowBankMenu(bankId);
        }
        
        public static void CheckPosForBankOfficeOpenMenu()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            if (Main.GetDistanceToSquared(Managers.Pickup.BankMazeOfficePos, playerPos) < Managers.Pickup.DistanceCheck)
                MenuList.ShowMazeBankOfficeMenu();
        }
        
        public static int GetBankIdInRadius(Vector3 pos, float radius)
        {
            for (int i = 0; i < Banks.Length / 4; i++)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3((float) Banks[i, 0], (float) Banks[i, 1], (float) Banks[i, 2])) < radius)
                    return Convert.ToInt32(Banks[i, 3]);
            }
            return -1;
        }

        public static double[,] GetBankInRadius(Vector3 pos, float radius)
        {
            for (int i = 0; i < Banks.Length / 4; i++)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3((float) Banks[i, 0], (float) Banks[i, 1], (float) Banks[i, 2])) < radius)
                    return Banks;
            }
            return null;
        }
    }
}