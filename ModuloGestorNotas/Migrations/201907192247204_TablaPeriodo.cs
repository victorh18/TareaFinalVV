namespace ModuloGestorNotas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablaPeriodo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Periodoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Anio = c.Int(nullable: false),
                        Cuatrimestre = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UsuariosPertenecenGrupoes", "PeriodoId", c => c.Int(nullable: false));
            CreateIndex("dbo.UsuariosPertenecenGrupoes", "PeriodoId");
            AddForeignKey("dbo.UsuariosPertenecenGrupoes", "PeriodoId", "dbo.Periodoes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuariosPertenecenGrupoes", "PeriodoId", "dbo.Periodoes");
            DropIndex("dbo.UsuariosPertenecenGrupoes", new[] { "PeriodoId" });
            DropColumn("dbo.UsuariosPertenecenGrupoes", "PeriodoId");
            DropTable("dbo.Periodoes");
        }
    }
}
