/**************************************************************************************************
 * 作    者：5                         创始时间：2017-10-09 15:50:47                              *
 * 修 改 人：                          修改时间：                                                 *
 * 描    述：事件类型实体                                                                         *
 **************************************************************************************************/

using NHibernate.Mapping.Attributes;

namespace PCITC.MES.EP.Entity.PMEntity
{

    /// <summary>
    /// 事件类型实体
    /// </summary>
    /// <remarks>
    /// 模块编号：pcitc_ep_entity_class_EventTypeEntity
    /// 作    者：5
    /// 创建时间：2017-10-09 15:50:47
    /// 修改编号：1
    /// 描    述：事件类型实体
    /// </remarks>
    [Class(Table = "t_pm_eventtype", OptimisticLock = OptimisticLockMode.Version, DynamicUpdate = true)]
    [Cache(Usage = CacheUsage.ReadWrite)]
    public class EventTypeEntity
    {
        #region Model

		/// <summary>
		/// 事件类型ID
		/// </summary>
        [Id(0, TypeType = typeof(decimal), Name = "EventTypeId", UnsavedValue = "0")]
        [Column(1, Name = "event_type_id", NotNull = true, SqlType = "number")]
        [Generator(2, Class = "sequence")]
        [Param(3, Name = "sequence", Content = "s_pm_eventtype")]
        public virtual Long EventTypeId { get; set; }

		/// <summary>
		/// 名称
		/// </summary>
        [Property(Column = "name")]
        public virtual String Name { get; set; }

		/// <summary>
		/// 上级ID
		/// </summary>
        [Property(Column = "parent_id")]
        public virtual Integer ParentId { get; set; }

		/// <summary>
		/// 是否启用（1是；0否）
		/// </summary>
        [Property(Column = "in_use")]
        public virtual int InUse { get; set; }

		/// <summary>
		/// 排序
		/// </summary>
        [Property(Column = "sort_num")]
        public virtual int SortNum { get; set; }

        #endregion

        #region 关联实体

        /// <summary>
        /// 事件类型
        /// </summary>
        [ManyToOne(Name = "EventType", ClassType = typeof(EventTypeEntity), Lazy = Laziness.Proxy,
             Column = "parent_id", Unique = true, Insert = false, Update = false)]
        public virtual EventTypeEntity EventType { get; set; }

		#endregion
    }
}

