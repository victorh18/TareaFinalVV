namespace ModuloGestorNotas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixModels1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notas", "UsuariosPertenecenGrupoId", "dbo.UsuariosPertenecenGrupoes");
            DropIndex("dbo.Notas", new[] { "UsuariosPertenecenGrupoId" });
            AddColumn("dbo.UsuariosPertenecenGrupoes", "NotaId", c => c.Int(nullable: false));
            AlterColumn("dbo.UsuariosPertenecenGrupoes", "UsuarioId", c => c.String());
            CreateIndex("dbo.UsuariosPertenecenGrupoes", "NotaId");
            AddForeignKey("dbo.UsuariosPertenecenGrupoes", "NotaId", "dbo.Notas", "Id", cascadeDelete: true);
            DropColumn("dbo.Notas", "UsuariosPertenecenGrupoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notas", "UsuariosPertenecenGrupoId", c => c.Int(nullable: false));
            DropForeignKey("dbo.UsuariosPertenecenGrupoes", "NotaId", "dbo.Notas");
            DropIndex("dbo.UsuariosPertenecenGrupoes", new[] { "NotaId" });
            AlterColumn("dbo.UsuariosPertenecenGrupoes", "UsuarioId", c => c.Int(nullable: false));
            DropColumn("dbo.UsuariosPertenecenGrupoes", "NotaId");
            CreateIndex("dbo.Notas", "UsuariosPertenecenGrupoId");
            AddForeignKey("dbo.Notas", "UsuariosPertenecenGrupoId", "dbo.UsuariosPertenecenGrupoes", "Id", cascadeDelete: true);
        }
    }
}
