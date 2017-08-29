namespace Vidly.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) VALUES (1,'Comedy')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2,'Thriller')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3,'Horror')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4,'Drama')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (5,'Fantasy')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (6,'Document')");
        }

        public override void Down()
        {
        }
    }
}
