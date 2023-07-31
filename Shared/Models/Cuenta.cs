using System.ComponentModel.DataAnnotations;
public class Cuenta
    {
        public int Id { get; set; }
        public string? NombreCliente { get; set; }
        public double Monto { get; set; }
    }
