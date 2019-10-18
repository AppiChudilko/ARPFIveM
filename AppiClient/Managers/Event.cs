using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Event : BaseScript
    {
        private static bool _isDead = false;
        private static bool _hasBeenDead = false;
        private static int _diedAt = 0;
        public static uint KillerWeapon = 0;
        
        private static int _ped = -1;
        
        public Event()
        {
            EventHandlers.Add("populationPedCreating", new Action<float, float, float, dynamic, dynamic>(OnPopulationPedCreating));
            
            EventHandlers.Add("ARP:TeleportToAdmin", new Action<float, float, float>(TeleportToAdmin));
            
            EventHandlers.Add("ARP:Knockout", new Action(User.Knockout));
            EventHandlers.Add("ARP:Tie", new Action(User.Tie));
            EventHandlers.Add("ARP:UnTie", new Action(User.UnTie));
            EventHandlers.Add("ARP:UseRope", new Action(User.UseTie));
            EventHandlers.Add("ARP:UseTieBandage", new Action(User.UseTieBandage));
            EventHandlers.Add("ARP:TieBandage", new Action(User.TieBandage));
            EventHandlers.Add("ARP:UnTieBandage", new Action(User.UnTieBandage));
            EventHandlers.Add("ARP:Incar", new Action(User.Incar));
            EventHandlers.Add("ARP:IncarAdmin", new Action(User.AdminInCar));
            EventHandlers.Add("ARP:UnDuty", new Action(User.UnDuty));
            EventHandlers.Add("ARP:UpdateAllData", new Action(User.GetAllDataEvent));
            EventHandlers.Add("ARP:SellPlayer", new Action(User.SellPlayer));
            EventHandlers.Add("ARP:EjectCar", new Action(User.EjectCar));
            EventHandlers.Add("ARP:TakeAllGuns", new Action<int>(User.TakeAllGuns));
            EventHandlers.Add("ARP:TakeAllGunsSAPD", new Action<int>(User.TakeAllGunSAPD));
            EventHandlers.Add("ARP:UseAdrenalin", new Action(User.UseAdrenalin));
            EventHandlers.Add("ARP:UseDef", new Action(User.UseDef));
            EventHandlers.Add("ARP:UseFirstAidKit", new Action(User.UseFirstAidKit));
            EventHandlers.Add("ARP:EmsHeal", new Action(User.EmsHeal));
            EventHandlers.Add("ARP:HospPlayer", new Action(HospPlayer));
            EventHandlers.Add("ARP:SellVehicleToUserShowMenu", new Action<int, int, string, string, int>(MenuList.ShowSellVehicleToUserMenu));
            EventHandlers.Add("ARP:SellBusinessToUserShowMenu", new Action<int, int, string, int>(MenuList.SellBusinessToUserShowMenu));
            EventHandlers.Add("ARP:SellHouseToUserShowMenu", new Action<int, int, string, int>(MenuList.SellHouseToUserShowMenu));
            EventHandlers.Add("ARP:AcceptSellToUser", new Action<int, int, int>(Vehicle.AcceptSellToUser));
            EventHandlers.Add("ARP:AcceptBuyBusinessToUser", new Action<int>(Business.Business.AcceptBuyBusinessToUser));
            EventHandlers.Add("ARP:AcceptBuyHouseToUser", new Action<int>(House.AcceptBuyHouseToUser));
            
            EventHandlers.Add("ARP:TalkNpc", new Action<int, string, string, string>(TalkNpc));
            EventHandlers.Add("ARP:TalkNpcToNet", new Action<int, string, string, string>(TalkNpcToNet));
            
            /*EventHandlers.Add("ARP:InviteMp", new Action<float, float, float>(MenuList.ShowAskInviteMpMenu));
            EventHandlers.Add("ARP:SlapMp", new Action<float, float, float, int, int>(User.SlapMp));
            EventHandlers.Add("ARP:GiveArmorMp", new Action<float, float, float, int, int>(User.GiveArmorMp));
            EventHandlers.Add("ARP:GiveHealthMp", new Action<float, float, float, int, int>(User.GiveHealthMp));
            EventHandlers.Add("ARP:GiveGunMp", new Action<float, float, float, int, string, int>(User.GiveGunMp));
            EventHandlers.Add("ARP:Admin:JailPlayer", new Action<int, int, string>(User.AdminJailPlayer));*/
            
            EventHandlers.Add("ARP:MisterK:ResetWanted", new Action<int, int>(MisterKResetWanted));
            EventHandlers.Add("ARP:MisterK:ResetWanted:Failed", new Action<int, string>(MisterKResetWantedFailed));
            EventHandlers.Add("ARP:MisterK:ResetWanted:Accept", new Action<int, int>(MisterKResetWantedAccept));
            
            EventHandlers.Add("ARP:OnPlayerDeath", new Action<int, float, float, float>(OnPlayerDeath));
            EventHandlers.Add("ARP:OnPlayerKiller", new Action<int, int, uint, string, float, float, float, float, float, float>(OnPlayerKiller));
            EventHandlers.Add("ARP:OnPlayerWasted", new Action<float, float, float>(OnPlayerWasted));
            
            EventHandlers.Add("ARP:UpdateVehicleNumber", new Action<int, int, int, string>(Vehicle.UpdateVehicleNumber));
            
            EventHandlers.Add("ARP:SellMeat", new Action<int, int>(SellMeat));
            EventHandlers.Add("ARP:SellFish", new Action<int, int>(SellFish));
            EventHandlers.Add("ARP:DeleteObject", new Action<int>(DeleteObject));
            
            EventHandlers.Add("ARP:TransferBank", new Action(TransferBank));
            EventHandlers.Add("ARP:PayTaxByNumber", new Action(PayTaxByNumber));
            EventHandlers.Add("ARP:ShowBankMenu", new Action(ShowBankMenu));
            EventHandlers.Add("ARP:SetWaypoint", new Action<float, float>(User.SetWaypoint));
            EventHandlers.Add("ARP:ArcadiusMenu", new Action(ArcadiusMenu));
            EventHandlers.Add("ARP:InvaderLoto", new Action(InvaderLoto));
            EventHandlers.Add("ARP:InvaderAd", new Action<int>(InvaderAd));
            EventHandlers.Add("ARP:ShowSmsList", new Action(ShowSmsList));
            EventHandlers.Add("ARP:ShowContList", new Action(ShowContList));
            EventHandlers.Add("ARP:HidePhone", new Action(HidePhone));
            EventHandlers.Add("ARP:911", new Action(C911));
            EventHandlers.Add("ARP:Misterk1", new Action(Misterk1));
            EventHandlers.Add("ARP:Misterk2", new Action(Misterk2));
            EventHandlers.Add("ARP:NewCont", new Action(NewCont));
            EventHandlers.Add("ARP:NewSms", new Action(NewSms));
            EventHandlers.Add("ARP:NewSmsWithNumber", new Action<string>(NewSmsWithNumber));
            EventHandlers.Add("ARP:ReadSms", new Action<string>(ReadSms));
            EventHandlers.Add("ARP:DelSms", new Action<int>(DelSms));
            EventHandlers.Add("ARP:DelCont", new Action<int>(DelCont));
            EventHandlers.Add("ARP:SmsInfo", new Action<int>(SmsInfo));
            EventHandlers.Add("ARP:ContInfo", new Action<int>(ContInfo));
            EventHandlers.Add("ARP:AddConsoleMessage", new Action<string>(Ctos.ExecuteCommand));
            
            EventHandlers.Add("ARP:GetCar", new Action<string>(GetCar));
            EventHandlers.Add("ARP:GiveAllWeapon", new Action(GiveAllWeapon));
            
            EventHandlers.Add("ARP:PromocodeActivate", new Action<string>(PromocodeActivate));
            
            //EventHandlers.Add("ARP:GrSix:DropCheckpoint", new Action<int>(Jobs.GroupSix.DropCheckpoint));
            EventHandlers.Add("ARP:GrSix:Grab", new Action<int>(Jobs.GroupSix.Grab));
            EventHandlers.Add("ARP:GrSix:Pay", new Action<int, int>(Jobs.GroupSix.DeleteVeh));
            
            EventHandlers.Add("ARP:HideMenu", new Action(MenuList.HideMenu));
            Tick += TickTimer;
        }
        
        public static void PromocodeActivate(string code)
        {
            switch (code)
            {
                case "VK":
                case "MYRKA":
                case "BROTHERS":
                    User.AddCashMoney(500);
                    User.Data.allow_marg = true;
                    Client.Sync.Data.Set(User.GetServerId(), "allow_marg", true);
                    
                    Inventory.AddItemServer(221, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                    Inventory.AddItemServer(26, 2, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                    Inventory.AddItemServer(16, 2, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);
                    
                    Notification.SendWithTime("~g~Вы активировали промокод");
                    Notification.SendWithTime("Получено: Пицца 2шт, Антипохмелин 1шт, Энергетик 2шт");
                    Notification.SendWithTime("Получено: Одноразовый рецепт марихуаны");
                    Notification.SendWithTime("Получено: 500$");
                    break;
                case "VMP":
                    Notification.SendWithTime("~g~Вы активировали промокод");
                    Inventory.TakeNewItem(259);
                    Inventory.TakeNewItem(261);
                    break;
                case "REDAGE":
                    Notification.SendWithTime("~g~Вы активировали промокод");
                    Inventory.TakeNewItem(260);
                    Inventory.TakeNewItem(261);
                    break;
                case "UMBRELLA":
                case "VINIPUX":
                    Notification.SendWithTime("~g~Вы активировали промокод");
                    Inventory.TakeNewItem(255);
                    Inventory.TakeNewItem(261);
                    break;
            }
        }
        
        public static async void GetCar(string car)
        {
            await Vehicle.SpawnByName(car, GetEntityCoords(GetPlayerPed(-1), true), GetEntityHeading(GetPlayerPed(-1)));
        }
        
        public static void GiveAllWeapon()
        {
            foreach(uint hash in Enum.GetValues(typeof(WeaponHash)))
                User.GiveWeapon(hash, 9999, false, true);
        }

        public static void HospPlayer()
        {
            User.Respawn(Spawn.HospSpawn, 90);
        }

        public static async void TransferBank()
        {
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
        }

        public static async void PayTaxByNumber()
        {
            int score = Convert.ToInt32(await Menu.GetUserInput("Счёт", null, 10));
            int num = Convert.ToInt32(await Menu.GetUserInput(Lang.GetTextToPlayer("_lang_1"), null, 8));
            if (num > await User.GetBankMoney())
            {
                Notification.SendWithTime("~r~У вас на счёте недостаточно денег");
                return;
            }
            TriggerServerEvent("ARP:PayTax", 1, num, score);
            User.StopScenario();
        }

        public static async void ShowBankMenu()
        {
            TriggerEvent("ARPPhone:ShowBankMenu", $"${await User.GetBankMoney():#,#}", $"{User.Data.bank_prefix}-{User.Data.bank_number}");
        }

        public static async void ArcadiusMenu()
        {
            if (User.Data.business_id > 0)
            {
                var money = await Business.Business.GetMoney(User.Data.business_id);
                Notification.SendPicture("На вашем счету: " + money, "Arcadius", "Ваш счёт", "CHAR_SOCIAL_CLUB", Notification.TypeChatbox);
            }
            else
            {
                Notification.SendPicture("Аккаунт не найден в системе", "Arcadius", "Ошибка", "CHAR_SOCIAL_CLUB", Notification.TypeChatbox);
            }
        }

        public static async void C911()
        {
            var text = await Menu.GetUserInput("Текст...", null, 50);
            if (text == "NULL") return;
            Notification.SendWithTime("~b~Сообщение было отправлено");
            Dispatcher.SendEms(User.Data.phone_code + "-" + User.Data.phone, text);
            User.StopScenario();
        }

        public static async void Misterk1()
        {
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
        }

        public static async void Misterk2()
        {
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
            
            Main.SaveLog("Cartel", $"[RESET_WANTED_START] {User.Data.rp_name}");

            await Delay(240000);
                    
            Client.Sync.Data.ResetLocally(User.GetServerId(), "misterTimeout");
        }

        public static async void InvaderLoto()
        {
            if (User.Data.money_bank < 7)
            {
                Notification.SendWithTime("~r~У Вас недостаточно денег в банке");
                return;
            }

            if (Client.Sync.Data.HasLocally(User.GetServerId(), "loto"))
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
                
            Client.Sync.Data.SetLocally(User.GetServerId(), "loto", number);
            Notification.SendWithTime("~g~Вы купили билет с числом " + Client.Sync.Data.GetLocally(User.GetServerId(), "loto"));
            User.StopScenario();
        }

        public static async void InvaderAd(int idx)
        {
            if (User.Data.money_bank < 100)
            {
                Notification.SendWithTime("~r~У Вас недостаточно денег в банке");
                return;
            }

            if (Client.Sync.Data.HasLocally(User.GetServerId(), "isAdTimeout"))
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

            text = Main.RemoveQuotes(text);
            TriggerServerEvent("ARP:AddAd", text, User.Data.rp_name, $"{User.Data.phone_code}-{User.Data.phone}", type);

            text = (text.Length > 49) ? text.Remove(51) + "..." : text;
                
            Notification.SendPictureToAll(text, "~g~Реклама", $"{User.Data.phone_code}-{User.Data.phone} ({User.Data.id})", "CHAR_LIFEINVADER", Notification.TypeChatbox);
                
            Main.SaveLog("AD", $"{User.Data.rp_name} ({User.Data.id}) - {text}");
                
            Client.Sync.Data.SetLocally(User.GetServerId(), "isAdTimeout", true);
            await Delay(300000);
            Client.Sync.Data.ResetLocally(User.GetServerId(), "isAdTimeout");
            User.StopScenario();
        }

        public static async void NewCont()
        {
            var title = await Menu.GetUserInput("Заголовок");
            if (title == "NULL") return;
            var num = await Menu.GetUserInput("Номер");
            if (num == "NULL") return;
            TriggerServerEvent("ARP:AddContact", $"{User.Data.phone_code}-{User.Data.phone}", title, num);
            User.StopScenario();
        }
        
        public static async void NewSms()
        {
            var number = await Menu.GetUserInput("Введите номер телефона", "", 15);
            var text = await Menu.GetUserInput("Текст", "", 300);
            if (text == "NULL") return;
            Chat.SendMeCommand("достал телефон и отправил смс");
            TriggerServerEvent("ARP:SendSms", Main.RemoveQuotes(number), Main.RemoveQuotes(text));
            User.StopScenario();
        }

        public static async void NewSmsWithNumber(string number)
        {
            var text = await Menu.GetUserInput("Текст", "", 300);
            if (text == "NULL") return;
            Chat.SendMeCommand("достал телефон и отправил смс");
            TriggerServerEvent("ARP:SendSms", Main.RemoveQuotes(number), Main.RemoveQuotes(text));
            User.StopScenario();
        }

        public static void ReadSms(string text)
        {
            UI.ShowToolTip(text);
        }

        public static void DelSms(int id)
        {
            TriggerServerEvent("ARP:DeleteSms", id);
            ShowSmsList();
        }

        public static void DelCont(int id)
        {
            TriggerServerEvent("ARP:DeleteContact", id);
            ShowContList();
        }

        public static void SmsInfo(int id)
        {
            TriggerServerEvent("ARP:OpenSmsInfoMenu", id);
        }

        public static void ContInfo(int id)
        {
            TriggerServerEvent("ARP:OpenContInfoMenu", id);
        }
        
        public static void ShowSmsList()
        {
            TriggerServerEvent("ARP:OpenSmsListMenu", User.Data.phone_code + "-" + User.Data.phone);
        }
        
        public static void ShowContList()
        {
            TriggerServerEvent("ARP:OpenContacntListMenu", User.Data.phone_code + "-" + User.Data.phone);
        }

        public static void HidePhone()
        {
            User.StopScenario();
        }

        public static void DeleteObject(int netId)
        {
            if (NetworkDoesEntityExistWithNetworkId(netId))
                new Prop(NetToObj(netId)).Delete();
        }

        public static void SellMeat(int money, int count)
        {
            if (count == 0)
            {
                Notification.SendWithTime("~r~У Вас нет мяса для продажи");
                return;
            }
            
            Notification.SendWithTime($"~g~Вы продали {count}шт. мяса");
            Notification.SendWithTime($"~g~Вы заработали: ~s~${money:#,#}");
            User.AddCashMoney(money);
        }

        public static void SellFish(int money, int count)
        {
            if (count == 0)
            {
                Notification.SendWithTime("~r~У Вас нет рыбы для продажи");
                return;
            }
            
            Notification.SendWithTime($"~g~Вы продали {count}шт. рыбы");
            Notification.SendWithTime($"~g~Вы заработали: ~s~${money:#,#}");
            User.AddCashMoney(money);
        }
        public static void SellJewelry(int money, int count)
        {
            if (count == 0)
            {
                Notification.SendWithTime("~r~У Вас нет предметов для продажи");
                return;
            }
            
            Notification.SendWithTime($"~g~Вы продали {count}шт. предметов");
            Notification.SendWithTime($"~g~Вы заработали: ~s~${money:#,#}");
            User.AddCashMoney(money);
        }

        public static void TeleportToAdmin(float x, float y, float z)
        {
            User.Teleport(new Vector3(x, y, z));
            Notification.SendWithTime("~y~Вас телепортнул администратор");
        }

        public static async void OnPopulationPedCreating(float x, float y, float z, dynamic model, dynamic other)
        {
            /*if (Weather.Temp < 10)
            {
                try
                {
                    uint hashOld = Convert.ToUInt32(model);
                    uint hash = (uint) Weather.ReplaceSummerToWinterSkin((int) hashOld);

                    var ms = 2000;
                    while (!HasModelLoaded(hash) && ms > 0)
                    {
                        RequestModel(hash);
                        ms--;
                        await Delay(1);
                    }
            
                    other.setModel(hash);

                    if (hashOld != hash && User.IsAdmin())
                        Debug.WriteLine($"CREATE PED {x} {y} {z} {model} {other}");
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"ERROR REPLACE WINTER PED {e}");
                }
            }*/
        }

        public static void TalkNpc(int ped, string param1, string param2, string param3)
        {
            foreach (var p in Main.GetPedListOnRadius())
            {
                if (p.Model.Hash != ped) continue;
                PlayAmbientSpeechWithVoice(p.Handle, param1, param2, param3, false);   
                SetEntityAlpha(p.Handle, 0, GetHashKey("player_one"));
            }
        }

        public static void TalkNpcToNet(int ped, string param1, string param2, string param3)
        {
            foreach (var p in Main.GetPedListOnRadius())
            {
                if (PedToNet(p.Handle) != ped) continue;
                PlayAmbientSpeechWithVoice(p.Handle, param1, param2, param3, false);   
                SetEntityAlpha(p.Handle, 0, GetHashKey("player_one"));
            }
        }

        public static void MisterKResetWantedFailed(int id, string msg)
        {
            if (User.Data.id != id) return;
            Notification.SendWithTime("~r~" + msg);
        }

        public static void MisterKResetWantedAccept(int id, int money)
        {
            if (User.Data.id != id) return;
            
            /*int moneyFull = money / 2;
            while (moneyFull > 0)
            {
                if (moneyFull == 1)
                {
                    Managers.Inventory.AddItemServer(138, 1, InventoryTypes.StockGang, 8, moneyFull, -1, -1, -1);
                    moneyFull = 0;
                }
                else if (moneyFull == 100)
                {
                    Managers.Inventory.AddItemServer(139, 1, InventoryTypes.StockGang, 8, moneyFull, -1, -1, -1);
                    moneyFull = 0;
                }
                else if (moneyFull <= 4000)
                {
                    Managers.Inventory.AddItemServer(140, 1, InventoryTypes.StockGang, 8, moneyFull, -1, -1, -1);
                    moneyFull = 0;
                }
                else if (moneyFull <= 8000)
                {
                    Managers.Inventory.AddItemServer(141, 1, InventoryTypes.StockGang, 8, moneyFull, -1, -1, -1);
                    moneyFull = 0;
                }
                else
                {
                    Managers.Inventory.AddItemServer(141, 1, InventoryTypes.StockGang, 8, 8000, -1, -1, -1);
                    moneyFull = moneyFull - 8000;
                }
            }*/
            
            Client.Sync.Data.SetLocally(User.GetServerId(), "GrabCash", money / 2 / User.Bonus);
            //User.AddBankMoney(money / 2);
            Notification.SendWithTime("~g~Вы заработали: $" + (money / 2 / User.Bonus));
            Notification.SendWithTime("Теперь вам надо отмыть деньги");
        }

        public static void MisterKResetWanted(int id, int idFrom)
        {
            if (User.Data.id != id) return;
            if (User.Data.wanted_level == 0) return;
            MenuList.ShowMisterKAccpetResetWantedMenu(id, idFrom);
        }

        public static void MisterKAcceptResetWanted(int id, int idFrom)
        {
            if (User.Data.money_bank < 1000 * User.Data.wanted_level)
            {
                Shared.TriggerEventToAllPlayers("ARP:MisterK:ResetWanted:Failed", idFrom, "У игрока нет $" + (1000 * User.Data.wanted_level) + " в банке");
                Notification.SendWithTime("~r~У вас нет $" + (1000 * User.Data.wanted_level) + " в банке");
                return;
            }
            
            User.RemoveBankMoney(1000 * User.Data.wanted_level);
            Notification.SendWithTime("~g~Вы воспользовались услугой, заплавтив: $" + (1000 * User.Data.wanted_level));
            Shared.TriggerEventToAllPlayers("ARP:MisterK:ResetWanted:Accept", idFrom, (1000 * User.Data.wanted_level));
            
            Main.SaveLog("Cartel", $"[RESET_WANTED_FINISH] {User.Data.rp_name} {(1000 * User.Data.wanted_level)}");
            
            User.Data.wanted_level = 0;
            Client.Sync.Data.Set(User.GetServerId(), "wanted_level", 0);
        }

        public static void OnPlayerDeath(int killerType, float x, float y, float z)
        {
            Jobs.GroupSix.MoneyInCar = 0;
            Main.SaveLog("Death", $"[DEATH] [{User.Data.rp_name}] [X:{x}, Y:{y}, Z:{z}] [TYPE: {killerType}]");
        }

        public static async void OnPlayerKiller(int killerType, int killerId, uint killerWeapon, string killerVehicleName, float x, float y, float z, float killerPosX, float killerPosY, float killerPosZ)
        {
            if (await Client.Sync.Data.Has(killerId, "rp_name"))
                Main.SaveLog("Death", $"[KILL] [{User.Data.rp_name}] [X:{x}, Y:{y}, Z:{z}] [TYPE: {killerType}] [NAME: {await Client.Sync.Data.Get(killerId, "rp_name")}]" +
                                              $"[WEAPON: {killerWeapon}] [VEHNAME: {killerVehicleName}] [KILLER POS X:{killerPosX}, Y:{killerPosY}, Z:{killerPosZ}]");
            else
                Main.SaveLog("Death", $"[KILL] [{User.Data.rp_name}] [X:{x}, Y:{y}, Z:{z}] [TYPE: {killerType}] [SERVERID: {killerId}]" +
                                              $"[WEAPON: {killerWeapon}] [VEHNAME: {killerVehicleName}] [KILLER POS X:{killerPosX}, Y:{killerPosY}, Z:{killerPosZ}]");
        }

        public static void OnPlayerWasted(float x, float y, float z)
        {
            Main.SaveLog("Death", $"[WASTED] [{User.Data.rp_name}] [X:{x}, Y:{y}, Z:{z}]");
        }

        private static async Task TickTimer()
        {
            var player = PlayerId();

            if (NetworkIsPlayerActive(player))
            {
                var ped = PlayerPedId();
                
                if (IsPedFatallyInjured(ped) && !_isDead)
                {
                    _isDead = true;

                    if (!_isDead)
                        _diedAt = GetGameTimer();

                    var killer = NetworkGetEntityKillerOfPlayer(player, ref KillerWeapon);
                    var killerEntityType = GetEntityType(killer);
                    var killerType = -1;
                    var killerVehicleName = "";
                    var killerId = GetPlayerByEntityId(killer);

                    if (killerEntityType == 1)
                    {
                        killerType = GetPedType(killer);

                        if (IsPedInAnyVehicle(killer, false))
                            killerVehicleName = GetDisplayNameFromVehicleModel((uint) GetEntityModel(GetVehiclePedIsUsing(killer)));
                    }

                    if (killer != ped && killerId != -1 && NetworkIsPlayerActive(killerId))
                        killerId = GetPlayerServerId(killerId);

                    if (killer == ped || killer == -1 || killerId == -1 || killerType == -1)
                    {
                        /*var pos = GetEntityCoords(ped, true);
                        TriggerEvent("ARP:OnPlayerDeath", killerType, pos.X, pos.Y, pos.Z);*/
                        _hasBeenDead = true;
                    }
                    else
                    {
                        var pos = GetEntityCoords(ped, true);
                        var killerPos = GetEntityCoords(GetPlayerPed(killerId), true);
                        TriggerEvent("ARP:OnPlayerKiller", killerType, killerId, KillerWeapon, killerVehicleName, pos.X, pos.Y, pos.Z, killerPos.X, killerPos.Y, killerPos.Z);

                        if (User.IsSapd() || User.IsSheriff() || User.IsEms() || User.IsGov() && User.Data.rank > 5)
                        {
                            Client.Sync.Data.Set(killerId, "wanted_level", 10);
                            Client.Sync.Data.Set(killerId, "wanted_reason", "Убийство сотрудника при исполнении");

                            TriggerServerEvent("ARP:SendServerToPlayerSubTitle", "Вы в розыске", killerId);
                    
                            Notification.SendPictureToDep($"Выдал розыск {User.PlayerIdList[killerId.ToString()]}. Уровень: 10", "Диспетчер", User.Data.rp_name, "CHAR_CALL911", Notification.TypeChatbox);
                        }
                        
                        Client.Sync.Data.Set(User.GetServerId(), "deathReason", KillerWeapon);
                        _hasBeenDead = true;
                    }
                }
                else if (IsPedFatallyInjured(ped))
                {
                    _isDead = false;
                    _diedAt = -1;
                }

                if (!_hasBeenDead && _diedAt > 0)
                {
                    /*var pos = GetEntityCoords(ped, true);
                    TriggerEvent("ARP:OnPlayerWasted", pos.X, pos.Y, pos.Z);*/
                    _hasBeenDead = true;
                }
                else if (_hasBeenDead && _diedAt <= 0)
                    _hasBeenDead = false;
            }
        }

        public static int GetPlayerByEntityId(int id)
        {
            for (int i = 0; i <= 32; i++)
            {
                if (NetworkIsPlayerActive(i) && GetPlayerPed(i) == id)
                    return i;
            }
            return -1;
        }
    }
}