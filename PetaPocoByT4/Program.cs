using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Windows.Forms;

namespace PetaPocoByT4
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Data.OracleClient.OracleConnection conn =
                 new OracleConnection();
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["oradb"].ConnectionString;
                conn.Open();
                string sql = " select * from T_EPM_VOCSALFORMULA"; // C#
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                OracleDataReader dr = cmd.ExecuteReader(); // C#
                List<string> contents = new List<string>();
                while (dr.Read())
                {
                    contents.Add(dr["content"].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //conn.Clone();
            }
        }
    }
}
