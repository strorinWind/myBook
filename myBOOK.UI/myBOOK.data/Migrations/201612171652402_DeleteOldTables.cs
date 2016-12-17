namespace myBOOK.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteOldTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Favourites", new[] { "Book_BookName", "Book_Author" }, "dbo.Books");
            DropForeignKey("dbo.Favourites", "User_Login", "dbo.Users");
            DropForeignKey("dbo.FutureReadBooks", new[] { "Book_BookName", "Book_Author" }, "dbo.Books");
            DropForeignKey("dbo.FutureReadBooks", "User_Login", "dbo.Users");
            DropForeignKey("dbo.PastReadBooks", new[] { "Book_BookName", "Book_Author" }, "dbo.Books");
            DropForeignKey("dbo.PastReadBooks", "User_Login", "dbo.Users");
            DropIndex("dbo.Favourites", new[] { "Book_BookName", "Book_Author" });
            DropIndex("dbo.Favourites", new[] { "User_Login" });
            DropIndex("dbo.FutureReadBooks", new[] { "Book_BookName", "Book_Author" });
            DropIndex("dbo.FutureReadBooks", new[] { "User_Login" });
            DropIndex("dbo.PastReadBooks", new[] { "Book_BookName", "Book_Author" });
            DropIndex("dbo.PastReadBooks", new[] { "User_Login" });
            DropTable("dbo.Favourites");
            DropTable("dbo.FutureReadBooks");
            DropTable("dbo.PastReadBooks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PastReadBooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Book_BookName = c.String(maxLength: 128),
                        Book_Author = c.String(maxLength: 128),
                        User_Login = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FutureReadBooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Book_BookName = c.String(maxLength: 128),
                        Book_Author = c.String(maxLength: 128),
                        User_Login = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Favourites",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Book_BookName = c.String(maxLength: 128),
                        Book_Author = c.String(maxLength: 128),
                        User_Login = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.PastReadBooks", "User_Login");
            CreateIndex("dbo.PastReadBooks", new[] { "Book_BookName", "Book_Author" });
            CreateIndex("dbo.FutureReadBooks", "User_Login");
            CreateIndex("dbo.FutureReadBooks", new[] { "Book_BookName", "Book_Author" });
            CreateIndex("dbo.Favourites", "User_Login");
            CreateIndex("dbo.Favourites", new[] { "Book_BookName", "Book_Author" });
            AddForeignKey("dbo.PastReadBooks", "User_Login", "dbo.Users", "Login");
            AddForeignKey("dbo.PastReadBooks", new[] { "Book_BookName", "Book_Author" }, "dbo.Books", new[] { "BookName", "Author" });
            AddForeignKey("dbo.FutureReadBooks", "User_Login", "dbo.Users", "Login");
            AddForeignKey("dbo.FutureReadBooks", new[] { "Book_BookName", "Book_Author" }, "dbo.Books", new[] { "BookName", "Author" });
            AddForeignKey("dbo.Favourites", "User_Login", "dbo.Users", "Login");
            AddForeignKey("dbo.Favourites", new[] { "Book_BookName", "Book_Author" }, "dbo.Books", new[] { "BookName", "Author" });
        }
    }
}
