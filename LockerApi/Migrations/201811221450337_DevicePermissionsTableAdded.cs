namespace LockerApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DevicePermissionsTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DevicePermissions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Device_Id = c.Int(nullable: false),
                    DeviceCode = c.String(nullable: false, maxLength: 50),
                    Name = c.String(maxLength: 50),
                    User_Id = c.String(nullable: false, maxLength: 128),
                    Givenby_User_Id = c.String(nullable: false, maxLength: 128),
                    ExpirationDate = c.DateTime(),
                    CreationDate = c.DateTime(nullable: false),
                    DateRemoved = c.DateTime(),
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
            DropForeignKey("dbo.DevicePermissions", "Device_Id", "dbo.Devices");
            DropIndex("dbo.Devices", new[] { "Device_Id" });
            DropForeignKey("dbo.DevicePermissions", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Devices", new[] { "User_Id" });
            DropForeignKey("dbo.DevicePermissions", "Givenby_User_Id", "dbo.AspNetUsers");
            DropTable("dbo.DevicePermissions");
        }
    }
}
