namespace LockerApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DevicesTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 50),
                    CodeHash = c.String(nullable: false, maxLength: 200),
                    Code = c.String(maxLength: 50),
                    SecretKeyHash = c.String(nullable: false, maxLength: 250),
                    User_Id = c.String(maxLength: 128),
                    IsDeleted = c.Boolean(nullable: false),
                    RegisteredOnUTC = c.DateTime(storeType: "smalldatetime"),
                    CreatedOnUTC = c.DateTime(storeType: "smalldatetime", nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id)
                .Index(t => t.CodeHash, unique: true)
                .Index(t => t.SecretKeyHash, unique: true);

        }

        public override void Down()
        {
            DropIndex("dbo.Devices", new[] { "SecretKeyHash" });
            DropIndex("dbo.Devices", new[] { "CodeHash" });
            DropForeignKey("dbo.Devices", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Devices", new[] { "User_Id" });
            DropTable("dbo.Devices");
        }
    }
}
