using System.ComponentModel.DataAnnotations;

namespace FitControl.API.Entities;

public class Aula : BaseEntity
{
    public int ModalidadeId { get; set; }
    public Modalidade Modalidade { get; set; }
    public int InstrutorId { get; set; }
    public Instrutor Instrutor { get; set; }
    public int SalaId { get; set; }
    public Sala Sala { get; set; }
    [StringLength(20, ErrorMessage = "O nome da aula deve ter no máximo 20 caracteres.")]
    public string Nome {get; set;}
    public DateTime HoraInicio  {get; set;}
    public DateTime HoraFim {get; set;}
    [Range(1, 100, ErrorMessage = "A capacidade deve ter valores entre 1 e 100")]
    public int Capacidade {get; set;}
    [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
    public string Descricao {get; set;}
    //public ICollection<Inscricao> Inscricaos {get; set; } = [];
}