///******************************************************************************** 
//** Copyright(c) 2015  All Rights Reserved. 
//** auth： 薛江涛 
//** mail： xjt927@126.com 
//** date： 2015/11/30 21:47:07 
//** desc： 尚未编写描述 
//** Ver :  V1.0.0 
//*********************************************************************************/

//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;

//namespace OracleConsole
//{
//    class Class2
//    {


//        static Regex rxCleanUp = new Regex(@"[^\w\d_]", RegexOptions.Compiled);

//        static string[] cs_keywords = { "abstract", "event", "new", "struct", "as", "explicit", "null",
//     "switch", "base", "extern", "object", "this", "bool", "false", "operator", "throw",
//     "break", "finally", "out", "true", "byte", "fixed", "override", "try", "case", "float",
//     "params", "typeof", "catch", "for", "private", "uint", "char", "foreach", "protected",
//     "ulong", "checked", "goto", "public", "unchecked", "class", "if", "readonly", "unsafe",
//     "const", "implicit", "ref", "ushort", "continue", "in", "return", "using", "decimal",
//     "int", "sbyte", "virtual", "default", "interface", "sealed", "volatile", "delegate",
//     "internal", "short", "void", "do", "is", "sizeof", "while", "double", "lock",
//     "stackalloc", "else", "long", "static", "enum", "namespace", "string" };

//        static Func<string, string> CleanUp = (str) =>
//        {
//            str = rxCleanUp.Replace(str, "_");

//            if (char.IsDigit(str[0]) || cs_keywords.Contains(str))
//                str = "@" + str;

//            return str;
//        };

//        static string CheckNullable(Column col)
//        {
//            string result = "";
//            if (col.IsNullable &&
//                col.PropertyType != "byte[]" &&
//                col.PropertyType != "string" &&
//                col.PropertyType != "Microsoft.SqlServer.Types.SqlGeography" &&
//                col.PropertyType != "Microsoft.SqlServer.Types.SqlGeometry"
//                )
//                result = "?";
//            return result;
//        }

//        string GetConnectionString(ref string connectionStringName, out string providerName)
//        {
//            var _CurrentProject = GetCurrentProject();

//            providerName = null;

//            string result = "";
//            ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
//            configFile.ExeConfigFilename = GetConfigPath();

//            if (string.IsNullOrEmpty(configFile.ExeConfigFilename))
//                throw new ArgumentNullException("The project does not contain App.config or Web.config file.");


//            var config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
//            var connSection = config.ConnectionStrings;

//            //if the connectionString is empty - which is the defauls
//            //look for count-1 - this is the last connection string
//            //and takes into account AppServices and LocalSqlServer
//            if (string.IsNullOrEmpty(connectionStringName))
//            {
//                if (connSection.ConnectionStrings.Count > 1)
//                {
//                    connectionStringName = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].Name;
//                    result = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].ConnectionString;
//                    providerName = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].ProviderName;
//                }
//            }
//            else
//            {
//                try
//                {
//                    result = connSection.ConnectionStrings[connectionStringName].ConnectionString;
//                    providerName = connSection.ConnectionStrings[connectionStringName].ProviderName;
//                }
//                catch
//                {
//                    result = "There is no connection string name called '" + connectionStringName + "'";
//                }
//            }

//            //	if (String.IsNullOrEmpty(providerName))
//            //		providerName="System.Data.SqlClient";

//            return result;
//        }

//        string _connectionString = "";
//        string _providerName = "";

//        void InitConnectionString()
//        {
//            _connectionString = ConnectionStringResult;
//            _providerName = ProviderNameResult;
//            if (String.IsNullOrEmpty(_connectionString) || String.IsNullOrEmpty(_providerName))
//            {
//                _connectionString = GetConnectionString(ref ConnectionStringName, out _providerName);

//                if (_connectionString.Contains("|DataDirectory|"))
//                {
//                    //have to replace it
//                    string dataFilePath = GetDataDirectory();
//                    _connectionString = _connectionString.Replace("|DataDirectory|", dataFilePath);
//                }
//            }
//        }

//        public string ConnectionString
//        {
//            get
//            {
//                InitConnectionString();
//                return _connectionString;
//            }
//        }

//        public string ProviderName
//        {
//            get
//            {
//                InitConnectionString();
//                return _providerName;
//            }
//        }

//        public EnvDTE.Project GetCurrentProject()
//        {

//            IServiceProvider _ServiceProvider = (IServiceProvider)Host;
//            if (_ServiceProvider == null)
//                throw new Exception("Host property returned unexpected value (null)");

//            EnvDTE.DTE dte = (EnvDTE.DTE)_ServiceProvider.GetService(typeof(EnvDTE.DTE));
//            if (dte == null)
//                throw new Exception("Unable to retrieve EnvDTE.DTE");

//            Array activeSolutionProjects = (Array)dte.ActiveSolutionProjects;
//            if (activeSolutionProjects == null)
//                throw new Exception("DTE.ActiveSolutionProjects returned null");

//            EnvDTE.Project dteProject = (EnvDTE.Project)activeSolutionProjects.GetValue(0);
//            if (dteProject == null)
//                throw new Exception("DTE.ActiveSolutionProjects[0] returned null");

//            return dteProject;

//        }

//        private string GetProjectPath()
//        {
//            EnvDTE.Project project = GetCurrentProject();
//            System.IO.FileInfo info = new System.IO.FileInfo(project.FullName);
//            return info.Directory.FullName;
//        }

//        private string GetConfigPath()
//        {
//            EnvDTE.Project project = GetCurrentProject();
//            foreach (EnvDTE.ProjectItem item in project.ProjectItems)
//            {
//                // if it is the app.config file, then open it up
//                if (item.Name.Equals("App.config", StringComparison.InvariantCultureIgnoreCase) || item.Name.Equals("Web.config", StringComparison.InvariantCultureIgnoreCase))
//                    return GetProjectPath() + "\\" + item.Name;
//            }
//            return String.Empty;
//        }

//        public string GetDataDirectory()
//        {
//            EnvDTE.Project project = GetCurrentProject();
//            return System.IO.Path.GetDirectoryName(project.FileName) + "\\App_Data\\";
//        }

//        static string zap_password(string connectionString)
//        {
//            var rx = new Regex("password=.*;", RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase);
//            return rx.Replace(connectionString, "password=**zapped**;");
//        }


//        public string GetColumnDefaultValue(Column col)
//        {
//            string sysType = string.Format("\"{0}\"", col.DefaultValue);
//            switch (col.PropertyType.ToLower())
//            {
//                case "long":
//                case "int":
//                case "double":
//                case "decimal":
//                case "bool":
//                    sysType = col.DefaultValue.ToString().Replace("'", "").Replace("\"", "");
//                    break;
//                case "guid":
//                    sysType = string.Format("\"{0}\"", col.DefaultValue);
//                    break;
//                case "datetime":
//                    {
//                        if (col.DefaultValue.ToLower() == "current_time" || col.DefaultValue.ToLower() == "current_date" || col.DefaultValue.ToLower() == "current_timestamp")
//                            sysType = "SystemMethods.CurrentDateTime";
//                        else
//                            sysType = "\"" + col.DefaultValue + "\"";
//                        break;
//                    }
//            }
//            return sysType;
//        }

//        Tables LoadTables(bool makeSingular)
//        {
//            InitConnectionString();

//            WriteLine("// This file was automatically generated by the PetaPoco T4 Template");
//            WriteLine("// Do not make changes directly to this file - edit the template instead");
//            WriteLine("// ");
//            WriteLine("// The following connection settings were used to generate this file");
//            WriteLine("// ");
//            WriteLine("//     Connection String Name: `{0}`", ConnectionStringName);
//            WriteLine("//     Provider:               `{0}`", ProviderName);
//            WriteLine("//     Connection String:      `{0}`", zap_password(ConnectionString));
//            WriteLine("//     Schema:                 `{0}`", SchemaName);
//            WriteLine("//     Include Views:          `{0}`", IncludeViews);
//            WriteLine("");

//            DbProviderFactory _factory;
//            try
//            {
//                _factory = DbProviderFactories.GetFactory(ProviderName);
//            }
//            catch (Exception x)
//            {
//                var error = x.Message.Replace("\r\n", "\n").Replace("\n", " ");
//                Warning(string.Format("Failed to load provider `{0}` - {1}", ProviderName, error));
//                WriteLine("");
//                WriteLine("// -----------------------------------------------------------------------------------------");
//                WriteLine("// Failed to load provider `{0}` - {1}", ProviderName, error);
//                WriteLine("// -----------------------------------------------------------------------------------------");
//                WriteLine("");
//                return new Tables();
//            }
//            WriteLine("//     Factory Name:          `{0}`", _factory.GetType().Name);

//            try
//            {
//                Tables result;
//                using (var conn = _factory.CreateConnection())
//                {
//                    conn.ConnectionString = ConnectionString;
//                    conn.Open();

//                    SchemaReader reader = null;

//                    if (_factory.GetType().Name == "MySqlClientFactory")
//                    {
//                        // MySql
//                        reader = new MySqlSchemaReader();
//                    }
//                    else if (_factory.GetType().Name == "SqlCeProviderFactory")
//                    {
//                        // SQL CE
//                        reader = new SqlServerCeSchemaReader();
//                    }
//                    else if (_factory.GetType().Name == "NpgsqlFactory")
//                    {
//                        // PostgreSQL
//                        reader = new PostGreSqlSchemaReader();
//                    }
//                    else if (_factory.GetType().Name == "OracleClientFactory")
//                    {
//                        // Oracle
//                        reader = new OracleSchemaReader();
//                    }
//                    else if (_factory.GetType().Name == "SQLiteFactory")
//                    {
//                        // Sqlite
//                        reader = new SqliteSchemaReader();
//                    }
//                    else
//                    {
//                        // Assume SQL Server
//                        reader = new SqlServerSchemaReader();
//                    }

//                    reader.outer = this;
//                    result = reader.ReadSchema(conn, _factory);

//                    // Remove unrequired tables/views
//                    for (int i = result.Count - 1; i >= 0; i--)
//                    {
//                        if (SchemaName != null && string.Compare(result[i].Schema, SchemaName, true) != 0)
//                        {
//                            result.RemoveAt(i);
//                            continue;
//                        }
//                        if ((!IncludeViews && result[i].IsView) || (!IncludeFunctions && result[i].IsFunction))
//                        {
//                            result.RemoveAt(i);
//                            continue;
//                        }
//                    }
//                }

//                var rxClean = new Regex("^(Equals|GetHashCode|GetType|ToString|repo|Save|IsNew|Insert|Update|Delete|Exists|SingleOrDefault|Single|First|FirstOrDefault|Fetch|Page|Query)$");
//                foreach (var t in result)
//                {
//                    if (!makeSingular)
//                    {
//                        t.ClassName = t.CleanName;
//                    }
//                    t.ClassName = ClassPrefix + t.ClassName + ClassSuffix;

//                    foreach (var c in t.Columns)
//                    {
//                        c.PropertyName = rxClean.Replace(c.PropertyName, "_$1");

//                        // Make sure property name doesn't clash with class name
//                        if (c.PropertyName == t.ClassName)
//                            c.PropertyName = "_" + c.PropertyName;
//                    }
//                }

//                return result;

//            }
//            catch (Exception x)
//            {
//                var error = x.Message.Replace("\r\n", "\n").Replace("\n", " ");
//                Warning(string.Format("Failed to read database schema - {0}", error));
//                WriteLine(x.ToString());
//                WriteLine("");
//                WriteLine("// -----------------------------------------------------------------------------------------");
//                WriteLine("// Failed to read database schema - {0}", error);
//                WriteLine("// -----------------------------------------------------------------------------------------");
//                WriteLine("");
//                return new Tables();
//            }


//        }

//        List<SP> SPsNotSupported(string providerName)
//        {
//            Warning("SP function creation is not supported for " + providerName);
//            WriteLine("");
//            WriteLine("// -----------------------------------------------------------------------------------------");
//            WriteLine("// SP function creation is not supported for  `{0}`", providerName);
//            WriteLine("// -----------------------------------------------------------------------------------------");
//            return new List<SP>();
//        }

//        List<SP> LoadSPs()
//        {
//            InitConnectionString();

//            WriteLine("// This file was automatically generated by the PetaPoco T4 Template");
//            WriteLine("// Do not make changes directly to this file - edit the template instead");
//            WriteLine("// ");
//            WriteLine("// The following connection settings were used to generate this file");
//            WriteLine("// ");
//            WriteLine("//     Connection String Name: `{0}`", ConnectionStringName);
//            WriteLine("//     Provider:               `{0}`", ProviderName);
//            WriteLine("//     Connection String:      `{0}`", zap_password(ConnectionString));
//            WriteLine("//     Schema:                 `{0}`", SchemaName);
//            WriteLine("//     Include Views:          `{0}`", IncludeViews);
//            WriteLine("");

//            DbProviderFactory _factory;
//            try
//            {
//                _factory = DbProviderFactories.GetFactory(ProviderName);
//            }
//            catch (Exception x)
//            {
//                var error = x.Message.Replace("\r\n", "\n").Replace("\n", " ");
//                Warning(string.Format("Failed to load provider `{0}` - {1}", ProviderName, error));
//                WriteLine("");
//                WriteLine("// -----------------------------------------------------------------------------------------");
//                WriteLine("// Failed to load provider `{0}` - {1}", ProviderName, error);
//                WriteLine("// -----------------------------------------------------------------------------------------");
//                WriteLine("");
//                return new List<SP>();
//            }
//            WriteLine("//     Factory Name:          `{0}`", _factory.GetType().Name);

//            try
//            {
//                List<SP> result;
//                using (var conn = _factory.CreateConnection())
//                {
//                    conn.ConnectionString = ConnectionString;
//                    conn.Open();

//                    SchemaReader reader = null;

//                    if (_factory.GetType().Name == "MySqlClientFactory")
//                    {
//                        // MySql
//                        reader = new MySqlSchemaReader();
//                        return SPsNotSupported(ProviderName);
//                    }
//                    else if (_factory.GetType().Name == "SqlCeProviderFactory")
//                    {
//                        // SQL CE
//                        reader = new SqlServerCeSchemaReader();
//                        return SPsNotSupported(ProviderName);
//                    }
//                    else if (_factory.GetType().Name == "NpgsqlFactory")
//                    {
//                        // PostgreSQL
//                        reader = new PostGreSqlSchemaReader();
//                        return SPsNotSupported(ProviderName);
//                    }
//                    else if (_factory.GetType().Name == "OracleClientFactory")
//                    {
//                        // Oracle
//                        reader = new OracleSchemaReader();
//                        return SPsNotSupported(ProviderName);
//                    }
//                    else if (_factory.GetType().Name == "SQLiteFactory")
//                    {
//                        // Sqlite
//                        reader = new SqliteSchemaReader();
//                        return SPsNotSupported(ProviderName);
//                    }
//                    else
//                    {
//                        // Assume SQL Server
//                        reader = new SqlServerSchemaReader();
//                    }

//                    reader.outer = this;
//                    result = reader.ReadSPList(conn, _factory);
//                    // Remove unrequired procedures
//                    for (int i = result.Count - 1; i >= 0; i--)
//                    {
//                        if (SchemaName != null && string.Compare(result[i].Schema, SchemaName, true) != 0)
//                        {
//                            result.RemoveAt(i);
//                            continue;
//                        }
//                    }
//                }
//                return result;
//            }
//            catch (Exception x)
//            {
//                var error = x.Message.Replace("\r\n", "\n").Replace("\n", " ");
//                Warning(string.Format("Failed to read database schema - {0}", error));
//                WriteLine("");
//                WriteLine("// -----------------------------------------------------------------------------------------");
//                WriteLine("// Failed to read database schema - {0}", error);
//                WriteLine("// -----------------------------------------------------------------------------------------");
//                WriteLine("");
//                return new List<SP>();
//            }


//        }

//        public bool IsTableNameInList(string tableName, Tables tbls)
//        {
//            if (tbls == null)
//                return false;
//            foreach (var tbItem in tbls)
//            {
//                if (String.Equals(tbItem.Name, tableName, StringComparison.InvariantCultureIgnoreCase))
//                {
//                    return true;
//                }
//            }
//            return false;
//        }

//        public Table GetTableFromListByName(string tableName, Tables tbls)
//        {
//            if (tbls == null)
//                return null;
//            foreach (var tbItem in tbls)
//            {
//                if (String.Equals(tbItem.Name, tableName, StringComparison.InvariantCultureIgnoreCase))
//                {
//                    return tbItem;
//                }
//            }
//            return null;
//        }

//        void SaveOutput(string outputFileName)
//        {
//            string templateDirectory = Path.GetDirectoryName(Host.TemplateFile);
//            string outputFilePath = Path.Combine(templateDirectory, outputFileName);
//            File.WriteAllText(outputFilePath, this.GenerationEnvironment.ToString());

//            this.GenerationEnvironment.Remove(0, this.GenerationEnvironment.Length);
//        }

//        abstract class SchemaReader
//        {
//            public abstract Tables ReadSchema(DbConnection connection, DbProviderFactory factory);
//            public abstract List<SP> ReadSPList(DbConnection connection, DbProviderFactory factory);
//            public GeneratedTextTransformation outer;
//            public void WriteLine(string o)
//            {
//                outer.WriteLine(o);
//            }

//        }

//        static int GetDatatypePrecision(string type)
//        {
//            int startPos = type.IndexOf(",");
//            if (startPos < 0)
//                return -1;
//            int endPos = type.IndexOf(")");
//            if (endPos < 0)
//                return -1;
//            string typePrecisionStr = type.Substring(startPos + 1, endPos - startPos - 1);
//            int result = -1;
//            if (int.TryParse(typePrecisionStr, out result))
//                return result;
//            else
//                return -1;
//        }

//        static int GetDatatypeSize(string type)
//        {
//            int startPos = type.IndexOf("(");
//            if (startPos < 0)
//                return -1;
//            int endPos = type.IndexOf(",");
//            if (endPos < 0)
//            {
//                endPos = type.IndexOf(")");
//            }
//            string typeSizeStr = type.Substring(startPos + 1, endPos - startPos - 1);
//            int result = -1;
//            if (int.TryParse(typeSizeStr, out result))
//                return result;
//            else
//                return -1;
//        }

//        // Edit here to get a method to read the proc
//        class SqlServerSchemaReader : SchemaReader
//        {
//            // SchemaReader.ReadSchema


//            public override Tables ReadSchema(DbConnection connection, DbProviderFactory factory)
//            {
//                var result = new Tables();

//                _connection = connection;
//                _factory = factory;

//                var cmd = _factory.CreateCommand();
//                cmd.Connection = connection;
//                cmd.CommandText = TABLE_SQL;

//                //pull the tables in a reader
//                using (cmd)
//                {

//                    using (var rdr = cmd.ExecuteReader())
//                    {
//                        while (rdr.Read())
//                        {
//                            Table tbl = new Table();
//                            tbl.Name = rdr["TABLE_NAME"].ToString();
//                            tbl.Schema = rdr["TABLE_SCHEMA"].ToString();
//                            tbl.IsView = string.Compare(rdr["TABLE_TYPE"].ToString(), "View", true) == 0;
//                            tbl.IsFunction = string.Compare(rdr["TABLE_TYPE"].ToString(), "TVF", true) == 0;
//                            tbl.CleanName = CleanUp(tbl.Name);
//                            tbl.ClassName = Inflector.MakeSingular(tbl.CleanName);

//                            result.Add(tbl);
//                        }
//                    }
//                }

//                foreach (var tbl in result)
//                {
//                    tbl.Columns = LoadColumns(tbl);

//                    // Mark the primary key
//                    string PrimaryKey = GetPK(tbl.Name);
//                    var pkColumn = tbl.Columns.SingleOrDefault(x => x.Name.ToLower().Trim() == PrimaryKey.ToLower().Trim());
//                    if (pkColumn != null)
//                    {
//                        pkColumn.IsPK = true;
//                    }
//                }


//                return result;
//            }

//            public override List<SP> ReadSPList(DbConnection connection, DbProviderFactory factory)
//            {
//                var result = new List<SP>();

//                _connection = connection;
//                _factory = factory;

//                var cmd = _factory.CreateCommand();
//                cmd.Connection = connection;
//                cmd.CommandText = SP_NAMES_SQL;

//                //pull the tables in a reader
//                using (cmd)
//                {
//                    using (var rdr = cmd.ExecuteReader())
//                    {
//                        while (rdr.Read())
//                        {
//                            SP sp = new SP();
//                            sp.Name = rdr["sp_name"].ToString();
//                            sp.Schema = rdr["schema_name"].ToString();
//                            sp.CleanName = CleanUp(sp.Name);
//                            sp.ClassName = Inflector.MakeSingular(sp.CleanName);
//                            result.Add(sp);
//                        }
//                    }
//                }
//                foreach (var sp in result)
//                {
//                    sp.Parameters = LoadSPParams(sp);
//                }
//                return result;
//            }

//            DbConnection _connection;
//            DbProviderFactory _factory;

//            List<Column> LoadColumns(Table tbl)
//            {

//                using (var cmd = _factory.CreateCommand())
//                {
//                    cmd.Connection = _connection;
//                    cmd.CommandText = COLUMN_SQL;

//                    var p = cmd.CreateParameter();
//                    p.ParameterName = "@tableName";
//                    p.Value = tbl.Name;
//                    cmd.Parameters.Add(p);

//                    p = cmd.CreateParameter();
//                    p.ParameterName = "@schemaName";
//                    p.Value = tbl.Schema;
//                    cmd.Parameters.Add(p);

//                    var result = new List<Column>();
//                    using (IDataReader rdr = cmd.ExecuteReader())
//                    {
//                        while (rdr.Read())
//                        {
//                            Column col = new Column();
//                            col.Name = rdr["ColumnName"].ToString();
//                            col.PropertyName = CleanUp(col.Name);
//                            col.PropertyType = GetPropertyType(rdr["DataType"].ToString());
//                            col.Size = GetDatatypeSize(rdr["DataType"].ToString());
//                            col.Precision = GetDatatypePrecision(rdr["DataType"].ToString());
//                            col.IsNullable = rdr["IsNullable"].ToString() == "YES";
//                            col.IsAutoIncrement = ((int)rdr["IsIdentity"]) == 1;
//                            col.IsComputed = ((int)rdr["IsComputed"]) == 1;
//                            result.Add(col);
//                        }
//                    }

//                    return result;
//                }
//            }

//            List<SPParam> LoadSPParams(SP sp)
//            {
//                using (var cmd = _factory.CreateCommand())
//                {
//                    cmd.Connection = _connection;
//                    cmd.CommandText = SP_PARAMETERS_SQL;

//                    var p = cmd.CreateParameter();
//                    p.ParameterName = "@spname";
//                    p.Value = sp.Name;
//                    cmd.Parameters.Add(p);

//                    var result = new List<SPParam>();
//                    using (IDataReader rdr = cmd.ExecuteReader())
//                    {
//                        while (rdr.Read())
//                        {
//                            if (rdr["IS_RESULT"].ToString().ToUpper() == "YES")
//                                continue;
//                            SPParam param = new SPParam();
//                            param.SysType = GetPropertyType(rdr["DATA_TYPE"].ToString());
//                            param.NullableSysType = GetNullablePropertyType(rdr["DATA_TYPE"].ToString());
//                            param.DbType = GetDbType(rdr["DATA_TYPE"].ToString()).ToString();
//                            param.Name = rdr["PARAMETER_NAME"].ToString().Replace("@", "");
//                            param.CleanName = CleanUp(param.Name);
//                            if (rdr["PARAMETER_MODE"].ToString().ToUpper() == "OUT")
//                                param.Direction = SPParamDir.OutDirection;
//                            else if (rdr["PARAMETER_MODE"].ToString().ToUpper() == "IN")
//                                param.Direction = SPParamDir.InDirection;
//                            else
//                                param.Direction = SPParamDir.InAndOutDirection;
//                            result.Add(param);
//                        }
//                    }
//                    return result;
//                }
//            }


//            string GetPK(string table)
//            {

//                string sql = @"SELECT c.name AS ColumnName
//                FROM sys.indexes AS i
//                INNER JOIN sys.index_columns AS ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
//                INNER JOIN sys.objects AS o ON i.object_id = o.object_id
//                LEFT OUTER JOIN sys.columns AS c ON ic.object_id = c.object_id AND c.column_id = ic.column_id
//                WHERE (i.is_primary_key = 1) AND (o.name = @tableName)";

//                using (var cmd = _factory.CreateCommand())
//                {
//                    cmd.Connection = _connection;
//                    cmd.CommandText = sql;

//                    var p = cmd.CreateParameter();
//                    p.ParameterName = "@tableName";
//                    p.Value = table;
//                    cmd.Parameters.Add(p);

//                    var result = "";
//                    DbDataReader reader = cmd.ExecuteReader();
//                    try
//                    {
//                        if (reader.Read())
//                        {
//                            result = reader[0].ToString();
//                            if (reader.Read())
//                            {
//                                result = "";
//                            }
//                        }
//                    }
//                    finally
//                    {
//                        // Always call Close when done reading.
//                        reader.Close();
//                    }
//                    return result;
//                }
//            }

//            string GetPropertyType(string sqlType)
//            {
//                string propertyType, dbType;
//                GetPropertyAndDbType(sqlType, out propertyType, out dbType);
//                return propertyType;
//            }

//            string GetNullablePropertyType(string sqlType)
//            {
//                string value = GetPropertyType(sqlType);
//                if (value.ToUpper() != "STRING" && value.ToUpper() != "BYTE[]")
//                    return value + "?";
//                else
//                    return value;
//            }

//            string GetDbType(string sqlType)
//            {
//                string propertyType, dbType;
//                GetPropertyAndDbType(sqlType, out propertyType, out dbType);
//                return dbType;
//            }


//            void GetPropertyAndDbType(string sqlType, out string propertyType, out string dbType)
//            {
//                string sysType = "string";
//                string sysDbType = "DbType.String";
//                switch (sqlType)
//                {
//                    case "varchar":
//                        sysType = "string";
//                        sysDbType = "DbType.AnsiString";
//                        break;
//                    case "bigint":
//                        sysType = "long";
//                        sysDbType = "DbType.Int64";
//                        break;
//                    case "smallint":
//                        sysType = "short";
//                        sysDbType = "DbType.Int16";
//                        break;
//                    case "int":
//                        sysType = "int";
//                        sysDbType = "DbType.Int32";
//                        break;
//                    case "uniqueidentifier":
//                        sysType = "Guid";
//                        sysDbType = "DbType.Guid";
//                        break;
//                    case "smalldatetime":
//                    case "datetime":
//                    case "datetime2":
//                    case "date":
//                    case "time":
//                        sysType = "DateTime";
//                        sysDbType = "DbType.DateTime";
//                        break;
//                    case "datetimeoffset":
//                        sysType = "DateTimeOffset";
//                        sysDbType = "DbType.DateTimeOffset";
//                        break;
//                    case "float":
//                        sysType = "double";
//                        sysDbType = "DbType.Double";
//                        break;
//                    case "real":
//                        sysType = "float";
//                        sysDbType = "DbType.Double";
//                        break;
//                    case "numeric":
//                    case "smallmoney":
//                    case "decimal":
//                    case "money":
//                        sysType = "decimal";
//                        sysDbType = "DbType.Decimal";
//                        break;
//                    case "tinyint":
//                        sysType = "byte";
//                        sysDbType = "DbType.Byte";
//                        break;
//                    case "bit":
//                        sysType = "bool";
//                        sysDbType = "DbType.Boolean";
//                        break;
//                    case "image":
//                    case "binary":
//                    case "varbinary":
//                    case "timestamp":
//                        sysType = "byte[]";
//                        sysDbType = "DbType.Binary";
//                        break;
//                    case "geography":
//                        sysType = "Microsoft.SqlServer.Types.SqlGeography";
//                        sysDbType = "DbType.";
//                        break;
//                    case "geometry":
//                        sysType = "Microsoft.SqlServer.Types.SqlGeometry";
//                        sysDbType = "DbType.";
//                        break;
//                }
//                propertyType = sysType;
//                dbType = sysDbType;
//            }

//            string GetDBType(string sqlType)
//            {
//                string sysType = "string";
//                switch (sqlType)
//                {
//                    case "bigint":
//                        sysType = "long";
//                        break;
//                    case "smallint":
//                        sysType = "short";
//                        break;
//                    case "int":
//                        sysType = "int";
//                        break;
//                    case "uniqueidentifier":
//                        sysType = "Guid";
//                        break;
//                    case "smalldatetime":
//                    case "datetime":
//                    case "datetime2":
//                    case "date":
//                    case "time":
//                        sysType = "DateTime";
//                        break;
//                    case "datetimeoffset":
//                        sysType = "DateTimeOffset";
//                        break;
//                    case "float":
//                        sysType = "double";
//                        break;
//                    case "real":
//                        sysType = "float";
//                        break;
//                    case "numeric":
//                    case "smallmoney":
//                    case "decimal":
//                    case "money":
//                        sysType = "decimal";
//                        break;
//                    case "tinyint":
//                        sysType = "byte";
//                        break;
//                    case "bit":
//                        sysType = "bool";
//                        break;
//                    case "image":
//                    case "binary":
//                    case "varbinary":
//                    case "timestamp":
//                        sysType = "byte[]";
//                        break;
//                    case "geography":
//                        sysType = "Microsoft.SqlServer.Types.SqlGeography";
//                        break;
//                    case "geometry":
//                        sysType = "Microsoft.SqlServer.Types.SqlGeometry";
//                        break;
//                }
//                return sysType;
//            }


//            const string TABLE_SQL = @"SELECT * FROM  INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' OR TABLE_TYPE='VIEW'
//								UNION
//							SELECT SPECIFIC_CATALOG, SPECIFIC_SCHEMA, SPECIFIC_NAME, 'TVF' FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCTION' AND DATA_TYPE = 'TABLE'";

//            const string COLUMN_SQL = @"SELECT T.[Database] ,
//									   T.Owner ,
//									   T.TableName ,
//									   T.ColumnName ,
//									   T.OrdinalPosition ,
//									   T.DefaultSetting ,
//									   T.IsNullable ,
//									   T.DataType ,
//									   T.MaxLength ,
//									   T.DatePrecision ,
//									   T.IsIdentity ,
//									   T.IsComputed FROM (
//								SELECT
//											TABLE_CATALOG AS [Database],
//											TABLE_SCHEMA AS Owner,
//											TABLE_NAME AS TableName,
//											COLUMN_NAME AS ColumnName,
//											ORDINAL_POSITION AS OrdinalPosition,
//											COLUMN_DEFAULT AS DefaultSetting,
//											IS_NULLABLE AS IsNullable, DATA_TYPE AS DataType,
//											CHARACTER_MAXIMUM_LENGTH AS MaxLength,
//											DATETIME_PRECISION AS DatePrecision,
//											COLUMNPROPERTY(object_id('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']'), COLUMN_NAME, 'IsIdentity') AS IsIdentity,
//											COLUMNPROPERTY(object_id('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']'), COLUMN_NAME, 'IsComputed') as IsComputed
//										FROM  INFORMATION_SCHEMA.COLUMNS
//										WHERE TABLE_NAME=@tableName AND TABLE_SCHEMA=@schemaName
//										--ORDER BY OrdinalPosition ASC
//								UNION
//								SELECT TABLE_CATALOG AS [Database],
//											TABLE_SCHEMA AS Owner,
//											TABLE_NAME AS TableName,
//											COLUMN_NAME AS ColumnName,
//											ORDINAL_POSITION AS OrdinalPosition,
//											COLUMN_DEFAULT AS DefaultSetting,
//											IS_NULLABLE AS IsNullable, DATA_TYPE AS DataType,
//											CHARACTER_MAXIMUM_LENGTH AS MaxLength,
//											DATETIME_PRECISION AS DatePrecision,
//											COLUMNPROPERTY(object_id('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']'), COLUMN_NAME, 'IsIdentity') AS IsIdentity,
//											COLUMNPROPERTY(object_id('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']'), COLUMN_NAME, 'IsComputed') as IsComputed  
//								FROM INFORMATION_SCHEMA.ROUTINE_COLUMNS
//								WHERE TABLE_NAME=@tableName AND TABLE_SCHEMA=@schemaName
//								) T
//								ORDER BY T.OrdinalPosition ASC";

//            const string SP_NAMES_SQL = @"SELECT  o.name AS sp_name, s.name AS schema_name
//FROM    sys.objects o
//        INNER JOIN sys.schemas s ON o.schema_id = s.schema_id
//WHERE   o.type = 'P'
//        AND o.name NOT IN ( 'fn_diagramobjects', 'sp_alterdiagram',
//                            'sp_creatediagram', 'sp_dropdiagram',
//                            'sp_helpdiagramdefinition', 'sp_helpdiagrams',
//                            'sp_renamediagram', 'sp_upgraddiagrams',
//                            'sysdiagrams' )";


//            const string SP_PARAMETERS_SQL = @"SELECT * from information_schema.PARAMETERS
//                                where SPECIFIC_NAME = @spname
//                                order by ORDINAL_POSITION";

//        }

//    }
//}
