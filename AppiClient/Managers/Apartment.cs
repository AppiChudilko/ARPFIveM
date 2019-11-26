using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Apartment : BaseScript
    {
        public static double[,] IntList =
        {
            { 0, 0, 0 }, //0
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
            { -664.9214, -856.8912, 40.65313 }, //16
            { -53.68669, -620.9601, 75.99986 }, //17
        };
        
        /*id,x,y,z,countFloor,gx,gy,gz*/
        public static dynamic[,] BuildList =
        {
            { -47.62529, -585.9817, 36.95303, 10, 0, 0, 0 }, //Пилбокс0
            { -59.31505, -616.3288, 36.35678, 8, 0, 0, 0 }, //Пилбокс1
            { -906.5585, -451.8453, 38.60527, 26, 0, 0, 0 }, //РокфордХиллз2
            { -909.408, -446.4228, 38.60527, 26, 0, 0, 0 }, //РокфордХиллз3
            { -1447.256, -537.7479, 33.74024, 10, 0, 0, 0 }, //ДельПьеро4
            { -774.9617, 312.6897, 84.69813, 24, 0, 0, 0 }, //Эклипс5
            { -268.4884, -962.137, 30.22313, 24, 0, 0, 0 }, //Пиллбокс6
            { 315.2311, -1092.664, 28.40107, 6, 0, 0, 0 }, //МишнРоу7
            { -1062.536, -1641.072, 3.4912427, 3, 0, 0, 0 }, //ЛаПуерта8
            { -1284.575, -1252.959, 3.099319, 3, 0, 0, 0 }, //Веспуччи9
            { -729.1189, -879.9578, 21.71092, 3, 0, 0, 0 }, //Сеул10
            { 561.5257, 93.09003, 95.09825, 3, 0, 0, 0 }, //Вайнвуд11
            { 773.5167, -150.4394, 74.62775, 3, 0, 0, 0 }, //Вайнвуд12
            { 1144.123, -1000.247, 44.307, 2, 0, 0, 0 }, //Мурьета13
            { 1145.298, -1008.498, 43.90734, 2, 0, 0, 0 }, //Мурьета14
            { 101.9671, -818.5921, 30.33646, 12, 0, 0, 0 }, //Мурьета15
            { -936.4761, -379.1135, 37.96133, 34, 0, 0, 0 }, //РичардМажестик16
            { 347.1563, -941.3441, 28.43246, 5, 0, 0, 0 }, //МишнРоу17
            { -470.4552, -678.8266, 31.71356, 10, 0, 0, 0 }, //Маленький Сеул18
            { 83.51759, -854.9324, 29.77145, 20, 0, 0, 0 }, //ПиллБокс19
            { -662.4131, -857.7378, 23.51864, 5, 0, 0, 0 }, //Маленький Сеул20
            
            {360.8011, -1072.488, 28.54089, 1, 0, 0, 0}, //21 Мишн-Роу
            {252.771, -1072.443, 28.37394, 2, 0, 0, 0}, //22 Мишн-Роу
            {185.7291, -1078.371, 28.27456, 11, 0, 0, 0}, //23 Пиллбокс-Хилл
            {145.91, -1058.97, 29.18612, 5, 0, 0, 0}, //24 Пиллбокс-Хилл
            {-297.3691, -829.5149, 31.41578, 14, 0, 0, 0}, //25 Пиллбокс-Хилл
            {8.011297, -916.2399, 28.90503, 14, 0, 0, 0}, //26 Пиллбокс-Хилл
            {15.71022, -941.5427, 28.905, 14, 0, 0, 0}, //27 Пиллбокс-Хилл
            {387.227, -993.5002, 28.41795, 2, 0, 0, 0}, //28 Мишн-Роу
            {387.3025, -973.9567, 28.43672, 2, 0, 0, 0}, //29 Мишн-Роу
            {390.9153, -909.4563, 28.41869, 2, 0, 0, 0}, //30 Мишн-Роу
            {368.463, -875.168, 28.29166, 2, 0, 0, 0}, //31 Текстайл-Сити
            {390.109, -75.59114, 67.18049, 11, 0, 0, 0}, //32 Хавик
            {388.3629, -0.5569023, 90.4148, 6, 0, 0, 0}, //33 Центр Вайнвуда
        };
        
        public static ApartmentData CurrentData = new ApartmentData();
        
        public static async Task<ApartmentData> GetAllData(int id)
        {
            var data = await Client.Sync.Data.GetAll(-100000 + id);
            if (data == null) return new ApartmentData();
            
            var bData = new ApartmentData();
            var localData = (IDictionary<String, Object>) data;
            foreach (var property in typeof(ApartmentData).GetProperties())
                property.SetValue(bData, localData[property.Name], null);

            return bData;
        }

        public static void LoadAll()
        {
            for (int i = 0; i < BuildList.Length / 7; i++)
            {
                var pos = new Vector3((float) BuildList[i, 0], (float) BuildList[i, 1], (float) BuildList[i, 2]);
                Checkpoint.Create(pos, 1.4f, "build");
                Marker.Create(pos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
                
                var blip = World.CreateBlip(pos);
                blip.Sprite = (BlipSprite) 475;
                blip.Name = "Апартаменты";
                blip.IsShortRange = true;
                blip.Scale = 0.4f; //86
            }
            
            for (int i = 0; i < IntList.Length / 3; i++)
            {
                var pos = new Vector3((float) IntList[i, 0], (float) IntList[i, 1], (float) IntList[i, 2]);
                Checkpoint.Create(pos, 1.4f, "apartment");
                Marker.Create(pos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            }
        }
        
        
        public static void UpdateInfo(int id, string userName, int userId)
        {
            TriggerServerEvent("ARP:UpdateApartmentInfo", userName, userId, id);
        }
        
        public static int GetBuildInRadiusOfPosition(Vector3 pos, float radius)
        {
            for (int i = 0; i < BuildList.Length / 7; i++)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3((float) BuildList[i, 0], (float) BuildList[i, 1], (float) BuildList[i, 2])) <= radius)
                    return i;
            }
            return -1;
        }
        
        public static void MenuEnter()
        {
            var bId = GetBuildInRadiusOfPosition(GetEntityCoords(GetPlayerPed(-1), true), 1.5f);
            if (bId == -1) return;
            MenuList.ShowApartamentTeleportMenu((int) BuildList[bId, 3], bId);
        }

        public static async void MenuExit()
        {
            var vw = User.GetPlayerVirtualWorld();
            if (vw >= 0) return;

            bool isFind = false;
            for (int i = 0; i < IntList.Length / 3; i++)
            {
                var pos = new Vector3((float) IntList[i, 0], (float) IntList[i, 1], (float) IntList[i, 2]);
                if (Main.GetDistanceToSquared(pos, GetEntityCoords(GetPlayerPed(-1), true)) > 2) continue;
                isFind = true;
            }
            for (int i = 0; i < House.HouseInts.Length / 3; i++)
            {
                var pos = new Vector3((float) House.HouseInts[i, 0], (float) House.HouseInts[i, 1], (float) House.HouseInts[i, 2]);
                if (Main.GetDistanceToSquared(pos, GetEntityCoords(GetPlayerPed(-1), true)) > 2) continue;
                isFind = true;
            }

            if (!isFind) return;
            CurrentData = await GetAllData(vw * -1);
            MenuList.ShowApartamentMenu(CurrentData);
        }
        
        public static async void Enter(int id)
        {/*
            CurrentData = await GetAllData(id);
            if (CurrentData.id == 0)
            {
                Notification.SendWithTime("~r~Ошибка загрузки квартиры #1");
                return;
            }
            
            int i = CurrentData.interior_id;
            User.Teleport(CurrentData.is_exterior
                ? new Vector3((float) IntList[i, 0], (float) IntList[i, 1], (float) IntList[i, 2])
                : new Vector3((float) House.HouseInts[i, 0], (float) House.HouseInts[i, 1],
                    (float) House.HouseInts[i, 2]));

            User.SetVirtualWorld(CurrentData.id * -1);*/
            
            CurrentData = await GetAllData(id);
            var userId = User.Data.id;
            if ((int) await Client.Sync.Data.Get(-100000 + CurrentData.id, "pin") > 0 && CurrentData.user_id != userId)
            {
                if (User.IsAdmin(4))
                    Notification.SendWithTime($"~g~Пароль: ~s~{(int) await Client.Sync.Data.Get(-100000 + CurrentData.id, "pin")}");
                
                int pass = Convert.ToInt32(await Menu.GetUserInput("Пароль", null, 5));
                if (pass == (int) await Client.Sync.Data.Get(-100000 + CurrentData.id, "pin"))
                {
                    int i = CurrentData.interior_id;
                    User.Teleport(CurrentData.is_exterior
                        ? new Vector3((float) IntList[i, 0], (float) IntList[i, 1], (float) IntList[i, 2])
                        : new Vector3((float) House.HouseInts[i, 0], (float) House.HouseInts[i, 1],
                            (float) House.HouseInts[i, 2]));

                    User.SetVirtualWorld(CurrentData.id * -1);
                }
                else
                {
                    Notification.SendWithTime("~r~Вы не верно ввели пароль");
                }
            }
            else
            {
                int i = CurrentData.interior_id;
                User.Teleport(CurrentData.is_exterior
                    ? new Vector3((float) IntList[i, 0], (float) IntList[i, 1], (float) IntList[i, 2])
                    : new Vector3((float) House.HouseInts[i, 0], (float) House.HouseInts[i, 1],
                        (float) House.HouseInts[i, 2]));

                User.SetVirtualWorld(CurrentData.id * -1);
            }
        }
        

        public static async void Exit()
        {
            CurrentData = await GetAllData(User.GetPlayerVirtualWorld() * -1);
            if (CurrentData.id == 0)
            {
                Notification.SendWithTime("~r~Ошибка загрузки квартиры #2");
                return;
            }
            int i = CurrentData.build_id;
            User.Teleport(new Vector3((float) BuildList[i, 0], (float) BuildList[i, 1], (float) BuildList[i, 2]));
            User.SetVirtualWorld(0);
        }

        public static async void Exit(Vector3 pos)
        {
            /*CurrentData = await GetAllData(User.GetPlayerVirtualWorld() * -1);
            if (CurrentData.id == 0)
            {
                Notification.SendWithTime("~r~Ошибка загрузки квартиры #2");
                return;
            }*/
            User.Teleport(pos);
            User.SetVirtualWorld(0);
        }
        
        public static async void Buy(int id)
        {
            await User.GetAllData();
            var hData = await GetAllData(id);
            var playerId = User.GetServerId();

            if (hData.id == 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_35"));
                return;
            }
            
            if (hData.user_id != 0)
            {
                Notification.SendWithTime("~r~Недвижимость уже куплена");
                return;
            }
            
            if (User.Data.apartment_id != 0)
            {
                Notification.SendWithTime("~r~У Вас уже есть недвижимость");
                return;
            }
            
            if (User.GetMoneyWithoutSync() < hData.price)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_2"));
                return;
            }
            
            Client.Sync.Data.Set(playerId, "apartment_id", hData.id);
            
            await Delay(200);

            if (await Client.Sync.Data.Get(playerId, "apartment_id") == 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_32"));
                return;
            }
            
            User.RemoveMoney(hData.price);
            Coffer.AddMoney(hData.price);

            User.Data.apartment_id = hData.id;
            
            Main.SaveLog("BuySellAprt", $"[BUY] {User.Data.id} {User.Data.rp_name} | {hData.price} | {User.Data.apartment_id}");
            
            Notification.SendWithTime("~g~Поздравляем с покупкой недвижимости");

            if (User.Data.reg_status != 3)
            {
                Client.Sync.Data.Set(playerId, "reg_time", 372);
                Client.Sync.Data.Set(playerId, "reg_status", 2);
            }
            
            UpdateInfo(hData.id, User.Data.rp_name, User.Data.id);

            User.SaveAccount();
            MenuList.HideMenu();
        }
        

        public static async void Sell(int id)
        {
            if (Screen.LoadingPrompt.IsActive)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_34"));
                return;
            }
            
            Client.Sync.Data.ShowSyncMessage = false;
            Screen.LoadingPrompt.Show("Обработка запроса, подождите");
            
            await User.GetAllData();
            var hData = await GetAllData(id);
            var playerId = User.GetServerId();

            if (hData.id == 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_35"));
                return;
            }
            
            if (User.Data.apartment_id == 0)
            {
                Notification.SendWithTime("~r~У Вас нет недвижимости");
                return;
            }
            
            Client.Sync.Data.Set(playerId, "apartment_id", 0);

            await Delay(200);

            if (await Client.Sync.Data.Get(playerId, "apartment_id") != 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_32"));
                return;
            }
            
            var nalog = hData.price * (100 - Coffer.GetNalog()) / 100;
            
            User.AddMoney(nalog);
            Coffer.RemoveMoney(nalog);
            
            Main.SaveLog("BuySellAprt", $"[SELL] {User.Data.id} {User.Data.rp_name} | {nalog} | {User.Data.apartment_id}");

            User.Data.apartment_id = 0;
            Notification.SendWithTime($"~g~{Coffer.GetNalog()}%\n~s~Получено: ~g~${nalog:#,#}" );

            if (User.Data.reg_status != 3)
            {
                Client.Sync.Data.Set(playerId, "reg_time", 28);
                Client.Sync.Data.Set(playerId, "reg_status", 1);
            }
            
            UpdateInfo(hData.id, "", 0);

            User.SaveAccount();
            MenuList.HideMenu();
            
            Screen.LoadingPrompt.Hide();
            Client.Sync.Data.ShowSyncMessage = true;
        }
    }
}

public class ApartmentData
{
    public int id {get;set;}
    public int user_id {get;set;}
    public string user_name {get;set;}
    public int price {get;set;}
    public int build_id {get;set;}
    public int floor {get;set;}
    public int interior_id {get;set;}
    public bool is_exterior {get;set;}
    public int pin {get;set;}
}