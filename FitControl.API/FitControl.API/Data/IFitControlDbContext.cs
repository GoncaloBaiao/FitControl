using FitControl.API.Entities;
using Microsoft.EntityFrameworkCore;
namespace FitControl.API.Data;

public interface IFitControlDbContext
{
    public DbSet<Aula>? Aulas { get; set; }
    public DbSet<Inscricao>? Inscricaos { get; set; }
    public DbSet<Instrutor>? Instrutors { get; set; }
    public DbSet<InstrutorModalidade>? InstrutorModalidades { get; set; }
    public DbSet<Modalidade>? Modalidades { get; set; }
    public DbSet<NivelDificuldade>? NivelDificuldades { get; set; }
    public DbSet<Sala>? Salas { get; set; }
    public DbSet<Socio>? Socios { get; set; }
    public DbSet<TipoPlano>? TipoPlanos { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}