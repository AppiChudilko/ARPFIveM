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
        
        public static void UpdateHousePin(int id, int pin)
        {
            Server.Sync.Data.Set(100000 + id, "pin", pin);
            Appi.MySql.ExecuteQuery("UPDATE houses SET pin = '" + pin + "' where id = '" + id + "'");
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
                int_z = (float) row["int_z"]
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
}