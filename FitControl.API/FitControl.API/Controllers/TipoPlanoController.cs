using FitControl.API.Data;
using FitControl.API.Entities;
using FitControl.API.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitControl.API.Controllers;

public class TipoPlanoController : ControllerBase
{
    private readonly IFitControlDbContext _fitControlAppDbContext;
    private readonly IMapper _mapper;

    public TipoPlanoController(IFitControlDbContext fitControlAppDbContext, IMapper mapper)
    {
        _fitControlAppDbContext = fitControlAppDbContext;
        _mapper = mapper;
    }
    
    [HttpGet("/tipoplanos")]
    public async Task<IResult> GetTipoPlanos()
    {
        if (_fitControlAppDbContext.TipoPlanos is not null)
        {
            var tipoplanos = _fitControlAppDbContext.TipoPlanos
                .Where(t => t.IsDeleted == false);

            if (tipoplanos.Any())
            {
                return Results.Ok(await tipoplanos.ToListAsync());
            }
        }
        return Results.NotFound();
    }
    
    [HttpGet("/tipoplano/{id}")]
    public async Task<IResult> GetTipoPlano(int id)
    {
        if (_fitControlAppDbContext.TipoPlanos is not null)
        {
            var tipoplno = await _fitControlAppDbContext.TipoPlanos
                .FirstOrDefaultAsync(t => t.IsDeleted == false && t.Id == id);

            if (tipoplno is not null)
            {
                return Results.Ok(tipoplno);
            }
        }
        return Results.Ok(new Socio());
    }

    [HttpPost("/tipoplano")]
    public async Task<IResult> AddTipoPlano([FromBody] TipoPlanoDto? tipoplano)
    {
        if (tipoplano is null)
        {
         return Results.BadRequest();   
        }
        
        var mapper = _mapper.Map<Models.TipoPlanoDto, Entities.TipoPlano>(tipoplano);

        mapper.CreatedAt = DateTime.Now;
        mapper.UpdatedAt = DateTime.Now;
        
        var tipoplanos = _fitControlAppDbContext.TipoPlanos;

        if (tipoplanos is not null)
        {
            tipoplanos.Add(mapper);
            await _fitControlAppDbContext.SaveChangesAsync();
            return Results.Ok("Tipo de plano adicionado com sucesso!");
        }
        return Results.Empty;
    } 
    
    [HttpPut("/tipoplano")]
    public async Task<IResult> UpdateTipoPlano ([FromBody] TipoPlanoDto? tipoplano)
    {
        if (tipoplano is null)
        {
            return Results.BadRequest();
        }

        if (_fitControlAppDbContext.TipoPlanos is null)
        {
            return Results.NotFound();
        }
        
        var oldTipoPlano = await _fitControlAppDbContext.TipoPlanos.FirstOrDefaultAsync(t => t.Id == tipoplano.Id);

        if (oldTipoPlano is null)
        {
            return Results.NotFound("Tipo de plano não foi encontrado!");
        }

        oldTipoPlano.UpdatedAt = DateTime.Now;
        tipoplano.Adapt(oldTipoPlano);
        
       try
       {
           var result = await _fitControlAppDbContext.SaveChangesAsync();

           if (result <= 0)
           {
               return Results.NotFound("Não foi possível guardar o tipo de plano!");
           }
       }
       catch (Exception e)
       {
           return Results.NotFound(e.Message);
       }

       return Results.Ok(tipoplano);
    }
    
    [HttpDelete("/tipoplano/softdelete/{id}")]
    public async Task<IResult> SoftDeleteTipoPlano(int id)
    {
        if (_fitControlAppDbContext.TipoPlanos is not null)
        {
            var tipoplano = await _fitControlAppDbContext.TipoPlanos.FirstOrDefaultAsync(t => t.Id == id);
            if (tipoplano is null)
            {
                return Results.NotFound("Tipo de plano não encontrado!");
            }
            
            tipoplano.IsDeleted = true;
            tipoplano.UpdatedAt = DateTime.Now;
            
            await _fitControlAppDbContext.SaveChangesAsync();
            return Results.Ok("Tipo de plano apagado com sucesso!");
        }
        return Results.Empty; 
    }
    
}