namespace myBOOK.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookName = c.String(nullable: false, maxLength: 128),
                        Author = c.String(nullable: false, maxLength: 128),
                        Genre = c.Int(nullable: false),
                        Description = c.String(),
                        LoadingLink = c.String(),
                    })
                .PrimaryKey(t => new { t.BookName, t.Author });
            
            CreateTable(
                "dbo.Favourites",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Book_BookName = c.String(maxLength: 128),
                        Book_Author = c.String(maxLength: 128),
                        User_Login = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Books", t => new { t.Book_BookName, t.Book_Author })
                .ForeignKey("dbo.Users", t => t.User_Login)
                .Index(t => new { t.Book_BookName, t.Book_Author })
                .Index(t => t.User_Login);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Login = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Password = c.Guid(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Login);
            
            CreateTable(
                "dbo.FutureReadBooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Book_BookName = c.String(maxLength: 128),
                        Book_Author = c.String(maxLength: 128),
                        User_Login = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Books", t => new { t.Book_BookName, t.Book_Author })
                .ForeignKey("dbo.Users", t => t.User_Login)
                .Index(t => new { t.Book_BookName, t.Book_Author })
                .Index(t => t.User_Login);
            
            CreateTable(
                "dbo.PastReadBooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Book_BookName = c.String(maxLength: 128),
                        Book_Author = c.String(maxLength: 128),
                        User_Login = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Books", t => new { t.Book_BookName, t.Book_Author })
                .ForeignKey("dbo.Users", t => t.User_Login)
                .Index(t => new { t.Book_BookName, t.Book_Author })
                .Index(t => t.User_Login);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReviewText = c.String(),
                        Book_BookName = c.String(maxLength: 128),
                        Book_Author = c.String(maxLength: 128),
                        User_Login = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Books", t => new { t.Book_BookName, t.Book_Author })
                .ForeignKey("dbo.Users", t => t.User_Login)
                .Index(t => new { t.Book_BookName, t.Book_Author })
                .Index(t => t.User_Login);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        Book_BookName = c.String(maxLength: 128),
                        Book_Author = c.String(maxLength: 128),
                        User_Login = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Books", t => new { t.Book_BookName, t.Book_Author })
                .ForeignKey("dbo.Users", t => t.User_Login)
                .Index(t => new { t.Book_BookName, t.Book_Author })
                .Index(t => t.User_Login);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Scores", "User_Login", "dbo.Users");
            DropForeignKey("dbo.Scores", new[] { "Book_BookName", "Book_Author" }, "dbo.Books");
            DropForeignKey("dbo.Reviews", "User_Login", "dbo.Users");
            DropForeignKey("dbo.Reviews", new[] { "Book_BookName", "Book_Author" }, "dbo.Books");
            DropForeignKey("dbo.PastReadBooks", "User_Login", "dbo.Users");
            DropForeignKey("dbo.PastReadBooks", new[] { "Book_BookName", "Book_Author" }, "dbo.Books");
            DropForeignKey("dbo.FutureReadBooks", "User_Login", "dbo.Users");
            DropForeignKey("dbo.FutureReadBooks", new[] { "Book_BookName", "Book_Author" }, "dbo.Books");
            DropForeignKey("dbo.Favourites", "User_Login", "dbo.Users");
            DropForeignKey("dbo.Favourites", new[] { "Book_BookName", "Book_Author" }, "dbo.Books");
            DropIndex("dbo.Scores", new[] { "User_Login" });
            DropIndex("dbo.Scores", new[] { "Book_BookName", "Book_Author" });
            DropIndex("dbo.Reviews", new[] { "User_Login" });
            DropIndex("dbo.Reviews", new[] { "Book_BookName", "Book_Author" });
            DropIndex("dbo.PastReadBooks", new[] { "User_Login" });
            DropIndex("dbo.PastReadBooks", new[] { "Book_BookName", "Book_Author" });
            DropIndex("dbo.FutureReadBooks", new[] { "User_Login" });
            DropIndex("dbo.FutureReadBooks", new[] { "Book_BookName", "Book_Author" });
            DropIndex("dbo.Favourites", new[] { "User_Login" });
            DropIndex("dbo.Favourites", new[] { "Book_BookName", "Book_Author" });
            DropTable("dbo.Scores");
            DropTable("dbo.Reviews");
            DropTable("dbo.PastReadBooks");
            DropTable("dbo.FutureReadBooks");
            DropTable("dbo.Users");
            DropTable("dbo.Favourites");
            DropTable("dbo.Books");
        }
    }
}
