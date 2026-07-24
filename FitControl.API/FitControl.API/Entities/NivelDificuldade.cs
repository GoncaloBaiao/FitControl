using System.ComponentModel.DataAnnotations;

namespace FitControl.API.Entities;

public class NivelDificuldade : BaseEntity
{
    [StringLength(15, ErrorMessage = "O nível de dificuldade deve ter no máximo 15 caracteres.")]
    public string Nivel { get; set; }
    //public ICollection<Modalidade> Modalidades {get; set; } = new List<Modalidade>();
}