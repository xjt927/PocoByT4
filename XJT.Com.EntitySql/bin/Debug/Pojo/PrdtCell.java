 

/*
 * 生产单元
 * 模块编号：pcitc_pojo_class_PrdtCell
 * 作    者：5
 * 创建时间：2017-10-09 15:50:47
 * 修改编号：1
 * 描    述：生产单元
 */
@Entity
@DynamicUpdate
@Table(name = "t_pm_prdtcell")
@SequenceGenerator(sequenceName = "s_pm_prdtcell", allocationSize = 1, name = "ID_SEQ")
public class PrdtCell extends BasicInfo 
 {

	/**
	 * 生产单元ID
	 */
	@Id
	@Column(name = "prdtcell_id") 
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "ID_SEQ")
	private Long prdtcellId;

	/**
	 * 装置ID
	 */
	@Column(name = "unit_id")
	private Long unitId;

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
	 * 装置
	 */
	@ManyToOne(fetch = FetchType.LAZY)
	@JoinColumn(name = "Unit_Id", insertable = false, updatable = false)
	private Unit unit;


        public Long getPrdtcellId()
        {
            return prdtcellId;
        }

        public void setPrdtcellId(Long prdtcellId)
        {
            this.prdtcellId = prdtcellId;
        }

        public Long getUnitId()
        {
            return unitId;
        }

        public void setUnitId(Long unitId)
        {
            this.unitId = unitId;
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

        public Unit getUnit()
        {
            return unit;
        }

        public void setUnit(Unit unit)
        {
            this.unit = unit;
        }
}

