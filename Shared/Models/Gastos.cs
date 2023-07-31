using System.ComponentModel.DataAnnotations;
public class Gastos
{
    [Key]
    public int GastoId { get; set; }

    [Required(ErrorMessage ="La fecha es requerida")]
    public DateTime Fecha { get; set; }
    [Required(ErrorMessage ="La descripción es requerida")]
    public string? Descripcion { get; set; }
    [Required(ErrorMessage ="El método de pago es requerida")]
    public string? MetodoDePago { get; set; }
    public double Total { get; set; }
    public List<GastosDetalle> DetalleGastos { get; set; }= new List<GastosDetalle> ();

}
