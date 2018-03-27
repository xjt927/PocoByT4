using System.ComponentModel;

namespace XJT.Com.EntitySql.Common
{
    public class EnumCommon
    {
        /// <summary>
        /// 日志模块
        /// </summary>
        public enum LogEnum
        {
            [Description("公共日志输出框")]
            CommonLog = 1,

            [Description("Entity日志输出框")]
            EntityLog = 2,

            [Description("Sql脚本日志输出框")]
            SqlLog = 3
        }

        /// <summary>
        /// 数据源模式
        /// </summary>
        public enum DataProviderEnum
        {
            [Description("Oracle")]
            OracleClient = 1,

            [Description("SqlServer")]
            SqlClient = 2,

            [Description("MySql")]
            MySqlClient = 3,

            [Description("SQLite")]
            SQLite = 4
        }

        /// <summary>
        /// 生成操作
        /// </summary>
        public enum GenerateEnum
        {
            [Description("全部")]
            All = 1,

            [Description("Entity实体")]
            Entity = 2,

            [Description("SQL脚本")]
            SQL = 3,

            [Description("Pojo实体")]
            Pojo = 4
        }
    }
}
