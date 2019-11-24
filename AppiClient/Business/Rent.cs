using System;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Business
{
    public class Rent : BaseScript
    {
        public static dynamic[,] CarRent =
        {
            { 1033245328, "Dinghy", "Boats", 434},
            { 276773164, "Dinghy2", "Boats", 315},
            { 509498602, "Dinghy3", "Boats", 365},
            { 867467158, "Dinghy4", "Boats", 430},
            { 861409633, "Jetmax", "Boats", 1460},
            { -1043459709, "Marquis", "Boats", 31010},
            { -1030275036, "Seashark", "Boats", 86},
            { -311022263, "Seashark3", "Boats", 92},
            { 944930284, "Smuggler", "Boats", 3960},
            { 231083307, "Speeder", "Boats", 1860},
            { 437538602, "Speeder2", "Boats", 1880},
            { 400514754, "Squalo", "Boats", 552},
            { 771711535, "Submersible", "Boats", 12510},
            { -1066334226, "Submersible2", "Boats", 17510},
            { -282946103, "Suntrap", "Boats", 300},
            { 1070967343, "Toro", "Boats", 6110},
            { 908897389, "Toro2", "Boats", 6260},
            { 290013743, "Tropic", "Boats", 705},
            { 1448677353, "Tropic2", "Boats", 735},
            { -2100640717, "Tug", "Boats", 13560},
           { -344943009, "Blista", "Compacts", 74},
            { 1039032026, "Blista2", "Compacts", 31},
            { -591651781, "Blista3", "Compacts", 42},
            { 1549126457, "Brioso", "Compacts", 144},
            { -1130810103, "Dilettante", "Compacts", 90},
            { -1177863319, "Issi2", "Compacts", 66},
            { 931280609, "Issi3", "Compacts", 260},
            { -431692672, "Panto", "Compacts", 58},
            { 1507916787, "Picador", "Compacts", 287},
            { -1450650718, "Prairie", "Compacts", 71},
            { 841808271, "Rhapsody", "Compacts", 75},
            { 330661258, "CogCabrio", "Coupes", 970},
            { -5153954, "Exemplar", "Coupes", 785},
            { -591610296, "F620", "Coupes", 530},
            { -391594584, "Felon", "Coupes", 420},
            { -89291282, "Felon2", "Coupes", 460},
            { -624529134, "Jackal", "Coupes", 134},
            { 1348744438, "Oracle", "Coupes", 122},
            { -511601230, "Oracle2", "Coupes", 305},
            { 1349725314, "Sentinel", "Coupes", 366},
            { 873639469, "Sentinel2", "Coupes", 416},
            { -1356880839, "Sentinel4", "Coupes", 123},
            { 1581459400, "Windsor", "Coupes", 2680},
            { -1930048799, "Windsor2", "Coupes", 3110},
            { -1122289213, "Zion", "Coupes", 154},
            { -1193103848, "Zion2", "Coupes", 204},
            { 1131912276, "bmx", "Cycles", 13},
            { 448402357, "Cruiser", "Cycles", 14},
            { -836512833, "Fixter", "Cycles", 17},
            { -186537451, "Scorcher", "Cycles", 18},
            { 1127861609, "TriBike", "Cycles", 24},
            { -1233807380, "TriBike2", "Cycles", 24},
            { -400295096, "TriBike3", "Cycles", 24},
            { 745926877, "Buzzard2", "Helicopters", 7910},
            { 744705981, "Frogger", "Helicopters", 13910},
            { -1984275979, "Havok", "Helicopters", 900},
            { -1660661558, "Maverick", "Helicopters", 9210},
            { -726768679, "Seasparrow", "Helicopters", 1960},
            { 710198397, "Supervolito", "Helicopters", 25010},
            { -1671539132, "Supervolito2", "Helicopters", 30010},
            { -339587598, "Swift", "Helicopters", 19460},
            { 1075432268, "Swift2", "Helicopters", 39460},
            { -1845487887, "Volatus", "Helicopters", 88510},
            { -2107990196, "Guardian", "Industrial", 915},
            { 1672195559, "Akuma", "Motorcycles", 56},
            { -571009320, "Angel", "Motorcycles", 94},
            { -2115793025, "Avarus", "Motorcycles", 66},
            { -2140431165, "Bagger", "Motorcycles", 250},
            { -114291515, "Bati", "Motorcycles", 145},
            { -891462355, "Bati2", "Motorcycles", 175},
            { 86520421, "BF400", "Motorcycles", 49},
            { -440768424, "Blazer4", "Motorcycles", 106},
            { 11251904, "CarbonRS", "Motorcycles", 193},
            { 6774487, "Chimera", "Motorcycles", 255},
            { 390201602, "Cliffhanger", "Motorcycles", 392},
            { 2006142190, "Daemon", "Motorcycles", 77},
            { -1404136503, "Daemon2", "Motorcycles", 80},
            { 822018448, "Defiler", "Motorcycles", 82},
            { -239841468, "Diablous", "Motorcycles", 130},
            { 1790834270, "Diablous2", "Motorcycles", 250},
            { -1670998136, "Double", "Motorcycles", 131},
            { 1753414259, "Enduro", "Motorcycles", 32},
            { 2035069708, "Esskey", "Motorcycles", 142},
            { -1842748181, "Faggio", "Motorcycles", 18},
            { 55628203, "Faggio2", "Motorcycles", 24},
            { -1289178744, "Faggio3", "Motorcycles", 32},
            { 627535535, "Fcr", "Motorcycles", 125},
            { -757735410, "Fcr2", "Motorcycles", 225},
            { 741090084, "Gargoyle", "Motorcycles", 595},
            { 1265391242, "Hakuchou", "Motorcycles", 115},
            { -255678177, "Hakuchou2", "Motorcycles", 215},
            { 301427732, "Hexer", "Motorcycles", 79},
            { -159126838, "Innovation", "Motorcycles", 194},
            { 640818791, "Lectro", "Motorcycles", 81},
            { -1523428744, "Manchez", "Motorcycles", 94},
            { -634879114, "Nemesis", "Motorcycles", 90},
            { -1606187161, "Nightblade", "Motorcycles", 145},
            { -909201658, "PCJ", "Motorcycles", 86},
            { 1873600305, "Ratbike", "Motorcycles", 59},
            { -893578776, "Ruffian", "Motorcycles", 87},
            { 788045382, "Sanchez", "Motorcycles", 84},
            { -1453280962, "Sanchez2", "Motorcycles", 89},
            { 1491277511, "Sanctus", "Motorcycles", 166},
            { -405626514, "Shotaro", "Motorcycles", 3910},
            { 743478836, "Sovereign", "Motorcycles", 95},
            { 1836027715, "Thrust", "Motorcycles", 61},
            { -140902153, "Vader", "Motorcycles", 77},
            { -1353081087, "Vindicator", "Motorcycles", 101},
            { -609625092, "Vortex", "Motorcycles", 207},
            { -618617997, "Wolfsbane", "Motorcycles", 50},
            { -1009268949, "Zombiea", "Motorcycles", 109},
            { -570033273, "Zombieb", "Motorcycles", 108},
            { -1205801634, "Blade", "Muscle", 338},
            { -682211828, "Buccaneer", "Muscle", 250},
            { -1013450936, "Buccaneer2", "Muscle", 350},
            { 349605904, "Chino", "Muscle", 205},
            { -1361687965, "Chino2", "Muscle", 305},
            { -1116818112, "Domc", "Muscle", 474},
            { 80636076, "Dominator", "Muscle", 91},
            { -915704871, "Dominator2", "Muscle", 101},
            { -986944621, "Dominator3", "Muscle", 460},
            { 723973206, "Dukes", "Muscle", 764},
            { 2134119907, "Dukes3", "Muscle", 854},
            { -1267543371, "Ellie", "Muscle", 1900},
            { -2119578145, "Faction", "Muscle", 305},
            { -1790546981, "Faction2", "Muscle", 405},
            { -2039755226, "Faction3", "Muscle", 425},
            { -1800170043, "Gauntlet", "Muscle", 209},
            { 349315417, "Gauntlet2", "Muscle", 219},
            { -1848730848, "Gauntlets", "Muscle", 262},
            { 15219735, "Hermes", "Muscle", 825},
            { 37348240, "Hotknife", "Muscle", 463},
            { 600450546, "Hustler", "Muscle", 493},
            { 525509695, "Moonbeam", "Muscle", 57},
            { 1896491931, "Moonbeam2", "Muscle", 157},
            { -1943285540, "NightShade", "Muscle", 352},
            { -2095439403, "Phoenix", "Muscle", 373},
            { -667151410, "RatLoader", "Muscle", 104},
            { -589178377, "RatLoader2", "Muscle", 404},
            { -227741703, "Ruiner", "Muscle", 97},
            { -1685021548, "SabreGT", "Muscle", 244},
            { 223258115, "SabreGT2", "Muscle", 344},
            { 729783779, "Slamvan", "Muscle", 365},
            { 833469436, "SlamVan2", "Muscle", 415},
            { 1119641113, "SlamVan3", "Muscle", 465},
            { 1923400478, "Stalion", "Muscle", 260},
            { -401643538, "Stalion2", "Muscle", 270},
            { 972671128, "Tampa", "Muscle", 282},
            { -825837129, "Vigero", "Muscle", 454},
            { -1758379524, "Vigero2", "Muscle", 55},
            { -498054846, "Virgo", "Muscle", 73},
            { -899509638, "Virgo2", "Muscle", 243},
            { 16646064, "Virgo3", "Muscle", 143},
            { 2006667053, "Voodoo", "Muscle", 363},
            { 523724515, "Voodoo2", "Muscle", 64},
            { 1871995513, "Yosemite", "Muscle", 165},
            { 1126868326, "BfInjection", "Off-Road", 79},
            { -349601129, "Bifta", "Off-Road", 104},
            { -2128233223, "Blazer", "Off-Road", 52},
            { -1269889662, "Blazer3", "Off-Road", 56},
            { -1435919434, "Bodhi2", "Off-Road", 122},
            { -1479664699, "Brawler", "Off-Road", 1295},
            { -1993175239, "Cara", "Off-Road", 375},
            { 1770332643, "DLoader", "Off-Road", 145},
            { -1661854193, "Dune", "Off-Road", 19},
            { 92612664, "Kalahari", "Off-Road", 190},
            { -121446169, "Kamacho", "Off-Road", 463},
            { -2064372143, "Mesa3", "Off-Road", 280},
            { 1390084576, "Rancher", "Off-Road", 262},
            { 1645267888, "RancherXL", "Off-Road", 310},
            { -1207771834, "Rebel", "Off-Road", 22},
            { -2045594037, "Rebel2", "Off-Road", 47},
            { -1532697517, "Riata", "Off-Road", 860},
            { -1189015600, "Sandking", "Off-Road", 252},
            { 989381445, "Sandking2", "Off-Road", 202},
            { 101905590, "TrophyTruck", "Off-Road", 765},
            { -663299102, "TrophyTruck2", "Off-Road", 715},
            { -1523619738, "AlphaZ1", "Planes", 5360},
            { -150975354, "Blimp", "Planes", 25010},
            { -613725916, "Blimp2", "Planes", 25510},
            { -644710429, "Cuban800", "Planes", 1364},
            { -901163259, "Dodo", "Planes", 3465},
            { 970356638, "Duster", "Planes", 489},
            { -1007528109, "Howard", "Planes", 4135},
            { 621481054, "Luxor", "Planes", 23010},
            { -1214293858, "Luxor2", "Planes", 53010},
            { -1746576111, "Mammatus", "Planes", 1500},
            { -1763555241, "Microlight", "Planes", 363},
            { -392675425, "Seabreeze", "Planes", 3500},
            { -2122757008, "Stunt", "Planes", 3000},
            { -1673356438, "Velum", "Planes", 26010},
            { 1077420264, "Velum2", "Planes", 28010},
            { 1341619767, "Vestra", "Planes", 18810},
            { -392250517, "Admiral2", "Sedans", 54},
            { -1809822327, "Asea", "Sedans", 75},
            { -1903012613, "Asterope", "Sedans", 155},
            { 906642318, "Cog55", "Sedans", 710},
            { -2030171296, "Cognoscenti", "Sedans", 530},
            { -685276541, "Emperor", "Sedans", 84},
            { -1883002148, "Emperor2", "Sedans", 33},
            { -311302597, "Emperor4", "Sedans", 154},
            { 1909141499, "Fugitive", "Sedans", 150},
            { 75131841, "Glendale", "Sedans", 161},
            { 40817712, "Greenwood", "Sedans", 67},
            { -1289722222, "Ingot", "Sedans", 43},
            { 886934177, "Intruder", "Sedans", 51},
            { -350899544, "Merit2", "Sedans", 100},
            { -2077743597, "Perennial", "Sedans", 29},
            { -1883869285, "Premier", "Sedans", 54},
            { -1150599089, "Primo", "Sedans", 55},
            { -2040426790, "Primo2", "Sedans", 155},
            { -14495224, "Regina", "Sedans", 86},
            { -1369781310, "Regina3", "Sedans", 94},
            { -322343873, "Schafter", "Sedans", 54},
            { -1477580979, "Stanier", "Sedans", 35},
            { -1445320949, "Stanier2", "Sedans", 40},
            { 1723137093, "Stratum", "Sedans", 33},
            { -1961627517, "Stretch", "Sedans", 535},
            { -1894894188, "Surge", "Sedans", 102},
            { -1008861746, "Tailgater", "Sedans", 76},
            { 251388012, "Torrence", "Sedans", 152},
            { -583281407, "Vincent", "Sedans", 45},
            { 1373123368, "Warrener", "Sedans", 559},
            { 1777363799, "Washington", "Sedans", 58},
            { 767087018, "Alpha", "Sports", 1290},
            { -1041692462, "Banshee", "Sports", 860},
            { 1274868363, "BestiaGTS", "Sports", 1930},
            { -304802106, "Buffalo", "Sports", 64},
            { 736902334, "Buffalo2", "Sports", 105},
            { 237764926, "Buffalo3", "Sports", 125},
            { 2072687711, "Carbonizzare", "Sports", 2990},
            { -1045541610, "Comet2", "Sports", 1710},
            { -2022483795, "Comet3", "Sports", 1255},
            { 1561920505, "Comet4", "Sports", 3910},
            { 661493923, "Comet5", "Sports", 4610},
            { 108773431, "Coquette", "Sports", 690},
            { -1728685474, "Coquette4", "Sports", 258},
            { -2125340601, "Coquette42", "Sports", 263},
            { 196747873, "Elegy", "Sports", 669},
            { -566387422, "Elegy2", "Sports", 709},
            { -1995326987, "Feltzer2", "Sports", 260},
            { -1566741232, "Feltzer3", "Sports", 12510},
            { -1259134696, "FlashGT", "Sports", 910},
            { -1089039904, "Furoregt", "Sports", 1700},
            { 499169875, "Fusilade", "Sports", 145},
            { 2016857647, "Futo", "Sports", 175},
            { 1909189272, "GB200", "Sports", 560},
            { -1297672541, "Jester", "Sports", 1790},
            { -1106353882, "Jester2", "Sports", 1820},
            { 544021352, "Khamelion", "Sports", 830},
            { -1372848492, "Kuruma", "Sports", 245},
            { 482197771, "Lynx", "Sports", 870},
            { -142942670, "Massacro", "Sports", 1330},
            { -631760477, "Massacro2", "Sports", 1360},
            { -1848994066, "Neon", "Sports", 2910},
            { 1032823388, "Ninef", "Sports", 960},
            { -1461482751, "Ninef2", "Sports", 1010},
            { -777172681, "Omnis", "Sports", 595},
            { 867799010, "Pariah", "Sports", 3260},
            { -377465520, "Penumbra", "Sports", 76},
            { -1529242755, "Raiden", "Sports", 2310},
            { -1934452204, "RapidGT", "Sports", 530},
            { 1737773231, "RapidGT2", "Sports", 580},
            { -674927303, "Raptor", "Sports", 610},
            { -410205223, "Revolter", "Sports", 755},
            { 719660200, "Ruston", "Sports", 990},
            { -1255452397, "Schafter2", "Sports", 186},
            { -1485523546, "Schafter3", "Sports", 286},
            { 1489967196, "Schafter4", "Sports", 336},
            { -746882698, "Schwarzer", "Sports", 246},
            { 1104234922, "Sentinel3", "Sports", 462},
            { -1757836725, "Seven70", "Sports", 2710},
            { 1886268224, "Specter", "Sports", 28510},
            { 1074745671, "Specter2", "Sports", 29510},
            { 1741861769, "Streiter", "Sports", 1410},
            { 970598228, "Sultan", "Sports", 276},
            { 384071873, "Surano", "Sports", 1500},
            { -1071380347, "Tampa2", "Sports", 782},
            { 600163992, "Taranis", "Sports", 895},
            { 1887331236, "Tropos", "Sports", 4760},
            { 1102544804, "Verlierer2", "Sports", 650},
            { 117401876, "BType", "Sports Classics", 4510},
            { -831834716, "BType2", "Sports Classics", 3810},
            { -602287871, "BType3", "Sports Classics", 5810},
            { 941800958, "Casco", "Sports Classics", 2910},
            { -988501280, "Cheburek", "Sports Classics", 61},
            { 223240013, "Cheetah2", "Sports Classics", 1260},
            { 1011753235, "Coquette2", "Sports Classics", 950},
            { 784565758, "Coquette3", "Sports Classics", 1240},
            { 1483171323, "Deluxo", "Sports Classics", 553},
            { 1617472902, "Fagaloa", "Sports Classics", 154},
            { -2079788230, "GT500", "Sports Classics", 6210},
            { -1405937764, "Infernus2", "Sports Classics", 2760},
            { 1051415893, "JB700", "Sports Classics", 9710},
            { -1660945322, "Mamba", "Sports Classics", 560},
            { -2124201592, "Manana", "Sports Classics", 221},
            { 1046206681, "Michelli", "Sports Classics", 466},
            { -433375717, "Monroe", "Sports Classics", 16010},
            { 1830407356, "Peyote", "Sports Classics", 460},
            { 1078682497, "Pigalle", "Sports Classics", 402},
            { 2049897956, "RapidGT3", "Sports Classics", 1605},
            { 1841130506, "Retinue", "Sports Classics", 359},
            { 903794909, "Savestra", "Sports Classics", 1160},
            { 1545842587, "Stinger", "Sports Classics", 85010},
            { -2098947590, "StingerGT", "Sports Classics", 120010},
            { 1504306544, "Torero", "Sports Classics", 4810},
            { 464687292, "Tornado", "Sports Classics", 365},
            { 1531094468, "Tornado2", "Sports Classics", 415},
            { 1762279763, "Tornado3", "Sports Classics", 85},
            { -2033222435, "Tornado4", "Sports Classics", 75},
            { -1797613329, "Tornado5", "Sports Classics", 515},
            { -1558399629, "Tornado6", "Sports Classics", 256},
            { -982130927, "Turismo2", "Sports Classics", 12510},
            { -391595372, "Viseris", "Sports Classics", 1460},
            { 838982985, "Z190", "Sports Classics", 305},
            { 758895617, "ZType", "Sports Classics", 100010},
            { -1216765807, "Adder", "Super", 17010},
            { -313185164, "Autarch", "Super", 21510},
            { 633712403, "Banshee2", "Super", 1790},
            { -1696146015, "Bullet", "Super", 2660},
            { -802062533, "Bullet2", "Super", 2810},
            { -1311154784, "Cheetah", "Super", 24510},
            { 1392481335, "Cyclone", "Super", 9810},
            { -2120700196, "Entity2", "Super", 25010},
            { -1291952903, "EntityXF", "Super", 13210},
            { 1426219628, "FMJ", "Super", 20510},
            { 1234311532, "GP1", "Super", 39010},
            { 418536135, "Infernus", "Super", 1730},
            { -2048333973, "Italigtb", "Super", 1910},
            { -482719877, "Italigtb2", "Super", 2610},
            { -1232836011, "LE7B", "Super", 29010},
            { 1034187331, "Nero", "Super", 33510},
            { 1093792632, "Nero2", "Super", 36510},
            { 1987142870, "Osiris", "Super", 22510},
            { -1758137366, "Penetrator", "Super", 6610},
            { -1829802492, "Pfister811", "Super", 14910},
            { 2123327359, "Prototipo", "Super", 25010},
            { 234062309, "Reaper", "Super", 33910},
            { 1352136073, "SC1", "Super", 2960},
            { 819197656, "Sheava", "Super", 3260},
            { -295689028, "SultanRS", "Super", 776},
            { 1123216662, "Superd", "Super", 2210},
            { 1663218586, "T20", "Super", 19510},
            { -1134706562, "Taipan", "Super", 16510},
            { 272929391, "Tempesta", "Super", 2360},
            { 1031562256, "Tezeract", "Super", 28510},
            { 408192225, "Turismor", "Super", 34010},
            { -376434238, "Tyrant", "Super", 8160},
            { 2067820283, "Tyrus", "Super", 57010},
            { 338562499, "Vacca", "Super", 1360},
            { 1939284556, "Vagner", "Super", 36510},
            { -998177792, "Visione", "Super", 43510},
            { -1622444098, "Voltic", "Super", 660},
            { 917809321, "XA21", "Super", 12910},
            { -1403128555, "Zentorno", "Super", 30110},
            { -808831384, "Baller", "SUVs", 370},
            { 142944341, "Baller2", "SUVs", 440},
            { 1878062887, "Baller3", "SUVs", 505},
            { 634118882, "Baller4", "SUVs", 575},
            { 850565707, "BJXL", "SUVs", 195},
            { 2006918058, "Cavalcade", "SUVs", 86},
            { -789894171, "Cavalcade2", "SUVs", 191},
            { 683047626, "Contender", "SUVs", 1960},
            { 1034516789, "Contender2", "SUVs", 135},
            { 1177543287, "Dubsta", "SUVs", 1060},
            { -394074634, "Dubsta2", "SUVs", 1510},
            { -1237253773, "Dubsta3", "SUVs", 8760},
            { -2078554704, "Executioner", "SUVs", 108},
            { -1137532101, "FQ2", "SUVs", 235},
            { -1775728740, "Granger", "SUVs", 170},
            { -261346873, "Granger2", "SUVs", 66},
            { -33078019, "Granger3", "SUVs", 72},
            { -1543762099, "Gresley", "SUVs", 142},
            { 884422927, "Habanero", "SUVs", 84},
            { 486987393, "Huntley", "SUVs", 2110},
            { -330060047, "Huntley2", "SUVs", 180},
            { 1269098716, "Landstalker", "SUVs", 189},
            { 914654722, "Mesa", "SUVs", 180},
            { -808457413, "Patriot", "SUVs", 725},
            { -1651067813, "Radi", "SUVs", 114},
            { 2136773105, "Rocoto", "SUVs", 389},
            { 1221512915, "Seminole", "SUVs", 122},
            { -1810806490, "Seminole2", "SUVs", 129},
            { 1337041428, "Serrano", "SUVs", 132},
            { 1203490606, "XLS", "SUVs", 122},
            { -599568815, "Sadler", "Utility", 323},
            { -2076478498, "Tractor2", "Utility", 460},
           
        };
        
        public static void LoadAll()
        {
            for (int i = 0; i < Main.RentMarkers.Length / 4; i++)
            {
                Vector3 shopPos = new Vector3((float) Main.RentMarkers[i, 0], (float) Main.RentMarkers[i, 1], (float) Main.RentMarkers[i, 2]);

                var blip = World.CreateBlip(shopPos);
                blip.Sprite = (BlipSprite) 559;
                blip.Name = Lang.GetTextToPlayer("_lang_71");
                blip.IsShortRange = true;
                blip.Scale = 0.4f;
                
                Managers.Checkpoint.Create(blip.Position, 1.4f, "show:menu");
                Managers.Marker.Create(blip.Position, 1f, 1f, Managers.Marker.Blue.R, Managers.Marker.Blue.G, Managers.Marker.Blue.B, Managers.Marker.Blue.A);
            }
            
            for (int i = 0; i < Main.RentCarMarkers.Length / 7; i++)
            {
                Vector3 shopPos = new Vector3((float) Main.RentCarMarkers[i, 0], (float) Main.RentCarMarkers[i, 1], (float) Main.RentCarMarkers[i, 2]);

                var blip = World.CreateBlip(shopPos);
                blip.IsShortRange = true;
                blip.Scale = 0.4f;
                
                switch ((int) Main.RentCarMarkers[i, 3])
                {
                    case 88:
                        blip.Sprite = (BlipSprite) 410;
                        blip.Name = Lang.GetTextToPlayer("_lang_72");
                        break;
                    case 89:
                    case 90:
                        blip.Sprite = (BlipSprite) 574;
                        blip.Name = Lang.GetTextToPlayer("_lang_73");
                        break;
                    case 93:
                        blip.Sprite = (BlipSprite) 251;
                        blip.Name = Lang.GetTextToPlayer("_lang_74");
                        break;
                    case 114:
                        blip.Sprite = (BlipSprite) 225;
                        blip.Color = (BlipColor) 60;
                        blip.Name = Lang.GetTextToPlayer("_lang_75");
                        break;
                    default:
                        blip.Sprite = (BlipSprite) 225;
                        blip.Name = Lang.GetTextToPlayer("_lang_76");
                        break;
                }
                
                Managers.Checkpoint.Create(blip.Position, 1.4f, "show:menu");
                Managers.Marker.Create(blip.Position, 1f, 1f, Managers.Marker.Blue.R, Managers.Marker.Blue.G, Managers.Marker.Blue.B, Managers.Marker.Blue.A);
            }
        }

        public static void CheckPosForOpenMenu()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            int shopId = GetShopIdInRadius(playerPos, 2f);
            if (shopId == -1) return;
            MenuList.ShowRentMenu(shopId);
        }

        public static void CheckPosForOpenCarMenu()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            int shopId = GetCarShopIdInRadius(playerPos, 2f);
            if (shopId == -1) return;
            MenuList.ShowRentCarMenu(shopId);
        }

        public static async void Buy(int item, int price, int shopId)
        {
            if (User.GetMoneyWithoutSync() < price)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                return;
            }
            
            if (!User.Data.a_lic && item > 0)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_77"));
                return;
            }
            
            User.RemoveMoney(price);
            Business.AddMoney(shopId, price);
            
            Random rand = new Random();
            var color = rand.Next(156);
            var number = "RENT" + rand.Next(9) + color;
            var coords = GetEntityCoords(GetPlayerPed(-1), true);
            
            switch (item)
            {
                case 1:
                {
                    var vehicleHash = Convert.ToUInt32(VehicleHash.Cruiser);
                    
                    if (!await Main.LoadModel(vehicleHash))
                        return;
                    
                    var veh = CreateVehicle(Convert.ToUInt32(VehicleHash.Cruiser), coords.X, coords.Y, coords.Z + 1f, GetEntityHeading(GetPlayerPed(-1)), true, false);
                    
                    CitizenFX.Core.Vehicle vehicle = new CitizenFX.Core.Vehicle(veh)
                    {
                        IsEngineRunning = true
                    };
            
                    SetVehicleColours(vehicle.Handle, color, color);
                    SetVehicleNumberPlateText(vehicle.Handle, number);
                    
                    new CitizenFX.Core.Ped(PlayerPedId()).SetIntoVehicle(vehicle, VehicleSeat.Driver);
                    SetVehicleOnGroundProperly(vehicle.Handle);
                    
                    if (!User.HasVehicleKey(number))
                        User.AddVehicleKey(number);
                    break;
                }
                case 2:
                {
                    var vehicleHash = Convert.ToUInt32(VehicleHash.Bmx);
                    if (!await Main.LoadModel(vehicleHash))
                        return;
                    var veh = CreateVehicle(Convert.ToUInt32(VehicleHash.Bmx), coords.X, coords.Y, coords.Z + 1f, GetEntityHeading(GetPlayerPed(-1)), true, false);
                    
                    CitizenFX.Core.Vehicle vehicle = new CitizenFX.Core.Vehicle(veh)
                    {
                        IsEngineRunning = true
                    };
            
                    SetVehicleColours(vehicle.Handle, color, color);
                    SetVehicleNumberPlateText(vehicle.Handle, number);
                    
                    new CitizenFX.Core.Ped(PlayerPedId()).SetIntoVehicle(vehicle, VehicleSeat.Driver);
                    SetVehicleOnGroundProperly(vehicle.Handle);
                    
                    if (!User.HasVehicleKey(number))
                        User.AddVehicleKey(number);
                    break;
                }
                case 3:
                {
                    var vehicleHash = Convert.ToUInt32(VehicleHash.Scorcher);
                    if (!await Main.LoadModel(vehicleHash))
                        return;
                    var veh = CreateVehicle(Convert.ToUInt32(VehicleHash.Scorcher), coords.X, coords.Y, coords.Z + 1f, GetEntityHeading(GetPlayerPed(-1)), true, false);
                    
                    CitizenFX.Core.Vehicle vehicle = new CitizenFX.Core.Vehicle(veh)
                    {
                        IsEngineRunning = true
                    };
            
                    SetVehicleColours(vehicle.Handle, color, color);
                    SetVehicleNumberPlateText(vehicle.Handle, number);
                    
                    new CitizenFX.Core.Ped(PlayerPedId()).SetIntoVehicle(vehicle, VehicleSeat.Driver);
                    SetVehicleOnGroundProperly(vehicle.Handle);
                    
                    if (!User.HasVehicleKey(number))
                        User.AddVehicleKey(number);
                    break;
                }
                case 4:
                {
                    var vehicleHash = Convert.ToUInt32(VehicleHash.Fixter);
                    if (!await Main.LoadModel(vehicleHash))
                        return;
                    var veh = CreateVehicle(Convert.ToUInt32(VehicleHash.Fixter), coords.X, coords.Y, coords.Z + 1f, GetEntityHeading(GetPlayerPed(-1)), true, false);
                    
                    CitizenFX.Core.Vehicle vehicle = new CitizenFX.Core.Vehicle(veh)
                    {
                        IsEngineRunning = true
                    };
            
                    SetVehicleColours(vehicle.Handle, color, color);
                    SetVehicleNumberPlateText(vehicle.Handle, number);
                    
                    new CitizenFX.Core.Ped(PlayerPedId()).SetIntoVehicle(vehicle, VehicleSeat.Driver);
                    SetVehicleOnGroundProperly(vehicle.Handle);
                    
                    if (!User.HasVehicleKey(number))
                        User.AddVehicleKey(number);
                    break;
                }
                case 5:
                {
                    var vehicleHash = Convert.ToUInt32(VehicleHash.TriBike);
                    if (!await Main.LoadModel(vehicleHash))
                        return;
                    var veh = CreateVehicle(Convert.ToUInt32(VehicleHash.TriBike), coords.X, coords.Y, coords.Z + 1f, GetEntityHeading(GetPlayerPed(-1)), true, false);
                    
                    CitizenFX.Core.Vehicle vehicle = new CitizenFX.Core.Vehicle(veh)
                    {
                        IsEngineRunning = true
                    };
            
                    SetVehicleColours(vehicle.Handle, color, color);
                    SetVehicleNumberPlateText(vehicle.Handle, number);
                    
                    new CitizenFX.Core.Ped(PlayerPedId()).SetIntoVehicle(vehicle, VehicleSeat.Driver);
                    SetVehicleOnGroundProperly(vehicle.Handle);
                    
                    if (!User.HasVehicleKey(number))
                        User.AddVehicleKey(number);
                    break;
                }
                case 6:
                {
                    var vehicleHash = Convert.ToUInt32(VehicleHash.TriBike2);
                    if (!await Main.LoadModel(vehicleHash))
                        return;
                    var veh = CreateVehicle(Convert.ToUInt32(VehicleHash.TriBike2), coords.X, coords.Y, coords.Z + 1f, GetEntityHeading(GetPlayerPed(-1)), true, false);
                    
                    CitizenFX.Core.Vehicle vehicle = new CitizenFX.Core.Vehicle(veh)
                    {
                        IsEngineRunning = true
                    };
            
                    SetVehicleColours(vehicle.Handle, color, color);
                    SetVehicleNumberPlateText(vehicle.Handle, number);
                    
                    new CitizenFX.Core.Ped(PlayerPedId()).SetIntoVehicle(vehicle, VehicleSeat.Driver);
                    SetVehicleOnGroundProperly(vehicle.Handle);
                    
                    if (!User.HasVehicleKey(number))
                        User.AddVehicleKey(number);
                    break;
                }
                case 7:
                {
                    var vehicleHash = Convert.ToUInt32(VehicleHash.TriBike3);
                    if (!await Main.LoadModel(vehicleHash))
                        return;
                    var veh = CreateVehicle(Convert.ToUInt32(VehicleHash.TriBike3), coords.X, coords.Y, coords.Z + 1f, GetEntityHeading(GetPlayerPed(-1)), true, false);
                    
                    CitizenFX.Core.Vehicle vehicle = new CitizenFX.Core.Vehicle(veh)
                    {
                        IsEngineRunning = true
                    };
            
                    SetVehicleColours(vehicle.Handle, color, color);
                    SetVehicleNumberPlateText(vehicle.Handle, number);
                    
                    new CitizenFX.Core.Ped(PlayerPedId()).SetIntoVehicle(vehicle, VehicleSeat.Driver);
                    SetVehicleOnGroundProperly(vehicle.Handle);
                    
                    if (!User.HasVehicleKey(number))
                        User.AddVehicleKey(number);
                    break;
                }
                case 8:
                {
                    var vehicleHash = Convert.ToUInt32(VehicleHash.Faggio2);
                    if (!await Main.LoadModel(vehicleHash))
                        return;
                    var veh = CreateVehicle(Convert.ToUInt32(VehicleHash.Faggio2), coords.X, coords.Y, coords.Z + 1f,
                        GetEntityHeading(GetPlayerPed(-1)), true, false);

                    CitizenFX.Core.Vehicle vehicle = new CitizenFX.Core.Vehicle(veh)
                    {
                        IsEngineRunning = true
                    };

                    SetVehicleColours(vehicle.Handle, color, color);
                    SetVehicleNumberPlateText(vehicle.Handle, number);

                    new CitizenFX.Core.Ped(PlayerPedId()).SetIntoVehicle(vehicle, VehicleSeat.Driver);
                    SetVehicleOnGroundProperly(vehicle.Handle);

                    if (!User.HasVehicleKey(number))
                        User.AddVehicleKey(number);
                    break;
                }
                case 9:
                {
                    var vehicleHash = Convert.ToUInt32(VehicleHash.Faggio);
                    if (!await Main.LoadModel(vehicleHash))
                        return;
                    var veh = CreateVehicle(Convert.ToUInt32(VehicleHash.Faggio), coords.X, coords.Y, coords.Z + 1f, GetEntityHeading(GetPlayerPed(-1)), true, false);
                    
                    CitizenFX.Core.Vehicle vehicle = new CitizenFX.Core.Vehicle(veh)
                    {
                        IsEngineRunning = true
                    };
            
                    SetVehicleColours(vehicle.Handle, color, color);
                    SetVehicleNumberPlateText(vehicle.Handle, number);
                    
                    new CitizenFX.Core.Ped(PlayerPedId()).SetIntoVehicle(vehicle, VehicleSeat.Driver);
                    SetVehicleOnGroundProperly(vehicle.Handle);
                    
                    if (!User.HasVehicleKey(number))
                        User.AddVehicleKey(number);
                    break;
                }
                default:
                {
                    var vehicleHash = Convert.ToUInt32(VehicleHash.Scorcher);
                    if (!await Main.LoadModel(vehicleHash))
                        return;
                    var veh = CreateVehicle(Convert.ToUInt32(VehicleHash.Scorcher), coords.X, coords.Y, coords.Z + 1f, GetEntityHeading(GetPlayerPed(-1)), true, false);
                    
                    CitizenFX.Core.Vehicle vehicle = new CitizenFX.Core.Vehicle(veh)
                    {
                        IsEngineRunning = true
                    };
            
                    SetVehicleColours(vehicle.Handle, color, color);
                    SetVehicleNumberPlateText(vehicle.Handle, number);
                    
                    new CitizenFX.Core.Ped(PlayerPedId()).SetIntoVehicle(vehicle, VehicleSeat.Driver);
                    SetVehicleOnGroundProperly(vehicle.Handle);
                    
                    if (!User.HasVehicleKey(number))
                        User.AddVehicleKey(number);
                    break;
                }
            }
        }

        public static async void BuyCar(VehicleHash hash, int price, int shopId)
        {
            if (User.GetMoneyWithoutSync() < price)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                return;
            }
            
            if (!User.Data.b_lic)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_78"));
                return;
            }
            
            User.RemoveMoney(price);
            Business.AddMoney(shopId, price);
            
            Random rand = new Random();
            var color = rand.Next(156);
            var number = "RENT" + rand.Next(9) + color;
            
            //var coords = GetEntityCoords(GetPlayerPed(-1), true);

            var coords = new Vector3((float) Main.RentCarMarkers[0, 4], (float) Main.RentCarMarkers[0, 5],
                (float) Main.RentCarMarkers[0, 6]);

            if (shopId == 87)
                coords = new Vector3((float) Main.RentCarMarkers[1, 4], (float) Main.RentCarMarkers[1, 5],
                    (float) Main.RentCarMarkers[1, 6]);
            else if (shopId == 88)
                coords = new Vector3((float) Main.RentCarMarkers[2, 4], (float) Main.RentCarMarkers[2, 5],
                    (float) Main.RentCarMarkers[2, 6]);
            else if (shopId == 89)
                coords = new Vector3((float) Main.RentCarMarkers[3, 4], (float) Main.RentCarMarkers[3, 5],
                    (float) Main.RentCarMarkers[3, 6]);
            else if (shopId == 90)
                coords = new Vector3((float) Main.RentCarMarkers[4, 4], (float) Main.RentCarMarkers[4, 5],
                    (float) Main.RentCarMarkers[4, 6]);
            else if (shopId == 93)
                coords = new Vector3((float) Main.RentCarMarkers[5, 4], (float) Main.RentCarMarkers[5, 5],
                    (float) Main.RentCarMarkers[5, 6]);
            
            var vehicleHash = Convert.ToUInt32(hash);
            if (!await Main.LoadModel(vehicleHash))
                return;
                    
            var veh = CreateVehicle(Convert.ToUInt32(hash), coords.X, coords.Y, coords.Z + 1f, (shopId == 87 ? 173.1862f : 67.43578f), true, false);
                    
            CitizenFX.Core.Vehicle vehicle = new CitizenFX.Core.Vehicle(veh)
            {
                IsEngineRunning = true
            };
            
            SetVehicleColours(vehicle.Handle, color, color);
            SetVehicleNumberPlateText(vehicle.Handle, number);
                    
            new CitizenFX.Core.Ped(PlayerPedId()).SetIntoVehicle(vehicle, VehicleSeat.Driver);
            SetVehicleOnGroundProperly(vehicle.Handle);
                    
            if (!User.HasVehicleKey(number))
                User.AddVehicleKey(number);
        }

        public static async void BuyTaxi(VehicleHash hash, int price = 150, int shopId = -1)
        {
            if (User.GetMoneyWithoutSync() < price)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                return;
            }
            
            if (!User.Data.b_lic)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_78"));
                return;
            }
            
            if (!User.Data.taxi_lic)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_79"));
                return;
            }
            
            User.RemoveMoney(price);
            Business.AddMoney(shopId, price);
            
            Random rand = new Random();
            var number = "TAXI" + rand.Next(900) + 99;
            
            var vehicleHash = Convert.ToUInt32(hash);
            if (!await Main.LoadModel(vehicleHash))
                return;

            if (shopId == 92)
            {
                var veh = CreateVehicle(Convert.ToUInt32(hash), -1052.034f, -249.933f, 37.40583f, 218.1207f, true, false);
                    
                CitizenFX.Core.Vehicle vehicle = new CitizenFX.Core.Vehicle(veh)
                {
                    IsEngineRunning = true
                };
            
                SetVehicleNumberPlateText(vehicle.Handle, number);
                    
                new CitizenFX.Core.Ped(PlayerPedId()).SetIntoVehicle(vehicle, VehicleSeat.Driver);
                SetVehicleOnGroundProperly(vehicle.Handle);
            }
            else
            {
                var veh = CreateVehicle(Convert.ToUInt32(hash), 906.6081f, -186.1309f, 74.62754f, 56.05503f, true, false);
                    
                CitizenFX.Core.Vehicle vehicle = new CitizenFX.Core.Vehicle(veh)
                {
                    IsEngineRunning = true
                };
            
                SetVehicleNumberPlateText(vehicle.Handle, number);
                    
                new CitizenFX.Core.Ped(PlayerPedId()).SetIntoVehicle(vehicle, VehicleSeat.Driver);
                SetVehicleOnGroundProperly(vehicle.Handle);
            }
                    
            
            if (!User.HasVehicleKey(number))
                User.AddVehicleKey(number);
        }

        public static int GetShopIdInRadius(Vector3 pos, float radius)
        {
            for (int i = 0; i < Main.RentMarkers.Length / 4; i++)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3((float) Main.RentMarkers[i, 0], (float) Main.RentMarkers[i, 1], (float) Main.RentMarkers[i, 2])) < radius)
                    return Convert.ToInt32(Main.RentMarkers[i, 3]);
            }
            return -1;
        }

        public static int GetCarShopIdInRadius(Vector3 pos, float radius)
        {
            for (int i = 0; i < Main.RentCarMarkers.Length / 7; i++)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3((float) Main.RentCarMarkers[i, 0], (float) Main.RentCarMarkers[i, 1], (float) Main.RentCarMarkers[i, 2])) < radius)
                    return Convert.ToInt32(Main.RentCarMarkers[i, 3]);
            }
            return -1;
        }

        public static double[,] GetShopInRadius(Vector3 pos, float radius)
        {
            for (int i = 0; i < Main.Shops.Length / 4; i++)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3((float) Main.RentMarkers[i, 0], (float) Main.RentMarkers[i, 1], (float) Main.RentMarkers[i, 2])) < radius)
                    return Main.Shops;
            }
            return null;
        }

        public static Vector3 FindNearest(Vector3 pos)
        {
            var shopPosPrew = new Vector3((float) Main.RentMarkers[0, 0], (float) Main.RentMarkers[0, 1], (float) Main.RentMarkers[0, 2]);
            for (int i = 0; i < Main.RentMarkers.Length / 4; i++)
            {
                var shopPos = new Vector3((float) Main.RentMarkers[i, 0], (float) Main.RentMarkers[i, 1], (float) Main.RentMarkers[i, 2]);
                if(Main.GetDistanceToSquared(shopPos, pos) < Main.GetDistanceToSquared(shopPosPrew, pos))
                    shopPosPrew = shopPos;
            }
            return shopPosPrew;
        }
    }
}