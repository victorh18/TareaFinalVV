namespace ModuloGestorNotas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotalTotalPropertyAtNotaModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notas", "NotaTotal", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notas", "NotaTotal");
        }
    }
}
