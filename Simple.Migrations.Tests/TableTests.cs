using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Migrations.Tests
{
    using System.Data;
    using TestTypes;
    using Xunit;

    public class TableTests
    {
        [Fact]
        public void CreatesTableWithColumnsForProperties()
        {
            var actual = Table.For<Character>();
            Assert.True(actual.Columns.Any(c => c.Name.Equals("Id") && c.ClrType == typeof(int)));
            Assert.True(actual.Columns.Any(c => c.Name.Equals("Name") && c.ClrType == typeof(string)));
        }

        [Fact]
        public void AssignsCorrectDbTypesForProperties()
        {
            var actual = Table.For<Character>();
            var columns = actual.GetColumnDictionary();
            Assert.Equal(DbType.Int32, columns["Id"].DbType);
            Assert.Equal(DbType.String, columns["Name"].DbType);
        }

        [Fact]
        public void CanSpecifyKeyColumn()
        {
            var actual = Table.For<Character>()
                              .WithPrimaryKey(c => c.Id);
            Assert.Equal("Id", actual.KeyColumn.Name);
        }

        [Fact]
        public void CanSpecifyKeyColumnAsIdentity()
        {
            var actual = Table.For<Character>()
                              .WithPrimaryKey(c => c.Id, true);
            Assert.True(actual.KeyColumn.IsIdentity);
        }

        [Fact]
        public void NonNullableClrTypeGivesNonNullableColumn()
        {
            var actual = Table.For<Character>();
            Assert.False(actual.GetColumnDictionary()["Id"].IsNullable);
        }

        [Fact]
        public void ReferenceClrTypeGivesNonNullableColumn()
        {
            var actual = Table.For<Character>();
            Assert.False(actual.GetColumnDictionary()["Name"].IsNullable);
        }

        [Fact]
        public void CanOverrideNullableForNonNullableClrType()
        {
            var actual = Table.For<Character>()
                              .Customize(c => c.Id, nullable: true);
            Assert.True(actual.GetColumnDictionary()["Id"].IsNullable);
        }

        [Fact]
        public void CanSetLengthForStringColumn()
        {
            var actual = Table.For<Character>()
                              .Customize(c => c.Name, 100);
            Assert.Equal(100, actual.GetColumnDictionary()["Name"].Length);
        }

        [Fact]
        public void StringLengthIsSetToCustomDefault()
        {
            Defaults.StringLength = 64;
            var actual = Table.For<Character>();
            Assert.Equal(64, actual.GetColumnDictionary()["Name"].Length);
        }
    }
}
