using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using XJT.Com.EntitySql.Common;

namespace XJT.Com.EntitySql.ExcelSource
{
    class Excel2Pojo
    {
        public void Excel2PojoDoWork(List<TableEntity> tableEntitys)
        {
            Tools.LogAction("\r\n", EnumCommon.LogEnum.EntityLog);
            Tools.LogAction("开始生成Pojo实体！", EnumCommon.LogEnum.EntityLog);
            GeneratePojo(tableEntitys);
            Tools.LogAction("生成Pojo实体结束！", EnumCommon.LogEnum.EntityLog);
        }

        /// <summary>
        /// 生成实体
        /// </summary>
        /// <param name="tableEntities"></param> 
        public void GeneratePojo(List<TableEntity> tableEntities)
        {
            string ignoreColumnNames = "crt_date,crt_user_id,crt_user_name,mnt_date,mnt_user_id,mnt_user_name";	//忽略的列
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string className = "";
            PojoTemplate pojoTemplate = new PojoTemplate();

            tableEntities.ForEach(x => x.ColumnEntityList.ToList().ForEach(z =>
              {
                  if (z.PropertyType == "decimal")
                  {
                      z.PropertyType = "Long";
                  }
                  else if (z.PropertyType == "decimal?")
                  {
                      z.PropertyType = "Integer";
                  }
                  else if (z.PropertyType == "int")
                  {
                      z.PropertyType = "Integer";
                  }
                  else if (z.PropertyType == "string")
                  {
                      z.PropertyType = "String";
                  }
              }));

            foreach (var tableEntity in tableEntities)
            {
                List<Tuple<string, string, string>> listAttribute = new List<Tuple<string, string, string>>();
                List<Tuple<string, string, string>> listForeignKey = new List<Tuple<string, string, string>>();

                string usingStr = " ";
                var enTableName = tableEntity.EnglishName;
                var cnTableName = tableEntity.ChineseName;
                string[] splits = enTableName.Split('_');
                if (splits.Length > 2)
                {
                    int startIndex = splits[0].Length + splits[1].Length + 2;
                    className = enTableName.Substring(startIndex);
                }
                StringBuilder columnBuilder = new StringBuilder();
                string fileDes = cnTableName;
                var foreignKey = "";

                bool isIncludeAll = false;
                var ignoreColuNames = ignoreColumnNames.Split(',');
                foreach (string coluName in ignoreColuNames)
                {
                    isIncludeAll = false;
                    foreach (var column in tableEntity.ColumnEntityList)
                    {
                        if (column.EnglishNameCol.ToLower() == coluName.ToLower())
                        {
                            isIncludeAll = true;
                            break;
                        }
                    }
                    if (!isIncludeAll)
                    {
                        break;
                    }
                }

                #region 类文件注释

                var classNode = pojoTemplate.ClassNode.Replace("{FileDes}", fileDes)
                    .Replace("{ModelNum}", "pcitc_pojo_class_" + className)
                    .Replace("{Time}", time)
                    .Replace("{Creater}", Tools.Creater)
                    .Replace("{SequenceName}", "s" + LetterConvert.StrToLower(enTableName).Substring(1))
                    .Replace("{TableName}", enTableName.ToLower())
                    .Replace("{ClassName}", isIncludeAll ? className + " extends BasicInfo" : className) + " {";

                #endregion

                foreach (ColumnEntity columnEntity in tableEntity.ColumnEntityList)
                {
                    #region 参数属性

                    if (!ignoreColuNames.Contains(columnEntity.EnglishNameCol.ToLower()))
                    {
                        if ((columnEntity.ChineseNameCol.Contains("(") || columnEntity.ChineseNameCol.Contains("（"))
                            && (columnEntity.ChineseNameCol.Contains("1") && columnEntity.ChineseNameCol.Contains("2")
                                ||
                                columnEntity.ChineseNameCol.Contains("0") && columnEntity.ChineseNameCol.Contains("1")))
                        {
                            columnEntity.PropertyType = "Integer";
                        }
                        if (columnEntity.EnglishNameCol.ToLower() == "sort_num")
                        {
                            columnEntity.PropertyType = "Integer";
                        }

                        string firstToLower = LetterConvert.ConvertStr(columnEntity.EnglishNameCol.Substring(0, 1).ToLower() + columnEntity.EnglishNameCol.Substring(1));
                        listAttribute.Add(new Tuple<string, string, string>(columnEntity.PropertyType, LetterConvert.ConvertStr(columnEntity.EnglishNameCol), firstToLower));

                        string column = "";
                        if (columnEntity.IsPrimaryKey)
                        {
                            column = pojoTemplate.ColumnKeyParameter.Replace("{ColumnDes}", columnEntity.ChineseNameCol)
                              .Replace("{Field}", columnEntity.EnglishNameCol.ToLower())
                              .Replace("{SequenceName}", "s" + LetterConvert.StrToLower(enTableName).Substring(1))
                              .Replace("{Type}", columnEntity.PropertyType)
                              .Replace("{ColumnName}",firstToLower);
                        }
                        else
                        {
                            column = pojoTemplate.ColumnParameter.Replace("{ColumnDes}", columnEntity.ChineseNameCol)
                              .Replace("{Field}", columnEntity.EnglishNameCol.ToLower())
                              .Replace("{Type}", columnEntity.PropertyType)
                              .Replace("{ColumnName}",firstToLower);
                        }
                        columnBuilder.Append(column);
                    }

                    #endregion

                    #region 外键字段 

                    if (columnEntity.IsForeignKey)
                    {

                        var entityName = LetterConvert.ConvertStr(columnEntity.EnglishNameCol.Replace("_ID", "").Replace("_id", "").Replace("_Id", ""));
                        var tbEntity = Tools.StorageTables.Find(x => x.ChineseName == columnEntity.AssociatedTable);
                        if (tbEntity == null)
                        {
                            continue;
                        }
                        var names = tbEntity.EnglishName.Split('_');
                        if (entityName.ToLower() != names[2].ToLower())
                        {
                            entityName = names[2];
                        }
                        string entityDes = tbEntity.ChineseName.ToLower();

                        var columnName = columnEntity.EnglishNameCol;

                        string firstToLower = entityName.Substring(0, 1).ToLower() + entityName.Substring(1);
                        listForeignKey.Add(new Tuple<string, string, string>(entityName, LetterConvert.ConvertStr(entityName), firstToLower));

                        foreignKey += pojoTemplate.ForeignKeyEntity.Replace("{ForeignKeyDes}", entityDes)
                            .Replace("{ForeignKey}", entityName)
                            .Replace("{Field}", columnName)
                            .Replace("{lowForeignKey}", firstToLower);
                    }

                    #endregion
                }
                if (tableEntity.ColumnEntityList.Any(x => x.IsForeignKey == true))
                {
                    columnBuilder.Append(foreignKey);
                    listAttribute = listAttribute.Concat(listForeignKey).ToList();
                }

                foreach (var tuple in listAttribute)
                {
                    columnBuilder.Append(pojoTemplate.GetAttrubute.Replace("{Field}", tuple.Item1)
                        .Replace("{ColumnName}", tuple.Item2)
                        .Replace("{firstToLower}",tuple.Item3));

                    columnBuilder.Append(pojoTemplate.SetAttrubute.Replace("{Field}", tuple.Item1)
                        .Replace("{ColumnName}", tuple.Item2)
                        .Replace("{firstToLower}",tuple.Item3));
                }

                columnBuilder.Append("\r\n}");
                string entityContent = string.Format(pojoTemplate.CreateEntity, usingStr, classNode, columnBuilder);

                Tools.File2Path(Tools.GeneratePath + "\\Pojo\\" + className + ".java", entityContent, EnumCommon.LogEnum.EntityLog);
            }

        }

    }
}
