using System;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;

namespace Client
{
    public class Lang : BaseScript
    {
        public static List<dynamic> ParamsList = new List<dynamic>();

        public static dynamic GetParam(int paramId)
        {
            return ParamsList.Any() && ParamsList.Count >= paramId ? ParamsList[paramId] : "null";
        }

        public static string GetRuText(string key)
        {
            switch (key)
            {
                case "_lang_0":
                    return "~r~Должно быть больше нуля";
                case "_lang_1":
                    return "Сумма";
                case "_lang_2":
                    return "~r~У Вас недостаточно денег на счету";
                case "_lang_3":
                    return "~r~Запрещено выводить более $10,000";
                case "_lang_4":
                    return $"Вывод средств: ~g~${Convert.ToInt32(GetParam(0)):#,#}";
                case "_lang_5":
                    return "~r~У Вас недостаточно денег на руках";
                case "_lang_6":
                    return "~r~Запрещено класть более $10,000";
                case "_lang_7":
                    return $"Зачисление средств: ~g~${Convert.ToInt32(GetParam(0)):#,#}";
                case "_lang_8":
                    return "~r~У Вас уже есть банковская карта";
                case "_lang_9":
                    return "Операция со счётом";
                case "_lang_10":
                    return "Создание счёта";
                case "_lang_11":
                    return "Закрытие счёта";
                case "_lang_12":
                    return "Вы оформили карту банка. Спасибо!";
                case "_lang_13":
                    return "Вы закрыли карту банка, СМС оповещения отключены.";
                case "_lang_14":
                    return "Бар";
                case "_lang_15":
                    return "~r~У Вас недостаточно средств";
                case "_lang_16":
                    return "выпивает стакан воды";
                case "_lang_17":
                    return "выпивает стакан колы";
                case "_lang_18":
                    return "выпивает стакан лимонада";
                case "_lang_19":
                    return "выпивает стакан пива";
                case "_lang_20":
                    return "выпивает стакан водки";
                case "_lang_21":
                    return "выпивает стакан виски";
                case "_lang_22":
                    return "~r~У игрока уже есть бизнес";
                case "_lang_23":
                    return "~g~Вы предложили игроку купить бизнес";
                case "_lang_24":
                    return "~g~Бизнес невозможно купить";
                case "_lang_25":
                    return "~r~У Вас недостаточно средств";
                case "_lang_26":
                    return "~r~Бизнес можно иметь с 21 года";
                case "_lang_27":
                    return "~g~Вы купили бизнес";
                case "_lang_28":
                    return "~g~Вы продали бизнес";
                case "_lang_29":
                    return "~g~Вы купили новый интерьер";
                case "_lang_30":
                    return "~r~Бизнес уже куплен";
                case "_lang_31":
                    return "~r~У Вас уже есть бизнес";
                case "_lang_32":
                    return "~r~Ошибка транзакции, попросите админа сделать рестарт";
                case "_lang_33":
                    return "~g~Поздравляем с покупкой бизнеса";
                case "_lang_34":
                    return "~r~Запрос еще не был обработан";
                case "_lang_35":
                    return "~r~Попробуйте еще раз";
                case "_lang_36":
                    return "Обработка запроса, подождите";
                case "_lang_37":
                    return "~r~У Вас нет бизнеса";
                case "_lang_38":
                    return $"Налог: ~g~{Coffer.GetNalog()}%\n~s~Получено: ~g~${Convert.ToInt32(GetParam(0)):#,#}";
                case "_lang_39":
                    return "Пункт смены номера";
                case "_lang_40":
                    return "~g~Транспорт отремонтирован";
                case "_lang_41":
                    return "~r~Транспорт не был отремонтирован";
                case "_lang_42":
                    return "~r~Транспорт не найден";
                case "_lang_43":
                    return "~r~Минимум 4 символа";
                case "_lang_44":
                    return "~r~Вы не правильно ввели номер";
                case "_lang_45":
                    return "~r~Только цифры (0-9) и буквы на англ. (A-Z)";
                case "_lang_46":
                    return "Мойка авто";
                case "_lang_47":
                    return "~g~Ваш транспорт теперь чист. Стоимость $21";
                case "_lang_48":
                    return "Казино";
                case "_lang_49":
                    return "Магазин одежды";
                case "_lang_50":
                    return "Ювелирный магазин";
                case "_lang_51":
                    return "~g~Вы купили одежду";
                case "_lang_52":
                    return "~r~Тюнинговать можно только личный транспорт";
                case "_lang_53":
                    return $"Вы купили деталь. Цена: ~g~${GetParam(0)}";
                case "_lang_54":
                    return "~r~Ошибка тюнинга";
                case "_lang_55":
                    return "~r~Логи отправлены разработчику";
                case "_lang_56":
                    return $"~y~Вы сняли деталь. Цена: ~g~${GetParam(0)}";
                case "_lang_57":
                    return $"Вы покрасили транспорт. Цена: ~g~${GetParam(0)}";
                case "_lang_58":
                    return "Заправка";
                case "_lang_59":
                    return "Магазин 24/7";
                case "_lang_60":
                    return "~r~В инвентаре нет места";
                case "_lang_61":
                    return "~g~Вы купили канистру";
                case "_lang_62":
                    return "~r~Транспорт не нуждается в заправке";
                case "_lang_63":
                    return $"~g~Вы заправили свой транспорт по цене: ${GetParam(0)}";
                case "_lang_64":
                    return "Спортзал";
                case "_lang_65":
                    return "~g~Вы устали, попробуйте чуть-чуть позже";
                case "_lang_66":
                    return "Магазин оружия";
                case "_lang_67":
                    return "~r~У Вас нет регистрации";
                case "_lang_68":
                    return "~r~(M - GPS - Важные места - Здание правительства)";
                case "_lang_69":
                    return "~g~Поздравляем с покупкой лицензии";
                case "_lang_70":
                    return "~r~У вас уже есть данная лицензия";
                case "_lang_71":
                    return "Пункт аренды транспорта";
                case "_lang_72":
                    return "Пункт аренды лодок";
                case "_lang_73":
                    return "Пункт аренды вертолётов";
                case "_lang_74":
                    return "Пункт аренды самолётов";
                case "_lang_75":
                    return "Пункт аренды такси";
                case "_lang_76":
                    return "Пункт аренды авто";
                case "_lang_77":
                    return "~r~У Вас нет лицензии категории А";
                case "_lang_78":
                    return "~r~У Вас нет лицензии категории B";
                case "_lang_79":
                    return "~r~У Вас нет прав категории D";
                case "_lang_80":
                    return $"~g~Вы купили телефон\nВаш номер: {GetParam(0)}";
                case "_lang_81":
                    return "~g~Вы купили часы";
                case "_lang_82":
                    return "~r~У вас уже есть рация";
                case "_lang_83":
                    return "~g~Вы купили рацию";
                case "_lang_84":
                    return $"~g~Вы купили \"{GetParam(0)}\". Кол-во: {GetParam(1)}";
                case "_lang_85":
                    return "Тату салон";
                case "_lang_86":
                    return "~r~Вам нужно прожить 2 месяца в штате";
                case "_lang_87":
                    return "~r~У Вас нет ~g~$50";
                case "_lang_88":
                    return "~g~Вы получили права категории D";
                case "_lang_89":
                    return "~y~Зачем Вам временная регистрация? O_o";
                case "_lang_90":
                    return "~g~Вы получили временную регистрацию на ~y~6 ~g~мес.";
                case "_lang_91":
                    return "~g~Вы оформили пособие по безработице";
                case "_lang_92":
                    return "~r~У Вас есть работа";
                case "_lang_93":
                    return "~g~Вы оформили пенсию";
                case "_lang_94":
                    return "~r~Вам нужно прожить 50 лет";
                case "_lang_95":
                    return "~r~Вам нужно прожить 6 месяцев в штате";
                case "_lang_96":
                    return "~r~Вам нужно прожить 2 месяца в штате. И иметь лицензию таксиста";
                case "_lang_97":
                    return "~r~Вам нужно прожить 1 месяц в штате";
                case "_lang_98":
                    return "~r~Вам должно быть 21 год";
                case "_lang_99":
                    return "~r~Вам должно быть 21 год и у Вас должно быть гражданство";
                case "_lang_100":
                    return "~r~Вам должно быть 25 лет и у Вас должно быть гражданство и лицензия адвоката";
                case "_lang_101":
                    return "~g~Вы устроились на работу";
                case "_lang_102":
                    return "~y~Вы уволились с работы";
                case "_lang_103":
                    return $"~r~Число не должно быть меньше {GetParam(0)} и больше {GetParam(1)}";
                case "_lang_104":
                    return "Новости правительства";
                case "_lang_105":
                    return $"Текущее пособие: ~g~${GetParam(0)}";
                case "_lang_106":
                    return $"Текущая пенсия: ~g~${GetParam(0)}";
                case "_lang_107":
                    return $"Текущий налог: ~g~{GetParam(0)}%";
                case "_lang_108":
                    return $"Текущий налог на бизнес: ~g~{GetParam(0)}%";
                case "_lang_109":
                    return "~g~Вы уже взяли задание";
                case "_lang_110":
                    return "Я тебе скинул координаты точки";
                case "_lang_111":
                    return "Начальник";
                case "_lang_112":
                    return "~r~У Вас уже есть набор инструментов";
                case "_lang_113":
                    return "~g~Вы взяли инструменты";
                case "_lang_114":
                    return "~g~Вы произвели необходимую проверку в доме";
                case "_lang_115":
                    return "~b~Вы закончили рабочий день";
                case "_lang_116":
                    return "~b~Вы начали рабочий день";
                case "_lang_117":
                    return "~b~Поднимитесь в башню чтобы начать работать";
                case "_lang_118":
                    return $"~b~Ваша зарплата: ~g~${GetParam(0)}";
                case "_lang_119":
                    return "~g~Вы начали рейс, не выходите из автобуса до конца поездки";
                case "_lang_120":
                    return "~b~Ожидайте 10 секунд";
                case "_lang_121":
                    return "~g~Вы закончили свой рейс";
                case "_lang_122":
                    return "~g~Двигайтесь к следующей остановке";
                case "_lang_123":
                    return "~r~Вы завершили досрочно свой рейс";
                case "_lang_124":
                    return "~r~Собирайте мусор возле берега";
                case "_lang_125":
                    return "Вы были отправлены на работу в карьер";
                case "_lang_126":
                    return "Начинай работать и даже не смейте думать о побеге";
                case "_lang_127":
                    return "Отлично. Теперь отправляйся обратно в свою камеру";
                case "_lang_128":
                    return "Убирайся отсюда, пока сам за решетку не отправился";
                case "_lang_129":
                    return "Координаты первой точки уже в твоем GPS";
                case "_lang_130":
                    return "Босс";
                case "_lang_131":
                    return "Работать инкассатором можно только имея лицензию на владение огнестрельным оружием, лицензию категории С, а также быть не младше 23 лет";
                /*case "_lang_":
                    return "";
                case "_lang_":
                    return "";
                case "_lang_":
                    return "";*/
            }
            return "~r~Error: ~w~Перевод не найден";
        }

        public static string GetEnText(string key)
        {
            switch (key)
            {
                case "a":
                    return "null";
                case "b":
                    return "null";
                case "c":
                    return "null";
                case "d":
                    return "null";
                case "e":
                    return "null";
                case "f":
                    return "null";
                case "g":
                    return "null";
                case "h":
                    return "null";
                case "i":
                    return "null";
                case "k":
                    return "null";
                case "l":
                    return "null";
                case "m":
                    return "null";
                case "n":
                    return "null";
                case "o":
                    return "null";
                case "p":
                    return "null";
                case "q":
                    return "null";
                case "r":
                    return "null";
                case "s":
                    return "null";
                case "t":
                    return "null";
                case "u":
                    return "null";
                case "v":
                    return "null";
                case "w":
                    return "null";
                case "x":
                    return "null";
                case "y":
                    return "null";
                case "z":
                    return "null";
            }
            
            return "~r~Error: ~w~Translate was not found";
        }

        public static string GetTextToPlayer(string key, params dynamic[] args)
        {
            ParamsList = new List<dynamic>();
            foreach (var arg in args)
                ParamsList.Add(arg);

            //return User.Data.s_lang == "RU" ? GetRuText(key) : GetEnText(key);
            return GetRuText(key);
        }

        public static string GetText(string key, params dynamic[] args)
        {
            ParamsList = new List<dynamic>();
            foreach (var arg in args)
                ParamsList.Add(arg);

            return GetRuText(key);
        }
    }
}