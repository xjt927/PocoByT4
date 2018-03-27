 

/*
 * 装置
 * 模块编号：pcitc_pojo_class_Unit
 * 作    者：5
 * 创建时间：2017-10-09 15:50:47
 * 修改编号：1
 * 描    述：装置
 */
@Entity
@DynamicUpdate
@Table(name = "t_pm_unit")
@SequenceGenerator(sequenceName = "s_pm_unit", allocationSize = 1, name = "ID_SEQ")
public class Unit extends BasicInfo 
 {

	/**
	 * 装置ID
	 */
	@Id
	@Column(name = "unit_id") 
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "ID_SEQ")
	private Long unitId;

	/**
	 * 车间ID
	 */
	@Column(name = "workshop_id")
	private Long workshopId;

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
	 * 车间
	 */
	@ManyToOne(fetch = FetchType.LAZY)
	@JoinColumn(name = "Workshop_Id", insertable = false, updatable = false)
	private Workshop workshop;


        public Long getUnitId()
        {
            return unitId;
        }

        public void setUnitId(Long unitId)
        {
            this.unitId = unitId;
        }

        public Long getWorkshopId()
        {
            return workshopId;
        }

        public void setWorkshopId(Long workshopId)
        {
            this.workshopId = workshopId;
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

        public Workshop getWorkshop()
        {
            return workshop;
        }

        public void setWorkshop(Workshop workshop)
        {
            this.workshop = workshop;
        }
}

