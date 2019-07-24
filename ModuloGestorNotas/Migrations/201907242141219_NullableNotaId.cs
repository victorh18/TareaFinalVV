namespace ModuloGestorNotas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableNotaId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsuariosPertenecenGrupoes", "NotaId", "dbo.Notas");
            DropIndex("dbo.UsuariosPertenecenGrupoes", new[] { "NotaId" });
            AlterColumn("dbo.UsuariosPertenecenGrupoes", "NotaId", c => c.Int());
            CreateIndex("dbo.UsuariosPertenecenGrupoes", "NotaId");
            AddForeignKey("dbo.UsuariosPertenecenGrupoes", "NotaId", "dbo.Notas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuariosPertenecenGrupoes", "NotaId", "dbo.Notas");
            DropIndex("dbo.UsuariosPertenecenGrupoes", new[] { "NotaId" });
            AlterColumn("dbo.UsuariosPertenecenGrupoes", "NotaId", c => c.Int(nullable: false));
            CreateIndex("dbo.UsuariosPertenecenGrupoes", "NotaId");
            AddForeignKey("dbo.UsuariosPertenecenGrupoes", "NotaId", "dbo.Notas", "Id", cascadeDelete: true);
        }
    }
}
