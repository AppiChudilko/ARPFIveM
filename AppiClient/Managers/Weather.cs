using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using CitizenFX.Core;
using NativeUI;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Weather : BaseScript
    {
        public static int Day = 0;
        public static int Month = 0;
        public static int Year = 0;
        public static int Hour = 0;
        public static int Min = 0;
        public static int Sec = 0;
        public static float Temp = 27;
        public static float TempServer = 27;
        public static string DayName = "Понедельник";
        
        public static int RealHour = 0;
        public static string FullRealDateTime = "";
        
        public static string CurrentWeather = "CLEAR";
        
        private static readonly string[] DayNames = {"Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота"};
        
        public Weather()
        {
            EventHandlers.Add("ARP:SyncDateTime", new Action<int, int, int, int, int>(DateTime));
            EventHandlers.Add("ARP:SyncRealDateTime", new Action<int>(RealDateTime));
            EventHandlers.Add("ARP:SyncRealFullDateTime", new Action<string>(SyncRealFullDateTime));
            EventHandlers.Add("ARP:SyncWeatherTemp", new Action<float>(SyncWeatherTemp));
            
            EventHandlers.Add("ARP:SetCurrentWeather", new Action<string>(SetCurrentWeather));
            EventHandlers.Add("ARP:NextWeather", new Action<string, int>(NextWeather));
            
            Tick += TimeSecSync;
            Tick += TimeSync;
            Tick += PhoneSync;
        }

        public static int ReplaceSummerToWinterSkin(Model ped)
        {
            switch (ped.Hash)
            {
                case -257153498:
                    return -680474188;
                case 2014052797:
                    return -1606864033;
                case 1250841910:
                    return 1546450936;
                case 189425762:
                    return 1720428295;
                case 808859815:
                    return 1633872967;
                case -945854168:
                    return 1146800212;
                case 1077785853:
                    return 1984382277;
                case 2021631368:
                    return -1111799518;
                case 600300561:
                    return -1660909656;
                case -408329255:
                    return -1567723049;
                case 1004114196:
                    return 894928436;
                case -1244692252:
                    return 373000027;
                case -88831029:
                    return -1452399100;
                case 951767867:
                    return 744758650;
                case 1165780219:
                    return 1500695792;
                case 331645324:
                    return 1126998116;
                case -1519253631:
                    return -20018299;
                case 115168927:
                    return 411102470;
                case 793439294:
                    return -1249041111;
                case 2111372120:
                    return -1160266880;
                case -961242577:
                    return -396800478;
                case 813893651:
                    return 826475330;
                case 1358380044:
                    return -1280051738;
                case 343259175:
                    return 1546450936;
                case 2097407511:
                    return 68070371;
                case 767028979:
                    return -900269486;
                case 1490458366:
                    return 1702441027;
                case 1264920838:
                    return -781039234;
                case -920443780:
                    return -1105135100;
                case 1596003233:
                    return 2073775040;
                case 623927022:
                    return -2076336881;
                case  -1852518909:
                    return -1007618204;
                case -356333586:
                    return 1750583735;
                case -1661836925:
                    return 826475330;
                case -1859912896:
                    return -1211756494;
            }
            return ped.Hash;
        }

        public static async void NextWeather(string weather, int delay)
        {
            Debug.WriteLine($"NEXT WEATHER: {weather} {delay}");
            /*"EXTRASUNNY",
            "CLEAR",
            "CLOUDS",
            "SMOG",
            "FOGGY",
            "OVERCAST",
            "RAIN",
            "THUNDER",
            "CLEARING",
            "XMAS"*/

            int weatherId = 0;

            switch (weather)
            {
                case "CLEAR":
                    weatherId = 1;
                    break;
                case "CLOUDS":
                    weatherId = 2;
                    break;
                case "SMOG":
                    weatherId = 3;
                    break;
                case "FOGGY":
                    weatherId = 4;
                    break;
                case "OVERCAST":
                    weatherId = 5;
                    break;
                case "RAIN":
                    weatherId = 6;
                    break;
                case "THUNDER":
                    weatherId = 7;
                    break;
                case "CLEARING":
                    weatherId = 8;
                    break;
                case "XMAS":
                    weatherId = 13;
                    break;
            }

            CitizenFX.Core.World.TransitionToWeather((CitizenFX.Core.Weather) weatherId, delay + 30);
            CurrentWeather = weather;

            //await Delay(delay + 29);
            //SetCurrentWeather(weather);
        }

        public static async void SetCurrentWeather(string weather)
        {   
            /*SetWeatherTypePersist(weather);
            SetWeatherTypeNowPersist(weather);
            SetWeatherTypeNow(weather);
            SetOverrideWeather(weather);*/
            int weatherId = 0;

            switch (weather)
            {
                case "CLEAR":
                    weatherId = 1;
                    break;
                case "CLOUDS":
                    weatherId = 2;
                    break;
                case "SMOG":
                    weatherId = 3;
                    break;
                case "FOGGY":
                    weatherId = 4;
                    break;
                case "OVERCAST":
                    weatherId = 5;
                    break;
                case "RAIN":
                    weatherId = 6;
                    break;
                case "THUNDER":
                    weatherId = 7;
                    break;
                case "CLEARING":
                    weatherId = 8;
                    break;
                case "XMAS":
                    weatherId = 13;
                    break;
            }
            
            World.TransitionToWeather((CitizenFX.Core.Weather) weatherId, 1);
            
            CurrentWeather = weather;
            Debug.WriteLine($"SET CURRENT WEATHER: {weather}");

            if (
                weather == "XMAS" ||
                weather == "SNOWLIGHT" ||
                weather == "BLIZZARD" ||
                weather == "SNOW"
            )
            {
                SetForceVehicleTrails(true);
                SetForcePedFootstepsTracks(true);

                RequestScriptAudioBank("ICE_FOOTSTEPS", false);
                RequestScriptAudioBank("SNOW_FOOTSTEPS", false);

                //N_0xc54a08c85ae4d410(3.0f);

                /*RequestNamedPtfxAsset("core_snow");
                while (!HasNamedPtfxAssetLoaded("core_snow"))
                    await Delay(10);
                UseParticleFxAssetNextCall("core_snow");*/
            }
            else
            {
                ReleaseNamedScriptAudioBank("ICE_FOOTSTEPS");
                ReleaseNamedScriptAudioBank("SNOW_FOOTSTEPS");
                
                SetForceVehicleTrails(false);
                SetForcePedFootstepsTracks(false);
                //RemoveNamedPtfxAsset("core_snow");
                //N_0xc54a08c85ae4d410(0.0f);
            }
        }
        
        public static void DateTime(int min, int hour, int day, int month, int year)
        {   
            var getDayName = new DateTime(year, month, day);
            DayName = DayNames[getDayName.DayOfWeek.GetHashCode()];

            Day = day;
            Month = month;
            Year = year;
            Hour = hour;
            Min = min;
            Sec = 0;
            
            NetworkOverrideClockTime(hour, min, Sec);
            
            SetClockDate(day, month, year);
            SetClockTime(hour, min, Sec);
        }

        public static void RealDateTime(int hour)
        {
            RealHour = hour;
        }

        public static void SyncRealFullDateTime(string dateTime)
        {
            FullRealDateTime = dateTime;
        }

        public static void SyncWeatherTemp(float temp)
        {
            Temp = temp;
            TempServer = temp;
        }
        
        private static async Task TimeSecSync()
        {
            await Delay(141);
            Sec++;
            if (Sec >= 59)
                Sec = 59;
        }
        
        private static async Task TimeSync()
        {
            await Delay(100);
            NetworkOverrideClockTime(Hour, Min, Sec);
        }
        
        private static async Task PhoneSync()
        {
            await Delay(500);
            TriggerEvent("ARPPhone:UpdateValues", DayName, $"{Day:D2}/{Month:D2}/{Year}", $"{Hour:D2}:{Min:D2}", $"{Temp}", $"{User.Data.phone_code}", $"{User.Data.bank_prefix}", $"{User.GetNetwork()}");
            
            /*PointF safe = UIMenu.GetSafezoneBounds();
            if (IsPedInAnyVehicle(PlayerPedId(), true))
            {
                var v = new CitizenFX.Core.Vehicle(GetVehiclePedIsUsing(PlayerPedId()));
                var fuelIndicator = 100;                
                var vehId = Vehicle.GetVehicleIdByNumber(Vehicle.GetVehicleNumber(v.Handle));
                if (vehId != -1)
                    fuelIndicator = Convert.ToInt32((Vehicle.VehicleInfoGlobalDataList[vehId].Fuel / Vehicle.VehicleInfoGlobalDataList[vehId].FullFuel) * 100);
                TriggerEvent("ARPRadar:UpdateInfo", CitizenFX.Core.UI.Screen.Hud.IsVisible, safe.X, safe.Y, true, UI.GetCurrentSpeed(), Convert.ToInt32(v.CurrentRPM*100), fuelIndicator);
            }
            else
                TriggerEvent("ARPRadar:UpdateInfo", CitizenFX.Core.UI.Screen.Hud.IsVisible, safe.X, safe.Y, false, 0, 0, 0);*/
            
        }
    }
}