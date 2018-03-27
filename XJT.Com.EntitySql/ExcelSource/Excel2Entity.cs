using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using XJT.Com.EntitySql.Common;

namespace XJT.Com.EntitySql.ExcelSource
{
    class Excel2Entity
    {
        public void Excel2EntityDoWork(List<TableEntity> tableEntitys)
        {
            Tools.LogAction("\r\n", EnumCommon.LogEnum.EntityLog);
            Tools.LogAction("开始生成Entity实体！", EnumCommon.LogEnum.EntityLog);
            GenerateEntity(tableEntitys);
            Tools.LogAction("生成Entity实体结束！", EnumCommon.LogEnum.EntityLog);
        }

        /// <summary>
        /// 生成实体
        /// </summary>
        /// <param name="tableEntities"></param> 
        public void GenerateEntity(List<TableEntity> tableEntities)
        {
            string ignoreColumnNames = "crt_date,crt_user_id,crt_user_name,mnt_date,mnt_user_id,mnt_user_name";	//忽略的列
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string namespaceTemplate = "EP.Entity.{0}Entity";
            string className = "";
            EntityTemplate entityTemplate = new EntityTemplate();

            foreach (var tableEntity in tableEntities)
            {
                string usingStr = "using NHibernate.Mapping.Attributes;";
                var enTableName = tableEntity.EnglishName;
                var cnTableName = tableEntity.ChineseName;
                string[] splits = enTableName.Split('_');
                if (splits.Length > 2)
                {
                    int startIndex = splits[0].Length + splits[1].Length + 2;
                    className = enTableName.Substring(startIndex) + "Entity";
                }
                string namespaceStr = string.Format(namespaceTemplate, tableEntity.EnglishName.Split('_')[1].ToUpper()) + "\r\n{";
                StringBuilder columnBuilder = new StringBuilder();
                string fileDes = cnTableName + "实体";
                var fileDesLen = Encoding.Default.GetByteCount(fileDes);
                var createrLen = Encoding.Default.GetByteCount(Tools.Creater);
                string fileNode = entityTemplate.FileNode.Replace("{Time}", time)
                    .Replace("{Creater}", Tools.Creater + "".PadRight(26 - createrLen))
                    .Replace("{FileDes}", fileDes + "".PadRight(85 - fileDesLen));
                var foreignKey = "\r\n        #endregion\r\n\r\n        #region 关联实体\r\n";

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

                if (tableEntity.ColumnEntityList.Any(x => x.PropertyType == "DateTime"))
                {
                    usingStr = "using System;\r\n" + usingStr;
                }
                if (tableEntity.ColumnEntityList.Any(x => x.EnglishNameCol.ToLower() == "company_id" || x.EnglishNameCol.ToLower() == "dept_id"))
                {
                    usingStr += "\r\nusing EP.Entity.AAAEntity;";
                }

                #region 类文件注释

                var classNode = entityTemplate.ClassNode.Replace("{FileDes}", fileDes)
                    .Replace("{ModelNum}", "pcitc_ep_entity_class_" + className)
                    .Replace("{Time}", time)
                    .Replace("{Creater}", Tools.Creater)
                    .Replace("{TableName}", enTableName.ToLower())
                    .Replace("{ClassName}", isIncludeAll ? className + " : BasicInfoEntity" : className) + "\r\n    {\r\n        #region Model";

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
                            columnEntity.PropertyType = "int";
                        }
                        if (columnEntity.EnglishNameCol.ToLower() == "sort_num")
                        {
                            columnEntity.PropertyType = "int";
                        }
                        string column = "";
                        if (columnEntity.IsPrimaryKey)
                        {
                            column = entityTemplate.ColumnKeyParameter.Replace("{ColumnDes}", columnEntity.ChineseNameCol)
                              .Replace("{Field}", columnEntity.EnglishNameCol.ToLower())
                              .Replace("{SequenceName}", "s" + LetterConvert.StrToLower(enTableName).Substring(1))
                              .Replace("{Type}", columnEntity.PropertyType)
                              .Replace("{ColumnName}", LetterConvert.ConvertStr(columnEntity.EnglishNameCol));
                        }
                        else
                        {
                            column = entityTemplate.ColumnParameter.Replace("{ColumnDes}", columnEntity.ChineseNameCol)
                              .Replace("{Field}", columnEntity.EnglishNameCol.ToLower())
                              .Replace("{Type}", columnEntity.PropertyType)
                              .Replace("{ColumnName}", LetterConvert.ConvertStr(columnEntity.EnglishNameCol));
                        }
                        columnBuilder.Append(column);
                    }

                    #endregion

                    #region 外键字段 

                    if (columnEntity.IsForeignKey)
                    {
                        if (columnEntity.EnglishNameCol.ToLower() == "company_id")
                        {
                            foreignKey += @"
        /// <summary>
        /// 企业实体
        /// </summary>
        [ManyToOne(Name = ""Company"", ClassType = typeof(OUEntity), Lazy = Laziness.Proxy,
            Column = ""company_id"", Unique = true, Insert = false, Update = false)]
        public virtual OUEntity Company { get; set; }
";
                        }
                        else if (columnEntity.EnglishNameCol.ToLower() == "dept_id")
                        {
                            foreignKey += @"

        /// <summary>
        /// 部门实体
        /// </summary>
        [ManyToOne(Name = ""Dept"", ClassType = typeof(OUEntity), Lazy = Laziness.Proxy, Column = ""dept_id"",
            Unique = true, Insert = false, Update = false)]
        public virtual OUEntity Dept { get; set; }";
                        }
                        else
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
                            string entityDes = tbEntity.ChineseName;

                            if (entityName.ToLower().Contains("voc"))
                            {
                                Regex reg = new Regex("voc", RegexOptions.IgnoreCase);
                                entityName = reg.Replace(entityName, "VOC");
                            }
                            var columnName = columnEntity.EnglishNameCol.ToLower();

                            foreignKey += entityTemplate.ForeignKeyEntity.Replace("{ForeignKeyDes}", entityDes)
                                .Replace("{ForeignKey}", entityName)
                                .Replace("{Field}", columnName);
                        }
                    }

                    #endregion
                }
                if (tableEntity.ColumnEntityList.Any(x => x.IsForeignKey == true))
                {
                    columnBuilder.Append(foreignKey);
                }
                columnBuilder.Append("\r\n		#endregion").Append("\r\n    }\r\n}");
                string entityContent = string.Format(entityTemplate.CreateEntity, fileNode, usingStr, namespaceStr, classNode, columnBuilder);

                Tools.File2Path(Tools.GeneratePath + "\\Entity\\" + className + ".cs", entityContent, EnumCommon.LogEnum.EntityLog);
            }

        }

    }
}
