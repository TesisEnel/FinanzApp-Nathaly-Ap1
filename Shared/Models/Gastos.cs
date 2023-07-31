using System.ComponentModel.DataAnnotations;
public class Gastos
{
    [Key]
    public int GastoId { get; set; }
  
    [Required(ErrorMessage ="La fecha es requerida")]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public List<GastosDetalle> DetalleGastos { get; set; }= new List<GastosDetalle> ();
   
}