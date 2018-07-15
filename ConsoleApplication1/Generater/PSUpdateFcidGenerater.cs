﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YdService.Db;
using YdService.Util;

namespace YDIOTService.Generater
{
    public class PSUpdateFcidGenerater
    {
        private SqlHelper sqlHelper;
        private static HashSet<string> tables;
        private string table;
        public PSUpdateFcidGenerater(String dbName, String tableName)
        {
            sqlHelper = new SqlHelper(dbName);
            tables = new HashSet<string>();
            tables.Add("day");
            tables.Add("month");
            tables.Add("year");
            tables.Add("day_electricity");
            tables.Add("month_electricity");
            tables.Add("year_electricity");
            this.table = tableName;
        }

        public void run()
        {
            if (tables.Contains(table))
            {
                string updateSql = @"UPDATE pd
            SET pd.fcid = fc.Facility_ID
            FROM
	            Polling_Log_Sta_" + table + @" pd,
	            Facility_Config fc,
	            [Usage] us
            WHERE
	            pd.mscid = fc.Msc_ID
            AND us.Usage_Name IN (" + CommonUtil.getUsageMatchStrFromConfig(table) + ");";
                sqlHelper.ExecteNonQueryText(updateSql);
            }

        }
    }
}