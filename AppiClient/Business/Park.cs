using CitizenFX.Core;

namespace Client.Business
{
    public class Park : BaseScript
    {   
        public static double[,] ParkMarkers =
        {
            { 191.2417, -1017.372, 28.91138, 4 },
            { 185.1676, -1014.91, 28.90265, 4 },
            { 179.7389, -1013.322, 28.91763, 4 },
            { 174.4107, -1011.498, 28.92295, 4 },
            { 152.9322, -1005.012, 28.91087, 4 },
        };
        
        public static double[,] ParkNoMarkers =
        {
            { 168.9033, -1010.501, 28.89563, 4 }
        };
        
        public static void LoadAll()
        {
            for (int i = 0; i < ParkMarkers.Length / 4; i++)
            {
                Managers.Checkpoint.Create(new Vector3((float) ParkMarkers[i, 0], (float) ParkMarkers[i, 1], (float) ParkMarkers[i, 2]), (float) ParkMarkers[i, 3], "park:pay");
            }
            for (int i = 0; i < ParkNoMarkers.Length / 4; i++)
            {
                Managers.Checkpoint.Create(new Vector3((float) ParkNoMarkers[i, 0], (float) ParkNoMarkers[i, 1], (float) ParkNoMarkers[i, 2]), (float) ParkNoMarkers[i, 3], "parkno:pay");
            }
        }
    }
}