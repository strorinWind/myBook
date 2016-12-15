namespace myBOOK.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBookViewLocate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "Rating");
            DropColumn("dbo.Books", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Books", "Rating", c => c.Double());
        }
    }
}
