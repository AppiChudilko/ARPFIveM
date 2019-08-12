using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Fractions
{
    public class Ems : BaseScript
    {       
        public static async void Buy(int item, int price, int count = 1)
        {
            await User.GetAllData();
            
            if (User.GetMoneyWithoutSync() < price * count)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_15"));
                return;
            }

            int plId = User.GetServerId();

            int amount = await Managers.Inventory.GetInvAmount(User.Data.id, InventoryTypes.Player);

            if (amount + Inventory.GetItemAmountById(item) > User.Amount)
            {
                Notification.SendWithTime(Lang.GetTextToPlayer("_lang_60"));
                return;
            }
            Managers.Inventory.AddItemServer(item, count, InventoryTypes.Player, User.Data.id, 10, -1, -1, -1);
                    
            Notification.SendWithTime(Lang.GetTextToPlayer("_lang_84", Inventory.GetItemNameById(item), count));
            User.RemoveMoney(price * count);
            Coffer.AddMoney(price * count);

            if (item == 155)
            {
                User.Data.allow_marg = false;
                Sync.Data.Set(User.GetServerId(), "allow_marg", false);
                Notification.SendWithTime("~b~Рецепт на лечебную марихуану был изъят");
            }
                    
            Managers.Inventory.UpdateAmount(User.Data.id, InventoryTypes.Player);
        }
        
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
                    if (User.Skin.SEX == 1) {
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 109, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 99, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 72, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 97, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 159, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 66, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 258, 0, 2);
                        ClearPedProp(GetPlayerPed(-1), 0);
                    }
                    else {
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 85, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 96, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 51, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 127, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 129, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 58, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 250, 0, 2);
                        ClearPedProp(GetPlayerPed(-1), 0);
                    }
                    break;
                case 2:
                    if (User.Skin.SEX == 1) {
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 109, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 99, 1, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 72, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 97, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 159, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 66, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 258, 1, 2);
                        ClearPedProp(GetPlayerPed(-1), 0);
                    }
                    else {
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 85, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 96,  1, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 51, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 127, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 129, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 58, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 250,  1, 2);
                        ClearPedProp(GetPlayerPed(-1), 0);
                    }
                    break;
            }
        }  
        
        public static void GarderobFire(int idx)
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
                    ClearPedProp(GetPlayerPed(-1), 0);
                    
                    Characher.UpdateCloth(false);
                    Characher.UpdateFace(false);
                    break;
                case 1:
                    if (User.Skin.SEX == 0) {
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 17, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 93, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 52, 1, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 0, 240, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 231, 0, 2);
                        
                        SetPedPropIndex(GetPlayerPed(-1), 0, 126, 0, true);
                    }
                    break;
                case 2:
                    if (User.Skin.SEX == 0) {
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 38, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 17, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 97, 17, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 52, 1, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 0, 240, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 251, 17, 2);
                        
                        SetPedPropIndex(GetPlayerPed(-1), 0, 126, 0, true);
                    }
                    break;
            }
        }
    }
}