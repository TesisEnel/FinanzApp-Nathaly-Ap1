using System.ComponentModel.DataAnnotations;
public class Gastos
{
    [Key]
    public int GastoId { get; set; }
  
    [Required(ErrorMessage ="La fecha es requerida")]
    public DateTime Fecha { get; set; }
    public List<GastosDetalle> DetalleGastos { get; set; }= new List<GastosDetalle> ();
   
}