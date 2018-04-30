///******************************************************************************** 
//** Copyright(c) 2015  All Rights Reserved. 
//** auth： 薛江涛 
//** mail： xjt927@126.com 
//** date： 2015/11/30 21:42:29 
//** desc： 尚未编写描述 
//** Ver :  V1.0.0 
//*********************************************************************************/

//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Data.Common;
//using System.Diagnostics;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Configuration;
////using System.Windows.Forms;
////using Microsoft.VisualStudio.TextTemplating;


//public class Table
//{
    
//string ConnectionStringName = "";
//string ConnectionStringResult="";
//string ProviderNameResult="";
//string Priv="";
//string Namespace = "";
//string ClassPrefix = "";
//string ClassSuffix = "";
//string SchemaName = null;
//bool IncludeViews = false;
//bool IncludeFunctions = false;


//    public List<Column> Columns;
//    public List<TableIndex> Indices;
//    public List<FKey> FKeys;
//    public string Name;
//    public string Schema;
//    public bool IsView;
//    public bool IsFunction;
//    public string CleanName;
//    public string ClassName;
//    public string SequenceName;
//    public bool Ignore;
//    public string SQL;

//    public Column PK
//    {
//        get
//        {
//            return this.Columns.SingleOrDefault(x=>x.IsPK);
//        }
//    }

//    public Column GetColumn(string columnName)
//    {
//        return Columns.Single(x=>string.Compare(x.Name, columnName, true)==0);
//    }

//    public Column this[string columnName]
//    {
//        get
//        {
//            return GetColumn(columnName);
//        }
//    }

//    public bool HasPK()
//    {
//        return ((PK != null) && (string.IsNullOrEmpty(PK.Name) != true));
//    }
//    public TableIndex GetIndex(string indexName)
//    {
//        return Indices.Single(x=>string.Compare(x.Name, indexName, true)==0);
//    }
//}

//public class Column
//{
//    public string Name;
//    public string PropertyName;
//    public string PropertyType;
//    public bool IsPK;
//    public bool IsNullable;
//    public bool IsAutoIncrement;
//    public bool IsComputed;
//    public bool Ignore;
//    public int Size;
//    public int Precision;
//    public string DefaultValue;
//    public string ProperPropertyType
//    {
//        get
//        {
//          if(IsNullable)
//          {
//            return PropertyType + CheckNullable(this);
//          }
//          return PropertyType;
//        }
//    }
//}

//public class Tables : List<Table>
//{
//    public Tables()
//    {
//    }

//    public Table GetTable(string tableName)
//    {
//        return this.Single(x=>string.Compare(x.Name, tableName, true)==0);
//    }

//    public Table this[string tableName]
//    {
//        get
//        {
//            return GetTable(tableName);
//        }
//    }

//}

//public class IndexColumn
//{
//    public string Name;
//    public bool IsAsc;
//}

//public class TableIndex
//{
//    public string Name;
//    public List<IndexColumn> IndexColumns;
//    public bool IsUnique;
//    public string SQL;
//}

//public class FKey
//{
//    public string ToTable;
//    public string FromColumn;
//    public string ToColumn;
//}

//public class SP
//{
//    public string Name;
//    public string CleanName;
//    public string ClassName;
//    public string Schema;
//    public string SchemaQualifiedName {get{return Schema+"."+Name;}}
//    public List<SPParam> Parameters;
//    public SP()
//    {
//        Parameters=new List<SPParam>();
//    }
//    public string ArgList
//    {
//        get
//        {
//            StringBuilder sb=new StringBuilder();
//            int loopCount=1;
//            foreach(var par in Parameters)
//            {
//                sb.AppendFormat("{0} {1}", par.SysType,par.CleanName);
//                if(loopCount<Parameters.Count)
//                    sb.Append(",");
//                loopCount++;
//            }
//            return sb.ToString();
//        }
//    }
//}

//public enum SPParamDir
//{
//  OutDirection,
//  InDirection,
//  InAndOutDirection
//}

//public class SPParam
//{
//    public string Name;
//    public string CleanName;
//    public string SysType;
//    public string NullableSysType;
//    public string DbType;
//    public SPParamDir Direction;
//}

//class SqlServerCeSchemaReader : SchemaReader
//{
//    // SchemaReader.ReadSchema
//    public override Tables ReadSchema(DbConnection connection, DbProviderFactory factory)
//    {
//        var result=new Tables();

//        _connection=connection;
//        _factory=factory;

//        var cmd=_factory.CreateCommand();
//        cmd.Connection=connection;
//        cmd.CommandText=TABLE_SQL;

//        //pull the tables in a reader
//        using(cmd)
//        {
//            using (var rdr=cmd.ExecuteReader())
//            {
//                while(rdr.Read())
//                {
//                    Table tbl=new Table();
//                    tbl.Name=rdr["TABLE_NAME"].ToString();
//                    tbl.CleanName=CleanUp(tbl.Name);
//                    tbl.ClassName=Inflector.MakeSingular(tbl.CleanName);
//                    tbl.Schema=null;
//                    tbl.IsView=false;
//                    result.Add(tbl);
//                }
//            }
//        }

//        foreach (var tbl in result)
//        {
//            tbl.Columns=LoadColumns(tbl);

//            // Mark the primary key
//            string PrimaryKey=GetPK(tbl.Name);
//            var pkColumn=tbl.Columns.SingleOrDefault(x=>x.Name.ToLower().Trim()==PrimaryKey.ToLower().Trim());
//            if(pkColumn!=null)
//                pkColumn.IsPK=true;
//        }


//        return result;
//    }

//    public override List<SP> ReadSPList(DbConnection connection, DbProviderFactory factory)
//    {
//        return new List<SP>();
//    }

//    DbConnection _connection;
//    DbProviderFactory _factory;


//    List<Column> LoadColumns(Table tbl)
//    {

//        using (var cmd=_factory.CreateCommand())
//        {
//            cmd.Connection=_connection;
//            cmd.CommandText=COLUMN_SQL;

//            var p = cmd.CreateParameter();
//            p.ParameterName = "@tableName";
//            p.Value=tbl.Name;
//            cmd.Parameters.Add(p);

//            var result=new List<Column>();
//            using (IDataReader rdr=cmd.ExecuteReader())
//            {
//                while(rdr.Read())
//                {
//                    Column col=new Column();
//                    col.Name=rdr["ColumnName"].ToString();
//                    col.PropertyName=CleanUp(col.Name);
//                    col.PropertyType=GetPropertyType(rdr["DataType"].ToString());
//                    col.Size=GetDatatypeSize(rdr["DataType"].ToString());
//                    col.Precision=GetDatatypePrecision(rdr["DataType"].ToString());
//                    col.IsNullable=rdr["IsNullable"].ToString()=="YES";
//                    col.IsAutoIncrement=rdr["AUTOINC_INCREMENT"]!=DBNull.Value;
//                    result.Add(col);
//                }
//            }

//            return result;
//        }
//    }

//    string GetPK(string table){

//        string sql=@"SELECT KCU.COLUMN_NAME
//			FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU
//			JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
//			ON KCU.CONSTRAINT_NAME=TC.CONSTRAINT_NAME
//			WHERE TC.CONSTRAINT_TYPE='PRIMARY KEY'
//			AND KCU.TABLE_NAME=@tableName";

//        using (var cmd=_factory.CreateCommand())
//        {
//            cmd.Connection=_connection;
//            cmd.CommandText=sql;

//            var p = cmd.CreateParameter();
//            p.ParameterName = "@tableName";
//            p.Value=table;
//            cmd.Parameters.Add(p);

//            var result = "";
//            DbDataReader reader = cmd.ExecuteReader();
//            try
//            {
//                if (reader.Read())
//                {
//                    result = reader[0].ToString();
//                    if (reader.Read())
//                    {
//                        result = "";
//                    }
//                }
//            }
//            finally
//            {
//                // Always call Close when done reading.
//                reader.Close();
//            }
//            return result;
//        }
//    }

//    string GetPropertyType(string sqlType)
//    {
//        string sysType="string";
//        switch (sqlType)
//        {
//            case "bigint":
//                sysType = "long";
//                break;
//            case "smallint":
//                sysType= "short";
//                break;
//            case "int":
//                sysType= "int";
//                break;
//            case "uniqueidentifier":
//                sysType=  "Guid";
//                 break;
//            case "smalldatetime":
//            case "datetime":
//            case "date":
//            case "time":
//                sysType=  "DateTime";
//                  break;
//            case "float":
//                sysType="double";
//                break;
//            case "real":
//                sysType="float";
//                break;
//            case "numeric":
//            case "smallmoney":
//            case "decimal":
//            case "money":
//                sysType=  "decimal";
//                 break;
//            case "tinyint":
//                sysType = "byte";
//                break;
//            case "bit":
//                sysType=  "bool";
//                   break;
//            case "image":
//            case "binary":
//            case "varbinary":
//            case "timestamp":
//                sysType=  "byte[]";
//                 break;
//        }
//        return sysType;
//    }



//    const string TABLE_SQL=@"SELECT *
//		FROM  INFORMATION_SCHEMA.TABLES
//		WHERE TABLE_TYPE='TABLE'";

//    const string COLUMN_SQL=@"SELECT
//			TABLE_CATALOG AS [Database],
//			TABLE_SCHEMA AS Owner,
//			TABLE_NAME AS TableName,
//			COLUMN_NAME AS ColumnName,
//			ORDINAL_POSITION AS OrdinalPosition,
//			COLUMN_DEFAULT AS DefaultSetting,
//			IS_NULLABLE AS IsNullable, DATA_TYPE AS DataType,
//			AUTOINC_INCREMENT,
//			CHARACTER_MAXIMUM_LENGTH AS MaxLength,
//			DATETIME_PRECISION AS DatePrecision
//		FROM  INFORMATION_SCHEMA.COLUMNS
//		WHERE TABLE_NAME=@tableName
//		ORDER BY OrdinalPosition ASC";

//}


//class PostGreSqlSchemaReader : SchemaReader
//{
//    // SchemaReader.ReadSchema
//    public override Tables ReadSchema(DbConnection connection, DbProviderFactory factory)
//    {
//        var result=new Tables();

//        _connection=connection;
//        _factory=factory;

//        var cmd=_factory.CreateCommand();
//        cmd.Connection=connection;
//        cmd.CommandText=TABLE_SQL;

//        //pull the tables in a reader
//        using(cmd)
//        {
//            using (var rdr=cmd.ExecuteReader())
//            {
//                while(rdr.Read())
//                {
//                    Table tbl=new Table();
//                    tbl.Name=rdr["table_name"].ToString();
//                    tbl.Schema=rdr["table_schema"].ToString();
//                    tbl.IsView=string.Compare(rdr["table_type"].ToString(), "View", true)==0;
//                    tbl.CleanName=CleanUp(tbl.Name);
//                    tbl.ClassName=Inflector.MakeSingular(tbl.CleanName);
//                    result.Add(tbl);
//                }
//            }
//        }

//        foreach (var tbl in result)
//        {
//            tbl.Columns=LoadColumns(tbl);

//            // Mark the primary key
//            string PrimaryKey=GetPK(tbl.Name);
//            var pkColumn=tbl.Columns.SingleOrDefault(x=>x.Name.ToLower().Trim()==PrimaryKey.ToLower().Trim());
//            if(pkColumn!=null)
//                pkColumn.IsPK=true;
//        }


//        return result;
//    }

//    public override List<SP> ReadSPList(DbConnection connection, DbProviderFactory factory)
//    {
//        return new List<SP>();
//    }

//    DbConnection _connection;
//    DbProviderFactory _factory;


//    List<Column> LoadColumns(Table tbl)
//    {

//        using (var cmd=_factory.CreateCommand())
//        {
//            cmd.Connection=_connection;
//            cmd.CommandText=COLUMN_SQL;

//            var p = cmd.CreateParameter();
//            p.ParameterName = "@tableName";
//            p.Value=tbl.Name;
//            cmd.Parameters.Add(p);

//            var result=new List<Column>();
//            using (IDataReader rdr=cmd.ExecuteReader())
//            {
//                while(rdr.Read())
//                {
//                    Column col=new Column();
//                    col.Name=rdr["column_name"].ToString();
//                    col.PropertyName=CleanUp(col.Name);
//                    col.PropertyType=GetPropertyType(rdr["udt_name"].ToString());
//                    col.Size=GetDatatypeSize(rdr["udt_name"].ToString());
//                    col.Precision=GetDatatypePrecision(rdr["udt_name"].ToString());
//                    col.IsNullable=rdr["is_nullable"].ToString()=="YES";
//                    col.IsAutoIncrement = rdr["column_default"].ToString().StartsWith("nextval(");
//                    result.Add(col);
//                }
//            }

//            return result;
//        }
//    }

//    string GetPK(string table){

//        string sql=@"SELECT kcu.column_name
//			FROM information_schema.key_column_usage kcu
//			JOIN information_schema.table_constraints tc
//			ON kcu.constraint_name=tc.constraint_name
//			WHERE lower(tc.constraint_type)='primary key'
//			AND kcu.table_name=@tablename";

//        using (var cmd=_factory.CreateCommand())
//        {
//            cmd.Connection=_connection;
//            cmd.CommandText=sql;

//            var p = cmd.CreateParameter();
//            p.ParameterName = "@tableName";
//            p.Value=table;
//            cmd.Parameters.Add(p);

//            var result = "";
//            DbDataReader reader = cmd.ExecuteReader();
//            try
//            {
//                if (reader.Read())
//                {
//                    result = reader[0].ToString();
//                    if (reader.Read())
//                    {
//                        result = "";
//                    }
//                }
//            }
//            finally
//            {
//                // Always call Close when done reading.
//                reader.Close();
//            }
//            return result;
//        }
//    }

//    string GetPropertyType(string sqlType)
//    {
//        switch (sqlType)
//        {
//            case "int8":
//            case "serial8":
//                return "long";

//            case "bool":
//                return "bool";

//            case "bytea	":
//                return "byte[]";

//            case "float8":
//                return "double";

//            case "int4":
//            case "serial4":
//                return "int";

//            case "money	":
//                return "decimal";

//            case "numeric":
//                return "decimal";

//            case "float4":
//                return "float";

//            case "int2":
//                return "short";

//            case "time":
//            case "timetz":
//            case "timestamp":
//            case "timestamptz":
//            case "date":
//                return "DateTime";

//            default:
//                return "string";
//        }
//    }



//    const string TABLE_SQL=@"
//			SELECT table_name, table_schema, table_type
//			FROM information_schema.tables
//			WHERE (table_type='BASE TABLE' OR table_type='VIEW')
//				AND table_schema NOT IN ('pg_catalog', 'information_schema');
//			";

//    const string COLUMN_SQL=@"
//			SELECT column_name, is_nullable, udt_name, column_default
//			FROM information_schema.columns
//			WHERE table_name=@tableName;
//			";

//}

//class MySqlSchemaReader : SchemaReader
//{
//    // SchemaReader.ReadSchema
//    public override Tables ReadSchema(DbConnection connection, DbProviderFactory factory)
//    {
//        var result=new Tables();

//        try{
//        var cmd=factory.CreateCommand();
//        cmd.Connection=connection;
//        cmd.CommandText=TABLE_SQL;

//        //pull the tables in a reader
//        using(cmd)
//        {
//            using (var rdr=cmd.ExecuteReader())
//            {
//                while(rdr.Read())
//                {
//                    Table tbl=new Table();
//                    tbl.Name=rdr["TABLE_NAME"].ToString();
//                    tbl.Schema=rdr["TABLE_SCHEMA"].ToString();
//                    tbl.IsView=string.Compare(rdr["TABLE_TYPE"].ToString(), "View", true)==0;
//                    tbl.CleanName=CleanUp(tbl.Name);
//                    tbl.ClassName=Inflector.MakeSingular(tbl.CleanName);
//                    result.Add(tbl);
//                }
//            }
//        }


//        //this will return everything for the DB
//        var schema  = connection.GetSchema("COLUMNS");

//        //loop again - but this time pull by table name
//        foreach (var item in result)
//        {
//            item.Columns=new List<Column>();

//            //pull the columns from the schema
//            var columns = schema.Select("TABLE_NAME='" + item.Name + "'");
//            foreach (var row in columns)
//            {
//                Column col=new Column();
//                col.Name=row["COLUMN_NAME"].ToString();
//                col.PropertyName=CleanUp(col.Name);
//                col.PropertyType=GetPropertyType(row);
//                col.Size=GetDatatypeSize(row["DATA_TYPE"].ToString());
//                col.Precision=GetDatatypePrecision(row["DATA_TYPE"].ToString());
//                col.IsNullable=row["IS_NULLABLE"].ToString()=="YES";
//                col.IsPK=row["COLUMN_KEY"].ToString()=="PRI";
//                col.IsAutoIncrement=row["extra"].ToString().ToLower().IndexOf("auto_increment")>=0;

//                item.Columns.Add(col);
//            }
//        }
//        }
//        catch(Exception ex)
//        {
//        WriteLine(ex.ToString());
//        return result;
//        }

//        return result;

//    }

//    public override List<SP> ReadSPList(DbConnection connection, DbProviderFactory factory)
//    {
//        return new List<SP>();
//    }

//    static string GetPropertyType(DataRow row)
//    {
//        bool bUnsigned = row["COLUMN_TYPE"].ToString().IndexOf("unsigned")>=0;
//        string propType="string";
//        switch (row["DATA_TYPE"].ToString())
//        {
//            case "bigint":
//                propType= bUnsigned ? "ulong" : "long";
//                break;
//            case "int":
//                propType= bUnsigned ? "uint" : "int";
//                break;
//            case "smallint":
//                propType= bUnsigned ? "ushort" : "short";
//                break;
//            case "guid":
//                propType=  "Guid";
//                 break;
//            case "smalldatetime":
//            case "date":
//            case "datetime":
//            case "timestamp":
//                propType=  "DateTime";
//                  break;
//            case "float":
//                propType="float";
//                break;
//            case "double":
//                propType="double";
//                break;
//            case "numeric":
//            case "smallmoney":
//            case "decimal":
//            case "money":
//                propType=  "decimal";
//                 break;
//            case "bit":
//            case "bool":
//            case "boolean":
//                propType=  "bool";
//                break;
//            case "tinyint":
//                propType =  bUnsigned ? "byte" : "sbyte";
//                break;
//            case "image":
//            case "binary":
//            case "blob":
//            case "mediumblob":
//            case "longblob":
//            case "varbinary":
//                propType=  "byte[]";
//                 break;

//        }
//        return propType;
//    }

//    const string TABLE_SQL=@"
//			SELECT *
//			FROM information_schema.tables
//			WHERE (table_type='BASE TABLE' OR table_type='VIEW')
//			";

//}

//class OracleSchemaReader : SchemaReader
//{
//    // SchemaReader.ReadSchema
//    public override Tables ReadSchema(DbConnection connection, DbProviderFactory factory)
//    {
//        var result=new Tables();
//    try
//    {
//        _connection=connection;
//        _factory=factory;
		 
//        var cmd=_factory.CreateCommand();
//        cmd.Connection=connection;
//        cmd.CommandText=TABLE_SQL;
//        cmd.GetType().GetProperty("BindByName").SetValue(cmd, true, null);
		
		
		
		
//        //pull the tables in a reader
//        using(cmd)
//        {
		
//            using (var rdr=cmd.ExecuteReader())
//            {
//                while(rdr.Read())
//                {
//                    Table tbl=new Table();
//                    tbl.Name=rdr["TABLE_NAME"].ToString();
//                    tbl.Schema = rdr["TABLE_SCHEMA"].ToString();
//                    tbl.IsView=string.Compare(rdr["TABLE_TYPE"].ToString(), "View", true)==0;
//                    tbl.CleanName=CleanUp(tbl.Name);
//                    tbl.ClassName=Inflector.MakeSingular(tbl.CleanName);
//                    result.Add(tbl);
//                }
//            }
//        }

//        foreach (var tbl in result)
//        {
//            tbl.Columns=LoadColumns(tbl);

//            // Mark the primary key
//            string PrimaryKey=GetPK(tbl.Name);
//            var pkColumn=tbl.Columns.SingleOrDefault(x=>x.Name.ToLower().Trim()==PrimaryKey.ToLower().Trim());
//            if(pkColumn!=null)
//                pkColumn.IsPK=true;
//        }
//        }
//        catch(Exception ex)
//        {
//        WriteLine(ex.ToString());
//        return result;
//        }

//        return result;
//    }

//    public override List<SP> ReadSPList(DbConnection connection, DbProviderFactory factory)
//    {
//        return new List<SP>();
//    }

//    DbConnection _connection;
//    DbProviderFactory _factory;


//    List<Column> LoadColumns(Table tbl)
//    {

//        using (var cmd=_factory.CreateCommand())
//        {
//            cmd.Connection=_connection;
//            cmd.CommandText=COLUMN_SQL;
//            cmd.GetType().GetProperty("BindByName").SetValue(cmd, true, null);

//            var p = cmd.CreateParameter();
//            p.ParameterName = ":tableName";
//            p.Value=tbl.Name;
//            cmd.Parameters.Add(p);

//            var result=new List<Column>();
//            using (IDataReader rdr=cmd.ExecuteReader())
//            {
//                while(rdr.Read())
//                {
//                    Column col=new Column();
//                    col.Name=rdr["ColumnName"].ToString();
//                    col.PropertyName=CleanUp(col.Name);
//                    col.PropertyType=GetPropertyType(rdr["DataType"].ToString(), (rdr["DataType"] == DBNull.Value ? null : rdr["DataType"].ToString()));
//                    col.Size=GetDatatypeSize(rdr["DataType"].ToString());
//                    col.Precision=GetDatatypePrecision(rdr["DataType"].ToString());
//                    col.IsNullable=rdr["IsNullable"].ToString()=="YES";
//                    col.IsAutoIncrement=false;
//                    result.Add(col);
//                }
//            }

//            return result;
//        }
//    }

//    string GetPK(string table){

//        string sql=@"select column_name from USER_CONSTRAINTS uc
//  inner join USER_CONS_COLUMNS ucc on uc.constraint_name = ucc.constraint_name
//where uc.constraint_type = 'P'
//and uc.table_name = upper(:tableName)
//and ucc.position = 1";

//        using (var cmd=_factory.CreateCommand())
//        {
//            cmd.Connection=_connection;
//            cmd.CommandText=sql;
//            cmd.GetType().GetProperty("BindByName").SetValue(cmd, true, null);

//            var p = cmd.CreateParameter();
//            p.ParameterName = ":tableName";
//            p.Value=table;
//            cmd.Parameters.Add(p);

//            var result = "";
//            DbDataReader reader = cmd.ExecuteReader();
//            try
//            {
//                if (reader.Read())
//                {
//                    result = reader[0].ToString();
//                    if (reader.Read())
//                    {
//                        result = "";
//                    }
//                }
//            }
//            finally
//            {
//                // Always call Close when done reading.
//                reader.Close();
//            }
//            return result;
//        }
//    }

//    string GetPropertyType(string sqlType, string dataScale)
//    {
//        string sysType="string";
//        switch (sqlType.ToLower())
//        {
//            case "bigint":
//                sysType = "long";
//                break;
//            case "smallint":
//                sysType= "short";
//                break;
//            case "int":
//                sysType= "int";
//                break;
//            case "uniqueidentifier":
//                sysType=  "Guid";
//                 break;
//            case "smalldatetime":
//            case "datetime":
//            case "date":
//                sysType=  "DateTime";
//                  break;
//            case "float":
//                sysType="double";
//                break;
//            case "real":
//            case "numeric":
//            case "smallmoney":
//            case "decimal":
//            case "money":
//            case "number":
//                sysType=  "decimal";
//                 break;
//            case "tinyint":
//                sysType = "byte";
//                break;
//            case "bit":
//                sysType=  "bool";
//                   break;
//            case "image":
//            case "binary":
//            case "varbinary":
//            case "timestamp":
//                sysType=  "byte[]";
//                 break;
//        }

//        if (sqlType == "number" && dataScale == "0")
//            return "long";

//        return sysType;
//    }



//    const string TABLE_SQL=@"select TABLE_NAME, 'Table' TABLE_TYPE, USER TABLE_SCHEMA
//from USER_TABLES
//union all
//select VIEW_NAME, 'View', USER
//from USER_VIEWS";


//    const string COLUMN_SQL=@"select table_name TableName,
// column_name ColumnName,
// data_type DataType,
// data_scale DataScale,
// nullable IsNullable
// from USER_TAB_COLS utc
// where table_name = :tableName
// order by column_id";

//}


//class SqliteSchemaReader : SchemaReader
//{
//    // SchemaReader.ReadSchema
//    public override Tables ReadSchema(DbConnection connection, DbProviderFactory factory)
//    {
//        var result=new Tables();
//        _connection=connection;
//        _factory=factory;
//        var cmd=_factory.CreateCommand();
//        cmd.Connection=connection;
//        cmd.CommandText=TABLE_SQL;
//        //cmd.GetType().GetProperty("BindByName").SetValue(cmd, true, null);
//        //pull the tables in a reader
//        using(cmd)
//        {
//            using (var rdr=cmd.ExecuteReader())
//            {
//                while(rdr.Read())
//                {
//                    Table tbl=new Table();
//                    tbl.Name=rdr["name"].ToString();
//                    tbl.Schema = "";
//                    tbl.IsView=string.Compare(rdr["type"].ToString(), "view", true)==0;
//                    tbl.CleanName=CleanUp(tbl.Name);
//                    tbl.ClassName=Inflector.MakeSingular(tbl.CleanName);
//                    tbl.SQL = rdr["sql"].ToString();
//                    result.Add(tbl);
//                }
//            }
//        }
//        foreach (var tbl in result)
//        {
//            tbl.Columns=LoadColumns(tbl);
//            tbl.Indices = LoadIndices(tbl.Name);
//            tbl.FKeys = LoadFKeys(tbl.Name);
//        }
//        return result;
//    }

//    public override List<SP> ReadSPList(DbConnection connection, DbProviderFactory factory)
//    {
//        return new List<SP>();
//    }

//    DbConnection _connection;
//    DbProviderFactory _factory;

//    List<Column> LoadColumns(Table tbl)
//    {
//        using (var cmd=_factory.CreateCommand())
//        {
//            cmd.Connection=_connection;
//            cmd.CommandText=string.Format(COLUMN_SQL,tbl.Name);

//            var result=new List<Column>();
//            using (IDataReader rdr=cmd.ExecuteReader())
//            {
//                while(rdr.Read())
//                {
//                    Column col=new Column();
//                    col.Name=rdr["name"].ToString();
//                    col.PropertyName=CleanUp(col.Name);
//                    col.PropertyType=GetPropertyType(rdr["type"].ToString(), (rdr["type"] == DBNull.Value ? null : rdr["type"].ToString()));
//                    col.Size=GetDatatypeSize(rdr["type"].ToString());
//                    col.Precision=GetDatatypePrecision(rdr["type"].ToString());
//                    col.IsNullable=rdr["notnull"].ToString()=="0";
//                    col.IsAutoIncrement=false;
//                    col.IsPK=rdr["pk"].ToString()!="0";
//                    if (col.IsPK)
//                       col.IsAutoIncrement = tbl.SQL.ToUpper().Contains("AUTOINCREMENT");
//                    else
//                        col.IsAutoIncrement = false;
//                    col.DefaultValue = rdr["dflt_value"] == DBNull.Value ? null : rdr["dflt_value"].ToString();
//                    result.Add(col);
//                }
//            }
//            return result;
//        }
//    }

//    List<TableIndex> LoadIndices(string tableName)
//    {
//        var result=new List<TableIndex>();
//        using (var cmd1=_factory.CreateCommand())
//        {
//            cmd1.Connection=_connection;
//            cmd1.CommandText=string.Format(INDEX_SQL,tableName);
//            using (IDataReader rdr1=cmd1.ExecuteReader())
//            {
//                while(rdr1.Read())
//                {
//                    TableIndex indx=new TableIndex();
//                    indx.Name=rdr1["name"].ToString();
//                    indx.SQL=rdr1["sql"].ToString();
//                    indx.IndexColumns = new List<IndexColumn>();
//                    indx.IsUnique = indx.SQL.ToUpper().Contains("UNIQUE");
//                    using (var cmd2=_factory.CreateCommand())
//                    {
//                        cmd2.Connection=_connection;
//                        cmd2.CommandText=string.Format(INDEX_INFO_SQL,indx.Name);
//                        using (IDataReader rdr2=cmd2.ExecuteReader())
//                        {
//                            while(rdr2.Read())
//                            {
//                              IndexColumn col = new IndexColumn();
//                              col.Name = rdr2["name"].ToString();
//                              indx.IndexColumns.Add(col);
//                            }
//                        }
//                    }
//                    result.Add(indx);
//                }
//            }
//        }
//        return result;
//    }

//    List<FKey> LoadFKeys(string tblName)
//    {
//        using (var cmd=_factory.CreateCommand())
//        {
//            cmd.Connection=_connection;
//            cmd.CommandText=string.Format(FKEY_INFO_SQL,tblName);

//            var result=new List<FKey>();
//            using (IDataReader rdr=cmd.ExecuteReader())
//            {
//                while(rdr.Read())
//                {
//                    FKey key=new FKey();
//                    key.ToTable=rdr["table"].ToString();
//                    key.ToColumn=rdr["to"].ToString();
//                    key.FromColumn=rdr["from"].ToString();
//                    result.Add(key);
//                }
//            }
//            return result;
//        }
//    }


//    string GetPropertyType(string sqlType, string dataScale)
//    {
//        string sysType="string";
//        switch (sqlType.ToLower())
//        {
//            case "integer":
//            case "int":
//            case "tinyint":
//            case "smallint":
//            case "mediumint":
//            case "int2":
//            case "int8":
//                sysType= "long";
//                break;
//            case "bigint":
//            case "unsigned big int":
//                sysType= "long";
//                break;
//            case "uniqueidentifier":
//                sysType=  "Guid";
//                 break;
//            case "smalldatetime":
//            case "datetime":
//            case "date":
//                sysType=  "DateTime";
//                  break;
//            case "float":
//            case "double precision":
//            case "double":
//                sysType="double";
//                break;
//            case "real":
//            case "numeric":
//            case "smallmoney":
//            case "decimal":
//            case "money":
//            case "number":
//                sysType=  "decimal";
//                 break;
//            case "bit":
//                sysType=  "bool";
//                   break;
//            case "image":
//            case "binary":
//            case "varbinary":
//            case "timestamp":
//                sysType=  "byte[]";
//                 break;
//        }

//        if (sqlType == "number" && dataScale == "0")
//            return "long";

//        return sysType;
//    }

//    const string TABLE_SQL=@"SELECT name, type , sql FROM sqlite_master WHERE type IN ('table','view') and name not in ('sqlite_sequence') ";
//    const string COLUMN_SQL=@"pragma table_info({0})";

//    const string INDEX_SQL=@"SELECT name , sql  FROM sqlite_master WHERE type IN ('index') and lower(tbl_name) = lower('{0}')";
//    const string INDEX_INFO_SQL=@"pragma index_info({0})";

//    const string FKEY_INFO_SQL=@"pragma foreign_key_list({0})";

//}

///// <summary>
///// Summary for the Inflector class
///// </summary>
//public static class Inflector {
//    private static readonly List<InflectorRule> _plurals = new List<InflectorRule>();
//    private static readonly List<InflectorRule> _singulars = new List<InflectorRule>();
//    private static readonly List<string> _uncountables = new List<string>();

//    /// <summary>
//    /// Initializes the <see cref="Inflector"/> class.
//    /// </summary>
//    static Inflector() {
//        AddPluralRule("$", "s");
//        AddPluralRule("s$", "s");
//        AddPluralRule("(ax|test)is$", "$1es");
//        AddPluralRule("(octop|vir)us$", "$1i");
//        AddPluralRule("(alias|status)$", "$1es");
//        AddPluralRule("(bu)s$", "$1ses");
//        AddPluralRule("(buffal|tomat)o$", "$1oes");
//        AddPluralRule("([ti])um$", "$1a");
//        AddPluralRule("sis$", "ses");
//        AddPluralRule("(?:([^f])fe|([lr])f)$", "$1$2ves");
//        AddPluralRule("(hive)$", "$1s");
//        AddPluralRule("([^aeiouy]|qu)y$", "$1ies");
//        AddPluralRule("(x|ch|ss|sh)$", "$1es");
//        AddPluralRule("(matr|vert|ind)ix|ex$", "$1ices");
//        AddPluralRule("([m|l])ouse$", "$1ice");
//        AddPluralRule("^(ox)$", "$1en");
//        AddPluralRule("(quiz)$", "$1zes");

//        AddSingularRule("s$", String.Empty);
//        AddSingularRule("ss$", "ss");
//        AddSingularRule("(n)ews$", "$1ews");
//        AddSingularRule("([ti])a$", "$1um");
//        AddSingularRule("((a)naly|(b)a|(d)iagno|(p)arenthe|(p)rogno|(s)ynop|(t)he)ses$", "$1$2sis");
//        AddSingularRule("(^analy)ses$", "$1sis");
//        AddSingularRule("([^f])ves$", "$1fe");
//        AddSingularRule("(hive)s$", "$1");
//        AddSingularRule("(tive)s$", "$1");
//        AddSingularRule("([lr])ves$", "$1f");
//        AddSingularRule("([^aeiouy]|qu)ies$", "$1y");
//        AddSingularRule("(s)eries$", "$1eries");
//        AddSingularRule("(m)ovies$", "$1ovie");
//        AddSingularRule("(x|ch|ss|sh)es$", "$1");
//        AddSingularRule("([m|l])ice$", "$1ouse");
//        AddSingularRule("(bus)es$", "$1");
//        AddSingularRule("(o)es$", "$1");
//        AddSingularRule("(shoe)s$", "$1");
//        AddSingularRule("(cris|ax|test)es$", "$1is");
//        AddSingularRule("(octop|vir)i$", "$1us");
//        AddSingularRule("(alias|status)$", "$1");
//        AddSingularRule("(alias|status)es$", "$1");
//        AddSingularRule("^(ox)en", "$1");
//        AddSingularRule("(vert|ind)ices$", "$1ex");
//        AddSingularRule("(matr)ices$", "$1ix");
//        AddSingularRule("(quiz)zes$", "$1");

//        AddIrregularRule("person", "people");
//        AddIrregularRule("man", "men");
//        AddIrregularRule("child", "children");
//        AddIrregularRule("sex", "sexes");
//        AddIrregularRule("tax", "taxes");
//        AddIrregularRule("move", "moves");

//        AddUnknownCountRule("equipment");
//        AddUnknownCountRule("information");
//        AddUnknownCountRule("rice");
//        AddUnknownCountRule("money");
//        AddUnknownCountRule("species");
//        AddUnknownCountRule("series");
//        AddUnknownCountRule("fish");
//        AddUnknownCountRule("sheep");
//    }

//    /// <summary>
//    /// Adds the irregular rule.
//    /// </summary>
//    /// <param name="singular">The singular.</param>
//    /// <param name="plural">The plural.</param>
//    private static void AddIrregularRule(string singular, string plural) {
//        AddPluralRule(String.Concat("(", singular[0], ")", singular.Substring(1), "$"), String.Concat("$1", plural.Substring(1)));
//        AddSingularRule(String.Concat("(", plural[0], ")", plural.Substring(1), "$"), String.Concat("$1", singular.Substring(1)));
//    }

//    /// <summary>
//    /// Adds the unknown count rule.
//    /// </summary>
//    /// <param name="word">The word.</param>
//    private static void AddUnknownCountRule(string word) {
//        _uncountables.Add(word.ToLower());
//    }

//    /// <summary>
//    /// Adds the plural rule.
//    /// </summary>
//    /// <param name="rule">The rule.</param>
//    /// <param name="replacement">The replacement.</param>
//    private static void AddPluralRule(string rule, string replacement) {
//        _plurals.Add(new InflectorRule(rule, replacement));
//    }

//    /// <summary>
//    /// Adds the singular rule.
//    /// </summary>
//    /// <param name="rule">The rule.</param>
//    /// <param name="replacement">The replacement.</param>
//    private static void AddSingularRule(string rule, string replacement) {
//        _singulars.Add(new InflectorRule(rule, replacement));
//    }

//    /// <summary>
//    /// Makes the plural.
//    /// </summary>
//    /// <param name="word">The word.</param>
//    /// <returns></returns>
//    public static string MakePlural(string word) {
//        return ApplyRules(_plurals, word);
//    }

//    /// <summary>
//    /// Makes the singular.
//    /// </summary>
//    /// <param name="word">The word.</param>
//    /// <returns></returns>
//    public static string MakeSingular(string word) {
//        return ApplyRules(_singulars, word);
//    }

//    /// <summary>
//    /// Applies the rules.
//    /// </summary>
//    /// <param name="rules">The rules.</param>
//    /// <param name="word">The word.</param>
//    /// <returns></returns>
//    private static string ApplyRules(IList<InflectorRule> rules, string word) {
//        string result = word;
//        if (!_uncountables.Contains(word.ToLower())) {
//            for (int i = rules.Count - 1; i >= 0; i--) {
//                string currentPass = rules[i].Apply(word);
//                if (currentPass != null) {
//                    result = currentPass;
//                    break;
//                }
//            }
//        }
//        return result;
//    }

//    /// <summary>
//    /// Converts the string to title case.
//    /// </summary>
//    /// <param name="word">The word.</param>
//    /// <returns></returns>
//    public static string ToTitleCase(string word) {
//        return Regex.Replace(ToHumanCase(AddUnderscores(word)), @"\b([a-z])",
//            delegate(Match match) { return match.Captures[0].Value.ToUpper(); });
//    }

//    /// <summary>
//    /// Converts the string to human case.
//    /// </summary>
//    /// <param name="lowercaseAndUnderscoredWord">The lowercase and underscored word.</param>
//    /// <returns></returns>
//    public static string ToHumanCase(string lowercaseAndUnderscoredWord) {
//        return MakeInitialCaps(Regex.Replace(lowercaseAndUnderscoredWord, @"_", " "));
//    }

//    /// <summary>
//    /// Adds the underscores.
//    /// </summary>
//    /// <param name="pascalCasedWord">The pascal cased word.</param>
//    /// <returns></returns>
//    public static string AddUnderscores(string pascalCasedWord) {
//        return Regex.Replace(Regex.Replace(Regex.Replace(pascalCasedWord, @"([A-Z]+)([A-Z][a-z])", "$1_$2"), @"([a-z\d])([A-Z])", "$1_$2"), @"[-\s]", "_").ToLower();
//    }

//    /// <summary>
//    /// Makes the initial caps.
//    /// </summary>
//    /// <param name="word">The word.</param>
//    /// <returns></returns>
//    public static string MakeInitialCaps(string word) {
//        return String.Concat(word.Substring(0, 1).ToUpper(), word.Substring(1).ToLower());
//    }

//    /// <summary>
//    /// Makes the initial lower case.
//    /// </summary>
//    /// <param name="word">The word.</param>
//    /// <returns></returns>
//    public static string MakeInitialLowerCase(string word) {
//        return String.Concat(word.Substring(0, 1).ToLower(), word.Substring(1));
//    }


//    /// <summary>
//    /// Determine whether the passed string is numeric, by attempting to parse it to a double
//    /// </summary>
//    /// <param name="str">The string to evaluated for numeric conversion</param>
//    /// <returns>
//    /// 	<c>true</c> if the string can be converted to a number; otherwise, <c>false</c>.
//    /// </returns>
//    public static bool IsStringNumeric(string str) {
//        double result;
//        return (double.TryParse(str, NumberStyles.Float, NumberFormatInfo.CurrentInfo, out result));
//    }

//    /// <summary>
//    /// Adds the ordinal suffix.
//    /// </summary>
//    /// <param name="number">The number.</param>
//    /// <returns></returns>
//    public static string AddOrdinalSuffix(string number) {
//        if (IsStringNumeric(number)) {
//            int n = int.Parse(number);
//            int nMod100 = n % 100;

//            if (nMod100 >= 11 && nMod100 <= 13)
//                return String.Concat(number, "th");

//            switch (n % 10) {
//                case 1:
//                    return String.Concat(number, "st");
//                case 2:
//                    return String.Concat(number, "nd");
//                case 3:
//                    return String.Concat(number, "rd");
//                default:
//                    return String.Concat(number, "th");
//            }
//        }
//        return number;
//    }

//    /// <summary>
//    /// Converts the underscores to dashes.
//    /// </summary>
//    /// <param name="underscoredWord">The underscored word.</param>
//    /// <returns></returns>
//    public static string ConvertUnderscoresToDashes(string underscoredWord) {
//        return underscoredWord.Replace('_', '-');
//    }


//    #region Nested type: InflectorRule

//    /// <summary>
//    /// Summary for the InflectorRule class
//    /// </summary>
//    private class InflectorRule {
//        /// <summary>
//        ///
//        /// </summary>
//        public readonly Regex regex;

//        /// <summary>
//        ///
//        /// </summary>
//        public readonly string replacement;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="InflectorRule"/> class.
//        /// </summary>
//        /// <param name="regexPattern">The regex pattern.</param>
//        /// <param name="replacementText">The replacement text.</param>
//        public InflectorRule(string regexPattern, string replacementText) {
//            regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
//            replacement = replacementText;
//        }

//        /// <summary>
//        /// Applies the specified word.
//        /// </summary>
//        /// <param name="word">The word.</param>
//        /// <returns></returns>
//        public string Apply(string word) {
//            if (!regex.IsMatch(word))
//                return null;

//            string replace = regex.Replace(word, replacement);
//            if (word == word.ToUpper())
//                replace = replace.ToUpper();

//            return replace;
//        }
//    }

//    #endregion
//}

//// https://raw.github.com/damieng/DamienGKit
//// http://damieng.com/blog/2009/11/06/multiple-outputs-from-t4-made-easy-revisited

//// Manager class records the various blocks so it can split them up
//class Manager {
//    private class Block {
//        public String Name;
//        public int Start, Length;
//        public bool IncludeInDefault;
//    }

//    private Block currentBlock;
//    private List<Block> files = new List<Block>();
//    private Block footer = new Block();
//    private Block header = new Block();
//    private ITextTemplatingEngineHost host;
//    private StringBuilder template;
//    protected List<String> generatedFileNames = new List<String>();

//    public static Manager Create(ITextTemplatingEngineHost host, StringBuilder template) {
//        return (host is IServiceProvider) ? new VSManager(host, template) : new Manager(host, template);
//    }

//    public void StartNewFile(String name) {
//        if (name == null)
//            throw new ArgumentNullException("name");
//        CurrentBlock = new Block { Name = name };
//    }

//    public void StartFooter(bool includeInDefault = true) {
//        CurrentBlock = footer;
//        footer.IncludeInDefault = includeInDefault;
//    }

//    public void StartHeader(bool includeInDefault = true) {
//        CurrentBlock = header;
//        header.IncludeInDefault = includeInDefault;
//    }

//    public void EndBlock() {
//        if (CurrentBlock == null)
//            return;
//        CurrentBlock.Length = template.Length - CurrentBlock.Start;
//        if (CurrentBlock != header && CurrentBlock != footer)
//            files.Add(CurrentBlock);
//        currentBlock = null;
//    }

//    public virtual void Process(bool split, bool sync = true) {
//        if (split) {
//            EndBlock();
//            String headerText = template.ToString(header.Start, header.Length);
//            String footerText = template.ToString(footer.Start, footer.Length);
//            String outputPath = Path.GetDirectoryName(host.TemplateFile);
//            files.Reverse();
//            if (!footer.IncludeInDefault)
//                template.Remove(footer.Start, footer.Length);
//            foreach(Block block in files) {
//                String fileName = Path.Combine(outputPath, block.Name);
//                String content = headerText + template.ToString(block.Start, block.Length) + footerText;
//                generatedFileNames.Add(fileName);
//                CreateFile(fileName, content);
//                template.Remove(block.Start, block.Length);
//            }
//            if (!header.IncludeInDefault)
//                template.Remove(header.Start, header.Length);
//        }
//    }

//    protected virtual void CreateFile(String fileName, String content) {
//        if (IsFileContentDifferent(fileName, content))
//            File.WriteAllText(fileName, content);
//    }

//    public virtual String GetCustomToolNamespace(String fileName) {
//        return null;
//    }

//    public virtual String DefaultProjectNamespace {
//        get { return null; }
//    }

//    protected bool IsFileContentDifferent(String fileName, String newContent) {
//        return !(File.Exists(fileName) && File.ReadAllText(fileName) == newContent);
//    }

//    private Manager(ITextTemplatingEngineHost host, StringBuilder template) {
//        this.host = host;
//        this.template = template;
//    }

//    private Block CurrentBlock {
//        get { return currentBlock; }
//        set {
//            if (CurrentBlock != null)
//                EndBlock();
//            if (value != null)
//                value.Start = template.Length;
//            currentBlock = value;
//        }
//    }

//    private class VSManager: Manager {
//        private EnvDTE.ProjectItem templateProjectItem;
//        private EnvDTE.DTE dte;
//        private Action<String> checkOutAction;
//        private Action<IEnumerable<String>> projectSyncAction;

//        public override String DefaultProjectNamespace {
//            get {
//                return templateProjectItem.ContainingProject.Properties.Item("DefaultNamespace").Value.ToString();
//            }
//        }

//        public override String GetCustomToolNamespace(string fileName) {
//            return dte.Solution.FindProjectItem(fileName).Properties.Item("CustomToolNamespace").Value.ToString();
//        }

//        public override void Process(bool split, bool sync) {
//            if (templateProjectItem.ProjectItems == null)
//                return;
//            base.Process(split, sync);
//            if (sync)
//                projectSyncAction.EndInvoke(projectSyncAction.BeginInvoke(generatedFileNames, null, null));
//        }

//        protected override void CreateFile(String fileName, String content) {
//            if (IsFileContentDifferent(fileName, content)) {
//                CheckoutFileIfRequired(fileName);
//                File.WriteAllText(fileName, content);
//            }
//        }

//        internal VSManager(ITextTemplatingEngineHost host, StringBuilder template)
//            : base(host, template) {
//            var hostServiceProvider = (IServiceProvider) host;
//            if (hostServiceProvider == null)
//                throw new ArgumentNullException("Could not obtain IServiceProvider");
//            dte = (EnvDTE.DTE) hostServiceProvider.GetService(typeof(EnvDTE.DTE));
//            if (dte == null)
//                throw new ArgumentNullException("Could not obtain DTE from host");
//            templateProjectItem = dte.Solution.FindProjectItem(host.TemplateFile);
//            checkOutAction = (String fileName) => dte.SourceControl.CheckOutItem(fileName);
//            projectSyncAction = (IEnumerable<String> keepFileNames) => ProjectSync(templateProjectItem, keepFileNames);
//        }

//        private static void ProjectSync(EnvDTE.ProjectItem templateProjectItem, IEnumerable<String> keepFileNames) {
//            var keepFileNameSet = new HashSet<String>(keepFileNames);
//            var projectFiles = new Dictionary<String, EnvDTE.ProjectItem>();
//            var originalFilePrefix = Path.GetFileNameWithoutExtension(templateProjectItem.get_FileNames(0)) + ".";
//            foreach(EnvDTE.ProjectItem projectItem in templateProjectItem.ProjectItems)
//                projectFiles.Add(projectItem.get_FileNames(0), projectItem);

//            // Remove unused items from the project
//            foreach(var pair in projectFiles)
//                if (!keepFileNames.Contains(pair.Key) && !(Path.GetFileNameWithoutExtension(pair.Key) + ".").StartsWith(originalFilePrefix))
//                    pair.Value.Delete();

//            // Add missing files to the project
//            foreach(String fileName in keepFileNameSet)
//                if (!projectFiles.ContainsKey(fileName))
//                    templateProjectItem.ProjectItems.AddFromFile(fileName);
//        }

//        private void CheckoutFileIfRequired(String fileName) {
//            var sc = dte.SourceControl;
//            if (sc != null && sc.IsItemUnderSCC(fileName) && !sc.IsItemCheckedOut(fileName))
//                checkOutAction.EndInvoke(checkOutAction.BeginInvoke(fileName, null, null));
//        }
//    }
//}