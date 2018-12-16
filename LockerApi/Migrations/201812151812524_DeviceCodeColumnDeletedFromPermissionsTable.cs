namespace LockerApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DeviceCodeColumnDeletedFromPermissionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DevicePermissions", "Description", c => c.String(maxLength: 100));
            DropColumn("dbo.DevicePermissions", "DeviceCode");
            DropColumn("dbo.DevicePermissions", "Name");
            CreateIndex("dbo.Devices", "Code");
        }

        public override void Down()
        {
            AddColumn("dbo.DevicePermissions", "Name", c => c.String(maxLength: 50));
            AddColumn("dbo.DevicePermissions", "DeviceCode", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.DevicePermissions", "Description");
            DropIndex("dbo.Devices", "Code");
        }
    }
}
