namespace myBOOK.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectedMistake : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RegistrationDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Users", "RegistrartionDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "RegistrartionDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Users", "RegistrationDate");
        }
    }
}
