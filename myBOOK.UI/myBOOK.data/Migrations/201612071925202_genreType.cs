namespace myBOOK.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class genreType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Genre", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Genre", c => c.String());
        }
    }
}
