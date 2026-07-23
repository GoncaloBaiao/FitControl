using System.ComponentModel.DataAnnotations;

namespace FitControl.API.Entities;

public class User : BaseEntity
{
    [StringLength(20)]
    public string Username { get; set; }
    public string Password { get; set; }
}