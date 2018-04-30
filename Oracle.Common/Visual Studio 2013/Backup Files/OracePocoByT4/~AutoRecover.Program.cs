using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;

namespace PetaPocoByT4
{
    class Program
    {
        static void Main(string[] args)
        {
            OracleConnection conn =
                new OracleConnection();
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["oradb"].ConnectionString;
                conn.Open();
                string sql = " select id,content from test"; // C#
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
                conn.Clone();
            }
        }
    }
}
