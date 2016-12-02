namespace myBOOK.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FutureReadBooks",
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
            
            CreateTable(
                "dbo.PastReadBooks",
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
            DropForeignKey("dbo.PastReadBooks", "User_ID", "dbo.Users");
            DropForeignKey("dbo.PastReadBooks", "Book_ID", "dbo.Books");
            DropForeignKey("dbo.FutureReadBooks", "User_ID", "dbo.Users");
            DropForeignKey("dbo.FutureReadBooks", "Book_ID", "dbo.Books");
            DropIndex("dbo.PastReadBooks", new[] { "User_ID" });
            DropIndex("dbo.PastReadBooks", new[] { "Book_ID" });
            DropIndex("dbo.FutureReadBooks", new[] { "User_ID" });
            DropIndex("dbo.FutureReadBooks", new[] { "Book_ID" });
            DropTable("dbo.PastReadBooks");
            DropTable("dbo.FutureReadBooks");
        }
    }
}
