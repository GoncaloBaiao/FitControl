using FitControl.API.Data;
using FitControl.API.Entities;
using FitControl.API.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitControl.API.Controllers;

public class GeneroController : ControllerBase
{
    private readonly IFitControlDbContext _fitControlDbContext;
    private readonly IMapper _mapper;
    
    public GeneroController(IFitControlDbContext fitControlDbContext, IMapper mapper)
    {
        _fitControlDbContext = fitControlDbContext;
        _mapper = mapper;
    }
    
    [HttpGet("/generos")]
    public async Task<IResult> GetGeneros()
    {
        if (_fitControlDbContext.Generos is not null)
        {
            var generos = _fitControlDbContext.Generos
                .Where(g => g.IsDeleted ==  false);
            
            if (generos.Any())
            {
                return Results.Ok(await generos.ToListAsync());
            }
        }

        return Results.Ok(new List<Genero>());
    }
    
    [HttpGet("/genero/{id}")]
    public async Task<IResult> GetGenero(int id)
    {
        if (_fitControlDbContext != null)
        {
            var Genero = await _fitControlDbContext.Generos
                .FirstOrDefaultAsync(g => g.IsDeleted == false && g.Id == id);

            if (Genero is not null)
            {
                return Results.Ok(Genero);
            }
        }
        return Results.Ok(new Genero());
    }
    
    [HttpPost("genero")]
    public async Task<IResult> AddGenero([FromBody] GeneroDto? genero)
    {
        if (genero is null)
        {
            return Results.BadRequest();   
        }
        
        var mapper = _mapper.Map<Models.GeneroDto, Entities.Genero>(genero);

        mapper.CreatedAt = DateTime.Now;
        mapper.UpdatedAt = DateTime.Now;
        
        var generos = _fitControlDbContext.Generos;

        if (generos is not null)
        {
            generos.Add(mapper);
            await _fitControlDbContext.SaveChangesAsync();
            return Results.Ok("Género adicionado com sucesso!");
        }
        return Results.Empty;
    }
    
    [HttpPut("/genero")]
    public async Task<IResult> UpdateGenero ([FromBody] GeneroDto? generoDto)
    {
        if (generoDto is null)
        {
            return Results.BadRequest();
        }

        if (_fitControlDbContext.Generos is null)
        {
            return Results.NotFound();
        }
        
        var oldGenero = await _fitControlDbContext.Generos.FirstOrDefaultAsync(g => g.Id == generoDto.Id);

        if (oldGenero is null)
        {
            return Results.NotFound("Género não foi encontrado!");
        }

        generoDto.Adapt(oldGenero);
        
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

        return Results.Ok(generoDto);
    }
    
    [HttpDelete("/genero/softdelete/{id}")]
    public async Task<IResult> DeleteGenero(int id)
    {
        if (_fitControlDbContext.Generos is not null)
        {
            var genero = await _fitControlDbContext.Generos.FirstOrDefaultAsync(g => g.Id == id);
            if (genero is null)
            {
                return Results.NotFound("Género não encontrado");
            }
            
            genero.IsDeleted = true;
            genero.UpdatedAt = DateTime.Now;
            
            await _fitControlDbContext.SaveChangesAsync();
            return Results.Ok("Género apagado com sucesso!");
        }
        return Results.Empty; 
    }
}