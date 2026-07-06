using FitControl.API.Data;
using FitControl.API.Entities;
using FitControl.API.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitControl.API.Controllers;

public class InstrutorController : ControllerBase
{
    private readonly IFitControlDbContext _fitControlDbContext;
    private readonly IMapper _mapper;

    public InstrutorController(IFitControlDbContext fitControlAppDbContext, IMapper mapper)
    {
        _fitControlDbContext = fitControlAppDbContext;
        _mapper = mapper;
    }
    
    [HttpGet("/instrutors")]
    public async Task<IResult> GetInstrutors()
    {
        if (_fitControlDbContext is not null)
        {
            var instrutor=_fitControlDbContext.Instrutors
                .Where(i=>i.IsDeleted ==  false);
            if (instrutor.Any())
            {
                return Results.Ok(await instrutor.ToListAsync());
            }
        }

        return Results.Ok(new List<Instrutor>());
    }

    [HttpGet("/instrutor/{id}")]
    public async Task<IResult> GetInstrutor(int id)
    {
        if (_fitControlDbContext is not null)
        {
            var instrutor=_fitControlDbContext.Instrutors
                .FirstOrDefaultAsync(i => i.IsDeleted == false && i.Id == id);
            if (instrutor is not null)
            {
                return Results.Ok(await instrutor);
            }
        }
        return Results.Ok(new Instrutor());
    }

    [HttpPost("/instrutor")]
    public async Task<IResult> AddInstrutor([FromBody] InstrutorDto instrutor)
    {
        if (instrutor is null)
        {
            return Results.BadRequest();
        }
        
        var mapper = _mapper.Map<Models.InstrutorDto, Entities.Instrutor>(instrutor);

        mapper.CreatedAt = DateTime.Now;
        mapper.UpdatedAt = DateTime.Now;
        
        var instrutors = _fitControlDbContext.Instrutors;

        if (instrutors is not null)
        {
            instrutors.Add(mapper);
            await _fitControlDbContext.SaveChangesAsync();
            return Results.Ok("Instrutor adicionado com sucesso!");
        }
        return Results.Empty;
    }
    
    [HttpPut("/instrutor")]
    public async Task<IResult> UpdateInstrutor ([FromBody] InstrutorDto? instrutorDto)
    {
        if (instrutorDto is null)
        {
            return Results.BadRequest();
        }

        if (_fitControlDbContext.Instrutors is null)
        {
            return Results.NotFound();
        }
        
        var oldInstutor = await _fitControlDbContext.Instrutors.FirstOrDefaultAsync(p => p.Id == instrutorDto.Id);

        if (oldInstutor is null)
        {
            return Results.NotFound("Instrutor não foi encontrado!");
        }

        instrutorDto.Adapt(oldInstutor);
        
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

        return Results.Ok(instrutorDto);
    } 
    
    [HttpDelete("/instrutor/softdelete/{id}")]
    public async Task<IResult> SoftDeleteInstrutors(int id)
    {
        if (_fitControlDbContext.Instrutors is not null)
        {
            var instrutor = await _fitControlDbContext.Instrutors.FirstOrDefaultAsync(i => i.Id == id);
            if (instrutor is null)
            {
                return Results.NotFound("Instrutor não encontrado");
            }
            
            instrutor.IsDeleted = true;
            instrutor.UpdatedAt = DateTime.Now;
            
            await _fitControlDbContext.SaveChangesAsync();
            return Results.Ok("Instrutor apagado com sucesso!");
        }
        return Results.Empty; 
    }
    
    

}

