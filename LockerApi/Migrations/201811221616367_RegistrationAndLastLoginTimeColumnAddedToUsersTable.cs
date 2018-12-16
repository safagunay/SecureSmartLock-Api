namespace LockerApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RegistrationAndLastLoginTimeColumnAddedToUsersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RegisteredOnUTC", c => c.DateTime(storeType: "smalldatetime", nullable: false));
            AddColumn("dbo.AspNetUsers", "LastLoginUTC", c => c.DateTime(storeType: "smalldatetime", nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastLoginUTC");
            DropColumn("dbo.AspNetUsers", "RegisteredOnUTC");
        }
    }
}
