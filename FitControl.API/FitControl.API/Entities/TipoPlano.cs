using System.ComponentModel.DataAnnotations;

namespace FitControl.API.Entities;

public class TipoPlano : BaseEntity
{
    [StringLength(15, ErrorMessage = "A designação deve ter no máximo 15 caracteres.")]
    public string Designacao { get; set; }
    
    [Range(5, 1000, ErrorMessage = "O preço deve ter valores entre 5€ a 1000€")]
    public decimal Preco { get; set; }
    //public ICollection<Socio> Socios {get; set; } = new List<Socio>();
}