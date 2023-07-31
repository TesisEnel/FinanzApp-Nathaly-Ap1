using System.ComponentModel.DataAnnotations;
public class Deudas
{
    [Key]
    public int DeudaId { get; set; }
  
    [Required(ErrorMessage ="La fecha es requerida")]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    [Required(ErrorMessage ="El nombre del acreedor es requerido")]
    public string? NombreAcreedor { get; set; }

    [Required(ErrorMessage ="El tipo de deuda es obligatoria")]
    public string? TipoDeuda { get; set; }

    [Required]
    [Range(minimum: 1, maximum: double.MaxValue, ErrorMessage = "Ingrese un monto mayor a 0.")]
    public double MontoTotal { get; set; }
    public DateTime FechaVencimiento{ get; set; }    
   
}
