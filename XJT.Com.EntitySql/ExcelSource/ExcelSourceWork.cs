using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XJT.Com.EntitySql.Common;

namespace XJT.Com.EntitySql.ExcelSource
{
    class ExcelSourceWork
    {
        /// <summary>
        /// 开始工作
        /// </summary>
        /// <param name="filePathList">excel路径</param> 
        /// <param name="ignoreSheet">忽略的sheet页</param>
        public void DoWork(IEnumerable<string> filePathList, IEnumerable<string> ignoreSheet, string tableNames)
        {
            try
            {
                List<TableEntity> newTableEntities = new List<TableEntity>();
                List<string> tableNameList = new List<string>();
                ReadExcel readExcel = new ReadExcel();
                if (Tools.StorageTables.Count == 0)
                {
                    var tableEntitys = readExcel.GetTableEntitys(filePathList, ignoreSheet);
                    Tools.StorageTables = tableEntitys;
                }

                if (tableNames.Length > 0)
                {
                    if (!string.IsNullOrWhiteSpace(tableNames))
                    {
                        tableNameList = tableNames.Split(',').ToList();
                    }
                    if (tableNameList.Count > 0)
                    {
                        tableNameList.ForEach(x =>
                        {
                            var entity = Tools.StorageTables.Find(y => y.EnglishName.ToLower() == x);
                            if (entity != null)
                            {
                                newTableEntities.Add(entity);
                            }
                        });
                    }
                }

                EnditySqlDoWork(newTableEntities.Count > 0 ? newTableEntities : Tools.StorageTables);
            }
            catch (Exception e)
            {
                Tools.LogAction(e.ToString(), EnumCommon.LogEnum.CommonLog);
            }
        }

        /// <summary>
        /// 调用生成实体或者SQL方法执行
        /// </summary>
        /// <param name="newTableEntities"></param>
        /// <param name="path"></param>
        private void EnditySqlDoWork(List<TableEntity> newTableEntities)
        {
            Excel2Sql excel2Sql = new Excel2Sql();
            Excel2Entity excel2Entity = new Excel2Entity();
            Excel2Pojo excel2Pojo = new Excel2Pojo();
            switch (Tools.SelectGenerateComb)
            {
                case (int)EnumCommon.GenerateEnum.Pojo:
                    Task.Factory.StartNew(() => excel2Pojo.Excel2PojoDoWork(newTableEntities));
                    break;
                case (int)EnumCommon.GenerateEnum.Entity:
                    Task.Factory.StartNew(() => excel2Entity.Excel2EntityDoWork(newTableEntities));
                    break;
                case (int)EnumCommon.GenerateEnum.SQL:
                    Task.Factory.StartNew(() => excel2Sql.Excel2SqlDoWork(newTableEntities));
                    break;
                default:
                    Task.Factory.StartNew(() => excel2Sql.Excel2SqlDoWork(newTableEntities));
                    Task.Factory.StartNew(() => excel2Entity.Excel2EntityDoWork(newTableEntities));
                    Task.Factory.StartNew(() => excel2Pojo.Excel2PojoDoWork(newTableEntities));
                    break;
            }
        }
    }
}
