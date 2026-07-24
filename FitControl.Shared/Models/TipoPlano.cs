using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FitControl.Shared.Models;

public class TipoPlano
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [StringLength(15, ErrorMessage = "A designação deve ter no máximo 15 caracteres.")]
    [JsonPropertyName("designacao")]
    public string Designacao { get; set; }
    
    [Range(5, 1000, ErrorMessage = "o Preço deve ter valores entre 5€ a 1000€")]
    [JsonPropertyName("preco")]
    public decimal Preco { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}