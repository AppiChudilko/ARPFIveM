using System;
using CitizenFX.Core;

namespace Client
{
    public class Coffer : BaseScript
    {
        protected static int Money = 500000;
        protected static int MoneyBomj = 120;
        protected static int MoneyLimit;
        protected static int MoneyOld = 500;
        protected static int Nalog = 3;
        protected static int BizzNalog = 5;
        
        public Coffer()
        {
            EventHandlers.Add("ARP:UpdateCoffer", new Action<int, int, int, int, int, int>(UpdateCoffer));
        }
        
        public static void UpdateCoffer(int money, int moneyBomj, int nalog, int bizzNalog, int moneyLimit, int moneyOld)
        {
            Money = money; 
            MoneyBomj = moneyBomj; 
            Nalog = nalog; 
            BizzNalog = bizzNalog; 
            MoneyLimit = moneyLimit; 
            MoneyOld = moneyOld; 
        }
        
        public static void SetMoney(int money)
        {
            Money = money;
            TriggerServerEvent("ARP:SetCofferMoney", Money);
        }

        public static void AddMoney(int money)
        {
            SetMoney(GetMoney() + money);
        }

        public static void RemoveMoney(int money)
        {
            SetMoney(GetMoney() - money);
        }

        public static void SetNalog(int nalog)
        {
            Nalog = nalog;
            TriggerServerEvent("ARP:SetCofferNalog", Nalog);
        }

        public static void SetMoneyOld(int money)
        {
            MoneyOld = money;
            TriggerServerEvent("ARP:SetCofferMoneyOld", MoneyOld);
        }

        public static void SetBizzNalog(int nalog)
        {
            BizzNalog = nalog;
            TriggerServerEvent("ARP:SetCofferBizzNalog", BizzNalog);
        }

        public static void SetPosob(int money)
        {
            MoneyBomj = money;
            TriggerServerEvent("ARP:SetCofferPosob", MoneyBomj);
        }

        public static int GetMoney()
        {
            return Money;
        }

        public static int GetMoneyOld()
        {
            return MoneyOld;
        }

        public static int GetNalog()
        {
            return Nalog;
        }

        public static int GetBizzNalog()
        {
            return BizzNalog;
        }

        public static int GetPosob()
        {
            return MoneyBomj;
        }
    }
}