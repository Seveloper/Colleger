using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace Migrations.Extensions;

public static class CreateTableExtensions
{
    /// <summary>Adds the standard <c>Id</c> primary key (Int32, identity).</summary>
    public static ICreateTableColumnOptionOrWithColumnSyntax WithId(
        this ICreateTableWithColumnSyntax table)
        => table.WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity();

    /// <summary>
    /// Adds audit columns: <c>CreatedBy</c>, <c>CreatedDate</c>, <c>UpdatedBy</c>, <c>UpdatedDate</c>.
    /// User ids are stored without a foreign key for efficiency.
    /// </summary>
    public static ICreateTableColumnOptionOrWithColumnSyntax WithTrackingColumns(
        this ICreateTableColumnOptionOrWithColumnSyntax table)
        => table
            .WithColumn("CreatedBy").AsInt32().NotNullable()
            .WithColumn("CreatedDate").AsDateTime2().NotNullable()
                .WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("UpdatedBy").AsInt32().Nullable()
            .WithColumn("UpdatedDate").AsDateTime2().Nullable();
}
