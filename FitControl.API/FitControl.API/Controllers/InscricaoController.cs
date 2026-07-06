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
    private readonly IFitControlDbContext _fitControlDbContext;
    private readonly IMapper _mapper;

    public InscricaoController(IFitControlDbContext fitControlAppDbContext, IMapper mapper)
    {
        _fitControlDbContext = fitControlAppDbContext;
        _mapper = mapper;
    }
    
    [HttpGet("/inscricaos")]
    public async Task<IResult> GetInscricaos()
    {
        if (_fitControlDbContext is not null)
        {
            var inscricao=_fitControlDbContext.Inscricaos
                .Include(x=>x.Aula)
                .Include(x=>x.Socio)
                .Where(i=>i.IsDeleted ==  false || i.Aula.IsDeleted == false || i.Socio.IsDeleted == false);
            if (inscricao.Any())
            {
                return Results.Ok(await inscricao.ToListAsync());
            }
        }

        return Results.Ok(new List<Inscricao>());
    }

    [HttpGet("/inscricao/{id}")]
    public async Task<Inscricao> GetInscricao(int id)
    {
        if (_fitControlDbContext is not null)
        {
            var inscricao= await _fitControlDbContext.Inscricaos
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

    [HttpPost("/inscricao")]
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
        
        var inscricaos = _fitControlDbContext.Inscricaos;

        if (inscricaos is not null)
        {
            inscricaos.Add(mapper);
            await _fitControlDbContext.SaveChangesAsync();
            return Results.Ok("Inscricao adicionada com sucesso!");
        }
        return Results.Empty;
    }
    
    [HttpPut("/inscricao")]
    public async Task<IResult> UpdateInscricao ([FromBody] InscricaoDto? inscricaoDto)
    {
        if (inscricaoDto is null)
        {
            return Results.BadRequest();
        }
        
        inscricaoDto.Aula = null;
        inscricaoDto.Socio = null;

        if (_fitControlDbContext.Inscricaos is null)
        {
            return Results.NotFound();
        }
        
        var oldInscricao = await _fitControlDbContext.Inscricaos.FirstOrDefaultAsync(p => p.Id == inscricaoDto.Id);

        if (oldInscricao is null)
        {
            return Results.NotFound("Inscricao não foi encontrada!");
        }

        inscricaoDto.Adapt(oldInscricao);
        
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

        return Results.Ok(inscricaoDto);
    }
    
    [HttpDelete("/inscricao/softdelete/{id}")]
    public async Task<IResult> SoftDeleteInscricao(int id)
    {
        if (_fitControlDbContext.Inscricaos is not null)
        {
            var inscricao = await _fitControlDbContext.Inscricaos.FirstOrDefaultAsync(i => i.Id == id);
            if (inscricao is null)
            {
                return Results.NotFound("Inscricao não encontrada");
            }
            
            inscricao.IsDeleted = true;
            inscricao.UpdatedAt = DateTime.Now;
            
            await _fitControlDbContext.SaveChangesAsync();
            return Results.Ok("Inscricao apagada com sucesso!");
        }
        return Results.Empty; 
    }
    
    // [HttpGet("/aula")]
    // public async Task<IActionResult> GetAula()
    // {
    //     if (_fitControlDbContext.Aulas is not null)
    //     {
    //         var aulas = await _fitControlDbContext.Aulas.ToListAsync();
    //
    //         if (aulas.Any())
    //         {
    //             return Ok(aulas);
    //         }
    //     }
    //     return NotFound();
    // }
    // [HttpGet("/socios")]
    // public async Task<IActionResult> GetSocios()
    // {
    //     if (_fitControlDbContext.Socios is not null)
    //     {
    //         var socios = await _fitControlDbContext.Socios.ToListAsync();
    //
    //         if (socios.Any())
    //         {
    //             return Ok(socios);
    //         }
    //     }
    //     return NotFound();
    // }

}

