using Microsoft.EntityFrameworkCore;

public class Context : DbContext
   {
       public Context(DbContextOptions<Context> Opcions) : base(Opcions) { } 
       public DbSet<Gastos> Gastos {get; set;}
       public DbSet<Deudas> Deudas {get; set;} 
       public DbSet<Ingresos> Ingresos {get; set;}
       public DbSet<Ahorros> Ahorros {get; set;}
       public DbSet<Cuenta> Cuenta {get; set;}

       protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
        modelBuilder.Entity<Cuenta>().HasData(
            new Cuenta
            {
                Id = 1,
                NombreCliente = "Nathaly Goris",
                Monto = 0
            }
        );
      }
    }