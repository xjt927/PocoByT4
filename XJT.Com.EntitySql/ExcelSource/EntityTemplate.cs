namespace XJT.Com.EntitySql.ExcelSource
{
   
    class EntityTemplate
    {
        #region 脚本模板

        public string CreateEntity = @"{0}

{1}

namespace {2}

{3}
{4}
";

        #region 文件头注释

        public string FileNode = @"/**************************************************************************************************
 * 作    者：{Creater}创始时间：{Time}                              *
 * 修 改 人：                          修改时间：                                                 *
 * 描    述：{FileDes}*
 **************************************************************************************************/";

        #endregion

        #region 类文件注释

        public string ClassNode = @"    /// <summary>
    /// {FileDes}
    /// </summary>
    /// <remarks>
    /// 模块编号：{ModelNum}
    /// 作    者：{Creater}
    /// 创建时间：{Time}
    /// 修改编号：1
    /// 描    述：{FileDes}
    /// </remarks>
    [Class(Table = ""{TableName}"", OptimisticLock = OptimisticLockMode.Version, DynamicUpdate = true)]
    [Cache(Usage = CacheUsage.ReadWrite)]
    public class {ClassName}";

        #endregion

        #region 字段属性

        public string ColumnKeyParameter = @"
		/// <summary>
		/// {ColumnDes}
		/// </summary>
        [Id(0, TypeType = typeof(decimal), Name = ""{ColumnName}"", UnsavedValue = ""0"")]
        [Column(1, Name = ""{Field}"", NotNull = true, SqlType = ""number"")]
        [Generator(2, Class = ""sequence"")]
        [Param(3, Name = ""sequence"", Content = ""{SequenceName}"")]
        public virtual {Type} {ColumnName} { get; set; }
";

        public string ColumnViewParameter = @"
		/// <summary>
		/// {ColumnDes}
		/// </summary>
        [Id(0, TypeType = typeof(decimal), Name = ""{ColumnName}"", UnsavedValue = ""0"")]
        [Column(1, Name = ""{Field}"", NotNull = true, SqlType = ""number"")]
        public virtual {Type} {ColumnName} { get; set; }
";

        public string ColumnParameter = @"
		/// <summary>
		/// {ColumnDes}
		/// </summary>
        [Property(Column = ""{Field}"")]
        public virtual {Type} {ColumnName} { get; set; }
";

        #endregion

        #region 关联实体

        public string ForeignKeyEntity = @"
        /// <summary>
        /// {ForeignKeyDes}
        /// </summary>
        [ManyToOne(Name = ""{ForeignKey}"", ClassType = typeof({ForeignKey}Entity), Lazy = Laziness.Proxy,
             Column = ""{Field}"", Unique = true, Insert = false, Update = false)]
        public virtual {ForeignKey}Entity {ForeignKey} { get; set; }
";

        public string ViewF = @"        /// <summary>
        /// 控制设施监测污染源关联视图
        /// </summary>
        [Bag(0, Name = ""{ForeignKey}"", Table = ""{TableName}"")]
        [Key(1, Column = ""{Field}"", Update = false)]
        [OneToMany(2, ClassType = typeof(ContrEquipMonPoltSourceEntity))]
        public virtual IList<ContrEquipMonPoltSourceEntity> ContrEquipMonPoltSourceList { get; set; }";

        #endregion

        #endregion

    }
}
