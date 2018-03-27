 

/*
 * 工厂
 * 模块编号：pcitc_ep_entity_class_Factory
 * 作    者：.
 * 创建时间：2017-10-09 10:36:49
 * 修改编号：1
 * 描    述：工厂
 */
@@Entity
@@DynamicUpdate
@@Table(name = "t_pm_factory")
@SequenceGenerator(sequenceName = "{SequenceName}", allocationSize = 1, name = "ID_SEQ")
public class Factory extends BasicInfo 

    {
        #region Model

	/**
	 * 工厂ID
	 */
	@@Id
	@@Column(name = "factory_id") 
    @@GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "ID_SEQ")
	private decimal factoryId;

	/**
	 * 名称
	 */
	@Column(name = "name")
	private string name;

	/**
	 * 简称
	 */
	@Column(name = "sname")
	private string sname;

	/**
	 * 标准编码
	 */
	@Column(name = "std_code")
	private string stdCode;

	/**
	 * 是否启用（1是；0否）
	 */
	@Column(name = "in_use")
	private int inUse;

	/**
	 * 排序
	 */
	@Column(name = "sort_num")
	private int sortNum;

	/**
	 * 描述
	 */
	@Column(name = "des")
	private string des;


        public decimal getFactory_Id()
        {
            return factoryId;
        }

        public void setFactory_Id(decimal factoryId)
        {
            this.factoryId = factoryId;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getSname()
        {
            return sname;
        }

        public void setSname(string sname)
        {
            this.sname = sname;
        }

        public string getStd_Code()
        {
            return stdCode;
        }

        public void setStd_Code(string stdCode)
        {
            this.stdCode = stdCode;
        }

        public int getIn_Use()
        {
            return inUse;
        }

        public void setIn_Use(int inUse)
        {
            this.inUse = inUse;
        }

        public int getSort_Num()
        {
            return sortNum;
        }

        public void setSort_Num(int sortNum)
        {
            this.sortNum = sortNum;
        }

        public string getDes()
        {
            return des;
        }

        public void setDes(string des)
        {
            this.des = des;
        }
		#endregion
    }
}

