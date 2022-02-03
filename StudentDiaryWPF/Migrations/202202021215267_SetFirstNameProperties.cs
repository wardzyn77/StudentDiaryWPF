namespace StudentDiaryWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetFirstNameProperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "FirstName", c => c.String(nullable: false, maxLength: 100));
            // tutaj mo¿na te¿ dodaæ jakiœ srypt SQL
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "FirstName", c => c.String());
            //dobrze, aby przy ewentualnym cofniêciu równie¿ by³ odpowiedni srypt SQL
        }
    }
}
