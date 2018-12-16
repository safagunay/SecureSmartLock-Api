namespace LockerApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DeleteDateRemovedColumnFromPermissionsTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DevicePermissions", "DateRemoved");
        }

        public override void Down()
        {
            AddColumn("dbo.DevicePermissions", "DateRemoved", c => c.DateTime(storeType: "smalldatetime"));
        }
    }
}
