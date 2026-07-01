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
    private readonly IFitControlDbContext _fitcontrolDbContext;
    private readonly IMapper _mapper;

    [HttpGet("/aulas")]
    public async Task<List<Aula>> GetAulas()
    {
        if (_fitcontrolDbContext is not null)
        {
            var aulas=_fitcontrolDbContext.Aulas
                .Include(x=>x.Sala)
                .Include(x=>x.Instrutor)
                .Include(x=>x.Modalidade)
                .Where(a=>a.IsDeleted ==  false || a.Sala.IsDeleted == false || a.Instrutor.IsDeleted == false || a.Modalidade.IsDeleted == false);
            if (aulas.Any())
            {
                return await aulas.ToListAsync();
            }
        }

        return new List<Aula>();
    }

    [HttpGet("/aulas/{id}")]
    public async Task<Aula> GetAula(int id)
    {
        if (_fitcontrolDbContext is not null)
        {
            var aula= await _fitcontrolDbContext.Aulas
                .Include(x=>x.Sala)
                .Include(x=>x.Instrutor)
                .Include(x=>x.Modalidade)
                .FirstOrDefaultAsync(a => (a.IsDeleted == false || a.Sala.IsDeleted == false || a.Instrutor.IsDeleted == false || a.Modalidade.IsDeleted == false) && a.Id == id);
            if (aula is not null)
            {
                return aula;
            }
        }
        return new Aula();
    }

    [HttpPost("/aulas")]
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
        
        var aulas = _fitcontrolDbContext.Aulas;

        if (aulas is not null)
        {
            aulas.Add(mapper);
            await _fitcontrolDbContext.SaveChangesAsync();
            return Results.Ok("Aula adicionada com sucesso!");
        }
        return Results.Empty;
    }
    [HttpPut("/aula")]
    public async Task<IActionResult> UpdateAula ([FromBody] AulaDto? aulaDto)
    {
        if (aulaDto is null)
        {
            return BadRequest();
        }
        
        aulaDto.Sala = null;
        aulaDto.Instrutor = null;
        aulaDto.Modalidade = null;

        if (_fitcontrolDbContext.Aulas is null)
        {
            return NotFound();
        }
        
        var oldAula = await _fitcontrolDbContext.Aulas.FirstOrDefaultAsync(p => p.Id == aulaDto.Id);

        if (oldAula is null)
        {
            return NotFound("Aula não foi encontrada!");
        }

        aulaDto.Adapt(oldAula);
        
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

        return Ok(aulaDto);
    }
    
    [HttpDelete("/aula/softdelete/{id}")]
    public async Task<IResult> SoftDeleteAula(int id)
    {
        if (_fitcontrolDbContext.Aulas is not null)
        {
            var aula = await _fitcontrolDbContext.Aulas.FirstOrDefaultAsync(p => p.Id == id);
            if (aula is null)
            {
                return Results.NotFound("Aula não encontrada");
            }
            
            aula.IsDeleted = true;
            aula.UpdatedAt = DateTime.Now;
            
            await _fitcontrolDbContext.SaveChangesAsync();
            return Results.Ok("Aula apagada com sucesso!");
        }
        return Results.Empty; 
    }
    
    [HttpGet("/sala")]
    public async Task<IActionResult> GetSala()
    {
        if (_fitcontrolDbContext.Salas is not null)
        {
            var salas = await _fitcontrolDbContext.Aulas.ToListAsync();

            if (salas.Any())
            {
                return Ok(salas);
            }
        }
        return NotFound();
    }
    [HttpGet("/instrutor")]
    public async Task<IActionResult> GetInstrutor()
    {
        if (_fitcontrolDbContext.Instrutors is not null)
        {
            var Instrutors = await _fitcontrolDbContext.Instrutors.ToListAsync();

            if (Instrutors.Any())
            {
                return Ok(Instrutors);
            }
        }
        return NotFound();
    }
    [HttpGet("/modalidade")]
    public async Task<IActionResult> GetModalidade()
    {
        if (_fitcontrolDbContext.Modalidades is not null)
        {
            var Modalidades = await _fitcontrolDbContext.Modalidades.ToListAsync();

            if (Modalidades.Any())
            {
                return Ok(Modalidades);
            }
        }
        return NotFound();
    }

}

