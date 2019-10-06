using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Client.Managers;
using static CitizenFX.Core.Native.API;
 
namespace Client
{
    public class Main : BaseScript
    {
        public static bool IsSpawnTrain = false;
        
        public static double[,] FleccaBankMarkers =
        {
            { 148.5, -1039.971, 29.37775 },
            { 1175.054, 2706.404, 38.09407 },
            { -1212.83, -330.3573, 37.78702 },
            { -2962.951, 482.8024, 14.7031 },
            { -350.6871, -49.60739, 48.04258 },
            { 314.3541, -278.5519, 54.17077 }
        };
        
        public static double[,] CartelGetCarStockCpPos =
        {
            { 45.80671, 6301.016, 30.22984 },
            { 2878.875, 4488.51, 47.24571 },
            { 2464, 1589.337, 31.72032 }
        };
        
        public static double[,] CartelGetCarStockVehPos =
        {
            { 45.86936, 6301.11, 30.81545, 206.9094 },
            { 2878.271, 4487.945, 47.88404, 155.0173 },
            { 2464.935, 1589.133, 32.16377, 269.2127 }
        };
        
        public static double[,] CartelMiserkPos =
        {
            { 2405.403, 3127.287, 47.15343 },
            { 2153.789, 4795.798, 40.18645 },
            { 44.80258, 6301.774, 30.22701 },
            { 1653.104, 33.88398, 171.8806 },
            { 581.62, -2728.516, 5.056003 },
            { 581.62, -2728.516, 5.056003 }
        };

        public static double[,] FuelMarkers =
        {
            { -1436.907, -276.7434, 46.2077 },
            { 1181.085, -332.457, 69.17603 },
            { -2555.328, 2334.161, 33.07804 },
            { 820.5913, -1028.354, 26.28338 },
            { -2096.782, -321.0891, 13.16862 },
            { 266.3101, -1262.233, 29.14289 },
            { -1798.881, 803.0383, 138.6512 },
            { -319.7586, -1471.749, 30.54867 },
            { 620.6727, 268.8043, 103.0894 },
            { 1208.05, -1402.304, 35.22414 },
            { -724.5606, -935.9816, 19.21322 },
            { -71.04662, -1761.899, 29.65545 },
            { 261.9595, 2607.725, 44.90799 },
            { 1210.331, 2660.641, 37.80556 },
            { 2680.197, 3263.732, 55.24052 },
            { 2003.131, 3773.745, 32.40389 },
            { 1699.996, 6415.61, 32.67349 },
            { 183.0834, 6602.298, 31.84904 },
            { -94.24948, 6419.677, 31.48952 },
            { 1785.835, 3331.355, 41.36417 },
            { 2582.799, 361.9792, 108.4573 },
            { -525.8305, -1210.831, 18.18483 },
            { 175.9581, -1561.268, 29.25868 },
            { -976.5975, -2998.578, 12.94507 },
            { -799.4369, -1503.509, -1.474802 },
            { -722.5702, -1472.637, 4.000523 }
        };

        public static double[,] FuelStation =
        {
            { -1428.609, -269.087, 46.2077, 0, 0, 20, 25 }, //270 Meria Moorningwood //25
            { 1163.282, -323.9004, 69.20513, 1, 1, 20, 26 }, //520 Mirror //26
            { -2544.321, 2316.688, 33.2159, 0, 2, 20, 27 }, //150 Zancudo //27
            { 818.1421, -1040.465, 26.75079, 0, 3, 20, 28 }, //210 Штрафстоянка (Мара) La Mesa //28
            { -2073.842, -327.2829, 13.31597, 0, 4, 20, 29 }, //190 Пляж СантаМоника (АШ) Pacific Bluffs //29
            { 288.9222, -1266.991, 29.44076, 0, 5, 20, 30 }, //320 Гетто возле моста Strawberry //30
            { -1820.795, 792.2365, 138.121, 1, 6, 20, 31 }, //300 LCN Richman Glen //31
            { -342.1414, -1483.01, 30.70372, 0, 7, 20, 32 }, //260 Scrap La Puerta //32
            { 645.6617, 267.5023, 103.2332, 0, 8, 20, 33 }, //290 WW DT Vinewood //33
            { 1211.271, -1389.322, 35.37689, 0, 9, 15, 34 }, //200 Mara //El Burro //34
            { -707.4156, -914.7997, 19.21559, 1, 10, 20, 35 }, //320 Vagos //Little Seoul LTD //35
            { -48.44829, -1757.987, 29.42101, 1, 11, 20, 36 }, //415 Getto Grove //Davis Xero //36
            { 265.9477, 2598.808, 44.78529, 0, 12, 10, 37 }, //98 Harmony Шоссе 68 //37
            { 1200.653, 2655.875, 37.85188, 0, 13, 10, 38 }, //110 GrandSenoraDesert Шоссе 68 (Тюрьма рядом) //38
            { 2677.276, 3281.558, 55.24114, 1, 14, 10, 39 }, //180 Шоссе Senora Тюрьма  //39
            { 2001.553, 3779.738, 32.18078, 0, 15, 15, 40 }, //150 Sandy Shores //40
            { 1705.878, 6424.97, 32.76269, 0, 16, 15, 41 }, //120 Шоссе там где Чиллиад справа от Палето Mount Chiliad //40
            { 162.0032, 6636.448, 31.56107, 0, 17, 20, 42 }, //100 Палето с права //PaletoBay GlobeOIL 42
            { -92.99864, 6410.08, 31.64046, 0, 18, 10, 43 }, //110 Палето город Xero 43
            { 1776.583, 3327.637, 41.43328, 0, 19, 10, 44 }, //110 Аэропорт южнее Сенди шорс Airport 44
            { 2557.362, 382.5201, 108.6229, 1, 20, 20, 45 }, //230 Шоссе справа в жопе мира //Tataviam  45
            { -531.2673, -1220.685, 18.45499, 0, 21, 20, 46 }, //230 STO, Центр возле моста Scrap и пристани Liitle Seol Xero 46
            { 167.0998, -1553.519, 29.26175, 0, 22, 20, 47 }, //260 Возле EMS Гетто //Davis RON 47
            { -998.1706, -3031.017, 13.94507, 0, 23, 50, 81 }, //Air
            { -784.4645, -1506.364, 1.5952133, 0, 24, 20, 82 }, //Boat
            { -706.188, -1466.079, 5.042738, 0, 25, 20, 83 } //Heli
        };

        public static double[,] Shops =
        {
            { 26.213, -1345.442, 29.49702, 3 }, //210 Гетто Strawberry
            { -1223.059, -906.7239, 12.32635, 4 }, //190 Лодочная Vespucci Canals
            { -1487.533, -379.3019, 40.16339, 5 }, //250 Мерия Moorningwood
            { 1135.979, -982.2205, 46.4158, 6 }, //130 Mara / Mirror
            { 1699.741, 4924.002, 42.06367, 7 }, //80 LCN Airport Grapeseed
            { 374.3559, 327.7817, 103.5664, 8 }, //230 WW AdminKV DT WW
            { -3241.895, 1001.701, 12.83071, 9 }, //90 Север Banham Canyon North
            { -3039.184, 586.3903, 7.90893, 10 }, //110 Шоссе Banham Canyon Шоссе
            { -2968.295, 390.9566, 15.04331, 11 }, //90 Юг  Banham Canyon South
            { 547.8511, 2669.281, 42.1565, 50 }, //65 Harmony Шоссе68
            { 1165.314, 2709.109, 38.15772, 51 }, //65 Senora Шоссе68
            { 1960.845, 3741.882, 31.34375, 84 }, //Sandy
            { 1729.792, 6414.979, 34.03723, 85 } //Chidiad
        };

        public static double[,] BarberShops =
        {
            { 138.7087, -1705.711, 28.29162, 109 }, //1
            { 1214.091, -472.9952, 65.208, 109 }, //1
            { -276.4055, 6226.398, 30.69552, 109 }, //1
            { -1282.688, -1117.432, 5.990113, 110 }, //2
            { 1931.844, 3730.305, 31.84443, 111 }, //3
            { -33.34319, -154.1892, 56.07654, 48 }, //4
            { -813.5332, -183.2378, 36.5689, 112 }, //5 Bob Mulét
        };

        public static double[,] GunShops =
        {
            { 22.08832, -1106.986, 29.79703, 75 }, //Pillbox
            { 252.17, -50.08245, 69.94106, 76 }, //Хевик
            { 842.2239, -1033.294, 28.19486, 77 }, //LaMesa
            { -661.947, -935.6796, 21.82924, 78 }, //Seul
            { -1305.899, -394.5485, 36.69577, 79 }, //Morningwood
            { 809.9118, -2157.209, 28.61901, 102 }, //Saipres-Flets
            { 2567.651, 294.4759, 107.7349, 103 }, //Tataviamskoe
            { -3171.98, 1087.908, 19.83874, 104 }, //Chumash
            { -1117.679, 2698.744, 17.55415, 105 }, //Zancudo River
            { 1693.555, 3759.9, 33.70533, 106 }, //Sandy
            { -330.36, 6083.885, 30.45477, 107 }, //PaletoBay
        };

        public static double[,] Bars =
        {
            { 127.024, -1284.24, 28.28062, 49 }, //Strip
            { -560.0792, 287.0196, 81.17641, 52 }, //Tequila
            { -1394.226, -605.4658, 29.31955, 53 }, //Banana
            { 988.5745, -96.85889, 73.84525, 72 }, //Байкер
            { 1986.267, 3054.349, 46.21521, 73 }, //Елов джек
            { -443.0947, 271.2448, 82.016, 80 }, //Comedy
            { -2055.519, -1024.646, 10.90755, 91 }, //Казино
            { -1587.188, -3012.827, -77.00496, 122 }, //Galaxy
            { -1578.218, -3014.328, -80.00593, 122 } //Galaxy
        };

        public static double[,] Tattoos =
        {
            { 324.2816, 180.2105, 102.5865, 94 }, //T1
            { 1864.066, 3746.909, 32.03188, 95 }, //T2
            { -294.0927, 6200.76, 30.48712, 95 }, //T2
            { -1155.336, -1427.223, 3.954459, 96 }, //T3
            { 1321.756, -1653.431, 51.27526, 97 }, //T4
            { -3169.667, 1077.457, 19.82918, 98 }, //T5
        };

        public static double[,] RentMarkers =
        {            
            { -1012.002, -2682.319, 12.98185, 15 }, 
            { 56.84695, -1332.3, 28.31281, 16 }, 
            { 318.2011, 133.5345, 102.5149, 17 }, 
            { -1440.246, -615.3691, 29.8274, 18 }, 
            { -3239.305, 978.7662, 11.71953, 19 }, 
            { -264.9207, 6285.907, 30.47458, 20 }, 
            { 1681.711, 4849.298, 41.10908, 21 }, 
            { 1868.006, 3684.482, 32.73838, 22 }, 
            { 1932.747, 2624.953, 45.1698, 23 }, 
            { 297.4761, -602.786, 42.30347, 24 }, 
            { 1128.115, -504.1843, 63.19245, 148 }, 
            { -53.82885, -914.3015, 28.43705, 149 }, 
            { -873.4641, -811.7601, 18.29254, 150 }, 
            { -824.2698, -116.8545, 36.58223, 151 }, 
            { -1205.656, -1553.266, 3.373455, 152 }, 
            { 287.8639, 2594.688, 43.43363, 153 }
        };

        public static double[,] RentCarMarkers =
        {
            { -40.93587, -1081.907, 25.63692, 86, -47.22956, -1081.951, 26.34948 }, //Simon / Premium Delux Motorsport
            { 551.2614, -203.3638, 53.39468, 87, 548.0169, -207.9812, 53.52542}, //Vinewood / Rent Exotic
            { -789.7569, -1451.667, 0.5952171, 88, -791.1743, -1446.605, 0.3094352}, //Rent Boat
            { -1127.256, -2862.057, 12.9462, 89, -1145.945, -2864.172, 13.84849}, //Rent Heli
            { -704.2238, -1398.512, 4.495285, 90, -724.9426, -1443.191, 4.903039}, //Rent Heli Vespuchi
            { -1373.805, -3280.547, 12.94482, 93, -1379.157, -3240.558, 14.54807}, //Rent Air
            { 895.4368, -179.3315, 73.70035, 114, 895.4368, -179.3315, 73.70035}, //Rent Taxi
        };
        
        public static double[,] KitchenList =
        {
            { 349.8293, -932.1685, 45.36568, 100000 },
            { -1909.91, -575.0604, 18.09722, 100000 },
            { -1282.89, 446.3326, 96.89471, 100000 },
            { -1153.151, -1521.806, 9.642298, 100000 },
            { -897.9225, -441.7401, 93.05853, 100000 },
            {  -852.9263, 688.3412, 151.8529, 100000 },
            { -782.1923, 330.5791, 186.3132, 100000 },
            { -758.5352, 610.5003, 143.1406, 100000 },
            { -674.4797, 595.6437, 144.3797, 100000 },
            { -618.301, 42.6407, 96.60004, 100000},
            { -566.1242, 656.9935, 144.832, 100000 },
            { -111.3519, -6.549356, 69.51958, 100000 },
            { -10.52031, -1428.414, 30.10148, 100000 },
            { 124.1809, 557.1547, 183.2971, 100000 },
            { 265.3231, -995.9853, -100.0086, 100000 },
            { 343.0825, 429.4016, 148.3808, 100000 },
            { 343.7529, -1002.998, -100.1962, 100000 },
            { 379.4201, 418.8586, 144.9001, 100000 },
            { 1395.23, 1145.007, 113.3336, 100000 },
            { 1975.355, 3818.645, 32.43632, 100000 }, //19
            { -9.682148, 520.001, 173.628, 30000 }, //20
            { -797.776, 187.5312, 71.60544, 30000 }, //21
            { -1440.387, 6759.043, 7.98046, 30000 }, //22
            { -769.8837, 340.0439, 210.397, 100000 },
            { -787.7949, 330.3934, 157.599, 100000 },
            { -782.215, 329.8719, 216.0382, 100000 },
            { -778.6613, 327.9729, 195.086, 100000 },
            { -1459.873, -534.1923, 54.52639, 100000 },
            { -1473.994, -537.3419, 72.44417, 100000 },
            { -31.02146, -587.9935, 87.71225, 100000 },
            { -11.44843, -584.9959, 78.43073, 100000 },
            { -896.725, -446.5864, 124.1319, 100000 },
            { -912.0232, -371.7621, 83.07791, 100000 },
            { -917.9376, -379.2882, 107.0377, 100000 },
            { -918.7728, -386.1582, 112.6746, 100000 },
            { -468.6447, -695.6874, 74.68432, 100000 },
            { 120.4625, -884.7316, 123.2703, 100000 },
            { -674.1874, -858.9833, 40.64307, 100000 },
            { -440.8985, 6274.877, 10.75166, 100000 },
            { -57.75088, -620.3068, 75.99939, 100000 },
            { 930.5132, -548.4226, 42.63166, 100000 },
        };
        
        public static string[,] Ranks =
        {
            { "Охранник", "Начальник охраны", "Секретарь", "Адвокат", "Помощник инспектора", "Инспектор", "Заместитель Мэра", "Мэр", "Вице-губернатор", "", "", "", "", "Губернатор" },
            { "Cadet PA", "Police Officier I", "Police Officier II", "Police Officier III", "Police Officier III+1", "Sergeant I", "Sergeant II", "Lieutenant I", "Lieutenant II", "Captain I", "Captain II", "Commander", "Assistant Chief of Police", "Chief of Police" },
            { "Стажер", "Дежурный", "Младший Агент", "Агент", "Глава отдела", "Инспектор", "Зам. директора FIB", "", "", "", "", "", "", "Директор FIB" },
            { "Рядовой-рекрут", "Рядовой 1 класса", "Младший капрал", "Капрал", "Сержант", "Первый Сержант", "Уорент-Офицер", "Второй Лейтенант", "Первый Лейтенант", "Капитан", "Майор", "Подполковник", "Полковник", "Генерал" },
            { "Стажёр", "Консультант", "Экзаменатор", "Инструктор", "Старший Инструктор", "Координатор", "Менеджер", "Старший Менеджер", "", "", "", "", "", "Управляющий" },
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
            { "Автошкола", "Автошкола" },
            { "Life Invader", "Invader" },
            { "Шериф Департамент","Sheriff" },
            { "Картель San Andreas", "Картель San Andreas" },
            { "Cosa Nostra", "Cosa Nostra" },
            { "Grove Street", "Grove Street" },
            { "Ballas", "Ballas" },
            { "Vagos", "Vagos" },
            { "Aztecas", "Aztecas" },
            { "Marabunta", "Marabunta" },
            { "", "" },
            { "Служба спасения", "Emergency" }
        };
        
        public static string[,] ClipsetMale =
        {
            { "Стандартная", "" }, 
            { "Сидя", "move_ped_crouched" }, 
            { "Уставшая", "ANIM_GROUP_MOVE_LEMAR_ALLEY" }, 
            { "С кулаком", "clipset@move@trash_fast_turn" }, 
            { "С предметом в руке", "missfbi4prepp1_garbageman" }, 
            { "С бодуна", "move_characters@franklin@fire" }, 
            { "Неспешная", "move_characters@Jimmy@slow@" }, 
            { "Напряжная", "move_characters@michael@fire" }, 
            { "Дамская", "FEMALE_FAST_RUNNER" }, 
            { "Дамская v.2", "move_f@flee@a" }, 
            { "Нервная", "move_f@scared" }, 
            { "Сексуальная", "move_f@sexy@a" }, 
            { "Похрамывать", "move_heist_lester" }, 
            { "Слегка похрамывать", "move_injured_generic" }, 
            { "Похрамывать с тростью", "move_lester_CaneUp" }, 
            { "Вальяжно", "move_m@bag" }, 
            { "Нахальная", "move_m@brave" }, 
            { "Нахальная (медленная)", "move_m@casual@d" }, 
            { "Упоротый", "MOVE_M@BAIL_BOND_NOT_TAZERED" }, 
            { "Сильно упоротый", "MOVE_M@BAIL_BOND_TAZERED" }, 
            //{ "Опьянение", "MOVE_M@DRUNK@SLIGHTLYDRUNK" }, 
            //{ "Сильное опьянение", "MOVE_M@DRUNK@VERYDRUNK" }, 
            { "С просони", "move_m@fire" }, 
            { "Гангстерская", "move_m@gangster@var_e" }, 
            { "Сонная", "move_m@gangster@var_f" }, 
            { "Бандитская", "move_m@gangster@var_i" }, 
            { "Расслабленная", "move_m@JOG@" }, 
            { "Деловая", "MOVE_P_M_ONE" }, 
            { "Злая", "move_p_m_zero_janitor" }, 
            { "Вальяжная (медленная)", "move_p_m_zero_slow" }, 
            { "Стильная", "MOVE_M@FEMME@" }, 
            { "Мафиозная", "MOVE_M@GANGSTER@NG" }, 
            { "Шикарная", "MOVE_M@POSH@" }, 
            { "Крутая", "MOVE_M@TOUGH_GUY@" }
        };
        
        public static string[,] ClipsetFemale =
        {
            { "Стандартная", "" }, 
            { "Сидя", "move_ped_crouched" }, 
            { "Уставшая", "ANIM_GROUP_MOVE_LEMAR_ALLEY" }, 
            { "С кулаком", "clipset@move@trash_fast_turn" }, 
            { "С предметом в руке", "missfbi4prepp1_garbageman" }, 
            { "С бодуна", "move_characters@franklin@fire" }, 
            { "Неспешная", "move_characters@Jimmy@slow@" }, 
            { "Напряжная", "move_characters@michael@fire" }, 
            { "Дамская", "FEMALE_FAST_RUNNER" }, 
            { "Дамская v.2", "move_f@flee@a" }, 
            { "Нервная", "move_f@scared" }, 
            { "Сексуальная", "move_f@sexy@a" }, 
            { "Похрамывать", "move_heist_lester" }, 
            { "Слегка похрамывать", "move_injured_generic" }, 
            { "Похрамывать с тростью", "move_lester_CaneUp" }, 
            { "Вальяжно", "move_m@bag" }, 
            { "Нахальная", "move_m@brave" }, 
            { "Нахальная (медленная)", "move_m@casual@d" }, 
            { "Упоротый", "MOVE_M@BAIL_BOND_NOT_TAZERED" }, 
            { "Сильно упоротый", "MOVE_M@BAIL_BOND_TAZERED" }, 
            //{ "Опьянение", "MOVE_M@DRUNK@SLIGHTLYDRUNK" }, 
            //{ "Сильное опьянение", "MOVE_M@DRUNK@VERYDRUNK" }, 
            { "С просони", "move_m@fire" }, 
            { "Гангстерская", "move_m@gangster@var_e" }, 
            { "Сонная", "move_m@gangster@var_f" }, 
            { "Бандитская", "move_m@gangster@var_i" }, 
            { "Расслабленная", "move_m@JOG@" }, 
            { "Деловая", "MOVE_P_M_ONE" }, 
            { "Злая", "move_p_m_zero_janitor" }, 
            { "Вальяжная (медленная)", "move_p_m_zero_slow" }, 
            { "Стильная", "MOVE_F@FEMME@" }, 
            { "Мафиозная", "MOVE_F@GANGSTER@NG" }, 
            { "Шикарная", "MOVE_F@POSH@" }, 
            { "Крутая", "MOVE_F@TOUGH_GUY@" }
        };

        private static List<CitizenFX.Core.Ped> _pedListCahce = new List<CitizenFX.Core.Ped>();
        private static List<CitizenFX.Core.Vehicle> _vehicleListCahce = new List<CitizenFX.Core.Vehicle>();
        private static List<CitizenFX.Core.Prop> _objListCahce = new List<CitizenFX.Core.Prop>();

        protected static int VehicleFind;
        protected static int PedFind;
        protected static int ObjFind;
        
        public static int MaxPlayers = 128;
        public static string ServerName = "MilkyWay";
        public static string LastName = "";
        
        public Main()
        {            
            Sync.Data.ShowSyncMessage = true;
            
            NetworkSetFriendlyFireOption(true);
            SetCanAttackFriendly(GetPlayerPed(-1), true, true);
            NetworkSetTalkerProximity(5f);
            
            LoadIpls();
            LoadBlips();
            Managers.Ped.LoadAllPeds();
            Managers.Apartment.LoadAll();
            Business.Gun.LoadAll();
            Business.Shop.LoadAll();
            Business.Fuel.LoadAll();
            Business.Rent.LoadAll();
            Business.Gum.LoadAll();
            Business.Park.LoadAll();
            Business.CarWash.LoadAll();
            Business.Bank.LoadAll();
            Business.Bar.LoadAll();
            Business.Tattoo.LoadAll();
            Business.Casino.LoadAll();
            Business.Cloth.LoadAll();
            Business.BarberShop.LoadAll();
            Business.Custom.CreatePickups();
            Business.CarNumber.CreatePickups();
            
            TriggerServerEvent("ARP:PlayerFinishLoad");
   
            /*#pragma warning disable 4014
            Spawn.SpawnPlayer("S_M_Y_Cop_01", -10.10947f, 508.5056f, 174.3281f, 180f, false, false, false);
            #pragma warning restore 4014
            
            User.Invisible(PlayerId(), true);
            MenuList.ShowAuthMenu();
            */
            
            User.SetDrugAmfLevel(0);
            User.SetDrugCocaLevel(0);
            User.SetDrugDmtLevel(0);
            User.SetDrugKsanLevel(0);
            User.SetDrugLsdLevel(0);
            User.SetDrugMargLevel(0);
            User.SetDrugMefLevel(0);
            User.SetDrunkLevel(0);
                       
            SetRichPresence("32 players");
            
            LoadInterior(GetInteriorAtCoords(440.84f, -983.14f, 30.69f));
            
            EventHandlers.Add("ARP:FromJsonServer", new Action<dynamic>(FromJsonServer));
            EventHandlers.Add("ARP:ToJsonServer", new Action<string>(ToJsonServer));
            EventHandlers.Add("ARP:UpdateDiscordStatus", new Action<string>(UpdateDiscordStatus));
        }
        
        public static void FinishLoad()
        {
            #pragma warning disable 4014
            Spawn.SpawnPlayer("S_M_Y_Cop_01", -10.10947f, 508.5056f, 174.3281f, 180f, false, false, false);
            #pragma warning restore 4014
            
            User.Invisible(PlayerId(), true);
            MenuList.ShowAuthMenu(LastName);
        }

        public static string RegEx(string text, string pattern = "\b[A-Za-z]+\b")
        {
            /*var arr = Regex.Matches(text, $@"{pattern}")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            return arr.Aggregate("", (current, mat) => current + mat);*/
            
            //return text.Replace("'", "").Replace(" ", ""); TODO доделать баг с авторизацией
            return text.Replace("'", "");
        }

        public static string RemoveQuotes(string text)
        {            
            return text.Replace("'", "");
        }

        public static string RemoveQuotesAndSpace(string text)
        {            
            return text.Replace("'", "").Replace(" ", "");
        }

        public static void UpdateDiscordStatus(string text)
        {
            SetRichPresence(text);
            SetDiscordAppId("501371306437246976");
            SetDiscordRichPresenceAsset("gtav2");
        }

        public static async Task<bool> LoadModel(uint model, int waitMs = 1000)
        {
            RequestModel(model);
            while (!HasModelLoaded(model) && waitMs > 0)
            {
                RequestModel(model);
                waitMs--;
                await Delay(1);
            }
            SetModelAsNoLongerNeeded(model);
            return HasModelLoaded(model);
        }

        public static void AddFractionVehicleLog(string name, string text, int fractionId)
        {
            SaveLog("DoFraction", $"[VEH] {name} ({fractionId}) | {text}");
            TriggerServerEvent("ARP:AddFractionVehicleLog", name, text, fractionId);
        }

        public static void AddFractionGunLog(string name, string text, int fractionId)
        {
            SaveLog("DoFraction", $"[GUN] {name} ({fractionId}) | {text}");
            TriggerServerEvent("ARP:AddFractionGunLog", name, text, fractionId);
        }

        public static void AddStockLog(string name, string text, int stockId)
        {
            TriggerServerEvent("ARP:AddStockLog", name, text, stockId);
        }
        
        public static int GetKitchenId(bool withNotif = false)
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            
            for (int i = 0; i < KitchenList.Length / 4; i++)
            {
                Vector3 shopPos = new Vector3((float) KitchenList[i, 0], (float) KitchenList[i, 1], (float) KitchenList[i, 2]);
                if (GetDistanceToSquared(playerPos, shopPos) > 1.2f) continue;

                int playerVw = User.GetPlayerVirtualWorld();
                if (!User.IsAdmin())
                {
                    if (playerVw < 0 && playerVw * -1 != User.Data.apartment_id)
                    {
                        if (withNotif)
                            Managers.Notification.SendWithTime("~r~Вы должны быть в своих апартаментах");
                        return 0;
                    }
                    if (playerVw > 0 && playerVw != User.Data.id_house && playerVw + 10000 != User.Data.condo_id)
                    {
                        if (withNotif)
                            Managers.Notification.SendWithTime("~r~Вы должны быть в своём доме");
                        return 0;
                    }
                }

                int kitchenId = User.GetPlayerVirtualWorld();

                if (playerVw == 0)
                {
                    switch (i)
                    {
                        case 19:
                            kitchenId = User.Data.id_house == 1 ? 1 : 0;
                            break;
                        case 20:
                            kitchenId = User.Data.id_house == 370 ? 370 : 0;
                            break;
                        case 21:
                            kitchenId = User.Data.id_house == 745 ? 745 : 0;
                            break;
                        case 22:
                            kitchenId = User.Data.id_house == 1139 ? 1139 : 0;
                            break;
                    }
                }
                //if (kitchenId > 10000)
                //    return kitchenId - 10000;
                if (kitchenId != 0) return kitchenId;
                if (withNotif)
                    Managers.Notification.SendWithTime("~r~Произошла неизвестная ошибка");
                return 0;
            }
            return 0;
        }
        
        public static int GetKitchenAmount()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            for (int i = 0; i < KitchenList.Length / 4; i++)
            {
                Vector3 shopPos = new Vector3((float) KitchenList[i, 0], (float) KitchenList[i, 1], (float) KitchenList[i, 2]);
                if (GetDistanceToSquared(playerPos, shopPos) > 1.2f) continue;
                return Convert.ToInt32(KitchenList[i, 3]);
            }
            return 20000;
        }
        
        public static int GetTimeStamp()
        {
            return (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp).ToLocalTime();
        }
        
        public static bool GetScreenEffectIsActive(string effectName)
        {
            return CitizenFX.Core.Native.Function.Call<bool>(CitizenFX.Core.Native.Hash._GET_SCREEN_EFFECT_IS_ACTIVE, effectName);
        }

        public static void UpdatePedListCache()
        {
            _pedListCahce.Clear();
            var handle = FindFirstPed(ref PedFind);
            do
            {
                _pedListCahce.Add(new CitizenFX.Core.Ped(PedFind));
            } while (FindNextPed(handle, ref PedFind));
            
            EndFindPed(handle);
        }
        
        public static void UpdateVehicleListCache()
        {
            _vehicleListCahce.Clear();
            var handle = FindFirstVehicle(ref VehicleFind);
            do
            {
                _vehicleListCahce.Add(new CitizenFX.Core.Vehicle(VehicleFind));
            } while (FindNextVehicle(handle, ref VehicleFind));
        
            EndFindVehicle(handle);
        }
        
        public static List<CitizenFX.Core.Entity> GetObjListOnRadius(float radius)
        {
            return GetObjListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), radius);
        }
        
        public static List<CitizenFX.Core.Entity> GetObjListOnRadius(Vector3 pos, float radius)
        {
            var list = new List<CitizenFX.Core.Entity>();
            var handle = FindFirstObject(ref ObjFind);
        
            do
            {
                var entity = new MyEntity(ObjFind);
                if (GetDistanceToSquared(pos, entity.Position) < radius)
                    list.Add(entity);
        
            } while (FindNextObject(handle, ref ObjFind));
        
            EndFindObject(handle);
            return list;
        }
        
        public static List<CitizenFX.Core.Vehicle> GetVehicleListOnRadius(float radius = 9999f)
        {
            return GetVehicleListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), radius);
        }
        
        public static List<CitizenFX.Core.Vehicle> GetVehicleListOnRadius(Vector3 pos, float radius = 9999f)
        {
            return _vehicleListCahce.Where(v => GetDistanceToSquared(pos, v.Position) < radius).ToList();
        }
        
        public static CitizenFX.Core.Vehicle FindNearestVehicle()
        {
            return FindNearestVehicle(GetEntityCoords(GetPlayerPed(-1), true));
        }
        
        public static CitizenFX.Core.Vehicle FindNearestVehicle(Vector3 pos, float radius = 6f)
        {
            CitizenFX.Core.Vehicle veh = null;
            var vPosPrew = new Vector3(0, 0, 0);
            
            foreach (CitizenFX.Core.Vehicle v in GetVehicleListOnRadius(pos, radius))
            {
                if (!(GetDistanceToSquared(v.Position, pos) < GetDistanceToSquared(vPosPrew, pos))) continue;
                vPosPrew = v.Position;
                veh = v;
            }
            return veh;
        }
        
        public static CitizenFX.Core.Ped FindNearestPed()
        {
            return FindNearestPed(GetEntityCoords(GetPlayerPed(-1), true));
        }
        
        public static CitizenFX.Core.Ped FindNearestPed(Vector3 pos, float radius = 3f)
        {
            CitizenFX.Core.Ped ped = null;
            var pPosPrew = new Vector3(0, 0, 0);
            
            foreach (CitizenFX.Core.Ped p in GetPedListOnRadius(pos, radius))
            {
                if (!(GetDistanceToSquared(p.Position, pos) < GetDistanceToSquared(pPosPrew, pos))) continue;
                pPosPrew = p.Position;
                ped = p;
            }
            return ped;
        }
        
        public static List<CitizenFX.Core.Ped> GetPedListOnRadius(float radius = 9999f)
        {
            return GetPedListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), radius);
        }
        
        public static List<CitizenFX.Core.Ped> GetPedListOnRadius(Vector3 pos, float radius = 9999f)
        {
            return _pedListCahce.Where(p => !IsPedAPlayer(p.Handle) && GetDistanceToSquared(pos, p.Position) < radius).ToList();
        }
        
        public static CitizenFX.Core.Player GetPlayerOnRadius(Vector3 pos, float radius)
        {
            return new PlayerList().Where(player => player.ServerId != User.GetServerId()).FirstOrDefault(player => GetDistanceToSquared(pos, GetEntityCoords(GetPlayerPed(player.Handle), true)) < radius);
        }
        
        public static List<CitizenFX.Core.Player> GetPlayerListOnRadius(float radius)
        {
            return new PlayerList().Where(player => GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), GetEntityCoords(GetPlayerPed(player.Handle), true)) < radius).ToList();
        }
        
        public static List<CitizenFX.Core.Player> GetPlayerListOnRadius(Vector3 pos, float radius)
        {
            return new PlayerList().Where(player => GetDistanceToSquared(pos, GetEntityCoords(GetPlayerPed(player.Handle), true)) < radius).ToList();
        }
        
        public static List<CitizenFX.Core.Player> GetPlayerListOnVehicle()
        {
            var pos = GetEntityCoords(GetPlayerPed(-1), true);
            var list = new List<Player>();
            if (!IsPedInAnyVehicle(PlayerPedId(), true)) return list;
            var veh = GetVehiclePedIsUsing(PlayerPedId());
            if (GetPedInVehicleSeat(veh, -1) != PlayerPedId()) return list;
            string number = Managers.Vehicle.GetVehicleNumber(veh);
            list.AddRange(from player in new PlayerList() where IsPedInAnyVehicle(GetPlayerPed(player.Handle), true) let vehPed = GetVehiclePedIsUsing(GetPlayerPed(player.Handle)) where GetDistanceToSquared(pos, GetEntityCoords(GetPlayerPed(player.Handle), true)) < 30f && Managers.Vehicle.GetVehicleNumber(vehPed) == number select player);
            return list;
        }

        public static void SaveLog(string fileName, string text)
        {
            TriggerServerEvent("ARP:SendLog", fileName, text);
        }

        public static void LoadBlips()
        {
            var blip = World.CreateBlip(new Vector3(437.5687f, -982.9395f, 30.69f));
            blip.Sprite = (BlipSprite) 60;
            blip.Name = "Los Santos Police Department";
            blip.IsShortRange = true;
            blip.Scale = 0.8f; //86
            
            blip = World.CreateBlip(new Vector3(-439.1755f, 6010.428f, 26.98567f));
            blip.Sprite = (BlipSprite) 570;
            blip.Color = (BlipColor) 71;
            blip.Name = "Sheriff's Department";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            blip = World.CreateBlip(new Vector3(-138.8656f, -634.0953f, 168.8204f));
            blip.Sprite = (BlipSprite) 535;
            blip.Color = (BlipColor) 67;
            blip.Name = "Arcadius - Бизнес Центр";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            blip = World.CreateBlip(new Vector3(-66.66476f, -802.0474f, 44.22729f));
            blip.Sprite = (BlipSprite) 475;
            blip.Color = (BlipColor) 59;
            blip.Name = "Государственный банк \"Maze\"";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            blip = World.CreateBlip(new Vector3(111.5687f, -749.9395f, 30.69f));
            blip.Sprite = (BlipSprite) 498;
            blip.Name = "Офис FIB";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            blip = World.CreateBlip(new Vector3(1830.489f, 2603.093f, 45.8891f));
            blip.Sprite = (BlipSprite) 238;
            blip.Name = "Федеральная тюрьма";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            blip = World.CreateBlip(new Vector3(-1379.659f, -499.748f, 33.15739f));
            blip.Sprite = (BlipSprite) 419;
            blip.Name = "Здание правительства";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            blip = World.CreateBlip(Managers.Pickup.AptekaPos);
            blip.Sprite = (BlipSprite) 153;
            blip.Color = (BlipColor) 69;
            blip.Name = "Аптека";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            blip = World.CreateBlip(Managers.Pickup.Apteka1Pos);
            blip.Sprite = (BlipSprite) 153;
            blip.Color = (BlipColor) 69;
            blip.Name = "Аптека";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            blip = World.CreateBlip(Managers.Pickup.Apteka2Pos);
            blip.Sprite = (BlipSprite) 153;
            blip.Color = (BlipColor) 69;
            blip.Name = "Аптека";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            blip = World.CreateBlip(Managers.Pickup.Apteka3Pos);
            blip.Sprite = (BlipSprite) 153;
            blip.Color = (BlipColor) 69;
            blip.Name = "Аптека";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            blip = World.CreateBlip(new Vector3(299.3565f, -584.7751f, 42.26088f));
            blip.Sprite = (BlipSprite) 489;
            blip.Color = (BlipColor) 59;
            blip.Name = "Здание больницы LS";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            blip = World.CreateBlip(new Vector3(210.0973f, -1649.418f, 29.8032f));
            blip.Sprite = (BlipSprite) 436;
            blip.Color = (BlipColor) 60;
            blip.Name = "Здание Fire Departament";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            blip = World.CreateBlip(new Vector3(-1581.689f, -557.913f, 34.95288f));
            blip.Sprite = (BlipSprite) 545;
            blip.Name = "Здание автошколы";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            blip = World.CreateBlip(new Vector3(-1337.255f, -1277.948f, 3.872962f));
            blip.Sprite = (BlipSprite) 362;
            blip.Name = "Магазин масок";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            blip = World.CreateBlip(Managers.Pickup.LifeInvaderShopPos);
            blip.Sprite = (BlipSprite) 77;
            //blip.Color = (BlipColor) 79;
            blip.Name = "Магазин Life Invader";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            blip = World.CreateBlip(Managers.Pickup.AutoRepairsPosCarPos);
            blip.Sprite = (BlipSprite) 402;
            blip.Name = "Auto Repairs";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            //TODO Добавить новых блипов, Позже. (Банки, и прочее)
            
            blip = World.CreateBlip(new Vector3(235.5093f, 216.8752f, 106.2867f));
            blip.Sprite = (BlipSprite) 374;
            blip.Color = (BlipColor) 65;
            blip.Name = "Частный банк \"Pacific Standard\"";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            //Flecca
            for (int i = 0; i < FleccaBankMarkers.Length / 3; i++)
            {
                blip = World.CreateBlip(new Vector3((float) FleccaBankMarkers[i, 0], (float) FleccaBankMarkers[i, 1], (float) FleccaBankMarkers[i, 2]));
                blip.Sprite = (BlipSprite) 374;
                blip.Color = (BlipColor) 69;
                blip.Name = "Частный банк \"Fleeca\"";
                blip.IsShortRange = true;
                blip.Scale = 0.8f;
            }
            
            //LSC
            for (int i = 0; i < Business.Custom.LscPickupPos.Length / 4; i++)
            {
                blip = World.CreateBlip(new Vector3((float) Business.Custom.LscPickupPos[i, 0], (float) Business.Custom.LscPickupPos[i, 1], (float) Business.Custom.LscPickupPos[i, 2]));
                blip.Sprite = (BlipSprite) 446;
                blip.Name = "Автомастерская";
                blip.IsShortRange = true;
                blip.Scale = 0.8f;
            }
            
            blip = World.CreateBlip(new Vector3(-110.9777f, 6470.198f, 31.62671f));
            blip.Sprite = (BlipSprite) 374;
            blip.Color = (BlipColor) 67;
            blip.Name = "Частный банк \"Blaine County\"";
            blip.IsShortRange = true;
            blip.Scale = 0.8f;
            
            blip = World.CreateBlip(Managers.Pickup.BgstarKeyPos);
            blip.Sprite = (BlipSprite) 50;
            blip.Color = (BlipColor) 59;
            blip.Name = "Гараж рабочего транспорта";
            blip.IsShortRange = true;
            blip.Scale = 0.4f;
            
            blip = World.CreateBlip(Managers.Pickup.ConnorKeyPos);
            blip.Sprite = (BlipSprite) 50;
            blip.Color = (BlipColor) 59;
            blip.Name = "Гараж рабочего транспорта";
            blip.IsShortRange = true;
            blip.Scale = 0.4f;
            
            blip = World.CreateBlip(Managers.Pickup.PhotoKeyPos);
            blip.Sprite = (BlipSprite) 225;
            blip.Color = (BlipColor) 59;
            blip.Name = "Гараж рабочего транспорта";
            blip.IsShortRange = true;
            blip.Scale = 0.4f;
            
            blip = World.CreateBlip(Managers.Pickup.LabKeyPos);
            blip.Sprite = (BlipSprite) 50;
            blip.Color = (BlipColor) 59;
            blip.Name = "Гараж рабочего транспорта";
            blip.IsShortRange = true;
            blip.Scale = 0.4f;
            
            blip = World.CreateBlip(Managers.Pickup.ScrapKeyPos);
            blip.Sprite = (BlipSprite) 50;
            blip.Color = (BlipColor) 59;
            blip.Name = "Гараж рабочего транспорта";
            blip.IsShortRange = true;
            blip.Scale = 0.4f;
            
            blip = World.CreateBlip(Managers.Pickup.SunbKeyPos);
            blip.Sprite = (BlipSprite) 50;
            blip.Color = (BlipColor) 59;
            blip.Name = "Гараж рабочего транспорта";
            blip.IsShortRange = true;
            blip.Scale = 0.4f;
            
            blip = World.CreateBlip(Managers.Pickup.WapKeyPos);
            blip.Sprite = (BlipSprite) 50;
            blip.Color = (BlipColor) 59;
            blip.Name = "Гараж рабочего транспорта";
            blip.IsShortRange = true;
            blip.Scale = 0.4f;
            
            blip = World.CreateBlip(Managers.Pickup.TrashKeyPos);
            blip.Sprite = (BlipSprite) 50;
            blip.Color = (BlipColor) 59;
            blip.Name = "Гараж рабочего транспорта";
            blip.IsShortRange = true;
            blip.Scale = 0.4f;
            
            blip = World.CreateBlip(Managers.Pickup.Bus1KeyPos);
            blip.Sprite = (BlipSprite) 50;
            blip.Color = (BlipColor) 59;
            blip.Name = "Гараж рабочего транспорта";
            blip.IsShortRange = true;
            blip.Scale = 0.4f;
            
            blip = World.CreateBlip(Managers.Pickup.Bus2KeyPos);
            blip.Sprite = (BlipSprite) 50;
            blip.Color = (BlipColor) 59;
            blip.Name = "Гараж рабочего транспорта";
            blip.IsShortRange = true;
            blip.Scale = 0.4f;
            
            blip = World.CreateBlip(Managers.Pickup.Mail1KeyPos);
            blip.Sprite = (BlipSprite) 50;
            blip.Color = (BlipColor) 59;
            blip.Name = "Гараж рабочего транспорта";
            blip.IsShortRange = true;
            blip.Scale = 0.4f;
            
            blip = World.CreateBlip(Managers.Pickup.Mail2KeyPos);
            blip.Sprite = (BlipSprite) 50;
            blip.Color = (BlipColor) 59;
            blip.Name = "Гараж рабочего транспорта";
            blip.IsShortRange = true;
            blip.Scale = 0.4f;
            
            blip = World.CreateBlip(Managers.Pickup.GroupSixKeyPos);
            blip.Sprite = (BlipSprite) 277;
            blip.Color = (BlipColor) 25;
            blip.Name = "Gruppe Sechs";
            blip.IsShortRange = true;
            blip.Scale = 1f;
        }

        public static void LoadIpls()
        {
            //Simeon: -47.16170 -1115.3327 26.5
            RequestIpl("shr_int");
    
            //Trevor: 1985.48132, 3828.76757, 32.5
            RequestIpl("trevorstrailertidy");
        
            //Heist Jewel: -637.20159 -239.16250 38.1
            RequestIpl("post_hiest_unload");
        
            //Max Renda: -585.8247, -282.72, 35.45475  Работу можно намутить
            RequestIpl("refit_unload");
      
            //Heist Union Depository: 2.69689322, -667.0166, 16.1306286
            RequestIpl("FINBANK");
    
            //Morgue: 239.75195, -1360.64965, 39.53437
            RequestIpl("Coroner_Int_on");
        
            //Cluckin Bell: -146.3837, 6161.5, 30.2062
            RequestIpl("CS1_02_cf_onmission1");
            RequestIpl("CS1_02_cf_onmission2");
            RequestIpl("CS1_02_cf_onmission3");
            RequestIpl("CS1_02_cf_onmission4");
        
            //Grapeseed's farm: 2447.9, 4973.4, 47.7
            RequestIpl("farm");
            RequestIpl("farmint");
            RequestIpl("farm_lod");
            RequestIpl("farm_props");
            RequestIpl("des_farmhouse");
        
            //FIB lobby: 105.4557, -745.4835, 44.7548
            RequestIpl("FIBlobby");
            RequestIpl("dt1_05_fib2_normal");
        
            //Billboard: iFruit
            RequestIpl("FruitBB");
            RequestIpl("sc1_01_newbill");
            RequestIpl("hw1_02_newbill");
            RequestIpl("hw1_emissive_newbill");
            RequestIpl("sc1_14_newbill");
            RequestIpl("dt1_17_newbill");
    
            //Lester's factory: 716.84, -962.05, 31.59
            RequestIpl("id2_14_during_door");
            RequestIpl("id2_14_during1");
        
            //Life Invader lobby: -1047.9, -233.0, 39.0
            RequestIpl("facelobby");
            
            //Авианосец
            RequestIpl("hei_carrier");
            RequestIpl("hei_carrier_distantlights");
            RequestIpl("hei_carrier_int1");
            RequestIpl("hei_carrier_int1_lod");
            RequestIpl("hei_carrier_int2");
            RequestIpl("hei_carrier_int2_lod");
            RequestIpl("hei_carrier_int3");
            RequestIpl("hei_carrier_int3_lod");
            RequestIpl("hei_carrier_int4");
            RequestIpl("hei_carrier_int4_lod");
            RequestIpl("hei_carrier_int5");
            RequestIpl("hei_carrier_int5_lod");
            RequestIpl("hei_carrier_int6");
            RequestIpl("hei_carrier_lod");
            RequestIpl("hei_carrier_lodlights");
            RequestIpl("hei_carrier_slod");

            //Яхта
            RequestIpl("hei_yacht_heist");
            RequestIpl("hei_yacht_heist_enginrm");
            RequestIpl("hei_yacht_heist_Lounge");
            RequestIpl("hei_yacht_heist_Bridge");
            RequestIpl("hei_yacht_heist_Bar");
            RequestIpl("hei_yacht_heist_Bedrm");
            RequestIpl("hei_yacht_heist_DistantLights");
            RequestIpl("hei_yacht_heist_LODLights");

            //Яхта2
            RequestIpl("gr_heist_yacht2");
            RequestIpl("gr_heist_yacht2_bar");
            RequestIpl("gr_heist_yacht2_bedrm");
            RequestIpl("gr_heist_yacht2_bridge");
            RequestIpl("gr_heist_yacht2_enginrm");
            RequestIpl("gr_heist_yacht2_lounge");
            RequestIpl("gr_grdlc_interior_placement_interior_0_grdlc_int_01_milo_");
        
            //Tunnels
            RequestIpl("v_tunnel_hole");
    
            //Carwash: 55.7, -1391.3, 30.5
            RequestIpl("Carwash_with_spinners");
        
            //Stadium "Fame or Shame": -248.49159240722656, -2010.509033203125, 34.57429885864258
            RequestIpl("sp1_10_real_interior");
            RequestIpl("sp1_10_real_interior_lod");
        
            //House in Banham Canyon: -3086.428, 339.2523, 6.3717
            RequestIpl("ch1_02_open");
            
            //Garage in La Mesa (autoshop): 970.27453, -1826.56982, 31.11477
            RequestIpl("bkr_bi_id1_23_door");
            
            //Hill Valley church - Grave: -282.46380000, 2835.84500000, 55.91446000
            RequestIpl("lr_cs6_08_grave_closed");
        
            //Lost's trailer park: 49.49379000, 3744.47200000, 46.38629000
            RequestIpl("methtrailer_grp1");
            
            //Lost safehouse: 984.1552, -95.3662, 74.50
            RequestIpl("bkr_bi_hw1_13_int");
                
            //Raton Canyon river: -1652.83, 4445.28, 2.52
            RequestIpl("CanyonRvrShallow");
            
            //Zancudo Gates (GTAO like): -1600.30100000, 2806.73100000, 18.79683000
            RequestIpl("CS3_07_MPGates");
            
            //Pillbox hospital:
            RequestIpl("rc12b_default");
    
            //RemoveIpl("rc12b_default");
            //RequestIpl("rc12b_hospitalinterior");
            
            
            //Josh's house: -1117.1632080078, 303.090698, 66.52217
            RequestIpl("bh1_47_joshhse_unburnt");
            RequestIpl("bh1_47_joshhse_unburnt_lod");
            
            RemoveIpl("sunkcargoship");
            RequestIpl("cargoship");
            
            RequestIpl("ex_sm_13_office_02b"); //АШ
            
            //RequestIpl("ex_dt1_02_office_02a"); // Бизнес Центр - old ex_dt1_02_office_03a
            
            foreach (string t in Business.Business.InteriorList)
                RequestIpl(t);
            
            RequestIpl("ex_sm_15_office_01a"); // Meria - old ex_dt1_02_office_03a
            
            RequestIpl("ex_dt1_11_office_01b"); //Maze Bank Office
            
            //Bahama Mamas: -1388.0013, -618.41967, 30.819599
            RequestIpl("hei_sm_16_interior_v_bahama_milo_");
            
            RequestIpl("apa_v_mp_h_01_a");
            RequestIpl("apa_v_mp_h_02_b");
            RequestIpl("apa_v_mp_h_08_c");
            
            RequestIpl("hei_hw1_blimp_interior_v_studio_lo_milo_");
            RequestIpl("hei_hw1_blimp_interior_v_apart_midspaz_milo_");
            RequestIpl("hei_hw1_blimp_interior_32_dlc_apart_high2_new_milo_");
            RequestIpl("hei_hw1_blimp_interior_10_dlc_apart_high_new_milo_");
            RequestIpl("hei_hw1_blimp_interior_28_dlc_apart_high2_new_milo_");
            RequestIpl("hei_hw1_blimp_interior_27_dlc_apart_high_new_milo_");
            RequestIpl("hei_hw1_blimp_interior_29_dlc_apart_high2_new_milo_");
            RequestIpl("hei_hw1_blimp_interior_30_dlc_apart_high2_new_milo_");
            RequestIpl("hei_hw1_blimp_interior_31_dlc_apart_high2_new_milo_");
            RequestIpl("apa_ch2_05e_interior_0_v_mp_stilts_b_milo_");
            RequestIpl("apa_ch2_04_interior_0_v_mp_stilts_b_milo_");
            RequestIpl("apa_ch2_04_interior_1_v_mp_stilts_a_milo_");
            RequestIpl("apa_ch2_09c_interior_2_v_mp_stilts_b_milo_");
            RequestIpl("apa_ch2_09b_interior_1_v_mp_stilts_b_milo_");
            RequestIpl("apa_ch2_09b_interior_0_v_mp_stilts_a_milo_");
            RequestIpl("apa_ch2_05c_interior_1_v_mp_stilts_a_milo_");
            RequestIpl("apa_ch2_12b_interior_0_v_mp_stilts_a_milo_");
            
            //Apparts
            /*
			RequestIpl("apa_v_mp_h_01_a");
            RequestIpl("apa_v_mp_h_01_c");
            RequestIpl("apa_v_mp_h_01_b");
            
			RequestIpl("apa_v_mp_h_01_a");
            RequestIpl("apa_v_mp_h_01_c");
            RequestIpl("apa_v_mp_h_01_b");
            
            RequestIpl("apa_v_mp_h_02_a");
            RequestIpl("apa_v_mp_h_02_c");
            RequestIpl("apa_v_mp_h_02_b");
            
            RequestIpl("apa_v_mp_h_03_a");
            RequestIpl("apa_v_mp_h_03_c");
            RequestIpl("apa_v_mp_h_03_b");
            
            RequestIpl("apa_v_mp_h_04_a");
            RequestIpl("apa_v_mp_h_04_c");
            RequestIpl("apa_v_mp_h_04_b");
            
            RequestIpl("apa_v_mp_h_05_a");
            RequestIpl("apa_v_mp_h_05_c");
            RequestIpl("apa_v_mp_h_05_b");
            
            RequestIpl("apa_v_mp_h_06_a");
            RequestIpl("apa_v_mp_h_06_c");
            RequestIpl("apa_v_mp_h_06_b");
            
            RequestIpl("apa_v_mp_h_07_a");
            RequestIpl("apa_v_mp_h_07_c");
            RequestIpl("apa_v_mp_h_07_b");
            
            RequestIpl("apa_v_mp_h_08_a");
            RequestIpl("apa_v_mp_h_08_c");
            RequestIpl("apa_v_mp_h_08_b");*/
        }
        
        public static string GetFractionName(int fractionId)
        {
            return fractionId == 0 ? "нет" : Fractions[fractionId - 1, 1];
        }

        public static string GetRankName(int fractionId, int rank)
        {
            return rank == 0 ? "нет" : Ranks[fractionId - 1, rank - 1];
        }
        
        public static string GetCompanyName(string name)
        {
            switch (name)
            {
                case "swater":
                case "sground":
                case "hlab":
                    return "Human Labs";
                case "water":
                    return "Water & Power";
                case "sunb":
                    return "Sunset Bleach";
                case "bgstar":
                    return "Bugstars";
                case "lawyer1":
                    return "UnitSA";
                case "lawyer2":
                    return "Planet-E";
                case "lawyer3":
                    return "Pearson Specter";
                case "trash":
                    return "Государство";
                case "scrap":
                    return "Государство";
                case "mail":
                    return "PostOp";
                case "mail2":
                    return "GoPostal";
                case "three":
                    return "O'Connor";
                case "taxi1":
                    return "Государство";
                case "taxi2":
                    return "Государство";
                case "bus1":
                    return "Государство";
                case "bus2":
                    return "Государство";
                case "bus3":
                    return "Государство";
                case "meh":
                    return "Государство";
                default:
                    return "Государство";
            }
        }

        public static float GetDistanceToSquared(Vector3 pos1, Vector3 pos2)
        {
            return (float) Math.Sqrt(pos1.DistanceToSquared(pos2));
        }

        public static float GetDistanceToSquared2D(Vector3 pos1, Vector3 pos2)
        {
            return (float) Math.Sqrt(pos1.DistanceToSquared2D(pos2));
        }
        
        public static Dictionary<string, string> JsonToDictionary(string json)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var entries = json.Split(',');
            foreach (var entry in entries)
            {
                var items = entry.Split(':');
                var key = "";
                var counter = 1;
                foreach (var item in items)
                {
                    counter++;
                    if (counter % 2 == 0)
                    {
                        key = item.Split('"')[1].ToString();
                    }
                    else
                    {
                        dict.Add(key, item.Split('"')[1].ToString());
                        counter = 1;
                    }
                }
            }

            return dict;
        }
        
        public static string[] StringToArray(string inputString)
        {
            string[] outputString = new string[3];

            var lastSpaceIndex = 0;
            var newStartIndex = 0;
            outputString[0] = inputString;

            if (inputString.Length <= 99) return outputString;
            
            for (int i = 0; i < inputString.Length; i++)
            {
                if (inputString.Substring(i, 1) == " ")
                {
                    lastSpaceIndex = i;
                }

                if (inputString.Length > 99 && i >= 98)
                {
                    if (i == 98)
                    {
                        outputString[0] = inputString.Substring(0, lastSpaceIndex);
                        newStartIndex = lastSpaceIndex + 1;
                    }
                    if (i > 98 && i < 198)
                    {
                        if (i == 197)
                        {
                            outputString[1] = inputString.Substring(newStartIndex, (lastSpaceIndex - (outputString[0].Length - 1)) - (inputString.Length - 1 > 197 ? 1 : -1));
                            newStartIndex = lastSpaceIndex + 1;
                        }
                        else if (i == inputString.Length - 1 && inputString.Length < 198)
                        {
                            outputString[1] = inputString.Substring(newStartIndex, ((inputString.Length - 1) - outputString[0].Length));
                            newStartIndex = lastSpaceIndex + 1;
                        }
                    }
                        
                    if (i <= 197) continue;
                        
                    if (i == inputString.Length - 1 || i == 296)
                    {
                        outputString[2] = inputString.Substring(newStartIndex, ((inputString.Length - 1) - outputString[0].Length) - outputString[1].Length);
                    }
                }
            }

            return outputString;
        }
        
        /*                               
        *                               *
        *              JSON             *
        *                               *
        */
        
        private static dynamic _dataFromJson = null;
        private static string _dataToJson = null;
        private static bool _isServerFromJsonCallBack = false;
        private static bool _isServerToJsonCallBack = false;
    
        public static async Task<dynamic> FromJson(string json, int waitMs = 500)
        {
            try
            {
                TriggerServerEvent("ARP:FromJson", json);
                SaveLog("JSON_TEST", json);
                
                while (!_isServerFromJsonCallBack && waitMs > 0)
                {
                    waitMs--;
                    await Delay(1);
                }
    
                dynamic returnData = _dataFromJson;
                ResetFromJsonCallback();
                return returnData;
            }
            catch (Exception e)
            {
                CitizenFX.Core.Debug.WriteLine($"FROM JSON {e}");
                throw;
            }
        }
        
        public static async Task<string> ToJson(object data, int waitMs = 500)
        {
            try
            {
                TriggerServerEvent("ARP:ToJson", data);
                
                while (!_isServerToJsonCallBack && waitMs > 0)
                {
                    waitMs--;
                    await Delay(1);
                }
    
                dynamic returnData = _dataToJson;
                ResetToJsonCallback();
                return returnData;
            }
            catch (Exception e)
            {
                CitizenFX.Core.Debug.WriteLine($"TO JSON {e}");
                throw;
            }
        }
        
        private static async void FromJsonServer(dynamic callback)
        {
            while (_isServerFromJsonCallBack)
            {
                await Delay(1);
            }
            _dataFromJson = callback;
            _isServerFromJsonCallBack = true;
        }
        
        private static async void ToJsonServer(string callback)
        {
            while (_isServerToJsonCallBack)
            {
                await Delay(1);
            }
            _dataToJson = callback;
            _isServerToJsonCallBack = true;
        }
        
        private static void ResetFromJsonCallback()
        {
            _dataFromJson = false;
            _isServerFromJsonCallBack = false;
        }
        
        private static void ResetToJsonCallback()
        {
            _dataToJson = null;
            _isServerToJsonCallBack = false;
        }
    }
}