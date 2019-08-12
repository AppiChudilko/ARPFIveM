using System;
using System.Collections.Generic;
using System.Data;
using CitizenFX.Core;

namespace Server.Managers
{
    public class Condo : BaseScript
    {
        public static List<CondoInfoGlobalData> CondoGlobalDataList = new List<CondoInfoGlobalData>();
        public static int MaxHouses;

        public Condo()
        {
            EventHandlers.Add("ARP:AddNewCondo", new Action<string, float, float, float, int, int>(AddNewCondo));
        }
     
        public static void LoadAllHouse()
        {
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM condo").Rows)
                LoadHouse(row);
            
            Debug.WriteLine("Total houses loaded: " + MaxHouses);
        }
        
        public static void AddNewCondo(string address, float x, float y, float z, int price, int intId)
        {
            if (intId > 14)
                intId = 1;

            float intx = (float) Apartment.HouseIntList[intId, 0];
            float inty = (float) Apartment.HouseIntList[intId, 1];
            float intz = (float) Apartment.HouseIntList[intId, 2];
            
            Debug.WriteLine($"{address}|{x}|{y}|{z}|{price}");
            Appi.MySql.ExecuteQuery("INSERT INTO condo (address, x, y, z, int_x, int_y, int_z, price) " +
                                    $"VALUES ('{address}', '{x}', '{y}', '{z}', '{intx}', '{inty}', '{intz}', '{price}')");
        }
        
        public static void SaveHouse(int id, string userName, int userId)
        {
            Server.Sync.Data.Set(300000 + id, "name_user", userName);
            Server.Sync.Data.Set(300000 + id, "id_user", userId);
            
            string sql = "UPDATE condo SET name_user = '" + userName + "', id_user = '" + userId + "', money_tax = '0' where id = '" + id + "'";
            Appi.MySql.ExecuteQuery(sql);
            
            TriggerClientEvent("ARP:UpdateClientCondoInfo", id, userName, userId);
        }
        
        public static void UpdateHousePin(int id, int pin)
        {
            Server.Sync.Data.Set(300000 + id, "pin", pin);
            Appi.MySql.ExecuteQuery("UPDATE condo SET pin = '" + pin + "' where id = '" + id + "'");
        }
        
        public static void LoadHouse(DataRow row)
        {
            var housesObj = new CondoInfoGlobalData()
            {
                id = (int) row["id"],
                address = (string) row["address"],
                price = (int) row["price"],
                name_user = (string) row["name_user"],
                id_user = (int) row["id_user"],
                pin = (int) row["pin"],
                x = (float) row["x"],
                y = (float) row["y"],
                z = (float) row["z"],
                int_x = (float) row["int_x"],
                int_y = (float) row["int_y"],
                int_z = (float) row["int_z"]
            };
            
            foreach (var property in typeof(CondoInfoGlobalData).GetProperties())
            {
                Server.Sync.Data.Reset(300000 + housesObj.id, property.Name);
                Server.Sync.Data.Set(300000 + housesObj.id, property.Name, property.GetValue(housesObj, null));
            }

            CondoGlobalDataList.Add(housesObj);
            MaxHouses++;
        }
    }
}

public class CondoInfoGlobalData
{
    public int id { get; set; }
    public string address { get; set; }
    public int price { get; set; }
    public string name_user { get; set; }
    public int id_user { get; set; }
    public int pin { get; set; }
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float int_x { get; set; }
    public float int_y { get; set; }
    public float int_z { get; set; }
}