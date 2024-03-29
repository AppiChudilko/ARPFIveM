﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CitizenFX.Core;
using NativeUI.PauseMenu;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Pickup : BaseScript
    {
        public static float DistanceCheck = 1.4f;
        
        //public static Vector3 BallasUnloadPos = new Vector3(-71.32738f, -1822.033f, 25.94197f);
        //public static Vector3 StockBallasPos = new Vector3(-71.32738f, -1822.033f, 25.94197f);
        //public static Vector3 MaraUnloadPos = new Vector3(1166.715f, -1641.621f, 35.95082f);
        //public static Vector3 StockMaraPos = new Vector3(1166.715f, -1641.621f, 35.95082f);
        //public static Vector3 CartelUnloadPos = new Vector3(253.535f, 375.433f, 104.5269f);
        //public static Vector3 StockCartelPos = new Vector3(245.6572f, 371.6031f, 104.7381f);
        public static Vector3 StockSapdPos = new Vector3(477.36f, -989.2f, 23.91476f);
        public static Vector3 StockSheriffPos = new Vector3(-439.1755f, 6010.428f, 26.98567f);
        
        
        public static Vector3 SheriffLicense = new Vector3(-449.33f, 6008.115f, 35.50835f);
        public static Vector3 SapdLicense = new Vector3(441.3231f, -978.7f, 29.68967f);
        
        public static Vector3 GovLicense = new Vector3(-1379.8f, -477.014f, 71.04215f);
        public static Vector3 GovNews = new Vector3(-1372.8f, -464.014f, 71.04215f);
        public static Vector3 GovCoffer = new Vector3(-1371.007f, -462.3383f, 71.0467f);
        public static Vector3 SheriffNews = new Vector3(-445.9962f, 6014.372f, 35.51099f);
        public static Vector3 SapdNews = new Vector3(447.03f, -973.31f, 29.69f);
        public static Vector3 EmsNews = new Vector3(361.9435f, -588.364f, 46.3222f);
        
       
        public static Vector3 ArcadiusUp = new Vector3(-145.3776f, -605.22f, 166.0067f);
        public static Vector3 ArcadiusDown = new Vector3(-146.2469f, -604.0237f, 166.0001f);
        
        public static Vector3 TeleportUp1 = new Vector3(-1788.222f, 425.3369f, 121.7411f);
        public static Vector3 TeleportDown1 = new Vector3(-1788.17f, 419.7766f, 131.3078f);
        
        public static Vector3 TeleportUp2 = new Vector3(-1798.062f, 409.0901f, 112.6726f);
        public static Vector3 TeleportDown2 = new Vector3(-1799.449f, 414.217f, 127.3076f);
        
        public static Vector3 TeleportUp3 = new Vector3(-1836.144f, 447.6227f, 125.5144f);
        public static Vector3 TeleportDown3 = new Vector3(-1848.783f, 444.8712f, 129.078f);
        
        public static Vector3 TeleportUp4 = new Vector3(-1838.549f, 437.5526f, 125.3198f);
        public static Vector3 TeleportDown4 = new Vector3(-1843.076f, 438.6912f, 128.7066f);
        
        //===========Больничные Койки============
        //public static Vector3 Koika1Pos = new Vector3(357.2887f, -583.5088f, 42.281f);
        //public static Vector3 Koika1Bed = new Vector3(356.7f, -584.99f, 43.2f);
        //=======================================
        
        public static readonly Vector3 BankMazeLiftOfficePos = new Vector3(-77.77799f, -829.6542f, 242.3859f);
        public static readonly Vector3 BankMazeLiftStreetPos = new Vector3(-66.66476f, -802.0474f, 43.22729f);
        public static readonly Vector3 BankMazeLiftRoofPos = new Vector3(-67.13605f, -821.9f, 320.2874f);
        public static readonly Vector3 BankMazeLiftGaragePos = new Vector3(-84.9765f, -818.7122f, 35.02804f);
        public static readonly Vector3 BankMazeOfficePos = new Vector3(-72.80013f, -816.4397f, 242.3859f);
            
        public static readonly Vector3 LifeInvaderShopPos = new Vector3(-1083.074f, -248.3521f, 36.76329f);
        public static readonly Vector3 HackerSpaceShopPos = new Vector3(522.0684f, 167.0983f, 98.38704f);
        
        public static readonly Vector3 WeldingToolShop = new Vector3(1210.057f, -3121.55f, 5.540324f);
        
        public static readonly Vector3 HackerSpaceOutPos = new Vector3(1672.243f, -26.09709f, 172.7747f);
        public static readonly Vector3 HackerSpaceInPos = new Vector3(1671.604f, -23.82703f, 177.2864f);
        
        public static readonly Vector3 MeriaUpPos = new Vector3(-1395.997f, -479.8439f, 71.04215f);
        public static readonly Vector3 MeriaDownPos = new Vector3(-1379.659f, -499.748f, 32.15739f);
        public static readonly Vector3 MeriaRoofPos = new Vector3(-1369f, -471.5994f, 83.44699f);
        public static readonly Vector3 MeriaGarPos = new Vector3(-1360.679f, -471.8841f, 30.69572f);
        public static readonly Vector3 MeriaGarderobPos = new Vector3(-1380.995f, -470.7387f, 71.04216f);
        public static readonly Vector3 MeriaHelpPos = new Vector3(-1381.844f, -477.9523f, 71.04205f);
        //public static readonly Vector3 MeriaKeyPos = new Vector3(-1381.507f, -466.2556f, 71.04215f);
        
        //--Pacific
        //public static Vector3 Terminal = new Vector3(253.3081f, 228.4226f, 101.6833f);
        
        
        public static readonly Vector3 SapdDutyPos = new Vector3(457.5687f, -992.9395f, 29.69f);
        public static readonly Vector3 SapdGarderobPos = new Vector3(455.5185f, -988.6027f, 29.6896f);
        public static readonly Vector3 SapdArsenalPos = new Vector3(452.057f, -980.2347f, 29.6896f);
        public static readonly Vector3 SapdClearPosf = new Vector3(440.5925f, -975.6348f, 29.69f);
        public static readonly Vector3 SapdArrestPos = new Vector3(459.6778f, -989.071f, 23.91487f);
        public static readonly Vector3 SapdToCyberRoomPos = new Vector3(445.4317f, -989.8f, 34.93f);
        public static readonly Vector3 SapdFromCyberRoomPos = new Vector3(445.4317f, -987.8f, 34.93f);
        //public static readonly Vector3 SapdToBalconPos = new Vector3(463.0852f, -1009.47f, 31.78511f);
        //public static readonly Vector3 SapdFromBalconPos = new Vector3(463.5898f, -1012.111f, 31.9835f);
        //public static readonly Vector3 SapdToBalcon2Pos = new Vector3(428.4888f, -995.2952f, 34.68689f);
        //public static readonly Vector3 SapdFromBalcon2Pos = new Vector3(464.1708f, -984.0346f, 38.89184f);
        //public static readonly Vector3 SapdToInterrogationPos = new Vector3(404.0302f, -997.302f, -100.004f);
        //public static readonly Vector3 SapdFromInterrogationPos = new Vector3(446.7996f, -985.8127f, 25.67422f);
        public static readonly Vector3 EmsLicensePos = new Vector3(353.706f, -584.1873f, 46.32218f);
        public static readonly Vector3 EmsPsyLicPos = new Vector3(342.9993f, -596.6797f, 46.32219f);
        
        public static readonly Vector3 SheriffGarderobPos = new Vector3(-452.945f, 6013.818f, 30.716f);
        public static readonly Vector3 SheriffGarderobPos2 = new Vector3(1848.908f, 3689.9604f, 33.2670f);
        public static readonly Vector3 SheriffArsenalPos = new Vector3(-437.330f, 6001.264f, 30.716f);
        public static readonly Vector3 SheriffArsenalPos2 = new Vector3(1857.1979f, 3689.1872f, 33.26704f);
        public static readonly Vector3 SheriffClearPos = new Vector3(-448.6859f, 6012.703f, 30.71638f);
        public static readonly Vector3 SheriffArrestPos = new Vector3(-441.605f, 6012.786f, 26.985f);
        public static readonly Vector3 ArrestPosPrison = new Vector3(1678.605f, 2593.786f, 44.56f);
        
        public static readonly Vector3 FibDutyPos = new Vector3(131.0169f, -729.158f, 257.1521f);
        public static readonly Vector3 FibArsenalPos = new Vector3(129.3821f, -730.57f, 257.1521f);
        public static readonly Vector3 FibLift0StationPos = new Vector3(122.9873f, -741.1865f, 32.13323f);
        public static readonly Vector3 FibLift1StationPos = new Vector3(136.2213f, -761.6816f, 44.75201f);
        public static readonly Vector3 FibLift2StationPos = new Vector3(136.2213f, -761.6816f, 241.152f);
        public static readonly Vector3 FibLift3StationPos = new Vector3(114.9807f, -741.8279f, 257.1521f);
        public static readonly Vector3 FibLift4StationPos = new Vector3(141.4099f, -735.3376f, 261.8516f);
        
        public static readonly Vector3 LicUpPos = new Vector3(-1580.642f, -561.7131f, 107.523f);
        public static readonly Vector3 LicDownPos = new Vector3(-1581.576f, -557.9908f, 33.953f);
        public static readonly Vector3 LicRoofPos = new Vector3(-1581.576f, -557.9908f, 33.953f);
        public static readonly Vector3 LicGaragePos = new Vector3(-1540.117f, -576.3737f, 24.70784f);
        public static readonly Vector3 LicBuyPos = new Vector3(-1576.237f, -579.495f, 107.523f);
        
        /*Keys*/
        public static readonly Vector3 GovKeyPos = new Vector3(-1366.483f, -480.0415f, 30.59574f);
        public static readonly Vector3 SapdKeyPos = new Vector3(458.65f, -1007.944f, 27.27073f);
        public static readonly Vector3 SheriffKeyPos = new Vector3(-459.5084f, 6016.024f, 30.4901f);
        public static readonly Vector3 SheriffSandyKeyPos = new Vector3(1854.828f, 3679.489f, 32.82781f);
        public static readonly Vector3 FibKeyPos = new Vector3(138.4407f, -702.3063f, 32.12376f);
        public static readonly Vector3 CartelKeyPos = new Vector3(1401.796f, 1114.37f, 113.8376f);
        public static readonly Vector3 TrashKeyPos = new Vector3(1569.828f, -2130.211f, 77.33018f);
        public static readonly Vector3 Bus1KeyPos = new Vector3(-589.9058f, -2087.348f, 4.990996f);
        public static readonly Vector3 Bus2KeyPos = new Vector3(-675.2166f, -2166.933f, 4.992994f);
        public static readonly Vector3 Bus3KeyPos = new Vector3(463.2085f, -575.5786f, 27.49981f);
        public static readonly Vector3 SunbKeyPos = new Vector3(-1185.243f, -1508.272f, 3.379671f);
        public static readonly Vector3 LabKeyPos = new Vector3(3605.323f, 3733.005f, 28.6894f);
        public static readonly Vector3 ConnorKeyPos = new Vector3(-1158.08f, -742.0112f, 18.66016f);
        public static readonly Vector3 BgstarKeyPos = new Vector3(152.6678f, -3077.842f, 4.896314f);
        public static readonly Vector3 WapKeyPos = new Vector3(598.5981f, 90.37159f, 91.82394f);
        public static readonly Vector3 ScrapKeyPos = new Vector3(-429.1001f, -1728f, 18.78384f);
        public static readonly Vector3 PhotoKeyPos = new Vector3(-1041.409f, -241.3437f, 36.84774f);
        public static readonly Vector3 Mail1KeyPos = new Vector3(-409.8598f, -2803.78f, 5.000382f);
        public static readonly Vector3 Mail2KeyPos = new Vector3(78.81596f, 112.1012f, 80.16817f);
        public static readonly Vector3 Ems1KeyPos = new Vector3(325.8369f, -573.5953f, 27.89865f);
        public static readonly Vector3 Ems2KeyPos = new Vector3(204.3715f, -1642.363f, 28.8032f);
        public static readonly Vector3 EmsSandyKeyPos = new Vector3(1836.468f, 3668.447f, 32.67833f);
        public static readonly Vector3 GroupSixKeyPos = new Vector3(-41.86055f, -663.8578f, 32.48044f);
        
        /*EMS*/
        public static readonly Vector3 EmsGarderobPos = new Vector3(314.2783f, -603.3641f, 42.29278f);
        public static readonly Vector3 EmsFireGarderobPos = new Vector3(215.5956f, -1648.889f, 28.80321f);
        public static readonly Vector3 EmsDuty1Pos = new Vector3(305.501f, -598.3095f, 42.2928f);
        public static readonly Vector3 EmsDuty2Pos = new Vector3(265.9458f, -1364.34f, 23.53779f);
        //public static readonly Vector3 EmsTakeMedPos = new Vector3(343.9628f, -573.6544f, 42.2816f);
        //public static readonly Vector3 EmsAptekaPos = new Vector3(260.5087f, -1358.359f, 23.53779f);
        public static readonly Vector3 EmsHealPos = new Vector3(307.922f, -566.7927f, 42.30193f);

        public static readonly Vector3 EmsInPos = new Vector3(275.4971f, -1361.269f, 23.53781f);
        public static readonly Vector3 EmsOutPos = new Vector3(344.0675f, -1397.467f, 31.50924f);
        public static readonly Vector3 EmsIn1Pos = new Vector3(306.6194f, -1432.875f, 28.93673f);
        public static readonly Vector3 EmsOut1Pos = new Vector3(279.6934f, -1349.311f, 23.53781f);
        
        public static readonly Vector3 EmsElevatorRoofPos = new Vector3(334.7327f, -1432.775f, 45.51179f);
        public static readonly Vector3 EmsElevatorParkPos = new Vector3(406.5373f, -1347.918f, 40.05356f);
        public static readonly Vector3 EmsElevatorPos = new Vector3(247.0811f, -1371.92f, 23.53779f);
        
        public static readonly Vector3 EmsElevatorHospRoofPos = new Vector3(335.8859f, -580.1857f, 73.0612f);
        public static readonly Vector3 EmsElevatorHosp5Pos = new Vector3(335.7971f, -580.0206f, 47.22446f);
        public static readonly Vector3 EmsElevatorHosp4Pos = new Vector3(335.7876f, -580.008f, 42.27963f);
        public static readonly Vector3 EmsElevatorHosp1Pos = new Vector3(335.7878f, -580.0073f, 27.89335f);
        
        //Sandy EMS
        public static readonly Vector3 EmsSandyGarderobPos = new Vector3(314.2783f, -603.3641f, 42.29278f);
        public static readonly Vector3 EmsSandyDutyPos = new Vector3(314.2783f, -603.3641f, 42.29278f);
        public static readonly Vector3 EmsSandyHealPos = new Vector3(314.2783f, -603.3641f, 42.29278f);
        public static readonly Vector3 EmsSandyPcPos = new Vector3(1825.198f, 3670.384f, 33.27105f);
        public static readonly Vector3 EmsSandyDirectorPcPos = new Vector3(1835.713f, 3691.424f, 33.27065f);
        
        //Apteka
        public static readonly Vector3 AptekaPos = new Vector3(317.9063f, -1076.87f, 28.47855f);
        public static readonly Vector3 Apteka1Pos = new Vector3(93.47087f, -229.6194f, 53.66363f);
        public static readonly Vector3 Apteka2Pos = new Vector3(301.4001f, -733.104f, 28.37248f);
        public static readonly Vector3 Apteka3Pos = new Vector3(-176.5283f, 6383.287f, 30.49546f);
        
        public static readonly Vector3 AptekaEnterPos1 = new Vector3(326.5005f, -1074.198f, 28.47986f);
        public static readonly Vector3 AptekaEnterPos2 = new Vector3(325.4413f, -1076.997f, 18.68166f);
        
        public static readonly Vector3 AptekaEnterPos11 = new Vector3(315.9269f, -1075.047f, 28.40762f);
        public static readonly Vector3 AptekaEnterPos22 = new Vector3(316.8633f, -1076.061f, 18.68166f);
        
        /*ElShop*/
        public static readonly Vector3 ElShopPos1 = new Vector3(-658.8024f, -855.8863f, 23.50986f);
        public static readonly Vector3 ElShopPos2 = new Vector3(-658.6975f, -854.5909f, 23.50342f);
        
        /*Club*/
        public static readonly Vector3 ClubGalaxyUserPos1 = new Vector3(-1569.33f, -3016.98f, -75.40616f);
        public static readonly Vector3 ClubGalaxyUserPos2 = new Vector3(4.723007f, 220.3487f, 106.7251f);
        
        public static readonly Vector3 ClubGalaxyVPos1 = new Vector3(-1640.193f, -2989.592f, -78.22095f);
        public static readonly Vector3 ClubGalaxyVPos2 = new Vector3(-22.13015f, 217.3953f, 105.5861f);
        
        /*ArcMotors*/
        public static readonly Vector3 ArcMotorsPos1 = new Vector3(-142.2805f, -590.9449f, 166f);
        public static readonly Vector3 ArcMotorsPos2 = new Vector3(-144.3968f, -577.2031f, 31.42448f);
        
        /*Apart*/
        public static readonly Vector3 Apart19RoofPos = new Vector3(109.9076f, -867.6014f, 133.7701f);
        public static readonly Vector3 Apart16RoofPos = new Vector3(-902.897f, -369.9444f, 135.2822f);
        public static readonly Vector3 Apart5GaragePos = new Vector3(-761.8995f, 352.0111f, 86.99801f);
        public static readonly Vector3 Apart0GaragePos = new Vector3(-15.46794f, -612.5906f, 34.86151f);
        
        /*Other*/
        public static readonly Vector3 WzlInPos = new Vector3(-569.2264f, -927.8373f, 35.83355f);
        public static readonly Vector3 WzlOutPos = new Vector3(-598.7546f, -929.9592f, 22.86355f);
        public static readonly Vector3 Ems1InPos = new Vector3(-292.4272f, -602.7892f, 47.43756f);
        public static readonly Vector3 Ems1OutPos = new Vector3(-292.3299f, -600.8806f, 32.55319f);
        
        /*Bar*/
        public static readonly Vector3 BannanaInPos = new Vector3(-1387.63f, -588.0929f, 29.31953f);
        public static readonly Vector3 BannanaOutPos = new Vector3(-1388.737f, -586.4232f, 29.21938f);
        public static readonly Vector3 ComedyInPos = new Vector3(-458.3946f, 284.7393f, 77.52148f);
        public static readonly Vector3 ComedyOutPos = new Vector3(-430.0718f, 261.1223f, 82.00773f);
        
        /*AutoRepairs*/
        public static readonly Vector3 AutoRepairsPos1 = new Vector3(1130.324f, -776.4052f, 56.61017f);
        public static readonly Vector3 AutoRepairsPos2 = new Vector3(1130.287f, -778.5369f, 56.62984f);
        public static readonly Vector3 AutoRepairsPosShop = new Vector3(1128.081f, -780.6564f, 56.62164f);
        public static readonly Vector3 AutoRepairsPosCarShop = new Vector3(1154.168f, -785.3322f, 56.59872f);
        public static readonly Vector3 AutoRepairsPosCarPos = new Vector3(1150.372f, -776.313f, 56.59872f);
        
        /*ExoticCars*/
        public static readonly Vector3 ExoticCarsPos1 = new Vector3(540.368f, -196.6675f, 53.4903f);
        public static readonly Vector3 ExoticCarsPos2 = new Vector3(542.0957f, -196.6553f, 53.48984f);
        public static readonly Vector3 ExoticCarsPosShop = new Vector3(544.283f, -198.9674f, 53.48359f);
        public static readonly Vector3 ExoticCarsPosCarShop = new Vector3(548.5602f, -172.7322f, 53.48133f);
        public static readonly Vector3 ExoticCarsPosCarPos = new Vector3(538.6563f, -176.844f, 54.06823f);
        
        /*Eat Prison*/
        public static readonly Vector3 EatPrisonPos = new Vector3(1753.543f, 2566.54f, 44.56501f);
        
        /*Grab*/
        //public static readonly Vector3 OtmDengPos = new Vector3(205.3196f, -2014.976f, 17.58537f);
        
        /*Cloth*/
        public static readonly Vector3 ClothMaskPos = new Vector3(-1337.255f, -1277.948f, 3.872962f);
        
        /*MedvedevBar*/
        public static readonly Vector3 MedvedevBarPos = new Vector3(-1436.695f, 207.3504f, 56.82117f);
        public static readonly Vector3 H1126BarPos = new Vector3(-73.65629f, 939.9308f, 231.8086f);
        public static readonly Vector3 SH1139BarPos = new Vector3(-1437.855f, 6758.31f, 7.980458f);
        public static readonly Vector3 AHouse254BarPos = new Vector3(-1436.864f, 207.1095f, 56.82117f);
        
        
        public static  Vector3 RobBank1 = new Vector3(146.7091f, -1045.741f, 28.36805f);
        public static  Vector3 RobBank2 = new Vector3(148.3756f, -1050.235f, 28.346f);
        
        public static  Vector3 RobBank201 = new Vector3(310.79f, -283.753f, 53.17f);
        
        
        public static  Vector3 RobBank301 = new Vector3(-354.03f, -54.77f, 48.03f);
        
        
        public static  Vector3 RobBank401 = new Vector3(-1211.52f, -335.81f, 36.7f);
        
        
        public static  Vector3 RobBank501 = new Vector3(253.4969f, 228.44f, 100.68f);
        
        
        public static  Vector3 RobBank601 = new Vector3(1176.08f, 2712.275f, 37.08f);
       
        
        public static  Vector3 RobBank701 = new Vector3(-2957f, 481.23f, 14.7f);
        
        
        public static  Vector3 RobBank801 = new Vector3(-105.1297f, 6471f, 30.62f);
        
        
        public static Vector3 FakeNumber = new Vector3(258.9601f, 2587.598f, 43.95412f);
        
        /*Jobs*/
        public static readonly Vector3 RoadWorkerStartPos = new Vector3(52.84556f, -722.4211f, 30.7647f);

        public static readonly Vector3 BuilderStartPos = new Vector3(-142.2255f, -936.2115f, 28.29189f);
        public static readonly Vector3 OceanStartPos = new Vector3(-1470.16f, -1394.12f, 1.6f);
        public static readonly Vector3 JailCareerJobEndPos = new Vector3(2945.142f, 2746.357f, 42.34696f);
        public static readonly Vector3 JailJobStartPos = new Vector3(1774.745f, 2552.335f, 44.56499f);
        public static readonly Vector3 JewelryStartPos = new Vector3(-623.37f, -236.85f, 37.06f);
        public static readonly Vector3 BuilderUpPos = new Vector3(-155.5601f, -945.4041f, 268.1353f);
        public static readonly Vector3 BuilderDownPos = new Vector3(-163.4722f, -942.6283f, 28.28476f);
        
        public static readonly Vector3 CleanerStartPos = new Vector3(-1539.165f, -448.0839f, 34.88203f);
        
        public static readonly Vector3 SpawnHelpPos = new Vector3(-1026.957f, -2734.395f, 13.75665f);
        
        /*Condo*/
        public static readonly Vector3 CondoPaletoTeleportPos1 = new Vector3(-154.586f, 6433.142f, 30.91588f);
        public static readonly Vector3 CondoPaletoTeleportPos2 = new Vector3(-155.0347f, 6427.412f, 35.16972f);
        
        /*Houses*/
        public static readonly Vector3 H201TeleportPos1 = new Vector3(-788.7031f, 465.7262f, 99.1721f);
        public static readonly Vector3 H201TeleportPos2 = new Vector3(-783.1652f, 467.477f, 104.375f);
        public static readonly Vector3 H633TeleportPos1 = new Vector3(1366.936f, -623.8598f, 73.71093f);
        public static readonly Vector3 H633TeleportPos2 = new Vector3(1362.042f, -613.5503f, 73.33795f);
        public static readonly Vector3 H254TeleportPos1 = new Vector3(-1541.266f, 92.08395f, 56.95172f);
        public static readonly Vector3 H254TeleportPos2 = new Vector3(-1547.152f, 89.32779f, 60.31318f);
        
        /*Houses MW*/
        public static readonly Vector3 HM148TeleportPos1 = new Vector3(325.1767f, 426.2805f, 144.5671f);
        public static readonly Vector3 HM148TeleportPos2 = new Vector3(324.0107f, 425.6216f, 144.646f);
        public static readonly Vector3 HM148TeleportPos11 = new Vector3(325.1767f, 426.2805f, 147.9713f);
        public static readonly Vector3 HM148TeleportPos22 = new Vector3(324.0107f, 425.6216f, 148.0414f);
        
        /*Sombrerp*/
        public static readonly Vector3 VanilaTeleportPos1 = new Vector3(132.8929f, -1293.792f, 28.26953f);
        public static readonly Vector3 VanilaTeleportPos2 = new Vector3(136.1532f, -1287.856f, 28.26954f);
        //9983
        public static readonly Vector3 LSC1TeleportPos1 = new Vector3(-1162.921f, -2016.372f, 12.89044f); //-1162.921, -2016.372, 12.89044, 315.0523
        public static readonly Vector3 LSC1TeleportPos2 = new Vector3(-1169.317f, -2022.787f, 11.87196f);  //-1169.317, -2022.787, 11.87196, 137.0372
        
        /*HousesAndromeda*/
        public static readonly Vector3 HA813TeleportPos1 = new Vector3(-3265.857f, 1032.263f, 15.63677f);
        public static readonly Vector3 HA813TeleportPos2 = new Vector3(-3267.404f, 1032.919f, 12.10994f);
        
        /*HousesAndromeda*/
        public static readonly Vector3 MW635TeleportPos1 = new Vector3(1397.453f, -571.3978f, 73.33883f);
        public static readonly Vector3 MW635TeleportPos2 = new Vector3(1404.992f, -563.0615f, 73.50157f);
        /*HousesAndromeda*/
        public static readonly Vector3 MW_House1126TeleportPos1 = new Vector3(-111.3779f, 999.8721f, 234.7572f);
        public static readonly Vector3 MW_House1126TeleportPos2 = new Vector3(-110.6115f, 997.5737f, 239.8519f);
        
        public static readonly Vector3 MW_House1126TeleportPos3 = new Vector3(-70.35943f, 1009.321f, 233.3987f);
        public static readonly Vector3 MW_House1126TeleportPos4 = new Vector3(-71.91076f, 1007.551f, 238.4967f);
        
        public static readonly Vector3 MW_House1126TeleportPos5 = new Vector3(-97.53703f, 988.5722f, 234.7569f);
        public static readonly Vector3 MW_House1126TeleportPos6 = new Vector3(-99.77562f, 987.8108f, 239.9464f);
        
        /*ArcMotors*/
        public static readonly Vector3 MArcMotorsTeleportPos1 = new Vector3(-146.4985f, -604.0351f, 166.0001f);
        public static readonly Vector3 MArcMotorsTeleportPos2 = new Vector3(-145.2635f, -605.1155f, 166.0067f);
        
        /*Biz*/
        public static readonly Vector3 InvaderPos1 = new Vector3(-1078.19f, -254.3557f, 43.02112f);
        public static readonly Vector3 InvaderPos2 = new Vector3(-1072.305f, -246.3927f, 53.00602f);
        
        /*NPC*/
        public static readonly Vector3 StartHelpPos = new Vector3(-1033.243f, -2735.249f, 19.16927f);
        
        /*Hackers*/
        public static double[,] SapdCyberPcPos =
        {
            { 438.35, -985.16, 34.93, 350 },
            { 439.61, -983.3, 34.93, 129.61 },
            { 437.31, -983.27, 34.93, 240 },
        };
        
        public static double[,] HackerSpacePcPos =
        {
            { 1667.348, -23.80529, 177.2666, 11.08518 },
            { 1667.577, -26.06265, 177.2666, 279.1537 }
        };
        
        public static void CreateTeleportPickups()
        {
            for (int i = 0; i < SapdCyberPcPos.Length / 4; i++)
            {
                Vector3 shopPos = new Vector3((float) SapdCyberPcPos[i, 0], (float) SapdCyberPcPos[i, 1], (float) SapdCyberPcPos[i, 2]);
                Managers.Checkpoint.Create(shopPos, 1.2f, "show:menu");
                Managers.Marker.Create(shopPos, 0.7f, 0.7f, Managers.Marker.Blue.R, Managers.Marker.Blue.G, Managers.Marker.Blue.B, Managers.Marker.Blue.A);
            }
            
            for (int i = 0; i < HackerSpacePcPos.Length / 4; i++)
            {
                Vector3 shopPos = new Vector3((float) HackerSpacePcPos[i, 0], (float) HackerSpacePcPos[i, 1], (float) HackerSpacePcPos[i, 2]);
                Managers.Checkpoint.Create(shopPos, 1.2f, "show:menu");
                Managers.Marker.Create(shopPos, 0.7f, 0.7f, Managers.Marker.Blue.R, Managers.Marker.Blue.G, Managers.Marker.Blue.B, Managers.Marker.Blue.A);
            }
            
            for (int i = 0; i < Main.KitchenList.Length / 4; i++)
            {
                Vector3 shopPos = new Vector3((float) Main.KitchenList[i, 0], (float) Main.KitchenList[i, 1], (float) Main.KitchenList[i, 2]);
                Managers.Checkpoint.Create(shopPos, 1.2f, "show:menu");
                Managers.Marker.Create(shopPos, 1f, 0.3f, Managers.Marker.Blue.R, Managers.Marker.Blue.G, Managers.Marker.Blue.B, Managers.Marker.Blue.A);
            }
            
            //NPC   
            Marker.Create(StartHelpPos, 1f, 1f, Marker.Yellow.R, Marker.Yellow.G, Marker.Yellow.B, Marker.Yellow.A);
            Checkpoint.Create(StartHelpPos, 1.4f, "show:dialog:menu");
            
            //AutoRepairs   
            Marker.Create(AutoRepairsPosShop, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(AutoRepairsPosShop, 1.4f, "show:menu");
            Marker.Create(AutoRepairsPosCarShop, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(AutoRepairsPosCarShop, 1.4f, "show:menu");
            Marker.Create(AutoRepairsPosCarPos, 4f, 0.3f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(AutoRepairsPosCarPos, 4.4f, "ar:info");
            
            //ExoticCars
            Marker.Create(ExoticCarsPosShop, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(ExoticCarsPosShop, 1.4f, "show:menu");
            Marker.Create(ExoticCarsPosCarShop, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(ExoticCarsPosCarShop, 1.4f, "show:menu");
            Checkpoint.Create(ExoticCarsPosCarPos, 4.4f, "ar:info");
            
            //Eat Prison
            Marker.Create(EatPrisonPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(EatPrisonPos, 1.4f, "show:menu");
            
            //EMS   
            Marker.Create(EmsFireGarderobPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(EmsFireGarderobPos, 1.4f, "show:menu");
            Marker.Create(EmsGarderobPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(EmsGarderobPos, 1.4f, "show:menu");
            //Marker.Create(EmsTakeMedPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            //Checkpoint.Create(EmsTakeMedPos, 1.4f, "show:menu");
            //Marker.Create(EmsAptekaPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            //Checkpoint.Create(EmsAptekaPos, 1.4f, "show:menu");
            Marker.Create(EmsDuty1Pos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(EmsDuty1Pos, 1.4f, "show:menu");
            Marker.Create(EmsDuty2Pos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(EmsDuty2Pos, 1.4f, "show:menu");
            Marker.Create(EmsHealPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(EmsHealPos, 1.4f, "show:menu");
            
            //Sandy EMS
            Marker.Create(EmsSandyGarderobPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(EmsSandyGarderobPos, 1.4f, "show:menu");
            Marker.Create(EmsSandyDutyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(EmsSandyDutyPos, 1.4f, "show:menu");
            
            
            
            //Koiki
            //Marker.Create(Koika1Pos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            //Checkpoint.Create(Koika1Pos, 1f, "show:menu");
            //Checkpoint.Create(Koika1Bed, 1f, "show:menu");
            
            //Computers
            /*Marker.Create(EmsNews, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(EmsNews, 1.4f, "show:menu");
            Marker.Create(GovLicense, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(GovLicense, 1.4f, "show:menu");
            Marker.Create(SheriffNews, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SheriffNews, 1.4f, "show:menu");
            Marker.Create(SapdNews, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SapdNews, 1.4f, "show:menu");
            Marker.Create(GovCoffer, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(GovCoffer, 1.4f, "show:menu");
            Marker.Create(GovNews, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(GovNews, 1.4f, "show:menu");*/
            
            //Apteka
            Marker.Create(AptekaPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(AptekaPos, 1.4f, "show:menu");
            Marker.Create(Apteka1Pos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(Apteka1Pos, 1.4f, "show:menu");
            Marker.Create(Apteka2Pos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(Apteka2Pos, 1.4f, "show:menu");
            Marker.Create(Apteka3Pos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(Apteka3Pos, 1.4f, "show:menu");
            
            //Keys   
            Marker.Create(Ems1KeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(Ems1KeyPos, 1.4f, "show:menu");
            Marker.Create(Ems2KeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(Ems2KeyPos, 1.4f, "show:menu");
            Marker.Create(GovKeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(GovKeyPos, 1.4f, "show:menu");
            Marker.Create(FibKeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(FibKeyPos, 1.4f, "show:menu");
            Marker.Create(CartelKeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(CartelKeyPos, 1.4f, "show:menu");
            Marker.Create(TrashKeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(TrashKeyPos, 1.4f, "show:menu");
            Marker.Create(Bus1KeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(Bus1KeyPos, 1.4f, "show:menu");
            Marker.Create(Bus2KeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(Bus2KeyPos, 1.4f, "show:menu");
            Marker.Create(Bus3KeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(Bus3KeyPos, 1.4f, "show:menu");
            Marker.Create(SunbKeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SunbKeyPos, 1.4f, "show:menu");
            Marker.Create(LabKeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(LabKeyPos, 1.4f, "show:menu");
            Marker.Create(ConnorKeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(ConnorKeyPos, 1.4f, "show:menu");
            Marker.Create(BgstarKeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(BgstarKeyPos, 1.4f, "show:menu");
            Marker.Create(WapKeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(WapKeyPos, 1.4f, "show:menu");
            Marker.Create(ScrapKeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(ScrapKeyPos, 1.4f, "show:menu");
            Marker.Create(PhotoKeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(PhotoKeyPos, 1.4f, "show:menu");
            Marker.Create(Mail1KeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(Mail1KeyPos, 1.4f, "show:menu");
            Marker.Create(Mail2KeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(Mail2KeyPos, 1.4f, "show:menu");
            Marker.Create(GroupSixKeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(GroupSixKeyPos, 1.4f, "show:menu");
            
            Marker.Create(FakeNumber, 4f, 0f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            //Checkpoint.Create(FakeNumber, 4.4f, "show:menu");
            
            Marker.Create(RobBank1, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(RobBank1, 1.4f, "show:menu");
            
            Marker.Create(RobBank201, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(RobBank201, 1.4f, "show:menu");
            
            Marker.Create(RobBank301, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(RobBank301, 1.4f, "show:menu");
            
            Marker.Create(RobBank401, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(RobBank401, 1.4f, "show:menu");
            
            Marker.Create(RobBank501, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(RobBank501, 1.4f, "show:menu");
            
            Marker.Create(RobBank601, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(RobBank601, 1.4f, "show:menu");
            
            Marker.Create(RobBank701, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(RobBank701, 1.4f, "show:menu");
            
            Marker.Create(RobBank801, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(RobBank801, 1.4f, "show:menu");

            //Hackerspace
            Marker.Create(HackerSpaceShopPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(HackerSpaceShopPos, 1.4f, "show:menu");
            
            //Welding shop
            Checkpoint.Create(WeldingToolShop, 2f, "show:menu");
            
            //Invader
            Marker.Create(LifeInvaderShopPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(LifeInvaderShopPos, 1.4f, "show:menu");
            
            //EMS
            Marker.Create(EmsInPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(EmsInPos, 1.4f, "pickup:teleport");
            Marker.Create(EmsOutPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(EmsOutPos, 1.4f, "pickup:teleport");
            
            Marker.Create(EmsIn1Pos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(EmsIn1Pos, 1.4f, "pickup:teleport");
            Marker.Create(EmsOut1Pos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(EmsOut1Pos, 1.4f, "pickup:teleport");
            
            Marker.Create(EmsElevatorPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(EmsElevatorPos, 1.4f, "pickup:teleport:menu");
            Marker.Create(EmsElevatorParkPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(EmsElevatorParkPos, 1.4f, "pickup:teleport:menu");
            Marker.Create(EmsElevatorRoofPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(EmsElevatorRoofPos, 1.4f, "pickup:teleport:menu");
            
            
            Marker.Create(EmsElevatorHospRoofPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(EmsElevatorHospRoofPos, 1.4f, "pickup:teleport:menu");
            Marker.Create(EmsElevatorHosp5Pos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(EmsElevatorHosp5Pos, 1.4f, "pickup:teleport:menu");
            Marker.Create(EmsElevatorHosp4Pos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(EmsElevatorHosp4Pos, 1.4f, "pickup:teleport:menu");
            Marker.Create(EmsElevatorHosp1Pos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(EmsElevatorHosp1Pos, 1.4f, "pickup:teleport:menu");
            
            
            //SAPD
            /*
            Marker.Create(SapdFromCyberRoomPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(SapdFromCyberRoomPos, 1.4f, "pickup:teleport");
            
            Marker.Create(SapdToCyberRoomPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(SapdToCyberRoomPos, 1.4f, "pickup:teleport");
            
            Marker.Create(SapdToBalconPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(SapdToBalconPos, 1.4f, "pickup:teleport");
            
            Marker.Create(SapdFromBalconPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(SapdFromBalconPos, 1.4f, "pickup:teleport");
            
            Marker.Create(SapdToBalcon2Pos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(SapdToBalcon2Pos, 1.4f, "pickup:teleport");
            
            Marker.Create(SapdFromBalcon2Pos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(SapdFromBalcon2Pos, 1.4f, "pickup:teleport");*/
            
            //Marker.Create(SapdToInterrogationPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            //Checkpoint.Create(SapdToInterrogationPos, 1.4f, "pickup:teleport");
            
            //Marker.Create(SapdFromInterrogationPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            //Checkpoint.Create(SapdFromInterrogationPos, 1.4f, "pickup:teleport");
            
            //Maze Bank
            Marker.Create(BankMazeLiftOfficePos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(BankMazeLiftOfficePos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(BankMazeLiftStreetPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(BankMazeLiftStreetPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(BankMazeLiftRoofPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(BankMazeLiftRoofPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(BankMazeLiftGaragePos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(BankMazeLiftGaragePos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(BankMazeOfficePos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(BankMazeOfficePos, 1.4f, "show:menu");
            
            //Meria
            Marker.Create(MeriaUpPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(MeriaUpPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(MeriaDownPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(MeriaDownPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(MeriaRoofPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(MeriaRoofPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(MeriaGarPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(MeriaGarPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(MeriaGarderobPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(MeriaGarderobPos, 1.4f, "show:menu");
            
            Marker.Create(MeriaHelpPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(MeriaHelpPos, 1.4f, "show:menu");
            
            //SAPD
            Marker.Create(SapdDutyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SapdDutyPos, 1.4f, "show:menu");
            
            Marker.Create(SapdGarderobPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SapdGarderobPos, 1.4f, "show:menu");
            
            Marker.Create(SapdArsenalPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SapdArsenalPos, 1.4f, "show:menu");
            
            Marker.Create(StockSapdPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(StockSapdPos, 1.4f, "show:menu");
            
            Marker.Create(StockSheriffPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(StockSheriffPos, 1.4f, "show:menu");
            
            Marker.Create(SapdClearPosf, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SapdClearPosf, 1.4f, "show:menu");
            
            Marker.Create(SapdArrestPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SapdArrestPos, 1.4f, "show:menu");
            
            Marker.Create(ArrestPosPrison, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(ArrestPosPrison, 1.4f, "show:menu");
            
            Marker.Create(SapdKeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SapdKeyPos, 1.4f, "show:menu");
            
            //Sheriff
            Marker.Create(SheriffGarderobPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SheriffGarderobPos, 1.4f, "show:menu");
            Marker.Create(SheriffGarderobPos2, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SheriffGarderobPos2, 1.4f, "show:menu");
            Marker.Create(SheriffArsenalPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SheriffArsenalPos, 1.4f, "show:menu");
            Marker.Create(SheriffArsenalPos2, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SheriffArsenalPos2, 1.4f, "show:menu");
            Marker.Create(SheriffClearPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SheriffClearPos, 1.4f, "show:menu");
            Marker.Create(SheriffArrestPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SheriffArrestPos, 1.4f, "show:menu");
            Marker.Create(SheriffKeyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SheriffKeyPos, 1.4f, "show:menu");
            Marker.Create(SheriffLicense, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SheriffLicense, 1.4f, "show:menu");
            Marker.Create(SapdLicense, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(SapdLicense, 1.4f, "show:menu");
            Marker.Create(EmsLicensePos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(EmsLicensePos, 1.4f, "show:menu");
            Marker.Create(EmsPsyLicPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(EmsPsyLicPos, 1.4f, "show:menu");
            
            //FIB
            Marker.Create(FibDutyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(FibDutyPos, 1.4f, "show:menu");
            
            Marker.Create(FibArsenalPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(FibArsenalPos, 1.4f, "show:menu");
            
            Marker.Create(FibLift0StationPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(FibLift0StationPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(FibLift1StationPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(FibLift1StationPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(FibLift2StationPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(FibLift2StationPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(FibLift3StationPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(FibLift3StationPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(FibLift4StationPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(FibLift4StationPos, 1.4f, "pickup:teleport:menu");
            
            //Bar
            /*Marker.Create(BannanaInPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(BannanaInPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(BannanaOutPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(BannanaOutPos, 1.4f, "pickup:teleport:menu");*/
            
            Marker.Create(AutoRepairsPos1, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(AutoRepairsPos1, 1.4f, "pickup:teleport:menu");
            Marker.Create(AutoRepairsPos2, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(AutoRepairsPos2, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(ExoticCarsPos1, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(ExoticCarsPos1, 1.4f, "pickup:teleport:menu");
            Marker.Create(ExoticCarsPos2, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(ExoticCarsPos2, 1.4f, "pickup:teleport:menu");
            
            //Apart
            Marker.Create(Apart0GaragePos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(Apart0GaragePos, 1.4f, "pickup:teleport:menu");
            Marker.Create(Apart5GaragePos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(Apart5GaragePos, 1.4f, "pickup:teleport:menu");
            Marker.Create(Apart16RoofPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(Apart16RoofPos, 1.4f, "pickup:teleport:menu");
            Marker.Create(Apart19RoofPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(Apart19RoofPos, 1.4f, "pickup:teleport:menu");
            
            //ElShop
            /*Marker.Create(ElShopPos1, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(ElShopPos1, 1.3f, "pickup:teleport");
            Marker.Create(ElShopPos2, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(ElShopPos2, 1.3f, "pickup:teleport");*/
            
            //ClubGalaxy
            Marker.Create(ClubGalaxyUserPos1, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(ClubGalaxyUserPos1, 1.3f, "pickup:teleport");
            Marker.Create(ClubGalaxyUserPos2, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(ClubGalaxyUserPos2, 1.3f, "pickup:teleport");
            //Marker.Create(ClubGalaxyVPos1, 4f, 0.3f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(ClubGalaxyVPos1, 4.3f, "pickup:teleport");
            Marker.Create(ClubGalaxyVPos2, 4f, 0.3f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(ClubGalaxyVPos2, 4.3f, "pickup:teleport");
            
            //ArcMotors
            Marker.Create(ArcMotorsPos1, 4f, 0.3f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(ArcMotorsPos1, 4.3f, "pickup:teleport");
            Marker.Create(ArcMotorsPos2, 4f, 0.3f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(ArcMotorsPos2, 4.3f, "pickup:teleport");
            
            //LSC1
            //Marker.Create(LSC1TeleportPos1, 4f, 0.3f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(LSC1TeleportPos1, 4.3f, "pickup:teleport");
            //Marker.Create(LSC1TeleportPos2, 4f, 0.3f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(LSC1TeleportPos2, 4.3f, "pickup:teleport");
            
            //Other
            Marker.Create(Ems1OutPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(Ems1OutPos, 1.4f, "pickup:teleport:menu");
            Marker.Create(Ems1InPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(Ems1InPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(WzlInPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(WzlInPos, 1.4f, "pickup:teleport:menu");
            Marker.Create(WzlOutPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(WzlOutPos, 1.4f, "pickup:teleport:menu");
            
            //Business
            Marker.Create(Business.Business.BusinessOfficePos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(Business.Business.BusinessOfficePos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(Business.Business.BusinessStreetPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(Business.Business.BusinessStreetPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(Business.Business.BusinessMotorPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(Business.Business.BusinessMotorPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(Business.Business.BusinessRoofPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(Business.Business.BusinessRoofPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(Business.Business.BusinessGaragePos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(Business.Business.BusinessGaragePos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(Business.Business.BusinessBotPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(Business.Business.BusinessBotPos, 1.4f, "show:menu");
            
            //Lic
            Marker.Create(LicUpPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(LicUpPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(LicDownPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(LicDownPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(LicRoofPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(LicRoofPos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(LicGaragePos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(LicGaragePos, 1.4f, "pickup:teleport:menu");
            
            Marker.Create(LicBuyPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(LicBuyPos, 1.4f, "show:menu");
            
            //Grab
            /*Marker.Create(OtmDengPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(OtmDengPos, 1.4f, "show:menu");*/
            
            //Cloth
            Marker.Create(ClothMaskPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(ClothMaskPos, 1.4f, "show:menu");
            
            //RoadWorker
            Marker.Create(RoadWorkerStartPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(RoadWorkerStartPos, 1.4f, "show:menu");
            
            //Builder
            Marker.Create(BuilderStartPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(BuilderStartPos, 1.4f, "show:menu");

            Marker.Create(BuilderUpPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(BuilderUpPos, 1.4f, "pickup:teleport");
            
            Marker.Create(BuilderDownPos, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(BuilderDownPos, 1.4f, "pickup:teleport");
            
            Marker.Create(CleanerStartPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(CleanerStartPos, 1.4f, "show:menu");
            
            //Ocean cleaner
            Marker.Create(OceanStartPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(OceanStartPos, 1.4f, "show:menu");
            
            //Jail Job Career
            Marker.Create(JailCareerJobEndPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(JailCareerJobEndPos, 1.4f, "show:menu");
            Marker.Create(JailJobStartPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
            Checkpoint.Create(JailJobStartPos, 1.4f, "show:menu");

            //Houses
            Marker.Create(CondoPaletoTeleportPos1, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(CondoPaletoTeleportPos1, 1.4f, "pickup:teleport");
            Marker.Create(CondoPaletoTeleportPos2, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(CondoPaletoTeleportPos2, 1.4f, "pickup:teleport");
            
            //Houses
            Marker.Create(H201TeleportPos1, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(H201TeleportPos1, 1.4f, "pickup:teleport");
            
            Marker.Create(H201TeleportPos2, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(H201TeleportPos2, 1.4f, "pickup:teleport");
            
            //Houses
            Marker.Create(H633TeleportPos1, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(H633TeleportPos1, 1.4f, "pickup:teleport");
            Marker.Create(H633TeleportPos2, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(H633TeleportPos2, 1.4f, "pickup:teleport");
            
            Marker.Create(H254TeleportPos1, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(H254TeleportPos1, 1.4f, "pickup:teleport");
            Marker.Create(H254TeleportPos2, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(H254TeleportPos2, 1.4f, "pickup:teleport");
            
            //ArcMotors
            if (Main.ServerName == "MilkyWay")
            {
                //MedvedevBarPos
                Marker.Create(MedvedevBarPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
                Checkpoint.Create(MedvedevBarPos, 1.4f, "show:menu");
                Marker.Create(H1126BarPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
                Checkpoint.Create(H1126BarPos, 1.4f, "show:menu");
                
                Marker.Create(MArcMotorsTeleportPos1, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
                Checkpoint.Create(MArcMotorsTeleportPos1, 1.4f, "pickup:teleport");
                Marker.Create(MArcMotorsTeleportPos2, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
                Checkpoint.Create(MArcMotorsTeleportPos2, 1.4f, "pickup:teleport");
                
                Marker.Create(MW635TeleportPos1, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
                Checkpoint.Create(MW635TeleportPos1, 1.4f, "pickup:teleport");
                Marker.Create(MW635TeleportPos2, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
                Checkpoint.Create(MW635TeleportPos2, 1.4f, "pickup:teleport");
                
                Checkpoint.Create(HM148TeleportPos1, 1.4f, "pickup:teleport");
                Checkpoint.Create(HM148TeleportPos2, 1.4f, "pickup:teleport");
                Checkpoint.Create(HM148TeleportPos11, 1.4f, "pickup:teleport");
                Checkpoint.Create(HM148TeleportPos22, 1.4f, "pickup:teleport");
            }
            if (Main.ServerName == "Andromeda")
            {
                //AHouses
                Marker.Create(HA813TeleportPos1, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
                Checkpoint.Create(HA813TeleportPos1, 1.4f, "pickup:teleport");
                Marker.Create(HA813TeleportPos2, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
                Checkpoint.Create(HA813TeleportPos2, 1.4f, "pickup:teleport");
                
                Marker.Create(AHouse254BarPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
                Checkpoint.Create(AHouse254BarPos, 1.4f, "show:menu");
            }
            if (Main.ServerName == "Sombrero")
            {
                //AHouses
                Marker.Create(VanilaTeleportPos1, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
                Checkpoint.Create(VanilaTeleportPos1, 1.4f, "pickup:teleport");
                Marker.Create(VanilaTeleportPos2, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
                Checkpoint.Create(VanilaTeleportPos2, 1.4f, "pickup:teleport");
             
                Marker.Create(SH1139BarPos, 1f, 1f, Marker.Blue.R, Marker.Blue.G, Marker.Blue.B, Marker.Blue.A);
                Checkpoint.Create(SH1139BarPos, 1.4f, "show:menu");
            }
            
            Marker.Create(InvaderPos1, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(InvaderPos1, 1.4f, "pickup:teleport");
            Marker.Create(InvaderPos2, 1f, 1f, Marker.Blue100.R, Marker.Blue100.G, Marker.Blue100.B, Marker.Blue100.A);
            Checkpoint.Create(InvaderPos2, 1.4f, "pickup:teleport");
        }
        
        public static async void CheckPlayerPosToPickup()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            
            /*Apart*/
            if (Main.GetDistanceToSquared(Apart0GaragePos, playerPos) < DistanceCheck)
                MenuList.ShowApartament01TeleportMenu();
            if (Main.GetDistanceToSquared(Apart5GaragePos, playerPos) < DistanceCheck)
                MenuList.ShowApartamentTeleportMenu(24, 5);
            if (Main.GetDistanceToSquared(Apart16RoofPos, playerPos) < DistanceCheck)
                MenuList.ShowApartamentTeleportMenu(34, 16);
            if (Main.GetDistanceToSquared(Apart19RoofPos, playerPos) < DistanceCheck)
                MenuList.ShowApartamentTeleportMenu(20, 19);
            
            //ElShop
            /*if (Main.GetDistanceToSquared(ElShopPos2, playerPos) < DistanceCheck)
                User.Teleport(ElShopPos1);
            if (Main.GetDistanceToSquared(ElShopPos1, playerPos) < DistanceCheck)
                User.Teleport(ElShopPos2);*/
            
            //Club Galaxy
            if (Main.GetDistanceToSquared(ClubGalaxyUserPos1, playerPos) < DistanceCheck)
                User.Teleport(ClubGalaxyUserPos2);
            if (Main.GetDistanceToSquared(ClubGalaxyUserPos2, playerPos) < DistanceCheck)
                User.Teleport(ClubGalaxyUserPos1);
            if (Main.GetDistanceToSquared(ClubGalaxyVPos1, playerPos) < DistanceCheck)
                User.VTeleport(ClubGalaxyVPos2, 353.0958f);
            if (Main.GetDistanceToSquared(ClubGalaxyVPos2, playerPos) < DistanceCheck)
            {
                if (await User.GetCashMoney() < 100)
                {
                    Notification.SendWithTime("~r~У Вас нет $100 в наличке");
                    return;
                }
                User.RemoveCashMoney(100);
                Business.Business.AddMoney(122, 100);
                User.VTeleport(ClubGalaxyVPos1, 259.5648f);
            }
            
            //ArcMotors
            if (Main.GetDistanceToSquared(ArcMotorsPos1, playerPos) < DistanceCheck)
                User.VTeleport(ArcMotorsPos2, 160.3578f);
            if (Main.GetDistanceToSquared(ArcMotorsPos2, playerPos) < DistanceCheck)
                User.VTeleport(ArcMotorsPos1, 128.2233f);
            
            //LSC1
            if (Main.GetDistanceToSquared(LSC1TeleportPos1, playerPos) < DistanceCheck)
                User.VTeleport(LSC1TeleportPos2, 137.6203f ); 
            if (Main.GetDistanceToSquared(LSC1TeleportPos2, playerPos) < DistanceCheck)
                User.VTeleport(LSC1TeleportPos1, 315.7973f); 
            
            /*Maze*/
            if (Main.GetDistanceToSquared(BankMazeLiftStreetPos, playerPos) < DistanceCheck)
                MenuList.ShowMazeOfficeTeleportMenu();
            if (Main.GetDistanceToSquared(BankMazeLiftOfficePos, playerPos) < DistanceCheck)
                MenuList.ShowMazeOfficeTeleportMenu();
            if (Main.GetDistanceToSquared(BankMazeLiftGaragePos, playerPos) < DistanceCheck)
                MenuList.ShowMazeOfficeTeleportMenu();
            if (Main.GetDistanceToSquared(BankMazeLiftRoofPos, playerPos) < DistanceCheck)
                MenuList.ShowMazeOfficeTeleportMenu();
            
            /*HackerSpace*/
            if (Main.GetDistanceToSquared(HackerSpaceInPos, playerPos) < DistanceCheck)
            {
                if (User.Data.mp0_watchdogs < 70)
                {
                    Notification.SendWithTime("~r~Ваш навык слишком низок чтобы войти в HackerSpace");
                    return;
                }
                User.Teleport(HackerSpaceOutPos);
            }
            if (Main.GetDistanceToSquared(HackerSpaceOutPos, playerPos) < DistanceCheck)
            {
                if (User.Data.mp0_watchdogs < 70)
                {
                    Notification.SendWithTime("~r~Ваш навык слишком низок чтобы войти в HackerSpace");
                    return;
                }
                User.Teleport(HackerSpaceInPos);
            }
            
            if (Main.GetDistanceToSquared(FakeNumber, playerPos) < DistanceCheck)
            {
                MenuList.ShowFakeNumberMenu();
            }

            if (Main.GetDistanceToSquared(RobBank1, playerPos) < DistanceCheck)
            {
                Grab.GrabBank();
            }
            if (Main.GetDistanceToSquared(RobBank201, playerPos) < DistanceCheck)
            {
                Grab.GrabBank();
            }
            if (Main.GetDistanceToSquared(RobBank301, playerPos) < DistanceCheck)
            {
                Grab.GrabBank();
            }
            if (Main.GetDistanceToSquared(RobBank401, playerPos) < DistanceCheck)
            {
                Grab.GrabBank();
            }
            if (Main.GetDistanceToSquared(RobBank501, playerPos) < DistanceCheck)
            {
                Grab.GrabBank();
            }
            if (Main.GetDistanceToSquared(RobBank601, playerPos) < DistanceCheck)
            {
                Grab.GrabBank();
            }
            if (Main.GetDistanceToSquared(RobBank701, playerPos) < DistanceCheck)
            {
                Grab.GrabBank();
            }
            if (Main.GetDistanceToSquared(RobBank801, playerPos) < DistanceCheck)
            {
                Grab.GrabBank();
            }
            
            //ArcadiusPrivate
            if (Main.GetDistanceToSquared(ArcadiusDown, playerPos) < DistanceCheck)
            {
                User.Teleport(ArcadiusUp);
            }
            if (Main.GetDistanceToSquared(ArcadiusUp, playerPos) < DistanceCheck)
            {
                User.Teleport(ArcadiusDown);
            }
            
            if (Main.GetDistanceToSquared(TeleportDown1, playerPos) < DistanceCheck)
            {
                User.Teleport(TeleportUp1);
            }
            if (Main.GetDistanceToSquared(TeleportUp1, playerPos) < DistanceCheck)
            {
                User.Teleport(TeleportDown1);
            }
            
            if (Main.GetDistanceToSquared(TeleportDown2, playerPos) < DistanceCheck)
            {
                User.Teleport(TeleportUp2);
            }
            if (Main.GetDistanceToSquared(TeleportUp2, playerPos) < DistanceCheck)
            {
                User.Teleport(TeleportDown2);
            }
            
            if (Main.GetDistanceToSquared(TeleportDown3, playerPos) < DistanceCheck)
            {
                User.Teleport(TeleportUp3);
            }
            if (Main.GetDistanceToSquared(TeleportUp3, playerPos) < DistanceCheck)
            {
                User.Teleport(TeleportDown3);
            }
            
            if (Main.GetDistanceToSquared(TeleportDown4, playerPos) < DistanceCheck)
            {
                User.Teleport(TeleportUp4);
            }
            if (Main.GetDistanceToSquared(TeleportUp4, playerPos) < DistanceCheck)
            {
                User.Teleport(TeleportDown4);
            }
            
            //Koiki
            /*if (Main.GetDistanceToSquared(Koika1Pos, playerPos) < DistanceCheck)
            {
                User.PlayAnimation("missfbi1", "cpr_pumpchest_idle");
                SetEntityCoordsNoOffset(GetPlayerPed(-1), 356.7f, -584.99f, 43.2f, false, false, false);
                //User.Freeze(PlayerId(), true);
                FreezeEntityPosition(GetPlayerPed(-1), true);
                await Delay(200);
                //User.Teleport(Koika1Bed);
                User.HealNaKoike();
                User.healing = true;
                //User.PlayAnimation("missfbi1", "cpr_pumpchest_idle", 9);
                if (User.healing == true)
                {
                    await Delay(2000);
                    //User.PlayAnimation("missfbi1", "cpr_pumpchest_idle");
                    return;
                }
                
            }
            if (Main.GetDistanceToSquared(Koika1Bed, playerPos) < DistanceCheck)
            {
                FreezeEntityPosition(GetPlayerPed(-1), false);
                User.Teleport(Koika1Pos);
                User.healing = false;
                User.Freeze(PlayerId(), false);
                if(User.healing == true && GetEntityHealth(GetPlayerPed(-1)) > 199)
                {
                    User.healing = false;
                    User.Freeze(PlayerId(), false);
                    Notification.Send("Вы здоровы <3");
                    User.Teleport(Koika1Pos);
                }
            }*/
            
            /*EMS*/
            if (Main.GetDistanceToSquared(EmsOutPos, playerPos) < DistanceCheck)
                User.Teleport(EmsInPos);
            if (Main.GetDistanceToSquared(EmsInPos, playerPos) < DistanceCheck)
                User.Teleport(EmsOutPos);
            
            if (Main.GetDistanceToSquared(EmsOut1Pos, playerPos) < DistanceCheck)
                User.Teleport(EmsIn1Pos);
            if (Main.GetDistanceToSquared(EmsIn1Pos, playerPos) < DistanceCheck)
                User.Teleport(EmsOut1Pos);
            
            if (Main.GetDistanceToSquared(EmsElevatorPos, playerPos) < DistanceCheck)
                MenuList.ShowEmsTeleportMenu();
            if (Main.GetDistanceToSquared(EmsElevatorParkPos, playerPos) < DistanceCheck)
                MenuList.ShowEmsTeleportMenu();
            if (Main.GetDistanceToSquared(EmsElevatorRoofPos, playerPos) < DistanceCheck)
                MenuList.ShowEmsTeleportMenu();
            
            if (Main.GetDistanceToSquared(EmsElevatorHospRoofPos, playerPos) < DistanceCheck)
                MenuList.ShowEmsNewTeleportMenu();
            if (Main.GetDistanceToSquared(EmsElevatorHosp5Pos, playerPos) < DistanceCheck)
                MenuList.ShowEmsNewTeleportMenu();
            if (Main.GetDistanceToSquared(EmsElevatorHosp4Pos, playerPos) < DistanceCheck)
                MenuList.ShowEmsNewTeleportMenu();
            if (Main.GetDistanceToSquared(EmsElevatorHosp1Pos, playerPos) < DistanceCheck)
                MenuList.ShowEmsNewTeleportMenu();
            
            /*SAPD*/
            /*
            if (Main.GetDistanceToSquared(SapdToBalconPos, playerPos) < DistanceCheck)
            {
                int pass = Convert.ToInt32(await Menu.GetUserInput("Введите пароль", null, 4));
                if (pass == (int) await Client.Sync.Data.Get(-9999, "sapdPass"))
                    User.Teleport(SapdFromBalconPos);
                else
                    Notification.SendWithTime("~r~Неверный пароль");
            }
            if (Main.GetDistanceToSquared(SapdFromBalconPos, playerPos) < DistanceCheck)
            {
                int pass = Convert.ToInt32(await Menu.GetUserInput("Введите пароль", null, 4));
                if (pass == (int) await Client.Sync.Data.Get(-9999, "sapdPass"))
                    User.Teleport(SapdToBalconPos);
                else
                    Notification.SendWithTime("~r~Неверный пароль");
            }*/

            if (Main.GetDistanceToSquared(SapdFromCyberRoomPos, playerPos) < DistanceCheck)
            {
                int pass = Convert.ToInt32(await Menu.GetUserInput("Введите пароль", null, 4));
                if (pass == (int) await Client.Sync.Data.Get(-9999, "sapdPass"))
                    User.Teleport(SapdToCyberRoomPos, 10000);
                else
                    Notification.SendWithTime("~r~Неверный пароль");
            }
            if (Main.GetDistanceToSquared(SapdToCyberRoomPos, playerPos) < DistanceCheck)
            {
                int pass = Convert.ToInt32(await Menu.GetUserInput("Введите пароль", null, 4));
                if (pass == (int) await Client.Sync.Data.Get(-9999, "sapdPass"))
                    User.Teleport(SapdFromCyberRoomPos, 10000);
                else
                    Notification.SendWithTime("~r~Неверный пароль");
            }
            /*
            if (Main.GetDistanceToSquared(SapdToBalcon2Pos, playerPos) < DistanceCheck)
                User.Teleport(SapdFromBalcon2Pos);
            if (Main.GetDistanceToSquared(SapdFromBalcon2Pos, playerPos) < DistanceCheck)
                User.Teleport(SapdToBalcon2Pos);
            */
            /*if (Main.GetDistanceToSquared(SapdToInterrogationPos, playerPos) < DistanceCheck)
            {
                User.Teleport(SapdFromInterrogationPos);   
                User.SetVirtualWorld(0);
            }
            if (Main.GetDistanceToSquared(SapdFromInterrogationPos, playerPos) < DistanceCheck)
            {
                User.Teleport(SapdToInterrogationPos);
                User.SetVirtualWorld(1);
            }*/
            
            /*Bar*/
            /*if (Main.GetDistanceToSquared(BannanaOutPos, playerPos) < DistanceCheck)
                User.Teleport(BannanaInPos);
            if (Main.GetDistanceToSquared(BannanaInPos, playerPos) < DistanceCheck)
                User.Teleport(BannanaOutPos);*/
            
            if (Main.GetDistanceToSquared(AutoRepairsPos2, playerPos) < DistanceCheck)
                User.Teleport(AutoRepairsPos1);
            if (Main.GetDistanceToSquared(AutoRepairsPos1, playerPos) < DistanceCheck)
                User.Teleport(AutoRepairsPos2);
            
            if (Main.GetDistanceToSquared(ExoticCarsPos1, playerPos) < DistanceCheck)
                User.Teleport(ExoticCarsPos2);
            if (Main.GetDistanceToSquared(ExoticCarsPos2, playerPos) < DistanceCheck)
                User.Teleport(ExoticCarsPos1);
            
            /*Other*/
            if (Main.GetDistanceToSquared(Ems1OutPos, playerPos) < DistanceCheck)
                User.Teleport(Ems1InPos);
            if (Main.GetDistanceToSquared(Ems1InPos, playerPos) < DistanceCheck)
                User.Teleport(Ems1OutPos);
            
            if (Main.GetDistanceToSquared(WzlOutPos, playerPos) < DistanceCheck)
                User.Teleport(WzlInPos);
            if (Main.GetDistanceToSquared(WzlInPos, playerPos) < DistanceCheck)
                User.Teleport(WzlOutPos);
            
            /*Lic*/
            if (Main.GetDistanceToSquared(LicDownPos, playerPos) < DistanceCheck)
                User.Teleport(LicUpPos);
            
            if (Main.GetDistanceToSquared(LicUpPos, playerPos) < DistanceCheck)
                User.Teleport(LicDownPos);
            
            /*Meria*/
            if (Main.GetDistanceToSquared(MeriaUpPos, playerPos) < DistanceCheck ||
                Main.GetDistanceToSquared(MeriaDownPos, playerPos) < DistanceCheck ||
                Main.GetDistanceToSquared(MeriaRoofPos, playerPos) < DistanceCheck ||
                Main.GetDistanceToSquared(MeriaGarPos, playerPos) < DistanceCheck)
                MenuList.ShowMeriaTeleportMenu();
            
            /*Fib*/
            if (Main.GetDistanceToSquared(FibLift0StationPos, playerPos) < DistanceCheck ||
                Main.GetDistanceToSquared(FibLift1StationPos, playerPos) < DistanceCheck ||
                Main.GetDistanceToSquared(FibLift2StationPos, playerPos) < DistanceCheck ||
                Main.GetDistanceToSquared(FibLift3StationPos, playerPos) < DistanceCheck ||
                Main.GetDistanceToSquared(FibLift4StationPos, playerPos) < DistanceCheck)
                MenuList.ShowFibTeleportMenu();
            
            /*Business*/
            if (Main.GetDistanceToSquared(Business.Business.BusinessStreetPos, playerPos) < DistanceCheck ||
                Main.GetDistanceToSquared(Business.Business.BusinessMotorPos, playerPos) < DistanceCheck ||
                Main.GetDistanceToSquared(Business.Business.BusinessRoofPos, playerPos) < DistanceCheck ||
                Main.GetDistanceToSquared(Business.Business.BusinessGaragePos, playerPos) < DistanceCheck ||
                Main.GetDistanceToSquared(Business.Business.BusinessOfficePos, playerPos) < DistanceCheck)
                MenuList.ShowBusinessTeleportMenu();
            
            /*Builder*/
            if (Main.GetDistanceToSquared(BuilderUpPos, playerPos) < DistanceCheck)
                User.Teleport(BuilderDownPos);
            
            if (Main.GetDistanceToSquared(BuilderDownPos, playerPos) < DistanceCheck)
                User.Teleport(BuilderUpPos);
            
            /*Houses*/
            if (Main.GetDistanceToSquared(CondoPaletoTeleportPos1, playerPos) < DistanceCheck)
                User.Teleport(CondoPaletoTeleportPos2);
            if (Main.GetDistanceToSquared(CondoPaletoTeleportPos2, playerPos) < DistanceCheck)
                User.Teleport(CondoPaletoTeleportPos1);
            
            /*Houses*/
            if (Main.GetDistanceToSquared(H201TeleportPos1, playerPos) < DistanceCheck)
                User.Teleport(H201TeleportPos2);
            if (Main.GetDistanceToSquared(H201TeleportPos2, playerPos) < DistanceCheck)
                User.Teleport(H201TeleportPos1);
            
            if (Main.GetDistanceToSquared(H633TeleportPos2, playerPos) < DistanceCheck)
                User.Teleport(H633TeleportPos1);
            if (Main.GetDistanceToSquared(H633TeleportPos1, playerPos) < DistanceCheck)
                User.Teleport(H633TeleportPos2);
            
            if (Main.GetDistanceToSquared(H254TeleportPos2, playerPos) < DistanceCheck)
                User.Teleport(H254TeleportPos1);
            if (Main.GetDistanceToSquared(H254TeleportPos1, playerPos) < DistanceCheck)
                User.Teleport(H254TeleportPos2);
            //AHouses

            if (Main.ServerName == "Sombrero")
            {
                if (Main.GetDistanceToSquared(VanilaTeleportPos1, playerPos) < DistanceCheck)
                    User.Teleport(VanilaTeleportPos2);
                if (Main.GetDistanceToSquared(VanilaTeleportPos2, playerPos) < DistanceCheck)
                    User.Teleport(VanilaTeleportPos1);
            }

            if (Main.ServerName == "Andromeda")
            {
                if (Main.GetDistanceToSquared(HA813TeleportPos1, playerPos) < DistanceCheck)
                    MenuList.ShowHouseOutMenu(await House.GetAllData(813));
                if (Main.GetDistanceToSquared(HA813TeleportPos2, playerPos) < DistanceCheck)
                    MenuList.ShowHouseOutMenu(await House.GetAllData(813));
            }

            if (Main.ServerName == "MilkyWay")
            {
                if (Main.GetDistanceToSquared(MArcMotorsTeleportPos1, playerPos) < DistanceCheck)
                    User.Teleport(MArcMotorsTeleportPos2);
                if (Main.GetDistanceToSquared(MArcMotorsTeleportPos2, playerPos) < DistanceCheck)
                    User.Teleport(MArcMotorsTeleportPos1);
                
                if (Main.GetDistanceToSquared(MW_House1126TeleportPos1, playerPos) < DistanceCheck)
                    User.Teleport(MW_House1126TeleportPos2);
                if (Main.GetDistanceToSquared(MW_House1126TeleportPos2, playerPos) < DistanceCheck)
                    User.Teleport(MW_House1126TeleportPos1);
                
                if (Main.GetDistanceToSquared(MW_House1126TeleportPos3, playerPos) < DistanceCheck)
                    User.Teleport(MW_House1126TeleportPos4);
                if (Main.GetDistanceToSquared(MW_House1126TeleportPos4, playerPos) < DistanceCheck)
                    User.Teleport(MW_House1126TeleportPos3);
                
                if (Main.GetDistanceToSquared(MW_House1126TeleportPos5, playerPos) < DistanceCheck)
                    User.Teleport(MW_House1126TeleportPos6);
                if (Main.GetDistanceToSquared(MW_House1126TeleportPos6, playerPos) < DistanceCheck)
                    User.Teleport(MW_House1126TeleportPos5);
                
                if (Main.GetDistanceToSquared(MW635TeleportPos1, playerPos) < DistanceCheck)
                    User.Teleport(MW635TeleportPos2);
                if (Main.GetDistanceToSquared(MW635TeleportPos2, playerPos) < DistanceCheck)
                    User.Teleport(MW635TeleportPos1);
                
                if (Main.GetDistanceToSquared(HM148TeleportPos1, playerPos) < DistanceCheck)
                    User.Teleport(HM148TeleportPos2);
                if (Main.GetDistanceToSquared(HM148TeleportPos2, playerPos) < DistanceCheck)
                    User.Teleport(HM148TeleportPos1);
                if (Main.GetDistanceToSquared(HM148TeleportPos11, playerPos) < DistanceCheck)
                    User.Teleport(HM148TeleportPos22);
                if (Main.GetDistanceToSquared(HM148TeleportPos22, playerPos) < DistanceCheck)
                    User.Teleport(HM148TeleportPos11);
            }
            
            if (Main.GetDistanceToSquared(InvaderPos1, playerPos) < DistanceCheck)
                User.Teleport(InvaderPos2);
            if (Main.GetDistanceToSquared(InvaderPos2, playerPos) < DistanceCheck)
                User.Teleport(InvaderPos1);
        }
        
        public static async void CheckPlayerPosToPickupPressE()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            
            for (int i = 0; i < SapdCyberPcPos.Length / 4; i++)
            {
                Vector3 gumPos = new Vector3((float) SapdCyberPcPos[i, 0], (float) SapdCyberPcPos[i, 1], (float) SapdCyberPcPos[i, 2]);
                if (Main.GetDistanceToSquared(playerPos, gumPos) > 1.2f) continue;
                
                User.PedRotation((float) SapdCyberPcPos[i, 3]);
                SetEntityCoords(GetPlayerPed(-1), gumPos.X, gumPos.Y, gumPos.Z, true, false, false, true);
                await Delay(1000);
                User.PlayScenario("PROP_HUMAN_SEAT_BENCH");
                int pass = Convert.ToInt32(await Menu.GetUserInput("Введите пароль", null, 4));
                if (pass == (int) await Client.Sync.Data.Get(-9999, "sapdPass"))
                    MenuList.ShowSapdCyberPcMenu();
                else
                    Notification.SendWithTime("~r~Неверный пароль");
                return;
            }
            
            for (int i = 0; i < HackerSpacePcPos.Length / 4; i++)
            {
                Vector3 gumPos = new Vector3((float) HackerSpacePcPos[i, 0], (float) HackerSpacePcPos[i, 1], (float) HackerSpacePcPos[i, 2]);
                if (Main.GetDistanceToSquared(playerPos, gumPos) > 1.2f) continue;
                
                User.PedRotation((float) HackerSpacePcPos[i, 3]);
                SetEntityCoords(GetPlayerPed(-1), gumPos.X, gumPos.Y, gumPos.Z, true, false, false, true);
                await Delay(1000);
                User.PlayScenario("PROP_HUMAN_SEAT_BENCH");
                MenuList.ShowHackerSpacePcMenu();
                return;
            }
            
            for (int i = 0; i < Main.KitchenList.Length / 4; i++)
            {
                Vector3 shopPos = new Vector3((float) Main.KitchenList[i, 0], (float) Main.KitchenList[i, 1], (float) Main.KitchenList[i, 2]);
                if (Main.GetDistanceToSquared(playerPos, shopPos) > 1.2f) continue;
                MenuList.ShowKitchenMenu(Main.GetKitchenId(true), Convert.ToInt32(Main.KitchenList[i, 3]));
                return;
            }
            
            /*Help*/
            if (Main.GetDistanceToSquared(StartHelpPos, playerPos) < DistanceCheck)
                MenuList.ShowTaxiAskGetTaxiMenu();
            
            /*HackerSpace Shop*/
            if (Main.GetDistanceToSquared(HackerSpaceShopPos, playerPos) < DistanceCheck)
                MenuList.ShowHackerPhoneBuyMenu();
            
            /*Welding Shop*/
            if(Main.GetDistanceToSquared(WeldingToolShop, playerPos) < DistanceCheck)
                MenuList.BuyGasWelder("Торговец", "Что желаешь купить?");
            
            /*Invader*/
            if (Main.GetDistanceToSquared(LifeInvaderShopPos, playerPos) < DistanceCheck)
                MenuList.ShowSetPhoneNumberMenu(92);
            
            /*RoadWorker*/
            if (Main.GetDistanceToSquared(RoadWorkerStartPos, playerPos) < DistanceCheck)
                MenuList.ShowJobRoadWorkerMenu();
            
            /*Builder*/
            if (Main.GetDistanceToSquared(BuilderStartPos, playerPos) < DistanceCheck)
                MenuList.ShowJobBuilderMenu();
            /*Ocean Cleaner*/
            if (Main.GetDistanceToSquared(OceanStartPos, playerPos) < DistanceCheck)
                MenuList.ShowJobOceanMenu();
            
            /*Jail Job*/
            if (Main.GetDistanceToSquared(JailJobStartPos, playerPos) < DistanceCheck)
                MenuList.ShowJobJailTeleportMenu();
            if (Main.GetDistanceToSquared(JailCareerJobEndPos, playerPos) < DistanceCheck)
                MenuList.ShowJobJailMenu();

            /*Cleaner*/
            if (Main.GetDistanceToSquared(CleanerStartPos, playerPos) < DistanceCheck)
                MenuList.ShowJobCleanderMenu();

            /*Lic*/
            if (Main.GetDistanceToSquared(LicBuyPos, playerPos) < DistanceCheck)
                MenuList.ShowLicBuyMenu();

            /*Business*/
            if (Main.GetDistanceToSquared(Business.Business.BusinessBotPos, playerPos) < DistanceCheck)
                MenuList.ShowBusinessMenu();
            
            /*Meria*/
            if (Main.GetDistanceToSquared(MeriaHelpPos, playerPos) < DistanceCheck)
                MenuList.ShowMeriaMainMenu();
            
            if (Main.GetDistanceToSquared(MeriaGarderobPos, playerPos) < DistanceCheck && User.IsGov())
                MenuList.ShowMeriaGarderobMenu();
                
            if (Main.GetDistanceToSquared(GovKeyPos, playerPos) < DistanceCheck)
                MenuList.ShowFractionKeyMenu("Правительство", "Транспорт правительства");
                
            if (Main.GetDistanceToSquared(EmsNews, playerPos) < DistanceCheck)
                MenuList.EmsNewsMenu();
            if (Main.GetDistanceToSquared(SapdNews, playerPos) < DistanceCheck)
                MenuList.SapdNewsMenu();
            if (Main.GetDistanceToSquared(SheriffNews, playerPos) < DistanceCheck)
                MenuList.SheriffNewsMenu();
            if (Main.GetDistanceToSquared(GovLicense, playerPos) < DistanceCheck)
                MenuList.GovLicenseMenu();
            if (Main.GetDistanceToSquared(GovNews, playerPos) < DistanceCheck)
                MenuList.GovNewsMenu();
            if (Main.GetDistanceToSquared(GovCoffer, playerPos) < DistanceCheck)
                MenuList.GovCofferMenu();
            
           /*EMS*/
            
            if (User.IsEms())
            {
                if (Main.GetDistanceToSquared(EmsDuty1Pos, playerPos) < DistanceCheck || Main.GetDistanceToSquared(EmsDuty2Pos, playerPos) < DistanceCheck)
                {
                    if (User.IsDuty())
                    {
                        Fractions.Ems.Garderob(0);
                        Notification.SendWithTime("~b~Вы ушли с дежурства");
                        Client.Sync.Data.ResetLocally(User.GetServerId(), "duty");
                    }
                    else
                    {
                        Notification.SendWithTime("~b~Вы вышли на дежурство");
                        Client.Sync.Data.SetLocally(User.GetServerId(), "duty", true);
                    }
                }
                
                if (User.IsDuty())
                {
                    if (Main.GetDistanceToSquared(Ems1KeyPos, playerPos) < DistanceCheck)
                        MenuList.ShowFractionKeyMenu("EMS", "Транспорт EMS");
                    if (Main.GetDistanceToSquared(Ems2KeyPos, playerPos) < DistanceCheck)
                        MenuList.ShowFractionKeyMenu("EMS", "Транспорт EMS");
                    
                    if (Main.GetDistanceToSquared(EmsFireGarderobPos, playerPos) < DistanceCheck)
                        MenuList.ShowEmsFIreGarderobMenu();
                    if (Main.GetDistanceToSquared(EmsGarderobPos, playerPos) < DistanceCheck)
                        MenuList.ShowEmsGarderobMenu();
                    //if (Main.GetDistanceToSquared(EmsTakeMedPos, playerPos) < DistanceCheck)
                    //    MenuList.ShowEmsArsenalMenu();
                    
                    if (Main.GetDistanceToSquared(EmsLicensePos, playerPos) < DistanceCheck)
                        MenuList.ShowEmsLicenseMenu();
                    
                    if (Main.GetDistanceToSquared(EmsPsyLicPos, playerPos) < DistanceCheck)
                        MenuList.ShowEms2LicenseMenu();
                    
                    
                }
            }
                
            /*if (Main.GetDistanceToSquared(EmsAptekaPos, playerPos) < DistanceCheck)
                MenuList.ShowEmsAptekaMenu();*/
                
            if (Main.GetDistanceToSquared(AutoRepairsPosShop, playerPos) < DistanceCheck)
                MenuList.ShowAutoRepairShopMenu(125);
            if (Main.GetDistanceToSquared(AutoRepairsPosCarShop, playerPos) < DistanceCheck)
                MenuList.ShowAutoRepairCarListShopMenu(125);
            if (Main.GetDistanceToSquared(ExoticCarsPosShop, playerPos) < DistanceCheck)
                MenuList.ShowAutoRepairShopMenu(87);
            if (Main.GetDistanceToSquared(ExoticCarsPosCarShop, playerPos) < DistanceCheck)
                MenuList.ShowAutoRepairCarListShopMenu(87);
            if (Main.GetDistanceToSquared(EatPrisonPos, playerPos) < DistanceCheck)
            {
                User.SetWaterLevel(100);
                User.SetEatLevel(1000);
                Chat.SendMeCommand("поел");
            }
            
            if (Main.GetDistanceToSquared(AptekaPos, playerPos) < DistanceCheck)
                MenuList.ShowAptekaMenu(124);
                
            if (Main.GetDistanceToSquared(Apteka1Pos, playerPos) < DistanceCheck)
                MenuList.ShowAptekaMenu(154);
                
            if (Main.GetDistanceToSquared(Apteka2Pos, playerPos) < DistanceCheck)
                MenuList.ShowAptekaMenu(157);
                
            if (Main.GetDistanceToSquared(Apteka3Pos, playerPos) < DistanceCheck)
                MenuList.ShowAptekaMenu(158);
            
            if (Main.GetDistanceToSquared(EmsHealPos, playerPos) < DistanceCheck)
                MenuList.ShowHealMenu();
            
            /*SAPD*/
            if (User.IsSapd())
            {
                if (Main.GetDistanceToSquared(SapdDutyPos, playerPos) < DistanceCheck)
                {
                    if (User.IsDuty())
                    {
                        Fractions.Sapd.Garderob(0);
                        Notification.SendWithTime("~b~Вы ушли с дежурства");
                        Client.Sync.Data.ResetLocally(User.GetServerId(), "duty");
                    }
                    else
                    {
                        Notification.SendWithTime("~b~Вы вышли на дежурство");
                        Client.Sync.Data.SetLocally(User.GetServerId(), "duty", true);
                    }
                }

                if (User.IsDuty())
                {
                    if (Main.GetDistanceToSquared(SapdGarderobPos, playerPos) < DistanceCheck)
                        MenuList.ShowSapdGarderobMenu();
                    
                    if (Main.GetDistanceToSquared(SapdArsenalPos, playerPos) < DistanceCheck)
                        MenuList.ShowSapdArsenalMenu();
                    
                    if (Main.GetDistanceToSquared(SapdClearPosf, playerPos) < DistanceCheck)
                        MenuList.ShowSapdClearOrUnjailMenu();
                    
                    if (Main.GetDistanceToSquared(SapdArrestPos, playerPos) < DistanceCheck)
                        MenuList.ShowSapdArrestMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 5f));
                    
                    if (Main.GetDistanceToSquared(ArrestPosPrison, playerPos) < DistanceCheck)
                        MenuList.ShowSapdArrestMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 5f));
                    
                    if (Main.GetDistanceToSquared(SapdKeyPos, playerPos) < DistanceCheck)
                        MenuList.ShowFractionKeyMenu("SAPD", "Транспорт SAPD");
                    
                    if (Main.GetDistanceToSquared(SapdLicense, playerPos) < DistanceCheck)
                        MenuList.ShowSapdLicenseMenu();
                }
            }
            
            if (User.IsSheriff())
            {
                if (Main.GetDistanceToSquared(SheriffGarderobPos, playerPos) < DistanceCheck)
                    MenuList.ShowSheriffGarderobMenu();
                if (Main.GetDistanceToSquared(SheriffGarderobPos2, playerPos) < DistanceCheck)
                    MenuList.ShowSheriffGarderobMenu();
                    
                if (Main.GetDistanceToSquared(SheriffArsenalPos, playerPos) < DistanceCheck)
                    MenuList.ShowSapdArsenalMenu();
                if (Main.GetDistanceToSquared(SheriffArsenalPos2, playerPos) < DistanceCheck)
                    MenuList.ShowSapdArsenalMenu();
                    
                if (Main.GetDistanceToSquared(SheriffClearPos, playerPos) < DistanceCheck)
                    MenuList.ShowSapdClearOrUnjailMenu();
                    
                if (Main.GetDistanceToSquared(SheriffArrestPos, playerPos) < DistanceCheck)
                    MenuList.ShowSapdArrestMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 5f));
                    
                if (Main.GetDistanceToSquared(SheriffKeyPos, playerPos) < DistanceCheck)
                    MenuList.ShowFractionKeyMenu("Sheriff", "Транспорт Sheriff");

                if (Main.GetDistanceToSquared(SheriffLicense, playerPos) < DistanceCheck)
                    MenuList.ShowLicenseMenu();
                
            }
            
            
            /*Fib*/
            if (User.IsFib())
            {
                if (Main.GetDistanceToSquared(FibDutyPos, playerPos) < DistanceCheck)
                {
                    if (User.IsDuty())
                    {
                        Notification.SendWithTime("~b~Вы ушли с дежурства");
                        Client.Sync.Data.ResetLocally(User.GetServerId(), "duty");
                    }
                    else
                    {
                        Notification.SendWithTime("~b~Вы вышли на дежурство");
                        Client.Sync.Data.SetLocally(User.GetServerId(), "duty", true);
                    }
                }

                if (Main.GetDistanceToSquared(FibArsenalPos, playerPos) < DistanceCheck && User.IsDuty())
                    MenuList.ShowFibArsenalMenu();
                
                if (Main.GetDistanceToSquared(FibKeyPos, playerPos) < DistanceCheck)
                    MenuList.ShowFractionKeyMenu("FIB", "Транспорт FIB");
            }
            
            /*Mask*/
            /*if (Main.GetDistanceToSquared(OtmDengPos, playerPos) < DistanceCheck)
            {
                if (Client.Sync.Data.HasLocally(User.GetServerId(), "GrabCash"))
                {
                    int money = (int) Client.Sync.Data.GetLocally(User.GetServerId(), "GrabCash") * User.Bonus;
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "GrabCash");
                    Notification.SendWithTime("~g~Награбленно: $" + money);
                    User.AddCashMoney(money);
                }
            }*/
            
            /*Mask*/
            if (Main.GetDistanceToSquared(ClothMaskPos, playerPos) < DistanceCheck)
                MenuList.ShowShopMaskMenu(74);
            
            /*Mask*/
            if (Main.GetDistanceToSquared(MedvedevBarPos, playerPos) < DistanceCheck)
                MenuList.ShowBarFreeMenu();
            if (Main.GetDistanceToSquared(H1126BarPos, playerPos) < DistanceCheck)
                MenuList.ShowBarFreeMenu();
            if (Main.GetDistanceToSquared(SH1139BarPos, playerPos) < DistanceCheck)
                MenuList.ShowBarFreeMenu();
            if (Main.GetDistanceToSquared(AHouse254BarPos, playerPos) < DistanceCheck)
                MenuList.ShowBarFreeMenu();
            
            
           /*Guns*/
           if ((User.IsSapd() || User.IsFib() || User.IsSheriff()) && Main.GetDistanceToSquared(StockSapdPos, playerPos) < DistanceCheck)
                Inventory.GetItemList(2, InventoryTypes.StockGang);
            
            /*Keys*/
            if (User.IsCartel() && Main.GetDistanceToSquared(CartelKeyPos, playerPos) < DistanceCheck)
                MenuList.ShowFractionKeyMenu("Картель", "Транспорт картеля");
            
            if (User.IsJobTrash() && Main.GetDistanceToSquared(TrashKeyPos, playerPos) < DistanceCheck)
                MenuList.ShowJobKeyMenu("Работа", "Ключи рабочего транспорта");
            
            if (User.IsJobBus1() && Main.GetDistanceToSquared(Bus1KeyPos, playerPos) < DistanceCheck)
                MenuList.ShowJobKeyMenu("Работа", "Ключи рабочего транспорта");
            
            if (User.IsJobBus2() && Main.GetDistanceToSquared(Bus2KeyPos, playerPos) < DistanceCheck)
                MenuList.ShowJobKeyMenu("Работа", "Ключи рабочего транспорта");
            
            if (User.IsJobBus3() && Main.GetDistanceToSquared(Bus3KeyPos, playerPos) < DistanceCheck)
                MenuList.ShowJobKeyMenu("Работа", "Ключи рабочего транспорта");
            
            if (User.IsJobSunb() && Main.GetDistanceToSquared(SunbKeyPos, playerPos) < DistanceCheck)
                MenuList.ShowJobKeyMenu("Работа", "Ключи рабочего транспорта");
            
            if (User.IsJobLab() && Main.GetDistanceToSquared(LabKeyPos, playerPos) < DistanceCheck)
                MenuList.ShowJobKeyMenu("Работа", "Ключи рабочего транспорта");
            
            if (User.IsJobConnor() && Main.GetDistanceToSquared(ConnorKeyPos, playerPos) < DistanceCheck)
                MenuList.ShowJobKeyMenu("Работа", "Ключи рабочего транспорта");
            
            if (User.IsJobBgstar() && Main.GetDistanceToSquared(BgstarKeyPos, playerPos) < DistanceCheck)
                MenuList.ShowJobKeyMenu("Работа", "Ключи рабочего транспорта");
            
            if (User.IsJobWap() && Main.GetDistanceToSquared(WapKeyPos, playerPos) < DistanceCheck)
                MenuList.ShowJobKeyMenu("Работа", "Ключи рабочего транспорта");
            
            if (User.IsJobScrap() && Main.GetDistanceToSquared(ScrapKeyPos, playerPos) < DistanceCheck)
                MenuList.ShowJobKeyMenu("Работа", "Ключи рабочего транспорта");
            
            if (Main.GetDistanceToSquared(PhotoKeyPos, playerPos) < DistanceCheck)
                MenuList.ShowRentCarMenu(92);
            
            if (User.IsJobMail() && Main.GetDistanceToSquared(Mail1KeyPos, playerPos) < DistanceCheck)
                MenuList.ShowJobKeyMenu("Работа", "Ключи рабочего транспорта");
            
            if (User.IsJobMail() && Main.GetDistanceToSquared(Mail2KeyPos, playerPos) < DistanceCheck)
                MenuList.ShowJobKeyMenu("Работа", "Ключи рабочего транспорта");
            
            if(User.IsJobGroupSix() && Main.GetDistanceToSquared(GroupSixKeyPos, playerPos) < DistanceCheck)
               MenuList.ShowJobKeyMenu("Работа", "Ключи рабочего транспорта");
        }
    }
}