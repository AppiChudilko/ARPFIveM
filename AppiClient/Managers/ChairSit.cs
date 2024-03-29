﻿using System;
using System.Collections.Generic;
using CitizenFX.Core;

namespace Client.Managers
{
    public class ChairSit : BaseScript
    {
        public static readonly dynamic[,] ItemList =
        {
            /*prop, task, x, y, z, rot) */
            { "prop_bench_01a", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.5, 180.0},
			{ "prop_bench_01b", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.5, 180.0},
			{ "prop_bench_01c", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.5, 180.0},
			{ "prop_bench_02", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.5, 180.0},
			{ "prop_bench_03", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.5, 180.0},
			{ "prop_bench_04", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.5, 180.0},
			{ "prop_bench_05", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.5, 180.0},
			{ "prop_bench_06", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.5, 180.0},
			{ "prop_bench_05", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.5, 180.0},
			{ "prop_bench_08", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.5, 180.0},
			{ "prop_bench_09", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.5, 180.0},
			{ "prop_bench_10", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.5, 180.0},
			{ "prop_bench_11", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.5, 180.0},
			{ "prop_fib_3b_bench", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.5, 180.0},
			{ "prop_ld_bench01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.5, 180.0},
			{ "prop_wait_bench_01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.5, 180.0},

			//CHAIR
			{ "hei_prop_heist_off_chair", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "hei_prop_hei_skid_chair", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_chair_01a", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_chair_01b", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_chair_02", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_chair_03", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_chair_04a", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_chair_04b", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_chair_05", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_chair_06", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_chair_05", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_chair_08", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_chair_09", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_chair_10", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_chateau_chair_01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_clown_chair", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_cs_office_chair", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_direct_chair_01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_direct_chair_02", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_gc_chair02", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_off_chair_01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_off_chair_03", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_off_chair_04", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_off_chair_04b", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_off_chair_04_s", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_off_chair_05", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_old_deck_chair", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_old_wood_chair", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_rock_chair_01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_skid_chair_01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_skid_chair_02", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_skid_chair_03", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_sol_chair", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_wheelchair_01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_wheelchair_01_s", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "p_armchair_01_s", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "p_clb_officechair_s", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "p_dinechair_01_s", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "p_ilev_p_easychair_s", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "p_soloffchair_s", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "p_yacht_chair_01_s", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_club_officechair", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_corp_bk_chair3", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_corp_cd_chair", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_corp_offchair", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_ilev_chair02_ped", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_ilev_hd_chair", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_ilev_p_easychair", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_ret_gc_chair03", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_ld_farm_chair01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_table_04_chr", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_table_05_chr", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_table_06_chr", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_ilev_leath_chr", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_table_01_chr_a", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_table_01_chr_b", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_table_02_chr", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_table_03b_chr", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_table_03_chr", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_torture_ch_01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_ilev_fh_dineeamesa", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},


			{ "v_ilev_fh_kitchenstool", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_ilev_tort_stool", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_ilev_fh_kitchenstool", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_ilev_fh_kitchenstool", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_ilev_fh_kitchenstool", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_ilev_fh_kitchenstool", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			
			//SEAT
			{ "hei_prop_yah_seat_01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "hei_prop_yah_seat_02", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "hei_prop_yah_seat_03", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_waiting_seat_01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_yacht_seat_01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_yacht_seat_02", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_yacht_seat_03", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_hobo_seat_01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},

			//COUCH
			{ "prop_rub_couch01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "miss_rub_couch_01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_ld_farm_couch01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_ld_farm_couch02", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_rub_couch02", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_rub_couch03", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_rub_couch04", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},

			//SOFA
			{ "p_lev_sofa_s", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "p_res_sofa_l_s", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "p_v_med_p_sofa_s", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "p_yacht_sofa_01_s", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_ilev_m_sofa", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_res_tre_sofa_s", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_tre_sofa_mess_a_s", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_tre_sofa_mess_b_s", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "v_tre_sofa_mess_c_s", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},

			//MISC
			{ "prop_roller_car_01", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
			{ "prop_roller_car_02", "PROP_HUMAN_SEAT_BENCH", 0.0, 0.0, 0.0, -90.0},
        };
    }
}