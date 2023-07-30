using System.ComponentModel.DataAnnotations;
public class GastosDetalle
{
    [Key]
    public int DetalleId { get; set; }
    public int GastosId { get; set; }
    public string? Descripcion { get; set; }
    public string?  Categoria { get; set; }
    public double Monto { get; set; }
  
}