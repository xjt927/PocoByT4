namespace XJT.Com.EntitySql.ExcelSource
{
    class SqlTemplate
    {
        #region 脚本模板
        public string CreateTableSql = @"CREATE SEQUENCE {6}
MINVALUE 1
MAXVALUE 9999999999999999999999999999
START WITH 1
INCREMENT BY 1
NOCACHE;

create table {0}
(
{1}
);
{2}
{3}
{4}
{5}
";
        #endregion

        #region {0} 数据表名称
        public string TableName = "";
        #endregion

        #region {1}数据表字段属性。 {0}:字段名称，{1}:字段类型，{2}默认值，{3}:是否为空
        public string ColumnTemplate = @"  {0}  {1} {2} {3} ";
        #endregion

        #region {2}表和字段注释。 {0}：表名称注释+字段注释
        public string CommentTemplate = @"
{0}
";
        //表名称注释
        public string TableComment = @"
comment on table {0}
  is '{1}'; ";

        //字段注释
        public string ColumnComment = @"
comment on column {0}.{1}
  is '{2}';";
        #endregion

        #region {3}主键。 {0}：表名称，{1}：主键约束名称，{2}：主键
        public string PrimaryKeyTemplate = @"
alter table {0}
  add constraint {1} primary key ({2})
  using index;";
        #endregion

        #region {4}唯一约束。{0}：表名称，{1}：唯一约束表名称，{2}：唯一约束字段
        public string UniqueTemplate = @"
alter table {0}
  add constraint {1} unique ({2})
  using index;";
        #endregion

        #region {5}外键关联。{0}：当前表名称，{1}：外键约束名称，{2}：当前表关联外键字段，{3}：外键关联表名称，机构单元使用 IP.T_AAA_OU，{4}：外键关联字段
        public string ForeignKeyTemplate = @"
alter table {0}
  add constraint {1} foreign key ({2})
  references {3} ({4}); ";
        #endregion
    }
}
