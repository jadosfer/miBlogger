namespace WebBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Email", c => c.String());
            DropColumn("dbo.Posts", "Autor");
            DropColumn("dbo.Posts", "EmailAutor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "EmailAutor", c => c.String());
            AddColumn("dbo.Posts", "Autor", c => c.String());
            DropColumn("dbo.Posts", "Email");
        }
    }
}
