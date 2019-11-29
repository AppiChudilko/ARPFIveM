using System;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Client.Vehicle;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class BankGrab : BaseScript
    {
        public static Vector3 Start = new Vector3(1158.67f, -463.7488f, 66.55032f);
        public static float Rotation = 344.8745f;
        public static Vector3 Finish = new Vector3(-640.3211f, 4000.528f, 123.6948f);
        public static bool IsLeaderHere = false;
        public static Vector3 Terminal = new Vector3(253.3081f, 228.4226f, 101.6833f);
        public static bool CodeEnteredCorrect = false;
        public static bool CodeNeeded = true;
        public static int scaleform = 0;
        public static bool ClickReturn = false;
        public static int lives = 3;

        public static readonly List<string> gamePasswordList = new List<string>()
        {
            "BLACKOUT",
            "HACKTHIS",
            "BACKDOOR",
            "UPDATEME",
            "ALAMOTOP",
            "HONGKONG",
            "CONNECTED",
            "XDMAN15",
            "DIAMOND1",
            "UMBRELLA",
            "VAULTHCK",
        };

        public static Vector3 VaultPos = new Vector3(0, 0 ,0);
        //--Список для экипировки
        //Бронежилет легкий/средний
        //Сумка
        //Штаны как у ПД но без карманов
        //Берцы
        //Тактическая куртка
        //Оружие MP5 или MP5K, пистолет мк2
        //Машина без номеров
        
        //--Список для инвентаря
        //Бур/дрель
        //Пароли или еще какая-гибудь херь для взлома
        //Стяжки/Веревки/наручники
        
        //--Побочки (квест линия)
        //Показать что ты можешь (Ограбить 5 магазинов за ночь) не быть пойманым
        //Съездить за оборудованием(купить за +-50.000)
        //Съездить за эккипировкой(15к на человека)
        //Добыть машину +Пара часов на работу с ней (машину угнать с базы мерриуэзер Rumpo3)
        //Куш с ограбления 200.000
        
        //Условия
        //На сервере минимум 5 копов
        //Копам приходят уведомления касательно всех действий(покупка оружия, экипировки и т.д.)\
        //Копам премии за сорванное ограбление
        //


        public static async void VaultOpen()
        {

            uint thermite = (uint) GetHashKey("hei_prop_heist_thermite_flash");
            uint bag_01 = (uint) GetHashKey("p_ld_heist_bag_01");
            uint bag_s = (uint) GetHashKey("p_ld_heist_bag_s");
            uint card1 = (uint) GetHashKey("hei_prop_heist_card_hack");
            uint card2 = (uint) GetHashKey("hei_prop_heist_card_hack_02");
            uint drill = (uint) GetHashKey("hei_prop_heist_drill");
            uint cash_pile = (uint) GetHashKey("hei_prop_heist_cash_pile");
            uint laptop = (uint) GetHashKey("gr_prop_gr_laptop_01a");
            string hackdict = "anim@heists@ornate_bank@hack";
            
            

            RequestModel(thermite);
            while(!HasModelLoaded(thermite))
                await Delay(1);
            
            RequestModel(bag_01);
            while(!HasModelLoaded(bag_01))
                await Delay(1);
            
            RequestModel(bag_s);
            while(!HasModelLoaded(bag_s))
                await Delay(1);

            RequestModel(card1);
            while(!HasModelLoaded(card1))
                await Delay(1);
            
            RequestModel(card2);
            while(!HasModelLoaded(card2))
                await Delay(1);
            
            RequestModel(drill);
            while(!HasModelLoaded(drill))
                await Delay(1);
            
            RequestModel(cash_pile);
            while(!HasModelLoaded(cash_pile))
                await Delay(1);
            
            RequestModel(laptop);
            while(!HasModelLoaded(laptop))
                await Delay(1);
            
            
            RequestAnimDict("anim@heists@ornate_bank@hack_heels");
            while(!HasAnimDictLoaded("anim@heists@ornate_bank@hack_heels"))
                await Delay(1);

            RequestAnimDict(hackdict);
            while (!HasAnimDictLoaded(hackdict))
                await Delay(1);

            var pp = GetPlayerPed(-1);

            TaskPlayAnim(pp, hackdict, "hack_enter", 8.0001f, -8.0001f, -1, 8, 0, false, false,
                false);
            await Delay((int) GetAnimDuration(hackdict, "hack_enter") * 1000);
            
            TaskPlayAnim(pp, "anim@heists@ornate_bank@hack", "hack_loop", 8.0001f, -8.0001f, -1, 9, 0, false, false,
                false);
            await Delay(3000);
            
            
            await Delay(2500);
            var VaultDoor = GetClosestObjectOfType(254.135f, 225.165f, 101.876f, 10.0f, 961976194, false, false, false);
            var closedRot = GetEntityRotationVelocity(VaultDoor).Z;
            float heading = GetEntityHeading(VaultDoor);
            Debug.WriteLine($"heading = {heading}");
            if (VaultDoor != 0)
            {
                while (heading > 0f)
                {
                    await Delay(5);
                    Debug.WriteLine($"heading = {heading}");
                    SetEntityHeading(VaultDoor, heading);
                    heading = heading - 0.5f;
                }
                FreezeEntityPosition(VaultDoor, true);
            }
        }
        
        public static async void AnimationTest()
        {
            User.PlayAnimation("anim@heists@fleeca_bank@drilling", "drill_straight_idle", 9);
            await Delay(3000);
            TaskStartScenarioInPlace(PlayerPedId(), "CODE_HUMAN_MEDIC_KNEEL", 0, true);
            
        }

        public static void DestroySoundAlarm()
        {
            
        }

        public static void TestRun()
        {

            var veh = CreateVehicle( (uint) VehicleHash.Landstalker, Start.X, Start.Y, Start.Z, Rotation, true, true);
            var driver = CreatePedInsideVehicle(veh, 1, (uint) PedHash.Popov, -1, true, true);

            TaskVehicleDriveToCoordLongrange(driver, veh, Finish.X, Finish.Y, World.GetGroundHeight(World.WaypointPosition), 60f, 786603, 20.0f);

        }

        public static async void MissionOneEquip(CitizenFX.Core.PlayerList players)
        {
            int pay = 15000 * players.Count();
            if (players.Count() < 5)
            {
                Chat.SendChatMessage("Незнакомец", "Вас слишком мало, приведи минимум 4 парней");
                await Delay(2500);
                Chat.SendChatMessage("Незнакомец", "Я не продам снаряжение диллетантам, которые могут меня выдать");
                return;
            }

            foreach(CitizenFX.Core.Player p in players)
            {
                
                if (User.IsLeader2() || User.IsSubLeader2())
                {
                    
                    IsLeaderHere = true;
                    if (User.Data.money < pay)
                    {
                        await Delay(1000);
                        await Delay(2000);
                        Chat.SendChatMessage("Незнакомец", "Кинуть меня решил? Валите его! И компанию его тоже.");
                        PedAttack(players);
                        return;
                    }
                    Chat.SendChatMessage("Незнакомец", "Приятно иметь с вами дело.");
                    User.RemoveMoney(pay);
                }

                if (IsLeaderHere)
                {
                    
                }

            }
        }

        public static void Phone()
        {
            
        }

        public static void PedAttack(CitizenFX.Core.PlayerList players)
        {
            
            //boss
            //security Milton
            //security2 VagosSpeak
            //security3 WeiCheng
            //security4 AntonB
            //security5 MovPrem01SMM
            //security6 VagosFun01
            //driver Popov
            //driver2 Stbla02AMY
            var npc1 = 1; //CreatePed(int pedType, uint modelHash, float x, float y, float z, float heading, bool isNetwork, bool thisScriptCheck);;
            var npc2 = World.CreatePed(PedHash.Bankman, new Vector3(69f, 69f, 69f));
            var npc3 = 1;
            var npc4 = 1;
            var npc5 = 1;
            var npc6 = 1;
            var npc7 = 1;
            var npc8 = 1;
            foreach (var p in players)
            {
                TaskCombatPed(npc1, GetPlayerPed(-1), 0, 16);
                //TaskCombatPed(npc2, GetPlayerPed(-1), 0, 16);
                TaskCombatPed(npc3, GetPlayerPed(-1), 0, 16);
                TaskCombatPed(npc4, GetPlayerPed(-1), 0, 16);
                TaskCombatPed(npc5, GetPlayerPed(-1), 0, 16);
                TaskCombatPed(npc6, GetPlayerPed(-1), 0, 16);
                TaskCombatPed(npc7, GetPlayerPed(-1), 0, 16);
                TaskCombatPed(npc8, GetPlayerPed(-1), 0, 16);
            }
        }

        public static async void SpawnPed()
        {
            CitizenFX.Core.Ped ped1 = await World.CreatePed(PedHash.Popov, GetEntityCoords(GetPlayerPed(-1), true));
            CitizenFX.Core.Ped ped2 = await World.CreatePed(PedHash.Milton, GetEntityCoords(GetPlayerPed(-1), true));
            CitizenFX.Core.Ped ped3 = await World.CreatePed(PedHash.VagosSpeak, GetEntityCoords(GetPlayerPed(-1), true));
            CitizenFX.Core.Ped ped4 = await World.CreatePed(PedHash.WeiCheng, GetEntityCoords(GetPlayerPed(-1), true));
            CitizenFX.Core.Ped ped5 = await World.CreatePed(PedHash.Antonb, GetEntityCoords(GetPlayerPed(-1), true));
            CitizenFX.Core.Ped ped6 = await World.CreatePed(PedHash.Movprem01SMM, GetEntityCoords(GetPlayerPed(-1), true));

            var pedId1 = NetToPed(ped1.NetworkId);
            var pedId2 = NetToPed(ped2.NetworkId);
            var pedId3 = NetToPed(ped3.NetworkId);
            var pedId4 = NetToPed(ped4.NetworkId);
            var pedId5 = NetToPed(ped5.NetworkId);
            var pedId6 = NetToPed(ped6.NetworkId);
            
            //ped1.PedGroup.Add(ped1, true);
            //ped1.PedGroup.Add(ped2, false);
            //ped1.PedGroup.Add(ped3, false);
            //ped1.PedGroup.Add(ped4, false);
            //ped1.PedGroup.Add(ped5, false);
            //ped1.PedGroup.Add(ped6, false);
            
            GiveWeaponToPed(pedId1, (uint) WeaponHash.SMG, 180, false, true);
            SetPedCombatAbility(pedId1, 100);
            
            GiveWeaponToPed(pedId2, (uint) WeaponHash.SMG, 180, false, true);
            SetPedCombatAbility(pedId2, 100);
            
            GiveWeaponToPed(pedId3, (uint) WeaponHash.SMG, 180, false, true);
            SetPedCombatAbility(pedId3, 100);
            
            GiveWeaponToPed(pedId4, (uint) WeaponHash.SMG, 180, false, true);
            SetPedCombatAbility(pedId4, 100);
            
            GiveWeaponToPed(pedId5, (uint) WeaponHash.SMG, 180, false, true);
            SetPedCombatAbility(pedId5, 100);
            
            GiveWeaponToPed(pedId6, (uint) WeaponHash.SMG, 180, false, true);
            SetPedCombatAbility(pedId6, 100);

        }
    }
}