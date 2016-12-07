namespace myBOOK.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Description", c => c.Int(nullable: false));
        }
    }
}
