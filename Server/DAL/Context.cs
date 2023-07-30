using Microsoft.EntityFrameworkCore;

public class Context : DbContext
   {
       public Context(DbContextOptions<Context> Opcions) : base(Opcions) { } 
       public DbSet<Gastos> Gastos {get; set;}
       public DbSet<Deudas> Deudas {get; set;} 
       public DbSet<Ingresos> Ingresos {get; set;}
       public DbSet<Ahorros> Ahorros {get; set;}
    }