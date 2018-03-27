using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using XJT.Com.EntitySql.ExcelSource;

namespace XJT.Com.EntitySql.Common
{
    public class Tools
    {
        /// <summary>
        /// 日志委托
        /// </summary>
        public static Action<string, EnumCommon.LogEnum> LogAction;

        public static string Creater { get; set; }
        public static string GeneratePath { get; set; }
        public static string TableNameLike { get; set; }
        public static int SelectGenerateComb { get; set; }

        private static List<TableEntity> _storageTables;

        /// <summary>
        /// 表实体集合
        /// </summary>
        public static List<TableEntity> StorageTables
        {
            get
            {
                if (_storageTables != null)
                {
                    return _storageTables;
                }
                return new List<TableEntity>();
            }
            set { _storageTables = value; }
        }

        /// <summary> 
        /// 日志输出到本地
        /// </summary>
        /// <param name="log"></param>
        public static void Log2File(string log)
        {
            try
            {
                string path = Directory.GetCurrentDirectory() + "\\logfile.txt";
                using (var sWriter = new StreamWriter(path, true, Encoding.UTF8))
                {
                    sWriter.WriteLine(DateTime.Now + " " + log);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void File2Path(string path, string sqlContent, EnumCommon.LogEnum logEnum)
        {
            try
            {
                var path1 = System.IO.Path.GetDirectoryName(path);
                if (!Directory.Exists(path1))
                {
                    if (path1 != null)
                        Directory.CreateDirectory(path1);
                }
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (var sWriter = new StreamWriter(path, true, Encoding.UTF8))
                {
                    sWriter.WriteLine(sqlContent);
                }
                Tools.LogAction("生成路径：" + path, logEnum);
            }
            catch (Exception ex)
            {
                Tools.LogAction(ex.ToString(), logEnum);
            }
        }


        /// <summary>
        /// 把枚举描述和值规则拼接字符串
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <returns>枚举值,枚举描述;枚举值,枚举描述;枚举值,枚举描述</returns>
        public static IList<DictionaryEntry> GetEnumList<T>()
        {
            List<DictionaryEntry> list = new List<DictionaryEntry>();
            Dictionary<int, string> dic = GetDictionary(typeof(T));
            DictionaryEntry entity;
            foreach (var key in dic.Keys)
            {
                entity = new DictionaryEntry();
                entity.Key = key;
                entity.Value = dic[key];
                list.Add(entity);
            }
            return list;
        }

        /// <summary>
        /// 获取枚举值、描述列表
        /// </summary>
        /// <param name="enumType">枚举类型</param>                
        /// <returns>
        /// 返回枚举值、描述列表
        /// </returns>
        private static Dictionary<int, string> GetDictionary(Type enumType)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (int enumValue in Enum.GetValues(enumType))
            {
                dic.Add(enumValue, string.Empty);
                FieldInfo fieldinfo = enumType.GetField(Enum.GetName(enumType, enumValue));
                if (fieldinfo == null)
                {
                    return null;
                }
                Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs.Length != 0)
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    dic[enumValue] = da.Description;
                }
            }
            return dic;
        }
    }


    /// <summary>
    /// 转换为大小写操作
    /// </summary> 
    public class LetterConvert
    {
        /// <summary>
        /// 字符串转换成大写
        /// </summary>
        /// <returns></returns>
        public static string StrToUpper(string str)
        {
            return str.ToUpper();
        }
        /// <summary>
        /// 字符串转换成小写
        /// </summary>
        /// <returns></returns>
        public static string StrToLower(string str)
        {
            return str.ToLower();
        }

        /// <summary>
        /// 首字母大写，其他字母小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertStr(string str)
        {
            string[] strArr = str.Split('_');
            var result = new StringBuilder();
            strArr.ToList().ForEach(item =>
            {
                if (item.Length > 1)
                {
                    string first = item.Substring(0, 1);
                    string content = item.Substring(1);
                    result.Append(first + StrToLower(content));
                }
                else
                {
                    result.Append(item);
                }
            });
            return result.ToString();
        }
    }
}
