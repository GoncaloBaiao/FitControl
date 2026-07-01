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
    private readonly IFitControlDbContext _fitcontrolDbContext;
    private readonly IMapper _mapper;

    [HttpGet("/instrutor")]
    public async Task<List<Instrutor>> GetInstrutors()
    {
        if (_fitcontrolDbContext is not null)
        {
            var instrutor=_fitcontrolDbContext.Instrutors
                .Where(i=>i.IsDeleted ==  false);
            if (instrutor.Any())
            {
                return await instrutor.ToListAsync();
            }
        }

        return new List<Instrutor>();
    }

    [HttpGet("/instrutors/{id}")]
    public async Task<Instrutor> GetInstrutor(int id)
    {
        if (_fitcontrolDbContext is not null)
        {
            var instrutor=_fitcontrolDbContext.Instrutors
                .FirstOrDefaultAsync(i => i.IsDeleted == false && i.Id == id);
            if (instrutor is not null)
            {
                return await instrutor;
            }
        }
        return new Instrutor();
    }

    [HttpPost("/instrutors")]
    public async Task<IResult> AddInstrutor([FromBody] InstrutorDto instrutor)
    {
        if (instrutor is null)
        {
            return Results.BadRequest();
        }
        
        var mapper = _mapper.Map<Models.InstrutorDto, Entities.Instrutor>(instrutor);

        mapper.CreatedAt = DateTime.Now;
        mapper.UpdatedAt = DateTime.Now;
        
        var instrutors = _fitcontrolDbContext.Instrutors;

        if (instrutors is not null)
        {
            instrutors.Add(mapper);
            await _fitcontrolDbContext.SaveChangesAsync();
            return Results.Ok("Instrutor adicionado com sucesso!");
        }
        return Results.Empty;
    }
    [HttpPut("/instrutor")]
    public async Task<IActionResult> UpdateInstrutor ([FromBody] InstrutorDto? instrutorDto)
    {
        if (instrutorDto is null)
        {
            return BadRequest();
        }

        if (_fitcontrolDbContext.Instrutors is null)
        {
            return NotFound();
        }
        
        var oldInstutor = await _fitcontrolDbContext.Instrutors.FirstOrDefaultAsync(p => p.Id == instrutorDto.Id);

        if (oldInstutor is null)
        {
            return NotFound("Instrutor não foi encontrado!");
        }

        instrutorDto.Adapt(oldInstutor);
        
        try
        {
            var result = await _fitcontrolDbContext.SaveChangesAsync();

            if (result <= 0)
            {
                return NotFound("Não foi possível guardar os dados");
            }
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }

        return Ok(instrutorDto);
    }
    
    [HttpDelete("/instrutor/softdelete/{id}")]
    public async Task<IResult> SoftDeleteInstrutors(int id)
    {
        if (_fitcontrolDbContext.Instrutors is not null)
        {
            var instrutor = await _fitcontrolDbContext.Instrutors.FirstOrDefaultAsync(i => i.Id == id);
            if (instrutor is null)
            {
                return Results.NotFound("Instrutor não encontrado");
            }
            
            instrutor.IsDeleted = true;
            instrutor.UpdatedAt = DateTime.Now;
            
            await _fitcontrolDbContext.SaveChangesAsync();
            return Results.Ok("Instrutor apagado com sucesso!");
        }
        return Results.Empty; 
    }
    
    

}

