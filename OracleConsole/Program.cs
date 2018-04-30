using System;
using System.Data;
using Maticsoft.DBUtility;

namespace OracleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            OracleControl();
        }
        static void OracleControl()
        { 
            try
            {
                string sql = "select * from T_EPM_VOCSALPARAMETER t";
                DataSet ds = DbHelperOra.Query(sql);
                string dsStr = ds.ToString();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
