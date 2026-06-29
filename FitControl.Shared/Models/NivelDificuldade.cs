using System.Text.Json.Serialization;

namespace FitControl.Shared.Models;

public class NivelDificuldade
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("nivel")]
    public string Nivel { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}