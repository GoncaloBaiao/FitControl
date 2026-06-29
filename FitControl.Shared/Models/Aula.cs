using System.Text.Json.Serialization;

namespace FitControl.Shared.Models;

public class Aula
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("modalidadeId")]
    public int ModalidadeId { get; set; }
    
    [JsonPropertyName("modalidade")]
    public Modalidade Modalidade { get; set; }
    
    [JsonPropertyName("instrutorId")]
    public int InstrutorId { get; set; }
    
    [JsonPropertyName("instrutor")]
    public Instrutor Instrutor { get; set; }
    
    [JsonPropertyName("salaId")]
    public int SalaId { get; set; }
    
    [JsonPropertyName("sala")]
    public Sala Sala { get; set; }
    
    [JsonPropertyName("nome")]
    public string Nome {get; set;}
    
    [JsonPropertyName("horaInicio")]
    public DateTime HoraInicio  {get; set;}
    
    [JsonPropertyName("horaFim")]
    public DateTime HoraFim {get; set;}
    
    [JsonPropertyName("capacidade")]
    public int Capacidade {get; set;}
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted {get; set;}
}