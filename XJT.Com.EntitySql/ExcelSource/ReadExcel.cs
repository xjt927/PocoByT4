using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using XJT.Com.EntitySql.Common;

namespace XJT.Com.EntitySql.ExcelSource
{
    /// <summary>
    /// 读取Excel
    /// </summary>
    class ReadExcel
    {
        /// <summary>
        /// 获取excel表字段
        /// </summary>
        /// <returns></returns>
        public List<TableEntity> GetTableEntitys(IEnumerable<string> filePathList, IEnumerable<string> ignoreSheet)
        {
            Tools.LogAction("获取Excel中的表！", EnumCommon.LogEnum.CommonLog);
            var tableEntiys = new List<TableEntity>();
            try
            {
                foreach (var filePath in filePathList)
                {
                    Tools.LogAction("获取Excel：" + Path.GetFileName(filePath), EnumCommon.LogEnum.CommonLog);
                    FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    var tableEntity = new TableEntity();
                    int tbIndex = 0;
                    DataSet dt = NPOIHelper.ImportExceltoDs(fileStream, ignoreSheet);
                    foreach (DataTable table in dt.Tables)
                    {
                        tbIndex++;
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            if ((table.Rows[i][0].ToString().Trim() == "中文名称" && table.Rows[i][1].ToString().Trim() != "英文名称")
                                || table.Rows[i][0].ToString().Trim() == "中文表名：" && table.Rows[i][1].ToString().Trim() != "英文名称：")
                            {
                                tableEntity = new TableEntity();
                                tableEntity.ChineseName = table.Rows[i][1].ToString().Trim();
                                tableEntity.EnglishName = table.Rows[i + 1][1].ToString().Trim();
                                if (tableEntity.EnglishName.Contains(" "))
                                {
                                    throw new Exception(tableEntity.ChineseName + tableEntity.EnglishName + "，英文表名中有空格，请检查。");
                                }
                                tableEntity.IsNewTable = table.Rows[i][2].ToString().Trim() == "新增表";
                                Tools.LogAction("获取表：" + tableEntity.ChineseName, EnumCommon.LogEnum.CommonLog);
                            }
                            else if ((table.Rows[i][0].ToString().Trim() == "中文名称" && table.Rows[i][1].ToString().Trim() == "英文名称")
                                || (table.Rows[i][0].ToString().Trim() == "中文名称：" && table.Rows[i][1].ToString().Trim() == "英文名称："))
                            {
                                i++;
                                var rowIndex = -1;
                                var colEntitys = new List<ColumnEntity>();
                                for (int j = 0; (i + j < table.Rows.Count) && (!string.IsNullOrWhiteSpace(table.Rows[i + j][0].ToString().Trim())); j++)
                                {
                                    rowIndex++;
                                    var colEntity = new ColumnEntity();
                                    colEntity.ChineseNameCol = table.Rows[i + j][0].ToString().Trim();
                                    colEntity.EnglishNameCol = table.Rows[i + j][1].ToString().Trim();
                                    colEntity.TypeCol = table.Rows[i + j][2].ToString().Trim();
                                    colEntity.IsNullCol = table.Rows[i + j][3].ToString().Trim().ToUpper() == "TRUE";
                                    colEntity.PropertyType = GetPropertyType(colEntity.TypeCol, colEntity.IsNullCol);
                                    colEntity.DefaultValue = table.Rows[i + j][4].ToString().Trim();
                                    colEntity.IsPrimaryKey = table.Rows[i + j][5].ToString().Trim().ToUpper() == "TRUE";
                                    colEntity.IsForeignKey = table.Rows[i + j][6].ToString().Trim().ToUpper() == "TRUE";
                                    colEntity.AssociatedTable = table.Rows[i + j][7].ToString().Trim();
                                    colEntity.IsUnique = table.Rows[i + j][8].ToString().Trim().ToUpper() == "TRUE";
                                    colEntitys.Add(colEntity);
                                }
                                i += rowIndex;
                                tableEntity.ColumnEntityList = colEntitys;
                                tableEntiys.Add(tableEntity);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return tableEntiys;
        }

        string GetPropertyType(string sqlType, bool isNullCol, string dataScale = "")
        {
            string sysType = "string";
            switch (sqlType.ToLower())
            {
                case "integer":
                case "int":
                case "tinyint":
                case "smallint":
                case "mediumint":
                case "int2":
                case "int8":
                    sysType = isNullCol ? "long?" : "long";
                    break;
                case "bigint":
                case "unsigned big int":
                    sysType = isNullCol ? "long?" : "long";
                    break;
                case "uniqueidentifier":
                    sysType = "Guid";
                    break;
                case "smalldatetime":
                case "datetime":
                case "date":
                    sysType = isNullCol ? "DateTime?" : "DateTime";
                    break;
                case "float":
                case "double precision":
                case "double":
                    sysType = isNullCol ? "double?" : "double";
                    break;
                case "real":
                case "numeric":
                case "smallmoney":
                case "decimal":
                case "money":
                case "number":
                    sysType = isNullCol ? "decimal?" : "decimal";
                    break;
                case "bit":
                    sysType = isNullCol ? "bool?" : "bool";
                    break;
                case "image":
                case "binary":
                case "varbinary":
                case "timestamp":
                    sysType = "byte[]";
                    break;
            }

            if (sqlType == "number" && dataScale == "0")
                return "long";

            return sysType;
        }
    }
}
