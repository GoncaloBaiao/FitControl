using System.Text.Json.Serialization;

namespace FitControl.Shared.Models;

public class Modalidade
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("nivelDificuldadeId")]
    public int NivelDificuldadeId { get; set; }
    
    [JsonPropertyName("nivelDificuldade")]
    public NivelDificuldade NivelDificuldade { get; set; }
    
    [JsonPropertyName("nome")]
    public string Nome { get; set; }
    
    [JsonPropertyName("descricao")]
    public string Descricao { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}