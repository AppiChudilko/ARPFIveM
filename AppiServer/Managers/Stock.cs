using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using CitizenFX.Core;

namespace Server.Managers
{
    public class Stock : BaseScript
    {
        public static List<StockInfoGlobalData> StockGlobalDataList = new List<StockInfoGlobalData>();
        public static int MaxHouses;

        public Stock()
        {
            EventHandlers.Add("ARP:AddNewStock", new Action<string, float, float, float, int>(AddNewStock));
            EventHandlers.Add("ARP:SaveStock", new Action<int>(SaveStock));
        }
        
        public static void AddNewStock(string address, float x, float y, float z, int price)
        {
            Debug.WriteLine($"{address}|{x}|{y}|{z}|{price}");
            Appi.MySql.ExecuteQuery("INSERT INTO stocks (address, x, y, z, price) " +
                                    $"VALUES ('{address}', '{x}', '{y}', '{z}', '{price}')");
        }

        public static void LoadAllStock()
        {
            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM stocks").Rows)
                LoadStock(row);
            
            Debug.WriteLine("Total houses loaded: " + MaxHouses);
        }
        
        public static void SaveStock(int id)
        {
            int pin1 = (int) Server.Sync.Data.Get(200000 + id, "pin1");
            int pin2 = (int) Server.Sync.Data.Get(200000 + id, "pin2");
            int pin3 = (int) Server.Sync.Data.Get(200000 + id, "pin3");
            Appi.MySql.ExecuteQuery("UPDATE stocks SET pin1 = '" + pin1 + "', pin2 = '" + pin2 + "', pin3 = '" + pin3 + "' where id = '" + id + "'");
        }
        
        public static void Save(int id, string userName, int userId)
        {
            Server.Sync.Data.Set(200000 + id, "user_name", userName);
            Server.Sync.Data.Set(200000 + id, "user_id", userId);
            
            Appi.MySql.ExecuteQuery("UPDATE stocks SET user_name = '" + userName + "', user_id = '" + userId + "', money_tax = '0' where id = '" + id + "'");
            
            TriggerClientEvent("ARP:UpdateClientStockInfo", id, userName, userId);
        }
        
        public static void LoadStock(DataRow row)
        {
            var housesObj = new StockInfoGlobalData()
            {
                id = (int) row["id"],
                price = (int) row["price"],
                address = (string) row["address"],
                user_name = (string) row["user_name"],
                user_id = (int) row["user_id"],
                pin1 = (int) row["pin1"],
                pin2 = (int) row["pin2"],
                pin3 = (int) row["pin3"],
                x = (float) row["x"],
                y = (float) row["y"],
                z = (float) row["z"],
            };
            
            foreach (var property in typeof(StockInfoGlobalData).GetProperties())
            {
                Server.Sync.Data.Reset(200000 + housesObj.id, property.Name);
                Server.Sync.Data.Set(200000 + housesObj.id, property.Name, property.GetValue(housesObj, null));
            }

            StockGlobalDataList.Add(housesObj);
            MaxHouses++;
        }
    }
}

public class StockInfoGlobalData
{
    public int id { get; set; }
    public string address { get; set; }
    public int price { get; set; }
    public string user_name { get; set; }
    public int user_id { get; set; }
    public int pin1 { get; set; }
    public int pin2 { get; set; }
    public int pin3 { get; set; }
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
}