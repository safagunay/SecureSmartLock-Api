namespace LockerApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class QRCodesTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QRCodes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    User_Id = c.String(nullable: false, maxLength: 128),
                    Hash = c.String(nullable: false, maxLength: 500),
                    CreatedOnUTC = c.DateTime(storeType: "smalldatetime", nullable: false),
                    ExpiresOnUTC = c.DateTime(storeType: "smalldatetime"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Hash, unique: true);

        }

        public override void Down()
        {
            DropForeignKey("dbo.QRCodes", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.QRCodes", new[] { "User_Id" });
            DropIndex("dbo.QRCodes", new[] { "Hash" });
            DropTable("dbo.QRCodes");
        }
    }
}
