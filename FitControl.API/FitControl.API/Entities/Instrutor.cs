using System.ComponentModel.DataAnnotations;

namespace FitControl.API.Entities;

public class Instrutor : BaseEntity
{
    [StringLength(20, ErrorMessage = "O nome do instrutor deve ter no máximo 20 caracteres.")]
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    [EmailAddress(ErrorMessage = "Email inválido.")]
    public string Email { get; set; }
    [RegularExpression(@"^(9\d{8}|2\d{8})$", 
        ErrorMessage = "O contacto telefónico deve ser um número português válido (9 dígitos).")]
    public string ContactoTelefonico { get; set; }
    //public ICollection<Aula> Aulas {get; set; } = new List<Aula>();
    //public ICollection<InstrutorModalidade> IntrutorModalidades {get; set; } = [];
}