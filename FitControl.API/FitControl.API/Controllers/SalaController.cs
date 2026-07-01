using FitControl.API.Data;
using FitControl.API.Entities;
using FitControl.API.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitControl.API.Controllers;

public class SalaController : ControllerBase
{
    private readonly IFitControlDbContext _fitControlAppDbContext;
    private readonly IMapper _mapper;

    public SalaController(IFitControlDbContext fitControlAppDbContext, IMapper mapper)
    {
        _fitControlAppDbContext = fitControlAppDbContext;
        _mapper = mapper;
    }
    
    [HttpGet("/salas")]
    public async Task<IResult> GetSalas()
    {
        if (_fitControlAppDbContext.Salas is not null)
        {
            var salas = _fitControlAppDbContext.Salas
                .Where(s => s.IsDeleted == false);

            if (salas.Any())
            {
                return Results.Ok(await salas.ToListAsync());
            }
        }
        return Results.NotFound();
    }
    
    [HttpGet("/sala/{id}")]
    public async Task<IResult> GetSala(int id)
    {
        if (_fitControlAppDbContext.Salas is not null)
        {
            var sala = await _fitControlAppDbContext.Salas
                .FirstOrDefaultAsync(s => s.IsDeleted == false && s.Id == id);

            if (sala is not null)
            {
                return Results.Ok(sala);
            }
        }
        return Results.Ok(new Sala());
    }

    [HttpPost("/sala")]
    public async Task<IResult> AddSala([FromBody] SalaDto? sala)
    {
        if (sala is null)
        {
         return Results.BadRequest();   
        }
        
        var mapper = _mapper.Map<Models.SalaDto, Entities.Sala>(sala);

        mapper.CreatedAt = DateTime.Now;
        mapper.UpdatedAt = DateTime.Now;
        
        var salas = _fitControlAppDbContext.Salas;

        if (salas is not null)
        {
            salas.Add(mapper);
            await _fitControlAppDbContext.SaveChangesAsync();
            return Results.Ok("Sala adicionado com sucesso!");
        }
        return Results.Empty;
    } 
    
    [HttpPut("/sala")]
    public async Task<IResult> UpdateSala ([FromBody] SalaDto? sala)
    {
        if (sala is null)
        {
            return Results.BadRequest();
        }

        if (_fitControlAppDbContext.Salas is null)
        {
            return Results.NotFound();
        }
        
        var oldSala = await _fitControlAppDbContext.Salas.FirstOrDefaultAsync(s => s.Id == sala.Id);

        if (oldSala is null)
        {
            return Results.NotFound("Sala não foi encontrada!");
        }

        oldSala.UpdatedAt = DateTime.Now;
        sala.Adapt(oldSala);
        
       try
       {
           var result = await _fitControlAppDbContext.SaveChangesAsync();

           if (result <= 0)
           {
               return Results.NotFound("Não foi possível guardar a sala!");
           }
       }
       catch (Exception e)
       {
           return Results.NotFound(e.Message);
       }

       return Results.Ok(sala);
    }
    
    [HttpDelete("/sala/softdelete/{id}")]
    public async Task<IResult> SoftDeleteSala(int id)
    {
        if (_fitControlAppDbContext.Salas is not null)
        {
            var sala = await _fitControlAppDbContext.Salas.FirstOrDefaultAsync(s => s.Id == id);
            if (sala is null)
            {
                return Results.NotFound("Sala não encontrada!");
            }
            
            sala.IsDeleted = true;
            sala.UpdatedAt = DateTime.Now;
            
            await _fitControlAppDbContext.SaveChangesAsync();
            return Results.Ok("Sala apagada com sucesso!");
        }
        return Results.Empty; 
    }
    
}