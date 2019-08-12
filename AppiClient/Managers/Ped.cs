using System;
using CitizenFX.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace Client.Managers
{
    public class Ped : BaseScript
    {
        public static List<PedData> PedList = new List<PedData>();
	    public static float LoadRange = 400f;
	    public static int Count = 0;

		public static async Task<int> CreatePed(string model, Vector3 position, float rotation, bool isDefault = false, string scenario = "", string animation1 = "", string animation2 = "", int flag = 9)
		{
			return await CreatePed(GetHashKey(model), position, rotation, isDefault, scenario, animation1, animation2, flag);
		}

		public static async Task<int> CreatePed(Model model, Vector3 position, float rotation, bool isDefault = false, string scenario = "", string animation1 = "", string animation2 = "", int flag = 9)
		{
			int id = PedList.Count;
			
			PedData pedData = new PedData
			{
				model = model,
				pos = position,
				rot = rotation,
				scenario = scenario,
				animation1 = animation1,
				animation2 = animation2,
				flag = flag,
				isDefault = isDefault,
				isCreate = false
			};
			PedList.Add(pedData);
			Count = PedList.Count;
			
			if (!IsModelValid((uint) model.Hash)) 
				return id;
		
			if (!await Main.LoadModel((uint) model.Hash))
				return id;
			return id;
		}

	    public Ped()
	    {
		    Tick += Draw;
	    }
	    
	    private static async Task Draw()
	    {
		    await Delay(10000);
		    var pos = GetEntityCoords(GetPlayerPed(-1), true);

		    if (PedList.Count > 0)
		    {
			    foreach (var item in PedList)
			    {
				    if (item == null)
					    continue;
				    
				    if (Main.GetDistanceToSquared(pos, item.pos) > LoadRange)
				    {
					    if (item.isCreate)
					    {
						    if (item.ped == null) continue;
						    try
						    {
							    item.ped.Delete();
							    item.ped = null;
							    item.isCreate = false;
						    }
						    catch (Exception e)
						    {
							    Debug.WriteLine("DELETE PED: " + e.ToString());
						    }
					    }
				    }
				    else
				    {
					    if (!item.isCreate)
					    {
						    if (item.ped != null) continue;
						    
						    try
						    {
								item.ped = await CreatePedLocally(item.model, item.pos, item.rot);
								if (item.ped == null || !item.ped.Exists())
								{
									item.ped = null;
									item.isCreate = false;
									continue;
								}
							    
							    FreezeEntityPosition(item.ped.Handle, true);
							    item.ped.IsPositionFrozen = true;
							    
							    //item.ped.MarkAsNoLongerNeeded();
							    item.ped.PositionNoOffset = item.pos;
							    
							    //SetEntityAsMissionEntity(item.ped.Handle, false, true);
							    
							    item.ped.CanBeTargetted = true;
							    item.ped.BlockPermanentEvents = true;
							    TaskSetBlockingOfNonTemporaryEvents(item.ped.Handle, true);
							    item.ped.IsInvincible = true;
							    item.ped.CanRagdoll = false;
					    
							    SetPedCanEvasiveDive(item.ped.Handle, false);
							    SetPedCanBeTargetted(item.ped.Handle, true);
							    SetPedCanBeTargettedByPlayer(item.ped.Handle, GetPlayerPed(-1), true);
								SetPedGetOutUpsideDownVehicle(item.ped.Handle, false);
								SetPedAsEnemy(item.ped.Handle, false);
								SetCanAttackFriendly(item.ped.Handle, false, false);
							    
							    if (item.isDefault)
							    	SetPedDefaultComponentVariation(item.ped.Handle);
							    
							    if (item.scenario != "")
								    TaskStartScenarioInPlace(item.ped.Handle, item.scenario, 0, true);
							    
							    if (item.animation1 != "")
							    {
								    int wait = 500;
								    RequestAnimDict(item.animation1);
								    while (!HasAnimDictLoaded(item.animation1) && wait > 0)
								    {
									    RequestAnimDict(item.animation1);
									    wait--;
									    await Delay(1);
								    }

								    TaskPlayAnim(item.ped.Handle, item.animation1, item.animation2, 8f, -8, -1, item.flag, 0, false, false, false);
							    }

							    item.isCreate = true;
						    }
						    catch (Exception e)
						    {
							    Debug.WriteLine($"ERROR CREATE PED: {Convert.ToInt64(item.model)}|{item.pos.X}|{item.pos.Y}|{item.pos.Z}|{e}");
						    }
					    }
					    /*else
					    {
						    if (item.ped == null) continue;
						    if (!item.ped.IsDead) continue;	
						    
						    try
						    {
							    item.ped.Delete();
							    item.ped = null;
							    item.isCreate = false;
							    Debug.WriteLine("DELETE PED DEBUG 1");
						    }
						    catch (Exception e)
						    {
							    Debug.WriteLine("DELETE PED: " + e.ToString());
						    }
					    }*/
				    }
			    } 
		    }
	    }

		public static async Task<CitizenFX.Core.Ped> CreatePedLocally(Model model, Vector3 position, float rotation)
		{
			if (!IsModelValid((uint) model.Hash))
				return null;
			
			if (!await Main.LoadModel((uint) model.Hash))
				return null;
			
			return !HasModelLoaded((uint) model.Hash) ? null : new CitizenFX.Core.Ped(CitizenFX.Core.Native.API.CreatePed(26, (uint) model.Hash, position.X, position.Y, position.Z, rotation, false, false));
		}

	    public static async void LoadAllPeds()
	    {
		    //Apteka
		    await CreatePed("a_f_y_business_02", new Vector3(318.4659f, -1080.464f, 19.68166f), 2.999455f, false, "WORLD_HUMAN_STAND_IMPATIENT");
		    
		    //NOOBS
		    await CreatePed("s_m_y_airworker", new Vector3(-1032.057f, -2734.756f, 20.16927f), 115.5994f, true);
		    
		    //EMS
		    await CreatePed("s_f_y_scrubs_01", new Vector3(262.7821f, -1359.238f, 24.53779f), 46.81502f, true, "WORLD_HUMAN_CLIPBOARD");
		    await CreatePed("s_m_m_doctor_01", new Vector3(280.5828f, -1333.853f, 24.53781f), 319.4619f, false, "WORLD_HUMAN_CLIPBOARD");
		    await CreatePed("s_m_m_paramedic_01", new Vector3(268.4438f, -1357.79f, 24.5378f), 327.3099f, false, "WORLD_HUMAN_STAND_MOBILE");
		    
		    //GOV
		    await CreatePed("s_m_m_highsec_01", new Vector3(-1385.913f, -477.0084f, 72.04214f), 191.1487f, true, "WORLD_HUMAN_GUARD_STAND");
		    await CreatePed("s_m_m_highsec_02", new Vector3(-1385.346f, -479.9799f, 72.04214f), 3.717501f, true, "WORLD_HUMAN_GUARD_STAND");
		    await CreatePed("a_f_y_business_04", new Vector3(-1379.815f, -477.6191f, 72.04214f), 90.06773f, true, "WORLD_HUMAN_CLIPBOARD");
		    
		    //Pacific Standart
		    await CreatePed(PedHash.Business04AFY, new Vector3(254.1582f, 222.8858f, 106.2869f), 156);
		    await CreatePed(PedHash.Business02AFY, new Vector3(252.3869f, 223.4011f, 106.2869f), 156);
		    await CreatePed(PedHash.Business01AMM, new Vector3(249.102f, 224.6973f, 106.287f), 156);
		    await CreatePed(PedHash.Business01AFY, new Vector3(247.2151f, 225.22f, 106.2876f), 156);
		    await CreatePed(PedHash.Business01AMY, new Vector3(243.8021f, 226.2166f, 106.2876f), 156);
		    await CreatePed(PedHash.Business02AMY, new Vector3(241.9458f, 227.1961f, 106.287f), 156);
            
            //Flecca
            await CreatePed(PedHash.TaosTranslator, new Vector3(148.0046f, -1041.758f, 29.36793f), -24);
            await CreatePed(PedHash.Patricia, new Vector3(175.031f, 2708.488f, 38.08792f), 175);
            await CreatePed(PedHash.MrKCutscene, new Vector3(-1211.733f, -332.3059f, 37.78094f), 27);
            
            //Blaine
            await CreatePed(PedHash.Molly, new Vector3(-112.1827f, 6471.3f, 31.62671f), 128);
            await CreatePed(PedHash.KorLieut01GMY, new Vector3(-109.9172f, 6469.146f, 31.62671f), 128);
		    
		    
		    //HackerSpace Shop
		    //await CreatePed("a_m_y_ktown_01", new Vector3(520.09f, 167.6327f, 99.39f), -103.1486f, true, "PROP_HUMAN_SEAT_BENCH");

		    await CreatePed("s_m_y_garbage", new Vector3(53.34285f, -723.2313f, 31.76511f), 44.9335f, true, "WORLD_HUMAN_SMOKING");
		    
		    //SAPD
		    await CreatePed("s_f_y_cop_01", new Vector3(440.3013f, -978.6867f, 30.6896f), 179.0161f, false, "WORLD_HUMAN_STAND_MOBILE");
		    await CreatePed("s_m_y_cop_01", new Vector3(454.3719f, -980.504f, 30.68959f), 72.25758f, false, "WORLD_HUMAN_CLIPBOARD");
		    await CreatePed("s_m_y_cop_01", new Vector3(462.1494f, -992.3374f, 24.91487f), 5.561146f, false, "WORLD_HUMAN_GUARD_STAND");
		    await CreatePed("s_m_y_cop_01", new Vector3(412.6824f, -1023.331f, 29.47568f), 7.878762f, false, "WORLD_HUMAN_CLIPBOARD");
		    await CreatePed("s_f_y_cop_01", new Vector3(461.6078f, -1021.399f, 32.98539f), 169.3055f, false, "WORLD_HUMAN_AA_COFFEE");
		    await CreatePed("s_m_y_cop_01", new Vector3(448.1678f, -988.1086f, 30.68959f), 20.97919f, false, "WORLD_HUMAN_STAND_MOBILE");
			await CreatePed("s_m_y_cop_01", new Vector3(426.3052f, -992.8116f, 35.68463f), 90.84706f, false, "WORLD_HUMAN_SMOKING");
		    
		    // 24/7 - Гора Чиллиад - Шоссе Сенора
			await CreatePed("mp_m_shopkeep_01", new Vector3(1728.476f, 6416.668f, 35.03724f), -109.9557f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// LTD Gasoline - Грейпсид - Грейпсид-Пейн-стрит
			await CreatePed("s_f_y_sweatshop_01", new Vector3(1698.477f, 4922.482f, 42.06366f), -32.02934f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// 24/7 - Сэнди Шорс - Нинланд-авеню
			await CreatePed("mp_m_shopkeep_01", new Vector3(1959.179f, 3741.332f, 32.34376f), -51.81022f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// 24/7 - Хармони - Шоссе 68
			await CreatePed("s_f_y_sweatshop_01", new Vector3(549.306f, 2669.898f, 42.15651f), 102.036f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Rob's Liquor - Пустыня Гранд-Сенора - Шоссе 68
			await CreatePed("mp_m_shopkeep_01", new Vector3(1165.198f, 2710.855f, 38.15769f), -169.9903f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// 24/7 - Пустыня Гранд-Сенора - Шоссе Сенора
			await CreatePed("s_f_y_sweatshop_01", new Vector3(2676.561f, 3280.001f, 55.24115f), -20.5138f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// 24/7 - Чумаш - Барбарено-роуд
			await CreatePed("mp_m_shopkeep_01", new Vector3(-3243.886f, 999.9983f, 12.83071f), -0.1504957f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// 24/7 - Каньон Бэнхэм - Инесено-роуд
			await CreatePed("s_f_y_sweatshop_01", new Vector3(-3040.344f, 584.0048f, 7.908932f), 25.86866f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Rob's Liquor - Каньон Бэнхэм - Шоссе Грейт-Оушн
			await CreatePed("mp_m_shopkeep_01", new Vector3(-2966.275f, 391.6495f, 15.04331f), 90.95544f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// LTD Gasoline - Ричман-Глен - Бэнхэм-Кэньон-драйв
			await CreatePed("s_f_y_sweatshop_01", new Vector3(-1820.364f, 794.7905f, 138.0867f), 136.5701f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// 24/7 - Центр Вайнвуда - Клинтон-авеню
			await CreatePed("mp_m_shopkeep_01", new Vector3(372.8323f, 327.9543f, 103.5664f), -93.31544f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Rob's Liquor - Морнингвуд - Просперити-стрит
			await CreatePed("s_f_y_sweatshop_01", new Vector3(-1486.615f, -377.3467f, 40.16341f), 135.9596f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Rob's Liquor - Каналы Веспуччи - Сан-Андреас-авеню
			await CreatePed("mp_m_shopkeep_01", new Vector3(-1221.311f, -907.9825f, 12.32635f), 44.03139f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// LTD Gasoline - Маленький Сеул - Паломино-авеню
			await CreatePed("s_f_y_sweatshop_01", new Vector3(-706.0112f, -912.8375f, 19.2156f), 93.35769f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// LTD Gasoline - Миррор-Парк - Вест-Миррор-драйв
			await CreatePed("mp_m_shopkeep_01", new Vector3(1164.863f, -322.054f, 69.2051f), 109.3829f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Rob's Liquor - Мурьета-Хайтс - Бульвар Эль-Ранчо
			await CreatePed("s_f_y_sweatshop_01", new Vector3(1134.109f, -983.1777f, 46.41582f), -74.49993f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// 24/7 - Строберри - Бульвар Инносенс
			await CreatePed("mp_m_shopkeep_01", new Vector3(24.17295f, -1345.768f, 29.49703f), -79.8604f, false, "WORLD_HUMAN_STAND_IMPATIENT");//
			// LTD Gasoline - Дэвис - Дэвис-авеню
			await CreatePed("s_f_y_sweatshop_01", new Vector3(-46.25561f, -1757.611f, 29.42101f), 55.09486f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// 24/7 - Татавиамские горы - Шоссе Паломино
			await CreatePed("mp_m_shopkeep_01", new Vector3(2555.677f, 380.6046f, 108.623f), 1.572431f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Digital Den - Маленький Сеул - Паломино-авеню
			await CreatePed("g_m_y_korean_01", new Vector3(-656.9416f, -858.7859f, 24.49001f), 2.746706f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Digital Den - Миррор-Парк - Бульвар Миррор-Парк
			await CreatePed("a_m_y_hipster_01", new Vector3(1132.687f, -474.5676f, 66.7187f), 345.9362f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Family Pharmacy - Мишн-Роу - Фантастик-плейс
			await CreatePed("a_m_m_business_01", new Vector3(317.9639f, -1078.319f, 28.47855f), 359.3141f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Dollars Pills - Альта - Альта-стрит
			await CreatePed("a_f_y_business_01", new Vector3(92.31831f, -231.1054f, 54.66363f), 327.2379f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// D.P. Pharmacy - Текстайл-Сити - Строберри-авеню
			await CreatePed("a_m_m_business_01", new Vector3(299.7478f, -733.0994f, 29.3525f), 255.0316f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Bay Side Drugs - Палето-Бей - Бульвар Палето
			await CreatePed("a_f_y_business_01", new Vector3(-177.5367f, 6384.567f, 31.49536f), 224.1046f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Ammu-Nation - Татавиамские горы - Шоссе Паломино
			await CreatePed("s_m_y_ammucity_01", new Vector3(2567.45f, 292.3297f, 108.7349f), 0.9863386f, false, "WORLD_HUMAN_GUARD_STAND");
			// Ammu-Nation - Чумаш - Барбарено-роуд
			await CreatePed("s_m_y_ammucity_01", new Vector3(-3173.501f, 1088.957f, 20.83874f), -106.5671f, false, "WORLD_HUMAN_GUARD_STAND");
			// Ammu-Nation - Река Занкудо - Шоссе 68
			await CreatePed("s_m_y_ammucity_01", new Vector3(-1118.609f, 2700.271f, 18.55414f), -135.1759f, false, "WORLD_HUMAN_GUARD_STAND");
			// Ammu-Nation - Сэнди-Шорс - Бульвар Алгонквин
			await CreatePed("s_m_y_ammucity_01", new Vector3(1692.413f, 3761.51f, 34.70534f), -126.9435f, false, "WORLD_HUMAN_GUARD_STAND");
			// Ammu-Nation - Палето-Бэй - Шоссе Грейт-Оушн
			await CreatePed("s_m_y_ammucity_01", new Vector3(-331.3555f, 6085.712f, 31.45477f), -133.1493f, false, "WORLD_HUMAN_GUARD_STAND");
			// Ammu-Nation - Пиллбокс-Хилл - Элгин-Авеню
			await CreatePed("s_m_y_ammucity_01", new Vector3(23.1827f, -1105.512f, 29.79702f), 158.1179f, false, "WORLD_HUMAN_GUARD_STAND");
			// Ammu-Nation - Хавик - Спэниш-авеню
			await CreatePed("s_m_y_ammucity_01", new Vector3(253.8001f, -51.07007f, 69.9411f), 71.83827f, false, "WORLD_HUMAN_GUARD_STAND");
			// Ammu-Nation - Ла-Меса - Шоссе Олимпик
			await CreatePed("s_m_y_ammucity_01", new Vector3(841.848f, -1035.449f, 28.19485f), -1.228782f, false, "WORLD_HUMAN_GUARD_STAND");
			// Ammu-Nation - Маленький Сеул - Паломино-авеню
			await CreatePed("s_m_y_ammucity_01", new Vector3(-661.7558f, -933.2841f, 21.82923f), -178.1721f, false, "WORLD_HUMAN_GUARD_STAND");
			// Ammu-Nation - Морнингвуд - Бульвар Морнингвуд
			await CreatePed("s_m_y_ammucity_01", new Vector3(-1303.956f, -395.2117f, 36.69579f), 75.62228f, false, "WORLD_HUMAN_GUARD_STAND");
			// Ammu-Nation - Сайпрес-Флэтс - Попьюлар-стрит
			await CreatePed("s_m_y_ammucity_01", new Vector3(809.6276f, -2159.31f, 29.61901f), -2.014809f, false, "WORLD_HUMAN_GUARD_STAND");
			// Blazing Tattoo - Центр Вайнвуда - Бульвар Ванйвуд
			await CreatePed("u_m_y_tattoo_01", new Vector3(319.8327f, 181.0894f, 103.5865f), -106.512f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Alamo Tattoo Studio - Сэнди-Шорс - Занкудо-авеню
			await CreatePed("u_m_y_tattoo_01", new Vector3(1862.807f, 3748.279f, 33.03187f), 40.61253f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Paleto Tattoo - Палето-Бэй - Дулуоз-авеню
			await CreatePed("u_m_y_tattoo_01", new Vector3(-292.3047f, 6199.946f, 31.48711f), -117.6071f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// The Pit - Каналы Веспуччи - Агуха-стрит
			await CreatePed("u_m_y_tattoo_01", new Vector3(-1151.971f, -1423.695f, 4.954463f), 136.3183f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Los Santos Tattoos - Эль-Бурро-Хайтс - Бульвар Инносенс
			await CreatePed("u_m_y_tattoo_01", new Vector3(1324.483f, -1650.021f, 52.27503f), 144.9793f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Ink Inc - Чумаш - Барбарено-роуд
			await CreatePed("u_m_y_tattoo_01", new Vector3(-3170.404f, 1072.786f, 20.82917f), -6.981083f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Herr Kutz Barber - Дэвис - Карсон-авеню
			await CreatePed("s_f_m_fembarber", new Vector3(134.8694f, -1708.296f, 29.29161f), 151.6018f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Herr Kutz Barber - Миррор-Парк - Бульвар Миррор-Парк
			await CreatePed("s_f_m_fembarber", new Vector3(1211.27f, -471.0499f, 66.20805f), 82.84951f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Herr Kutz Barber - Палето-Бэй - Дулуоз-авеню
			await CreatePed("s_f_m_fembarber", new Vector3(-278.3121f, 6230.216f, 31.69552f), 60.1603f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Beach Combover Barber - Веспуччи - Магеллан-авеню
			await CreatePed("s_f_m_fembarber", new Vector3(-1284.274f, -1115.853f, 6.99013f), 99.18153f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// O'Sheas Barbers Shop - Сэнди-Шорс - Альгамбра-драйв
			await CreatePed("s_f_m_fembarber", new Vector3(1931.232f, 3728.298f, 32.84444f), -144.9153f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Hair on Hawick - Хавик - Хавик-авеню
			await CreatePed("s_f_m_fembarber", new Vector3(-31.19347f, -151.4883f, 57.07652f), -7.542643f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Bob Mulet - Рокфорд-Хиллз - Мэд-Уэйн-Тандер-драйв
			await CreatePed("s_f_m_fembarber", new Vector3(-822.4669f, -183.7317f, 37.56892f), -139.7869f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Binco - Каналы Веспуччи - Паломино-авеню
			await CreatePed("a_f_y_hipster_02", new Vector3(-823.3749f, -1072.378f, 11.32811f), -108.4307f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Binco - Текстайл-Сити - Синнерс-пэссейдж
			await CreatePed("a_m_y_hipster_02", new Vector3(427.0797f, -806.0226f, 29.49113f), 130.6033f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Discount Store - Палето-Бэй - Бульвар Палето
			await CreatePed("a_f_y_hipster_02", new Vector3(6.133633f, 6511.472f, 31.87784f), 82.75452f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Discount Store - Грейпсид - Грейпсид-Мэйн-стрит
			await CreatePed("a_m_y_hipster_02", new Vector3(1695.472f, 4823.236f, 42.0631f), 125.9657f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Discount Store - Пустыня Гранд-Сенора - Шоссе 68
			await CreatePed("a_f_y_hipster_02", new Vector3(1196.317f, 2711.907f, 38.22262f), -145.9363f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Discount Store - Река Занкудо - Шоссе 68
			await CreatePed("a_m_y_hipster_02", new Vector3(-1102.664f, 2711.66f, 19.10786f), -103.8504f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Discount Store - Строберри - Бульвар Инносентс
			await CreatePed("a_f_y_hipster_02", new Vector3(73.73582f, -1392.895f, 29.37614f), -68.70364f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Sub Urban - Хармони - Шоссе 68
			await CreatePed("s_f_y_shop_mid", new Vector3(612.8171f, 2761.852f, 42.08812f), -63.55088f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Sub Urban - Дель-Перро - Норт-Рокфорд-драйв
			await CreatePed("a_m_y_hipster_01", new Vector3(-1194.562f, -767.3227f, 17.31602f), -120.527f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Sub Urban - Чумаш - Шоссе Грейт-Оушн
			await CreatePed("s_f_y_shop_mid", new Vector3(-3168.905f, 1043.997f, 20.86322f), 80.39653f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Sub Urban - Альта - Хавик-авеню
			await CreatePed("a_m_y_hipster_01", new Vector3(127.306f, -223.5369f, 54.55785f), 101.7699f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Ponsonbys - Бертон - Бульвар Лас-Лагунас
			await CreatePed("a_f_y_business_01", new Vector3(-164.6587f, -302.2024f, 39.7333f), -90.87177f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Ponsonbys - Рокфорд-Хиллз - Портола-драйв
			await CreatePed("a_m_y_business_01", new Vector3(-708.5155f, -152.5676f, 37.41148f), 133.2013f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Ponsonbys - Морнингвуд - Кугар-авеню
			await CreatePed("a_f_y_business_01", new Vector3(-1449.5f, -238.6422f, 49.81335f), 60.38498f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Vangelico - Рокфорд-Хиллз - Рокфорд-драйв
			await CreatePed("u_f_y_jewelass_01", new Vector3(-623.1789f, -229.2665f, 38.05703f), 48.75668f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			await CreatePed("ig_jewelass", new Vector3(-620.9707f, -232.295f, 38.05703f), -134.2347f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			await CreatePed("u_m_m_jewelsec_01", new Vector3(-628.8972f, -238.8752f, 38.05712f), -49.34913f, false, "WORLD_HUMAN_GUARD_STAND");
			// Vespucci Movie Masks - Веспуччи-бич - Витус-стрит
			await CreatePed("s_m_y_shop_mask", new Vector3(-1334.673f, -1276.343f, 4.963552f), 142.5475f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			// Beekers Garage - Палето-Бэй - Бульвар Палето
			await CreatePed("s_m_m_autoshop_01", new Vector3(106.3625f, 6628.315f, 31.78724f), -108.3491f, false, "WORLD_HUMAN_CLIPBOARD");
			// Los Santos Customs Senora - Пустыня-Гранд-Сенора - Шоссе 68
			await CreatePed("s_m_m_autoshop_01", new Vector3(1178.711f, 2639.02f, 37.7538f), 64.71403f, false, "WORLD_HUMAN_CLIPBOARD");
			// Los Santos Customs Burton - Бертон - Карсер-вэй
			await CreatePed("s_m_m_autoshop_01", new Vector3(-345.0504f, -129.6553f, 39.00965f), -149.6841f, false, "WORLD_HUMAN_CLIPBOARD");
			// Los Santos Customs La Mesa - Ла-Меса - Шоссе-Олимпик
			await CreatePed("s_m_m_autoshop_01", new Vector3(737.2117f, -1083.939f, 22.16883f), 97.4564f, false, "WORLD_HUMAN_CLIPBOARD");
			// Hayes Autos - Строберри - Литл-Бигхорн-авеню
			await CreatePed("s_m_m_autoshop_01", new Vector3(471.7564f, -1310.021f, 29.22494f), -128.6412f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bennys Original Motor Works - Строберри - Альта-стрит
			await CreatePed("ig_benny", new Vector3(-216.5449f, -1320.012f, 30.89039f), -97.54453f, false, "WORLD_HUMAN_CLIPBOARD");
			// Los Santos Customs LSIA - Международный аэропорт Лос-Сантос - Гринвич-Парквэй
			await CreatePed("s_m_m_autoshop_01", new Vector3(-1145.874f, -2003.389f, 13.18026f), 94.71597f, false, "WORLD_HUMAN_CLIPBOARD");
			// Arcadius Motors - Пиллбокс-Хилл - Агуха-стрит
			await CreatePed("s_m_m_autoshop_01", new Vector3(-146.3981f, -583.4999f, 167.0001f), 170.6504f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bike rent - Международный аэропорт Лос-Сантос - Нью-Эмпайр-вэй
			await CreatePed("a_f_y_skater_01", new Vector3(-1013.832f, -2681.289f, 13.98584f), -129.831f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bike rent - Строберри - Элгин-авеню
			await CreatePed("a_m_m_skater_01", new Vector3(54.99599f, -1332.448f, 29.31313f), -89.18844f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bike rent - Центр Вайнвуда - Бульвар Ванйвуд
			await CreatePed("a_m_y_skater_01", new Vector3(317.7819f, 131.6896f, 103.5097f), -8.225225f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bike rent - Дель-Перро - Магеллан-авеню
			await CreatePed("a_f_y_skater_01", new Vector3(-1438.929f, -616.7726f, 30.83312f), 43.13194f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bike rent - Чумаш - Барбарено-роуд
			await CreatePed("a_m_m_skater_01", new Vector3(-3241.305f, 978.2664f, 12.7019f), -75.12445f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bike rent - Палето-Бэй - Бульвар Палето
			await CreatePed("a_m_y_skater_01", new Vector3(-266.6101f, 6286.955f, 31.51312f), -122.874f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bike rent - Грейпсид - Грейпсид-Мэйн-стрит
			await CreatePed("a_f_y_skater_01", new Vector3(1683.86f, 4849.291f, 42.1307f), 80.91198f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bike rent - Сэнди-Шорс - Альгамбра-драйв
			await CreatePed("a_m_m_skater_01", new Vector3(1866.788f, 3686.039f, 33.80155f), -140.6224f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bike rent - Пустыня-Гранд-Сенора - Шоссе 68
			await CreatePed("a_m_y_skater_01", new Vector3(1931.512f, 2626.497f, 46.13971f), -134.649f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bike rent - Текстайл-Сити - Элгин-авеню
			await CreatePed("a_f_y_skater_01", new Vector3(297.899f, -600.8516f, 43.33313f), 170.1455f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bike rent - Миррор-Парк - Никола-авеню
			await CreatePed("a_m_m_skater_01", new Vector3(1127.567f, -502.2049f, 64.18119f), -159.9116f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bike rent - Пиллбокс-Хилл - Бульвар Веспуччи
			await CreatePed("a_m_y_skater_01", new Vector3(-54.65418f, -912.4835f, 29.47488f), -148.2788f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bike rent - Маленький Сеул - Декер-стрит
			await CreatePed("a_f_y_skater_01", new Vector3(-873.3246f, -809.8458f, 19.2563f), -174.0148f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bike rent - Рокфорд-Хиллз - Южный бульвар Дель-Перро
			await CreatePed("a_m_m_skater_01", new Vector3(-824.8772f, -115.1577f, 37.58219f), -162.9892f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bike rent - Веспуччи-бич - Гома-стрит
			await CreatePed("a_m_y_skater_01", new Vector3(-1205.739f, -1555.178f, 4.373027f), -3.119672f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bike rent - Хармони - Сенора-роуд
			await CreatePed("a_f_y_skater_01", new Vector3(286.0219f, 2594.102f, 44.40743f), -69.00703f, false, "WORLD_HUMAN_CLIPBOARD");
			// Bike rent - Мишн-Роу - Алти-стрит
			await CreatePed("a_f_y_skater_01", new Vector3(387.2543f, -946.5811f, 29.42514f), -175.2946f, false, "WORLD_HUMAN_CLIPBOARD");
			// Boat rent - Ла-Пуэрта - Шэнк-стрит
			await CreatePed("a_m_y_runner_01", new Vector3(-790.4313f, -1453.044f, 1.596039f), -38.84312f, false, "WORLD_HUMAN_CLIPBOARD");
			// Boat rent - Бухта Палето - Шоссе Грейт-Оушн
			await CreatePed("a_f_y_runner_01", new Vector3(-1603.928f, 5251.08f, 3.974748f), 108.5822f, false, "WORLD_HUMAN_CLIPBOARD");
			// Boat rent - Сан-Шаньский горный хребет - Кэтфиш-Вью
			await CreatePed("a_m_y_runner_01", new Vector3(3867.177f, 4463.583f, 2.727666f), 73.1316f, false, "WORLD_HUMAN_CLIPBOARD");
	    
		    //Auto Repairs Mirror Park
		    await CreatePed("s_m_y_xmech_01", new Vector3(1126.111f, -780.718f, 57.62164f), 275.3218f, true);
		    
		    //Galaxy Club
		    await CreatePed("csb_sol", new Vector3(-1603.116f, -3012.589f, -77.79606f), 268.7613f, true, "WORLD_HUMAN_STAND_IMPATIENT_UPRIGHT");
		    await CreatePed("s_f_y_clubbar_01", new Vector3(-1584.91f, -3012.711f, -76.00503f), 92.36889f, true, "WORLD_HUMAN_STAND_IMPATIENT");
		    await CreatePed("s_m_y_clubbar_01", new Vector3(-1577.998f, -3016.774f, -79.0059f), 2.09483f, true, "WORLD_HUMAN_STAND_IMPATIENT");
		    await CreatePed("s_f_y_clubbar_01", new Vector3(-1572.174f, -3013.531f, -74.40615f), 268.5251f, true, "WORLD_HUMAN_STAND_IMPATIENT");
		    await CreatePed("s_m_y_doorman_01", new Vector3(-1568.171f, -3013.593f, -74.40615f), 99.50303f, true, "WORLD_HUMAN_GUARD_STAND");
		    await CreatePed("a_m_y_clubcust_01", new Vector3(-1582.093f, -3014.937f, -79.00597f), 272.4213f, true, "WORLD_HUMAN_AA_SMOKE");
		    await CreatePed("s_m_y_doorman_01", new Vector3(-1588.103f, -3014.392f, -79.00605f), 357.8188f, true, "WORLD_HUMAN_GUARD_STAND");
		    await CreatePed("a_m_y_clubcust_02", new Vector3(-1595.915f, -3007.505f, -76.00497f), 180.965f, true, "WORLD_HUMAN_DRINKING");
		    await CreatePed("u_m_m_jewelsec_01", new Vector3(-1592.343f, -3004.983f, -76.005f), 171.1091f, true, "WORLD_HUMAN_LEANING");
		    await CreatePed("a_f_y_clubcust_02", new Vector3(-1590.95f, -3018.558f, -76.005f), 353.0403f, true, "WORLD_HUMAN_PROSTITUTE_HIGH_CLASS");
		    await CreatePed("s_m_m_highsec_01", new Vector3(-1632.198f, -3009.134f, -78.05933f), 5.323215f, true, "WORLD_HUMAN_CLIPBOARD");
		    await CreatePed("u_m_y_dancerave_01", new Vector3(-1596.161f, -3007.892f, -78.21108f), 187.1315f, true, "", "amb@world_human_partying@female@partying_beer@idle_a", "idle_b");
		    await CreatePed("u_f_y_dancerave_01", new Vector3(-1598.525f, -3015.817f, -78.21107f), 326.4045f, true, "", "mp_safehouse", "lap_dance_girl");
		    
		    //Багама
		    await CreatePed("s_f_y_clubbar_01", new Vector3(-1392.119f, -604.5723f, 30.31955f), 115.109f, false, "WORLD_HUMAN_STAND_IMPATIENT_UPRIGHT"); 
		    await CreatePed("s_m_y_clubbar_01", new Vector3(-1375.474f, -628.1735f, 30.81958f), 32.54577f, false, "WORLD_HUMAN_STAND_IMPATIENT_UPRIGHT"); 
		    await CreatePed("csb_dix", new Vector3(-1381.417f, -616.2663f, 31.49791f), 118.8055f, false, "WORLD_HUMAN_STAND_IMPATIENT"); 
		    await CreatePed("s_m_m_bouncer_01", new Vector3(-1390.285f, -592.863f, 30.31956f), 118.8055f, false, "WORLD_HUMAN_GUARD_STAND"); 
		    await CreatePed("s_m_m_highsec_01", new Vector3(-1390.104f, -611.6881f, 30.31955f), 118.8055f, false, "WORLD_HUMAN_GUARD_STAND"); 
		    await CreatePed("s_m_m_highsec_02", new Vector3(-1385.458f, -628.8311f, 30.81957f), 306.6313f, false, "WORLD_HUMAN_GUARD_STAND"); 
		    await CreatePed("a_m_y_clubcust_02 ", new Vector3(-1381.426f, -632.3288f, 30.81956f), 23.77576f, false, "WORLD_HUMAN_DRINKING"); 
		    await CreatePed("a_f_y_clubcust_03", new Vector3(-1390.985f, -597.8441f, 30.31956f), 99.953f, false, "WORLD_HUMAN_PROSTITUTE_HIGH_CLASS"); 
		    await CreatePed("u_f_y_danceburl_01", new Vector3(-1379.885f, -617.5175f, 31.75787f), 117.7217f, false, "", "timetable@tracy@ig_5@idle_a", "idle_a"); 
		    await CreatePed("u_m_y_danceburl_01", new Vector3(-1383.338f, -612.1739f, 31.75784f), 122.0663f, false, "", "timetable@tracy@ig_5@idle_a", "idle_c"); 
		    
		    //Казино
		    await CreatePed("s_m_y_barman_01", new Vector3(-2057.389f, -1023.452f, 11.90751f), 201.855f, false, "WORLD_HUMAN_STAND_IMPATIENT");
		    await CreatePed("s_f_y_bartender_01", new Vector3(-2094.733f, -1014.989f, 8.98046f), 252.3773f, false, "WORLD_HUMAN_STAND_IMPATIENT_UPRIGHT");
		    await CreatePed("s_m_y_devinsec_01", new Vector3(-2090.859f, -1010.439f, 8.971148f), 184.4666f, false, "WORLD_HUMAN_GUARD_STAND");
		    await CreatePed("s_m_m_highsec_01", new Vector3(-2077.515f, -1014.104f, 12.77869f), 348.6507f, false, "WORLD_HUMAN_BINOCULARS");
		    await CreatePed("s_m_m_pilot_01", new Vector3(-2083.36f, -1018.152f, 12.7819f), 236.4131f, false, "WORLD_HUMAN_AA_COFFEE");
		    await CreatePed("s_m_m_highsec_01", new Vector3(-2072.56f, -1026.991f, 11.91077f), 346.0147f, false, "WORLD_HUMAN_GUARD_STAND");
		    await CreatePed("s_m_m_highsec_02", new Vector3(-2053.578f, -1021.199f, 11.90756f), 164.1931f, false, "CODE_HUMAN_CROSS_ROAD_WAIT");
		    await CreatePed("s_m_y_devinsec_01", new Vector3(-2036.385f, -1034.018f, 8.971494f), 251.8719f, false, "CODE_HUMAN_CROSS_ROAD_WAIT");
		    await CreatePed("s_m_m_highsec_02", new Vector3(-2020.781f, -1040.394f, 2.582928f), 246.5897f, false, "WORLD_HUMAN_CLIPBOARD");
		    
		    //ЙеловДжек
		    await CreatePed("a_m_m_hillbilly_02", new Vector3(1992.681f, 3052.793f, 47.21467f), 325.3711f, false, "WORLD_HUMAN_LEANING");
		    await CreatePed("a_f_y_genhot_01", new Vector3(1984.271f, 3054.644f, 47.21519f), 234.1265f, false, "WORLD_HUMAN_STAND_IMPATIENT_UPRIGHT");
		    await CreatePed("a_m_m_hillbilly_01", new Vector3(1987.624f, 3050.098f, 47.21511f), 62.54489f, false, "WORLD_HUMAN_COP_IDLES");
		    
		    //Ванилла
		    await CreatePed("s_m_y_doorman_01", new Vector3(130.3964f, -1297.915f, 29.23274f), 212.3112f, false, "WORLD_HUMAN_GUARD_STAND");
		    await CreatePed("s_f_y_bartender_01", new Vector3(129.0223f, -1282.954f, 29.27212f), 120.4467f, false, "WORLD_HUMAN_STAND_IMPATIENT");
		    await CreatePed("csb_g", new Vector3(120.7224f, -1281.19f, 29.48052f), 116.3616f, false, "WORLD_HUMAN_STAND_IMPATIENT_UPRIGHT");
		    await CreatePed("a_m_m_soucent_02", new Vector3(125.9214f, -1291.432f, 29.27164f), 29.20783f, false, "WORLD_HUMAN_LEANING");
		    await CreatePed("a_m_y_stbla_02", new Vector3(116.768f, -1296.403f, 29.26947f), 117.4593f, false, "WORLD_HUMAN_STAND_MOBILE");
		    await CreatePed("a_m_m_og_boss_01", new Vector3(115.3141f, -1281.251f, 28.26315f), 193.957f, false, "CODE_HUMAN_CROSS_ROAD_WAIT");
		    await CreatePed("s_f_y_stripper_02", new Vector3(104.0908f, -1292.182f, 29.25871f), 298.1895f, false, "", "mini@strip_club@lap_dance_2g@ld_2g_p3", "ld_2g_p3_s2");
		    await CreatePed("s_f_y_stripper_01", new Vector3(109.7466f, -1289.49f, 28.85873f), 211.7832f, false, "", "mp_safehouse", "lap_dance_girl");
		    
		    //Текилла
		    await CreatePed("u_m_y_guido_01", new Vector3(-564.2014f, 279.3034f, 82.97667f), 174.6297f, false, "WORLD_HUMAN_STAND_IMPATIENT");
		    await CreatePed("s_m_y_doorman_01", new Vector3(-559.8546f, 276.6152f, 82.97633f), 83.12157f, false, "WORLD_HUMAN_GUARD_STAND");
		    await CreatePed("u_m_y_party_01", new Vector3(-562.0344f, 286.1403f, 82.17637f), 264.906f, false, "WORLD_HUMAN_STAND_IMPATIENT_UPRIGHT");
		    await CreatePed("a_m_m_bevhills_02", new Vector3(-558.6783f, 279.6758f, 82.17625f), 256.2623f, false, "WORLD_HUMAN_LEANING");
		    await CreatePed("s_m_y_doorman_01", new Vector3(-565.6351f, 291.2729f, 85.37643f), 180.1021f, false, "WORLD_HUMAN_CLIPBOARD");
		    await CreatePed("s_f_y_bartender_01", new Vector3(-564.6986f, 287.3053f, 85.37729f), 260.9834f, false, "WORLD_HUMAN_STAND_IMPATIENT");
		    await CreatePed("ig_dale", new Vector3(-560.601f, 281.9693f, 85.67645f), 261.9857f, false, "WORLD_HUMAN_STAND_MOBILE");
		    await CreatePed("s_m_m_bouncer_01", new Vector3(-569.0171f, 291.8075f, 79.17664f), 266.5707f, false, "WORLD_HUMAN_LEANING");
		   
		    
		    if (Main.ServerName == "MilkyWay")
		    {
			    // MW_House621
			    await CreatePed("s_m_m_highsec_02", new Vector3(865.6689f, -575.504f, 57.39248f), -73.45435f, false, "WORLD_HUMAN_GUARD_STAND");
			    
			    // MW_Benny
				await CreatePed("mp_f_bennymech_01", new Vector3(-199.5539f, -1315.21f, 31.08936f), 6.954351f, false, "WORLD_HUMAN_STAND_IMPATIENT");
				await CreatePed("s_m_y_xmech_02_mp", new Vector3(-195.9207f, -1320.746f, 31.08936f), 55.93908f, false, "WORLD_HUMAN_STAND_MOBILE");
				await CreatePed("mp_m_weapexp_01", new Vector3(-227.6785f, -1326.488f, 30.89039f), -136.2915f, false, "CODE_HUMAN_MEDIC_TIME_OF_DEATH");
				await CreatePed("mp_m_waremech_01", new Vector3(-227.8101f, -1327.341f, 30.89039f), -25.05675f, false, "WORLD_HUMAN_JANITOR");
				await CreatePed("s_m_y_dwservice_01", new Vector3(-205.621f, -1339.558f, 34.89401f), 62.54784f, false, "WORLD_HUMAN_AA_COFFEE");
				await CreatePed("s_m_m_autoshop_01", new Vector3(-204.8545f, -1327.568f, 30.89039f), -5.008956f, false, "WORLD_HUMAN_LEANING");
				
				
				// MW_House1126
				await CreatePed("s_m_m_highsec_01", new Vector3(-137.6827f, 974.5458f, 235.75f), -99.98929f, false, "WORLD_HUMAN_GUARD_STAND");
				await CreatePed("s_m_m_highsec_02", new Vector3(-132.2644f, 972.4172f, 235.7416f), 64.99982f, false, "WORLD_HUMAN_GUARD_STAND");
				await CreatePed("a_f_y_soucent_02", new Vector3(-106.3265f, 961.6937f, 233.3073f), 116.0001f, false, "WORLD_HUMAN_GARDENER_PLANT");
				await CreatePed("s_m_m_gardener_01", new Vector3(-62.68492f, 940.9781f, 232.4227f), 70.99993f, false, "WORLD_HUMAN_GARDENER_LEAF_BLOWER");
				await CreatePed("s_m_y_devinsec_01", new Vector3(-110.9386f, 999.0899f, 240.8519f), 28.40079f, false, "CODE_HUMAN_CROSS_ROAD_WAIT");
				await CreatePed("u_m_m_jewelsec_01", new Vector3(-93.59818f, 987.443f, 240.9464f), -152.9994f, false, "WORLD_HUMAN_STAND_IMPATIENT");
				await CreatePed("s_m_m_highsec_01", new Vector3(-61.13178f, 978.3408f, 232.8693f), -153.9992f, false, "WORLD_HUMAN_STAND_IMPATIENT_UPRIGHT");
				await CreatePed("s_m_m_highsec_02", new Vector3(-82.46698f, 942.5785f, 233.0286f), 0.09982711f, false, "WORLD_HUMAN_STAND_IMPATIENT");
				await CreatePed("s_m_y_devinsec_01", new Vector3(-113.0847f, 983.8484f, 235.7563f), 119.1996f, false, "WORLD_HUMAN_GUARD_STAND");
				await CreatePed("u_m_m_jewelsec_01", new Vector3(-48.72831f, 951.6181f, 232.1743f), -173.2996f, false, "WORLD_HUMAN_GARDENER_LEAF_BLOWER");
				await CreatePed("s_m_m_highsec_01", new Vector3(-68.40871f, 1007.543f, 234.3994f), -34.9999f, false, "WORLD_HUMAN_STAND_IMPATIENT_UPRIGHT");
				await CreatePed("a_f_m_eastsa_02", new Vector3(-80.19171f, 980.4577f, 234.1708f), -134.9996f, false, "WORLD_HUMAN_GARDENER_PLANT");
								
				// MW_House635 (МилкиВей)
			    await CreatePed("a_c_husky", new Vector3(1386.469f, -567.5695f, 73.87412f), 139.5668f, false, "");
			    
			    //pb
			    await CreatePed("s_m_m_highsec_02", new Vector3(-1456.371f, -234.2658f, 49.79763f), -125.4425f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("s_m_m_highsec_01", new Vector3(-714.8142f, -157.6767f, 37.41516f), -50.99994f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("s_m_m_highsec_02", new Vector3(-156.6874f, -303.4997f, 39.7333f), 80.91859f, false, "WORLD_HUMAN_GUARD_STAND");
			    
			    //638
			    await CreatePed("a_c_rottweiler", new Vector3(1334.308f, -549.1798f, 72.66816f), 84.60688f, false, "");
			    //630
			    await CreatePed("g_m_y_mexgang_01", new Vector3(1300.315f, -571.6823f, 71.73909f), -34.99988f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("a_c_husky", new Vector3(1298.647f, -572.245f, 71.27047f), -15.20612f, false, "");
			    //634
			    await CreatePed("g_m_y_mexgang_01", new Vector3(1385.052f, -591.235f, 74.33883f), 97.11836f, false, "WORLD_HUMAN_GUARD_STAND");
			    //631
			    await CreatePed("s_m_m_highsec_01", new Vector3(1323.312f, -580.5349f, 73.2123f), -34.86584f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("s_m_m_highsec_02", new Vector3(1325.463f, -581.5007f, 73.1956f), 3.299999f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			    //1126
			    await CreatePed("a_f_m_beach_01", new Vector3(-74.09051f, 937.4064f, 233.0286f), -2.140633f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			    //Medvedev
			    await CreatePed("a_f_y_beach_01", new Vector3(-1434.912f, 208.3563f, 57.82212f), 120.6265f, false, "WORLD_HUMAN_STRIP_WATCH_STAND");
			    await CreatePed("s_m_m_highsec_02", new Vector3(-1437.21f, 198.3388f, 57.4277f), -13.30951f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("s_m_m_highsec_01", new Vector3(-1578.033f, 154.2689f, 58.61543f), 31.99976f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("s_m_m_highsec_02", new Vector3(-1537.969f, 93.6037f, 56.77967f), -32.27812f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("s_m_m_highsec_02", new Vector3(-1437.671f, 213.2588f, 57.82664f), 156.9993f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("u_m_m_jewelsec_01", new Vector3(-1462.902f, 64.80191f, 52.89207f), -144.9995f, false, "WORLD_HUMAN_COP_IDLES");
			    await CreatePed("s_m_y_devinsec_01", new Vector3(-1441.452f, 170.808f, 55.67257f), -115.4814f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("s_m_m_highsec_01", new Vector3(-1432.78f, 234.8961f, 59.99134f), -44.99718f, false, "WORLD_HUMAN_COP_IDLES");
			    await CreatePed("s_m_m_highsec_02", new Vector3(-1569.499f, 116.88f, 59.17944f), -82.50211f, false, "WORLD_HUMAN_SMOKING");
			    await CreatePed("u_m_m_jewelsec_01", new Vector3(-1520.506f, 71.15067f, 61.31355f), 66.0073f, false, "WORLD_HUMAN_GUARD_STAND");
			    
			    //Triada
			    await CreatePed("a_m_y_ktown_01", new Vector3(-395.6711f, 148.3317f, 65.7228f), 90.22314f, false, "WORLD_HUMAN_LEANING");
			    await CreatePed("g_m_y_korean_01", new Vector3(-396.2311f, 148.9898f, 65.72287f), 181.8046f, false, "WORLD_HUMAN_SMOKING");
			    await CreatePed("a_m_y_ktown_02", new Vector3(-400.4317f, 152.2468f, 65.52409f), -91.99957f, false, "WORLD_HUMAN_DRUG_DEALER");
			    await CreatePed("g_m_y_korean_02", new Vector3(-399.6681f, 153.2239f, 65.52409f), -167.9997f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("u_m_m_edtoh", new Vector3(-408.6283f, 153.6015f, 73.74726f), -94.99969f, false, "PROP_HUMAN_BUM_SHOPPING_CART");
			    
			    //Arc Motors
			    await CreatePed("s_m_y_xmech_01", new Vector3(-145.4404f, -584.6904f, 167.0001f), 74.41486f, true, "WORLD_HUMAN_CLIPBOARD");
			    await CreatePed("s_m_m_highsec_02", new Vector3(-142.3815f, -586.4323f, 167.0001f), 221.4349f, false, "WORLD_HUMAN_LEANING"); 
			    await CreatePed("s_m_m_highsec_01", new Vector3(-139.9419f, -594.3448f, 167.0001f), 37.87105f, false, "WORLD_HUMAN_GUARD_STAND"); 
			    await CreatePed("mp_m_weapwork_01", new Vector3(-150.3959f, -605.8906f, 167.0002f), 107.5879f, false, "WORLD_HUMAN_MAID_CLEAN"); 
			    await CreatePed("mp_m_waremech_01", new Vector3(-156.5627f, -589.2254f, 167.0001f), 48.45295f, false, "WORLD_HUMAN_STAND_IMPATIENT"); 
			    await CreatePed("s_m_y_devinsec_01", new Vector3(-148.6546f, -580.074f, 32.59664f), 163.5633f, false, "CODE_HUMAN_CROSS_ROAD_WAIT"); 
			    await CreatePed("u_m_m_jewelsec_01", new Vector3(-142.7623f, -582.204f, 32.59211f), 168.2717f, false, "WORLD_HUMAN_GUARD_STAND");  
			    
			    //Mara
			    await CreatePed("g_m_y_salvagoon_01", new Vector3(1157.851f, -1643.692f, 36.9641f), 208.6681f, true, "WORLD_HUMAN_LEANING");
			    await CreatePed("g_m_y_salvagoon_03", new Vector3(1167.329f, -1657.836f, 36.80291f), 194.4609f, true, "WORLD_HUMAN_GUARD_STAND");
			    
			    //Invader
			    await CreatePed("s_m_m_highsec_01", new Vector3(-1070.441f, -241.8547f, 42.2711f), -177.083f, true, "WORLD_HUMAN_CLIPBOARD");
			    
			    //h1000
			    await CreatePed("s_m_y_devinsec_01", new Vector3(-2553.185f, 1914.876f, 169.0382f), 192.4609f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("s_m_m_highsec_01", new Vector3(-2589.459f, 1913.051f,167.499f), 275.0403f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("s_m_m_highsec_02", new Vector3(-2607.178f, 1893.856f, 167.3201f), 99.1884f, false, "CODE_HUMAN_CROSS_ROAD_WAIT"); 
			    
			    //h254
			    await CreatePed("u_m_m_jewelsec_01", new Vector3(-1540.591f, 129.3182f, 56.78024f), 141.9995f, true, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("a_c_chop", new Vector3(-1540.974f, 129.5253f, 56.20662f), 138.9997f, true);
			    await CreatePed("s_m_m_highsec_02", new Vector3(-1465.686f, 63.48855f, 52.98018f), 145.356f, true, "WORLD_HUMAN_CLIPBOARD");
			    await CreatePed("s_m_m_highsec_01", new Vector3(-1476.765f, 62.62791f, 53.54455f), -113.9994f, true, "WORLD_HUMAN_SMOKING");
			    await CreatePed("s_m_m_highsec_02", new Vector3(-1621.417f, 76.76521f, 61.70078f), -160.9918f, true, "WORLD_HUMAN_CLIPBOARD");
			    await CreatePed("s_m_m_highsec_01", new Vector3(-1611.005f, 71.36069f, 61.35305f), 157.2253f, true, "", "random@arrests", "generic_radio_chatter", 49);
			    
			    //Legion
			    /*await CreatePed("s_m_y_construct_01", new Vector3(151.4028f, -994.296f, 29.35422f), -32.4312f, true, "WORLD_HUMAN_CONST_DRILL");
			    await CreatePed("s_m_y_construct_02", new Vector3(163.0518f, -998.0789f, 30.32196f), 129.813f, true, "WORLD_HUMAN_CONST_DRILL");
			    await CreatePed("s_m_y_construct_01", new Vector3(155.8922f, -993.4783f, 29.83535f), 32.44175f, true, "WORLD_HUMAN_CONST_DRILL");
			    await CreatePed("s_m_y_garbage", new Vector3(172.7315f, -1004.029f, 29.33721f), 157.9998f, true, "WORLD_HUMAN_CLIPBOARD");
			    await CreatePed("s_m_y_construct_02", new Vector3(202.1781f, -1019.751f, 29.37678f), 105.0006f, true, "WORLD_HUMAN_JANITOR");
			    await CreatePed("s_m_y_garbage", new Vector3(194.2207f, -1009.241f, 29.30713f), 123.9997f, true, "WORLD_HUMAN_SMOKING");
			    await CreatePed("s_m_y_construct_02", new Vector3(148.5554f, -997.1383f, 29.35715f), -67.96685f, true, "WORLD_HUMAN_AA_COFFEE");
			    await CreatePed("s_m_y_construct_01", new Vector3(161.5838f, -1002.704f, 29.35312f), 14.99998f, true, "WORLD_HUMAN_CONST_DRILL");
			    await CreatePed("s_m_y_construct_02", new Vector3(183.5627f, -1007.781f, 30.02006f), -7.999996f, true, "WORLD_HUMAN_CONST_DRILL");
			    await CreatePed("s_m_y_garbage", new Vector3(171.9563f, -1001.102f, 29.34045f), 154.6703f, true, "WORLD_HUMAN_HAMMERING"); */
		    }
		    else if (Main.ServerName == "Sombrero")
		    {   
			    //  S_BlaineBank (Сомбреро)
			    await CreatePed("s_m_m_security_01", new Vector3(-105.6016f, 6470.556f, 31.62671f), 176.488f, false, "WORLD_HUMAN_AA_COFFEE");
			    await CreatePed("s_m_m_security_01", new Vector3(-114.6676f, 6462.918f, 31.46845f), 142.9996f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("s_m_m_security_01", new Vector3(-140.0592f, 6462.314f, 31.67567f), -147.9575f, false, "WORLD_HUMAN_CLIPBOARD");
			    
			    //1139
			    await CreatePed("s_m_y_doorman_01", new Vector3(-1366.121f, 6733.49f, 2.582926f), -99.96954f, false, "WORLD_HUMAN_CLIPBOARD");
			    await CreatePed("s_m_m_highsec_01", new Vector3(-1381.059f, 6742.416f, 8.971489f), -96.99951f, false, "CODE_HUMAN_CROSS_ROAD_WAIT");
			    await CreatePed("s_m_m_highsec_02", new Vector3(-1398.979f, 6752.728f, 11.90755f), 170.9992f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("s_m_y_barman_01", new Vector3(-1402.902f, 6750.727f, 11.90751f), -158.9991f, false, "WORLD_HUMAN_STAND_IMPATIENT");
			    await CreatePed("s_m_m_linecook", new Vector3(-1404.997f, 6743.973f, 11.90752f), -41.00029f, false, "WORLD_HUMAN_STAND_IMPATIENT_UPRIGHT");
			    await CreatePed("s_m_m_highsec_01", new Vector3(-1417.741f, 6747.027f, 11.91074f), -6.999996f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("s_m_m_pilot_01", new Vector3(-1430.72f, 6756.944f, 12.7819f), 47.99963f, false, "WORLD_HUMAN_BINOCULARS");
			    await CreatePed("s_m_m_highsec_02", new Vector3(-1436.208f, 6763.711f, 8.971146f), -178.9993f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("mp_f_chbar_01", new Vector3(-1439.464f, 6760.174f, 8.980458f), -49.00003f, false, "PROP_HUMAN_PARKING_METER");
			    await CreatePed("s_m_m_highsec_01", new Vector3(-1460.87f, 6768.025f, 7.799459f), 63.99968f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("s_m_m_highsec_02", new Vector3(-1428.429f, 6749.271f, 5.884088f), 76.20076f, false, "WORLD_HUMAN_GUARD_STAND");
			    
			    //pacific
			    await CreatePed("u_m_m_jewelsec_01", new Vector3(244.6015f, 212.9023f, 106.2868f), 75.73093f, false, "WORLD_HUMAN_CLIPBOARD");
			    await CreatePed("s_m_m_security_01", new Vector3(263.4272f, 219.1586f, 101.6832f), -40.45869f, false, "WORLD_HUMAN_AA_COFFEE");
			    
			    //Arc Motors
			    await CreatePed("a_f_m_beach_01", new Vector3(-140.2769f, -594.1744f, 167.0001f), 53.89476f, false, "", "mini@strip_club@lap_dance_2g@ld_2g_p3", "ld_2g_p3_s2");
			    await CreatePed("a_f_y_beach_01", new Vector3(-143.4031f, -586.8325f, 167.0001f), -157.1466f, false, "", "amb@world_human_partying@female@partying_beer@idle_a", "idle_b");
			    
			    //h623
			    await CreatePed("cs_paper", new Vector3(905.8243f, -616.5538f, 58.04898f), 316.3369f, true, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("csb_reporter", new Vector3(903.2858f, -614.2041f, 58.04898f), 310.8976f, true, "WORLD_HUMAN_LEANING");
		    }
		    else if (Main.ServerName == "Andromeda")
		    {
			    //h1077
			    await CreatePed("g_m_y_ballaeast_01", new Vector3(110.2269f, -1962.862f, 20.94876f), 22.61524f, false, "WORLD_HUMAN_AA_SMOKE");
			    await CreatePed("g_m_y_ballaorig_01", new Vector3(110.2392f, -1975.529f, 20.93855f), -67.69401f, false, "WORLD_HUMAN_GUARD_STAND");
			    
			    //604
			    await CreatePed("s_m_y_doorman_01", new Vector3(1096.046f, -420.5536f, 67.19109f), 3.686397f, false, "WORLD_HUMAN_GUARD_STAND");
			    await CreatePed("s_m_m_highsec_01", new Vector3(1113.043f, -417.0046f, 67.15672f), 176.9997f, false, "WORLD_HUMAN_AA_SMOKE");
			    await CreatePed("s_m_m_mariachi_01", new Vector3(1121.048f, -408.9032f, 67.15778f), 148.0248f, false, "WORLD_HUMAN_MUSICIAN");
		    }
		    else if (Main.ServerName == "SunFlower")
		    {
			    //rent lapuerta
			    await CreatePed("s_m_y_xmech_02_mp", new Vector3(-703.0923f, -1398.897f, 5.496182f), 84.95451f, false, "WORLD_HUMAN_CLIPBOARD");
		    }
	    }
    }
}

public class PedData
{
	public CitizenFX.Core.Ped ped { get; set; }
	public Model model { get; set; }
	public Vector3 pos { get; set; }
	public float rot { get; set; }
	public string scenario { get; set; }
	public string animation1 { get; set; }
	public string animation2 { get; set; }
	public int flag { get; set; }
	public bool isDefault { get; set; }
	public bool isCreate { get; set; }
}