 

/*
 * 报警点类型
 * 模块编号：pcitc_pojo_class_AlarmPointType
 * 作    者：5
 * 创建时间：2017-10-09 15:50:47
 * 修改编号：1
 * 描    述：报警点类型
 */
@Entity
@DynamicUpdate
@Table(name = "t_pm_alarmpointtype")
@SequenceGenerator(sequenceName = "s_pm_alarmpointtype", allocationSize = 1, name = "ID_SEQ")
public class AlarmPointType 
 {

	/**
	 * 报警点类型ID
	 */
	@Id
	@Column(name = "alarm_point_type_id") 
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "ID_SEQ")
	private Long alarmPointTypeId;

	/**
	 * 名称
	 */
	@Column(name = "name")
	private String name;

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


        public Long getAlarmPointTypeId()
        {
            return alarmPointTypeId;
        }

        public void setAlarmPointTypeId(Long alarmPointTypeId)
        {
            this.alarmPointTypeId = alarmPointTypeId;
        }

        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
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

