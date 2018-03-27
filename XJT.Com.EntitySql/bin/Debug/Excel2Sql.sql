------------------------------------------------  工厂  T_PM_Factory  ------------------------------------------------
CREATE SEQUENCE S_PM_Factory
MINVALUE 1
MAXVALUE 9999999999999999999999999999
START WITH 1
INCREMENT BY 1
NOCACHE;

create table T_PM_Factory
(
  factory_id            NUMBER  not null  ,
  name                  VARCHAR2(200)  not null  ,
  sname                 VARCHAR2(200)  not null  ,
  std_code              VARCHAR2(200)  not null  ,
  in_use                NUMBER default 1 not null  ,
  crt_date              DATE default SYSDATE not null  ,
  mnt_date              DATE default SYSDATE not null  ,
  crt_user_id           VARCHAR2(200)  not null  ,
  mnt_user_id           VARCHAR2(200)  not null  ,
  crt_user_name         VARCHAR2(200)  not null  ,
  mnt_user_name         VARCHAR2(200)  not null  ,
  sort_num              NUMBER default 0 not null  ,
  des                   VARCHAR2(2000)    
);

comment on table T_PM_Factory
  is '工厂'; 
comment on column T_PM_Factory.factory_id
  is '工厂ID';
comment on column T_PM_Factory.name
  is '名称';
comment on column T_PM_Factory.sname
  is '简称';
comment on column T_PM_Factory.std_code
  is '标准编码';
comment on column T_PM_Factory.in_use
  is '是否启用（1是；0否）';
comment on column T_PM_Factory.crt_date
  is '创建时间';
comment on column T_PM_Factory.mnt_date
  is '维护时间';
comment on column T_PM_Factory.crt_user_id
  is '创建人ID';
comment on column T_PM_Factory.mnt_user_id
  is '最后维护人ID';
comment on column T_PM_Factory.crt_user_name
  is '创建人名称';
comment on column T_PM_Factory.mnt_user_name
  is '最后维护人名称';
comment on column T_PM_Factory.sort_num
  is '排序';
comment on column T_PM_Factory.des
  is '描述';

alter table T_PM_Factory
  add constraint PK_PM_Factory primary key (Factory_Id)
  using index;

alter table T_PM_Factory
  add constraint UK_PM_Factory_NSSC unique (Name,Sname,Std_Code)
  using index;

------------------------------------------------  车间  T_PM_Workshop  ------------------------------------------------
CREATE SEQUENCE S_PM_Workshop
MINVALUE 1
MAXVALUE 9999999999999999999999999999
START WITH 1
INCREMENT BY 1
NOCACHE;

create table T_PM_Workshop
(
  workshop_id           NUMBER  not null  ,
  factory_id            NUMBER  not null  ,
  name                  VARCHAR2(200)  not null  ,
  sname                 VARCHAR2(200)  not null  ,
  std_code              VARCHAR2(200)  not null  ,
  in_use                NUMBER default 1 not null  ,
  crt_date              DATE default SYSDATE not null  ,
  mnt_date              DATE default SYSDATE not null  ,
  crt_user_id           VARCHAR2(200)  not null  ,
  mnt_user_id           VARCHAR2(200)  not null  ,
  crt_user_name         VARCHAR2(200)  not null  ,
  mnt_user_name         VARCHAR2(200)  not null  ,
  sort_num              NUMBER default 0 not null  ,
  des                   VARCHAR2(2000)    
);

comment on table T_PM_Workshop
  is '车间'; 
comment on column T_PM_Workshop.workshop_id
  is '车间ID';
comment on column T_PM_Workshop.factory_id
  is '工厂ID';
comment on column T_PM_Workshop.name
  is '名称';
comment on column T_PM_Workshop.sname
  is '简称';
comment on column T_PM_Workshop.std_code
  is '标准编码';
comment on column T_PM_Workshop.in_use
  is '是否启用（1是；0否）';
comment on column T_PM_Workshop.crt_date
  is '创建时间';
comment on column T_PM_Workshop.mnt_date
  is '维护时间';
comment on column T_PM_Workshop.crt_user_id
  is '创建人ID';
comment on column T_PM_Workshop.mnt_user_id
  is '最后维护人ID';
comment on column T_PM_Workshop.crt_user_name
  is '创建人名称';
comment on column T_PM_Workshop.mnt_user_name
  is '最后维护人名称';
comment on column T_PM_Workshop.sort_num
  is '排序';
comment on column T_PM_Workshop.des
  is '描述';

alter table T_PM_Workshop
  add constraint PK_PM_Workshop primary key (Workshop_Id)
  using index;

alter table T_PM_Workshop
  add constraint UK_PM_Workshop_NSSC unique (Name,Sname,Std_Code)
  using index;

alter table T_PM_Workshop
  add constraint FK_PM_Workshop_FI foreign key (Factory_Id)
  references T_PM_Factory (Factory_Id); 
------------------------------------------------  装置  T_PM_Unit  ------------------------------------------------
CREATE SEQUENCE S_PM_Unit
MINVALUE 1
MAXVALUE 9999999999999999999999999999
START WITH 1
INCREMENT BY 1
NOCACHE;

create table T_PM_Unit
(
  unit_id               NUMBER  not null  ,
  workshop_id           NUMBER  not null  ,
  name                  VARCHAR2(200)  not null  ,
  sname                 VARCHAR2(200)  not null  ,
  std_code              VARCHAR2(200)  not null  ,
  in_use                NUMBER default 1 not null  ,
  crt_date              DATE default SYSDATE not null  ,
  mnt_date              DATE default SYSDATE not null  ,
  crt_user_id           VARCHAR2(200)  not null  ,
  mnt_user_id           VARCHAR2(200)  not null  ,
  crt_user_name         VARCHAR2(200)  not null  ,
  mnt_user_name         VARCHAR2(200)  not null  ,
  sort_num              NUMBER default 0 not null  ,
  des                   VARCHAR2(2000)    
);

comment on table T_PM_Unit
  is '装置'; 
comment on column T_PM_Unit.unit_id
  is '装置ID';
comment on column T_PM_Unit.workshop_id
  is '车间ID';
comment on column T_PM_Unit.name
  is '名称';
comment on column T_PM_Unit.sname
  is '简称';
comment on column T_PM_Unit.std_code
  is '标准编码';
comment on column T_PM_Unit.in_use
  is '是否启用（1是；0否）';
comment on column T_PM_Unit.crt_date
  is '创建时间';
comment on column T_PM_Unit.mnt_date
  is '维护时间';
comment on column T_PM_Unit.crt_user_id
  is '创建人ID';
comment on column T_PM_Unit.mnt_user_id
  is '最后维护人ID';
comment on column T_PM_Unit.crt_user_name
  is '创建人名称';
comment on column T_PM_Unit.mnt_user_name
  is '最后维护人名称';
comment on column T_PM_Unit.sort_num
  is '排序';
comment on column T_PM_Unit.des
  is '描述';

alter table T_PM_Unit
  add constraint PK_PM_Unit primary key (Unit_Id)
  using index;

alter table T_PM_Unit
  add constraint UK_PM_Unit_NSSC unique (Name,Sname,Std_Code)
  using index;

alter table T_PM_Unit
  add constraint FK_PM_Unit_WI foreign key (Workshop_Id)
  references T_PM_Workshop (Workshop_Id); 
------------------------------------------------  生产单元  T_PM_PrdtCell  ------------------------------------------------
CREATE SEQUENCE S_PM_PrdtCell
MINVALUE 1
MAXVALUE 9999999999999999999999999999
START WITH 1
INCREMENT BY 1
NOCACHE;

create table T_PM_PrdtCell
(
  prdtcell_id           NUMBER  not null  ,
  unit_id               NUMBER  not null  ,
  name                  VARCHAR2(200)  not null  ,
  sname                 VARCHAR2(200)  not null  ,
  in_use                NUMBER default 1 not null  ,
  crt_date              DATE default SYSDATE not null  ,
  mnt_date              DATE default SYSDATE not null  ,
  crt_user_id           VARCHAR2(200)  not null  ,
  mnt_user_id           VARCHAR2(200)  not null  ,
  crt_user_name         VARCHAR2(200)  not null  ,
  mnt_user_name         VARCHAR2(200)  not null  ,
  sort_num              NUMBER default 0 not null  ,
  des                   VARCHAR2(2000)    
);

comment on table T_PM_PrdtCell
  is '生产单元'; 
comment on column T_PM_PrdtCell.prdtcell_id
  is '生产单元ID';
comment on column T_PM_PrdtCell.unit_id
  is '装置ID';
comment on column T_PM_PrdtCell.name
  is '名称';
comment on column T_PM_PrdtCell.sname
  is '简称';
comment on column T_PM_PrdtCell.in_use
  is '是否启用（1是；0否）';
comment on column T_PM_PrdtCell.crt_date
  is '创建时间';
comment on column T_PM_PrdtCell.mnt_date
  is '维护时间';
comment on column T_PM_PrdtCell.crt_user_id
  is '创建人ID';
comment on column T_PM_PrdtCell.mnt_user_id
  is '最后维护人ID';
comment on column T_PM_PrdtCell.crt_user_name
  is '创建人名称';
comment on column T_PM_PrdtCell.mnt_user_name
  is '最后维护人名称';
comment on column T_PM_PrdtCell.sort_num
  is '排序';
comment on column T_PM_PrdtCell.des
  is '描述';

alter table T_PM_PrdtCell
  add constraint PK_PM_PrdtCell primary key (PrdtCell_Id)
  using index;

alter table T_PM_PrdtCell
  add constraint UK_PM_PrdtCell_NS unique (Name,Sname)
  using index;

alter table T_PM_PrdtCell
  add constraint FK_PM_PrdtCell_UI foreign key (Unit_Id)
  references T_PM_Unit (Unit_Id); 
------------------------------------------------  计量单位  T_PM_MeasUnit  ------------------------------------------------
CREATE SEQUENCE S_PM_MeasUnit
MINVALUE 1
MAXVALUE 9999999999999999999999999999
START WITH 1
INCREMENT BY 1
NOCACHE;

create table T_PM_MeasUnit
(
  measunit_id           NUMBER  not null  ,
  name                  NVARCHAR2(200)  not null  ,
  sign                  NVARCHAR2(200)  not null  ,
  in_use                NUMBER default 1 not null  ,
  crt_date              DATE default SYSDATE not null  ,
  mnt_date              DATE default SYSDATE not null  ,
  crt_user_id           VARCHAR2(200)  not null  ,
  mnt_user_id           VARCHAR2(200)  not null  ,
  crt_user_name         VARCHAR2(200)  not null  ,
  mnt_user_name         VARCHAR2(200)  not null  ,
  sort_num              NUMBER default 0 not null  ,
  des                   VARCHAR2(2000)    
);

comment on table T_PM_MeasUnit
  is '计量单位'; 
comment on column T_PM_MeasUnit.measunit_id
  is '计量单位ID';
comment on column T_PM_MeasUnit.name
  is '名称';
comment on column T_PM_MeasUnit.sign
  is '符号';
comment on column T_PM_MeasUnit.in_use
  is '是否启用（1是；0否）';
comment on column T_PM_MeasUnit.crt_date
  is '创建时间';
comment on column T_PM_MeasUnit.mnt_date
  is '维护时间';
comment on column T_PM_MeasUnit.crt_user_id
  is '创建人ID';
comment on column T_PM_MeasUnit.mnt_user_id
  is '最后维护人ID';
comment on column T_PM_MeasUnit.crt_user_name
  is '创建人名称';
comment on column T_PM_MeasUnit.mnt_user_name
  is '最后维护人名称';
comment on column T_PM_MeasUnit.sort_num
  is '排序';
comment on column T_PM_MeasUnit.des
  is '描述';

alter table T_PM_MeasUnit
  add constraint PK_PM_MeasUnit primary key (MeasUnit_Id)
  using index;

alter table T_PM_MeasUnit
  add constraint UK_PM_MeasUnit_NS unique (Name,Sign)
  using index;

------------------------------------------------  报警点类型  T_PM_AlarmPointType  ------------------------------------------------
CREATE SEQUENCE S_PM_AlarmPointType
MINVALUE 1
MAXVALUE 9999999999999999999999999999
START WITH 1
INCREMENT BY 1
NOCACHE;

create table T_PM_AlarmPointType
(
  alarm_point_type_id   NUMBER  not null  ,
  name                  VARCHAR2(200)  not null  ,
  in_use                NUMBER default 1 not null  ,
  sort_num              NUMBER default 0 not null  ,
  des                   VARCHAR2(2000)    
);

comment on table T_PM_AlarmPointType
  is '报警点类型'; 
comment on column T_PM_AlarmPointType.alarm_point_type_id
  is '报警点类型ID';
comment on column T_PM_AlarmPointType.name
  is '名称';
comment on column T_PM_AlarmPointType.in_use
  is '是否启用（1是；0否）';
comment on column T_PM_AlarmPointType.sort_num
  is '排序';
comment on column T_PM_AlarmPointType.des
  is '描述';

alter table T_PM_AlarmPointType
  add constraint PK_PM_AlarmPointType primary key (Alarm_Point_Type_Id)
  using index;

alter table T_PM_AlarmPointType
  add constraint UK_PM_AlarmPointType_Name unique (Name)
  using index;

------------------------------------------------  报警点  T_PM_AlarmPoint  ------------------------------------------------
CREATE SEQUENCE S_PM_AlarmPoint
MINVALUE 1
MAXVALUE 9999999999999999999999999999
START WITH 1
INCREMENT BY 1
NOCACHE;

create table T_PM_AlarmPoint
(
  alarm_point_id        NUMBER  not null  ,
  prdtcell_id           NUMBER  not null  ,
  tag                   VARCHAR2(200)  not null  ,
  location              VARCHAR2(200)  not null  ,
  pid_code              VARCHAR2(200)    ,
  alarm_point_type_id   NUMBER  not null  ,
  monitor_type          NUMBER  not null  ,
  measunit_id           NUMBER  not null  ,
  instrmt_type          NUMBER  not null  ,
  virtual_reality_flag  NUMBER  not null  ,
  alarm_point_hh        NUMBER    ,
  alarm_point_hi        NUMBER    ,
  alarm_point_lo        NUMBER    ,
  alarm_point_ll        NUMBER    ,
  in_use                NUMBER default 1 not null  ,
  crt_date              DATE default SYSDATE not null  ,
  mnt_date              DATE default SYSDATE not null  ,
  crt_user_id           VARCHAR2(200)  not null  ,
  mnt_user_id           VARCHAR2(200)  not null  ,
  crt_user_name         VARCHAR2(200)  not null  ,
  mnt_user_name         VARCHAR2(200)  not null  ,
  sort_num              NUMBER default 0 not null  ,
  des                   VARCHAR2(2000)    
);

comment on table T_PM_AlarmPoint
  is '报警点'; 
comment on column T_PM_AlarmPoint.alarm_point_id
  is '报警点ID';
comment on column T_PM_AlarmPoint.prdtcell_id
  is '生产单元ID';
comment on column T_PM_AlarmPoint.tag
  is '位号';
comment on column T_PM_AlarmPoint.location
  is '位置';
comment on column T_PM_AlarmPoint.pid_code
  is 'PID图号';
comment on column T_PM_AlarmPoint.alarm_point_type_id
  is '报警点类型ID';
comment on column T_PM_AlarmPoint.monitor_type
  is '监测类型（1物料；2能源；3质量）';
comment on column T_PM_AlarmPoint.measunit_id
  is '计量单位ID';
comment on column T_PM_AlarmPoint.instrmt_type
  is '仪表类型（1监测表；2控制表）';
comment on column T_PM_AlarmPoint.virtual_reality_flag
  is '虚实标记（0实表(按读数)；1虚表(按用量)）';
comment on column T_PM_AlarmPoint.alarm_point_hh
  is '报警点高高报';
comment on column T_PM_AlarmPoint.alarm_point_hi
  is '报警点高报';
comment on column T_PM_AlarmPoint.alarm_point_lo
  is '报警点低报';
comment on column T_PM_AlarmPoint.alarm_point_ll
  is '报警点低低报';
comment on column T_PM_AlarmPoint.in_use
  is '是否启用（1是；0否）';
comment on column T_PM_AlarmPoint.crt_date
  is '创建时间';
comment on column T_PM_AlarmPoint.mnt_date
  is '维护时间';
comment on column T_PM_AlarmPoint.crt_user_id
  is '创建人ID';
comment on column T_PM_AlarmPoint.mnt_user_id
  is '最后维护人ID';
comment on column T_PM_AlarmPoint.crt_user_name
  is '创建人名称';
comment on column T_PM_AlarmPoint.mnt_user_name
  is '最后维护人名称';
comment on column T_PM_AlarmPoint.sort_num
  is '排序';
comment on column T_PM_AlarmPoint.des
  is '描述';

alter table T_PM_AlarmPoint
  add constraint PK_PM_AlarmPoint primary key (Alarm_Point_Id)
  using index;

alter table T_PM_AlarmPoint
  add constraint UK_PM_AlarmPoint_Tag unique (Tag)
  using index;

alter table T_PM_AlarmPoint
  add constraint FK_PM_AlarmPoint_PI foreign key (PrdtCell_Id)
  references T_PM_PrdtCell (PrdtCell_Id); 
alter table T_PM_AlarmPoint
  add constraint FK_PM_AlarmPoint_APTI foreign key (Alarm_Point_Type_Id)
  references T_PM_AlarmPointType (Alarm_Point_Type_Id); 
alter table T_PM_AlarmPoint
  add constraint FK_PM_AlarmPoint_MI foreign key (MeasUnit_Id)
  references T_PM_MeasUnit (MeasUnit_Id); 
------------------------------------------------  事件类型  T_PM_EventType  ------------------------------------------------
CREATE SEQUENCE S_PM_EventType
MINVALUE 1
MAXVALUE 9999999999999999999999999999
START WITH 1
INCREMENT BY 1
NOCACHE;

create table T_PM_EventType
(
  event_type_id         NUMBER  not null  ,
  name                  VARCHAR2(200)  not null  ,
  parent_id             NUMBER    ,
  in_use                NUMBER default 1 not null  ,
  sort_num              NUMBER default 0 not null  
);

comment on table T_PM_EventType
  is '事件类型'; 
comment on column T_PM_EventType.event_type_id
  is '事件类型ID';
comment on column T_PM_EventType.name
  is '名称';
comment on column T_PM_EventType.parent_id
  is '上级ID';
comment on column T_PM_EventType.in_use
  is '是否启用（1是；0否）';
comment on column T_PM_EventType.sort_num
  is '排序';

alter table T_PM_EventType
  add constraint PK_PM_EventType primary key (Event_Type_Id)
  using index;

alter table T_PM_EventType
  add constraint UK_PM_EventType_Name unique (Name)
  using index;

alter table T_PM_EventType
  add constraint FK_PM_EventType_PI foreign key (Parent_Id)
  references T_PM_EventType (Event_Type_Id); 

