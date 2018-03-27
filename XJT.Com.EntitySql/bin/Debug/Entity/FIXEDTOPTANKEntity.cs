/**************************************************************************************************
 * 作    者：123                       创始时间：2017-08-22 23:36:06                              *
 * 修 改 人：                          修改时间：                                                 *
 * 描    述：固定顶罐实体                                                                         *
 **************************************************************************************************/

using System;
using NHibernate.Mapping.Attributes;

namespace PCITC.MES.EP.Entity.VOCEntity
{

    /// <summary>
    /// 固定顶罐实体
    /// </summary>
    /// <remarks>
    /// 模块编号：pcitc_ep_entity_class_FIXEDTOPTANKEntity
    /// 作    者：123
    /// 创建时间：2017-08-22 23:36:06
    /// 修改编号：1
    /// 描    述：固定顶罐实体
    /// </remarks>
    [Class(Table = "t_voc_fixedtoptank", OptimisticLock = OptimisticLockMode.Version, DynamicUpdate = true)]
    [Cache(Usage = CacheUsage.ReadWrite)]
    public class FIXEDTOPTANKEntity : BasicInfoEntity
    {
        #region Model

		/// <summary>
		/// 固定顶罐ID
		/// </summary>
        [Id(0, TypeType = typeof(decimal), Name = "VocEmissPointId", UnsavedValue = "0")]
        [Column(1, Name = "voc_emiss_point_id", NotNull = true, SqlType = "number")]
        [Generator(2, Class = "sequence")]
        [Param(3, Name = "sequence", Content = "s_voc_fixedtoptank")]
        public virtual decimal VocEmissPointId { get; set; }

		/// <summary>
		/// 罐区ID
		/// </summary>
        [Property(Column = "polt_source_id")]
        public virtual decimal PoltSourceId { get; set; }

		/// <summary>
		/// 罐号
		/// </summary>
        [Property(Column = "tank_num")]
        public virtual string TankNum { get; set; }

		/// <summary>
		/// 罐类型ID
		/// </summary>
        [Property(Column = "tank_type_id")]
        public virtual decimal TankTypeId { get; set; }

		/// <summary>
		/// 公称容积(m3)
		/// </summary>
        [Property(Column = "nominal_volume")]
        public virtual decimal NominalVolume { get; set; }

		/// <summary>
		/// 有效容积(m3)
		/// </summary>
        [Property(Column = "eff_volume")]
        public virtual decimal EffVolume { get; set; }

		/// <summary>
		/// 直径(m)
		/// </summary>
        [Property(Column = "diameter")]
        public virtual decimal Diameter { get; set; }

		/// <summary>
		/// 罐体高度(m)
		/// </summary>
        [Property(Column = "tank_height")]
        public virtual decimal? TankHeight { get; set; }

		/// <summary>
		/// 罐顶高度(m)
		/// </summary>
        [Property(Column = "tanktop_height")]
        public virtual decimal? TanktopHeight { get; set; }

		/// <summary>
		/// 罐体长度(m)
		/// </summary>
        [Property(Column = "tank_length")]
        public virtual decimal? TankLength { get; set; }

		/// <summary>
		/// 罐体总长度(m)
		/// </summary>
        [Property(Column = "tank_total_length")]
        public virtual decimal? TankTotalLength { get; set; }

		/// <summary>
		/// 罐密封类型ID
		/// </summary>
        [Property(Column = "tank_seal_type_id")]
        public virtual decimal TankSealTypeId { get; set; }

		/// <summary>
		/// 呼吸阀/定压阀运行效率(%)
		/// </summary>
        [Property(Column = "breather_eff")]
        public virtual decimal? BreatherEff { get; set; }

		/// <summary>
		/// 平均储存液量(t)
		/// </summary>
        [Property(Column = "avg_storage_liquid")]
        public virtual decimal AvgStorageLiquid { get; set; }

		/// <summary>
		/// 呼吸阀压力设定(kPa)
		/// </summary>
        [Property(Column = "breather_pressure")]
        public virtual decimal BreatherPressure { get; set; }

		/// <summary>
		/// 呼吸阀真空设定(kPa)
		/// </summary>
        [Property(Column = "breather_vacuum")]
        public virtual decimal BreatherVacuum { get; set; }

		/// <summary>
		/// 是否地下罐（1是；0否）
		/// </summary>
        [Property(Column = "in_unground_tank")]
        public virtual int InUngroundTank { get; set; }

		/// <summary>
		/// 是否不锈钢罐或内防腐（1是；0否）
		/// </summary>
        [Property(Column = "in_stain")]
        public virtual int InStain { get; set; }

		/// <summary>
		/// 固定顶罐罐顶形式ID
		/// </summary>
        [Property(Column = "fixed_top_tank_type_id")]
        public virtual decimal FixedTopTankTypeId { get; set; }

		/// <summary>
		/// 罐锥顶斜率(%)
		/// </summary>
        [Property(Column = "tank_top_slope")]
        public virtual decimal? TankTopSlope { get; set; }

		/// <summary>
		/// 罐拱顶半径(m)
		/// </summary>
        [Property(Column = "tank_top_radii")]
        public virtual decimal? TankTopRadii { get; set; }

		/// <summary>
		/// 控制设施ID
		/// </summary>
        [Property(Column = "contr_equip_id")]
        public virtual decimal? ContrEquipId { get; set; }

		/// <summary>
		/// 建成日期
		/// </summary>
        [Property(Column = "tank_crt_date")]
        public virtual DateTime TankCrtDate { get; set; }

		/// <summary>
		/// 首次收料日期
		/// </summary>
        [Property(Column = "first_mtrl_date")]
        public virtual DateTime FirstMtrlDate { get; set; }

		/// <summary>
		/// 描述
		/// </summary>
        [Property(Column = "des")]
        public virtual string Des { get; set; }

        #endregion

        #region 关联实体

        /// <summary>
        /// VOCs排放点
        /// </summary>
        [ManyToOne(Name = "VOCEmissPoint", ClassType = typeof(VOCEmissPointEntity), Lazy = Laziness.Proxy,
             Column = "voc_emiss_point_id", Unique = true, Insert = false, Update = false)]
        public virtual VOCEmissPointEntity VOCEmissPoint { get; set; }

        /// <summary>
        /// 罐区
        /// </summary>
        [ManyToOne(Name = "TANKAREA", ClassType = typeof(TANKAREAEntity), Lazy = Laziness.Proxy,
             Column = "polt_source_id", Unique = true, Insert = false, Update = false)]
        public virtual TANKAREAEntity TANKAREA { get; set; }

        /// <summary>
        /// 罐类型
        /// </summary>
        [ManyToOne(Name = "TankType", ClassType = typeof(TankTypeEntity), Lazy = Laziness.Proxy,
             Column = "tank_type_id", Unique = true, Insert = false, Update = false)]
        public virtual TankTypeEntity TankType { get; set; }

        /// <summary>
        /// 罐密封类型
        /// </summary>
        [ManyToOne(Name = "TankSealType", ClassType = typeof(TankSealTypeEntity), Lazy = Laziness.Proxy,
             Column = "tank_seal_type_id", Unique = true, Insert = false, Update = false)]
        public virtual TankSealTypeEntity TankSealType { get; set; }

        /// <summary>
        /// 固定顶罐罐顶形式
        /// </summary>
        [ManyToOne(Name = "FixedTopTankType", ClassType = typeof(FixedTopTankTypeEntity), Lazy = Laziness.Proxy,
             Column = "fixed_top_tank_type_id", Unique = true, Insert = false, Update = false)]
        public virtual FixedTopTankTypeEntity FixedTopTankType { get; set; }

        /// <summary>
        /// 控制设施
        /// </summary>
        [ManyToOne(Name = "ContrEquip", ClassType = typeof(ContrEquipEntity), Lazy = Laziness.Proxy,
             Column = "contr_equip_id", Unique = true, Insert = false, Update = false)]
        public virtual ContrEquipEntity ContrEquip { get; set; }

		#endregion
    }
}

