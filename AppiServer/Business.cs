using System;
using System.Data;
using CitizenFX.Core;

namespace Server
{
    public class Business : BaseScript
    {
        public static int Count = 0;
        
        /*
        TypeList.Add("Банк"); //0
        TypeList.Add("Магазин 24/7"); //1
        TypeList.Add("Магазин одежды"); //2
        TypeList.Add("Автомастерская"); //3
        TypeList.Add("Пункт аренды"); //4
        TypeList.Add("Заправка"); //5
        TypeList.Add("Парикмахерская"); //6
        TypeList.Add("Развлечение"); //7
        TypeList.Add("Сотовые операторы"); //8
        TypeList.Add("Юридические компании"); //9
        TypeList.Add("Офис"); //10
        TypeList.Add("Ганшоп"); //11
        TypeList.Add("Тату Салон"); //12
        TypeList.Add("Разное"); //13
        */
        
        public static void LoadAll()
        {
            DataTable tbl = Appi.MySql.ExecuteQueryWithResult("SELECT * FROM business");

            foreach (DataRow row in tbl.Rows)
            {
                Sync.Data.Set(-20000 + (int) row["id"], "id", (int) row["id"]);
                Sync.Data.Set(-20000 + (int) row["id"], "name", (string) row["name"]);
                Sync.Data.Set(-20000 + (int) row["id"], "price", (int) row["price"]);
                Sync.Data.Set(-20000 + (int) row["id"], "user_name", (string) row["user_name"]);
                Sync.Data.Set(-20000 + (int) row["id"], "user_id", (int) row["user_id"]);
                Sync.Data.Set(-20000 + (int) row["id"], "bank", (int) row["bank"]);
                Sync.Data.Set(-20000 + (int) row["id"], "type", (int) row["type"]);
                Sync.Data.Set(-20000 + (int) row["id"], "price_product", (int) row["price_product"]);
                Sync.Data.Set(-20000 + (int) row["id"], "price_card1", (int) row["price_card1"]);
                Sync.Data.Set(-20000 + (int) row["id"], "price_card2", (int) row["price_card2"]);
                Sync.Data.Set(-20000 + (int) row["id"], "tarif", (int) row["tarif"]);
                Sync.Data.Set(-20000 + (int) row["id"], "interior", (int) row["interior"]);
                Sync.Data.Set(-20000 + (int) row["id"], "money_tax", (int) row["money_tax"]);
                Sync.Data.Set(-20000 + (int) row["id"], "score_tax", (int) row["score_tax"]);
                
                Sync.Data.Set(-20000 + (int) row["id"], "bizzPayDay", Convert.ToInt32((int) row["price"] / 180));
				if ((int) row["id"] == 92 || (int) row["id"] == 91)
					Sync.Data.Set(-20000 + (int) row["id"], "bizzPayDay", Convert.ToInt32((int) row["price"] / 360));
                
                if (Main.ServerName == "Sombrero")
                    Sync.Data.Set(-20000 + (int) row["id"], "bizzPayDay", Convert.ToInt32((int) row["price"] / 60));

                Count++;

                Debug.WriteLine("Business load " + Sync.Data.Get(-20000 + (int) row["id"], "name") + " - " + Sync.Data.Get(-20000 + (int) row["id"], "bank"));
            }
        }
        
        public static void AddMoney(int id, int count)
        {
            Sync.Data.Set(-20000 + id, "bank", GetMoney(id) + count);
        }

        public static void RemoveMoney(int id, int count)
        {
            Sync.Data.Set(-20000 + id, "bank", GetMoney(id) - count);
        }

        public static int GetMoney(int id)
        {
            if (Sync.Data.Has(-20000 + id, "bank"))
                return (int) Sync.Data.Get(-20000 + id, "bank");
            return 0;
        }
    }
}

public class BusinessData
{
    public int id {get;set;}
    public string name {get;set;}
    public int price {get;set;}
    public string user_name {get;set;}
    public int user_id {get;set;}
    public int bank {get;set;}
    public int type {get;set;}
    public int price_product {get;set;}
    public int price_card1 {get;set;}
    public int price_card2 {get;set;}
    public int tarif {get;set;}
    public int interior {get;set;}
}