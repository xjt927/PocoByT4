/**************************************************************************************************
 * 作    者：5                         创始时间：2017-10-09 15:50:47                              *
 * 修 改 人：                          修改时间：                                                 *
 * 描    述：报警点实体                                                                           *
 **************************************************************************************************/

using System;
using NHibernate.Mapping.Attributes;

namespace PCITC.MES.EP.Entity.PMEntity
{

    /// <summary>
    /// 报警点实体
    /// </summary>
    /// <remarks>
    /// 模块编号：pcitc_ep_entity_class_AlarmPointEntity
    /// 作    者：5
    /// 创建时间：2017-10-09 15:50:47
    /// 修改编号：1
    /// 描    述：报警点实体
    /// </remarks>
    [Class(Table = "t_pm_alarmpoint", OptimisticLock = OptimisticLockMode.Version, DynamicUpdate = true)]
    [Cache(Usage = CacheUsage.ReadWrite)]
    public class AlarmPointEntity : BasicInfoEntity
    {
        #region Model

		/// <summary>
		/// 报警点ID
		/// </summary>
        [Id(0, TypeType = typeof(decimal), Name = "AlarmPointId", UnsavedValue = "0")]
        [Column(1, Name = "alarm_point_id", NotNull = true, SqlType = "number")]
        [Generator(2, Class = "sequence")]
        [Param(3, Name = "sequence", Content = "s_pm_alarmpoint")]
        public virtual Long AlarmPointId { get; set; }

		/// <summary>
		/// 生产单元ID
		/// </summary>
        [Property(Column = "prdtcell_id")]
        public virtual Long PrdtcellId { get; set; }

		/// <summary>
		/// 位号
		/// </summary>
        [Property(Column = "tag")]
        public virtual String Tag { get; set; }

		/// <summary>
		/// 位置
		/// </summary>
        [Property(Column = "location")]
        public virtual String Location { get; set; }

		/// <summary>
		/// PID图号
		/// </summary>
        [Property(Column = "pid_code")]
        public virtual String PidCode { get; set; }

		/// <summary>
		/// 报警点类型ID
		/// </summary>
        [Property(Column = "alarm_point_type_id")]
        public virtual Long AlarmPointTypeId { get; set; }

		/// <summary>
		/// 监测类型（1物料；2能源；3质量）
		/// </summary>
        [Property(Column = "monitor_type")]
        public virtual int MonitorType { get; set; }

		/// <summary>
		/// 计量单位ID
		/// </summary>
        [Property(Column = "measunit_id")]
        public virtual Long MeasunitId { get; set; }

		/// <summary>
		/// 仪表类型（1监测表；2控制表）
		/// </summary>
        [Property(Column = "instrmt_type")]
        public virtual int InstrmtType { get; set; }

		/// <summary>
		/// 虚实标记（0实表(按读数)；1虚表(按用量)）
		/// </summary>
        [Property(Column = "virtual_reality_flag")]
        public virtual int VirtualRealityFlag { get; set; }

		/// <summary>
		/// 报警点高高报
		/// </summary>
        [Property(Column = "alarm_point_hh")]
        public virtual Integer AlarmPointHh { get; set; }

		/// <summary>
		/// 报警点高报
		/// </summary>
        [Property(Column = "alarm_point_hi")]
        public virtual Integer AlarmPointHi { get; set; }

		/// <summary>
		/// 报警点低报
		/// </summary>
        [Property(Column = "alarm_point_lo")]
        public virtual Integer AlarmPointLo { get; set; }

		/// <summary>
		/// 报警点低低报
		/// </summary>
        [Property(Column = "alarm_point_ll")]
        public virtual Integer AlarmPointLl { get; set; }

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

		/// <summary>
		/// 描述
		/// </summary>
        [Property(Column = "des")]
        public virtual String Des { get; set; }

        #endregion

        #region 关联实体

        /// <summary>
        /// 生产单元
        /// </summary>
        [ManyToOne(Name = "Prdtcell", ClassType = typeof(PrdtcellEntity), Lazy = Laziness.Proxy,
             Column = "prdtcell_id", Unique = true, Insert = false, Update = false)]
        public virtual PrdtcellEntity Prdtcell { get; set; }

        /// <summary>
        /// 报警点类型
        /// </summary>
        [ManyToOne(Name = "AlarmPointType", ClassType = typeof(AlarmPointTypeEntity), Lazy = Laziness.Proxy,
             Column = "alarm_point_type_id", Unique = true, Insert = false, Update = false)]
        public virtual AlarmPointTypeEntity AlarmPointType { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [ManyToOne(Name = "Measunit", ClassType = typeof(MeasunitEntity), Lazy = Laziness.Proxy,
             Column = "measunit_id", Unique = true, Insert = false, Update = false)]
        public virtual MeasunitEntity Measunit { get; set; }

		#endregion
    }
}

