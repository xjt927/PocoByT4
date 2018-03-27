 

/*
 * 计量单位
 * 模块编号：pcitc_pojo_class_MeasUnit
 * 作    者：5
 * 创建时间：2017-10-09 15:50:47
 * 修改编号：1
 * 描    述：计量单位
 */
@Entity
@DynamicUpdate
@Table(name = "t_pm_measunit")
@SequenceGenerator(sequenceName = "s_pm_measunit", allocationSize = 1, name = "ID_SEQ")
public class MeasUnit extends BasicInfo 
 {

	/**
	 * 计量单位ID
	 */
	@Id
	@Column(name = "measunit_id") 
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "ID_SEQ")
	private Long measunitId;

	/**
	 * 名称
	 */
	@Column(name = "name")
	private String name;

	/**
	 * 符号
	 */
	@Column(name = "sign")
	private String sign;

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


        public Long getMeasunitId()
        {
            return measunitId;
        }

        public void setMeasunitId(Long measunitId)
        {
            this.measunitId = measunitId;
        }

        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public String getSign()
        {
            return sign;
        }

        public void setSign(String sign)
        {
            this.sign = sign;
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

