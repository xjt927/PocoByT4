/**************************************************************************************************
 * 作    者：5                         创始时间：2017-10-09 15:50:47                              *
 * 修 改 人：                          修改时间：                                                 *
 * 描    述：报警点类型实体                                                                       *
 **************************************************************************************************/

using NHibernate.Mapping.Attributes;

namespace PCITC.MES.EP.Entity.PMEntity
{

    /// <summary>
    /// 报警点类型实体
    /// </summary>
    /// <remarks>
    /// 模块编号：pcitc_ep_entity_class_AlarmPointTypeEntity
    /// 作    者：5
    /// 创建时间：2017-10-09 15:50:47
    /// 修改编号：1
    /// 描    述：报警点类型实体
    /// </remarks>
    [Class(Table = "t_pm_alarmpointtype", OptimisticLock = OptimisticLockMode.Version, DynamicUpdate = true)]
    [Cache(Usage = CacheUsage.ReadWrite)]
    public class AlarmPointTypeEntity
    {
        #region Model

		/// <summary>
		/// 报警点类型ID
		/// </summary>
        [Id(0, TypeType = typeof(decimal), Name = "AlarmPointTypeId", UnsavedValue = "0")]
        [Column(1, Name = "alarm_point_type_id", NotNull = true, SqlType = "number")]
        [Generator(2, Class = "sequence")]
        [Param(3, Name = "sequence", Content = "s_pm_alarmpointtype")]
        public virtual Long AlarmPointTypeId { get; set; }

		/// <summary>
		/// 名称
		/// </summary>
        [Property(Column = "name")]
        public virtual String Name { get; set; }

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
    }
}

