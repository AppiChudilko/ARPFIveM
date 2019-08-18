using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Checkpoint : BaseScript
    {
        public static List<CheckpointData> CheckpointDataList = new List<CheckpointData>();
        public static Dictionary<string, int> CheckpointEntityList = new Dictionary<string, int>();
        public static Dictionary<string, int> CheckpointWithMarkerList = new Dictionary<string, int>();
        public static int Inc = 0;
  
        public Checkpoint()
        {
            Tick += CheckpointTick;
            
            EventHandlers.Add("ARP:OnPlayerExitCheckpoint", new Action<int, string>(OnPlayerExitCheckpoint));
            EventHandlers.Add("ARP:OnPlayerEnterCheckpoint", new Action<int, string>(OnPlayerEnterCheckpoint));
        }
        
        public static void OnPlayerExitCheckpoint(int checkpointId, string name)
        {
        }
        
        public static async void OnPlayerEnterCheckpoint(int checkpointId, string name)
        {
            switch (name)
            {
                case "apartment":
                case "build":
                case "house":
                case "house:water":
                    UI.ShowToolTip("Нажмите ~INPUT_PICKUP~ чтобы открыть меню");
                    break;
                case "show:cloth":
                    UI.ShowToolTip("Нажмите ~INPUT_PICKUP~ чтобы открыть меню магазина");
                    break;
                case "pickup:teleport":
                    UI.ShowToolTip("Нажмите ~INPUT_CHARACTER_WHEEL~ чтобы воспользоваться пикапом");
                    break;
                case "pickup:teleport:menu":
                    UI.ShowToolTip("Нажмите ~INPUT_CHARACTER_WHEEL~ чтобы открыть меню");
                    break;
                case "pickup:teleport:enter":
                    UI.ShowToolTip("Нажмите ~INPUT_CHARACTER_WHEEL~ чтобы войти");
                    break;
                case "pickup:teleport:exit":
                    UI.ShowToolTip("Нажмите ~INPUT_CHARACTER_WHEEL~ чтобы выйти");
                    break;
                case "show:menu":
                    UI.ShowToolTip("Нажмите ~INPUT_PICKUP~ чтобы открыть меню");
                    break;
                case "show:dialog:menu":
                    UI.ShowToolTip("Нажмите ~INPUT_PICKUP~ чтобы открыть меню диалога");
                    break;
                case "shop:menu":
                    UI.ShowToolTip("Нажмите ~INPUT_PICKUP~ открыть меню магазина");
                    break;
                case "bar:menu":
                    UI.ShowToolTip("Нажмите ~INPUT_PICKUP~ открыть меню бара");
                    break;
                case "fuel:menu":
                    UI.ShowToolTip("Нажмите ~INPUT_PICKUP~ открыть меню заправки");
                    break;
                case "ar:info":
                    UI.ShowToolTip("Место для ремонта транспорта");
                    break;
                case "gum":
                    UI.ShowToolTip("Нажмите ~INPUT_PICKUP~ чтобы взаимодействовать");
                    break;
                case "vehicle:wash":
                    if (IsPedInAnyVehicle(PlayerPedId(), true))
                    {
                        var veh = GetVehiclePedIsUsing(PlayerPedId());
                        if (GetPedInVehicleSeat(veh, -1) == PlayerPedId())
                            Business.CarWash.Wash(new CitizenFX.Core.Vehicle(veh));
                    }
                    break;
                case "park:pay":
                    if (IsPedInAnyVehicle(PlayerPedId(), true))
                    {
                        var veh = GetVehiclePedIsUsing(PlayerPedId());
                        if (GetPedInVehicleSeat(veh, -1) == PlayerPedId())
                        {
                            UI.ShowToolTip("~b~Платная парковка~s~\nС Вас взяли плату за парковку в размере ~g~$10");
                            User.RemoveCashMoney(10);
                        }
                    }
                    break;
                case "parkno:pay":
                    if (IsPedInAnyVehicle(PlayerPedId(), true))
                    {
                        var veh = GetVehiclePedIsUsing(PlayerPedId());
                        if (GetPedInVehicleSeat(veh, -1) == PlayerPedId())
                        {
                            UI.ShowToolTip("~r~Штраф за парковку~s~\nС Вас взяли штраф за парковку в размере ~g~$100");
                            User.RemoveCashMoney(100);
                        }
                    }
                    break;
                case "taxi:finish":
                    Business.Taxi.IsTaskRoadFinish = true;
                    Notification.SendWithTime("~g~Припаркуйтесь и высадите пассажира");
                    Delete(checkpointId);
                    break;
                case "job:builder:work":
                    DeleteWithMarker(checkpointId);
                    Jobs.Builder.WorkProcess();
                    break;
                case "job:ocean:work":
                    DeleteWithMarker(checkpointId);
                    Jobs.Ocean.WorkProcessOcean();
                    break;
                /*case "job:jewelry:work":
                    DeleteWithMarker(checkpointId);
                    Jobs.Search.WorkProcessJewelry();
                    break;*/
                case "job:roadworker:work":
                    DeleteWithMarker(checkpointId);
                    Jobs.RoadWorker.WorkProcess();
                    break;
                case "job:cleaner:work":
                    DeleteWithMarker(checkpointId);
                    Jobs.Cleaner.WorkProcess();
                    break;
                case "job:bus1:work":
                case "job:bus2:work":
                case "job:bus3:work":
                    DeleteWithMarker(checkpointId);
                    Jobs.Bus.NextCheckpoint();
                    break;
                case "job:gardener:work":
                    DeleteWithMarker(checkpointId);
                    Jobs.Gardener.WorkProcess();
                    break;
                case "job:photo:work":
                    DeleteWithMarker(checkpointId);
                    Jobs.Photo.WorkProcess();
                    break;
                case "checkpoint:withdelete":
                    DeleteWithMarker(checkpointId);
                    break;
                case "job:hlab:work":
                    DeleteWithMarker(checkpointId);
                    Jobs.HumanLab.WorkProcess();
                    break;
                case "job:bugstars:work":
                    
                    if (!Client.Sync.Data.HasLocally(User.GetServerId(), "BugstarsTool"))
                    {
                        Notification.SendWithTime("~g~Возьмите инструменты в авто");
                        return;
                    }
                    
                    DeleteWithMarker(checkpointId);
                    Jobs.Bugstars.WorkProcess();
                    break;
                case "job:sunbeach:work":
                    
                    if (!Client.Sync.Data.HasLocally(User.GetServerId(), "SunBeachTool"))
                    {
                        Notification.SendWithTime("~g~Возьмите инструменты в авто");
                        return;
                    }
                    
                    DeleteWithMarker(checkpointId);
                    Jobs.SunBeach.WorkProcess();
                    break;
                case "job:waterpower:work":
                    
                    if (!Client.Sync.Data.HasLocally(User.GetServerId(), "WaterPowerTool"))
                    {
                        Notification.SendWithTime("~g~Возьмите инструменты в авто");
                        return;
                    }
                    
                    DeleteWithMarker(checkpointId);
                    Jobs.WaterPower.WorkProcess();
                    break;
                case "lsc:info:clr":
                    UI.ShowToolTip("Место перекраски ТС");
                    break;
                case "grab:sell:car":
                    UI.ShowToolTip("Нажмите ~INPUT_PICKUP~ чтобы сбыть транспорт");
                    break;
                case "grab:start":
                    UI.ShowToolTip("Нажмите ~INPUT_PICKUP~ чтобы начать грабить");
                    break;
                case "lsc:info:tun":
                    UI.ShowToolTip("Место тюнинга / починки ТС");
                    break;
                case "car:set:number":
                    UI.ShowToolTip("Место для смены номера");
                    break;
                case "cartel:car:gun:0":
                {
                    UI.ShowLoadDisplay();
                    await Delay(500);
                    
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "cartel:block");
                    DeleteWithMarker(checkpointId);

                    var v = await Vehicle.SpawnByName("Burrito3",
                        new Vector3(
                            (float) Main.CartelGetCarStockVehPos[0, 0],
                            (float) Main.CartelGetCarStockVehPos[0, 1],
                            (float) Main.CartelGetCarStockVehPos[0, 2]
                        ),
                        (float) Main.CartelGetCarStockVehPos[0, 3]
                    );
                                        
                    Inventory.AddItemServer(27, 15, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 140, -1, -1, -1);
                    Inventory.AddItemServer(153, 8, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 140, -1, -1, -1);
                    Inventory.AddItemServer(28, 8, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 120, -1, -1, -1);
                    Inventory.AddItemServer(30, 8, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 260, -1, -1, -1);
                    
                    Inventory.AddItemServer(101, 5, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(90, 2, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(71, 5, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(77, 10, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(58, 2, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(69, 2, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(64, 2, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(106, 5, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(94, 5, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                        
                    Dispatcher.SendEms("Код 2", $"Марка: ~y~Burrito3\nНомера: ~y~{GetVehicleNumberPlateText(v.Handle)}", false);
                    await Delay(500);
                    UI.HideLoadDisplay();
                    break;
                }
                case "cartel:car:gun:1":
                {
                    UI.ShowLoadDisplay();
                    await Delay(500);
                    
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "cartel:block");
                    DeleteWithMarker(checkpointId);

                    var v = await Vehicle.SpawnByName("Burrito3",
                        new Vector3(
                            (float) Main.CartelGetCarStockVehPos[1, 0],
                            (float) Main.CartelGetCarStockVehPos[1, 1],
                            (float) Main.CartelGetCarStockVehPos[1, 2]
                        ),
                        (float) Main.CartelGetCarStockVehPos[1, 3]
                    );
                    
                    Inventory.AddItemServer(27, 15, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 140, -1, -1, -1);
                    Inventory.AddItemServer(153, 8, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 140, -1, -1, -1);
                    Inventory.AddItemServer(28, 8, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 120, -1, -1, -1);
                    Inventory.AddItemServer(30, 8, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 260, -1, -1, -1);
                    
                    Inventory.AddItemServer(101, 5, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(90, 2, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(71, 5, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(77, 10, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(58, 2, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(69, 2, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(64, 2, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(106, 5, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(94, 5, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);

                    Dispatcher.SendEms("Код 2", $"Марка: ~y~Burrito3\nНомера: ~y~{GetVehicleNumberPlateText(v.Handle)}", false);    
                    await Delay(500);
                    UI.HideLoadDisplay();
                    break;
                }
                case "cartel:car:gun:2":
                {
                    UI.ShowLoadDisplay();
                    await Delay(500);
                    
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "cartel:block");
                    DeleteWithMarker(checkpointId);

                    var v = await Vehicle.SpawnByName("Burrito3",
                        new Vector3(
                            (float) Main.CartelGetCarStockVehPos[2, 0],
                            (float) Main.CartelGetCarStockVehPos[2, 1],
                            (float) Main.CartelGetCarStockVehPos[2, 2]
                        ),
                        (float) Main.CartelGetCarStockVehPos[2, 3]
                    );
                    
                    Inventory.AddItemServer(27, 15, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 140, -1, -1, -1);
                    Inventory.AddItemServer(153, 8, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 140, -1, -1, -1);
                    Inventory.AddItemServer(28, 8, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 120, -1, -1, -1);
                    Inventory.AddItemServer(30, 8, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 260, -1, -1, -1);
                    
                    Inventory.AddItemServer(101, 5, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(90, 2, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(71, 5, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(77, 10, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(58, 2, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(69, 2, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(64, 2, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(106, 5, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);
                    Inventory.AddItemServer(94, 5, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1, -1, -1, -1);

                    Dispatcher.SendEms("Код 2", $"Марка: ~y~Burrito3\nНомера: ~y~{GetVehicleNumberPlateText(v.Handle)}", false);       
                    await Delay(500);
                    UI.HideLoadDisplay();
                    break;
                }
                case "cartel:car:drug:0":
                {
                    UI.ShowLoadDisplay();
                    await Delay(500);
                    
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "cartel:block");
                    DeleteWithMarker(checkpointId);

                    var v = await Vehicle.SpawnByName("Burrito3", 
                        new Vector3(
                            (float) Main.CartelGetCarStockVehPos[0, 0], 
                            (float) Main.CartelGetCarStockVehPos[0, 1], 
                            (float) Main.CartelGetCarStockVehPos[0, 2]
                        ),
                        (float) Main.CartelGetCarStockVehPos[0, 3]
                    );
                    
                    Inventory.AddItemServer(142, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(142, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    
                    Inventory.AddItemServer(163, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(163, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    
                    Inventory.AddItemServer(165, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(165, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    
                    Inventory.AddItemServer(167, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(167, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    
                    Inventory.AddItemServer(169, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(169, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    
                    Inventory.AddItemServer(170, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(170, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    
                    Dispatcher.SendEms("Код 2", $"Марка: ~y~Burrito3\nНомера: ~y~{GetVehicleNumberPlateText(v.Handle)}", false);    
                    await Delay(500);
                    UI.HideLoadDisplay();
                    break;
                }
                case "cartel:car:drug:1":
                {
                    UI.ShowLoadDisplay();
                    await Delay(500);
                    
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "cartel:block");
                    DeleteWithMarker(checkpointId);

                    var v = await Vehicle.SpawnByName("Burrito3", 
                        new Vector3(
                            (float) Main.CartelGetCarStockVehPos[1, 0], 
                            (float) Main.CartelGetCarStockVehPos[1, 1], 
                            (float) Main.CartelGetCarStockVehPos[1, 2]
                        ),
                        (float) Main.CartelGetCarStockVehPos[1, 3]
                    );
                    
                    Inventory.AddItemServer(142, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(142, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    
                    Inventory.AddItemServer(163, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(163, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    
                    Inventory.AddItemServer(165, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(165, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    
                    Inventory.AddItemServer(167, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(167, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    
                    Inventory.AddItemServer(169, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(169, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    
                    Inventory.AddItemServer(170, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(170, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    
                    Dispatcher.SendEms("Код 2", $"Марка: ~y~Burrito3\nНомера: ~y~{GetVehicleNumberPlateText(v.Handle)}", false);   
                    await Delay(500);
                    UI.HideLoadDisplay();
                    break;
                }
                case "cartel:car:drug:2":
                {
                    UI.ShowLoadDisplay();
                    await Delay(500);
                    
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "cartel:block");
                    DeleteWithMarker(checkpointId);

                    var v = await Vehicle.SpawnByName("Burrito3", 
                        new Vector3(
                            (float) Main.CartelGetCarStockVehPos[2, 0], 
                            (float) Main.CartelGetCarStockVehPos[2, 1], 
                            (float) Main.CartelGetCarStockVehPos[2, 2]
                        ),
                        (float) Main.CartelGetCarStockVehPos[2, 3]
                    );
                    
                    Inventory.AddItemServer(142, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(142, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    
                    Inventory.AddItemServer(163, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(163, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    
                    Inventory.AddItemServer(165, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(165, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    
                    Inventory.AddItemServer(167, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(167, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    
                    Inventory.AddItemServer(169, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(169, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    
                    Inventory.AddItemServer(170, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                    Inventory.AddItemServer(170, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 1000, -1, -1, -1);
                        
                    Dispatcher.SendEms("Код 2", $"Марка: ~y~Burrito3\nНомера: ~y~{GetVehicleNumberPlateText(v.Handle)}", false);  
                    await Delay(500);
                    UI.HideLoadDisplay();
                    break;
                }
                case "cartel:car:drugm:0":
                {
                    UI.ShowLoadDisplay();
                    await Delay(500);
                    
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "cartel:block");
                    DeleteWithMarker(checkpointId);

                    var v = await Vehicle.SpawnByName("Burrito3", 
                        new Vector3(
                            (float) Main.CartelGetCarStockVehPos[0, 0], 
                            (float) Main.CartelGetCarStockVehPos[0, 1], 
                            (float) Main.CartelGetCarStockVehPos[0, 2]
                        ),
                        (float) Main.CartelGetCarStockVehPos[0, 3]
                    );
                    
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(143, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 200, -1, -1, -1);
                    
                    Dispatcher.SendEms("Код 2", $"Марка: ~y~Burrito3\nНомера: ~y~{GetVehicleNumberPlateText(v.Handle)}", false);   
                    await Delay(500);
                    UI.HideLoadDisplay();
                    break;
                }
                case "cartel:car:drugm:1":
                {
                    UI.ShowLoadDisplay();
                    await Delay(500);
                    
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "cartel:block");
                    DeleteWithMarker(checkpointId);

                    var v = await Vehicle.SpawnByName("Burrito3", 
                        new Vector3(
                            (float) Main.CartelGetCarStockVehPos[1, 0], 
                            (float) Main.CartelGetCarStockVehPos[1, 1], 
                            (float) Main.CartelGetCarStockVehPos[1, 2]
                        ),
                        (float) Main.CartelGetCarStockVehPos[1, 3]
                    );
                    
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(143, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 200, -1, -1, -1);
                    
                    Dispatcher.SendEms("Код 2", $"Марка: ~y~Burrito3\nНомера: ~y~{GetVehicleNumberPlateText(v.Handle)}", false);   
                    await Delay(500);
                    UI.HideLoadDisplay();
                    break;
                }
                case "cartel:car:drugm:2":
                {
                    UI.ShowLoadDisplay();
                    await Delay(500);
                    
                    Client.Sync.Data.ResetLocally(User.GetServerId(), "cartel:block");
                    DeleteWithMarker(checkpointId);

                    var v = await Vehicle.SpawnByName("Burrito3", 
                        new Vector3(
                            (float) Main.CartelGetCarStockVehPos[2, 0], 
                            (float) Main.CartelGetCarStockVehPos[2, 1], 
                            (float) Main.CartelGetCarStockVehPos[2, 2]
                        ),
                        (float) Main.CartelGetCarStockVehPos[2, 3]
                    );
                    
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(145, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 800, -1, -1, -1);
                    Inventory.AddItemServer(143, 1, InventoryTypes.Vehicle, Inventory.ConvertNumberToHash(Vehicle.GetVehicleNumber(v.Handle)), 200, -1, -1, -1);
                    
                    Dispatcher.SendEms("Код 2", $"Марка: ~y~Burrito3\nНомера: ~y~{GetVehicleNumberPlateText(v.Handle)}", false);  
                    await Delay(500);
                    UI.HideLoadDisplay();
                    break;
                }
            }
        }
        
        public static int Create(Vector3 pos, float range, string name = "")
        {
            Inc++;

            CheckpointDataList.Add(
                new CheckpointData
                {
                    Id = Inc,
                    X = pos.X,
                    Y = pos.Y,
                    Z = pos.Z,
                    Name = name,
                    Range = range,
                    Visible = true
                }
            );
            return Inc;
        }
        
        public static bool Delete(int checkpointId)
        {
            foreach (var item in CheckpointDataList)
            {
                if (item.Id != checkpointId) continue;
                CheckpointDataList[CheckpointDataList.IndexOf(item)].Visible = false;
                return true;
            }
            return false;
            //return (from item in CheckpointDataList where item.Id == checkpointId select CheckpointDataList.Remove(item)).FirstOrDefault();
        }
        
        public static int CreateWithMarker(Vector3 pos, float range, float height, int r, int g, int b, int a, string name = "")
        {
            var checkpointId = Create(pos, range + 0.4f, name);
            var markerId = Marker.Create(pos, range, height, r, g, b, a);
            
            CheckpointWithMarkerList.Add(checkpointId.ToString(), markerId);
            
            return checkpointId;
        }
        
        public static bool DeleteWithMarker(int checkpointId)
        {
            if (!CheckpointWithMarkerList.ContainsKey(checkpointId.ToString())) return false;
            Marker.Delete(CheckpointWithMarkerList[checkpointId.ToString()]);   
            CheckpointWithMarkerList.Remove(checkpointId.ToString());
            return Delete(checkpointId);
        }
        
        public static bool DeleteWithMarker(string checkpointName)
        {
            return (from item in CheckpointDataList where checkpointName == item.Name select DeleteWithMarker(item.Id)).FirstOrDefault();
        }
        
        public static Vector3 GetPosition(int checkpointId)
        {
            foreach (var item in CheckpointDataList)
            {
                if (item.Id == checkpointId)
                    return new Vector3(item.X, item.Y, item.Z);
            }
            return new Vector3();
        }
        
        public static float DistanceTo(int checkpointId, Vector3 pos)
        {
            return Main.GetDistanceToSquared(GetPosition(checkpointId), pos);
        }
        
        private static async Task CheckpointTick()
        {
            //var entity = GetPlayerPed(-1);
            
            /*if (IsPedInAnyVehicle(PlayerPedId(), true))
                entity = GetVehiclePedIsUsing(PlayerPedId());*/

            await Delay(500);
            
            var entity = GetPlayerPed(-1);
            var pos = GetEntityCoords(entity, true);

            foreach (var item in CheckpointDataList)
            {
                if (!item.Visible) continue;
                if (Main.GetDistanceToSquared(pos, new Vector3(item.X, item.Y, item.Z)) > item.Range)
                {
                    if (!CheckpointEntityList.ContainsKey(item.Id.ToString())) continue;
                    CheckpointEntityList.Remove(item.Id.ToString());
                    TriggerEvent("ARP:OnPlayerExitCheckpoint", item.Id, item.Name);
                }
                else
                {
                    if (CheckpointEntityList.ContainsKey(item.Id.ToString())) continue;
                    CheckpointEntityList.Add(item.Id.ToString(), entity);
                    TriggerEvent("ARP:OnPlayerEnterCheckpoint", item.Id, item.Name);
                }
            }
        }
    }
}

public class CheckpointData
{
    public int Id { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public float Range { get; set; }
    public string Name { get; set; }
    public bool Visible { get; set; }
}