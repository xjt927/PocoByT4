/**************************************************************************************************
 * 作    者：jiangtao.xue              创始时间：2017-08-23 09:19:53                              *
 * 修 改 人：                          修改时间：                                                 *
 * 描    述：装卸台运行信息实体                                                                   *
 **************************************************************************************************/

using System;
using NHibernate.Mapping.Attributes;

namespace PCITC.MES.EP.Entity.VOCEntity
{

    /// <summary>
    /// 装卸台运行信息实体
    /// </summary>
    /// <remarks>
    /// 模块编号：pcitc_ep_entity_class_LOADRACKRUNEntity
    /// 作    者：jiangtao.xue
    /// 创建时间：2017-08-23 09:19:53
    /// 修改编号：1
    /// 描    述：装卸台运行信息实体
    /// </remarks>
    [Class(Table = "t_voc_loadrackrun", OptimisticLock = OptimisticLockMode.Version, DynamicUpdate = true)]
    [Cache(Usage = CacheUsage.ReadWrite)]
    public class LOADRACKRUNEntity : BasicInfoEntity
    {
        #region Model

        /// <summary>
        /// 装卸台运行信息ID
        /// </summary>
        [Id(0, TypeType = typeof(decimal), Name = "LoadrackRunId", UnsavedValue = "0")]
        [Column(1, Name = "loadrack_run_id", NotNull = true, SqlType = "number")]
        [Generator(2, Class = "sequence")]
        [Param(3, Name = "sequence", Content = "s_voc_loadrackrun")]
        public virtual decimal LoadrackRunId { get; set; }

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
        /// 储存/装载物料类别ID
        /// </summary>
        [Property(Column = "storage_load_mtrl_catgr_id")]
        public virtual decimal StorageLoadMtrlCatgrId { get; set; }

        /// <summary>
        /// 装载物料ID
        /// </summary>
        [Property(Column = "mtrl_id")]
        public virtual decimal MtrlId { get; set; }

        /// <summary>
        /// 装卸台装载方式ID
        /// </summary>
        [Property(Column = "loadrack_load_type_id")]
        public virtual decimal LoadrackLoadTypeId { get; set; }

        /// <summary>
        /// 装卸台操作方式ID
        /// </summary>
        [Property(Column = "load_type_id")]
        public virtual decimal LoadTypeId { get; set; }

        /// <summary>
        /// 船体情况ID
        /// </summary>
        [Property(Column = "ship_cond_id")]
        public virtual decimal? ShipCondId { get; set; }

        /// <summary>
        /// 船舶上次装载物性质ID
        /// </summary>
        [Property(Column = "ship_mtrl_nature_id")]
        public virtual decimal? ShipMtrlNatureId { get; set; }

        /// <summary>
        /// 船舶装载物料类型ID
        /// </summary>
        [Property(Column = "load_mtrl_type_id")]
        public virtual decimal? LoadMtrlTypeId { get; set; }

        /// <summary>
        /// 实际周转量(t)
        /// </summary>
        [Property(Column = "fact_turnover_qty")]
        public virtual decimal FactTurnoverQty { get; set; }

        /// <summary>
        /// 实际装载温度(℃)
        /// </summary>
        [Property(Column = "fact_load_t")]
        public virtual decimal? FactLoadT { get; set; }

        #endregion

        #region 关联实体

        /// <summary>
        /// VOCs排放点
        /// </summary>
        [ManyToOne(Name = "VOCEmissPoint", ClassType = typeof(VOCEmissPointEntity), Lazy = Laziness.Proxy,
             Column = "voc_emiss_point_id", Unique = true, Insert = false, Update = false)]
        public virtual VOCEmissPointEntity VOCEmissPoint { get; set; }

        /// <summary>
        /// 储存/装载物料类别
        /// </summary>
        [ManyToOne(Name = "StorageLoadMtrlCatgr", ClassType = typeof(StorageLoadMtrlCatgrEntity), Lazy = Laziness.Proxy,
             Column = "storage_load_mtrl_catgr_id", Unique = true, Insert = false, Update = false)]
        public virtual StorageLoadMtrlCatgrEntity StorageLoadMtrlCatgr { get; set; }

        /// <summary>
        /// 装卸台装载方式
        /// </summary>
        [ManyToOne(Name = "LoadrackLoadType", ClassType = typeof(LoadrackLoadTypeEntity), Lazy = Laziness.Proxy,
             Column = "loadrack_load_type_id", Unique = true, Insert = false, Update = false)]
        public virtual LoadrackLoadTypeEntity LoadrackLoadType { get; set; }

        /// <summary>
        /// 装卸台操作方式
        /// </summary>
        [ManyToOne(Name = "LoadType", ClassType = typeof(LoadTypeEntity), Lazy = Laziness.Proxy,
             Column = "load_type_id", Unique = true, Insert = false, Update = false)]
        public virtual LoadTypeEntity LoadType { get; set; }

        /// <summary>
        /// 船体情况
        /// </summary>
        [ManyToOne(Name = "ShipCond", ClassType = typeof(ShipCondEntity), Lazy = Laziness.Proxy,
             Column = "ship_cond_id", Unique = true, Insert = false, Update = false)]
        public virtual ShipCondEntity ShipCond { get; set; }

        /// <summary>
        /// 船舶上次装载物性质
        /// </summary>
        [ManyToOne(Name = "ShipMtrlNature", ClassType = typeof(ShipMtrlNatureEntity), Lazy = Laziness.Proxy,
             Column = "ship_mtrl_nature_id", Unique = true, Insert = false, Update = false)]
        public virtual ShipMtrlNatureEntity ShipMtrlNature { get; set; }

        /// <summary>
        /// 船舶装载物料类型
        /// </summary>
        [ManyToOne(Name = "LoadMtrlType", ClassType = typeof(LoadMtrlTypeEntity), Lazy = Laziness.Proxy,
             Column = "load_mtrl_type_id", Unique = true, Insert = false, Update = false)]
        public virtual LoadMtrlTypeEntity LoadMtrlType { get; set; }

        #endregion
    }
}

