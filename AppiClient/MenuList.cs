using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using Client.Business;
using Client.Managers;
using Client.Vehicle;
using NativeUI;
using static CitizenFX.Core.Native.API;
using Notification = Client.Managers.Notification;
using Weather = CitizenFX.Core.Weather;

namespace Client 
{
    public class MenuList : BaseScript
    {
        public static UIMenu UiMenu = null;
        protected static MenuPool MenuPool = new MenuPool();
        protected static int Obj;
        protected static bool OpenE = false;
        
        protected static NativeUI.PauseMenu.TabView TabTest = null;
        
        private static float _screenX = 0f;
        private static float _screenY = 0f;
        public static int welding = 0;
        private static int _lastSpec = 0;
        
        public static Camera Camera;

        public static bool soson = false;
        
        public MenuList()
        {
            EventHandlers.Add("ARP:CloseMenu", new Action(HideMenu));
            Tick += ProcessMenuPool;
            Tick += ProcessMainMenu;
        }
        
        
        public static async void ShowAuthMenu(string nick = "", string pass = "")
        {
            if (await Ctos.IsBlackout(true))
            {
                Ctos.SetBlackout();
            }
            
            HideMenu();
            User.Freeze(PlayerId(), true);
            
            UI.ShowToolTip("Нажмите ~INPUT_PICKUP~ если у Вас закрылось меню авторизации");
            
            var menu = new Menu();
            UiMenu = menu.Create("Авторизация", "~b~Авторизация на сервере", true, true);
            var nickname = menu.AddMenuItem(UiMenu, "Имя Фамилия", "Введите ваш ник на сервере через пробел");
            var password = menu.AddMenuItem(UiMenu, "Пароль", "Введите ваш пароль на сервере");
            var authButton = menu.AddMenuItem(UiMenu, "~g~Авторизоваться");
            var regButton = menu.AddMenuItem(UiMenu, "~y~Регистрация", "Перейти к регистрации");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть и выйти");
            
            if (nick != "")
                nickname.SetRightLabel(nick);
            
            if (pass != "")
            {
                var pass1 = "";
                for (int i = 0; i < pass.Length; i++)
                    pass1 += "*";
                
                password.SetRightLabel(pass1);
            }
            
            UiMenu.OnItemSelect += async (sender, item, index) =>
            {
                if (item == closeButton)
                    User.Kick(PlayerId());
                else if (item == regButton)
                    ShowRegMenu();
                else if (item == nickname)
                {
                    HideMenu();
                    var userInput = await Menu.GetUserInput("Введите ваше Имя и Фамилию", "", 100);
                    if (userInput == "NULL") return;
                    nick = Main.RemoveQuotes(userInput);
                    await Delay(500);
                    ShowAuthMenu(nick, pass);
                }
                else if (item == password)
                {
                    HideMenu();
                    var userInput = await Menu.GetUserInput("Введите ваш пароль", "", 32);
                    if (userInput == "NULL") return;
                    pass = userInput;
                    await Delay(500);
                    ShowAuthMenu(nick, pass);
                }
                else if (item == authButton)
                {
                    if (nick != "" && pass != "")
                    {
                        TriggerServerEvent("ARP:LoginPlayer", nick, pass);
                    }
                    else
                        Notification.Send("Логин или пароль - пуст");
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowRegMenu(string name = "", string surname = "", string password = "", string email = "", string referer = "", bool acceptRules = false)
        {
            HideMenu();
            User.Freeze(PlayerId(), true);
            
            var menu = new Menu();
            UiMenu = menu.Create("Регистрация", "~b~Регистрация на сервере", true, true);
            var nameItem = menu.AddMenuItem(UiMenu, "Имя (Англ)", name != "" ? $"~b~Ваше имя:~s~ {name}" : "Введите ваше имя");
            var surnameItem = menu.AddMenuItem(UiMenu, "Фамилия (Англ)", surname != "" ? $"~b~Ваша фамилия:~s~ {surname}" : "Введите вашу фамилию");
            var passwordItem = menu.AddMenuItem(UiMenu, "Пароль", password != "" ? "~b~Ваш пароль введён" : "Введите ваш пароль на сервере");
            var emailItem = menu.AddMenuItem(UiMenu, "Email", email != "" ? $"~b~Ваш Email:~s~ {email}" : "Введите ваш email");
            var refererItem = menu.AddMenuItem(UiMenu, "Ник пригласившего (Через пробел)", referer != "" ? $"~b~Пригласивший:~s~ {referer}" : $"~b~Пригласивший:~s~ ~r~нет");
            var acceptRulesItem = menu.AddCheckBoxItem(UiMenu, "Согласен с правилами сервера", acceptRules, "https://alamo-rp.com/newbie");
            var regButton = menu.AddMenuItem(UiMenu, "~g~Регистрация");
            var authButton = menu.AddMenuItem(UiMenu, "~y~Авторизация", "Перейти к авторизации");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть и выйти");
            
            if (name != "")
                nameItem.SetRightLabel(name);
            if (surname != "")
                surnameItem.SetRightLabel(surname);
            if (email != "")
                emailItem.SetRightLabel(email);
            if (referer != "")
                refererItem.SetRightLabel(referer);
            if (password != "")
            {
                var pass = "";
                for (int i = 0; i < password.Length; i++)
                    pass += "*";
                
                passwordItem.SetRightLabel(pass);
            }

            UiMenu.OnCheckboxChange += (sender, item, _checked) =>
            {
                if (item != acceptRulesItem) return;
                acceptRules = _checked;
            };
            
            UiMenu.OnItemSelect += async (sender, item, index) =>
            {
                if (item == closeButton)
                    User.Kick(PlayerId());
                else if (item == authButton)
                    ShowAuthMenu();
                else if (item == nameItem)
                {
                    HideMenu();
                    var userInput = await Menu.GetUserInput("Ваше имя (Англ)", "", 32);
                    if (userInput == "NULL") return;
                    name = Main.RemoveQuotesAndSpace(userInput);
                    await Delay(500);
                    ShowRegMenu(name, surname, password, email, referer, acceptRules);
                }
                else if (item == surnameItem)
                {
                    HideMenu();
                    var userInput = await Menu.GetUserInput("Ваша фамилия (Англ)", "", 32);
                    if (userInput == "NULL") return;
                    surname = Main.RemoveQuotesAndSpace(userInput);
                    await Delay(500);
                    ShowRegMenu(name, surname, password, email, referer, acceptRules);
                }
                else if (item == passwordItem)
                {
                    HideMenu();
                    var userInput = await Menu.GetUserInput("Введите ваш пароль", "", 32);
                    if (userInput == "NULL") return;
                    password = userInput;
                    await Delay(500);
                    ShowRegMenu(name, surname, password, email, referer, acceptRules);
                }
                else if (item == emailItem)
                {
                    HideMenu();
                    var userInput = await Menu.GetUserInput("Введите ваш Email", "", 50);
                    if (userInput == "NULL") return;
                    //email = Main.RegEx(userInput, "\b[A-Za-z@-.]+\b");
                    email = Main.RemoveQuotesAndSpace(userInput);
                    await Delay(500);
                    ShowRegMenu(name, surname, password, email, referer, acceptRules);
                }
                else if (item == refererItem)
                {
                    HideMenu();
                    var userInput = await Menu.GetUserInput("Введите ник пригласившего", "", 100);
                    if (userInput == "NULL") return;
                    //email = Main.RegEx(userInput, "\b[A-Za-z@-.]+\b");
                    referer = Main.RemoveQuotes(userInput);
                    await Delay(500);
                    ShowRegMenu(name, surname, password, email, referer, acceptRules);
                }
                else if (item == regButton)
                {
                    if (name == "")
                    {
                        Notification.Send("Имя - поле не заполнено");
                        return;
                    }
                    if (surname == "")
                    {
                        Notification.Send("Фамилия - поле не заполнено");
                        return;
                    }
                    if (password == "")
                    {
                        Notification.Send("Пароль - поле не заполнено");
                        return;
                    }
                    if (email == "")
                    {
                        Notification.Send("Email - поле не заполнено");
                        return;
                    }
                    if (!acceptRules)
                    {
                        Notification.Send("Вы не согласились с правилами сервера");
                        return;
                    }
                    TriggerServerEvent("ARP:RegPlayer", name, surname, password, email, referer, acceptRules);
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowMisterKAccpetResetWantedMenu(int id, int idFrom)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Розыск", "~b~Согласны очистить розыск?");
      
            var yesButton = menu.AddMenuItem(UiMenu, "~g~Да");
            var noButton = menu.AddMenuItem(UiMenu, "~r~Нет");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == yesButton)
                {
                    HideMenu();
                    Event.MisterKAcceptResetWanted(id, idFrom);
                }
                if (item == noButton)
                {
                    HideMenu();
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowAskCharacherCustomMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Кастомизация", "~b~Хотите кастомизировать персонажа?", true, true);
        
            var yesButton = menu.AddMenuItem(UiMenu, "~g~Да");
            var noButton = menu.AddMenuItem(UiMenu, "~r~Нет");
            
            UiMenu.OnItemSelect += async (sender, item, index) =>
            {
                if (item == yesButton)
                {
                    var rand = new Random();
                    User.SetVirtualWorld(rand.Next(10000));
                    User.SetSkin("mp_m_freemode_01");
                    
                    Characher.UpdateFace();
                    Characher.UpdateCloth();
                    
                    RequestCollisionAtCoord(9.653649f, 528.3086f, 169.635f);
                    await Delay(1000);
                    
                    User.Teleport(new Vector3(9.653649f, 528.3086f, 169.635f));
                    NetworkResurrectLocalPlayer(9.653649f, 528.3086f, 169.635f, 120.0613f, true, true);
                    User.PedRotation(120.0613f);
                    
                    User.Freeze(PlayerId(), true);
                    /*Camera.Position = new Vector3(-7.973644f, 513.0889f, 174.6281f);
                    Camera.PointAt(new Vector3(-10.10947f, 508.5056f, 174.6281f));*/
                    
                    Camera = new CitizenFX.Core.Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
                    Camera.IsActive = true;
                    Camera.Position = new Vector3(8.243752f, 527.4373f, 171.6173f);
                    Camera.PointAt(new Vector3(9.653649f, 528.3086f, 171.335f));
                    RenderScriptCams(true, false, Camera.Handle, false, false);
                    
                    ShowCharacherCustomMenu();
                }
                if (item == noButton)
                {
                    HideMenu();
                    Sync.Data.Set(User.GetServerId(), "s_is_characher", true);
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        public static void ShowAskSellTrMenu(string id)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Продажа", "~b~Согласны на продажу?", true, true);
            
        
            var yesButton = menu.AddMenuItem(UiMenu, "~g~Да");
            var noButton = menu.AddMenuItem(UiMenu, "~r~Нет");
            
            UiMenu.OnItemSelect += async (sender, item, index) =>
            {


                if (item == yesButton)
                    
                { 
                    switch(id)
                    {
                        case "car_id1":
                        {
                            Managers.Vehicle.SellCar(User.Data.car_id1);
                            HideMenu();
                            break;
                        }
                        case "car_id2":
                        {
                            Managers.Vehicle.SellCar(User.Data.car_id2);
                            HideMenu();
                            break;
                        }
                        case "car_id3":
                        {
                            Managers.Vehicle.SellCar(User.Data.car_id3);
                            HideMenu();
                            break;
                        }        
                        case "car_id4":
                        {
                            Managers.Vehicle.SellCar(User.Data.car_id4);
                            HideMenu();
                            break;
                        }
                        case "car_id5":
                        {
                            Managers.Vehicle.SellCar(User.Data.car_id5);
                            HideMenu();
                            break;
                        }
                        case "car_id6":
                        {
                            Managers.Vehicle.SellCar(User.Data.car_id6);
                            HideMenu();
                            break;
                        }
                        case "car_id7":
                        {
                            Managers.Vehicle.SellCar(User.Data.car_id7);
                            HideMenu();
                            break;
                        }
                        case "car_id8":
                        {
                            Managers.Vehicle.SellCar(User.Data.car_id8);
                            HideMenu();
                            break;
                   
                    
                }
            }


            }
                if (item == noButton)
                {
                    HideMenu();
                    
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        public static void ShowAskSellSMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Maze", "~b~Согласны на продажу?", true, true);
        
            var yesButton = menu.AddMenuItem(UiMenu, "~g~Да");
            var noButton = menu.AddMenuItem(UiMenu, "~r~Нет");
            
            UiMenu.OnItemSelect += async (sender, item, index) =>
            {
                if (item == yesButton)
                {
                    Stock.SellStock(Stock.GetStockFromId(User.Data.stock_id));  
                    HideMenu();
                    }
                if (item == noButton)
                {
                    HideMenu();
                    
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        public static void ShowAskSellHMenu()
        {
            HideMenu();
            var menu = new Menu();
            UiMenu = menu.Create("Maze", "~b~Согласны на продажу?", true, true);
            var yesButton = menu.AddMenuItem(UiMenu, "~g~Да");
            var noButton = menu.AddMenuItem(UiMenu, "~r~Нет");
            UiMenu.OnItemSelect += async (sender, item, index) =>
            {
                if (item == yesButton)
                {
                    House.SellHouse(House.GetHouseFromId(User.Data.id_house));
                    HideMenu();
                }
                if (item == noButton)
                {
                    HideMenu();
                }
            };
            MenuPool.Add(UiMenu);
        }

        public static void ShowAskSellKMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Maze", "~b~Согласны на продажу?", true, true);
        
            var yesButton = menu.AddMenuItem(UiMenu, "~g~Да");
            var noButton = menu.AddMenuItem(UiMenu, "~r~Нет");
            
            UiMenu.OnItemSelect += async (sender, item, index) =>
            {
                if (item == yesButton)
                {
                    
                    Condo.SellHouse(Condo.GetHouseFromId(User.Data.condo_id));   
                    HideMenu();
                    
                    
                }
                if (item == noButton)
                {
                    HideMenu();
                    
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        public static void ShowAskSellApsMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Апартаменты", "~b~Согласны на продажу?", true, true);
        
            var yesButton = menu.AddMenuItem(UiMenu, "~g~Да");
            var noButton = menu.AddMenuItem(UiMenu, "~r~Нет");
            
            UiMenu.OnItemSelect += async (sender, item, index) =>
            {
                if (item == yesButton)
                {
                    Managers.Apartment.Sell(User.Data.apartment_id);
                    HideMenu();
                    
                    
                }
                if (item == noButton)
                {
                    HideMenu();
                    
                }
            };
            
            MenuPool.Add(UiMenu);
        }
		 public static void ShowAskSellBMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Arcadius", "~b~Согласны на продажу?", true, true);
        
            var yesButton = menu.AddMenuItem(UiMenu, "~g~Да");
            var noButton = menu.AddMenuItem(UiMenu, "~r~Нет");
            
            UiMenu.OnItemSelect += async (sender, item, index) =>
            {
                if (item == yesButton)
                {
                    Business.Business.Sell(User.Data.business_id);
                    HideMenu();
                    
                    
                }
                if (item == noButton)
                {
                    HideMenu();
                    
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowAskInviteMpMenu(float x, float y, float z)
        {
            if (!User.IsLogin() || User.Data.jail_time > 1) return;
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Мероприятие", "~b~Хотите на мероприятие?", true, true);
      
            var yesButton = menu.AddMenuItem(UiMenu, "~g~Да");
            var noButton = menu.AddMenuItem(UiMenu, "~r~Нет");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == yesButton)
                {
                    HideMenu();
                    User.Teleport(new Vector3(x, y, z));
                }
                if (item == noButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowCharacherCustomMenu()
        {
            await Delay(500);
            HideMenu();
            User.SetSkin("mp_m_freemode_01");
        
            OpenE = true;
            UI.ShowToolTip("Нажмите ~INPUT_PICKUP~ если у Вас закрылось меню авторизации");
            
            Characher.UpdateFace();
            Characher.UpdateCloth(false);
            
            User.Freeze(PlayerId(), true);
            var menu = new Menu();
            UiMenu = menu.Create("Персонаж", "~b~Влево/вправо менять внешность");
            var motherItem = menu.AddMenuItem(UiMenu, "Мать", "Нажмите \"~g~Enter~s~\" чтобы выбрать");
            var fatherItem = menu.AddMenuItem(UiMenu, "Отец", "Нажмите \"~g~Enter~s~\" чтобы выбрать");
            var sexItem = menu.AddCheckBoxItem(UiMenu, "Пол мужской", true, "Нажмите \"~g~Enter~s~\" чтобы выбрать");
            
            var list = new List<dynamic>();
            for (var i = 0; i < 11; i++)
                list.Add((i * 10) + "%");
            
            var faceParentItem = menu.AddMenuItemList(UiMenu, "Схожесть #1", list, "Похож на родителей (Лицо)");
            var skinParentItem = menu.AddMenuItemList(UiMenu, "Схожесть #2", list, "Похож на родителей (Кожа)");
            
            list = new List<dynamic>();
            for (var i = 0; i < 36; i++)
                list.Add(i);
            var hairListItem = menu.AddMenuItemList(UiMenu, "Волосы", list);
            
            list = new List<dynamic>();
            for (var i = 0; i < 64; i++)
                list.Add(i);
            var hairColorListItem = menu.AddMenuItemList(UiMenu, "Цвет волос", list);
            
            list = new List<dynamic>();
            for (var i = 0; i < 32; i++)
                list.Add(i);
            var eyeColorListItem = menu.AddMenuItemList(UiMenu, "Цвет глаз", list);
            
            list = new List<dynamic>();
            for (var i = 0; i < 30; i++)
                list.Add(i);
            var eyeBrowsListItem = menu.AddMenuItemList(UiMenu, "Брови", list);
            
            list = new List<dynamic>();
            for (var i = 0; i < 64; i++)
                list.Add(i);
            var eyeBrowsColorListItem = menu.AddMenuItemList(UiMenu, "Цвет бровей", list);
            
            
            menu.AddMenuItem(UiMenu, "~b~Рандом", "Выбор случайного персонажа").Activated += (uimenu, item) =>
            {
                var rand = new Random();
                
                User.Skin.GTAO_SHAPE_MIX = ToFloat(rand.Next(100)) * 0.1f;
                User.Skin.GTAO_SKIN_MIX = ToFloat(rand.Next(100)) * 0.1f;
                User.Skin.GTAO_SHAPE_SECOND_ID = rand.Next(46);
                User.Skin.GTAO_SHAPE_THRID_ID = rand.Next(46);
                User.Skin.GTAO_SKIN_SECOND_ID = rand.Next(12);
                User.Skin.GTAO_SKIN_THRID_ID = rand.Next(12);
                User.Skin.GTAO_HAIR = rand.Next(23);
                User.Skin.GTAO_HAIR_COLOR = rand.Next(64);
                User.Skin.GTAO_EYE_COLOR = rand.Next(32);
                User.Skin.GTAO_EYEBROWS = rand.Next(30);
                User.Skin.GTAO_EYEBROWS_COLOR = rand.Next(64);
        
                Client.Sync.Data.Set(User.GetServerId(), "GTAO_SHAPE_MIX", User.Skin.GTAO_SHAPE_MIX);
                Client.Sync.Data.Set(User.GetServerId(), "GTAO_SKIN_MIX", User.Skin.GTAO_SKIN_MIX);
                Client.Sync.Data.Set(User.GetServerId(), "GTAO_SHAPE_SECOND_ID", User.Skin.GTAO_SHAPE_SECOND_ID);
                Client.Sync.Data.Set(User.GetServerId(), "GTAO_SHAPE_THRID_ID", User.Skin.GTAO_SHAPE_THRID_ID);
                Client.Sync.Data.Set(User.GetServerId(), "GTAO_SKIN_SECOND_ID", User.Skin.GTAO_SKIN_SECOND_ID);
                Client.Sync.Data.Set(User.GetServerId(), "GTAO_SKIN_THRID_ID", User.Skin.GTAO_SKIN_THRID_ID);
                Client.Sync.Data.Set(User.GetServerId(), "GTAO_HAIR", User.Skin.GTAO_HAIR);
                Client.Sync.Data.Set(User.GetServerId(), "GTAO_HAIR_COLOR", User.Skin.GTAO_HAIR_COLOR);
                Client.Sync.Data.Set(User.GetServerId(), "GTAO_EYE_COLOR", User.Skin.GTAO_EYE_COLOR);
                Client.Sync.Data.Set(User.GetServerId(), "GTAO_EYEBROWS", User.Skin.GTAO_EYEBROWS);
                Client.Sync.Data.Set(User.GetServerId(), "GTAO_EYEBROWS_COLOR", User.Skin.GTAO_EYEBROWS_COLOR);
                
                Characher.UpdateFace(false);
            };
            
            var doneBtn = menu.AddMenuItem(UiMenu, "~g~Сохранить");
        
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == doneBtn)
                {
                    HideMenu();
                    User.Freeze(PlayerId(), false);
                    Camera.Delete();
                    RenderScriptCams(false, true, 500, true, true);
                    User.SetVirtualWorld(0);
                    
                    Characher.UpdateFace(false);
                    Characher.UpdateCloth(false);
        
                    Sync.Data.Set(User.GetServerId(), "s_is_characher", true);
                    
                    User.SaveAccount();
                    OpenE = false;
                    
                    /*
                    menu.AddMenuItem(UiMenu, "Торс").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        ShowShopClothMenu(shopId, 3, 11);
                    };
        
                    menu.AddMenuItem(UiMenu, "Ноги").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        ShowShopClothMenu(shopId, 3, 4);
                    };
        
                    menu.AddMenuItem(UiMenu, "Обувь").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        ShowShopClothMenu(shopId, 3, 6);
                    };
                    */
                    
                    //(int) cloth[i, 10] 
                    
                    
                    dynamic[,] cloth = User.Skin.SEX == 1 ? Cloth.ClothF : Cloth.ClothM;
                    var rand = new Random();
                    var listTorso = new List<int>();
                    var listLeg = new List<int>();
                    var listFoot = new List<int>();
                    
                    for (int i = 0; i < cloth.Length / 12; i++)
                    {
                        if ((int) cloth[i, 1] != 11) continue;
                        if ((int) cloth[i, 0] != 0) continue;
                        if ((int) cloth[i, 10] > Managers.Weather.Temp + 5) continue;
                        listTorso.Add(i);
                    }
                    
                    for (int i = 0; i < cloth.Length / 12; i++)
                    {
                        if ((int) cloth[i, 1] != 4) continue;
                        if ((int) cloth[i, 0] != 0) continue;
                        if ((int) cloth[i, 10] > Managers.Weather.Temp + 5) continue;
                        listLeg.Add(i);
                    }
                    
                    for (int i = 0; i < cloth.Length / 12; i++)
                    {
                        if ((int) cloth[i, 1] != 6) continue;
                        if ((int) cloth[i, 0] != 0) continue;
                        if ((int) cloth[i, 10] > Managers.Weather.Temp + 5) continue;
                        listFoot.Add(i);
                    }
        
                    int idTorso = listTorso[rand.Next(listTorso.Count)]; 
                    int idLeg = listLeg[rand.Next(listLeg.Count)]; 
                    int idFoot = listFoot[rand.Next(listFoot.Count)]; 
                    
                    Cloth.Buy(0, (int) cloth[idTorso, 1], (int) cloth[idTorso, 2], rand.Next((int) cloth[idTorso, 3] + 1), (int) cloth[idTorso, 4], (int) cloth[idTorso, 5], (int) cloth[idTorso, 6], (int) cloth[idTorso, 7], 0, true);
                    Cloth.Buy(0, (int) cloth[idLeg, 1], (int) cloth[idLeg, 2], rand.Next((int) cloth[idLeg, 3] + 1), (int) cloth[idLeg, 4], (int) cloth[idLeg, 5], (int) cloth[idLeg, 6], (int) cloth[idLeg, 7], 0, true);
                    Cloth.Buy(0, (int) cloth[idFoot, 1], (int) cloth[idFoot, 2], rand.Next((int) cloth[idFoot, 3] + 1), (int) cloth[idFoot, 4], (int) cloth[idFoot, 5], (int) cloth[idFoot, 6], (int) cloth[idFoot, 7], 0, true);
        
                    if (User.Data.age == 18 && User.Data.exp_age < 2)
                        Newbie.FlyScene();
                    else
                    {
                        User.Teleport(new Vector3(-1042.389f, -2745.814f, 20.3594f));
                        User.PedRotation(328.8829f);
                    }
                }
                if (item == motherItem)
                    ShowCharacherCustomMotherMenu();
                if (item == fatherItem)
                    ShowCharacherCustomFatherMenu();
            };
            
            UiMenu.OnCheckboxChange += (sender, item, _checked) =>
            {
                if (item != sexItem) return;
                User.SetSkin(_checked ? "mp_m_freemode_01" : "mp_f_freemode_01");
                User.Skin.SEX = _checked ? 0 : 1;
                Sync.Data.Set(User.GetServerId(), "SEX", User.Skin.SEX);
                
                Characher.UpdateFace(false);
                Characher.UpdateCloth(false);
            };
            
            UiMenu.OnListChange += (sender, item, index) =>
            { 
                if (item == faceParentItem)
                {
                    User.Skin.GTAO_SHAPE_MIX = ToFloat(index) * 0.1f;
                    Sync.Data.Set(User.GetServerId(), "GTAO_SHAPE_MIX", User.Skin.GTAO_SHAPE_MIX);
                    Characher.UpdateFace(false);
                }
                if (item == skinParentItem)
                {
                    User.Skin.GTAO_SKIN_MIX = ToFloat(index) * 0.1f;
                    Sync.Data.Set(User.GetServerId(), "GTAO_SKIN_MIX", User.Skin.GTAO_SKIN_MIX);
                    Characher.UpdateFace(false);
                }
                if (item == hairListItem)
                {
                    User.Skin.GTAO_HAIR = index;
                    
                    if (index == 23 && GetEntityModel(GetPlayerPed(-1)) == 1885233650)
                        User.Skin.GTAO_HAIR = 36;
                    
                    if (index == 24 && GetEntityModel(GetPlayerPed(-1)) == -1667301416)
                        User.Skin.GTAO_HAIR = 36;
                    
                    Sync.Data.Set(User.GetServerId(), "GTAO_HAIR", User.Skin.GTAO_HAIR);
                    Characher.UpdateFace(false);
                }
                if (item == hairColorListItem)
                {
                    User.Skin.GTAO_HAIR_COLOR = index;
                    Sync.Data.Set(User.GetServerId(), "GTAO_HAIR_COLOR", User.Skin.GTAO_HAIR_COLOR);
                    Characher.UpdateFace(false);
                }
                if (item == eyeColorListItem)
                {
                    User.Skin.GTAO_EYE_COLOR = index;
                    Sync.Data.Set(User.GetServerId(), "GTAO_EYE_COLOR", User.Skin.GTAO_EYE_COLOR);
                    Characher.UpdateFace(false);
                }
                if (item == eyeBrowsListItem)
                {
                    User.Skin.GTAO_EYEBROWS = index;
                    Sync.Data.Set(User.GetServerId(), "GTAO_EYEBROWS", User.Skin.GTAO_EYEBROWS);
                    Characher.UpdateFace(false);
                }
                if (item == eyeBrowsColorListItem)
                {
                    User.Skin.GTAO_EYEBROWS_COLOR = index;
                    Sync.Data.Set(User.GetServerId(), "GTAO_EYEBROWS_COLOR", User.Skin.GTAO_EYEBROWS_COLOR);
                    Characher.UpdateFace(false);
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowCharacherCustomFatherMenu()
        {
            await Delay(500);
            HideMenu();
            User.SetSkin("mp_m_freemode_01");
            
            Characher.UpdateFace(false);
            Characher.UpdateCloth(false);
            
            var list = new List<dynamic>();
            for (var i = 0; i < 46; i++)
                list.Add(i);
            
            var menu = new Menu();
            UiMenu = menu.Create("Персонаж - Отец", "~b~Влево/вправо менять внешность");
            
            var faceList = menu.AddMenuItemList(UiMenu, "Лицо", list);
            
            list = new List<dynamic>();
            for (var i = 0; i < 12; i++)
                list.Add(i);
            
            var skinList = menu.AddMenuItemList(UiMenu, "Кожа", list);
            var doneBtn = menu.AddMenuItem(UiMenu, "~g~Готово");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == doneBtn)
                    ShowCharacherCustomMenu();
            };
            
            UiMenu.OnListChange += (sender, item, index) =>
            {
                if (item == faceList)
                {
                    User.Skin.GTAO_SHAPE_SECOND_ID = index;
                    Sync.Data.Set(User.GetServerId(), "GTAO_SHAPE_SECOND_ID", User.Skin.GTAO_SHAPE_SECOND_ID);
                    SetPedHeadBlendData(
                        GetPlayerPed(-1),
                        User.Skin.GTAO_SHAPE_SECOND_ID,    
                        0,    
                        0,    
                        User.Skin.GTAO_SKIN_SECOND_ID,    
                        0,    
                        0,    
                        0,    
                        0,    
                        0,    
                        false    
                    );
                }
                if (item == skinList)
                {
                    User.Skin.GTAO_SKIN_SECOND_ID = index;
                    Sync.Data.Set(User.GetServerId(), "GTAO_SKIN_SECOND_ID", User.Skin.GTAO_SKIN_SECOND_ID);
                    SetPedHeadBlendData(
                        GetPlayerPed(-1),
                        User.Skin.GTAO_SHAPE_SECOND_ID,    
                        0,    
                        0,    
                        User.Skin.GTAO_SKIN_SECOND_ID,    
                        0,    
                        0,    
                        0,    
                        0,    
                        0,    
                        false    
                    );
                }
            };
        
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowCharacherCustomMotherMenu()
        {
            await Delay(500);
            HideMenu();
            User.SetSkin("mp_f_freemode_01");
            
            Characher.UpdateFace(false);
            Characher.UpdateCloth(false);
            
            var list = new List<dynamic>();
            for (var i = 0; i < 46; i++)
                list.Add(i);
            
            var menu = new Menu();
            UiMenu = menu.Create("Персонаж - Мать", "~b~Влево/вправо менять внешность");
            
            var faceList = menu.AddMenuItemList(UiMenu, "Лицо", list);
            
            list = new List<dynamic>();
            for (var i = 0; i < 12; i++)
                list.Add(i);
            
            var skinList = menu.AddMenuItemList(UiMenu, "Кожа", list);
            var doneBtn = menu.AddMenuItem(UiMenu, "~g~Готово");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == doneBtn)
                    ShowCharacherCustomMenu();
            };
            
            UiMenu.OnListChange += (sender, item, index) =>
            {
                if (item == faceList)
                {
                    User.Skin.GTAO_SHAPE_THRID_ID = index;
                    Sync.Data.Set(User.GetServerId(), "GTAO_SHAPE_THRID_ID", User.Skin.GTAO_SHAPE_THRID_ID);
                    SetPedHeadBlendData(
                        GetPlayerPed(-1),
                        User.Skin.GTAO_SHAPE_THRID_ID,    
                        0,    
                        0,    
                        User.Skin.GTAO_SKIN_THRID_ID,    
                        0,    
                        0,    
                        0,    
                        0,    
                        0,    
                        false    
                    );
                }
                if (item == skinList)
                {
                    User.Skin.GTAO_SKIN_THRID_ID = index;
                    Sync.Data.Set(User.GetServerId(), "GTAO_SKIN_THRID_ID", User.Skin.GTAO_SKIN_THRID_ID);
                    SetPedHeadBlendData(
                        GetPlayerPed(-1),
                        User.Skin.GTAO_SHAPE_THRID_ID,    
                        0,    
                        0,    
                        User.Skin.GTAO_SKIN_THRID_ID,    
                        0,    
                        0,    
                        0,    
                        0,    
                        0,    
                        false    
                    );
                }
            };
        
            MenuPool.Add(UiMenu);
        }

        public static void ShowVehicleStatsMenu(CitizenFX.Core.Vehicle veh)
        {
            HideMenu();
            
            var vehId = Managers.Vehicle.GetVehicleIdByNumber(GetVehicleNumberPlateText(veh.Handle));
            var menu = new Menu();

            if (vehId == -1)
            {
                var vehNpcItem = VehInfo.Get(veh.Model.Hash);

                UiMenu = menu.Create("Транспорт", "~b~Модель: ~s~" + vehNpcItem.DisplayName);

                menu.AddMenuItem(UiMenu, "~b~Расход топлива:~s~").SetRightLabel(vehNpcItem.FuelMinute + "л.");
                menu.AddMenuItem(UiMenu, "~b~Вместимость бака:~s~").SetRightLabel(vehNpcItem.FullFuel + "л.");
                menu.AddMenuItem(UiMenu, "~b~Объем багажника:~s~").SetRightLabel(vehNpcItem.Stock + "см³");
                menu.AddMenuItem(UiMenu, "~b~Вместимость багажника:~s~").SetRightLabel(vehNpcItem.StockFull + "гр.");

                var backBtn = menu.AddMenuItem(UiMenu, "~g~Назад");
                var closeBtn = menu.AddMenuItem(UiMenu, "~r~Закрыть");

                UiMenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == closeBtn)
                        HideMenu();
                    if (item == backBtn)
                        ShowVehicleMenu(veh);
                };

                MenuPool.Add(UiMenu);
                return;
            }
            
            var vehItem = Managers.Vehicle.VehicleInfoGlobalDataList[vehId];
            UiMenu = menu.Create("Транспорт", "~b~Модель: ~s~" + vehItem.DisplayName);
            
            menu.AddMenuItem(UiMenu, "~b~Расход топлива:~s~").SetRightLabel(vehItem.FuelMinute + "л.");
            menu.AddMenuItem(UiMenu, "~b~Вместимость бака:~s~").SetRightLabel(vehItem.FullFuel + "л.");
            menu.AddMenuItem(UiMenu, "~b~Объем багажника:~s~").SetRightLabel(vehItem.Stock + "см³");
            menu.AddMenuItem(UiMenu, "~b~Вместимость багажника:~s~").SetRightLabel(vehItem.StockFull + "гр.");

            if (vehItem.IsUserOwner)
            {
                menu.AddMenuItem(UiMenu, "~b~=============[~s~Состояние~b~]=============");
                menu.AddMenuItem(UiMenu, "~b~Пробег:~s~").SetRightLabel(Convert.ToInt32(vehItem.SMp) + " миль");
                menu.AddMenuItem(UiMenu, "~b~Масло:~s~").SetRightLabel(Managers.Vehicle.CheckOil(vehItem.SOil));
                
                menu.AddMenuItem(UiMenu, "~b~Кузов:~s~").SetRightLabel(Managers.Vehicle.GetTypeHealth(vehItem.SBody));
                menu.AddMenuItem(UiMenu, "~b~Колеса:~s~").SetRightLabel(Managers.Vehicle.GetTypeHealth(vehItem.SWhBkl));

                switch (veh.ClassType)
                {
                    case VehicleClass.Boats:
                    case VehicleClass.Helicopters:
                    case VehicleClass.Planes:
                    case VehicleClass.Trains:
                    case VehicleClass.Cycles:
                        break;
                    case VehicleClass.Motorcycles: 
                        menu.AddMenuItem(UiMenu, "~b~Двигатель:~s~").SetRightLabel(Managers.Vehicle.GetTypeHealth(vehItem.SEngine));
                        menu.AddMenuItem(UiMenu, "~b~Подвеска:~s~").SetRightLabel(Managers.Vehicle.GetTypeHealth(vehItem.SSuspension));
                        break;
                    default:
                        menu.AddMenuItem(UiMenu, "~b~Двигатель:~s~").SetRightLabel(Managers.Vehicle.GetTypeHealth(vehItem.SEngine));
                        menu.AddMenuItem(UiMenu, "~b~Подвеска:~s~").SetRightLabel(Managers.Vehicle.GetTypeHealth(vehItem.SSuspension));
                        break;
                }
            }
          
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowVehicleMenu(veh);
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowVehicleDoMenu(CitizenFX.Core.Vehicle veh)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Транспорт", "~b~Управление транспортом");
                
            var vehIndicator1Btn = menu.AddMenuItem(UiMenu, "~g~Вкл~s~ / ~r~выкл~s~ Левый поворотник", "Кнопка ~g~Стрелка влево");
            var vehIndicator2Btn = menu.AddMenuItem(UiMenu, "~g~Вкл~s~ / ~r~выкл~s~ Правый поворотник", "Кнопка ~g~Стрелка вправо");
            var vehIndicator3Btn = menu.AddMenuItem(UiMenu, "~g~Вкл~s~ / ~r~выкл~s~ Аварийку");

            if (VehInfo.GetClassName(veh.Model.Hash) != "Helicopters" &&
                VehInfo.GetClassName(veh.Model.Hash) != "Planes" &&
                VehInfo.GetClassName(veh.Model.Hash) != "Boats" &&
                VehInfo.GetClassName(veh.Model.Hash) != "Cycles" &&
                VehInfo.GetClassName(veh.Model.Hash) != "Motorcycles"
                )
            {
                var listWindow = new List<dynamic> {"Опустить", "Поднять"};
            
                menu.AddMenuItemList(UiMenu, "Переднее левое окно", listWindow).OnListSelected += (uimenu, idx) =>
                {
                    HideMenu();
                    if (idx == 0)
                        RemoveVehicleWindow(veh.Handle, 0);
                    else
                        FixVehicleWindow(veh.Handle, 0);
                };
                menu.AddMenuItemList(UiMenu, "Переднее правое окно", listWindow).OnListSelected += (uimenu, idx) =>
                {
                    HideMenu();
                    if (idx == 0)
                        RemoveVehicleWindow(veh.Handle, 1);
                    else
                        FixVehicleWindow(veh.Handle, 1);
                };
            
                if (GetVehicleModelNumberOfSeats((uint) veh.Model.Hash) > 2)
                {
                    menu.AddMenuItemList(UiMenu, "Заднее левое окно", listWindow).OnListSelected += (uimenu, idx) =>
                    {
                        HideMenu();
                        if (idx == 0)
                            RemoveVehicleWindow(veh.Handle, 2);
                        else
                            FixVehicleWindow(veh.Handle, 2);
                    };
                    menu.AddMenuItemList(UiMenu, "Заднее правое окно", listWindow).OnListSelected += (uimenu, idx) =>
                    {
                        HideMenu();
                        if (idx == 0)
                            RemoveVehicleWindow(veh.Handle, 3);
                        else
                            FixVehicleWindow(veh.Handle, 3);
                    };
                }
            }
            
            menu.AddMenuItem(UiMenu, "~g~Откр~s~ / ~r~закр~s~ Капот").Activated += (sender, item) =>
            {
                if (veh.Doors[VehicleDoorIndex.Hood].IsOpen)
                    veh.Doors[VehicleDoorIndex.Hood].Close();
                else
                    veh.Doors[VehicleDoorIndex.Hood].Open();
            };
            menu.AddMenuItem(UiMenu, "~g~Откр~s~ / ~r~закр~s~ Багажник").Activated += (sender, item) =>
            {
                if (veh.Doors[VehicleDoorIndex.Trunk].IsOpen)
                    veh.Doors[VehicleDoorIndex.Trunk].Close();
                else
                    veh.Doors[VehicleDoorIndex.Trunk].Open();
            };
            
            var backBtn = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeBtn = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeBtn)
                    HideMenu();
                if (item == backBtn)
                    ShowVehicleMenu(veh);
                if (item == vehIndicator3Btn)
                {
                    if (GetVehicleIndicatorLights(veh.Handle) != 3) // either all lights are off, or one of the two (left/right) is off.
                    {
                        Managers.Vehicle.EnableLeftIndicator(veh, true);
                        Managers.Vehicle.EnableRightIndicator(veh, true);
                    }
                    else // both are on.
                    {
                        Managers.Vehicle.EnableLeftIndicator(veh, false);
                        Managers.Vehicle.EnableRightIndicator(veh, false);
                    }
                }
                if (item == vehIndicator1Btn)
                {
                    if (GetVehicleIndicatorLights(veh.Handle) != 1) // Left indicator is (only) off
                    {
                        Managers.Vehicle.EnableLeftIndicator(veh, true);
                        Managers.Vehicle.EnableRightIndicator(veh, false);
                    }
                    else
                    {
                        Managers.Vehicle.EnableLeftIndicator(veh, false);
                        Managers.Vehicle.EnableRightIndicator(veh, false);
                    }
                }
                if (item == vehIndicator2Btn)
                {
                    if (GetVehicleIndicatorLights(veh.Handle) != 2) // Left indicator is (only) off
                    {
                        Managers.Vehicle.EnableLeftIndicator(veh, false);
                        Managers.Vehicle.EnableRightIndicator(veh, true);
                    }
                    else
                    {
                        Managers.Vehicle.EnableLeftIndicator(veh, false);
                        Managers.Vehicle.EnableRightIndicator(veh, false);
                    }
                }
            };
                
            MenuPool.Add(UiMenu);
        }

        public static void ShowVehicleAutoPilotMenu(CitizenFX.Core.Vehicle veh)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Транспорт", "~b~Автопилот");
            
            menu.AddMenuItem(UiMenu, "~g~Вкл~s~ / ~r~выкл~s~ автопилот").Activated += (sender, item) =>
            {
                Managers.Vehicle.SwitchAutopilot(veh);
            };
            
            var list = new List<dynamic> {"20", "30", "40", "50", "60", "70", "80"};
            menu.AddMenuItemList(UiMenu, "Скорость автопилота", list, "Нажмите ~g~Enter~s~ чтобы применить").OnListSelected += (sender, idx) =>
            {
                Managers.Vehicle.AutoPilotSpeed = Convert.ToInt32(Convert.ToInt32(list[idx]) / 2.2);
                Notification.SendWithTime("~g~Скорость автопилота: " + Convert.ToInt32(list[idx]));

                if (Client.Sync.Data.HasLocally(User.GetServerId(), "autopilot"))
                {
                    ClearPedTasks(GetPlayerPed(-1));
                    TaskVehicleDriveToCoordLongrange(GetPlayerPed(-1), veh.Handle, World.WaypointPosition.X,
                        World.WaypointPosition.Y, World.GetGroundHeight(World.WaypointPosition), Managers.Vehicle.AutoPilotSpeed, 786603,
                        20.0f);
                }
            };
            
            var backBtn = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeBtn = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeBtn)
                    HideMenu();
                if (item == backBtn)
                    ShowVehicleMenu(veh);
            };
                
            MenuPool.Add(UiMenu);
        }

        public static void ShowVehicleSpeedLimitMenu(CitizenFX.Core.Vehicle veh)
        {
            HideMenu();
            if (VehInfo.GetClassName(veh.Model.Hash) != "Helicopters" &&
                VehInfo.GetClassName(veh.Model.Hash) != "Planes" &&
                VehInfo.GetClassName(veh.Model.Hash) != "Cycles" &&
                VehInfo.GetClassName(veh.Model.Hash) != "Motorcycles"
            )
            {
                var menu = new Menu();
                UiMenu = menu.Create("Транспорт", "~b~Ограничитель скорости");

                menu.AddMenuItem(UiMenu, "~g~Вкл~s~ / ~r~выкл~s~ ограничитель скорости").Activated += (sender, item) =>
                {
                    if (!Client.Sync.Data.HasLocally(User.GetServerId(), "speedlimit"))
                    {
                        veh.MaxSpeed = Managers.Vehicle.MaxSpeed;
                        Client.Sync.Data.SetLocally(User.GetServerId(), "speedlimit", true);
                        Notification.SendWithTime("~g~Ограничитель скорости активирован");
                    }
                    else
                    {
                        veh.MaxSpeed = GetVehicleHandlingFloat(veh.Handle, "CHandlingData", "fInitialDriveMaxFlatVel");
                        Client.Sync.Data.ResetLocally(User.GetServerId(), "speedlimit");
                        Notification.SendWithTime("~g~Ограничитель скорости деактивирован");
                    }
                };

                var list = new List<dynamic> {"5", "10", "20", "30", "40", "50", "60", "70", "80", "100"};

                menu.AddMenuItemList(UiMenu, "Допустимая скорость", list, "Нажмите ~g~Enter~s~ чтобы применить")
                    .OnListSelected += (sender, idx) =>
                {
                    Managers.Vehicle.MaxSpeed = Convert.ToInt32(Convert.ToInt32(list[idx]) / 2.2);
                    Notification.SendWithTime("~g~Скорость: " + Convert.ToInt32(list[idx]));
                };

                var backBtn = menu.AddMenuItem(UiMenu, "~g~Назад");
                var closeBtn = menu.AddMenuItem(UiMenu, "~r~Закрыть");

                UiMenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == closeBtn)
                    HideMenu();
                    if (item == backBtn)
                    ShowVehicleMenu(veh);
                };

                MenuPool.Add(UiMenu);
            }
        }
        
        public static void ShowCopMegaphoneMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Мегафон", "~b~Меню реплик мегафона");
            
            menu.AddMenuItem(UiMenu, "Остановить пешехода").Activated += async (uimenu, item) =>
            {
                if (Sync.Data.HasLocally(User.GetServerId(), "isTimeoutMegaphone"))
                {
                    Notification.SendWithTime("~r~Доступно раз в 20 секунд");
                    return;
                }

                Sync.Data.SetLocally(User.GetServerId(), "isTimeoutMegaphone", true);
                uint spawnModel = (uint) GetHashKey("player_one");
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
    
                if (!await Main.LoadModel(spawnModel))
                    return;
                
                var ped = CreatePed(6, spawnModel, pos.X, pos.Y, pos.Z, 0, true, false);
        
                AttachEntityToEntity(ped, GetPlayerPed(-1), 0, 0, 0, 0, 0, 0, 0, false, false, false, true, 0, true);
                
                //AttachEntityToEntity(nPlayerPed, copPlayerPed, 28422, -0.0300,0.0000,-0.1100,0.0000,0.0000,86.0000, false, true, false, false, 2, true)
                
                var p = new CitizenFX.Core.Ped(ped);
        
                SetEntityAlpha(ped, 0, GetHashKey("player_one"));
                //NetworkFadeOutEntity(ped, true, false);
        
                await Delay(2000);
                Shared.TriggerEventToAllPlayers("ARP:TalkNpc", p.Model.Hash, "STOP_ON_FOOT_MEGAPHONE", (User.Skin.SEX == 1 ? "S_F_Y_COP_01_WHITE_FULL_01" : "S_M_Y_COP_01_WHITE_FULL_01"), "SPEECH_PARAMS_FORCE_MEGAPHONE");

                await Delay(10000);
                p.Delete();
                Sync.Data.ResetLocally(User.GetServerId(), "isTimeoutMegaphone");
            };
            
            menu.AddMenuItem(UiMenu, "Остановить ТС").Activated += async (uimenu, item) =>
            {
                if (Sync.Data.HasLocally(User.GetServerId(), "isTimeoutMegaphone"))
                {
                    Notification.SendWithTime("~r~Доступно раз в 20 секунд");
                    return;
                }

                Sync.Data.SetLocally(User.GetServerId(), "isTimeoutMegaphone", true);
                uint spawnModel = (uint) GetHashKey("player_one");
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
    
                if (!await Main.LoadModel(spawnModel))
                    return;
                var ped = CreatePed(6, spawnModel, pos.X, pos.Y, pos.Z, 0, true, false);
        
                AttachEntityToEntity(ped, GetPlayerPed(-1), 0, 0, 0, 0, 0, 0, 0, false, false, false, true, 0, true);
        
                var p = new CitizenFX.Core.Ped(ped);
        
                SetEntityAlpha(ped, 0, GetHashKey("player_one"));
                //NetworkFadeOutEntity(ped, true, false);
        
                await Delay(2000);
                Shared.TriggerEventToAllPlayers("ARP:TalkNpc", p.Model.Hash, "STOP_VEHICLE_CAR_MEGAPHONE", (User.Skin.SEX == 1 ? "S_F_Y_COP_01_WHITE_FULL_01" : "S_M_Y_COP_01_WHITE_FULL_01"), "SPEECH_PARAMS_FORCE_MEGAPHONE");

                await Delay(10000);
                p.Delete();
                Sync.Data.ResetLocally(User.GetServerId(), "isTimeoutMegaphone");
            };
            
            menu.AddMenuItem(UiMenu, "Остановить ТС (агрессивно)").Activated += async (uimenu, item) =>
            {
                uint spawnModel = (uint) GetHashKey("player_one");
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
            
                if (!await Main.LoadModel(spawnModel))
                    return;
                var ped = CreatePed(6, spawnModel, pos.X, pos.Y, pos.Z, 0, true, false);
                
                AttachEntityToEntity(ped, GetPlayerPed(-1), 0, 0, 0, 0, 0, 0, 0, false, false, false, true, 0, true);
                
                var p = new CitizenFX.Core.Ped(ped)
                {
                    //IsVisible = false,
                    //IsInvincible = true
                };
                
                SetEntityAlpha(ped, 0, GetHashKey("player_one"));
                //NetworkFadeOutEntity(ped, true, false);
                
                await Delay(2000);
                Shared.TriggerEventToAllPlayers("ARP:TalkNpc", p.Model.Hash, "STOP_VEHICLE_CAR_WARNING_MEGAPHONE", (User.Skin.SEX == 1 ? "S_F_Y_COP_01_WHITE_FULL_01" : "S_M_Y_COP_01_WHITE_FULL_01"), "SPEECH_PARAMS_FORCE_MEGAPHONE");

                await Delay(10000);
                p.Delete();
            };
            
            menu.AddMenuItem(UiMenu, "Покинуть зону").Activated += async (uimenu, item) =>
            {
                if (Sync.Data.HasLocally(User.GetServerId(), "isTimeoutMegaphone"))
                {
                    Notification.SendWithTime("~r~Доступно раз в 20 секунд");
                    return;
                }

                Sync.Data.SetLocally(User.GetServerId(), "isTimeoutMegaphone", true);
                uint spawnModel = (uint) GetHashKey("player_one");
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
    
                if (!await Main.LoadModel(spawnModel))
                    return;
                
                var ped = CreatePed(6, spawnModel, pos.X, pos.Y, pos.Z, 0, true, false);
        
                AttachEntityToEntity(ped, GetPlayerPed(-1), 0, 0, 0, 0, 0, 0, 0, false, false, false, true, 0, true);
        
                var p = new CitizenFX.Core.Ped(ped);
        
                SetEntityAlpha(ped, 0, GetHashKey("player_one"));
                //NetworkFadeOutEntity(ped, true, false);
        
                await Delay(2000);
                Shared.TriggerEventToAllPlayers("ARP:TalkNpc", p.Model.Hash, "CLEAR_AREA_MEGAPHONE", (User.Skin.SEX == 1 ? "S_F_Y_COP_01_WHITE_FULL_01" : "S_M_Y_COP_01_WHITE_FULL_01"), "SPEECH_PARAMS_FORCE_MEGAPHONE");

                await Delay(10000);
                p.Delete();
                Sync.Data.ResetLocally(User.GetServerId(), "isTimeoutMegaphone");
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static async void ShowVehicleMenu(CitizenFX.Core.Vehicle veh)
        {
            HideMenu();

            string ownerName = "Государство";
            if (veh.Model.Hash == -956048545 || veh.Model.Hash == 1208856469 || veh.Model.Hash == 1884962369)
                ownerName = "Downtown Club Co.";
            if (veh.Model.Hash == 1747439474)
                ownerName = "Gruppe Sechs";
            
            var vehId = Managers.Vehicle.GetVehicleIdByNumber(GetVehicleNumberPlateText(veh.Handle));

            bool canEnabledRadar = false;

            switch (VehInfo.GetDisplayName(veh.Model.Hash).ToLower())
            {
                case "police":
                case "police2":
                case "police22":
                case "police3":
                case "police4":
                case "police5":
                case "policeold1":
                case "policeold2":
                case "fbi":
                case "fbi2":
                case "policet":
                case "pranger":
                case "sheriff":
                case "sheriff2":
                case "alamo":
                case "alamo1":
                case "alamo3":
                case "alamo4":
                case "alamo7":
                case "alligator":
                case "sahpbuffalo":
                case "policebuffalo":
                case "noosebuffalo":
                case "polbufsc2":
                case "hwaybul":
                case "hwaycoq42":
                case "scoutpol":
                case "scoutpol2":
                case "scpd1 buffalo":
                case "scpd4 vacca":
                case "scpd5 rocoto":
                case "scpd6 sentinel":
                case "scpd7 elegy2":
                case "scpd10 felon":
                case "scpd11 f620":
                case "Intercept2":
                        canEnabledRadar = true;
                    break;
                case "unk":
                    if (User.IsAdmin())
                        canEnabledRadar = true;
                    break;
            }

            if (vehId == -1)
            {
                var menu = new Menu();
                UiMenu = menu.Create("Транспорт", $"~b~Владелец: ~s~{ownerName}");
                
                var engineBtn = menu.AddMenuItem(UiMenu, "~g~Вкл~s~ / ~r~выкл~s~ двигатель");
                var vehItemNoOwner = VehInfo.Get(veh.Model.Hash);
                if (vehItemNoOwner.FullFuel == 1)
                {
                    menu.AddMenuItem(UiMenu, "Автопилот").Activated += (sender, item) =>
                    {
                        HideMenu();
                        ShowVehicleAutoPilotMenu(veh);
                    };
                }
                if (canEnabledRadar)
                {
                    menu.AddMenuItem(UiMenu, "~g~Вкл~s~ / ~r~выкл~s~ радар").Activated += (sender, item) =>
                    {
                        HideMenu();
                        UI.IsShowRadar = !UI.IsShowRadar;
                    };
                    
                    /*menu.AddMenuItem(UiMenu, "Мегафон").Activated += (sender, item) =>
                    {
                        HideMenu();
                        ShowCopMegaphoneMenu();
                    };*/
                }
                
                if (veh.Model.Hash == -956048545 || veh.Model.Hash == 1208856469 || veh.Model.Hash == 1884962369 || veh.Model.Hash == 2088999036)
                {
                    menu.AddMenuItem(UiMenu, "~y~Меню таксиста").Activated += (sender, item) =>
                    {
                        HideMenu();
                        ShowPlayerTaxiTaskMenu(veh);
                    };
                }
                
                menu.AddMenuItem(UiMenu, "Ограничитель скорости").Activated += (sender, item) =>
                {
                    HideMenu();
                    ShowVehicleSpeedLimitMenu(veh);
                };
                
                /*menu.AddMenuItem(UiMenu, "Такси 1", $"Цена: ~g~$150").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Business.Rent.BuyTaxi(VehicleHash.Taxi, 150, shopId);
                };
                menu.AddMenuItem(UiMenu, "Такси 2", $"Цена: ~g~$150").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Business.Rent.BuyTaxi((VehicleHash) 1208856469, 150, shopId);
                };
                menu.AddMenuItem(UiMenu, "Такси 3", $"Цена: ~g~$150").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Business.Rent.BuyTaxi((VehicleHash) 1884962369, 150, shopId);
                };*/
                
                var vehStatsBtn = menu.AddMenuItem(UiMenu, "Характеристики");
                var vehDoBtn = menu.AddMenuItem(UiMenu, "Управление транспортом");
                
                menu.AddMenuItem(UiMenu, "~y~Выкинуть из транспорта").Activated += (sender, item) =>
                {
                    ShowPlayerEjectMenu(Main.GetPlayerListOnVehicle());
                };
                
                var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
             
                UiMenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == closeButton)
                        HideMenu();
                    if (item == engineBtn)
                    {
                        HideMenu();
                        Managers.Vehicle.Engine(veh);
                    }
                    if (item == vehStatsBtn)
                        ShowVehicleStatsMenu(veh);
                    if (item == vehDoBtn)
                        ShowVehicleDoMenu(veh);
                };
                
                MenuPool.Add(UiMenu);
                return;
            }

            //var vehItem = Managers.Vehicle.VehicleInfoGlobalDataList[vehId];
            var vehItem = await Managers.Vehicle.GetAllData(vehId);

            if (!vehItem.IsUserOwner)
            {
                ownerName = vehItem.FractionId != 0 ? Main.GetFractionName(vehItem.FractionId) : Main.GetCompanyName(vehItem.Job);
                var menu = new Menu();
                UiMenu = menu.Create("Транспорт", $"~b~Владелец: ~s~{ownerName}");
                
                var engineBtn = menu.AddMenuItem(UiMenu, "~g~Вкл~s~ / ~r~выкл~s~ двигатель");
                if (vehItem.FullFuel == 1 && vehItem.price > 49000)
                {
                    menu.AddMenuItem(UiMenu, "Автопилот").Activated += (sender, item) =>
                    {
                        HideMenu();
                        ShowVehicleAutoPilotMenu(veh);
                    };
                }
                if (canEnabledRadar)
                {
                    menu.AddMenuItem(UiMenu, "~g~Вкл~s~ / ~r~выкл~s~ радар").Activated += (sender, item) =>
                    {
                        HideMenu();
                        UI.IsShowRadar = !UI.IsShowRadar;
                    };
                    
                    /*menu.AddMenuItem(UiMenu, "Мегафон").Activated += (sender, item) =>
                    {
                        HideMenu();
                        ShowCopMegaphoneMenu();
                    };*/
                }
                
                if (veh.Model.Hash == -956048545 || veh.Model.Hash == 1208856469 || veh.Model.Hash == 1884962369 || veh.Model.Hash == 2088999036)
                {
                    menu.AddMenuItem(UiMenu, "~y~Меню таксиста").Activated += (sender, item) =>
                    {
                        HideMenu();
                        ShowPlayerTaxiTaskMenu(veh);
                    };
                }
                
                menu.AddMenuItem(UiMenu, "Ограничитель скорости").Activated += (sender, item) =>
                {
                    HideMenu();
                    ShowVehicleSpeedLimitMenu(veh);
                };
                menu.AddMenuItem(UiMenu, "Топливо", $"Топливо: ~g~{Math.Round(vehItem.Fuel, 2)}");
                var vehStatsBtn = menu.AddMenuItem(UiMenu, "Характеристики");
                var vehDoBtn = menu.AddMenuItem(UiMenu, "Управление транспортом");
                var vehLockButton = menu.AddMenuItem(UiMenu, "~g~Открыть ~s~/ ~r~Закрыть~s~");
                
                menu.AddMenuItem(UiMenu, "~y~Выкинуть из транспорта").Activated += (sender, item) =>
                {
                    ShowPlayerEjectMenu(Main.GetPlayerListOnVehicle());
                };

                if (vehItem.Job == "trash")
                {
                    menu.AddMenuItem(UiMenu, "~g~Разгрузить мусор").Activated += (sender, item) =>
                    {
                        Jobs.Trash.UnloadTrash(veh);
                    };
                    menu.AddMenuItem(UiMenu, "~b~Справка").Activated += (sender, item) =>
                    {
                        UI.ShowToolTip("~b~Справка\n~s~Ищите мусорные пакеты и коробки, собирайте их и грузите в транспорт. Затем разгрузите транспорт в соответсвующей зоне (Если нажмете разгрузить, будет метка)");
                    };
                }
                if (vehItem.Job == "bus1")
                {
                    menu.AddMenuItem(UiMenu, "~g~Начать рейс", "Зарплата: ~g~$200").Activated += (sender, item) =>
                    {
                        Jobs.Bus.Start(1);
                    };
                    menu.AddMenuItem(UiMenu, "~r~Завершить рейс", "Завершение рейса досрочно").Activated += (sender, item) =>
                    {
                        Jobs.Bus.Stop();
                    };
                }
                if (vehItem.Job == "bus2")
                {
                    menu.AddMenuItem(UiMenu, "~g~Начать рейс", "Зарплата: ~g~$70").Activated += (sender, item) =>
                    {
                        Jobs.Bus.Start(2);
                    };
                    menu.AddMenuItem(UiMenu, "~r~Завершить рейс", "Завершение рейса досрочно").Activated += (sender, item) =>
                    {
                        Jobs.Bus.Stop();
                    };
                }
                if (vehItem.Job == "bus3")
                {
                    menu.AddMenuItem(UiMenu, "~g~Начать рейс", "Зарплата: ~g~$365").Activated += (sender, item) =>
                    {
                        Jobs.Bus.Start(3);
                    };
                    menu.AddMenuItem(UiMenu, "~r~Завершить рейс", "Завершение рейса досрочно").Activated += (sender, item) =>
                    {
                        Jobs.Bus.Stop();
                    };
                }
                if (vehItem.Job == "mail" || vehItem.Job == "mail2")
                {
                    menu.AddMenuItem(UiMenu, "~g~Взять почту из транспорта").Activated += (sender, item) =>
                    {
                        Jobs.Mail.TakeMail();
                    };
                }
                if (vehItem.Job == "scrap")
                {
                    menu.AddMenuItem(UiMenu, "~g~Загрузить металлолом").Activated += (sender, item) =>
                    {
                        Jobs.Scrap.Load();
                    };
                    menu.AddMenuItem(UiMenu, "~g~Разгрузить металлолом", "Зарплата: ~g~$50").Activated += (sender, item) =>
                    {
                        HideMenu();
                        Jobs.Scrap.UnLoad();
                    };
                }
                if (vehItem.Job == "three")
                {
                    menu.AddMenuItem(UiMenu, "~g~Получить задание", "Зарплата: ~g~$25").Activated += (sender, item) =>
                    {
                        HideMenu();
                        Jobs.Gardener.Start();
                    };
                }
                if (vehItem.Job == "GrSix" && User.Data.job == "GrSix")
                {
                    menu.AddMenuItem(UiMenu, "Код 0", "Необходима немедленная поддержка, нападение на экипаж").Activated += (sender, item) =>
                    {
                        HideMenu();
                        Dispatcher.SendEms("Код 0", "Нападение на экипаж инкассаторского автомобиля");
                    };
                    menu.AddMenuItem(UiMenu, "~g~Получить задание", "~y~Зарплата зависит от кол-ва точек и суммы").Activated += (sender, item) =>
                    {
                        HideMenu();
                        Jobs.GroupSix.Start();
                    };
                    menu.AddMenuItem(UiMenu, "~g~Денег в машине").Activated += (sender, item) =>
                    {
                        TriggerServerEvent("ARP:GrSix:MoneyInCarCheck", veh.NetworkId);
                    };
                    menu.AddMenuItem(UiMenu, "Сдать ТС", "~y~Зарплата зависит от кол-ва точек").Activated += (sender, item) =>
                        {
                            HideMenu();
                            TriggerServerEvent("ARP:GrSix:Payment", veh.NetworkId);
                        };
                }
                /*if (vehItem.Hash == 1747439474 && User.Data.job != "GrSix")
                {
                    menu.AddMenuItem(UiMenu, "~r~Ограбить").Activated += (sender, item) =>
                    {
                        HideMenu();
                        Managers.Grab.GrabGrSix();
                    };
                }*/
                if (vehItem.Job == "photo")
                {
                    menu.AddMenuItem(UiMenu, "~g~Получить задание", "Зарплата: ~g~$~70").Activated += (sender, item) =>
                    {
                        HideMenu();
                        Jobs.Photo.Start();
                    };
                }
                if (vehItem.Job == "bgstar")
                {
                    menu.AddMenuItem(UiMenu, "~g~Получить задание").Activated += (sender, item) =>
                    {
                        HideMenu();
                        Jobs.Bugstars.FindHouse();
                    };
                    menu.AddMenuItem(UiMenu, "~g~Взять инструменты").Activated += (sender, item) =>
                    {
                        Jobs.Bugstars.TakeTool();
                    };
                }
                if (vehItem.Job == "water")
                {
                    menu.AddMenuItem(UiMenu, "~g~Получить задание").Activated += (sender, item) =>
                    {
                        HideMenu();
                        Jobs.WaterPower.FindHouse();
                    };
                    menu.AddMenuItem(UiMenu, "~g~Взять инструменты").Activated += (sender, item) =>
                    {
                        Jobs.WaterPower.TakeTool();
                    };
                }
                if (vehItem.Job == "sunb")
                {
                    menu.AddMenuItem(UiMenu, "~g~Получить задание").Activated += (sender, item) =>
                    {
                        HideMenu();
                        Jobs.SunBeach.FindHouse();
                    };
                    menu.AddMenuItem(UiMenu, "~g~Взять инструменты").Activated += (sender, item) =>
                    {
                        Jobs.SunBeach.TakeTool();
                    };
                }
                if (vehItem.Job == "sground" || vehItem.Job == "swater")
                {
                    menu.AddMenuItem(UiMenu, "~g~Получить задание").Activated += (sender, item) =>
                    {
                        HideMenu();
                        Jobs.HumanLab.Start();
                    };
                    menu.AddMenuItem(UiMenu, "~g~Положить пробу").Activated += (sender, item) =>
                    {
                        Jobs.HumanLab.DropInCar(veh);
                    };
                    menu.AddMenuItem(UiMenu, "~g~Разгрузить транспорт").Activated += (sender, item) =>
                    {
                        HideMenu();
                        Jobs.HumanLab.UnloadCar(veh);
                    };
                }
              
                var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
                
                UiMenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == closeButton)
                        HideMenu();
                    if (item == engineBtn)
                    {
                        HideMenu();
                        
                        if (User.CanOpenVehicle(vehId, veh.Handle))
                            Managers.Vehicle.Engine(veh);
                        else
                            Notification.SendWithTime("~r~У Вас нет ключей");
                    }
                    if (item == vehLockButton)
                    {
                        HideMenu();
                        if (User.CanOpenVehicle(vehId, veh.Handle))
                            Managers.Vehicle.LockStatus(veh);
                        else
                            Notification.SendWithTime("~r~У Вас нет ключей");
                    
                    }
                    if (item == vehStatsBtn)
                        ShowVehicleStatsMenu(veh);
                    if (item == vehDoBtn)
                        ShowVehicleDoMenu(veh);
                };
                
                MenuPool.Add(UiMenu);
            }
            else
            {
                var menu = new Menu();
                UiMenu = menu.Create("Транспорт", $"~b~Владелец: ~s~{vehItem.user_name}");
                
                UIMenuItem engineBtn = null;
                UIMenuItem vehParkButton = null;
                
                //TODO Найти место куда будут продавать игроки тачки
                //UIMenuItem vehSellButton = null; В специальном месте продать тачку
                
                var userId = User.Data.id;

                if (vehItem.id_user == userId)
                {
                    engineBtn = menu.AddMenuItem(UiMenu, "~g~Вкл~s~ / ~r~выкл~s~ двигатель");
                    if (vehItem.FullFuel == 1 && vehItem.price > 49000)
                    {
                        menu.AddMenuItem(UiMenu, "Автопилот").Activated += (sender, item) =>
                        {
                            HideMenu();
                            ShowVehicleAutoPilotMenu(veh);
                        };
                    }
                }
                
                if (canEnabledRadar)
                {
                    menu.AddMenuItem(UiMenu, "~g~Вкл~s~ / ~r~выкл~s~ радар").Activated += (sender, item) =>
                    {
                        HideMenu();
                        UI.IsShowRadar = !UI.IsShowRadar;
                    };
                    
                    /*menu.AddMenuItem(UiMenu, "Мегафон").Activated += (sender, item) =>
                    {
                        HideMenu();
                        ShowCopMegaphoneMenu();
                    };*/
                }
                
                if (veh.Model.Hash == -956048545 || veh.Model.Hash == 1208856469 || veh.Model.Hash == 1884962369 || veh.Model.Hash == 2088999036)
                {
                    menu.AddMenuItem(UiMenu, "~y~Меню таксиста").Activated += (sender, item) =>
                    {
                        HideMenu();
                        ShowPlayerTaxiTaskMenu(veh);
                    };
                }
                
                menu.AddMenuItem(UiMenu, "Ограничитель скорости").Activated += (sender, item) =>
                {
                    HideMenu();
                    ShowVehicleSpeedLimitMenu(veh);
                };
                menu.AddMenuItem(UiMenu, "Топливо", $"Топливо: ~g~{Math.Round(vehItem.Fuel, 2)}");
                var vehStatsBtn = menu.AddMenuItem(UiMenu, "Характеристики");
                var vehDoBtn = menu.AddMenuItem(UiMenu, "Управление транспортом");
                var vehLockButton = menu.AddMenuItem(UiMenu, "~g~Открыть ~s~/ ~r~Закрыть~s~");
                
                menu.AddMenuItem(UiMenu, "~y~Выкинуть из транспорта").Activated += (sender, item) =>
                {
                    ShowPlayerEjectMenu(Main.GetPlayerListOnVehicle());
                };

                if (vehItem.id_user == userId)
                {
                    vehParkButton = menu.AddMenuItem(UiMenu, "Припарковать");
                    //vehSellButton = menu.AddMenuItem(UiMenu, "~r~Продать");

                    if (vehItem.neon_type > 0)
                    {
                        var listWindow = new List<dynamic> {"Вкл", "Выкл"};
            
                        menu.AddMenuItemList(UiMenu, "~b~Неон", listWindow).OnListSelected += (uimenu, idx) =>
                        {
                            HideMenu();
                            if (idx == 0)
                            {
                                SetVehicleNeonLightsColour(veh.Handle, vehItem.neon_r, vehItem.neon_g, vehItem.neon_b);
                                SetVehicleNeonLightEnabled(veh.Handle, 0, true);
                                SetVehicleNeonLightEnabled(veh.Handle, 1, true);
                                SetVehicleNeonLightEnabled(veh.Handle, 2, true);
                                SetVehicleNeonLightEnabled(veh.Handle, 3, true);
                            }
                            else
                            {
                                SetVehicleNeonLightsColour(veh.Handle, vehItem.neon_r, vehItem.neon_g, vehItem.neon_b);
                                SetVehicleNeonLightEnabled(veh.Handle, 0, false);
                                SetVehicleNeonLightEnabled(veh.Handle, 1, false);
                                SetVehicleNeonLightEnabled(veh.Handle, 2, false);
                                SetVehicleNeonLightEnabled(veh.Handle, 3, false);
                            }
                        };
                        
                        if (vehItem.neon_type > 1)
                        {
                            menu.AddMenuItem(UiMenu, "~b~Цвет неона").Activated += async (sender, item) =>
                            {
                                HideMenu();
                                int r = Convert.ToInt32(await Menu.GetUserInput("R", "", 3));
                                int g = Convert.ToInt32(await Menu.GetUserInput("G", "", 3));
                                int b = Convert.ToInt32(await Menu.GetUserInput("B", "", 3));

                                SetVehicleNeonLightsColour(veh.Handle, r, g, b);
                                SetVehicleNeonLightEnabled(veh.Handle, 0, true);
                                SetVehicleNeonLightEnabled(veh.Handle, 1, true);
                                SetVehicleNeonLightEnabled(veh.Handle, 2, true);
                                SetVehicleNeonLightEnabled(veh.Handle, 3, true);
                                
                                Sync.Data.Set(vehId + 110000, "neon_r", r);
                                Managers.Vehicle.VehicleInfoGlobalDataList[vehId].neon_r = r;
                                
                                Sync.Data.Set(vehId + 110000, "neon_g", g);
                                Managers.Vehicle.VehicleInfoGlobalDataList[vehId].neon_g = g;
                                
                                Sync.Data.Set(vehId + 110000, "neon_b", b);
                                Managers.Vehicle.VehicleInfoGlobalDataList[vehId].neon_b = b;
                            };
                        }
                    }
                }
              
                var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
                
                UiMenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == closeButton)
                        HideMenu();
                    else if (item == engineBtn)
                    {
                        HideMenu();
                        if (User.CanOpenVehicle(vehId, veh.Handle))
                            Managers.Vehicle.Engine(veh);
                        else
                            Notification.SendWithTime("~r~У Вас нет ключей");
                    }
                    else if (item == vehLockButton)
                    {
                        HideMenu();
                        if (User.CanOpenVehicle(vehId, veh.Handle))
                            Managers.Vehicle.LockStatus(veh);
                        else
                            Notification.SendWithTime("~r~У Вас нет ключей");
                    
                    }
                    else if (item == vehParkButton)
                    {
                        TriggerServerEvent("ARP:UpdateVehPark", vehItem.id, veh.Position.X, veh.Position.Y, veh.Position.Z, veh.Heading);
                        Notification.SendWithTime("~g~Транспорт припаркован");
                    }
                    else if (item == vehStatsBtn)
                        ShowVehicleStatsMenu(veh);
                    else if (item == vehDoBtn)
                        ShowVehicleDoMenu(veh);
                };
                
                MenuPool.Add(UiMenu);
            }
        }
        
        public static void ShowVehicleOutMenu(CitizenFX.Core.Vehicle veh)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Транспорт", "~b~Модель: ~s~" + veh.DisplayName);
      
            var vehLockButton = menu.AddMenuItem(UiMenu, "~g~Открыть ~s~/ ~r~Закрыть~s~");
            //var vehInfoButton = menu.AddMenuItem(UiMenu, "Информация о ТС", "Информация о ТС, который стоит рядом с вами");

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == vehLockButton)
                {
                    HideMenu();
                    var vehId = Managers.Vehicle.GetVehicleIdByNumber(GetVehicleNumberPlateText(veh.Handle));
                    if (User.CanOpenVehicle(vehId, veh.Handle))
                        Managers.Vehicle.LockStatus(veh);
                    else
                        Notification.SendWithTime("~r~У Вас нет ключей");
                    
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowVehicleOut2Menu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Транспорт", "~b~Ваш транспорт");

            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            
            if (User.Data.car_id1 > 0)
            {
                var vehicle = Managers.Vehicle.GetVehicleById(User.Data.car_id1);
                menu.AddMenuItem(UiMenu, "Найти ТС #1", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    User.SetWaypoint(vehicle.CurrentPosX, vehicle.CurrentPosY);
                };

                if (Managers.Vehicle.CanSpawn(vehicle))
                {
                    menu.AddMenuItem(UiMenu, "Зареспавнить ТС #1", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                    {
                        HideMenu();

                        /*if (Main.GetDistanceToSquared(new Vector3(vehicle.CurrentPosX, vehicle.CurrentPosY, vehicle.CurrentPosZ), playerPos) > 100f)
                        {
                            User.SetWaypoint(vehicle.CurrentPosX, vehicle.CurrentPosY);
                            Notification.SendWithTime("~g~Нужно быть рядом с парковочным местом");
                            return;
                        }*/
                        
                        Managers.Vehicle.SpawnVehicleByVehData(vehicle);
                    };
                }
            }
            
            if (User.Data.car_id2 > 0) {
                var vehicle = Managers.Vehicle.GetVehicleById(User.Data.car_id2);
                menu.AddMenuItem(UiMenu, "Найти ТС #2", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    User.SetWaypoint(vehicle.CurrentPosX, vehicle.CurrentPosY);
                };

                if (Managers.Vehicle.CanSpawn(vehicle))
                {
                    menu.AddMenuItem(UiMenu, "Зареспавнить ТС #2", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Managers.Vehicle.SpawnVehicleByVehData(vehicle);
                    };
                }
            }
            
            if (User.Data.car_id3 > 0) {
                var vehicle = Managers.Vehicle.GetVehicleById(User.Data.car_id3);
                menu.AddMenuItem(UiMenu, "Найти ТС #3", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    User.SetWaypoint(vehicle.CurrentPosX, vehicle.CurrentPosY);
                };

                if (Managers.Vehicle.CanSpawn(vehicle))
                {
                    menu.AddMenuItem(UiMenu, "Зареспавнить ТС #3", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Managers.Vehicle.SpawnVehicleByVehData(vehicle);
                    };
                }
            }
            
            if (User.Data.car_id4 > 0) {
                var vehicle = Managers.Vehicle.GetVehicleById(User.Data.car_id4);
                menu.AddMenuItem(UiMenu, "Найти ТС #4", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    User.SetWaypoint(vehicle.CurrentPosX, vehicle.CurrentPosY);
                };

                if (Managers.Vehicle.CanSpawn(vehicle))
                {
                    menu.AddMenuItem(UiMenu, "Зареспавнить ТС #4", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Managers.Vehicle.SpawnVehicleByVehData(vehicle);
                    };
                }
            }
            
            if (User.Data.car_id5 > 0) {
                var vehicle = Managers.Vehicle.GetVehicleById(User.Data.car_id5);
                menu.AddMenuItem(UiMenu, "Найти ТС #5", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    User.SetWaypoint(vehicle.CurrentPosX, vehicle.CurrentPosY);
                };

                if (Managers.Vehicle.CanSpawn(vehicle))
                {
                    menu.AddMenuItem(UiMenu, "Зареспавнить ТС #5", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Managers.Vehicle.SpawnVehicleByVehData(vehicle);
                    };
                }
            }
            
            if (User.Data.car_id6 > 0) {
                var vehicle = Managers.Vehicle.GetVehicleById(User.Data.car_id6);
                menu.AddMenuItem(UiMenu, "Найти ТС #6", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    User.SetWaypoint(vehicle.CurrentPosX, vehicle.CurrentPosY);
                };

                if (Managers.Vehicle.CanSpawn(vehicle))
                {
                    menu.AddMenuItem(UiMenu, "Зареспавнить ТС #6", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Managers.Vehicle.SpawnVehicleByVehData(vehicle);
                    };
                }
            }
            
            if (User.Data.car_id7 > 0) {
                var vehicle = Managers.Vehicle.GetVehicleById(User.Data.car_id7);
                menu.AddMenuItem(UiMenu, "Найти ТС #7", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    User.SetWaypoint(vehicle.CurrentPosX, vehicle.CurrentPosY);
                };

                if (Managers.Vehicle.CanSpawn(vehicle))
                {
                    menu.AddMenuItem(UiMenu, "Зареспавнить ТС #7", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Managers.Vehicle.SpawnVehicleByVehData(vehicle);
                    };
                }
            }
            
            if (User.Data.car_id8 > 0) {
                var vehicle = Managers.Vehicle.GetVehicleById(User.Data.car_id8);
                menu.AddMenuItem(UiMenu, "Найти ТС #8", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    User.SetWaypoint(vehicle.CurrentPosX, vehicle.CurrentPosY);
                };

                if (Managers.Vehicle.CanSpawn(vehicle))
                {
                    menu.AddMenuItem(UiMenu, "Зареспавнить ТС #8", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Managers.Vehicle.SpawnVehicleByVehData(vehicle);
                    };
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowSellVehicleToUserMenu(int serverId, int vId, string number, string displayName, int price)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Транспорт", "~b~Модель: ~s~" + displayName);
      
            menu.AddMenuItem(UiMenu, "~g~Купить", $"~b~Цена:~s~ ${price:#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Vehicle.CheckSellToUser(serverId, vId, price);
            };
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Не покупать");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void SellBusinessToUserShowMenu(int serverId, int bId, string name, int price)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Бизнес", "~b~Название: ~s~" + name);
      
            menu.AddMenuItem(UiMenu, "~g~Купить", $"~b~Цена:~s~ ${price:#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Business.AcceptBuy(serverId, bId, price);
            };
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Не покупать");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void SellHouseToUserShowMenu(int serverId, int hId, string name, int price)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Дом", "~b~Название: ~s~" + name);
      
            menu.AddMenuItem(UiMenu, "~g~Купить", $"~b~Цена:~s~ ${price:#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                House.AcceptBuy(serverId, hId, price);
            };
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Не покупать");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void HookupHouseToUserShowMenu(int serverId, int hId, string name, string pidn)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Дом", "~b~Название: ~s~" + name);
      
            menu.AddMenuItem(UiMenu, "~g~Поселиться").Activated += (uimenu, item) =>
            {
                HideMenu();
                House.AcceptHookup(serverId, hId, pidn);
            };
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Не подселяться");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowHouseOutMenu(HouseInfoGlobalData h)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create($"№{h.id}", $"~b~Адрес: ~s~{h.address} {h.id}");
            menu.AddMenuItem(UiMenu, $"~b~Владелец дома:~s~ {h.name_user}");
            
            UIMenuItem enterButton = menu.AddMenuItem(UiMenu, "~g~Войти");

            if (User.Data.job == "mail" || User.Data.job == "mail2")
            {
                if (!await Client.Sync.Data.Has(h.id, "isMail"))
                {
                    menu.AddMenuItem(UiMenu, "~g~Положить почту").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Jobs.Mail.SendMail(h.id);
                    };
                }
                else
                {
                    menu.AddMenuItem(UiMenu, "~o~Дом уже обслуживался");
                }
                
            }
            
            var userId = User.Data.id;
            if (userId == h.id_user && h.pin > 0)
            {
                menu.AddMenuItem(UiMenu, "~y~Сменить пароль от дома").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    int pin1 = Convert.ToInt32(await Menu.GetUserInput("Пароль", null, 5));
                    int pin2 = Convert.ToInt32(await Menu.GetUserInput("Повторить пароль", null, 5));

                    if (pin1 < 1)
                    {
                        Notification.SendWithTime("~r~Пароль должен быть больше нуля");
                        return;
                    }

                    if (pin1 == pin2)
                    {
                        Notification.SendWithTime($"~g~Ваш новый пароль: ~s~{pin1}");
                        TriggerServerEvent("ARP:UpdateHousePin", h.id, pin1);
                    }
                    else
                    {
                        Notification.SendWithTime("~r~Пароли не совпадают");
                    }
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == enterButton)
                {
                    House.EnterHouse(h);
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowHouseOutExMenu(HouseInfoGlobalData h)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create($"№{h.id}", $"~b~Адрес: ~s~{h.address} {h.id}");
            menu.AddMenuItem(UiMenu, $"~b~Владелец дома:~s~ {h.name_user}");
            
            if (User.Data.job == "mail" || User.Data.job == "mail2")
            {
                if (!await Client.Sync.Data.Has(h.id, "isMail"))
                {
                    menu.AddMenuItem(UiMenu, "~g~Положить почту").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Jobs.Mail.SendMail(h.id);
                    };
                }
                else
                {
                    menu.AddMenuItem(UiMenu, "~o~Дом уже обслуживался");
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowHouseInMenu(HouseInfoGlobalData h)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create($"№{h.id}", $"~b~Адрес: ~s~{h.address} {h.id}");
      
            var exitButton = menu.AddMenuItem(UiMenu, "~g~Выйти");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            if (h.id == 813 && Main.ServerName == "Andromeda")
            {
                menu.AddMenuItem(UiMenu, "Балкон №1").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    User.SetVirtualWorld(0);
                    User.Teleport(Managers.Pickup.HA813TeleportPos1);
                };
                menu.AddMenuItem(UiMenu, "Балкон №2").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    User.SetVirtualWorld(0);
                    User.Teleport(Managers.Pickup.HA813TeleportPos2);
                };
            }
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == exitButton)
                {
                    House.ExitHouse(h);
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowHouseBuyMenu(HouseInfoGlobalData h)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create($"№{h.id}", $"~b~Адрес: ~s~{h.address} {h.id}");
      
            var buyButton = menu.AddMenuItem(UiMenu, $"Купить дом за ~g~${h.price:#,#}");

            if (h.int_x != 0)
            {
                menu.AddMenuItem(UiMenu, "~g~Осмотреть дом").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    House.EnterHouse(h);
                };
            }
            
            if (User.Data.job == "mail" || User.Data.job == "mail2")
            {
                if (!await Client.Sync.Data.Has(h.id, "isMail"))
                {
                    menu.AddMenuItem(UiMenu, "~g~Положить почту").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Jobs.Mail.SendMail(h.id);
                    };
                }
                else
                {
                    menu.AddMenuItem(UiMenu, "~o~Дом уже обслуживался");
                }
                
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == buyButton)
                    House.BuyHouse(h);
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowCondoInMenu(CondoInfoGlobalData h)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create($"№{h.id}", $"~b~Адрес: ~s~{h.address} {h.id}");
      
            var exitButton = menu.AddMenuItem(UiMenu, "~g~Выйти");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == exitButton)
                {
                    Condo.ExitHouse(h);
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowCondoBuyMenu(CondoInfoGlobalData h)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create($"№{h.id}", $"~b~Адрес: ~s~{h.address} {h.id}");
      
            var buyButton = menu.AddMenuItem(UiMenu, $"Купить квартиру за ~g~${h.price:#,#}");
            
            menu.AddMenuItem(UiMenu, "~g~Осмотреть квартиру").Activated += (uimenu, item) =>
            {
                HideMenu();
                Condo.EnterHouse(h);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == buyButton)
                    Condo.BuyHouse(h);
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowCondoOutMenu(CondoInfoGlobalData h)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create($"№{h.id}", $"~b~Адрес: ~s~{h.address} {h.id}");
            menu.AddMenuItem(UiMenu, $"~b~Владелец дома:~s~ {h.name_user}");
            
            UIMenuItem enterButton = menu.AddMenuItem(UiMenu, "~g~Войти");

            
            var userId = User.Data.id;
            if (userId == h.id_user && h.pin > 0)
            {
                menu.AddMenuItem(UiMenu, "~y~Сменить пароль от дома").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    int pin1 = Convert.ToInt32(await Menu.GetUserInput("Пароль", null, 5));
                    int pin2 = Convert.ToInt32(await Menu.GetUserInput("Повторить пароль", null, 5));

                    if (pin1 < 1)
                    {
                        Notification.SendWithTime("~r~Пароль должен быть больше нуля");
                        return;
                    }

                    if (pin1 == pin2)
                    {
                        Notification.SendWithTime($"~g~Ваш новый пароль: ~s~{pin1}");
                        TriggerServerEvent("ARP:UpdateCondoPin", h.id, pin1);
                    }
                    else
                    {
                        Notification.SendWithTime("~r~Пароли не совпадают");
                    }
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == enterButton)
                {
                    Condo.EnterHouse(h);
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowStockBuyOwnerMenu(StockInfoGlobalData h)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create($"№{h.id}", $"~b~Адрес: ~s~{h.address} {h.id}");
            menu.AddMenuItem(UiMenu, $"~b~Владелец:~s~ {h.user_name}");
            
            UIMenuItem enterButton = menu.AddMenuItem(UiMenu, "~g~Войти");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == enterButton)
                    Stock.EnterStock(h);
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowStockInMenu(StockInfoGlobalData h)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create($"№{h.id}", $"~b~Адрес: ~s~{h.address} {h.id}");
      
            var exitButton = menu.AddMenuItem(UiMenu, "~g~Выйти");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == exitButton)
                    Stock.ExitStock(h);
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowStockBuyMenu(StockInfoGlobalData h)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create($"№{h.id}", $"~b~Адрес: ~s~{h.address} {h.id}");
      
            var buyButton = menu.AddMenuItem(UiMenu, $"Купить склад за ~g~${h.price:#,#}");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == buyButton)
                    Stock.BuyStock(h);
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowStockPcMenu()
        {
            HideMenu();

            var h = await Stock.GetAllData(User.GetPlayerVirtualWorld() - 50000);
            if (h.user_id != User.Data.id)
            {
                Notification.SendWithTime("~r~Вы должны быть владельцем склада");
                return;
            }

            /*var h = Stock.GetStockFromId(User.GetPlayerVirtualWorld() - 50000);
            if (h.user_id != User.Data.id) return;*/
            
            var menu = new Menu();
            UiMenu = menu.Create("Склад", "~b~PC");
      
            menu.AddMenuItem(UiMenu, "Сменить пароль от склада").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int pin1 = Convert.ToInt32(await Menu.GetUserInput("Пароль", null, 5));
                int pin2 = Convert.ToInt32(await Menu.GetUserInput("Повторить пароль", null, 5));

                if (pin1 == pin2)
                {
                    Notification.SendWithTime($"~g~Ваш новый пароль: ~s~{pin1}");
                    Client.Sync.Data.Set(200000 + h.id, "pin1", pin1);
                    Stock.Save(h.id);
                }
                else
                {
                    Notification.SendWithTime("~r~Пароли не совпадают");
                }
            };
      
            menu.AddMenuItem(UiMenu, "Сменить пароль от сейфа #1").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int pin1 = Convert.ToInt32(await Menu.GetUserInput("Пароль", null, 4));
                int pin2 = Convert.ToInt32(await Menu.GetUserInput("Повторить пароль", null, 4));

                if (pin1 == pin2)
                {
                    Notification.SendWithTime($"~g~Ваш новый пароль: ~s~{pin1}");
                    Client.Sync.Data.Set(200000 + h.id, "pin2", pin1);
                    Stock.Save(h.id);
                }
                else
                {
                    Notification.SendWithTime("~r~Пароли не совпадают");
                }
            };
      
            menu.AddMenuItem(UiMenu, "Сменить пароль от сейфа #2").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int pin1 = Convert.ToInt32(await Menu.GetUserInput("Пароль", null, 4));
                int pin2 = Convert.ToInt32(await Menu.GetUserInput("Повторить пароль", null, 4));

                if (pin1 == pin2)
                {
                    Notification.SendWithTime($"~g~Ваш новый пароль: ~s~{pin1}");
                    Client.Sync.Data.Set(200000 + h.id, "pin3", pin1);
                    Stock.Save(h.id);
                }
                else
                {
                    Notification.SendWithTime("~r~Пароли не совпадают");
                }
            };
      
            menu.AddMenuItem(UiMenu, "~y~Лог").Activated += (uimenu, item) =>
            {
                HideMenu();
                TriggerServerEvent("ARP:SendPlayerStockLog", User.GetPlayerVirtualWorld() - 50000);
            };

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowStockDropMenu(int id, int itemId)
        {
            HideMenu();

            int stockId = User.GetPlayerVirtualWorld() - 50000;

            var stockItem = await Stock.GetAllData(stockId);
            
            var menu = new Menu();
            UiMenu = menu.Create($"Склад", $"~b~Список склада");
      
            menu.AddMenuItem(UiMenu, "Сейф #1").Activated += async (uimenu, item) =>
            {
                HideMenu();
                if (stockItem.pin2 == Convert.ToInt32(await Menu.GetUserInput("Пароль", null, 4)))
                    Managers.Inventory.DropItemToUserStock(id, itemId, 1);
                else
                    Notification.SendWithTime("~r~Вы не верно ввели пароль от сейфа");
            };
            menu.AddMenuItem(UiMenu, "Сейф #2").Activated += async (uimenu, item) =>
            {
                HideMenu();
                if (stockItem.pin3 == Convert.ToInt32(await Menu.GetUserInput("Пароль", null, 4)))
                    Managers.Inventory.DropItemToUserStock(id, itemId, 2);
                else
                    Notification.SendWithTime("~r~Вы не верно ввели пароль от сейфа");
            };
            menu.AddMenuItem(UiMenu, "Плотный деревянный ящик #1").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.DropItemToUserStock(id, itemId, 3);
            };
            menu.AddMenuItem(UiMenu, "Плотный деревянный ящик #2").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.DropItemToUserStock(id, itemId, 4);
            };
            menu.AddMenuItem(UiMenu, "Плотный деревянный ящик #3").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.DropItemToUserStock(id, itemId, 5);
            };
            menu.AddMenuItem(UiMenu, "Плотный деревянный ящик #4").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.DropItemToUserStock(id, itemId, 6);
            };
            menu.AddMenuItem(UiMenu, "Деревянный ящик #1").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.DropItemToUserStock(id, itemId, 7);
            };
            menu.AddMenuItem(UiMenu, "Деревянный ящик #2").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.DropItemToUserStock(id, itemId, 8);
            };
            menu.AddMenuItem(UiMenu, "Деревянный ящик #3").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.DropItemToUserStock(id, itemId, 9);
            };
            menu.AddMenuItem(UiMenu, "Деревянный ящик #4").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.DropItemToUserStock(id, itemId, 10);
            };
            menu.AddMenuItem(UiMenu, "Деревянный ящик #5").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.DropItemToUserStock(id, itemId, 11);
            };
            menu.AddMenuItem(UiMenu, "Деревянный ящик #6").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.DropItemToUserStock(id, itemId, 0);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowStockTakeMenu()
        {
            HideMenu();

            int stockId = User.GetPlayerVirtualWorld() - 50000;

            var stockItem = await Stock.GetAllData(stockId);
            
            var menu = new Menu();
            UiMenu = menu.Create($"Склад", $"~b~Список склада");
      
            menu.AddMenuItem(UiMenu, "Сейф #1").Activated += async (uimenu, item) =>
            {
                HideMenu();
                if (stockItem.pin2 == Convert.ToInt32(await Menu.GetUserInput("Пароль", null, 4)))
                    Managers.Inventory.GetItemList(stockItem.id, InventoryTypes.UserStock + 1);
                else
                    Notification.SendWithTime("~r~Вы не верно ввели пароль от сейфа");
            };
            menu.AddMenuItem(UiMenu, "Сейф #2").Activated += async (uimenu, item) =>
            {
                HideMenu();
                if (stockItem.pin3 == Convert.ToInt32(await Menu.GetUserInput("Пароль", null, 4)))
                    Managers.Inventory.GetItemList(stockItem.id, InventoryTypes.UserStock + 2);
                else
                    Notification.SendWithTime("~r~Вы не верно ввели пароль от сейфа");
            };
            menu.AddMenuItem(UiMenu, "Плотный деревянный ящик #1").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.GetItemList(stockItem.id, InventoryTypes.UserStock + 3);
            };
            menu.AddMenuItem(UiMenu, "Плотный деревянный ящик #2").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.GetItemList(stockItem.id, InventoryTypes.UserStock + 4);
            };
            menu.AddMenuItem(UiMenu, "Плотный деревянный ящик #3").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.GetItemList(stockItem.id, InventoryTypes.UserStock + 5);
            };
            menu.AddMenuItem(UiMenu, "Плотный деревянный ящик #4").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.GetItemList(stockItem.id, InventoryTypes.UserStock + 6);
            };
            menu.AddMenuItem(UiMenu, "Деревянный ящик #1").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.GetItemList(stockItem.id, InventoryTypes.UserStock + 7);
            };
            menu.AddMenuItem(UiMenu, "Деревянный ящик #2").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.GetItemList(stockItem.id, InventoryTypes.UserStock + 8);
            };
            menu.AddMenuItem(UiMenu, "Деревянный ящик #3").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.GetItemList(stockItem.id, InventoryTypes.UserStock + 9);
            };
            menu.AddMenuItem(UiMenu, "Деревянный ящик #4").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.GetItemList(stockItem.id, InventoryTypes.UserStock + 10);
            };
            menu.AddMenuItem(UiMenu, "Деревянный ящик #5").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.GetItemList(stockItem.id, InventoryTypes.UserStock + 11);
            };
            menu.AddMenuItem(UiMenu, "Деревянный ящик #6").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.GetItemList(stockItem.id, InventoryTypes.UserStock + 0);
            };
            
            menu.AddMenuItem(UiMenu, "~y~Отмыть деньги").Activated += (uimenu, item) =>
            {
                HideMenu();
                SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                int money = (int) Sync.Data.GetLocally(User.GetServerId(), "GrabCash") * User.Bonus;
                Sync.Data.ResetLocally(User.GetServerId(), "GrabCash");
                Notification.SendWithTime("~g~Награбленно: $" + money);
                Notification.SendWithTime("~g~Ваша доля: $" + (money / 2));

                int moneyFull = money / 2;
                
                while (moneyFull > 0)
                {
                    if (moneyFull == 1)
                    {
                        Managers.Inventory.AddItemServer(138, 1, InventoryTypes.UserStock + 1, stockItem.id, moneyFull, -1, -1, -1);
                        moneyFull = 0;
                    }
                    else if (moneyFull == 100)
                    {
                        Managers.Inventory.AddItemServer(139, 1, InventoryTypes.UserStock + 1, stockItem.id, moneyFull, -1, -1, -1);
                        moneyFull = 0;
                    }
                    else if (moneyFull <= 4000)
                    {
                        Managers.Inventory.AddItemServer(140, 1, InventoryTypes.UserStock + 1, stockItem.id, moneyFull, -1, -1, -1);
                        moneyFull = 0;
                    }
                    else if (moneyFull <= 8000)
                    {
                        Managers.Inventory.AddItemServer(141, 1, InventoryTypes.UserStock + 1, stockItem.id, moneyFull, -1, -1, -1);
                        moneyFull = 0;
                    }
                    else
                    {
                        Managers.Inventory.AddItemServer(141, 1, InventoryTypes.UserStock + 1, stockItem.id, 8000, -1, -1, -1);
                        moneyFull = moneyFull - 8000;
                    }
                }
                
                User.AddCashMoney(money / 2);
                Main.AddStockLog(User.Data.rp_name, $"Отмыл деньги: ${money}. Доля: ${money / 2}", stockId);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowMeriaTeleportMenu()
        {
            HideMenu();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Лифт во время блекаута не работает");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Правительство", "~b~Офис правительства");

            menu.AddMenuItem(UiMenu, "Офис").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.MeriaUpPos);
            };

            menu.AddMenuItem(UiMenu, "Улица").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.MeriaDownPos);
            };

            menu.AddMenuItem(UiMenu, "Гараж").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.MeriaGarPos);
            };

            menu.AddMenuItem(UiMenu, "Крыша").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.MeriaRoofPos);
            };
      
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowLicTeleportMenu()
        {
            HideMenu();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Лифт во время блекаута не работает");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Офис", "~b~Лифт");

            menu.AddMenuItem(UiMenu, "Парковка").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.LicGaragePos);
            };

            menu.AddMenuItem(UiMenu, "Улица").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.LicDownPos);
            };

            menu.AddMenuItem(UiMenu, "Офис").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.LicUpPos);
            };

            menu.AddMenuItem(UiMenu, "Крыша").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.LicRoofPos);
            };
      
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowEmsTeleportMenu()
        {
            HideMenu();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Лифт во время блекаута не работает");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Ems", "~b~Лифт EMS");

            menu.AddMenuItem(UiMenu, "Парковка").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.EmsElevatorParkPos);
            };

            menu.AddMenuItem(UiMenu, "Больница").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.EmsElevatorPos);
            };

            menu.AddMenuItem(UiMenu, "Крыша").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.EmsElevatorRoofPos);
            };
      
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowEmsNewTeleportMenu()
        {
            HideMenu();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Лифт во время блекаута не работает");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Ems", "~b~Лифт EMS");

            menu.AddMenuItem(UiMenu, "1 этаж").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.EmsElevatorHosp1Pos);
            };

            menu.AddMenuItem(UiMenu, "4 этаж").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.EmsElevatorHosp4Pos);
            };

            menu.AddMenuItem(UiMenu, "5 этаж").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.EmsElevatorHosp5Pos);
            };

            menu.AddMenuItem(UiMenu, "Крыша").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.EmsElevatorHospRoofPos);
            };
      
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowApartament01TeleportMenu()
        {
            HideMenu();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Лифт во время блекаута не работает");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Лифт", "~b~Лифт");

            menu.AddMenuItem(UiMenu, "Апартаменты #1").Activated += (uimenu, item) =>
            {
                HideMenu();
                MenuList.ShowApartamentTeleportMenu(10, 0);
            };

            menu.AddMenuItem(UiMenu, "Апартаменты #2").Activated += (uimenu, item) =>
            {
                HideMenu();
                MenuList.ShowApartamentTeleportMenu(8, 1);
            };
      
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowMazeOfficeTeleportMenu()
        {
            HideMenu();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Лифт во время блекаута не работает");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Maze", "~b~Лифт Maze");

            menu.AddMenuItem(UiMenu, "Гараж").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.BankMazeLiftGaragePos);
            };

            menu.AddMenuItem(UiMenu, "Офис").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.BankMazeLiftOfficePos);
            };

            menu.AddMenuItem(UiMenu, "Улица").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.BankMazeLiftStreetPos);
            };

            menu.AddMenuItem(UiMenu, "Крыша").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.BankMazeLiftRoofPos);
            };
      
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowFibTeleportMenu()
        {
            HideMenu();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Лифт во время блекаута не работает");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("FIB", "~b~Офис FIB");

            menu.AddMenuItem(UiMenu, "Гараж").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.FibLift0StationPos);
            };

            menu.AddMenuItem(UiMenu, "1 этаж").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.FibLift1StationPos);
            };

            menu.AddMenuItem(UiMenu, "49 этаж").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.FibLift2StationPos);
            };

            menu.AddMenuItem(UiMenu, "52 этаж").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.FibLift3StationPos);
            };

            menu.AddMenuItem(UiMenu, "Крыша").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(Managers.Pickup.FibLift4StationPos);
            };
      
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowBusinessTeleportMenu()
        {
            HideMenu();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Лифт во время блекаута не работает");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Arcadius", "~b~Бизнес центр");

            for (int i = 0; i < Business.Business.TypeList.Length; i++)
            {
                var i1 = i;
                menu.AddMenuItem(UiMenu, Business.Business.TypeList[i]).Activated += (uimenu, item) =>
                {
                    HideMenu();
                    TriggerServerEvent("ARP:OpenBusinnesListMenu", i1);
                };
            }

            menu.AddMenuItem(UiMenu, "~b~Arcadius Motors").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Business.Exit(Business.Business.BusinessMotorPos);
            };

            menu.AddMenuItem(UiMenu, "~g~Улица").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Business.Exit(Business.Business.BusinessStreetPos);
            };

            menu.AddMenuItem(UiMenu, "~g~Крыша").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Business.Exit(Business.Business.BusinessRoofPos);
            };

            menu.AddMenuItem(UiMenu, "~g~Гараж").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Business.Exit(Business.Business.BusinessGaragePos);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowApartamentMenu(ApartmentData data)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("№" + data.id, $"~b~Владелец: ~s~{(data.user_id == 0 ? "Государство" : data.user_name)}");

            menu.AddMenuItem(UiMenu, "~g~Выйти").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowApartamentTeleportMenu((int) Managers.Apartment.BuildList[data.build_id, 3], data.build_id);
            };

            if (data.user_id == 0)
            {
                menu.AddMenuItem(UiMenu, "~g~Купить", $"Цена: ~g~${data.price:#,#}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Managers.Apartment.Buy(data.id);
                };
            }
            else if (data.user_id == User.Data.id)
            {
                var nalog = data.price * (100 - Coffer.GetNalog()) / 100;
                menu.AddMenuItem(UiMenu, "~r~Продать", $"Цена: ~g~${nalog:#,#}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowAskSellApsMenu();
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowApartamentTeleportMenu(int countFloor, int buildId)
        {
            HideMenu();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Лифт во время блекаута не работает");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Апартаменты", "~b~Меню апартаментов");

            if (User.GetPlayerVirtualWorld() != 0)
            {
                menu.AddMenuItem(UiMenu, "~g~Улица").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Managers.Apartment.Exit();
                };
            }
            if (buildId == 1 || buildId == 2)
            {
                menu.AddMenuItem(UiMenu, "~g~Парковка").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Managers.Apartment.Exit(new Vector3(-15.46794f, -612.5906f, 34.86151f));
                };
            }
            if (buildId == 5)
            {
                menu.AddMenuItem(UiMenu, "~g~Парковка").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Managers.Apartment.Exit(new Vector3(-761.8995f, 352.0111f, 86.99801f));
                };
            }

            if (buildId == 16)
            {
                menu.AddMenuItem(UiMenu, "~g~Крыша").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Managers.Apartment.Exit(new Vector3(-902.897f, -369.9444f, 135.2822f));
                };
            }

            if (buildId == 19)
            {
                menu.AddMenuItem(UiMenu, "~g~Крыша").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Managers.Apartment.Exit(new Vector3(109.9076f, -867.6014f, 133.7701f));
                };
            }

            for (int i = 1; i <= countFloor; i++)
            {
                var floor = i;
                menu.AddMenuItem(UiMenu, "Этаж №" + i).Activated += (uimenu, item) =>
                {
                    HideMenu();
                    TriggerServerEvent("ARP:OpenApartamentListMenu", floor, buildId);
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowJobTrashMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Работа", "~b~Выберите пункт меню");

            menu.AddMenuItem(UiMenu, "Взять мусор").Activated += (uimenu, item) =>
            {
                HideMenu();
                Jobs.Trash.TakeTrash();
            };

            menu.AddMenuItem(UiMenu, "Положить мусор в ТС").Activated += (uimenu, item) =>
            {
                HideMenu();
                Jobs.Trash.PutTrash();
            };
      
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowJobBuilderMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Прораб", "~b~Выберите пункт меню");

            menu.AddMenuItem(UiMenu, "~g~Начать/~r~Закончить~s~ рабочий день").Activated += (uimenu, item) =>
            {
                HideMenu();
                Jobs.Builder.StartOrEnd();
            };

            menu.AddMenuItem(UiMenu, "Забрать деньги").Activated += (uimenu, item) =>
            {
                HideMenu();
                Jobs.Builder.TakeMoney();
            };
      
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void GrSixOgrabOrInCarMenu()
        {
            UI.ShowToolTip("Нажмите ~INPUT_PICKUP~ чтобы открыть меню");
            
            var menu = new Menu();
            UiMenu = menu.Create("Меню транспортра", "~b~Выберите действие");
            if (User.Data.job == "GrSix")
            {
                menu.AddMenuItem(UiMenu, "~g~Положить деньги в машину").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Jobs.GroupSix.PutMoneyInCar();
                };
                return;
            }
            menu.AddMenuItem(UiMenu, "Вскрыть задний отсек", "Необходима газовая горелка или взрывчатка").Activated += (uimenu, item) =>
            {
                HideMenu();
                TriggerServerEvent("ARP:GrSix:Grab");
            };
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowJobOceanMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Чистка берега", "~b~Выберите пункт меню");

            menu.AddMenuItem(UiMenu, "~g~Начать/~r~Закончить~s~ работу").Activated += (uimenu, item) =>
            {
                HideMenu();
                Jobs.Ocean.StartOrEndOcean();
            };

            menu.AddMenuItem(UiMenu, "Забрать деньги").Activated += (uimenu, item) =>
            {
                HideMenu();
                Jobs.Ocean.TakeMoneyOcean();
            };
      
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        /*public static void ShowJobJewelryMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Поиск предметов", "~b~Выберите пункт меню");

            menu.AddMenuItem(UiMenu, "~g~Начать/~r~Закончить~s~ работу").Activated += (uimenu, item) =>
            {
                HideMenu();
                Jobs.Search.StartOrEndJewelry();
            };

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
            
        }*/

        public static void ShowJobJailMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Охранник", "~b~Выберите пункт меню");

            menu.AddMenuItem(UiMenu, "~g~Начать/~r~Закончить~s~ работу").Activated += (uimenu, item) =>
            {
                HideMenu();
                Jobs.JailJob.StartOrEndJailJob();
            };

            menu.AddMenuItem(UiMenu, "Забрать деньги").Activated += (uimenu, item) =>
            {
                HideMenu();
                Jobs.JailJob.TakeMoneyJail();
            };

            menu.AddMenuItem(UiMenu, "~g~Вернуться во двор тюрьмы").Activated += (uimenu, item) =>
            {
                HideMenu();
                //Jobs.JailJob.JailTeleport();
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowJobJailTeleportMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Тюремщик", "~b~Выберите пункт меню");

            menu.AddMenuItem(UiMenu, "~g~Отправиться в карьер на работу").Activated += (uimenu, item) =>
            {
                HideMenu();
                Jobs.JailJob.JailJobTeleportFromJail();
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowJobRoadWorkerMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Прораб", "~b~Выберите пункт меню");

            menu.AddMenuItem(UiMenu, "~g~Начать/~r~Закончить~s~ рабочий день").Activated += (uimenu, item) =>
            {
                HideMenu();
                Jobs.RoadWorker.StartOrEnd();
            };

            menu.AddMenuItem(UiMenu, "Забрать деньги").Activated += (uimenu, item) =>
            {
                HideMenu();
                Jobs.RoadWorker.TakeMoney();
            };
      
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowJobCleanderMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Работодатель", "~b~Выберите пункт меню");

            menu.AddMenuItem(UiMenu, "~g~Начать/~r~Закончить~s~ рабочий день").Activated += (uimenu, item) =>
            {
                HideMenu();
                Jobs.Cleaner.StartOrEnd();
            };

            menu.AddMenuItem(UiMenu, "Забрать деньги").Activated += (uimenu, item) =>
            {
                HideMenu();
                Jobs.Cleaner.TakeMoney();
            };
      
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowVehicleListMenu(List<CitizenFX.Core.Vehicle> vehicleList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Список", "~b~Список ТС рядом с Вами");
            
            foreach (CitizenFX.Core.Vehicle v in vehicleList)
                menu.AddMenuItem(UiMenu, $"~b~{v.DisplayName}");
          
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowPedListMenu(List<CitizenFX.Core.Ped> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Список", "~b~Список педов рядом с Вами");
            
            foreach (CitizenFX.Core.Ped p in pedList)
                menu.AddMenuItem(UiMenu, $"~b~{p.Model}");
          
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowPlayerListMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Список", "~b~Список игроков рядом с Вами");

            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;      
                    if (!(Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(p.Handle), true), GetEntityCoords(GetPlayerPed(-1), true)) < 10f)) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}");
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowAdminTeleportToPlayerMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Admin", "~b~Телепорт к игроку");
            
            menu.AddMenuItem(UiMenu, "~b~По ID").Activated += async (uimenu, item) =>
            {
                var id = Convert.ToInt32(await Menu.GetUserInput("ID Игрока", null, 11));
                foreach (Player p in new PlayerList())
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    if (User.PlayerIdList[p.ServerId.ToString()] == id)
                    {
                        var playerPos = GetEntityCoords(GetPlayerPed(p.Handle), true);
                        User.Teleport(playerPos);
                        Main.SaveLog("AdminTeleportToPlayer", $"{User.Data.rp_name} to {User.PlayerIdList[p.ServerId.ToString()]} {playerPos.X}, {playerPos.Y}, {playerPos.Z}");
                    }
                }
            };
            
            foreach (Player p in new PlayerList())
            {
                if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                try
                {
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += async (uimenu, item) =>
                    {
                        var playerPos = GetEntityCoords(GetPlayerPed(p.Handle), true);
                        User.Teleport(playerPos);
                        Main.SaveLog("AdminTeleportToPlayer", $"{User.Data.rp_name} to {User.PlayerIdList[p.ServerId.ToString()]} {playerPos.X}, {playerPos.Y}, {playerPos.Z}");
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowAdminTeleportToMeMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Admin", "~b~Телепорт к себе");
            
            menu.AddMenuItem(UiMenu, "~b~По ID").Activated += async (uimenu, item) =>
            {
                var id = Convert.ToInt32(await Menu.GetUserInput("ID Игрока", null, 11));
                foreach (Player p in new PlayerList())
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    if (User.PlayerIdList[p.ServerId.ToString()] == id)
                    {
                        var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
                        Shared.TriggerEventToPlayer(p.ServerId, "ARP:TeleportToAdmin", playerPos.X, playerPos.Y, playerPos.Z);
                        Main.SaveLog("AdminTeleportToAdmin", $"{User.Data.rp_name} player {User.PlayerIdList[p.ServerId.ToString()]} to coord {playerPos.X}, {playerPos.Y}, {playerPos.Z}");
                    }
                }
            };
            
            foreach (Player p in new PlayerList())
            {
                if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                try
                {
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += async (uimenu, item) =>
                    {
                        var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
                        Shared.TriggerEventToPlayer(p.ServerId, "ARP:TeleportToAdmin", playerPos.X, playerPos.Y, playerPos.Z);
                        Main.SaveLog("AdminTeleportToAdmin", $"{User.Data.rp_name} player {User.PlayerIdList[p.ServerId.ToString()]} to coord {playerPos.X}, {playerPos.Y}, {playerPos.Z}");
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowAdminSpecMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Admin", "~b~Наблюдать за игроком");
            
            menu.AddMenuItem(UiMenu, "~b~Спектатор по ID").Activated += async (uimenu, item) =>
            {
                var id = Convert.ToInt32(await Menu.GetUserInput("ID Игрока", null, 11));
                foreach (Player p in new PlayerList())
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    if (User.PlayerIdList[p.ServerId.ToString()] == id)
                    {
                        if (NetworkIsInSpectatorMode())
                            NetworkSetInSpectatorMode(false, GetPlayerPed(_lastSpec));
                        
                        var plPos = GetEntityCoords(GetPlayerPed(p.Handle), true);
                        RequestCollisionAtCoord(plPos.X, plPos.Y, plPos.Z);
                        _lastSpec = p.Handle;
                        NetworkSetInSpectatorMode(true, GetPlayerPed(p.Handle));
                    }
                }
            };
            
            foreach (Player p in new PlayerList())
            {
                if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) {
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~Не авторизован").Activated += async (uimenu, item) =>
                    {
                        if (NetworkIsInSpectatorMode())
                            NetworkSetInSpectatorMode(false, GetPlayerPed(_lastSpec));
                        
                        var plPos = GetEntityCoords(GetPlayerPed(p.Handle), true);
                        RequestCollisionAtCoord(plPos.X, plPos.Y, plPos.Z);
                        _lastSpec = p.Handle;
                        NetworkSetInSpectatorMode(true, GetPlayerPed(p.Handle));
                    };
                    continue;
                }
                try
                {
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += async (uimenu, item) =>
                    {
                        if (NetworkIsInSpectatorMode())
                            NetworkSetInSpectatorMode(false, GetPlayerPed(_lastSpec));
                        
                        var plPos = GetEntityCoords(GetPlayerPed(p.Handle), true);
                        RequestCollisionAtCoord(plPos.X, plPos.Y, plPos.Z);
                        _lastSpec = p.Handle;
                        NetworkSetInSpectatorMode(true, GetPlayerPed(p.Handle));
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            
            menu.AddMenuItem(UiMenu, "~o~Выйти из спектатора").Activated += (uimenu, item) =>
            {
                if (NetworkIsInSpectatorMode())
                    NetworkSetInSpectatorMode(false, GetPlayerPed(_lastSpec));
            };

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                {
                    HideMenu();
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        
        public static async void ShowAdminKickMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Admin", "~b~Кикнуть игрока");
            
            menu.AddMenuItem(UiMenu, "~b~По ID").Activated += async (uimenu, item) =>
            {
                var id = Convert.ToInt32(await Menu.GetUserInput("ID Игрока", null, 11));
                foreach (Player p in new PlayerList())
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    if (User.PlayerIdList[p.ServerId.ToString()] == id)
                    {
                        HideMenu();
                        var reason = await Menu.GetUserInput("Причина", null, 32);
                        if (reason == "NULL") return;
                        TriggerServerEvent("ARP:KickPlayerServerId", p.ServerId, reason);
                        Notification.SendWithTime($"~y~Вы кикнули игрока {User.PlayerIdList[p.ServerId.ToString()]}");
                    }
                }
            };
            
            foreach (Player p in new PlayerList())
            {
                if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) {
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~Не авторизован").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        var reason = await Menu.GetUserInput("Причина", null, 32);
                        if (reason == "NULL") return;
                        TriggerServerEvent("ARP:KickPlayerServerId", p.ServerId, reason);
                        Notification.SendWithTime($"~y~Вы кикнули игрока");
                    };
                    continue;
                }
                try
                {
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += async (uimenu1, item1) =>
                    {
                        HideMenu();
                        var reason = await Menu.GetUserInput("Причина", null, 32);
                        if (reason == "NULL") return;
                        TriggerServerEvent("ARP:KickPlayerServerId", p.ServerId, reason);
                        Notification.SendWithTime($"~y~Вы кикнули игрока {User.PlayerIdList[p.ServerId.ToString()]}");
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            
            /*foreach (Player p in new PlayerList())
            {
                try
                {
                    var plData = await User.GetAllDataByServerId(p.ServerId);
                    if (plData == null) continue;
                    
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{plData.id} - {plData.rp_name}").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        var reason = await Menu.GetUserInput("Причина", null, 32);
                        if (reason == "NULL") return;
                        TriggerServerEvent("ARP:KickPlayerServerId", p.ServerId, reason);
                        Notification.SendWithTime($"~y~Вы кикнули игрока {plData.rp_name}");
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }*/
            
            CitizenFX.Core.UI.Screen.LoadingPrompt.Hide();
            Sync.Data.ShowSyncMessage = true;

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowAdminBanMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Admin", "~b~Забанить игрока");
            

            var list = new List<dynamic> {"1ч", "12ч", "1д", "3д", "7д", "14д", "30д", "90д"};
            
            menu.AddMenuItemList(UiMenu, "~b~По ID", list).OnListSelected += async (uimenu1, idx1) =>
            {
                var id = Convert.ToInt32(await Menu.GetUserInput("ID Игрока", null, 11));
                foreach (Player p in new PlayerList())
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    if (User.PlayerIdList[p.ServerId.ToString()] == id)
                    {
                        HideMenu();
                        var reason = await Menu.GetUserInput("Причина", null, 32);
                        if (reason == "NULL") return;
                        TriggerServerEvent("ARP:BanPlayerServerId", p.ServerId, idx1, reason);
                        Notification.SendWithTime($"~y~Вы забанили игрока {User.PlayerIdList[p.ServerId.ToString()]}");
                    }
                }
            };
            
            foreach (Player p in new PlayerList())
            {
                if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) {
                    menu.AddMenuItemList(UiMenu, $"~b~ID: ~s~Не авторизован", list).OnListSelected += async (uimenu, idx) =>
                    {
                        HideMenu();
                        var reason = await Menu.GetUserInput("Причина", null, 32);
                        if (reason == "NULL") return;
                        TriggerServerEvent("ARP:BanPlayerServerId", p.ServerId, idx, reason);
                        Notification.SendWithTime($"~y~Вы забанили игрока");
                    };
                    continue;
                }
                try
                {
                    menu.AddMenuItemList(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}", list).OnListSelected += async (uimenu1, idx1) =>
                    {
                        HideMenu();
                        var reason = await Menu.GetUserInput("Причина", null, 32);
                        if (reason == "NULL") return;
                        TriggerServerEvent("ARP:BanPlayerServerId", p.ServerId, idx1, reason);
                        Notification.SendWithTime($"~y~Вы забанили игрока {User.PlayerIdList[p.ServerId.ToString()]}");
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            /*foreach (Player p in new PlayerList())
            {
                try
                {
                    var plData = await User.GetAllDataByServerId(p.ServerId);
                    if (plData == null) continue;
                    
                    menu.AddMenuItemList(UiMenu, $"~b~ID: ~s~{plData.id} - {plData.rp_name}", list).OnListSelected += async (uimenu, idx) =>
                    {
                        HideMenu();
                        var reason = await Menu.GetUserInput("Причина", null, 32);
                        if (reason == "NULL") return;
                        TriggerServerEvent("ARP:BanPlayerServerId", p.ServerId, idx, reason);
                        Notification.SendWithTime($"~y~Вы забанили игрока {plData.rp_name}");
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }*/
            
            CitizenFX.Core.UI.Screen.LoadingPrompt.Hide();
            Sync.Data.ShowSyncMessage = true;

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowAdminBlackListMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Admin", "~b~Занести в черный список");

            menu.AddMenuItem(UiMenu, "~b~По ID").Activated += async (uimenu, item) =>
            {
                var id = Convert.ToInt32(await Menu.GetUserInput("ID Игрока", null, 11));
                foreach (Player p in new PlayerList())
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    if (User.PlayerIdList[p.ServerId.ToString()] == id)
                    {
                        HideMenu();
                        var reason = await Menu.GetUserInput("Причина", null, 32);
                        if (reason == "NULL") return;
                        TriggerServerEvent("ARP:BlackListPlayerServerId", p.ServerId, reason);
                        Notification.SendWithTime(
                            $"~y~Вы занесли в черный список игрока {User.PlayerIdList[p.ServerId.ToString()]}");
                    }
                }
            };
            
            foreach (Player p in new PlayerList())
            {
                if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) {
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~Не авторизован").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        var reason = await Menu.GetUserInput("Причина", null, 32);
                        if (reason == "NULL") return;
                        TriggerServerEvent("ARP:BlackListPlayerServerId", p.ServerId, reason);
                        Notification.SendWithTime($"~y~Вы занесли в черный список игрока");
                    };
                    continue;
                }
                try
                {
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        var reason = await Menu.GetUserInput("Причина", null, 32);
                        if (reason == "NULL") return;
                        TriggerServerEvent("ARP:BlackListPlayerServerId", p.ServerId, reason);
                        Notification.SendWithTime($"~y~Вы занесли в черный список игрока {User.PlayerIdList[p.ServerId.ToString()]}");
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            /*foreach (Player p in new PlayerList())
            {
                try
                {
                    var plData = await User.GetAllDataByServerId(p.ServerId);
                    if (plData == null) continue;
                    
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{plData.id} - {plData.rp_name}").Activated += async (uimenu, idx) =>
                    {
                        HideMenu();
                        var reason = await Menu.GetUserInput("Причина", null, 32);
                        if (reason == "NULL") return;
                        TriggerServerEvent("ARP:BlackListPlayerServerId", p.ServerId, reason);
                        Notification.SendWithTime($"~y~Вы занесли в черный список игрока {plData.rp_name}");
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }*/

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowAdminMenu()
        {
            HideMenu();

            await User.GetAllData();

            if (!User.IsAdmin())
                return;
            
            var menu = new Menu();
            UiMenu = menu.Create("Admin", "~b~Админ панель");
            
            menu.AddMenuItem(UiMenu, "Транспорт").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowAdminVehicleMenu();
            };
            
            menu.AddMenuItem(UiMenu, "Написать уведомление").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var title = await Menu.GetUserInput("Заголовок", null, 15);
                var text = await Menu.GetUserInput("Текст...", null, 50);
                if (text == "NULL") return;
                Notification.SendPictureToAll(text, "Администрация", title, "CHAR_LIFEINVADER", Notification.TypeChatbox);
                Main.SaveLog("AdminNotification", $"{User.Data.rp_name} | {title} | {text}");
                
            };
                
            menu.AddMenuItem(UiMenu, "NoClip").Activated += (uimenu, item) =>
            {
                HideMenu();
                NoClip.NoClipEnabled = true;
                Notification.SendWithTime("~b~Ноуклип включён, отключить на G");
            };

            menu.AddMenuItem(UiMenu, "Для мероприятий").Activated += (uimenu, item) =>
            {
                ShowAdminMpMenu();
            };
            
            menu.AddMenuItem(UiMenu, "Пополнить шкалу жажды и голода").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaterLevel(100);
                User.SetEatLevel(1000);
                Notification.SendWithTime("~b~Готово");
            };
            menu.AddMenuItem(UiMenu, "Восстановить здоровье и броню").Activated += (uimenu, item) =>
            {
                HideMenu();
                int r = 1;
                int pt = 200;
                var coords = GetEntityCoords(GetPlayerPed(-1), true);
                Shared.TriggerEventToAllPlayers("ARP:GiveHealthMp", coords.X, coords.Y, coords.Z, r, pt);
                Shared.TriggerEventToAllPlayers("ARP:GiveArmorMp", coords.X, coords.Y, coords.Z, r, pt);
                Notification.SendWithTime("~b~Готово");
                Main.SaveLog("AdminMpHeal", $"{User.Data.rp_name} {r}f, {pt}");
                Main.SaveLog("AdminMpArmor", $"{User.Data.rp_name} {r}f, {pt}");
            };

            menu.AddMenuItem(UiMenu, "~y~Спектатор").Activated += (uimenu, item) =>
            {
                ShowAdminSpecMenu();
            };

            menu.AddMenuItem(UiMenu, "~g~Посадить в тюрьму").Activated += async (uimenu, item) =>
            {
                HideMenu();
                Debug.WriteLine("ok");
                int id = Convert.ToInt32(await Menu.GetUserInput("ID игрока", "", 10));
                int m = Convert.ToInt32(await Menu.GetUserInput("Кол-во минут", "", 10));
                string text = await Menu.GetUserInput("Причина", null, 50);
                Debug.WriteLine("ok");
                Shared.TriggerEventToAllPlayers("ARP:Admin:JailPlayer", id, m, text);
                Debug.WriteLine("ok");
                Notification.SendWithTime("~b~Вы посадили игрока в тюрьму");
                Main.SaveLog("AdminJail", $"{User.Data.rp_name} jail {id}, {m} {text}");
            };
            

            menu.AddMenuItem(UiMenu, "~y~Кикнуть игрока").Activated += (uimenu, item) =>
            {
                ShowAdminKickMenu();
            };
            
            if (User.IsAdmin(2))
            {
                menu.AddMenuItem(UiMenu, "~y~Забанить игрока").Activated += (uimenu, item) =>
                {
                    ShowAdminBanMenu();
                };
            
                menu.AddMenuItem(UiMenu, "~r~Занести в ЧС").Activated += (uimenu, item) =>
                {
                    ShowAdminBlackListMenu();
                };
            
                menu.AddMenuItem(UiMenu, "Одежда").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowAdminClothMenu();
                };
            }

            if (User.IsAdmin(4))
            {
                menu.AddMenuItem(UiMenu, "~o~Кикнуть всех").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    var reason = await Menu.GetUserInput("Причина", null, 32);
                    if (reason == "NULL") return;
                    foreach (Player p in new PlayerList())
                    {
                        try
                        {
                            TriggerServerEvent("ARP:KickPlayerServerId", p.ServerId, reason);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.Message);
                            throw;
                        }
                    }
                };
                
                var list = new List<dynamic>();
                for (int i = 0; i < 17; i++)
                    list.Add(i);
            
                menu.AddMenuItemList(UiMenu, "Лидерка", list).OnListSelected += (uimenu, idx) =>
                {
                    Sync.Data.Set(User.GetServerId(), "fraction_id", idx);
                    Sync.Data.Set(User.GetServerId(), "rank", 14);
                    User.Data.fraction_id = idx;
                    User.Data.rank = 14;
                    Notification.SendWithTime("~g~Фракция: " + idx);
                };
            }

            var listInviz = new List<dynamic> {"Вкл", "Выкл"};
            menu.AddMenuItemList(UiMenu, "Инвиз", listInviz).OnListSelected += (uimenu, idx) =>
            {
                HideMenu();
                User.Invisible(PlayerId(), idx == 0);
            };
            
            menu.AddMenuItemList(UiMenu, "Годмод", listInviz).OnListSelected += (uimenu, idx) =>
            {
                HideMenu();

                var playerPed = GetPlayerPed(-1);
                
                if (idx == 0)
                {
                    SetPedCanRagdoll(playerPed, false);
                    SetPedCanRagdollFromPlayerImpact(playerPed, false);
                    SetPedRagdollOnCollision(playerPed, false);
                    SetPedCanBeDraggedOut(playerPed, false);
                    SetPedConfigFlag(playerPed, 32, false);
                    ClearPedBloodDamage(playerPed);
                    ResetPedVisibleDamage(playerPed);
                    ClearPedLastWeaponDamage(playerPed);
                    SetEntityProofs(playerPed, true, true, true, true, true, true, true, true);
                    SetPlayerInvincible(PlayerId(), true);
                    Main.SaveLog("AdminGM", $"{User.Data.rp_name} disable");
                }
                else
                {
                    SetPedCanRagdoll(playerPed, true);
                    SetPedCanRagdollFromPlayerImpact(playerPed, true);
                    SetPedRagdollOnCollision(playerPed, true);
                    SetPedCanBeDraggedOut(playerPed, true);
                    SetPedConfigFlag(playerPed, 32, true);
                    ClearPedBloodDamage(playerPed);
                    ResetPedVisibleDamage(playerPed);
                    ClearPedLastWeaponDamage(playerPed);
                    SetPlayerInvincible(PlayerId(), false);
                    Main.SaveLog("AdminGM", $"{User.Data.rp_name} enable");
                }
            };
            
            menu.AddMenuItem(UiMenu, "Телепорт").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowAdminTeleportMenu();
            };

            if (User.IsAdmin(3) || Main.ServerName == "Earth")
            {
                var list = new List<dynamic>();
                for (int i = 0; i < 223; i++)
                    list.Add(Inventory.ItemList[i, 0].ToString());
                
                menu.AddMenuItemList(UiMenu, "Взять предмет", list).OnListSelected += (uimenu, idx) =>
                {
                    Managers.Inventory.TakeNewItem(idx);
                };
            }

            if (User.IsAdmin(5))
            {
                /*menu.AddMenuItem(UiMenu, "Меню для тестов", "Не советую что-то использовать все в логах").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowTesterMenu();
                };*/
                menu.AddMenuItem(UiMenu, "Меню разработчика").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowDeveloperMenu();
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
       
        
        public static async void ShowAdminMpMenu()
        {
            HideMenu();

            await User.GetAllData();

            if (!User.IsAdmin())
                return;
            
            var menu = new Menu();
            UiMenu = menu.Create("Admin", "~b~Меню мероприятий");
           
            menu.AddMenuItem(UiMenu, "Пригласить на МП").Activated += (uimenu, item) =>
            {
                HideMenu();
                var coords = GetEntityCoords(GetPlayerPed(-1), true);
                Shared.TriggerEventToAllPlayers("ARP:InviteMp", coords.X, coords.Y, coords.Z);
                Main.SaveLog("AdminMpInvite", $"{User.Data.rp_name}");
            };
           
            menu.AddMenuItem(UiMenu, "Зона").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int r = Convert.ToInt32(await Menu.GetUserInput("Радиус", "", 10));
                var coords = GetEntityCoords(GetPlayerPed(-1), true);
                Shared.TriggerEventToAllPlayers("ARP:CreateZoneMp", coords.X, coords.Y, coords.Z, r);
            };
           
            menu.AddMenuItem(UiMenu, "Удалить зону").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var coords = GetEntityCoords(GetPlayerPed(-1), true);
                Shared.TriggerEventToAllPlayers("ARP:DeleteZoneMp");
            };
           
            menu.AddMenuItem(UiMenu, "Подкинуть в радиусе").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int r = Convert.ToInt32(await Menu.GetUserInput("Радиус", "", 10));
                int h = Convert.ToInt32(await Menu.GetUserInput("Высота", "", 10));
                var coords = GetEntityCoords(GetPlayerPed(-1), true);
                Shared.TriggerEventToAllPlayers("ARP:SlapMp", coords.X, coords.Y, coords.Z, r, h);
                Main.SaveLog("AdminMpSlap", $"{User.Data.rp_name}");
            };
           
            menu.AddMenuItem(UiMenu, "Выдать оружие в радиусе").Activated += async (uimenu, item) =>
            {
                HideMenu();
                string gun = await Menu.GetUserInput("Оружие", "", 30);
                int r = Convert.ToInt32(await Menu.GetUserInput("Радиус", "", 10));
                int pt = Convert.ToInt32(await Menu.GetUserInput("Патроны", "", 10));
                var coords = GetEntityCoords(GetPlayerPed(-1), true);
                Shared.TriggerEventToAllPlayers("ARP:GiveGunMp", coords.X, coords.Y, coords.Z, r, gun, pt);
                Main.SaveLog("AdminMpGun", $"{User.Data.rp_name} {r}f, {gun}, {pt}");
            };
           
            menu.AddMenuItem(UiMenu, "Выдать ХП в радиусе").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int r = Convert.ToInt32(await Menu.GetUserInput("Радиус", "", 10));
                int pt = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 10));
                var coords = GetEntityCoords(GetPlayerPed(-1), true);
                Shared.TriggerEventToAllPlayers("ARP:GiveHealthMp", coords.X, coords.Y, coords.Z, r, pt);
                Main.SaveLog("AdminMpHeal", $"{User.Data.rp_name} {r}f, {pt}");
            };
           
            menu.AddMenuItem(UiMenu, "Выдать броню в радиусе").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int r = Convert.ToInt32(await Menu.GetUserInput("Радиус", "", 10));
                int pt = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 10));
                var coords = GetEntityCoords(GetPlayerPed(-1), true);
                Shared.TriggerEventToAllPlayers("ARP:GiveArmorMp", coords.X, coords.Y, coords.Z, r, pt);
                Main.SaveLog("AdminMpArmor", $"{User.Data.rp_name} {r}f, {pt}");
            };
           
            menu.AddMenuItem(UiMenu, "Выдать скины в радиусе").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int r = Convert.ToInt32(await Menu.GetUserInput("Радиус", "", 10));
                string skin = await Menu.GetUserInput("Название скина", "", 32);
                var coords = GetEntityCoords(GetPlayerPed(-1), true);
                Shared.TriggerEventToAllPlayers("ARP:GiveSkinMp", coords.X, coords.Y, coords.Z, r, skin);
                Main.SaveLog("AdminMpSkin", $"{User.Data.rp_name} {r}f, {skin}");
            };
            
            menu.AddMenuItem(UiMenu, "Адреналин в радиусе").Activated += async (uimenu, item) =>
            {
                HideMenu();
                
                var num = await Menu.GetUserInput("Радиус", null, 4);
                if (num == "NULL") return;
                
                foreach (CitizenFX.Core.Player p in Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), Convert.ToInt32(num)))
                    Shared.TriggerEventToPlayer(p.ServerId, "ARP:UseAdrenalin");
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowAdminVehicleMenu()
        {
            HideMenu();

            await User.GetAllData();

            if (!User.IsAdmin())
                return;
            
            var menu = new Menu();
            UiMenu = menu.Create("Admin", "~b~Меню транспорта");
           
            menu.AddMenuItem(UiMenu, "Спавн авто").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var coords = GetEntityCoords(GetPlayerPed(-1), true);
                var vehName = await Menu.GetUserInput("Введите название машины", "", 100);
                await Managers.Vehicle.SpawnByName(vehName, coords, GetEntityHeading(GetPlayerPed(-1)));
                Notification.SendWithTime("~b~Spawn car~s~\n" + coords.X + ", " + coords.Y + ", " + coords.Z);
                
                Main.SaveLog("AdminSpawnVehicle", $"{User.Data.rp_name} - {vehName}");
            };
           
            menu.AddMenuItem(UiMenu, "Починить").Activated += (uimenu, item) =>
            {
                HideMenu();
                if (!IsPedInAnyVehicle(PlayerPedId(), true)) return;
                var veh = GetVehiclePedIsUsing(PlayerPedId());
                Managers.Vehicle.Repair(new CitizenFX.Core.Vehicle(veh));
                Notification.SendWithTime("~b~Вы починили транспорт");
            };
            
            menu.AddMenuItem(UiMenu, "Удалить авто в радиусе").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var num = await Menu.GetUserInput("Радиус", null, 4);
                if (num == "NULL") return;
                
                foreach (CitizenFX.Core.Vehicle v in Main.GetVehicleListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), Convert.ToInt32(num)))
                    v.Delete();
            };
            
            menu.AddMenuItem(UiMenu, "Установить неон").Activated += async (uimenu, idx) =>
            {
                int r = Convert.ToInt32(await Menu.GetUserInput("Цена", "", 3));
                int g = Convert.ToInt32(await Menu.GetUserInput("Цена", "", 3));
                int b = Convert.ToInt32(await Menu.GetUserInput("Цена", "", 3));

                var vehicle = GetVehiclePedIsUsing(PlayerPedId());
                SetVehicleNeonLightsColour(vehicle, r, g, b);
                SetVehicleNeonLightEnabled(vehicle, 0, true);
                SetVehicleNeonLightEnabled(vehicle, 1, true);
                SetVehicleNeonLightEnabled(vehicle, 2, true);
                SetVehicleNeonLightEnabled(vehicle, 3, true);
            };
            
            menu.AddMenuItem(UiMenu, "Удалить ботов в радиусе").Activated += async (uimenu, item) =>
            {
                HideMenu();
                
                var num = await Menu.GetUserInput("Радиус", null, 4);
                if (num == "NULL") return;
                
                foreach (CitizenFX.Core.Ped p in Main.GetPedListOnRadius(Convert.ToInt32(num)))
                    p.Delete();
            };
            
            menu.AddMenuItem(UiMenu, "Выдать ключ от авто").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var num = await Menu.GetUserInput("Номер", null, 8);
                if (num == "NULL") return;
                User.AddVehicleKey(num);
                Notification.SendWithTime($"~b~Вы взяли ключи для ТС с номером: {num}");
            };

            menu.AddMenuItem(UiMenu, "Удалить ближ ТС", "Временная функция для багнутых ТС").Activated +=
                async (uimenu, item) =>
                {
                    HideMenu();

                    Shared.TriggerEventToPlayer(User.GetServerId(), "ARP:IncarAdmin");
                };

            if (User.IsAdmin(3))
            {
                menu.AddMenuItem(UiMenu, "Сменить номер").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    if (!IsPedInAnyVehicle(PlayerPedId(), true)) return;
                    var veh = GetVehiclePedIsUsing(PlayerPedId());
                    var newNumber = await Menu.GetUserInput("Номер ТС", "", 8);
                    SetVehicleNumberPlateText(veh, newNumber.ToUpper());
                    Notification.SendWithTime("~b~Вы сменили номер ТС");
                    
                    Main.SaveLog("AdminChangeNumberVehicle", $"{User.Data.rp_name} - {newNumber}");
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowAdminTeleportMenu()
        {
            HideMenu();

            await User.GetAllData();

            if (!User.IsAdmin())
                return;
            
            var menu = new Menu();
            UiMenu = menu.Create("Admin", "~b~Меню телепорта");
           
            menu.AddMenuItem(UiMenu, "Телепорт на метку").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Teleport(new Vector3(World.WaypointPosition.X, World.WaypointPosition.Y, World.GetGroundHeight(World.WaypointPosition)));
                Notification.SendWithTime("~b~Teleported");
                User.Invisible(PlayerId(), true);
                Main.SaveLog("AdminTeleportPoint", $"{User.Data.rp_name} {new Vector3(World.WaypointPosition.X, World.WaypointPosition.Y, World.GetGroundHeight(World.WaypointPosition))}");
            };
           
            menu.AddMenuItem(UiMenu, "Телепорт к игроку").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowAdminTeleportToPlayerMenu();
            };
           
            menu.AddMenuItem(UiMenu, "Телепорт к себе").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowAdminTeleportToMeMenu();
            };
           
            menu.AddMenuItem(UiMenu, "Телепорт по номеру дома").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var idH = Convert.ToInt32(await Menu.GetUserInput("X", "", 10))-1;
                if (House.HouseGlobalDataList.Count < idH) return;
                User.Teleport(new Vector3(House.HouseGlobalDataList[idH].x, House.HouseGlobalDataList[idH].y, House.HouseGlobalDataList[idH].z));
                Main.SaveLog("AdminTeleportHouse", $"{User.Data.rp_name} {idH}");
            };
           
            menu.AddMenuItem(UiMenu, "Телепорт по координатам").Activated += async (uimenu, item) =>
            {
                HideMenu();

                var x = await Menu.GetUserInput("X", "", 10);
                var y = await Menu.GetUserInput("Y", "", 10);
                var z = await Menu.GetUserInput("Z", "", 10);
                    
                User.Teleport(new Vector3(Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(z)));
                Notification.SendWithTime("~b~Teleported~s~\n" + x + ", " + y + ", " + z);
                Main.SaveLog("AdminTeleportCoord", $"{User.Data.rp_name} {x}, {y}, {z}");
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += async (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowDeveloperMenu()
        {
            HideMenu();

            await User.GetAllData();

            if (!User.IsAdmin())
                return;
            
            var menu = new Menu();
            UiMenu = menu.Create("Developer", $"~b~Ped:~s~ {Main.GetPedListOnRadius(9999f).Count}. ~b~Veh:~s~ {Main.GetVehicleListOnRadius(9999f).Count}");
           
            if (User.IsAdmin(5))
            {
                menu.AddMenuItem(UiMenu, "Координаты игрока").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    var name = await Menu.GetUserInput("Name", "", 200);
                    var coords = GetEntityCoords(GetPlayerPed(-1), true);
                    Main.SaveLog("coords", name + ": " + coords.X + ", " + coords.Y + ", " + (coords.Z - 1) + ", " + GetEntityHeading(GetPlayerPed(-1)));
                    Notification.Send("~b~Save coords~s~\n" + coords.X + ", " + coords.Y + ", " + (coords.Z - 1) + ", " + GetEntityHeading(GetPlayerPed(-1)));
                };
            
                menu.AddMenuItem(UiMenu, "Координаты ТС").Activated += async (uimenu, item) =>
                {
                    if (!IsPedInAnyVehicle(PlayerPedId(), true)) return;
                    HideMenu();
                    
                    var name = await Menu.GetUserInput("Name", "", 200);
                    var veh = GetVehiclePedIsUsing(PlayerPedId());
                    var coords = GetEntityCoords(veh, true);
                    
                    Main.SaveLog("coords", name + "|" + "Veh: " + coords.X + ", " + coords.Y + ", " + coords.Z + ", " + GetEntityHeading(veh));
                    Notification.Send("~b~Save veh coords~s~\n" + coords.X + ", " + coords.Y + ", " + coords.Z + ", " + GetEntityHeading(veh));
                };
                
                menu.AddMenuItem(UiMenu, "Load IPL").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    var ipl = await Menu.GetUserInput("IPL Name", "", 200);
                    RequestIpl(ipl);
                    Notification.SendWithTime("~b~IPL Loaded~s~\n" + ipl);
                };
                
                menu.AddMenuItem(UiMenu, "UnLoad IPL").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    var ipl = await Menu.GetUserInput("IPL Name", "", 200);
                    RemoveIpl(ipl);
                    Notification.SendWithTime("~b~IPL UnLoaded~s~\n" + ipl);
                };
                
                menu.AddMenuItem(UiMenu, "Новый ТС").Activated += async (uimenu, item) =>
                {
                    HideMenu();

                    if (IsPedInAnyVehicle(PlayerPedId(), true))
                    {
                        var veh = GetVehiclePedIsUsing(PlayerPedId());
                        if (GetPedInVehicleSeat(veh, -1) == PlayerPedId())
                        {
                            var v = new CitizenFX.Core.Vehicle(veh);
                            var vData = VehInfo.Get(v.Model.Hash);
                            
                            int price = Convert.ToInt32(await Menu.GetUserInput("Цена", "", 10));
                            int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 4));
                            var pos = v.Position;
                            
                            TriggerServerEvent("ARP:AddNewVehicle", v.Model.Hash, vData.DisplayName, vData.ClassName, vData.FullFuel, vData.FuelMinute, vData.StockFull, vData.Stock, pos.X, pos.Y, pos.Z, v.Heading, price, count);
                            Notification.SendWithTime($"~b~Транспорт {vData.DisplayName}({count}) добавлен.");
                        }
                    }
                };
                
                menu.AddMenuItem(UiMenu, "Новый Склад").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    var pos = GetEntityCoords(GetPlayerPed(-1), true);                    
                    int price = Convert.ToInt32(await Menu.GetUserInput("Цена", "", 10));                            
                    TriggerServerEvent("ARP:AddNewStock", UI.GetPlayerZoneName(), pos.X, pos.Y, pos.Z-1, price);
                    Notification.SendWithTime($"~b~Склад добавлен.");
                };
                
                menu.AddMenuItem(UiMenu, "Новая квартира").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    var pos = GetEntityCoords(GetPlayerPed(-1), true);                    
                    int price = Convert.ToInt32(await Menu.GetUserInput("Цена", "", 10));                            
                    int intId = Convert.ToInt32(await Menu.GetUserInput("Интерьер", "", 10));                            
                    TriggerServerEvent("ARP:AddNewCondo", UI.GetPlayerZoneName(), pos.X, pos.Y, pos.Z-1, price, intId);
                    
                    var blip = World.CreateBlip(pos);
                    blip.Sprite = (BlipSprite) 40;
                    blip.IsShortRange = true;
                    blip.Scale = 0.4f;
                    
                    Notification.SendWithTime($"~b~Квартира добавлена.");
                };
                
                menu.AddMenuItem(UiMenu, "Новые апартаменты").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    var pos = GetEntityCoords(GetPlayerPed(-1), true);                    
                    int bId = Convert.ToInt32(await Menu.GetUserInput("ID Дома", "", 10));                            
                    int floor = Convert.ToInt32(await Menu.GetUserInput("Кол-во этажей", "", 10));                            
                    int intCount = Convert.ToInt32(await Menu.GetUserInput("Кол-во интерьеров", "", 10));

                    for (int j = 0; j < intCount; j++)
                    {       
                        int intId = Convert.ToInt32(await Menu.GetUserInput("ID Интерьера", "", 10));
                        int price = Convert.ToInt32(await Menu.GetUserInput("Цена", "", 10));
                        for (int i = 1; i <= floor; i++)
                            TriggerServerEvent("ARP:AddNewApart", bId, i, price, intId);
                    }
                    
                    /*for (int i = 1; i <= floor; i++)
                    {
                        for (int j = 0; j < intCount; j++)
                        {       
                            int intId = Convert.ToInt32(await Menu.GetUserInput("ID Интерьера", "", 10));
                            int price = Convert.ToInt32(await Menu.GetUserInput("Цена", "", 10));
                            TriggerServerEvent("ARP:AddNewApart", bId, i, price, intId);
                        }
                    }*/
                    
                    Main.SaveLog("buildId", $"{{{pos.X}, {pos.Y}, {pos.Z-1}, {floor}, 0, 0, 0}}, //{bId} {UI.GetPlayerZoneName()}");
                    
                    var blip = World.CreateBlip(pos);
                    blip.Sprite = (BlipSprite) 475;
                    blip.IsShortRange = true;
                    blip.Scale = 0.4f;
                    
                    Notification.SendWithTime($"~b~Апартаменты добавлены.");
                };
                
                var list = new List<dynamic>();
                for (int i = 0; i < 223; i++)
                    list.Add(i);
                menu.AddMenuItemList(UiMenu, "Спавн объекта", list).OnListSelected += (uimenu, idx) =>
                {
                    Objects.CreateObject(Inventory.GetItemHashById(idx), GetEntityCoords(GetPlayerPed(-1), true), new Vector3(0, 0, 0), true, true);
                };
                
                var listLiv = new List<dynamic>();
                for (int i = 0; i < 20; i++)
                    listLiv.Add(i);
                menu.AddMenuItemList(UiMenu, "Liv", listLiv).OnListSelected += (uimenu, idx) =>
                {
                    SetVehicleLivery(GetVehiclePedIsUsing(PlayerPedId()), idx);
                };
            }

            menu.AddMenuItem(UiMenu, "Узнать хеш авто").Activated += (uimenu, item) =>
            {
                HideMenu();
                if (IsPedInAnyVehicle(PlayerPedId(), true))
                {
                    Debug.WriteLine("VEHICLE HASH: " + GetEntityModel(GetVehiclePedIsUsing(PlayerPedId())));
                    Main.SaveLog("VEHHASH", $"{GetDisplayNameFromVehicleModel((uint) GetEntityModel(GetVehiclePedIsUsing(PlayerPedId())))} {GetEntityModel(GetVehiclePedIsUsing(PlayerPedId()))}");
                }
            };
            
            menu.AddMenuItem(UiMenu, "Усилить патрули 1").Activated += (uimenu, item) =>
            {
                HideMenu();
                Shared.TriggerEventToAllPlayers("ARP:HightSapd", 1);
                Notification.SendWithTime("~b~Патрули усиленны.");
            };
            
            menu.AddMenuItem(UiMenu, "Усилить патрули 2").Activated += (uimenu, item) =>
            {
                HideMenu();
                Shared.TriggerEventToAllPlayers("ARP:HightSapd", 2);
                Notification.SendWithTime("~b~Патрули усиленны.");
            };
            
            menu.AddMenuItem(UiMenu, "В тюрьму").Activated += (uimenu, item) =>
            {
                HideMenu();
                Jail.JailPlayerScene(600);
            };
            
            menu.AddMenuItem(UiMenu, "Очистить").Activated += (uimenu, item) =>
            {
                HideMenu();
                Shared.TriggerEventToAllPlayers("ARP:HightSapd", 0);
                Notification.SendWithTime("~b~Патрули очищены.");
            };
            
            var weatherList = new List<dynamic>
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
                "NEUTRAL",
                "SNOW",
                "BLIZZARD",
                "SNOWLIGHT",
                "XMAS",
                "HALLOWEEN"
            };

            menu.AddMenuItemList(UiMenu, "Погода", weatherList).OnListSelected += (uimenu, idx) =>
            {
                SetWeatherTypePersist(weatherList[idx].ToString());
                SetWeatherTypeNowPersist(weatherList[idx].ToString());
                SetWeatherTypeNow(weatherList[idx].ToString());
                SetOverrideWeather(weatherList[idx].ToString());

                if (
                    weatherList[idx].ToString() == "XMAS" ||
                    weatherList[idx].ToString() == "SNOWLIGHT" ||
                    weatherList[idx].ToString() == "BLIZZARD" ||
                    weatherList[idx].ToString() == "SNOW"
                    )
                {
                    SetForceVehicleTrails(true);
                    SetForcePedFootstepsTracks(true);
                    SetOverrideWeather("XMAS");
                }
                else
                {
                    SetForceVehicleTrails(false);
                    SetForcePedFootstepsTracks(false);
                }
            };

            menu.AddMenuItemList(UiMenu, "Погода2", weatherList).OnListSelected += (uimenu, idx) =>
            {
                World.TransitionToWeather(Weather.ThunderStorm, 60);
            };
            
            menu.AddMenuItem(UiMenu, "Ветер").Activated += (uimenu, item) =>
            {
                HideMenu();
                Notification.SendWithTime($"~b~Ветер.{GetWindSpeed()} | {GetWindDirection()}");
            };
            
            menu.AddMenuItem(UiMenu, "Эффекты офф").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                User.SetDrugAmfLevel(0);
                User.SetDrugCocaLevel(0);
                User.SetDrugDmtLevel(0);
                User.SetDrugKetLevel(0);
                User.SetDrugLsdLevel(0);
                User.SetDrugMargLevel(0);
                User.SetDrugMefLevel(0);
                User.SetDrunkLevel(0);
                
                StopAllScreenEffects();
            };

            var list2 = new List<dynamic>
            {
                "SwitchHUDIn",
                "SwitchHUDOut",
                "FocusIn",
                "FocusOut",
                "MinigameEndNeutral",
                "MinigameEndTrevor",
                "MinigameEndFranklin",
                "MinigameEndMichael",
                "MinigameTransitionOut",
                "MinigameTransitionIn",
                "SwitchShortNeutralIn",
                "SwitchShortFranklinIn",
                "SwitchShortTrevorIn",
                "SwitchShortMichaelIn",
                "SwitchOpenMichaelIn",
                "SwitchOpenFranklinIn",
                "SwitchOpenTrevorIn",
                "SwitchHUDMichaelOut",
                "SwitchHUDFranklinOut",
                "SwitchHUDTrevorOut",
                "SwitchShortFranklinMid",
                "SwitchShortMichaelMid",
                "SwitchShortTrevorMid",
                "DeathFailOut",
                "CamPushInNeutral",
                "CamPushInFranklin",
                "CamPushInMichael",
                "CamPushInTrevor",
                "SwitchOpenMichaelIn",
                "SwitchSceneFranklin",
                "SwitchSceneTrevor",
                "SwitchSceneMichael",
                "SwitchSceneNeutral",
                "MP_Celeb_Win",
                "MP_Celeb_Win_Out",
                "MP_Celeb_Lose",
                "MP_Celeb_Lose_Out",
                "DeathFailNeutralIn",
                "DeathFailMPDark",
                "DeathFailMPIn",
                "MP_Celeb_Preload_Fade",
                "PeyoteEndOut",
                "PeyoteEndIn",
                "PeyoteIn",
                "PeyoteOut",
                "MP_race_crash",
                "SuccessFranklin",
                "SuccessTrevor",
                "SuccessMichael",
                "DrugsMichaelAliensFightIn",
                "DrugsMichaelAliensFight",
                "DrugsMichaelAliensFightOut",
                "DrugsTrevorClownsFightIn",
                "DrugsTrevorClownsFight",
                "DrugsTrevorClownsFightOut",
                "HeistCelebPass",
                "HeistCelebPassBW",
                "HeistCelebEnd",
                "HeistCelebToast",
                "MenuMGHeistIn",
                "MenuMGTournamentIn",
                "MenuMGSelectionIn",
                "ChopVision",
                "DMT_flight_intro",
                "DMT_flight",
                "DrugsDrivingIn",
                "DrugsDrivingOut",
                "SwitchOpenNeutralFIB5",
                "HeistLocate",
                "MP_job_load",
                "RaceTurbo",
                "MP_intro_logo",
                "HeistTripSkipFade",
                "MenuMGHeistOut",
                "MP_corona_switch",
                "MenuMGSelectionTint",
                "SuccessNeutral",
                "ExplosionJosh3",
                "SniperOverlay",
                "RampageOut",
                "Rampage",
                "Dont_tazeme_bro",
                "DeathFailOut"
            };

            menu.AddMenuItemList(UiMenu, "Эффекты", list2).OnListSelected += (uimenu, idx) =>
            {
                StopAllScreenEffects();
                StartScreenEffect(list2[idx].ToString(), 0, true);
            };

            var list3 = new List<dynamic>
            {
                "ANIM_GROUP_MOVE_BALLISTIC",
                "ANIM_GROUP_MOVE_LEMAR_ALLEY",
                "clipset@move@trash_fast_turn",
                "FEMALE_FAST_RUNNER",
                "missfbi4prepp1_garbageman",
                "move_characters@franklin@fire",
                "move_characters@Jimmy@slow@",
                "move_characters@michael@fire",
                "move_f@flee@a",
                "move_f@scared",
                "move_f@sexy@a",
                "move_heist_lester",
                "move_injured_generic",
                "move_lester_CaneUp",
                "move_m@bag",
                "MOVE_M@BAIL_BOND_NOT_TAZERED",
                "MOVE_M@BAIL_BOND_TAZERED",
                "move_m@brave",
                "move_m@casual@d",
                "move_m@drunk@moderatedrunk",
                "MOVE_M@DRUNK@MODERATEDRUNK",
                "MOVE_M@DRUNK@MODERATEDRUNK_HEAD_UP",
                "MOVE_M@DRUNK@SLIGHTLYDRUNK",
                "MOVE_M@DRUNK@VERYDRUNK",
                "move_m@fire",
                "move_m@gangster@var_e",
                "move_m@gangster@var_f",
                "move_m@gangster@var_i",
                "move_m@JOG@",
                "MOVE_M@PRISON_GAURD",
                "MOVE_P_M_ONE",
                "MOVE_P_M_ONE_BRIEFCASE",
                "move_p_m_zero_janitor",
                "move_p_m_zero_slow",
                "move_ped_bucket",
                "move_ped_crouched",
                "move_ped_mop",
                "MOVE_M@FEMME@",
                "MOVE_F@FEMME@",
                "MOVE_M@GANGSTER@NG",
                "MOVE_F@GANGSTER@NG",
                "MOVE_M@POSH@",
                "MOVE_F@POSH@",
                "MOVE_M@TOUGH_GUY@",
                "MOVE_F@TOUGH_GUY@"
            };

            menu.AddMenuItemList(UiMenu, "Походка", list3).OnListSelected += async (uimenu, idx) =>
            {
                ResetPedMovementClipset(GetPlayerPed(-1), 0);
                
                string clipSet = list3[idx].ToString();
                RequestClipSet(clipSet);
                if (!HasClipSetLoaded(clipSet))
                    await Delay(100);
                
                SetPedMovementClipset(GetPlayerPed(-1), clipSet, 0);
            };

            menu.AddMenuItem(UiMenu, "Взять педа 1").Activated += (uimenu, item) =>
            {
                HideMenu();

                CitizenFX.Core.Ped ped = Main.FindNearestPed();

                if (ped == null)
                    return;
                
                SetEntityAsMissionEntity(ped.Handle, true, true);
                
                ClearPedTasks(ped.Handle);
                ClearPedTasksImmediately(ped.Handle);
                ClearPedSecondaryTask(ped.Handle);
                
                AttachEntityToEntity(ped.Handle, GetPlayerPed(-1), 28422, -0.0300f, 0.0000f, -0.1100f, 0.0000f, 0.0000f, 86.0000f, false, true, false, true, 2, true);
                //AttachEntityToEntity(ped.Handle, GetPlayerPed(-1), 28422, 0, 0, 0, 0, 0, 0, false, false, false, true, 0, true);
                User.PlayAnimation("anim@heists@box_carry@", "idle");
                User.PlayPedAnimation(ped, "anim@veh@lowrider@low@front_ps@arm@base", "sit", 9);
                
                SetPedToRagdoll(ped.Handle, 50000, 50000, 0, false, false, false);
            };
            
            menu.AddMenuItem(UiMenu, "Взять педа 2").Activated += (uimenu, item) =>
            {
                HideMenu();

                CitizenFX.Core.Ped ped = Main.FindNearestPed();
                
                if (ped == null)
                    return;
                
                SetEntityAsMissionEntity(ped.Handle, true, true);
                
                ClearPedTasks(ped.Handle);
                ClearPedTasksImmediately(ped.Handle);
                ClearPedSecondaryTask(ped.Handle);
                
                AttachEntityToEntity(ped.Handle, GetPlayerPed(-1), 24818, -0.0300f, 0.0000f, -0.1100f, 0.0000f, 0.0000f, 86.0000f, false, true, false, true, 2, true);
                //AttachEntityToEntity(ped.Handle, GetPlayerPed(-1), 28422, 0, 0, 0, 0, 0, 0, false, false, false, true, 0, true);
                User.PlayAnimation("missfbi3_camcrew", "base_cman");
                User.PlayPedAnimation(ped, "amb@world_human_bum_slumped@male@laying_on_left_side@base", "base");
                
                SetPedToRagdoll(ped.Handle, 50000, 50000, 0, false, false, false);
            };
            
            menu.AddMenuItem(UiMenu, "Blackout").Activated += (uimenu, item) =>
            {
                HideMenu();
                Ctos.HackBlackout();
            };
            
            menu.AddMenuItem(UiMenu, "Телефоны").Activated += (uimenu, item) =>
            {
                HideMenu();
                Ctos.HackPhoneInRadius();
            };
            
            menu.AddMenuItem(UiMenu, "ElecboxDestroy").Activated +=  (uimenu, item) =>
            {
                HideMenu();
                Ctos.ElecboxDestroy();
            };
            
            menu.AddMenuItem(UiMenu, "StreetLightDestroy").Activated +=  (uimenu, item) =>
            {
                HideMenu();
                Ctos.StreetLightDestroy();
            };
            
            menu.AddMenuItem(UiMenu, "TrafficLightDestroy").Activated +=  (uimenu, item) =>
            {
                HideMenu();
                Ctos.TrafficLightDestroy();
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static async void TakePed1(List<Player> pedList)
            {
                
                HideMenu();
            
                var menu = new Menu();
                UiMenu = menu.Create("Взять на руки", "~b~Взять игрока на руки");
                
                foreach (Player p in pedList)
                {
                    try
                    {
                        if (p.ServerId == GetPlayerServerId(PlayerId())) continue;    
                        if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                        menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += async (uimenu, item) =>
                        {
                            HideMenu();
                            CitizenFX.Core.Ped ped = new CitizenFX.Core.Ped(GetPlayerPed((p.ServerId)));;

                            if (ped == null)
                            {
                                Debug.WriteLine("Ped == NULL");
                                return;
                            }

                            SetEntityAsMissionEntity(ped.Handle, true, true);
                
                            ClearPedTasks(ped.Handle);
                            ClearPedTasksImmediately(ped.Handle);
                            ClearPedSecondaryTask(ped.Handle);

                            AttachEntityToEntity(GetPlayerPed(-1), ped.Handle, 9816, 0.005f, 0.25f, 0.01f, 0.9f, 0.30f, 192.0f, false, false, false, false, 2, false);
                            //AttachEntityToEntity(ped.Handle, GetPlayerPed(-1), 28422, -0.0300f, 0.0000f, -0.1100f, 0.0000f, 0.0000f, 86.0000f, false, true, false, true, 2, true);
                            //AttachEntityToEntity(ped.Handle, GetPlayerPed(-1), 0, 0, 0, 0, 0, 0, 0, false, false, false, true, 0, true);

                            User.PlayAnimation("anim@heists@box_carry@", "idle");
                            User.PlayPedAnimation(ped, "anim@veh@lowrider@low@front_ps@arm@base", "sit", 9);

                        };
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                        throw;
                    }
                }

                var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
                UiMenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == closeButton)
                        HideMenu();
                };
            
                MenuPool.Add(UiMenu);
            }
        
        public static async void AdminGiveMoney()
        {
            var money = Convert.ToInt32(await Menu.GetUserInput("Не более $500.000! Если больше - будет бан!", "", 6));
            User.AddMoney(money);
            Main.SaveLog("AdminGiveMoney", $"{User.Data.rp_name} - {money}");
        }
        
        public static void ShowAdminClothMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Admin", "~b~Одежда");

            var list = new List<dynamic>();
            for (int i = 0; i < 500; i++)
                list.Add(i);
            
            var listColor = new List<dynamic>();
            for (int i = -1; i < 100; i++)
                listColor.Add(i);

            int[] id = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] idColor = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < 12; i++)
            {
                int i1 = i;
                menu.AddMenuItemList(UiMenu, "Слот " + i, list).OnListChanged += (uimenu, idx) =>
                {
                    id[i1] = idx;
                    SetPedComponentVariation(GetPlayerPed(-1), i1, id[i1], idColor[i1], 2);
                    
                };
                menu.AddMenuItemList(UiMenu, "Цвет " + i, listColor).OnListChanged += (uimenu, idx) =>
                {
                    idColor[i1] = idx;
                    SetPedComponentVariation(GetPlayerPed(-1), i1, id[i1], idColor[i1], 2);
                };
                menu.AddMenuItem(UiMenu, "~b~========================");
            }
            
            for (int i = 0; i < 8; i++)
            {
                int i1 = i;
                menu.AddMenuItemList(UiMenu, "ПСлот " + i, list).OnListChanged += (uimenu, idx) =>
                {
                    id[i1] = idx;
                    SetPedPropIndex(GetPlayerPed(-1), i1, id[i1], idColor[i1], true);
                };
                menu.AddMenuItemList(UiMenu, "ПЦвет " + i, listColor).OnListChanged += (uimenu, idx) =>
                {
                    idColor[i1] = idx;
                    SetPedPropIndex(GetPlayerPed(-1), i1, id[i1], idColor[i1], true);
                };
                menu.AddMenuItem(UiMenu, "~b~========================");
            }
            
            for (int overlayid = 0; overlayid < 13; overlayid++)
            {
                var listOv = new List<dynamic>();
                for (int i = 0; i < GetNumHeadOverlayValues(overlayid); i++)
                    listOv.Add(i);
                var overlayid1 = overlayid;
                menu.AddMenuItemList(UiMenu, $"Overlay{overlayid}", listOv).OnListChanged += (uimenu, idx) =>
                {
                    int colortype = (overlayid1 == 1 || overlayid1 == 2 || overlayid1 == 10)
                        ? 1
                        : (overlayid1 == 5 || overlayid1 == 8)
                            ? 2
                            : 0;
                    
                    SetPedHeadOverlay(GetPlayerPed(-1), overlayid1, idx, 1f);
                    SetPedHeadOverlayColor(GetPlayerPed(-1), overlayid1, colortype, 1, 1);
                };
            
            }

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static async void ShowTesterMenu()
        {
            await User.GetAllData();

            if (!User.IsAdmin())
                return;
            
            var menu = new Menu();
            UiMenu = menu.Create("Меню для тестов", "~b~Не использовать в личных целях~s~ ");
            
            menu.AddMenuItem(UiMenu, "Работы").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowMeriaJobListMenu();
            };
            menu.AddMenuItem(UiMenu, "Лицензии ТС").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowLicBuyMenu();
            };
            menu.AddMenuItem(UiMenu, "Деньги", "Не более $500.000").Activated += (uimenu, item) =>
            {
                HideMenu();
                AdminGiveMoney();
            };
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
        }
        
        public static void ShowOnlinePlayersMenu()
        {
            HideMenu();
            
            PlayerList plList = new PlayerList();

            var menu = new Menu();
            UiMenu = menu.Create("Список", "~b~Всего игроков:~s~ " + plList.Count());
            
            foreach (Player p in plList)
                menu.AddMenuItem(UiMenu, $"~b~{p.Name} ({p.ServerId})~s~");
          
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowMainMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Меню", "~b~Главное меню");

            var players = new PlayerList();
            
            menu.AddMenuItem(UiMenu, "Персонаж").Activated += (uimenu, item) =>
            {
                ShowPlayerMenu();
            };
            
            menu.AddMenuItem(UiMenu, "Транспорт").Activated += (uimenu, item) =>
            {
                if (IsPedInAnyVehicle(PlayerPedId(), true))
                {
                    var veh = GetVehiclePedIsUsing(PlayerPedId());
                    if (GetPedInVehicleSeat(veh, -1) == PlayerPedId())
                        ShowVehicleMenu(new CitizenFX.Core.Vehicle(veh));
                    else
                        Notification.SendWithTime("~r~Вы должны быть на водительском месте");
                }
                else
                {
                    var veh = Main.FindNearestVehicle();
                    if (veh != null)
                        ShowVehicleOutMenu(veh);
                    else
                        ShowVehicleOut2Menu();
                }
            };

            if (User.Data.fraction_id > 0) {
                menu.AddMenuItem(UiMenu, "Организация").Activated += async (uimenu, item) =>
                {
                    await User.GetAllData();
                    ShowFractionMenu();
                };
            }

            if (User.Data.fraction_id2 > 0) {
                menu.AddMenuItem(UiMenu, "Неоф. Организация").Activated += async (uimenu, item) =>
                {
                    await User.GetAllData();
                    ShowFraction2Menu();
                };
            }
            
            menu.AddMenuItem(UiMenu, "Помощь").Activated += (uimenu, item) =>
            {
                ShowHelpMenu();
            };
            
            menu.AddMenuItem(UiMenu, "GPS").Activated += (uimenu, item) =>
            {
                ShowGpsMenu();
            };
            
            menu.AddMenuItem(UiMenu, "Настройки").Activated += (uimenu, item) =>
            {
                ShowSettingsMenu();
            };
            
            menu.AddMenuItem(UiMenu, "~b~Ввести промокод").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var msg = await Menu.GetUserInput("Промокод", null, 10);
                if (msg == "NULL") return;
                TriggerServerEvent("ARP:Promocode", Main.RemoveQuotesAndSpace(msg.ToUpper()));
            };
            
            menu.AddMenuItem(UiMenu, "~y~Задать вопрос").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var msg = await Menu.GetUserInput("Напишите вопрос", null, 200);
                if (msg == "NULL") return;
                
                Shared.TriggerEventToAllPlayers("ARP:SendAskMessage", msg, User.Data.id, User.Data.rp_name);
                
                Notification.SendWithTime("~g~Вопрос отправлен");
                Notification.SendWithTime("~g~Если хелперы в сети, они вам ответят");
            };
            if (User.IsHelper())
            {
                menu.AddMenuItem(UiMenu, "~y~Ответить на вопрос").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    var id = await Menu.GetUserInput("ID игрока", null, 6);
                    if (id == "NULL") return;
                    var msg = await Menu.GetUserInput("Ответ", null, 200);
                    if (msg == "NULL") return;

                    int money = 5 + (User.Data.helper_level * 5);
                    
                    Main.SaveLog("HelperAnswer", $"Helper: {User.Data.rp_name} to {id}. {msg}");
                    
                    User.AddCashMoney(money);
                    User.Data.count_hask++;
                    Client.Sync.Data.Set(User.GetServerId(), "count_hask", User.Data.count_hask);
                    Shared.TriggerEventToAllPlayers("ARP:SendAskToPlayerMessage", msg, Convert.ToInt32(id), User.Data.id, User.Data.rp_name);
                    Notification.SendWithTime("~g~Ответ отправлен. +$" + money);
                };
            }

            menu.AddMenuItem(UiMenu, "~r~Жалоба").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var msg = await Menu.GetUserInput("Напишите жалобу", null, 200);
                if (msg == "NULL") return;
                
                Shared.TriggerEventToAllPlayers("ARP:SendReportMessage", msg, User.Data.id, User.Data.rp_name);
                
                Notification.SendWithTime("~g~Жалоба отправлена");
                Notification.SendWithTime("~g~Если администрация в сети, она её рассмотрит");
            };
            if (User.IsAdmin())
            {
                menu.AddMenuItem(UiMenu, "~r~Ответить на жалобу").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    var id = await Menu.GetUserInput("ID игрока", null, 6);
                    if (id == "NULL") return;
                    var msg = await Menu.GetUserInput("Ответ", null, 200);
                    if (msg == "NULL") return;

                    int money = 15 + (User.Data.admin_level * 5);

                    Main.SaveLog("ReportAnswer", $"Admin: {User.Data.rp_name} to {id}. {msg}");

                    User.AddCashMoney(money);
                    User.Data.count_aask++;
                    Client.Sync.Data.Set(User.GetServerId(), "count_aask", User.Data.count_aask);
                    Shared.TriggerEventToAllPlayers("ARP:SendReportToPlayerMessage", msg, Convert.ToInt32(id),
                        User.Data.id, User.Data.rp_name);
                    Notification.SendWithTime("~g~Ответ отправлен. +$" + money);
                };
                menu.AddMenuItem(UiMenu, "~o~Предупредить игрока").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    var id = await Menu.GetUserInput("ID игрока", null, 6);
                    if (id == "NULL") return;
                    var msg = await Menu.GetUserInput("Ответ", null, 200);
                    if (msg == "NULL") return;
                
                    int money = 15 + (User.Data.admin_level * 5);
                    
                    Main.SaveLog("ReportAnswer", $"Admin: {User.Data.rp_name} to {id}. {msg}");
                    
                    User.AddCashMoney(money);
                    User.Data.count_aask++;
                    Client.Sync.Data.Set(User.GetServerId(), "count_aask", User.Data.count_aask);
                    Shared.TriggerEventToAllPlayers("ARP:SendWarningToPlayerMessage", msg, Convert.ToInt32(id), User.Data.id, User.Data.rp_name);
                    Notification.SendWithTime("~g~Предупреждение отправлено. +$" + money);
                };
            }

            if (Main.ServerName == "SunFlower")
            {
                menu.AddMenuItem(UiMenu, "Текущий онлайн", $"~b~Сейчас игроков:~s~ {players.Count()}\n~g~Enter~s~ - см. подробнее").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    TriggerServerEvent("ARP:SendPlayersList");
                };
            }
            else
                menu.AddMenuItem(UiMenu, "Текущий онлайн", $"~b~Сейчас игроков:~s~ {players.Count()}");

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        
        public static void ShowFractionMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Организация", "~b~Меню организации");

            switch (User.Data.fraction_id)
            {
                case 1:
                    menu.AddMenuItem(UiMenu, "Написать членам организации").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        var text = await Menu.GetUserInput("Текст...", null, 50);
                        if (text == "NULL") return;
                        Notification.SendPictureToFraction(text, $"Правительство [{Managers.Weather.Hour:D2}:{Managers.Weather.Min:D2}]", $"{User.Data.rp_name}", "CHAR_BANK_MAZE", Notification.TypeChatbox, 1);
                        Chat.SendMeCommand("отравляет сообщение по служебному телефону");
                    };
                    
                    menu.AddMenuItem(UiMenu, "Эвакуировать ближайший ТС").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Main.FindNearestVehicle().Delete();
                        Chat.SendMeCommand("говорит \"запрашиваю эвакуатор\" в рацию");
                    };
                    
                    /*if (User.Data.rank == 10 || User.Data.rank == 11 || User.Data.rank == 13 || User.Data.rank == 14)
                    {
                        menu.AddMenuItem(UiMenu, "Выдать лицензию адвоката").Activated += (uimenu, item) =>
                        {
                            ShowGovGiveLawLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
                            Chat.SendMeCommand("передает документ");
                        };
                        menu.AddMenuItem(UiMenu, "Выдать лицензию на бизнес").Activated += (uimenu, item) =>
                        {
                            ShowGovGiveBizzLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
                            Chat.SendMeCommand("передает документ");
                        };
                        menu.AddMenuItem(UiMenu, "Выдать лицензию на охоту").Activated += (uimenu, item) =>
                        {
                            ShowGovGiveAnimalLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
                            Chat.SendMeCommand("передает документ");
                        };
                        menu.AddMenuItem(UiMenu, "Выдать лицензию на рыбалку").Activated += (uimenu, item) =>
                        {
                            ShowGovGiveFishLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
                            Chat.SendMeCommand("передает документ");
                        };
                    }*/
                    
                    if (User.Data.rank > 8)
                    {/*
                        menu.AddMenuItem(UiMenu, "Пособие", $"Ставка: ~g~${Coffer.GetPosob()}").Activated += async (uimenu, item) =>
                        {
                            HideMenu();
                            Fractions.Government.SetPosob(Convert.ToInt32(await Menu.GetUserInput("Введите число", null, 3)));
                        };
                        menu.AddMenuItem(UiMenu, "Налог", $"Ставка: ~g~{Coffer.GetNalog()}%").Activated += async (uimenu, item) =>
                        {
                            HideMenu();
                            Fractions.Government.SetNalog(Convert.ToInt32(await Menu.GetUserInput("Введите число", null, 1)));
                        };
                        menu.AddMenuItem(UiMenu, "Налог на бизнес", $"Ставка: ~g~{Coffer.GetBizzNalog()}%").Activated += async (uimenu, item) =>
                        {
                            HideMenu();
                            Fractions.Government.SetNalogBusiness(Convert.ToInt32(await Menu.GetUserInput("Введите число", null, 1)));
                        };
                        menu.AddMenuItem(UiMenu, "Пенсия", $"Ставка: ~g~${Coffer.GetMoneyOld()}").Activated += async (uimenu, item) =>
                        {
                            HideMenu();
                            Fractions.Government.SetPension(Convert.ToInt32(await Menu.GetUserInput("Введите число", null, 3)));
                        };*/
                        
                        /*menu.AddMenuItem(UiMenu, "Положить деньги в казну").Activated += async (uimenu, item) =>
                        {
                            HideMenu();
                                
                            if (await Ctos.IsBlackout())
                            {
                                Notification.SendWithTime("~r~Банк во время блекаута не работает");
                                return;
                            }
                                
                            int number = Convert.ToInt32(await Menu.GetUserInput("Введите число", null, 10));

                            if (number > await User.GetCashMoney())
                            {
                                Notification.SendWithTime("~g~Нет деняк");
                                return;
                            }
                            
                            Coffer.AddMoney(number);
                            User.RemoveCashMoney(number);
                                
                            Notification.SendWithTime("~g~Вы положили деньги");
                            Main.SaveLog("CofferTakeLog", User.Data.rp_name + " - " + number);
                        };
                        
                        if (User.IsLeader())
                        {
                            menu.AddMenuItem(UiMenu, "Взять деньги").Activated += async (uimenu, item) =>
                            {
                                HideMenu();
                                
                                if (await Ctos.IsBlackout())
                                {
                                    Notification.SendWithTime("~r~Банк во время блекаута не работает");
                                    return;
                                }
                                
                                int number = Convert.ToInt32(await Menu.GetUserInput("Введите число", null, 10));
                                Coffer.RemoveMoney(number);
                                User.AddCashMoney(number);
                                
                                Notification.SendWithTime("~g~Вы взяли деньги");
                                Main.SaveLog("CofferTakeLog", User.Data.rp_name + " - " + number);
                            };
                        }*/
                    }
                    
                    //menu.AddMenuItem(UiMenu, "В казне:").SetRightLabel($"~g~${Coffer.GetMoney():#,#}");
                    
                    menu.AddMenuItem(UiMenu, "Локальные коды").Activated += (uimenu, item) =>
                    {
                        ShowTenCodeLocalList();
                    };
                    
                    menu.AddMenuItem(UiMenu, "Коды департамента").Activated += (uimenu, item) =>
                    {
                        ShowTenCodeDepList();
                    };
                    if (User.Data.rank == 9 || User.Data.rank == 12)
                    {
                          
                         menu.AddMenuItem(UiMenu, "Изъятие").Activated += (uimenu, item) =>
                    { 
                   var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f);
                  if (player == null)
                    {
                    Notification.SendWithTime("~r~Рядом с вами никого нет");
                    return;
                    }

                    ShowGovTakeList(player.ServerId);
                     };
                    }
                    
                    
                    if (User.Data.rank >= 8)
                    {
                        /*
                        menu.AddMenuItem(UiMenu, "~y~Написать новость").Activated += async (uimenu, item) =>
                        {
                            HideMenu();
                            var title = await Menu.GetUserInput("Заголовок", null, 15);
                            var text = await Menu.GetUserInput("Текст...", null, 50);
                            if (text == "NULL") return;
                            Notification.SendPictureToAll(text, "Новости правительства", title, "CHAR_BANK_MAZE", Notification.TypeChatbox);
                         };*/
                    }
                    break;
                case 2:

                    if (!User.IsDuty()) break;
                    
                    /*menu.AddMenuItem(UiMenu, "Эвакуировать ближайший ТС").Activated += async (uimenu, item) =>
                    {
                        var vehicle = Main.GetVehicleOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 3f);

                        if (vehicle == null)
                        {
                            Notification.SendWithTime("~r~Рядом с вами нет ТС");
                            return;
                        }
                        
                        RequestModel(Convert.ToUInt32(-140033735));
                        if (!HasModelLoaded(Convert.ToUInt32(-140033735)))
                        {
                            RequestModel(Convert.ToUInt32(-140033735));
                            await Delay(10);
                        }

                        var pedveh = CreatePedInsideVehicle(vehicle.Handle, 26, Convert.ToUInt32(-140033735), -1, true, true);
                        vehicle.IsEngineRunning = true;
                        vehicle.LockStatus = VehicleLockStatus.Locked;

                        var ped = new Ped(pedveh);
                        
                        SetVehicleEngineOn(vehicle.Handle, true, true, true);
                        TaskVehicleDriveToCoord(pedveh, vehicle.Handle, 818.2474f, -1333.713f, 25.41951f, 80.0f, 1, 0, 1074528293, 1, 1);
                        
                        await Delay(30000);
                        
                        vehicle.MarkAsNoLongerNeeded();
                        ped.MarkAsNoLongerNeeded();
                        
                        SetEntityAsMissionEntity(vehicle.Handle, false, false);
                        SetEntityAsMissionEntity(ped.Handle, false, false);

                        while (DoesEntityExist(vehicle.Handle))
                            await Delay(1000);

                        vehicle.Delete();
                        ped.Delete();	
                    };*/
                    
                    menu.AddMenuItem(UiMenu, "Диспетчерская").Activated += (uimenu, item) =>
                    {
                        ShowDispatcherList();
                    };
                    
                    menu.AddMenuItem(UiMenu, "Написать членам организации").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        var text = await Menu.GetUserInput("Текст...", null, 50);
                        if (text == "NULL") return;
                        Notification.SendPictureToFraction(text, $"SAPD [{Managers.Weather.Hour:D2}:{Managers.Weather.Min:D2}]", $"{User.Data.rp_name}", "WEB_LOSSANTOSPOLICEDEPT", Notification.TypeChatbox, 2);
                        Chat.SendMeCommand("отправляет сообщение по служебному телефону");
                    };
                    
                    menu.AddMenuItem(UiMenu, "Снять наручники").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1.5f);
                        if (player == null)
                        {
                            Notification.SendWithTime("~r~Рядом с вами никого нет");
                            return;
                        }

                        if (await Client.Sync.Data.Has(player.ServerId, "isCuff"))
                        {
                            User.PlayAnimation("mp_arresting", "a_uncuff", 8);
                            Shared.Cuff(player.ServerId);
                            Managers.Inventory.AddItemServer(40, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                            return;
                        }

                        Notification.SendWithTime("~y~Человек не в наручниках");
                    };
                    
                    menu.AddMenuItem(UiMenu, "Эвакуировать ближайший ТС").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Main.FindNearestVehicle().Delete();
                        Chat.SendMeCommand("говорит \"запрашиваю эвакуатор\" в рацию");
                    };
                    
                    menu.AddMenuItem(UiMenu, "Локальные коды").Activated += (uimenu, item) =>
                    {
                        ShowTenCodeLocalList();
                    };
                    
                    menu.AddMenuItem(UiMenu, "Коды департамента").Activated += (uimenu, item) =>
                    {
                        ShowTenCodeDepList();
                    };
                    
                    menu.AddMenuItem(UiMenu, "Поддержка").Activated += (uimenu, item) =>
                    {
                        ShowSupportUnitList();
                    };
                    /*menu.AddMenuItem(UiMenu, "Отправить авто на штраф строянку").Activated += (uimenu, item) =>
                    {
                        var veh = Main.FindNearestVehicle();
                        AutoPenaltyMenu(veh);
                    };*/
                    
                    menu.AddMenuItem(UiMenu, "Изъятие").Activated += (uimenu, item) =>
                    {
                        var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f);
                        if (player == null)
                        {
                            Notification.SendWithTime("~r~Рядом с вами никого нет");
                            return;
                        }
                        
                        ShowSapdTakeList(player.ServerId);
                    };
                    
                    menu.AddMenuItem(UiMenu, "Обыск игрока").Activated += async (uimenu, item) =>
                    {
                        var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f);
                        if (player == null)
                        {
                            Notification.SendWithTime("~r~Рядом с вами никого нет");
                            return;
                        }
                        
                        User.PlayScenario("CODE_HUMAN_MEDIC_KNEEL");
                        await Delay(5000);
                        
                        Managers.Inventory.GetItemList((int) await Sync.Data.Get(player.ServerId, "id"), InventoryTypes.Player);
                        
                        User.StopScenario();
                    };
                    
                    menu.AddMenuItem(UiMenu, "Выдать розыск").Activated += (uimenu, item) =>
                    {
                        ShowGiveWantedMenu();
                    };
                    
                    /*menu.AddMenuItem(UiMenu, "Выдать розыск транспорту").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        string number = await Menu.GetUserInput("Номер ТС", null, 10);
                        int count = Convert.ToInt32(await Menu.GetUserInput("Розыск", null, 1));
                        
                        if (count < 0)
                    };*/
                    
                    /*menu.AddMenuItem(UiMenu, "Выписать штраф").Activated += (uimenu, item) =>
                    {
                        ShowSapdGiveTicketMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
                    };*/
                    
                    /*if (User.Data.rank > 3)
                    {
                        menu.AddMenuItem(UiMenu, "Выдать лицензию на оружие").Activated += (uimenu, item) =>
                        {
                            ShowSapdGiveGunLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
                        };
                    }
                    if (User.Data.rank > 9)
                    {
                        menu.AddMenuItem(UiMenu, "Получить пароль").Activated += async (uimenu, item) =>
                        {
                            Notification.Send($"~g~Текущий пароль: ~s~{await Client.Sync.Data.Get(-9999, "sapdPass")}");
                        };
                    }
                    
                    if (User.IsLeader() || User.IsSubLeader())
                    {
                        menu.AddMenuItem(UiMenu, "~y~Написать новость").Activated += async (uimenu, item) =>
                        {
                            HideMenu();
                            var title = await Menu.GetUserInput("Заголовок", null, 15);
                            var text = await Menu.GetUserInput("Текст...", null, 50);
                            if (text == "NULL") return;
                            Notification.SendPictureToAll(text, "Новости SAPD", title, "WEB_LOSSANTOSPOLICEDEPT", Notification.TypeChatbox);
                        };
                        
                        menu.AddMenuItem(UiMenu, "~y~Лог на транспорт").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            TriggerServerEvent("ARP:SendPlayerVehicleLog");
                        };
                    }*/
                    
                    break;
                case 7:
                    
                    /*menu.AddMenuItem(UiMenu, "Эвакуировать ближайший ТС").Activated += async (uimenu, item) =>
                    {
                        var vehicle = Main.GetVehicleOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 3f);

                        if (vehicle == null)
                        {
                            Notification.SendWithTime("~r~Рядом с вами нет ТС");
                            return;
                        }
                        
                        RequestModel(Convert.ToUInt32(-140033735));
                        if (!HasModelLoaded(Convert.ToUInt32(-140033735)))
                        {
                            RequestModel(Convert.ToUInt32(-140033735));
                            await Delay(10);
                        }

                        var pedveh = CreatePedInsideVehicle(vehicle.Handle, 26, Convert.ToUInt32(-140033735), -1, true, true);
                        vehicle.IsEngineRunning = true;
                        vehicle.LockStatus = VehicleLockStatus.Locked;

                        var ped = new Ped(pedveh);
                        
                        SetVehicleEngineOn(vehicle.Handle, true, true, true);
                        TaskVehicleDriveToCoord(pedveh, vehicle.Handle, 818.2474f, -1333.713f, 25.41951f, 80.0f, 1, 0, 1074528293, 1, 1);
                        
                        await Delay(30000);
                        
                        vehicle.MarkAsNoLongerNeeded();
                        ped.MarkAsNoLongerNeeded();
                        
                        SetEntityAsMissionEntity(vehicle.Handle, false, false);
                        SetEntityAsMissionEntity(ped.Handle, false, false);

                        while (DoesEntityExist(vehicle.Handle))
                            await Delay(1000);

                        vehicle.Delete();
                        ped.Delete();	
                    };*/
                    
                    menu.AddMenuItem(UiMenu, "Диспетчерская").Activated += (uimenu, item) =>
                    {
                        ShowDispatcherList();
                    };
                    
                    menu.AddMenuItem(UiMenu, "Написать членам организации").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        var text = await Menu.GetUserInput("Текст...", null, 50);
                        if (text == "NULL") return;
                        Notification.SendPictureToFraction(text, $"Sheriff's Dept. [{Managers.Weather.Hour:D2}:{Managers.Weather.Min:D2}]", $"{User.Data.rp_name}", "WEB_LOSSANTOSPOLICEDEPT", Notification.TypeChatbox, 7);
                        Chat.SendMeCommand("отправляет сообщение по служебному телефону");
                    };
                    
                    menu.AddMenuItem(UiMenu, "Снять наручники").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1.5f);
                        if (player == null)
                        {
                            Notification.SendWithTime("~r~Рядом с вами никого нет");
                            return;
                        }

                        if (await Client.Sync.Data.Has(player.ServerId, "isCuff"))
                        {
                            User.PlayAnimation("mp_arresting", "a_uncuff", 8);
                            Shared.Cuff(player.ServerId);
                            Managers.Inventory.AddItemServer(40, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                            return;
                        }

                        Notification.SendWithTime("~y~Человек не в наручниках");
                    };
                    
                    menu.AddMenuItem(UiMenu, "Эвакуировать ближайший ТС").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Main.FindNearestVehicle().Delete();
                        Chat.SendMeCommand("говорит \"запрашиваю эвакуатор\" в рацию");
                    };
                    
                    menu.AddMenuItem(UiMenu, "Локальные коды").Activated += (uimenu, item) =>
                    {
                        ShowTenCodeLocalList();
                    };
                    
                    menu.AddMenuItem(UiMenu, "Коды департамента").Activated += (uimenu, item) =>
                    {
                        ShowTenCodeDepList();
                    };
                    
                    menu.AddMenuItem(UiMenu, "Поддержка").Activated += (uimenu, item) =>
                    {
                        ShowSupportUnitList();
                    };
                    
                    menu.AddMenuItem(UiMenu, "Изъятие").Activated += (uimenu, item) =>
                    {
                        var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f);
                        if (player == null)
                        {
                            Notification.SendWithTime("~r~Рядом с вами никого нет");
                            return;
                        }
                        
                        ShowSapdTakeList(player.ServerId);
                    };
                    
                    menu.AddMenuItem(UiMenu, "Обыск игрока").Activated += async (uimenu, item) =>
                    {
                        var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f);
                        if (player == null)
                        {
                            Notification.SendWithTime("~r~Рядом с вами никого нет");
                            return;
                        }
                        
                        User.PlayScenario("CODE_HUMAN_MEDIC_KNEEL");
                        await Delay(5000);
                        
                        Managers.Inventory.GetItemList((int) await Sync.Data.Get(player.ServerId, "id"), InventoryTypes.Player);
                        
                        User.StopScenario();
                    };
                    
                    menu.AddMenuItem(UiMenu, "Выдать розыск").Activated += (uimenu, item) =>
                    {
                        ShowGiveWantedMenu();
                    };
                    
                    /*menu.AddMenuItem(UiMenu, "Выдать розыск транспорту").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        string number = await Menu.GetUserInput("Номер ТС", null, 10);
                        int count = Convert.ToInt32(await Menu.GetUserInput("Розыск", null, 1));
                        
                        if (count < 0)
                    };*/
                    
                    /*menu.AddMenuItem(UiMenu, "Выписать штраф").Activated += (uimenu, item) =>
                    {
                        ShowSapdGiveTicketMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
                    };*/
                    
                    /*if (User.Data.rank > 3)
                    {
                        menu.AddMenuItem(UiMenu, "Выдать лицензию на оружие").Activated += (uimenu, item) =>
                        {
                            ShowSapdGiveGunLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
                        };
                    } 
                    if (User.Data.rank > 6)
                    {
                        menu.AddMenuItem(UiMenu, "Получить пароль").Activated += async (uimenu, item) =>
                        {
                            Notification.Send($"~g~Текущий пароль: ~s~{await Client.Sync.Data.Get(-9999, "sapdPass")}");
                        };
                    }
                    
                    if (User.IsLeader() || User.IsSubLeader())
                    {
                        menu.AddMenuItem(UiMenu, "~y~Написать новость").Activated += async (uimenu, item) =>
                        {
                            HideMenu();
                            var title = await Menu.GetUserInput("Заголовок", null, 15);
                            var text = await Menu.GetUserInput("Текст...", null, 50);
                            if (text == "NULL") return;
                            Notification.SendPictureToAll(text, "Новости Sheriff's Dept.", title, "WEB_LOSSANTOSPOLICEDEPT", Notification.TypeChatbox);
                        };
                        
                        /*menu.AddMenuItem(UiMenu, "~y~Лог на транспорт").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            TriggerServerEvent("ARP:SendPlayerVehicleLog");
                        };
                    }

                    if (User.Data.rank > 7)
                    {
                        menu.AddMenuItem(UiMenu, "~y~Лог на транспорт").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            TriggerServerEvent("ARP:SendPlayerVehicleLog");
                        };
                    }*/
                    
                    break;
                case 16:

                    if (!User.IsDuty()) break;
                    
                    menu.AddMenuItem(UiMenu, "Диспетчерская").Activated += (uimenu, item) =>
                    {
                        ShowDispatcherList();
                    };
                    
                    menu.AddMenuItem(UiMenu, "Написать членам организации").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        var text = await Menu.GetUserInput("Текст...", null, 50);
                        if (text == "NULL") return;
                        Notification.SendPictureToFraction(text, $"EMS [{Managers.Weather.Hour:D2}:{Managers.Weather.Min:D2}]", $"{User.Data.rp_name}", "CHAR_CALL911", Notification.TypeChatbox, 16);
                        Chat.SendMeCommand("отправляет сообщение по служебному телефону");
                    };
                    
                    menu.AddMenuItem(UiMenu, "Эвакуировать ближайший ТС").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Main.FindNearestVehicle().Delete();
                        Chat.SendMeCommand("говорит \"запрашиваю эвакуатор\" в рацию");
                    };
                    
                    menu.AddMenuItem(UiMenu, "Эвакуировать ближайший труп").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                
                        foreach (CitizenFX.Core.Ped p in Main.GetPedListOnRadius(3f))
                        {
                            p.Delete();
                            return;
                        }
                    };
                    
                    menu.AddMenuItem(UiMenu, "Госпитализировать").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        ShowGoHospMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
                    };
                    menu.AddMenuItem(UiMenu, "Использовать Дефибриллятор").Activated += (uimenu, item) => 
                    {
                        HideMenu();
                        //foreach (CitizenFX.Core.Player p in Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), Convert.ToInt32(1)))
                          //  Shared.TriggerEventToPlayer(p.ServerId, "ARP:UseAdrenalin");
                          var pPos = GetEntityCoords(GetPlayerPed(-1), true);
                          var player = Main.GetPlayerOnRadius(pPos, 1.2f);
                          if (player == null)
                          {
                              Notification.SendWithTime("~r~Рядом с вами никого нет");
                              return;
                          }
                          Main.SaveLog("GangBang", $"[ANDRENALINE] {User.Data.rp_name} - {User.PlayerIdList[player.ServerId.ToString()]} | {pPos.X} {pPos.Y} {pPos.Z}");
                          Shared.TriggerEventToPlayer(player.ServerId, "ARP:UseDef");
                              //Chat.SendMeCommand("использовал дефибриллятор");
                    };
                    menu.AddMenuItem(UiMenu, "Использовать набор первой помощи").Activated += (uimenu, item) => 
                    {
                        HideMenu();
                        //foreach (CitizenFX.Core.Player p in Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), Convert.ToInt32(1)))
                        //  Shared.TriggerEventToPlayer(p.ServerId, "ARP:UseAdrenalin");
                        var pPos = GetEntityCoords(GetPlayerPed(-1), true);
                        var player = Main.GetPlayerOnRadius(pPos, 1.2f);
                        if (player == null)
                        {
                            Notification.SendWithTime("~r~Рядом с вами никого нет");
                            return;
                        }
                        

                            Shared.TriggerEventToPlayer(player.ServerId, "ARP:UseFirstAidKit");
                            //Chat.SendMeCommand("использовал набор первой помощи");
                    };
                    
                    
                    menu.AddMenuItem(UiMenu, "Локальные коды").Activated += (uimenu, item) =>
                    {
                        ShowTenCodeLocalList();
                    };
                    
                    menu.AddMenuItem(UiMenu, "Коды департамента").Activated += (uimenu, item) =>
                    {
                        ShowTenCodeDepList();
                    };
                    
                        /*if (User.Data.rank > 8)
                    {
                        menu.AddMenuItem(UiMenu, "Выдать рецепт марихуаны", "~y~Запрещено выдавать его платно!").Activated += (uimenu, item) =>
                        {
                            ShowGiveMargLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
                        };
                        
                        menu.AddMenuItem(UiMenu, "Выдать мед. страховку").Activated += (uimenu, item) =>
                        {
                            ShowGiveMedLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
                        };
                        menu.AddMenuItem(UiMenu, "Выдать справку о мед").Activated += (uimenu, item) =>
                        {
                            ShowEmsGivePsyLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
                        };
                    }*/
                    
                    /*if (User.IsLeader() || User.IsSubLeader())
                    {
                        menu.AddMenuItem(UiMenu, "~y~Написать новость").Activated += async (uimenu, item) =>
                        {
                            HideMenu();
                            var title = await Menu.GetUserInput("Заголовок", null, 15);
                            var text = await Menu.GetUserInput("Текст...", null, 50);
                            if (text == "NULL") return;
                            Notification.SendPictureToAll(text, "Новости EMS", title, "CHAR_CALL911", Notification.TypeChatbox);
                        };
                        
                        menu.AddMenuItem(UiMenu, "~y~Лог на транспорт").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            TriggerServerEvent("ARP:SendPlayerVehicleLog");
                        };
                    }*/
                    
                    break;
                case 3:

                    if (!User.IsDuty()) break;
                    
                    menu.AddMenuItem(UiMenu, "Диспетчерская").Activated += (uimenu, item) =>
                    {
                        ShowDispatcherList();
                    };
                    
                    menu.AddMenuItem(UiMenu, "Написать членам организации").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        var text = await Menu.GetUserInput("Текст...", null, 50);
                        if (text == "NULL") return;
                        Notification.SendPictureToFraction(text, $"FIB [{Managers.Weather.Hour:D2}:{Managers.Weather.Min:D2}]", $"{User.Data.rp_name}", "DIA_TANNOY", Notification.TypeChatbox, 3);
                        Chat.SendMeCommand("отправляет сообщение по служебному телефону");
                    };
                    menu.AddMenuItem(UiMenu, "Снять наручники").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1.5f);
                        if (player == null)
                        {
                            Notification.SendWithTime("~r~Рядом с вами никого нет");
                            return;
                        }

                        if (await Client.Sync.Data.Has(player.ServerId, "isCuff"))
                        {
                            User.PlayAnimation("mp_arresting", "a_uncuff", 8);
                            Shared.Cuff(player.ServerId);
                            Managers.Inventory.AddItemServer(40, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                            return;
                        }

                        Notification.SendWithTime("~y~Человек не в наручниках");
                    };
                    
                    menu.AddMenuItem(UiMenu, "Локальные коды").Activated += (uimenu, item) =>
                    {
                        ShowTenCodeLocalList();
                    };
                    
                    menu.AddMenuItem(UiMenu, "Коды департамента").Activated += (uimenu, item) =>
                    {
                        ShowTenCodeDepList();
                    };
                    
                    menu.AddMenuItem(UiMenu, "Изъятие").Activated += (uimenu, item) =>
                    {
                        var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f);
                        if (player == null)
                        {
                            Notification.SendWithTime("~r~Рядом с вами никого нет");
                            return;
                        }
                        
                        ShowSapdTakeList(player.ServerId);
                    };
                    
                    menu.AddMenuItem(UiMenu, "Обыск игрока").Activated += async (uimenu, item) =>
                    {
                        var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f);
                        if (player == null)
                        {
                            Notification.SendWithTime("~r~Рядом с вами никого нет");
                            return;
                        }
                        
                        User.PlayScenario("CODE_HUMAN_MEDIC_KNEEL");
                        await Delay(5000);
                        
                        Managers.Inventory.GetItemList((int) await Sync.Data.Get(player.ServerId, "id"), InventoryTypes.Player);
                        
                        User.StopScenario();
                    };
                    
                    menu.AddMenuItem(UiMenu, "Выдать розыск").Activated += (uimenu, item) =>
                    {
                        ShowGiveWantedMenu();
                    };
                    
                    if (User.IsLeader() || User.IsSubLeader())
                    {
                        menu.AddMenuItem(UiMenu, "~y~Написать новость").Activated += async (uimenu, item) =>
                        {
                            HideMenu();
                            var title = await Menu.GetUserInput("Заголовок", null, 15);
                            var text = await Menu.GetUserInput("Текст...", null, 50);
                            if (text == "NULL") return;
                            Notification.SendPictureToAll(text, "Новости FIB", title, "DIA_TANNOY", Notification.TypeChatbox);
                        };
                        
                        menu.AddMenuItem(UiMenu, "~y~Лог на транспорт").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            TriggerServerEvent("ARP:SendPlayerVehicleLog");
                        };
                    }
                    
                    break;
                case 8:
                    if (User.Data.rank > 3)
                    {
                        menu.AddMenuItem(UiMenu, "Заказать фургон с оружием", "Стоимость: ~g~$15000").Activated += (uimenu, index) =>
                        {
                            if (Managers.Weather.Hour < 22 && Managers.Weather.Hour > 6)
                            {
                                Notification.SendWithTime("~r~Можно заказать с 22 до 6 утра.");
                                return;
                            }
                            if (Sync.Data.HasLocally(User.GetServerId(), "cartel:block"))
                            {
                                Notification.SendWithTime("~r~Вы уже заказывали фургон");
                                return;
                            }
                            if (User.Data.money < 15000)
                            {
                                Notification.SendWithTime("~r~У Вас нет столько налички");
                                return;
                            }
                            User.RemoveCashMoney(15000);
                            
                            var rand = new Random();
                            int randCp = rand.Next(Main.CartelGetCarStockCpPos.Length / 3);
                            
                            var cpPos = new Vector3((float) Main.CartelGetCarStockCpPos[randCp, 0], (float) Main.CartelGetCarStockCpPos[randCp, 1], (float) Main.CartelGetCarStockCpPos[randCp, 2]);
    
                            User.SetWaypoint(cpPos.X, cpPos.Y);
                            Managers.Checkpoint.CreateWithMarker(cpPos, 1f, 1f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "cartel:car:gun:" + randCp);
                            
                            Sync.Data.SetLocally(User.GetServerId(), "cartel:block", true);
                            Sync.Data.SetLocally(User.GetServerId(), "cartel:car:gun:" + randCp, true);
                            
                            Main.SaveLog("Cartel", $"[GET_CAR_GUN] {User.Data.rp_name}");
                        };
                        
                        menu.AddMenuItem(UiMenu, "Заказать фургон с тяжелыми наркотиками", "Стоимость: ~g~$30000").Activated += (uimenu, index) =>
                        {
                            if (Managers.Weather.Hour < 22 && Managers.Weather.Hour > 6)
                            {
                                Notification.SendWithTime("~r~Можно заказать с 22 до 6 утра.");
                                return;
                            }
                            if (Sync.Data.HasLocally(User.GetServerId(), "cartel:block"))
                            {
                                Notification.SendWithTime("~r~Вы уже заказывали фургон");
                                return;
                            }
                            if (User.Data.money < 30000)
                            {
                                Notification.SendWithTime("~r~У Вас нет столько налички");
                                return;
                            }
                            User.RemoveCashMoney(30000);
                            
                            var rand = new Random();
                            int randCp = rand.Next(Main.CartelGetCarStockCpPos.Length / 3);
                            
                            var cpPos = new Vector3((float) Main.CartelGetCarStockCpPos[randCp, 0], (float) Main.CartelGetCarStockCpPos[randCp, 1], (float) Main.CartelGetCarStockCpPos[randCp, 2]);
                            
                            Managers.Checkpoint.CreateWithMarker(cpPos, 1f, 1f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "cartel:car:drug:" + randCp);
                            User.SetWaypoint(cpPos.X, cpPos.Y);
                            
                            Sync.Data.SetLocally(User.GetServerId(), "cartel:block", true);
                            Sync.Data.SetLocally(User.GetServerId(), "cartel:car:drug:" + randCp, true);
                            
                            Main.SaveLog("Cartel", $"[GET_CAR_DRUG] {User.Data.rp_name}");
                        };
                        
                        menu.AddMenuItem(UiMenu, "Заказать фургон с марихуаной", "Стоимость: ~g~$8000").Activated += (uimenu, index) =>
                        {
                            if (Managers.Weather.Hour < 22 && Managers.Weather.Hour > 6)
                            {
                                Notification.SendWithTime("~r~Можно заказать с 22 до 6 утра.");
                                return;
                            }
                            if (Sync.Data.HasLocally(User.GetServerId(), "cartel:block"))
                            {
                                Notification.SendWithTime("~r~Вы уже заказывали фургон");
                                return;
                            }
                            if (User.Data.money < 8000)
                            {
                                Notification.SendWithTime("~r~У Вас нет столько налички");
                                return;
                            }
                            
                            User.RemoveCashMoney(8000);
                            
                            var rand = new Random();
                            int randCp = rand.Next(Main.CartelGetCarStockCpPos.Length / 3);
                            
                            var cpPos = new Vector3((float) Main.CartelGetCarStockCpPos[randCp, 0], (float) Main.CartelGetCarStockCpPos[randCp, 1], (float) Main.CartelGetCarStockCpPos[randCp, 2]);
                            
                            Managers.Checkpoint.CreateWithMarker(cpPos, 1f, 1f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "cartel:car:drugm:" + randCp);
                            User.SetWaypoint(cpPos.X, cpPos.Y);
                            
                            Sync.Data.SetLocally(User.GetServerId(), "cartel:block", true);
                            Sync.Data.SetLocally(User.GetServerId(), "cartel:car:drugm:" + randCp, true);
                            
                            Main.SaveLog("Cartel", $"[GET_CAR_MARG] {User.Data.rp_name}");
                        };
                    }
                    
                    if (User.TimerAbduction == 0)
                    {
                        menu.AddMenuItem(UiMenu, "~y~Совершить сделку").Activated += async (uimenu, item) =>
                        {
                            var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 2f);
                            if (player == null)
                            {
                                Notification.SendWithTime("~r~Рядом с вами никого нет");
                                return;
                            }

                            if (!await Sync.Data.Has(player.ServerId, "isTie"))
                            {
                                Notification.SendWithTime("~y~Игрок не связан");
                                return;
                            }
                            
                            for (int i = 0; i < Main.Shops.Length / 4; i++)
                            {
                                if (Main.GetDistanceToSquared(GetEntityCoords(GetPlayerPed(-1), true), new Vector3((float) Main.CartelMiserkPos[i, 0], (float) Main.CartelMiserkPos[i, 1], (float) Main.CartelMiserkPos[i, 2])) < 10f)
                                {
                            
                                    Main.SaveLog("Cartel", $"[SELL_PLAYER] {User.Data.rp_name} - {User.PlayerIdList[player.ServerId.ToString()]}");
                                    User.TimerAbduction = -1;
                                    Shared.TriggerEventToPlayer(player.ServerId, "ARP:SellPlayer");
                                    Notification.SendWithTime("~y~Вы продали игрока\n~s~Теперь вам надо отмыть деньги");
                                    Sync.Data.SetLocally(User.GetServerId(), "GrabCash", (int) await Client.Sync.Data.Get(player.ServerId, "money") / User.Bonus);
                                    return;
                                }
                            }
                        };
                    }
                    
                    if (User.IsLeader() || User.IsSubLeader())
                    {
                        menu.AddMenuItem(UiMenu, "~y~Лог на транспорт").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            TriggerServerEvent("ARP:SendPlayerVehicleLog");
                        };
                       }
                    break;
                case 11:
                case 14:

                    menu.AddMenuItem(UiMenu, "Получить задание на угон").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Grab.GetGrabRandomVehicle();
                    };

                    /*menu.AddMenuItem(UiMenu, "Поддержка").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        ShowSupportUnitGangList();
                    };*/

                    /*if (User.Data.rank > 5)
                    {
                        menu.AddMenuItem(UiMenu, "Выдать рецепт марихуаны").Activated += (uimenu, item) =>
                        {
                            ShowGiveMargLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
                        };
                    }*/
                    break;
            }

            if (User.IsLeader() || User.IsSubLeader())
            {
                menu.AddMenuItem(UiMenu, "~y~Лог").Activated += (uimenu, item) =>
                {
                    //ShowFractionMemberListMenu();
                    HideMenu();
                    TriggerServerEvent("ARP:SendPlayerGunLog");
                };
            }

            if (User.IsLeader() || User.IsSubLeader())
            {

            menu.AddMenuItem(UiMenu, "~g~Принять в организацию").Activated += (uimenu, item) =>
                    {
                        ShowFractionMemberInviteMenu(
                            Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
                    };
                
            }
            
            menu.AddMenuItem(UiMenu, "Установить тег").Activated += async (uimenu, item) =>
            {
                //ShowFractionMemberListMenu();
                HideMenu();
                var text = await Menu.GetUserInput("Тег", null, 16);
                if (text == "NULL")
                {
                    User.Data.tag = "";
                    Sync.Data.Set(User.GetServerId(), "tag", "");
                    Notification.SendWithTime("~y~Вы удалили тег");
                    return;
                }
                
                User.Data.tag = text;
                Sync.Data.Set(User.GetServerId(), "tag", text);
                Notification.SendWithTime("~y~Вы установили тег - " + text);
            };
            
            menu.AddMenuItem(UiMenu, "Список членов организации").Activated += (uimenu, item) =>
            {
                //ShowFractionMemberListMenu();
                HideMenu();
                TriggerServerEvent("ARP:SendPlayerMembers");
            };
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowMainMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowFraction2Menu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Организация", "~b~Меню организации");
            
            menu.AddMenuItem(UiMenu, "Получить задание на угон").Activated += (uimenu, item) =>
            {
                HideMenu();
                Grab.GetGrabRandomVehicle();
            };
            
            /*menu.AddMenuItem(UiMenu, "Запросить спец. фургон").Activated += (uimenu, item) =>
            {
                if (Managers.Weather.Hour < 22 && Managers.Weather.Hour > 6)
                {
                    Notification.SendWithTime("~r~Можно заказать с 22 до 6 утра.");
                    return;
                }
                if (Sync.Data.HasLocally(User.GetServerId(), "cartel:block"))
                {
                    Notification.SendWithTime("~r~Вы уже заказывали фургон");
                    return;
                }
                if (User.Data.money < 1000)
                {
                    Notification.SendWithTime("~r~У Вас нет столько налички");
                    return;
                }
                            
                User.RemoveCashMoney(1000);
                            
                var rand = new Random();
                int randCp = rand.Next(Main.CartelGetCarStockCpPos.Length / 3);
                            
                var cpPos = new Vector3((float) Main.CartelGetCarStockCpPos[randCp, 0], (float) Main.CartelGetCarStockCpPos[randCp, 1], (float) Main.CartelGetCarStockCpPos[randCp, 2]);
                            
                Managers.Checkpoint.CreateWithMarker(cpPos, 1f, 1f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "cartel:car:drugm:" + randCp);
                User.SetWaypoint(cpPos.X, cpPos.Y);
                            
                Sync.Data.SetLocally(User.GetServerId(), "cartel:block", true);
                Sync.Data.SetLocally(User.GetServerId(), "cartel:car:drugm:" + randCp, true);
            };*/

            if (User.IsLeader2() || User.IsSubLeader2())
            {
                menu.AddMenuItem(UiMenu, "~g~Принять в организацию").Activated += (uimenu, item) =>
                {
                    ShowFraction2MemberInviteMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
                };
            }
            
            menu.AddMenuItem(UiMenu, "Установить тег").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var text = await Menu.GetUserInput("Тег", null, 16);
                if (text == "NULL")
                {
                    User.Data.tag = "";
                    Sync.Data.Set(User.GetServerId(), "tag", "");
                    Notification.SendWithTime("~y~Вы удалили тег");
                    return;
                }
                
                User.Data.tag = text;
                Sync.Data.Set(User.GetServerId(), "tag", text);
                Notification.SendWithTime("~y~Вы установили тег - " + text);
            };
            
            menu.AddMenuItem(UiMenu, "Список членов организации").Activated += (uimenu, item) =>
            {
                HideMenu();
                TriggerServerEvent("ARP:SendPlayerMembers2");
            };
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowMainMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowSupportUnitList()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Поддержка", "~b~Юниты поддержки");
            
            var list = new List<dynamic>{"Code 0", "Code 1", "Code 2", "Code 3"};
            
            menu.AddMenuItemList(UiMenu, "Стандартный патруль", list).OnListSelected += (uimenu, idx) =>
            {
                HideMenu();
                PedAi.SendCode(idx, true, 600, UnitTypes.Standart);
                Chat.SendMeCommand("говорит \"запрашиваю поддержку\" в рацию");
            };
            menu.AddMenuItemList(UiMenu, "Хайвей патруль", list).OnListSelected += (uimenu, idx) =>
            {
                HideMenu();
                PedAi.SendCode(idx, true, 600, UnitTypes.HightWay);
                Chat.SendMeCommand("говорит \"запрашиваю поддержку\" в рацию");
            };
            menu.AddMenuItemList(UiMenu, "Детективы", list).OnListSelected += (uimenu, idx) =>
            {
                HideMenu();
                PedAi.SendCode(idx, true, 600, UnitTypes.Detective);
                Chat.SendMeCommand("говорит \"запрашиваю поддержку\" в рацию");
            };
            menu.AddMenuItemList(UiMenu, "FIB", list).OnListSelected += (uimenu, idx) =>
            {
                HideMenu();
                PedAi.SendCode(idx, true, 600, UnitTypes.Fib);
                Chat.SendMeCommand("говорит \"запрашиваю поддержку\" в рацию");
            };
            menu.AddMenuItemList(UiMenu, "SWAT", list).OnListSelected += (uimenu, idx) =>
            {
                HideMenu();
                PedAi.SendCode(idx, true, 600, UnitTypes.Swat);
                Chat.SendMeCommand("говорит \"запрашиваю поддержку\" в рацию");
            };
            
            menu.AddMenuItem(UiMenu, "Medic Unit").Activated += (uimenu, item) =>
            {
                HideMenu();
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                PedAi.Add(pos.X, pos.Y, pos.Z, UnitTypes.Medic, 600);
                Notification.SendPicture("Поддержка в пути", "Диспетчер", "Медики", "CHAR_CALL911", Notification.TypeChatbox);
                Chat.SendMeCommand("говорит \"запрашиваю поддержку\" в рацию");
            };
            
            menu.AddMenuItem(UiMenu, "Код 4", "Помощь не требуется.\nВсе спокойно").Activated += (uimenu, item) =>
            {
                HideMenu();
                PedAi.SendCode(4);
                Chat.SendMeCommand("говорит \"помощь не требуется\" в рацию");
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowSupportUnitGangList()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Поддержка", "~b~Юниты поддержки");

            int unit = 0;
            switch (User.Data.fraction_id)
            {
                case 11:
                    unit = UnitTypes.Ballas;
                    break;
                case 14:
                    unit = UnitTypes.Mara;
                    break;
            }
            
            menu.AddMenuItem(UiMenu, "Легкая поддержка").Activated += (uimenu, item) =>
            {
                HideMenu();
                PedAi.SendGangUnit(0, 600, unit);
            };
            menu.AddMenuItem(UiMenu, "Тяжелая поддержка").Activated += (uimenu, item) =>
            {
                HideMenu();
                PedAi.SendGangUnit(1, 600, unit);
            };
            
            menu.AddMenuItem(UiMenu, "Помощь не требуется").Activated += (uimenu, item) =>
            {
                HideMenu();
                PedAi.SendGangUnit(4);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowTenCodeLocalList()
        {
            HideMenu();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Связь во время блекаута не работает");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Коды", "~b~Локальные коды");
            
            menu.AddMenuItem(UiMenu, "Код 0", "Необходима немедленная поддержка").Activated += (uimenu, item) =>
            {
                HideMenu();
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                Notification.SendPictureToFraction($"{User.Data.rp_name} - запрашивает поддержку", "Диспетчер", "Код 0", "CHAR_CALL911", Notification.TypeChatbox, User.Data.fraction_id);
                Shared.SetWaypointToFraction(User.Data.fraction_id, pos.X, pos.Y);
                Chat.SendMeCommand("говорит \"Код 0\" в рацию");
            };
            
            menu.AddMenuItem(UiMenu, "Код 1", "Офицер в бедственном положении").Activated += (uimenu, item) =>
            {
                HideMenu();
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                Notification.SendPictureToFraction($"{User.Data.rp_name} - в бедственном положении", "Диспетчер", "Код 1", "CHAR_CALL911", Notification.TypeChatbox, User.Data.fraction_id);
                Shared.SetWaypointToFraction(User.Data.fraction_id, pos.X, pos.Y);
                Chat.SendMeCommand("говорит \"Код 1\" в рацию");

            };
            
            menu.AddMenuItem(UiMenu, "Код 2", "Приоритетный вызов\n(без сирен/стробоскопов)").Activated += (uimenu, item) =>
            {
                HideMenu();
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                Notification.SendPictureToFraction($"{User.Data.rp_name} - запрашивает поддержку", "Диспетчер", "Код 2", "CHAR_CALL911", Notification.TypeChatbox, User.Data.fraction_id);
                Shared.SetWaypointToFraction(User.Data.fraction_id, pos.X, pos.Y);
                Chat.SendMeCommand("говорит \"Код 2\" в рацию");

            };
            
            menu.AddMenuItem(UiMenu, "Код 3", "Срочный вызов\n(сирены, стробоскопы)").Activated += (uimenu, item) =>
            {
                HideMenu();
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                Notification.SendPictureToFraction($"{User.Data.rp_name} - запрашивает поддержку", "Диспетчер", "Код 3", "CHAR_CALL911", Notification.TypeChatbox, User.Data.fraction_id);
                Shared.SetWaypointToFraction(User.Data.fraction_id, pos.X, pos.Y);
                Chat.SendMeCommand("говорит \"Код 3\" в рацию");

            };
            
            menu.AddMenuItem(UiMenu, "Код 4", "Помощь не требуется.\nВсе спокойно").Activated += (uimenu, item) =>
            {
                HideMenu();
                Notification.SendPictureToFraction($"{User.Data.rp_name} - все спокойно", "Диспетчер", "Код 4", "CHAR_CALL911", Notification.TypeChatbox, User.Data.fraction_id);
                Chat.SendMeCommand("говорит \"Код 4\" в рацию");

            };
            
            menu.AddMenuItem(UiMenu, "Код 6", "Задерживаюсь на месте").Activated += (uimenu, item) =>
            {
                HideMenu();
                Notification.SendPictureToFraction($"{User.Data.rp_name} задерживается на месте", "Диспетчер", "Код 6", "CHAR_CALL911", Notification.TypeChatbox, User.Data.fraction_id);
                Chat.SendMeCommand("говорит \"Код 6\" в рацию");
            };
            
            menu.AddMenuItem(UiMenu, "Код 7", "Перерыв на обед").Activated += (uimenu, item) =>
            {
                HideMenu();
                Notification.SendPictureToFraction($"{User.Data.rp_name} вышел на обед", "Диспетчер", "Код 7", "CHAR_CALL911", Notification.TypeChatbox, User.Data.fraction_id);
                Chat.SendMeCommand("говорит \"Код 7\" в рацию");
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowTenCodeDepList()
        {
            HideMenu();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Связь во время блекаута не работает");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Коды", "~b~Коды департамента");
            
            menu.AddMenuItem(UiMenu, "Код 0", "Необходима немедленная поддержка").Activated += (uimenu, item) =>
            {
                HideMenu();
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                Notification.SendPictureToDep($"{User.Data.rp_name} - запрашивает поддержку", "Диспетчер", "Код 0", "CHAR_CALL911", Notification.TypeChatbox);
                Shared.SetWaypointToDep(pos.X, pos.Y);
                Chat.SendMeCommand("говорит \"Код 0\" в рацию");
            };
            
            menu.AddMenuItem(UiMenu, "Код 1", "Офицер в бедственном положении").Activated += (uimenu, item) =>
            {
                HideMenu();
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                Notification.SendPictureToDep($"{User.Data.rp_name} - в бедственном положении", "Диспетчер", "Код 1", "CHAR_CALL911", Notification.TypeChatbox);
                Shared.SetWaypointToDep(pos.X, pos.Y);
                Chat.SendMeCommand("говорит \"Код 1\" в рацию");
            };
            
            menu.AddMenuItem(UiMenu, "Код 2", "Приоритетный вызов\n(без сирен/стробоскопов)").Activated += (uimenu, item) =>
            {
                HideMenu();
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                Notification.SendPictureToDep($"{User.Data.rp_name} - запрашивает поддержку", "Диспетчер", "Код 2", "CHAR_CALL911", Notification.TypeChatbox);
                Shared.SetWaypointToDep(pos.X, pos.Y);
                Chat.SendMeCommand("говорит \"Код 2\" в рацию");
            };
            
            menu.AddMenuItem(UiMenu, "Код 3", "Срочный вызов\n(сирены, стробоскопы)").Activated += (uimenu, item) =>
            {
                HideMenu();
                var pos = GetEntityCoords(GetPlayerPed(-1), true);
                Notification.SendPictureToDep($"{User.Data.rp_name} - запрашивает поддержку", "Диспетчер", "Код 3", "CHAR_CALL911", Notification.TypeChatbox);
                Shared.SetWaypointToDep(pos.X, pos.Y);
                Chat.SendMeCommand("говорит \"Код 3\" в рацию");
            };
            
            menu.AddMenuItem(UiMenu, "Код 4", "Помощь не требуется.\nВсе спокойно").Activated += (uimenu, item) =>
            {
                HideMenu();
                Notification.SendPictureToDep($"{User.Data.rp_name} - все спокойно", "Диспетчер", "Код 4", "CHAR_CALL911", Notification.TypeChatbox);
                Chat.SendMeCommand("говорит \"Код 4\" в рацию");
            };
            
            menu.AddMenuItem(UiMenu, "Код 6", "Задерживаюсь на месте").Activated += (uimenu, item) =>
            {
                HideMenu();
                Notification.SendPictureToDep($"{User.Data.rp_name} задерживается на месте", "Диспетчер", "Код 6", "CHAR_CALL911", Notification.TypeChatbox);
                Chat.SendMeCommand("говорит \"Код 6\" в рацию");
            };
            
            menu.AddMenuItem(UiMenu, "Код 7", "Перерыв на обед").Activated += (uimenu, item) =>
            {
                HideMenu();
                Notification.SendPictureToDep($"{User.Data.rp_name} вышел на обед", "Диспетчер", "Код 7", "CHAR_CALL911", Notification.TypeChatbox);
                Chat.SendMeCommand("говорит \"Код 7\" в рацию");
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowDispatcherList()
        {
            HideMenu();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Связь во время блекаута не работает");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Диспетчер", "~b~Нажмите ~g~Enter~b~ чтобы принять вызов");

            var list = Dispatcher.EmsList;
            list.Reverse();

            int i = 1;
            foreach (var itemList in list)
            {
                menu.AddMenuItem(UiMenu, $"~b~{i}.~s~ {itemList.Title} [{itemList.Time}]", $"Район: {itemList.Street1}").Activated += (uimenu, item) =>
                {
                    HideMenu();

                    string rank = User.IsEms() ? "Сотрудник EMS" : "Офицер";
                    
                    if (itemList.WithCoord)
                    {
                        Dispatcher.SendNotification("10-4 - 911", $"{rank} {User.Data.rp_name} принял вызов \"{itemList.Title}\"", $"~y~Детали: ~s~{itemList.Desc}", $"~y~Район: ~s~{itemList.Street1}");
                        Chat.SendMeCommand("говорит \"принимаю вызов\" в рацию");
                        User.SetWaypoint(itemList.X, itemList.Y);
                        
                        int length = itemList.Title.Split('-').Length;
                        if (length == 2)
                            Shared.TriggerEventToAllPlayers("ARP:AcceptDispatch", itemList.Title);
                    }
                    else
                        Dispatcher.SendNotification("10-4 - 911", $"{rank} {User.Data.rp_name} принял вызов \"{itemList.Title}\"", $"~y~Детали: ~s~{itemList.Desc}");
                };
                i++;
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowHackerServerFilesList(string ip)
        {
            HideMenu();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Связь во время блекаута не работает");
                return;
            }
            
            User.PlayPhoneAnimation();
            
            Screen.LoadingPrompt.Show("Loading...");
            
            var menu = new Menu();
            UiMenu = menu.Create(ip, "~b~File list");

            var data = await Client.Sync.Data.GetAll(GetHashKey(ip));
            if (data == null)
            {
                Notification.Send("~r~System error, try again");
                return;
            }

            int i = 1;
            foreach (var itemList in (IDictionary<String, Object>) data)
            {
                var listDo = new List<dynamic> {"Delete"};
                await Delay(100);
                    
                menu.AddMenuItemList(UiMenu, $"~b~{i}. ~s~{itemList.Key}", listDo).OnListSelected += (uimenu, idx) =>
                {
                    HideMenu();
                    User.StopScenario();

                    if (idx == 0)
                    {
                        Sync.Data.Reset(GetHashKey(ip), itemList.Key);
                        Notification.Send($"~b~File {itemList.Key} has been delete");
                    }
                };
                i++;
            }
            
            Screen.LoadingPrompt.Hide();
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
       
                            
        public static async void AutoPenaltyMenu(CitizenFX.Core.Vehicle veh)
                {
                    HideMenu();
        
                    if (await Ctos.IsBlackout())
                    {
                        Notification.SendWithTime("~r~Связь во время блекаута не работает");
                        return;
                    }
                    
                    
                    Chat.SendMeCommand("говорит \"запрашиваю эвакуатор\" в рацию");
                    var v = Main.FindNearestVehicle();
                    if (v == null)
                    {
                        Notification.SendWithTime("~r~Нужно быть рядом с машиной");
                        return;
                    }
                    
                    
                    var menu = new Menu();
                    UiMenu = menu.Create("Эвакуатор", "~b~Меню");
                    
                    menu.AddMenuItem(UiMenu, "Эвакуировать на штраф стоянку:", $"~b~Марка: ~s~{veh.DisplayName}\n~b~").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                    };
                    
                    var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
                    
                    UiMenu.OnItemSelect += (sender, item, index) =>
                    {
                        if (item == closeButton)
                            HideMenu();
                    };
                    
                    MenuPool.Add(UiMenu);
                }
        public static async void ShowHackerSearchList(int radius)
        {
            HideMenu();
            
            User.PlayPhoneAnimation();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Связь во время блекаута не работает");
                return;
            }
            if (User.GetNetwork() < 40)
            {
                Notification.SendWithTime("~r~Слишком слабая связь");
                return;
            }
            
            var list = Main.GetObjListOnRadius(radius);
            var listCount = list.Count;
            var rand = new Random();
            
            var menu = new Menu();
            UiMenu = menu.Create("Linux", $"~b~bash search.sh {radius}");

            var plPos = GetEntityCoords(GetPlayerPed(-1), true);

            int ii = 0;
            int i = 1;
            foreach (var itemId in list)
            {
                Screen.LoadingPrompt.Show($"Searching {++ii}/{listCount}");
                await Delay(10 + rand.Next(40));
                foreach (string t in Ctos.GetElecboxList())
                {
                    if (itemId.Model.Hash != GetHashKey(t) && itemId.Model.Hash != (uint) GetHashKey(t)) continue;
                    var listDo = new List<dynamic> {"Destroy"};
                    if (User.Data.mp0_watchdogs > 15)
                        listDo = new List<dynamic> {"Destroy", "Explode"};
                    if (User.Data.mp0_watchdogs > 55)
                        listDo = new List<dynamic> {"Destroy", "Explode", "Destroy Radius", "Explode Radius"};
                    
                    await Delay(100);
                    
                    menu.AddMenuItemList(UiMenu, $"~b~{i}. ~s~ElecBox [{Main.GetDistanceToSquared(plPos, itemId.Position)}f]", listDo, $"Street: {World.GetStreetName(itemId.Position)}").OnListSelected += async (uimenu, idx) =>
                    {
                        HideMenu();
                        User.StopScenario();

                        if (idx == 0)
                        {
                            await Ctos.DownloadFile("elecbackdoor.sh");
                            await Ctos.ExecuteFile("elecbackdoor.sh");
                            Ctos.ElecboxDestroy(itemId.Position, 5);
                            User.AddWatchDogs(30, 10);
                        }
                        if (idx == 1)
                        {
                            await Ctos.DownloadFile("elecbackdoor.sh");
                            await Ctos.ExecuteFile("elecbackdoor.sh", 100);
                            Ctos.ElecboxExplode(itemId.Position, 5);
                            Ctos.TrafficLightDestroy(itemId.Position, 200);
                            Ctos.StreetLightDestroy(itemId.Position, 200);
                            User.AddWatchDogs(40, 10);
                        }
                        if (idx == 2)
                        {
                            await Ctos.DownloadFile("elecbackdoorv2.sh");
                            int r = Convert.ToInt32(await Menu.GetUserInput("Radius", null, 3));
                            if (r < 1 || r > 300)
                            {
                                Notification.Send("Radius > 1 or < 300");
                                return;
                            }
                            await Ctos.ExecuteFile("elecbackdoorv2.sh", 200);
                            Ctos.ElecboxDestroy(itemId.Position, r);
                            User.AddWatchDogs(85, 20);
                        }
                        if (idx == 3)
                        {
                            await Ctos.DownloadFile("elecbackdoorv2.sh");
                            int r = Convert.ToInt32(await Menu.GetUserInput("Radius", null, 3));
                            if (r < 1 || r > 300)
                            {
                                Notification.Send("Radius > 1 or < 300");
                                return;
                            }
                            await Ctos.ExecuteFile("elecbackdoorv2.sh", 200);
                            Ctos.ElecboxExplode(itemId.Position, r);
                            Ctos.TrafficLightDestroy(itemId.Position, r + 200);
                            Ctos.StreetLightDestroy(itemId.Position, r + 200);
                            User.AddWatchDogs(85, 20);
                        }
                    };
                    i++;
                }
                
                foreach (string t in Ctos.GetTrafficLightList())
                {
                    if (itemId.Model.Hash != GetHashKey(t) && itemId.Model.Hash != (uint) GetHashKey(t)) continue;
                    var listDo = new List<dynamic> {"Destroy"};   
                    if (User.Data.mp0_watchdogs > 55)
                        listDo = new List<dynamic> {"Destroy", "Destroy Radius"};
                    menu.AddMenuItemList(UiMenu, $"~b~{i}. ~s~TrafficLight [{Main.GetDistanceToSquared(plPos, itemId.Position)}f]", listDo, $"Street: {World.GetStreetName(itemId.Position)}").OnListSelected += async (uimenu, idx) =>
                    {
                        HideMenu();
                        User.StopScenario();
                        
                        if (idx == 0)
                        {
                            await Ctos.DownloadFile("traffbackdoor.sh");
                            await Ctos.ExecuteFile("traffbackdoor.sh");
                            Ctos.TrafficLightDestroy(itemId.Position, 5);
                            User.AddWatchDogs(30, 10);
                        }
                        if (idx == 1)
                        {
                            await Ctos.DownloadFile("traffbackdoorv2.sh");
                            int r = Convert.ToInt32(await Menu.GetUserInput("Radius", null, 3));
                            if (r < 1 || r > 300)
                            {
                                Notification.Send("Radius > 1 or < 300");
                                return;
                            }
                            await Ctos.ExecuteFile("traffbackdoorv2.sh", 200);
                            Ctos.TrafficLightDestroy(itemId.Position, r);
                            User.AddWatchDogs(85, 20);
                        }
                    };
                    i++;
                }
                
                foreach (string t in Ctos.GetStreetLightList())
                {
                    if (itemId.Model.Hash != GetHashKey(t) && itemId.Model.Hash != (uint) GetHashKey(t)) continue;
                    var listDo = new List<dynamic> {"Destroy"};   
                    if (User.Data.mp0_watchdogs > 55)
                        listDo = new List<dynamic> {"Destroy", "Destroy Radius"};
                    menu.AddMenuItemList(UiMenu, $"~b~{i}. ~s~StreetLight [{Main.GetDistanceToSquared(plPos, itemId.Position)}f]", listDo, $"Street: {World.GetStreetName(itemId.Position)}").OnListSelected += async (uimenu, idx) =>
                    {
                        HideMenu();
                        User.StopScenario();
                        
                        if (idx == 0)
                        {
                            await Ctos.DownloadFile("lightbackdoor.sh");
                            await Ctos.ExecuteFile("lightbackdoor.sh");
                            Ctos.StreetLightDestroy(itemId.Position, 5);
                            User.AddWatchDogs(30, 10);
                        }
                        if (idx == 1)
                        {
                            await Ctos.DownloadFile("lightbackdoorv2.sh");
                            int r = Convert.ToInt32(await Menu.GetUserInput("Radius", null, 3));
                            if (r < 1 || r > 300)
                            {
                                Notification.Send("Radius > 1 or < 300");
                                return;
                            }
                            await Ctos.ExecuteFile("lightbackdoorv2.sh", 200);
                            Ctos.StreetLightDestroy(itemId.Position, r);
                            User.AddWatchDogs(85, 20);
                        }
                    };
                    i++;
                }
                
                await Delay(1);
            }

            foreach (var ped in Main.GetPedListOnRadius(radius))
            {
                if (User.IsAnimal(ped.Model.Hash) || ped.IsInVehicle()) continue;
                
                var listDo = new List<dynamic> {"Send sms"};
                if (User.Data.mp0_watchdogs > 20)
                    listDo = new List<dynamic> {"Send sms", "Hack bank card"};
                if (User.Data.mp0_watchdogs > 55)
                    listDo = new List<dynamic> {"Send sms", "Hack bank card", "Send sms in radius", "Hack bank card in radius"};
                
                await Delay(100);
                
                menu.AddMenuItemList(UiMenu, $"~b~{i}. ~s~Phone [{Main.GetDistanceToSquared(plPos, ped.Position)}f]", listDo, $"Street: {World.GetStreetName(ped.Position)}").OnListSelected += async (uimenu, idx) =>
                {
                    HideMenu();
                    User.StopScenario();

                    if (Screen.LoadingPrompt.IsActive)
                    {
                        Notification.Send("~r~Please stand by");
                        return;
                    }

                    if (idx == 0)
                    {
                        await Ctos.DownloadFile("phonebackdoor.sh");
                        await Ctos.ExecuteFile("phonebackdoor.sh");
                        Ctos.HackPhone(ped);
                        User.AddWatchDogs(30, 10);
                    }
                    if (idx == 1)
                    {
                        await Ctos.DownloadFile("cardbackdoor.sh");
                        await Ctos.ExecuteFile("cardbackdoor.sh", 50);
                        Ctos.HackPhoneBankCard(ped);
                        User.AddWatchDogs(40, 15);
                    }
                    if (idx == 2)
                    {
                        await Ctos.DownloadFile("phonebackdoorv2.sh");
                        int r = Convert.ToInt32(await Menu.GetUserInput("Radius", null, 3));
                        if (r < 1 || r > 300)
                        {
                            Notification.Send("Radius > 1 or < 300");
                            return;
                        }
                        await Ctos.ExecuteFile("phonebackdoorv2.sh", 200);
                        Ctos.HackPhoneInRadius(ped.Position, r);
                        User.AddWatchDogs(80, 20);
                    }
                    if (idx == 3)
                    {
                        await Ctos.DownloadFile("cardbackdoorv2.sh");
                        int r = Convert.ToInt32(await Menu.GetUserInput("Radius", null, 3));
                        if (r < 1 || r > 300)
                        {
                            Notification.Send("Radius > 1 or < 300");
                            return;
                        }
                        await Ctos.ExecuteFile("cardbackdoorv2.sh", 200);
                        Ctos.HackPhoneBankCardInRadius(ped.Position, r);
                        User.AddWatchDogs(90, 20);
                    }
                };
                i++;
            }
            
            Screen.LoadingPrompt.Hide();
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                {
                    HideMenu();
                    User.PlayPhoneAnimation();
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowHackerSearchPyList(int radius)
        {
            HideMenu();
            
            User.PlayPhoneAnimation();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Связь во время блекаута не работает");
                return;
            }
            if (User.GetNetwork() < 40)
            {
                Notification.SendWithTime("~r~Слишком слабая связь");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Linux", $"~b~python search.py {radius}");

            var plPos = GetEntityCoords(GetPlayerPed(-1), true);

            int i = 0;
            /*for (i = 0; i < Ctos.Network.Length / 3; i++)
            {
                Screen.LoadingPrompt.Show($"Searching {i}/{Ctos.Network.Length / 3}");
                var shopPos = new Vector3((float) Ctos.Network[i, 0], (float) Ctos.Network[i, 1], (float) Ctos.Network[i, 2]);
                var distance = Main.GetDistanceToSquared(shopPos, plPos);
                if (!(distance < radius)) continue;
                
                var listDo = new List<dynamic> {"Destroy"};

                await Delay(100);

                var i1 = i;
                menu.AddMenuItemList(UiMenu, $"~b~{i}. ~s~Network [{distance}f]", listDo, $"Street: {World.GetStreetName(shopPos)}").OnListSelected += async (uimenu, idx) =>
                {
                    HideMenu();
                    User.StopScenario();

                    if (await Sync.Data.Has(-1, "DisableNetwork" + i1))
                    {
                        Notification.Send("~r~No network");
                        return;
                    }

                    if (Screen.LoadingPrompt.IsActive)
                    {
                        Notification.Send("~r~Please stand by");
                        return;
                    }

                    if (idx == 0)
                    {
                        await Ctos.DownloadFile("fnetwork.py", 20);
                        await Ctos.ExecuteFile("fnetwork.py", 500);
                        Sync.Data.Set(-1, "DisableNetwork" + i1, true);
                        User.AddWatchDogs(98, 10);
                    }
                };
            }*/

            foreach (var ped in Main.GetPedListOnRadius(radius))
            {
                if (User.IsAnimal(ped.Model.Hash) || ped.IsInVehicle()) continue;
                
                var listDo = new List<dynamic> {"Sms to dispatch"};
                
                await Delay(100);
                
                menu.AddMenuItemList(UiMenu, $"~b~{++i}. ~s~Phone [{Main.GetDistanceToSquared(plPos, ped.Position)}f]", listDo, $"Street: {World.GetStreetName(ped.Position)}").OnListSelected += async (uimenu, idx) =>
                {
                    HideMenu();
                    User.StopScenario();

                    if (Screen.LoadingPrompt.IsActive)
                    {
                        Notification.Send("~r~Please stand by");
                        return;
                    }

                    if (idx == 0)
                    {
                        int type = Convert.ToInt32(await Menu.GetUserInput("Sms Type", null, 1));
                        var listDispatch = new List<string> {"Драка", "Пьяный мужчина", "Неадекватное поведение", "ДТП", "ДТП"};
                        var rand = new Random();

                        var textSms = listDispatch[rand.Next(listDispatch.Count)];
                        if (type >= 0 && type < 4)
                            textSms = listDispatch[type];
                        
                        await Ctos.DownloadFile("phonesms.py", 20);
                        await Ctos.ExecuteFile("phonesms.py", 1000);
                        
                        Notification.Send("~g~Send sms. Text: ~s~" + textSms);
                        
                        Ctos.HackPhone(ped);
                        Dispatcher.SendEms("Код 2", textSms);
                        User.AddWatchDogs(99, 10);
                    }
                };
            }
            
            Screen.LoadingPrompt.Hide();
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                {
                    HideMenu();
                    User.PlayPhoneAnimation();
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowStreetList()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Улицы", "~b~GPS опасных улиц");

            for (int i = 0; i < Grab.GrabPosList.Length / 4; i++)
            {
                var pos = new Vector3((float) Grab.GrabPosList[i, 1], (float) Grab.GrabPosList[i, 2], (float) Grab.GrabPosList[i, 3]);
                var streetName = World.GetZoneLocalizedName(pos);
                
                menu.AddMenuItem(UiMenu, "Склад: " + streetName).Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Notification.SendPictureToFraction("Офицер выдвинулся на улицу " + streetName, "Диспетчер", User.Data.rp_name, "CHAR_CALL911", Notification.TypeChatbox, User.Data.fraction_id);
                    User.SetWaypoint(pos.X, pos.Y);
                };
            }

            for (int i = 0; i < Main.Shops.Length / 4; i++)
            {
                var pos = new Vector3((float) Main.Shops[i, 0], (float) Main.Shops[i, 1], (float) Main.Shops[i, 2]);
                var streetName = World.GetZoneLocalizedName(pos);
                
                menu.AddMenuItem(UiMenu, "Магазин: " + streetName).Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Notification.SendPictureToFraction("Офицер выдвинулся на улицу " + streetName, "Диспетчер", User.Data.rp_name, "CHAR_CALL911", Notification.TypeChatbox, User.Data.fraction_id);
                    User.SetWaypoint(pos.X, pos.Y);
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowFractionMemberInviteMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Организация", "~b~Принять в организацию");
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        
                        var plData = await User.GetAllDataByServerId(p.ServerId);
                        if (plData == null) return;
                        if (plData.fraction_id > 0)
                        {
                            Notification.SendWithTime("~r~Данный игрок уже состоит в организации");
                            return;
                        }
                        
                        Sync.Data.Set(p.ServerId, "rank", 1);
                        Sync.Data.Set(p.ServerId, "fraction_id", User.Data.fraction_id);
                        Shared.TriggerEventToPlayer(p.ServerId, "ARP:UpdateAllData");
                        
                        Main.SaveLog("fractionLog", $"[INVITE] {User.Data.rp_name} ({User.Data.fraction_id}) - {User.PlayerIdList[p.ServerId.ToString()]}");
                        
                        TriggerServerEvent("ARP:SendServerToPlayerSubTitle", $"~g~Вас приняли в {Main.GetFractionName(User.Data.fraction_id)}", p.ServerId);
                        Notification.SendWithTime($"~g~Вы приняли во фракцию {User.PlayerIdList[p.ServerId.ToString()]}");
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowFraction2MemberInviteMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Организация", "~b~Принять в организацию");
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        
                        var plData = await User.GetAllDataByServerId(p.ServerId);
                        if (plData == null) return;
                        if (plData.fraction_id2 > 0)
                        {
                            Notification.SendWithTime("~r~Данный игрок уже состоит в организации");
                            return;
                        }
                        
                        Sync.Data.Set(p.ServerId, "rank2", 1);
                        Sync.Data.Set(p.ServerId, "fraction_id2", User.Data.fraction_id2);
                        Shared.TriggerEventToPlayer(p.ServerId, "ARP:UpdateAllData");
                        
                        Main.SaveLog("fractionLog2", $"[INVITE] {User.Data.rp_name} ({User.Data.fraction_id2}) - {User.PlayerIdList[p.ServerId.ToString()]}");
                        
                        TriggerServerEvent("ARP:SendServerToPlayerSubTitle", $"~g~Вас приняли в {await Client.Sync.Data.Get(-9000000 + User.Data.fraction_id2, "name")}", p.ServerId);
                        Notification.SendWithTime($"~g~Вы приняли во фракцию {User.PlayerIdList[p.ServerId.ToString()]}");
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowFractionMemberListMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Организация", "~b~Список членов организации");

            Sync.Data.ShowSyncMessage = false;
            CitizenFX.Core.UI.Screen.LoadingPrompt.Show("Загрузка данных");
            
            foreach (Player p in new PlayerList())
            {
                try
                {
                    var plData = await User.GetAllDataByServerId(p.ServerId);
                    if (plData == null) continue;
                    if (plData.fraction_id != User.Data.fraction_id) continue;
                    
                    if ((User.IsLeader() || User.IsSubLeader()) && plData.id != User.Data.id && plData.rank < 14)
                    {
                        menu.AddMenuItem(UiMenu, plData.rp_name, Main.GetRankName(plData.fraction_id, plData.rank)).Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            ShowFractionMemberDoMenu(p.ServerId, plData.rp_name);
                        };
                    }
                    else
                        menu.AddMenuItem(UiMenu, plData.rp_name, Main.GetRankName(plData.fraction_id, plData.rank));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            CitizenFX.Core.UI.Screen.LoadingPrompt.Hide();
            Sync.Data.ShowSyncMessage = true;
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowFractionMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowFractionMemberDoMenu(int serverId, string name)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Организация", "~b~Действие над: ~s~" + name);
            
            menu.AddMenuItem(UiMenu, "Выдать ранг").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int number = Convert.ToInt32(await Menu.GetUserInput("Введите число", null, 2));

                if (number < 1 && number > 13)
                {
                    Notification.SendWithTime("~r~Число должно быть выше 0 и ниже 14");
                    return;
                }

                if (Main.GetRankName(User.Data.fraction_id, number) == "")
                {
                    Notification.SendWithTime("~r~Этот ранг выдать нельзя");
                    return;
                }
                
                Sync.Data.Set(serverId, "rank", number);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", $"~g~Вам выдали {number} ранг", serverId);
                Notification.SendWithTime($"~g~Вы выдали {number} ранг");
                Shared.TriggerEventToPlayer(serverId, "ARP:UpdateAllData");
            };
            
            menu.AddMenuItem(UiMenu, "~r~Уволить").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "rank", 0);
                Sync.Data.Set(serverId, "fraction_id", 0);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "~r~Вас уволили из организации", serverId);
                Shared.TriggerEventToPlayer(serverId, "ARP:UpdateAllData");
                Notification.SendWithTime($"~r~Вы уволили {name} из организации");
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowFractionMemberDoNewMenu(string name)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Организация", "~b~Действие над: ~s~" + name);
            
            menu.AddMenuItem(UiMenu, "Выдать ранг").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int number = Convert.ToInt32(await Menu.GetUserInput("Введите число", null, 2));

                if (number < 1 || number > 13)
                {
                    Notification.SendWithTime("~r~Число должно быть выше 0 и ниже 14");
                    return;
                }

                if (Main.GetRankName(User.Data.fraction_id, number) == "")
                {
                    Notification.SendWithTime("~r~Этот ранг выдать нельзя");
                    return;
                }
                
                TriggerServerEvent("ARP:GivePlayerRank", name, number);
            };
            
            menu.AddMenuItem(UiMenu, "~r~Уволить").Activated += (uimenu, item) =>
            {
                HideMenu();
                TriggerServerEvent("ARP:Uninvite", name);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowFractionMemberDoNewMenu2(string name)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Организация", "~b~Действие над: ~s~" + name);
            
            menu.AddMenuItem(UiMenu, "Выдать ранг").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int number = Convert.ToInt32(await Menu.GetUserInput("Введите число", null, 2));

                if (number < 1 || number > 10)
                {
                    Notification.SendWithTime("~r~Число должно быть выше 0 и ниже 11");
                    return;
                }
                
                TriggerServerEvent("ARP:GivePlayerRank2", name, number);
            };
            
            menu.AddMenuItem(UiMenu, "~r~Уволить").Activated += (uimenu, item) =>
            {
                HideMenu();
                TriggerServerEvent("ARP:Uninvite2", name);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowHelpMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Справка", "~b~Меню справки");
            
            menu.AddMenuItem(UiMenu, "С чего начать?").Activated += (uimenu, item) =>
            {
                Chat.SendChatInfoMessage("СПРАВКА | С чего начать?", 
                    "<br>1) Вам нужно приехать в <i>здание правительства</i>, оформить регистрацию и пособие по безработице (M - GPS - Важные места - Здание Правительства)" +
                    "<br>2) Подзаработайте не много денег (Мойщик окон / Строитель / Дорожный рабочий). Работы находяться в GPS - Работы" +
                    "<br>3) Получите права в автошколе категории А и B" +
                    "<br>4) Далее в здании правительства во вкладке \"Трудовая биржа\" вам доступны еще 3 профессии (Дезинсектор, Уборщик квартир, Мехатронник)");
            };
            menu.AddMenuItem(UiMenu, "Где работать?").Activated += (uimenu, item) =>
            {
                Chat.SendChatInfoMessage("СПРАВКА | Где работать?",
                    "<br>1) Устроиться на трудовой бирже (M - GPS - Здание правительства)" +
                    "<br>2) M - GPS - Работы (Найти нужный автопарк)" +
                    "<br>3) Взять из гаража ваш транспорт" +
                    "<br>4) Нужно транспорт открыть через меню <i>2</i>");
            };
            menu.AddMenuItem(UiMenu, "Горячие клавиши").Activated += (uimenu, item) =>
            {
                Chat.SendChatInfoMessage("СПРАВКА | Горячие клавиши",
                    "<br>F5 / NUM9 - узнать ID игрока (Нужно зажать)" +
                    "<br>F10 - сбросить анимацию " +
                    "<br>F9 - меню быстрых анимаций");
            };
            menu.AddMenuItem(UiMenu, "Реферальная система").Activated += (uimenu, item) =>
            {
                Chat.SendChatInfoMessage("СПРАВКА | Реферальная система",
                    "<br>- При достижении 21 года, пригласивший получает 100 Appi Coins на своё счёт" +
                    "<br>- При достижении 25 лет, пригласивший получает 10% от вашего доната. То есть, вы задонатили 100 Appi Coins, к вам они пришли все 100 и сверху пригласивший получил 10ac");
            };
            menu.AddMenuItem(UiMenu, "Доп. Информация").Activated += (uimenu, item) =>
            {
                Chat.SendChatInfoMessage("СПРАВКА | Доп. Информация",
                    "<br>- Различные FAQ для новичков у нас есть на форуме" +
                    "<br>- Так же у нас есть Discord (Можно его найти на сайте <i>alamo-rp.com</i>" +
                    "<br>- У нас в дискорде весьма уютно и всегда можно пообщаться с администрацией");
            };
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowMainMenu();
                /*if (item == jobButton)
                    ShowGpsJobMenu();*/
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowRockstarEditorMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Rockstar", "~b~Rockstar Editor");

            if (IsRecording())
            {
                menu.AddMenuItem(UiMenu, "Остановить и сохранить").Activated += (uimenu, item) =>
                {
                    StopRecordingAndSaveClip();
                };
                menu.AddMenuItem(UiMenu, "Остановить и отменить").Activated += (uimenu, item) =>
                {
                    StopRecordingAndSaveClip();
                };
            }
            else
            {
                menu.AddMenuItem(UiMenu, "Записать повтор").Activated += (uimenu, item) =>
                {
                    StartRecording(0);
                };
                menu.AddMenuItem(UiMenu, "Начать запись").Activated += (uimenu, item) =>
                {
                    StartRecording(1);
                };
            }
            
            menu.AddMenuItem(UiMenu, "Открыть редактор").Activated += (uimenu, item) =>
            {
                NetworkSessionLeaveSinglePlayer();
                ActivateRockstarEditor();
            };
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowMainMenu();
                /*if (item == jobButton)
                    ShowGpsJobMenu();*/
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowSettingsMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Настройки", "~b~Меню ваших настроек");
            
            menu.AddMenuItem(UiMenu, "~y~Rockstar Editor").Activated += (uimenu, item) =>
            {
                ShowRockstarEditorMenu();
            };
            
            var list3 = new List<dynamic> {"Вкл", "Выкл"};
            menu.AddMenuItemList(UiMenu, "Показывать интерфейс", list3, "Нажмите ~g~Enter~s~ чтобы применить").OnListSelected += (uimenu, index) =>
            {
                TriggerEvent("ARPHUD:Show", index == 0);
                Screen.Hud.IsVisible = index == 0;
                Screen.Hud.IsRadarVisible = index == 0;
            };

            if (User.Data.id_house > 0 && User.Data.apartment_id > 0)
            {
                var list2 = new List<dynamic> {"Дом", "Апартаменты"};
                menu.AddMenuItemList(UiMenu, "Точка появления", list2, "Нажмите ~g~Enter~s~ чтобы применить").OnListSelected += (uimenu, index) =>
                {
                    User.Data.s_is_spawn_aprt = index == 1;
                    Sync.Data.Set(User.GetServerId(), "s_is_spawn_aprt", User.Data.s_is_spawn_aprt);
                    Notification.SendWithTime("~b~Точка появления: " + list2[index]);
                };
            }

            var list = new List<dynamic> {"Шепот", "Нормально", "Крик"};
            menu.AddMenuItemList(UiMenu, "Тип голосового чата", list, "Нажмите ~g~Enter~s~ чтобы применить").OnListSelected += (uimenu, index) =>
            {
                User.Voice = index;

                switch (User.Voice)
                {
                    case 0:
                        User.VoiceString = "Шепот";
                        NetworkSetTalkerProximity(2f);
                        break;
                    case 2:
                        User.VoiceString = "Крик";
                        NetworkSetTalkerProximity(10f);
                        break;
                    default:
                        User.Voice = 1;
                        User.VoiceString = "Нормально";
                        NetworkSetTalkerProximity(5f);
                        break;
                }
            };
            
            var listBal = new List<dynamic> {"Вкл", "Выкл"};
            menu.AddMenuItemList(UiMenu, "Объем голосового чата", listBal, "В случае если у вас сломан один наушник, отключите").OnListSelected += (uimenu, idx) =>
            {
                User.Data.s_voice_balance = idx == 0;
                Client.Sync.Data.Set(User.GetServerId(), "s_voice_balance", User.Data.s_voice_balance);
                Notification.Send($"~g~Объем: {(idx == 0 ? "Вкл" : "Выкл")}");
            };
            
            var listVol = new List<dynamic> {"0", "10", "20", "30", "40", "50", "60", "70", "80", "90", "100"};
            menu.AddMenuItemList(UiMenu, "Громкость голосового чата", listVol).OnListSelected += (uimenu, idx) =>
            {
                User.Data.s_voice_vol = idx - 1;
                if (idx == 0)
                    User.Data.s_voice_vol = 0;
                else
                    User.Data.s_voice_vol = Convert.ToInt32(listVol[idx]) / 100f;
                Client.Sync.Data.Set(User.GetServerId(), "s_voice_vol", User.Data.s_voice_vol);
                
                Notification.Send($"~g~Громкость: {listVol[idx]}%");
            };
            
            menu.AddMenuItem(UiMenu, "Перезагрузить голосовой чат").Activated += (uimenu, item) =>
            {
                Voice.Restart();
            };
            
            menu.AddMenuItem(UiMenu, "~b~Пофиксить кастомизацию #1").Activated += (uimenu, item) =>
            {
                Characher.UpdateFace();
                Characher.UpdateTattoo();
                Characher.UpdateCloth();
                
                
            };
            
            menu.AddMenuItem(UiMenu, "~b~Пофиксить кастомизацию #2").Activated += (uimenu, item) =>
            {
                Characher.UpdateFace(false);
                Characher.UpdateTattoo(false);
                Characher.UpdateCloth(false);
                
                };
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowMainMenu();
                /*if (item == jobButton)
                    ShowGpsJobMenu();*/
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowGpsMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("GPS", "~b~Меню вашего GPS");
            
            menu.AddMenuItem(UiMenu, "Важные места").Activated += (uimenu, item) =>
            {
                ShowGpsImportantMenu();
            };
            menu.AddMenuItem(UiMenu, "Работы").Activated += (uimenu, item) =>
            {
                ShowGpsJobMenu();
            };
            menu.AddMenuItem(UiMenu, "Магазины и прочее").Activated += (uimenu, item) =>
            {
                ShowGpsOtherMenu();
            };
            menu.AddMenuItem(UiMenu, "Компании").Activated += (uimenu, item) =>
            {
                ShowGpsCompMenu();
            };
            menu.AddMenuItem(UiMenu, "Интересные места").Activated += (uimenu, item) =>
            {
                ShowGpsInterestingMenu();
            };
            menu.AddMenuItem(UiMenu, "~y~Убрать метку").Activated += (uimenu, item) =>
            {
                World.RemoveWaypoint();
            };
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowMainMenu();
                /*if (item == jobButton)
                    ShowGpsJobMenu();*/
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowGpsImportantMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("GPS", "~b~Важные места");

            menu.AddMenuItem(UiMenu, "Здание правительства", "Получение регистрации / Трудоустройство / Иные вопросы").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-1379, -499);
            };

            menu.AddMenuItem(UiMenu, "Автошкола", "Получение прав всех категорий").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-1581, -557);
            };

            menu.AddMenuItem(UiMenu, "Государственный банк \"~r~Maze Bank~s~\"", "Продажа вашего имущества / операции со счетом").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-75, -826);
            };

            menu.AddMenuItem(UiMenu, "Частный банк \"~o~Pacific Standard~s~\"").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(235, 216);
            };

            menu.AddMenuItem(UiMenu, "Найти ближайший \"~g~Fleeca\"~s~ банк").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                var fleccaPosPrew = new Vector3((float) Main.FleccaBankMarkers[0, 0], (float) Main.FleccaBankMarkers[0, 1], (float) Main.FleccaBankMarkers[0, 2]);
                for (int i = 0; i < 3; i++)
                {
                    var coords = GetEntityCoords(GetPlayerPed(-1), true);
                    
                    var fleccaPos = new Vector3((float) Main.FleccaBankMarkers[i, 0], (float) Main.FleccaBankMarkers[i, 1], (float) Main.FleccaBankMarkers[i, 2]);
                    if(Main.GetDistanceToSquared(fleccaPos, coords) < Main.GetDistanceToSquared(fleccaPosPrew, coords))
                        fleccaPosPrew = fleccaPos;
                }
                
                User.SetWaypoint(fleccaPosPrew.X, fleccaPosPrew.Y);
            };

            menu.AddMenuItem(UiMenu, "Частный банк \"~b~Blaine County~s~\"").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-111, 6467);
            };

            menu.AddMenuItem(UiMenu, "Бизнес центр \"~b~Arcadius~s~\"").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-139, -631);
            };

            menu.AddMenuItem(UiMenu, "Полицейский участок").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(437, -982);
            };
            menu.AddMenuItem(UiMenu, "Департамент Шерифов").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-443, 6016);
            };

            menu.AddMenuItem(UiMenu, "Больница Лос-Сантоса").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(299, -584);
            };

            menu.AddMenuItem(UiMenu, "Федеральная тюрьма").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(1830, 2603);
            };
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowGpsMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowGpsJobMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("GPS", "~b~Работы");

            menu.AddMenuItem(UiMenu, "Стройка").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-142, -936);
            };
            menu.AddMenuItem(UiMenu, "Мойщик окон").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-1539, -448);
            };
            menu.AddMenuItem(UiMenu, "Дорожные работы").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(53, -723);
            };
            menu.AddMenuItem(UiMenu, "Свалка металлолома").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-428, -1728);
            };
            menu.AddMenuItem(UiMenu, "Стоянка мусоровозов").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(1569, -2130);
            };
            menu.AddMenuItem(UiMenu, "Стоянка садовников").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-1146, -745);
            };
            menu.AddMenuItem(UiMenu, "Автобусный парк Los Santos Transit").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-717, -2175);
            };
            menu.AddMenuItem(UiMenu, "Автобусный парк Dashound").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(463, -575);
            };
            menu.AddMenuItem(UiMenu, "Чистка берега").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-1470, -1394);
            };
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowGpsMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowGpsOtherMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("GPS", "~b~Магазины и прочее");
            
            menu.AddMenuItem(UiMenu, "Аптека Family Pharmacy").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(Managers.Pickup.AptekaPos.X, Managers.Pickup.AptekaPos.Y);
            };

            menu.AddMenuItem(UiMenu, "Найти магазин электронной техники").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(Shop.ShopElPos.X, Shop.ShopElPos.Y);
            };

            menu.AddMenuItem(UiMenu, "Найти ближайший магазин 24/7").Activated += (uimenu, item) =>
            {
                HideMenu();
                var shopPos = Business.Shop.FindNearest(GetEntityCoords(GetPlayerPed(-1), true));
                User.SetWaypoint(shopPos.X, shopPos.Y);
            };

            menu.AddMenuItem(UiMenu, "Найти ближайшую заправку").Activated += (uimenu, item) =>
            {
                HideMenu();
                var fuelPos = Business.Fuel.FindNearest(GetEntityCoords(GetPlayerPed(-1), true));
                User.SetWaypoint(fuelPos.X, fuelPos.Y);
            };

            menu.AddMenuItem(UiMenu, "Найти ближайший пункт аренды").Activated += (uimenu, item) =>
            {
                HideMenu();
                var rentPos = Business.Rent.FindNearest(GetEntityCoords(GetPlayerPed(-1), true));
                User.SetWaypoint(rentPos.X, rentPos.Y);
            };

            menu.AddMenuItem(UiMenu, "Найти ближайший бар").Activated += (uimenu, item) =>
            {
                HideMenu();
                var rentPos = Business.Bar.FindNearest(GetEntityCoords(GetPlayerPed(-1), true));
                User.SetWaypoint(rentPos.X, rentPos.Y);
            };

            menu.AddMenuItem(UiMenu, "Найти ближайший магазин оружия").Activated += (uimenu, item) =>
            {
                HideMenu();
                var rentPos = Business.Gun.FindNearest(GetEntityCoords(GetPlayerPed(-1), true));
                User.SetWaypoint(rentPos.X, rentPos.Y);
            };

            menu.AddMenuItem(UiMenu, "Найти ближайший тату салон").Activated += (uimenu, item) =>
            {
                HideMenu();
                var rentPos = Business.Tattoo.FindNearest(GetEntityCoords(GetPlayerPed(-1), true));
                User.SetWaypoint(rentPos.X, rentPos.Y);
            };

            menu.AddMenuItem(UiMenu, "Магазин масок").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-1337.255f, -1277.948f);
            };

            menu.AddMenuItem(UiMenu, "Парикмахерская").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-33.62676f, -154.6508f);
            };

            menu.AddMenuItem(UiMenu, "Auto Repairs Mirror Park", "Починка личного транспорта").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(Managers.Pickup.AutoRepairsPosCarPos.X, Managers.Pickup.AutoRepairsPosCarPos.Y);
            };

            menu.AddMenuItem(UiMenu, "Аавтомойка").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-700, -932);
            };

            /*menu.AddMenuItem(UiMenu, "Найти ближайший пункт аренды").Activated += (uimenu, item) =>
            {
                HideMenu();
                var fuelPos = Business.Fuel.FindNearest(GetEntityCoords(GetPlayerPed(-1), true));
                User.SetWaypoint(fuelPos.X, fuelPos.Y);
            };*/
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowGpsMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowGpsCompMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("GPS", "~b~Компании");

            menu.AddMenuItem(UiMenu, "Post Op").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-416, -2855);
            };

            menu.AddMenuItem(UiMenu, "GoPostal").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(74, 120);
            };

            menu.AddMenuItem(UiMenu, "Bugstars").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(151, -3083);
            };

            menu.AddMenuItem(UiMenu, "Gruppe Sechs").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-42, -664);
            };

                menu.AddMenuItem(UiMenu, "Sunset Bleach").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-1194, -1480);
            };

            menu.AddMenuItem(UiMenu, "Water & Power").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(633, 125);
            };

            menu.AddMenuItem(UiMenu, "O'Connor").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-1146, -745);
            };

            menu.AddMenuItem(UiMenu, "Humane Labs").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(3616, 3730);
            };

            menu.AddMenuItem(UiMenu, "Life Invader").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-1041.409f, -241.3437f);
            };

            menu.AddMenuItem(UiMenu, "Downtown Cab Co.").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(895.4368f, -179.3315f);
            };
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowGpsMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowGpsInterestingMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("GPS", "~b~Интересные места");

            menu.AddMenuItem(UiMenu, "Life Invader").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-1041.409f, -241.3437f);
            };

            menu.AddMenuItem(UiMenu, "Международный аэропорт").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-1037, -2737);
            };

            menu.AddMenuItem(UiMenu, "Спортзал").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-1204, -1564);
            };

            menu.AddMenuItem(UiMenu, "Площадь Лос-Сантоса").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(161, -993);
            };

            menu.AddMenuItem(UiMenu, "Стриптиз клуб").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(105, -1291);
            };

            menu.AddMenuItem(UiMenu, "Бар \"Tequila\"").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-562, 286);
            };

            menu.AddMenuItem(UiMenu, "Бар \"Yellow Jack\"").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(1986, 3054);
            };

            menu.AddMenuItem(UiMenu, "Байкер клуб").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(988, -96);
            };

            menu.AddMenuItem(UiMenu, "Камеди клуб").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-450, 280);
            };

            menu.AddMenuItem(UiMenu, "Казино").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-2065, -1024);
            };

            menu.AddMenuItem(UiMenu, "Пляж").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-1581, -1162);
            };

            menu.AddMenuItem(UiMenu, "Обсерватория").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(-411, 1173);
            };

            menu.AddMenuItem(UiMenu, "Надпись Vinewood").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(716, 1203);
            };

            menu.AddMenuItem(UiMenu, "Сцена-1").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(226, 1173);
            };

            menu.AddMenuItem(UiMenu, "Сцена-2").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaypoint(689, 602);
            };
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowGpsMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowAnimationMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Анимации", "~b~Меню анимаций");
            
            //С 22 до 9
            if (Managers.Weather.RealHour >= 21 || Managers.Weather.RealHour <= 10)
            {
                menu.AddMenuItem(UiMenu, "~g~РП Сон", "Навыки во время РП сна не падают").Activated += (uimenu, item) =>
                {
                    if (User.GetPlayerVirtualWorld() != 0)
                    {
                        User.PlayAnimation("amb@world_human_bum_slumped@male@laying_on_right_side@base", "base", 9);
                        User.IsRpAnim = true;
                    }
                    else
                    {
                        Notification.SendWithTime("~r~Разрешено использовать только в доме");
                    }
                };
            }
            
            menu.AddMenuItem(UiMenu, "Анимации действий").Activated += (uimenu, item) =>
            {
                ShowAnimationActionMenu();
            };
            
            menu.AddMenuItem(UiMenu, "Позирующие анимации").Activated += (uimenu, item) =>
            {
                ShowAnimationPoseMenu();
            };
            
            menu.AddMenuItem(UiMenu, "Положительные эмоции").Activated += (uimenu, item) =>
            {
                ShowAnimationPositiveMenu();
            };
            
            menu.AddMenuItem(UiMenu, "Негативные эмоции").Activated += (uimenu, item) =>
            {
                ShowAnimationNegativeMenu();
            };
            
            menu.AddMenuItem(UiMenu, "Танцы").Activated += (uimenu, item) =>
            {
                ShowAnimationDanceMenu();
            };
            
            menu.AddMenuItem(UiMenu, "Остальные анимации").Activated += (uimenu, item) =>
            {
                ShowAnimationRemainMenu();
            };
            
            var listRagdoll = new List<dynamic> {"Вкл", "Выкл"};
            menu.AddMenuItemList(UiMenu, "Ragdoll", listRagdoll).OnListSelected += (uimenu, idx) =>
            {
                HideMenu();
                User.EnableRagdoll = idx == 0;
            };

            var stopButton = menu.AddMenuItem(UiMenu, "~y~Остановить анимацию");
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowPlayerMenu();
                if (item == stopButton) {
                    User.StopScenario();
                    User.StopAnimation();
                    User.IsRpAnim = false;
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowAnimationFastMenu()
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create("Анимации", "~b~Меню анимаций");
            
            menu.AddMenuItem(UiMenu, "Руки за голову").Activated += (uimenu, item) =>
            {
                User.PlayArrestAnimation();
            };
            
            menu.AddMenuItem(UiMenu, "Руки вверх").Activated += (uimenu, item) =>
            {
                User.PlayAnimation("random@mugging3", "handsup_standing_base", 49);
            };
            
            menu.AddMenuItem(UiMenu, "Сесть").Activated += (uimenu, item) =>
            {
                User.PlayScenario("PROP_HUMAN_SEAT_BENCH");
            };
            
            menu.AddMenuItem(UiMenu, "Записывать в блокнот").Activated += (uimenu, item) =>
            {
                User.PlayScenario("CODE_HUMAN_MEDIC_TIME_OF_DEATH");
            };
            
            menu.AddMenuItem(UiMenu, "Читать документ").Activated += (uimenu, item) =>
            {
                User.PlayScenario("WORLD_HUMAN_CLIPBOARD");
            };
            
            menu.AddMenuItem(UiMenu, "Копаться в телефоне").Activated += (uimenu, item) =>
            {
                User.PlayScenario("WORLD_HUMAN_STAND_MOBILE");
            };

            var stopButton = menu.AddMenuItem(UiMenu, "~y~Остановить анимацию");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == stopButton) {
                    User.StopScenario();
                    User.StopAnimation();
                    User.IsRpAnim = false;
                }
            };
            
            MenuPool.Add(UiMenu);
        }

        /*public static async void ShowPlayerToPlayerAnimationMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Анимации взаимодействий", "~b~ТЕСТ");
            menu.AddMenuItem(UiMenu, "Пожать руку").Activated += async (uimenu, item) =>
            {
                HideMenu();
                
                var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1.5f);
                if (player == null)
                {
                    Notification.SendWithTime("~r~Рядом никого нет");
                    return;
                }
                Shared.TriggerEventToPlayer(User.GetServerId(), "ARP:UserPlayAnimationToPlayer", "mp_cp_welcome_tutgreet", "greet", 8);
                User.PlayAnimation("mp_cp_welcome_tutgreet", "greet", 8);
            };
        }*/

        public static void ShowAnimationActionMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Анимации", "~b~Анимации действий");
            
            menu.AddMenuItem(UiMenu, "Руки за голову").Activated += (uimenu, item) =>
            {
                User.PlayArrestAnimation();
            };

            for (int i = 0; i < PedAnimations.Actions.Length / 4; i++)
            {
                var i1 = i;
                menu.AddMenuItem(UiMenu, (string) PedAnimations.Actions[i, 0]).Activated += (uimenu, item) =>
                {
                    User.PlayAnimation((string) PedAnimations.Actions[i1, 1], (string) PedAnimations.Actions[i1, 2], (int) PedAnimations.Actions[i1, 3]);
                };
            }

            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowAnimationMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowAnimationPoseMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Анимации", "~b~Позирующие анимации");

            for (int i = 0; i < PedAnimations.Pose.Length / 4; i++)
            {
                var i1 = i;
                menu.AddMenuItem(UiMenu, (string) PedAnimations.Pose[i, 0]).Activated += (uimenu, item) =>
                {
                    User.PlayAnimation((string) PedAnimations.Pose[i1, 1], (string) PedAnimations.Pose[i1, 2], (int) PedAnimations.Pose[i1, 3]);
                };
            }

            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowAnimationMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowAnimationPositiveMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Анимации", "~b~Положительные эмоции");

            for (int i = 0; i < PedAnimations.Positive.Length / 4; i++)
            {
                var i1 = i;
                menu.AddMenuItem(UiMenu, (string) PedAnimations.Positive[i, 0]).Activated += (uimenu, item) =>
                {
                    User.PlayAnimation((string) PedAnimations.Positive[i1, 1], (string) PedAnimations.Positive[i1, 2], (int) PedAnimations.Positive[i1, 3]);
                };
            }

            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowAnimationMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowAnimationNegativeMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Анимации", "~b~Негативные эмоции");

            for (int i = 0; i < PedAnimations.Negative.Length / 4; i++)
            {
                var i1 = i;
                menu.AddMenuItem(UiMenu, (string) PedAnimations.Negative[i, 0]).Activated += (uimenu, item) =>
                {
                    User.PlayAnimation((string) PedAnimations.Negative[i1, 1], (string) PedAnimations.Negative[i1, 2], (int) PedAnimations.Negative[i1, 3]);
                };
            }

            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowAnimationMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowAnimationDanceMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Анимации", "~b~Танцы");

            for (int i = 0; i < PedAnimations.Dance.Length / 4; i++)
            {
                var i1 = i;
                menu.AddMenuItem(UiMenu, (string) PedAnimations.Dance[i, 0]).Activated += (uimenu, item) =>
                {
                    User.PlayAnimation((string) PedAnimations.Dance[i1, 1], (string) PedAnimations.Dance[i1, 2], (int) PedAnimations.Dance[i1, 3]);
                };
            }

            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowAnimationMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowAnimationRemainMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Анимации", "~b~Остальные анимации");
            
            foreach (var val in PedScenarios.ScenarioNames)
            {
                menu.AddMenuItem(UiMenu, val.Key).Activated += (uimenu, item) =>
                {
                    //HideMenu();
                    User.PlayScenario(PedScenarios.ScenarioNames[val.Key]);
                };
            }

            for (int i = 0; i < PedAnimations.Remain.Length / 4; i++)
            {
                var i1 = i;
                menu.AddMenuItem(UiMenu, (string) PedAnimations.Remain[i, 0]).Activated += (uimenu, item) =>
                {
                    User.PlayAnimation((string) PedAnimations.Remain[i1, 1], (string) PedAnimations.Remain[i1, 2], (int) PedAnimations.Remain[i1, 3]);
                };
            }

            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowAnimationMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowPlayerMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Персонаж", "~b~Меню вашего персонажа");
            
            var bagButton = menu.AddMenuItem(UiMenu, "Инвентарь");
            menu.AddMenuItem(UiMenu, "Экипировка").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowPlayerEquipMenu(); 
            };
            var doButton = menu.AddMenuItem(UiMenu, "Действия");
            var docButton = menu.AddMenuItem(UiMenu, "Документы");

            if (User.Data.phone > 0)
            {
                menu.AddMenuItem(UiMenu, "Телефон").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowPlayerPhoneMenu(); 
                };
            }
            if (User.Data.is_buy_walkietalkie && Main.ServerName != "Earth")
            {
                menu.AddMenuItem(UiMenu, "Рация").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowPlayerWalkietalkieMenu(); 
                };
            }
            
            var list = new List<dynamic>();
            var list2 = new List<dynamic>();

            if (User.Skin.SEX == 1)
            {
                for (int i = 0; i < Main.ClipsetFemale.Length / 2; i++)
                {
                    list.Add(Main.ClipsetFemale[i, 0]);
                    list2.Add(Main.ClipsetFemale[i, 1]);
                }
            }
            else
            {
                for (int i = 0; i < Main.ClipsetMale.Length / 2; i++)
                {
                    list.Add(Main.ClipsetMale[i, 0]);
                    list2.Add(Main.ClipsetMale[i, 1]);
                }
            }
            
            menu.AddMenuItemList(UiMenu, "Походка", list).OnListSelected += (uimenu, idx) =>
            {
                HideMenu();
                User.SetPlayerNewClipset(list2[idx].ToString());
                Notification.SendWithTime($"~g~Походка \"{list[idx].ToString()}\" установлена");
            };
            
            var statsButton = menu.AddMenuItem(UiMenu, "Статистика");
            var animButton = menu.AddMenuItem(UiMenu, "Анимации");
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowMainMenu();
                if (item == statsButton)
                    ShowPlayerStatsMenu();
                if (item == doButton)
                    ShowPlayerDoMenu();
                if (item == bagButton)
                    Managers.Inventory.GetItemList(User.Data.id, InventoryTypes.Player);
                if (item == docButton)
                    ShowPlayerDocListMenu();
                if (item == animButton)
                    ShowAnimationMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        
        public static async void ShowPlayerEquipMenu()
        {
            HideMenu();

            await Delay(1000);
            
            var menu = new Menu();
            UiMenu = menu.Create("Персонаж", "~b~Меню вашей экипировки");
            
            if (User.Data.phone_code > 0)
            {
                menu.AddMenuItem(UiMenu, "Телефон").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    if (Screen.LoadingPrompt.IsActive)
                    {
                        Notification.SendWithTime("~r~Дожитесь окончания загрузки");
                        return;
                    }
                    Managers.Inventory.UnEquipItem(8);
                };
            }
            if (User.Data.bank_prefix > 0)
            {
                menu.AddMenuItem(UiMenu, "Банковская карта").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Managers.Inventory.UnEquipItem(50);
                };
            }
            if (User.Data.item_clock)
            {
                menu.AddMenuItem(UiMenu, "Часы").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Managers.Inventory.UnEquipItem(7);
                };
            }
            menu.AddMenuItem(UiMenu, "Наличка").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowPlayerEquipMoneyMenu();
            };
            /*if (User.Data.id_house > 0)
            {
                menu.AddMenuItem(UiMenu, "Ключи от дома").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Managers.Inventory.UnEquipItem(43);
                };
            }
            if (User.Data.apartment_id > 0)
            {
                menu.AddMenuItem(UiMenu, "Ключи от квартиры").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Managers.Inventory.UnEquipItem(44);
                };
            }*/

            if (User.Data.jail_time < 10)
            {
                if (User.Data.mask > 0)
                {
                    menu.AddMenuItem(UiMenu, "Использовать маску").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        if (Sync.Data.HasLocally(User.GetServerId(), "hasBuyMask"))
                        {
                            SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                            Sync.Data.ResetLocally(User.GetServerId(), "hasBuyMask");
                        }
                        else
                        {
                            SetPedComponentVariation(GetPlayerPed(-1), 1, User.Data.mask, 0, 2);
                            Sync.Data.SetLocally(User.GetServerId(), "hasBuyMask", true);
                        }
                    };
                }

                if (User.Data.torso != 15)
                {
                    menu.AddMenuItem(UiMenu, "Снять торс").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Managers.Inventory.UnEquipItem(265);
                    };
                }

                if (User.Skin.SEX == 0)
                {
                    if (User.Data.leg != 61)
                    {
                        menu.AddMenuItem(UiMenu, "Снять штаны").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            Managers.Inventory.UnEquipItem(266);
                        };
                    }
                }
                else
                {
                    if (User.Data.leg != 15)
                    {
                        menu.AddMenuItem(UiMenu, "Снять штаны").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            Managers.Inventory.UnEquipItem(266);
                        };
                    }
                }

                if (User.Skin.SEX == 0)
                {
                    if (User.Data.foot != 34)
                    {
                        menu.AddMenuItem(UiMenu, "Снять обувь").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            Managers.Inventory.UnEquipItem(267);
                        };
                    }
                }
                else
                {
                    if (User.Data.foot != 35)
                    {
                        menu.AddMenuItem(UiMenu, "Снять обувь").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            Managers.Inventory.UnEquipItem(267);
                        };
                    }
                }

                if (User.Data.accessorie > 0)
                {
                    menu.AddMenuItem(UiMenu, "Снять аксессуар").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Managers.Inventory.UnEquipItem(268);
                    };
                }

                if (User.Data.hat >= 0)
                {
                    menu.AddMenuItem(UiMenu, "Снять головной убор").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Managers.Inventory.UnEquipItem(269);
                    };
                }

                if (User.Data.glasses >= 0)
                {
                    menu.AddMenuItem(UiMenu, "Снять очки").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Managers.Inventory.UnEquipItem(270);
                    };
                }

                if (User.Data.ear >= 0)
                {
                    menu.AddMenuItem(UiMenu, "Снять серьги").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Managers.Inventory.UnEquipItem(271);
                    };
                }

                if (User.Data.watch >= 0)
                {
                    menu.AddMenuItem(UiMenu, "Снять часы/браслет").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Managers.Inventory.UnEquipItem(272);
                    };
                }
                if (User.Data.is_buy_walkietalkie)
                {
                    menu.AddMenuItem(UiMenu, "Рация").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Managers.Inventory.UnEquipItem(47);
                        };
                }

                if (User.Data.bracelet >= 0)
                {
                    menu.AddMenuItem(UiMenu, "Снять браслет").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Managers.Inventory.UnEquipItem(273);
                    };
                }
            }

            for (int n = 54; n < 138; n++)
            {
                foreach(uint hash in Enum.GetValues(typeof(WeaponHash)))
                {
                    string name = Enum.GetName(typeof(WeaponHash), hash);
                    if (!String.Equals(name, Client.Inventory.GetItemNameHashById(n), StringComparison.CurrentCultureIgnoreCase)) continue;
                    if (!HasPedGotWeapon(GetPlayerPed(-1), hash, false)) continue;
                    
                    var n1 = n;
                    menu.AddMenuItem(UiMenu, Inventory.GetItemNameById(n)).Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        int ammoItem = Managers.Inventory.AmmoTypeToAmmo(GetPedAmmoType(GetPlayerPed(-1), hash));
                        if (ammoItem != -1)
                        {
                            Managers.Inventory.UnEquipItem(Managers.Inventory.AmmoTypeToAmmo(GetPedAmmoType(GetPlayerPed(-1), hash)), GetAmmoInPedWeapon(GetPlayerPed(-1), hash));
                            await Delay(100);
                        }
                        Managers.Inventory.UnEquipItem(n1);
                    };
                }
            }
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowMainMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowPlayerEquipMoneyMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Персонаж", "~b~Меню налички");
            
            menu.AddMenuItem(UiMenu, "Посчитать").Activated += (uimenu, item) =>
            {
                HideMenu();
                Notification.SendWithTime($"В кошельке: ~g~${User.Data.money}");
            };
            menu.AddMenuItem(UiMenu, "Убрать в инвентарь").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.UnEquipItem(140);
            };
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowMainMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowPlayerStatsMenu()
        {
            HideMenu();
            
            await User.GetAllData();

            var menu = new Menu();
            UiMenu = menu.Create("Статистика", "~b~" + User.Data.rp_name);
            
            menu.AddMenuItem(UiMenu, "~b~Имя:~s~").SetRightLabel(User.Data.rp_name);
            menu.AddMenuItem(UiMenu, "~b~Возраст:~s~").SetRightLabel(User.Data.age.ToString());
            menu.AddMenuItem(UiMenu, "~b~Месяцев:~s~").SetRightLabel(Math.Round(User.Data.exp_age / 31.00, 2) + " / 12мес.");
            menu.AddMenuItem(UiMenu, "~b~Работа:~s~").SetRightLabel(User.IsGos() ? User.GetFractionName() : User.GetWorkName());
            menu.AddMenuItem(UiMenu, "~b~Регистрация:~s~").SetRightLabel(User.GetRegStatusName());
            
            if (User.Data.reg_time > 0)
                menu.AddMenuItem(UiMenu, "~b~Время регистрации:~s~").SetRightLabel(Math.Round(User.Data.reg_time / 31.00, 2) + " мес.");
            
            if (User.Data.bank_prefix > 0)
                menu.AddMenuItem(UiMenu, "~b~Номер карты:~s~").SetRightLabel(User.Data.bank_prefix + "-" + User.Data.bank_number);
            
            if (User.Data.phone_code > 0)
                menu.AddMenuItem(UiMenu, "~b~Телефон:~s~").SetRightLabel(User.Data.phone_code + "-" + User.Data.phone);
            
            menu.AddMenuItem(UiMenu, "~b~Розыск:~s~").SetRightLabel(User.Data.wanted_level.ToString());
            menu.AddMenuItem(UiMenu, "~b~Рецепт марихуаны:~s~").SetRightLabel(User.Data.allow_marg ? "Есть" : "Нет");
            menu.AddMenuItem(UiMenu, "~b~Лицензия категории \"А\":~s~").SetRightLabel(User.Data.a_lic ? "Есть" : "Нет");
            menu.AddMenuItem(UiMenu, "~b~Лицензия категории \"B\":~s~").SetRightLabel(User.Data.b_lic ? "Есть" : "Нет");
            menu.AddMenuItem(UiMenu, "~b~Лицензия категории \"C\":~s~").SetRightLabel(User.Data.c_lic ? "Есть" : "Нет");
            menu.AddMenuItem(UiMenu, "~b~Лицензия на пил. самолёта:~s~").SetRightLabel(User.Data.air_lic ? "Есть" : "Нет");
            menu.AddMenuItem(UiMenu, "~b~Лицензия на пил. вертолёта:~s~").SetRightLabel(User.Data.heli_lic ? "Есть" : "Нет");
            menu.AddMenuItem(UiMenu, "~b~Лицензия на водный транспорт:~s~").SetRightLabel(User.Data.ship_lic ? "Есть" : "Нет");
            menu.AddMenuItem(UiMenu, "~b~Лицензия на оружие:~s~").SetRightLabel(User.Data.gun_lic ? "Есть" : "Нет");
            menu.AddMenuItem(UiMenu, "~b~Лицензия на таксиста:~s~").SetRightLabel(User.Data.taxi_lic ? "Есть" : "Нет");
            menu.AddMenuItem(UiMenu, "~b~Лицензия на адвоката:~s~").SetRightLabel(User.Data.law_lic ? "Есть" : "Нет");
            menu.AddMenuItem(UiMenu, "~b~Лицензия на бизнес:~s~").SetRightLabel(User.Data.biz_lic ? "Есть" : "Нет");
            menu.AddMenuItem(UiMenu, "~b~Разрешение на охоту:~s~").SetRightLabel(User.Data.animal_lic ? "Есть" : "Нет");
            menu.AddMenuItem(UiMenu, "~b~Разрешение на рыболовство:~s~").SetRightLabel(User.Data.fish_lic ? "Есть" : "Нет");
            menu.AddMenuItem(UiMenu, "~b~Мед. страховка:~s~").SetRightLabel(User.Data.med_lic ? "Есть" : "Нет");
            menu.AddMenuItem(UiMenu, "~b~Справка о псих. здоровье:~s~").SetRightLabel(User.Data.psy_lic ? "Есть" : "Нет");

            menu.AddMenuItem(UiMenu, "~b~Выносливость:~s~").SetRightLabel((User.Data.mp0_stamina + 1) + "%");
            menu.AddMenuItem(UiMenu, "~b~Сила:~s~").SetRightLabel((User.Data.mp0_strength + 1) + "%");
            menu.AddMenuItem(UiMenu, "~b~Объем легких:~s~").SetRightLabel((User.Data.mp0_lung_capacity + 1) + "%");
            menu.AddMenuItem(UiMenu, "~b~Навык водителя:~s~").SetRightLabel((User.Data.mp0_wheelie_ability + 1) + "%");
            menu.AddMenuItem(UiMenu, "~b~Навык пилота:~s~").SetRightLabel((User.Data.mp0_flying_ability + 1) + "%");
            menu.AddMenuItem(UiMenu, "~b~Навык стрельбы:~s~").SetRightLabel((User.Data.mp0_shooting_ability + 1) + "%");
            menu.AddMenuItem(UiMenu, "~b~Навык владения консолью:~s~").SetRightLabel((User.Data.mp0_watchdogs + 1) + "%");
          
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowPlayerMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static async void ShowPlayerSellCarMenu(List<Player> pedList, int vehId, string number, string displayName)
        {
            HideMenu();
            
            var vehItem = Managers.Vehicle.GetVehicleById(vehId);
            if (await Client.Sync.Data.Has(110000 + vehItem.VehId, "sell_price"))
            {
                if ((int) await Client.Sync.Data.Get(110000 + vehItem.VehId, "sell_price") > 0)
                {
                    Notification.SendWithTime("~r~Уберите транспорт с продажи в PREMIUM DELUXE MOTORSPORT");
                    return;
                }
            }
            
            if (!vehItem.IsVisible)
            {
                Notification.SendWithTime("~r~Уберите транспорт с продажи в PREMIUM DELUXE MOTORSPORT");
                Client.Sync.Data.ShowSyncMessage = true;
                CitizenFX.Core.UI.Screen.LoadingPrompt.Hide();
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Продать", "~b~Транспорт: ~s~" + displayName);
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += async (uimenu, index) =>
                    {
                        HideMenu();
                        int price = Convert.ToInt32(await Menu.GetUserInput("Цена", null, 10));
                        if (price < 1)
                        {
                            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                            return;
                        }
                        Managers.Vehicle.SellToUser(p.ServerId, vehId, number, displayName, price);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowPlayerSellBusinessMenu(List<Player> pedList, int bId, string name)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Продать", "~b~" + name);
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += async (uimenu, index) =>
                    {
                        HideMenu();
                        int price = Convert.ToInt32(await Menu.GetUserInput("Цена", null, 10));
                        if (price < 1)
                        {
                            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                            return;
                        }
                        Business.Business.SellToUser(p.ServerId, bId, name, price);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowPlayerSellHouseMenu(List<Player> pedList, int hId, string name)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Продать", "~b~" + name);
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += async (uimenu, index) =>
                    {
                        HideMenu();
                        int price = Convert.ToInt32(await Menu.GetUserInput("Цена", null, 10));
                        if (price < 1)
                        {
                            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                            return;
                        }
                        Managers.House.SellToUser(p.ServerId, hId, name, price);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowPlayerHookupHouseMenu(List<Player> pedList, int hId, string name)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Подселить", "~b~" + name);
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += async (uimenu, index) =>
                    {
                        HideMenu();
                        
                        Managers.House.HookupToUser(p.ServerId, hId, name);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowPlayerAntiHookupHouseMenu(List<Player> pedList, int hId, string name)
        {
            HideMenu();
            
            //Managers.House.HookupToUser(p.ServerId, hId, name);
            TriggerServerEvent("ARP:UpdateHouseInfoAntiHookup", hId, User.Data.id);
        }

        public static void ShowPlayerSellStockMenu(List<Player> pedList, int hId, string name)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Продать", "~b~" + name);
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += async (uimenu, index) =>
                    {
                        HideMenu();
                        int price = Convert.ToInt32(await Menu.GetUserInput("Цена", null, 10));
                        if (price < 1)
                        {
                            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                            return;
                        }
                        Managers.House.SellToUser(p.ServerId, hId, name, price);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowPlayerGiveInvItemMenu(List<Player> pedList, int id, int itemId)
        {
            HideMenu();
            
            if (CitizenFX.Core.UI.Screen.LoadingPrompt.IsActive)
            {
                Notification.SendWithTime("~r~Данное действие сейчас недоступно");
                return;
            }

            string itemName = Inventory.GetItemNameById(itemId);
            
            var menu = new Menu();
            UiMenu = menu.Create("Передать", "~b~" + itemName);
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += (uimenu, index) =>
                    {
                        HideMenu();
                        Managers.Inventory.GiveItem(id, itemId, User.PlayerIdList[p.ServerId.ToString()]);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowPlayerGiveMoneyMenu(List<Player> pedList)
        {
            HideMenu();
            
            if (CitizenFX.Core.UI.Screen.LoadingPrompt.IsActive)
            {
                Notification.SendWithTime("~r~Данное действие сейчас недоступно");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Передать", "~b~Передать деньги");
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += async (uimenu, index) =>
                    {
                        HideMenu();
                        int money = Convert.ToInt32(await Menu.GetUserInput(Lang.GetTextToPlayer("_lang_1"), null, 10));
                        if (money < 1)
                        {
                            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                            return;
                        }
                        User.GiveCashMoney(p.ServerId, money);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowPlayerEjectMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Транспорт", "~b~Выкинуть из транспорта");
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += (uimenu, index) =>
                    {
                        HideMenu(); 
                        Shared.TriggerEventToPlayer(p.ServerId, "ARP:EjectCar");
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowPlayerDocListMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Документы", "~b~Меню ваших документов");
            
            menu.AddMenuItem(UiMenu, "Показать паспорт").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowPlayerDocShowPassMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
            };
            
            menu.AddMenuItem(UiMenu, "Показать лицензии").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowPlayerDocShowLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
            };

            if (User.IsGos())
            {
                menu.AddMenuItem(UiMenu, "Показать удостоверение").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowPlayerGovDocShowLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
                };
            }
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowPlayerMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowPlayerDoMenu()
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create("Действия", "~b~Меню ваших действий");

            menu.AddMenuItem(UiMenu, "Передать деньги").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowPlayerGiveMoneyMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
            };

            menu.AddMenuItem(UiMenu, "Вырубить").Activated += async (uimenu, item) =>
            {
                HideMenu();

                if (Sync.Data.HasLocally(User.GetServerId(), "isKnockoutTimeout"))
                {
                    Notification.SendWithTime("~r~Таймаут 1 минута");
                    return;
                }

                var pPos = GetEntityCoords(GetPlayerPed(-1), true);
                var player = Main.GetPlayerOnRadius(pPos, 1.5f);
                if (player == null)
                {
                    Notification.SendWithTime("~r~Рядом с вами никого нет");
                    return;
                }

                var rand = new Random();
                if (rand.Next(3) >= 1)
                {
                    Main.SaveLog("GangBang",
                        $"[KNOCK] {User.Data.rp_name} - {User.PlayerIdList[player.ServerId.ToString()]} | {pPos.X} {pPos.Y} {pPos.Z}");
                    Shared.TriggerEventToPlayer(player.ServerId, "ARP:Knockout");
                    Chat.SendMeCommand("замахнулся кулаком и ударил человека напротив");
                }
                else
                {
                    Main.SaveLog("GangBang",
                        $"[FAIL_KNOCK] {User.Data.rp_name} - {User.PlayerIdList[player.ServerId.ToString()]} | {pPos.X} {pPos.Y} {pPos.Z}");
                    Chat.SendMeCommand("замахнулся кулаком и промахнулся");
                }

                Sync.Data.SetLocally(User.GetServerId(), "isKnockoutTimeout", true);
                await Delay(60000);
                Sync.Data.ResetLocally(User.GetServerId(), "isKnockoutTimeout");
            };

            /*menu.AddMenuItem(UiMenu, "Развязать человека").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1.5f);
                if (player == null)
                {
                    Notification.SendWithTime("~r~Рядом с вами никого нет");
                    return;
                }

                if (await Client.Sync.Data.Has(player.ServerId, "isTie"))
                {
                    Shared.TriggerEventToPlayer(player.ServerId, "ARP:UnTie");
                    Notification.SendWithTime("~y~Вы развязали игрока");
                    Chat.SendMeCommand("развязал человека рядом");
                    Managers.Inventory.AddItemServer(0, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                    return;
                }

                Notification.SendWithTime("~y~Человек не связан");
            };*/

            menu.AddMenuItem(UiMenu, "Снять мешок с игрока").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1.5f);
                if (player == null)
                {
                    Notification.SendWithTime("~r~Рядом с вами никого нет");
                    return;
                }

                if (await Client.Sync.Data.Has(player.ServerId, "isTieBandage"))
                {
                    Shared.TriggerEventToPlayer(player.ServerId, "ARP:UnTieBandage");
                    Notification.SendWithTime("~y~Вы сняли мешок с головы");
                    Chat.SendMeCommand("снял мешок с головы человеку рядом");
                    Managers.Inventory.AddItemServer(1, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                    return;
                }

                Notification.SendWithTime("~y~На игроке нет мешка");
            };

            menu.AddMenuItem(UiMenu, "Затащить в ближайшее авто").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1.5f);
                if (player == null)
                {
                    Notification.SendWithTime("~r~Рядом с вами никого нет");
                    return;
                }

                if (!await Sync.Data.Has(player.ServerId, "isTie") && !await Sync.Data.Has(player.ServerId, "isCuff"))
                {
                    Notification.SendWithTime("~r~Игрок должен быть связан или в наручниках");
                    return;
                }
                
                Shared.TriggerEventToPlayer(player.ServerId, "ARP:Incar");
            };

            menu.AddMenuItem(UiMenu, "Обыск игрока").Activated += async (uimenu, item) =>
            {
                var pPos = GetEntityCoords(GetPlayerPed(-1), true);
                var player = Main.GetPlayerOnRadius(pPos, 1f);
                if (player == null)
                {
                    Notification.SendWithTime("~r~Рядом с вами никого нет");
                    return;
                }

                if (!await Sync.Data.Has(player.ServerId, "isTie") && !await Sync.Data.Has(player.ServerId, "isCuff"))
                {
                    Notification.SendWithTime("~r~Игрок должен быть связан или в наручниках");
                    return;
                }

                Main.SaveLog("GangBang",
                    $"[FIND] {User.Data.rp_name} - {User.PlayerIdList[player.ServerId.ToString()]} | {pPos.X} {pPos.Y} {pPos.Z}");

                User.PlayScenario("CODE_HUMAN_MEDIC_KNEEL");
                await Delay(5000);

                Managers.Inventory.GetItemList((int) await Sync.Data.Get(player.ServerId, "id"), InventoryTypes.Player);

                User.StopScenario();
            };

            menu.AddMenuItem(UiMenu, "Снять спец. экипировку").Activated += async (uimenu, item) =>
            {
                
                var pPos = GetEntityCoords(GetPlayerPed(-1), true);
                var player = Main.GetPlayerOnRadius(pPos, 1f);
                if (player == null)
                {
                    Notification.SendWithTime("~r~Рядом с вами никого нет");
                    return;
                }
                
                if (!await Sync.Data.Has(player.ServerId, "isTie") && !await Sync.Data.Has(player.ServerId, "isCuff"))
                {
                    Notification.SendWithTime("~r~Игрок должен быть связан или в наручниках");
                    return;
                }
                //Main.SaveLog("UnDuty", $"[FIND] {User.Data.rp_name} - {User.PlayerIdList[player.ServerId.ToString()]} | {pPos.X} {pPos.Y} {pPos.Z}");
                TriggerServerEvent("ARP:UnDuty");
                Shared.TriggerEventToPlayer(player.ServerId, "ARP:UnDuty");
                Notification.SendWithTime("~r~Вы отобрали у человека рацию");
                Chat.SendMeCommand("Обыскивает человека");
                await Delay(2000);
                Chat.SendMeCommand("Отобрал рацию");
            };
            
            menu.AddMenuItem(UiMenu, "Изъять оружие").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1.5f);
                if (player == null)
                {
                    Notification.SendWithTime("~r~Рядом с вами никого нет");
                    return;
                }
                
                if (!await Sync.Data.Has(player.ServerId, "isTie") && !await Sync.Data.Has(player.ServerId, "isCuff"))
                {
                    Notification.SendWithTime("~r~Игрок должен быть связан или в наручниках");
                    return;
                }
                
                Shared.TriggerEventToPlayer(player.ServerId, "ARP:TakeAllGuns", User.Data.id);
                
                Chat.SendMeCommand("обыскал человека напротив и изъял оружие");
            };
            
            /*menu.AddMenuItem(UiMenu, "Взять на руки").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var player = Main.GetPlayerOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1.5f);
                if (!await Sync.Data.Has(player.ServerId, "isTie") && !await Sync.Data.Has(player.ServerId, "isCuff"))
                {
                    Notification.SendWithTime("~r~Игрок не связан или на игроке нет наручников");
                    return;
                }
                TakePed1(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f));
                
                
            };*/
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowPlayerMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowPlayerDocShowPassMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Документы", "~b~Показать паспорт");
            
            foreach (Player p in pedList)
            {
                try
                {
                    //if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        TriggerServerEvent("ARP:SendPlayerShowPass", p.ServerId);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowPlayerDocShowLicMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Документы", "~b~Показать лицензии");
            
            foreach (Player p in pedList)
            {
                try
                {
                    //if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        TriggerServerEvent("ARP:SendPlayerShowDoc", p.ServerId);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowPlayerGovDocShowLicMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Документы", "~b~Показать удостоверение");
            
            foreach (Player p in pedList)
            {
                try
                {
                    //if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        TriggerServerEvent("ARP:SendPlayerShowGovDoc", p.ServerId);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowToPlayerMenu(string title, string desc, dynamic data)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create(title, desc);
            
            foreach (var property in (IDictionary<String, Object>) data)
                menu.AddMenuItem(UiMenu, "~b~" + property.Key + ": ~s~").SetRightLabel(property.Value.ToString());
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowToPlayerMembersMenu(dynamic data)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Организация", "~b~Список членов организации");

            foreach (var property in (IDictionary<String, Object>) data)
            {
                string name = property.Key.Remove(0, 8);
                if ((User.IsLeader() || User.IsSubLeader()) && name != User.Data.rp_name)
                {
                    menu.AddMenuItem(UiMenu, property.Key, property.Value.ToString()).Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        ShowFractionMemberDoNewMenu(name);
                    };
                }
                else
                    menu.AddMenuItem(UiMenu, property.Key, property.Value.ToString());
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowToPlayerMembersMenu2(dynamic data)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Организация", "~b~Список членов организации");

            foreach (var property in (IDictionary<String, Object>) data)
            {
                string name = property.Key.Remove(0, 8);
                if ((User.IsLeader2() || User.IsSubLeader2()) && name != User.Data.rp_name)
                {
                    menu.AddMenuItem(UiMenu, property.Key, property.Value.ToString()).Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        ShowFractionMemberDoNewMenu2(name);
                    };
                }
                else
                    menu.AddMenuItem(UiMenu, property.Key, property.Value.ToString());
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowToPlayersListMenu(dynamic data)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Список", "~b~Список");

            foreach (var property in (IDictionary<String, Object>) data)
                menu.AddMenuItem(UiMenu, property.Key, property.Value.ToString());
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowToPlayerBankInfoMenu(dynamic data)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Банк", "~b~Информация о клиентах");

            foreach (var property in (IDictionary<String, Object>) data)
            {
                menu.AddMenuItem(UiMenu, $"~b~{property.Key}~s~").SetRightLabel(property.Value.ToString());
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowToPlayerLogVehicleMenu(dynamic data)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Лог", "~b~Лог на транспорт (Время по МСК)");

            foreach (var property in (IDictionary<String, Object>) data)
            {
                var array = property.Key.Split('|');
                menu.AddMenuItem(UiMenu, "~b~" + array[0] + "~s~", property.Value.ToString()).SetRightLabel(array[1]);
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowToPlayerLogGunMenu(dynamic data)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Лог", "~b~Лог (Время по МСК)");

            foreach (var property in (IDictionary<String, Object>) data)
            {
                var array = property.Key.Split('|');
                menu.AddMenuItem(UiMenu, "~b~" + array[0] + "~s~", property.Value.ToString()).SetRightLabel(array[1]);
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowToPlayerBusinessListMenu(dynamic data, dynamic data2)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Arcadius", "~b~Список офисов");

            var tempData2 = (IDictionary<String, Object>) data2;

            foreach (var property in (IDictionary<String, Object>) data)
            {
                menu.AddMenuItem(UiMenu, tempData2[property.Key].ToString(), $"~b~Владелец: ~s~{(property.Value.ToString() != "" ? property.Value.ToString() : "Государство")}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Business.Business.Enter(Convert.ToInt32(property.Key));
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowToPlayerApartmentListMenu(dynamic data)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Апартаменты", "~b~Список апартаментов");


            foreach (var property in (IDictionary<String, Object>) data)
            {
                menu.AddMenuItem(UiMenu, "Апартаменты №" + property.Key, $"~b~Владелец: ~s~{(property.Value.ToString() != "" ? property.Value.ToString() : "Государство")}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Apartment.Enter(Convert.ToInt32(property.Key));
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static async void ShowToPlayerItemListMenu(dynamic data, dynamic data2, int ownerId, int ownerType)
        {
            HideMenu();
            
            var tempData2 = (IDictionary<String, Object>) data2;
            int invAmountMax = await Managers.Inventory.GetInvAmountMax(ownerId, ownerType);
            int sum = tempData2.Sum(property => Inventory.GetItemAmountById(Convert.ToInt32(property.Value)));
            
            Managers.Inventory.SetInvAmount(ownerId, ownerType, sum);

            string name = "Предметы";
            if (ownerType == InventoryTypes.Vehicle)
            {
                var vehicle = Managers.Inventory.FindVehicle(ownerId);
                if (vehicle != null)
                    name = VehInfo.GetDisplayName(vehicle.Model.Hash);
            }
            
            var menu = new Menu();
            UiMenu = menu.Create(name, invAmountMax != -1 ? $"~b~Объем: ~s~{sum}/{invAmountMax}см³" : "~b~Список предметов");
            
            foreach (var property in (IDictionary<String, Object>) data)
            {
                int itemId = Convert.ToInt32(tempData2[property.Key]);
                string itemName = Inventory.GetItemNameById(itemId);
                
                if (itemId >= 265 && itemId <= 273 && property.Value.ToString() != "")
                {
                    try
                    {
                        var prefix = Convert.ToInt32(property.Value.ToString().Split('|')[0]);
                        var keyId = Convert.ToInt32(property.Value.ToString().Split('|')[1]);
                        if (itemId >= 265 && itemId < 269)
                            itemName = prefix == 1 ? Cloth.ClothF[keyId, 9] : Cloth.ClothM[keyId, 9];
                        else 
                            itemName = prefix == 1 ? Cloth.PropF[keyId, 5] : Cloth.PropM[keyId, 5];
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                    }
                }
                else if (property.Value.ToString() != "")
                    itemName += " " + property.Value.ToString();
                
                menu.AddMenuItem(UiMenu, itemName, $"~b~Вес: ~s~{Inventory.GetItemWeightById(itemId)}гр.\n~b~Объем: ~s~{Inventory.GetItemAmountById(itemId)}см³").Activated += (uimenu, item) =>
                {
                    HideMenu();

                    if (ownerType == InventoryTypes.StockGang)
                    {
                        if (!User.IsLeader() && (itemId == 138 || itemId == 139 || itemId == 140 || itemId == 141))
                            Notification.SendWithTime("~r~Доступно только для лидера");
                        else
                            Managers.Inventory.GetInfoItem(Convert.ToInt32(property.Key));
                    }
                    else
                        Managers.Inventory.GetInfoItem(Convert.ToInt32(property.Key));
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowToPlayerItemWorldListMenu(dynamic data, dynamic data2)
        {
            HideMenu();
            
            var tempData2 = (IDictionary<String, Object>) data2;
            
            var menu = new Menu();
            UiMenu = menu.Create("Предметы", "~b~Список предметов");
            
            foreach (var property in (IDictionary<String, Object>) data)
            {
                int itemId = Convert.ToInt32(tempData2[property.Key]);
                string itemName = Inventory.GetItemNameById(itemId);

                if (itemId >= 265 && itemId <= 273 && property.Value.ToString() != "")
                {
                    try
                    {
                        var prefix = Convert.ToInt32(property.Value.ToString().Split('|')[0]);
                        var keyId = Convert.ToInt32(property.Value.ToString().Split('|')[1]);
                        if (itemId >= 265 && itemId < 269)
                            itemName = prefix == 1 ? Cloth.ClothF[keyId, 9] : Cloth.ClothM[keyId, 9];
                        else 
                            itemName = prefix == 1 ? Cloth.PropF[keyId, 5] : Cloth.PropM[keyId, 5];
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                    }
                }
                else if (property.Value.ToString() != "")
                    itemName += " " + property.Value.ToString();
                
                menu.AddMenuItem(UiMenu, itemName, $"~b~Вес: ~s~{Inventory.GetItemWeightById(itemId)}гр.\n~b~Объем: ~s~{Inventory.GetItemAmountById(itemId)}см³").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Managers.Inventory.GetInfoItem(Convert.ToInt32(property.Key));
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static async void ShowToPlayerInfoItemMenu(int id, int prefix, int number, int keyId, int itemId, int ownerType, int ownerId, int countItems, bool justInfo = false)
        {
            HideMenu();

            Managers.Inventory.CurrentItem = id;
            
            string itemName = Inventory.GetItemNameById(itemId);

            if (prefix > 0 && number > 0)
                itemName = $"{itemName} ({prefix}-{number})";
            else if (keyId > 0)
                itemName = $"{itemName} #{keyId}";
            
            int amount = Inventory.GetItemAmountById(itemId);
            int weight = Inventory.GetItemWeightById(itemId);
            
            var menu = new Menu();
            UiMenu = menu.Create("Предмет", "~b~" + itemName);

            if (!justInfo)
            {
                if (ownerType == InventoryTypes.Player && ownerId == User.Data.id)
                {
                    var plPos = GetEntityCoords(GetPlayerPed(-1), true);
                    if (Inventory.CanEquipById(itemId))
                    {
                        if (itemId == 54 || itemId == 56 || itemId == 57 || itemId == 62 || itemId == 63 ||
                            itemId == 65 || itemId == 69)
                        {
                            menu.AddMenuItem(UiMenu, "~g~Разрезать веревки").Activated += (uimenu, item) =>
                            {
                                HideMenu();
                                User.UnTieKnife();
                            };
                        }
                        if (itemId == 7 || itemId == 63 )
                        {
                            menu.AddMenuItem(UiMenu, "~g~Использовать").Activated += (uimenu, item) =>
                            {
                                HideMenu();
                                Managers.Inventory.UseItem(id, itemId, 1);
                            };
                        }
                        menu.AddMenuItem(UiMenu, "~g~Экипировать").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            Managers.Inventory.EquipItem(id, itemId, prefix, number, keyId,countItems);
                        };
                    }
                    else
                    {
                        if (itemId == 142 || itemId == 143 || itemId == 144 || itemId == 145 ||
                            itemId == 163 || itemId == 164 || itemId == 165 || itemId == 166 ||
                            itemId == 167 || itemId == 168 || itemId == 169 || itemId == 170 ||
                            itemId == 171 || itemId == 172 || itemId == 173 || itemId == 174 ||
                            itemId == 175 || itemId == 176 || itemId == 177 || itemId == 178 ||
                            itemId == 154 || itemId == 155 || itemId == 156 || itemId == 157 ||
                            itemId == 179 || itemId == 180
                        )
                        {
                            menu.AddMenuItem(UiMenu, "~g~Взять 1гр.").Activated += (uimenu, item) =>
                            {
                                Managers.Inventory.TakeDrugItem(id, itemId, countItems);
                            };

                            if (
                                itemId == 142 || itemId == 143 || itemId == 144 || itemId == 145 ||
                                itemId == 163 || itemId == 164 || itemId == 165 || itemId == 166 ||
                                itemId == 167 || itemId == 168 || itemId == 169 || itemId == 170
                                )
                            {
                                if (countItems >= 10)
                                {
                                    menu.AddMenuItem(UiMenu, "~g~Взять 10гр.").Activated += (uimenu, item) =>
                                    {
                                        Managers.Inventory.TakeDrugItem(id, itemId, countItems, true, 10);
                                    };
                                }

                                if (countItems >= 50)
                                {
                                    menu.AddMenuItem(UiMenu, "~g~Взять 50гр.").Activated += (uimenu, item) =>
                                    {
                                        Managers.Inventory.TakeDrugItem(id, itemId, countItems, true, 50);
                                    };
                                }
                            }

                            menu.AddMenuItem(UiMenu, "~g~Взвесить").Activated += (uimenu, item) =>
                            {
                                Chat.SendMeCommand("взвесил");
                                Notification.SendWithTime($"~g~В пачке {countItems}гр.");
                            };
                        }

                        if (itemId == 4)
                        {
                            menu.AddMenuItem(UiMenu, "~g~Вскрыть ближайший ТС").Activated += (uimenu, item) =>
                            {
                                HideMenu();
                                Managers.Inventory.UseItem(id, itemId, 1);
                            };
                            menu.AddMenuItem(UiMenu, "~g~Вскрыть наручники").Activated += (uimenu, item) =>
                            {
                                HideMenu();
                                Managers.Inventory.UseItem(id, itemId, 2);
                            };
                        }
                        else
                        {
                            menu.AddMenuItem(UiMenu, "~g~Использовать").Activated += (uimenu, item) =>
                            {
                                HideMenu();
                                Managers.Inventory.UseItem(id, itemId, 1);
                            };
                        }
                    }
                    
                    if (itemId == 140 || itemId == 141)
                    {
                        menu.AddMenuItem(UiMenu, "~g~Посчитать").Activated += (uimenu, item) =>
                        {
                            Chat.SendMeCommand("считает наличку");
                            Notification.SendWithTime($"~g~В пачке ${countItems:#,#}");
                        };
                    }
                    if ((itemId >= 146 && itemId <= 153) || (itemId >= 27 && itemId <= 30))
                    {
                        menu.AddMenuItem(UiMenu, "~g~Посчитать").Activated += (uimenu, item) =>
                        {
                            Chat.SendMeCommand("считает патроны");
                            Notification.SendWithTime($"~g~В коробке {countItems:#,#}пт.");
                        };
                    }
                    menu.AddMenuItem(UiMenu, "~y~Передать").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        ShowPlayerGiveInvItemMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f), id, itemId);
                    };
                    menu.AddMenuItem(UiMenu, "~y~Положить в транспорт").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        var vehList = Main.GetVehicleListOnRadius(plPos, 5f);
                        ShowInvVehDropMenu(vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList(), id, itemId, prefix, number, keyId, countItems);
                    };

                    if (User.IsSapd() && Main.GetDistanceToSquared(Managers.Pickup.StockSapdPos, plPos) < Managers.Pickup.DistanceCheck)
                    {
                        menu.AddMenuItem(UiMenu, "~y~Положить на склад").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            Managers.Inventory.DropItemToStockGang(id, itemId, User.IsSapd() ? 2 : User.Data.fraction_id);
                        };
                    }
                    if (User.IsSheriff() && Main.GetDistanceToSquared(Managers.Pickup.StockSheriffPos, plPos) < Managers.Pickup.DistanceCheck)
                    {
                        menu.AddMenuItem(UiMenu, "~y~Положить на склад").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            Managers.Inventory.DropItemToStockGang(id, itemId, User.IsSheriff() ? 7 : User.Data.fraction_id);
                        };
                    }

                    int kitchenId = Main.GetKitchenId();
                    if (kitchenId != 0)
                    {
                        menu.AddMenuItem(UiMenu, "~y~Положить в холодильник").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            Managers.Inventory.DropItemToFridge(id, itemId, kitchenId);
                        };
                    }
                    
                    if (User.GetPlayerVirtualWorld() > 50000)
                    {
                        menu.AddMenuItem(UiMenu, "~y~Положить на склад").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            ShowStockDropMenu(id, itemId);
                            //Managers.Inventory.DropItemToFridge(id, itemId, kitchenId);
                        };
                    }
                    
                    if (User.GetPlayerVirtualWorld() == 0)
                    {
                        menu.AddMenuItem(UiMenu, "~o~Выкинуть на землю").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            Managers.Inventory.DropItem(id, itemId, GetEntityCoords(GetPlayerPed(-1), true));
                        };
                    }
                }
                else
                {
                    menu.AddMenuItem(UiMenu, "~g~Взять").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Managers.Inventory.TakeItem(id, itemId, ownerType);
                    };
                    
                    if (itemId == 142 || itemId == 143 || itemId == 144 || itemId == 145 ||
                        itemId == 163 || itemId == 164 || itemId == 165 || itemId == 166 ||
                        itemId == 167 || itemId == 168 || itemId == 169 || itemId == 170 ||
                        itemId == 171 || itemId == 172 || itemId == 173 || itemId == 174 ||
                        itemId == 175 || itemId == 176 || itemId == 177 || itemId == 178 ||
                        itemId == 154 || itemId == 155 || itemId == 156 || itemId == 157 ||
                        itemId == 179 || itemId == 180
                        )
                    {
                        menu.AddMenuItem(UiMenu, "~g~Взять 1гр.").Activated += (uimenu, item) =>
                        {
                            Managers.Inventory.TakeDrugItem(id, itemId, countItems);
                        };

                        if (
                            itemId == 142 || itemId == 143 || itemId == 144 || itemId == 145 ||
                            itemId == 163 || itemId == 164 || itemId == 165 || itemId == 166 ||
                            itemId == 167 || itemId == 168 || itemId == 169 || itemId == 170
                        )
                        {
                            if (countItems >= 10)
                            {
                                menu.AddMenuItem(UiMenu, "~g~Взять 10гр.").Activated += (uimenu, item) =>
                                {
                                    Managers.Inventory.TakeDrugItem(id, itemId, countItems, true, 10);
                                };
                            }

                            if (countItems >= 50)
                            {
                                menu.AddMenuItem(UiMenu, "~g~Взять 50гр.").Activated += (uimenu, item) =>
                                {
                                    Managers.Inventory.TakeDrugItem(id, itemId, countItems, true, 50);
                                };
                            }
                        }

                        menu.AddMenuItem(UiMenu, "~g~Взвесить").Activated += (uimenu, item) =>
                        {
                            Chat.SendMeCommand("взвесил");
                            Notification.SendWithTime($"~g~В пачке {countItems}гр.");
                        };
                    }
                    
                    var plPos = GetEntityCoords(GetPlayerPed(-1), true);
                    menu.AddMenuItem(UiMenu, "~y~Положить в транспорт").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        var vehList = Main.GetVehicleListOnRadius(plPos, 5f);
                        ShowInvVehDropMenu(vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList(), id, itemId, prefix, number, keyId, countItems);
                    };

                    if (User.IsSapd() && Main.GetDistanceToSquared(Managers.Pickup.StockSapdPos, plPos) < Managers.Pickup.DistanceCheck)
                    {
                        menu.AddMenuItem(UiMenu, "~y~Положить на склад").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            Managers.Inventory.DropItemToStockGang(id, itemId, User.IsSapd() ? 2 : User.Data.fraction_id);
                        };
                    }
                    if (User.IsSheriff() && Main.GetDistanceToSquared(Managers.Pickup.StockSheriffPos, plPos) < Managers.Pickup.DistanceCheck)
                    {
                        menu.AddMenuItem(UiMenu, "~y~Положить на склад").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            Managers.Inventory.DropItemToStockGang(id, itemId, User.IsSheriff() ? 7 : User.Data.fraction_id);
                        };
                    }
                    
                    int kitchenId = Main.GetKitchenId();
                    if (kitchenId != 0)
                    {
                        menu.AddMenuItem(UiMenu, "~y~Положить в холодильник").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            Managers.Inventory.DropItemToFridge(id, itemId, kitchenId);
                        };
                    }
                    if (User.GetPlayerVirtualWorld() > 50000)
                    {
                        menu.AddMenuItem(UiMenu, "~y~Положить на склад").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            ShowStockDropMenu(id, itemId);
                            //Managers.Inventory.DropItemToFridge(id, itemId, kitchenId);
                        };
                    }
                    
                    if (itemId == 140 || itemId == 141)
                    {
                        menu.AddMenuItem(UiMenu, "~g~Посчитать").Activated += (uimenu, item) =>
                        {
                            Chat.SendMeCommand("считает наличку");
                            Notification.SendWithTime($"~g~В пачке ${countItems:#,#}");
                        };
                    }
                    if ((itemId >= 146 && itemId <= 153) || (itemId >= 27 && itemId <= 30))
                    {
                        menu.AddMenuItem(UiMenu, "~g~Посчитать").Activated += (uimenu, item) =>
                        {
                            Chat.SendMeCommand("считает патроны");
                            Notification.SendWithTime($"~g~В коробке {countItems:#,#}пт.");
                        };
                    }
                    if (User.GetPlayerVirtualWorld() == 0 && ownerType != InventoryTypes.World)
                    {
                        menu.AddMenuItem(UiMenu, "~o~Выкинуть на землю").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            Managers.Inventory.DropItem(id, itemId, GetEntityCoords(GetPlayerPed(-1), true));
                        };
                    }
                }
            }
            
            menu.AddMenuItem(UiMenu, $"~b~Вес:~s~ {weight}гр.");
            menu.AddMenuItem(UiMenu, $"~b~Объем:~s~ {amount}см³");
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowInvSelectMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Инвентарь", "~b~Инвентарь");

            menu.AddMenuItem(UiMenu, "Личный инвентарь").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.GetItemList(User.Data.id, InventoryTypes.Player);
            };
            menu.AddMenuItem(UiMenu, "Багажник транспорта").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                var vehList = Main.GetVehicleListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 5f);
                ShowInvVehBagMenu(vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
            };
            menu.AddMenuItem(UiMenu, "Предметы рядом").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.GetItemListInRadius(GetEntityCoords(GetPlayerPed(-1), true));
            };
            menu.AddMenuItem(UiMenu, "Экипировка").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowPlayerEquipMenu();
            };

            if (Managers.Weather.CurrentWeather == "XMAS")
            {
                var plPos = GetEntityCoords(GetPlayerPed(-1), true);
                if (!IsPedInAnyVehicle(PlayerPedId(), true) && User.GetPlayerVirtualWorld() == 0 && GetInteriorAtCoords(plPos.X, plPos.Y, plPos.Z) == 0 && !IsPedSwimming(GetPlayerPed(-1)))
                {
                    menu.AddMenuItem(UiMenu, "Взять снежок").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        User.PlayAnimation("anim@mp_snowball","pickup_snowball", 8);
                        User.GiveWeapon((uint) WeaponHash.Snowball, 1, false, false);
                    };
                }    
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static async void ShowPlayerPhoneMenu()
        {
            HideMenu();

            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Связь во время блекаута не работает");
                return;
            }

            if (User.GetNetwork() < 1)
            {
                Notification.SendWithTime("~r~Нет связи");
                return;
            }

            if (User.GetPlayerVirtualWorld() > 50000)
            {
                Notification.SendWithTime("~r~Нет связи");
                return;
            }

            if (User.Data.jail_time > 0)
            {
                Notification.SendWithTime("~r~Вы в тюрьме");
                return;
            }
            
            //User.PlayPhoneAnimation();//
            TriggerEvent("ARPPhone:Show");
            return;
            
            var menu = new Menu();
            UiMenu = menu.Create("Телефон", "~b~Меню вашего телефона");
            
            menu.AddMenuItem(UiMenu, "Смс").Activated += (uimenu, item) =>
            {
                HideMenu();
                TriggerServerEvent("ARP:OpenSmsListMenu", User.Data.phone_code + "-" + User.Data.phone);
            };
            
            /*menu.AddMenuItem(UiMenu, "Авиарежим").Activated += async (uimenu, item) =>
            {
                HideMenu();
                if (await Client.Sync.Data.Has(User.GetServerId(), "disableNetwork"))
                {
                    Notification.SendWithTime("~g~Авиарежим выключен");
                    Client.Sync.Data.Reset(User.GetServerId(), "disableNetwork");
                }
                else
                {
                    Notification.SendWithTime("~g~Авиарежим включён");
                    Client.Sync.Data.Set(User.GetServerId(), "disableNetwork", true);
                }
            };*/

            if (User.Data.phone_code == 404 || User.Data.phone_code == 403)
            {
                menu.AddMenuItem(UiMenu, "Консоль").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    if (User.GetPlayerVirtualWorld() != 0)
                    {
                        Notification.SendWithTime("~r~Консоль не работает в интерьерах");
                        return;
                    }
                    if (User.GetNetwork() < 40)
                    {
                        Notification.SendWithTime("~r~Связь слишком слабая");
                        return;
                    }
                    Ctos.ExecuteCommand(await Menu.GetUserInput("Введите команду", null, 100));
                    User.StopScenario();
                };
            }
            else
            {
                menu.AddMenuItem(UiMenu, "Записная книжка").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    TriggerServerEvent("ARP:OpenContacntListMenu", User.Data.phone_code + "-" + User.Data.phone);
                    //ShowPlayerPhoneBookMenu();
                };
    
                if (User.Data.bank_prefix > 0)
                {
                    menu.AddMenuItem(UiMenu, "Перевести на другой счёт", "1% от суммы, при переводе").Activated += async (uimenu, index) =>
                    {
                        HideMenu();
                        int prefix = Convert.ToInt32(await Menu.GetUserInput("Префикс карты", null, 4));
                        int number = Convert.ToInt32(await Menu.GetUserInput("Номер карты", null, 10));
                        int sum = Convert.ToInt32(await Menu.GetUserInput(Lang.GetTextToPlayer("_lang_1"), null, 10));
                        if (number < 10000)
                        {
                            Notification.SendWithTime("~r~Должно быть больше 5 цифр");
                            return;
                        }
                        if (prefix < 1 || sum < 1)
                        {
                            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                            return;
                        }
                        if (sum > User.Data.money_bank)
                        {
                            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_2"));
                            return;
                        }
                        
                        TriggerServerEvent("ARP:TransferMoneyBank", prefix, number, sum);
                        User.StopScenario();
                    };
                    
                    menu.AddMenuItem(UiMenu, "Оплатить налог по номеру счёта").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        int score = Convert.ToInt32(await Menu.GetUserInput("Счёт", null, 10));
                        int num = Convert.ToInt32(await Menu.GetUserInput(Lang.GetTextToPlayer("_lang_1"), null, 8));
                        if (num > await User.GetBankMoney())
                        {
                            Notification.SendWithTime("~r~У вас на счёте недостаточно денег");
                            return;
                        }
                        TriggerServerEvent("ARP:PayTax", 1, num, score);
                        User.StopScenario();
                    };
                }
            }
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                {
                    User.StopScenario();
                    HideMenu();
                }
                if (item == backButton)
                    ShowPlayerMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowPlayerWalkietalkieMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Рация", "~b~Меню вашей рации");
            
            var list = new List<dynamic> {"Левый", "Оба", "Правый"};
            menu.AddMenuItemList(UiMenu, "Наушники", list).OnListSelected += (uimenu, idx) =>
            {
                User.Data.s_radio_balance = idx - 1;
                Client.Sync.Data.Set(User.GetServerId(), "s_radio_balance", User.Data.s_radio_balance);
                Notification.Send($"~g~Наушник установлен");
            };
            
            var listVol = new List<dynamic> {"0", "10", "20", "30", "40", "50", "60", "70", "80", "90", "100"};
            menu.AddMenuItemList(UiMenu, "Громкость", listVol).OnListSelected += (uimenu, idx) =>
            {
                User.Data.s_radio_vol = idx - 1;
                if (idx == 0)
                    User.Data.s_radio_vol = 0;
                else
                    User.Data.s_radio_vol = Convert.ToInt32(listVol[idx]) / 100f;
                Client.Sync.Data.Set(User.GetServerId(), "s_radio_vol", User.Data.s_radio_vol);
                Notification.Send($"~g~Громкость: {listVol[idx]}%");
            };
            
            menu.AddMenuItem(UiMenu, "Изменить частоту", $"Текущая частота: {User.Data.walkietalkie_num}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var num = Convert.ToInt32(await Menu.GetUserInput("До точки", null, 3));
                if (num > 520 || num < 30)
                {
                    Notification.SendWithTime("~r~Значение должно быть от 30 до 520");
                    return;
                }
                var num2 = Convert.ToInt32(await Menu.GetUserInput("После точки", null, 3));
                if (num2 < 0)
                {
                    Notification.SendWithTime("~r~Значение должно быть больше 0");
                    return;
                }
                User.Data.walkietalkie_num = num + "." + num2;
                Client.Sync.Data.Set(User.GetServerId(), "walkietalkie_num", User.Data.walkietalkie_num);
                Notification.SendWithTime("~g~Значение установлено: ~s~" + User.Data.walkietalkie_num);
            };
            
            menu.AddMenuItem(UiMenu, "Справка").Activated += (uimenu, item) =>
            {
                HideMenu();
                UI.ShowToolTip("~b~Справка\n~s~Говорить на кнопку ~INPUT_VEH_PUSHBIKE_SPRINT~.\nДоп клавиши зажимать не надо!");
            };
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowPlayerMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowPlayerPhoneSmsMenu(dynamic data, dynamic data2, dynamic data3, dynamic data4, string phone)
        {
            HideMenu();
            
            string smsList = "<li class=\"collection-item green-text\" act=\"newsms\" tabindex=\"0\">Написать СМС</li>";
            int i = 0;
            
            var tempData2 = (IDictionary<String, Object>) data2;
            var tempData3 = (IDictionary<String, Object>) data3;
            var tempData4 = (IDictionary<String, Object>) data4;
            string phoneOwner = $"{User.Data.phone_code}-{User.Data.phone}";

            foreach (var property in (IDictionary<String, Object>) data)
            {
                //menu.AddMenuItemList(UiMenu, $"{(phone != property.Value.ToString() ? $"~g~Входящее: ~s~{property.Value}" : $"~r~Исходящее: ~s~{tempData2[property.Key]}" )}", list, $"~b~Время:~s~ {tempData3[property.Key]}").OnListSelected += async (uimenu, idx) =>
                var phoneNumber = (phone != property.Value.ToString()
                    ? $"{property.Value}"
                    : $"{tempData2[property.Key]}");                
                
                var phoneInOrOut = phone != property.Value.ToString() ? "Входящее" : "Исходящее";
                smsList += $"<li class=\"collection-item\" act=\"smsinfo\" param1=\"{property.Key}\" tabindex=\"{++i}\">{phoneNumber}<br><label>{tempData3[property.Key]} / {phoneInOrOut}</label></li>";
            }

            smsList += $"<li class=\"collection-item green-text\" act=\"tomain\" tabindex=\"{++i}\">Назад</li>";
            //<li class="collection-item" tabindex="2">555-11111111 <a class="secondary-content"><i class="material-icons phone-sms-ico phone-sms-ico-out">chat_bubble</i></a></li>
            TriggerEvent("ARPPhone:AddSmsList", smsList);
            return;
            var menu = new Menu();
            UiMenu = menu.Create("Телефон", "~b~Список ваших СМС");

            if (phoneOwner == phone)
            {
                menu.AddMenuItem(UiMenu, "Написать смс").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    var number = await Menu.GetUserInput("Введите номер телефона", "", 15);
                    var text = await Menu.GetUserInput("Текст", "", 300);
                    if (text == "NULL") return;
                    Chat.SendMeCommand("достал телефон и отправил смс");
                    TriggerServerEvent("ARP:SendSms", Main.RemoveQuotes(number), Main.RemoveQuotes(text));
                    User.StopScenario();
                };
            }
            
            foreach (var property in (IDictionary<String, Object>) data)
            {   
                bool isNumberFromOwner = phone != property.Value.ToString();

                var list = new List<dynamic>
                {
                    isNumberFromOwner ? "Ответить" : "Eщё одно",
                    "Прочитать",
                    "Удалить"
                };

                menu.AddMenuItemList(UiMenu, $"{(phone != property.Value.ToString() ? $"~g~Входящее: ~s~{property.Value}" : $"~r~Исходящее: ~s~{tempData2[property.Key]}" )}", list, $"~b~Время:~s~ {tempData3[property.Key]}").OnListSelected += async (uimenu, idx) =>
                {
                    if (idx == 0)
                    {
                        HideMenu();
                        var sms = await Menu.GetUserInput("Текст", "", 300);
                        if (sms == "NULL") return;
                        Chat.SendMeCommand("отправил смс");
                        TriggerServerEvent("ARP:SendSms", Main.RemoveQuotes(isNumberFromOwner ? property.Value.ToString() : tempData2[property.Key].ToString()), Main.RemoveQuotes(sms));
                        User.StopScenario();
                    }
                    else if (idx == 2)
                    {
                        HideMenu();
                        TriggerServerEvent("ARP:DeleteSms", Convert.ToInt32(property.Key));
                        await Delay(500);
                        TriggerServerEvent("ARP:OpenSmsListMenu", phone);
                    }
                    else
                    {
                        UI.ShowToolTip($"~b~Номер:~s~ {property.Value}\n{tempData4[property.Key]}");
                    }
                };
            }
            
            /*foreach (var property in (IDictionary<String, Object>) data)
            {   
                menu.AddMenuItem(UiMenu, $"{(phone != property.Value.ToString() ? $"~g~Входящее: ~s~{property.Value}" : $"~r~Исходящее: ~s~{tempData2[property.Key]}" )}", $"~b~Время:~s~ {tempData3[property.Key]}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    TriggerServerEvent("ARP:OpenSmsInfoMenu", Convert.ToInt32(property.Key));
                };
            }*/

            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                {
                    HideMenu();
                    User.StopScenario();
                }
                if (item == backButton)
                    ShowPlayerPhoneMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowPlayerPhoneSmsInfoMenu(int id, string numberFrom, string numberTo, string text, string dateTime)
        {
            bool isNumberFromOwner = numberFrom == User.Data.phone_code + "-" + User.Data.phone;
            string smsItem = $"<div class=\"hide\" id=\"data-sms-text\">~b~Номер:~s~ {numberFrom}\n{text}</div>";
            smsItem += $"<li class=\"collection-item\" tabindex=\"0\">Отправитель: <label>{numberFrom}</label></li>";
            smsItem += $"<li class=\"collection-item\" tabindex=\"1\">Получатель: <label>{numberTo}</label></li>";
            smsItem += $"<li class=\"collection-item\" tabindex=\"2\">Время: <label>{dateTime}</label></li>";
            smsItem += "<li class=\"collection-item\" act=\"sms-read\" tabindex=\"3\">Прочитать</li>";
            if (isNumberFromOwner)
                smsItem += $"<li class=\"collection-item\" act=\"newsmswithnum\" param1=\"{numberTo}\" tabindex=\"4\">Написать ещё одно</li>";
            else
                smsItem += $"<li class=\"collection-item\" act=\"newsmswithnum\" param1=\"{numberFrom}\" tabindex=\"4\">Ответить</li>";
            smsItem += $"<li class=\"collection-item red-text\" act=\"sms-del\" param1=\"{id}\" tabindex=\"5\">Удалить</li>";
            smsItem += "<li class=\"collection-item green-text\" act=\"tomain\" tabindex=\"6\">Назад</li>";
            TriggerEvent("ARPPhone:ShowSmsItem", smsItem);
            return;
            
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Телефон", "~b~Список ваших СМС");
            
            menu.AddMenuItem(UiMenu, "~b~Отправитель: ~s~" + numberFrom);
            menu.AddMenuItem(UiMenu, "~b~Получатель: ~s~" + numberTo);
            menu.AddMenuItem(UiMenu, "~b~Время: ~s~" + dateTime);

            //bool isNumberFromOwner = numberFrom == User.Data.phone_code + "-" + User.Data.phone;
            
            menu.AddMenuItem(UiMenu, isNumberFromOwner ? "~g~Написать ещё одно" : "~g~Ответить").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var sms = await Menu.GetUserInput("Текст", "", 300);
                Chat.SendMeCommand("отправил смс");
                TriggerServerEvent("ARP:SendSms", Main.RemoveQuotes(isNumberFromOwner ? numberTo : numberFrom), Main.RemoveQuotes(sms));
                User.StopScenario();
            };
            
            menu.AddMenuItem(UiMenu, "~g~Прочитать").Activated += (uimenu, item) =>
            {
                HideMenu();
                UI.ShowToolTip($"~b~Номер:~s~ {numberFrom}\n{text}");
                //Chat.SendChatInfoMessage($"Номер: {numberFrom}", text);
                User.StopScenario();
            };
            
            menu.AddMenuItem(UiMenu, "~y~Удалить").Activated += (uimenu, item) =>
            {
                HideMenu();
                TriggerServerEvent("ARP:DeleteSms", id);
                User.StopScenario();
            };

            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                {
                    HideMenu();
                    User.StopScenario();
                }
                if (item == backButton)
                    ShowPlayerPhoneMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowPlayerPhoneContInfoMenu(int id, string title, string number)
        {
            string smsItem = $"<li class=\"collection-item\" tabindex=\"0\">{title}</li>";
            smsItem += $"<li class=\"collection-item\" tabindex=\"1\"><label>{number}</label></li>";
            smsItem += $"<li class=\"collection-item\" act=\"newsmswithnum\" param1=\"{number}\" tabindex=\"4\">Написать</li>";
            smsItem += $"<li class=\"collection-item red-text\" act=\"cont-del\" param1=\"{id}\" tabindex=\"5\">Удалить</li>";
            smsItem += "<li class=\"collection-item green-text\" act=\"tomain\" tabindex=\"6\">Назад</li>";
            TriggerEvent("ARPPhone:ShowContItem", smsItem);
        }

        public static void ShowPlayerPhoneBookMenu(dynamic data1, dynamic data2, string phone)
        {
            string smsList = "<li class=\"collection-item green-text\" act=\"newcont\" tabindex=\"0\">Новый контакт</li>";
            smsList += "<li class=\"collection-item\" act=\"911\" tabindex=\"1\">Экстренная служба<br><label>911</label></li>";

            if (User.IsCartel() && User.Data.rank > 4)
            {
                smsList += "<li class=\"collection-item\" act=\"misterk1\" tabindex=\"1\">Мистер К<br><label>Продажа человека</label></li>";
                smsList += "<li class=\"collection-item\" act=\"misterk2\" tabindex=\"1\">Мистер К<br><label>Снять розыск</label></li>";
            }
            
            int i = 0;
            var tempData1 = (IDictionary<String, Object>) data1;
            var tempData2 = (IDictionary<String, Object>) data2;
            smsList = ((IDictionary<String, Object>) data1).Aggregate(smsList, (current, property) => current + $"<li class=\"collection-item\" act=\"continfo\" param1=\"{property.Key}\" tabindex=\"{++i}\">{property.Value}<br><label>{tempData2[property.Key]}</label></li>");
            smsList += $"<li class=\"collection-item green-text\" act=\"tomain\" tabindex=\"{++i}\">Назад</li>";
            TriggerEvent("ARPPhone:AddContList", smsList);
            return;
            
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Телефон", "~b~Список ваших номеров");
            
            menu.AddMenuItem(UiMenu, "~g~Новый контакт").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var title = await Menu.GetUserInput("Заголовок");
                if (title == "NULL") return;
                var num = await Menu.GetUserInput("Номер");
                if (num == "NULL") return;
                TriggerServerEvent("ARP:AddContact", phone, title, num);
            };
            
            /*menu.AddMenuItem(UiMenu, "Механик", "~b~Телефон:~s~ 323-555-1111");
            menu.AddMenuItem(UiMenu, "Такси", "~b~Телефон:~s~ 323-555-5555");
            menu.AddMenuItem(UiMenu, "Адвокат", "~b~Телефон:~s~ 323-555-0001");*/
            menu.AddMenuItem(UiMenu, "Maze Bank", "~b~Телефон:~s~ 323-555-7777").Activated += (uimenu, item) =>
            {
                HideMenu();
                if (User.Data.bank_prefix == 1111)
                    Notification.SendPicture($"На карте средств: ${User.Data.money_bank:#,#}", "~r~Maze~s~ Bank", "Финансы", "CHAR_BANK_MAZE", Notification.TypeChatbox);
                else
                    Notification.SendPicture("Извините, Вы не обслуживаетесь в нашем банке :c", "~r~Maze~s~ Bank", "Ошибка", "CHAR_BANK_MAZE", Notification.TypeChatbox);
                User.StopScenario();
            };
            menu.AddMenuItem(UiMenu, "Fleeca Bank", "~b~Телефон:~s~ 323-555-4545").Activated += (uimenu, item) =>
            {
                HideMenu();
                if (User.Data.bank_prefix == 2222)
                    Notification.SendPicture($"На карте средств: ${User.Data.money_bank:#,#}", "~g~Fleeca~s~ Bank", "Финансы", "CHAR_BANK_FLEECA", Notification.TypeChatbox);
                else
                    Notification.SendPicture("Извините, Вы не обслуживаетесь в нашем банке :c", "~g~Fleeca~s~ Bank", "Ошибка", "CHAR_BANK_FLEECA", Notification.TypeChatbox);
                User.StopScenario();
            };
            menu.AddMenuItem(UiMenu, "Blaine Bank", "~b~Телефон:~s~ 323-555-9229").Activated += (uimenu, item) =>
            {
                HideMenu();
                if (User.Data.bank_prefix == 3333)
                    Notification.SendPicture($"На карте средств: ${User.Data.money_bank:#,#}", "~b~Blaine~s~ Bank", "Финансы", "CHAR_STEVE_TREV_CONF", Notification.TypeChatbox);
                else
                    Notification.SendPicture("Извините, Вы не обслуживаетесь в нашем банке :c", "~b~Blaine~s~ Bank", "Ошибка", "CHAR_STEVE_TREV_CONF", Notification.TypeChatbox);
                User.StopScenario();
            };
            menu.AddMenuItem(UiMenu, "Pacific Bank", "~b~Телефон:~s~ 323-555-9229").Activated += (uimenu, item) =>
            {
                HideMenu();
                if (User.Data.bank_prefix == 4444)
                    Notification.SendPicture($"На карте средств: ${User.Data.money_bank:#,#}", "~o~Pacific~s~ Bank", "Финансы", "CHAR_STEVE_MIKE_CONF", Notification.TypeChatbox);
                else
                    Notification.SendPicture("Извините, Вы не обслуживаетесь в нашем банке :c", "~o~Pacific~s~ Bank", "Ошибка", "CHAR_STEVE_MIKE_CONF", Notification.TypeChatbox);
                User.StopScenario();
            };
            
            menu.AddMenuItem(UiMenu, "Экстренная служба", "~b~Телефон:~s~ 911 \nПолиция, Медики, Пожарные").Activated += async (uimenu, item) =>
            {
                HideMenu();
                var text = await Menu.GetUserInput("Текст...", null, 50);
                if (text == "NULL") return;
                
                Notification.SendWithTime("~b~Сообщение было отправлено");
                Dispatcher.SendEms("Номер: " + User.Data.phone_code + "-" + User.Data.phone, text);
                User.StopScenario();
            };

            var list = new List<dynamic> {"Покупка", "Продажа", "Разное"};

            menu.AddMenuItemList(UiMenu, "Реклама Life Invader", list, "Стоимость ~g~$100").OnListSelected += async (uimenu, idx) =>
            {
                HideMenu();

                if (User.Data.money_bank < 100)
                {
                    Notification.SendWithTime("~r~У Вас недостаточно денег в банке");
                    return;
                }

                if (Sync.Data.HasLocally(User.GetServerId(), "isAdTimeout"))
                {
                    Notification.SendWithTime("~r~Таймаут 5 минуты");
                    return;
                }
                
                var text = await Menu.GetUserInput("Текст...", null, 150);
                if (text == "NULL") return;

                string type = "Разное";
                switch (idx)
                {
                    case 0:
                        type = "Покупка";
                        break;
                    case 1:
                        type = "Продажа";
                        break;
                }
                
                Notification.SendWithTime("~b~Сообщение было отправлено");
                
                User.RemoveBankMoney(100);
                Business.Business.AddMoney(92, 100);
                
                TriggerServerEvent("ARP:AddAd", text, User.Data.rp_name, $"{User.Data.phone_code}-{User.Data.phone}", type);

                text = (text.Length > 49) ? text.Remove(51) + "..." : text;
                
                Notification.SendPictureToAll(text, "~g~Реклама", $"{User.Data.phone_code}-{User.Data.phone} ({User.Data.id})", "CHAR_LIFEINVADER", Notification.TypeChatbox);
                
                Main.SaveLog("AD", $"{User.Data.rp_name} ({User.Data.id}) - {text}");
                
                Sync.Data.SetLocally(User.GetServerId(), "isAdTimeout", true);
                await Delay(300000);
                Sync.Data.ResetLocally(User.GetServerId(), "isAdTimeout");
                User.StopScenario();
            };
            
            menu.AddMenuItem(UiMenu, "Лотерея Life Invader", "Стоимость ~g~$7").Activated += async (uimenu, item) =>
            {
                HideMenu();

                if (User.Data.money_bank < 7)
                {
                    Notification.SendWithTime("~r~У Вас недостаточно денег в банке");
                    return;
                }

                if (Sync.Data.HasLocally(User.GetServerId(), "loto"))
                {
                    Notification.SendWithTime("~r~Лотерейный билетик может быть только 1");
                    return;
                }

                var number = Convert.ToInt32(await Menu.GetUserInput("Введите число", null, 2));

                if (number < 0 || number > 50)
                {
                    Notification.SendWithTime("~r~Число должно быть от 1 до 50");
                    return;
                }
                
                Sync.Data.SetLocally(User.GetServerId(), "loto", number);
                Notification.SendWithTime("~g~Вы купили билет с числом " + Sync.Data.GetLocally(User.GetServerId(), "loto"));
                User.StopScenario();
            };

            if (User.Data.business_id > 0)
            {
                menu.AddMenuItem(UiMenu, "~b~Arcadius ~s~Бизнес-центр", "~b~Телефон:~s~ 323-555-8765").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    var money = await Business.Business.GetMoney(User.Data.business_id);
                    Notification.SendPicture("На вашем счету: " + money, "Arcarius", "Ваш счёт", "CHAR_SOCIAL_CLUB", Notification.TypeChatbox);
                    User.StopScenario();
                };
            }
            if (User.IsCartel() && User.Data.rank > 4)
            {
                menu.AddMenuItem(UiMenu, "Мистер К", "Продажа человека").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    if (Managers.Weather.Hour < 22 && Managers.Weather.Hour > 4)
                    {
                        Notification.SendPicture("Набери мне с 22 до 4 утра", "Мистер К", "Дело", "CHAR_HUMANDEFAULT", Notification.TypeChatbox);
                        return;
                    }
                    
                    if (User.TimerAbduction > -1)
                    {
                        Notification.SendPicture("Сказал же, жди координат!", "Мистер К", "Дело", "CHAR_HUMANDEFAULT", Notification.TypeChatbox);
                        return;
                    }

                    User.TimerAbduction = 10;
                    Notification.SendPicture("Скоро скину координаты", "Мистер К", "Дело", "CHAR_HUMANDEFAULT", Notification.TypeChatbox);
                    Chat.SendChatMessageWithCommand("Через 30 реальных минут произойдет сделка");
                    
                    Notification.SendPictureToFraction($"Скоро произойдет сделка по продаже человека, за работу", "Диспетчер", "Похищение", "CHAR_CALL911", Notification.TypeChatbox, 3);
                    User.StopScenario();
                };
                menu.AddMenuItem(UiMenu, "Мистер К", "Снять розыск").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    var id = await Menu.GetUserInput("ID игрока", null, 6);
                    if (id == "NULL") return;
                    if (Client.Sync.Data.HasLocally(User.GetServerId(), "misterTimeout"))
                    {
                        Notification.SendWithTime("~r~Нельзя использовать так часто");
                        return;
                    }

                    Client.Sync.Data.SetLocally(User.GetServerId(), "misterTimeout", true);
                    
                    Shared.TriggerEventToAllPlayers("ARP:MisterK:ResetWanted", Convert.ToInt32(id), User.Data.id);
                    User.StopScenario();

                    await Delay(240000);
                    
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "misterTimeout");
                };
            }

            foreach (var property in tempData1)
            {
                var doList = new List<dynamic>
                {
                    "Написать",
                    "Удалить"
                };
                
                menu.AddMenuItemList(UiMenu, property.Value.ToString(), doList, $"~b~Номер: ~s~{tempData2[property.Key]}").OnListSelected += async (uimenu, idx) =>
                {
                    HideMenu();
                    if (idx == 0)
                    {
                        HideMenu();
                        var sms = await Menu.GetUserInput("Текст", "", 300);
                        if (sms == "NULL") return;
                        Chat.SendMeCommand("отправил смс");
                        TriggerServerEvent("ARP:SendSms", Main.RemoveQuotes(tempData2[property.Key].ToString()), Main.RemoveQuotes(sms));
                        User.StopScenario();
                    }
                    else
                    {
                        HideMenu();
                        TriggerServerEvent("ARP:DeleteContact", Convert.ToInt32(property.Key));
                        await Delay(500);
                        TriggerServerEvent("ARP:OpenContacntListMenu", phone);
                    }
                };
            }
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                {
                    HideMenu();
                    User.StopScenario();
                }
                if (item == backButton)
                    ShowPlayerPhoneMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowPlayerTaxiTaskMenu(CitizenFX.Core.Vehicle v)
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create("Такси", "~b~Список заказов");

            menu.AddMenuItem(UiMenu, "~b~Рация таксистов: ~s~").SetRightLabel("333.555");

            /*menu.AddMenuItem(UiMenu, "~y~Найти заказ").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Taxi.FindNpc();
            };*/

            menu.AddMenuItem(UiMenu, "~g~Вкл~s~ / ~r~выкл~s~ Поиск заказов", "Припаркуйтесь на оживлённой улице и ждите заказов от NPC").Activated += (uimenu, item) =>
            {
                HideMenu();
                if (UI.GetCurrentSpeed() > 1)
                {
                    Notification.SendWithTime("~r~Припаркуйтесь на оживлённой улице и ждите заказов от NPC");
                    return;
                }
                if (Business.Taxi.IsTask)
                {
                    Notification.SendWithTime("~r~Сначала завершите текущий заказ");
                    return;
                }
                Business.Taxi.IsFindNpc = true;
            };
            
            foreach (var property in Business.Taxi.TaskList)
            {   
                int uid = property.Value;
                string uphone = property.Key;

                menu.AddMenuItem(UiMenu, uphone, "Нажмите ~g~Enter~s~ чтобы принять заказ").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    if (!await Sync.Data.Has(uid, "GetTaxi"))
                    {
                        Notification.SendWithTime("~r~Заказ уже не актуален");
                        return;
                    }
                    
                    User.SetWaypoint(Business.Taxi.TaskList2[uphone].X, Business.Taxi.TaskList2[uphone].Y);
                    Shared.TriggerEventToPlayer(uid, "ARP:AcceptTaxi", $"{User.Data.phone_code}-{User.Data.phone}");
                    Notification.SendWithTime("~b~Вы приняли заказ");
                };
            }

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowBankMenu(int bankId, int price = 1)
        {
            HideMenu();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Банк во время блекаута не работает");
                return;
            }

            await Delay(300);
            
            var menu = new Menu();
            UiMenu = menu.Create("Банк", "Нажмите \"~g~Enter~s~\", чтобы выбрать пункт.");

            if (bankId == 0 && User.Data.bank_prefix == 1111 || bankId == 1 && User.Data.bank_prefix == 2222 || bankId == 2 && User.Data.bank_prefix == 3333 || bankId == 108 && User.Data.bank_prefix == 4444) {
                
                menu.AddMenuItem(UiMenu, "Снять средства").Activated += (uimenu, index) =>
                {
                    HideMenu();
                    if (CitizenFX.Core.UI.Screen.LoadingPrompt.IsActive)
                    {
                        Notification.SendWithTime("~r~Данное действие сейчас недоступно");
                        return;
                    }
                    Business.Bank.Widthdraw();
                };
                
                menu.AddMenuItem(UiMenu, "Положить средства").Activated += (uimenu, index) =>
                {
                    HideMenu();
                    if (CitizenFX.Core.UI.Screen.LoadingPrompt.IsActive)
                    {
                        Notification.SendWithTime("~r~Данное действие сейчас недоступно");
                        return;
                    }
                    Business.Bank.Deposit();
                };
    
                menu.AddMenuItem(UiMenu, "Баланс", $"Ваш баланс: ~g~${User.Data.money_bank:#,#}");
                menu.AddMenuItem(UiMenu, "Номер счёта", $"Номер карты: ~g~{User.Data.bank_prefix}-{User.Data.bank_number}");
                
                menu.AddMenuItem(UiMenu, "Перевести на другой счёт", "1% от суммы, при переводе").Activated += async (uimenu, index) =>
                {
                    HideMenu();
                    int prefix = Convert.ToInt32(await Menu.GetUserInput("Префикс карты", null, 4));
                    int number = Convert.ToInt32(await Menu.GetUserInput("Номер карты", null, 10));
                    int sum = Convert.ToInt32(await Menu.GetUserInput(Lang.GetTextToPlayer("_lang_1"), null, 10));
                    if (number < 10000)
                    {
                        Notification.SendWithTime("~r~Должно быть больше 5 цифр");
                        return;
                    }
                    if (prefix < 1 || sum < 1)
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                        return;
                    }
                    if (sum > User.Data.money_bank)
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                        return;
                    }
                    
                    TriggerServerEvent("ARP:TransferMoneyBank", prefix, number, sum);
                };

                if (bankId == 1 && User.Data.bank_prefix == 2222 || bankId == 2 && User.Data.bank_prefix == 3333 || bankId == 108 && User.Data.bank_prefix == 4444)
                {
                    menu.AddMenuItem(UiMenu, "~y~Изменить номер карты", "Цена: ~g~$100,000").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        int number = Convert.ToInt32(await Menu.GetUserInput("Номер карты", null, 8));
                        if (number < 10000)
                        {
                            Notification.SendWithTime("~r~Должно быть больше 5 цифр");
                            return;
                        }
                    
                        TriggerServerEvent("ARP:ChangeNumberCard", User.Data.bank_prefix, number);
                    };
                }
                
                menu.AddMenuItem(UiMenu, "~r~Закрыть счёт").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Business.Bank.CloseCard();
                };
            }
            else if (bankId == 0)
            {
                menu.AddMenuItem(UiMenu, "Оформить карту банка", "Оформить карту нужно в офисе Maze Bank").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    User.SetWaypoint(-75, -826);
                    Notification.SendWithTime("~r~Оформить карту нужно в офисе Maze Bank");
                };
            }
            else
            {
                var bData = await Business.Business.GetPriceCard1(bankId);
                menu.AddMenuItem(UiMenu, "Оформить карту банка", "Цена: ~g~$" + bData).Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Business.Bank.NewCard(bankId, bData);
                };
            }

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowBankAtmMenu()
        {
            HideMenu();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Банкомат во время блекаута не работает");
                return;
            }

            await Delay(300);
            
            User.PlayScenario("PROP_HUMAN_ATM");
            
            var menu = new Menu();
            UiMenu = menu.Create("Банк", "Нажмите \"~g~Enter~s~\", чтобы выбрать пункт.", true, true);

            if (User.Data.bank_prefix == 1111 || User.Data.bank_prefix == 2222 || User.Data.bank_prefix == 3333 || User.Data.bank_prefix == 4444) {
                
                menu.AddMenuItem(UiMenu, "Снять средства").Activated += (uimenu, index) =>
                {
                    HideMenu();
                    if (CitizenFX.Core.UI.Screen.LoadingPrompt.IsActive)
                    {
                        Notification.SendWithTime("~r~Данное действие сейчас недоступно");
                        return;
                    }
                    Business.Bank.Widthdraw(true);
                    User.PlayScenario("PROP_HUMAN_ATM");
                };
                
                menu.AddMenuItem(UiMenu, "Положить средства").Activated += (uimenu, index) =>
                {
                    HideMenu();
                    if (CitizenFX.Core.UI.Screen.LoadingPrompt.IsActive)
                    {
                        Notification.SendWithTime("~r~Данное действие сейчас недоступно");
                        return;
                    }
                    Business.Bank.Deposit(true);
                    User.PlayScenario("PROP_HUMAN_ATM");
                };
    
                menu.AddMenuItem(UiMenu, "Баланс", $"Ваш баланс: ~g~${User.Data.money_bank:#,#}");
                menu.AddMenuItem(UiMenu, "Номер счёта", $"Номер карты: ~g~{User.Data.bank_prefix}-{User.Data.bank_number}");

                if (User.Data.mp0_watchdogs > 30 && (User.Data.phone_code == 403 || User.Data.phone_code == 404))
                {
                    menu.AddMenuItem(UiMenu, "~y~Установить бекдор").Activated += async (uimenu, index) =>
                    {
                        HideMenu();
                        
                        if (new PlayerList().Count() < 10)
                        {
                            Notification.SendWithTime("~r~Онлайн на сервере должен быть не менее 10 человек");
                            return;
                        }
                        
                        if (CitizenFX.Core.UI.Screen.LoadingPrompt.IsActive)
                        {
                            Notification.SendWithTime("~r~Данное действие сейчас недоступно");
                            return;
                        }
                        
                        Client.Sync.Data.SetLocally(User.GetServerId(), "backdoor", true);
                        Notification.SendWithTime("~b~Backdoor connected");
                        
                        var rand = new Random();
                        if (User.Data.mp0_watchdogs < 50 && rand.Next(2) == 0)
                            Dispatcher.SendEms("Хакерская атака", $"Взлом банкомата с номера: {User.Data.phone_code}-{User.Data.phone}");
                        if (User.Data.mp0_watchdogs < 80 && rand.Next(3) == 0)
                            Dispatcher.SendEms("Хакерская атака", $"Взлом банкомата с номера: {User.Data.phone_code}-{User.Data.phone}");
                        if (User.Data.mp0_watchdogs < 101 && rand.Next(4) == 0)
                            Dispatcher.SendEms("Хакерская атака", $"Взлом банкомата с номера: {User.Data.phone_code}-{User.Data.phone}");

                        await Ctos.ExecuteFile("AtmBackdoor.sh", 50);
                        
                        User.PlayScenario("PROP_HUMAN_ATM");
                    };
                }
            }

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                {
                    HideMenu();
                    User.PlayScenario("PROP_HUMAN_ATM");
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        public static async void ShowHealWardrobeMenu()
        {
            HideMenu();
            

            await Delay(300);
            
            
            var menu = new Menu();
            UiMenu = menu.Create("Банк", "Нажмите \"~g~Enter~s~\", чтобы выбрать пункт.", true, true);

            menu.AddMenuItem(UiMenu, "Использовать набор первой помощи").Activated += (uimenu, item) => 
            {
                HideMenu();
                //foreach (CitizenFX.Core.Player p in Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), Convert.ToInt32(1)))
                //  Shared.TriggerEventToPlayer(p.ServerId, "ARP:UseAdrenalin");
                var pPos = GetEntityCoords(GetPlayerPed(-1), true);
                var player = Main.GetPlayerOnRadius(pPos, 1.2f);
                if (player == null)
                {
                    Notification.SendWithTime("~r~Рядом с вами никого нет");
                    return;
                }

                Shared.TriggerEventToPlayer(player.ServerId, "ARP:UseFirstAidKit");
                Chat.SendMeCommand("использовал набор первой помощи");
            };
            

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                {
                    HideMenu();
                    User.PlayScenario("PROP_HUMAN_ATM");
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        
        public static async void ShowBusinessMenu()
        {
            HideMenu();

            var data = await Business.Business.GetAllData(User.GetPlayerVirtualWorld());

            if (data.id == 0)
            {
                Notification.SendWithTime("~r~Ошибка загрузки бизнеса #3");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Arcadius", $"~b~Владелец: ~s~{(data.user_name == "" ? "Государство" : data.user_name)}");
            
            var bankList = new List<dynamic> {"Maze Bank (3%)", "Fleeca Bank (1%)", "Blaine Bank (1%)", "Pacific Bank (1%)"};
            var priceList = new List<dynamic> {"Очень низкая", "Низкая", "Нормальная", "Высокая", "Очень высокая"};
            var priceBuyCard1 = new List<dynamic> {"10", "20", "30", "40", "50", "70", "100", "150", "200", "300", "500"};
            var priceBuyCard2 = new List<dynamic>
            {
                "5000",
                "6000",
                "7000",
                "8000",
                "9000",
                "10000",
                "15000",
                "20000",
                "30000",
                "40000",
                "50000",
                "100000"
            };

            var nalog = Coffer.GetBizzNalog();

            if (data.type == 3)
                nalog = nalog + 20;
            if (data.type == 11)
                nalog = nalog + 20;
            
            menu.AddMenuItem(UiMenu, "~b~Название: ~s~").SetRightLabel(data.name);
            menu.AddMenuItem(UiMenu, "~b~Налог на бизнес: ~s~").SetRightLabel($"{nalog}%");

            if (User.Data.id == data.user_id)
            {
                menu.AddMenuItem(UiMenu, "~b~Банк: ~s~").SetRightLabel(data.bank.ToString("#,#"));
                
                
                menu.AddMenuItemList(UiMenu, "Ваш банк", bankList, "Банк который вас обслуживает").OnListSelected += (uimenu, idx) =>
                {
                    HideMenu();
                    Business.Business.SetTarif(data.id, idx);
                    Notification.SendWithTime($"~g~~b~Ваш новый банк: ~s~{bankList[idx]}");
                };
                
                var list = new List<dynamic>();
                for (int i = 0; i < 9; i++)
                    list.Add(i);
                var intList = menu.AddMenuItemList(UiMenu, "Сменить интерьер", list, "Цена: ~g~$50,000");

                intList.Index = data.interior;

                intList.OnListChanged += (uimenu, idx) =>
                {
                    Business.Business.LoadInterior(idx);
                };
                intList.OnListSelected += (uimenu, idx) =>
                {
                    HideMenu();
                    Business.Business.BuyInterior(data.id, idx);
                };

                switch (data.type)
                {
                    case 0:
                    {
                        menu.AddMenuItem(UiMenu, "Информация о клиентах").Activated += (uimenu, idx) =>
                        {
                            HideMenu();
                            switch (data.id)
                            {
                                case 1:
                                    TriggerServerEvent("ARP:SendPlayerBankClientInfo", 2222);
                                    break;
                                case 2:
                                    TriggerServerEvent("ARP:SendPlayerBankClientInfo", 3333);
                                    break;
                                case 108:
                                    TriggerServerEvent("ARP:SendPlayerBankClientInfo", 4444);
                                    break;
                            }
                        };
                        menu.AddMenuItemList(UiMenu, "Цена за покупку карты", priceBuyCard1).OnListSelected += (uimenu, idx) =>
                        {
                            HideMenu();
                            Business.Business.SetPriceBuyCard1(data.id, Convert.ToInt32(priceBuyCard1[idx]));
                            Notification.SendWithTime("~g~Вы изменили цену за покупку карты");
                        };
                        menu.AddMenuItemList(UiMenu, "Цена за обслуживание", priceList, "4$ = 100%").OnListSelected += (uimenu, idx) =>
                        {
                            HideMenu();
                            Business.Business.SetPrice(data.id, idx);
                            Notification.SendWithTime("~g~Вы изменили цену за обслуживание");
                        };
                        break;
                    }
                    case 1:
                    case 5:
                    case 7:
                    case 11:
                    {
                        if (data.id != 120 && data.id != 124)
                        {
                            menu.AddMenuItemList(UiMenu, "Цены на весь товар", priceList).OnListSelected += (uimenu, idx) =>
                            {
                                HideMenu();
                                Business.Business.SetPrice(data.id, idx);
                                Notification.SendWithTime("~g~Вы изменили цену на товар");
                            };
                        }
                        break;
                    }
                    case 4:
                    {
                        menu.AddMenuItemList(UiMenu, "Цена за аренду", priceList).OnListSelected += (uimenu, idx) =>
                        {
                            HideMenu();
                            Business.Business.SetPrice(data.id, idx);
                            Notification.SendWithTime("~g~Вы изменили цену за аренду");
                        };
                        break;
                    }
                    case 10:
                    {
                        menu.AddMenuItem(UiMenu, "Сменить название").Activated += async (uimenu, item) =>
                        {
                            HideMenu();
                            string name = await Menu.GetUserInput("Название", null, 30);
                            if (name == "NULL")
                                return;
                    
                            Business.Business.SetName(data.id, Main.RemoveQuotes(name));
                            Notification.SendWithTime("~g~Вы установили название для офиса");
                            if (User.Data.fraction_id2 > 0 && User.Data.rank2 == 11)
                                TriggerServerEvent("ARP:RenameUnofFraction", Main.RemoveQuotes(name), User.Data.fraction_id2);
                        };
                        break;
                    }
                }
                
                menu.AddMenuItem(UiMenu, "Положить средства").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    if (CitizenFX.Core.UI.Screen.LoadingPrompt.IsActive)
                    {
                        Notification.SendWithTime("~r~Данное действие сейчас недоступно");
                        return;
                    }
                    int money = Convert.ToInt32(await Menu.GetUserInput(Lang.GetTextToPlayer("_lang_1"), null, 10));
                    if (money < 0)
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                        return;
                    }
                    
                    if (User.Data.money < money)
                    {
                        Notification.SendWithTime("~r~У Вас нет такой суммы на руках");
                        return;
                    }
                    
                    User.RemoveCashMoney(money);
                    Business.Business.AddMoney(data.id, money);
                    Notification.SendWithTime("~g~Вы положили деньги в бизнес");
                };
                
                menu.AddMenuItem(UiMenu, "Снять средства").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    if (CitizenFX.Core.UI.Screen.LoadingPrompt.IsActive)
                    {
                        Notification.SendWithTime("~r~Данное действие сейчас недоступно");
                        return;
                    }
                    int money = Convert.ToInt32(await Menu.GetUserInput(Lang.GetTextToPlayer("_lang_1"), null, 10));
                    if (money < 0)
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                        return;
                    }
                    
                    if (await Business.Business.GetMoney(data.id) < money)
                    {
                        Notification.SendWithTime("~r~У Вас нет такой суммы на руках");
                        return;
                    }

                    if (data.type == 10)
                    {
                        User.AddCashMoney(money);
                        Business.Business.RemoveMoney(data.id, money);
                        Notification.SendWithTime("~g~Вы сняли деньги с бизнеса");
                    }
                    else 
                    {
                        var bankTarif = 2;
                        if (data.tarif != 0)
                            bankTarif = 1;

                        switch (data.tarif) {
                            case 0:
                                Coffer.AddMoney(money * (bankTarif / 100));
                                break;
                            case 1:
                                Business.Business.AddMoney(1, money * (bankTarif / 100));
                                break;
                            case 2:
                                Business.Business.AddMoney(2, money * (bankTarif / 100));
                                break;
                            case 3:
                                Business.Business.AddMoney(108, money * (bankTarif / 100));
                                break;
                        }
                        
                        User.AddCashMoney(money * (100 - nalog - bankTarif) / 100);
                        Coffer.AddMoney(money * nalog / 100);
                        Business.Business.RemoveMoney(data.id, money);
                        Notification.SendWithTime("~g~Вы сняли деньги с бизнеса с учетом налога ~y~" + nalog + bankTarif + "%");
                        Notification.SendWithTime($"~b~{bankTarif}% от суммы отправлен банку который вас обслуживает");
                    }
                };
                
                if (User.Data.fraction_id2 == 0)
                {
                    menu.AddMenuItem(UiMenu, "~y~Создать организацию").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        TriggerServerEvent("ARP:CreateUnofFraction", Main.RemoveQuotes(data.name));
                    };
                }
                else if (User.Data.fraction_id2 > 0 && User.Data.rank2 == 11)
                {
                    menu.AddMenuItem(UiMenu, "~r~Расформировать организацию").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Notification.SendWithTime("~b~Подтвердите действие, команда /rasform");
                    };
                }
                
                menu.AddMenuItem(UiMenu, "~r~Продать", $"Цена: ~g~${(data.price * (100 - Coffer.GetNalog()) / 100):#,#}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    if (User.Data.fraction_id2 > 0 && User.Data.rank2 == 11)
                    {
                        Notification.SendWithTime("~r~Для начала расформируйте организацию");
                        return;
                    }
                    ShowAskSellBMenu();
                };
                
                menu.AddMenuItem(UiMenu, "~y~Продать бизнес игроку", $"~b~{data.name}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    if (User.Data.fraction_id2 > 0 && User.Data.rank2 == 11)
                    {
                        Notification.SendWithTime("~r~Для начала расформируйте организацию");
                        return;
                    }
                    ShowPlayerSellBusinessMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f), User.Data.business_id, data.name);
                };
            }
            else if (data.user_id == 0)
            {
                menu.AddMenuItem(UiMenu, "~g~Купить", $"Цена: ~g~${data.price:#,#}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Business.Business.Buy(data.id);
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowLicBuyMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Агенство", "~b~Купить лицензию");
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Связь во время блекаута не работает");
                return;
            }
            
            menu.AddMenuItem(UiMenu, "Категория A", "Цена: ~g~$75").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Lic.BuyLic(0);
            };

            menu.AddMenuItem(UiMenu, "Категория B", "Цена: ~g~$200").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Lic.BuyLic(1);
            };

            menu.AddMenuItem(UiMenu, "Категория C", "Цена: ~g~$600").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Lic.BuyLic(2);
            };

            menu.AddMenuItem(UiMenu, "Пилот самолёта", "Цена: ~g~$1200").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Lic.BuyLic(3);
            };
            menu.AddMenuItem(UiMenu, "Пилот вертолёта", "Цена: ~g~$1400").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Lic.BuyLic(5);
            };

            menu.AddMenuItem(UiMenu, "Водный транспорт", "Цена: ~g~$920").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Lic.BuyLic(4);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowEmsGarderobMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Гардероб", "~b~Гардероб EMS");

            var list = new List<dynamic> {"Civil", "Paramedic #1", "Paramedic #2"};
            //list.Add("SWAT");
            
            menu.AddMenuItemList(UiMenu, "Форма", list).OnListChanged += (uimenu, idx) =>
            {
                Fractions.Ems.Garderob(idx);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowEmsFIreGarderobMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Арсенал", "~b~Арсенал SAFD");

            var list = new List<dynamic> {"Civil", "SAFD #1", "SAFD #2"};            
            menu.AddMenuItemList(UiMenu, "Форма", list).OnListChanged += (uimenu, idx) =>
            {
                Fractions.Ems.GarderobFire(idx);
            };
            
            menu.AddMenuItem(UiMenu, "Сухпаёк").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.TakeNewItem(32);
                Main.AddFractionGunLog(User.Data.rp_name, "Сухпаёк", User.Data.fraction_id);
            };

            menu.AddMenuItem(UiMenu, "Фонарик").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                User.GiveWeapon((uint) WeaponHash.Flashlight, 1, false, false);
                Notification.SendWithTime("~b~Вы взяли фонарик");
                Main.AddFractionGunLog(User.Data.rp_name, "Фонарик", User.Data.fraction_id);
            };

            menu.AddMenuItem(UiMenu, "Огнетушитель").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                User.GiveWeapon((uint) WeaponHash.FireExtinguisher, 1, false, false);
                Notification.SendWithTime("~b~Вы взяли огнетушитель");
                Main.AddFractionGunLog(User.Data.rp_name, "Огнетушитель", User.Data.fraction_id);
            };

            menu.AddMenuItem(UiMenu, "Лом").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                User.GiveWeapon((uint) WeaponHash.Crowbar, 1, false, false);
                Notification.SendWithTime("~b~Вы взяли лом");
                Main.AddFractionGunLog(User.Data.rp_name, "Лом", User.Data.fraction_id);
            };

            menu.AddMenuItem(UiMenu, "Гаечный ключ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                User.GiveWeapon((uint) WeaponHash.Wrench, 1, false, false);
                Notification.SendWithTime("~b~Вы взяли гаечный ключ");
                Main.AddFractionGunLog(User.Data.rp_name, "Гаечный ключ", User.Data.fraction_id);
            };

            menu.AddMenuItem(UiMenu, "Топор").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                User.GiveWeapon((uint) WeaponHash.Hatchet, 1, false, false);
                Notification.SendWithTime("~b~Вы взяли топор");
                Main.AddFractionGunLog(User.Data.rp_name, "Топор", User.Data.fraction_id);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowSapdGarderobMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Гардероб", "~b~Гардероб SAPD");

            var list = new List<dynamic> {"Civil", "Cadet", "Officer", "Air Support Division", "NOOSE Black", "NOOSE", "Detective", "Highway Patrol"};            
            if (User.Skin.SEX == 1)
                list = new List<dynamic> {"Civil", "Cadet", "Officer", "Air Support Division", "NOOSE Black", "NOOSE", "Detective"};
            
            menu.AddMenuItemList(UiMenu, "Форма", list).OnListChanged += (uimenu, idx) =>
            {
                Fractions.Sapd.Garderob(idx);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowSheriffGarderobMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Гардероб", "~b~Гардероб Sheriff");

            var list = new List<dynamic> {"Civil", "Officer #1", "Officer #2", "Air Support Division", "Tactical Division"};            
            menu.AddMenuItemList(UiMenu, "Форма", list).OnListChanged += (uimenu, idx) =>
            {
                Fractions.Sapd.GarderobSheriff(idx);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowGiveWantedMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Розыск", "~b~Выдача розыска");

            var list = new List<dynamic> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            
            foreach (Player p in new PlayerList())
            {
                if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;   
                
                menu.AddMenuItemList(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}", list).OnListSelected += async (uimenu, idx) =>
                {
                    HideMenu();

                    var reason = await Menu.GetUserInput("Причина", null, 32);

                    if ((int) await Sync.Data.Get(Convert.ToInt32(p.ServerId), "jail_time") > 0)
                    {
                        Notification.SendWithTime("~r~Игрок уже в тюрьме");
                        return;
                    }
                    
                    int addLvl = ++idx;
                    int lvl = (int) await Sync.Data.Get(Convert.ToInt32(p.ServerId), "wanted_level");
                    Sync.Data.Set(Convert.ToInt32(p.ServerId), "wanted_level", lvl + addLvl > 10 ? 10 : addLvl);
                    Sync.Data.Set(Convert.ToInt32(p.ServerId), "wanted_reason", reason);

                    TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "Вам выдали розыск", Convert.ToInt32(p.ServerId));
                    
                    Notification.SendPictureToDep($"Выдал розыск {User.PlayerIdList[p.ServerId.ToString()]}. Уровень: {idx}", "Диспетчер", User.Data.rp_name, "CHAR_CALL911", Notification.TypeChatbox);
                    Notification.SendWithTime($"~g~Вы выдали розыск {User.PlayerIdList[p.ServerId.ToString()]}. Уровень: {idx}");
                    Chat.SendMeCommand("объявляет подозреваемого в розыск по рации");
                };
            }

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowFibArsenalMenu()
        {
            HideMenu();
            return;
            
            var menu = new Menu();
            UiMenu = menu.Create("FIB", "~b~Арсенал FIB");
            
            menu.SetMenuBannerSprite(UiMenu, "shopui_title_gr_gunmod", "shopui_title_gr_gunmod");
            
            menu.AddMenuItem(UiMenu, "Сухпаёк").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.AddItemServer(32, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                Managers.Inventory.UpdateAmount(User.Data.id, InventoryTypes.Player);
                Notification.SendWithTime("~b~Вы взяли сухпаёк");
                Main.AddFractionGunLog(User.Data.rp_name, "Сухпаёк", User.Data.fraction_id);
            };

            menu.AddMenuItem(UiMenu, "Наручники").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.AddItemServer(40, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                Managers.Inventory.UpdateAmount(User.Data.id, InventoryTypes.Player);
                Notification.SendWithTime("~b~Вы взяли наручники");
                Main.AddFractionGunLog(User.Data.rp_name, "Наручники", User.Data.fraction_id);
            };

            menu.AddMenuItem(UiMenu, "Фонарик").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.GiveWeapon((uint) WeaponHash.Flashlight, 1, false, false);
                Notification.SendWithTime("~b~Вы взяли фонарик");
                Main.AddFractionGunLog(User.Data.rp_name, "Фонарик", User.Data.fraction_id);
            };
            
            menu.AddMenuItem(UiMenu, "Дубинка").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.GiveWeapon((uint) WeaponHash.Nightstick, 1, false, false);
                Notification.SendWithTime("~b~Вы взяли дубинку");
                Main.AddFractionGunLog(User.Data.rp_name, "Дубинка", User.Data.fraction_id);
            };
            
            menu.AddMenuItem(UiMenu, "Электрошокер").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.GiveWeapon((uint) WeaponHash.StunGun, 500, false, false);
                Notification.SendWithTime("~b~Вы взяли электрошокер");
                Main.AddFractionGunLog(User.Data.rp_name, "Электрошокер", User.Data.fraction_id);
            };
            
             menu.AddMenuItem(UiMenu, "Beretta 90Two").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.GiveWeapon((uint) WeaponHash.PistolMk2, 120, false, false);
                Notification.SendWithTime("~b~Вы взяли Beretta 90Two");
                Main.AddFractionGunLog(User.Data.rp_name, "Beretta 90Two", User.Data.fraction_id);
            };
            
            menu.AddMenuItem(UiMenu, "Benelli M3").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.GiveWeapon((uint) WeaponHash.PumpShotgun, 16, false, false);
                Notification.SendWithTime("~b~Вы взяли Benelli M3");
                Main.AddFractionGunLog(User.Data.rp_name, "Benelli M3", User.Data.fraction_id);
            };
            
            menu.AddMenuItem(UiMenu, "MP5A3").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.GiveWeapon((uint) WeaponHash.SMG, 450, false, false);
                SetWeaponObjectTintIndex((int) WeaponHash.SMG, (int) WeaponTint.LSPD);
                Notification.SendWithTime("~b~Вы взяли MP5A3");
                Main.AddFractionGunLog(User.Data.rp_name, "MP5A3", User.Data.fraction_id);
            };
            
            menu.AddMenuItem(UiMenu, "HK-416").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.GiveWeapon((uint) WeaponHash.CarbineRifle, 450, false, false);
                Notification.SendWithTime("~b~Вы взяли HK-416");
                Main.AddFractionGunLog(User.Data.rp_name, "HK-416", User.Data.fraction_id);
            };
            
            menu.AddMenuItem(UiMenu, "L115A3").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.GiveWeapon((uint) WeaponHash.SniperRifle, 20, false, false);
                Notification.SendWithTime("~b~Вы взяли L115A3");
                Main.AddFractionGunLog(User.Data.rp_name, "L115A3", User.Data.fraction_id);
            };
            
            menu.AddMenuItem(UiMenu, "Дымовая граната").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.GiveWeapon((uint) WeaponHash.SmokeGrenade, 3, false, false);
                Notification.SendWithTime("~b~Вы взяли дымовую гранату");
                Main.AddFractionGunLog(User.Data.rp_name, "Дымовая граната", User.Data.fraction_id);
            };
            
           /* menu.AddMenuItem(UiMenu, "Газовая граната").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.GiveWeapon((uint) WeaponHash.BZGas, 3, false, false);
                Notification.SendWithTime("~b~Вы взяли газовую гранату");
                Main.AddFractionGunLog(User.Data.rp_name, "Газовая граната", User.Data.fraction_id);
            };  */
            
           menu.AddMenuItem(UiMenu, "Сдать оружие").Activated += (uimenu, item) =>
           {
               HideMenu();
               TriggerEvent("ARP:TakeAllGunsSAPD", User.GetServerId());
           };
           
            menu.AddMenuItem(UiMenu, "Парашют").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.GiveWeapon((uint) WeaponHash.Parachute, 1, false, false);
                Notification.SendWithTime("~b~Вы взяли парашют");
                Main.AddFractionGunLog(User.Data.rp_name, "Парашют", User.Data.fraction_id);
            };

            menu.AddMenuItem(UiMenu, "Бронежилет").Activated += (uimenu, item) =>
            {
                HideMenu();
                SetPedArmour(GetPlayerPed(-1), 100);
                Notification.SendWithTime("~b~Вы взяли бронежилет");
                Main.AddFractionGunLog(User.Data.rp_name, "Бронежилет", User.Data.fraction_id);
            };
            
            var list = new List<dynamic> {"Classic", "HRT Black", "HRT Desert"};
            //list.Add("SWAT");
            
            menu.AddMenuItemList(UiMenu, "Форма", list).OnListChanged += (uimenu, idx) =>
            {
                Fractions.Fib.Garderob(idx);
            };
            
            /*var list = (from v in Managers.Vehicle.VehicleInfoGlobalDataList where v.FractionId != 0 && v.FractionId == User.Data.fraction_id select v.Number).Cast<dynamic>().ToList();
            menu.AddMenuItemList(UiMenu, "Ключи", list, "Нажмите \"~g~Enter~s~\" чтобы взять ключи").OnListSelected += (uimenu, idx) =>
            {
                HideMenu();
                User.AddVehicleKey(list[idx].ToString());
                Notification.SendWithTime($"~b~Вы взяли ключи для ТС с номером: {list[idx]}");
            };*/
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowSapdClearOrUnjailMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("PC", "~b~Меню PC");
            
            menu.AddMenuItem(UiMenu, "Выпустить из тюрьмы").Activated += (uimenu, item) =>
            {
                HideMenu();
                if (User.Data.rank < 4)
                {
                    Notification.SendWithTime("~r~Вам не доступно по рангу");
                    return;
                }
                ShowSapdUnjailMenu();
            };
            
            menu.AddMenuItem(UiMenu, "Очистить розыск").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowSapdClearMenu();
            };

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowSapdUnjailMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("SAPD", "~b~Выпустить из тюрьмы");
            
            foreach (Player p in new PlayerList())
            {
                if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;

                if ((int) await Sync.Data.Get(p.ServerId, "jail_time") > 0)
                {
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += (uimenu, idx) =>
                    {
                        HideMenu();
                        Shared.TriggerEventToPlayer(p.ServerId, "ARP:UnjailPlayer");
                        Notification.SendPictureToDep($"{User.PlayerIdList[p.ServerId.ToString()]} - выпущен из тюрьмы", "Диспетчер", User.Data.rp_name, "CHAR_CALL911", Notification.TypeChatbox);
                        Main.SaveLog("UnJail", $"{User.Data.rp_name} unjail {User.PlayerIdList[p.ServerId.ToString()]}");
                    };
                }
            }

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowSapdClearMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("SAPD", "~b~Очистить розыск");
            
            foreach (Player p in new PlayerList())
            {
                if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;   
                
                menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += (uimenu, idx) =>
                {
                    HideMenu();
                    
                    Sync.Data.Set(Convert.ToInt32(p.ServerId), "wanted_level", 0);
                    TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "Вам очистили розыск", Convert.ToInt32(p.ServerId));
                    Notification.SendPictureToDep($"Снял розыск {User.PlayerIdList[p.ServerId.ToString()]}", "Диспетчер", User.Data.rp_name, "CHAR_CALL911", Notification.TypeChatbox);
                    Notification.SendWithTime($"~g~Вы очистили розыск {User.PlayerIdList[p.ServerId.ToString()]}");
                };
            }

            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowSapdGiveTicketMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("SAPD", "~b~Выписать штраф");
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += async (uimenu, item) =>
                    {
                        HideMenu();

                        var wantedLevel = Convert.ToInt32((int) await Sync.Data.Get(p.ServerId, "wanted_level"));
                        
                        if (wantedLevel == 0)
                        {
                            Notification.SendWithTime("~r~У игрока нет розыска");
                            return;
                        }
                        
                        string reason = await Menu.GetUserInput("Причина");
                        TriggerServerEvent("ARP:SendServerToPlayerJail", reason, wantedLevel * 200, p.ServerId);
                        Notification.SendWithTime($"~y~Вы посадили {User.PlayerIdList[p.ServerId.ToString()]} в тюрьму");
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        public static void ShowGovTakeList(int serverId)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Изъятие", $"~b~Изъятие у игрока с ID: {User.PlayerIdList[serverId.ToString()]}");
            
            menu.AddMenuItem(UiMenu, "Изъять лицензию категории А").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "a_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            
            menu.AddMenuItem(UiMenu, "Изъять лицензию категории B").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "b_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            
            menu.AddMenuItem(UiMenu, "Изъять лицензию категории C").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "c_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            
            menu.AddMenuItem(UiMenu, "Изъять лицензию на пил. самолёта").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "air_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            menu.AddMenuItem(UiMenu, "Изъять лицензию на пил. вертолёта").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "heli_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            
            menu.AddMenuItem(UiMenu, "Изъять лицензию на водный транспорт").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "ship_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            
            menu.AddMenuItem(UiMenu, "Изъять лицензию на оружие").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "gun_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            menu.AddMenuItem(UiMenu, "Изъять лицензию Таксиста").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "taxi_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            menu.AddMenuItem(UiMenu, "Изъять лицензию Адвоката").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "law_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            menu.AddMenuItem(UiMenu, "Изъять лицензию на Бизнес").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "biz_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            menu.AddMenuItem(UiMenu, "Изъять лицензию на Охоту").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "animal_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
			menu.AddMenuItem(UiMenu, "Изъять лицензию на Рыбалку").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "fish_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowSapdTakeList(int serverId)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Изъятие", $"~b~Изъятие у игрока с ID: {User.PlayerIdList[serverId.ToString()]}");
            
            menu.AddMenuItem(UiMenu, "Изъять лицензию категории А").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "a_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            
            menu.AddMenuItem(UiMenu, "Изъять лицензию категории B").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "b_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            
            menu.AddMenuItem(UiMenu, "Изъять лицензию категории C").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "c_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            
            menu.AddMenuItem(UiMenu, "Изъять лицензию на пил. самолёта").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "air_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            menu.AddMenuItem(UiMenu, "Изъять лицензию на пил. вертолёта").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "heli_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            
            menu.AddMenuItem(UiMenu, "Изъять лицензию на водный транспорт").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "ship_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            
            menu.AddMenuItem(UiMenu, "Изъять лицензию на оружие").Activated += (uimenu, item) =>
            {
                HideMenu();
                Sync.Data.Set(serverId, "gun_lic", false);
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли лицензию", serverId);
                Notification.SendWithTime($"~y~Вы изъяли лицензию");
                Chat.SendMeCommand("обыскал человека напротив и изъял какие-то бумаги");
            };
            
            menu.AddMenuItem(UiMenu, "Изъять оружие", "Так же изымается награбленное, офицеру за это дается премия").Activated += (uimenu, item) =>
            {
                HideMenu();
                Shared.TriggerEventToPlayer(serverId, "ARP:TakeAllGuns");
                TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "У Вас изъяли оружие", serverId);
                Notification.SendWithTime($"~y~Вы изъяли оружие");
                Chat.SendMeCommand("обыскал человека напротив и изъял оружие");
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowGoHospMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("EMS", "~b~Госпитализировать");
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Shared.TriggerEventToPlayer(p.ServerId, "ARP:HospPlayer");
                        Notification.SendWithTime($"~y~Вы госпитализировали человека");
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowGiveMargLicMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Рецепт", "~b~Выдать рецепт марихуаны");
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += (uimenu, item) =>
                    {
                        HideMenu();

                        Sync.Data.Set(p.ServerId, "allow_marg", true);
                        TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "Вам выдали рецепт марихуаны", p.ServerId);
                        Notification.SendWithTime($"~y~Вы выдали рецепт марихуаны");
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowGiveMedLicMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Страховка", "~b~Выдать мед. страховку");
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += (uimenu, item) =>
                    {
                        HideMenu();

                        Sync.Data.Set(p.ServerId, "med_lic", true);
                        TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "Вам выдали мед. страховку", p.ServerId);
                        Notification.SendWithTime($"~y~Вы выдали мед. страховку");
                        
                        Main.AddFractionGunLog(User.Data.rp_name, "Выдал мед. страховку", User.Data.fraction_id);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowGovGiveLawLicMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Правительство", "~b~Выдать лицензию адвоката");
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += (uimenu, item) =>
                    {
                        HideMenu();

                        Sync.Data.Set(p.ServerId, "law_lic", true);
                        TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "Вам выдали лицензию адвоката", p.ServerId);
                        Notification.SendWithTime($"~y~Вы выдали лицензию на адвоката");
                        
                        Main.AddFractionGunLog(User.Data.rp_name, "Выдал лицензию на адвоката", User.Data.fraction_id);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowSapdGiveGunLicMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("SAPD", "~b~Выдать лицензию на оружие");
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += (uimenu, item) =>
                    {
                        HideMenu();

                        Sync.Data.Set(p.ServerId, "gun_lic", true);
                        TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "Вам выдали лицензию на оружие", p.ServerId);
                        Notification.SendWithTime($"~y~Вы выдали лицензию на оружие");
                        
                        Main.AddFractionGunLog(User.Data.rp_name, "Выдал лицензию на оружие", User.Data.fraction_id);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        public static void ShowEms2LicenseMenu()
        {
            if (User.IsEms())
            {
                HideMenu();

                var menu = new Menu();
                UiMenu = menu.Create("Выдача справки", "~b~Меню");
        
                menu.AddMenuItem(UiMenu, "Выдать справку о псих. здоровье").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    ShowEmsGivePsyLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 2f));
                };
        
        
                var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
        
                UiMenu.OnItemSelect += (sender, item, index) =>
                { 
                    if (item == closeButton) 
                        HideMenu();
                };
        
                MenuPool.Add(UiMenu);
        
            }
        }
        public static void ShowEmsGivePsyLicMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("EMS", "~b~Выдать справку о психическом здоровье ");
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += (uimenu, item) =>
                    {
                        HideMenu();

                        Sync.Data.Set(p.ServerId, "psy_lic", true);
                        TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "Вам выдали справку о псих. здоровье", p.ServerId);
                        Notification.SendWithTime($"~y~Вы выдали справку о псих. здоровье");
                        
                        Main.AddFractionGunLog(User.Data.rp_name, "Выдал справку о псих. здоровье", User.Data.fraction_id);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowGovGiveBizzLicMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Правительство", "~b~Выдать лицензию на бизнес");
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += (uimenu, item) =>
                    {
                        HideMenu();

                        Sync.Data.Set(p.ServerId, "biz_lic", true);
                        TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "Вам выдали лицензию на бизнес", p.ServerId);
                        Notification.SendWithTime($"~y~Вы выдали лицензию на бизнес");
                        
                        Main.AddFractionGunLog(User.Data.rp_name, "Выдал лицензию на бизнес", User.Data.fraction_id);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowGovGiveAnimalLicMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Правительство", "~b~Выдать лицензию на охоту");
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += async (uimenu, item) =>
                    {
                        HideMenu();

                        if (!(bool) await Sync.Data.Get(p.ServerId, "gun_lic"))
                        {
                            Notification.SendWithTime($"~y~У игрока должна быть лицензия на оружие");
                            return;
                        }

                        Sync.Data.Set(p.ServerId, "animal_lic", true);
                        TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "Вам выдали лицензию на охоту", p.ServerId);
                        Notification.SendWithTime($"~y~Вы выдали лицензию на охоту");
                        
                        Main.AddFractionGunLog(User.Data.rp_name, "Выдал лицензию на охоту", User.Data.fraction_id);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowGovGiveFishLicMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Правительство", "~b~Выдать лицензию на рыбалку");
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += (uimenu, item) =>
                    {
                        HideMenu();

                        Sync.Data.Set(p.ServerId, "fish_lic", true);
                        TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "Вам выдали лицензию на рыбалку", p.ServerId);
                        Notification.SendWithTime($"~y~Вы выдали лицензию на рыбалку");
                        
                        Main.AddFractionGunLog(User.Data.rp_name, "Выдал лицензию на рыбалку", User.Data.fraction_id);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowSapdArrestMenu(List<Player> pedList)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("PC", "~b~Посадить в тюрьму");
            
            foreach (Player p in pedList)
            {
                try
                {
                    if (p.ServerId == GetPlayerServerId(PlayerId())) continue;
                    if (!User.PlayerIdList.ContainsKey(p.ServerId.ToString())) continue;
                    menu.AddMenuItem(UiMenu, $"~b~ID: ~s~{User.PlayerIdList[p.ServerId.ToString()]}").Activated += async (uimenu, item) =>
                    {
                        HideMenu();

                        var wantedLevel = Convert.ToInt32((int) await Sync.Data.Get(p.ServerId, "wanted_level"));
                        
                        if (wantedLevel == 0)
                        {
                            Notification.SendWithTime("~r~У игрока нет розыска");
                            return;
                        }

                        int premia = 100 * wantedLevel;
                        string reason = await Menu.GetUserInput("Причина");
                        TriggerServerEvent("ARP:SendServerToPlayerJail", reason, wantedLevel * 600, p.ServerId);
                        Notification.SendWithTime($"~y~Вы посадили {User.PlayerIdList[p.ServerId.ToString()]} в тюрьму");
                        User.AddMoney(premia);//zametka
                        Notification.SendWithTime("~g~Вы получили премию в размере: $" + premia);
                         
                        Main.SaveLog("JailPD", User.Data.rp_name + " jailed id " + User.PlayerIdList[p.ServerId.ToString()] + ", " + reason);
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        
        public static void SapdNewsMenu()
        {
            if (User.IsSapd())
            {
                HideMenu();

                var menu = new Menu();
                UiMenu = menu.Create("Компьютер", "~b~Меню");
                
                if (User.IsLeader() || User.IsSubLeader())
                {
                    menu.AddMenuItem(UiMenu, "~y~Написать новость").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        var title = await Menu.GetUserInput("Заголовок", null, 15);
                        var text = await Menu.GetUserInput("Текст...", null, 50);
                        if (text == "NULL") return;
                        Notification.SendPictureToAll(text, "Новости SAPD", title, "WEB_LOSSANTOSPOLICEDEPT", Notification.TypeChatbox);
                    };
                        
                    menu.AddMenuItem(UiMenu, "~y~Лог на транспорт").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        TriggerServerEvent("ARP:SendPlayerVehicleLog");
                    };
                    
                    if (User.IsLeader() || User.IsSubLeader())
                    {
                        menu.AddMenuItem(UiMenu, "~y~Лог").Activated += (uimenu, item) =>
                        {
                            //ShowFractionMemberListMenu();
                            HideMenu();
                            TriggerServerEvent("ARP:SendPlayerGunLog");
                        };
                        
                        menu.AddMenuItem(UiMenu, "~g~Принять в организацию").Activated += (uimenu, item) =>
                        {
                            ShowFractionMemberInviteMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 2f));
                        };
                    }
                }
        
                if (User.Data.rank > 9)
                {
                    menu.AddMenuItem(UiMenu, "Получить пароль").Activated += async (uimenu, item) =>
                    {
                        Notification.Send($"~g~Текущий пароль: ~s~{await Client.Sync.Data.Get(-9999, "sapdPass")}");
                    };
                }
        
        
                var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
        
                UiMenu.OnItemSelect += (sender, item, index) =>
                { 
                    if (item == closeButton) 
                        HideMenu();
                };
        
                MenuPool.Add(UiMenu);
            }
        }
        public static void EmsNewsMenu()
        {
            if (User.IsEms())
            {
                HideMenu();

                var menu = new Menu();
                UiMenu = menu.Create("Компьютер", "~b~Меню");

                if (User.IsLeader() || User.IsSubLeader())
                {
                    menu.AddMenuItem(UiMenu, "~y~Написать новость").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        var title = await Menu.GetUserInput("Заголовок", null, 15);
                        var text = await Menu.GetUserInput("Текст...", null, 50);
                        if (text == "NULL") return;
                        Notification.SendPictureToAll(text, "Новости EMS", title, "CHAR_CALL911", Notification.TypeChatbox);
                    };

                    if (User.IsLeader() || User.IsSubLeader())
                    {
                        menu.AddMenuItem(UiMenu, "~y~Лог").Activated += (uimenu, item) =>
                        {
                            //ShowFractionMemberListMenu();
                            HideMenu();
                            TriggerServerEvent("ARP:SendPlayerGunLog");
                        };
                        menu.AddMenuItem(UiMenu, "~y~Лог на транспорт").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            TriggerServerEvent("ARP:SendPlayerVehicleLog");
                        };
                        menu.AddMenuItem(UiMenu, "~g~Принять в организацию").Activated += (uimenu, item) =>
                        {
                            ShowFractionMemberInviteMenu(
                                Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 2f));
                        };
                    }
                }

                var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");

                UiMenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == closeButton)
                        HideMenu();
                };

                MenuPool.Add(UiMenu);
            }
        }
        public static void GovLicenseMenu()
        {
            if (User.IsGov())
            {
                HideMenu();

                var menu = new Menu();
                UiMenu = menu.Create("Лицензии", "~b~Меню");

               
                    if (User.Data.rank == 10 || User.Data.rank == 11 || User.Data.rank == 13 || User.Data.rank == 14)
                    {
                        menu.AddMenuItem(UiMenu, "Выдать лицензию адвоката").Activated += (uimenu, item) =>
                        {
                            ShowGovGiveLawLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true),
                                1f));
                            Chat.SendMeCommand("передает документ");
                        };
                        menu.AddMenuItem(UiMenu, "Выдать лицензию на бизнес").Activated += (uimenu, item) =>
                        {
                            ShowGovGiveBizzLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true),
                                1f));
                            Chat.SendMeCommand("передает документ");
                        };
                        menu.AddMenuItem(UiMenu, "Выдать лицензию на охоту").Activated += (uimenu, item) =>
                        {
                            ShowGovGiveAnimalLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true),
                                1f));
                            Chat.SendMeCommand("передает документ");
                        };
                        menu.AddMenuItem(UiMenu, "Выдать лицензию на рыбалку").Activated += (uimenu, item) =>
                        {
                            ShowGovGiveFishLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true),
                                1f));
                            Chat.SendMeCommand("передает документ");
                        };
                    }


                var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");

                UiMenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == closeButton)
                        HideMenu();
                };

                MenuPool.Add(UiMenu);
            }
        }
        public static void GovNewsMenu()
        {
            if (User.IsGov())
            {
                HideMenu();

                var menu = new Menu();
                UiMenu = menu.Create("Компьютер", "~b~Меню");

                if (User.Data.rank == 8 || User.Data.rank >=10)
                {
                    menu.AddMenuItem(UiMenu, "~y~Написать новость").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        var title = await Menu.GetUserInput("Заголовок", null, 15);
                        var text = await Menu.GetUserInput("Текст...", null, 50);
                        if (text == "NULL") return;
                        Notification.SendPictureToAll(text, "Новости правительства", title, "CHAR_BANK_MAZE",
                            Notification.TypeChatbox);
                    };

                    if (User.Data.rank == 13 || User.Data.rank == 14)
                    {
                        menu.AddMenuItem(UiMenu, "~y~Лог").Activated += (uimenu, item) =>
                        {
                            //ShowFractionMemberListMenu();
                            HideMenu();
                            TriggerServerEvent("ARP:SendPlayerGunLog");
                        };
                        menu.AddMenuItem(UiMenu, "~y~Лог на транспорт").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            TriggerServerEvent("ARP:SendPlayerVehicleLog");
                        };
                        
                    }
                    if ( User.Data.rank == 10 || User.Data.rank == 11 || User.Data.rank == 13 || User.Data.rank == 14 )
                    {
                        menu.AddMenuItem(UiMenu, "~g~Принять в организацию").Activated += (uimenu, item) =>
                    {
                        ShowFractionMemberInviteMenu(
                            Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 2f));
                    };
                        
                    }

                    if (User.Data.rank == 14)
                    {
                        menu.AddMenuItem(UiMenu, "Пособие", $"Ставка: ~g~${Coffer.GetPosob()}").Activated +=
                            async (uimenu, item) =>
                            {
                                HideMenu();
                                Fractions.Government.SetPosob(
                                    Convert.ToInt32(await Menu.GetUserInput("Введите число", null, 3)));
                            };
                        menu.AddMenuItem(UiMenu, "Налог", $"Ставка: ~g~{Coffer.GetNalog()}%").Activated +=
                            async (uimenu, item) =>
                            {
                                HideMenu();
                                Fractions.Government.SetNalog(
                                    Convert.ToInt32(await Menu.GetUserInput("Введите число", null, 1)));
                            };
                        menu.AddMenuItem(UiMenu, "Налог на бизнес", $"Ставка: ~g~{Coffer.GetBizzNalog()}%").Activated +=
                            async (uimenu, item) =>
                            {
                                HideMenu();
                                Fractions.Government.SetNalogBusiness(
                                    Convert.ToInt32(await Menu.GetUserInput("Введите число", null, 1)));
                            };
                        menu.AddMenuItem(UiMenu, "Пенсия", $"Ставка: ~g~${Coffer.GetMoneyOld()}").Activated +=
                            async (uimenu, item) =>
                            {
                                HideMenu();
                                Fractions.Government.SetPension(
                                    Convert.ToInt32(await Menu.GetUserInput("Введите число", null, 3)));
                            };
                    }
                }

                var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");

                UiMenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == closeButton)
                        HideMenu();
                };

                MenuPool.Add(UiMenu);
            }
        }
        public static void GovCofferMenu()
        {
            if (User.IsGov())
            {
                HideMenu();

                var menu = new Menu();
                UiMenu = menu.Create("Казна", "~b~Меню");

                if (User.Data.rank > 8)
                    {
                        menu.AddMenuItem(UiMenu, "Положить деньги в казну").Activated += async (uimenu, item) =>
                        {
                            HideMenu();
                                
                            if (await Ctos.IsBlackout())
                            {
                                Notification.SendWithTime("~r~Банк во время блекаута не работает");
                                return;
                            }
                                
                            int number = Convert.ToInt32(await Menu.GetUserInput("Введите число", null, 10));

                            if (number > await User.GetCashMoney())
                            {
                                Notification.SendWithTime("~g~Нет деняк");
                                return;
                            }
                            
                            Coffer.AddMoney(number);
                            User.RemoveCashMoney(number);
                                
                            Notification.SendWithTime("~g~Вы положили деньги");
                            Main.SaveLog("CofferTakeLog", User.Data.rp_name + " - " + number);
                        };
                        
                        if (User.IsLeader())
                        {
                            menu.AddMenuItem(UiMenu, "Взять деньги").Activated += async (uimenu, item) =>
                            {
                                HideMenu();
                                
                                if (await Ctos.IsBlackout())
                                {
                                    Notification.SendWithTime("~r~Банк во время блекаута не работает");
                                    return;
                                }
                                
                                int number = Convert.ToInt32(await Menu.GetUserInput("Введите число", null, 10));
                                Coffer.RemoveMoney(number);
                                User.AddCashMoney(number);
                                
                                Notification.SendWithTime("~g~Вы взяли деньги");
                                Main.SaveLog("CofferTakeLog", User.Data.rp_name + " - " + number);
                            };
                        }
                    }
                    
                    menu.AddMenuItem(UiMenu, "В казне:").SetRightLabel($"~g~${Coffer.GetMoney():#,#}");
                    
                
                
                var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");

                UiMenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == closeButton)
                        HideMenu();
                };

                MenuPool.Add(UiMenu);
            }
        }
        public static void SheriffNewsMenu()
        {
            if (User.IsSheriff())
            {
                HideMenu();

                var menu = new Menu();
                UiMenu = menu.Create("Компьютер", "~b~Меню");

                if (User.IsLeader() || User.IsSubLeader())
                {
                    menu.AddMenuItem(UiMenu, "~y~Написать новость").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        var title = await Menu.GetUserInput("Заголовок", null, 15);
                        var text = await Menu.GetUserInput("Текст...", null, 50);
                        if (text == "NULL") return;
                        Notification.SendPictureToAll(text, "Новости Sheriff's Dept.", title, "WEB_LOSSANTOSPOLICEDEPT", Notification.TypeChatbox);
                    };
                    //zametka 1
                    if (User.Data.rank > 8)
                    {
                        menu.AddMenuItem(UiMenu, "~g~Принять в организацию").Activated += (uimenu, item) =>
                        {
                            ShowFractionMemberInviteMenu(
                                Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 2f));
                        };
                    }

                    if (User.IsLeader() || User.IsSubLeader())
                    {
                        menu.AddMenuItem(UiMenu, "~y~Лог").Activated += (uimenu, item) =>
                        {
                            //ShowFractionMemberListMenu();
                            HideMenu();
                            TriggerServerEvent("ARP:SendPlayerGunLog");
                        };
                        menu.AddMenuItem(UiMenu, "~y~Лог на транспорт").Activated += (uimenu, item) =>
                        {
                            HideMenu();
                            TriggerServerEvent("ARP:SendPlayerVehicleLog");
                        };

                        menu.AddMenuItem(UiMenu, "Получить пароль").Activated += async (uimenu, item) =>
                        { 
                            Notification.Send($"~g~Текущий пароль: ~s~{await Client.Sync.Data.Get(-9999, "sapdPass")}");
                        };
                        menu.AddMenuItem(UiMenu, "~g~Принять в организацию").Activated += (uimenu, item) =>
                        {
                            ShowFractionMemberInviteMenu(
                                Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 2f));
                        };
                    }
                }

                var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");

                UiMenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == closeButton)
                        HideMenu();
                };

                MenuPool.Add(UiMenu);
            }
        }
        
        public static void ShowSapdLicenseMenu()
        {
            if (User.IsSapd())
            {
                HideMenu();

                var menu = new Menu();
                UiMenu = menu.Create("Выдача лицензии", "~b~Меню");
        
                menu.AddMenuItem(UiMenu, "Выдать лицензию на оружие").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    ShowSapdGiveGunLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 2f));
                };
        
        
                var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
        
                UiMenu.OnItemSelect += (sender, item, index) =>
                { 
                    if (item == closeButton) 
                        HideMenu();
                };
        
                MenuPool.Add(UiMenu);
        
            }
        }

        public static void ShowSapdCyberPcMenu()
        {
            HideMenu();
            var menu = new Menu();
                UiMenu = menu.Create("SAPD", "~b~PC");

                menu.AddMenuItem(UiMenu, "Информация о человеке (Телефон)").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    int prefix = Convert.ToInt32(await Menu.GetUserInput("Префикс", null, 3));
                    int number = Convert.ToInt32(await Menu.GetUserInput("Номер", null, 10));
                    if (prefix == 0 || number == 0) return;
                    TriggerServerEvent("ARP:SendPlayerShowPassByHackerByPhone", prefix, number);
                };

                menu.AddMenuItem(UiMenu, "Информация о человеке (CardID)").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    int id = Convert.ToInt32(await Menu.GetUserInput("CardID", null, 10));
                    TriggerServerEvent("ARP:SendPlayerShowPassByHacker", id);
                };

                menu.AddMenuItem(UiMenu, "Доступ к СМС").Activated += async (uimenu, item) => 
                {
                    HideMenu();
                    int prefix = Convert.ToInt32(await Menu.GetUserInput("Префикс", null, 3));
                    int number = Convert.ToInt32(await Menu.GetUserInput("Номер", null, 10));
                    if (prefix == 0 || number == 0) return;
                    TriggerServerEvent("ARP:OpenSmsListMenu", prefix + "-" + number);
                };

                menu.AddMenuItem(UiMenu, "Получить местоположение человека",
                    "(( Если номер телефона экипирован и человек не связан ))").Activated += async (uimenu, item) =>
                {
                    HideMenu();

                    if (await Ctos.IsBlackout())
                    {
                        Notification.SendWithTime("~r~Связь во время блекаута не работает");
                        return;
                    }

                    int prefix = Convert.ToInt32(await Menu.GetUserInput("Префикс", null, 3));
                    int number = Convert.ToInt32(await Menu.GetUserInput("Номер", null, 10));

                    Sync.Data.ShowSyncMessage = false;
                    CitizenFX.Core.UI.Screen.LoadingPrompt.Show("Загрузка данных...");

                    foreach (Player p in new PlayerList())
                    {
                        try
                        {
                            var plData = await User.GetAllDataByServerId(p.ServerId);
                            if (plData == null) continue;
                            if (plData.phone_code == prefix && plData.phone == number)
                            {
                                if (!await Sync.Data.Has(p.ServerId, "disableNetwork") &&
                                    !await Sync.Data.Has(p.ServerId, "disablePhone") &&
                                    !await Sync.Data.Has(p.ServerId, "isTie") &&
                                    !await Sync.Data.Has(p.ServerId, "isCuff"))
                                {
                                    var playerPos = GetEntityCoords(GetPlayerPed(p.Handle), true);
                                    User.SetWaypoint(playerPos.X, playerPos.Y);
                                    CitizenFX.Core.UI.Screen.LoadingPrompt.Hide();

                                    Notification.SendWithTime("~g~Местоположение было установлено");
                                    await Delay(50);
                                    Notification.Send($"~y~Район:~s~ {World.GetZoneLocalizedName(playerPos)}");
                                    await Delay(50);
                                    Notification.Send($"~y~Улица:~s~ {World.GetStreetName(playerPos)}");

                                    Sync.Data.ShowSyncMessage = true;
                                    return;
                                }
                                else
                                {
                                    Notification.SendWithTime("~g~Телефон выключен или вне зоны действия сети");
                                    return;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.Message);
                            throw;
                        }
                    }

                    Notification.SendWithTime("~r~Человек не найден");

                    CitizenFX.Core.UI.Screen.LoadingPrompt.Hide();
                    Sync.Data.ShowSyncMessage = true;
                };

                menu.AddMenuItem(UiMenu, "Проверка подключения").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    int id = Convert.ToInt32(await Menu.GetUserInput("CardID", null, 10));
                    bool isUserConnect = await Client.Sync.Data.Has(id, "isConnectConsole");
                    Notification.Send($"~g~Connected: {isUserConnect}");
                    if (isUserConnect)
                        Notification.Send($"~g~Connect IP: {await Client.Sync.Data.Has(id, "connectIp")}");
                };

                var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");

                UiMenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == closeButton)
                        HideMenu();
                };

                MenuPool.Add(UiMenu);
        }

        public static void ShowHackerSpacePcMenu()
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create("HackerSpace", "~b~PC");

            if (Client.Sync.Data.HasLocally(User.GetServerId(), "isIncludePhone"))
            {
                menu.AddMenuItem(UiMenu, "~g~Обновить телефон", User.Data.s_is_usehackerphone ? "Стоимость: ~g~$10,000" : "Прошивка в первый раз бесплатно").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    if (User.Data.s_is_usehackerphone && await User.GetCashMoney() < 10000)
                    {
                        Notification.SendWithTime("~r~У Вас нет $10,000");
                        return;
                    }
                    if (User.Data.phone_code == 403)
                    {
                        Notification.SendWithTime("~r~Телефон не нуждается в прошивке");
                        return;
                    }
                    if (User.Data.phone_code != 404)
                    {
                        Notification.SendWithTime("~r~На телефоне должен быть установлен Kali Linux");
                        return;
                    }
                    
                    if (User.Data.s_is_usehackerphone)
                        User.RemoveCashMoney(10000);

                    Chat.SendMeCommand("что-то печатает");
                    
                    User.Freeze(PlayerId(), true);

                    await Ctos.DownloadFile("python", 1000);
                    await Ctos.ExecuteFile("python", 100);
                    await Ctos.DownloadFile("proxy");
                    await Ctos.ExecuteFile("proxy");
                    await Ctos.DownloadFile("xdman", 100);
                    await Ctos.ExecuteFile("xdman", 50);
                    
                    User.Freeze(PlayerId(), false);
                    
                    User.Data.s_is_usehackerphone = true;
                    Client.Sync.Data.Set(User.GetServerId(), "s_is_usehackerphone", true);
                    User.Data.phone_code = 403;
                    Client.Sync.Data.Set(User.GetServerId(), "phone_code", 403);

                    Notification.SendWithTime("~g~Вы прошили свой телефон");
                };
                
                menu.AddMenuItem(UiMenu, "~y~Отключить телефон").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Chat.SendMeCommand("отключил свой телефон");
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "isIncludePhone");
                    Notification.SendWithTime("~g~Вы отключили свой телефон");
                };
            }
            else
            {
                menu.AddMenuItem(UiMenu, "~g~Подключить телефон").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Chat.SendMeCommand("подключил свой телефон через usb");
                    Client.Sync.Data.SetLocally(User.GetServerId(), "isIncludePhone", true);
                    Notification.SendWithTime("~g~Вы подключили свой телефон");
                };
            }

            if (User.Data.mp0_watchdogs > 96)
            {
                menu.AddMenuItem(UiMenu, "~r~FSOCIETY.PY").Activated += async (uimenu, item) =>
                {
                    HideMenu();

                    Chat.SendMeCommand("что-то печатает");
                    
                    User.Freeze(PlayerId(), true);

                    await Ctos.DownloadFile("proxy");
                    await Ctos.ExecuteFile("proxy");
                    await Ctos.DownloadFile("fsociety.py", 1000);
                    await Ctos.ExecuteFile("fsociety.py", 100);
                    Dispatcher.SendEms("Хакерская атака", "Взлом системы управления городом");
                    Chat.SendMeCommand("что-то печатает");
                    await Ctos.ExecuteFile("proxy");
                    await Ctos.ExecuteFile("fsociety.py", 500);
                    await Ctos.ExecuteFile("proxy");
                    await Ctos.ExecuteFile("fsociety.py", 1000);
                    await Ctos.ExecuteFile("proxy");
                    await Ctos.ExecuteFile("fsociety.py", 1000);
                    Chat.SendMeCommand("что-то печатает");
                    await Ctos.ExecuteFile("proxy");
                    await Ctos.DownloadFile("blackout.py");
                    Chat.SendMeCommand("ихидно нажал кнопку enter");
                    await Ctos.ExecuteFile("blackout.py", 1000);
                    
                    User.Freeze(PlayerId(), false);
                    
                    Ctos.HackBlackout();
                };
            }

            menu.AddMenuItem(UiMenu, "Информация о человеке (Телефон)").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int prefix = Convert.ToInt32(await Menu.GetUserInput("Префикс", null, 3));
                int number = Convert.ToInt32(await Menu.GetUserInput("Номер", null, 10));
                if (prefix == 0 || number == 0) return;
                TriggerServerEvent("ARP:SendPlayerShowPassByHackerByPhone", prefix, number);
                Chat.SendMeCommand("что-то печатает");
            };

            menu.AddMenuItem(UiMenu, "Информация о человеке (CardID)").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int id = Convert.ToInt32(await Menu.GetUserInput("CardID", null, 10));
                TriggerServerEvent("ARP:SendPlayerShowPassByHacker", id);
                Chat.SendMeCommand("что-то печатает");
            };

            menu.AddMenuItem(UiMenu, "Проверка подключения").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int id = Convert.ToInt32(await Menu.GetUserInput("CardID", null, 10));
                bool isUserConnect = await Client.Sync.Data.Has(id, "isConnectConsole");
                Notification.Send($"~g~Connected: {isUserConnect}");
                if (isUserConnect)
                    Notification.Send($"~g~Connect IP: {await Client.Sync.Data.Has(id, "connectIp")}");
                Chat.SendMeCommand("что-то печатает");
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");

            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowKitchenMenu(int kitchenId, int amount)
        {
            HideMenu();

            if (kitchenId == 0)
                return;

            var menu = new Menu();
            UiMenu = menu.Create("Кухня", "~b~Кухня");

            menu.AddMenuItem(UiMenu, "Выпить воды").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.SetWaterLevel(100);
                Chat.SendMeCommand("выпивает воды");
            };

            menu.AddMenuItem(UiMenu, "Холодильник").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.GetItemList(kitchenId, InventoryTypes.Fridge);
            };

            menu.AddMenuItem(UiMenu, "Приготовить всю еду", "Приготовить еду, которая у вас в инвентаре").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.CookFood(User.Data.id);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");

            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static async void ShowSapdArsenalMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Арсенал", "~b~Арсенал");
            
            if (Sync.Data.HasLocally(User.GetServerId(), "isTimeoutArsenal"))
            {
                Notification.SendWithTime("~r~Доступно раз в 20 минут");
                return;
            }
            
            menu.SetMenuBannerSprite(UiMenu, "shopui_title_gr_gunmod", "shopui_title_gr_gunmod");

            menu.AddMenuItem(UiMenu, "Сухпаёк").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.TakeNewItem(32);
                Main.AddFractionGunLog(User.Data.rp_name, "Сухпаёк", User.Data.fraction_id);
            };

            menu.AddMenuItem(UiMenu, "Наручники").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.TakeNewItem(40);
                Main.AddFractionGunLog(User.Data.rp_name, "Наручники", User.Data.fraction_id);
            };

            menu.AddMenuItem(UiMenu, "Фонарик").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                User.GiveWeapon((uint) WeaponHash.Flashlight, 1, false, false);
                Notification.SendWithTime("~b~Вы взяли фонарик");
                Main.AddFractionGunLog(User.Data.rp_name, "Фонарик", User.Data.fraction_id);
            };
            
            menu.AddMenuItem(UiMenu, "Дубинка").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.GiveWeapon((uint) WeaponHash.Nightstick, 1, false, false);
                Notification.SendWithTime("~b~Вы взяли дубинку");
                Main.AddFractionGunLog(User.Data.rp_name, "Дубинка", User.Data.fraction_id);
            };
            
            menu.AddMenuItem(UiMenu, "Электрошокер").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.GiveWeapon((uint) WeaponHash.StunGun, 500, false, false);
                Notification.SendWithTime("~b~Вы взяли электрошокер");
                Main.AddFractionGunLog(User.Data.rp_name, "Электрошокер", User.Data.fraction_id);
            };
            
            menu.AddMenuItem(UiMenu, "Beretta 90Two").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.GiveWeapon((uint) WeaponHash.PistolMk2, 120, false, false);
                Notification.SendWithTime("~b~Вы взяли Beretta 90Two");
                Main.AddFractionGunLog(User.Data.rp_name, "Beretta 90Two", User.Data.fraction_id);
            };
            
            menu.AddMenuItem(UiMenu, "Benelli M3").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.GiveWeapon((uint) WeaponHash.PumpShotgun, 16, false, false);
                Notification.SendWithTime("~b~Вы взяли Benelli M3");
                Main.AddFractionGunLog(User.Data.rp_name, "Benelli M3", User.Data.fraction_id);
            };
            
            menu.AddMenuItem(UiMenu, "MP5A3").Activated += (uimenu, item) =>
            {
                HideMenu();

                if (User.Data.rank < 2)
                {
                    Notification.SendWithTime("~r~Вам не доступно это оружие");
                    return;
                }
                
                User.GiveWeapon((uint) WeaponHash.SMG, 450, false, false);
                SetWeaponObjectTintIndex((int) WeaponHash.SMG, (int) WeaponTint.LSPD);
                Notification.SendWithTime("~b~Вы взяли MP5A3");
                Main.AddFractionGunLog(User.Data.rp_name, "MP5A3", User.Data.fraction_id);
            };
            
            menu.AddMenuItem(UiMenu, "HK-416").Activated += (uimenu, item) =>
            {
                HideMenu();

                if (User.Data.rank < 2)
                {
                    Notification.SendWithTime("~r~Вам не доступно это оружие");
                    return;
                }
                
                User.GiveWeapon((uint) WeaponHash.CarbineRifle, 450, false, false);
                Notification.SendWithTime("~b~Вы взяли HK-416");
                Main.AddFractionGunLog(User.Data.rp_name, "HK-416", User.Data.fraction_id);
            };
            
            menu.AddMenuItem(UiMenu, "L115A3").Activated += (uimenu, item) =>
            {
                HideMenu();

                if (User.Data.rank < 2)
                {
                    Notification.SendWithTime("~r~Вам не доступно это оружие");
                    return;
                }
                
                User.GiveWeapon((uint) WeaponHash.SniperRifle, 20, false, false);
                Notification.SendWithTime("~b~Вы взяли L115A3");
                Main.AddFractionGunLog(User.Data.rp_name, "L115A3", User.Data.fraction_id);
            };
            
            menu.AddMenuItem(UiMenu, "Дымовая граната").Activated += (uimenu, item) =>
            {
                HideMenu();

                if (User.Data.rank < 2)
                {
                    Notification.SendWithTime("~r~Вам не доступно это оружие");
                    return;
                }
                
                User.GiveWeapon((uint) WeaponHash.SmokeGrenade, 3, false, false);
                Notification.SendWithTime("~b~Вы взяли дымовую гранату");
                Main.AddFractionGunLog(User.Data.rp_name, "Дымовая граната", User.Data.fraction_id);
            };
            
            menu.AddMenuItem(UiMenu, "Парашют").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.GiveWeapon((uint) WeaponHash.Parachute, 1, false, false);
                Notification.SendWithTime("~b~Вы взяли парашют");
                Main.AddFractionGunLog(User.Data.rp_name, "Парашют", User.Data.fraction_id);
            };

            menu.AddMenuItem(UiMenu, "Бронежилет").Activated += (uimenu, item) =>
            {
                HideMenu();
                SetPedArmour(GetPlayerPed(-1), 100);
                Notification.SendWithTime("~b~Вы взяли бронежилет");
                Main.AddFractionGunLog(User.Data.rp_name, "Бронежилет", User.Data.fraction_id);
            };

            menu.AddMenuItem(UiMenu, "Полицейское огорождение").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.TakeNewItem(199);
                Main.AddFractionGunLog(User.Data.rp_name, "Полицейское огорождение", User.Data.fraction_id);
            };

            menu.AddMenuItem(UiMenu, "Полосатый конус").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.TakeNewItem(201);
                Main.AddFractionGunLog(User.Data.rp_name, "Полосатый конус", User.Data.fraction_id);
            };

            menu.AddMenuItem(UiMenu, "Красный конус").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.TakeNewItem(202);
                Main.AddFractionGunLog(User.Data.rp_name, "Красный конус", User.Data.fraction_id);
            };
            
            menu.AddMenuItem(UiMenu, "Сдать оружие").Activated += (uimenu, item) =>
            {
                HideMenu();
                TriggerEvent("ARP:TakeAllGunsSAPD", User.GetServerId());
            };
            
            /*var list = (from v in Managers.Vehicle.VehicleInfoGlobalDataList where v.FractionId != 0 && v.FractionId == User.Data.fraction_id select v.Number).Cast<dynamic>().ToList();
            menu.AddMenuItemList(UiMenu, "Ключи", list, "Нажмите \"~g~Enter~s~\" чтобы взять ключи").OnListSelected += (uimenu, idx) =>
            {
                HideMenu();
                User.AddVehicleKey(list[idx].ToString());
                Notification.SendWithTime($"~b~Вы взяли ключи для ТС с номером: {list[idx]}");
            };*/
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);

            await Delay(1 * 60 * 1000);
            Sync.Data.SetLocally(User.GetServerId(), "isTimeoutArsenal", true);
            await Delay(20 * 60 * 1000);
            Sync.Data.ResetLocally(User.GetServerId(), "isTimeoutArsenal");
        }
        
        /*public static async void ShowEmsArsenalMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("EMS", "~b~Мед. шкаф EMS");
            
            if (Sync.Data.HasLocally(User.GetServerId(), "isTimeoutArsenal"))
            {
                Notification.SendWithTime("~r~Доступно раз в 20 минут");
                return;
            }

            menu.AddMenuItem(UiMenu, "Сухпаёк").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.TakeNewItem(32);
                Main.AddFractionGunLog(User.Data.rp_name, "Сухпаёк", User.Data.fraction_id);
            };

            menu.AddMenuItem(UiMenu, "Адреналин").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.TakeNewItem(31);
                Main.AddFractionGunLog(User.Data.rp_name, "Адреналин", User.Data.fraction_id);
            };

            menu.AddMenuItem(UiMenu, "Аптечка").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.TakeNewItem(215);
                Main.AddFractionGunLog(User.Data.rp_name, "Антипохмелин", User.Data.fraction_id);
            };

            menu.AddMenuItem(UiMenu, "Антипохмелин").Activated += (uimenu, item) =>
            {
                HideMenu();
                Managers.Inventory.TakeNewItem(221);
                Main.AddFractionGunLog(User.Data.rp_name, "Антипохмелин", User.Data.fraction_id);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);

            await Delay(1 * 60 * 1000);
            Sync.Data.SetLocally(User.GetServerId(), "isTimeoutArsenal", true);
            await Delay(20 * 60 * 1000);
            Sync.Data.ResetLocally(User.GetServerId(), "isTimeoutArsenal");
        }
        */
        public static void ShowEmsAptekaMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("EMS", "~b~Аптека");

            menu.AddMenuItem(UiMenu, "Адреналин", $"Цена: ~g~$400").Activated += async (uimenu, item) =>
            {
                HideMenu();
                if (!User.Data.med_lic)
                {
                    Notification.SendWithTime("~r~У Вас нет медстраховки");
                    return;
                }
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Fractions.Ems.Buy(31, 400, count);
            };

            menu.AddMenuItem(UiMenu, "Аптечка", $"Цена: ~g~$350").Activated += async (uimenu, item) =>
            {
                HideMenu();
                if (!User.Data.med_lic)
                {
                    Notification.SendWithTime("~r~У Вас нет медстраховки");
                    return;
                }
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Fractions.Ems.Buy(215, 350, count);
            };

            menu.AddMenuItem(UiMenu, "Антипохмелин", $"Цена: ~g~$30").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Fractions.Ems.Buy(221, 30, count);
            };

            menu.AddMenuItem(UiMenu, "Лечебная марихуана (10гр)", $"Цена: ~g~$40").Activated += async (uimenu, item) =>
            {
                HideMenu();

                if (!User.Data.allow_marg)
                {
                    Notification.SendWithTime("~r~У вас должен быть рецепт на лечебную марихуану");
                    return;
                }
                Fractions.Ems.Buy(155, 40, 1);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowAutoRepairShopMenu(int shopId)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Auto Repairs", "~b~Магазин");

            menu.AddMenuItem(UiMenu, "Отмычка", $"Цена: ~g~$10").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(4, 10, shopId, count);
            };
            
            menu.AddMenuItem(UiMenu, "Верёвка", $"Цена: ~g~$10").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Shop.Buy(0, 10, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Мешок", $"Цена: ~g~$20").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Shop.Buy(1, 20, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Набор инструментов", $"Цена: ~g~$240").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Shop.Buy(6, 240, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Моторное масло", $"Цена: ~g~$50").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Shop.Buy(5, 50, shopId);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowAutoRepairCarShopMenu(CitizenFX.Core.Vehicle v, int shopId)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Auto Repairs", "~b~Магазин");
  
            foreach (var vehData in Managers.Vehicle.VehicleInfoGlobalDataList)
            {
                if (vehData.Number != Managers.Vehicle.GetVehicleNumber(v.Handle)) continue;

                if (vehData.SOil > 0)
                {
                    int price = 100;
                    menu.AddMenuItem(UiMenu, "Замена масла", $"Цена: ~g~${price:#,#}").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        if (await User.GetCashMoney() < price)
                        {
                            Notification.SendWithTime("~r~У Вас недостаточно средств");
                            return;
                        }
                        
                        Managers.Vehicle.VehicleInfoGlobalDataList[vehData.VehId].SOil = 0;
                        Sync.Data.Set(110000 + vehData.VehId, "SOil", 0);
                        TriggerServerEvent("ARP:SaveVehicle", vehData.VehId);
                        User.RemoveCashMoney(price);
                        Business.Business.AddMoney(shopId, price);
                        Notification.SendWithTime("~g~Вашему транспорту произвели замену масла");
                    };
                }

                if (vehData.SBody > 0)
                {
                    int price = Convert.ToInt32(vehData.price * 0.01);
                    if (price > 10000)
                        price = 10000;
                    if (price < 100)
                        price = 100;

                    menu.AddMenuItem(UiMenu, "Починка кузова", $"Цена: ~g~${price:#,#}").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        if (await User.GetCashMoney() < price)
                        {
                            Notification.SendWithTime("~r~У Вас недостаточно средств");
                            return;
                        }
                        Managers.Vehicle.Repair(v);
                        Managers.Vehicle.VehicleInfoGlobalDataList[vehData.VehId].SBody = 0;
                        Sync.Data.Set(110000 + vehData.VehId, "SBody", 0);
                        TriggerServerEvent("ARP:SaveVehicle", vehData.VehId);
                        User.RemoveCashMoney(price);
                        Business.Business.AddMoney(shopId, price);
                        Notification.SendWithTime("~g~Вашему транспорту починили кузов");
                    };
                }
                else
                {
                    int price = 200;

                    menu.AddMenuItem(UiMenu, "Починка кузова", $"Цена: ~g~${price:#,#}").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        if (await User.GetCashMoney() < price)
                        {
                            Notification.SendWithTime("~r~У Вас недостаточно средств");
                            return;
                        }
                        Managers.Vehicle.Repair(v);
                        User.RemoveCashMoney(price);
                        Business.Business.AddMoney(shopId, price);
                        Notification.SendWithTime("~g~Вашему транспорту починили кузов");
                    };
                }

                if (vehData.SEngine > 1)
                {
                    int price = Convert.ToInt32(vehData.price * 0.01);
                    if (price > 20000)
                        price = 20000;
                    if (price < 100)
                        price = 100;
                    
                    menu.AddMenuItem(UiMenu, "Замена двигателя", $"Цена: ~g~${price:#,#}").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        if (await User.GetCashMoney() < price)
                        {
                            Notification.SendWithTime("~r~У Вас недостаточно средств");
                            return;
                        }
                        Managers.Vehicle.Repair(v);
                        Managers.Vehicle.VehicleInfoGlobalDataList[vehData.VehId].SEngine = 0;
                        Sync.Data.Set(110000 + vehData.VehId, "SEngine", 0);
                        TriggerServerEvent("ARP:SaveVehicle", vehData.VehId);
                        User.RemoveCashMoney(price);
                        Business.Business.AddMoney(shopId, price);
                        Notification.SendWithTime("~g~Вашему транспорту заменили двигатель");
                    };
                }

                if (vehData.SSuspension > 0)
                {
                    int price = Convert.ToInt32(vehData.price * 0.01);
                    if (price > 7000)
                        price = 7000;
                    if (price < 100)
                        price = 100;
                    
                    menu.AddMenuItem(UiMenu, "Замена подвески", $"Цена: ~g~${price:#,#}").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        if (await User.GetCashMoney() < price)
                        {
                            Notification.SendWithTime("~r~У Вас недостаточно средств");
                            return;
                        }
                        Managers.Vehicle.Repair(v);
                        Managers.Vehicle.VehicleInfoGlobalDataList[vehData.VehId].SSuspension = 0;
                        Sync.Data.Set(110000 + vehData.VehId, "SSuspension", 0);
                        TriggerServerEvent("ARP:SaveVehicle", vehData.VehId);
                        User.RemoveCashMoney(price);
                        Business.Business.AddMoney(shopId, price);
                        Notification.SendWithTime("~g~Вашему транспорту заменили подвеску");
                    };
                }

                if (vehData.SWhBkl > 0)
                {
                    int price = Convert.ToInt32(vehData.price * 0.01);
                    if (price > 2000)
                        price = 2000;
                    if (price < 100)
                        price = 100;
                    
                    menu.AddMenuItem(UiMenu, "Замена колёс", $"Цена: ~g~${price:#,#}").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        if (await User.GetCashMoney() < price)
                        {
                            Notification.SendWithTime("~r~У Вас недостаточно средств");
                            return;
                        }
                        Managers.Vehicle.Repair(v);
                        Managers.Vehicle.VehicleInfoGlobalDataList[vehData.VehId].SWhBkl = 0;
                        Sync.Data.Set(110000 + vehData.VehId, "SWhBkl", 0);
                        TriggerServerEvent("ARP:SaveVehicle", vehData.VehId);
                        User.RemoveCashMoney(price);
                        Business.Business.AddMoney(shopId, price);
                        Notification.SendWithTime("~g~Вашему транспорту заменили колёса");
                    };
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowAutoRepairCarListShopMenu(int shopId)
        {
            HideMenu();
            
            var vehList = Main.GetVehicleListOnRadius(Managers.Pickup.AutoRepairsPosCarPos, 4f);
            var menu = new Menu();
            UiMenu = menu.Create("Auto Repairs", "~b~Ремонт");

            foreach (var vehItem in vehList)
            {
                menu.AddMenuItem(UiMenu, "~b~Номер авто:~s~ " + Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).Activated += (uimenu, index) =>
                {
                    HideMenu();
                    ShowAutoRepairCarShopMenu(vehItem, shopId);
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        
        public static void ShowAptekaMenu(int shopId)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Аптека", "~b~Аптека");

            menu.AddMenuItem(UiMenu, "Медстраховка", $"Цена: ~g~$20,000").Activated += async (uimenu, item) =>
            {
                HideMenu();
                if (User.Data.med_lic)
                {
                    Notification.SendWithTime("~r~У Вас есть медстраховка");
                    return;
                }
                int price = 20000;
                if (User.GetMoneyWithoutSync() < price)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                    return;
                }
                User.Data.med_lic = true;
                Client.Sync.Data.Set(User.GetServerId(), "med_lic", true);
                User.RemoveMoney(price);
                Business.Business.AddMoney(shopId, price);
                Notification.SendWithTime("~g~Вы купили мед. страховку");
            };

            menu.AddMenuItem(UiMenu, "Адреналин", $"Цена: ~g~$900").Activated += async (uimenu, item) =>
            {
                HideMenu();
                if (!User.Data.med_lic)
                {
                    Notification.SendWithTime("~r~У Вас нет медстраховки");
                    return;
                }
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(31, 900, shopId, count);
            };

            menu.AddMenuItem(UiMenu, "Аптечка", $"Цена: ~g~$120").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(215, 120, shopId, count);
            };

            menu.AddMenuItem(UiMenu, "Антипохмелин", $"Цена: ~g~$20").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(221, 20, shopId, count);
            };

            menu.AddMenuItem(UiMenu, "Лечебная марихуана (10гр)", $"Цена: ~g~$20").Activated += async (uimenu, item) =>
            {
                HideMenu();

                if (!User.Data.allow_marg)
                {
                    Notification.SendWithTime("~r~У вас должен быть рецепт на лечебную марихуану");
                    return;
                }
                Business.Shop.Buy(155, 20, shopId);
            };
            
            if (User.Data.fraction_id2 > 0)
            {
                menu.AddMenuItem(UiMenu, "~o~Ограбить").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Grab.GrabShop(shopId);
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        public static void ShowHealMenu()
        {
            if (User.IsEms())
            {
                if (User.IsDuty())
                {
                    HideMenu();

                    var menu = new Menu();
                    UiMenu = menu.Create("Курс лечения", "~b~Меню");

                    menu.AddMenuItem(UiMenu, "Провести курс лечение человеку рядом").Activated += async (uimenu, item) =>
                    {
                        HideMenu();
                        //foreach (CitizenFX.Core.Player p in Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), Convert.ToInt32(1)))
                        //  Shared.TriggerEventToPlayer(p.ServerId, "ARP:UseAdrenalin");
                        var pPos = GetEntityCoords(GetPlayerPed(-1), true);
                        var player = Main.GetPlayerOnRadius(pPos, 2f);
                        if (player == null)
                        {
                            Notification.SendWithTime("~r~Рядом с вами никого нет");
                            return;
                        }

                        Shared.TriggerEventToPlayer(player.ServerId, "ARP:EmsHeal");
                        Chat.SendMeCommand("проводит курс лечения человеку рядом");
                        
                    };


                    var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");

                    UiMenu.OnItemSelect += (sender, item, index) =>
                    {
                        if (item == closeButton)
                            HideMenu();
                    };

                    MenuPool.Add(UiMenu);
                }
            }
        }
        public static void ShowEmsLicenseMenu()
        {
            if (User.IsEms())
            {
                HideMenu();

                var menu = new Menu();
                UiMenu = menu.Create("Выдача лицензии", "~b~Меню");

                if (User.Data.rank > 8)
                {
                    menu.AddMenuItem(UiMenu, "Выдать рецепт марихуаны", "~y~Запрещено выдавать его платно!")
                        .Activated += (uimenu, item) =>
                    {
                        ShowGiveMargLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true),
                            1f));
                    };

                    menu.AddMenuItem(UiMenu, "Выдать мед. страховку").Activated += (uimenu, item) =>
                    {
                        ShowGiveMedLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true),
                            1f));
                    };
                    }


                var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");

                UiMenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == closeButton)
                        HideMenu();
                };

                MenuPool.Add(UiMenu);
            }
        }

        public static void ShowLicenseMenu()
        {
            if (User.IsSheriff())
            {
                HideMenu();

                var menu = new Menu();
                UiMenu = menu.Create("Выдача лицензии", "~b~Меню");

                menu.AddMenuItem(UiMenu, "Выдать лицензию на оружие").Activated += async (uimenu, item) =>
                {
                    HideMenu();
                    ShowSapdGiveGunLicMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 2f));
                };


                var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");

                UiMenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == closeButton)
                        HideMenu();
                };

                MenuPool.Add(UiMenu);

            }
        }

        
        

        public static async void ShowFractionKeyMenu(string title, string desc, int fraction = -1)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create(title, "~b~" + desc);
            
            var list = (from v in Managers.Vehicle.VehicleInfoGlobalDataList where v.FractionId != 0 && v.FractionId == User.Data.fraction_id && Managers.Vehicle.CanSpawn(v) select v.Number).Cast<dynamic>().ToList();
            var list2 = (from v in Managers.Vehicle.VehicleInfoGlobalDataList where v.FractionId != 0 && v.FractionId == User.Data.fraction_id && Managers.Vehicle.CanSpawn(v) select v.DisplayName + ": " + v.Number).Cast<dynamic>().ToList();
            
            menu.AddMenuItemList(UiMenu, "Транспорт", list2, "Нажмите \"~g~Enter~s~\" чтобы взять транспорт").OnListSelected += async (uimenu, idx) =>
            {
                HideMenu();

                /*if (User.VehicleKeyList.Count > 10)
                {
                    Notification.SendWithTime("~r~У Вас слишком много ключей");
                    return;
                }*/

                var veh = await Managers.Vehicle.GetAllData(Managers.Vehicle.GetVehicleIdByNumber((string) list[idx]));
                if (Managers.Vehicle.CanSpawn(veh))
                {
                    Managers.Vehicle.SpawnVehicleByVehData(veh);

                    User.AddVehicleKey(list[idx].ToString());
                    Notification.SendWithTime($"~b~Вы взяли ключи для ТС с номером: {list[idx]}");
                    Notification.SendWithTime($"~b~Транспорт находиться на парковке, найдите его");
                    User.SetWaypoint(veh.CurrentPosX, veh.CurrentPosY);
                    
                    Main.AddFractionVehicleLog(User.Data.rp_name, $"{veh.DisplayName} - {veh.Number}", User.Data.fraction_id);
                    //Managers.Checkpoint.CreateWithMarker(new Vector3(veh.CurrentPosX, veh.CurrentPosY, veh.CurrentPosZ), 10f, 5f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "checkpoint:withdelete");
                }
                else
                {
                    Notification.SendWithTime($"~b~Транспорт уже используется");
                }
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowJobKeyMenu(string title, string desc)
        {
            
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create(title, "~b~" + desc);
            
            var list = (from v in Managers.Vehicle.VehicleInfoGlobalDataList where v.Job != "" && v.Job == User.Data.job && Managers.Vehicle.CanSpawn(v) select v.Number).Cast<dynamic>().ToList();
            var list2 = (from v in Managers.Vehicle.VehicleInfoGlobalDataList where v.Job != "" && v.Job == User.Data.job && Managers.Vehicle.CanSpawn(v) select v.DisplayName + ": " + v.Number).Cast<dynamic>().ToList();

            if (User.Data.job == "GrSix")
            {
                menu.AddMenuItem(UiMenu, "~g~Взять форму", "Стоимость $1.000").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Jobs.GroupSix.UniformSet();
                };
                menu.AddMenuItem(UiMenu, "~g~Взять экиппировку", "Стоимость $2.000").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Jobs.GroupSix.Equip();
                };
                menu.AddMenuItem(UiMenu, "~g~Снять экипировку").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Jobs.GroupSix.Dequip();
                };
                menu.AddMenuItem(UiMenu, "Залог за ТС: $\"4.500\"");
            }
            
            if (User.Data.job == "trash")
            {
                menu.AddMenuItem(UiMenu, "~g~Начать/~r~Закончить~s~ рабочий день").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Jobs.Trash.StartOrEnd();
                };
            }
            
            menu.AddMenuItemList(UiMenu, "Транспорт", list2, "Нажмите \"~g~Enter~s~\" чтобы взять транспорт").OnListSelected += async (uimenu, idx) =>
            {
                HideMenu();
                
                if (User.Data.job == "trash" && !Jobs.Trash.IsStartWork)
                {
                    Notification.SendWithTime("~r~Вы должны сначала начать рабочий день");
                    return;
                }

                /*if (User.VehicleKeyList.Count > 10)
                {
                    Notification.SendWithTime("~r~У Вас слишком много ключей");
                    return;
                }*/

                var veh = await Managers.Vehicle.GetAllData(Managers.Vehicle.GetVehicleIdByNumber((string) list[idx]));
                
                if (Managers.Vehicle.CanSpawn(veh))
                {
                    if (User.Data.job == "GrSix")
                    {
                        if (Sync.Data.HasLocally(User.GetServerId(), "GrSix:Uniform") &&
                            Sync.Data.HasLocally(User.GetServerId(), "GrSix:Equip"))
                        {
                            if (User.Data.money < 4500)
                            {
                                Notification.SendWithTime("~r~У вас недостаточно денег, чтобы внести залог");
                                return;
                            }

                            User.RemoveMoney(4500);
                            Coffer.AddMoney(4500);
                        }
                        else
                        {
                            Notification.SendWithTime("~r~Вы не экипированы для начала работы");
                            return;
                        }
                    }
                    Managers.Vehicle.SpawnVehicleByVehData(veh);

                    User.AddVehicleKey(list[idx].ToString());
                    Notification.SendWithTime($"~b~Вы взяли ключи для ТС с номером: {list[idx]}");
                    Notification.SendWithTime($"~b~Транспорт находиться на парковке, найдите его");
                    User.SetWaypoint(veh.CurrentPosX, veh.CurrentPosY);
                    //Managers.Checkpoint.CreateWithMarker(new Vector3(veh.CurrentPosX, veh.CurrentPosY, veh.CurrentPosZ), 10f, 5f, Marker.Red.R, Marker.Red.G, Marker.Red.B, Marker.Red.A, "checkpoint:withdelete");
                }
                else
                {
                    Notification.SendWithTime($"~b~Транспорт уже используется");
                }
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowMeriaGarderobMenu()
{
    HideMenu();

    var menu = new Menu();
    UiMenu = menu.Create("Гардероб", "~b~Гардероб правительства");

    menu.AddMenuItem(UiMenu, "Сухпаёк").Activated += (uimenu, item) =>
    {
        HideMenu();
        Managers.Inventory.AddItemServer(32, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
        Managers.Inventory.UpdateAmount(User.Data.id, InventoryTypes.Player);
        Notification.SendWithTime("~b~Вы взяли сухпаёк");
        Main.AddFractionGunLog(User.Data.rp_name, "Сухпаёк", User.Data.fraction_id);
    };

    if (User.Data.rank >= 1)
    {
        menu.AddMenuItem(UiMenu, "Бронежилет").Activated += (uimenu, item) =>
        {
            HideMenu();
            SetPedArmour(GetPlayerPed(-1), 100);
            Notification.SendWithTime("~b~Вы взяли бронежилет");
            Main.AddFractionGunLog(User.Data.rp_name, "Бронежилет", User.Data.fraction_id);
        };

        if (User.Data.rank == 3 || User.Data.rank == 5 || User.Data.rank == 8)
        {
            menu.AddMenuItem(UiMenu, "Экипировка Агента").Activated += (uimenu, item) =>
            {
                HideMenu();
                SetPedArmour(GetPlayerPed(-1), 100);

                User.GiveWeapon((uint)WeaponHash.CombatPDW, 210, false, false);
                User.GiveWeapon((uint)WeaponHash.PistolMk2, 82, false, false);
                User.GiveWeapon((uint)WeaponHash.Flashlight, 1, false, false);
                User.GiveWeapon((uint)WeaponHash.StunGun, 1, false, false);
                User.GiveWeapon((uint)WeaponHash.Nightstick, 1, false, false);
                Managers.Inventory.TakeNewItem(40);
                Managers.Inventory.TakeNewItem(40);


                Notification.SendWithTime("~b~Вы взяли экпировку");
                Main.AddFractionGunLog(User.Data.rp_name, "Экипировка Агента", User.Data.fraction_id);
            };
            menu.AddMenuItem(UiMenu, "Сдать оружие").Activated += (uimenu, item) =>
            {
                HideMenu();
                TriggerEvent("ARP:TakeAllGunsSAPD", User.GetServerId());
            };
        }
}
                
            
            /*var list = (from v in Managers.Vehicle.VehicleInfoGlobalDataList where v.FractionId != 0 && v.FractionId == User.Data.fraction_id select v.Number).Cast<dynamic>().ToList();
            menu.AddMenuItemList(UiMenu, "Ключи", list, "Нажмите \"~g~Enter~s~\" чтобы взять ключи").OnListSelected += (uimenu, idx) =>
            {
                HideMenu();
                User.AddVehicleKey(list[idx].ToString());
                Notification.SendWithTime($"~b~Вы взяли ключи для ТС с номером: {list[idx]}");
            };*/
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowMeriaMainMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Секретарь", "~b~Секретарь правительства");

            menu.AddMenuItem(UiMenu, "Лицензия таксиста", "Цена: ~g~$50").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.BuyTaxiLic();
            };

            menu.AddMenuItem(UiMenu, "Оформить регистрацию", "Оформление регистрации на 6 месяцев").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GiveRegStatus();
            };
            
            menu.AddMenuItem(UiMenu, "Трудовая биржа").Activated += (uimenu, item) =>
            {
                ShowMeriaJobListMenu();
            };
            
            menu.AddMenuItem(UiMenu, "~b~Почетные жители штата").Activated += (uimenu, item) =>
            {
                ShowMeriaFavoriteListMenu();
            };
            
            menu.AddMenuItem(UiMenu, "Оформить пособие").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GivePosob();
            };
            
            menu.AddMenuItem(UiMenu, "Оформить пенсию").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GivePension();
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowMeriaFavoriteListMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Список", "~b~Почётные жители штата");

            menu.AddMenuItem(UiMenu, "Paul Vitti", "Основатель партии GRPA\n(( MilkyWay ))").Activated += (uimenu, item) =>
            {
                UI.ShowToolTip("Paul Vitti организовал работу правительства. Добился изменения и принятия конституции и законов штата, заложив правовую основу для последующих губернаторов.");
            };

            menu.AddMenuItem(UiMenu, "Diego Garcia", "Активное участие в соц. жизни штата\n(( MilkyWay ))").Activated += (uimenu, item) =>
            {
                UI.ShowToolTip("Diego Garcia внес огромный вклад в развитие социальной жизни Штата, благоустроил и реконструировал район Миррор-Парк за собственные средства и продолжает этим заниматься по сей день.");
            };

            menu.AddMenuItem(UiMenu, "Abagail Kovak", "Представитель закона\n(( MilkyWay ))").Activated += (uimenu, item) =>
            {
                UI.ShowToolTip("Бывший Шеф SAPD, был награждён Орденом Почёта за долгую и верную службу, а также особые заслуги перед гражданами штата, и занесен в Зал Почёта и Славы Полицейского Департамента штата San Andreas.");
            };

            menu.AddMenuItem(UiMenu, "Tony Costello", "Бизнесмен\n(( MilkyWay ))").Activated += (uimenu, item) =>
            {
                UI.ShowToolTip("Основатель таких компаний как LifeInvader / Premium Deluxe Motors, активное участие в жизни штата, внёс огромный вклад в его экономику.");
            };
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowMeriaMainMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowMeriaJobListMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Трудовая биржа", "~b~Тут можно устроиться на работу");

            menu.AddMenuItem(UiMenu, "Мехатроник", "Компания: ~y~Water & Power").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GetJob("water");
            };
            menu.AddMenuItem(UiMenu, "Уборщик квартир", "Компания: ~y~Sunset Bleach").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GetJob("sunb");
            };
            menu.AddMenuItem(UiMenu, "Дезинсектор", "Компания: ~y~Bugstars").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GetJob("bgstar");
            };
            menu.AddMenuItem(UiMenu, "Садовник", "Компания: ~y~O'Connor").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GetJob("three");
            };
            menu.AddMenuItem(UiMenu, "Инкассатор", "Компания: ~y~GruppeSechs").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GetJob("GrSix");
            };
            menu.AddMenuItem(UiMenu, "Фотограф", "Компания: ~y~LifeInvader").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GetJob("photo");
            };
            menu.AddMenuItem(UiMenu, "Мусорщик").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GetJob("trash");
            };
            /*menu.AddMenuItem(UiMenu, "Развозчик металлолома").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GetJob("scrap");
            };*/
            menu.AddMenuItem(UiMenu, "Почтальон (PostOp)", "Компания: ~y~PostOp").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GetJob("mail");
            };
            menu.AddMenuItem(UiMenu, "Почтальон (GoPost)", "Компания: ~y~GoPost").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GetJob("mail2");
            };
            menu.AddMenuItem(UiMenu, "Водитель L.S.I.A. автобуса ", "Компания: ~y~Los Santos Transit").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GetJob("bus2");
            };
            menu.AddMenuItem(UiMenu, "Водитель городского автобуса", "Компания: ~y~Los Santos Transit").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GetJob("bus1");
            };
            menu.AddMenuItem(UiMenu, "Водитель междугороднего автобуса", "Компания: ~y~Dashound").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GetJob("bus3");
            };
            menu.AddMenuItem(UiMenu, "Учёный - Гидролог", "Компания: ~y~Humane Labs").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GetJob("swater");
            };
            menu.AddMenuItem(UiMenu, "Учёный - Биолог", "Компания: ~y~Humane Labs").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.GetJob("sground");
            };
            menu.AddMenuItem(UiMenu, "~y~Уволиться с работы").Activated += (uimenu, item) =>
            {
                HideMenu();
                Fractions.Government.ResetJob();
            };
            menu.AddMenuItem(UiMenu, "~y~Уволиться из организации", "Увольнение из неофициальной организации").Activated += (uimenu, item) =>
            {
                HideMenu();
                User.Data.fraction_id2 = 0;
                User.Data.rank2 = 0;
                Client.Sync.Data.Set(User.GetServerId(), "rank2", 0);
                Client.Sync.Data.Set(User.GetServerId(), "fraction_id2", 0);
                Notification.SendWithTime("~g~Вы уволились из организации");
            };
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowMeriaMainMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowMazeBankOfficeMenu()
        {
            HideMenu();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Банк во время блекаута не работает");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Maze", "Офис государственного банка \"~r~Maze~s~\"");

            var openHvbMenuButton = menu.AddMenuItem(UiMenu, "Имущество", "Операции с вашим имуществом");
            
            
            menu.AddMenuItem(UiMenu, "Налоговый кабинет").Activated += (uimenu, index) =>
            {
                HideMenu();
                ShowMazeBankOfficeTaxMenu();
            };

            if (User.Data.bank_prefix == 1111)
            {
                menu.AddMenuItem(UiMenu, "Снять средства").Activated += (uimenu, index) =>
                {
                    HideMenu();
                    if (CitizenFX.Core.UI.Screen.LoadingPrompt.IsActive)
                    {
                        Notification.SendWithTime("~r~Данное действие сейчас недоступно");
                        return;
                    }
                    Business.Bank.Widthdraw();
                };
                
                menu.AddMenuItem(UiMenu, "Положить средства").Activated += (uimenu, index) =>
                {
                    HideMenu();
                    if (CitizenFX.Core.UI.Screen.LoadingPrompt.IsActive)
                    {
                        Notification.SendWithTime("~r~Данное действие сейчас недоступно");
                        return;
                    }
                    Business.Bank.Deposit();
                };
    
                menu.AddMenuItem(UiMenu, "Баланс", $"Ваш баланс: ~g~${User.Data.money_bank:#,#}");
                menu.AddMenuItem(UiMenu, "Номер счёта", $"Номер карты: ~g~{User.Data.bank_prefix}-{User.Data.bank_number}");
                
                menu.AddMenuItem(UiMenu, "Перевести на другой счёт", "1% от суммы, при переводе").Activated += async (uimenu, index) =>
                {
                    HideMenu();
                    int prefix = Convert.ToInt32(await Menu.GetUserInput("Префикс карты", null, 4));
                    int number = Convert.ToInt32(await Menu.GetUserInput("Номер карты", null, 10));
                    int sum = Convert.ToInt32(await Menu.GetUserInput(Lang.GetTextToPlayer("_lang_1"), null, 10));
                    if (number < 10000)
                    {
                        Notification.SendWithTime("~r~Должно быть больше 5 цифр");
                        return;
                    }
                    if (prefix < 1 || sum < 1)
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                        return;
                    }
                    if (sum > User.Data.money_bank)
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_2"));
                        return;
                    }
                    
                    TriggerServerEvent("ARP:TransferMoneyBank", prefix, number, sum);
                };
                
                menu.AddMenuItem(UiMenu, "~r~Закрыть счёт").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Business.Bank.CloseCard();
                };
            }
            else
            {
                menu.AddMenuItem(UiMenu, "Оформить карту банка", "Цена: ~g~$50").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Business.Bank.NewCard(0, 50);
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == openHvbMenuButton)
                {
                    ShowMazeBankOfficeSellHvbMenu();
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowMazeBankOfficeSellHvbMenu()
        {
            HideMenu();

            await Delay(500);
            
            var menu = new Menu();
            UiMenu = menu.Create("Maze", $"~b~Текущая налоговая ставка: ~s~{Coffer.GetNalog()}%");
            menu.AddMenuItem(UiMenu, "Управление вашим имуществом", "Здесь вы можете продать своё имущество государству");

            await User.GetAllData();
            
            if (User.Data.id_house > 0)
            {
                HouseInfoGlobalData h = House.GetHouseFromId(User.Data.id_house);
                var nalog = h.price * (100 - Coffer.GetNalog()) / 100;
                menu.AddMenuItem(UiMenu, "Продать дом", $"Продать дом государству\nЦена: ~g~${nalog:#,#}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowPlayerAntiHookupHouseMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f), User.Data.id_house, $"{h.address} #{h.id}");
                    ShowAskSellHMenu();
                    
                };
                menu.AddMenuItem(UiMenu, "~y~Продать дом игроку", $"~b~{h.address} #{h.id}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowPlayerSellHouseMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f), User.Data.id_house, $"{h.address} #{h.id}");
                };
                menu.AddMenuItem(UiMenu, "~g~Подселить игрока к себе", $"~b~{h.address} #{h.id}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowPlayerHookupHouseMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f), User.Data.id_house, $"{h.address} #{h.id}");
                };
                menu.AddMenuItem(UiMenu, "~r~Выселить всех из дома", $"~b~{h.address} #{h.id}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowPlayerAntiHookupHouseMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f), User.Data.id_house, $"{h.address} #{h.id}");
                    Notification.SendWithTime("~r~Теперь с вами никто больше не живет :(");
                };
            }
            
            if (User.Data.condo_id > 0)
            {
                CondoInfoGlobalData h = Condo.GetHouseFromId(User.Data.condo_id);
                var nalog = h.price * (100 - Coffer.GetNalog()) / 100;
                menu.AddMenuItem(UiMenu, "Продать квартиру", $"Продать квартиру государству\nЦена: ~g~${nalog:#,#}").Activated += (uimenu, item) =>
                {
                    HideMenu();

                    ShowAskSellKMenu();

                };
            }
            
            if (User.Data.stock_id > 0)
            {
                StockInfoGlobalData h = Stock.GetStockFromId(User.Data.stock_id);
                var nalog = h.price * (100 - Coffer.GetNalog()) / 100;
                menu.AddMenuItem(UiMenu, "Продать склад", $"Продать склад государству\nЦена: ~g~${nalog:#,#}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowAskSellSMenu();
                };
                /*menu.AddMenuItem(UiMenu, "~y~Продать склад игроку", $"~b~{h.address} #{h.id}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowPlayerSellStockMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f), User.Data.id_house, $"{h.address} #{h.id}");
                };*/
            }
            
            if (User.Data.car_id1 > 0)
            {
                var vehicle = Managers.Vehicle.GetVehicleById(User.Data.car_id1);
                var nalog = vehicle.price * (100 - (Coffer.GetNalog() + 20)) / 100;
                menu.AddMenuItem(UiMenu, "Продать ТС #1", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Цена: ~s~${nalog:#,#}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    var temp = "car_id1";
                    ShowAskSellTrMenu(temp);

                };
                menu.AddMenuItem(UiMenu, "~y~Продать ТС #1 игроку", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowPlayerSellCarMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f), User.Data.car_id1, vehicle.Number, vehicle.DisplayName);
                };
            }
            
            if (User.Data.car_id2 > 0) {
                var vehicle = Managers.Vehicle.GetVehicleById(User.Data.car_id2);
                var nalog = vehicle.price * (100 - (Coffer.GetNalog() + 20)) / 100;
                menu.AddMenuItem(UiMenu, "Продать ТС #2", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Цена: ~s~${nalog:#,#}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    var temp = "car_id2";
                    ShowAskSellTrMenu(temp);
                };
                menu.AddMenuItem(UiMenu, "~y~Продать ТС #2 игроку", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowPlayerSellCarMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f), User.Data.car_id2, vehicle.Number, vehicle.DisplayName);
                };
            }
            
            if (User.Data.car_id3 > 0) {
                var vehicle = Managers.Vehicle.GetVehicleById(User.Data.car_id3);
                var nalog = vehicle.price * (100 - (Coffer.GetNalog() + 20)) / 100;
                menu.AddMenuItem(UiMenu, "Продать ТС #3", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Цена: ~s~${nalog:#,#}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    var temp = "car_id3";
                    ShowAskSellTrMenu(temp);
                };
                menu.AddMenuItem(UiMenu, "~y~Продать ТС #3 игроку", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowPlayerSellCarMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f), User.Data.car_id3, vehicle.Number, vehicle.DisplayName);
                };
            }
            
            if (User.Data.car_id4 > 0) {
                var vehicle = Managers.Vehicle.GetVehicleById(User.Data.car_id4);
                var nalog = vehicle.price * (100 - (Coffer.GetNalog() + 20)) / 100;
                menu.AddMenuItem(UiMenu, "Продать ТС #4", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Цена: ~s~${nalog:#,#}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    var temp = "car_id4";
                    ShowAskSellTrMenu(temp);
                };
                menu.AddMenuItem(UiMenu, "~y~Продать ТС #4 игроку", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowPlayerSellCarMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f), User.Data.car_id4, vehicle.Number, vehicle.DisplayName);
                };
            }
            
            if (User.Data.car_id5 > 0) {
                var vehicle = Managers.Vehicle.GetVehicleById(User.Data.car_id5);
                var nalog = vehicle.price * (100 - (Coffer.GetNalog() + 20)) / 100;
                menu.AddMenuItem(UiMenu, "Продать ТС #5", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Цена: ~s~${nalog:#,#}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    var temp = "car_id5";
                    ShowAskSellTrMenu(temp);
                };
                menu.AddMenuItem(UiMenu, "~y~Продать ТС #5 игроку", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowPlayerSellCarMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f), User.Data.car_id5, vehicle.Number, vehicle.DisplayName);
                };
            }
            
            if (User.Data.car_id6 > 0) {
                var vehicle = Managers.Vehicle.GetVehicleById(User.Data.car_id6);
                var nalog = vehicle.price * (100 - (Coffer.GetNalog() + 20)) / 100;
                menu.AddMenuItem(UiMenu, "Продать ТС #6", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Цена: ~s~${nalog:#,#}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    var temp = "car_id6";
                    ShowAskSellTrMenu(temp);
                };
                menu.AddMenuItem(UiMenu, "~y~Продать ТС #6 игроку", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowPlayerSellCarMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f), User.Data.car_id6, vehicle.Number, vehicle.DisplayName);
                };
            }
            
            if (User.Data.car_id7 > 0) {
                var vehicle = Managers.Vehicle.GetVehicleById(User.Data.car_id7);
                var nalog = vehicle.price * (100 - (Coffer.GetNalog() + 20)) / 100;
                menu.AddMenuItem(UiMenu, "Продать ТС #7", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Цена: ~s~${nalog:#,#}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    var temp = "car_id7";
                    ShowAskSellTrMenu(temp);
                };
                menu.AddMenuItem(UiMenu, "~y~Продать ТС #7 игроку", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowPlayerSellCarMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f), User.Data.car_id7, vehicle.Number, vehicle.DisplayName);
                };
            }
            
            if (User.Data.car_id8 > 0) {
                var vehicle = Managers.Vehicle.GetVehicleById(User.Data.car_id8);
                var nalog = vehicle.price * (100 - (Coffer.GetNalog() + 20)) / 100;
                menu.AddMenuItem(UiMenu, "Продать ТС #8", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Цена: ~s~${nalog:#,#}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    
                    var temp = "car_id8";
                    ShowAskSellTrMenu(temp);
                };
                menu.AddMenuItem(UiMenu, "~y~Продать ТС #8 игроку", $"~b~Марка: ~s~{vehicle.DisplayName}\n~b~Номера: ~s~{vehicle.Number}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowPlayerSellCarMenu(Main.GetPlayerListOnRadius(GetEntityCoords(GetPlayerPed(-1), true), 1f), User.Data.car_id8, vehicle.Number, vehicle.DisplayName);
                };
            }
      
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowMazeBankOfficeMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowMazeBankOfficeTaxMenu()
        {
            HideMenu();
            
            await Delay(500);
            
            var menu = new Menu();
            UiMenu = menu.Create("Maze", "~b~Налоговый кабинет");

            await User.GetAllData();
            
            menu.AddMenuItem(UiMenu, "Оплатить налог по номеру счёта").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int score = Convert.ToInt32(await Menu.GetUserInput("Счёт", null, 10));
                int num = Convert.ToInt32(await Menu.GetUserInput(Lang.GetTextToPlayer("_lang_1"), null, 8));
                if (num > await User.GetBankMoney())
                {
                    Notification.SendWithTime("~r~У вас на счёте недостаточно денег");
                    return;
                }
                TriggerServerEvent("ARP:PayTax", 1, num, score);
            };
            
            if (User.Data.id_house > 0)
            {
                menu.AddMenuItem(UiMenu, "Налог за дом").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowMazeBankOfficeTaxInfoMenu(0, User.Data.id_house);
                };
            }
            
            if (User.Data.condo_id > 0)
            {
                menu.AddMenuItem(UiMenu, "Налог за квартиру").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowMazeBankOfficeTaxInfoMenu(5, User.Data.condo_id);
                };
            }
            
            if (User.Data.stock_id > 0)
            {
                menu.AddMenuItem(UiMenu, "Налог за склад").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowMazeBankOfficeTaxInfoMenu(4, User.Data.stock_id);
                };
            }
            
            if (User.Data.business_id > 0)
            {
                menu.AddMenuItem(UiMenu, "Налог за бизнес").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowMazeBankOfficeTaxInfoMenu(2, User.Data.business_id);
                };
            }
            
            if (User.Data.apartment_id > 0)
            {
                menu.AddMenuItem(UiMenu, "Налог за апартаменты").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowMazeBankOfficeTaxInfoMenu(3, User.Data.apartment_id);
                };
            }
            
            if (User.Data.car_id1 > 0)
            {
                menu.AddMenuItem(UiMenu, "Налог за ТС #1").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowMazeBankOfficeTaxInfoMenu(1, User.Data.car_id1);
                };
            }
            
            if (User.Data.car_id2 > 0) {
                menu.AddMenuItem(UiMenu, "Налог за ТС #2").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowMazeBankOfficeTaxInfoMenu(1, User.Data.car_id2);
                };
            }
            
            if (User.Data.car_id3 > 0) {
                menu.AddMenuItem(UiMenu, "Налог за ТС #3").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowMazeBankOfficeTaxInfoMenu(1, User.Data.car_id3);
                };
            }
            
            if (User.Data.car_id4 > 0) {
                menu.AddMenuItem(UiMenu, "Налог за ТС #4").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowMazeBankOfficeTaxInfoMenu(1, User.Data.car_id4);
                };
            }
            
            if (User.Data.car_id5 > 0) {
                menu.AddMenuItem(UiMenu, "Налог за ТС #5").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowMazeBankOfficeTaxInfoMenu(1, User.Data.car_id5);
                };
            }
            
            if (User.Data.car_id6 > 0) {
                menu.AddMenuItem(UiMenu, "Налог за ТС #6").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowMazeBankOfficeTaxInfoMenu(1, User.Data.car_id6);
                };
            }
            
            if (User.Data.car_id7 > 0) {
                menu.AddMenuItem(UiMenu, "Налог за ТС #7").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowMazeBankOfficeTaxInfoMenu(1, User.Data.car_id7);
                };
            }
            
            if (User.Data.car_id8 > 0) {
                menu.AddMenuItem(UiMenu, "Налог за ТС #8").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowMazeBankOfficeTaxInfoMenu(1, User.Data.car_id8);
                };
            }
      
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowMazeBankOfficeMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowMazeBankOfficeTaxInfoMenu(int type, int id)
        {
            HideMenu();

            int tax = 0;
            int taxLimit = 0;
            int taxDay = 0;
            int score = 0;
            string name = "";

            if (type == 0)
            {
                HouseInfoGlobalData item = House.GetHouseFromId(id);
                taxDay = Convert.ToInt32((item.price * 0.0001 + 10) / 7);
                tax = (int) await Sync.Data.Get(100000 + item.id, "money_tax");
                taxLimit = Convert.ToInt32(item.price * 0.0001 + 10) * 21;
                score = (int) await Sync.Data.Get(100000 + item.id, "score_tax");

                name = item.address + " №" + item.id;
            }
            else if (type == 1)
            {
                var item = Managers.Vehicle.GetVehicleById(id);
                taxDay = Convert.ToInt32((item.price * 0.0001 + 10) / 7);
                tax = (int) await Sync.Data.Get(110000 + item.id, "money_tax");
                taxLimit = Convert.ToInt32(item.price * 0.0001 + 10) * 21;
                score = (int) await Sync.Data.Get(110000 + item.id, "score_tax");
                
                name = item.DisplayName + " (" + item.Number + ")";
            }
            else if (type == 2)
            {
                var item = await Business.Business.GetAllData(id);
                taxDay = Convert.ToInt32((item.price * 0.0001 + 10) / 7);
                tax = (int) await Sync.Data.Get(-20000 + item.id, "money_tax");
                taxLimit = Convert.ToInt32(item.price * 0.0001 + 10) * 21;
                score = (int) await Sync.Data.Get(-20000 + item.id, "score_tax");
                
                name = item.name;
            }
            else if (type == 3)
            {
                var item = await Apartment.GetAllData(id);
                taxDay = Convert.ToInt32((item.price * 0.0001 + 10) / 7);
                tax = (int) await Sync.Data.Get(-100000 + item.id, "money_tax");
                taxLimit = Convert.ToInt32(item.price * 0.0001 + 10) * 21;
                score = (int) await Sync.Data.Get(-100000 + item.id, "score_tax");
                
                name = "Апартаменты №" + item.id;
            }
            else if (type == 4)
            {
                StockInfoGlobalData item = Stock.GetStockFromId(id);
                taxDay = Convert.ToInt32((item.price * 0.0001 + 10) / 7);
                tax = (int) await Sync.Data.Get(200000 + item.id, "money_tax");
                taxLimit = Convert.ToInt32(item.price * 0.0001 + 10) * 21;
                score = (int) await Sync.Data.Get(200000 + item.id, "score_tax");
                
                name = "Склад №" + item.id;
            }
            else if (type == 5)
            {
                CondoInfoGlobalData item = Condo.GetHouseFromId(id);
                taxDay = Convert.ToInt32((item.price * 0.0001 + 10) / 7);
                tax = (int) await Sync.Data.Get(300000 + item.id, "money_tax");
                taxLimit = Convert.ToInt32(item.price * 0.0001 + 10) * 21;
                score = (int) await Sync.Data.Get(300000 + item.id, "score_tax");

                name = item.address + " №" + item.id;
            }

            var menu = new Menu();
            UiMenu = menu.Create("Maze", name);
            
            menu.AddMenuItem(UiMenu, $"~b~Счёт:~s~ {score}", "Уникальный счёт вашего имущества");
            menu.AddMenuItem(UiMenu, $"~b~Ваша задолженность:~s~ ~r~{(tax == 0 ? "~g~Отсутствует" : $"${tax:#,#}")}", $"Ваш текущий долг, при достижении ~r~${taxLimit:#,#}~s~ ваше имущество будет изъято");
            menu.AddMenuItem(UiMenu, $"~b~Налог в день:~s~ ${taxDay:#,#}", "Индвивидуальная налоговая ставка");
            menu.AddMenuItem(UiMenu, $"~b~Допустимый лимит:~s~ ${taxLimit:#,#}", "Допустимый лимит до обнуления имущества");
            
            menu.AddMenuItem(UiMenu, $"~g~Оплатить наличкой", $"К оплате ${tax*-1:#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int num = Convert.ToInt32(await Menu.GetUserInput(Lang.GetTextToPlayer("_lang_1"), null, 8));
                if (num > await User.GetCashMoney())
                {
                    Notification.SendWithTime("~r~У вас на руках недостаточно денег");
                    return;
                }
                TriggerServerEvent("ARP:PayTax", 0, num, score);
            };
            
            menu.AddMenuItem(UiMenu, $"~g~Оплатить картой", $"К оплате ${tax*-1:#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int num = Convert.ToInt32(await Menu.GetUserInput(Lang.GetTextToPlayer("_lang_1"), null, 8));
                if (num > await User.GetBankMoney())
                {
                    Notification.SendWithTime("~r~У вас на счёте недостаточно денег");
                    return;
                }
                TriggerServerEvent("ARP:PayTax", 1, num, score);
            };
                        
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowRentMenu(int shopId, int price = 1)
        {
            HideMenu();

            price = await Business.Business.GetPrice(shopId);
            
            var menu = new Menu();
            UiMenu = menu.Create("Аренда", "Нажмите \"~g~Enter~s~\", чтобы арендовать.");
            
            menu.AddMenuItem(UiMenu, "Cruiser", $"Цена: ~g~${10}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Rent.Buy(1, 10, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "BMX", $"Цена: ~g~${15}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Rent.Buy(2, 15, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Scorcher", $"Цена: ~g~${20}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Rent.Buy(3,20, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Fixter", $"Цена: ~g~${25}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Rent.Buy(4, 25, shopId);
            };
            menu.AddMenuItem(UiMenu, "Tri-Cycles Race Bike", $"Цена: ~g~${30}").Activated += (uimenu, item) =>
            {
               HideMenu();
               Business.Rent.Buy(5, 30, shopId);
            };
            menu.AddMenuItem(UiMenu, "Whippet Race Bike", $"Цена: ~g~${30}").Activated += (uimenu, item) =>
            {
               HideMenu();
               Business.Rent.Buy(6, 30, shopId);
            };
            menu.AddMenuItem(UiMenu, "Endurex Race Bike", $"Цена: ~g~${30}").Activated += (uimenu, item) =>
            {
               HideMenu();
               Business.Rent.Buy(7, 30, shopId);
            };
            menu.AddMenuItem(UiMenu, "Faggio", $"Цена: ~g~${60}").Activated += (uimenu, item) =>
            {
               HideMenu();
               Business.Rent.Buy(8, 60, shopId);
            };
            menu.AddMenuItem(UiMenu, "Faggio Sport", $"Цена: ~g~${80}").Activated += (uimenu, item) =>
            {
               HideMenu();
               Business.Rent.Buy(9, 80, shopId);
            };

        var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static async void ShowRentCarClassMenu(string className, int shopId, int price = 1)
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create("Аренда", "Нажмите \"~g~Enter~s~\", чтобы арендовать.");

            for (int i = 0; i < Rent.CarRent.Length / 4; i++)
            {
                var i1 = i;
                if (className != (string) Rent.CarRent[i1, 2]) continue;
                menu.AddMenuItem(UiMenu, (string) Rent.CarRent[i1, 1], $"Цена: ~g~${((int) Rent.CarRent[i1, 3] * price):#,#}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Business.Rent.BuyCar((VehicleHash) GetHashKey((string) Rent.CarRent[i1, 1]), (int) Rent.CarRent[i1, 3] * price, shopId);
                };
            }
            
            menu.AddMenuItem(UiMenu, "~r~Закрыть").Activated += (uimenu, item) =>
            {
                HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }

        public static async void ShowRentCarMenu(int shopId, int price = 1)
        {
            HideMenu();

            price = await Business.Business.GetPrice(shopId);
            
            if (shopId == 88) {
                ShowRentCarClassMenu("Boats", shopId, price);
                return;
            }
            else if (shopId == 89 || shopId == 90) {
                ShowRentCarClassMenu("Helicopters", shopId, price);
                return;
            }
            else if (shopId == 93) {
                ShowRentCarClassMenu("Planes", shopId, price);
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Аренда", "Нажмите \"~g~Enter~s~\", чтобы арендовать.");

            if (shopId == 86 || shopId == 87)
            {
                menu.AddMenuItem(UiMenu, "Cycles").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowRentCarClassMenu("Cycles", shopId, price);
                };
                
                menu.AddMenuItem(UiMenu, "Compacts").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowRentCarClassMenu("Compacts", shopId, price);
                };
                
                menu.AddMenuItem(UiMenu, "Coupes").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowRentCarClassMenu("Coupes", shopId, price);
                };
                
                menu.AddMenuItem(UiMenu, "Industrial").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowRentCarClassMenu("Industrial", shopId, price);
                };
                
                menu.AddMenuItem(UiMenu, "Motorcycles").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowRentCarClassMenu("Motorcycles", shopId, price);
                };
                
                menu.AddMenuItem(UiMenu, "Muscle").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowRentCarClassMenu("Muscle", shopId, price);
                };
                
                menu.AddMenuItem(UiMenu, "Off-Road").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowRentCarClassMenu("Off-Road", shopId, price);
                };
                
                menu.AddMenuItem(UiMenu, "Sedans").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowRentCarClassMenu("Sedans", shopId, price);
                };
                
                menu.AddMenuItem(UiMenu, "Sports").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowRentCarClassMenu("Sports", shopId, price);
                };
                
                menu.AddMenuItem(UiMenu, "Super").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowRentCarClassMenu("Super", shopId, price);
                };
                
                menu.AddMenuItem(UiMenu, "Sports Classics").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowRentCarClassMenu("Sports Classics", shopId, price);
                };
                
                menu.AddMenuItem(UiMenu, "SUVs").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowRentCarClassMenu("SUVs", shopId, price);
                };
                
                menu.AddMenuItem(UiMenu, "Utility").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowRentCarClassMenu("Utility", shopId, price);
                };
                
                }
            else if (shopId == 114)
            {
                menu.AddMenuItem(UiMenu, "Stanier Taxi", $"Цена: ~g~$150").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Business.Rent.BuyTaxi(VehicleHash.Taxi, 150, shopId);
                };
                menu.AddMenuItem(UiMenu, "Minivan Taxi", $"Цена: ~g~$100").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Business.Rent.BuyTaxi((VehicleHash) 1884962369, 100, shopId);
                };
            }
            else if (shopId == 92)
            {
                menu.AddMenuItem(UiMenu, "Stanier Taxi", $"Цена: ~g~$150").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Business.Rent.BuyTaxi((VehicleHash) 2088999036, 150, shopId);
                };

                if (User.IsJobPhoto())
                {
                    menu.AddMenuItem(UiMenu, "Фургон фотографа").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        ShowJobKeyMenu("Работа", "Ключи рабочего транспорта");
                    };
                }
            }
                        
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowGunShopMenu(int shopId, int price = 1)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create(" ", "~b~Магазин оружия");
            menu.SetMenuBannerSprite(UiMenu, "shopui_title_gunclub", "shopui_title_gunclub");
            
            menu.AddMenuItem(UiMenu, "Кастет", $"Цена: ~g~${(30 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Gun.Buy(64, 30 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Нож", $"Цена: ~g~${(40 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Gun.Buy(63, 40 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Выкидной нож", $"Цена: ~g~${(50 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Gun.Buy(69, 50 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Кавалерийский кинжал", $"Цена: ~g~${(200 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Gun.Buy(54, 200 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Мачете", $"Цена: ~g~${(250 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Gun.Buy(65, 250 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Taurus PT92", $"Цена: ~g~${(250 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(77, 250 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "HK P7M10", $"Цена: ~g~${(350 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(80, 350 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Colt Junior", $"Цена: ~g~${(400 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(81, 400 * price, 1, shopId);
            };
            
            /*menu.AddMenuItem(UiMenu, "Сolt SCAMP", $"Цена: ~g~${(300 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(70, 300 * price, 1, shopId);
            };*/
            
            menu.AddMenuItem(UiMenu, "P99", $"Цена: ~g~${(450 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(71, 450 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "FN Model 1922", $"Цена: ~g~${(1900 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(83, 1900 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Обрез", $"Цена: ~g~${(2820 * price):#,#} ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(87, 2820 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Mossberg 500", $"Цена: ~g~${(3820 * price):#,#} ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(92, 3820 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Benelli M3", $"Цена: ~g~${(4400 * price):#,#} ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(90, 4400 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Сайга-12К", $"Цена: ~g~${(5650 * price):#,#} ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(88, 5650 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "TEC-9", $"Цена: ~g~${(900 * price):#,#} ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(99, 900 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "MP5A3", $"Цена: ~g~${(1220 * price):#,#} ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(103, 1220 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "MP5K", $"Цена: ~g~${(1420 * price):#,#} ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(104, 1420 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "P-90", $"Цена: ~g~${(1620 * price):#,#} ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(94, 1620 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "SIG MPX-SD", $"Цена: ~g~${(4620 * price):#,#} ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(97, 4620 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Mk 48", $"Цена: ~g~${(19500 * price):#,#} ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(95, 19500 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "ПКП «Печенег»", $"Цена: ~g~${(19500 * price):#,#} ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(100, 19500 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "HK-416", $"Цена: ~g~${(9800 * price):#,#} ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(110, 9800 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "AK-102", $"Цена: ~g~${(12000 * price):#,#} ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(106, 12000 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "G36C", $"Цена: ~g~${(10500 * price):#,#} ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(113, 10500 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "AKS-47u", $"Цена: ~g~${(8800 * price):#,#} ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(112, 8800 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "QBZ-97", $"Цена: ~g~${(9900 * price):#,#} ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(108, 9900 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Коробка патронов 12.7mm", $"Цена: ~g~${(800 * price):#,#} за 60пт ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(146, 800 * price, 60, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Коробка патронов 9mm (SMG)", $"Цена: ~g~${(300 * price):#,#} за 140пт ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(153, 300 * price, 140, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Коробка патронов 9mm (Пистолет)", $"Цена: ~g~${(300 * price):#,#} за 140пт ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(27, 300 * price, 140, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Коробка патронов 18.5mm", $"Цена: ~g~${(900 * price):#,#} за 120пт ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(28, 900 * price, 120, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Коробка патронов 7.62mm", $"Цена: ~g~${(900 * price):#,#} за 130пт ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(29, 900 * price, 130, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Коробка патронов 5.56mm", $"Цена: ~g~${(1100 * price):#,#} за 260пт ").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (!User.Data.gun_lic)
                {
                    Notification.SendWithTime("~r~Без лицензии нельзя купить это оружие");
                    return;
                }
                
                Business.Gun.Buy(30, 1100 * price, 260, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Лёгкая броня", $"Цена: ~g~${(150 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();

                if (User.Data.money < 150 * price)
                {
                    Notification.SendWithTime("~r~недостаточно средств");
                    return;
                }
                
                SetPedArmour(GetPlayerPed(-1), 30);
                Business.Business.AddMoney(shopId, 150 * price);
                User.RemoveMoney(150 * price);
            };
            
            menu.AddMenuItem(UiMenu, "Средняя броня", $"Цена: ~g~${(250 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();

                if (User.Data.money < 250 * price)
                {
                    Notification.SendWithTime("~r~недостаточно средств");
                    return;
                }
                
                SetPedArmour(GetPlayerPed(-1), 60);
                Business.Business.AddMoney(shopId, 250 * price);
                User.RemoveMoney(250 * price);
            };
            
            menu.AddMenuItem(UiMenu, "Тяжелая броня", $"Цена: ~g~${(350 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();

                if (User.Data.money < 350 * price)
                {
                    Notification.SendWithTime("~r~недостаточно средств");
                    return;
                }
                
                SetPedArmour(GetPlayerPed(-1), 100);
                Business.Business.AddMoney(shopId, 350 * price);
                User.RemoveMoney(350 * price);
            };
            
            menu.AddMenuItem(UiMenu, "Парашют", $"Цена: ~g~${(180 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();

                if (User.Data.money < 180 * price)
                {
                    Notification.SendWithTime("~r~недостаточно средств");
                    return;
                }
                
                User.GiveWeapon((uint) WeaponHash.Parachute, 1, false, false);
                Business.Business.AddMoney(shopId, 180 * price);
                User.RemoveMoney(180 * price);
                //Business.Gun.Buy("Parachute", 80 * price, 1, shopId);
            };
            
            if (User.Data.fraction_id2 > 0)
            {
                menu.AddMenuItem(UiMenu, "~o~Ограбить").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Grab.GrabShop(shopId);
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowCasinoRateMenu(int casinoId, int gameType, int rate = 10)
        {
            HideMenu();

            if (CitizenFX.Core.UI.Screen.LoadingPrompt.IsActive)
            {
                Notification.SendWithTime("~r~Данное действие сейчас недоступно");
                return;
            }

            int casinoBank = await Business.Business.GetMoney(casinoId);

            if (casinoBank < 10)
            {
                Notification.SendWithTime("~r~Казино сейчас не работает");
                return;
            }
            
            if (User.Data.money < rate)
            {
                Notification.SendWithTime("~r~У Вас нет денег для ставки");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create("Казино", "~b~Меню казино");
            
            menu.AddMenuItem(UiMenu, "Сделать ставку", $"Ставка: ~g~${rate:#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int money = Convert.ToInt32(await Menu.GetUserInput("Ставка", "", 8));
                if (money < 1)
                {
                    Notification.SendWithTime("~r~Ставка должна быть больше нуля");
                    ShowCasinoRateMenu(casinoId, gameType);
                    return;
                }
                if (money > User.GetMoneyWithoutSync())
                {
                    Notification.SendWithTime("~r~У вас нет столько денег");
                    ShowCasinoRateMenu(casinoId, gameType);
                    return;
                }
                if (money * 3 > casinoBank)
                {
                    Notification.SendWithTime("~r~В банке казино нет такой большой суммы");
                    ShowCasinoRateMenu(casinoId, gameType);
                    return;
                }
                Notification.SendWithTime($"~g~Новая ставка: ${money:#,#}");
                ShowCasinoRateMenu(casinoId, gameType, money);
            };

            if (gameType == 1)
            {
                var list = new List<dynamic> {"Red", "Black", "Zero"};
                menu.AddMenuItemList(UiMenu, "Начать игру в рулетку", list).OnListSelected += (uimenu, idx) =>
                {
                    HideMenu();
                    Business.Casino.StartRulet(casinoId, rate, idx);
                };

                menu.AddMenuItem(UiMenu, "~y~Правила игры").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Chat.SendChatInfoMessage("СПРАВКА | Казино | Рулетка",
                        "<br>- У вас есть выбор, поставить на черное, красное или зеро" +
                        "<br>- Если вы поставили на черное или красное и вы угадали, ваш выигрыш будет умножен на 1.5" +
                        "<br>- Если вы поставили на зеро и угадали, то ваш выигрыш будет умножен на 5" +
                        "<br>- В ином случае вся ваша ставка сгорает");
                };
            }
            else
            {
                menu.AddMenuItem(UiMenu, "Начать игру в комбо").Activated += (uimenu, idx) =>
                {
                    HideMenu();
                    Business.Casino.StartCombo(casinoId, rate);
                };
                
                menu.AddMenuItem(UiMenu, "~y~Правила игры").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Chat.SendChatInfoMessage("СПРАВКА | Казино | Комбо", 
                        "<br>- Если у вас выпали три одинаковых числа, то ваш выигрыш будет умножен на 3" +
                        "<br>- Если у вас выпали два одинаковых числа, то ваш выигрыш будет умножен на 2" +
                        "<br>- В ином случае вся ваша ставка сгорает");
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                {
                    Business.Casino.Stop();
                    HideMenu();
                }
            };
            
            UiMenu.OnMenuClose += sender =>
            {
                HideMenu();
                Business.Casino.Stop();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowBarMenu(int shopId, int price = 1)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Бар", "~b~Меню");
            
            menu.AddMenuItem(UiMenu, "Вода", $"Цена: ~g~${(5 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Bar.Buy("water", 5 * price, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Кола", $"Цена: ~g~${(10 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Bar.Buy("cola", 10 * price, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Лимонад", $"Цена: ~g~${(7 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Bar.Buy("limonad", 7 * price, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Пиво", $"Цена: ~g~${(10 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Bar.Buy("bear", 10 * price, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Водка", $"Цена: ~g~${(19 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Bar.Buy("vodka", 19 * price, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Виски", $"Цена: ~g~${(28 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Bar.Buy("whishkey", 28 * price, shopId);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowBarFreeMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Бар", "~b~Меню");
            
            menu.AddMenuItem(UiMenu, "Вода").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Bar.Buy("water", 0, 1);
            };
            
            menu.AddMenuItem(UiMenu, "Кола").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Bar.Buy("cola", 0, 1);
            };
            
            menu.AddMenuItem(UiMenu, "Лимонад").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Bar.Buy("limonad", 0, 1);
            };
            
            menu.AddMenuItem(UiMenu, "Пиво").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Bar.Buy("bear", 0, 1);
            };
            
            menu.AddMenuItem(UiMenu, "Водка").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Bar.Buy("vodka", 0, 1);
            };
            
            menu.AddMenuItem(UiMenu, "Виски").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Bar.Buy("whishkey", 0, 1);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowElectroShopMenu(int shopId)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Digital Den", "Нажмите \"~g~Enter~s~\", чтобы купить.");
            
            menu.AddMenuItem(UiMenu, "Телефон IFruit", "Цена: ~g~$200").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Shop.Buy(8, 200, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Телефон Invader", "Цена: ~g~$600").Activated += (uimenu, item) =>
            {
                HideMenu();
                if (User.Data.money < 600)
                {
                    Notification.SendWithTime("~r~Нужно иметь $600");
                    return;
                }
                if (User.Data.phone_code > 0 || User.Data.phone > 0)
                {
                    Notification.SendWithTime("~r~У Вас уже есть телефон");
                    return;
                }

                Random rand = new Random();
                    
                User.Data.phone_code = 777;
                User.Data.phone = rand.Next(10000, 999999);

                Sync.Data.Set(User.GetServerId(), "phone_code", User.Data.phone_code);
                Sync.Data.Set(User.GetServerId(), "phone", User.Data.phone);
                Notification.SendWithTime($"~g~Вы купили телефон\nВаш номер: ~s~{User.Data.phone_code}-{User.Data.phone}");

                User.RemoveMoney(600);
                Business.Business.AddMoney(shopId, 300);
                Business.Business.AddMoney(92, 300);
            };
            
            menu.AddMenuItem(UiMenu, "Телефон Kali", "Цена: ~g~$2500").Activated += (uimenu, item) =>
            {
                HideMenu();
                if (User.Data.money < 2500)
                {
                    Notification.SendWithTime("~r~Нужно иметь $2500");
                    return;
                }
                if (User.Data.phone_code > 0 || User.Data.phone > 0)
                {
                    Notification.SendWithTime("~r~У Вас уже есть телефон");
                    return;
                }

                Random rand = new Random();
                    
                User.Data.phone_code = 404;
                User.Data.phone = rand.Next(10000, 999999);

                Sync.Data.Set(User.GetServerId(), "phone_code", User.Data.phone_code);
                Sync.Data.Set(User.GetServerId(), "phone", User.Data.phone);
                Notification.SendWithTime($"~g~Вы купили телефон\nВаш номер: ~s~{User.Data.phone_code}-{User.Data.phone}");
                Notification.SendWithTime("~g~На данном телефоне установлен ~b~Kali Linux");

                User.RemoveMoney(2500);
                Business.Business.AddMoney(shopId, 2500);
            };
            
            menu.AddMenuItem(UiMenu, "Электронные часы «Appi Watch»", "Цена: ~g~$350").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Shop.Buy(7, 350, shopId);
            };

            menu.AddMenuItem(UiMenu, "Рация", "Цена: ~g~$2800").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Shop.Buy(47, 2800, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Фонарик", "Цена: ~g~$50").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Gun.Buy(59, 50, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Дверь с пинкодом для дома", "Цена: ~g~$10000").Activated += async (uimenu, item) =>
            {
                HideMenu();
                if (User.Data.id_house == 0)
                {
                    Notification.SendWithTime("~r~У Вас нет дома");
                    return;
                }
                if ((int) await Client.Sync.Data.Get(100000 + User.Data.id_house, "pin") > 0)
                {
                    Notification.SendWithTime("~r~В вашем доме уже установлена данная дверь");
                    return;
                }
                if (await User.GetCashMoney() < 10000)
                {
                    Notification.SendWithTime("~r~У Вас нет столько налички");
                    return;
                }
                TriggerServerEvent("ARP:UpdateHousePin", User.Data.id_house, 9999);
                User.RemoveCashMoney(10000);
                Business.Business.AddMoney(shopId, 3500);
                Notification.SendWithTime("~g~Вы купили дверь с пинкодом для вашего дома");
            };
            
            if (User.Data.fraction_id2 > 0)
            {
                menu.AddMenuItem(UiMenu, "~o~Ограбить").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Grab.GrabShop(shopId);
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowShopMenu(int shopId, int price = 1)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create(" ", "Нажмите \"~g~Enter~s~\", чтобы купить.");

            menu.SetMenuBannerSprite(UiMenu, "shopui_title_conveniencestore", "shopui_title_conveniencestore");
            
            /*menu.AddMenuItem(UiMenu, "Телефон", $"Цена: ~g~${(100 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Shop.Buy(8, 100 * price, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Электронные часы «Appi Watch»", $"Цена: ~g~${(200 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Shop.Buy(7, 200 * price, shopId);
            };*/
            
            menu.AddMenuItem(UiMenu, "Отмычка", $"Цена: ~g~${(10 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(4, 10 * price, shopId, count);
            };
            
            menu.AddMenuItem(UiMenu, "Верёвка", $"Цена: ~g~${(10 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Shop.Buy(0, 10 * price, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Мешок", $"Цена: ~g~${(20 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Shop.Buy(1, 20 * price, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Набор инструментов", $"Цена: ~g~${(120 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Shop.Buy(6, 120 * price, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Игральные кости", $"Цена: ~g~${(1 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Shop.Buy(253, 1 * price, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Удочка", $"Цена: ~g~${(50 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Shop.Buy(251, 50 * price, shopId);
            };

            /*if (Main.ServerName != "Earth")
            {
                menu.AddMenuItem(UiMenu, "Рация", $"Цена: ~g~${(800 * price):#,#}").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Business.Shop.Buy(47, 800 * price, shopId);
                };
            }*/
            
            menu.AddMenuItem(UiMenu, "Фонарик", $"Цена: ~g~${(50 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Gun.Buy(59, 50 * price, 1, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "Еда").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowShopEatMenu(shopId, price);
            };
            
            menu.AddMenuItem(UiMenu, "Напитки").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowShopWaterMenu(shopId, price);
            };

            if (shopId != User.Data.business_id)
            {
                menu.AddMenuItem(UiMenu, "~y~Продать всё сырое мясо").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    if (!User.Data.animal_lic)
                    {
                        Notification.SendWithTime("~r~У Вас должна быть лицензия на охоту");
                        Notification.SendWithTime("~r~Приобрести её можно у правительства");
                        return;
                    }
                    TriggerServerEvent("ARP:Inventory:SellMeat", User.Data.id, shopId);
                };
            
                menu.AddMenuItem(UiMenu, "~y~Продать всю сырую рыбу").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    if (!User.Data.fish_lic)
                    {
                        Notification.SendWithTime("~r~У Вас должна быть лицензия на рыбалку");
                        Notification.SendWithTime("~r~Приобрести её можно у правительства");
                        return;
                    }
                    TriggerServerEvent("ARP:Inventory:SellFish", User.Data.id, shopId);
                };
            }

            if (User.Data.fraction_id2 > 0)
            {
                menu.AddMenuItem(UiMenu, "~o~Ограбить").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Grab.GrabShop(shopId);
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowShopEatMenu(int shopId, int price = 1)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create(" ", "Нажмите \"~g~Enter~s~\", чтобы купить.");

            menu.SetMenuBannerSprite(UiMenu, "shopui_title_conveniencestore", "shopui_title_conveniencestore");
            
            menu.AddMenuItem(UiMenu, "Жвачка", $"~b~Калории:~s~ 40\n~b~Цена: ~g~${(1 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(10, 1 * price, shopId, count);
            };
            
            menu.AddMenuItem(UiMenu, "Батончик «Pluto»", $"~b~Калории:~s~ 190\n~b~Цена: ~g~${(5 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(11, 5 * price, shopId, count);
            };
            
            menu.AddMenuItem(UiMenu, "Чипсы «AppiPot»", $"~b~Калории:~s~ 160\n~b~Цена: ~g~${(7 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(12, 7 * price, shopId, count);
            };
            
            menu.AddMenuItem(UiMenu, "Роллы", $"~b~Калории:~s~ 320\n~b~Цена: ~g~${(18 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(13, 18 * price, shopId, count);
            };
            
            menu.AddMenuItem(UiMenu, "Гамбургер", $"~b~Калории:~s~ 380\n~b~Цена: ~g~${(18 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(14, 18 * price, shopId, count);
            };
            
            menu.AddMenuItem(UiMenu, "Салат Цезарь", $"~b~Калории:~s~ 420\n~b~Цена: ~g~${(22 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(15, 22 * price, shopId, count);
            };
            
            menu.AddMenuItem(UiMenu, "Пицца", $"~b~Калории:~s~ 550\n~b~Цена: ~g~${(24 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(16, 24 * price, shopId, count);
            };
            
            menu.AddMenuItem(UiMenu, "Жаркое", $"~b~Калории:~s~ 550\n~b~Цена: ~g~${(30 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(17, 30 * price, shopId, count);
            };
            
            menu.AddMenuItem(UiMenu, "Кесадилья с мясным фаршем и сыром", $"~b~Калории:~s~ 850\n~b~Цена: ~g~${(37 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(18, 37 * price, shopId, count);
            };
            
            menu.AddMenuItem(UiMenu, "Фрикасе из кролика", $"~b~Калории:~s~ 1100\n~b~Цена: ~g~${(43 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(19, 43 * price, shopId, count);
            };
            
            menu.AddMenuItem(UiMenu, "Фрукты", $"~b~Калории:~s~ 60\n~b~Цена: ~g~${(14 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(20, 14 * price, shopId, count);
            };
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowShopMenu(shopId, price);
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowShopWaterMenu(int shopId, int price = 1)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create(" ", "Нажмите \"~g~Enter~s~\", чтобы купить.");

            menu.SetMenuBannerSprite(UiMenu, "shopui_title_conveniencestore", "shopui_title_conveniencestore");
            
            menu.AddMenuItem(UiMenu, "Вода", $"~b~Гидратация: ~s~99%\n~b~Цена: ~g~${(1 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(21, 1 * price, shopId, count);
            };
            
            menu.AddMenuItem(UiMenu, "Кофе", $"~b~Гидратация: ~s~95%\n~b~Цена: ~g~${(2 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(22, 2 * price, shopId, count);
            };
            
            menu.AddMenuItem(UiMenu, "Чай", $"~b~Гидратация: ~s~90%\n~b~Цена: ~g~${(2 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(23, 2 * price, shopId, count);
            };
            
            menu.AddMenuItem(UiMenu, "Лимонад", $"~b~Гидратация: ~s~70%\n~b~Цена: ~g~${(2 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(24, 2 * price, shopId, count);
            };
            
            menu.AddMenuItem(UiMenu, "Кока-кола", $"~b~Гидратация: ~s~55%\n~b~Цена: ~g~${(3 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(25, 3 * price, shopId, count);
            };
            
            menu.AddMenuItem(UiMenu, "Энергетик", $"~b~Гидратация: ~s~110%\n~b~Цена: ~g~${(5 * price):#,#}").Activated += async (uimenu, item) =>
            {
                HideMenu();
                int count = Convert.ToInt32(await Menu.GetUserInput("Кол-во", "", 1));
                if (count < 1)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_0"));
                    return;
                }
                Business.Shop.Buy(26, 5 * price, shopId, count);
            };
            
            var backButton = menu.AddMenuItem(UiMenu, "~g~Назад");
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
                if (item == backButton)
                    ShowShopMenu(shopId, price);
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowTattooShopMenu(string title1, string title2, int shopId)
        {
            HideMenu();
            Characher.UpdateTattoo(false);
            
            var menu = new Menu();
            UiMenu = menu.Create(" ", "~b~Тату салон");

            menu.SetMenuBannerSprite(UiMenu, title1, title2);

            menu.AddMenuItem(UiMenu, "Голова").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowTattooShopSortMenu(title1, title2, "ZONE_HEAD", shopId);
            };

            menu.AddMenuItem(UiMenu, "Торс").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowTattooShopSortMenu(title1, title2, "ZONE_TORSO", shopId);
            };

            menu.AddMenuItem(UiMenu, "Левая рука").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowTattooShopSortMenu(title1, title2, "ZONE_LEFT_ARM", shopId);
            };

            menu.AddMenuItem(UiMenu, "Правая рука").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowTattooShopSortMenu(title1, title2, "ZONE_RIGHT_ARM", shopId);
            };

            menu.AddMenuItem(UiMenu, "Левая нога").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowTattooShopSortMenu(title1, title2, "ZONE_LEFT_LEG", shopId);
            };

            menu.AddMenuItem(UiMenu, "Правая нога").Activated += (uimenu, item) =>
            {
                HideMenu();
                ShowTattooShopSortMenu(title1, title2, "ZONE_RIGHT_LEG", shopId);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowTattooShopSortMenu(string title1, string title2, string type, int shopId)
        {
            HideMenu();
            
            if (await Ctos.IsBlackout())
            {
                Notification.SendWithTime("~r~Тату салон во время блекаута не работает");
                return;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create(" ", "~b~Тату салон", true, true);

            menu.SetMenuBannerSprite(UiMenu, title1, title2);
            
            menu.AddMenuItem(UiMenu, "~o~Свести тату", "Цена: ~g~$990").Activated += (uimenu, item) =>
            {
                if (User.Data.money < 990)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_25"));
                    return;
                }
                
                switch (type)
                {
                    case "ZONE_HEAD":
                        User.Data.tattoo_head_c = "";
                        User.Data.tattoo_head_o = "";
                        
                        Sync.Data.Set(User.GetServerId(), "tattoo_head_c", "");
                        Sync.Data.Set(User.GetServerId(), "tattoo_head_o", "");
                        break;
                    case "ZONE_TORSO":
                        User.Data.tattoo_torso_c = "";
                        User.Data.tattoo_torso_o = "";
                        
                        Sync.Data.Set(User.GetServerId(), "tattoo_torso_c", "");
                        Sync.Data.Set(User.GetServerId(), "tattoo_torso_o", "");
                        break;
                    case "ZONE_LEFT_ARM":
                        User.Data.tattoo_left_arm_c = "";
                        User.Data.tattoo_left_arm_o = "";
                        
                        Sync.Data.Set(User.GetServerId(), "tattoo_left_arm_c", "");
                        Sync.Data.Set(User.GetServerId(), "tattoo_left_arm_o", "");
                        break;
                    case "ZONE_RIGHT_ARM":
                        User.Data.tattoo_right_arm_c = "";
                        User.Data.tattoo_right_arm_o = "";
                        
                        Sync.Data.Set(User.GetServerId(), "tattoo_right_arm_c", "");
                        Sync.Data.Set(User.GetServerId(), "tattoo_right_arm_o", "");
                        break;
                    case "ZONE_RIGHT_LEG":
                        User.Data.tattoo_right_leg_c = "";
                        User.Data.tattoo_right_leg_o = "";
                        
                        Sync.Data.Set(User.GetServerId(), "tattoo_right_leg_c", "");
                        Sync.Data.Set(User.GetServerId(), "tattoo_right_leg_o", "");
                        break;
                    case "ZONE_LEFT_LEG":
                        User.Data.tattoo_left_leg_c = "";
                        User.Data.tattoo_left_leg_o = "";
                        
                        Sync.Data.Set(User.GetServerId(), "tattoo_left_leg_c", "");
                        Sync.Data.Set(User.GetServerId(), "tattoo_left_leg_o", "");
                        break;
                }
                
                User.RemoveMoney(990);
                Business.Business.AddMoney(shopId, 990);
                Notification.SendSubtitle($"~g~Потрачено $990");
                Characher.UpdateTattoo();
            };

            var list = new List<UIMenuItem>();

            for (int i = 0; i < Tattoo.TattooList.Length / 6; i++)
            {
                if ((string) Tattoo.TattooList[i, 4] != type)
                    continue;
                
                if ((
                        (string) Tattoo.TattooList[i, 1] == "mpbeach_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpbiker_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpchristmas2_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpgunrunning_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mphipster_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mplowrider_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mplowrider2_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpluxe_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpluxe2_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpsmuggler_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpstunt_overlays"
                    ) && title1 == "shopui_title_tattoos")
                    continue;
                
                if ((
                        (string) Tattoo.TattooList[i, 1] == "mpairraces_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpbeach_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpbusiness_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpchristmas2_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpgunrunning_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mphipster_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpimportexport_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpluxe_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpluxe2_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpsmuggler_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpstunt_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "multiplayer_overlays"
                    ) && title1 == "shopui_title_tattoos2")
                    continue;
                
                if ((
                        (string) Tattoo.TattooList[i, 1] == "mpairraces_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpbeach_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpbiker_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpbusiness_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpchristmas2_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpgunrunning_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpimportexport_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mplowrider_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mplowrider2_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpsmuggler_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpstunt_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "multiplayer_overlays"
                    ) && title1 == "shopui_title_tattoos3")
                    continue;
                
                if ((
                        (string) Tattoo.TattooList[i, 1] == "mpairraces_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpbeach_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpbiker_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpbusiness_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpchristmas2_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpgunrunning_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mphipster_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpimportexport_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mplowrider_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mplowrider2_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpluxe_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpluxe2_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "multiplayer_overlays"
                    ) && title1 == "shopui_title_tattoos4")
                    continue;
                
                if ((
                        (string) Tattoo.TattooList[i, 1] == "mpairraces_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpbiker_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpbusiness_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mphipster_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpimportexport_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mplowrider_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mplowrider2_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpluxe_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpluxe2_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpsmuggler_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "mpstunt_overlays" ||
                        (string) Tattoo.TattooList[i, 1] == "multiplayer_overlays"
                    ) && title1 == "shopui_title_tattoos5")
                    continue;
                
                int i1 = i;
                int price = Convert.ToInt32(Convert.ToInt32(Tattoo.TattooList[i, 5]) / 10);
                if (User.Skin.SEX == 1 && (string) Tattoo.TattooList[i, 3] != "")
                {
                    var menuItem = menu.AddMenuItem(UiMenu, $"{Tattoo.TattooList[i, 0]}", $"Цена: ~g~${price:#,#}");
                    menuItem.Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        ShowTattooShopApplyMenu("shopui_title_tattoos", "shopui_title_tattoos",
                            (string) Tattoo.TattooList[i1, 1], (string) Tattoo.TattooList[i1, 3],
                            (string) Tattoo.TattooList[i1, 4], price, shopId);
                    };
                    
                    list.Add(menuItem);
                }
                else if (User.Skin.SEX == 0 && (string) Tattoo.TattooList[i, 2] != "")
                {
                    var menuItem = menu.AddMenuItem(UiMenu, $"{Tattoo.TattooList[i, 0]}", $"Цена: ~g~${price:#,#}");
                    menuItem.Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        ShowTattooShopApplyMenu("shopui_title_tattoos", "shopui_title_tattoos",
                            (string) Tattoo.TattooList[i1, 1], (string) Tattoo.TattooList[i1, 2],
                            (string) Tattoo.TattooList[i1, 4], price, shopId);
                    };
                    
                    list.Add(menuItem);
                }
            }
            
            UiMenu.OnIndexChange += (sender, index) =>
            {
                Debug.WriteLine(list[index-1].Text);
                
                if (list[index-1].Text == "~r~Закрыть" || list[index-1].Text == "Закрыть")
                    return;
                
                ClearPedDecorations(GetPlayerPed(-1));
                ApplyPedOverlay(GetPlayerPed(-1), (uint) GetHashKey(Tattoo.FindTattooCollection(list[index-1].Text)), (uint) GetHashKey(Tattoo.FindTattooOverlay(list[index-1].Text)));
            };
            
            UiMenu.OnMenuClose += (sender) =>
            {
                Characher.UpdateTattoo();
                HideMenu();
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                {
                    Characher.UpdateTattoo();
                    HideMenu();
                }
            };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowTattooShopApplyMenu(string title1, string title2, string collection, string overlay, string type, int price, int shopId)
        {
            HideMenu();
            
            ClearPedDecorations(GetPlayerPed(-1));
            ApplyPedOverlay(GetPlayerPed(-1), (uint) GetHashKey(collection), (uint) GetHashKey(overlay));
            
            var menu = new Menu();
            UiMenu = menu.Create(" ", $"~b~Цена:~s~ ${price:#,#}", true, true);

            menu.SetMenuBannerSprite(UiMenu, title1, title2);

            menu.AddMenuItem(UiMenu, "~g~Купить").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (User.Data.money < price)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_25"));
                    return;
                }
                
                switch (type)
                {
                    case "ZONE_HEAD":
                        if (!IsStringNullOrEmpty(User.Data.tattoo_head_c) && !IsStringNullOrEmpty(User.Data.tattoo_head_o))
                        {
                            Notification.SendWithTime("~r~Татуировку сначала нужно свести");
                            Characher.UpdateTattoo(false);
                            ShowTattooShopMenu(title1, title2, shopId);
                            return;
                        }
                        
                        User.Data.tattoo_head_c = collection;
                        User.Data.tattoo_head_o = overlay;
                        
                        Sync.Data.Set(User.GetServerId(), "tattoo_head_c", collection);
                        Sync.Data.Set(User.GetServerId(), "tattoo_head_o", overlay);
                        break;
                    case "ZONE_TORSO":
                        if (!IsStringNullOrEmpty(User.Data.tattoo_torso_c) && !IsStringNullOrEmpty(User.Data.tattoo_torso_o))
                        {
                            Notification.SendWithTime("~r~Татуировку сначала нужно свести");
                            Characher.UpdateTattoo(false);
                            ShowTattooShopMenu(title1, title2, shopId);
                            return;
                        }
                        
                        User.Data.tattoo_torso_c = collection;
                        User.Data.tattoo_torso_o = overlay;
                        
                        Sync.Data.Set(User.GetServerId(), "tattoo_torso_c", collection);
                        Sync.Data.Set(User.GetServerId(), "tattoo_torso_o", overlay);
                        break;
                    case "ZONE_LEFT_ARM":
                        if (!IsStringNullOrEmpty(User.Data.tattoo_left_arm_c) && !IsStringNullOrEmpty(User.Data.tattoo_left_arm_o))
                        {
                            Notification.SendWithTime("~r~Татуировку сначала нужно свести");
                            Characher.UpdateTattoo(false);
                            ShowTattooShopMenu(title1, title2, shopId);
                            return;
                        }
                        
                        User.Data.tattoo_left_arm_c = collection;
                        User.Data.tattoo_left_arm_o = overlay;
                        
                        Sync.Data.Set(User.GetServerId(), "tattoo_left_arm_c", collection);
                        Sync.Data.Set(User.GetServerId(), "tattoo_left_arm_o", overlay);
                        break;
                    case "ZONE_RIGHT_ARM":
                        if (!IsStringNullOrEmpty(User.Data.tattoo_right_arm_c) && !IsStringNullOrEmpty(User.Data.tattoo_right_arm_o))
                        {
                            Notification.SendWithTime("~r~Татуировку сначала нужно свести");
                            Characher.UpdateTattoo(false);
                            ShowTattooShopMenu(title1, title2, shopId);
                            return;
                        }
                        
                        User.Data.tattoo_right_arm_c = collection;
                        User.Data.tattoo_right_arm_o = overlay;
                        
                        Sync.Data.Set(User.GetServerId(), "tattoo_right_arm_c", collection);
                        Sync.Data.Set(User.GetServerId(), "tattoo_right_arm_o", overlay);
                        break;
                    case "ZONE_RIGHT_LEG":
                        if (!IsStringNullOrEmpty(User.Data.tattoo_right_leg_c) && !IsStringNullOrEmpty(User.Data.tattoo_right_leg_o))
                        {
                            Notification.SendWithTime("~r~Татуировку сначала нужно свести");
                            Characher.UpdateTattoo(false);
                            ShowTattooShopMenu(title1, title2, shopId);
                            return;
                        }
                        
                        User.Data.tattoo_right_leg_c = collection;
                        User.Data.tattoo_right_leg_o = overlay;
                        
                        Sync.Data.Set(User.GetServerId(), "tattoo_right_leg_c", collection);
                        Sync.Data.Set(User.GetServerId(), "tattoo_right_leg_o", overlay);
                        break;
                    case "ZONE_LEFT_LEG":
                        if (!IsStringNullOrEmpty(User.Data.tattoo_left_leg_c) && !IsStringNullOrEmpty(User.Data.tattoo_left_leg_o))
                        {
                            Notification.SendWithTime("~r~Татуировку сначала нужно свести");
                            Characher.UpdateTattoo(false);
                            ShowTattooShopMenu(title1, title2, shopId);
                            return;
                        }
                        
                        User.Data.tattoo_left_leg_c = collection;
                        User.Data.tattoo_left_leg_o = overlay;
                        
                        Sync.Data.Set(User.GetServerId(), "tattoo_left_leg_c", collection);
                        Sync.Data.Set(User.GetServerId(), "tattoo_left_leg_o", overlay);
                        break;
                }
                
                
                User.RemoveMoney(price);
                Business.Business.AddMoney(shopId, price);
                Notification.SendSubtitle($"~g~Потрачено ${price:#,#}");
                Characher.UpdateTattoo();
                ShowTattooShopMenu(title1, title2, shopId);
            };
            
            menu.AddMenuItem(UiMenu, "~r~Отмена").Activated += (uimenu, item) =>
            {
                Characher.UpdateTattoo();
                HideMenu();
                ShowTattooShopMenu(title1, title2, shopId);
            };
            
            UiMenu.OnMenuClose += (sender) =>
            {
                Characher.UpdateTattoo();
                HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
  public static void ShowBarberShopMenu(int shopId = 0)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create(" ", "~b~Влево/вправо менять внешность", true, true);
            
            if (shopId == 109)
                menu.SetMenuBannerSprite(UiMenu, "shopui_title_barber", "shopui_title_barber");
            else if (shopId == 110)
                menu.SetMenuBannerSprite(UiMenu, "shopui_title_barber2", "shopui_title_barber2");
            else if (shopId == 111)
                menu.SetMenuBannerSprite(UiMenu, "shopui_title_barber3", "shopui_title_barber3");
            else if (shopId == 48)
                menu.SetMenuBannerSprite(UiMenu, "shopui_title_barber4", "shopui_title_barber4");
            else if (shopId == 112)
                menu.SetMenuBannerSprite(UiMenu, "shopui_title_highendsalon", "shopui_title_highendsalon");
            
            var list = new List<dynamic>();

            if (User.Skin.SEX == 1)
            {
                for (var i = 0; i < 77; i++)
                    list.Add(i);
            }
            else
            {
                for (var i = 0; i < 72; i++)
                    list.Add(i);
            }
            
            var hairItem = menu.AddMenuItemList(UiMenu, "Причёска", list, "Цена: ~g~$400");

            hairItem.OnListChanged += (uimenu, index) =>
            {
                User.Skin.GTAO_HAIR = index;
                    
                if (index == 23 && GetEntityModel(GetPlayerPed(-1)) == 1885233650)
                    User.Skin.GTAO_HAIR = 72;
                    
                if (index == 24 && GetEntityModel(GetPlayerPed(-1)) == -1667301416)
                    User.Skin.GTAO_HAIR = 77;
                
                Characher.UpdateFace(false);
            };
            
            hairItem.OnListSelected += (uimenu, index) =>
            {
                HideMenu();
                    
                Sync.Data.Set(User.GetServerId(), "GTAO_HAIR", User.Skin.GTAO_HAIR);
                
                User.Skin.GTAO_HAIR = index;
                    
                if (index == 23 && GetEntityModel(GetPlayerPed(-1)) == 1885233650)
                    User.Skin.GTAO_HAIR = 36;
                    
                if (index == 24 && GetEntityModel(GetPlayerPed(-1)) == -1667301416)
                    User.Skin.GTAO_HAIR = 36;
                
                User.RemoveMoney(300);
                Business.Business.AddMoney(shopId, 400);
                Notification.SendSubtitle("~g~Потрачено $400");
                Characher.UpdateFace();
            };
            
            
            list = new List<dynamic>();
            for (var i = 0; i < 64; i++)
                list.Add(i);

            var hairColorItem = menu.AddMenuItemList(UiMenu, "Цвет волос", list, "Цена: ~g~$160");
            hairColorItem.OnListChanged+= (uimenu, index) =>
            {
                User.Skin.GTAO_HAIR_COLOR = index;
                Characher.UpdateFace(false);
            };
            hairColorItem.OnListSelected += (uimenu, index) =>
            {
                HideMenu();
                User.Skin.GTAO_HAIR_COLOR = index;
                Sync.Data.Set(User.GetServerId(), "GTAO_HAIR_COLOR", User.Skin.GTAO_HAIR_COLOR);
                User.RemoveMoney(160);
                Business.Business.AddMoney(shopId, 160);
                Notification.SendSubtitle("~g~Потрачено $160");
                Characher.UpdateFace();
            };

            var hairColor2Item = menu.AddMenuItemList(UiMenu, "Мелирование волос", list, "Цена: ~g~$160");
            hairColor2Item.OnListChanged+= (uimenu, index) =>
            {
                User.Skin.GTAO_HAIR_COLOR2 = index;
                Characher.UpdateFace(false);
            };
            hairColor2Item.OnListSelected += (uimenu, index) =>
            {
                HideMenu();
                User.Skin.GTAO_HAIR_COLOR2 = index;
                Sync.Data.Set(User.GetServerId(), "GTAO_HAIR_COLOR2", User.Skin.GTAO_HAIR_COLOR2);
                User.RemoveMoney(160);
                Business.Business.AddMoney(shopId, 160);
                Notification.SendSubtitle("~g~Потрачено $160");
                Characher.UpdateFace();
            };
            
            
            list = new List<dynamic>();
            for (var i = 0; i < 32; i++)
                list.Add(i);

            var eyeColorItem = menu.AddMenuItemList(UiMenu, "Цвет глаз", list, "Цена: ~g~$120");
            eyeColorItem.OnListChanged+= (uimenu, index) =>
            {
                User.Skin.GTAO_EYE_COLOR = index;
                Characher.UpdateFace(false);
            };
            eyeColorItem.OnListSelected += (uimenu, index) =>
            {
                HideMenu();
                User.Skin.GTAO_EYE_COLOR = index;
                Sync.Data.Set(User.GetServerId(), "GTAO_EYE_COLOR", User.Skin.GTAO_EYE_COLOR);
                User.RemoveMoney(120);
                Business.Business.AddMoney(shopId, 120);
                Notification.SendSubtitle("~g~Потрачено $120");
                Characher.UpdateFace();
            };
            
            
            list = new List<dynamic>();
            for (var i = 0; i < 30; i++)
                list.Add(i);

            var browsItem = menu.AddMenuItemList(UiMenu, "Брови", list, "Цена: ~g~$70");
            browsItem.OnListChanged+= (uimenu, index) =>
            {
                User.Skin.GTAO_EYEBROWS = index;
                Characher.UpdateFace(false);
            };
            browsItem.OnListSelected += (uimenu, index) =>
            {
                HideMenu();
                User.Skin.GTAO_EYEBROWS = index;
                Sync.Data.Set(User.GetServerId(), "GTAO_EYEBROWS", User.Skin.GTAO_EYEBROWS);
                User.RemoveMoney(70);
                Business.Business.AddMoney(shopId, 70);
                Notification.SendSubtitle("~g~Потрачено $70");
                Characher.UpdateFace();
            };
            
            
            list = new List<dynamic>();
            for (var i = 0; i < 64; i++)
                list.Add(i);

            var browsColorItem = menu.AddMenuItemList(UiMenu, "Цвет бровей", list, "Цена: ~g~$50");
            browsColorItem.OnListChanged+= (uimenu, index) =>
            {
                User.Skin.GTAO_EYEBROWS_COLOR = index;
                Characher.UpdateFace(false);
            };
            browsColorItem.OnListSelected += (uimenu, index) =>
            {
                HideMenu();
                User.Skin.GTAO_EYEBROWS_COLOR = index;
                Sync.Data.Set(User.GetServerId(), "GTAO_EYEBROWS_COLOR", User.Skin.GTAO_EYEBROWS_COLOR);
                User.RemoveMoney(50);
                Business.Business.AddMoney(shopId, 50);
                Notification.SendSubtitle("~g~Потрачено $50");
                Characher.UpdateFace();
            };
            
            list = new List<dynamic>{"~r~Нет"};
            for (var i = 1; i < 10; i++)
                list.Add(i);
            var frecklesItem = menu.AddMenuItemList(UiMenu, "Веснушки", list, "Цена: ~g~$250");
            frecklesItem.OnListChanged+= (uimenu, index) =>
            {
                User.Skin.GTAO_OVERLAY9 = index - 1;
                Characher.UpdateFace(false);
            };
            frecklesItem.OnListSelected += (uimenu, index) =>
            {
                HideMenu();
                User.Skin.GTAO_OVERLAY9 = index - 1;
                Sync.Data.Set(User.GetServerId(), "GTAO_OVERLAY9", User.Skin.GTAO_OVERLAY9);
                User.RemoveMoney(250);
                Business.Business.AddMoney(shopId, 250);
                Notification.SendSubtitle("~g~Потрачено $250");
                Characher.UpdateFace();
            };
            
            list = new List<dynamic>();
            for (var i = 0; i < 5; i++)
                list.Add(i);
            var frecklesColorItem = menu.AddMenuItemList(UiMenu, "Цвет веснушек", list, "Цена: ~g~$120");
            frecklesColorItem.OnListChanged+= (uimenu, index) =>
            {
                User.Skin.GTAO_OVERLAY9_COLOR = index;
                Characher.UpdateFace(false);
            };
            frecklesColorItem.OnListSelected += (uimenu, index) =>
            {
                HideMenu();
                User.Skin.GTAO_OVERLAY9_COLOR = index;
                Sync.Data.Set(User.GetServerId(), "GTAO_OVERLAY9_COLOR", User.Skin.GTAO_OVERLAY9_COLOR);
                User.RemoveMoney(120);
                Business.Business.AddMoney(shopId, 120);
                Notification.SendSubtitle("~g~Потрачено $120");
                Characher.UpdateFace();
            };
            
            if (User.Skin.SEX == 0)
            {
                list = new List<dynamic> {"~r~Нет"};
                for (var i = 1; i < 30; i++)
                    list.Add(i);

                var beardItem = menu.AddMenuItemList(UiMenu, "Борода", list, "Цена: ~g~$250");
                beardItem.OnListChanged += (uimenu, index) =>
                {
                    User.Skin.GTAO_OVERLAY = index - 1;
                    Characher.UpdateFace(false);
                };
                beardItem.OnListSelected += (uimenu, index) =>
                {
                    HideMenu();
                    User.Skin.GTAO_OVERLAY = index - 1;
                    Sync.Data.Set(User.GetServerId(), "GTAO_OVERLAY", User.Skin.GTAO_OVERLAY);
                    User.RemoveMoney(250);
                    Business.Business.AddMoney(shopId, 250);
                    Notification.SendSubtitle("~g~Потрачено $250");
                    Characher.UpdateFace();
                };


                list = new List<dynamic>();
                for (var i = 0; i < 64; i++)
                    list.Add(i);

                var beardColorItem = menu.AddMenuItemList(UiMenu, "Цвет бороды", list, "Цена: ~g~$120");
                beardColorItem.OnListChanged += (uimenu, index) =>
                {
                    User.Skin.GTAO_OVERLAY_COLOR = index;
                    Characher.UpdateFace(false);
                };
                beardColorItem.OnListSelected += (uimenu, index) =>
                {
                    HideMenu();
                    User.Skin.GTAO_OVERLAY_COLOR = index;
                    Sync.Data.Set(User.GetServerId(), "GTAO_OVERLAY_COLOR", User.Skin.GTAO_OVERLAY_COLOR);
                    User.RemoveMoney(120);
                    Business.Business.AddMoney(shopId, 120);
                    Notification.SendSubtitle("~g~Потрачено $120");
                    Characher.UpdateFace();
                };
                
                list = new List<dynamic>{"~r~Нет"};
                for (var i = 0; i < GetNumHeadOverlayValues(10)+1; i++)
                    list.Add(i);
                var blemishesItem = menu.AddMenuItemList(UiMenu, "Волосы на груди", list, "Цена: ~g~$250");
                blemishesItem.OnListChanged+= (uimenu, index) =>
                {
                    User.Skin.GTAO_OVERLAY10 = index - 1;
                    Characher.UpdateFace(false);
                };
                blemishesItem.OnListSelected += (uimenu, index) =>
                {
                    HideMenu();
                    User.Skin.GTAO_OVERLAY10 = index - 1;
                    Sync.Data.Set(User.GetServerId(), "GTAO_OVERLAY10", User.Skin.GTAO_OVERLAY10);
                    User.RemoveMoney(250);
                    Business.Business.AddMoney(shopId, 250);
                    Notification.SendSubtitle("~g~Потрачено $250");
                    Characher.UpdateFace();
                };
            
                list = new List<dynamic>();
                for (var i = 0; i < 60; i++)
                    list.Add(i);
                var blemishesColorItem = menu.AddMenuItemList(UiMenu, "Цвет волос на груди", list, "Цена: ~g~$120");
                blemishesColorItem.OnListChanged+= (uimenu, index) =>
                {
                    User.Skin.GTAO_OVERLAY10_COLOR = index;
                    Characher.UpdateFace(false);
                };
                blemishesColorItem.OnListSelected += (uimenu, index) =>
                {
                    HideMenu();
                    User.Skin.GTAO_OVERLAY10_COLOR = index;
                    Sync.Data.Set(User.GetServerId(), "GTAO_OVERLAY10_COLOR", User.Skin.GTAO_OVERLAY10_COLOR);
                    User.RemoveMoney(120);
                    Business.Business.AddMoney(shopId, 120);
                    Notification.SendSubtitle("~g~Потрачено $120");
                    Characher.UpdateFace();
                };
            }
            else
            {
                list = new List<dynamic>{"~r~Нет"};
                for (var i = 1; i < GetNumHeadOverlayValues(8)+1; i++)
                    list.Add(i);
                var lipstickItem = menu.AddMenuItemList(UiMenu, "Помада", list, "Цена: ~g~$250");
                lipstickItem.OnListChanged+= (uimenu, index) =>
                {
                    User.Skin.GTAO_OVERLAY8 = index - 1;
                    Characher.UpdateFace(false);
                };
                lipstickItem.OnListSelected += (uimenu, index) =>
                {
                    HideMenu();
                    User.Skin.GTAO_OVERLAY8 = index - 1;
                    Sync.Data.Set(User.GetServerId(), "GTAO_OVERLAY8", User.Skin.GTAO_OVERLAY8);
                    User.RemoveMoney(250);
                    Business.Business.AddMoney(shopId, 250);
                    Notification.SendSubtitle("~g~Потрачено $250");
                    Characher.UpdateFace();
                };
                
                list = new List<dynamic>();
                for (var i = 0; i < 60; i++)
                    list.Add(i);
                var lipstickColorItem = menu.AddMenuItemList(UiMenu, "Цвет помады", list, "Цена: ~g~$120");
                lipstickColorItem.OnListChanged+= (uimenu, index) =>
                {
                    User.Skin.GTAO_OVERLAY8_COLOR = index;
                    Characher.UpdateFace(false);
                };
                lipstickColorItem.OnListSelected += (uimenu, index) =>
                {
                    HideMenu();
                    User.Skin.GTAO_OVERLAY8_COLOR = index;
                    Sync.Data.Set(User.GetServerId(), "GTAO_OVERLAY8_COLOR", User.Skin.GTAO_OVERLAY8_COLOR);
                    User.RemoveMoney(120);
                    Business.Business.AddMoney(shopId, 120);
                    Notification.SendSubtitle("~g~Потрачено $120");
                    Characher.UpdateFace();
                };
                
                list = new List<dynamic>{"~r~Нет"};
                for (var i = 1; i < 6+1; i++)
                    list.Add(i);
                var brushItem = menu.AddMenuItemList(UiMenu, "Румянец", list, "Цена: ~g~$250");
                brushItem.OnListChanged+= (uimenu, index) =>
                {
                    User.Skin.GTAO_OVERLAY5 = index - 1;
                    Characher.UpdateFace(false);
                };
                brushItem.OnListSelected += (uimenu, index) =>
                {
                    HideMenu();
                    User.Skin.GTAO_OVERLAY5 = index - 1;
                    Sync.Data.Set(User.GetServerId(), "GTAO_OVERLAY5", User.Skin.GTAO_OVERLAY5);
                    User.RemoveMoney(250);
                    Business.Business.AddMoney(shopId, 250);
                    Notification.SendSubtitle("~g~Потрачено $250");
                    Characher.UpdateFace();
                };
                
                list = new List<dynamic>();
                for (var i = 0; i < 10; i++)
                    list.Add(i);
                var brushColorItem = menu.AddMenuItemList(UiMenu, "Цвет румянца", list, "Цена: ~g~$120");
                brushColorItem.OnListChanged+= (uimenu, index) =>
                {
                    User.Skin.GTAO_OVERLAY5_COLOR = index;
                    Characher.UpdateFace(false);
                };
                brushColorItem.OnListSelected += (uimenu, index) =>
                {
                    HideMenu();
                    User.Skin.GTAO_OVERLAY5_COLOR = index;
                    Sync.Data.Set(User.GetServerId(), "GTAO_OVERLAY5_COLOR", User.Skin.GTAO_OVERLAY5_COLOR);
                    User.RemoveMoney(120);
                    Business.Business.AddMoney(shopId, 120);
                    Notification.SendSubtitle("~g~Потрачено $120");
                    Characher.UpdateFace();
                };
                
                list = new List<dynamic>{"~r~Нет"};
                for (var i = 1; i < GetNumHeadOverlayValues(4)+1; i++)
                    list.Add(i);
                var makeupItem = menu.AddMenuItemList(UiMenu, "Макияж", list, "Цена: ~g~$250");
                makeupItem.OnListChanged+= (uimenu, index) =>
                {
                    User.Skin.GTAO_OVERLAY4 = index - 1;
                    Characher.UpdateFace(false);
                };
                makeupItem.OnListSelected += (uimenu, index) =>
                {
                    HideMenu();
                    User.Skin.GTAO_OVERLAY4 = index - 1;
                    Sync.Data.Set(User.GetServerId(), "GTAO_OVERLAY4", User.Skin.GTAO_OVERLAY4);
                    User.RemoveMoney(250);
                    Business.Business.AddMoney(shopId, 250);
                    Notification.SendSubtitle("~g~Потрачено $250");
                    Characher.UpdateFace();
                };
                
                list = new List<dynamic>();
                for (var i = 0; i < 10; i++)
                    list.Add(i);
                var makeupColorItem = menu.AddMenuItemList(UiMenu, "Цвет макияжа", list, "Цена: ~g~$120");
                makeupColorItem.OnListChanged+= (uimenu, index) =>
                {
                    User.Skin.GTAO_OVERLAY4_COLOR = index;
                    Characher.UpdateFace(false);
                };
                makeupColorItem.OnListSelected += (uimenu, index) =>
                {
                    HideMenu();
                    User.Skin.GTAO_OVERLAY4_COLOR = index;
                    Sync.Data.Set(User.GetServerId(), "GTAO_OVERLAY4_COLOR", User.Skin.GTAO_OVERLAY4_COLOR);
                    User.RemoveMoney(120);
                    Business.Business.AddMoney(shopId, 120);
                    Notification.SendSubtitle("~g~Потрачено $120");
                    Characher.UpdateFace();
                };
                
                /*list = new List<dynamic>{"~r~Нет"};
                for (var i = 1; i < GetNumHeadOverlayValues(6)+1; i++)
                    list.Add(i);
                var faceItem = menu.AddMenuItemList(UiMenu, "Макияж-2", list, "Цена: ~g~$250");
                faceItem.OnListChanged+= (uimenu, index) =>
                {
                    User.Skin.GTAO_OVERLAY6 = index - 1;
                    Characher.UpdateFace(false);
                };
                faceItem.OnListSelected += (uimenu, index) =>
                {
                    HideMenu();
                    User.Skin.GTAO_OVERLAY6 = index - 1;
                    Sync.Data.Set(User.GetServerId(), "GTAO_OVERLAY6", User.Skin.GTAO_OVERLAY6);
                    User.RemoveMoney(250);
                    Business.Business.AddMoney(shopId, 250);
                    Notification.SendSubtitle("~g~Потрачено $250");
                    Characher.UpdateFace();
                };
                
                list = new List<dynamic>();
                for (var i = 0; i < 10; i++)
                    list.Add(i);
                var faceColorItem = menu.AddMenuItemList(UiMenu, "Цвет макияжа-2", list, "Цена: ~g~$120");
                faceColorItem.OnListChanged+= (uimenu, index) =>
                {
                    User.Skin.GTAO_OVERLAY6_COLOR = index;
                    Characher.UpdateFace(false);
                };
                faceColorItem.OnListSelected += (uimenu, index) =>
                {
                    HideMenu();
                    User.Skin.GTAO_OVERLAY6_COLOR = index;
                    Sync.Data.Set(User.GetServerId(), "GTAO_OVERLAY6_COLOR", User.Skin.GTAO_OVERLAY6_COLOR);
                    User.RemoveMoney(120);
                    Business.Business.AddMoney(shopId, 120);
                    Notification.SendSubtitle("~g~Потрачено $120");
                    Characher.UpdateFace();
                };*/
            }
            
            if (User.Data.fraction_id2 > 0)
            {
                menu.AddMenuItem(UiMenu, "~o~Ограбить").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Grab.GrabShop(shopId);
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");

            UiMenu.OnMenuClose += sender =>
            {
                HideMenu();
                Characher.UpdateFace();
            };
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                {
                    HideMenu();
                    Characher.UpdateFace();
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowShopMaskMenu(int shopId = 0)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Магазин", "~b~Магазин масок", true, true);
            
            var list = new List<dynamic>();
            for (var i = 0; i < 96; i++)
                list.Add(i);

            var list2 = new List<dynamic> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 
                                           22, 23, 24, 25, 26, 29, 30, 31, 32, 33, 34, 36, 37, 38, 39, 40, 41, 41, 43,
                                           44, 45, 47, 48, 49, 50, 51, 54, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67,
                                           68, 69, 72, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86,
                                           87, 88, 90, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 107, 108, 111,
                                           116, 124, 127, 133, 143};

            var listItem = menu.AddMenuItemList(UiMenu, "Маски", list, "Цена: ~g~$200");
            
            listItem.OnListChanged += (uimenu, idx) =>
            {
                Sync.Data.SetLocally(User.GetServerId(), "hasBuyMask", true);
                SetPedComponentVariation(GetPlayerPed(-1), 1, (int) list2[idx], 0, 2);
            };
            listItem.OnListSelected += (uimenu, idx) =>
            {
                HideMenu();

                if (User.Data.money < 200)
                {
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                    return;
                }
                
                Sync.Data.ResetLocally(User.GetServerId(), "hasBuyMask");
                SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                User.Data.mask = (int) list2[idx];
                Sync.Data.Set(User.GetServerId(), "mask", (int) list2[idx]);
                Notification.SendWithTime("~g~Вы купили маску, использовать ее можно через инвентарь");
                
                User.RemoveCashMoney(200);
                Business.Business.AddMoney(74, 200);
            };
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");

            UiMenu.OnMenuClose += sender =>
            {
                HideMenu();
                Sync.Data.ResetLocally(User.GetServerId(), "hasBuyMask");
                Characher.UpdateCloth();
            };
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                {
                    HideMenu();
                    Sync.Data.ResetLocally(User.GetServerId(), "hasBuyMask");
                    Characher.UpdateCloth();
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowShopClothMenu(int shopId, int type, int menuType)
        {
            HideMenu();

            string title1 = "";
            string title2 = "";

            switch (type)
            {
                case 0:
                    title1 = "shopui_title_lowendfashion";
                    title2 = "shopui_title_lowendfashion";
                    break;
                case 1:
                    title1 = "shopui_title_midfashion";
                    title2 = "shopui_title_midfashion";
                    break;
                case 2:
                    title1 = "shopui_title_highendfashion";
                    title2 = "shopui_title_highendfashion";
                    break;
                case 3:
                    title1 = "shopui_title_gunclub";
                    title2 = "shopui_title_gunclub";
                    break;
                case 5:
                    title1 = "shopui_title_lowendfashion2";
                    title2 = "shopui_title_lowendfashion2";
                    break;
            }
            
            var menu = new Menu();
            UiMenu = menu.Create(title1 != "" ? " " : "Vangelico", "~b~Магазин", true, true);

            if (title1 != "")
                menu.SetMenuBannerSprite(UiMenu, title1, title2);

            if (type == 5)
            {
                menu.AddMenuItem(UiMenu, "Бейсбольная бита", $"Цена: ~g~$100").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Business.Gun.Buy(55, 100, 1, shopId);
                };
                
                menu.AddMenuItem(UiMenu, "Бейсбольный мяч", $"Цена: ~g~$10").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    Business.Gun.Buy(127, 10, 1, shopId);
                };
            }

            if (menuType == 0)
            {
                menu.AddMenuItem(UiMenu, "Головные уборы").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowShopPropMenu(shopId, type, 0);
                };
            
                menu.AddMenuItem(UiMenu, "Очки").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowShopPropMenu(shopId, type, 1);
                };
            
                menu.AddMenuItem(UiMenu, "Серьги").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowShopPropMenu(shopId, type, 2);
                };
            
                menu.AddMenuItem(UiMenu, "Левая рука").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowShopPropMenu(shopId, type, 6);
                };
            
                menu.AddMenuItem(UiMenu, "Правая рука").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowShopPropMenu(shopId, type, 7);
                };
                
                if (User.Data.fraction_id2 > 0)
                {
                    menu.AddMenuItem(UiMenu, "~o~Ограбить").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Grab.GrabShop(shopId);
                    };
                }
            }
            else if (menuType == 1)
            {
                menu.AddMenuItem(UiMenu, "Головные уборы").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowShopPropMenu(shopId, type, 0);
                };
            
                menu.AddMenuItem(UiMenu, "Очки").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowShopPropMenu(shopId, type, 1);
                };
                
                menu.AddMenuItem(UiMenu, "Торс").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowShopClothMenu(shopId, 3, 11);
                };

                menu.AddMenuItem(UiMenu, "Ноги").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowShopClothMenu(shopId, 3, 4);
                };

                menu.AddMenuItem(UiMenu, "Обувь").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowShopClothMenu(shopId, 3, 6);
                };
            }
            else
            {
                if (menuType == 7)
                {
                    menu.AddMenuItem(UiMenu, "~y~Снять").Activated += (uimenu, item) =>
                    {
                        HideMenu();
                        Business.Cloth.Buy(0, menuType, 0, 0, -1, -1, -1, -1, shopId, true);
                    };
                }
                
                dynamic[,] cloth = User.Skin.SEX == 1 ? Cloth.ClothF : Cloth.ClothM;
                for (int i = 0; i < cloth.Length / 12; i++)
                {
                    var id = i;
                
                    if ((int) cloth[id, 1] != menuType) continue;
                    if ((int) cloth[id, 0] != type) continue;
                
                    var list = new List<dynamic>();
                    for (var j = 0; j <= (int) cloth[i, 3] + 1; j++) {
                        list.Add(j);
                    }

                    var menuItem = menu.AddMenuItemList(UiMenu, (string) cloth[i, 9].ToString(), list, $"Цена: ~g~${((int) cloth[i, 8]):#,#}{((int) cloth[i, 10] > -99 ? $"\n~s~Термостойкость до ~g~{cloth[i, 10]}°" : "")}");
                
                    menuItem.OnListSelected += (uimenu, index) =>
                    {
                        HideMenu();
                        Business.Cloth.Buy((int) cloth[id, 8], (int) cloth[id, 1], (int) cloth[id, 2], index, (int) cloth[id, 4], (int) cloth[id, 5], (int) cloth[id, 6], (int) cloth[id, 7], shopId);
                    };

                    menuItem.OnListChanged += (uimenu, index) =>
                    {
                        Business.Cloth.Change((int) cloth[id, 1], (int) cloth[id, 2], index, (int) cloth[id, 4], (int) cloth[id, 5], (int) cloth[id, 6], (int) cloth[id, 7]);
                    };
                }
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");

            UiMenu.OnMenuClose += sender =>
            {
                HideMenu();
                Characher.UpdateCloth();
            };
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                {
                    HideMenu();
                    Characher.UpdateCloth();
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowShopPropMenu(int shopId, int type, int menuType)
        {
            HideMenu();

            string title1 = "";
            string title2 = "";

            switch (type)
            {
                case 0:
                    title1 = "shopui_title_lowendfashion";
                    title2 = "shopui_title_lowendfashion";
                    break;
                case 1:
                    title1 = "shopui_title_midfashion";
                    title2 = "shopui_title_midfashion";
                    break;
                case 2:
                    title1 = "shopui_title_highendfashion";
                    title2 = "shopui_title_highendfashion";
                    break;
                case 3:
                    title1 = "shopui_title_gunclub";
                    title2 = "shopui_title_gunclub";
                    break;
                case 5:
                    title1 = "shopui_title_lowendfashion2";
                    title2 = "shopui_title_lowendfashion2";
                    break;
            }
            
            var menu = new Menu();
        UiMenu = menu.Create(title1 != "" ? " " : "Vangelico", "~b~Магазин", true, true);

            if (title1 != "")
                menu.SetMenuBannerSprite(UiMenu, title1, title2);

            menu.AddMenuItem(UiMenu, "~y~Снять").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Cloth.BuyProp(0, menuType, -1, -1, shopId, true);
            };

            dynamic[,] cloth = User.Skin.SEX == 1 ? Cloth.PropF : Cloth.PropM;
            
            for (int i = 0; i < cloth.Length / 6; i++)
            {
                var id = i;
                
                if ((int) cloth[id, 1] != menuType) continue;
                if ((int) cloth[id, 0] != type) continue;
                
                var list = new List<dynamic>();
                for (var j = 0; j <= (int) cloth[i, 3] + 1; j++) {
                    list.Add(j);
                }

                var menuItem = menu.AddMenuItemList(UiMenu, (string) cloth[i, 5].ToString(), list, $"Цена: ~g~${((int) cloth[i, 4]):#,#}");
                
                menuItem.OnListSelected += (uimenu, index) =>
                {
                    HideMenu();
                    Business.Cloth.BuyProp((int) cloth[id, 4], (int) cloth[id, 1], (int) cloth[id, 2], index, shopId);
                };

                menuItem.OnListChanged += (uimenu, index) =>
                {
                    Business.Cloth.ChangeProp((int) cloth[id, 1], (int) cloth[id, 2], index);
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");

            UiMenu.OnMenuClose += sender =>
            {
                HideMenu();
                Characher.UpdateCloth();
            };
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                {
                    HideMenu();
                    Characher.UpdateCloth();
                }
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowSellCarListMenu(List<CitizenFX.Core.Vehicle> vehList)
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create("Сбыт", "~b~Сбыт транспорта");

            foreach (var vehItem in vehList)
            {
                menu.AddMenuItem(UiMenu, "~b~Номер авто:~s~ " + Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).Activated += (uimenu, index) =>
                {
                    HideMenu();
                    Grab.SellVehicle(vehItem);
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowFuelMenu(int fuelId, int isShop, List<string> vehList, int price = 1)
        {
            HideMenu();

            price = await Business.Business.GetPrice(fuelId);

            var menu = new Menu();
            UiMenu = menu.Create("Заправка", "~b~1 литр = ~g~$" + price);
            
            menu.AddMenuItem(UiMenu, "Купить канистру", $"Цена: ~g~${(10 * price):#,#}").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Fuel.Buy("fuel_item", 10 * price, fuelId);
            };

            if (isShop == 1)
            {
                menu.AddMenuItem(UiMenu, "Магазин").Activated += (uimenu, item) =>
                {
                    HideMenu();
                    ShowShopMenu(fuelId, price);
                };
            }
           
            var list = new List<dynamic> {"1л", "5л", "10л", "Полный бак"};

            foreach (var vehItem in vehList)
            {
                menu.AddMenuItemList(UiMenu, "~b~Номер авто:~s~ " + vehItem, list).OnListSelected += async (uimenu, index) =>
                {
                    HideMenu();
                    
                    if (await Ctos.IsBlackout())
                    {
                        Notification.SendWithTime("~r~Заправка во время блекаута не работает");
                        return;
                    }
                    
                    Business.Fuel.FuelVeh(vehItem, 1 * price, fuelId, index);
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowInvVehBagMenu(List<string> vehList)
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create("Багажник", $"~b~Список транспорта");
            
            foreach (var vehItem in vehList)
            {
                menu.AddMenuItem(UiMenu, "~b~Номер:~s~ " + vehItem).Activated += (uimenu, index) =>
                {
                    HideMenu();
                    var v = Managers.Vehicle.GetVehicleByNumber(vehItem);
                    if (v == null)
                    {
                        Notification.SendWithTime("~r~Поблизости ТС не найден");
                        return;
                    }
                    if (v.IsDead)
                    {
                        Notification.SendWithTime("~r~Транспорт уничтожен");
                        return;
                    }
                    if (v.LockStatus != VehicleLockStatus.Unlocked)
                    {
                        Notification.SendWithTime("~r~Транспорт закрыт");
                        return;
                    }
                    if (IsPedInAnyVehicle(PlayerPedId(), true))
                    {
                        Notification.SendWithTime("~r~Вы должны находиться около багажника");
                        return;
                    }
                    if (VehInfo.Get(v.Model.Hash).Stock == 0)
                    {
                        Notification.SendWithTime("~r~Багажник отсутсвует у этого ТС");
                        return;
                    }
                    v.Doors[VehicleDoorIndex.Trunk].Open();
                    Managers.Inventory.GetItemList(Managers.Inventory.ConvertNumberToHash(Managers.Vehicle.GetVehicleNumber(v.Handle)), InventoryTypes.Vehicle);
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static async void ShowInvVehDropMenu(List<string> vehList, int id, int itemId, int prefix, int number, int keyId, int countItems)
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create("Положить", $"~b~{Inventory.GetItemNameById(itemId)}");
            
            foreach (var vehItem in vehList)
            {
                menu.AddMenuItem(UiMenu, "~b~Номер:~s~ " + vehItem).Activated += (uimenu, index) =>
                {
                    HideMenu();
                    Managers.Inventory.DropItemToVehicle(id, itemId, vehItem);
                };
            }
            
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
            
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowSetCarNumberMenu(int lscId)
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create(" ", "~b~Автомастерская", true, true);

            menu.SetMenuBannerSprite(UiMenu, "shopui_title_ie_modgarage", "shopui_title_ie_modgarage");

			switch (lscId) {
				case 14:
				case 54:
				case 55:
				case 57:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_carmod", "shopui_title_carmod");
					break;
				case 71:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_carmod2", "shopui_title_carmod2");
					break;
				case 56:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_supermod", "shopui_title_supermod");
					break;
			}
            
            menu.AddMenuItem(UiMenu, "Полный ремонт", "Цена: ~g~$200").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.CarNumber.ShowVehicleRepairList(lscId);
            };
            
            menu.AddMenuItem(UiMenu, "Сменить номер", "Цена: ~g~$40000").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.CarNumber.ShowVehicleNumberList(lscId);
            };
            
            menu.AddMenuItem(UiMenu, "~r~Закрыть").Activated += (uimenu, item) => { HideMenu(); Business.Custom.CloseMenu(); };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowSetPhoneNumberMenu(int bId)
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create("Invader", "~b~Магазин");
            
            menu.AddMenuItem(UiMenu, "Телефон", "Цена: ~g~$500").Activated += (uimenu, item) =>
            {
                HideMenu();
                if (User.Data.money < 500)
                {
                    Notification.SendWithTime("~r~Нужно иметь 500$");
                    return;
                }
                if (User.Data.phone_code > 0 || User.Data.phone > 0)
                {
                    Notification.SendWithTime("~r~У Вас уже есть телефон");
                    return;
                }

                Random rand = new Random();
                    
                User.Data.phone_code = 777;
                User.Data.phone = rand.Next(10000, 999999);

                Sync.Data.Set(User.GetServerId(), "phone_code", User.Data.phone_code);
                Sync.Data.Set(User.GetServerId(), "phone", User.Data.phone);
                Notification.SendWithTime($"~g~Вы купили телефон\nВаш номер: ~s~{User.Data.phone_code}-{User.Data.phone}");

                User.RemoveMoney(500);
                Business.Business.AddMoney(bId, 500);
            };
            
            menu.AddMenuItem(UiMenu, "Сменить номер телефона", "Цена: ~g~$100,000").Activated += async (uimenu, item) =>
            {
                HideMenu();
                if (User.Data.phone_code != 777)
                {
                    Notification.SendWithTime("~r~Вы должны иметь телефон с префиксом ~b~777");
                    return;
                }
                
                if (User.Data.money < 500)
                {
                    Notification.SendWithTime("~r~Нужно иметь 100,000$");
                    return;
                }
                
                int number = Convert.ToInt32(await Menu.GetUserInput("Номер телефона", null, 8));
                if (number < 10000)
                {
                    Notification.SendWithTime("~r~Должно быть больше 5 цифр");
                    return;
                }
                    
                TriggerServerEvent("ARP:ChangeNumberPhone", User.Data.phone_code, number);
            };
            
            menu.AddMenuItem(UiMenu, "~r~Закрыть").Activated += (uimenu, item) => { HideMenu(); };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowHackerPhoneBuyMenu()
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create("Kali", "~b~Магазин");
            
            menu.AddMenuItem(UiMenu, "Телефон", "Цена: ~g~$1,200").Activated += (uimenu, item) =>
            {
                HideMenu();
                if (User.Data.money < 1200)
                {
                    Notification.SendWithTime("~r~Нужно иметь $1,200");
                    return;
                }
                if (User.Data.phone_code > 0 || User.Data.phone > 0)
                {
                    Notification.SendWithTime("~r~У Вас уже есть телефон");
                    return;
                }

                Random rand = new Random();
                    
                User.Data.phone_code = 404;
                User.Data.phone = rand.Next(10000, 999999);

                Sync.Data.Set(User.GetServerId(), "phone_code", User.Data.phone_code);
                Sync.Data.Set(User.GetServerId(), "phone", User.Data.phone);
                Notification.SendWithTime($"~g~Вы купили телефон\nВаш номер: ~s~{User.Data.phone_code}-{User.Data.phone}");
                Notification.SendWithTime("~g~На данном телефоне установлен ~b~Kali Linux");

                User.RemoveMoney(1200);
                //Business.Business.AddMoney(bId, 500);
            };
            
            menu.AddMenuItem(UiMenu, "Информация о Exploit DB", "Цена: ~g~$100").Activated += async (uimenu, item) =>
            {
                HideMenu();
                if (User.Data.money < 100)
                {
                    Notification.SendWithTime("~r~Нужно иметь $100");
                    return;
                }
                User.RemoveMoney(100);
                
                UI.ShowToolTip("Подключиться туда невозможно, но этот сервис предоставляет различные эксплойты. Скачать их можно по команде ~b~wget~s~.\n~b~IP Сервиса:~s~ 54.37.128.202");
                await Delay(20000);
                UI.ShowToolTip("~b~Список файлов:~s~\nsearch.sh\nphone555.sh\nphone404.sh\nphone777.sh\nelcar.sh\nsportcar.sh\ngetuserinfo.sh\natmbackdoor.sh\n\nУ каждого файла есть параметр ~b~help");
                //Business.Business.AddMoney(bId, 500);
            };
            
            menu.AddMenuItem(UiMenu, "~r~Закрыть").Activated += (uimenu, item) => { HideMenu(); };
            
            MenuPool.Add(UiMenu);
        }

        public static void BuyGasWelder(string title, string desc)
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create(title, "~b~" + desc);
            
            menu.AddMenuItem(UiMenu, "Газовоя горелка", "Цена: ~g~$6.000").Activated += async (uimenu, item) =>
            {
                HideMenu();
                if (Sync.Data.HasLocally(User.GetServerId(), "weldingtool"))
                {
                    Chat.SendChatMessage("Торговец", "Больше продать не могу, хватит с тебя. Не нужны мне конкуренты.");
                    return;
                }

                if (User.Data.money < 6000)
                {
                    Chat.SendChatMessage("Торговец", "Не, не, не, давай 6000");
                    Notification.SendWithTime("~r~Нужно иметь $6.000");
                    return;
                }
                
                User.RemoveMoney(6000);
                
                Managers.Inventory.AddItemServer(275, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);

                Sync.Data.SetLocally(User.GetServerId(), "weldingtool", true);
                
                Notification.SendWithTime("~g~Вы купили газовую горелку");
                Chat.SendChatMessage("Торговец", "Не знаю зачем она тебе, но вообще, ме пофиг, главное, у меня бабки. Бывай");
            };
            var closeButton = menu.AddMenuItem(UiMenu, "~r~Закрыть");
                        
            UiMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == closeButton)
                    HideMenu();
            };
                        
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowLscMenu(int lscId)
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create(" ", "~b~Автомастерская", true, true);

            menu.SetMenuBannerSprite(UiMenu, "shopui_title_ie_modgarage", "shopui_title_ie_modgarage");

			switch (lscId) {
				case 14:
				case 54:
				case 55:
				case 57:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_carmod", "shopui_title_carmod");
					break;
				case 71:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_carmod2", "shopui_title_carmod2");
					break;
				case 56:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_supermod", "shopui_title_supermod");
					break;
			}
            
            menu.AddMenuItem(UiMenu, "Полный ремонт", "Цена: ~g~$200").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Custom.ShowVehicleRepairList(lscId);
            };
            
            menu.AddMenuItem(UiMenu, "Тюнинг").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Custom.ShowVehicleTunningList(lscId);
            };
            
            menu.AddMenuItem(UiMenu, "Сменить номер", "Цена: ~g~$40000").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Custom.ShowVehicleNumberList(lscId);
            };
            
            menu.AddMenuItem(UiMenu, "Цвет").Activated += (uimenu, item) =>
            {
                HideMenu();
                Business.Custom.ShowVehicleColorList(lscId);
            };
            
            menu.AddMenuItem(UiMenu, "~r~Закрыть").Activated += (uimenu, item) => { HideMenu(); Business.Custom.CloseMenu(); };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowLscRepairList(int lscId, List<string> vehList)
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create(" ", "~b~Ремонт вашего транспорта", true, true);

            menu.SetMenuBannerSprite(UiMenu, "shopui_title_ie_modgarage", "shopui_title_ie_modgarage");

			switch (lscId) {
				case 14:
				case 54:
				case 55:
				case 57:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_carmod", "shopui_title_carmod");
					break;
				case 71:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_carmod2", "shopui_title_carmod2");
					break;
				case 56:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_supermod", "shopui_title_supermod");
					break;
			}
            
            foreach (var vehItem in vehList)
            {
                menu.AddMenuItem(UiMenu, "~b~Номер авто:~s~ " + vehItem).Activated += (uimenu, index) =>
                {
                    HideMenu();
                    Business.Custom.RepairVeh(vehItem, lscId);
                };
            }
            
            menu.AddMenuItem(UiMenu, "~r~Закрыть").Activated += (uimenu, item) => { HideMenu(); Business.Custom.CloseMenu(); };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowRepairList(int lscId, List<string> vehList)
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create("Починка", "~b~Ремонт вашего транспорта", true, true);
            
            foreach (var vehItem in vehList)
            {
                menu.AddMenuItem(UiMenu, "~b~Номер авто:~s~ " + vehItem).Activated += (uimenu, index) =>
                {
                    HideMenu();
                    Business.CarNumber.RepairVeh(vehItem, lscId);
                };
            }
            
            menu.AddMenuItem(UiMenu, "~r~Закрыть").Activated += (uimenu, item) => { HideMenu(); };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowVehicleNumberList(int lscId, List<string> vehList)
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create("Номер", "~b~Англ. буквы (A-Z) и цифры (0-9)", true, true);
            
            foreach (var vehItem in vehList)
            {
                menu.AddMenuItem(UiMenu, "~b~Номер авто:~s~ " + vehItem).Activated += async (uimenu, index) =>
                {
                    HideMenu();
                    Business.CarNumber.SetNumber(vehItem, lscId, await Menu.GetUserInput("Новый номер", "", 8));
                };
            }
            
            menu.AddMenuItem(UiMenu, "~r~Закрыть").Activated += (uimenu, item) => { HideMenu(); };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowLscTunList(int lscId, List<string> vehList)
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create(" ", "~b~Тюнинг вашего транспорта", true, true);

            menu.SetMenuBannerSprite(UiMenu, "shopui_title_ie_modgarage", "shopui_title_ie_modgarage");

			switch (lscId) {
				case 14:
				case 54:
				case 55:
				case 57:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_carmod", "shopui_title_carmod");
					break;
				case 71:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_carmod2", "shopui_title_carmod2");
					break;
				case 56:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_supermod", "shopui_title_supermod");
					break;
			}
            
            foreach (var vehItem in vehList)
            {
                menu.AddMenuItem(UiMenu, "~b~Номер авто:~s~ " + vehItem).Activated += (uimenu, index) =>
                {
                    HideMenu();
                    Business.Custom.TunningVeh(vehItem, lscId);
                };
            }
            
            menu.AddMenuItem(UiMenu, "~r~Закрыть").Activated += (uimenu, item) => { HideMenu(); Business.Custom.CloseMenu(); };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowLscClrList(int lscId, List<string> vehList)
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create(" ", "~b~Покраска вашего транспорта", true, true);

            menu.SetMenuBannerSprite(UiMenu, "shopui_title_ie_modgarage", "shopui_title_ie_modgarage");

			switch (lscId) {
				case 14:
				case 54:
				case 55:
				case 57:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_carmod", "shopui_title_carmod");
					break;
				case 71:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_carmod2", "shopui_title_carmod2");
					break;
				case 56:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_supermod", "shopui_title_supermod");
					break;
			}
            
            foreach (var vehItem in vehList)
            {
                menu.AddMenuItem(UiMenu, "~b~Номер авто:~s~ " + vehItem).Activated += (uimenu, index) =>
                {
                    HideMenu();
                    Business.Custom.ColorVeh(vehItem, lscId);
                };
            }
            
            menu.AddMenuItem(UiMenu, "~r~Закрыть").Activated += (uimenu, item) => { HideMenu(); Business.Custom.CloseMenu(); };
            
            MenuPool.Add(UiMenu);
        }

        public static void ShowLscColorMenu(int lscId, CitizenFX.Core.Vehicle veh)
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create(" ", "~b~Покраска вашего транспорта", true, true);

            menu.SetMenuBannerSprite(UiMenu, "shopui_title_ie_modgarage", "shopui_title_ie_modgarage");

			switch (lscId) {
				case 14:
				case 54:
				case 55:
				case 57:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_carmod", "shopui_title_carmod");
					break;
				case 71:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_carmod2", "shopui_title_carmod2");
					break;
				case 56:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_supermod", "shopui_title_supermod");
					break;
			}
            
            UiMenu.OnMenuClose += (sender) =>
            {
                ShowLscMenu(lscId);
            };
            
            var list = new List<dynamic>();
            for (int j = 0; j < 160; j++)
                list.Add(j);

            var menuItem1 = menu.AddMenuItemList(UiMenu, "Цвет-1", list, "Цена: ~g~$500");
            menuItem1.OnListChanged += (uimenu, index) =>
            {
                SetVehicleColours(veh.Handle, index, Convert.ToInt32(veh.Mods.SecondaryColor));
            };
            menuItem1.OnListSelected += (uimenu, index) =>
            {
                HideMenu();
                Business.Custom.Color1Veh(index, 500, lscId, veh);
            };

            var menuItem2 = menu.AddMenuItemList(UiMenu, "Цвет-2", list, "Цена: ~g~$300");
            menuItem2.OnListChanged += (uimenu, index) =>
            {
                SetVehicleColours(veh.Handle, Convert.ToInt32(veh.Mods.PrimaryColor), index);
            };
            menuItem2.OnListSelected += (uimenu, index) =>
            {
                HideMenu();
                Business.Custom.Color2Veh(index, 300, lscId, veh);
            };
            
            menu.AddMenuItem(UiMenu, "~b~Цвет неона", "Цена: ~g~$2000").Activated += (uimenu, item) =>
            {
                HideMenu(); 
                Business.Custom.ColorNeonVeh(2000, lscId, veh);
            };
            
            menu.AddMenuItem(UiMenu, "~r~Закрыть").Activated += (uimenu, item) => { HideMenu(); Business.Custom.CloseMenuWithRemoveMod(veh); };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowLscTunningMenu(int lscId, List<int> slots, List<int> count, CitizenFX.Core.Vehicle veh)
        {
            HideMenu();

            var menu = new Menu();
            UiMenu = menu.Create(" ", "~b~Тюнинг вашего транспорта", true, true);
            UiMenu.AddInstructionalButton(new InstructionalButton((Control) 22, "Переключить камеру"));
            menu.SetMenuBannerSprite(UiMenu, "shopui_title_ie_modgarage", "shopui_title_ie_modgarage");

            switch (lscId) {
				case 14:
				case 54:
				case 55:
				case 57:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_carmod", "shopui_title_carmod");
					break;
				case 71:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_carmod2", "shopui_title_carmod2");
					break;
				case 56:
					menu.SetMenuBannerSprite(UiMenu, "shopui_title_supermod", "shopui_title_supermod");
					break;
			}
            
            UiMenu.OnMenuClose += (sender) =>
            {
                ShowLscMenu(lscId);
            };
            
            for (int i = 0; i < slots.Count; i++)
            {
                var list = new List<dynamic> {"~r~Снять"};
                for (int j = 0; j < count[i]; j++)
                    list.Add(j);
                
                var i1 = i;
                switch (slots[i])
                {
                    case 0:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Спойлер", list, "Цена: ~g~$1500");
                        
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 1500, lscId, veh);
                        };
                        break;
                    }
                    case 1:
                    {
                        if (veh.Model.Hash == -410205223 ||
                            veh.Model.Hash == 903794909 ||
                            veh.Model.Hash == -391595372 ||
                            veh.Model.Hash == -1483171323 ||
                            veh.Model.Hash == -726768679 ||
                            veh.Model.Hash == -1763555241 ||
                            veh.Model.Hash == -1984275979 ||
                            veh.Model.Hash == 1561920505) 
                            break;
                        
                        var menuItem = menu.AddMenuItemList(UiMenu, "Передний бампер", list, "Цена: ~g~$2000");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 2000, lscId, veh);
                        };
                        break;
                    }
                    case 2:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Задний бампер", list, "Цена: ~g~$2000");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 2000, lscId, veh);
                        };
                        break;
                    }
                    case 3:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Юбки", list, "Цена: ~g~$1850");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 1850, lscId, veh);
                        };
                        break;
                    }
                    case 4:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Выхлоп", list, "Цена: ~g~$1620");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 1620, lscId, veh);
                        };
                        break;
                    }
                    case 5:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Рамка", list, "Цена: ~g~$1200");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 1200, lscId, veh);
                        };
                        break;
                    }
                    case 6:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Решетка", list, "Цена: ~g~$900");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 900, lscId, veh);
                        };
                        break;
                    }
                    case 7:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Капот", list, "Цена: ~g~$2200");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 2200, lscId, veh);
                        };
                        break;
                    }
                    case 43:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Капот-2", list, "Цена: ~g~$2200");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 2200, lscId, veh);
                        };
                        break;
                    }
                    case 8:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Крыло", list, "Цена: ~g~$1300");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 1300, lscId, veh);
                        };
                        break;
                    }
                    case 10:
                    {
                        if (veh.Model.Hash == -410205223 ||
                            veh.Model.Hash == 903794909 ||
                            veh.Model.Hash == -391595372 ||
                            veh.Model.Hash == -1483171323 ||
                            veh.Model.Hash == -726768679 ||
                            veh.Model.Hash == -1763555241 ||
                            veh.Model.Hash == -1984275979 ||
                            veh.Model.Hash == 1561920505) 
                            break;
                        
                        var menuItem = menu.AddMenuItemList(UiMenu, "Крыша", list, "Цена: ~g~$1450");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 1450, lscId, veh);
                        };
                        break;
                    }
                    case 44:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Крыша-2", list, "Цена: ~g~$1450");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 1450, lscId, veh);
                        };
                        break;
                    }
                    case 14:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Гудок", list, "Цена: ~g~$123000");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 123000, lscId, veh);
                        };
                        break;
                    }
                    case 15:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Подвеска", list, "Цена: ~g~$3200");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 3200, lscId, veh);
                        };
                        break;
                    }
                    /*case 18:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Положение номера", list, "Цена: ~g~$200");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 200, lscId);
                        };
                        break;
                    }*/
                    case 19:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Решетка радиатора", list, "Цена: ~g~$3200");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 3200, lscId, veh);
                        };
                        break;
                    }
                        /*case 78:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Тип колес", list, "Выберите тип колес");
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            HideMenu();
                            SetVehicleWheelType(veh.NetworkId, i);
                            Notification.Send("~g~Тип колес был обновлен");
                            ShowLscTunningMenu(lscId, slots, count, veh);
                        };
                        break;
                    }*/
                    case 23:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Колёса", list, "Цена: ~g~$5500");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 5500, lscId, veh);
                        };
                        break;
                    }
                    case 24:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Задние колёса", list, "Цена: ~g~$2500");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 2500, lscId, veh);
                        };
                        break;
                    }
                    case 28:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Украшения", list, "Цена: ~g~$2500");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 2500, lscId, veh);
                        };
                        break;
                    }
                    case 30:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Дизайн циферблата", list, "Цена: ~g~$500");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 500, lscId, veh);
                        };
                        break;
                    }
                    case 32:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Сиденья", list, "Цена: ~g~$1500");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 1500, lscId, veh);
                        };
                        break;
                    }
                    case 38:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Гидравлика", list, "Цена: ~g~$15000");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 15000, lscId, veh);
                        };
                        break;
                    }
                    case 46:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Стёкла", list, "Цена: ~g~$500");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 500, lscId, veh);
                        };
                        break;
                    }
                    case 48:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Винил", list, "Цена: ~g~$20000");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 20000, lscId, veh);
                        };
                        break;
                    }
                    case 74:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Цвет приборной панели", list, "Цена: ~g~$2000");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 2000, lscId, veh);
                        };
                        break;
                    }
                    case 75:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Цвет отделки", list, "Цена: ~g~$5000");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 5000, lscId, veh);
                        };
                        break;
                    }
                    case 11:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Двигатель", list, "Цена за 1 уровень: ~g~$5000");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 5000, lscId, veh);
                        };
                        break;
                    }
                    case 12:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Тормоза", list, "Цена за 1 уровень: ~g~$2000");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 2000, lscId, veh);
                        };
                        break;
                    }
                    case 13:
                    {
                        var menuItem = menu.AddMenuItemList(UiMenu, "Трансмиссия", list, "Цена за 1 уровень: ~g~$3000");
                        menuItem.OnListChanged += (uimenu, index) =>
                        {
                            if (index == 0)
                            {
                                RemoveVehicleMod(veh.Handle, slots[i1]);
                                return;
                            }
                            --index;
                            SetVehicleMod(veh.Handle, slots[i1], index, false);
                        };
                        menuItem.OnListSelected += (uimenu, index) =>
                        {
                            Business.Custom.UpgradeVeh(slots[i1], index, 3000, lscId, veh);
                        };
                        break;
                    }
                }
            }


            int livCount = GetVehicleLiveryCount(veh.Handle);
            if (livCount >= 0)
            {
                var listLiv = new List<dynamic>();
                for (int j = 0; j < livCount; j++)
                    listLiv.Add(j);
                
                var menuItemLiv = menu.AddMenuItemList(UiMenu, "Уникальная раскраска", listLiv, "Цена: ~g~$15000");
                menuItemLiv.OnListChanged += (uimenu, index) =>
                {
                    SetVehicleLivery(veh.Handle, index);
                };
                menuItemLiv.OnListSelected += (uimenu, index) =>
                {
                    Business.Custom.UpgradeVeh(99, index, 15000, lscId, veh);
                };
            }
            
            var list2 = new List<dynamic> {"~r~Снять", "~g~Купить"};
            var menuItem2 = menu.AddMenuItemList(UiMenu, "Турбо", list2, "Цена: ~g~$25000");
            menuItem2.OnListChanged += (uimenu, index) =>
            {
                if (index == 0)
                {
                    RemoveVehicleMod(veh.Handle, 18);
                    return;
                }
                --index;
                SetVehicleMod(veh.Handle, 18, index, false);
            };
            menuItem2.OnListSelected += (uimenu, index) =>
            {
                Business.Custom.UpgradeVeh(18, index, 25000, lscId, veh);
            };
            
            /*var list5 = new List<dynamic> {"~r~Снять", "~g~Купить"};
            var menuItem5 = menu.AddMenuItemList(UiMenu, "Ксенон", list5, "Цена: ~g~$5000");
            menuItem5.OnListChanged += (uimenu, index) =>
            {
                if (index == 0)
                {
                    RemoveVehicleMod(veh.Handle, 22);
                    return;
                }
                --index;
                SetVehicleMod(veh.Handle, 22, index, false);
            };
            menuItem5.OnListSelected += (uimenu, index) =>
            {
                Business.Custom.UpgradeVeh(22, index, 5000, lscId, veh);
            };*/

            var list1 = new List<dynamic> {"~r~Снять"};
            for (int j = 0; j < 7; j++)
                list1.Add(j);
            
            var menuItem1 = menu.AddMenuItemList(UiMenu, "Тонировка", list1, "Цена: ~g~$2000");
            menuItem1.OnListChanged += (uimenu, index) =>
            {
                if (index == 0)
                {
                    RemoveVehicleMod(veh.Handle, 69);
                    return;
                }
                --index;
                SetVehicleMod(veh.Handle, 69, index, false);
                SetVehicleWindowTint(veh.Handle, index);
            };
            menuItem1.OnListSelected += (uimenu, index) =>
            {
                Business.Custom.UpgradeVeh(69, index, 2000, lscId, veh);
            };

            if (!veh.Model.IsBike && !veh.Model.IsBicycle)
            {
                list1 = new List<dynamic> {"~r~Снять", "~g~Поставить"};
                var menuItem3 = menu.AddMenuItemList(UiMenu, "Неон (Тип #1)", list1, "Цена: ~g~$100000");
                menuItem3.OnListChanged += (uimenu, index) =>
                {
                    if (index == 0)
                    {
                        SetVehicleNeonLightEnabled(veh.Handle, 0, false);
                        SetVehicleNeonLightEnabled(veh.Handle, 1, false);
                        SetVehicleNeonLightEnabled(veh.Handle, 2, false);
                        SetVehicleNeonLightEnabled(veh.Handle, 3, false);
                        return;
                    }
                    SetVehicleNeonLightsColour(veh.Handle, 255, 255, 255);
                    SetVehicleNeonLightEnabled(veh.Handle, 0, true);
                    SetVehicleNeonLightEnabled(veh.Handle, 1, true);
                    SetVehicleNeonLightEnabled(veh.Handle, 2, true);
                    SetVehicleNeonLightEnabled(veh.Handle, 3, true);
                };
                menuItem3.OnListSelected += (uimenu, index) =>
                {
                    Business.Custom.UpgradeVeh(998, index, 100000, lscId, veh);
                };

                list1 = new List<dynamic> {"~r~Снять", "~g~Поставить"};
                var menuItem4 = menu.AddMenuItemList(UiMenu, "Неон (Тип #2)", list1, "Цена: ~g~$200000");
                menuItem4.OnListChanged += (uimenu, index) =>
                {
                    if (index == 0)
                    {
                        SetVehicleNeonLightEnabled(veh.Handle, 0, false);
                        SetVehicleNeonLightEnabled(veh.Handle, 1, false);
                        SetVehicleNeonLightEnabled(veh.Handle, 2, false);
                        SetVehicleNeonLightEnabled(veh.Handle, 3, false);
                        return;
                    }
                    SetVehicleNeonLightsColour(veh.Handle, 255, 255, 255);
                    SetVehicleNeonLightEnabled(veh.Handle, 0, true);
                    SetVehicleNeonLightEnabled(veh.Handle, 1, true);
                    SetVehicleNeonLightEnabled(veh.Handle, 2, true);
                    SetVehicleNeonLightEnabled(veh.Handle, 3, true);
                };
                menuItem4.OnListSelected += (uimenu, index) =>
                {
                    Business.Custom.UpgradeVeh(999, index, 200000, lscId, veh);
                };
            }

            menu.AddMenuItem(UiMenu, "~r~Закрыть").Activated += (uimenu, item) => { HideMenu(); Business.Custom.CloseMenuWithRemoveMod(veh); };
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowTaxiAskGetTaxiMenu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Гражданский", "~b~Диалог с мужчиной", true, true);
            
            menu.AddMenuItem(UiMenu, "Попросить помочь вызвать такси").Activated += async (uimenu, item) =>
            {
                HideMenu();
                
                Chat.SendChatMessage(User.Data.id.ToString(), "Здравствуйте, не могли бы вы помочь мне вызвать такси?");
                await Delay(1000);
                Chat.SendChatInfoMessage("Гражданин", "Привет, конечно, без проблем");
                
                Managers.Taxi.CreateForNewPlayer(new Vector3(-1026.969f, -2494.6f, 19.67122f), 149.7756f, new Vector3(-1035.026f, -2728.515f, 18.72507f), 240.2601f, new Vector3(-1526.977f, -466.6963f, 34.90295f), 149.7756f);
            };
            menu.AddMenuItem(UiMenu, "~r~Закрыть").Activated += (uimenu, item) =>
            {
                HideMenu();
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowTaxiAsk0Menu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Таксист", "~b~Дать ответ таксисту", true, true);
            
            menu.AddMenuItem(UiMenu, "Заработки").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (User.Skin.SEX == 1)
                    Chat.SendChatMessage(User.Data.id.ToString(), "Привет, приехала сюда на заработки");
                else
                    Chat.SendChatMessage(User.Data.id.ToString(), "Привет, приехал сюда на заработки");
                Managers.Taxi.WaitAnswerId = 0;
            };
            
            menu.AddMenuItem(UiMenu, "Жить").Activated += (uimenu, item) =>
            {
                HideMenu();
                Chat.SendChatMessage(User.Data.id.ToString(), "Привет, хочу переехать сюда");
                Managers.Taxi.WaitAnswerId = 1;
            };
            
            menu.AddMenuItem(UiMenu, "Не хочу отвечать").Activated += (uimenu, item) =>
            {
                HideMenu();
                Chat.SendChatMessage(User.Data.id.ToString(), "Привет, пусть это останется секретом");
                Managers.Taxi.WaitAnswerId = 2;
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowTaxiAsk1Menu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Таксист", "~b~Дать ответ таксисту", true, true);
            
            menu.AddMenuItem(UiMenu, "Да, подскажи").Activated += (uimenu, item) =>
            {
                HideMenu();
                
                if (User.Skin.SEX == 1)
                    Chat.SendChatMessage(User.Data.id.ToString(), "Конечно, буду рада");
                else
                    Chat.SendChatMessage(User.Data.id.ToString(), "Конечно, буду рад");
                Managers.Taxi.WaitAnswerId = 0;
            };
            
            menu.AddMenuItem(UiMenu, "~y~Нет, разберусь").Activated += (uimenu, item) =>
            {
                HideMenu();
                Chat.SendChatMessage(User.Data.id.ToString(), "Нет, спасибо, разберусь");
                Managers.Taxi.WaitAnswerId = 1;
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowTaxiAsk2Menu()
        {
            HideMenu();
            
            var menu = new Menu();
            UiMenu = menu.Create("Таксист", "~b~Дать ответ таксисту", true, true);
            
            menu.AddMenuItem(UiMenu, "Понятно").Activated += (uimenu, item) =>
            {
                HideMenu();
                Chat.SendChatMessage(User.Data.id.ToString(), "Понятно, а где подзаработать?");
                Managers.Taxi.WaitAnswerId = 0;
            };
            
            MenuPool.Add(UiMenu);
        }
        
        public static void ShowTestMenu() //TODO TAB MENU
        {
            HideMenu();

            TabTest = new NativeUI.PauseMenu.TabView("TEST TITLE");
            TabTest.Money = "test12312";
            TabTest.MoneySubtitle = "Test123123123123";
            TabTest.Visible = true;
            TabTest.Name = User.Data.rp_name;
            
            var tabItem = new NativeUI.PauseMenu.TabItem("ITEM NAME");

            var closetem = new NativeUI.PauseMenu.TabTextItem("Закрыть", "Закрыть",
                "ОЧЕНЬ ДЛИННЫЙ ТЕКСТОЧЕНЬ ДЛИННЫЙ ТЕКСТОЧЕНЬ ДЛИННЫЙ ТЕКСТОЧЕНЬ ДЛИННЫЙ ТЕКСТОЧЕНЬ ДЛИННЫЙ ТЕКСТОЧЕНЬ ДЛИННЫЙ ТЕКСТОЧЕНЬ ДЛИННЫЙ ТЕКСТОЧЕНЬ ДЛИННЫЙ ТЕКСТОЧЕНЬ ДЛИННЫЙ ТЕКСТОЧЕНЬ ДЛИННЫЙ ТЕКСТОЧЕНЬ ДЛИННЫЙ ТЕКСТОЧЕНЬ ДЛИННЫЙ ТЕКСТОЧЕНЬ ДЛИННЫЙ ТЕКСТОЧЕНЬ ДЛИННЫЙ ТЕКСТОЧЕНЬ ДЛИННЫЙ ТЕКСТОЧЕНЬ ДЛИННЫЙ ТЕКСТ")
            {
                CanBeFocused = false
            };

            closetem.Activated += (sender, args) =>
            {
                TabTest.Visible = false;
                TabTest = null;
            };
            
            TabTest.AddTab(tabItem);
            TabTest.AddTab(closetem);
        }

        public static void HideMenu()
        {
            MenuPool.CloseAllMenus();
            MenuPool = new MenuPool();
            UiMenu = null;
        }
        
        
        private static async Task ProcessMainMenu()
        {
            if (UiMenu != null)
            {
                if (UiMenu.Visible)
                {
                    Game.DisableControlThisFrame(0, (Control) 157);
                    Game.DisableControlThisFrame(0, (Control) 158);
                }
            }
            else
            {
                await Delay(10);
            }
            
            
            /*else if (UiMenu == null || UiMenu != null && !UiMenu.Visible)
            {
                if (Game.CurrentInputMode == InputMode.MouseAndKeyboard && (Game.IsControlJustPressed(0, (Control) 157) || Game.IsDisabledControlJustPressed(0, (Control) 157)))
                    ShowAuthMenu();
            }*/
        }
        
        private static async Task ProcessMenuPool()
        {
            MenuPool.ProcessMenus();
            if (MenuPool.ToList().Count > 1 || MenuPool.ToList().Count > 0 && Menu.IsShowInput)
                HideMenu();
            
            /*Game.DisableAllControlsThisFrame(0);
            var x = Control.CursorX;
            var y = Control.CursorY;*/
            
            /*Key: 1*/
            
            if (User.IsLogin() && Game.CurrentInputMode == InputMode.MouseAndKeyboard && !Menu.IsShowInput)
            {
                if ((Game.IsControlJustPressed(0, (Control) 244) || Game.IsDisabledControlJustPressed(0, (Control) 244)) && !Sync.Data.HasLocally(User.GetServerId(), "isTie") && !Sync.Data.HasLocally(User.GetServerId(), "isCuff")) //M
                    ShowMainMenu();
                if ((Game.IsControlJustPressed(0, (Control) 246) && Game.IsDisabledControlJustPressed(0, (Control) 303)) && User.IsDead()) //Y
                {
                    var msg = await Menu.GetUserInput("Напишите вопрос", null, 200);
                    if (msg == "NULL") return;
                
                    Shared.TriggerEventToAllPlayers("ARP:SendAskMessage", msg, User.Data.id, User.Data.rp_name);
                
                    Notification.SendWithTime("~g~Вопрос отправлен");
                    Notification.SendWithTime("~g~Если хелперы в сети, они вам ответят");
                }
                if ((Game.IsControlJustPressed(0, (Control) 246) && Game.IsDisabledControlJustPressed(0, (Control) 246)) && User.IsDead()) //U
                {
                    var msg = await Menu.GetUserInput("Напишите жалобу", null, 200);
                    if (msg == "NULL") return;
                
                    Shared.TriggerEventToAllPlayers("ARP:SendReportMessage", msg, User.Data.id, User.Data.rp_name);
                
                    Notification.SendWithTime("~g~Жалоба отправлена");
                    Notification.SendWithTime("~g~Если администрация в сети, она её рассмотрит");
                }
                
                if (Game.IsControlJustPressed(0, (Control) 246) && Sync.Data.HasLocally(User.GetServerId(), "isCuff")) //Y
                {
                    var msg = await Menu.GetUserInput("Напишите вопрос", null, 200);
                    if (msg == "NULL") return;
                
                    Shared.TriggerEventToAllPlayers("ARP:SendAskMessage", msg, User.Data.id, User.Data.rp_name);
                
                    Notification.SendWithTime("~g~Вопрос отправлен");
                    Notification.SendWithTime("~g~Если хелперы в сети, они вам ответят");
                }
                if (Game.IsControlJustPressed(0, (Control) 303) && Sync.Data.HasLocally(User.GetServerId(), "isCuff")) //U
                {
                    var msg = await Menu.GetUserInput("Напишите жалобу", null, 200);
                    if (msg == "NULL") return;
                
                    Shared.TriggerEventToAllPlayers("ARP:SendReportMessage", msg, User.Data.id, User.Data.rp_name);
                
                    Notification.SendWithTime("~g~Жалоба отправлена");
                    Notification.SendWithTime("~g~Если администрация в сети, она её рассмотрит");
                }
                
                if (Game.IsControlJustPressed(0, (Control) 246) && Sync.Data.HasLocally(User.GetServerId(), "isTie")) //Y
                {
                    var msg = await Menu.GetUserInput("Напишите вопрос", null, 200);
                    if (msg == "NULL") return;
                
                    Shared.TriggerEventToAllPlayers("ARP:SendAskMessage", msg, User.Data.id, User.Data.rp_name);
                
                    Notification.SendWithTime("~g~Вопрос отправлен");
                    Notification.SendWithTime("~g~Если хелперы в сети, они вам ответят");
                }
                if (Game.IsControlJustPressed(0, (Control) 303) && Sync.Data.HasLocally(User.GetServerId(), "isTie")) //U
                {
                    var msg = await Menu.GetUserInput("Напишите жалобу", null, 200);
                    if (msg == "NULL") return;
                
                    Shared.TriggerEventToAllPlayers("ARP:SendReportMessage", msg, User.Data.id, User.Data.rp_name);
                
                    Notification.SendWithTime("~g~Жалоба отправлена");
                    Notification.SendWithTime("~g~Если администрация в сети, она её рассмотрит");
                }

                    
                if ((Game.IsControlJustPressed(0, (Control) 157) || Game.IsDisabledControlJustPressed(0, (Control) 157)) && !Sync.Data.HasLocally(User.GetServerId(), "isTie") && !Sync.Data.HasLocally(User.GetServerId(), "isCuff")) //1
                    ShowPlayerMenu();
                
                if (Game.IsControlJustPressed(0, (Control) 158) || Game.IsDisabledControlJustPressed(0, (Control) 158)) //2
                {
                    if (IsPedInAnyVehicle(PlayerPedId(), true))
                    {
                        var veh = GetVehiclePedIsUsing(PlayerPedId());
                        if (GetPedInVehicleSeat(veh, -1) == PlayerPedId())
                            ShowVehicleMenu(new CitizenFX.Core.Vehicle(veh));
                    }
                    else
                    {
                        var veh = Main.FindNearestVehicle();
                        if (veh != null)
                            ShowVehicleOutMenu(veh);
                        else
                            ShowVehicleOut2Menu();
                    }
                }
                
                if (IsPedInAnyVehicle(PlayerPedId(), false) && (Game.IsControlJustPressed(0, (Control) 75) || Game.IsDisabledControlJustPressed(0, (Control) 160)))
                {
                    await Delay(300);
                    var veh = GetVehiclePedIsIn(PlayerPedId(), true);
                    if (GetIsVehicleEngineRunning(veh))
                    {
                        SetVehicleEngineOn(veh, true, true, true);
                        SetVehicleRadioEnabled(veh, true);
                        SetVehicleRadioLoud(veh, true);
                    }
                }
       
                if ((Game.IsControlJustPressed(0, (Control) 160) || Game.IsDisabledControlJustPressed(0, (Control) 160)) && !Sync.Data.HasLocally(User.GetServerId(), "isTie") && !Sync.Data.HasLocally(User.GetServerId(), "isCuff")) //3
                {
                    await User.GetAllData();
                    if (User.Data.fraction_id > 0)
                        ShowFractionMenu();
                }
                if (Game.IsControlJustPressed(0, (Control) 164) || Game.IsDisabledControlJustPressed(0, (Control) 164)) //4
                {
                    await User.GetAllData();
                    if (User.Data.fraction_id2 > 0)
                        ShowFraction2Menu();
                }
                if (Game.IsControlJustPressed(0, (Control) 165) || Game.IsDisabledControlJustPressed(0, (Control) 165)) //5
                {
                }
                if (Game.IsControlJustPressed(0, (Control) 162) || Game.IsDisabledControlJustPressed(0, (Control) 162)) //8
                {
                    if (User.IsAdmin())
                        ShowAdminMenu();
                }
                if ((Game.IsControlJustPressed(0, (Control) 163) || Game.IsDisabledControlJustPressed(0, (Control) 163)) && !Sync.Data.HasLocally(User.GetServerId(), "isTie") && !Sync.Data.HasLocally(User.GetServerId(), "isCuff")) //9
                {
                    ShowAnimationMenu();
                }
                if ((Game.IsControlJustPressed(0, (Control) 161) || Game.IsDisabledControlJustPressed(0, (Control) 161)) && !Sync.Data.HasLocally(User.GetServerId(), "isTie") && !Sync.Data.HasLocally(User.GetServerId(), "isCuff")) //9
                {
                    ShowAnimationActionMenu();
                }
                if (Game.IsControlJustPressed(0, (Control) 19) || Game.IsDisabledControlJustPressed(0, (Control) 19)) //LALT
                {
                    Managers.Pickup.CheckPlayerPosToPickup();
                }
                /*if (Game.IsControlJustPressed(0, (Control) 47) || Game.IsDisabledControlJustPressed(0, (Control) 47)) //G
                {
                    if (IsPedInAnyVehicle(PlayerPedId(), true))
                    {
                        var veh = GetVehiclePedIsUsing(PlayerPedId());
                        if (GetPedInVehicleSeat(veh, -1) == PlayerPedId())
                        {
                            if (!Sync.Data.HasLocally(VehToNet(veh) + 100000, "isSiren"))
                            {
                                Managers.Vehicle.DisableSirenSound(new CitizenFX.Core.Vehicle(veh), true);
                                Sync.Data.SetLocally(VehToNet(veh) + 100000, "isSiren", true);
                            }
                            else
                            {
                                Managers.Vehicle.DisableSirenSound(new CitizenFX.Core.Vehicle(veh), false);
                                Sync.Data.ResetLocally(VehToNet(veh) + 100000, "isSiren");
                            }
                        }
                    }
                }*/
                if (Business.Custom.IsOpenMenu && (Game.IsControlJustPressed(0, (Control) 22) || Game.IsDisabledControlJustPressed(0, (Control) 22)))
                {
                    Business.Custom.CameraNext();
                }
                if ((Game.IsControlJustPressed(0, (Control) 38) || Game.IsDisabledControlJustPressed(0, (Control) 38)) && !Sync.Data.HasLocally(User.GetServerId(), "isTie") && !Sync.Data.HasLocally(User.GetServerId(), "isCuff")) //E
                {
                    if (OpenE)
                        ShowCharacherCustomMenu();
                    
                    if (Business.Custom.IsOpenMenu || Business.Custom.IsOpenColorMenu)
                        ShowLscMenu(Business.Custom.MenuId);

                    if (Jobs.Trash.IsStartWork && !IsPedInAnyVehicle(PlayerPedId(), true))
                        ShowJobTrashMenu();

                    var pos = GetEntityCoords(GetPlayerPed(-1), true);
                    
                    Business.Gum.Start();
                    Business.Casino.Start();
                    Business.Gun.CheckPosForOpenMenu();
                    Business.Shop.CheckPosForOpenMenu();
                    Business.Fuel.CheckPosForOpenMenu();
                    Business.Bank.CheckPosForOpenMenu();
                    Business.Rent.CheckPosForOpenMenu();
                    Business.Rent.CheckPosForOpenCarMenu();
                    Business.Tattoo.CheckPosForOpenMenu();
                    Business.Bar.CheckPosForOpenMenu();
                    Business.Cloth.CheckPosForOpenMenu();
                    Business.BarberShop.CheckPosForOpenMenu();
                    Business.Bank.CheckPosForBankOfficeOpenMenu();
                    Business.Custom.CheckPosPickups();
                    Business.CarNumber.CheckPosPickups();
                    Managers.Pickup.CheckPlayerPosToPickupPressE();
                    //Managers.Grab.GrabStock();
                    Managers.Grab.OpenSellCarMenuList();

                    if (User.Data.bank_prefix == 2222 && Timer.EntityFleeca > 0)
                        ShowBankAtmMenu();
                    if (User.Data.bank_prefix > 0 && (Timer.EntityOther1 > 0 || Timer.EntityOther2 > 0 || Timer.EntityOther3 > 0))
                        ShowBankAtmMenu();
                    if (User.Data.bank_prefix > 0 && (Timer.EntityHeal > 0 || Timer.EntityHeal > 0 || Timer.EntityHeal > 0))
                        ShowHealWardrobeMenu();
                    
                    Managers.Apartment.MenuEnter();
                    Managers.Apartment.MenuExit();

                    if (User.GetPlayerVirtualWorld() > 50000 && Main.GetDistanceToSquared(pos, Stock.StockPos) < 2)
                        ShowStockTakeMenu();
                    if (User.GetPlayerVirtualWorld() > 50000 && Main.GetDistanceToSquared(pos, Stock.PcPos) < 2)
                        ShowStockPcMenu();
                    
                    int hId = House.GetHouseInRadiusOfPosition(pos, 1.5f);
                    int intId = House.GetHouseInteriorInRadiusOfPosition(pos, 1.5f);
                    if (hId != -1)
                        House.MenuEnterHouse(House.HouseGlobalDataList[hId]);
                    else if (intId != -1)
                        House.MenuExitHouse(House.HouseGlobalDataList[intId]);
                    
                    int cId = Condo.GetHouseInRadiusOfPosition(pos, 1.5f);
                    int cintId = Condo.GetHouseInteriorInRadiusOfPosition(pos, 1.5f);
                    if (cId != -1)
                        Condo.MenuEnterHouse(Condo.CondoGlobalDataList[cId]);
                    else if (cintId != -1)
                        Condo.MenuExitHouse(Condo.CondoGlobalDataList[cintId]);
                    
                    int sId = Stock.GetStockInRadiusOfPosition(pos, 1.5f);
                    int sintId = Stock.GetStockInteriorInRadiusOfPosition(pos, 1.5f);
                    if (sId != -1)
                        Stock.MenuEnterStock(Stock.StockGlobalDataList[sId]);
                    else if (sintId != -1)
                        Stock.MenuExitStock(Stock.StockGlobalDataList[sintId]);
                }

                //F10
                if ((Game.IsControlJustPressed(0, (Control) 57) || Game.IsDisabledControlJustPressed(0, (Control) 57)) && !Sync.Data.HasLocally(User.GetServerId(), "isTie") && !Sync.Data.HasLocally(User.GetServerId(), "isCuff")) //E
                {
                    User.StopAnimation();
                    User.StopScenario();
                }
                //F9
                if ((Game.IsControlJustPressed(0, (Control) 56) || Game.IsDisabledControlJustPressed(0, (Control) 56)) && !Sync.Data.HasLocally(User.GetServerId(), "isTie") && !Sync.Data.HasLocally(User.GetServerId(), "isCuff")) //E
                {
                    ShowAnimationFastMenu();
                }
                //Ё
                if ((Game.IsControlJustPressed(0, (Control) 243) || Game.IsDisabledControlJustPressed(0, (Control) 243)) && !Sync.Data.HasLocally(User.GetServerId(), "isTie") && !Sync.Data.HasLocally(User.GetServerId(), "isCuff")) //E
                {
                    ShowInvSelectMenu();
                }
                if ((Game.IsControlJustPressed(0, (Control) 159) || Game.IsDisabledControlJustPressed(0, (Control) 159)) && !Sync.Data.HasLocally(User.GetServerId(), "isTie") && !Sync.Data.HasLocally(User.GetServerId(), "isCuff")) //E
                {
                    if (User.Data.phone > 0)
                    {
                        ShowPlayerPhoneMenu();//zametka 5
                    }
                }
                if ((Game.IsControlJustPressed(0, (Control) 165) || Game.IsDisabledControlJustPressed(0, (Control) 165)) && !Sync.Data.HasLocally(User.GetServerId(), "isTie") && !Sync.Data.HasLocally(User.GetServerId(), "isCuff")) //E
                {
                    if (User.Data.is_buy_walkietalkie)
                    {
                        ShowPlayerWalkietalkieMenu();
                    }
                }
                

                if (IsPedInAnyVehicle(PlayerPedId(), true))
                {
                    var veh = GetVehiclePedIsUsing(PlayerPedId());
                    
                    if (GetPedInVehicleSeat(veh, -1) == PlayerPedId())
                    {
                        var state = GetVehicleIndicatorLights(veh); // 0 = none, 1 = left, 2 = right, 3 = both
                    
                        if (Game.IsControlJustPressed(0, (Control) 174) || Game.IsDisabledControlJustPressed(0, (Control) 174)) // left indicator
                        {
                            if (state != 1) // Left indicator is (only) off
                            {
                                Managers.Vehicle.EnableLeftIndicator(new CitizenFX.Core.Vehicle(veh), true);
                                Managers.Vehicle.EnableRightIndicator(new CitizenFX.Core.Vehicle(veh), false);
                            }
                            else
                            {
                                Managers.Vehicle.EnableLeftIndicator(new CitizenFX.Core.Vehicle(veh), false);
                                Managers.Vehicle.EnableRightIndicator(new CitizenFX.Core.Vehicle(veh), false);
                            }
                        }
                        else if (Game.IsControlJustPressed(0, (Control) 175) || Game.IsDisabledControlJustPressed(0, (Control) 175)) // right indicator
                        {
                            if (state != 2) // Right indicator (only) is off
                            {
                                Managers.Vehicle.EnableLeftIndicator(new CitizenFX.Core.Vehicle(veh), false);
                                Managers.Vehicle.EnableRightIndicator(new CitizenFX.Core.Vehicle(veh), true);
                            }
                            else
                            {
                                Managers.Vehicle.EnableLeftIndicator(new CitizenFX.Core.Vehicle(veh), false);
                                Managers.Vehicle.EnableRightIndicator(new CitizenFX.Core.Vehicle(veh), false);
                            }
                        } 
                    }
                }
            }
            if (!User.IsLogin() && Game.CurrentInputMode == InputMode.MouseAndKeyboard && !Menu.IsShowInput)
            {
                if ((Game.IsControlJustPressed(0, (Control) 38) || Game.IsDisabledControlJustPressed(0, (Control) 38)))
                    ShowAuthMenu();
                /*if (MenuPool.ToList().Count == 0)
                    ShowAuthMenu();*/
            }
        }
    }
}

