using FluentMigrator;
using Migrations.Extensions;

namespace Migrations.Migrations;

[Migration(202604180002, "Create Users table")]
public class _202604180002_CreateUsers : Migration
{
    public override void Up()
    {
        Create.Table("Users")
            .WithId()
            .WithColumn("Status").AsInt32().NotNullable().WithDefaultValue(1)
            .WithColumn("Username").AsString(50).NotNullable()
            .WithColumn("Email").AsString(75).NotNullable()
            .WithColumn("PasswordHash").AsString(200).NotNullable()
            .WithTrackingColumns();

        Create.Index("IX_Users_Username").OnTable("Users").OnColumn("Username").Unique();
    }

    public override void Down()
    {
        Delete.Table("Users");
    }
}
