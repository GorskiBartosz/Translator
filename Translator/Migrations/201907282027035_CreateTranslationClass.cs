namespace Translator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTranslationClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Translations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Source = c.String(),
                        TranslationType = c.String(),
                        TranslatedText = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Translations");
        }
    }
}
