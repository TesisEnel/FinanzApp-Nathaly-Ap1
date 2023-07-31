using System.ComponentModel.DataAnnotations;
public class Ahorros
{
    [Key]
    public int AhorroId { get; set; }

    [Required(ErrorMessage ="El nombre de la meta es requerido")]
    public string? NombreMeta { get; set; }

    [Required(ErrorMessage ="La fecha es requerida")]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    [Required]
    [Range(minimum: 1, maximum: double.MaxValue, ErrorMessage = "Ingrese un monto mayor a 0.")]
    public double MontoMeta { get; set; }

    [Required(ErrorMessage ="La frecuencia de ahorro es requerida")]
    public string? FrecuenciaAhorro{ get; set; }

    
}