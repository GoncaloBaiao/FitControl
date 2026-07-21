using System.ComponentModel.DataAnnotations.Schema;

namespace FitControl.API.Entities;

public class Socio : BaseEntity
{
    public int TipoPlanoId { get; set; }
    public TipoPlano TipoPlano { get; set; }
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public Genero Genero { get; set; }
    public float Altura { get; set; }
    public float Peso { get; set; }
    public string Email { get; set; }
    public string ContactoTelefonico { get; set; }
    public DateTime InicioSubscricao { get; set; }
    public DateTime FimSubscricao { get; set; }
    //public ICollection<Inscricao> Inscricaos {get; set; } = [];
}