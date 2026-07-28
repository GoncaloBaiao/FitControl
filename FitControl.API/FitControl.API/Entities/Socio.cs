using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitControl.API.Entities;

public class Socio : BaseEntity
{
    public int TipoPlanoId { get; set; }
    public TipoPlano TipoPlano { get; set; }
    [StringLength(20, ErrorMessage = "O nome do sócio deve ter no máximo 20 caracteres.")]
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public int GeneroId { get; set; }
    public Genero Genero { get; set; }
    [Range(1, 3, ErrorMessage = "A altura deve ter valores entre 1m e 3m")]
    public float Altura { get; set; }
    [Range(35, 200, ErrorMessage = "O peso deve ter valores entre 35kg a 200kg")]
    public float Peso { get; set; }
    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    public string Email { get; set; }
    [RegularExpression(@"^(9\d{8}|2\d{8})$", 
        ErrorMessage = "O contacto telefónico deve ser um número português válido (9 dígitos).")]
    public string ContactoTelefonico { get; set; }
    public DateTime InicioSubscricao { get; set; }
    public DateTime FimSubscricao { get; set; }
    //public ICollection<Inscricao> Inscricaos {get; set; } = [];
}