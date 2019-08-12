using System;
using System.Linq;
using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Business
{
    public class Fuel : BaseScript
    {
        public static void LoadAll()
        {
            for (int i = 0; i < Main.FuelMarkers.Length / 3; i++)
            {
                int fuelStationId = Convert.ToInt32(Main.FuelStation[i, 4]);

                Vector3 fuelPos = new Vector3((float) Main.FuelMarkers[fuelStationId, 0], (float) Main.FuelMarkers[fuelStationId, 1], (float) Main.FuelMarkers[fuelStationId, 2] - 1);
                Vector3 fuelShopPos = new Vector3((float) Main.FuelStation[i, 0], (float) Main.FuelStation[i, 1], (float) Main.FuelStation[i, 2]);

                var blip = World.CreateBlip(fuelPos);
                blip.Sprite = (BlipSprite) 415;
                blip.Name = Lang.GetTextToPlayer("_lang_58");
                blip.IsShortRange = true;
                blip.Scale = 0.8f;

                if ((int) Main.FuelStation[i, 3] == 1)
                {
                    blip = World.CreateBlip(fuelShopPos);
                    blip.Sprite = (BlipSprite) 52;
                    blip.Name = Lang.GetTextToPlayer("_lang_59");
                    blip.IsShortRange = true;
                    blip.Scale = 0.8f;
                }
                
                Managers.Checkpoint.Create(fuelShopPos, 1.4f, "fuel:menu");
                Managers.Marker.Create(fuelShopPos - new Vector3(0, 0, 1), 1f, 1f, Managers.Marker.Blue.R, Managers.Marker.Blue.G, Managers.Marker.Blue.B, Managers.Marker.Blue.A);
            }
        }
        
        public static async void Buy(string item, int price, int fuelId)
        {
            await User.GetAllData();
            
            if (User.GetMoneyWithoutSync() < price)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                return;
            }

            switch (item)
            {
                case "fuel_item":
                    int amount = await Managers.Inventory.GetInvAmount(User.Data.id, InventoryTypes.Player);

                    if (amount + Inventory.GetItemAmountById(9) > User.Amount)
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_60"));
                        return;
                    }

                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_61"));
                    Managers.Inventory.AddItemServer(9, 1, InventoryTypes.Player, User.Data.id, 1, -1, -1, -1);

                    User.RemoveMoney(price);
                    Business.AddMoney(fuelId, price);
                    Managers.Inventory.UpdateAmount(User.Data.id, InventoryTypes.Player);
                    break;
            }
        }
        
        public static async void FuelVeh(string number, int price, int fuelId, int index)
        {
            await User.GetAllData();
            
            if (User.GetMoneyWithoutSync() < price)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                return;
            }

            var vehId = Managers.Vehicle.GetVehicleIdByNumber(number);

            if (vehId == -1 || !Managers.Vehicle.HasVehicleId(vehId))
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_62"));
                return;
            }
            
            if (Convert.ToInt32(Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Fuel) == Managers.Vehicle.VehicleInfoGlobalDataList[vehId].FullFuel)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_62"));
                return;
            }

            switch (index)
            {
                case 0:

                    if (Managers.Vehicle.VehicleInfoGlobalDataList[vehId].FullFuel < Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Fuel + 1)
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_62"));
                        break;
                    }

                    if (User.GetMoneyWithoutSync() < price)
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_25"));
                        break;
                    }

                    Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Fuel += 1;
                    Sync.Data.Set(vehId + 110000, "Fuel", Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Fuel);
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_63", price));
                    
                    User.RemoveMoney(price);
                    Business.AddMoney(fuelId, price);
                    break;
                case 1:
                    if (Managers.Vehicle.VehicleInfoGlobalDataList[vehId].FullFuel < Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Fuel + 5)
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_62"));
                        break;
                    }

                    if (User.GetMoneyWithoutSync() < 5 * price)
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_25"));
                        break;
                    }
                    
                    Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Fuel += 5;
                    Sync.Data.Set(vehId + 110000, "Fuel", Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Fuel);
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_63", price * 5));
                    
                    User.RemoveMoney(5 * price);
                    Business.AddMoney(fuelId, 5 * price);
                    break;
                case 2:
                    if (Managers.Vehicle.VehicleInfoGlobalDataList[vehId].FullFuel < Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Fuel + 10)
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_62"));
                        break;
                    }

                    if (User.GetMoneyWithoutSync() < 10 * price)
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_25"));
                        break;
                    }
                    
                    Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Fuel += 10;
                    Sync.Data.Set(vehId + 110000, "Fuel", Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Fuel);
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_63", price * 10));

                    User.RemoveMoney(10 * price);
                    Business.AddMoney(fuelId, 10 * price);
                    break;
                case 3:

                    int money = Convert.ToInt32(Managers.Vehicle.VehicleInfoGlobalDataList[vehId].FullFuel - Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Fuel);

                    if (User.GetMoneyWithoutSync() < money * price)
                    {
                        Notification.SendWithTime(Lang.GetTextToPlayer("_lang_25"));
                        break;
                    }
                    
                    Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Fuel = Managers.Vehicle.VehicleInfoGlobalDataList[vehId].FullFuel;
                    Sync.Data.Set(vehId + 110000, "Fuel", Managers.Vehicle.VehicleInfoGlobalDataList[vehId].Fuel);
                    Notification.SendWithTime(Lang.GetTextToPlayer("_lang_63", price * money));

                    User.RemoveMoney(money * price);
                    Business.AddMoney(fuelId, money * price);
                    break;
            }
        }

        public static void CheckPosForOpenMenu()
        {
            var playerPos = GetEntityCoords(GetPlayerPed(-1), true);
            int fuelId = GetFuelIdInRadius(playerPos, 2f);
            if (fuelId == -1) return;
            
            for (int i = 0; i < Main.FuelMarkers.Length / 3; i++)
            {
                if (!(Main.GetDistanceToSquared(playerPos,
                          new Vector3((float) Main.FuelStation[i, 0], (float) Main.FuelStation[i, 1],
                              (float) Main.FuelStation[i, 2])) < 2f)) continue;
                
                var vehList = Main.GetVehicleListOnRadius(
                    new Vector3((float) Main.FuelMarkers[i, 0], (float) Main.FuelMarkers[i, 1],
                        (float) Main.FuelMarkers[i, 2]), Convert.ToSingle(Main.FuelStation[i, 5]));

                MenuList.ShowFuelMenu(fuelId, Convert.ToInt32(Main.FuelStation[i, 3]), vehList.Select(vehItem => Managers.Vehicle.GetVehicleNumber(vehItem.Handle)).ToList());
                return;
            }
        }

        public static int GetFuelIdInRadius(Vector3 pos, float radius)
        {
            for (int i = 0; i < Main.FuelMarkers.Length / 3; i++)
            {
                if (Main.GetDistanceToSquared(pos, new Vector3((float) Main.FuelStation[i, 0], (float) Main.FuelStation[i, 1], (float) Main.FuelStation[i, 2])) < radius)
                    return Convert.ToInt32(Main.FuelStation[i, 6]);
            }
            return -1;
        }

        public static Vector3 FindNearest(Vector3 pos)
        {
            var fuelPosPrew = new Vector3((float) Main.FuelStation[0, 0], (float) Main.FuelStation[0, 1], (float) Main.FuelStation[0, 2]);
            for (int i = 0; i < Main.FuelMarkers.Length / 3; i++)
            {
                var fuelPos = new Vector3((float) Main.FuelStation[i, 0], (float) Main.FuelStation[i, 1], (float) Main.FuelStation[i, 2]);
                if(Main.GetDistanceToSquared(fuelPos, pos) < Main.GetDistanceToSquared(fuelPosPrew, pos))
                    fuelPosPrew = fuelPos;
            }
            return fuelPosPrew;
        }
    }
}