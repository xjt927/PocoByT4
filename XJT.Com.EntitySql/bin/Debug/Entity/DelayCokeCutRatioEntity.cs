/**************************************************************************************************
 * 作    者：好人0002                  创始时间：2017-08-14 16:09:40                              *
 * 修 改 人：                          修改时间：                                                 *
 * 描    述：延迟焦化装置切焦过程VOCs排放系数实体                                                 *
 **************************************************************************************************/

using System;
using NHibernate.Mapping.Attributes;

namespace PCITC.MES.EP.Entity.VOCEntity
{

    /// <summary>
    /// 延迟焦化装置切焦过程VOCs排放系数实体
    /// </summary>
    /// <remarks>
    /// 模块编号：pcitc_ep_entity_class_DelayCokeCutRatioEntity
    /// 作    者：好人0002
    /// 创建时间：2017-08-14 16:09:40
    /// 修改编号：1
    /// 描    述：延迟焦化装置切焦过程VOCs排放系数实体
    /// </remarks>
    [Class(Table = "t_voc_delaycokecutratio", OptimisticLock = OptimisticLockMode.Version, DynamicUpdate = true)]
    [Cache(Usage = CacheUsage.ReadWrite)]
    public class DelayCokeCutRatioEntity : BasicInfoEntity
    {
        #region Model

		/// <summary>
		/// 延迟焦化装置切焦过程VOCs排放系数ID
		/// </summary>
        [Id(0, TypeType = typeof(decimal), Name = "DelayCokeCutRatioId", UnsavedValue = "0")]
        [Column(1, Name = "delay_coke_cut_ratio_id", NotNull = true, SqlType = "number")]
        [Generator(2, Class = "sequence")]
        [Param(3, Name = "sequence", Content = "s_voc_delaycokecutratio")]
        public virtual decimal DelayCokeCutRatioId { get; set; }

		/// <summary>
		/// 排放清单版本ID
		/// </summary>
        [Property(Column = "emiss_list_version_id")]
        public virtual decimal EmissListVersionId { get; set; }

		/// <summary>
		/// 污染物ID
		/// </summary>
        [Property(Column = "polt_mtrl_id")]
        public virtual decimal PoltMtrlId { get; set; }

		/// <summary>
		/// 排放系数(t/t装置进料)
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
        /// 污染物
        /// </summary>
        [ManyToOne(Name = "PoltMtrl", ClassType = typeof(PoltMtrlEntity), Lazy = Laziness.Proxy,
             Column = "polt_mtrl_id", Unique = true, Insert = false, Update = false)]
        public virtual PoltMtrlEntity PoltMtrl { get; set; }

		#endregion
    }
}

