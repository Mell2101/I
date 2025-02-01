using System;
using FluentMigrator;

namespace I.Migrations.Y2025;

[Migration(202501021653)]
public class Migration202501021653 : Migration
{
    public override void Up()
    {
        Create.Table("core_area")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("create_date").AsDateTimeOffset().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("update_date").AsDateTimeOffset().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("area_id").AsString().Unique()
            .WithColumn("name").AsString().Nullable();

        Create.Table("core_employer")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("create_date").AsDateTimeOffset().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("update_date").AsDateTimeOffset().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("employer_id").AsString().Unique()
            .WithColumn("url").AsString().Nullable()
            .WithColumn("name").AsString().Nullable()
            .WithColumn("logo_url").AsString().Nullable();

        Create.Table("core_salary")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("create_date").AsDateTimeOffset().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("update_date").AsDateTimeOffset().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("from_selary").AsInt64().Nullable()
            .WithColumn("to_selary").AsInt64().Nullable()
            .WithColumn("currency").AsString().Nullable();
    }
    public override void Down()
    {
        Delete.Table("core_area");
        Delete.Table("core_salary");
        Delete.Table("core_employer");
    }

}
