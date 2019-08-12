using System;
using System.Data;
using CitizenFX.Core;

namespace Server
{
    public class Coffer : BaseScript
    {
        public static string Name = "Government";
        public static int Money = 500000;
        public static int MoneyBomj = 120;
        public static int MoneyLimit;
        public static int MoneyOld = 500;
        public static int Nalog = 3;
        public static int BizzNalog = 5;
        public static int Id = 1;

        public static void LoadCoffer(int id)
        {
            Id = id;
            DataTable tbl = Appi.MySql.ExecuteQueryWithResult("SELECT * FROM coffers WHERE id = '" + id + "'");

            foreach (DataRow row in tbl.Rows)
            {
                Name = (string) row["name"];
                Money = (int) row["money"];
                MoneyBomj = (int) row["moneyBomj"];
                Nalog = (int) row["nalog"];
                BizzNalog = (int) row["nalog_bizz"];
                MoneyLimit = (int) row["moneyLimit"];
                MoneyOld = (int) row["moneyOld"];
            }

            Debug.WriteLine("Coffers load: $" + Money);
        }

        public static void SaveCoffer()
        {
            if(Money < 0) { LoadCoffer(1); return; }
            Appi.MySql.ExecuteQuery("UPDATE coffers SET name = '" + Name + "', money = '" + Money + "', moneyBomj = '" +
                               MoneyBomj + "', nalog = '" + Nalog + "', nalog_bizz = '" + BizzNalog + "', moneyOld = '" + MoneyOld + "', moneyLimit = '" + MoneyLimit + "' WHERE id = '" + Id +
                               "'");
            
            TriggerClientEvent("ARP:UpdateCoffer", Money, MoneyBomj, Nalog, BizzNalog, MoneyLimit, MoneyOld);
        }

        public static void SaveCofferLog(string doing, int moneyFull, int moneyCount, int money)
        {
            if(moneyFull == 0) return;
            string sql = "INSERT INTO coffers_log (do, timestamp, date, time, money_full, money_add, money) " +
            "VALUES ('" + doing + "', '" + Main.GetTimeStamp() + "', '" +
                         $"{DateTime.Now:yyyy-MM-dd}" + "', '" +
                         $"{DateTime.Now:HH:mm:ss}" + "', '" + moneyFull + "', '" + moneyCount + "', '" + money + "')";
            Appi.MySql.ExecuteQuery(sql);
        }

        public static void SetMoney(int money)
        {
            Money = money;
            SaveCoffer();
        }

        public static void AddMoney(int money)
        {
            Money = GetMoney() + money;
            SaveCoffer();
        }

        public static void RemoveMoney(int money)
        {
            Money = GetMoney() - money;
            SaveCoffer();
        }

        public static void SetNalog(int nalog)
        {
            Nalog = nalog;
            SaveCoffer();
        }

        public static void SetMoneyOld(int money)
        {
            MoneyOld = money;
            SaveCoffer();
        }

        public static void SetBizzNalog(int nalog)
        {
            BizzNalog = nalog;
            SaveCoffer();
        }

        public static void SetPosob(int money)
        {
            MoneyBomj = money;
            SaveCoffer();
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