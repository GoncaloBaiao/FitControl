using FitControl.API.Data;
using FitControl.API.Entities;
using FitControl.API.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitControl.API.Controllers;

public class SocioController : ControllerBase
{
    private readonly IFitControlDbContext _fitControlAppDbContext;
    private readonly IMapper _mapper;

    public SocioController(IFitControlDbContext fitControlAppDbContext, IMapper mapper)
    {
        _fitControlAppDbContext = fitControlAppDbContext;
        _mapper = mapper;
    }
    
    [HttpGet("/socios")]
    public async Task<IResult> GetSocios()
    {
        if (_fitControlAppDbContext.Socios is null)
        {
            return Results.Ok(new List<Socio>());
        }

        var socios = await _fitControlAppDbContext.Socios
            .Include(s => s.TipoPlano)
            .Include(s => s.Genero)
            .Where(s => s.IsDeleted == false)
            .ToListAsync();

        return Results.Ok(socios);
    }
    
    [HttpGet("/socio/{id}")]
    public async Task<IResult> GetSocio(int id)
    {
        if (_fitControlAppDbContext.Socios is not null)
        {
            var socio = await _fitControlAppDbContext.Socios
                .Include(s => s.TipoPlano)
                .Include(s => s.Genero)
                .FirstOrDefaultAsync(s => s.IsDeleted == false && s.Id == id);

            if (socio is not null)
            {
                return Results.Ok(socio);
            }
        }
        return Results.Ok(new Socio());
    }

    [HttpPost("/socio")]
    public async Task<IResult> AddSocio([FromBody] SocioDto? socio)
    {
        if (socio is null)
        {
         return Results.BadRequest();   
        }

        socio.TipoPlano = null;
        socio.Genero = null;
        
        var mapper = _mapper.Map<Models.SocioDto, Entities.Socio>(socio);

        mapper.CreatedAt = DateTime.Now;
        mapper.UpdatedAt = DateTime.Now;
        
        var socios = _fitControlAppDbContext.Socios;
        
        if (socio.InicioSubscricao >= socio.FimSubscricao)
        {
            return Results.BadRequest("A data de início da subscrição deve ser menor que a data de fim.");
        }

        if (socios is not null)
        {
            socios.Add(mapper);
            await _fitControlAppDbContext.SaveChangesAsync();
            return Results.Ok("Sócio adicionado com sucesso!");
        }
        return Results.Empty;
    } 
    
    [HttpPut("/socio")]
    public async Task<IResult> UpdateSocio ([FromBody] SocioDto? socio)
    {
        if (socio is null)
        {
            return Results.BadRequest();
        }

        if (_fitControlAppDbContext.Socios is null)
        {
            return Results.NotFound();
        }
        
        var oldSocio = await _fitControlAppDbContext.Socios.FirstOrDefaultAsync(s => s.Id == socio.Id);
        
        

        if (oldSocio is null)
        {
            return Results.NotFound("Sócio não foi encontrado!");
        }
        if (oldSocio.InicioSubscricao >= oldSocio.FimSubscricao)
                 {
                     return Results.BadRequest("A data de início da subscrição deve ser menor que a data de fim.");
                 }

        oldSocio.UpdatedAt = DateTime.Now;
        socio.Adapt(oldSocio);
        
       try
       {
           var result = await _fitControlAppDbContext.SaveChangesAsync();

           if (result <= 0)
           {
               return Results.NotFound("Não foi possível guardar o sócio!");
           }
       }
       catch (Exception e)
       {
           return Results.NotFound(e.Message);
       }

       return Results.Ok(socio);
    }
    
    [HttpDelete("/socio/softdelete/{id}")]
    public async Task<IResult> DeleteSocio(int id)
    {
        if (_fitControlAppDbContext.Socios is not null)
        {
            var socio = await _fitControlAppDbContext.Socios.FirstOrDefaultAsync(s => s.Id == id);
            if (socio is null)
            {
                return Results.NotFound("Sócio não encontrado!");
            }
            
            socio.IsDeleted = true;
            socio.UpdatedAt = DateTime.Now;
            
            await _fitControlAppDbContext.SaveChangesAsync();
            return Results.Ok("Sócio apagado com sucesso!");
        }
        return Results.Empty; 
    }
    
}