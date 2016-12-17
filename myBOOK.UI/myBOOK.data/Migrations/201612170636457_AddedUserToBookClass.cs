namespace myBOOK.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserToBookClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserToBooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Category = c.Int(nullable: false),
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
            DropForeignKey("dbo.UserToBooks", "User_Login", "dbo.Users");
            DropForeignKey("dbo.UserToBooks", new[] { "Book_BookName", "Book_Author" }, "dbo.Books");
            DropIndex("dbo.UserToBooks", new[] { "User_Login" });
            DropIndex("dbo.UserToBooks", new[] { "Book_BookName", "Book_Author" });
            DropTable("dbo.UserToBooks");
        }
    }
}
