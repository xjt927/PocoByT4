/**************************************************************************************************
 * 作    者：00好1                     创始时间：2017-08-12 16:03:32                              *
 * 修 改 人：                          修改时间：                                                 *
 * 描    述：实体                                                                                 *
 **************************************************************************************************/

using NHibernate.Mapping.Attributes;
using PCITC.MES.EP.Entity.AAAEntity;

namespace PCITC.MES.PM.Entity.V_PM_ACCU_TREE
{

    /// <summary>
    /// 实体
    /// </summary>
    /// <remarks>
    /// 模块编号：pcitc_ep_entity_class_ACCU_TREE
    /// 作    者：00好1
    /// 创建时间：2017-08-12 16:03:32
    /// 修改编号：1
    /// 描    述：实体
    /// </remarks>
    [Class(Table = "accu_tree", OptimisticLock = OptimisticLockMode.Version, DynamicUpdate = true)]
    [Cache(Usage = CacheUsage.ReadWrite)]
    public class ACCU_TREE : BasicInfoEntity
    {
        #region Model

		/// <summary>
		/// 
		/// </summary>
        [Id(0, TypeType = typeof(decimal), Name = "AccuId", UnsavedValue = "0")]
        [Column(1, Name = "accu_id", NotNull = true, SqlType = "number")]
        public virtual decimal AccuId { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "accu_type")]
        public virtual decimal AccuType { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "company_id")]
        public virtual decimal CompanyId { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "dept_id")]
        public virtual decimal DeptId { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "name")]
        public virtual string Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "sname")]
        public virtual string Sname { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "parent_id")]
        public virtual decimal ParentId { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "in_use")]
        public virtual decimal InUse { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "crt_date")]
        public virtual DateTime CrtDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "mnt_date")]
        public virtual DateTime MntDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "crt_user_id")]
        public virtual string CrtUserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "mnt_user_id")]
        public virtual string MntUserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "crt_user_name")]
        public virtual string CrtUserName { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "mnt_user_name")]
        public virtual string MntUserName { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "sort_num")]
        public virtual decimal SortNum { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "des")]
        public virtual string Des { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "in_apro")]
        public virtual decimal InApro { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "root_id")]
        public virtual decimal RootId { get; set; }

		/// <summary>
		/// 
		/// </summary>
        [Property(Column = "org_level")]
        public virtual decimal OrgLevel { get; set; }

		#endregion
    }
}
