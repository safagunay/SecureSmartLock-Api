namespace LockerApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DevicePermissionRecordsTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DevicePermissionRecords",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Device_Id = c.Int(nullable: false),
                    Description = c.String(maxLength: 100),
                    User_Id = c.String(nullable: false, maxLength: 128),
                    Givenby_User_Id = c.String(nullable: false, maxLength: 128),
                    ExpirationDate = c.DateTime(storeType: "smalldatetime"),
                    CreationDate = c.DateTime(storeType: "smalldatetime", nullable: false),
                    DateRemoved = c.DateTime(storeType: "smalldatetime", nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.Device_Id)
                .Index(t => t.Device_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Givenby_User_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.DevicePermissionRecords", "Device_Id", "dbo.Devices");
            DropIndex("dbo.Devices", new[] { "Device_Id" });
            DropForeignKey("dbo.DevicePermissionRecords", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Devices", new[] { "User_Id" });
            DropForeignKey("dbo.DevicePermissionRecords", "Givenby_User_Id", "dbo.AspNetUsers");
            DropTable("dbo.DevicePermissionRecords");
        }
    }
}
