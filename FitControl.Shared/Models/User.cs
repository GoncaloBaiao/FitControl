using System.Text.Json.Serialization;

namespace FitControl.Shared.Models;

public class User
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}