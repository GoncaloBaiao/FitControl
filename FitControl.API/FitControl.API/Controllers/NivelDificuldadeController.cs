using FitControl.API.Data;
using FitControl.API.Entities;
using FitControl.API.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitControl.API.Controllers;

public class NivelDificuldadeController : ControllerBase
{
    private readonly IFitControlDbContext _fitControlAppDbContext;
    private readonly IMapper _mapper;

    public NivelDificuldadeController(IFitControlDbContext fitControlAppDbContext, IMapper mapper)
    {
        _fitControlAppDbContext = fitControlAppDbContext;
        _mapper = mapper;
    }
    
    [HttpGet("/niveldificuldades")]
    public async Task<IResult> GetNivelDificuldades()
    {
        if (_fitControlAppDbContext.NivelDificuldades is not null)
        {
            var nivelDificuldades = _fitControlAppDbContext.NivelDificuldades
                .Where(n => n.IsDeleted == false);

            if (nivelDificuldades.Any())
            {
                return Results.Ok(await nivelDificuldades.ToListAsync());
            }
        }
        return Results.NotFound();
    }
    
    [HttpGet("/niveldificuldade/{id}")]
    public async Task<IResult> GetNivelDificuldade(int id)
    {
        if (_fitControlAppDbContext.NivelDificuldades is not null)
        {
            var nivelDificuldade = await _fitControlAppDbContext.NivelDificuldades
                .FirstOrDefaultAsync(n => n.IsDeleted == false && n.Id == id);

            if (nivelDificuldade is not null)
            {
                return Results.Ok(nivelDificuldade);
            }
        }
        return Results.Ok(new NivelDificuldade());
    }

    [HttpPost("/niveldificuldade")]
    public async Task<IResult> AddNivelDificuldade([FromBody] NivelDificuldadeDto? niveldificuldade)
    {
        if (niveldificuldade is null)
        {
         return Results.BadRequest();   
        }
        
        var mapper = _mapper.Map<Models.NivelDificuldadeDto, Entities.NivelDificuldade>(niveldificuldade);

        mapper.CreatedAt = DateTime.Now;
        mapper.UpdatedAt = DateTime.Now;
        
        var niveldificuldades = _fitControlAppDbContext.NivelDificuldades;

        if (niveldificuldades is not null)
        {
            niveldificuldades.Add(mapper);
            await _fitControlAppDbContext.SaveChangesAsync();
            return Results.Ok("Nivel de dificuldade adicionado com sucesso!");
        }
        return Results.Empty;
    } 
    
    [HttpPut("/niveldificuldade")]
    public async Task<IResult> UpdateNivelDificuldade ([FromBody] NivelDificuldadeDto? niveldificuldade)
    {
        if (niveldificuldade is null)
        {
            return Results.BadRequest();
        }

        if (_fitControlAppDbContext.NivelDificuldades is null)
        {
            return Results.NotFound();
        }
        
        var oldNivelDificuldade = await _fitControlAppDbContext.NivelDificuldades.FirstOrDefaultAsync(n => n.Id == niveldificuldade.Id);

        if (oldNivelDificuldade is null)
        {
            return Results.NotFound("Nivel de dificuldade não foi encontrado!");
        }

        oldNivelDificuldade.UpdatedAt = DateTime.Now;
        niveldificuldade.Adapt(oldNivelDificuldade);
        
       try
       {
           var result = await _fitControlAppDbContext.SaveChangesAsync();

           if (result <= 0)
           {
               return Results.NotFound("Não foi possível guardar o nível de dificuldade!");
           }
       }
       catch (Exception e)
       {
           return Results.NotFound(e.Message);
       }

       return Results.Ok(niveldificuldade);
    }
    
    [HttpDelete("/niveldificuldade/softdelete/{id}")]
    public async Task<IResult> SoftDeleteNivelDificuldade(int id)
    {
        if (_fitControlAppDbContext.NivelDificuldades is not null)
        {
            var nivelDificuldade = await _fitControlAppDbContext.NivelDificuldades.FirstOrDefaultAsync(n => n.Id == id);
            if (nivelDificuldade is null)
            {
                return Results.NotFound("Nível de dificuldade não encontrado!");
            }
            
            nivelDificuldade.IsDeleted = true;
            nivelDificuldade.UpdatedAt = DateTime.Now;
            
            await _fitControlAppDbContext.SaveChangesAsync();
            return Results.Ok("Nível de dificuldade apagado com sucesso!");
        }
        return Results.Empty; 
    }
    
}