using System.Text.Json.Serialization;

namespace FitControl.Shared.Models;

public class Inscricao
{
    [JsonPropertyName("aulaId")]
    public int AulaId { get; set; }
    
    [JsonPropertyName("aula")]
    public Aula Aula { get; set; }
    
    [JsonPropertyName("socioId")]
    public int SocioId { get; set; }
    
    [JsonPropertyName("socio")]
    public Socio Socio { get; set; }
    
    [JsonPropertyName("dataInscricao")]
    public DateTime DataInscricao { get; set; }
    
    [JsonPropertyName("dataCancelamento")]
    public DateTime DataCancelamento { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}