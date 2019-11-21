using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using CitizenFX.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Server
{
    public class Main : BaseScript
    {
        public static string ServerName = "MilkyWay";
        public static int MaxPlayers = 128;
        
        public static string[,] Ranks =
        {
            { "Секретарь", "Адвокат", "Агент S.A.S.S.", "Инспектор", "Начальник S.A.S.S.","Прокурор","Главный Инспектор", "Директор S.A.S.S.","Судья", "Заместитель Мэра", "Окружной Мэр", "Верховный Судья","Вице-Губернатор", "Губернатор" },
            { "Cadet PA", "Police Officier I", "Police Officier II", "Police Officier III", "Police Officier III+1", "Sergeant I", "Sergeant II", "Lieutenant I", "Lieutenant II", "Captain I", "Captain II", "Commander", "Assistant Chief of Police", "Chief of Police" },
            { "Стажер", "Дежурный", "Младший Агент", "Агент", "Глава отдела", "Инспектор", "Зам. директора FIB", "", "", "", "", "", "", "Директор FIB" },
            { "Рядовой-рекрут", "Рядовой 1 класса", "Младший капрал", "Капрал", "Сержант", "Первый Сержант", "Уорент-Офицер", "Второй Лейтенант", "Первый Лейтенант", "Капитан", "Майор", "Подполковник", "Полковник", "Генерал" },
            { "Грузчик", "Водитель-Стажёр", "региональный Водитель", "Федеральный Водитель", "Товаровед", "Управляющий складом", "Менеджер", "Начальник Отдела Перевозок", "Директор", "", "", "", "", "Управляющий" },
            { "Стажёр", "Редактор", "Журналист", "Ведущий", "Маркетолог", "Главный Редактор", "Заместитель Директора", "", "", "", "", "", "", "Генеральный Директор" },
            { "Deputy Sheriff Trainee","Deputy Sheriff I","Corporal","Sergeant","Lieutenant","Captain","Major","Assistant Sheriff","Undersheriff","","","","","Sheriff" },
            { "Ранг 1", "Ранг 2", "Ранг 3", "Ранг 4", "Ранг 5", "Ранг 6", "Ранг 7", "Ранг 8", "Ранг 9", "Ранг 10", "", "", "", "Лидер" },
            { "Ранг 1", "Ранг 2", "Ранг 3", "Ранг 4", "Ранг 5", "Ранг 6", "Ранг 7", "Ранг 8", "Ранг 9", "Ранг 10", "", "", "", "Лидер" },
            { "Ранг 1", "Ранг 2", "Ранг 3", "Ранг 4", "Ранг 5", "Ранг 6", "Ранг 7", "Ранг 8", "Ранг 9", "Ранг 10", "", "", "", "Лидер" },
            { "Ранг 1", "Ранг 2", "Ранг 3", "Ранг 4", "Ранг 5", "Ранг 6", "Ранг 7", "Ранг 8", "Ранг 9", "Ранг 10", "", "", "", "Лидер" },
            { "Ранг 1", "Ранг 2", "Ранг 3", "Ранг 4", "Ранг 5", "Ранг 6", "Ранг 7", "Ранг 8", "Ранг 9", "Ранг 10", "", "", "", "Лидер" },
            { "Ранг 1", "Ранг 2", "Ранг 3", "Ранг 4", "Ранг 5", "Ранг 6", "Ранг 7", "Ранг 8", "Ранг 9", "Ранг 10", "", "", "", "Лидер" },
            { "Ранг 1", "Ранг 2", "Ранг 3", "Ранг 4", "Ранг 5", "Ранг 6", "Ранг 7", "Ранг 8", "Ранг 9", "Ранг 10", "", "", "", "Лидер" },
            { "Ранг 1", "Ранг 2", "Ранг 3", "Ранг 4", "Ранг 5", "Ранг 6", "Ранг 7", "Ранг 8", "Ранг 9", "Ранг 10", "", "", "", "Лидер" },
             { "Стажер", "Интерн", "Младший специалист ", "Старший специалист", "Главный специалист", "Спасательный", "Начальник пожарного батальона", "Лётный офицер", "Врач", "Лидер эскадрильи", "Зам. главы департамента.", "Глава департамента", "Зам. дир. EMS", "Директор EMS" }
        };

        public static string[,] Fractions =
        {
            { "Правительство", "Правительство" },
            { "Департамент полиции Сан Андреас", "SAPD" },
            { "Федеральное бюро расследований", "FIB" },
            { "Армия USMC", "USMC" },
            { "Транспортная Компания Dewbauchee", "Dewbauchee" },
            { "Life Invader", "Invader" },
            { "Шериф Департамент","Sheriff" },
            { "Картель Los-Zetas", "Картель Los-Zetas" },
            { "Cosa Nostra", "Cosa Nostra" },
            { "Grove Street", "Grove Street" },
            { "Ballas", "Ballas" },
            { "Vagos", "Vagos" },
            { "Aztecas", "Aztecas" },
            { "Marabunta", "Marabunta" },
            { "Шериф Департамент", "Sheriff"},
            { "Служба спасения", "Emergency" }
        };
        
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
        

        public static dynamic[,] Vnfo =
        {
            { 1682114128 },
            { 884483972 },
            { 941494461 },
            { 777714999 },
            { 734217681 },
            { -1210451983 },
            { -48031959 },
            { -1590337689 },
            { 1254014755 },
            { 1897744184 },
            { -827162039 },
            { -312295511 },
            { -1860900134 },
            { -1924433270 },
            { 469291905 },
            { -748008636 },
            { 433954513 },
            { 1933662059 },
            { -2096818938 },
            { 1180875963 },
            { 1356124575 },
            { -1241712818 },
            { 159274291 },
            { 886810209 },
            { -1242608589 },
            { -1987130134 },
            { -233098306 },
            { 121658888 },
            { 444171386 },
            { 682434785 },
            { 1132262048 },
            { -616331036 },
            { -488123221 },
            { 1181327175 },
            { 837858166 },
            { 788747387 },
            { -50547061 },
            { 1394036463 },
            { 2025593404 },
            { 1949211328 },
            { -42959138 },
            { -82626025 },
            { -1600252419 },
            { 1543134283 },
            { 353883353 },
            { -2118308144 },
            { 408970549 },
            { 1824333165 },
            { -32878452 },
            { 368211810 },
            { 970385471 },
            { 1058115860 },
            { -1281684762 },
            { 165154707 },
            { -749299473 },
            { 1565978651 },
            { -1295027632 },
            { 1036591958 },
            { -1386191424 },
            { -975345305 },
            { -1214505995 },
            { -1700874274 },
            { 1981688531 },
            { 1043222410 },
            { 447548909 },
            { 1171614426 },
            { 1127131465 },
            { -1647941228 },
            { 1938952078 },
            { -2007026063 },
            { 2046537925 },
            { -1627000575 },
            { 1912215274 },
            { -1973172295 },
            { -1536924937 },
            { -1779120616 },
            { 456714581 },
            { -34623805 },
            { 741586030 },
            { -1205689942 },
            { -1693015116 },
            { -1683328900 },
            { 1922257928 },
            { 850991848 },
            { -1649536104 },
            { 177270108 },
            { 1747439474 },
            { -214455498 },
            { 1886712733 },
            { -1006919392 },
            { -2130482718 },
            { 1353720154 },
            { 444583674 },
            { -784816453 },
            { 475220373 },
            { -1705304628 },
            { 48339065 },
            { -947761570 },
            { 562680400 },
            { -823509173 },
            { 1074326203 },
            { 630371791 },
            { -212993243 },
            { -692292317 },
            { 321739290 },
            { -32236122 },
            { -1435527158 },
            { 782665360 },
            { 1489874736 },
            { -1881846085 },
            { 1283517198 },
            { -305727417 },
            { -713569950 },
            { -2072933068 },
            { -1098802077 },
            { -956048545 },
            { 1941029835 },
            { 1917016601 },
            { -1255698084 },
            { -1207431159 },
            { -1476447243 },
            { -1637149482 },
            { -399841706 },
            { 524108981 },
            { -960289747 },
            { -2140210194 },
            { 1019737494 },
            { 356391690 },
            { 390902130 },
            { 2078290630 },
            { 1784254509 },
            { 2091594960 },
            { -1352468814 },
            { -1770643266 },
            { -730904777 },
            { 1956216962 },
            { 2016027501 },
            { 712162987 },
            { -877478386 },
            { -1579533167 },
            { -2058878099 },
            { 1502869817 },
            { 1560980623 },
            { 1147287684 },
            { -769147461 },
            { -884690486 },
            { 1491375716 },
            { 1783355638 },
            { -845979911 },
            { -1700801569 },
            { -1323100960 },
            { -442313018 },
            { 1445631933 },
            { 516990260 },
            { 2132890591 },
            { 887537515 },
            { 1945374990 },
            { 1653666139 },
            { -1988428699 },
            { 2069146067 },
            { 2044532910 },
            { -307958377 },
            { 1692272545 },
            { 345756458 },
            { -638562243 },
            { 219613597 },
            { 540101442 },
            { -1106120762 },
            { -1478704292 },
            { 668439077 },
            { -1694081890 },
            { -2042350822 },
            { 2139203625 },
            { -1890996696 },
            { 2038858402 },
            { -801550069 },
            { 679453769 },
            { 1909700336 },
            { -27326686 },
            { -1812949672 },
            { -1374500452 },
            { -688189648 },
            { -1375060657 },
            { -1293924613 },
            { 1009171724 },
            { -1924800695 },
            { -1744505657 },
            { 444994115 },
            { 1637620610 },
            { -755532233 },
            { 628003514 },
            { 1537277726 },
            { 1239571361 },
            { 1721676810 },
            { 840387324 },
            { -715746948 },
            { -286046740 },
            { -1146969353 },
            { 1542143200 },
            { -579747861 },
            { -2061049099 },
            { 373261600 },
            { 1742022738 },
        };
        
        
        public Main()
        {
            Appi.MySql.ExecuteQuery("UPDATE users SET is_online='0'");
            Appi.MySql.ExecuteQuery("UPDATE monitoring SET online = '0', last_update='" + GetTimeStamp() + "' where id = '1'");
            
            /*for(int i = 0; i < Vnfo.Length; i++)
            {
                var vInf = new Vehicle.VehInfo();
                var vehInfo = vInf.Get((int) Vnfo[i, 0]);
                if (vehInfo.DisplayName == "Unk") continue;
                Appi.MySql.ExecuteQuery("INSERT INTO veh_info (display_name, class_name, hash, stock, stock_full, fuel_full, fuel_min) " +
                                        $"VALUES ('{vehInfo.DisplayName}', '{vehInfo.ClassName}', '{(int) Vnfo[i, 0]}', '{vehInfo.Stock}', '{vehInfo.StockFull}', '{vehInfo.FullFuel}', '{vehInfo.FuelMinute}')");
                Debug.WriteLine("DONE: " + (int) Vnfo[i, 0]);
            }
            for(int i = 0; i < BuildList.Length / 7; i++)
            {
                Appi.MySql.ExecuteQuery("INSERT INTO builds (x, y, z, floors) " +
                                        $"VALUES ('{(float) BuildList[i, 0]}', '{(float) BuildList[i, 1]}', '{(float) BuildList[i, 2]}', '{(float) BuildList[i, 3]}')");
                Debug.WriteLine("DONE: " + (int) Vnfo[i, 0]);
            }*/
            
            Managers.House.LoadAllHouse();
            Managers.Condo.LoadAllHouse();
            Managers.Stock.LoadAllStock();
            Business.LoadAll();
            FractionUnoff.LoadAll();
            Apartment.LoadAll();
            Coffer.LoadCoffer(1);
            CitizenFX.Core.Native.API.SetMapName("San Andreas");

            var rand = new Random();
            Sync.Data.Set(-9999, "sapdPass", rand.Next(1000, 9999));
        }
        
        public static IDictionary<String, Object> LoadJson(string file)
        {
            using (StreamReader r = new StreamReader(file))
            {
                return JsonConvert.DeserializeObject<IDictionary<String, Object>>(r.ReadToEnd());
            }
        }

        public static string RegEx(string text, string pattern = "A-Za-z")
        {
            var arr = Regex.Matches(text, @"\b[" + pattern + "]+\b")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            return arr.Aggregate("", (current, mat) => current + mat);
        }
        
        public static string RemoveQuotes(string text)
        {            
            return text.Replace("'", "");
        }

        public static string DeleteSqlHack(string text, string pattern = "\b[A-Za-z]+\b")
        {
            /*var arr = Regex.Matches(text, $@"{pattern}")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            return arr.Aggregate("", (current, mat) => current + mat);*/
            
            return text.Replace("'", "");
        }
        
        public static string GetFractionName(int fractionId)
        {
            return fractionId == 0 ? "нет" : Fractions[fractionId - 1, 1];
        }

        public static string GetRankName(int fractionId, int rank)
        {
            return rank == 0 ? "нет" : Ranks[fractionId - 1, rank - 1];
        }
        
        public static void UpdateDiscordStatus()
        {
            /*var plList = new PlayerList();
            if (ServerName == "Venus")
                TriggerClientEvent("ARP:UpdateDiscordStatus", plList.Count() + " players");
            else
                TriggerClientEvent("ARP:UpdateDiscordStatus", plList.Count() + " players");*/
        }
    
        public static dynamic FromJson(string json)
        {   
            return JObject.Parse(json);
        }

        public static string ToJson(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
        
        public static void SaveLog(string filename, string log)
        {
            try
            {
                File.AppendAllText($"log/{filename}.log", $"[{DateTime.Now:dd/MM/yyyy}] [{DateTime.Now:HH:mm:ss tt}] {log}\n");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"ServerLog TRY-CATCH {log} | {e}");
            }
        }

        public static int GetTimeStamp()
        {
            return (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp + 3600).ToLocalTime();
        }

        public static string UnixTimeStampToDateTimeShort(double unixTimeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp + 3600).ToLocalTime();
            return $"{dateTime:MM/dd} {dateTime:HH:mm}";
        }

        public static string Sha256(string randomString)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
            return crypto.Aggregate(hash, (current, theByte) => current + theByte.ToString("x2"));
        }
    }
}