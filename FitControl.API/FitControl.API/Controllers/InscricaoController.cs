using FitControl.API.Data;
using FitControl.API.Entities;
using FitControl.API.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitControl.API.Controllers;

public class InscricaoController : ControllerBase
{
    private readonly IFitControlDbContext _fitControlDbContext;
    private readonly IMapper _mapper;

    public InscricaoController(IFitControlDbContext fitControlAppDbContext, IMapper mapper)
    {
        _fitControlDbContext = fitControlAppDbContext;
        _mapper = mapper;
    }

    [HttpGet("/inscricoes")]
    public async Task<IResult> GetInscricoes()
    {
        if (_fitControlDbContext?.Inscricaos is null)
            return Results.Ok();

        var inscricoes = await _fitControlDbContext.Inscricaos
            .Include(i => i.Aula)
            .Include(i => i.Socio)
            .Where(a=>a.IsDeleted ==  false && a.Aula.IsDeleted == false && a.Socio.IsDeleted == false)
            .ToListAsync();
            
        return Results.Ok(inscricoes);
    }

    [HttpGet("/inscricao/{id}")]
    public async Task<Inscricao> GetInscricao(int id)
    {
        if (_fitControlDbContext?.Inscricaos is null)
            return new Inscricao();

        var inscricao = await _fitControlDbContext.Inscricaos
            .AsNoTracking()
            .Include(i => i.Aula)
            .Include(i => i.Socio)
            .FirstOrDefaultAsync(i =>
                i.Id == id &&
                !i.IsDeleted &&
                i.Aula != null && !i.Aula.IsDeleted &&
                i.Socio != null && !i.Socio.IsDeleted);

        return inscricao ?? new Inscricao();
    }

    [HttpPost("/inscricao")]
    public async Task<IResult> AddInscricao([FromBody] InscricaoDto inscricao)
    {
        if (inscricao is null) return Results.BadRequest();

        inscricao.Aula = null;
        inscricao.Socio = null;

        var mapper = _mapper.Map<Models.InscricaoDto, Entities.Inscricao>(inscricao);
        mapper.CreatedAt = DateTime.Now;
        mapper.UpdatedAt = DateTime.Now;

        if (_fitControlDbContext.Inscricaos is null) return Results.Empty;
        
        var aula = await _fitControlDbContext.Aulas
            .FirstOrDefaultAsync(a => a.Id == inscricao.AulaId);

        if (aula is null)
            return Results.NotFound("Aula não encontrada.");
        
        var inscritos = await _fitControlDbContext.Inscricaos
            .CountAsync(i =>
                i.AulaId == inscricao.AulaId &&
                !i.IsDeleted);
        
        if (inscritos >= aula.Capacidade)
        {
            return Results.BadRequest("A aula excedeu a capacidade máxima.");
        }
        

        _fitControlDbContext.Inscricaos.Add(mapper);
        await _fitControlDbContext.SaveChangesAsync();
        return Results.Ok("Inscricao adicionada com sucesso!");
    }

    [HttpPut("/inscricao")]
    public async Task<IResult> UpdateInscricao([FromBody] InscricaoDto? inscricaoDto)
    {
        if (inscricaoDto is null) return Results.BadRequest();

        inscricaoDto.Aula = null;
        inscricaoDto.Socio = null;

        if (_fitControlDbContext.Inscricaos is null) return Results.NotFound();

        var oldInscricao = await _fitControlDbContext.Inscricaos
            .FirstOrDefaultAsync(p => p.Id == inscricaoDto.Id);

        if (oldInscricao is null) return Results.NotFound("Inscricao não foi encontrada!");

        inscricaoDto.Adapt(oldInscricao);

        try
        {
            var result = await _fitControlDbContext.SaveChangesAsync();
            if (result <= 0) return Results.NotFound("Não foi possível guardar os dados");
        }
        catch (Exception e)
        {
            return Results.NotFound(e.Message);
        }

        return Results.Ok(inscricaoDto);
    }

    [HttpDelete("/inscricao/softdelete/{id}")]
    public async Task<IResult> SoftDeleteInscricao(int id)
    {
        if (_fitControlDbContext.Inscricaos is null) return Results.Empty;

        var inscricao = await _fitControlDbContext.Inscricaos.FirstOrDefaultAsync(i => i.Id == id);
        if (inscricao is null) return Results.NotFound("Inscricao não encontrada");

        inscricao.IsDeleted = true;
        inscricao.UpdatedAt = DateTime.Now;

        await _fitControlDbContext.SaveChangesAsync();
        return Results.Ok("Inscricao apagada com sucesso!");
    }
}