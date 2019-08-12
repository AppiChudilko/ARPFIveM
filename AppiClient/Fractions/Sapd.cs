using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Fractions
{
    public class Sapd : BaseScript
    {       
        public static double[,] ParkCoords1 =
        {
            {818.2474, -1333.713, 25.41951, 0.1019741, 0.0683303, 179.2672, 1},
            {818.1519, -1341.188, 25.44183, -0.07044683, 0.1040661, 179.2578, 1},
            {818.1708, -1348.711, 25.43764, -0.01981894, -0.09097183, 179.5677, 1},
            {818.0542, -1355.89, 25.43768, 0.09976841, 0.07016949, 179.2085, 1},
            {817.8038, -1363.664, 25.44584, 0.07213013, 0.1859535, 178.3947, 1},
            {827.9199, -1350.936, 25.41675, 0.09758671, 0.06651632, 63.93314, 1},
            {827.887, -1345.261, 25.41486, 0.0561335, 0.0488271, 66.38979, 1},
            {828.2803, -1339.523, 25.41404, 0.07933819, -0.008169645, 65.51044, 1},
            {827.4096, -1333.437, 25.42864, -0.02108298, -0.1096952, 66.06586, 1},
            {844.0919, -1334.508, 25.42293, 0.3590898, 0.6329361, -113.755, 1},
            {843.858, -1340.252, 25.38145, 0.4401198, 0.06999297, -114.2257, 1},
            {844.0555, -1346.505, 25.39391, 0.5264887, 0.01778992, -114.4031, 1},
            {843.8363, -1352.204, 25.40289, 0.2448082, 0.07146069, -115.1981, 1},
            {865.6303, -1377.904, 25.45208, -0.06520657, -0.002679751, 37.00005, 1},
            {862.7524, -1383.075, 25.464, -0.1416674, 0.090586, 36.10522, 1},
            {859.8205, -1388.377, 25.47348, -0.00594167, -0.08527711, 35.90311, 1},
            {856.9055, -1394.033, 25.46087, 0.02824776, -0.1433623, 35.68408, 1},
            {854.5536, -1398.546, 25.45164, 0.01949957, -0.1093814, 34.23299, 1},
            {851.3375, -1404.401, 25.44578, 0.2154542, -0.00354859, 30.84429, 1},
            {834.2018, -1414.153, 25.46961, -0.1779763, 0.2849497, -29.64148, 1},
            {827.5501, -1414.562, 25.47576, -0.08927248, -0.01321608, -26.74745, 1},
            {833.2374, -1401.877, 25.47007, -0.2266881, 0.03368136, -92.15203, 1}
        };

        public static double[,] ParkCoords2 =
        {
            { 417.2396, -1627.475, 28.61091, -0.02417492, 0.05311042, 140.8292, 2 },
            { 419.6542, -1629.48, 28.61064, -0.07187133, -0.007910334, 140.93, 2 },
            { 420.9694, -1635.836, 28.61083, 0.003595591, -0.01002473, 87.20014, 2 },
            { 421.0769, -1638.919, 28.61114, -0.008082117, 0.008284682, 88.65549, 2 },
            { 420.7576, -1641.909, 28.61082, 0.06902136, -0.03683354, 87.16963, 2 },
            { 418.6073, -1646.514, 28.61071, -0.05485066, 0.01524432, 48.65864, 2 },
            { 410.7686, -1656.836, 28.61096, -0.03469914, 0.04864034, -38.62315, 2 },
            { 407.8423, -1654.818, 28.61155, -0.07128326, 0.03516208, -41.04113, 2 },
            { 405.6661, -1652.623, 28.6117, -0.02293508, 0.05257649, -41.0909, 2 },
            { 403.3712, -1650.564, 28.61263, -0.01126562, -0.02279798, -39.8219, 2 },
            { 400.9555, -1648.488, 28.61147, -0.00317691, -0.02294677, -41.91074, 2 },
            { 398.4095, -1646.452, 28.6111, -0.002991262, -0.004858712, -42.90865, 2 },
            { 395.9372, -1644.666, 28.61073, -0.02764419, 0.06212308, -43.80695, 2 },
            { 408.9845, -1638.815, 28.61094, 0.008808017, 0.01536415, -128.429, 2 },
            { 411.2961, -1636.834, 28.61058, -0.04493853, 0.03408775, -131.6957, 2 }
        };

        public static double[,] ParkCoords3 =
        {
            { 184.5134, -1145.817, 28.617, -177.9469, 3 },
            { 178.6691, -1145.5, 28.61669, -177.2995, 3 },
            { 175.7976, -1145.43, 28.61705, -176.5872, 3 },
            { 165.4863, -1145.615, 28.61683, -179.9522, 3 },
            { 160.1376, -1145.059, 28.61692, 179.6814, 3 },
            { 157.1401, -1144.99, 28.61709, -179.8791, 3 },
            { 147.8453, -1144.965, 28.61702, -178.3237, 3 },
            { 145.0765, -1144.976, 28.61664, 177.936, 3 },
            { 139.1158, -1144.517, 28.61673, -178.9356, 3 },
            { 133.3598, -1144.769, 28.61665, -178.2386, 3 },
            { 125.8084, -1144.672, 28.61717, -179.0134, 3 },
            { 120.406, -1144.659, 28.61735, -178.7092, 3 },
            { 114.827, -1143.972, 28.63708, -179.4183, 3 },
            { 107.3987, -1144.217, 28.63418, -179.3294, 3 },
            { 102.1568, -1143.963, 28.62041, -175.5185, 3 },
            { 95.96684, -1144.021, 28.64137, -166.568, 3 },
            { 85.92948, -1145.888, 28.81474, -134.0375, 3 },
            { 90.90682, -1144.329, 28.699, 174.21, 3 },
            { 124.1343, -1151.63, 28.56348, -92.67147, 3 },
            { 135.1806, -1155.283, 28.65556, -89.45984, 3 },
            { 143.946, -1155.189, 28.6586, -89.3822, 3 },
            { 153.8471, -1155.252, 28.65443, -87.75483, 3 },
            { 161.3591, -1155.449, 28.65507, -89.46068, 3 },
            { 172.1775, -1156.242, 28.65134, -108.4552, 3 },
            { 182.2754, -1156.821, 28.65565, -91.40263, 3 },
            { 190.4515, -1155.126, 28.63805, 3.385163, 3 }
        };

        public static void Garderob(int idx)
        {
            switch (idx)
            {
                case 0:
                    /*API.ClearPlayerAccessory(sender, 0);
                    User.ResetPlayerPed(sender);
                    User.ResetPlayerCloth(sender);*/
                    
                    Sync.Data.ResetLocally(User.GetServerId(), "hasMask");
                    
                    SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                    ClearPedProp(GetPlayerPed(-1), 0);
                    
                    Characher.UpdateCloth(false);
                    Characher.UpdateFace(false);
                    break;
                case 1:
                    ClearPedProp(GetPlayerPed(-1), 0);
                    
                    if (User.Skin.SEX == 1) {
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 34, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 52, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 35, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 86, 0, 2);
                    }
                    else {
                        SetPedComponentVariation(GetPlayerPed(-1), 0, 1, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 11, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 35, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 54, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 38, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 58, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 13, 3, 2);
                    }
                    break;
                case 2:
                    ClearPedProp(GetPlayerPed(-1), 0);
                    
                    if (User.Skin.SEX == 1) {
                        SetPedComponentVariation(GetPlayerPed(-1), 0, 1, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 14, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 34, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 52, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 35, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 48, 0, 2);
                        
                        if (User.Data.rank == 3)
                            SetPedComponentVariation(GetPlayerPed(-1), 10, 7, 1, 2);
                        else if (User.Data.rank == 4)
                            SetPedComponentVariation(GetPlayerPed(-1), 10, 7, 2, 2);
                        else if (User.Data.rank > 4)
                            SetPedComponentVariation(GetPlayerPed(-1), 10, 7, 3, 2);
                    }
                    else {
                        SetPedComponentVariation(GetPlayerPed(-1), 0, 1, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 35, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 54, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 58, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 55, 0, 2);
                        
                        if (User.Data.rank == 3)
                            SetPedComponentVariation(GetPlayerPed(-1), 10, 8, 1, 2);
                        else if (User.Data.rank == 4)
                            SetPedComponentVariation(GetPlayerPed(-1), 10, 8, 2, 2);
                        else if (User.Data.rank > 4)
                            SetPedComponentVariation(GetPlayerPed(-1), 10, 8, 3, 2);
                    }

                    SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                    break;
                case 3:
                    /*API.ClearPlayerAccessory(sender, 0);
                    User.ResetPlayerPed(sender);
                    User.ResetPlayerCloth(sender);*/
                    
                    Sync.Data.ResetLocally(User.GetServerId(), "hasMask");
                    
                    Characher.UpdateFace(false);
                    SetPedPropIndex(GetPlayerPed(-1), 0, 19, 0, true);
                    
                    if (User.Skin.SEX == 1) {
                        SetPedComponentVariation(GetPlayerPed(-1), 0, 1, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 19, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 38, 2, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 57, 9, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 52, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 35, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 59, 2, 2);
                    }
                    else {
                        SetPedComponentVariation(GetPlayerPed(-1), 0, 1, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 18, 4, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 9, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 24, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 57, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 48, 0, 2);
                    }
                    break;
                case 4:
                    SetPedHeadBlendData(
                        GetPlayerPed(-1),
                        0,    
                        0,    
                        0,    
                        User.Skin.GTAO_SKIN_THRID_ID,    
                        User.Skin.GTAO_SKIN_SECOND_ID,    
                        User.Skin.GTAO_SKIN_FIRST_ID,        
                        0,    
                        User.Skin.GTAO_SKIN_MIX,    
                        0,    
                        false    
                    );
                    
                    Sync.Data.SetLocally(User.GetServerId(), "hasMask", true);
                    
                    if (User.Skin.SEX == 1) {
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 56, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 18, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 32, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 25, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 35, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 12, 1, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 43, 0, 2);
                        
                        SetPedPropIndex(GetPlayerPed(-1), 0, 116, 0, true);
                    }
                    else {
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 52, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 17, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 33, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 25, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 57, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 4, 1, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 139, 3, 2);
                        
                        SetPedPropIndex(GetPlayerPed(-1), 0, 117, 0, true);
                    }
                    break;
                case 5:
                    
                    Sync.Data.ResetLocally(User.GetServerId(), "hasMask");
                    
                    Characher.UpdateCloth(false);
                    Characher.UpdateFace(false);
                    
                    if (User.Skin.SEX == 1) {
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 18, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 30, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 25, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 152, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 12, 1, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 103, 4, 2);
                        
                        SetPedPropIndex(GetPlayerPed(-1), 0, 58, 2, true);
                    }
                    else {
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 17, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 31, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 25, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 122, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 4, 1, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 111, 4, 2);
                        
                        SetPedPropIndex(GetPlayerPed(-1), 0, 58, 2, true);
                    }
                    break;
                case 6:
                    Sync.Data.ResetLocally(User.GetServerId(), "hasMask");
                    
                    SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                    
                    Characher.UpdateCloth(false);
                    Characher.UpdateFace(false);
                    
                    if (User.Skin.SEX == 1) {
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 121, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 9, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 6, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 29, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 160, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 86, 0, 2);
                        
                        SetPedPropIndex(GetPlayerPed(-1), 0, 120, 0, true);
                    }
                    else {
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 121, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 11, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 10, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 40, 9, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 130, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 95, 0, 2);
                        
                        SetPedPropIndex(GetPlayerPed(-1), 0, 121, 0, true);
                    }
                    break;
                case 7:
                    ClearPedProp(GetPlayerPed(-1), 0);
                    
                    if (User.Skin.SEX == 1) {
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 34, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 52, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 35, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 86, 0, 2);
                    }
                    else {
                        SetPedComponentVariation(GetPlayerPed(-1), 0, 1, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 11, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 48, 4, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 54, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 38, 7, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 58, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 26, 4, 2);
                    }
                    break;
            }
        }

        public static void GarderobSheriff(int idx)
        {
            switch (idx)
            {
                case 0:
                    /*API.ClearPlayerAccessory(sender, 0);
                    User.ResetPlayerPed(sender);
                    User.ResetPlayerCloth(sender);*/
                    
                    Sync.Data.ResetLocally(User.GetServerId(), "hasMask");
                    
                    SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                    ClearPedProp(GetPlayerPed(-1), 0);
                    
                    Characher.UpdateCloth(false);
                    Characher.UpdateFace(false);
                    break;
                case 1:
                    ClearPedProp(GetPlayerPed(-1), 0);
                    Characher.UpdateCloth(false);
                    Characher.UpdateFace(false);
                    
                    if (User.Skin.SEX == 1) {
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 35, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 27, 2, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 64, 2, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 55, 0, 2);
                    }
                    else {
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 58, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 11, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 13, 1, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 10, 2, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 23, 1, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 54, 0, 2);
                    }
                    break;
                case 2:
                    ClearPedProp(GetPlayerPed(-1), 0);
                    Characher.UpdateCloth(false);
                    Characher.UpdateFace(false);
                    
                    if (User.Skin.SEX == 1) {
                        SetPedComponentVariation(GetPlayerPed(-1), 0, 1, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 11, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 64, 1, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 55, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 159, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 250, 3, 2);
                    }
                    else {
                        SetPedComponentVariation(GetPlayerPed(-1), 0, 1, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 11, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 25, 6, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 54, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 38, 7, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 58, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 26, 4, 2);
                    }
                    break;
                case 3:
                    /*API.ClearPlayerAccessory(sender, 0);
                    User.ResetPlayerPed(sender);
                    User.ResetPlayerCloth(sender);*/
                    
                    Sync.Data.ResetLocally(User.GetServerId(), "hasMask");
                    
                    Characher.UpdateFace(false);
                    SetPedPropIndex(GetPlayerPed(-1), 0, 19, 0, true);
                    
                    if (User.Skin.SEX == 1) {
                        SetPedComponentVariation(GetPlayerPed(-1), 0, 1, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 19, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 38, 2, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 57, 9, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 52, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 35, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 59, 2, 2);
                    }
                    else {
                        SetPedComponentVariation(GetPlayerPed(-1), 0, 1, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 18, 4, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 9, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 24, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 57, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 48, 0, 2);
                    }
                    break;
                case 4:
                    Sync.Data.ResetLocally(User.GetServerId(), "hasMask");
                    
                    Characher.UpdateFace(false);
                    SetPedPropIndex(GetPlayerPed(-1), 0, 19, 0, true);
                    
                    if (User.Skin.SEX == 1) {
                        SetPedPropIndex(GetPlayerPed(-1), 0, 116, 1, true);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 52, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 0, 21, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 160, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 18, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 46, 2, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 13, 2, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 61, 7, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 24, 0, 2);
                    }
                    else {
                        SetPedPropIndex(GetPlayerPed(-1), 0, 117, 1, true);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 52, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 130, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 17, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 53, 2, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 12, 2, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 59, 7, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 24, 0, 2);
                    }
                    break;
            }
        }
    }
}