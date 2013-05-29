using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Migrations
{
    using System;
    using System.Collections.Immutable;
    using System.Linq.Expressions;
    using System.Reflection;

    public class Table<T>
    {
        private readonly ImmutableDictionary<string, ColumnSpec> _columns;
        private ColumnSpec _keyColumn;

        public Table()
        {
            _columns = CreateColumns();
        }

        public IEnumerable<ColumnSpec> Columns
        {
            get { return _columns.Values; }
        }

        private static ImmutableDictionary<string, ColumnSpec> CreateColumns()
        {
            return typeof (T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                             .Select(p => new ColumnSpec(p.Name, p.PropertyType))
                             .ToImmutableDictionary(c => c.Name, c => c);
        }

        public Table<T> WithPrimaryKey(Expression<Func<T, int>> key, bool isIdentity = false)
        {
            var member = (MemberExpression)key.Body;
            _keyColumn = Columns.Single(c => c.Name == member.Member.Name);
            _keyColumn.IsIdentity = isIdentity;
            return this;
        }

        public ColumnSpec KeyColumn
        {
            get { return _keyColumn; }
        }

        public IDictionary<string, ColumnSpec> GetColumnDictionary()
        {
            return Columns.ToDictionary(c => c.Name);
        }

        private void Customize(ColumnSpec column, bool nullable)
        {
            column.IsNullable = nullable;
        }

        public Table<T> Customize<TKey>(Expression<Func<T, TKey>> property, bool nullable = false)
        {
            Customize(_columns[property.GetMemberName()], nullable);
            return this;
        }

        public Table<T> Customize(Expression<Func<T, string>> property, int length, bool nullable = false)
        {
            var column = _columns[property.GetMemberName()];
            column.Length = length;
            Customize(column, nullable);
            return this;
        }
    }

    static class ExpressionHelper
    {
        public static string GetMemberName<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        {
            return ((MemberExpression)expression.Body).Member.Name;
        }
    }
}
