 

/*
 * 工厂
 * 模块编号：pcitc_pojo_class_Factory
 * 作    者：5
 * 创建时间：2017-10-09 15:50:47
 * 修改编号：1
 * 描    述：工厂
 */
@Entity
@DynamicUpdate
@Table(name = "t_pm_factory")
@SequenceGenerator(sequenceName = "s_pm_factory", allocationSize = 1, name = "ID_SEQ")
public class Factory extends BasicInfo 
 {

	/**
	 * 工厂ID
	 */
	@Id
	@Column(name = "factory_id") 
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "ID_SEQ")
	private Long factoryId;

	/**
	 * 名称
	 */
	@Column(name = "name")
	private String name;

	/**
	 * 简称
	 */
	@Column(name = "sname")
	private String sname;

	/**
	 * 标准编码
	 */
	@Column(name = "std_code")
	private String stdCode;

	/**
	 * 是否启用（1是；0否）
	 */
	@Column(name = "in_use")
	private Integer inUse;

	/**
	 * 排序
	 */
	@Column(name = "sort_num")
	private Integer sortNum;

	/**
	 * 描述
	 */
	@Column(name = "des")
	private String des;


        public Long getFactoryId()
        {
            return factoryId;
        }

        public void setFactoryId(Long factoryId)
        {
            this.factoryId = factoryId;
        }

        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public String getSname()
        {
            return sname;
        }

        public void setSname(String sname)
        {
            this.sname = sname;
        }

        public String getStdCode()
        {
            return stdCode;
        }

        public void setStdCode(String stdCode)
        {
            this.stdCode = stdCode;
        }

        public Integer getInUse()
        {
            return inUse;
        }

        public void setInUse(Integer inUse)
        {
            this.inUse = inUse;
        }

        public Integer getSortNum()
        {
            return sortNum;
        }

        public void setSortNum(Integer sortNum)
        {
            this.sortNum = sortNum;
        }

        public String getDes()
        {
            return des;
        }

        public void setDes(String des)
        {
            this.des = des;
        }
}

