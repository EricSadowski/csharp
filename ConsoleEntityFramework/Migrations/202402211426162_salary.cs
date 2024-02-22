namespace ConsoleEntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salary : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Salary", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Salary");
        }
    }
}
