namespace LockerApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RegistrationAndLastLoginTimeColumnAddedToUsersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RegistrationDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "LastLoginDateTime", c => c.DateTime(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastLoginDateTime");
            DropColumn("dbo.AspNetUsers", "RegistrationDateTime");
        }
    }
}
