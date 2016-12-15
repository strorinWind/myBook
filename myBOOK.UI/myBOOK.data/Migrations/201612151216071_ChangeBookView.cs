namespace myBOOK.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBookView : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Rating", c => c.Double());
            AddColumn("dbo.Books", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Discriminator");
            DropColumn("dbo.Books", "Rating");
        }
    }
}
