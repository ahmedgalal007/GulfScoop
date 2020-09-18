namespace gulfsoccer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_OldLink_Column_Type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OldPostLinks", "Link", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OldPostLinks", "Link", c => c.String());
        }
    }
}
