 

/*
 * 事件类型
 * 模块编号：pcitc_pojo_class_EventType
 * 作    者：5
 * 创建时间：2017-10-09 15:50:47
 * 修改编号：1
 * 描    述：事件类型
 */
@Entity
@DynamicUpdate
@Table(name = "t_pm_eventtype")
@SequenceGenerator(sequenceName = "s_pm_eventtype", allocationSize = 1, name = "ID_SEQ")
public class EventType 
 {

	/**
	 * 事件类型ID
	 */
	@Id
	@Column(name = "event_type_id") 
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "ID_SEQ")
	private Long eventTypeId;

	/**
	 * 名称
	 */
	@Column(name = "name")
	private String name;

	/**
	 * 上级ID
	 */
	@Column(name = "parent_id")
	private Integer parentId;

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
	 * 事件类型
	 */
	@ManyToOne(fetch = FetchType.LAZY)
	@JoinColumn(name = "Parent_Id", insertable = false, updatable = false)
	private EventType eventType;


        public Long getEventTypeId()
        {
            return eventTypeId;
        }

        public void setEventTypeId(Long eventTypeId)
        {
            this.eventTypeId = eventTypeId;
        }

        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public Integer getParentId()
        {
            return parentId;
        }

        public void setParentId(Integer parentId)
        {
            this.parentId = parentId;
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

        public EventType getEventtype()
        {
            return eventType;
        }

        public void setEventtype(EventType eventType)
        {
            this.eventType = eventType;
        }
}

