namespace PsikoterapsitlerBurada.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateCategoryTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO CATEGORIES VALUES('Çocuk')");
            Sql("INSERT INTO CATEGORIES VALUES('Ergen')");
            Sql("INSERT INTO CATEGORIES VALUES('Çift')");
            Sql("INSERT INTO CATEGORIES VALUES('Cinsel')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM CATEGORIES WHERE Id = 1");
            Sql("DELETE FROM CATEGORIES WHERE Id = 2");
            Sql("DELETE FROM CATEGORIES WHERE Id = 3");
            Sql("DELETE FROM CATEGORIES WHERE Id = 4");
          
        }
    }
}
