namespace ModuloGestorNotas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsuariosPertenecenGrupoes", "PeriodoId", "dbo.Periodoes");
            DropIndex("dbo.UsuariosPertenecenGrupoes", new[] { "PeriodoId" });
            AddColumn("dbo.Grupoes", "Codigo", c => c.String());
            AddColumn("dbo.Grupoes", "PeriodoId", c => c.Int(nullable: false));
            AddColumn("dbo.Periodoes", "Codigo", c => c.String());
            CreateIndex("dbo.Grupoes", "PeriodoId");
            AddForeignKey("dbo.Grupoes", "PeriodoId", "dbo.Periodoes", "Id", cascadeDelete: true);
            DropColumn("dbo.UsuariosPertenecenGrupoes", "PeriodoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UsuariosPertenecenGrupoes", "PeriodoId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Grupoes", "PeriodoId", "dbo.Periodoes");
            DropIndex("dbo.Grupoes", new[] { "PeriodoId" });
            DropColumn("dbo.Periodoes", "Codigo");
            DropColumn("dbo.Grupoes", "PeriodoId");
            DropColumn("dbo.Grupoes", "Codigo");
            CreateIndex("dbo.UsuariosPertenecenGrupoes", "PeriodoId");
            AddForeignKey("dbo.UsuariosPertenecenGrupoes", "PeriodoId", "dbo.Periodoes", "Id", cascadeDelete: true);
        }
    }
}
