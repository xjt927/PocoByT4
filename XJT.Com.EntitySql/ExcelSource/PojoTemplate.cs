namespace XJT.Com.EntitySql.ExcelSource
{
   
    class PojoTemplate
    {
        #region 脚本模板

        public string CreateEntity = @"{0}

{1}
{2}
";
        #region 类文件注释

        public string ClassNode = @"/*
 * {FileDes}
 * 模块编号：{ModelNum}
 * 作    者：{Creater}
 * 创建时间：{Time}
 * 修改编号：1
 * 描    述：{FileDes}
 */
@Entity
@DynamicUpdate
@Table(name = ""{TableName}"")
@SequenceGenerator(sequenceName = ""{SequenceName}"", allocationSize = 1, name = ""ID_SEQ"")
public class {ClassName} 
";

        #endregion

        #region 字段属性

        public string ColumnKeyParameter = @"
	/**
	 * {ColumnDes}
	 */
	@Id
	@Column(name = ""{Field}"") 
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = ""ID_SEQ"")
	private {Type} {ColumnName};
";

        public string ColumnViewParameter = @"
	/**
	 * {ColumnDes}
	 */
	@Column(name = ""{Field}"")
	private {Type} {ColumnName};
";

        public string ColumnParameter = @"
	/**
	 * {ColumnDes}
	 */
	@Column(name = ""{Field}"")
	private {Type} {ColumnName};
";

        #endregion

        #region 关联实体

        public string ForeignKeyEntity = @"
	/**
	 * {ForeignKeyDes}
	 */
	@ManyToOne(fetch = FetchType.LAZY)
	@JoinColumn(name = ""{Field}"", insertable = false, updatable = false)
	private {ForeignKey} {lowForeignKey};
";

        #endregion

        #region get  set

        public string GetAttrubute = @"

        public {Field} get{ColumnName}()
        {
            return {firstToLower};
        }";

        public string SetAttrubute = @"

        public void set{ColumnName}({Field} {firstToLower})
        {
            this.{firstToLower} = {firstToLower};
        }";



        #endregion

        #endregion

    }
}
