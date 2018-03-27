using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using XJT.Com.EntitySql.Common;
using XJT.Com.EntitySql.ExcelSource;

namespace XJT.Com.EntitySql.DatabaseSource
{
    class DatabaseSourceWork
    {
        public void DoWork(string connectionStr, string providerName, string tableNames)
        {
            GenerateCore generateCore = new GenerateCore();
            //oracle数据库 
            generateCore.ConnectionStringResult = connectionStr;// "User ID=mes_ep_dev;Password=mes_ep_dev;Data Source=ep_new";
            generateCore.ProviderNameResult = GetProviderResult(providerName);
            GenerateCore.TableNames = tableNames;
            GenerateCore.TableNameLike = Tools.TableNameLike;

            // 读取表数据

            Tools.LogAction("读取数据库！", EnumCommon.LogEnum.EntityLog);
            var tables = generateCore.LoadTables(false);
            Tools.LogAction($"读取到{tables.Count}张表！", EnumCommon.LogEnum.EntityLog);
            Tools.LogAction("开始生成Entity实体！", EnumCommon.LogEnum.EntityLog);
            GenerateEntity(tables, generateCore);
            Tools.LogAction("生成Entity实体结束！", EnumCommon.LogEnum.EntityLog);
        }

        private void GenerateEntity(GenerateCore.Tables tables, GenerateCore generateCore)
        {
            string namespaceTemplate = "EP.Entity.{0}Entity";
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            EntityTemplate entityTemplate = new EntityTemplate();
            string ignoreColumnNames = "crt_date,crt_user_id,crt_user_name,mnt_date,mnt_user_id,mnt_user_name"; //忽略的列
            // 生成输出
            if (tables.Count > 0)
            {
                StringBuilder entityContent = new StringBuilder();

                var manager = GenerateCore.Manager.Create(null, entityContent);
                manager.StartHeader();
                manager.EndBlock();
                int num = 1;
                foreach (GenerateCore.Table tbl in from t in tables where !t.Ignore select t)
                {
                    Tools.LogAction($"生成第{num++}张表！", EnumCommon.LogEnum.EntityLog);

                    #region 属性

                    string usingStr = "using NHibernate.Mapping.Attributes;";
                    string foreignKey = "\r\n        #endregion\r\n\r\n        #region 关联实体\r\n";
                    //查询表中外键关联表，用以生成关联实体
                    var consTables = generateCore.LoadConstraints(tbl.Name);
                    StringBuilder columnBuilder = new StringBuilder();
                    string className = tbl.Name;
                    string fileDes = (tbl.Comments == null ? className : tbl.Comments) + "实体";
                    string namespaceStr = string.Format(namespaceTemplate, className.Split('_')[1].ToUpper() ) + "\r\n{";
                    string[] splits = tbl.Name.Split('_');
                    if (splits.Length > 2)
                    {
                        int startIndex = splits[0].Length + splits[1].Length + 2;
                        className = tbl.Name.Substring(startIndex);
                    }
                    bool isIncludeAll = false;
                    var ignoreColuNames = ignoreColumnNames.Split(',');
                    foreach (string coluName in ignoreColuNames)
                    {
                        isIncludeAll = tbl.Columns.Any(column => column.Name.ToLower() == coluName.ToLower());
                        if (!isIncludeAll)
                        {
                            break;
                        }
                    }
                    manager.StartNewFile(LetterConvert.ConvertStr(className) + "Entity.cs");

                    var fileDesLen = Encoding.Default.GetByteCount(fileDes);
                    var createrLen = Encoding.Default.GetByteCount(Tools.Creater);
                    string fileNode = entityTemplate.FileNode.Replace("{Time}", time)
                        .Replace("{Creater}", Tools.Creater + "".PadRight(26 - createrLen))
                        .Replace("{FileDes}", fileDes + "".PadRight(85 - fileDesLen));

                    if (tbl.Columns.Any(x => x.PropertyName == "DateTime"))
                    {
                        usingStr = "using System;\r\n" + usingStr;
                    }

                    if (
                        tbl.Columns.Any(
                            x => x.PropertyName.ToLower() == "company_id" || x.PropertyName.ToLower() == "dept_id"))
                    {
                        usingStr += "\r\nusing EP.Entity.AAAEntity;";
                    }

                    #endregion

                    #region 类文件注释

                    var classNode = entityTemplate.ClassNode.Replace("{FileDes}", fileDes)
                                        .Replace("{ModelNum}", "pcitc_ep_entity_class_" + className)
                                        .Replace("{Time}", time)
                                        .Replace("{Creater}", Tools.Creater)
                                        .Replace("{TableName}", className.ToLower())
                                        .Replace("{ClassName}",
                                            isIncludeAll ? className + " : BasicInfoEntity" : className) +
                                    "\r\n    {\r\n        #region Model";

                    #endregion

                    #region 参数属性

                    bool isIdField = true;
                    foreach (GenerateCore.Column col in tbl.Columns)
                    {

                        if (!col.IsPK)
                        {
                            if ((col.Comments.Contains("(") || col.Comments.Contains("（"))
                                && (col.Comments.Contains("1") && col.Comments.Contains("2")
                                    || col.Comments.Contains("0") && col.Comments.Contains("1")))
                            {
                                col.PropertyType = col.PropertyType.Replace("decimal", "int");
                            }
                            string column = "";
                            if (col.IsPK)
                            {
                                column = entityTemplate.ColumnKeyParameter.Replace("{ColumnDes}", col.Comments)
                                    .Replace("{Field}", col.PropertyName.ToLower())
                                    .Replace("{SequenceName}", "s" + LetterConvert.StrToLower(tbl.Name).Substring(1))
                                    .Replace("{Type}", col.PropertyType)
                                    .Replace("{ColumnName}", LetterConvert.ConvertStr(col.PropertyName));
                            }
                            else if (tbl.IsView && isIdField)
                            {
                                isIdField = false;
                                column = entityTemplate.ColumnViewParameter.Replace("{ColumnDes}", col.Comments)
                                      .Replace("{Field}", col.PropertyName.ToLower())
                                      .Replace("{Type}", col.PropertyType)
                                      .Replace("{ColumnName}", LetterConvert.ConvertStr(col.PropertyName));

                            }
                            else
                            {
                                column = entityTemplate.ColumnParameter.Replace("{ColumnDes}", col.Comments)
                                    .Replace("{Field}", col.PropertyName.ToLower())
                                    .Replace("{Type}", col.PropertyType)
                                    .Replace("{ColumnName}", LetterConvert.ConvertStr(col.PropertyName));
                            }
                            columnBuilder.Append(column);
                        }
                    }

                    #endregion

                    #region 外键字段 

                    if (consTables.Count > 0)
                        consTables.ForEach(x =>
                        {
                            if (consTables.Count > 0)
                            {
                                var columnName = x.OrColumnName.ToLower();
                                var entityName = LetterConvert.ConvertStr(columnName.Replace("_ID", ""));

                                if (columnName == "company_id")
                                {
                                    foreignKey += @"
        /// <summary>
        /// 企业实体
        /// </summary>
        [ManyToOne(Name = ""Company"", ClassType = typeof(OUEntity), Lazy = Laziness.Proxy,
            Column = ""company_id"", Unique = true, Insert = false, Update = false)]
        public virtual OUEntity Company { get; set; }";
                                }
                                else if (columnName == "dept_id")
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
                                    if (entityName.ToLower().Contains("voc"))
                                    {
                                        Regex reg = new Regex("voc",
                                            RegexOptions.IgnoreCase | RegexOptions.IgnoreCase);
                                        entityName = reg.Replace(entityName, "VOC");
                                    }
                                    foreignKey += entityTemplate.ForeignKeyEntity
                                        .Replace("{ForeignKeyDes}", x.TableComments)
                                        .Replace("{ForeignKey}", entityName)
                                        .Replace("{Field}", columnName);
                                }
                            }
                        });
                    #endregion

                    columnBuilder.Append("\r\n		#endregion").Append("\r\n    }\r\n}");
                    string content = string.Format(entityTemplate.CreateEntity, fileNode, usingStr, namespaceStr,
                        classNode, columnBuilder);
                    entityContent.Append(content);
                    manager.EndBlock();
                }
                manager.StartFooter();
                manager.EndBlock();
                manager.Process(true);
            }
        }


        private string GetProviderResult(string providerName)
        {
            switch (providerName)
            {
                case "Oracle":
                    return "System.Data.OracleClient";
                case "SqlServer":
                    return "System.Data.SqlClient";
                case "MySql":
                    return "MySql.Data.MySqlClient";
                case "SQLite":
                    return "System.Data.SQLite";
                default:
                    return "";
            }
        }
    }
}
