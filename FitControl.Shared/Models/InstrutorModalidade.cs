using System.Text.Json.Serialization;

namespace FitControl.Shared.Models;

public class InstrutorModalidade
{
    [JsonPropertyName("instrutorId")]
    public int InstrutorId { get; set; }
    
    [JsonPropertyName("instrutor")]
    public Instrutor Instrutor { get; set; }
    
    [JsonPropertyName("modalidadeId")]
    public int ModalidadeId { get; set; }
    
    [JsonPropertyName("modalidade")]
    public Modalidade Modalidade { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}