﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LinqToObject
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="TiKuX2.0")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void Inserttb_CourseInfo(tb_CourseInfo instance);
    partial void Updatetb_CourseInfo(tb_CourseInfo instance);
    partial void Deletetb_CourseInfo(tb_CourseInfo instance);
    partial void Inserttb_Chapter(tb_Chapter instance);
    partial void Updatetb_Chapter(tb_Chapter instance);
    partial void Deletetb_Chapter(tb_Chapter instance);
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::LinqToObject.Properties.Settings.Default.TiKuX2_0ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<tb_CourseInfo> tb_CourseInfo
		{
			get
			{
				return this.GetTable<tb_CourseInfo>();
			}
		}
		
		public System.Data.Linq.Table<tb_Chapter> tb_Chapter
		{
			get
			{
				return this.GetTable<tb_Chapter>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="")]
	public partial class tb_CourseInfo : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _Names;
		
		private int _Orders;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnNamesChanging(string value);
    partial void OnNamesChanged();
    partial void OnOrdersChanging(int value);
    partial void OnOrdersChanged();
    #endregion
		
		public tb_CourseInfo()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", IsPrimaryKey=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Names", CanBeNull=false)]
		public string Names
		{
			get
			{
				return this._Names;
			}
			set
			{
				if ((this._Names != value))
				{
					this.OnNamesChanging(value);
					this.SendPropertyChanging();
					this._Names = value;
					this.SendPropertyChanged("Names");
					this.OnNamesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Orders")]
		public int Orders
		{
			get
			{
				return this._Orders;
			}
			set
			{
				if ((this._Orders != value))
				{
					this.OnOrdersChanging(value);
					this.SendPropertyChanging();
					this._Orders = value;
					this.SendPropertyChanged("Orders");
					this.OnOrdersChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tb_Chapter")]
	public partial class tb_Chapter : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _CptName;
		
		private System.Nullable<int> _CourseID;
		
		private System.Nullable<int> _ParentID;
		
		private string _ParentPath;
		
		private System.Nullable<int> _Level;
		
		private System.Nullable<int> _Quantity;
		
		private System.Nullable<int> _NoteNum;
		
		private System.Nullable<int> _Collection;
		
		private System.Nullable<int> _Orders;
		
		private System.Nullable<int> _NodeType;
		
		private string _DirCode;
		
		private System.Nullable<int> _IsOpen;
		
		private System.Nullable<bool> _IsExam;
		
		private string _Description;
		
		private string _State;
		
		private System.Nullable<int> _CreatedBy;
		
		private System.Nullable<System.DateTime> _CreatedOn;
		
		private System.Nullable<int> _ModifyBy;
		
		private System.Nullable<System.DateTime> _ModifyOn;
		
		private System.Nullable<int> _Del;
		
		private string _OriginalId;
		
		private string _SuperChapter;
		
		private System.Nullable<long> _fromid;
		
		private string _fromtype;
		
		private System.Nullable<byte> _IsTrial;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnCptNameChanging(string value);
    partial void OnCptNameChanged();
    partial void OnCourseIDChanging(System.Nullable<int> value);
    partial void OnCourseIDChanged();
    partial void OnParentIDChanging(System.Nullable<int> value);
    partial void OnParentIDChanged();
    partial void OnParentPathChanging(string value);
    partial void OnParentPathChanged();
    partial void OnLevelChanging(System.Nullable<int> value);
    partial void OnLevelChanged();
    partial void OnQuantityChanging(System.Nullable<int> value);
    partial void OnQuantityChanged();
    partial void OnNoteNumChanging(System.Nullable<int> value);
    partial void OnNoteNumChanged();
    partial void OnCollectionChanging(System.Nullable<int> value);
    partial void OnCollectionChanged();
    partial void OnOrdersChanging(System.Nullable<int> value);
    partial void OnOrdersChanged();
    partial void OnNodeTypeChanging(System.Nullable<int> value);
    partial void OnNodeTypeChanged();
    partial void OnDirCodeChanging(string value);
    partial void OnDirCodeChanged();
    partial void OnIsOpenChanging(System.Nullable<int> value);
    partial void OnIsOpenChanged();
    partial void OnIsExamChanging(System.Nullable<bool> value);
    partial void OnIsExamChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnStateChanging(string value);
    partial void OnStateChanged();
    partial void OnCreatedByChanging(System.Nullable<int> value);
    partial void OnCreatedByChanged();
    partial void OnCreatedOnChanging(System.Nullable<System.DateTime> value);
    partial void OnCreatedOnChanged();
    partial void OnModifyByChanging(System.Nullable<int> value);
    partial void OnModifyByChanged();
    partial void OnModifyOnChanging(System.Nullable<System.DateTime> value);
    partial void OnModifyOnChanged();
    partial void OnDelChanging(System.Nullable<int> value);
    partial void OnDelChanged();
    partial void OnOriginalIdChanging(string value);
    partial void OnOriginalIdChanged();
    partial void OnSuperChapterChanging(string value);
    partial void OnSuperChapterChanged();
    partial void OnfromidChanging(System.Nullable<long> value);
    partial void OnfromidChanged();
    partial void OnfromtypeChanging(string value);
    partial void OnfromtypeChanged();
    partial void OnIsTrialChanging(System.Nullable<byte> value);
    partial void OnIsTrialChanged();
    #endregion
		
		public tb_Chapter()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CptName", DbType="VarChar(100)")]
		public string CptName
		{
			get
			{
				return this._CptName;
			}
			set
			{
				if ((this._CptName != value))
				{
					this.OnCptNameChanging(value);
					this.SendPropertyChanging();
					this._CptName = value;
					this.SendPropertyChanged("CptName");
					this.OnCptNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CourseID", DbType="Int")]
		public System.Nullable<int> CourseID
		{
			get
			{
				return this._CourseID;
			}
			set
			{
				if ((this._CourseID != value))
				{
					this.OnCourseIDChanging(value);
					this.SendPropertyChanging();
					this._CourseID = value;
					this.SendPropertyChanged("CourseID");
					this.OnCourseIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParentID", DbType="Int")]
		public System.Nullable<int> ParentID
		{
			get
			{
				return this._ParentID;
			}
			set
			{
				if ((this._ParentID != value))
				{
					this.OnParentIDChanging(value);
					this.SendPropertyChanging();
					this._ParentID = value;
					this.SendPropertyChanged("ParentID");
					this.OnParentIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParentPath", DbType="VarChar(200)")]
		public string ParentPath
		{
			get
			{
				return this._ParentPath;
			}
			set
			{
				if ((this._ParentPath != value))
				{
					this.OnParentPathChanging(value);
					this.SendPropertyChanging();
					this._ParentPath = value;
					this.SendPropertyChanged("ParentPath");
					this.OnParentPathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Level]", Storage="_Level", DbType="Int")]
		public System.Nullable<int> Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				if ((this._Level != value))
				{
					this.OnLevelChanging(value);
					this.SendPropertyChanging();
					this._Level = value;
					this.SendPropertyChanged("Level");
					this.OnLevelChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Quantity", DbType="Int")]
		public System.Nullable<int> Quantity
		{
			get
			{
				return this._Quantity;
			}
			set
			{
				if ((this._Quantity != value))
				{
					this.OnQuantityChanging(value);
					this.SendPropertyChanging();
					this._Quantity = value;
					this.SendPropertyChanged("Quantity");
					this.OnQuantityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NoteNum", DbType="Int")]
		public System.Nullable<int> NoteNum
		{
			get
			{
				return this._NoteNum;
			}
			set
			{
				if ((this._NoteNum != value))
				{
					this.OnNoteNumChanging(value);
					this.SendPropertyChanging();
					this._NoteNum = value;
					this.SendPropertyChanged("NoteNum");
					this.OnNoteNumChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Collection", DbType="Int")]
		public System.Nullable<int> Collection
		{
			get
			{
				return this._Collection;
			}
			set
			{
				if ((this._Collection != value))
				{
					this.OnCollectionChanging(value);
					this.SendPropertyChanging();
					this._Collection = value;
					this.SendPropertyChanged("Collection");
					this.OnCollectionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Orders", DbType="Int")]
		public System.Nullable<int> Orders
		{
			get
			{
				return this._Orders;
			}
			set
			{
				if ((this._Orders != value))
				{
					this.OnOrdersChanging(value);
					this.SendPropertyChanging();
					this._Orders = value;
					this.SendPropertyChanged("Orders");
					this.OnOrdersChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NodeType", DbType="Int")]
		public System.Nullable<int> NodeType
		{
			get
			{
				return this._NodeType;
			}
			set
			{
				if ((this._NodeType != value))
				{
					this.OnNodeTypeChanging(value);
					this.SendPropertyChanging();
					this._NodeType = value;
					this.SendPropertyChanged("NodeType");
					this.OnNodeTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DirCode", DbType="VarChar(15)")]
		public string DirCode
		{
			get
			{
				return this._DirCode;
			}
			set
			{
				if ((this._DirCode != value))
				{
					this.OnDirCodeChanging(value);
					this.SendPropertyChanging();
					this._DirCode = value;
					this.SendPropertyChanged("DirCode");
					this.OnDirCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsOpen", DbType="Int")]
		public System.Nullable<int> IsOpen
		{
			get
			{
				return this._IsOpen;
			}
			set
			{
				if ((this._IsOpen != value))
				{
					this.OnIsOpenChanging(value);
					this.SendPropertyChanging();
					this._IsOpen = value;
					this.SendPropertyChanged("IsOpen");
					this.OnIsOpenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsExam", DbType="Bit")]
		public System.Nullable<bool> IsExam
		{
			get
			{
				return this._IsExam;
			}
			set
			{
				if ((this._IsExam != value))
				{
					this.OnIsExamChanging(value);
					this.SendPropertyChanging();
					this._IsExam = value;
					this.SendPropertyChanged("IsExam");
					this.OnIsExamChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(3000)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_State", DbType="NChar(10)")]
		public string State
		{
			get
			{
				return this._State;
			}
			set
			{
				if ((this._State != value))
				{
					this.OnStateChanging(value);
					this.SendPropertyChanging();
					this._State = value;
					this.SendPropertyChanged("State");
					this.OnStateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreatedBy", DbType="Int")]
		public System.Nullable<int> CreatedBy
		{
			get
			{
				return this._CreatedBy;
			}
			set
			{
				if ((this._CreatedBy != value))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._CreatedBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreatedOn", DbType="DateTime")]
		public System.Nullable<System.DateTime> CreatedOn
		{
			get
			{
				return this._CreatedOn;
			}
			set
			{
				if ((this._CreatedOn != value))
				{
					this.OnCreatedOnChanging(value);
					this.SendPropertyChanging();
					this._CreatedOn = value;
					this.SendPropertyChanged("CreatedOn");
					this.OnCreatedOnChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ModifyBy", DbType="Int")]
		public System.Nullable<int> ModifyBy
		{
			get
			{
				return this._ModifyBy;
			}
			set
			{
				if ((this._ModifyBy != value))
				{
					this.OnModifyByChanging(value);
					this.SendPropertyChanging();
					this._ModifyBy = value;
					this.SendPropertyChanged("ModifyBy");
					this.OnModifyByChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ModifyOn", DbType="DateTime")]
		public System.Nullable<System.DateTime> ModifyOn
		{
			get
			{
				return this._ModifyOn;
			}
			set
			{
				if ((this._ModifyOn != value))
				{
					this.OnModifyOnChanging(value);
					this.SendPropertyChanging();
					this._ModifyOn = value;
					this.SendPropertyChanged("ModifyOn");
					this.OnModifyOnChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Del", DbType="Int")]
		public System.Nullable<int> Del
		{
			get
			{
				return this._Del;
			}
			set
			{
				if ((this._Del != value))
				{
					this.OnDelChanging(value);
					this.SendPropertyChanging();
					this._Del = value;
					this.SendPropertyChanged("Del");
					this.OnDelChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OriginalId", DbType="NVarChar(64)")]
		public string OriginalId
		{
			get
			{
				return this._OriginalId;
			}
			set
			{
				if ((this._OriginalId != value))
				{
					this.OnOriginalIdChanging(value);
					this.SendPropertyChanging();
					this._OriginalId = value;
					this.SendPropertyChanged("OriginalId");
					this.OnOriginalIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SuperChapter", DbType="NVarChar(64)")]
		public string SuperChapter
		{
			get
			{
				return this._SuperChapter;
			}
			set
			{
				if ((this._SuperChapter != value))
				{
					this.OnSuperChapterChanging(value);
					this.SendPropertyChanging();
					this._SuperChapter = value;
					this.SendPropertyChanged("SuperChapter");
					this.OnSuperChapterChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_fromid", DbType="BigInt")]
		public System.Nullable<long> fromid
		{
			get
			{
				return this._fromid;
			}
			set
			{
				if ((this._fromid != value))
				{
					this.OnfromidChanging(value);
					this.SendPropertyChanging();
					this._fromid = value;
					this.SendPropertyChanged("fromid");
					this.OnfromidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_fromtype", DbType="NVarChar(30)")]
		public string fromtype
		{
			get
			{
				return this._fromtype;
			}
			set
			{
				if ((this._fromtype != value))
				{
					this.OnfromtypeChanging(value);
					this.SendPropertyChanging();
					this._fromtype = value;
					this.SendPropertyChanged("fromtype");
					this.OnfromtypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsTrial", DbType="TinyInt")]
		public System.Nullable<byte> IsTrial
		{
			get
			{
				return this._IsTrial;
			}
			set
			{
				if ((this._IsTrial != value))
				{
					this.OnIsTrialChanging(value);
					this.SendPropertyChanging();
					this._IsTrial = value;
					this.SendPropertyChanged("IsTrial");
					this.OnIsTrialChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591