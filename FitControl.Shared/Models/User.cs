using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FitControl.Shared.Models;

public class User
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [StringLength(20, ErrorMessage = "O nome de utilizador deve ter no máximo 20 caracteres.")]
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}