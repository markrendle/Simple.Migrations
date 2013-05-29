namespace Simple.Migrations
{
    using System;
    using System.Data;

    public class ColumnSpec
    {
        private readonly string _name;
        private readonly Type _clrType;
        private DbType? _dbType;
        private bool? _isNullable;
        private int _length = Defaults.StringLength;

        public ColumnSpec(string name, Type clrType) : this(name, clrType, null)
        {
        }

        public ColumnSpec(string name, Type clrType, DbType? dbType)
        {
            _name = name;
            _clrType = clrType;
            _dbType = dbType;
        }

        public DbType? DbType
        {
            get { return _dbType ?? _clrType.ToDbType(); }
        }

        public Type ClrType
        {
            get { return _clrType; }
        }

        public string Name
        {
            get { return _name; }
        }

        public bool IsIdentity { get; set; }

        public bool IsNullable
        {
            get { return _isNullable ?? _clrType.IsNullableValueType(); }
            set { _isNullable = value; }
        }

        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }
    }
}