 

/*
 * 报警点
 * 模块编号：pcitc_pojo_class_AlarmPoint
 * 作    者：5
 * 创建时间：2017-10-09 15:50:47
 * 修改编号：1
 * 描    述：报警点
 */
@Entity
@DynamicUpdate
@Table(name = "t_pm_alarmpoint")
@SequenceGenerator(sequenceName = "s_pm_alarmpoint", allocationSize = 1, name = "ID_SEQ")
public class AlarmPoint extends BasicInfo 
 {

	/**
	 * 报警点ID
	 */
	@Id
	@Column(name = "alarm_point_id") 
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "ID_SEQ")
	private Long alarmPointId;

	/**
	 * 生产单元ID
	 */
	@Column(name = "prdtcell_id")
	private Long prdtcellId;

	/**
	 * 位号
	 */
	@Column(name = "tag")
	private String tag;

	/**
	 * 位置
	 */
	@Column(name = "location")
	private String location;

	/**
	 * PID图号
	 */
	@Column(name = "pid_code")
	private String pidCode;

	/**
	 * 报警点类型ID
	 */
	@Column(name = "alarm_point_type_id")
	private Long alarmPointTypeId;

	/**
	 * 监测类型（1物料；2能源；3质量）
	 */
	@Column(name = "monitor_type")
	private Integer monitorType;

	/**
	 * 计量单位ID
	 */
	@Column(name = "measunit_id")
	private Long measunitId;

	/**
	 * 仪表类型（1监测表；2控制表）
	 */
	@Column(name = "instrmt_type")
	private Integer instrmtType;

	/**
	 * 虚实标记（0实表(按读数)；1虚表(按用量)）
	 */
	@Column(name = "virtual_reality_flag")
	private Integer virtualRealityFlag;

	/**
	 * 报警点高高报
	 */
	@Column(name = "alarm_point_hh")
	private Integer alarmPointHh;

	/**
	 * 报警点高报
	 */
	@Column(name = "alarm_point_hi")
	private Integer alarmPointHi;

	/**
	 * 报警点低报
	 */
	@Column(name = "alarm_point_lo")
	private Integer alarmPointLo;

	/**
	 * 报警点低低报
	 */
	@Column(name = "alarm_point_ll")
	private Integer alarmPointLl;

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
	 * 生产单元
	 */
	@ManyToOne(fetch = FetchType.LAZY)
	@JoinColumn(name = "PrdtCell_Id", insertable = false, updatable = false)
	private Prdtcell prdtcell;

	/**
	 * 报警点类型
	 */
	@ManyToOne(fetch = FetchType.LAZY)
	@JoinColumn(name = "Alarm_Point_Type_Id", insertable = false, updatable = false)
	private AlarmPointType alarmPointType;

	/**
	 * 计量单位
	 */
	@ManyToOne(fetch = FetchType.LAZY)
	@JoinColumn(name = "MeasUnit_Id", insertable = false, updatable = false)
	private Measunit measunit;


        public Long getAlarmPointId()
        {
            return alarmPointId;
        }

        public void setAlarmPointId(Long alarmPointId)
        {
            this.alarmPointId = alarmPointId;
        }

        public Long getPrdtcellId()
        {
            return prdtcellId;
        }

        public void setPrdtcellId(Long prdtcellId)
        {
            this.prdtcellId = prdtcellId;
        }

        public String getTag()
        {
            return tag;
        }

        public void setTag(String tag)
        {
            this.tag = tag;
        }

        public String getLocation()
        {
            return location;
        }

        public void setLocation(String location)
        {
            this.location = location;
        }

        public String getPidCode()
        {
            return pidCode;
        }

        public void setPidCode(String pidCode)
        {
            this.pidCode = pidCode;
        }

        public Long getAlarmPointTypeId()
        {
            return alarmPointTypeId;
        }

        public void setAlarmPointTypeId(Long alarmPointTypeId)
        {
            this.alarmPointTypeId = alarmPointTypeId;
        }

        public Integer getMonitorType()
        {
            return monitorType;
        }

        public void setMonitorType(Integer monitorType)
        {
            this.monitorType = monitorType;
        }

        public Long getMeasunitId()
        {
            return measunitId;
        }

        public void setMeasunitId(Long measunitId)
        {
            this.measunitId = measunitId;
        }

        public Integer getInstrmtType()
        {
            return instrmtType;
        }

        public void setInstrmtType(Integer instrmtType)
        {
            this.instrmtType = instrmtType;
        }

        public Integer getVirtualRealityFlag()
        {
            return virtualRealityFlag;
        }

        public void setVirtualRealityFlag(Integer virtualRealityFlag)
        {
            this.virtualRealityFlag = virtualRealityFlag;
        }

        public Integer getAlarmPointHh()
        {
            return alarmPointHh;
        }

        public void setAlarmPointHh(Integer alarmPointHh)
        {
            this.alarmPointHh = alarmPointHh;
        }

        public Integer getAlarmPointHi()
        {
            return alarmPointHi;
        }

        public void setAlarmPointHi(Integer alarmPointHi)
        {
            this.alarmPointHi = alarmPointHi;
        }

        public Integer getAlarmPointLo()
        {
            return alarmPointLo;
        }

        public void setAlarmPointLo(Integer alarmPointLo)
        {
            this.alarmPointLo = alarmPointLo;
        }

        public Integer getAlarmPointLl()
        {
            return alarmPointLl;
        }

        public void setAlarmPointLl(Integer alarmPointLl)
        {
            this.alarmPointLl = alarmPointLl;
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

        public Prdtcell getPrdtcell()
        {
            return prdtcell;
        }

        public void setPrdtcell(Prdtcell prdtcell)
        {
            this.prdtcell = prdtcell;
        }

        public AlarmPointType getAlarmpointtype()
        {
            return alarmPointType;
        }

        public void setAlarmpointtype(AlarmPointType alarmPointType)
        {
            this.alarmPointType = alarmPointType;
        }

        public Measunit getMeasunit()
        {
            return measunit;
        }

        public void setMeasunit(Measunit measunit)
        {
            this.measunit = measunit;
        }
}

