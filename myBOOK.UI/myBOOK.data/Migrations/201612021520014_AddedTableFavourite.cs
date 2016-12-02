namespace myBOOK.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTableFavourite : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favourites",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Book_ID = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Books", t => t.Book_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.Book_ID)
                .Index(t => t.User_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favourites", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Favourites", "Book_ID", "dbo.Books");
            DropIndex("dbo.Favourites", new[] { "User_ID" });
            DropIndex("dbo.Favourites", new[] { "Book_ID" });
            DropTable("dbo.Favourites");
        }
    }
}
