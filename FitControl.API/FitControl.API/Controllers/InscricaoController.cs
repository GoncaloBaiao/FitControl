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
    private readonly IFitControlDbContext _fitcontrolDbContext;
    private readonly IMapper _mapper;

    [HttpGet("/inscricao")]
    public async Task<List<Inscricao>> GetInscricao()
    {
        if (_fitcontrolDbContext is not null)
        {
            var inscricao=_fitcontrolDbContext.Inscricaos
                .Include(x=>x.Aula)
                .Include(x=>x.Socio)
                .Where(i=>i.IsDeleted ==  false || i.Aula.IsDeleted == false || i.Socio.IsDeleted == false);
            if (inscricao.Any())
            {
                return await inscricao.ToListAsync();
            }
        }

        return new List<Inscricao>();
    }

    [HttpGet("/inscricaos/{id}")]
    public async Task<Inscricao> GetInscricao(int id)
    {
        if (_fitcontrolDbContext is not null)
        {
            var inscricao= await _fitcontrolDbContext.Inscricaos
                .Include(x=>x.Aula)
                .Include(x=>x.Socio)
                .FirstOrDefaultAsync(i => (i.IsDeleted == false || i.Aula.IsDeleted == false || i.Socio.IsDeleted == false) && i.Id == id);
            if (inscricao is not null)
            {
                return inscricao;
            }
        }
        return new Inscricao();
    }

    [HttpPost("/inscricaos")]
    public async Task<IResult> AddInscricao([FromBody] InscricaoDto inscricao)
    {
        if (inscricao is null)
        {
            return Results.BadRequest();
        }

        inscricao.Aula = null;
        inscricao.Socio = null;
        
        var mapper = _mapper.Map<Models.InscricaoDto, Entities.Inscricao>(inscricao);

        mapper.CreatedAt = DateTime.Now;
        mapper.UpdatedAt = DateTime.Now;
        
        var inscricaos = _fitcontrolDbContext.Inscricaos;

        if (inscricaos is not null)
        {
            inscricaos.Add(mapper);
            await _fitcontrolDbContext.SaveChangesAsync();
            return Results.Ok("Inscricao adicionada com sucesso!");
        }
        return Results.Empty;
    }
    [HttpPut("/inscricao")]
    public async Task<IActionResult> UpdateInscricao ([FromBody] InscricaoDto? inscricaoDto)
    {
        if (inscricaoDto is null)
        {
            return BadRequest();
        }
        
        inscricaoDto.Aula = null;
        inscricaoDto.Socio = null;

        if (_fitcontrolDbContext.Inscricaos is null)
        {
            return NotFound();
        }
        
        var oldInscricao = await _fitcontrolDbContext.Inscricaos.FirstOrDefaultAsync(p => p.Id == inscricaoDto.Id);

        if (oldInscricao is null)
        {
            return NotFound("Inscricao não foi encontrada!");
        }

        inscricaoDto.Adapt(oldInscricao);
        
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

        return Ok(inscricaoDto);
    }
    
    [HttpDelete("/inscricao/softdelete/{id}")]
    public async Task<IResult> SoftDeleteInscricao(int id)
    {
        if (_fitcontrolDbContext.Inscricaos is not null)
        {
            var inscricao = await _fitcontrolDbContext.Inscricaos.FirstOrDefaultAsync(i => i.Id == id);
            if (inscricao is null)
            {
                return Results.NotFound("Inscricao não encontrada");
            }
            
            inscricao.IsDeleted = true;
            inscricao.UpdatedAt = DateTime.Now;
            
            await _fitcontrolDbContext.SaveChangesAsync();
            return Results.Ok("Inscricao apagada com sucesso!");
        }
        return Results.Empty; 
    }
    
    [HttpGet("/aula")]
    public async Task<IActionResult> GetAula()
    {
        if (_fitcontrolDbContext.Aulas is not null)
        {
            var aulas = await _fitcontrolDbContext.Aulas.ToListAsync();

            if (aulas.Any())
            {
                return Ok(aulas);
            }
        }
        return NotFound();
    }
    [HttpGet("/socios")]
    public async Task<IActionResult> GetSocios()
    {
        if (_fitcontrolDbContext.Socios is not null)
        {
            var socios = await _fitcontrolDbContext.Socios.ToListAsync();

            if (socios.Any())
            {
                return Ok(socios);
            }
        }
        return NotFound();
    }

}

