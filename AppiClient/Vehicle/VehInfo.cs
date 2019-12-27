using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Client.Vehicle
{
    public class VehInfo : BaseScript
    {
        public static string GetDisplayName(int vehicleHash)
        {
            return Get(vehicleHash).DisplayName;
        }
        
        public static string GetClassName(int vehicleHash)
        {
            return Get(vehicleHash).ClassName;
        }
        
        /*public void SetData(NetHandle veh, int vehicleHash, bool isBroke = false)
        {
            var info = Get(vehicleHash);
            
            API.Shared.SetEntityData(veh, "FUEL_MINUTE", info.FuelMinute);
            API.Shared.SetEntityData(veh, "FULL_FUEL", info.FullFuel);
            API.Shared.SetEntityData(veh, "LOAD", info.Stock);
            API.Shared.SetEntityData(veh, "LOAD_COUNT", info.StockFull);
            API.Shared.SetEntityData(veh, "STOCK", info.StockItem);

            if (isBroke)
            {
                API.Shared.SetEntityData(veh, "S_MP", 4);
                API.Shared.SetEntityData(veh, "S_WH_BK_L", 4);
                API.Shared.SetEntityData(veh, "S_WH_B_L", 4);
                API.Shared.SetEntityData(veh, "S_WH_BK_R", 4);
                API.Shared.SetEntityData(veh, "S_WH_B_R", 4);
                API.Shared.SetEntityData(veh, "S_ENGINE", 4);
                API.Shared.SetEntityData(veh, "S_SUSPENSION", 4);
                API.Shared.SetEntityData(veh, "S_BODY", 4);
                API.Shared.SetEntityData(veh, "S_CANDLE", 40545);
                API.Shared.SetEntityData(veh, "S_OIL", 12520);
            }
            else
            {
                API.Shared.SetEntityData(veh, "S_MP", info.SMp);
                API.Shared.SetEntityData(veh, "S_WH_BK_L", info.SWhBkl);
                API.Shared.SetEntityData(veh, "S_WH_B_L", info.SWhBl);
                API.Shared.SetEntityData(veh, "S_WH_BK_R", info.SWhBkr);
                API.Shared.SetEntityData(veh, "S_WH_B_R", info.SWhBr);
                API.Shared.SetEntityData(veh, "S_ENGINE", info.SEngine);
                API.Shared.SetEntityData(veh, "S_SUSPENSION", info.SSuspension);
                API.Shared.SetEntityData(veh, "S_BODY", info.SBody);
                API.Shared.SetEntityData(veh, "S_CANDLE", info.SCandle);
                API.Shared.SetEntityData(veh, "S_OIL", info.SOil);
            }
            
            API.Shared.SetEntityData(veh, "CAN_LOAD", (info.StockFull != 0));
            
            if (API.Shared.GetEntityData(veh, "ID_USER") == null)
            {
                API.Shared.SetEntityData(veh, "FUEL", info.Fuel);
            }
        }*/
        
        public static VehicleInfoData Get(int vehicleHash)
        {
            switch (vehicleHash)
            {
                case -344943009:
                    return new VehicleInfoData {
                        DisplayName = "Blista",
                        ClassName = "Compacts", 
                        IsBroke = false, 
                        Stock = 420000, 
                        StockFull = 110000, 
                        StockItem = 0,
                        FullFuel = 50, 
                        FuelMinute = 4, 
                        Fuel = 50, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1039032026:
                    return new VehicleInfoData {
                        DisplayName = "Blista2",
                        ClassName = "Compacts", 
                        IsBroke = false, 
                        Stock = 294000, 
                        StockFull = 95000, 
                        StockItem = 0,
                        FullFuel = 58, 
                        FuelMinute = 6, 
                        Fuel = 58, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -591651781:
                    return new VehicleInfoData {
                        DisplayName = "Blista3",
                        ClassName = "Compacts", 
                        IsBroke = false, 
                        Stock = 294000, 
                        StockFull = 95000, 
                        StockItem = 0,
                        FullFuel = 58, 
                        FuelMinute = 6, 
                        Fuel = 58, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1549126457:
                    return new VehicleInfoData {
                        DisplayName = "Brioso",
                        ClassName = "Compacts", 
                        IsBroke = false, 
                        Stock = 385000, 
                        StockFull = 90000, 
                        StockItem = 0,
                        FullFuel = 45, 
                        FuelMinute = 6, 
                        Fuel = 45, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1130810103:
                    return new VehicleInfoData {
                        DisplayName = "Dilettante",
                        ClassName = "Compacts", 
                        IsBroke = false, 
                        Stock = 672000, 
                        StockFull = 120000, 
                        StockItem = 0,
                        FullFuel = 48, 
                        FuelMinute = 5, 
                        Fuel = 48, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1682114128:
                    return new VehicleInfoData {
                        DisplayName = "Dilettante2",
                        ClassName = "Compacts", 
                        IsBroke = false, 
                        Stock = 672000, 
                        StockFull = 120000, 
                        StockItem = 0,
                        FullFuel = 48, 
                        FuelMinute = 5, 
                        Fuel = 48, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1177863319:
                    return new VehicleInfoData {
                        DisplayName = "Issi2",
                        ClassName = "Compacts", 
                        IsBroke = false, 
                        Stock = 420000, 
                        StockFull = 100000, 
                        StockItem = 0,
                        FullFuel = 46, 
                        FuelMinute = 2, 
                        Fuel = 46, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
					
                case 931280609:
                    return new VehicleInfoData {
                        DisplayName = "Issi3",
                        ClassName = "Compacts", 
                        IsBroke = false, 
                        Stock = 140000, 
                        StockFull = 80000, 
                        StockItem = 0,
                        FullFuel = 35, 
                        FuelMinute = 4, 
                        Fuel = 35, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
					
                case -431692672:
                    return new VehicleInfoData {
                        DisplayName = "Panto",
                        ClassName = "Compacts", 
                        IsBroke = false, 
                        Stock = 364000, 
                        StockFull = 80000, 
                        StockItem = 0,
                        FullFuel = 38, 
                        FuelMinute = 4, 
                        Fuel = 38, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1450650718:
                    return new VehicleInfoData {
                        DisplayName = "Prairie",
                        ClassName = "Compacts", 
                        IsBroke = false, 
                        Stock = 585000, 
                        StockFull = 115000, 
                        StockItem = 0,
                        FullFuel = 60, 
                        FuelMinute = 5, 
                        Fuel = 60, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 841808271:
                    return new VehicleInfoData {
                        DisplayName = "Rhapsody",
                        ClassName = "Compacts", 
                        IsBroke = false, 
                        Stock = 294000, 
                        StockFull = 95000, 
                        StockItem = 0,
                        FullFuel = 35, 
                        FuelMinute = 4, 
                        Fuel = 35, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 330661258:
                    return new VehicleInfoData {
                        DisplayName = "CogCabrio",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 526000, 
                        StockFull = 160000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 7, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -5153954:
                    return new VehicleInfoData {
                        DisplayName = "Exemplar",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 99000, 
                        StockFull = 150000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 9, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -591610296:
                    return new VehicleInfoData {
                        DisplayName = "F620",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 160000, 
                        StockFull = 160000, 
                        StockItem = 0,
                        FullFuel = 71, 
                        FuelMinute = 9, 
                        Fuel = 71, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -391594584:
                    return new VehicleInfoData {
                        DisplayName = "Felon",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 330000, 
                        StockFull = 140000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 9, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -89291282:
                    return new VehicleInfoData {
                        DisplayName = "Felon2",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 210000, 
                        StockFull = 130000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 9, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -447711397:
                    return new VehicleInfoData {
                        DisplayName = "Paragon",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 475000, 
                        StockFull = 520000, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 12, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                    
                case 1416466158:
                    return new VehicleInfoData {
                        DisplayName = "Paragon2",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 475000, 
                        StockFull = 520000, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 14, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -624529134:
                    return new VehicleInfoData {
                        DisplayName = "Jackal",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 240000, 
                        StockFull = 145000, 
                        StockItem = 0,
                        FullFuel = 65, 
                        FuelMinute = 7, 
                        Fuel = 65, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1348744438:
                    return new VehicleInfoData {
                        DisplayName = "Oracle",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 384000, 
                        StockFull = 130000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 7, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -511601230:
                    return new VehicleInfoData {
                        DisplayName = "Oracle2",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 480000, 
                        StockFull = 140000, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 9, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1349725314:
                    return new VehicleInfoData {
                        DisplayName = "Sentinel",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 720000, 
                        StockFull = 150000, 
                        StockItem = 0,
                        FullFuel = 63, 
                        FuelMinute = 7, 
                        Fuel = 63, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 873639469:
                    return new VehicleInfoData {
                        DisplayName = "Sentinel2",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 900000, 
                        StockFull = 160000, 
                        StockItem = 0,
                        FullFuel = 63, 
                        FuelMinute = 7, 
                        Fuel = 63, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1581459400:
                    return new VehicleInfoData {
                        DisplayName = "Windsor",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 540000, 
                        StockFull = 165000, 
                        StockItem = 0,
                        FullFuel = 93, 
                        FuelMinute = 11, 
                        Fuel = 93, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1930048799:
                    return new VehicleInfoData {
                        DisplayName = "Windsor2",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 270000, 
                        StockFull = 135000, 
                        StockItem = 0,
                        FullFuel = 110, 
                        FuelMinute = 12, 
                        Fuel = 110, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1122289213:
                    return new VehicleInfoData {
                        DisplayName = "Zion",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 588000, 
                        StockFull = 155000, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 7, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1193103848:
                    return new VehicleInfoData {
                        DisplayName = "Zion2",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 336000, 
                        StockFull = 145000, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 7, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1131912276:
                    return new VehicleInfoData {
                        DisplayName = "Bmx",
                        ClassName = "Cycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 448402357:
                    return new VehicleInfoData {
                        DisplayName = "Cruiser",
                        ClassName = "Cycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -836512833:
                    return new VehicleInfoData {
                        DisplayName = "Fixter",
                        ClassName = "Cycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -186537451:
                    return new VehicleInfoData {
                        DisplayName = "Scorcher",
                        ClassName = "Cycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1127861609:
                    return new VehicleInfoData {
                        DisplayName = "TriBike",
                        ClassName = "Cycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1233807380:
                    return new VehicleInfoData {
                        DisplayName = "TriBike2",
                        ClassName = "Cycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -400295096:
                    return new VehicleInfoData {
                        DisplayName = "TriBike3",
                        ClassName = "Cycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1672195559:
                    return new VehicleInfoData {
                        DisplayName = "Akuma",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 5000, 
                        StockFull = 20000, 
                        StockItem = 0,
                        FullFuel = 35, 
                        FuelMinute = 4, 
                        Fuel = 35, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2115793025:
                    return new VehicleInfoData {
                        DisplayName = "Avarus",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 42, 
                        FuelMinute = 5, 
                        Fuel = 42, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2140431165:
                    return new VehicleInfoData {
                        DisplayName = "Bagger",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 180000, 
                        StockFull = 45000, 
                        StockItem = 0,
                        FullFuel = 55, 
                        FuelMinute = 5, 
                        Fuel = 55, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -891462355:
                    return new VehicleInfoData {
                        DisplayName = "Bati2",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 38, 
                        FuelMinute = 5, 
                        Fuel = 38, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -114291515:
                    return new VehicleInfoData {
                        DisplayName = "Bati",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 38, 
                        FuelMinute = 5, 
                        Fuel = 38, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 86520421:
                    return new VehicleInfoData {
                        DisplayName = "BF400",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 28, 
                        FuelMinute = 4, 
                        Fuel = 28, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -440768424:
                    return new VehicleInfoData {
                        DisplayName = "Blazer4",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 35, 
                        FuelMinute = 5, 
                        Fuel = 35, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 11251904:
                    return new VehicleInfoData {
                        DisplayName = "CarbonRS",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 49, 
                        FuelMinute = 5, 
                        Fuel = 49, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 6774487:
                    return new VehicleInfoData {
                        DisplayName = "Chimera",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 50, 
                        FuelMinute = 6, 
                        Fuel = 50, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 390201602:
                    return new VehicleInfoData {
                        DisplayName = "Cliffhanger",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 44, 
                        FuelMinute = 4, 
                        Fuel = 44, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1404136503:
                    return new VehicleInfoData {
                        DisplayName = "Daemon2",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 36, 
                        FuelMinute = 4, 
                        Fuel = 36, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2006142190:
                    return new VehicleInfoData {
                        DisplayName = "Daemon",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 36, 
                        FuelMinute = 4, 
                        Fuel = 36, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 822018448:
                    return new VehicleInfoData {
                        DisplayName = "Defiler",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 36, 
                        FuelMinute = 5, 
                        Fuel = 36, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -239841468:
                    return new VehicleInfoData {
                        DisplayName = "Diablous",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 10000, 
                        StockFull = 25000, 
                        StockItem = 0,
                        FullFuel = 38, 
                        FuelMinute = 4, 
                        Fuel = 38, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1790834270:
                    return new VehicleInfoData {
                        DisplayName = "Diablous2",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 10000, 
                        StockFull = 35000, 
                        StockItem = 0,
                        FullFuel = 45, 
                        FuelMinute = 5, 
                        Fuel = 45, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1670998136:
                    return new VehicleInfoData {
                        DisplayName = "Double",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 42, 
                        FuelMinute = 5, 
                        Fuel = 42, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1753414259:
                    return new VehicleInfoData {
                        DisplayName = "Enduro",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 15000, 
                        StockFull = 20000, 
                        StockItem = 0,
                        FullFuel = 30, 
                        FuelMinute = 4, 
                        Fuel = 30, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2035069708:
                    return new VehicleInfoData {
                        DisplayName = "Esskey",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 15000, 
                        StockFull = 20000, 
                        StockItem = 0,
                        FullFuel = 33, 
                        FuelMinute = 4, 
                        Fuel = 33, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 55628203:
                    return new VehicleInfoData {
                        DisplayName = "Faggio2",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 20000, 
                        StockFull = 25000, 
                        StockItem = 0,
                        FullFuel = 25, 
                        FuelMinute = 3, 
                        Fuel = 25, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1289178744:
                    return new VehicleInfoData {
                        DisplayName = "Faggio3",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 30000, 
                        StockFull = 25000, 
                        StockItem = 0,
                        FullFuel = 28, 
                        FuelMinute = 2, 
                        Fuel = 28, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1842748181:
                    return new VehicleInfoData {
                        DisplayName = "Faggio",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 30000, 
                        StockFull = 25000, 
                        StockItem = 0,
                        FullFuel = 28, 
                        FuelMinute = 2, 
                        Fuel = 28, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -757735410:
                    return new VehicleInfoData {
                        DisplayName = "Fcr2",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 5000, 
                        StockFull = 20000, 
                        StockItem = 0,
                        FullFuel = 38, 
                        FuelMinute = 5, 
                        Fuel = 38, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 627535535:
                    return new VehicleInfoData {
                        DisplayName = "Fcr",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 5000, 
                        StockFull = 25000, 
                        StockItem = 0,
                        FullFuel = 32, 
                        FuelMinute = 4, 
                        Fuel = 32, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 741090084:
                    return new VehicleInfoData {
                        DisplayName = "Gargoyle",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 41, 
                        FuelMinute = 5, 
                        Fuel = 41, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -255678177:
                    return new VehicleInfoData {
                        DisplayName = "Hakuchou2",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 20000, 
                        StockFull = 25000, 
                        StockItem = 0,
                        FullFuel = 59, 
                        FuelMinute = 6, 
                        Fuel = 59, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1265391242:
                    return new VehicleInfoData {
                        DisplayName = "Hakuchou",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 20000, 
                        StockFull = 25000, 
                        StockItem = 0,
                        FullFuel = 50, 
                        FuelMinute = 5, 
                        Fuel = 50, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 301427732:
                    return new VehicleInfoData {
                        DisplayName = "Hexer",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 35, 
                        FuelMinute = 4, 
                        Fuel = 35, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -159126838:
                    return new VehicleInfoData {
                        DisplayName = "Innovation",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 5000, 
                        StockFull = 20000, 
                        StockItem = 0,
                        FullFuel = 40, 
                        FuelMinute = 5, 
                        Fuel = 40, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 640818791:
                    return new VehicleInfoData {
                        DisplayName = "Lectro",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 37, 
                        FuelMinute = 4, 
                        Fuel = 37, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1523428744:
                    return new VehicleInfoData {
                        DisplayName = "Manchez",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 40, 
                        FuelMinute = 5, 
                        Fuel = 40, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -634879114:
                    return new VehicleInfoData {
                        DisplayName = "Nemesis",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 5000, 
                        StockFull = 20000, 
                        StockItem = 0,
                        FullFuel = 30, 
                        FuelMinute = 4, 
                        Fuel = 30, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1606187161:
                    return new VehicleInfoData {
                        DisplayName = "Nightblade",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 45, 
                        FuelMinute = 5, 
                        Fuel = 45, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 884483972:
                    return new VehicleInfoData {
                        DisplayName = "Oppressor",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 51, 
                        FuelMinute = 12, 
                        Fuel = 51, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -909201658:
                    return new VehicleInfoData {
                        DisplayName = "PCJ",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 10000, 
                        StockFull = 25000, 
                        StockItem = 0,
                        FullFuel = 41, 
                        FuelMinute = 5, 
                        Fuel = 41, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1873600305:
                    return new VehicleInfoData {
                        DisplayName = "Ratbike",
                        ClassName = "Motorcycles", 
                        IsBroke = true, 
                        Stock = 5000, 
                        StockFull = 25000, 
                        StockItem = 0,
                        FullFuel = 29, 
                        FuelMinute = 4, 
                        Fuel = 29, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -893578776:
                    return new VehicleInfoData {
                        DisplayName = "Ruffian",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 5000, 
                        StockFull = 20000, 
                        StockItem = 0,
                        FullFuel = 38, 
                        FuelMinute = 4, 
                        Fuel = 38, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1453280962:
                    return new VehicleInfoData {
                        DisplayName = "Sanchez2",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 5000, 
                        StockFull = 25000, 
                        StockItem = 0,
                        FullFuel = 35, 
                        FuelMinute = 4, 
                        Fuel = 35, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 788045382:
                    return new VehicleInfoData {
                        DisplayName = "Sanchez",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 5000, 
                        StockFull = 25000, 
                        StockItem = 0,
                        FullFuel = 35, 
                        FuelMinute = 4, 
                        Fuel = 35, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1491277511:
                    return new VehicleInfoData {
                        DisplayName = "Sanctus",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 58, 
                        FuelMinute = 6, 
                        Fuel = 58, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -405626514:
                    return new VehicleInfoData {
                        DisplayName = "Shotaro",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 8, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 743478836:
                    return new VehicleInfoData {
                        DisplayName = "Sovereign",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 180000, 
                        StockFull = 35000, 
                        StockItem = 0,
                        FullFuel = 49, 
                        FuelMinute = 5, 
                        Fuel = 49, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1836027715:
                    return new VehicleInfoData {
                        DisplayName = "Thrust",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 10000, 
                        StockFull = 40000, 
                        StockItem = 0,
                        FullFuel = 50, 
                        FuelMinute = 4, 
                        Fuel = 50, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -140902153:
                    return new VehicleInfoData {
                        DisplayName = "Vader",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 5000, 
                        StockFull = 20000, 
                        StockItem = 0,
                        FullFuel = 36, 
                        FuelMinute = 4, 
                        Fuel = 36, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1353081087:
                    return new VehicleInfoData {
                        DisplayName = "Vindicator",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 10000, 
                        StockFull = 25000, 
                        StockItem = 0,
                        FullFuel = 48, 
                        FuelMinute = 4, 
                        Fuel = 48, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -609625092:
                    return new VehicleInfoData {
                        DisplayName = "Vortex",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 39, 
                        FuelMinute = 5, 
                        Fuel = 39, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -618617997:
                    return new VehicleInfoData {
                        DisplayName = "Wolfsbane",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 5000, 
                        StockFull = 25000, 
                        StockItem = 0,
                        FullFuel = 44, 
                        FuelMinute = 4, 
                        Fuel = 44, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1009268949:
                    return new VehicleInfoData {
                        DisplayName = "Zombiea",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 41, 
                        FuelMinute = 5, 
                        Fuel = 41, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -570033273:
                    return new VehicleInfoData {
                        DisplayName = "Zombieb",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 41, 
                        FuelMinute = 5, 
                        Fuel = 41, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1205801634:
                    return new VehicleInfoData {
                        DisplayName = "Blade",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 336000, 
                        StockFull = 250000, 
                        StockItem = 0,
                        FullFuel = 84, 
                        FuelMinute = 14, 
                        Fuel = 84, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -682211828:
                    return new VehicleInfoData {
                        DisplayName = "Buccaneer",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 245000, 
                        StockFull = 280000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 12, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1013450936:
                    return new VehicleInfoData {
                        DisplayName = "Buccaneer2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 336000, 
                        StockFull = 290000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 12, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 349605904:
                    return new VehicleInfoData {
                        DisplayName = "Chino",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 343000, 
                        StockFull = 275000, 
                        StockItem = 0,
                        FullFuel = 87, 
                        FuelMinute = 12, 
                        Fuel = 87, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1361687965:
                    return new VehicleInfoData {
                        DisplayName = "Chino2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 392000, 
                        StockFull = 285000, 
                        StockItem = 0,
                        FullFuel = 87, 
                        FuelMinute = 12, 
                        Fuel = 87, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 80636076:
                    return new VehicleInfoData {
                        DisplayName = "Dominator",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 350000, 
                        StockFull = 320000, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 16, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -915704871:
                    return new VehicleInfoData {
                        DisplayName = "Dominator2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 350000, 
                        StockFull = 320000, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 16, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                    
                case -986944621:
                    return new VehicleInfoData {
                        DisplayName = "Dominator3",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 290000, 
                        StockFull = 350000, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 16, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
					
                case 723973206:
                    return new VehicleInfoData {
                        DisplayName = "Dukes",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 216000, 
                        StockFull = 330000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 14, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -326143852:
                    return new VehicleInfoData {
                        DisplayName = "Dukes2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 50000, 
                        StockFull = 450000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 20, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case -1267543371:
                    return new VehicleInfoData {
                        DisplayName = "Ellie",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 240000, 
                        StockFull = 320000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 16, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case -2119578145:
                    return new VehicleInfoData {
                        DisplayName = "Faction",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 392000, 
                        StockFull = 265000, 
                        StockItem = 0,
                        FullFuel = 78, 
                        FuelMinute = 12, 
                        Fuel = 78, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1790546981:
                    return new VehicleInfoData {
                        DisplayName = "Faction2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 310000, 
                        StockFull = 255000, 
                        StockItem = 0,
                        FullFuel = 78, 
                        FuelMinute = 12, 
                        Fuel = 78, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2039755226:
                    return new VehicleInfoData {
                        DisplayName = "Faction3",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 360000, 
                        StockFull = 320000, 
                        StockItem = 0,
                        FullFuel = 78, 
                        FuelMinute = 14, 
                        Fuel = 78, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1800170043:
                    return new VehicleInfoData {
                        DisplayName = "Gauntlet",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 315000, 
                        StockFull = 340000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 12, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 349315417:
                    return new VehicleInfoData {
                        DisplayName = "Gauntlet2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 315000, 
                        StockFull = 340000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 12, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case 722226637:
                    return new VehicleInfoData {
                        DisplayName = "Gauntlet3",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 185000, 
                        StockFull = 280000, 
                        StockItem = 0,
                        FullFuel = 78, 
                        FuelMinute = 11, 
                        Fuel = 78, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case 1934384720:
                    return new VehicleInfoData {
                        DisplayName = "Gauntlet4",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 225000, 
                        StockFull = 280000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 13, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                    
                case -882629065:
                    return new VehicleInfoData {
                        DisplayName = "Nebula",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 385000, 
                        StockFull = 300000, 
                        StockItem = 0,
                        FullFuel = 65, 
                        FuelMinute = 6, 
                        Fuel = 65, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case 15219735:
                    return new VehicleInfoData {
                        DisplayName = "Hermes",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 520000, 
                        StockFull = 250000, 
                        StockItem = 0,
                        FullFuel = 64, 
                        FuelMinute = 12, 
                        Fuel = 64, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 37348240:
                    return new VehicleInfoData {
                        DisplayName = "Hotknife",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 181000, 
                        StockFull = 230000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 12, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 600450546:
                    return new VehicleInfoData {
                        DisplayName = "Hustler",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 205000, 
                        StockFull = 225000, 
                        StockItem = 0,
                        FullFuel = 60, 
                        FuelMinute = 11, 
                        Fuel = 60, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case 2068293287:
                    return new VehicleInfoData {
                        DisplayName = "Lurcher",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 780000, 
                        StockFull = 290000, 
                        StockItem = 0,
                        FullFuel = 87, 
                        FuelMinute = 12, 
                        Fuel = 87, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 525509695:
                    return new VehicleInfoData {
                        DisplayName = "Moonbeam",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 1540000, 
                        StockFull = 350000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 13, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1896491931:
                    return new VehicleInfoData {
                        DisplayName = "Moonbeam2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 1232000, 
                        StockFull = 340000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 13, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1943285540:
                    return new VehicleInfoData {
                        DisplayName = "Nightshade",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 72000, 
                        StockFull = 360000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 14, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2095439403:
                    return new VehicleInfoData {
                        DisplayName = "Phoenix",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 126000, 
                        StockFull = 310000, 
                        StockItem = 0,
                        FullFuel = 79, 
                        FuelMinute = 13, 
                        Fuel = 79, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1507916787:
                    return new VehicleInfoData {
                        DisplayName = "Picador",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 1338000, 
                        StockFull = 330000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 11, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -667151410:
                    return new VehicleInfoData {
                        DisplayName = "RatLoader",
                        ClassName = "Muscle", 
                        IsBroke = true, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 12, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case -589178377:
                    return new VehicleInfoData {
                        DisplayName = "RatLoader2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 750000, 
                        StockFull = 400000, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 8, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -227741703:
                    return new VehicleInfoData {
                        DisplayName = "Ruiner",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 364000, 
                        StockFull = 310000, 
                        StockItem = 0,
                        FullFuel = 71, 
                        FuelMinute = 12, 
                        Fuel = 71, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 941494461:
                    return new VehicleInfoData {
                        DisplayName = "Ruiner2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 71, 
                        FuelMinute = 14, 
                        Fuel = 71, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 777714999:
                    return new VehicleInfoData {
                        DisplayName = "Ruiner3",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 364000, 
                        StockFull = 310000, 
                        StockItem = 0,
                        FullFuel = 71, 
                        FuelMinute = 14, 
                        Fuel = 71, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1685021548:
                    return new VehicleInfoData {
                        DisplayName = "SabreGT",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 318000, 
                        StockFull = 325000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 16, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 223258115:
                    return new VehicleInfoData {
                        DisplayName = "SabreGT2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 318000, 
                        StockFull = 365000, 
                        StockItem = 0,
                        FullFuel = 82,
                        FuelMinute = 16, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 734217681:
                    return new VehicleInfoData {
                        DisplayName = "Sadler2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 840000, 
                        StockFull = 720000, 
                        StockItem = 0,
                        FullFuel = 120, 
                        FuelMinute = 12, 
                        Fuel = 120, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 729783779:
                    return new VehicleInfoData {
                        DisplayName = "SlamVan",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 936000, 
                        StockFull = 390000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 11, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 833469436:
                    return new VehicleInfoData {
                        DisplayName = "SlamVan2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 2772000, 
                        StockFull = 390000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 11, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1119641113:
                    return new VehicleInfoData {
                        DisplayName = "SlamVan3",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 920000, 
                        StockFull = 460000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 11, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1923400478:
                    return new VehicleInfoData {
                        DisplayName = "Stalion",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 175000, 
                        StockFull = 260000, 
                        StockItem = 0,
                        FullFuel = 78, 
                        FuelMinute = 11, 
                        Fuel = 78, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -401643538:
                    return new VehicleInfoData {
                        DisplayName = "Stalion2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 90000, 
                        StockFull = 260000, 
                        StockItem = 0,
                        FullFuel = 78, 
                        FuelMinute = 11, 
                        Fuel = 78, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 972671128:
                    return new VehicleInfoData {
                        DisplayName = "Tampa",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 210000, 
                        StockFull = 275000, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 12, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1210451983:
                    return new VehicleInfoData {
                        DisplayName = "Tampa3",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 210000, 
                        StockFull = 410000, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 12, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -825837129:
                    return new VehicleInfoData {
                        DisplayName = "Vigero",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 175000, 
                        StockFull = 275000, 
                        StockItem = 0,
                        FullFuel = 86, 
                        FuelMinute = 11, 
                        Fuel = 86, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -498054846:
                    return new VehicleInfoData {
                        DisplayName = "Virgo",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 318000, 
                        StockFull = 235000, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 13, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -899509638:
                    return new VehicleInfoData {
                        DisplayName = "Virgo2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 260000, 
                        StockFull = 245000, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 15, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 16646064:
                    return new VehicleInfoData {
                        DisplayName = "Virgo3",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 290000, 
                        StockFull = 245000, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 13, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2006667053:
                    return new VehicleInfoData {
                        DisplayName = "Voodoo",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 231000, 
                        StockFull = 260000, 
                        StockItem = 0,
                        FullFuel = 76, 
                        FuelMinute = 14, 
                        Fuel = 76, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 523724515:
                    return new VehicleInfoData {
                        DisplayName = "Voodoo2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 250000, 
                        StockFull = 120000, 
                        StockItem = 0,
                        FullFuel = 76, 
                        FuelMinute = 18, 
                        Fuel = 76, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1871995513:
                    return new VehicleInfoData {
                        DisplayName = "Yosemite",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 1596000, 
                        StockFull = 320000, 
                        StockItem = 0,
                        FullFuel = 76, 
                        FuelMinute = 18, 
                        Fuel = 76, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1126868326:
                    return new VehicleInfoData {
                        DisplayName = "BfInjection",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 13, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -349601129:
                    return new VehicleInfoData {
                        DisplayName = "Bifta",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 48, 
                        FuelMinute = 6, 
                        Fuel = 48, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case -2128233223:
                    return new VehicleInfoData {
                        DisplayName = "Blazer",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 10000, 
                        StockFull = 50000, 
                        StockItem = 0,
                        FullFuel = 14, 
                        FuelMinute = 1, 
                        Fuel = 14, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -48031959:
                    return new VehicleInfoData {
                        DisplayName = "Blazer2",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 22000, 
                        StockFull = 50000, 
                        StockItem = 0,
                        FullFuel = 14, 
                        FuelMinute = 1, 
                        Fuel = 14, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1269889662:
                    return new VehicleInfoData {
                        DisplayName = "Blazer3",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 15000, 
                        StockFull = 50000, 
                        StockItem = 0,
                        FullFuel = 14, 
                        FuelMinute = 2, 
                        Fuel = 14, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1590337689:
                    return new VehicleInfoData {
                        DisplayName = "Blazer5",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 15000, 
                        StockFull = 60000, 
                        StockItem = 0,
                        FullFuel = 15, 
                        FuelMinute = 1, 
                        Fuel = 15, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1435919434:
                    return new VehicleInfoData {
                        DisplayName = "Bodhi2",
                        ClassName = "Off-Road", 
                        IsBroke = true, 
                        Stock = 1359000, 
                        StockFull = 350000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 10, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1479664699:
                    return new VehicleInfoData {
                        DisplayName = "Brawler",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 312000, 
                        StockFull = 340000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 11, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
					
                case -1993175239:
                    return new VehicleInfoData {
                        DisplayName = "Cara",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 1204000, 
                        StockFull = 845000, 
                        StockItem = 0,
                        FullFuel = 140, 
                        FuelMinute = 17, 
                        Fuel = 140, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1254014755:
                    return new VehicleInfoData {
                        DisplayName = "Caracara",
                        ClassName = "Off-Road", 
                        IsBroke = true, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 140, 
                        FuelMinute = 20, 
                        Fuel = 140, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1770332643:
                    return new VehicleInfoData {
                        DisplayName = "DLoader",
                        ClassName = "Off-Road", 
                        IsBroke = true, 
                        Stock = 741000, 
                        StockFull = 390000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 12, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1661854193:
                    return new VehicleInfoData {
                        DisplayName = "Dune",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 20, 
                        FuelMinute = 4, 
                        Fuel = 20, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 534258863:
                    return new VehicleInfoData {
                        DisplayName = "Dune2",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 20, 
                        FuelMinute = 6, 
                        Fuel = 20, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1897744184:
                    return new VehicleInfoData {
                        DisplayName = "Dune3",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 18, 
                        FuelMinute = 2, 
                        Fuel = 18, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -827162039:
                    return new VehicleInfoData {
                        DisplayName = "Dune4",
                        ClassName = "Off-Road", 
                        IsBroke = true, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 60, 
                        FuelMinute = 12, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -312295511:
                    return new VehicleInfoData {
                        DisplayName = "Dune5",
                        ClassName = "Off-Road", 
                        IsBroke = true, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 60, 
                        FuelMinute = 12, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1860900134:
                    return new VehicleInfoData {
                        DisplayName = "Insurgent",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 567000, 
                        StockFull = 1200000, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 12, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2071877360:
                    return new VehicleInfoData {
                        DisplayName = "Insurgent2",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 2469000, 
                        StockFull = 1200000, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 12, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1924433270:
                    return new VehicleInfoData {
                        DisplayName = "Insurgent3",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 567000, 
                        StockFull = 1200000, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 10, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 92612664:
                    return new VehicleInfoData {
                        DisplayName = "Kalahari",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 661000, 
                        StockFull = 210000, 
                        StockItem = 0,
                        FullFuel = 60, 
                        FuelMinute = 7, 
                        Fuel = 60, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -121446169:
                    return new VehicleInfoData {
                        DisplayName = "Kamacho",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 1323000, 
                        StockFull = 480000, 
                        StockItem = 0,
                        FullFuel = 110, 
                        FuelMinute = 18, 
                        Fuel = 110, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 469291905:
                    return new VehicleInfoData {
                        DisplayName = "Lguard",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 2400000, 
                        StockFull = 550000, 
                        StockItem = 0,
                        FullFuel = 100, 
                        FuelMinute = 12, 
                        Fuel = 100, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1233534620:
                    return new VehicleInfoData {
                        DisplayName = "Marshall",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 120, 
                        FuelMinute = 20, 
                        Fuel = 120, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case -1349095620:
                    return new VehicleInfoData {
                        DisplayName = "Caracara 4x4",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 1204000, 
                        StockFull = 1004000, 
                        StockItem = 0,
                        FullFuel = 140, 
                        FuelMinute = 17, 
                        Fuel = 140, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                    
                case -362150785:
                    return new VehicleInfoData {
                        DisplayName = "Hellion",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 1905000, 
                        StockFull = 980000, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 10, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case -1829436850:
                    return new VehicleInfoData {
                        DisplayName = "Novak",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 265000, 
                        StockFull = 430000, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 9, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 914654722:
                    return new VehicleInfoData {
                        DisplayName = "Mesa",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 750000, 
                        StockFull = 220000, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 10, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -748008636:
                    return new VehicleInfoData {
                        DisplayName = "Mesa2",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 750000, 
                        StockFull = 220000, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 10, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2064372143:
                    return new VehicleInfoData {
                        DisplayName = "Mesa3",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 750000, 
                        StockFull = 390000, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 16, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case -845961253:
                    return new VehicleInfoData {
                        DisplayName = "Monster",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 162, 
                        FuelMinute = 25, 
                        Fuel = 162, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 433954513:
                    return new VehicleInfoData {
                        DisplayName = "Nightshark",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 1134000, 
                        StockFull = 940000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 15, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1645267888:
                    return new VehicleInfoData {
                        DisplayName = "RancherXL",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 1822000, 
                        StockFull = 395000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 11, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1933662059:
                    return new VehicleInfoData {
                        DisplayName = "RancherXL2",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 1822000, 
                        StockFull = 395000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 11, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1207771834:
                    return new VehicleInfoData {
                        DisplayName = "Rebel",
                        ClassName = "Off-Road", 
                        IsBroke = true, 
                        Stock = 1271000, 
                        StockFull = 200000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 15, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2045594037:
                    return new VehicleInfoData {
                        DisplayName = "Rebel2",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 1271000, 
                        StockFull = 395000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 11, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1532697517:
                    return new VehicleInfoData {
                        DisplayName = "Riata",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 778000, 
                        StockFull = 410000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 13, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1189015600:
                    return new VehicleInfoData {
                        DisplayName = "Sandking",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 1428000, 
                        StockFull = 500000, 
                        StockItem = 0,
                        FullFuel = 105, 
                        FuelMinute = 15, 
                        Fuel = 105, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 989381445:
                    return new VehicleInfoData {
                        DisplayName = "Sandking2",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 1428000, 
                        StockFull = 540000, 
                        StockItem = 0,
                        FullFuel = 105, 
                        FuelMinute = 15, 
                        Fuel = 105, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2096818938:
                    return new VehicleInfoData {
                        DisplayName = "Technical",
                        ClassName = "Compacts", 
                        IsBroke = true, 
                        Stock = 889000, 
                        StockFull = 285000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 11, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1180875963:
                    return new VehicleInfoData {
                        DisplayName = "Technical2",
                        ClassName = "Off-Road", 
                        IsBroke = true, 
                        Stock = 830000, 
                        StockFull = 255000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 11, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1356124575:
                    return new VehicleInfoData {
                        DisplayName = "Technical3",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 889000, 
                        StockFull = 240000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 11, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 101905590:
                    return new VehicleInfoData {
                        DisplayName = "TrophyTruck",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 120, 
                        FuelMinute = 15, 
                        Fuel = 120, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -663299102:
                    return new VehicleInfoData {
                        DisplayName = "TrophyTruck2",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 125, 
                        FuelMinute = 16, 
                        Fuel = 125, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 850565707:
                    return new VehicleInfoData {
                        DisplayName = "BJXL",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 607000, 
                        StockFull = 450000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 11, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -808831384:
                    return new VehicleInfoData {
                        DisplayName = "Baller",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 675000, 
                        StockFull = 455000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 10, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 142944341:
                    return new VehicleInfoData {
                        DisplayName = "Baller2",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 607000, 
                        StockFull = 470000, 
                        StockItem = 0,
                        FullFuel = 68, 
                        FuelMinute = 10, 
                        Fuel = 68, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1878062887:
                    return new VehicleInfoData {
                        DisplayName = "Baller3",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 607000, 
                        StockFull = 485000, 
                        StockItem = 0,
                        FullFuel = 68, 
                        FuelMinute = 10, 
                        Fuel = 68, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 634118882:
                    return new VehicleInfoData {
                        DisplayName = "Baller4",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 607000, 
                        StockFull = 475000, 
                        StockItem = 0,
                        FullFuel = 68, 
                        FuelMinute = 10, 
                        Fuel = 68, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 470404958:
                    return new VehicleInfoData {
                        DisplayName = "Baller5",
                        ClassName = "Compacts", 
                        IsBroke = false, 
                        Stock = 607000, 
                        StockFull = 520000, 
                        StockItem = 0,
                        FullFuel = 68, 
                        FuelMinute = 10, 
                        Fuel = 68, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 666166960:
                    return new VehicleInfoData {
                        DisplayName = "Baller6",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 607000, 
                        StockFull = 540000, 
                        StockItem = 0,
                        FullFuel = 68, 
                        FuelMinute = 10, 
                        Fuel = 68, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
					
                case 2006918058:
                    return new VehicleInfoData {
                        DisplayName = "Cavalcade",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 945000, 
                        StockFull = 410000, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 12, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case -789894171:
                    return new VehicleInfoData {
                        DisplayName = "Cavalcade2",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 945000, 
                        StockFull = 425000, 
                        StockItem = 0,
                        FullFuel = 110, 
                        FuelMinute = 13, 
                        Fuel = 110, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 683047626:
                    return new VehicleInfoData {
                        DisplayName = "Contender",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 1822000, 
                        StockFull = 555000, 
                        StockItem = 0,
                        FullFuel = 102, 
                        FuelMinute = 16, 
                        Fuel = 102, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1177543287:
                    return new VehicleInfoData {
                        DisplayName = "Dubsta",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 892000, 
                        StockFull = 405000, 
                        StockItem = 0,
                        FullFuel = 96, 
                        FuelMinute = 12, 
                        Fuel = 96, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -394074634:
                    return new VehicleInfoData {
                        DisplayName = "Dubsta2",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 892000, 
                        StockFull = 465000, 
                        StockItem = 0,
                        FullFuel = 100, 
                        FuelMinute = 14, 
                        Fuel = 100, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1237253773:
                    return new VehicleInfoData {
                        DisplayName = "Dubsta3",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 1204000, 
                        StockFull = 845000, 
                        StockItem = 0,
                        FullFuel = 120, 
                        FuelMinute = 16, 
                        Fuel = 120, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1137532101:
                    return new VehicleInfoData {
                        DisplayName = "FQ2",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 324000, 
                        StockFull = 445000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 10, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1775728740:
                    return new VehicleInfoData {
                        DisplayName = "Granger",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 2199000, 
                        StockFull = 530000, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 12, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -261346873:
                    return new VehicleInfoData {
                        DisplayName = "Granger2",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 1805000, 
                        StockFull = 440000, 
                        StockItem = 0,
                        FullFuel = 92, 
                        FuelMinute = 12, 
                        Fuel = 92, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -33078019:
                    return new VehicleInfoData {
                        DisplayName = "Granger3",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 1805000, 
                        StockFull = 440000, 
                        StockItem = 0,
                        FullFuel = 92, 
                        FuelMinute = 12, 
                        Fuel = 92, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2078554704:
                    return new VehicleInfoData {
                        DisplayName = "Executioner",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 2005000, 
                        StockFull = 590000, 
                        StockItem = 0,
                        FullFuel = 105, 
                        FuelMinute = 13, 
                        Fuel = 105, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1543762099:
                    return new VehicleInfoData {
                        DisplayName = "Gresley",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 585000, 
                        StockFull = 420000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 884422927:
                    return new VehicleInfoData {
                        DisplayName = "Habanero",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 438000, 
                        StockFull = 355000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 10, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 486987393:
                    return new VehicleInfoData {
                        DisplayName = "Huntley",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 525000, 
                        StockFull = 395000, 
                        StockItem = 0,
                        FullFuel = 94, 
                        FuelMinute = 10, 
                        Fuel = 94, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1269098716:
                    return new VehicleInfoData {
                        DisplayName = "Landstalker",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 1500000, 
                        StockFull = 480000, 
                        StockItem = 0,
                        FullFuel = 76, 
                        FuelMinute = 10, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -808457413:
                    return new VehicleInfoData {
                        DisplayName = "Patriot",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 850000, 
                        StockFull = 565000, 
                        StockItem = 0,
                        FullFuel = 150, 
                        FuelMinute = 30, 
                        Fuel = 150, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1651067813:
                    return new VehicleInfoData {
                        DisplayName = "Radi",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 405000, 
                        StockFull = 380000, 
                        StockItem = 0,
                        FullFuel = 76, 
                        FuelMinute = 10, 
                        Fuel = 76, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2136773105:
                    return new VehicleInfoData {
                        DisplayName = "Rocoto",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 680000, 
                        StockFull = 435000, 
                        StockItem = 0,
                        FullFuel = 100, 
                        FuelMinute = 10, 
                        Fuel = 100, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1221512915:
                    return new VehicleInfoData {
                        DisplayName = "Seminole",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 756000, 
                        StockFull = 415000, 
                        StockItem = 0,
                        FullFuel = 74, 
                        FuelMinute = 12, 
                        Fuel = 74, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                case -1810806490:
                    return new VehicleInfoData {
                        DisplayName = "Seminole2",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 756000, 
                        StockFull = 415000, 
                        StockItem = 0,
                        FullFuel = 74, 
                        FuelMinute = 12, 
                        Fuel = 74, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1337041428:
                    return new VehicleInfoData {
                        DisplayName = "Serrano",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 573000, 
                        StockFull = 440000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 11, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1203490606:
                    return new VehicleInfoData {
                        DisplayName = "XLS",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 850000, 
                        StockFull = 455000, 
                        StockItem = 0,
                        FullFuel = 100, 
                        FuelMinute = 11, 
                        Fuel = 100, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -432008408:
                    return new VehicleInfoData {
                        DisplayName = "XLS2",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 850000, 
                        StockFull = 495000, 
                        StockItem = 0,
                        FullFuel = 100, 
                        FuelMinute = 11, 
                        Fuel = 100, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
					
                 case -392250517:
                    return new VehicleInfoData {
                        DisplayName = "Admiral2",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 330000, 
                        StockFull = 155000, 
                        StockItem = 0,
                        FullFuel = 61, 
                        FuelMinute = 5, 
                        Fuel = 61, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        			
                 case -1809822327:
                    return new VehicleInfoData {
                        DisplayName = "Asea",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 315000, 
                        StockFull = 125000, 
                        StockItem = 0,
                        FullFuel = 61, 
                        FuelMinute = 5, 
                        Fuel = 61, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -350899544:
                    return new VehicleInfoData {
                        DisplayName = "Merit2",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 392000, 
                        StockFull = 125000, 
                        StockItem = 0,
                        FullFuel = 65, 
                        FuelMinute = 6, 
                        Fuel = 65, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1807623979:
                    return new VehicleInfoData {
                        DisplayName = "Asea2",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 315000, 
                        StockFull = 125000, 
                        StockItem = 0,
                        FullFuel = 61, 
                        FuelMinute = 5, 
                        Fuel = 61, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1903012613:
                    return new VehicleInfoData {
                        DisplayName = "Asterope",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 183000, 
                        StockFull = 130000, 
                        StockItem = 0,
                        FullFuel = 61, 
                        FuelMinute = 8, 
                        Fuel = 61, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case 906642318:
                    return new VehicleInfoData {
                        DisplayName = "Cog55",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 313000, 
                        StockFull = 140000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 9, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 704435172:
                    return new VehicleInfoData {
                        DisplayName = "Cog552",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 313000, 
                        StockFull = 180000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 9, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2030171296:
                    return new VehicleInfoData {
                        DisplayName = "Cognoscenti",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 313000, 
                        StockFull = 135000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 9, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -604842630:
                    return new VehicleInfoData {
                        DisplayName = "Cognoscenti2",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 313000, 
                        StockFull = 175000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 9, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -685276541:
                    return new VehicleInfoData {
                        DisplayName = "Emperor",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 330000, 
                        StockFull = 115000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 3, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1883002148:
                    return new VehicleInfoData {
                        DisplayName = "Emperor2",
                        ClassName = "Sedans", 
                        IsBroke = true, 
                        Stock = 330000, 
                        StockFull = 55000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 8, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1241712818:
                    return new VehicleInfoData {
                        DisplayName = "Emperor3",
                        ClassName = "Sedans", 
                        IsBroke = true, 
                        Stock = 330000, 
                        StockFull = 115000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 6, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1909141499:
                    return new VehicleInfoData {
                        DisplayName = "Fugitive",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 288000, 
                        StockFull = 135000, 
                        StockItem = 0,
                        FullFuel = 60, 
                        FuelMinute = 6, 
                        Fuel = 60, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 75131841:
                    return new VehicleInfoData {
                        DisplayName = "Glendale",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 120000, 
                        StockFull = 105000, 
                        StockItem = 0,
                        FullFuel = 68, 
                        FuelMinute = 7, 
                        Fuel = 68, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 40817712:
                    return new VehicleInfoData {
                        DisplayName = "Greenwood",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 325000, 
                        StockFull = 125000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 6, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1289722222:
                    return new VehicleInfoData {
                        DisplayName = "Ingot",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 708000, 
                        StockFull = 150000, 
                        StockItem = 0,
                        FullFuel = 68, 
                        FuelMinute = 8, 
                        Fuel = 66, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 886934177:
                    return new VehicleInfoData {
                        DisplayName = "Intruder",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 126000, 
                        StockFull = 120000, 
                        StockItem = 0,
                        FullFuel = 74, 
                        FuelMinute = 7, 
                        Fuel = 44, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -114627507:
                    return new VehicleInfoData {
                        DisplayName = "Limo2",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 60, 
                        FuelMinute = 3, 
                        Fuel = 60, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1883869285:
                    return new VehicleInfoData {
                        DisplayName = "Premier",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 181000, 
                        StockFull = 120000, 
                        StockItem = 0,
                        FullFuel = 65, 
                        FuelMinute = 8, 
                        Fuel = 65, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1150599089:
                    return new VehicleInfoData {
                        DisplayName = "Primo",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 254000, 
                        StockFull = 115000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 8, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2040426790:
                    return new VehicleInfoData {
                        DisplayName = "Primo2",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 190000, 
                        StockFull = 120000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 8, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -14495224:
                    return new VehicleInfoData {
                        DisplayName = "Regina",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 1100000, 
                        StockFull = 145000, 
                        StockItem = 0,
                        FullFuel = 62, 
                        FuelMinute = 7, 
                        Fuel = 62, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 627094268:
                    return new VehicleInfoData {
                        DisplayName = "Romero",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 822000, 
                        StockFull = 195000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 9, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1477580979:
                    return new VehicleInfoData {
                        DisplayName = "Stanier",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 378000, 
                        StockFull = 145000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 8, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1445320949:
                    return new VehicleInfoData {
                        DisplayName = "Stanier2",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 378000, 
                        StockFull = 145000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 8, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1723137093:
                    return new VehicleInfoData {
                        DisplayName = "Stratum",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 630000, 
                        StockFull = 160000, 
                        StockItem = 0,
                        FullFuel = 65, 
                        FuelMinute = 8, 
                        Fuel = 65, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1961627517:
                    return new VehicleInfoData {
                        DisplayName = "Stretch",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 390000, 
                        StockFull = 120000, 
                        StockItem = 0,
                        FullFuel = 79, 
                        FuelMinute = 9, 
                        Fuel = 79, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1894894188:
                    return new VehicleInfoData {
                        DisplayName = "Surge",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 429000, 
                        StockFull = 110000, 
                        StockItem = 0,
                        FullFuel = 1, 
                        FuelMinute = 0, 
                        Fuel = 1, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1008861746:
                    return new VehicleInfoData {
                        DisplayName = "Tailgater",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 285000, 
                        StockFull = 125000, 
                        StockItem = 0,
                        FullFuel = 63, 
                        FuelMinute = 8, 
                        Fuel = 63, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case 1373123368:
                    return new VehicleInfoData {
                        DisplayName = "Warrener",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 292000, 
                        StockFull = 95000, 
                        StockItem = 0,
                        FullFuel = 68, 
                        FuelMinute = 9, 
                        Fuel = 68, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1777363799:
                    return new VehicleInfoData {
                        DisplayName = "Washington",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 307000, 
                        StockFull = 130000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 9, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 767087018:
                    return new VehicleInfoData {
                        DisplayName = "Alpha",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 236000, 
                        StockFull = 240000, 
                        StockItem = 0,
                        FullFuel = 92, 
                        FuelMinute = 10, 
                        Fuel = 92, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1041692462:
                    return new VehicleInfoData {
                        DisplayName = "Banshee",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 175000, 
                        StockFull = 260000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 10, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 633712403:
                    return new VehicleInfoData {
                        DisplayName = "Banshee2",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 13, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1274868363:
                    return new VehicleInfoData {
                        DisplayName = "BestiaGTS",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 120000, 
                        StockFull = 240000, 
                        StockItem = 0,
                        FullFuel = 81, 
                        FuelMinute = 9, 
                        Fuel = 81, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                    
                    case 686471183:
                        return new VehicleInfoData {
                            DisplayName = "Drafter",
                            ClassName = "Sports", 
                            IsBroke = false, 
                            Stock = 315000, 
                            StockFull = 240000, 
                            StockItem = 0,
                            FullFuel = 72, 
                            FuelMinute = 8, 
                            Fuel = 72, 
                            SWhBkl = 0, 
                            SWhBl = 0, 
                            SWhBkr = 0, 
                            SWhBr = 0, 
                            SEngine = 0, 
                            SSuspension = 0, 
                            SBody = 0, 
                            SCandle = 0, 
                            SOil = 0, 
                            SMp = 0 
                        };
        
                case -304802106:
                    return new VehicleInfoData {
                        DisplayName = "Buffalo",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 270000, 
                        StockFull = 310000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 8, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 736902334:
                    return new VehicleInfoData {
                        DisplayName = "Buffalo2",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 270000, 
                        StockFull = 325000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 237764926:
                    return new VehicleInfoData {
                        DisplayName = "Buffalo3",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 270000, 
                        StockFull = 310000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2072687711:
                    return new VehicleInfoData {
                        DisplayName = "Carbonizzare",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 430000, 
                        StockFull = 235000, 
                        StockItem = 0,
                        FullFuel = 83, 
                        FuelMinute = 11, 
                        Fuel = 83, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1045541610:
                    return new VehicleInfoData {
                        DisplayName = "Comet2",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 135000, 
                        StockFull = 220000, 
                        StockItem = 0,
                        FullFuel = 68, 
                        FuelMinute = 8, 
                        Fuel = 68, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2022483795:
                    return new VehicleInfoData {
                        DisplayName = "Comet3",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 160000, 
                        StockFull = 190000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 11, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1561920505:
                    return new VehicleInfoData {
                        DisplayName = "Comet4",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 160000, 
                        StockFull = 190000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 15, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 661493923:
                    return new VehicleInfoData {
                        DisplayName = "Comet5",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 160000, 
                        StockFull = 190000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 12, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 108773431:
                    return new VehicleInfoData {
                        DisplayName = "Coquette",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 65, 
                        FuelMinute = 8, 
                        Fuel = 65, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2125340601:
                    return new VehicleInfoData {
                        DisplayName = "Coquette42",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 10, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                    
                case -208911803:
                    return new VehicleInfoData {
                        DisplayName = "Jugular",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 250000, 
                        StockFull = 180000, 
                        StockItem = 0,
                        FullFuel = 78, 
                        FuelMinute = 9, 
                        Fuel = 78, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
					
                case -1728685474:
                    return new VehicleInfoData {
                        DisplayName = "Coquette4",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 10, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 196747873:
                    return new VehicleInfoData {
                        DisplayName = "Elegy",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 350000, 
                        StockFull = 205000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 12, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -566387422:
                    return new VehicleInfoData {
                        DisplayName = "Elegy2",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 220000, 
                        StockFull = 235000, 
                        StockItem = 0,
                        FullFuel = 74, 
                        FuelMinute = 10, 
                        Fuel = 74, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1995326987:
                    return new VehicleInfoData {
                        DisplayName = "Feltzer2",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 300000, 
                        StockFull = 255000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 10, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1566741232:
                    return new VehicleInfoData {
                        DisplayName = "Feltzer3",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 230000, 
                        StockFull = 170000, 
                        StockItem = 0,
                        FullFuel = 78, 
                        FuelMinute = 15, 
                        Fuel = 78, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1259134696:
                    return new VehicleInfoData {
                        DisplayName = "FlashGT",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 230000, 
                        StockFull = 170000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 11, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1089039904:
                    return new VehicleInfoData {
                        DisplayName = "Furoregt",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 86, 
                        FuelMinute = 9, 
                        Fuel = 86, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 499169875:
                    return new VehicleInfoData {
                        DisplayName = "Fusilade",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 220000, 
                        StockFull = 170000, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 9, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2016857647:
                    return new VehicleInfoData {
                        DisplayName = "Futo",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 200000, 
                        StockFull = 165000, 
                        StockItem = 0,
                        FullFuel = 56, 
                        FuelMinute = 8, 
                        Fuel = 56, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1909189272:
                    return new VehicleInfoData {
                        DisplayName = "GB200",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 200000, 
                        StockFull = 165000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 11, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1115909093:
                    return new VehicleInfoData {
                        DisplayName = "Hotring",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 300000, 
                        StockFull = 265000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 14, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1405937764:
                    return new VehicleInfoData {
                        DisplayName = "Infernus2",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 31000, 
                        StockFull = 190000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 12, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case -1297672541:
                    return new VehicleInfoData {
                        DisplayName = "Jester",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 8, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1106353882:
                    return new VehicleInfoData {
                        DisplayName = "Jester2",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 8, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 544021352:
                    return new VehicleInfoData {
                        DisplayName = "Khamelion",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 200000, 
                        StockFull = 205000, 
                        StockItem = 0,
                        FullFuel = 1, 
                        FuelMinute = 0, 
                        Fuel = 1, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1372848492:
                    return new VehicleInfoData {
                        DisplayName = "Kuruma",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 240000, 
                        StockFull = 265000, 
                        StockItem = 0,
                        FullFuel = 74, 
                        FuelMinute = 9, 
                        Fuel = 74, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 410882957:
                    return new VehicleInfoData {
                        DisplayName = "Kuruma2",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 240000, 
                        StockFull = 305000, 
                        StockItem = 0,
                        FullFuel = 74, 
                        FuelMinute = 9, 
                        Fuel = 74, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 482197771:
                    return new VehicleInfoData {
                        DisplayName = "Lynx",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 20000, 
                        StockFull = 230000, 
                        StockItem = 0,
                        FullFuel = 71, 
                        FuelMinute = 9, 
                        Fuel = 71, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -142942670:
                    return new VehicleInfoData {
                        DisplayName = "Massacro",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 135000, 
                        StockFull = 270000, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 10, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -631760477:
                    return new VehicleInfoData {
                        DisplayName = "Massacro2",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 135000, 
                        StockFull = 270000, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 10, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1848994066:
                    return new VehicleInfoData {
                        DisplayName = "Neon",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 1, 
                        FuelMinute = 0, 
                        Fuel = 1, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1032823388:
                    return new VehicleInfoData {
                        DisplayName = "Ninef",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 20000, 
                        StockFull = 240000, 
                        StockItem = 0,
                        FullFuel = 73, 
                        FuelMinute = 12, 
                        Fuel = 73, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1461482751:
                    return new VehicleInfoData {
                        DisplayName = "Ninef2",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 20000, 
                        StockFull = 230000, 
                        StockItem = 0,
                        FullFuel = 73, 
                        FuelMinute = 12, 
                        Fuel = 73, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -777172681:
                    return new VehicleInfoData {
                        DisplayName = "Omnis",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 11, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 867799010:
                    return new VehicleInfoData {
                        DisplayName = "Pariah",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 150000, 
                        StockFull = 265000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 15, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -377465520:
                    return new VehicleInfoData {
                        DisplayName = "Penumbra",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 351000, 
                        StockFull = 185000, 
                        StockItem = 0,
                        FullFuel = 77, 
                        FuelMinute = 11, 
                        Fuel = 77, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1529242755:
                    return new VehicleInfoData {
                        DisplayName = "Raiden",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 315000, 
                        StockFull = 190000, 
                        StockItem = 0,
                        FullFuel = 1, 
                        FuelMinute = 0, 
                        Fuel = 1, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1934452204:
                    return new VehicleInfoData {
                        DisplayName = "RapidGT",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 45000, 
                        StockFull = 235000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 11, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1737773231:
                    return new VehicleInfoData {
                        DisplayName = "RapidGT2",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 80000, 
                        StockFull = 225000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 11, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -674927303:
                    return new VehicleInfoData {
                        DisplayName = "Raptor",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 55, 
                        FuelMinute = 5, 
                        Fuel = 55, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -410205223:
                    return new VehicleInfoData {
                        DisplayName = "Revolter",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 168000, 
                        StockFull = 175000, 
                        StockItem = 0,
                        FullFuel = 68, 
                        FuelMinute = 9, 
                        Fuel = 68, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 719660200:
                    return new VehicleInfoData {
                        DisplayName = "Ruston",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 76, 
                        FuelMinute = 9, 
                        Fuel = 76, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1255452397:
                    return new VehicleInfoData {
                        DisplayName = "Schafter2",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 300000, 
                        StockFull = 150000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 8, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1485523546:
                    return new VehicleInfoData {
                        DisplayName = "Schafter3",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 300000, 
                        StockFull = 165000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 9, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1489967196:
                    return new VehicleInfoData {
                        DisplayName = "Schafter4",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 300000, 
                        StockFull = 160000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 9, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -888242983:
                    return new VehicleInfoData {
                        DisplayName = "Schafter5",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 300000, 
                        StockFull = 205000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 9, 
                        Fuel = 9, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case 1922255844:
                    return new VehicleInfoData {
                        DisplayName = "Schafter6",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 300000, 
                        StockFull = 200000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 9, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -746882698:
                    return new VehicleInfoData {
                        DisplayName = "Schwarzer",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 270000, 
                        StockFull = 155000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 8, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1104234922:
                    return new VehicleInfoData {
                        DisplayName = "Sentinel3",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 216000, 
                        StockFull = 135000, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 8, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1757836725:
                    return new VehicleInfoData {
                        DisplayName = "Seven70",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 50000, 
                        StockFull = 210000, 
                        StockItem = 0,
                        FullFuel = 97, 
                        FuelMinute = 10, 
                        Fuel = 97, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1886268224:
                    return new VehicleInfoData {
                        DisplayName = "Specter",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 10, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1074745671:
                    return new VehicleInfoData {
                        DisplayName = "Specter2",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 13, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1741861769:
                    return new VehicleInfoData {
                        DisplayName = "Streiter",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 688000, 
                        StockFull = 470000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 15, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 970598228:
                    return new VehicleInfoData {
                        DisplayName = "Sultan",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 285000, 
                        StockFull = 195000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 384071873:
                    return new VehicleInfoData {
                        DisplayName = "Surano",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 140000, 
                        StockFull = 230000, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 10, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1071380347:
                    return new VehicleInfoData {
                        DisplayName = "Tampa2",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 74, 
                        FuelMinute = 9, 
                        Fuel = 74, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1887331236:
                    return new VehicleInfoData {
                        DisplayName = "Tropos",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 78, 
                        FuelMinute = 10, 
                        Fuel = 78, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1102544804:
                    return new VehicleInfoData {
                        DisplayName = "Verlierer2",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 80000, 
                        StockFull = 240000, 
                        StockItem = 0,
                        FullFuel = 79, 
                        FuelMinute = 9, 
                        Fuel = 79, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 159274291:
                    return new VehicleInfoData {
                        DisplayName = "Ardent",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 126000, 
                        StockFull = 190000, 
                        StockItem = 0,
                        FullFuel = 78, 
                        FuelMinute = 10, 
                        Fuel = 78, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 117401876:
                    return new VehicleInfoData {
                        DisplayName = "BType",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 192000, 
                        StockFull = 110000, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 10, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -831834716:
                    return new VehicleInfoData {
                        DisplayName = "BType2",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 10, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -602287871:
                    return new VehicleInfoData {
                        DisplayName = "BType3",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 192000, 
                        StockFull = 110000, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 10, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 941800958:
                    return new VehicleInfoData {
                        DisplayName = "Casco",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 273000, 
                        StockFull = 165000, 
                        StockItem = 0,
                        FullFuel = 65, 
                        FuelMinute = 9, 
                        Fuel = 65, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 223240013:
                    return new VehicleInfoData {
                        DisplayName = "Cheetah2",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 150000, 
                        StockFull = 180000, 
                        StockItem = 0,
                        FullFuel = 77, 
                        FuelMinute = 10, 
                        Fuel = 77, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -988501280:
                    return new VehicleInfoData {
                        DisplayName = "Cheburek",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 320000, 
                        StockFull = 140000, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 7, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1011753235:
                    return new VehicleInfoData {
                        DisplayName = "Coquette2",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 76, 
                        FuelMinute = 11, 
                        Fuel = 76, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 784565758:
                    return new VehicleInfoData {
                        DisplayName = "Coquette3",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 251000, 
                        StockFull = 180000, 
                        StockItem = 0,
                        FullFuel = 76, 
                        FuelMinute = 11, 
                        Fuel = 76, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1483171323:
                    return new VehicleInfoData {
                        DisplayName = "Deluxo",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 49000, 
                        StockFull = 160000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 13, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1617472902:
                    return new VehicleInfoData {
                        DisplayName = "Fagaloa",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 800000, 
                        StockFull = 195000, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 9, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2079788230:
                    return new VehicleInfoData {
                        DisplayName = "GT500",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 154000, 
                        StockFull = 155000, 
                        StockItem = 0,
                        FullFuel = 79, 
                        FuelMinute = 12, 
                        Fuel = 79, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1051415893:
                    return new VehicleInfoData {
                        DisplayName = "JB700",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 171000, 
                        StockFull = 160000, 
                        StockItem = 0,
                        FullFuel = 62, 
                        FuelMinute = 9, 
                        Fuel = 62, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -214906006:
                    return new VehicleInfoData {
                        DisplayName = "Jester3",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 90000, 
                        StockFull = 190000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 11, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1660945322:
                    return new VehicleInfoData {
                        DisplayName = "Mamba",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 175000, 
                        StockFull = 160000, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 9, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2124201592:
                    return new VehicleInfoData {
                        DisplayName = "Manana",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 263000, 
                        StockFull = 110000, 
                        StockItem = 0,
                        FullFuel = 76, 
                        FuelMinute = 11, 
                        Fuel = 76, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1046206681:
                    return new VehicleInfoData {
                        DisplayName = "Michelli",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 263000, 
                        StockFull = 110000, 
                        StockItem = 0,
                        FullFuel = 66, 
                        FuelMinute = 8, 
                        Fuel = 66, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case -433375717:
                    return new VehicleInfoData {
                        DisplayName = "Monroe",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 30000, 
                        StockFull = 175000, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 10, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1830407356:
                    return new VehicleInfoData {
                        DisplayName = "Peyote",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 264000, 
                        StockFull = 135000, 
                        StockItem = 0,
                        FullFuel = 69, 
                        FuelMinute = 9, 
                        Fuel = 69, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1804415708:
                    return new VehicleInfoData {
                        DisplayName = "Peyote2",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 264000, 
                        StockFull = 135000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 14, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1078682497:
                    return new VehicleInfoData {
                        DisplayName = "Pigalle",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 378000, 
                        StockFull = 170000, 
                        StockItem = 0,
                        FullFuel = 69, 
                        FuelMinute = 9, 
                        Fuel = 69, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2049897956:
                    return new VehicleInfoData {
                        DisplayName = "RapidGT3",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 151000, 
                        StockFull = 270000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 15, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1841130506:
                    return new VehicleInfoData {
                        DisplayName = "Retinue",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 216000, 
                        StockFull = 205000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 8, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 903794909:
                    return new VehicleInfoData {
                        DisplayName = "Savestra",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 252000, 
                        StockFull = 195000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 8, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1545842587:
                    return new VehicleInfoData {
                        DisplayName = "Stinger",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 352000, 
                        StockFull = 180000, 
                        StockItem = 0,
                        FullFuel = 78, 
                        FuelMinute = 11, 
                        Fuel = 78, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2098947590:
                    return new VehicleInfoData {
                        DisplayName = "StingerGT",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 2, 
                        StockItem = 0,
                        FullFuel = 79, 
                        FuelMinute = 14, 
                        Fuel = 79, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 886810209:
                    return new VehicleInfoData {
                        DisplayName = "Stromberg",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 2, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1504306544:
                    return new VehicleInfoData {
                        DisplayName = "Torero",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 30000, 
                        StockFull = 170000, 
                        StockItem = 0,
                        FullFuel = 79, 
                        FuelMinute = 12, 
                        Fuel = 79, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 464687292:
                    return new VehicleInfoData {
                        DisplayName = "Tornado",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 224000, 
                        StockFull = 135000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 9, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1531094468:
                    return new VehicleInfoData {
                        DisplayName = "Tornado2",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 224000, 
                        StockFull = 125000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 9, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1762279763:
                    return new VehicleInfoData {
                        DisplayName = "Tornado3",
                        ClassName = "Sports Classics", 
                        IsBroke = true, 
                        Stock = 224000, 
                        StockFull = 70000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 12, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2033222435:
                    return new VehicleInfoData {
                        DisplayName = "Tornado4",
                        ClassName = "Sports Classics", 
                        IsBroke = true, 
                        Stock = 224000, 
                        StockFull = 65000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 12, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1797613329:
                    return new VehicleInfoData {
                        DisplayName = "Tornado5",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 212000, 
                        StockFull = 125000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1558399629:
                    return new VehicleInfoData {
                        DisplayName = "Tornado6",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 224000, 
                        StockFull = 250000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 15, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -391595372:
                    return new VehicleInfoData {
                        DisplayName = "Viseris",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 70000, 
                        StockFull = 245000, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 14, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 838982985:
                    return new VehicleInfoData {
                        DisplayName = "Z190",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 132000, 
                        StockFull = 175000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 9, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 758895617:
                    return new VehicleInfoData {
                        DisplayName = "ZType",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 65, 
                        FuelMinute = 9, 
                        Fuel = 65, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1216765807:
                    return new VehicleInfoData {
                        DisplayName = "Adder",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 56000, 
                        StockFull = 210000, 
                        StockItem = 0,
                        FullFuel = 100, 
                        FuelMinute = 11, 
                        Fuel = 100, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -313185164:
                    return new VehicleInfoData {
                        DisplayName = "Autarch",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 110, 
                        FuelMinute = 12, 
                        Fuel = 110, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1696146015:
                    return new VehicleInfoData {
                        DisplayName = "Bullet",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 11, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -802062533:
                    return new VehicleInfoData {
                        DisplayName = "Bullet2",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 11, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1311154784:
                    return new VehicleInfoData {
                        DisplayName = "Cheetah",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 11, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1392481335:
                    return new VehicleInfoData {
                        DisplayName = "Cyclone",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 1, 
                        FuelMinute = 0, 
                        Fuel = 1, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1291952903:
                    return new VehicleInfoData {
                        DisplayName = "EntityXF",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 78, 
                        FuelMinute = 11, 
                        Fuel = 78, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2120700196:
                    return new VehicleInfoData {
                        DisplayName = "Entity2",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 12, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1426219628:
                    return new VehicleInfoData {
                        DisplayName = "FMJ",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 86, 
                        FuelMinute = 11, 
                        Fuel = 86, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1234311532:
                    return new VehicleInfoData {
                        DisplayName = "GP1",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 37000, 
                        StockFull = 225000, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 12, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 418536135:
                    return new VehicleInfoData {
                        DisplayName = "Infernus",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 100, 
                        FuelMinute = 11, 
                        Fuel = 100, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1232836011:
                    return new VehicleInfoData {
                        DisplayName = "LE7B",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 12, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1034187331:
                    return new VehicleInfoData {
                        DisplayName = "Nero",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 30000, 
                        StockFull = 220000, 
                        StockItem = 0,
                        FullFuel = 86, 
                        FuelMinute = 11, 
                        Fuel = 86, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case 1093792632:
                    return new VehicleInfoData {
                        DisplayName = "Nero2",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 89, 
                        FuelMinute = 13, 
                        Fuel = 89, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1987142870:
                    return new VehicleInfoData {
                        DisplayName = "Osiris",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 78, 
                        FuelMinute = 12, 
                        Fuel = 78, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1758137366:
                    return new VehicleInfoData {
                        DisplayName = "Penetrator",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 96000, 
                        StockFull = 220000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 11, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1829802492:
                    return new VehicleInfoData {
                        DisplayName = "Pfister811",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 11, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2123327359:
                    return new VehicleInfoData {
                        DisplayName = "Prototipo",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 115, 
                        FuelMinute = 15, 
                        Fuel = 115, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 234062309:
                    return new VehicleInfoData {
                        DisplayName = "Reaper",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 148000, 
                        StockFull = 215000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 12, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1352136073:
                    return new VehicleInfoData {
                        DisplayName = "SC1",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 12, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 819197656:
                    return new VehicleInfoData {
                        DisplayName = "Sheava",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 10, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -295689028:
                    return new VehicleInfoData {
                        DisplayName = "SultanRS",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 252000, 
                        StockFull = 195000, 
                        StockItem = 0,
                        FullFuel = 92, 
                        FuelMinute = 13, 
                        Fuel = 92, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1123216662:
                    return new VehicleInfoData {
                        DisplayName = "Superd",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 318000, 
                        StockFull = 145000, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 10, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1663218586:
                    return new VehicleInfoData {
                        DisplayName = "T20",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 12, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1134706562:
                    return new VehicleInfoData {
                        DisplayName = "Taipan",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 13, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 272929391:
                    return new VehicleInfoData {
                        DisplayName = "Tempesta",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 40000, 
                        StockFull = 235000, 
                        StockItem = 0,
                        FullFuel = 100, 
                        FuelMinute = 11, 
                        Fuel = 100, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1031562256:
                    return new VehicleInfoData {
                        DisplayName = "Tezeract",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 1, 
                        FuelMinute = 0, 
                        Fuel = 1, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -982130927:
                    return new VehicleInfoData {
                        DisplayName = "Turismo2",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 48000, 
                        StockFull = 210000, 
                        StockItem = 0,
                        FullFuel = 86, 
                        FuelMinute = 12, 
                        Fuel = 86, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 408192225:
                    return new VehicleInfoData {
                        DisplayName = "Turismor",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 11, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -376434238:
                    return new VehicleInfoData {
                        DisplayName = "Tyrant",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 50000, 
                        StockFull = 210000, 
                        StockItem = 0,
                        FullFuel = 92, 
                        FuelMinute = 12, 
                        Fuel = 92, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2067820283:
                    return new VehicleInfoData {
                        DisplayName = "Tyrus",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 11, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 338562499:
                    return new VehicleInfoData {
                        DisplayName = "Vacca",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 60000, 
                        StockFull = 240000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 11, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1939284556:
                    return new VehicleInfoData {
                        DisplayName = "Vagner",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 92, 
                        FuelMinute = 10, 
                        Fuel = 92, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1242608589:
                    return new VehicleInfoData {
                        DisplayName = "Vigilante",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 13, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -998177792:
                    return new VehicleInfoData {
                        DisplayName = "Visione",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 12, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1622444098:
                    return new VehicleInfoData {
                        DisplayName = "Voltic",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 1, 
                        FuelMinute = 0, 
                        Fuel = 1, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 989294410:
                    return new VehicleInfoData {
                        DisplayName = "Voltic2",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 1, 
                        FuelMinute = 0, 
                        Fuel = 1, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1403128555:
                    return new VehicleInfoData {
                        DisplayName = "Zentorno",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 100, 
                        FuelMinute = 12, 
                        Fuel = 100, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case 1044193113:
                    return new VehicleInfoData {
                        DisplayName = "Thrax",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 18000, 
                        StockFull = 180000, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 12, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case -324618589:
                    return new VehicleInfoData {
                        DisplayName = "S80RR",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 13, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case -664141241:
                    return new VehicleInfoData {
                        DisplayName = "Krieger",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 13, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                    
                    case 1323778901:
                        return new VehicleInfoData {
                            DisplayName = "Emerus",
                            ClassName = "Super", 
                            IsBroke = false, 
                            Stock = 0, 
                            StockFull = 0, 
                            StockItem = 0,
                            FullFuel = 95, 
                            FuelMinute = 13, 
                            Fuel = 95, 
                            SWhBkl = 0,
                            SWhBl = 0, 
                            SWhBkr = 0, 
                            SWhBr = 0, 
                            SEngine = 0, 
                            SSuspension = 0, 
                            SBody = 0, 
                            SCandle = 0, 
                            SOil = 0, 
                            SMp = 0 
                        };
                    
                case -682108547:
                    return new VehicleInfoData {
                        DisplayName = "Zorrusso",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 12, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case -1620126302:
                    return new VehicleInfoData {
                        DisplayName = "Neo",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 80000, 
                        StockFull = 220000, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 12, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };                

                case -941272559:
                    return new VehicleInfoData {
                        DisplayName = "Locust",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    }; 

                case -2048333973:
                    return new VehicleInfoData {
                        DisplayName = "Italigtb",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 80000, 
                        StockFull = 220000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 11, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -482719877:
                    return new VehicleInfoData {
                        DisplayName = "Italigtb2",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 80000, 
                        StockFull = 230000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 13, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 917809321:
                    return new VehicleInfoData {
                        DisplayName = "XA21",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 36000, 
                        StockFull = 210000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 11, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case -16948145:
                    return new VehicleInfoData {
                        DisplayName = "Bison",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 1944000, 
                        StockFull = 800000, 
                        StockItem = 0,
                        FullFuel = 87, 
                        FuelMinute = 12, 
                        Fuel = 87, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2072156101:
                    return new VehicleInfoData {
                        DisplayName = "Bison2",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 1620000, 
                        StockFull = 750000, 
                        StockItem = 0,
                        FullFuel = 87, 
                        FuelMinute = 12, 
                        Fuel = 87, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1739845664:
                    return new VehicleInfoData {
                        DisplayName = "Bison3",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 1296000, 
                        StockFull = 650000, 
                        StockItem = 0,
                        FullFuel = 87, 
                        FuelMinute = 12, 
                        Fuel = 87, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1069929536:
                    return new VehicleInfoData {
                        DisplayName = "BobcatXL",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 2880000, 
                        StockFull = 705000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 12, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1987130134:
                    return new VehicleInfoData {
                        DisplayName = "Boxville",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 9180000, 
                        StockFull = 1400000, 
                        StockItem = 0,
                        FullFuel = 105, 
                        FuelMinute = 12, 
                        Fuel = 105, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -233098306:
                    return new VehicleInfoData {
                        DisplayName = "Boxville2",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 9180000, 
                        StockFull = 1400000, 
                        StockItem = 0,
                        FullFuel = 105, 
                        FuelMinute = 12, 
                        Fuel = 105, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 121658888:
                    return new VehicleInfoData {
                        DisplayName = "Boxville3",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 8721000, 
                        StockFull = 1250000, 
                        StockItem = 0,
                        FullFuel = 105, 
                        FuelMinute = 12, 
                        Fuel = 105, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 444171386:
                    return new VehicleInfoData {
                        DisplayName = "Boxville4",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 9180000, 
                        StockFull = 1400000, 
                        StockItem = 0,
                        FullFuel = 105, 
                        FuelMinute = 12, 
                        Fuel = 105, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 682434785:
                    return new VehicleInfoData {
                        DisplayName = "Boxville5",
                        ClassName = "Vans", 
                        IsBroke = true, 
                        Stock = 7803000, 
                        StockFull = 1155000, 
                        StockItem = 0,
                        FullFuel = 105, 
                        FuelMinute = 15, 
                        Fuel = 105, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1346687836:
                    return new VehicleInfoData {
                        DisplayName = "Burrito",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 5593000, 
                        StockFull = 950000, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 9, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -907477130:
                    return new VehicleInfoData {
                        DisplayName = "Burrito2",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 5146000, 
                        StockFull = 900000, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 9, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1743316013:
                    return new VehicleInfoData {
                        DisplayName = "Burrito3",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 5593000, 
                        StockFull = 950000, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 9, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 893081117:
                    return new VehicleInfoData {
                        DisplayName = "Burrito4",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 5593000, 
                        StockFull = 950000, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 9, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1132262048:
                    return new VehicleInfoData {
                        DisplayName = "Burrito5",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 5593000, 
                        StockFull = 950000, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 9, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1876516712:
                    return new VehicleInfoData {
                        DisplayName = "Camper",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 18000000, 
                        StockFull = 700000, 
                        StockItem = 0,
                        FullFuel = 105, 
                        FuelMinute = 12, 
                        Fuel = 105, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1745203402:
                    return new VehicleInfoData {
                        DisplayName = "GBurrito",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 5593000, 
                        StockFull = 1100000, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 10, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 296357396:
                    return new VehicleInfoData {
                        DisplayName = "GBurrito2",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 5593000, 
                        StockFull = 1100000, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 10, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -120287622:
                    return new VehicleInfoData {
                        DisplayName = "Journey",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 9568000, 
                        StockFull = 390000, 
                        StockItem = 0,
                        FullFuel = 92, 
                        FuelMinute = 15, 
                        Fuel = 92, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -310465116:
                    return new VehicleInfoData {
                        DisplayName = "Minivan",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 1254000, 
                        StockFull = 345000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 9, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1126264336:
                    return new VehicleInfoData {
                        DisplayName = "Minivan2",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 1191000, 
                        StockFull = 335000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 9, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1488164764:
                    return new VehicleInfoData {
                        DisplayName = "Paradise",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 5300000, 
                        StockFull = 995000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 11, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -119658072:
                    return new VehicleInfoData {
                        DisplayName = "Pony",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 5150000, 
                        StockFull = 1025000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 12, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 943752001:
                    return new VehicleInfoData {
                        DisplayName = "Pony2",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 5150000, 
                        StockFull = 1025000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 12, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1162065741:
                    return new VehicleInfoData {
                        DisplayName = "Rumpo",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 4505000, 
                        StockFull = 1055000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 12, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1776615689:
                    return new VehicleInfoData {
                        DisplayName = "Rumpo2",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 4505000, 
                        StockFull = 1055000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 12, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1475773103:
                    return new VehicleInfoData {
                        DisplayName = "Rumpo3",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 2835000, 
                        StockFull = 1395000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 15, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -810318068:
                    return new VehicleInfoData {
                        DisplayName = "Speedo",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 4800000, 
                        StockFull = 905000, 
                        StockItem = 0,
                        FullFuel = 92, 
                        FuelMinute = 11, 
                        Fuel = 92, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 728614474:
                    return new VehicleInfoData {
                        DisplayName = "Speedo2",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 4800000, 
                        StockFull = 905000, 
                        StockItem = 0,
                        FullFuel = 92, 
                        FuelMinute = 11, 
                        Fuel = 92, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 524266990:
                    return new VehicleInfoData {
                        DisplayName = "Speedo3",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 4800000, 
                        StockFull = 905000, 
                        StockItem = 0,
                        FullFuel = 92, 
                        FuelMinute = 11, 
                        Fuel = 92, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 699456151:
                    return new VehicleInfoData {
                        DisplayName = "Surfer",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 5020000, 
                        StockFull = 390000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 9, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1311240698:
                    return new VehicleInfoData {
                        DisplayName = "Surfer2",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 5020000, 
                        StockFull = 170000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 13, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1951180813:
                    return new VehicleInfoData {
                        DisplayName = "Taco",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 1166000, 
                        StockFull = 1150000, 
                        StockItem = 0,
                        FullFuel = 92, 
                        FuelMinute = 13, 
                        Fuel = 92, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 65402552:
                    return new VehicleInfoData {
                        DisplayName = "Youga",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 4100000, 
                        StockFull = 795000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 11, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 486160351:
                    return new VehicleInfoData {
                        DisplayName = "Contender8",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 2100000, 
                        StockFull = 495000, 
                        StockItem = 0,
                        FullFuel = 110, 
                        FuelMinute = 15, 
                        Fuel = 110, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1026149675:
                    return new VehicleInfoData {
                        DisplayName = "Youga2",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 3200000, 
                        StockFull = 865000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 13, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1033245328:
                    return new VehicleInfoData {
                        DisplayName = "Dinghy",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 60, 
                        FuelMinute = 6, 
                        Fuel = 60, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 276773164:
                    return new VehicleInfoData {
                        DisplayName = "Dinghy2",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 60, 
                        FuelMinute = 0, 
                        Fuel = 60, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 509498602:
                    return new VehicleInfoData {
                        DisplayName = "Dinghy3",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 60, 
                        FuelMinute = 6, 
                        Fuel = 60, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 867467158:
                    return new VehicleInfoData {
                        DisplayName = "Dinghy4",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 60, 
                        FuelMinute = 6, 
                        Fuel = 60, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 861409633:
                    return new VehicleInfoData {
                        DisplayName = "Jetmax",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 450000, 
                        StockFull = 220000, 
                        StockItem = 0,
                        FullFuel = 280, 
                        FuelMinute = 19, 
                        Fuel = 280, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1043459709:
                    return new VehicleInfoData {
                        DisplayName = "Marquis",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 1500000, 
                        StockFull = 750000, 
                        StockItem = 0,
                        FullFuel = 455, 
                        FuelMinute = 20, 
                        Fuel = 455, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1030275036:
                    return new VehicleInfoData {
                        DisplayName = "Seashark",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 30000, 
                        StockFull = 65000, 
                        StockItem = 0,
                        FullFuel = 40, 
                        FuelMinute = 4, 
                        Fuel = 40, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -616331036:
                    return new VehicleInfoData {
                        DisplayName = "Seashark2",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 30000, 
                        StockFull = 65000, 
                        StockItem = 0,
                        FullFuel = 40, 
                        FuelMinute = 4, 
                        Fuel = 40, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case -311022263:
                    return new VehicleInfoData {
                        DisplayName = "Seashark3",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 30000, 
                        StockFull = 65000, 
                        StockItem = 0,
                        FullFuel = 40, 
                        FuelMinute = 4, 
                        Fuel = 40, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 231083307:
                    return new VehicleInfoData {
                        DisplayName = "Speeder",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 380000, 
                        StockFull = 190000, 
                        StockItem = 0,
                        FullFuel = 250, 
                        FuelMinute = 19, 
                        Fuel = 250, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 437538602:
                    return new VehicleInfoData {
                        DisplayName = "Speeder2",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 380000, 
                        StockFull = 190000, 
                        StockItem = 0,
                        FullFuel = 250, 
                        FuelMinute = 19, 
                        Fuel = 250, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 400514754:
                    return new VehicleInfoData {
                        DisplayName = "Squalo",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 290000, 
                        StockFull = 175000, 
                        StockItem = 0,
                        FullFuel = 220, 
                        FuelMinute = 17, 
                        Fuel = 220, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 771711535:
                    return new VehicleInfoData {
                        DisplayName = "Submersible",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 300, 
                        FuelMinute = 23, 
                        Fuel = 300, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1066334226:
                    return new VehicleInfoData {
                        DisplayName = "Submersible2",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 350, 
                        FuelMinute = 25, 
                        Fuel = 350, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -282946103:
                    return new VehicleInfoData {
                        DisplayName = "Suntrap",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 110, 
                        FuelMinute = 10, 
                        Fuel = 110, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1070967343:
                    return new VehicleInfoData {
                        DisplayName = "Toro",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 400000, 
                        StockFull = 250000, 
                        StockItem = 0,
                        FullFuel = 230, 
                        FuelMinute = 16, 
                        Fuel = 230, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 908897389:
                    return new VehicleInfoData {
                        DisplayName = "Toro2",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 400000, 
                        StockFull = 250000, 
                        StockItem = 0,
                        FullFuel = 230, 
                        FuelMinute = 16, 
                        Fuel = 230, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 290013743:
                    return new VehicleInfoData {
                        DisplayName = "Tropic",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 250000, 
                        StockFull = 120000, 
                        StockItem = 0,
                        FullFuel = 250, 
                        FuelMinute = 14, 
                        Fuel = 250, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1448677353:
                    return new VehicleInfoData {
                        DisplayName = "Tropic2",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 250000, 
                        StockFull = 120000, 
                        StockItem = 0,
                        FullFuel = 250, 
                        FuelMinute = 14, 
                        Fuel = 250, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2100640717:
                    return new VehicleInfoData {
                        DisplayName = "Tug",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 54000000, 
                        StockFull = 2350000, 
                        StockItem = 0,
                        FullFuel = 1200, 
                        FuelMinute = 90, 
                        Fuel = 1200, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -488123221:
                    return new VehicleInfoData {
                        DisplayName = "Predator",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 300000, 
                        StockFull = 160000, 
                        StockItem = 0,
                        FullFuel = 280, 
                        FuelMinute = 19, 
                        Fuel = 280, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1181327175:
                    return new VehicleInfoData {
                        DisplayName = "Akula",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 1100, 
                        FuelMinute = 79, 
                        Fuel = 1100, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 837858166:
                    return new VehicleInfoData {
                        DisplayName = "Annihilator",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 1200, 
                        FuelMinute = 85, 
                        Fuel = 1200, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 788747387:
                    return new VehicleInfoData {
                        DisplayName = "Buzzard",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 480, 
                        FuelMinute = 52, 
                        Fuel = 480, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 745926877:
                    return new VehicleInfoData {
                        DisplayName = "Buzzard2",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 480, 
                        FuelMinute = 52, 
                        Fuel = 480, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -50547061:
                    return new VehicleInfoData {
                        DisplayName = "Cargobob",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 35000000, 
                        StockFull = 1960000, 
                        StockItem = 0,
                        FullFuel = 2400, 
                        FuelMinute = 120, 
                        Fuel = 2400, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1621617168:
                    return new VehicleInfoData {
                        DisplayName = "Cargobob2",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 35000000, 
                        StockFull = 1960000, 
                        StockItem = 0,
                        FullFuel = 2400, 
                        FuelMinute = 120, 
                        Fuel = 2400, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1394036463:
                    return new VehicleInfoData {
                        DisplayName = "Cargobob3",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 35000000, 
                        StockFull = 1960000, 
                        StockItem = 0,
                        FullFuel = 2400, 
                        FuelMinute = 120, 
                        Fuel = 2400, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2025593404:
                    return new VehicleInfoData {
                        DisplayName = "Cargobob4",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 35000000, 
                        StockFull = 1960000, 
                        StockItem = 0,
                        FullFuel = 2400, 
                        FuelMinute = 120, 
                        Fuel = 2400, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 744705981:
                    return new VehicleInfoData {
                        DisplayName = "Frogger",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 800, 
                        FuelMinute = 58, 
                        Fuel = 800, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1949211328:
                    return new VehicleInfoData {
                        DisplayName = "Frogger2",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 15, 
                        StockItem = 0,
                        FullFuel = 800, 
                        FuelMinute = 58, 
                        Fuel = 800, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1984275979:
                    return new VehicleInfoData {
                        DisplayName = "Havok",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 300, 
                        FuelMinute = 24, 
                        Fuel = 300, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -42959138:
                    return new VehicleInfoData {
                        DisplayName = "Hunter",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 1300, 
                        FuelMinute = 93, 
                        Fuel = 1300, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1660661558:
                    return new VehicleInfoData {
                        DisplayName = "Maverick",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 420, 
                        FuelMinute = 35, 
                        Fuel = 420, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -82626025:
                    return new VehicleInfoData {
                        DisplayName = "Savage",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 10, 
                        StockItem = 0,
                        FullFuel = 1400, 
                        FuelMinute = 103, 
                        Fuel = 1400, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -726768679:
                    return new VehicleInfoData {
                        DisplayName = "Seasparrow",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 469, 
                        FuelMinute = 47, 
                        Fuel = 469, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1044954915:
                    return new VehicleInfoData {
                        DisplayName = "Skylift",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 2000, 
                        FuelMinute = 110, 
                        Fuel = 2000, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 710198397:
                    return new VehicleInfoData {
                        DisplayName = "Supervolito",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 200000, 
                        StockFull = 120000, 
                        StockItem = 0,
                        FullFuel = 600, 
                        FuelMinute = 52, 
                        Fuel = 600, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1671539132:
                    return new VehicleInfoData {
                        DisplayName = "Supervolito2",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 200000, 
                        StockFull = 120000, 
                        StockItem = 0,
                        FullFuel = 600, 
                        FuelMinute = 52, 
                        Fuel = 600, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -339587598:
                    return new VehicleInfoData {
                        DisplayName = "Swift",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 320000, 
                        StockFull = 150000, 
                        StockItem = 0,
                        FullFuel = 750, 
                        FuelMinute = 61, 
                        Fuel = 750, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1075432268:
                    return new VehicleInfoData {
                        DisplayName = "Swift2",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 320000, 
                        StockFull = 150000, 
                        StockItem = 0,
                        FullFuel = 750, 
                        FuelMinute = 61, 
                        Fuel = 750, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1600252419:
                    return new VehicleInfoData {
                        DisplayName = "Valkyrie",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 600, 
                        FuelMinute = 52, 
                        Fuel = 600, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1543134283:
                    return new VehicleInfoData {
                        DisplayName = "Valkyrie2",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 600, 
                        FuelMinute = 52, 
                        Fuel = 600, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1845487887:
                    return new VehicleInfoData {
                        DisplayName = "Volatus",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 400000, 
                        StockFull = 250000, 
                        StockItem = 0,
                        FullFuel = 1000, 
                        FuelMinute = 74, 
                        Fuel = 1000, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 353883353:
                    return new VehicleInfoData {
                        DisplayName = "Polmav",
                        ClassName = "Helicopters", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 420, 
                        FuelMinute = 35, 
                        Fuel = 420, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1523619738:
                    return new VehicleInfoData {
                        DisplayName = "AlphaZ1",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 450, 
                        FuelMinute = 36, 
                        Fuel = 250, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2118308144:
                    return new VehicleInfoData {
                        DisplayName = "Avenger",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 70000000, 
                        StockFull = 3550000, 
                        StockItem = 0,
                        FullFuel = 5200, 
                        FuelMinute = 260, 
                        Fuel = 5200, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 408970549:
                    return new VehicleInfoData {
                        DisplayName = "Avenger2",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 70000000, 
                        StockFull = 3550000, 
                        StockItem = 0,
                        FullFuel = 5200, 
                        FuelMinute = 260, 
                        Fuel = 5200, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1824333165:
                    return new VehicleInfoData {
                        DisplayName = "Besra",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 100000, 
                        StockFull = 120000, 
                        StockItem = 0,
                        FullFuel = 1100, 
                        FuelMinute = 75, 
                        Fuel = 1100, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -150975354:
                    return new VehicleInfoData {
                        DisplayName = "Blimp",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 700000, 
                        StockFull = 190000, 
                        StockItem = 0,
                        FullFuel = 280, 
                        FuelMinute = 30, 
                        Fuel = 280, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -613725916:
                    return new VehicleInfoData {
                        DisplayName = "Blimp2",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 700000, 
                        StockFull = 190000, 
                        StockItem = 0,
                        FullFuel = 280, 
                        FuelMinute = 30, 
                        Fuel = 280, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -32878452:
                    return new VehicleInfoData {
                        DisplayName = "Bombushka",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 45000000, 
                        StockFull = 3900000, 
                        StockItem = 0,
                        FullFuel = 3500, 
                        FuelMinute = 215, 
                        Fuel = 3500, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 368211810:
                    return new VehicleInfoData {
                        DisplayName = "CargoPlane",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 250000000, 
                        StockFull = 10500000, 
                        StockItem = 0,
                        FullFuel = 7000, 
                        FuelMinute = 410, 
                        Fuel = 7000, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -644710429:
                    return new VehicleInfoData {
                        DisplayName = "Cuban800",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 300000, 
                        StockFull = 245000, 
                        StockItem = 0,
                        FullFuel = 850, 
                        FuelMinute = 35, 
                        Fuel = 850, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -901163259:
                    return new VehicleInfoData {
                        DisplayName = "Dodo",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 1200000, 
                        StockFull = 450000, 
                        StockItem = 0,
                        FullFuel = 760, 
                        FuelMinute = 48, 
                        Fuel = 760, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 970356638:
                    return new VehicleInfoData {
                        DisplayName = "Duster",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 340, 
                        FuelMinute = 45, 
                        Fuel = 340, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1007528109:
                    return new VehicleInfoData {
                        DisplayName = "Howard",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 280, 
                        FuelMinute = 35, 
                        Fuel = 280, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 970385471:
                    return new VehicleInfoData {
                        DisplayName = "Hydra",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 2200, 
                        FuelMinute = 265, 
                        Fuel = 2200, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1058115860:
                    return new VehicleInfoData {
                        DisplayName = "Jet",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 3100, 
                        FuelMinute = 280, 
                        Fuel = 3100, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1281684762:
                    return new VehicleInfoData {
                        DisplayName = "Lazer",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 2400, 
                        FuelMinute = 285, 
                        Fuel = 2400, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 621481054:
                    return new VehicleInfoData {
                        DisplayName = "Luxor",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 400000, 
                        StockFull = 1350000, 
                        StockItem = 0,
                        FullFuel = 1200, 
                        FuelMinute = 65, 
                        Fuel = 1200, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1214293858:
                    return new VehicleInfoData {
                        DisplayName = "Luxor2",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 400000, 
                        StockFull = 1350000, 
                        StockItem = 0,
                        FullFuel = 1200, 
                        FuelMinute = 65, 
                        Fuel = 1200, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1746576111:
                    return new VehicleInfoData {
                        DisplayName = "Mammatus",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 250, 
                        FuelMinute = 16, 
                        Fuel = 250, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1763555241:
                    return new VehicleInfoData {
                        DisplayName = "Microlight",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 140, 
                        FuelMinute = 6, 
                        Fuel = 140, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 165154707:
                    return new VehicleInfoData {
                        DisplayName = "Miljet",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 1400000, 
                        StockFull = 700000, 
                        StockItem = 0,
                        FullFuel = 3100, 
                        FuelMinute = 180, 
                        Fuel = 3100, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -749299473:
                    return new VehicleInfoData {
                        DisplayName = "Mogul",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 1200000, 
                        StockFull = 550000, 
                        StockItem = 0,
                        FullFuel = 650, 
                        FuelMinute = 43, 
                        Fuel = 650, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1565978651:
                    return new VehicleInfoData {
                        DisplayName = "Molotok",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 580, 
                        FuelMinute = 42, 
                        Fuel = 580, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1295027632:
                    return new VehicleInfoData {
                        DisplayName = "Nimbus",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 900000, 
                        StockFull = 1695000, 
                        StockItem = 0,
                        FullFuel = 950, 
                        FuelMinute = 54, 
                        Fuel = 950, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1036591958:
                    return new VehicleInfoData {
                        DisplayName = "Nokota",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 450, 
                        FuelMinute = 31, 
                        Fuel = 450, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1386191424:
                    return new VehicleInfoData {
                        DisplayName = "Pyro",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 560, 
                        FuelMinute = 42, 
                        Fuel = 560, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -975345305:
                    return new VehicleInfoData {
                        DisplayName = "Rogue",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 540, 
                        FuelMinute = 26, 
                        Fuel = 540, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -392675425:
                    return new VehicleInfoData {
                        DisplayName = "Seabreeze",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 150000, 
                        StockFull = 210000, 
                        StockItem = 0,
                        FullFuel = 560, 
                        FuelMinute = 23, 
                        Fuel = 560, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1214505995:
                    return new VehicleInfoData {
                        DisplayName = "Shamal",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 550000, 
                        StockFull = 890000, 
                        StockItem = 0,
                        FullFuel = 980, 
                        FuelMinute = 58, 
                        Fuel = 980, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1700874274:
                    return new VehicleInfoData {
                        DisplayName = "Starling",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 50000, 
                        StockFull = 95000, 
                        StockItem = 0,
                        FullFuel = 520, 
                        FuelMinute = 42, 
                        Fuel = 520, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2122757008:
                    return new VehicleInfoData {
                        DisplayName = "Stunt",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 310, 
                        FuelMinute = 34, 
                        Fuel = 310, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1981688531:
                    return new VehicleInfoData {
                        DisplayName = "Titan",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 70000000, 
                        StockFull = 4590000, 
                        StockItem = 0,
                        FullFuel = 4500, 
                        FuelMinute = 260, 
                        Fuel = 4500, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1043222410:
                    return new VehicleInfoData {
                        DisplayName = "Tula",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 1600000, 
                        StockFull = 690000, 
                        StockItem = 0,
                        FullFuel = 1590, 
                        FuelMinute = 138, 
                        Fuel = 1590, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1673356438:
                    return new VehicleInfoData {
                        DisplayName = "Velum",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 250000, 
                        StockFull = 440000, 
                        StockItem = 0,
                        FullFuel = 600, 
                        FuelMinute = 39, 
                        Fuel = 600, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1077420264:
                    return new VehicleInfoData {
                        DisplayName = "Velum2",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 350000, 
                        StockFull = 440000, 
                        StockItem = 0,
                        FullFuel = 600, 
                        FuelMinute = 39, 
                        Fuel = 600, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1341619767:
                    return new VehicleInfoData {
                        DisplayName = "Vestra",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 220000, 
                        StockFull = 190000, 
                        StockItem = 0,
                        FullFuel = 440, 
                        FuelMinute = 32, 
                        Fuel = 440, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 447548909:
                    return new VehicleInfoData {
                        DisplayName = "Vestra",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 6440, 
                        FuelMinute = 232, 
                        Fuel = 6440, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1171614426:
                    return new VehicleInfoData {
                        DisplayName = "Ambulance",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 4590000, 
                        StockFull = 1950000, 
                        StockItem = 0,
                        FullFuel = 86, 
                        FuelMinute = 9, 
                        Fuel = 86, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1127131465:
                    return new VehicleInfoData {
                        DisplayName = "FBI",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 270000, 
                        StockFull = 240000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1647941228:
                    return new VehicleInfoData {
                        DisplayName = "FBI2",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 2199000, 
                        StockFull = 590000, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 12, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1938952078:
                    return new VehicleInfoData {
                        DisplayName = "FireTruck",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 93, 
                        FuelMinute = 9,
                        Fuel = 93, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2007026063:
                    return new VehicleInfoData {
                        DisplayName = "PBus",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 1445000, 
                        StockFull = 1650000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 12, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2046537925:
                    return new VehicleInfoData {
                        DisplayName = "Police",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 378000, 
                        StockFull = 205000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 8, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1627000575:
                    return new VehicleInfoData {
                        DisplayName = "Police2",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 270000, 
                        StockFull = 270000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
					
                case -2131287627:
                    return new VehicleInfoData {
                        DisplayName = "Hwaycoq4",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 15, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                case -1061320731:
                    return new VehicleInfoData {
                        DisplayName = "Lssheriffcoq4",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 15, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };

                case 1268703337:
                    return new VehicleInfoData {
                        DisplayName = "Policecoq4",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 15, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
					
                case 930078522:
                    return new VehicleInfoData {
                        DisplayName = "Policecoq42",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 15, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
            
                case 2052401827:
                    return new VehicleInfoData {
                        DisplayName = "Hwaycoq42",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 15, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                    
                case 1698774573:
                    return new VehicleInfoData {
                        DisplayName = "Hwaybul",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 15, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                    
                case 880915229:
                    return new VehicleInfoData {
                        DisplayName = "Hwaybul2",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 15, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
					
                case 1075324164:
                    return new VehicleInfoData {
                        DisplayName = "Lssheriffbul",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 15, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
					
                case -456772041:
                    return new VehicleInfoData {
                        DisplayName = "Policebul",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 15, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
					
                case -278760861:
                    return new VehicleInfoData {
                        DisplayName = "Policebul2",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 15, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
					
                case -50950777:
                    return new VehicleInfoData {
                        DisplayName = "Policebul3",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 15, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1454219340:
                    return new VehicleInfoData {
                        DisplayName = "NooseBuffalo",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 270000, 
                        StockFull = 310000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -333464494:
                    return new VehicleInfoData {
                        DisplayName = "SAHPBuffalo",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 270000, 
                        StockFull = 310000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 13, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                case -590854301:
                    return new VehicleInfoData {
                        DisplayName = "PoliceBuffalo",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 270000, 
                        StockFull = 310000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                    
                case 1982188179:
                    return new VehicleInfoData {
                        DisplayName = "Scpd1 Buffalo",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 270000, 
                        StockFull = 310000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                    
                case -561505450:
                    return new VehicleInfoData {
                        DisplayName = "Scpd4 Vacca",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 60000, 
                        StockFull = 240000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 11, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                    
                case -1286617882:
                    return new VehicleInfoData {
                        DisplayName = "Scpd5 Rocoto",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 680000, 
                        StockFull = 435000, 
                        StockItem = 0,
                        FullFuel = 100, 
                        FuelMinute = 10, 
                        Fuel = 100, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                    
                case -118239187:
                    return new VehicleInfoData {
                        DisplayName = "Scpd6 Sentinel",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 720000, 
                        StockFull = 150000, 
                        StockItem = 0,
                        FullFuel = 63, 
                        FuelMinute = 7, 
                        Fuel = 63, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                    
                case -271532569:
                    return new VehicleInfoData {
                        DisplayName = "Scpd7 Elegy2",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 220000, 
                        StockFull = 235000, 
                        StockItem = 0,
                        FullFuel = 74, 
                        FuelMinute = 10, 
                        Fuel = 74, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 376094636:
                    return new VehicleInfoData {
                        DisplayName = "Scpd10 Felon",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 330000, 
                        StockFull = 140000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 9, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -660061144:
                    return new VehicleInfoData {
                        DisplayName = "Scpd11 F620",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 160000, 
                        StockFull = 160000, 
                        StockItem = 0,
                        FullFuel = 71, 
                        FuelMinute = 9, 
                        Fuel = 71, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1912215274:
                    return new VehicleInfoData {
                        DisplayName = "Police3",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 280000, 
                        StockFull = 225000, 
                        StockItem = 0,
                        FullFuel = 63, 
                        FuelMinute = 8, 
                        Fuel = 63, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1973172295:
                    return new VehicleInfoData {
                        DisplayName = "Police4",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 378000, 
                        StockFull = 200000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 8, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1536924937:
                    return new VehicleInfoData {
                        DisplayName = "PoliceOld1",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 1822000, 
                        StockFull = 375000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 11, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1779120616:
                    return new VehicleInfoData {
                        DisplayName = "PoliceOld2",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 269000, 
                        StockFull = 160000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 3, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 456714581:
                    return new VehicleInfoData {
                        DisplayName = "PoliceT",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 5300000, 
                        StockFull = 980000, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 13, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -34623805:
                    return new VehicleInfoData {
                        DisplayName = "Policeb",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 30000, 
                        StockFull = 40000, 
                        StockItem = 0,
                        FullFuel = 45, 
                        FuelMinute = 5, 
                        Fuel = 45, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 741586030:
                    return new VehicleInfoData {
                        DisplayName = "Pranger",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 2199000, 
                        StockFull = 550000, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 12, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1205689942:
                    return new VehicleInfoData {
                        DisplayName = "Riot",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 7200000, 
                        StockFull = 2350000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 10, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1693015116:
                    return new VehicleInfoData {
                        DisplayName = "Riot2",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 120, 
                        FuelMinute = 15, 
                        Fuel = 120, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1683328900:
                    return new VehicleInfoData {
                        DisplayName = "Sheriff",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 378000, 
                        StockFull = 205000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 8, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1922257928:
                    return new VehicleInfoData {
                        DisplayName = "Sheriff2",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 2199000, 
                        StockFull = 550000, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 12, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2053223216:
                    return new VehicleInfoData {
                        DisplayName = "Benson",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 25600000, 
                        StockFull = 4550000, 
                        StockItem = 0,
                        FullFuel = 146, 
                        FuelMinute = 13, 
                        Fuel = 146, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 850991848:
                    return new VehicleInfoData {
                        DisplayName = "Biff",
                        ClassName = "Commercials", 
                        IsBroke = true, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 122, 
                        FuelMinute = 12, 
                        Fuel = 122, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1518533038:
                    return new VehicleInfoData {
                        DisplayName = "Hauler",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 140, 
                        FuelMinute = 13, 
                        Fuel = 140, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 387748548:
                    return new VehicleInfoData {
                        DisplayName = "Hauler2",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 140, 
                        FuelMinute = 13, 
                        Fuel = 140, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 904750859:
                    return new VehicleInfoData {
                        DisplayName = "Mule",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 21000000, 
                        StockFull = 3450000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 11, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1324550758:
                    return new VehicleInfoData {
                        DisplayName = "Rumpobox",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 13500000, 
                        StockFull = 2250000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 11, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1050465301:
                    return new VehicleInfoData {
                        DisplayName = "Mule2",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 20300000, 
                        StockFull = 3350000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 11, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2052737935:
                    return new VehicleInfoData {
                        DisplayName = "Mule3",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 20300000, 
                        StockFull = 3350000, 
                        StockItem = 0,
                        FullFuel = 110, 
                        FuelMinute = 12, 
                        Fuel = 110, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 569305213:
                    return new VehicleInfoData {
                        DisplayName = "Packer",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 150, 
                        FuelMinute = 14, 
                        Fuel = 150, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2137348917:
                    return new VehicleInfoData {
                        DisplayName = "Phantom",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 180, 
                        FuelMinute = 14, 
                        Fuel = 180, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1827997487:
                    return new VehicleInfoData {
                        DisplayName = "Roadkiller",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 190, 
                        FuelMinute = 14, 
                        Fuel = 190, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1649536104:
                    return new VehicleInfoData {
                        DisplayName = "Phantom2",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 180, 
                        FuelMinute = 14, 
                        Fuel = 180, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 177270108:
                    return new VehicleInfoData {
                        DisplayName = "Phantom3",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 190, 
                        FuelMinute = 14, 
                        Fuel = 190, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2112052861:
                    return new VehicleInfoData {
                        DisplayName = "Pounder",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 30000000, 
                        StockFull = 5900000, 
                        StockItem = 0,
                        FullFuel = 152, 
                        FuelMinute = 13, 
                        Fuel = 152, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1747439474:
                    return new VehicleInfoData {
                        DisplayName = "Stockade",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 7200000, 
                        StockFull = 3650000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 10, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -214455498:
                    return new VehicleInfoData {
                        DisplayName = "Stockade3",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 7200000, 
                        StockFull = 3650000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 10, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1886712733:
                    return new VehicleInfoData {
                        DisplayName = "Bulldozer",
                        ClassName = "Industrial", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 112, 
                        FuelMinute = 15, 
                        Fuel = 112, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1006919392:
                    return new VehicleInfoData {
                        DisplayName = "Cutter",
                        ClassName = "Industrial", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 60, 
                        FuelMinute = 9, 
                        Fuel = 60, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2130482718:
                    return new VehicleInfoData {
                        DisplayName = "Dump",
                        ClassName = "Industrial", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 220, 
                        FuelMinute = 35, 
                        Fuel = 220, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1353720154:
                    return new VehicleInfoData {
                        DisplayName = "Flatbed",
                        ClassName = "Industrial", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 180, 
                        FuelMinute = 14, 
                        Fuel = 180, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2107990196:
                    return new VehicleInfoData {
                        DisplayName = "Guardian",
                        ClassName = "Industrial", 
                        IsBroke = false, 
                        Stock = 2900000, 
                        StockFull = 1650000, 
                        StockItem = 0,
                        FullFuel = 180, 
                        FuelMinute = 22, 
                        Fuel = 180, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 444583674:
                    return new VehicleInfoData {
                        DisplayName = "Handler",
                        ClassName = "Industrial", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 62, 
                        FuelMinute = 6, 
                        Fuel = 62, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -784816453:
                    return new VehicleInfoData {
                        DisplayName = "Mixer",
                        ClassName = "Industrial", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 15, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 475220373:
                    return new VehicleInfoData {
                        DisplayName = "Mixer2",
                        ClassName = "Industrial", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 15, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1705304628:
                    return new VehicleInfoData {
                        DisplayName = "Rubble",
                        ClassName = "Industrial", 
                        IsBroke = true, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 120, 
                        FuelMinute = 15, 
                        Fuel = 120, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 48339065:
                    return new VehicleInfoData {
                        DisplayName = "TipTruck",
                        ClassName = "Industrial", 
                        IsBroke = true, 
                        Stock = 13602000, 
                        StockFull = 2350000, 
                        StockItem = 0,
                        FullFuel = 125, 
                        FuelMinute = 15, 
                        Fuel = 125, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -947761570:
                    return new VehicleInfoData {
                        DisplayName = "TipTruck2",
                        ClassName = "Industrial", 
                        IsBroke = true, 
                        Stock = 9060000, 
                        StockFull = 2015000, 
                        StockItem = 0,
                        FullFuel = 125, 
                        FuelMinute = 15, 
                        Fuel = 125, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 562680400:
                    return new VehicleInfoData {
                        DisplayName = "APC",
                        ClassName = "Military", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 92, 
                        FuelMinute = 9, 
                        Fuel = 92, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -823509173:
                    return new VehicleInfoData {
                        DisplayName = "Barracks",
                        ClassName = "Military", 
                        IsBroke = false, 
                        Stock = 9000000, 
                        StockFull = 2550000, 
                        StockItem = 0,
                        FullFuel = 150, 
                        FuelMinute = 20, 
                        Fuel = 150, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1074326203:
                    return new VehicleInfoData {
                        DisplayName = "Barracks2",
                        ClassName = "Military", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 150, 
                        FuelMinute = 20, 
                        Fuel = 150, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 630371791:
                    return new VehicleInfoData {
                        DisplayName = "Barracks3",
                        ClassName = "Military", 
                        IsBroke = false, 
                        Stock = 9000000, 
                        StockFull = 2550000, 
                        StockItem = 0,
                        FullFuel = 150, 
                        FuelMinute = 20, 
                        Fuel = 150, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -212993243:
                    return new VehicleInfoData {
                        DisplayName = "Barrage",
                        ClassName = "Military", 
                        IsBroke = false, 
                        Stock = 504000, 
                        StockFull = 430000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 12, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -692292317:
                    return new VehicleInfoData {
                        DisplayName = "Chernobog",
                        ClassName = "Military", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 250, 
                        FuelMinute = 25, 
                        Fuel = 250, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 321739290:
                    return new VehicleInfoData {
                        DisplayName = "Crusader",
                        ClassName = "Military", 
                        IsBroke = false, 
                        Stock = 750000, 
                        StockFull = 240000, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 10, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -32236122:
                    return new VehicleInfoData {
                        DisplayName = "Halftrack",
                        ClassName = "Military", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1435527158:
                    return new VehicleInfoData {
                        DisplayName = "Khanjali",
                        ClassName = "Military", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 320, 
                        FuelMinute = 40, 
                        Fuel = 320, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 782665360:
                    return new VehicleInfoData {
                        DisplayName = "Rhino",
                        ClassName = "Military", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 280, 
                        FuelMinute = 30, 
                        Fuel = 280, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1489874736:
                    return new VehicleInfoData {
                        DisplayName = "Thruster",
                        ClassName = "Military", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 65, 
                        FuelMinute = 15, 
                        Fuel = 65, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1881846085:
                    return new VehicleInfoData {
                        DisplayName = "Trailersmall2",
                        ClassName = "Military", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1283517198:
                    return new VehicleInfoData {
                        DisplayName = "Airbus",
                        ClassName = "Service", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 110, 
                        FuelMinute = 13, 
                        Fuel = 110, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -305727417:
                    return new VehicleInfoData {
                        DisplayName = "Brickade",
                        ClassName = "Service", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 188, 
                        FuelMinute = 22, 
                        Fuel = 188, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -713569950:
                    return new VehicleInfoData {
                        DisplayName = "Bus",
                        ClassName = "Service", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 110, 
                        FuelMinute = 13, 
                        Fuel = 110, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2072933068:
                    return new VehicleInfoData {
                        DisplayName = "Coach",
                        ClassName = "Service", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 130, 
                        FuelMinute = 14, 
                        Fuel = 130, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2103821244:
                    return new VehicleInfoData {
                        DisplayName = "Rallytruck",
                        ClassName = "Service", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 165, 
                        FuelMinute = 18, 
                        Fuel = 165, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1098802077:
                    return new VehicleInfoData {
                        DisplayName = "RentalBus",
                        ClassName = "Service", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 9, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -956048545:
                    return new VehicleInfoData {
                        DisplayName = "Taxi",
                        ClassName = "Service", 
                        IsBroke = false, 
                        Stock = 178000, 
                        StockFull = 165000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 8, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1941029835:
                    return new VehicleInfoData {
                        DisplayName = "Tourbus",
                        ClassName = "Service", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 9, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1917016601:
                    return new VehicleInfoData {
                        DisplayName = "Trash",
                        ClassName = "Service", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 12, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1255698084:
                    return new VehicleInfoData {
                        DisplayName = "Trash2",
                        ClassName = "Service", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 12, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1912017790:
                    return new VehicleInfoData {
                        DisplayName = "Wastlndr",
                        ClassName = "Service", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 140, 
                        FuelMinute = 16, 
                        Fuel = 140, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1207431159:
                    return new VehicleInfoData {
                        DisplayName = "ArmyTanker",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1476447243:
                    return new VehicleInfoData {
                        DisplayName = "ArmyTrailer",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1637149482:
                    return new VehicleInfoData {
                        DisplayName = "ArmyTrailer2",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -399841706:
                    return new VehicleInfoData {
                        DisplayName = "BaleTrailer",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 524108981:
                    return new VehicleInfoData {
                        DisplayName = "BoatTrailer",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -960289747:
                    return new VehicleInfoData {
                        DisplayName = "CableCar",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2140210194:
                    return new VehicleInfoData {
                        DisplayName = "DockTrailer",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 62000000, 
                        StockFull = 7200000, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1019737494:
                    return new VehicleInfoData {
                        DisplayName = "GrainTrailer",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 356391690:
                    return new VehicleInfoData {
                        DisplayName = "PropTrailer",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 390902130:
                    return new VehicleInfoData {
                        DisplayName = "RakeTrailer",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2078290630:
                    return new VehicleInfoData {
                        DisplayName = "TR2",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1784254509:
                    return new VehicleInfoData {
                        DisplayName = "TR3",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                    case 2091594960:
                    return new VehicleInfoData {
                        DisplayName = "TR4",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1352468814:
                    return new VehicleInfoData {
                        DisplayName = "TRFlat",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1770643266:
                    return new VehicleInfoData {
                        DisplayName = "TVTrailer",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 62000000, 
                        StockFull = 7200000, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -730904777:
                    return new VehicleInfoData {
                        DisplayName = "Tanker",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1956216962:
                    return new VehicleInfoData {
                        DisplayName = "Tanker2",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2016027501:
                    return new VehicleInfoData {
                        DisplayName = "TrailerLogs",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 712162987:
                    return new VehicleInfoData {
                        DisplayName = "TrailerSmall",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -877478386:
                    return new VehicleInfoData {
                        DisplayName = "Trailers",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 62000000, 
                        StockFull = 7200000, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1579533167:
                    return new VehicleInfoData {
                        DisplayName = "Trailers2",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 62000000, 
                        StockFull = 7200000, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2058878099:
                    return new VehicleInfoData {
                        DisplayName = "Trailers3",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 70000000, 
                        StockFull = 6400000, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1502869817:
                    return new VehicleInfoData {
                        DisplayName = "TrailerLarge",
                        ClassName = "Trailer", 
                        IsBroke = false, 
                        Stock = 40000000, 
                        StockFull = 6500000, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1560980623:
                    return new VehicleInfoData {
                        DisplayName = "Airtug",
                        ClassName = "Utility", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 40, 
                        FuelMinute = 4, 
                        Fuel = 40, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1147287684:
                    return new VehicleInfoData {
                        DisplayName = "Caddy",
                        ClassName = "Utility", 
                        IsBroke = false, 
                        Stock = 75000, 
                        StockFull = 95000, 
                        StockItem = 0,
                        FullFuel = 20, 
                        FuelMinute = 4, 
                        Fuel = 20, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -537896628:
                    return new VehicleInfoData {
                        DisplayName = "Caddy2",
                        ClassName = "Utility", 
                        IsBroke = false, 
                        Stock = 75000, 
                        StockFull = 45000, 
                        StockItem = 0,
                        FullFuel = 20, 
                        FuelMinute = 4, 
                        Fuel = 20, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -769147461:
                    return new VehicleInfoData {
                        DisplayName = "Caddy3",
                        ClassName = "Utility", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 20, 
                        FuelMinute = 4, 
                        Fuel = 20, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -884690486:
                    return new VehicleInfoData {
                        DisplayName = "Docktug",
                        ClassName = "Utility", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 30, 
                        FuelMinute = 5, 
                        Fuel = 30, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1491375716:
                    return new VehicleInfoData {
                        DisplayName = "Forklift",
                        ClassName = "Utility", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 22, 
                        FuelMinute = 4, 
                        Fuel = 22, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1783355638:
                    return new VehicleInfoData {
                        DisplayName = "Mower",
                        ClassName = "Utility", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 22, 
                        FuelMinute = 3, 
                        Fuel = 22, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -845979911:
                    return new VehicleInfoData {
                        DisplayName = "Ripley",
                        ClassName = "Utility", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 1, 
                        StockItem = 0,
                        FullFuel = 244, 
                        FuelMinute = 25, 
                        Fuel = 244, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -599568815:
                    return new VehicleInfoData {
                        DisplayName = "Sadler",
                        ClassName = "Utility", 
                        IsBroke = false, 
                        Stock = 840000, 
                        StockFull = 720000, 
                        StockItem = 0,
                        FullFuel = 87, 
                        FuelMinute = 12, 
                        Fuel = 87, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1700801569:
                    return new VehicleInfoData {
                        DisplayName = "Scrap",
                        ClassName = "Utility", 
                        IsBroke = true, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 120, 
                        FuelMinute = 12, 
                        Fuel = 120, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1323100960:
                    return new VehicleInfoData {
                        DisplayName = "TowTruck",
                        ClassName = "Utility", 
                        IsBroke = true, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 12, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -442313018:
                    return new VehicleInfoData {
                        DisplayName = "TowTruck2",
                        ClassName = "Utility", 
                        IsBroke = true, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1641462412:
                    return new VehicleInfoData {
                        DisplayName = "Tractor",
                        ClassName = "Utility", 
                        IsBroke = true, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 42, 
                        FuelMinute = 6, 
                        Fuel = 42, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 3, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2076478498:
                    return new VehicleInfoData {
                        DisplayName = "Tractor2",
                        ClassName = "Utility", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 136, 
                        FuelMinute = 14, 
                        Fuel = 136, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1445631933:
                    return new VehicleInfoData {
                        DisplayName = "Tractor3",
                        ClassName = "Utility", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 136, 
                        FuelMinute = 14, 
                        Fuel = 136, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 516990260:
                    return new VehicleInfoData {
                        DisplayName = "UtilliTruck",
                        ClassName = "Utility", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 96, 
                        FuelMinute = 12, 
                        Fuel = 96, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2132890591:
                    return new VehicleInfoData {
                        DisplayName = "UtilliTruck3",
                        ClassName = "Utility", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 87, 
                        FuelMinute = 12, 
                        Fuel = 87, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 887537515:
                    return new VehicleInfoData {
                        DisplayName = "UtilliTruck2",
                        ClassName = "Utility", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 94, 
                        FuelMinute = 12, 
                        Fuel = 94, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case 1012102587:
                    return new VehicleInfoData {
                        DisplayName = "Police22",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 270000, 
                        StockFull = 325000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case -1051863850:
                    return new VehicleInfoData {
                        DisplayName = "Coroner2",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 3855000, 
                        StockFull = 1055000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 12, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case -1674384553:
                    return new VehicleInfoData {
                        DisplayName = "Police5",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 585000, 
                        StockFull = 420000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case -454630943:
                    return new VehicleInfoData {
                        DisplayName = "Alamo",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 1499000, 
                        StockFull = 460000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 12, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case -894568490:
                    return new VehicleInfoData {
                        DisplayName = "Alamo3",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 1499000, 
                        StockFull = 460000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 12, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case -604300688:
                    return new VehicleInfoData {
                        DisplayName = "Alamo4",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 1499000, 
                        StockFull = 460000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 12, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                /*case -304802106:
                    return new VehicleInfoData {
                        DisplayName = "Hwaybufsc",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 270000, 
                        StockFull = 310000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 15, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };*/
                case 1493409374:
                    return new VehicleInfoData {
                        DisplayName = "Fbibufsc",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 270000, 
                        StockFull = 310000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                case 749836236:
                    return new VehicleInfoData {
                        DisplayName = "Prdsuv",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 756000, 
                        StockFull = 415000, 
                        StockItem = 0,
                        FullFuel = 74, 
                        FuelMinute = 12, 
                        Fuel = 74, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                case -983454510:
                    return new VehicleInfoData {
                        DisplayName = "Prdsuv2",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 756000, 
                        StockFull = 415000, 
                        StockItem = 0,
                        FullFuel = 74, 
                        FuelMinute = 12, 
                        Fuel = 74, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1848730848:
                    return new VehicleInfoData {
                        DisplayName = "Gauntlets",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 225000, 
                        StockFull = 365000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 13, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case -1385344391:
                    return new VehicleInfoData {
                        DisplayName = "Gauntletc",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 345000, 
                        StockFull = 315000, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 14, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case -1678617083:
                    return new VehicleInfoData {
                        DisplayName = "Hellhound",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 315000, 
                        StockFull = 390000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 14, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case 2134119907:
                    return new VehicleInfoData {
                        DisplayName = "Dukes3",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 216000, 
                        StockFull = 330000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 14, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case -1116818112:
                    return new VehicleInfoData {
                        DisplayName = "Domc",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 165000, 
                        StockFull = 320000, 
                        StockItem = 0,
                        FullFuel = 74, 
                        FuelMinute = 14, 
                        Fuel = 74, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case 1021614827:
                    return new VehicleInfoData {
                        DisplayName = "Schwartzerc",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 392000, 
                        StockFull = 145000, 
                        StockItem = 0,
                        FullFuel = 64, 
                        FuelMinute = 8, 
                        Fuel = 64, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                case 600163992:
                    return new VehicleInfoData {
                        DisplayName = "Taranis",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 471000, 
                        StockFull = 175000, 
                        StockItem = 0,
                        FullFuel = 1, 
                        FuelMinute = 0, 
                        Fuel = 1, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                            
                case -571009320:
                    return new VehicleInfoData {
                        DisplayName = "Angel",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 2000, 
                        StockFull = 20000, 
                        StockItem = 0,
                        FullFuel = 42, 
                        FuelMinute = 5, 
                        Fuel = 42, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1884962369:
                    return new VehicleInfoData {
                        DisplayName = "Cabby",
                        ClassName = "Service", 
                        IsBroke = false, 
                        Stock = 1191000, 
                        StockFull = 335000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 9, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1034516789:
                    return new VehicleInfoData {
                        DisplayName = "Contender2",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 2124000, 
                        StockFull = 650000, 
                        StockItem = 0,
                        FullFuel = 89, 
                        FuelMinute = 12, 
                        Fuel = 89, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -311302597:
                    return new VehicleInfoData {
                        DisplayName = "Emperor4",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 330000, 
                        StockFull = 105000, 
                        StockItem = 0,
                        FullFuel = 78, 
                        FuelMinute = 8, 
                        Fuel = 78, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -330060047:
                    return new VehicleInfoData {
                        DisplayName = "Huntley2",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 575000, 
                        StockFull = 415000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 9, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 207497487:
                    return new VehicleInfoData {
                        DisplayName = "Packer2",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 150, 
                        FuelMinute = 15, 
                        Fuel = 150, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2077743597:
                    return new VehicleInfoData {
                        DisplayName = "Perennial",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 765000, 
                        StockFull = 205000, 
                        StockItem = 0,
                        FullFuel = 78, 
                        FuelMinute = 8, 
                        Fuel = 78, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1590284256:
                    return new VehicleInfoData {
                        DisplayName = "Perennial2",
                        ClassName = "Service", 
                        IsBroke = false, 
                        Stock = 765000, 
                        StockFull = 205000, 
                        StockItem = 0,
                        FullFuel = 78, 
                        FuelMinute = 8, 
                        Fuel = 78, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1390084576:
                    return new VehicleInfoData {
                        DisplayName = "Rancher",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 1822000, 
                        StockFull = 395000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 10, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1369781310:
                    return new VehicleInfoData {
                        DisplayName = "Regina3",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 600000, 
                        StockFull = 145000, 
                        StockItem = 0,
                        FullFuel = 62, 
                        FuelMinute = 7, 
                        Fuel = 62, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -322343873:
                    return new VehicleInfoData {
                        DisplayName = "Schafter",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 210000, 
                        StockFull = 145000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 7, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 944930284:
                    return new VehicleInfoData {
                        DisplayName = "Smuggler",
                        ClassName = "Boats", 
                        IsBroke = false, 
                        Stock = 320000, 
                        StockFull = 280000, 
                        StockItem = 0,
                        FullFuel = 250, 
                        FuelMinute = 20, 
                        Fuel = 250, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -583281407:
                    return new VehicleInfoData {
                        DisplayName = "Vincent",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 205000, 
                        StockFull = 210000, 
                        StockItem = 0,
                        FullFuel = 78, 
                        FuelMinute = 10, 
                        Fuel = 78, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1208856469:
                    return new VehicleInfoData {
                        DisplayName = "Taxi2",
                        ClassName = "Service", 
                        IsBroke = false, 
                        Stock = 285000, 
                        StockFull = 178000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 8, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1758379524:
                    return new VehicleInfoData {
                        DisplayName = "Virgo2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 318000, 
                        StockFull = 235000, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 15, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1356880839:
                    return new VehicleInfoData {
                        DisplayName = "Sentinel4",
                        ClassName = "Coupes", 
                        IsBroke = false, 
                        Stock = 520000, 
                        StockFull = 155000, 
                        StockItem = 0,
                        FullFuel = 64, 
                        FuelMinute = 7, 
                        Fuel = 64, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -595004596:
                    return new VehicleInfoData {
                        DisplayName = "Intercept2",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 280000, 
                        StockFull = 225000, 
                        StockItem = 0,
                        FullFuel = 63, 
                        FuelMinute = 8, 
                        Fuel = 63, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 251388012:
                    return new VehicleInfoData {
                        DisplayName = "Torrence",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 280000, 
                        StockFull = 225000, 
                        StockItem = 0,
                        FullFuel = 60, 
                        FuelMinute = 6, 
                        Fuel = 60, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 440299355:
                    return new VehicleInfoData {
                        DisplayName = "Steed2",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 5050000, 
                        StockFull = 1085000, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 12, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                    
                case 1945374990:
                    return new VehicleInfoData {
                        DisplayName = "Mule4",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 21000000, 
                        StockFull = 3450000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 11, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
		        case 1653666139:
                    return new VehicleInfoData {
                        DisplayName = "Pounder2",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 30000000, 
                        StockFull = 5900000, 
                        StockItem = 0,
                        FullFuel = 152, 
                        FuelMinute = 13, 
                        Fuel = 152, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
				case -1988428699:
                    return new VehicleInfoData {
                        DisplayName = "Terbyte",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 152, 
                        FuelMinute = 16, 
                        Fuel = 152, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
		
                case 2069146067:
                    return new VehicleInfoData {
                        DisplayName = "Oppressor2",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 55, 
                        FuelMinute = 15, 
                        Fuel = 51, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -54332285:
                    return new VehicleInfoData {
                        DisplayName = "Freecrwaler",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 485000, 
                        StockFull = 460000, 
                        StockItem = 0,
                        FullFuel = 86, 
                        FuelMinute = 14, 
                        Fuel = 86, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 2044532910:
                    return new VehicleInfoData {
                        DisplayName = "Menacer",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 1534000, 
                        StockFull = 1240000, 
                        StockItem = 0,
                        FullFuel = 120, 
                        FuelMinute = 16, 
                        Fuel = 120, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -307958377:
                    return new VehicleInfoData {
                        DisplayName = "Blimp3",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 700000, 
                        StockFull = 190000, 
                        StockItem = 0,
                        FullFuel = 280, 
                        FuelMinute = 30, 
                        Fuel = 280, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1692272545:
                    return new VehicleInfoData {
                        DisplayName = "Strikeforce",
                        ClassName = "Planes", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 1800, 
                        FuelMinute = 235, 
                        Fuel = 1800, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -420911112:
                    return new VehicleInfoData {
                        DisplayName = "Patriot2",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 850000, 
                        StockFull = 565000, 
                        StockItem = 0,
                        FullFuel = 165, 
                        FuelMinute = 30, 
                        Fuel = 165, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 321186144:
                    return new VehicleInfoData {
                        DisplayName = "Stafford",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 320000, 
                        StockFull = 120000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 5, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 345756458:
                    return new VehicleInfoData {
                        DisplayName = "PBus2",
                        ClassName = "Service", 
                        IsBroke = false, 
                        Stock = 1445000, 
                        StockFull = 1650000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 12, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 500482303:
                    return new VehicleInfoData {
                        DisplayName = "Swinger",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 51000, 
                        StockFull = 190000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 13, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -638562243:
                    return new VehicleInfoData {
                        DisplayName = "Scramjet",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 100, 
                        FuelMinute = 15, 
                        Fuel = 100, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 219613597:
                    return new VehicleInfoData {
                        DisplayName = "Speedo4",
                        ClassName = "Vans", 
                        IsBroke = false, 
                        Stock = 4800000, 
                        StockFull = 905000, 
                        StockItem = 0,
                        FullFuel = 92, 
                        FuelMinute = 11, 
                        Fuel = 92, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1168952148:
                    return new VehicleInfoData {
                        DisplayName = "Toros",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 11, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1456744817:
                    return new VehicleInfoData {
                        DisplayName = "Tulip",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 215000, 
                        StockFull = 270000, 
                        StockItem = 0,
                        FullFuel = 84, 
                        FuelMinute = 10, 
                        Fuel = 84, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -49115651:
                    return new VehicleInfoData {
                        DisplayName = "Vamos",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 285000, 
                        StockFull = 265000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 12, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 540101442:
                    return new VehicleInfoData {
                        DisplayName = "zr380",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 110, 
                        FuelMinute = 15, 
                        Fuel = 110, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1106120762:
                    return new VehicleInfoData {
                        DisplayName = "zr3802",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 110, 
                        FuelMinute = 15, 
                        Fuel = 110, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1478704292:
                    return new VehicleInfoData {
                        DisplayName = "zr3803",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 110, 
                        FuelMinute = 15, 
                        Fuel = 110, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case 668439077:
                    return new VehicleInfoData {
                        DisplayName = "Bruiser",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 120000, 
                        StockFull = 600000, 
                        StockItem = 0,
                        FullFuel = 142, 
                        FuelMinute = 25, 
                        Fuel = 142, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case -1694081890:
                    return new VehicleInfoData {
                        DisplayName = "Bruiser2",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 120000, 
                        StockFull = 600000, 
                        StockItem = 0,
                        FullFuel = 142, 
                        FuelMinute = 25, 
                        Fuel = 142, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case -2042350822:
                    return new VehicleInfoData {
                        DisplayName = "Bruiser3",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 120000, 
                        StockFull = 600000, 
                        StockItem = 0,
                        FullFuel = 142, 
                        FuelMinute = 25, 
                        Fuel = 142, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case 2139203625:
                    return new VehicleInfoData {
                        DisplayName = "Brutus",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 16, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case -1890996696:
                    return new VehicleInfoData {
                        DisplayName = "Brutus2",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 16, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case 2038858402:
                    return new VehicleInfoData {
                        DisplayName = "Brutus3",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 16, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -801550069:
                    return new VehicleInfoData {
                        DisplayName = "Cerberus",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 260, 
                        FuelMinute = 25, 
                        Fuel = 260, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 679453769:
                    return new VehicleInfoData {
                        DisplayName = "Cerberus2",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 260, 
                        FuelMinute = 25, 
                        Fuel = 260, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1909700336:
                    return new VehicleInfoData {
                        DisplayName = "Cerberus3",
                        ClassName = "Commercials", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 260, 
                        FuelMinute = 25, 
                        Fuel = 260, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1566607184:
                    return new VehicleInfoData {
                        DisplayName = "Clique",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 212000, 
                        StockFull = 200000, 
                        StockItem = 0,
                        FullFuel = 70, 
                        FuelMinute = 9, 
                        Fuel = 70, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -27326686:
                    return new VehicleInfoData {
                        DisplayName = "Deathbike",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 45, 
                        FuelMinute = 5, 
                        Fuel = 45, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1812949672:
                    return new VehicleInfoData {
                        DisplayName = "Deathbike2",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 45, 
                        FuelMinute = 5, 
                        Fuel = 45, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1374500452:
                    return new VehicleInfoData {
                        DisplayName = "Deathbike3",
                        ClassName = "Motorcycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 45, 
                        FuelMinute = 5, 
                        Fuel = 45, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1591739866:
                    return new VehicleInfoData {
                        DisplayName = "Deveste",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 110, 
                        FuelMinute = 14, 
                        Fuel = 110, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1279262537:
                    return new VehicleInfoData {
                        DisplayName = "Deviant",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 360000, 
                        StockFull = 340000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 15, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
			
                case -688189648:
                    return new VehicleInfoData {
                        DisplayName = "Dominator4",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 17, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
		        case -1375060657:
                    return new VehicleInfoData {
                        DisplayName = "Dominator5",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 16, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1293924613:
                    return new VehicleInfoData {
                        DisplayName = "Dominator6",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 17, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2096690334:
                    return new VehicleInfoData {
                        DisplayName = "Impaler",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 185000, 
                        StockFull = 265000, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 12, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1009171724:
                    return new VehicleInfoData {
                        DisplayName = "Impaler2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 14, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1924800695:
                    return new VehicleInfoData {
                        DisplayName = "Impaler3",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 12, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1744505657:
                    return new VehicleInfoData {
                        DisplayName = "Impaler4",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 95, 
                        FuelMinute = 14, 
                        Fuel = 95, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 444994115:
                    return new VehicleInfoData {
                        DisplayName = "Imperator",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 195000, 
                        StockFull = 285000, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 12, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1637620610:
                    return new VehicleInfoData {
                        DisplayName = "Imperator2",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 195000, 
                        StockFull = 285000, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 12, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -755532233:
                    return new VehicleInfoData {
                        DisplayName = "Imperator3",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 195000, 
                        StockFull = 285000, 
                        StockItem = 0,
                        FullFuel = 98, 
                        FuelMinute = 12, 
                        Fuel = 98, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 628003514:
                    return new VehicleInfoData {
                        DisplayName = "Issi4",
                        ClassName = "Compacts", 
                        IsBroke = false, 
                        Stock = 140000, 
                        StockFull = 80000, 
                        StockItem = 0,
                        FullFuel = 35, 
                        FuelMinute = 8, 
                        Fuel = 35, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
		
                case 1537277726:
                    return new VehicleInfoData {
                        DisplayName = "Issi5",
                        ClassName = "Compacts", 
                        IsBroke = false, 
                        Stock = 140000, 
                        StockFull = 80000, 
                        StockItem = 0,
                        FullFuel = 35, 
                        FuelMinute = 8, 
                        Fuel = 35, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
		
                case 1239571361:
                    return new VehicleInfoData {
                        DisplayName = "Issi6",
                        ClassName = "Compacts", 
                        IsBroke = false, 
                        Stock = 140000, 
                        StockFull = 80000, 
                        StockItem = 0,
                        FullFuel = 35, 
                        FuelMinute = 8, 
                        Fuel = 35, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
		
                case 1854776567:
                    return new VehicleInfoData {
                        DisplayName = "Issi7",
                        ClassName = "Compacts", 
                        IsBroke = false, 
                        Stock = 440000, 
                        StockFull = 230000, 
                        StockItem = 0,
                        FullFuel = 68, 
                        FuelMinute = 10, 
                        Fuel = 68, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
		
                case -331467772:
                    return new VehicleInfoData {
                        DisplayName = "Italigto",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 90000, 
                        StockFull = 210000, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 12, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case 1721676810:
                    return new VehicleInfoData {
                        DisplayName = "Monster3",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 155, 
                        FuelMinute = 25, 
                        Fuel = 155, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case 840387324:
                    return new VehicleInfoData {
                        DisplayName = "Monster4",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 155, 
                        FuelMinute = 25, 
                        Fuel = 155, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case -715746948:
                    return new VehicleInfoData {
                        DisplayName = "Monster5",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 155, 
                        FuelMinute = 25, 
                        Fuel = 155, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case -286046740:
                    return new VehicleInfoData {
                        DisplayName = "Rcbandito",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 1, 
                        FuelMinute = 0, 
                        Fuel = 1, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
	    
               case -1146969353:
                    return new VehicleInfoData {
                        DisplayName = "Scarab",
                        ClassName = "Military", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 220, 
                        FuelMinute = 35, 
                        Fuel = 220, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case 1542143200:
                    return new VehicleInfoData {
                        DisplayName = "Scarab2",
                        ClassName = "Military", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 220, 
                        FuelMinute = 35, 
                        Fuel = 220, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
               case -579747861:
                    return new VehicleInfoData {
                        DisplayName = "Scarab3",
                        ClassName = "Military", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 220, 
                        FuelMinute = 35, 
                        Fuel = 220, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -507495760:
                    return new VehicleInfoData {
                        DisplayName = "Schlagen",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 160000, 
                        StockFull = 220000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 12, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -2061049099:
                    return new VehicleInfoData {
                        DisplayName = "SlamVan4",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 14, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 373261600:
                    return new VehicleInfoData {
                        DisplayName = "SlamVan5",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 14, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1742022738:
                    return new VehicleInfoData {
                        DisplayName = "SlamVan6",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 14, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -654498607:
                    return new VehicleInfoData {
                        DisplayName = "Brigham",
                        ClassName = "Sedans", 
                        IsBroke = false, 
                        Stock = 392000, 
                        StockFull = 125000, 
                        StockItem = 0,
                        FullFuel = 68, 
                        FuelMinute = 6, 
                        Fuel = 68, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
		         case -1085906637:
                    return new VehicleInfoData {
                        DisplayName = "Dubsta4x4",
                        ClassName = "Off-Road", 
                        IsBroke = false, 
                        Stock = 892000, 
                        StockFull = 465000, 
                        StockItem = 0,
                        FullFuel = 105, 
                        FuelMinute = 15, 
                        Fuel = 105, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
		        case -557821895:
                    return new VehicleInfoData {
                        DisplayName = "Glendaleks",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 120000, 
                        StockFull = 105000, 
                        StockItem = 0,
                        FullFuel = 75, 
                        FuelMinute = 10, 
                        Fuel = 75, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1932502825:
                    return new VehicleInfoData {
                        DisplayName = "Infernus4",
                        ClassName = "Super", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 100, 
                        FuelMinute = 10, 
                        Fuel = 100, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
					
                case 1668181497:
                    return new VehicleInfoData {
                        DisplayName = "Niner",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 20000, 
                        StockFull = 240000, 
                        StockItem = 0,
                        FullFuel = 79, 
                        FuelMinute = 13, 
                        Fuel = 79, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -449022887:
                    return new VehicleInfoData {
                        DisplayName = "Sabre",
                        ClassName = "Muscle", 
                        IsBroke = false, 
                        Stock = 392000, 
                        StockFull = 265000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 12, 
                        Fuel = 80, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -1109893355:
                    return new VehicleInfoData {
                        DisplayName = "Scout",
                        ClassName = "SUVs", 
                        IsBroke = false, 
                        Stock = 657000, 
                        StockFull = 470000, 
                        StockItem = 0,
                        FullFuel = 85, 
                        FuelMinute = 12, 
                        Fuel = 85, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1977528411:
                    return new VehicleInfoData {
                        DisplayName = "Scoutpol",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 657000, 
                        StockFull = 470000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 15, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1162796823:
                    return new VehicleInfoData {
                        DisplayName = "Scoutpol2",
                        ClassName = "Emergency", 
                        IsBroke = false, 
                        Stock = 657000, 
                        StockFull = 470000, 
                        StockItem = 0,
                        FullFuel = 90, 
                        FuelMinute = 15, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case -467275782:
                    return new VehicleInfoData {
                        DisplayName = "Streiter2",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 688000, 
                        StockFull = 470000, 
                        StockItem = 0,
                        FullFuel = 80, 
                        FuelMinute = 12, 
                        Fuel = 90, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1862507111:
                    return new VehicleInfoData {
                        DisplayName = "Zion3",
                        ClassName = "Sports Classics", 
                        IsBroke = false, 
                        Stock = 343000, 
                        StockFull = 120000, 
                        StockItem = 0,
                        FullFuel = 72, 
                        FuelMinute = 9, 
                        Fuel = 72, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 1564782073:
                    return new VehicleInfoData {
                        DisplayName = "Zr380s",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 375000, 
                        StockFull = 215000, 
                        StockItem = 0,
                        FullFuel = 82, 
                        FuelMinute = 12, 
                        Fuel = 82, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
        
                case 781406351:
                    return new VehicleInfoData {
                        DisplayName = "Zr380c",
                        ClassName = "Sports", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 88, 
                        FuelMinute = 13, 
                        Fuel = 88, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                    
                case -1690093342:
                    return new VehicleInfoData {
                        DisplayName = "Lowriderb",
                        ClassName = "Cycles", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 0, 
                        StockItem = 0,
                        FullFuel = 0, 
                        FuelMinute = 0, 
                        Fuel = 0, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
                
                default:
                    return new VehicleInfoData {
                        DisplayName = "Unk",
                        ClassName = "Unk", 
                        IsBroke = false, 
                        Stock = 0, 
                        StockFull = 471000, 
                        StockItem = 175000,
                        FullFuel = 65, 
                        FuelMinute = 8, 
                        Fuel = 65, 
                        SWhBkl = 0, 
                        SWhBl = 0, 
                        SWhBkr = 0, 
                        SWhBr = 0, 
                        SEngine = 0, 
                        SSuspension = 0, 
                        SBody = 0, 
                        SCandle = 0, 
                        SOil = 0, 
                        SMp = 0 
                    };
            }
        }
    }
}

public class VehicleInfoPlayerData
{
    public int id { get; set; }
    public int id_user { get; set; }
    public string user_name { get; set; }
    public string name { get; set; }
    public int hash { get; set; }
    public int price { get; set; }
    public int stock { get; set; }
    public int stock_full { get; set; }
    public int stock_item { get; set; }
    public float fuel { get; set; }
    public int full_fuel { get; set; }
    public int fuel_minute { get; set; }
    public bool lock_status { get; set; }
    public int wanted_level { get; set; }
    public int color1 { get; set; }
    public int color2 { get; set; }
    public int neon_type { get; set; }
    public int neon_r { get; set; }
    public int neon_g { get; set; }
    public int neon_b { get; set; }
    public string number { get; set; }
    public string upgrade { get; set; }
    
    public float s_mp { get; set; }
    public int s_wh_bk_l { get; set; }
    public int s_wh_b_l { get; set; }
    public int s_wh_bk_r { get; set; }
    public int s_wh_b_r { get; set; }
    public int s_engine { get; set; }
    public int s_suspension { get; set; }
    public int s_body { get; set; }
    public float s_candle { get; set; }
    public float s_oil { get; set; }
    
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float rot { get; set; }
    public float x_park { get; set; }
    public float y_park { get; set; }
    public float z_park { get; set; }
    public float rot_park { get; set; }
    public string cop_park_name { get; set; }
    public bool is_cop_park { get; set; }
}

public class VehicleInfoData
{
    public string DisplayName { get; set; }
    public string ClassName { get; set; }
    
    public bool IsBroke { get; set; }
    
    public int Stock { get; set; }
    public int StockFull { get; set; }
    public int StockItem { get; set; }
    
    public float Fuel { get; set; }
    public int FullFuel { get; set; }
    public int FuelMinute { get; set; }
    
    public int SWhBkl { get; set; }
    public int SWhBl { get; set; }
    public int SWhBkr { get; set; }
    public int SWhBr { get; set; }
    public int SEngine { get; set; }
    public int SSuspension { get; set; }
    public int SBody { get; set; }
    
    public float SCandle { get; set; }
    public float SOil { get; set; }
    public float SMp { get; set; }
}

public class VehicleInfoGlobalData
{
    public int NetId { get; set; }
    public int VehId { get; set; }
    
    /*For Fraction / Global*/
    
    public int FractionId { get; set; }
    public bool IsUserOwner { get; set; }
    public string Job { get; set; }
    public int Livery { get; set; }
    public string Number { get; set; }
    public int StyleNumber { get; set; }
    public bool CanLoad { get; set; }
    
    public string DisplayName { get; set; }
    public string ClassName { get; set; }
    
    public bool IsBroke { get; set; }
    
    public int Stock { get; set; }
    public int StockFull { get; set; }
    public int StockItem { get; set; }
    
    public float Fuel { get; set; }
    public int FullFuel { get; set; }
    public int FuelMinute { get; set; }
    
    public int SWhBkl { get; set; }
    public int SWhBl { get; set; }
    public int SWhBkr { get; set; }
    public int SWhBr { get; set; }
    public int SEngine { get; set; }
    public int SSuspension { get; set; }
    public int SBody { get; set; }
    
    public float SCandle { get; set; }
    public float SOil { get; set; }
    public float SMp { get; set; }
    
    public int Hash { get; set; }
    
    /*For User*/
    
    public int id { get; set; }
    public int id_user { get; set; }
    public string user_name { get; set; }
    public string name { get; set; }
    public int price { get; set; }
    public int stock { get; set; }
    public int stock_item { get; set; }
    public float fuel { get; set; }
    public bool lock_status { get; set; }
    public int wanted_level { get; set; }
    public int color1 { get; set; }
    public int color2 { get; set; }
    public int neon_type { get; set; }
    public int neon_r { get; set; }
    public int neon_g { get; set; }
    public int neon_b { get; set; }
    public string upgrade { get; set; }
    
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float rot { get; set; }
    public float x_park { get; set; }
    public float y_park { get; set; }
    public float z_park { get; set; }
    public float rot_park { get; set; }
    public string cop_park_name { get; set; }
    public bool is_cop_park { get; set; }
    
    
    /*For Server*/
    
    public float CurrentPosX { get; set; }
    public float CurrentPosY { get; set; }
    public float CurrentPosZ { get; set; }
    public float CurrentRotX { get; set; }
    public float CurrentRotY { get; set; }
    public float CurrentRotZ { get; set; }
    public float RespawnPosX { get; set; }
    public float RespawnPosY { get; set; }
    public float RespawnPosZ { get; set; }
    public float RespawnRotX { get; set; }
    public int RespawnTime { get; set; }
    public int RespawnTimeMax { get; set; }
    public int IndicatorType { get; set; }
    public bool EngineStatus { get; set; }
    public bool LockStatus { get; set; }
    public bool IsVisible { get; set; }
    public int Health { get; set; }
}