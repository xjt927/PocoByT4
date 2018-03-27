using System.Collections.Generic;

namespace XJT.Com.EntitySql.ExcelSource
{
    public class TableEntity
    {
        /// <summary>
        /// 中文名称
        /// </summary>
        public string ChineseName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 是否新增
        /// </summary>
        public bool IsNewTable { get; set; }

        /// <summary>
        /// 数据表字段内容
        /// </summary>
        public IEnumerable<ColumnEntity> ColumnEntityList { get; set; }
    }

    /// <summary>
    /// 表字段内容
    /// </summary>
    public class ColumnEntity
    {

        /// <summary>
        /// 中文名称
        /// </summary>
        public string ChineseNameCol { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishNameCol { get; set; }

        /// <summary>
        /// 字段类型（数据库存储）
        /// </summary>
        public string TypeCol { get; set; }

        /// <summary>
        /// 字段类型（实体类型）
        /// </summary>
        public string PropertyType { get; set; }
        
        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNullCol { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 是否外键
        /// </summary>
        public bool IsForeignKey { get; set; }

        /// <summary>
        /// 关联表
        /// </summary>
        public string AssociatedTable { get; set; }

        /// <summary>
        /// 是否唯一
        /// </summary>
        public bool IsUnique { get; set; }
    }
}
