namespace myBOOK.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BookName = c.String(),
                        Author = c.String(),
                        Genre = c.String(),
                        Description = c.Int(nullable: false),
                        LoadingLink = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReviewText = c.String(),
                        Book_ID = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Books", t => t.Book_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.Book_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        RegistrartionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
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
            DropForeignKey("dbo.Scores", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Scores", "Book_ID", "dbo.Books");
            DropForeignKey("dbo.Reviews", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Reviews", "Book_ID", "dbo.Books");
            DropIndex("dbo.Scores", new[] { "User_ID" });
            DropIndex("dbo.Scores", new[] { "Book_ID" });
            DropIndex("dbo.Reviews", new[] { "User_ID" });
            DropIndex("dbo.Reviews", new[] { "Book_ID" });
            DropTable("dbo.Scores");
            DropTable("dbo.Users");
            DropTable("dbo.Reviews");
            DropTable("dbo.Books");
        }
    }
}
