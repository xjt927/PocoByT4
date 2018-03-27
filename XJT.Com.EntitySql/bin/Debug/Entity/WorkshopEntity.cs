/**************************************************************************************************
 * 作    者：5                         创始时间：2017-10-09 15:50:47                              *
 * 修 改 人：                          修改时间：                                                 *
 * 描    述：车间实体                                                                             *
 **************************************************************************************************/

using System;
using NHibernate.Mapping.Attributes;

namespace PCITC.MES.EP.Entity.PMEntity
{

    /// <summary>
    /// 车间实体
    /// </summary>
    /// <remarks>
    /// 模块编号：pcitc_ep_entity_class_WorkshopEntity
    /// 作    者：5
    /// 创建时间：2017-10-09 15:50:47
    /// 修改编号：1
    /// 描    述：车间实体
    /// </remarks>
    [Class(Table = "t_pm_workshop", OptimisticLock = OptimisticLockMode.Version, DynamicUpdate = true)]
    [Cache(Usage = CacheUsage.ReadWrite)]
    public class WorkshopEntity : BasicInfoEntity
    {
        #region Model

		/// <summary>
		/// 车间ID
		/// </summary>
        [Id(0, TypeType = typeof(decimal), Name = "WorkshopId", UnsavedValue = "0")]
        [Column(1, Name = "workshop_id", NotNull = true, SqlType = "number")]
        [Generator(2, Class = "sequence")]
        [Param(3, Name = "sequence", Content = "s_pm_workshop")]
        public virtual decimal WorkshopId { get; set; }

		/// <summary>
		/// 工厂ID
		/// </summary>
        [Property(Column = "factory_id")]
        public virtual decimal FactoryId { get; set; }

		/// <summary>
		/// 名称
		/// </summary>
        [Property(Column = "name")]
        public virtual string Name { get; set; }

		/// <summary>
		/// 简称
		/// </summary>
        [Property(Column = "sname")]
        public virtual string Sname { get; set; }

		/// <summary>
		/// 标准编码
		/// </summary>
        [Property(Column = "std_code")]
        public virtual string StdCode { get; set; }

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
        public virtual string Des { get; set; }

        #endregion

        #region 关联实体

        /// <summary>
        /// 工厂
        /// </summary>
        [ManyToOne(Name = "Factory", ClassType = typeof(FactoryEntity), Lazy = Laziness.Proxy,
             Column = "factory_id", Unique = true, Insert = false, Update = false)]
        public virtual FactoryEntity Factory { get; set; }

		#endregion
    }
}

