using System;
using System.Data;
using CitizenFX.Core;

namespace Server
{
    public class Apartment : BaseScript
    {
        public static int Count = 0;
        
        public static double[,] HouseIntList =
        {
            { 1972.724, 3816.522, 32.4 }, //0
            { 1972.724, 3816.522, 32.4 }, //1
            { 1397.666, 1163.96, 113.3336 }, //2
            { -1150.642, -1520.649, 9.63273 }, //3
            { 346.6588, -1012.286, -100.19624 }, //4
            { -110.2899, -14.17893, 69.51956 }, //5
            { 1274.026, -1719.583, 53.77145 }, //6
            { -1902.083, -572.4432, 18.09722 }, //7
            { 265.9925, -1007.13, -101.9903 }, //8
            { 151.2914, -1007.358, -100 }, //9
            { -859.8812, 691.0566, 151.8607 }, //10
            { -888.1979, -451.5262, 94.46114 }, //11
            { -611.2705, 58.77193, 97.20025 }, //12
            { -781.8554, 318.5241, 186.9488 }, //13
            { 938.6196, -539.2518, 42.63986 } //14
        };
        
        public static double[,] IntList =
        {
            { -784.5507, 323.7101, 210.9972 }, //0
            { -784.5507, 323.7101, 210.9972 }, //1
            { -774.5441, 331.6583, 159.0015 }, //2
            { -786.5707, 315.8176, 216.6384 }, //3
            { -774.1142, 342.0598, 195.6862 }, //4
            { -1449.955, -525.8912, 55.92899 }, //5
            { -1452.596, -540.1031, 73.04433 }, //6
            { -18.64361, -591.7625, 89.11481 }, //7
            { -30.95796, -595.1829, 79.0309 }, //8
            { -907.8871, -453.3672, 125.5344 }, //9
            { -923.0826, -378.5831, 84.48054 }, //10
            { -907.3911, -372.3075, 108.4403 }, //11
            { -912.6751, -365.2449, 113.2748 }, //12
            { 352.701, -931.4678, 45.37909 }, //13
            { -458.6382, -705.2723, 76.08691 }, //14
            { 127.8148, -866.9971, 123.2456 }, //15
            { -664.9214, -856.8912, 41.65313 }, //16
            { -53.68669, -620.9601, 75.99986 }, //17
            { -907.8871, -453.3672, 125.5344 }, //9
            { -907.8871, -453.3672, 125.5344 }, //9
        };

        public Apartment()
        {
            
            EventHandlers.Add("ARP:AddNewApart", new Action<int, int, int, int>(AddNewApart));
        }
        
        public static void AddNewApart(int bId, int floor, int price, int intId)
        {
            if (intId > 14)
                intId = 1;
            Appi.MySql.ExecuteQuery("INSERT INTO apartment (build_id, floor, interior_id, price) " +
                                    $"VALUES ('{bId}', '{floor}', '{intId}', '{price}')");
        }
        
        public static void LoadAll()
        {
            DataTable tbl = Appi.MySql.ExecuteQueryWithResult("SELECT * FROM apartment");

            foreach (DataRow row in tbl.Rows)
            {
                Sync.Data.Set(-100000 + (int) row["id"], "id", (int) row["id"]);
                Sync.Data.Set(-100000 + (int) row["id"], "user_id", (int) row["user_id"]);
                Sync.Data.Set(-100000 + (int) row["id"], "user_name", (string) row["user_name"]);
                Sync.Data.Set(-100000 + (int) row["id"], "price", (int) row["price"]);
                Sync.Data.Set(-100000 + (int) row["id"], "build_id", (int) row["build_id"]);
                Sync.Data.Set(-100000 + (int) row["id"], "floor", (int) row["floor"]);
                Sync.Data.Set(-100000 + (int) row["id"], "interior_id", (int) row["interior_id"]);
                Sync.Data.Set(-100000 + (int) row["id"], "is_exterior", (bool) row["is_exterior"]);
                Sync.Data.Set(-100000 + (int) row["id"], "pin", (int) row["pin"]);

                Count++;
            }
            
            Debug.WriteLine("Apartment load: " + Count);
        }
        
        public static void Save(int id, string userName, int userId)
        {
            Sync.Data.Set(-100000 + id, "user_name", userName);
            Sync.Data.Set(-100000 + id, "user_id", userId);
            string sql = "UPDATE apartment SET user_name = '" + userName + "', user_id = '" + userId + "', money_tax = '0', pin = '0' where id = '" + id + "'";
            Appi.MySql.ExecuteQuery(sql);
        }
    }
}