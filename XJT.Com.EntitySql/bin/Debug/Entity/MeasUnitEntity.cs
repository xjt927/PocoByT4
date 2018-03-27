/**************************************************************************************************
 * 作    者：5                         创始时间：2017-10-09 15:50:47                              *
 * 修 改 人：                          修改时间：                                                 *
 * 描    述：计量单位实体                                                                         *
 **************************************************************************************************/

using System;
using NHibernate.Mapping.Attributes;

namespace PCITC.MES.EP.Entity.PMEntity
{

    /// <summary>
    /// 计量单位实体
    /// </summary>
    /// <remarks>
    /// 模块编号：pcitc_ep_entity_class_MeasUnitEntity
    /// 作    者：5
    /// 创建时间：2017-10-09 15:50:47
    /// 修改编号：1
    /// 描    述：计量单位实体
    /// </remarks>
    [Class(Table = "t_pm_measunit", OptimisticLock = OptimisticLockMode.Version, DynamicUpdate = true)]
    [Cache(Usage = CacheUsage.ReadWrite)]
    public class MeasUnitEntity : BasicInfoEntity
    {
        #region Model

		/// <summary>
		/// 计量单位ID
		/// </summary>
        [Id(0, TypeType = typeof(decimal), Name = "MeasunitId", UnsavedValue = "0")]
        [Column(1, Name = "measunit_id", NotNull = true, SqlType = "number")]
        [Generator(2, Class = "sequence")]
        [Param(3, Name = "sequence", Content = "s_pm_measunit")]
        public virtual Long MeasunitId { get; set; }

		/// <summary>
		/// 名称
		/// </summary>
        [Property(Column = "name")]
        public virtual String Name { get; set; }

		/// <summary>
		/// 符号
		/// </summary>
        [Property(Column = "sign")]
        public virtual String Sign { get; set; }

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

