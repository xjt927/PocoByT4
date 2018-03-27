 

/*
 * 车间
 * 模块编号：pcitc_ep_entity_class_Workshop
 * 作    者：3
 * 创建时间：2017-10-09 13:05:45
 * 修改编号：1
 * 描    述：车间
 */
@@Entity
@@DynamicUpdate
@@Table(name = "t_pm_workshop")
@SequenceGenerator(sequenceName = "s_pm_workshop", allocationSize = 1, name = "ID_SEQ")
public class Workshop extends BasicInfo 
 {

	/**
	 * 车间ID
	 */
	@Id
	@Column(name = "workshop_id") 
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "ID_SEQ")
	private Long workshopId;

	/**
	 * 工厂ID
	 */
	@Column(name = "factory_id")
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

	/**
	 * 工厂
	 */
	@ManyToOne(fetch = FetchType.LAZY)
	@JoinColumn(name = "Factory_Id", insertable = false, updatable = false)
	private Factory factory;


        public Long getWorkshopId()
        {
            return workshopId;
        }

        public void setWorkshopId(Long workshopId)
        {
            this.workshopId = workshopId;
        }

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

        public Factory getFactory()
        {
            return factory;
        }

        public void setFactory(Factory factory)
        {
            this.factory = factory;
        }
}

