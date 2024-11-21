namespace PruebaTecnica1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                {
                    EmpresaID = c.Int(nullable: false, identity: true),
                    Nombre = c.String(nullable: false),
                    Codigo = c.Int(nullable: false),
                    Direccion = c.String(),
                    Telefono = c.String(),
                    Ciudad = c.String(),
                    Departamento = c.String(),
                    Pais = c.String(),
                    FechaCreacion = c.DateTime(),
                    FechaUltimaModificacion = c.DateTime(),
                })
                .PrimaryKey(t => t.EmpresaID);

        }
        
        public override void Down()
        {
            DropTable("dbo.Companies");
        }
    }
}
