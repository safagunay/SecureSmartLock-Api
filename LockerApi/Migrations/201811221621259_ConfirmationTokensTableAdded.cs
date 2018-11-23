namespace LockerApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ConfirmationTokensTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConfirmationTokens",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Token = c.String(nullable: false, maxLength: 1000),
                    ExpirationDateTime = c.DateTime(),
                    User_Id = c.String(maxLength: 128),
                    Type = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.ConfirmationTokens", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ConfirmationTokens", new[] { "User_Id" });
            DropTable("dbo.ConfirmationTokens");
        }
    }
}
