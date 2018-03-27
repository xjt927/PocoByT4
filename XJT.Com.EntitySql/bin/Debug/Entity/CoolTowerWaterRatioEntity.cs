/**************************************************************************************************
 * 作    者：好人0002                  创始时间：2017-08-14 16:09:40                              *
 * 修 改 人：                          修改时间：                                                 *
 * 描    述：冷却塔/循环水排放系数实体                                                            *
 **************************************************************************************************/

using System;
using NHibernate.Mapping.Attributes;

namespace PCITC.MES.EP.Entity.VOCEntity
{

    /// <summary>
    /// 冷却塔/循环水排放系数实体
    /// </summary>
    /// <remarks>
    /// 模块编号：pcitc_ep_entity_class_CoolTowerWaterRatioEntity
    /// 作    者：好人0002
    /// 创建时间：2017-08-14 16:09:40
    /// 修改编号：1
    /// 描    述：冷却塔/循环水排放系数实体
    /// </remarks>
    [Class(Table = "t_voc_cooltowerwaterratio", OptimisticLock = OptimisticLockMode.Version, DynamicUpdate = true)]
    [Cache(Usage = CacheUsage.ReadWrite)]
    public class CoolTowerWaterRatioEntity : BasicInfoEntity
    {
        #region Model

		/// <summary>
		/// 冷却塔/循环水排放系数ID
		/// </summary>
        [Id(0, TypeType = typeof(decimal), Name = "CooltowerWaterRatioId", UnsavedValue = "0")]
        [Column(1, Name = "cooltower_water_ratio_id", NotNull = true, SqlType = "number")]
        [Generator(2, Class = "sequence")]
        [Param(3, Name = "sequence", Content = "s_voc_cooltowerwaterratio")]
        public virtual decimal CooltowerWaterRatioId { get; set; }

		/// <summary>
		/// 排放清单版本ID
		/// </summary>
        [Property(Column = "emiss_list_version_id")]
        public virtual decimal EmissListVersionId { get; set; }

		/// <summary>
		/// 冷却塔类型ID
		/// </summary>
        [Property(Column = "cooltower_type_id")]
        public virtual decimal CooltowerTypeId { get; set; }

		/// <summary>
		/// 排放系数(t/m3-循环水量)
		/// </summary>
        [Property(Column = "emiss_ratio")]
        public virtual decimal EmissRatio { get; set; }

        #endregion

        #region 关联实体

        /// <summary>
        /// 排放清单版本
        /// </summary>
        [ManyToOne(Name = "EmissListVersion", ClassType = typeof(EmissListVersionEntity), Lazy = Laziness.Proxy,
             Column = "emiss_list_version_id", Unique = true, Insert = false, Update = false)]
        public virtual EmissListVersionEntity EmissListVersion { get; set; }

        /// <summary>
        /// 冷却塔类型
        /// </summary>
        [ManyToOne(Name = "CooltowerType", ClassType = typeof(CooltowerTypeEntity), Lazy = Laziness.Proxy,
             Column = "cooltower_type_id", Unique = true, Insert = false, Update = false)]
        public virtual CooltowerTypeEntity CooltowerType { get; set; }

		#endregion
    }
}

