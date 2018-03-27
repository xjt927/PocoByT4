/**************************************************************************************************
 * 作    者：123                       创始时间：2017-08-22 23:20:55                              *
 * 修 改 人：                          修改时间：                                                 *
 * 描    述：原油理化参数实体                                                                     *
 **************************************************************************************************/

using System;
using NHibernate.Mapping.Attributes;
using PCITC.MES.EP.Entity.AAAEntity;

namespace PCITC.MES.EP.Entity.VOCEntity
{

    /// <summary>
    /// 原油理化参数实体
    /// </summary>
    /// <remarks>
    /// 模块编号：pcitc_ep_entity_class_CRDOILPHYSPARAEntity
    /// 作    者：123
    /// 创建时间：2017-08-22 23:20:55
    /// 修改编号：1
    /// 描    述：原油理化参数实体
    /// </remarks>
    [Class(Table = "t_voc_crdoilphyspara", OptimisticLock = OptimisticLockMode.Version, DynamicUpdate = true)]
    [Cache(Usage = CacheUsage.ReadWrite)]
    public class CRDOILPHYSPARAEntity : BasicInfoEntity
    {
        #region Model

		/// <summary>
		/// 原油理化参数ID
		/// </summary>
        [Id(0, TypeType = typeof(decimal), Name = "CrdoilPhysparaId", UnsavedValue = "0")]
        [Column(1, Name = "crdoil_physpara_id", NotNull = true, SqlType = "number")]
        [Generator(2, Class = "sequence")]
        [Param(3, Name = "sequence", Content = "s_voc_crdoilphyspara")]
        public virtual decimal CrdoilPhysparaId { get; set; }

		/// <summary>
		/// 企业ID
		/// </summary>
        [Property(Column = "company_id")]
        public virtual decimal CompanyId { get; set; }

		/// <summary>
		/// 原油名称
		/// </summary>
        [Property(Column = "crdoil_name")]
        public virtual string CrdoilName { get; set; }

		/// <summary>
		/// 行业ID
		/// </summary>
        [Property(Column = "industry_id")]
        public virtual decimal IndustryId { get; set; }

		/// <summary>
		/// 油垢因子介质ID
		/// </summary>
        [Property(Column = "oil_factor_medium_id")]
        public virtual decimal OilFactorMediumId { get; set; }

		/// <summary>
		/// 船舶装载排放因子介质ID
		/// </summary>
        [Property(Column = "ship_load_factor_medium_id")]
        public virtual decimal ShipLoadFactorMediumId { get; set; }

		/// <summary>
		/// 密度(t/m³)
		/// </summary>
        [Property(Column = "density")]
        public virtual decimal Density { get; set; }

		/// <summary>
		/// 油气分子质量(g/g-mol)
		/// </summary>
        [Property(Column = "oilgas_mol_qly")]
        public virtual decimal OilgasMolQly { get; set; }

		/// <summary>
		/// 雷德蒸汽压测定方法
		/// </summary>
        [Property(Column = "rvp_method")]
        public virtual string RvpMethod { get; set; }

		/// <summary>
		/// 雷德蒸汽压(kPa)
		/// </summary>
        [Property(Column = "rvp")]
        public virtual decimal Rvp { get; set; }

		/// <summary>
		/// 初馏点(℃)
		/// </summary>
        [Property(Column = "initial_point")]
        public virtual decimal? InitialPoint { get; set; }

		/// <summary>
		/// 是否为挥发性有机液体（1是；0否）
		/// </summary>
        [Property(Column = "in_vola")]
        public virtual int InVola { get; set; }

		/// <summary>
		/// 是否为原油或高挥发性有机物或危险化学品（1是；0否）
		/// </summary>
        [Property(Column = "in_hvola")]
        public virtual int InHvola { get; set; }

		/// <summary>
		/// 是否为有毒有害气体或者粉尘物质（1是；0否）
		/// </summary>
        [Property(Column = "in_haza")]
        public virtual int InHaza { get; set; }

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
        /// 企业实体
        /// </summary>
        [ManyToOne(Name = "Company", ClassType = typeof(OUEntity), Lazy = Laziness.Proxy,
            Column = "company_id", Unique = true, Insert = false, Update = false)]
        public virtual OUEntity Company { get; set; }

        /// <summary>
        /// 行业
        /// </summary>
        [ManyToOne(Name = "Industry", ClassType = typeof(IndustryEntity), Lazy = Laziness.Proxy,
             Column = "industry_id", Unique = true, Insert = false, Update = false)]
        public virtual IndustryEntity Industry { get; set; }

        /// <summary>
        /// 油垢因子介质
        /// </summary>
        [ManyToOne(Name = "OilFactorMedium", ClassType = typeof(OilFactorMediumEntity), Lazy = Laziness.Proxy,
             Column = "oil_factor_medium_id", Unique = true, Insert = false, Update = false)]
        public virtual OilFactorMediumEntity OilFactorMedium { get; set; }

        /// <summary>
        /// 船舶装载排放因子介质
        /// </summary>
        [ManyToOne(Name = "ShipLoadFactorMedium", ClassType = typeof(ShipLoadFactorMediumEntity), Lazy = Laziness.Proxy,
             Column = "ship_load_factor_medium_id", Unique = true, Insert = false, Update = false)]
        public virtual ShipLoadFactorMediumEntity ShipLoadFactorMedium { get; set; }

		#endregion
    }
}

