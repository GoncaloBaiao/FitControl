using System.ComponentModel.DataAnnotations;

namespace FitControl.API.Entities;

public class Sala : BaseEntity
{
    [StringLength(20, ErrorMessage = "O nome da sala deve ter no máximo 20 caracteres.")]
    public string Nome { get; set; }
    [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
    public string Descricao { get; set; }
    //public ICollection<Aula> Aulas {get; set; } = new List<Aula>();
}