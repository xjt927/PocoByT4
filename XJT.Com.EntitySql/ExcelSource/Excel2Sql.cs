using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XJT.Com.EntitySql.Common;

namespace XJT.Com.EntitySql.ExcelSource
{
    public class Excel2Sql
    {
        public void Excel2SqlDoWork(List<TableEntity> tableEntitys)
        {
            string exceptionLog = "\r\n\r\n";//异常信息
            Tools.LogAction("开始生成Sql脚本！", EnumCommon.LogEnum.SqlLog);
            var sqlContent = GenerateSql(tableEntitys, ref exceptionLog);
            if (string.IsNullOrWhiteSpace(exceptionLog))
            {
                Tools.File2Path(Tools.GeneratePath + "\\Excel2Sql.sql", sqlContent, EnumCommon.LogEnum.SqlLog);
                Tools.LogAction("生成Sql脚本成功！", EnumCommon.LogEnum.SqlLog);
            }
            else
            {
                Tools.LogAction("生成Sql脚本失败！", EnumCommon.LogEnum.SqlLog);
            }
        }

        /// <summary>
        /// 生成sql
        /// </summary>
        /// <param name="tableEntities"></param>
        /// <param name="exceptionLog"></param>
        public string GenerateSql(List<TableEntity> tableEntities, ref string exceptionLog)
        {
            string enTableName = "";
            string cnTableName = "";
            List<TableEntity> newTableEntities = new List<TableEntity>();
            try
            {
                SqlTemplate sqlTemplate = new SqlTemplate();
                StringBuilder generateSql = new StringBuilder();
                foreach (TableEntity tableEntity in tableEntities)
                { 
                    enTableName = tableEntity.EnglishName;
                    cnTableName = tableEntity.ChineseName;
                    string cuttingLine = "  ".PadLeft(50, '-') + cnTableName + "  " + enTableName + "  ".PadRight(50, '-') + "\r\n";
                    generateSql.Append(cuttingLine);
                    StringBuilder columnTemplateBuild = new StringBuilder();
                    string primaryKeyTemplate = "";
                    StringBuilder columnCommentBuild = new StringBuilder();
                    string uniqueTemplate = "";
                    StringBuilder foreignKeyTemplateBuild = new StringBuilder();

                    #region sql拼接

                    #region 列及注释
                    string tableComment = string.Format(sqlTemplate.TableComment, enTableName, cnTableName);
                    columnCommentBuild.Append(tableComment);
                    foreach (ColumnEntity columnEntity in tableEntity.ColumnEntityList)
                    {
                        if (string.IsNullOrWhiteSpace(columnEntity.EnglishNameCol) || string.IsNullOrWhiteSpace(columnEntity.ChineseNameCol) || string.IsNullOrWhiteSpace(columnEntity.TypeCol))
                        {
                            Tools.LogAction("《" + cnTableName + "》表结构有问题，请查看。", EnumCommon.LogEnum.SqlLog);
                        }
                        columnTemplateBuild.Append(string.Format(sqlTemplate.ColumnTemplate + " ,\r\n", columnEntity.EnglishNameCol.ToLower().PadRight(20),
                             columnEntity.TypeCol, string.IsNullOrWhiteSpace(columnEntity.DefaultValue) ? "" : "default " + columnEntity.DefaultValue, columnEntity.IsNullCol ? "" : "not null"));

                        columnCommentBuild.Append(string.Format(sqlTemplate.ColumnComment, enTableName,
                            columnEntity.EnglishNameCol.ToLower(), columnEntity.ChineseNameCol));
                    }
                    #endregion

                    #region 主键

                    primaryKeyTemplate = string.Format(sqlTemplate.PrimaryKeyTemplate, enTableName,
                        "PK" + enTableName.Substring(1), tableEntity.ColumnEntityList.ToList().Find(x => x.IsPrimaryKey).EnglishNameCol);
                    if (("PK" + enTableName.Substring(1)).Length > 30)
                    {
                        Tools.LogAction("《" + cnTableName + "》表的主键名称超过30个字符 " + "PK" + enTableName.Substring(1), EnumCommon.LogEnum.SqlLog);
                    }
                    #endregion

                    #region 唯一约束

                    string colStrUnique = "";
                    var enNameColList = tableEntity.ColumnEntityList.ToList().FindAll(x => x.IsUnique).Select(x => x.EnglishNameCol).ToList();
                    if (enNameColList.Count == 1)
                    {
                        colStrUnique = enNameColList[0];
                    }
                    else
                    {
                        enNameColList.ToList().ForEach(u =>
                        {
                            string[] colList = u.Split('_');
                            if (colList.Length > 1)
                            {
                                colList.ToList().ForEach(x =>
                                {
                                    if (!string.IsNullOrWhiteSpace(x))
                                    {
                                        colStrUnique += x.Substring(0, 1);
                                    }
                                });
                            }
                            else
                            {
                                colStrUnique += u.Substring(0, 1);
                            }
                        });
                    }
                    if (!string.IsNullOrWhiteSpace(colStrUnique))
                    {
                        uniqueTemplate = string.Format(sqlTemplate.UniqueTemplate, enTableName, "UK" + enTableName.Substring(1) + "_" + colStrUnique,
                           string.Join(",", enNameColList));
                        if (("UK" + enTableName.Substring(1) + "_" + colStrUnique).Length > 30)
                        {
                            Tools.LogAction("《" + cnTableName + "》表的唯一约束名称超过30个字符 " + ("UK" + enTableName.Substring(1) + "_" + colStrUnique), EnumCommon.LogEnum.SqlLog);
                        }
                    }

                    #endregion

                    #region 外键关联 

                    foreach (ColumnEntity columnEntity in tableEntity.ColumnEntityList.ToList().FindAll(x => x.IsForeignKey))
                    {
                        string foreignTable = "";
                        string foreignPrimaryKey = "";
                        var tbEntity = Tools.StorageTables.Find(x => x.ChineseName == columnEntity.AssociatedTable);
                        if (columnEntity.AssociatedTable == "机构单元\n（AAA平台表）")
                        {
                            foreignTable = "IP.T_AAA_OU";
                            foreignPrimaryKey = "OU_ID";
                        }
                        else if (tbEntity != null)
                        {
                            foreignTable = tbEntity.EnglishName;
                            var keyEntity = tbEntity.ColumnEntityList.ToList().Find(x => x.IsPrimaryKey);
                            if (keyEntity != null)
                            {
                                foreignPrimaryKey = keyEntity.EnglishNameCol;
                            }
                        }

                        if (string.IsNullOrWhiteSpace(foreignTable) || string.IsNullOrWhiteSpace(foreignPrimaryKey))
                        {

                            exceptionLog += "当前表为：" + cnTableName + "  " + enTableName + "，找不到外键表：" + columnEntity.AssociatedTable + "\r\n";
                        }
                        string[] colList = columnEntity.EnglishNameCol.Split('_');
                        string colStr = "";
                        if (colList.Length > 1)
                        {
                            colList.ToList().ForEach(x =>
                            {
                                if (!string.IsNullOrWhiteSpace(x))
                                {
                                    colStr += x.Substring(0, 1);
                                }
                            });
                        }
                        else
                        {
                            colStr += columnEntity.EnglishNameCol.Substring(0, 1);
                        }

                        foreignKeyTemplateBuild.Append(string.Format(sqlTemplate.ForeignKeyTemplate, enTableName,
                            "FK" + enTableName.Substring(1) + "_" + colStr, columnEntity.EnglishNameCol, foreignTable, foreignPrimaryKey));
                        if (("FK" + enTableName.Substring(1) + "_" + colStr).Length > 30)
                        {
                            Tools.LogAction("《" + cnTableName + "》表的外键关联名称超过30个字符 " + "FK" + enTableName.Substring(1) + "_" + colStr, EnumCommon.LogEnum.SqlLog);
                        }
                    }
                    #endregion
                    #endregion

                    string columnTemplateStr = columnTemplateBuild.ToString().Substring(0, columnTemplateBuild.ToString().Length - ",\r\n".Length);
                    generateSql.Append(string.Format(sqlTemplate.CreateTableSql, enTableName, columnTemplateStr,
                       columnCommentBuild, primaryKeyTemplate, uniqueTemplate, foreignKeyTemplateBuild, "S" + enTableName.Substring(1)));
                }
                if (!string.IsNullOrWhiteSpace(exceptionLog))
                {
                    Tools.LogAction(exceptionLog, EnumCommon.LogEnum.SqlLog);
                }
                return generateSql.ToString();
            }
            catch (Exception e)
            {
                exceptionLog += e.ToString();
                Tools.LogAction(cnTableName + "表有问题。" + e.ToString(), EnumCommon.LogEnum.SqlLog);
                return "出问题了";
            }
        }

    }
}
