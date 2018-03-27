/**************************************************************************************************
 * 作    者：好人0002                  创始时间：2017-08-14 16:09:40                              *
 * 修 改 人：                          修改时间：                                                 *
 * 描    述：固定顶罐运行信息实体                                                                 *
 **************************************************************************************************/

using System;
using NHibernate.Mapping.Attributes;

namespace PCITC.MES.EP.Entity.VOCEntity
{

    /// <summary>
    /// 固定顶罐运行信息实体
    /// </summary>
    /// <remarks>
    /// 模块编号：pcitc_ep_entity_class_FIXEDTOPTANKRUNEntity
    /// 作    者：好人0002
    /// 创建时间：2017-08-14 16:09:40
    /// 修改编号：1
    /// 描    述：固定顶罐运行信息实体
    /// </remarks>
    [Class(Table = "t_voc_fixedtoptankrun", OptimisticLock = OptimisticLockMode.Version, DynamicUpdate = true)]
    [Cache(Usage = CacheUsage.ReadWrite)]
    public class FIXEDTOPTANKRUNEntity : BasicInfoEntity
    {
        #region Model

		/// <summary>
		/// 固定顶罐运行信息ID
		/// </summary>
        [Id(0, TypeType = typeof(decimal), Name = "FixedtoptankRunId", UnsavedValue = "0")]
        [Column(1, Name = "fixedtoptank_run_id", NotNull = true, SqlType = "number")]
        [Generator(2, Class = "sequence")]
        [Param(3, Name = "sequence", Content = "s_voc_fixedtoptankrun")]
        public virtual decimal FixedtoptankRunId { get; set; }

		/// <summary>
		/// VOCs排放点ID
		/// </summary>
        [Property(Column = "voc_emiss_point_id")]
        public virtual decimal VocEmissPointId { get; set; }

		/// <summary>
		/// 期间类型(1年、2季、3月、9不定期)
		/// </summary>
        [Property(Column = "period_type")]
        public virtual int PeriodType { get; set; }

		/// <summary>
		/// 开始日期
		/// </summary>
        [Property(Column = "begin_date")]
        public virtual DateTime BeginDate { get; set; }

		/// <summary>
		/// 结束日期
		/// </summary>
        [Property(Column = "end_date")]
        public virtual DateTime EndDate { get; set; }

		/// <summary>
		/// 储存物料类别(1原油；2混合物；3单体物质；4单体物质混合物)
		/// </summary>
        [Property(Column = "mtrl_catgr")]
        public virtual int MtrlCatgr { get; set; }

		/// <summary>
		/// 储存物料ID
		/// </summary>
        [Property(Column = "mtrl_id")]
        public virtual decimal MtrlId { get; set; }

		/// <summary>
		/// 液体平均储存高度(m)
		/// </summary>
        [Property(Column = "liquid_avg_storage_height")]
        public virtual decimal LiquidAvgStorageHeight { get; set; }

		/// <summary>
		/// 实际压力(表压)(kPa)
		/// </summary>
        [Property(Column = "fact_pressure")]
        public virtual decimal FactPressure { get; set; }

		/// <summary>
		/// 实际周转量(t)
		/// </summary>
        [Property(Column = "fact_turnover_qty")]
        public virtual decimal FactTurnoverQty { get; set; }

		/// <summary>
		/// 最大储存液体量(t)
		/// </summary>
        [Property(Column = "max_storage_qty")]
        public virtual decimal MaxStorageQty { get; set; }

		/// <summary>
		/// 最小储存液体量(t)
		/// </summary>
        [Property(Column = "min_storage_qty")]
        public virtual decimal MinStorageQty { get; set; }

		/// <summary>
		/// 实际平均储存温度(℃)
		/// </summary>
        [Property(Column = "fact_avg_storage_t")]
        public virtual decimal FactAvgStorageT { get; set; }

		/// <summary>
		/// 罐漆颜色ID
		/// </summary>
        [Property(Column = "tank_paint_colour_id")]
        public virtual decimal? TankPaintColourId { get; set; }

		/// <summary>
		/// 罐喷漆色光ID
		/// </summary>
        [Property(Column = "tank_paint_light_id")]
        public virtual decimal? TankPaintLightId { get; set; }

		/// <summary>
		/// 罐漆状况ID
		/// </summary>
        [Property(Column = "tank_paint_state_id")]
        public virtual decimal? TankPaintStateId { get; set; }

		/// <summary>
		/// 保温材料太阳能吸收率(%)
		/// </summary>
        [Property(Column = "heat_solarabs")]
        public virtual decimal? HeatSolarabs { get; set; }

        #endregion

        #region 关联实体

        /// <summary>
        /// VOCs排放点
        /// </summary>
        [ManyToOne(Name = "VOCEmissPoint", ClassType = typeof(VOCEmissPointEntity), Lazy = Laziness.Proxy,
             Column = "voc_emiss_point_id", Unique = true, Insert = false, Update = false)]
        public virtual VOCEmissPointEntity VOCEmissPoint { get; set; }

        /// <summary>
        /// 罐漆颜色
        /// </summary>
        [ManyToOne(Name = "TankPaintColour", ClassType = typeof(TankPaintColourEntity), Lazy = Laziness.Proxy,
             Column = "tank_paint_colour_id", Unique = true, Insert = false, Update = false)]
        public virtual TankPaintColourEntity TankPaintColour { get; set; }

        /// <summary>
        /// 罐喷漆色光
        /// </summary>
        [ManyToOne(Name = "TankPaintLight", ClassType = typeof(TankPaintLightEntity), Lazy = Laziness.Proxy,
             Column = "tank_paint_light_id", Unique = true, Insert = false, Update = false)]
        public virtual TankPaintLightEntity TankPaintLight { get; set; }

        /// <summary>
        /// 罐漆状况
        /// </summary>
        [ManyToOne(Name = "TankPaintState", ClassType = typeof(TankPaintStateEntity), Lazy = Laziness.Proxy,
             Column = "tank_paint_state_id", Unique = true, Insert = false, Update = false)]
        public virtual TankPaintStateEntity TankPaintState { get; set; }

		#endregion
    }
}

