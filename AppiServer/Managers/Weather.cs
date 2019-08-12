using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Server.Managers
{
    public class Weather : BaseScript
    {
        private static int _year = 2016;
        private static int _month = 1;
        private static int _day = 1;
        private static int _hour = 12;
        private static int _minute = 0;
        private static double _tempNew = 27;
        private static int _weatherType = 0;
        private static bool _isSnow = false;

        private static string _weather = "";
        
        public Weather()
        {
            Tick += WeatherSync;
            Tick += TimeSync;
            Tick += Min5Timer;
            Tick += Min30Timer;
            Tick += RandomTimer;

            foreach (DataRow row in Appi.MySql.ExecuteQueryWithResult("SELECT * FROM daynight WHERE id = '1'").Rows)
            {
                _year = (int) row["year"];
                _month = (int) row["month"];
                _day = (int) row["day"];
                _hour = (int) row["hour"];
                _minute = (int) row["minute"];
            }

            var rand = new Random();
            if (_month < 2 || _month > 11) //Зима
            {
                _tempNew = rand.Next(4) * -1;
                _weatherType = 0;
            }
            else if (_month == 2) //Зима
            {
                _tempNew = rand.Next(4) + 8;
                _weatherType = 0;
            }
            else if (_month >= 3 && _month <= 5) //Весна
            {
                _tempNew = rand.Next(4) + 16;
                _weatherType = 1;
            }
            else if (_month >= 6 && _month <= 9) //Лето
            {
                _tempNew = rand.Next(4) + 25;
                _weatherType = 2;
            }
            else //Осень
            {
                _tempNew = rand.Next(4) + 16;
                _weatherType = 3;
            }
            
            NextRandomWeatherByType(_weatherType);
            
            Debug.WriteLine($"WEATHER MANAGER: {GetWeather()} | {_tempNew} | {GetRpDateTime()}");
        }
        
        public static string GetRpDateTime()
        {
            return $"{_hour:D2}:{_minute:D2}, {_day:D2}/{_month:D2}/{_year}";
        }

        public static void SetPlayerCurrentWeather(Player player)
        {
            player.TriggerEvent("ARP:SetCurrentWeather", GetWeather());
        }

        public static void NextRandomWeather()
        {
            NextRandomWeatherByType(GetWeatherType());
        }

        public static void NextRandomWeatherByType(int weatherType)
        {
            var weatherList = new List<string>
            {
                "EXTRASUNNY",
                "CLEAR",
                "CLOUDS",
                "SMOG",
                "FOGGY",
                "OVERCAST",
                "RAIN",
                "THUNDER",
                "CLEARING",
                "XMAS"
            };
            
            switch (weatherType)
            {
                case 0:
                    
                    weatherList = new List<string>
                    {
                        "EXTRASUNNY",
                        "CLOUDS",
                        "CLOUDS",
                        "SMOG",
                        "SMOG",
                        "FOGGY",
                        "FOGGY",
                        "OVERCAST",
                        "OVERCAST"
                    };
                    
                    if (weatherType == 0)
                        if (_tempNew < 1)
                            weatherList = new List<string> { "XMAS" };
                    
                    break;
                case 1:
                    weatherList = new List<string>
                    {
                        "EXTRASUNNY",
                        "EXTRASUNNY",
                        "EXTRASUNNY",
                        "EXTRASUNNY",
                        "EXTRASUNNY",
                        "EXTRASUNNY",
                        "CLEAR",
                        "CLEAR",
                        "CLEAR",
                        "CLEAR",
                        "CLEAR",
                        "CLOUDS",
                        "CLOUDS",
                        "CLOUDS",
                        "CLOUDS",
                        "CLOUDS",
                        "SMOG",
                        "SMOG",
                        "SMOG",
                        "FOGGY",
                        "FOGGY",
                        "FOGGY",
                        "OVERCAST",
                        "OVERCAST",
                        "OVERCAST",
                        "RAIN",
                        "THUNDER",
                        "CLEARING"
                    };
                    break;
                case 3:
                    weatherList = new List<string>
                    {
                        "CLOUDS",
                        "CLOUDS",
                        "CLOUDS",
                        "CLOUDS",
                        "SMOG",
                        "FOGGY",
                        "OVERCAST",
                        "RAIN",
                        "THUNDER",
                        "CLEARING"
                    };
                    break;
                case 2:
                    weatherList = new List<string>
                    {
                        "EXTRASUNNY",
                        "EXTRASUNNY",
                        "EXTRASUNNY",
                        "EXTRASUNNY",
                        "EXTRASUNNY",
                        "EXTRASUNNY",
                        "CLEAR",
                        "CLEAR",
                        "CLEAR",
                        "CLEAR",
                        "CLEAR",
                        "CLOUDS",
                        "CLOUDS",
                        "CLOUDS",
                        "CLOUDS",
                        "CLOUDS",
                        "SMOG",
                        "SMOG",
                        "SMOG",
                        "FOGGY",
                        "FOGGY",
                        "FOGGY",
                        "OVERCAST",
                        "OVERCAST",
                        "OVERCAST",
                        "RAIN",
                        "THUNDER",
                        "CLEARING"
                    };
                    break;
            }
            
            var rand = new Random();
            SetWeather(weatherList[rand.Next(weatherList.Count)]);

            if (_hour > 4 && _hour < 7)
            {
                switch (GetWeather())
                {
                    case "EXTRASUNNY":
                    case "CLEAR":
                    case "CLOUDS":
                        SetWeather("FOGGY");
                        break;
                }
            }

            if (_hour > 20)
            {
                switch (GetWeather())
                {
                    case "EXTRASUNNY":
                    case "CLEAR":
                    case "CLOUDS":
                        SetWeather("SMOG");
                        break;
                }
            }
                
            TriggerClientEvent("ARP:NextWeather", GetWeather(), rand.Next(240));
        }
        
        public static void SetWeather(string weather)
        {
            var rand = new Random();
            if (weather == "RAIN" || weather == "THUNDER" || weather == "CLEARING")
            {
                if (rand.Next(0, 3) == 0)
                    NextRandomWeather();
            }
            _weather = weather;
        }
        
        public static string GetWeather()
        {
            return _weather;
        }
        
        public static int GetWeatherType()
        {
            return _weatherType;
        }

        private static async Task WeatherSync()
        {
            await Delay(60000);
        }

        private static async Task TimeSync()
        {
            await Delay(8500);
            
            _minute++;
            if (_minute > 59)
            {
                _minute = 0;
                _hour++;
                
                TriggerClientEvent("ARP:PayDay");

                if (_hour > 23)
                {
                    _hour = 0;
                    _day++;

                    if (_day > DateTime.DaysInMonth(_year, _month))
                    {
                        _day = 1;
                        _month++;

                        if (_month > 12)
                        {
                            _month = 1;
                            _year++;
                        }
                    }
                    
                    for (int i = 0; i < 1300; i++)
                    {
                        if (Server.Sync.Data.Has(i, "isMail"))
                            Server.Sync.Data.Reset(i, "isMail");
                    }
                }
            }
            
            TriggerClientEvent("ARP:SyncDateTime", _minute, _hour, _day, _month, _year);
            TriggerClientEvent("ARP:SyncRealDateTime", DateTime.Now.Hour);
            TriggerClientEvent("ARP:SyncWeatherTemp", Math.Round(_tempNew, 1));
            TriggerClientEvent("ARP:SyncRealFullDateTime", $"{DateTime.Now:dd/MM} {DateTime.Now:HH:mm}");
        }
        
        private static async Task Min5Timer()
        {
            await Delay(1000 * 60 * 5);
            Appi.MySql.ExecuteQuery("UPDATE daynight SET  year = '" + _year + "', month = '" + _month + "', day = '" + _day + "', hour = '" + _hour + "', minute = '" + _minute + "' where id = '1'");
        }

        private static async Task RandomTimer()
        {
            var rand = new Random();   
            await Delay(1000 * 60 * 10 + rand.Next(5, 25));
            NextRandomWeather();
        }

        private static async Task Min30Timer()
        {
            await Delay(1000 * 60 * 30);
            
            var rand = new Random();

            switch (_weatherType)
            {
                case 0:
                    if (_hour > 1 && _hour <= 6)
                        _tempNew = _tempNew - (rand.NextDouble() + 2);
                    else if (_hour > 6 && _hour <= 12)
                        _tempNew = _tempNew + rand.NextDouble();
                    else if (_hour > 12 && _hour <= 16)
                        _tempNew = _tempNew + (rand.NextDouble() + 1);
                    else if (_hour > 16 && _hour <= 20)
                        _tempNew = _tempNew + rand.NextDouble();
                    else if (_hour > 20 && _hour <= 23)
                        _tempNew = _tempNew + rand.NextDouble();
                    else 
                        _tempNew = _tempNew - rand.NextDouble() - 0.3;
                    break;
                case 1:
                case 2:
                case 3:
                    if (_hour > 1 && _hour <= 6)
                        _tempNew = _tempNew - (rand.NextDouble() + 1.2);
                    else if (_hour > 6 && _hour <= 12)
                        _tempNew = _tempNew + rand.NextDouble();
                    else if (_hour > 12 && _hour <= 16)
                        _tempNew = _tempNew + (rand.NextDouble() + 1);
                    else if (_hour > 16 && _hour <= 20)
                        _tempNew = _tempNew + rand.NextDouble();
                    else if (_hour > 20 && _hour <= 23)
                        _tempNew = _tempNew + rand.NextDouble();
                    else 
                        _tempNew = _tempNew - rand.NextDouble() - 0.1;
                    break;
            }

            
            TriggerClientEvent("ARP:SyncWeatherTemp", Math.Round(_tempNew, 1));
        }
    }
}