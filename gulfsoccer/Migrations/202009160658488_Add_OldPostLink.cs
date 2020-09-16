namespace gulfsoccer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_OldPostLink : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OldPostLinks",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OldPostLinks");
        }
    }
}
