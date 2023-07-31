using System.ComponentModel.DataAnnotations;

public class Ingresos
{
    [Key]
    public int IngresoId { get; set; }
  
    [Required(ErrorMessage ="La fecha es requerida")]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    [Required]
    [Range(minimum: 1, maximum: double.MaxValue, ErrorMessage = "Ingrese un monto mayor a 0.")]
    public double Monto { get; set; }

    [Required(ErrorMessage ="La descripcion es requerida")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage ="La categoria es requerida")]
    public string? Categoria { get; set; }
    
   
}
