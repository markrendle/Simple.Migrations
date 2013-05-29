namespace Simple.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Xml.Linq;

    public static class TypeHelper
    {
        private static readonly HashSet<Type> BaseTypes = new HashSet<Type>
                                                              {
                                                                  typeof (bool),
                                                                  typeof (char),
                                                                  typeof (sbyte),
                                                                  typeof (byte),
                                                                  typeof (short),
                                                                  typeof (ushort),
                                                                  typeof (int),
                                                                  typeof (uint),
                                                                  typeof (long),
                                                                  typeof (ulong),
                                                                  typeof (float),
                                                                  typeof (double),
                                                                  typeof (decimal),
                                                                  typeof (DateTime),
                                                                  typeof (DateTimeOffset),
                                                                  typeof (string),
                                                                  typeof (byte[]),
                                                                  typeof (Guid),
                                                              };

        private static readonly Dictionary<DbType, Type> DbTypeToClrTypeMap =
            new Dictionary<DbType, Type>
                {
                    {DbType.Int16, typeof (short)},
                    {DbType.Int32, typeof (int)},
                    {DbType.Double, typeof (double)},
                    {DbType.Guid, typeof (Guid)},
                    {DbType.SByte, typeof (sbyte)},
                    {DbType.Single, typeof (Single)},
                    {DbType.Int64, typeof (long)},
                    {DbType.Object, typeof (object)},
                    {DbType.Byte, typeof (byte)},
                    {DbType.Boolean, typeof (bool)},
                    {DbType.AnsiString, typeof (string)},
                    {DbType.Binary, typeof (byte[])},
                    {DbType.DateTime, typeof (DateTime)},
                    {DbType.Decimal, typeof (decimal)},
                    {DbType.Currency, typeof (decimal)},
                    {DbType.Date, typeof (DateTime)},
                    {DbType.StringFixedLength, typeof (string)},
                    {DbType.AnsiStringFixedLength, typeof (string)},
                    {DbType.Xml, typeof (string)},
                    {DbType.DateTime2, typeof (DateTime)},
                    {DbType.VarNumeric, typeof (double)},
                    {DbType.UInt16, typeof (ushort)},
                    {DbType.String, typeof (string)},
                    {DbType.Time, typeof (TimeSpan)},
                    {DbType.UInt64, typeof (ulong)},
                    {DbType.UInt32, typeof (uint)},
                    {DbType.DateTimeOffset, typeof (DateTimeOffset)},
                };

        private static readonly IDictionary<Type, DbType> ClrTypeToDbTypeMap =
            new Dictionary<Type, DbType>
                {
                    {typeof (short), DbType.Int16},
                    {typeof (int), DbType.Int32},
                    {typeof (double), DbType.Double},
                    {typeof (Guid), DbType.Guid},
                    {typeof (sbyte), DbType.SByte},
                    {typeof (Single), DbType.Single},
                    {typeof (long), DbType.Int64},
                    {typeof (object), DbType.Object},
                    {typeof (byte), DbType.Byte},
                    {typeof (bool), DbType.Boolean},
                    {typeof (byte[]), DbType.Binary},
                    {typeof (DateTime), DbType.DateTime},
                    {typeof (decimal), DbType.Decimal},
                    {typeof (XElement), DbType.Xml},
                    {typeof (ushort), DbType.UInt16},
                    {typeof (string), DbType.String},
                    {typeof (TimeSpan), DbType.Time},
                    {typeof (ulong), DbType.UInt64},
                    {typeof (uint), DbType.UInt32},
                    {typeof (DateTimeOffset), DbType.DateTimeOffset},
                };

        public static bool IsKnownType(Type type)
        {
            return BaseTypes.Contains(type);
        }

        public static Type ToClrType(this DbType dbType)
        {
            if (!DbTypeToClrTypeMap.ContainsKey(dbType)) return typeof(object);
            return DbTypeToClrTypeMap[dbType];
        }

        public static DbType ToDbType(this Type clrType)
        {
            DbType dbType;
            if (!ClrTypeToDbTypeMap.TryGetValue(clrType, out dbType))
            {
                dbType = DbType.Object;
            }
            return dbType;
        }

        public static bool IsNullableValueType(this Type clrType)
        {
            return typeof (Nullable<>).IsAssignableFrom(clrType);
        }
    }
}