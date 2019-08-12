using CitizenFX.Core;
using Client.Managers;
using static CitizenFX.Core.Native.API;

namespace Client.Fractions
{
    public class Fib : BaseScript
    {
        

        public static void Garderob(int idx)
        {
            switch (idx)
            {
                case 0:
                    /*API.ClearPlayerAccessory(sender, 0);
                    User.ResetPlayerPed(sender);
                    User.ResetPlayerCloth(sender);*/
                    
                    Characher.UpdateCloth(false);
                    Characher.UpdateFace(false);
                    SetPedComponentVariation(GetPlayerPed(-1), 1, 0, 0, 2);
                    SetPedComponentVariation(GetPlayerPed(-1), 9, 0, 0, 2);
                    
                    Sync.Data.ResetLocally(User.GetServerId(), "hasMask");
                    
                    ClearPedProp(GetPlayerPed(-1), 0);
                    break;
                case 1://51
                    
                    SetPedHeadBlendData(
                        GetPlayerPed(-1),
                        0,    
                        0,    
                        0,    
                        0,    
                        0,    
                        0,    
                        0,    
                        0,    
                        0,    
                        false    
                    );
                    
                    Sync.Data.SetLocally(User.GetServerId(), "hasMask", true);
                    
                    if (User.Skin.SEX == 1) {
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 52, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 18, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 32, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 25, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 35, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 3, 1, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 44, 3, 2);
                        
                        SetPedPropIndex(GetPlayerPed(-1), 0, 116, 0, true);
                    }
                    else {
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 52, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 17, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 33, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 25, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 57, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 4, 1, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 139, 3, 2);
                        
                        SetPedPropIndex(GetPlayerPed(-1), 0, 117, 0, true);
                    }
                    break;
                case 2://51
                    
                    SetPedHeadBlendData(
                        GetPlayerPed(-1),
                        0,    
                        0,    
                        0,    
                        0,    
                        0,    
                        0,    
                        0,    
                        0,    
                        0,    
                        false    
                    );
                    
                    Sync.Data.SetLocally(User.GetServerId(), "hasMask", true);
                    
                    if (User.Skin.SEX == 1) {
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 52, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 18, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 90, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 25, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 35, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 17, 1, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 218, 4, 2);
                        
                        SetPedPropIndex(GetPlayerPed(-1), 0, 116, 0, true);
                    }
                    else {
                        SetPedComponentVariation(GetPlayerPed(-1), 1, 52, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 3, 17, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 4, 87, 4, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 5, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 6, 25, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 7, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 8, 57, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 9, 15, 1, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 10, 0, 0, 2);
                        SetPedComponentVariation(GetPlayerPed(-1), 11, 220, 4, 2);
                        
                        SetPedPropIndex(GetPlayerPed(-1), 0, 117, 0, true);
                    }
                    break;
            }
        }
    }
}