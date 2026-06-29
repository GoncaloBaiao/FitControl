using System.Text.Json.Serialization;

namespace FitControl.Shared.Models;

public class TipoPlano
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("designacao")]
    public string Designacao { get; set; }
    
    [JsonPropertyName("preco")]
    public decimal Preco { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}