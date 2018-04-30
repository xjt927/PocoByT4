/******************************************************************************** 
** Copyright(c) 2015  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2015/11/28 22:57:04 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System;
using System.Configuration;

namespace Oracle.Common
{ 
    public class ConfigeCom
    {
        private const string _oraConnection = "";

        public static string OracleConnection
        {
            get
            {

                if (string.IsNullOrWhiteSpace(_oraConnection))
                {
                    try
                    {
                        return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    return _oraConnection;
                }
            }
        }
    }
}
