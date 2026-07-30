using FitControl.API.Data;
using FitControl.API.Entities;
using FitControl.API.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitControl.API.Controllers;

public class AulaController : ControllerBase
{
    private readonly IFitControlDbContext _fitControlDbContext;
    private readonly IMapper _mapper;

    public AulaController(IFitControlDbContext fitControlAppDbContext, IMapper mapper)
    {
        _fitControlDbContext = fitControlAppDbContext;
        _mapper = mapper;
    }
    
    [HttpGet("/aulas")]
    public async Task<IResult> GetAulas()
    {
        if (_fitControlDbContext is not null)
        {
            var aulas=_fitControlDbContext.Aulas
                .Include(x=>x.Sala)
                .Include(x=>x.Instrutor)
                .Include(x=>x.Modalidade)
                .Where(a=>a.IsDeleted ==  false && a.Sala.IsDeleted == false && a.Instrutor.IsDeleted == false && a.Modalidade.IsDeleted == false);
            if (aulas.Any())
            {
                return Results.Ok(await aulas.ToListAsync());
            }
        }

        return Results.Ok(new List<Aula>());
    }

    [HttpGet("/aula/{id}")]
    public async Task<IResult> GetAula(int id)
    {
        if (_fitControlDbContext is not null)
        {
            var aula= await _fitControlDbContext.Aulas
                .Include(x=>x.Sala)
                .Include(x=>x.Instrutor)
                .Include(x=>x.Modalidade)
                .FirstOrDefaultAsync(a => (a.IsDeleted == false || a.Sala.IsDeleted == false || a.Instrutor.IsDeleted == false || a.Modalidade.IsDeleted == false) && a.Id == id);
            if (aula is not null)
            {
                return Results.Ok(aula);
            }
        }
        return Results.Ok(new Aula());
    }

    [HttpPost("/aula")]
    public async Task<IResult> AddAula([FromBody] AulaDto aula)
    {
        if (aula is null)
        {
            return Results.BadRequest();
        }

        aula.Sala = null;
        aula.Instrutor = null;
        aula.Modalidade = null;
        
        var mapper = _mapper.Map<Models.AulaDto, Entities.Aula>(aula);

        mapper.CreatedAt = DateTime.Now;
        mapper.UpdatedAt = DateTime.Now;
        
        var aulas = _fitControlDbContext.Aulas;
        
        if (aula.HoraInicio >= aula.HoraFim)
        {
            return Results.BadRequest("A hora de início deve ser menor que a hora de fim.");
        }

        if (aulas is not null)
        {
            aulas.Add(mapper);
            await _fitControlDbContext.SaveChangesAsync();
            return Results.Ok("Aula adicionada com sucesso!");
        }

        
        return Results.Empty;
    }
    
    [HttpPut("/aula")]
    public async Task<IResult> UpdateAula ([FromBody] AulaDto? aulaDto)
    {
        if (aulaDto is null)
        {
            return Results.BadRequest();
        }
        
        aulaDto.Sala = null;
        aulaDto.Instrutor = null;
        aulaDto.Modalidade = null;

        if (_fitControlDbContext.Aulas is null)
        {
            return Results.NotFound();
        }
        
        var oldAula = await _fitControlDbContext.Aulas.FirstOrDefaultAsync(p => p.Id == aulaDto.Id);
        
        if (aulaDto.HoraInicio >= aulaDto.HoraFim)
        {
            return Results.BadRequest("A hora de início deve ser menor que a hora de fim.");
        }

        if (oldAula is null)
        {
            return Results.NotFound("Aula não foi encontrada!");
        }

        aulaDto.Adapt(oldAula);
        
        try
        {
            var result = await _fitControlDbContext.SaveChangesAsync();

            if (result <= 0)
            {
                return Results.NotFound("Não foi possível guardar os dados");
            }
        }
        catch (Exception e)
        {
            return Results.NotFound(e.Message);
        }

        return Results.Ok(aulaDto);
    }
    
    [HttpDelete("/aula/softdelete/{id}")]
    public async Task<IResult> SoftDeleteAula(int id)
    {
        if (_fitControlDbContext.Aulas is not null)
        {
            var aula = await _fitControlDbContext.Aulas.FirstOrDefaultAsync(p => p.Id == id);
            if (aula is null)
            {
                return Results.NotFound("Aula não encontrada");
            }
            
            aula.IsDeleted = true;
            aula.UpdatedAt = DateTime.Now;
            
            await _fitControlDbContext.SaveChangesAsync();
            return Results.Ok("Aula apagada com sucesso!");
        }
        return Results.Empty; 
    }
    
    // [HttpGet("/sala")]
    // public async Task<IResult> GetSala()
    // {
    //     if (_fitControlDbContext.Salas is not null)
    //     {
    //         var salas = await _fitControlDbContext.Aulas.ToListAsync();
    //
    //         if (salas.Any())
    //         {
    //             return Results.Ok(salas);
    //         }
    //     }
    //     return Results.NotFound();
    // }
    //
    // [HttpGet("/instrutor")]
    // public async Task<IActionResult> GetInstrutor()
    // {
    //     if (_fitControlDbContext.Instrutors is not null)
    //     {
    //         var Instrutors = await _fitControlDbContext.Instrutors.ToListAsync();
    //
    //         if (Instrutors.Any())
    //         {
    //             return Ok(Instrutors);
    //         }
    //     }
    //     return NotFound();
    // }
    //
    // [HttpGet("/modalidade")]
    // public async Task<IActionResult> GetModalidade()
    // {
    //     if (_fitControlDbContext.Modalidades is not null)
    //     {
    //         var Modalidades = await _fitControlDbContext.Modalidades.ToListAsync();
    //
    //         if (Modalidades.Any())
    //         {
    //             return Ok(Modalidades);
    //         }
    //     }
    //     return NotFound();
    // }

}

