using System.Collections.Generic;
using System.Data;
using CitizenFX.Core;

namespace Server.Managers
{
    public class House : BaseScript
    {
        public static List<HouseInfoGlobalData> HouseGlobalDataList = new List<HouseInfoGlobalData>();
        public static int MaxHouses;

        public static void LoadAllHouse()
        {
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM houses").Rows)
                LoadHouse(row);
            
            Debug.WriteLine("Total houses loaded: " + MaxHouses);
        }
        
        public static void SaveHouse(int id, int isBuy, string userName, int userId)
        {
            Server.Sync.Data.Set(100000 + id, "name_user", userName);
            Server.Sync.Data.Set(100000 + id, "id_user", userId);
            Server.Sync.Data.Set(100000 + id, "is_buy", isBuy == 1);
            
            string sql = "UPDATE houses SET is_buy = '" + isBuy + "', name_user = '" + userName + "', id_user = '" + userId + "', money_tax = '0' where id = '" + id + "'";
            Appi.MySql.ExecuteQuery(sql);
            
            TriggerClientEvent("ARP:UpdateClientHouseInfo", id, isBuy, userName, userId);
        }
        public static void SaveHouseHookup(int id, int userId, string pidn)
        {
            if (pidn == "pid2")
            {
                Server.Sync.Data.Set(100000 + id, "pid2", userId);
                string sql = "UPDATE houses SET pid2 = '" + userId + "' where id = '" + id + "'";
                Appi.MySql.ExecuteQuery(sql);
            }
            if (pidn == "pid3")
            {
                Server.Sync.Data.Set(100000 + id, "pid3", userId);
                string sql = "UPDATE houses SET pid3 = '" + userId + "' where id = '" + id + "'";
                Appi.MySql.ExecuteQuery(sql);
            }
            if (pidn == "pid4")
            {
                Server.Sync.Data.Set(100000 + id, "pid4", userId);
                string sql = "UPDATE houses SET pid4 = '" + userId + "' where id = '" + id + "'";
                Appi.MySql.ExecuteQuery(sql);
            }
            if (pidn == "pid5")
            {
                Server.Sync.Data.Set(100000 + id, "pid5", userId);
                string sql = "UPDATE houses SET pid5 = '" + userId + "' where id = '" + id + "'";
                Appi.MySql.ExecuteQuery(sql);
            }
            if (pidn == "pid6")
            {
                Server.Sync.Data.Set(100000 + id, "pid6", userId);
                string sql = "UPDATE houses SET pid6 = '" + userId + "' where id = '" + id + "'";
                Appi.MySql.ExecuteQuery(sql);
            }
            if (pidn == "pid7")
            {
                Server.Sync.Data.Set(100000 + id, "pid7", userId);
                string sql = "UPDATE houses SET pid7 = '" + userId + "' where id = '" + id + "'";
                Appi.MySql.ExecuteQuery(sql);
            }
            if (pidn == "pid8")
            {
                Server.Sync.Data.Set(100000 + id, "pid8", userId);
                string sql = "UPDATE houses SET pid8 = '" + userId + "' where id = '" + id + "'";
                Appi.MySql.ExecuteQuery(sql);
            }
            if (pidn == "pid9")
            {
                Server.Sync.Data.Set(100000 + id, "pid9", userId);
                string sql = "UPDATE houses SET pid9 = '" + userId + "' where id = '" + id + "'";
                Appi.MySql.ExecuteQuery(sql);
            }
            if (pidn == "pid10")
            {
                Server.Sync.Data.Set(100000 + id, "pid10", userId);
                string sql = "UPDATE houses SET pid10 = '" + userId + "' where id = '" + id + "'";
                Appi.MySql.ExecuteQuery(sql);
            }
        }
        
        public static void SaveHouseAntiHookup(int id, int userId)
                {
                    Server.Sync.Data.Set(100000 + id, "pid2", 0);
                    string sql2 = "UPDATE houses SET pid2 = '0' where id = '" + id + "'";
                    Server.Sync.Data.Set(100000 + id, "pid3", 0);
                    string sql3 = "UPDATE houses SET pid3 = '0' where id = '" + id + "'";
                    Server.Sync.Data.Set(100000 + id, "pid4", 0);
                    string sql4 = "UPDATE houses SET pid4 = '0' where id = '" + id + "'";
                    Server.Sync.Data.Set(100000 + id, "pid5", 0);
                    string sql5 = "UPDATE houses SET pid5 = '0' where id = '" + id + "'";
                    Server.Sync.Data.Set(100000 + id, "pid6", 0);
                    string sql6 = "UPDATE houses SET pid6 = '0' where id = '" + id + "'";
                    Server.Sync.Data.Set(100000 + id, "pid7", 0);
                    string sql7 = "UPDATE houses SET pid7 = '0' where id = '" + id + "'";
                    Server.Sync.Data.Set(100000 + id, "pid8", 0);
                    string sql8 = "UPDATE houses SET pid8 = '0' where id = '" + id + "'";
                    Server.Sync.Data.Set(100000 + id, "pid9", 0);
                    string sql9 = "UPDATE houses SET pid9 = '0' where id = '" + id + "'";
                    Server.Sync.Data.Set(100000 + id, "pid10", 0);
                    string sql10 = "UPDATE houses SET pid10 = '0' where id = '" + id + "'";
                    Appi.MySql.ExecuteQuery(sql2);
                    Appi.MySql.ExecuteQuery(sql3);
                    Appi.MySql.ExecuteQuery(sql4);
                    Appi.MySql.ExecuteQuery(sql5);
                    Appi.MySql.ExecuteQuery(sql6);
                    Appi.MySql.ExecuteQuery(sql7);
                    Appi.MySql.ExecuteQuery(sql8);
                    Appi.MySql.ExecuteQuery(sql9);
                    Appi.MySql.ExecuteQuery(sql10);
                    
                }
        
        public static void UpdateHousePin(int id, int pin)
        {
            Server.Sync.Data.Set(100000 + id, "pin", pin);
            Appi.MySql.ExecuteQuery("UPDATE houses SET pin = '" + pin + "' where id = '" + id + "'");
        }
        public static void UpdateApartmentPin(int id, int pin)
        {
            Server.Sync.Data.Set(100000 + id, "pin", pin);
            Appi.MySql.ExecuteQuery("UPDATE apartment SET pin = '" + pin + "' where id = '" + id + "'");
        }
        
        public static void LoadHouse(DataRow row)
        {
            var housesObj = new HouseInfoGlobalData()
            {
                id = (int) row["id"],
                address = (string) row["address"],
                price = (int) row["price"],
                name_user = (string) row["name_user"],
                id_user = (int) row["id_user"],
                is_buy = (bool) row["is_buy"],
                pin = (int) row["pin"],
                x = (float) row["x"],
                y = (float) row["y"],
                z = (float) row["z"],
                int_x = (float) row["int_x"],
                int_y = (float) row["int_y"],
                int_z = (float) row["int_z"],
                pid2 = (int) row["pid2"],
                pid3 = (int) row["pid3"],
                pid4 = (int) row["pid4"],
                pid5 = (int) row["pid5"],
                pid6 = (int) row["pid6"],
                pid7 = (int) row["pid7"],
                pid8 = (int) row["pid8"],
                pid9 = (int) row["pid9"],
                pid10 = (int) row["pid10"]
            };
            
            foreach (var property in typeof(HouseInfoGlobalData).GetProperties())
            {
                Server.Sync.Data.Reset(100000 + housesObj.id, property.Name);
                Server.Sync.Data.Set(100000 + housesObj.id, property.Name, property.GetValue(housesObj, null));
            }

            HouseGlobalDataList.Add(housesObj);
            MaxHouses++;
        }
    }
}

public class HouseInfoGlobalData
{
    public int id { get; set; }
    public string address { get; set; }
    public int price { get; set; }
    public string name_user { get; set; }
    public int id_user { get; set; }
    public bool is_buy { get; set; }
    public int pin { get; set; }
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float int_x { get; set; }
    public float int_y { get; set; }
    public float int_z { get; set; }
    public int pid2 { get; set; }
    public int pid3 { get; set; }
    public int pid4 { get; set; }
    public int pid5 { get; set; }
    public int pid6 { get; set; }
    public int pid7 { get; set; }
    public int pid8 { get; set; }
    public int pid9 { get; set; }
    public int pid10 { get; set; }
}