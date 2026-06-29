using System.Text.Json.Serialization;

namespace FitControl.Shared.Models;

public class Sala
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("nome")]
    public string Nome { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}