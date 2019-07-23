namespace ModuloGestorNotas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TriggerAssignRole : DbMigration
    {
        public override void Up()
        {
            Sql("CREATE TRIGGER Tr_AsignarRol ON [dbo].[AspNetUsers] AFTER INSERT AS BEGIN DECLARE @User_Id NVARCHAR(50) SET @User_Id = (SELECT Id FROM INSERTED) INSERT INTO AspNetUserRoles(UserId, RoleId) VALUES(@User_Id, '3') END ");
        }
        
        public override void Down()
        {
            Sql("DROP TRIGGER Tr_AsignarRol");
        }
    }
}
