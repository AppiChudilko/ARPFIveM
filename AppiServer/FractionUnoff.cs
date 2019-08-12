using System;
using System.Data;
using CitizenFX.Core;

namespace Server
{
    public class FractionUnoff : BaseScript
    {
        public static int Count = 0;

        public static void LoadAll()
        {
            DataTable tbl = Appi.MySql.ExecuteQueryWithResult("SELECT * FROM fraction_list");
            Count = 0;
            
            foreach (DataRow row in tbl.Rows)
            {
                Sync.Data.Set(-9000000 + (int) row["id"], "id", (int) row["id"]);
                Sync.Data.Set(-9000000 + (int) row["id"], "owner_id", (int) row["owner_id"]);
                Sync.Data.Set(-9000000 + (int) row["id"], "name", (string) row["name"]);
                Count++;

                Debug.WriteLine("Fraction load " + Sync.Data.Get(-9000000 + (int) row["id"], "name"));
            }
        }

        public static void Create([FromSource] Player player, string name)
        {
            if (!User.IsLogin(player)) return;
            int userId = (int) Sync.Data.Get(User.GetServerId(player), "id");
            Appi.MySql.ExecuteQuery("INSERT INTO fraction_list (owner_id, name) " + $"VALUES ('{userId}', '{name}')");
            TriggerClientEvent(player, "ARP:SendPlayerNotification", "~g~Вы создали организацию");
            
            DataTable tbl2 = Appi.MySql.ExecuteQueryWithResult($"SELECT * FROM fraction_list WHERE owner_id = '{userId}'");
            foreach (DataRow row in tbl2.Rows)
                Sync.Data.Set(User.GetServerId(player), "fraction_id2", (int) row["id"]);
            
            Sync.Data.Set(User.GetServerId(player), "rank2", 11);
            User.UpdateAllData(player);
            LoadAll();
        }

        public static void Rename([FromSource] Player player, int fractionId, string name)
        {
            int userId = (int) Sync.Data.Get(User.GetServerId(player), "id");
            Appi.MySql.ExecuteQuery($"UPDATE fraction_list SET name = '{name}' WHERE id = '{fractionId}' AND owner_id = '{userId}'");
            TriggerClientEvent(player, "ARP:SendPlayerNotification", "~y~Вы переименовали организацию");
            Sync.Data.Set(-9000000 + fractionId, "name", name);
        }

        public static void Delete([FromSource] Player player, int fractionId)
        {
            if (!User.IsLogin(player)) return;
            int userId = (int) Sync.Data.Get(User.GetServerId(player), "id");
            Appi.MySql.ExecuteQuery($"DELETE FROM fraction_list WHERE id = '{fractionId}' AND owner_id = '{userId}'");
            Appi.MySql.ExecuteQuery($"UPDATE users SET rank2 = '0', fraction_id2 = '0' WHERE fraction_id2 = {fractionId}");

            foreach (var p in new PlayerList())
            {
                if (fractionId == (int) Sync.Data.Get(User.GetServerId(p), "fraction_id2"))
                {
                    Sync.Data.Set(User.GetServerId(p), "fraction_id2", 0);
                    Sync.Data.Set(User.GetServerId(p), "rank2", 0);
                    User.UpdateAllData(player);
                    TriggerClientEvent(p, "ARP:SendPlayerNotification", "~y~Организация была расформирована");
                }
            }
            
            TriggerClientEvent(player, "ARP:SendPlayerNotification", "~y~Вы удалили организацию");
        }
    }
}