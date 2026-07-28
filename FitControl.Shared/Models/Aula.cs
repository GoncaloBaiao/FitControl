using System.ComponentModel.DataAnnotations;
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
    [StringLength(20, ErrorMessage = "O nome da aula deve ter no máximo 20 caracteres.")]
    [JsonPropertyName("nome")]
    public string Nome {get; set;}
    
    [JsonPropertyName("horaInicio")]
    public DateTime? HoraInicio  {get; set;}
    
    [JsonPropertyName("horaFim")]
    public DateTime? HoraFim {get; set;}
    [Range(1, 100, ErrorMessage = "A capacidade deve ter valores entre 1 e 100")]
    [JsonPropertyName("capacidade")]
    public int Capacidade {get; set;}
    
    [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
    [JsonPropertyName("descricao")]
    public string Descricao {get; set;}
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted {get; set;}
    
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt {get; set;}
    
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt {get; set;}
}