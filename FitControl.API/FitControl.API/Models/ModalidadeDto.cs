using FitControl.API.Entities;

namespace FitControl.API.Models;

public class ModalidadeDto
{
    public int Id { get; set; }
    
    public int NivelDificuldadeId { get; set; }
    
    public NivelDificuldade NivelDificuldade { get; set; }
    
    public string Nome { get; set; }
    
    public string Descricao { get; set; }
    
    public bool IsDeleted { get; set; }
}