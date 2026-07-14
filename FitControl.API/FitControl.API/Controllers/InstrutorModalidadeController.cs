using FitControl.API.Data;
using FitControl.API.Entities;
using FitControl.API.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitControl.API.Controllers;

public class InstrutorModalidadeController : ControllerBase
{
    private readonly IFitControlDbContext _fitControlDbContext;
    private readonly IMapper _mapper;

    public InstrutorModalidadeController(IFitControlDbContext fitControlAppDbContext, IMapper mapper)
    {
        _fitControlDbContext = fitControlAppDbContext;
        _mapper = mapper;
    }
    
    [HttpGet("/instrutormodalidades")]
    public async Task<IResult> GetInstrutorModalidades()
    {
        if (_fitControlDbContext is not null)
        {
            var instrutormodalidade=_fitControlDbContext.InstrutorModalidades
                .Include(x=>x.Instrutor)
                .Include(x=>x.Modalidade)
                .Where(i=>i.IsDeleted ==  false && i.Instrutor.IsDeleted == false && i.Modalidade.IsDeleted == false);
            if (instrutormodalidade.Any())
            {
                return Results.Ok(await instrutormodalidade.ToListAsync());
            }
        }

        return Results.Ok(new List<InstrutorModalidade>());
    }

    [HttpGet("/instrutormodalidade/{id}")]
    public async Task<IResult> GetInstrutorModalidade(int id)
    {
        if (_fitControlDbContext is not null)
        {
            var instrutormodalidade= await _fitControlDbContext.InstrutorModalidades
                .Include(x=>x.Instrutor)
                .Include(x=>x.Modalidade)
                .FirstOrDefaultAsync(i => (i.IsDeleted == false || i.Instrutor.IsDeleted == false || i.Modalidade.IsDeleted == false) && i.Id == id);
            if (instrutormodalidade is not null)
            {
                return Results.Ok(instrutormodalidade);
            }
        }
        return Results.Ok(new InstrutorModalidade());
    }

    [HttpPost("/instrutormodalidade")]
    public async Task<IResult> AddInscricao([FromBody] InstrutorModalidadeDto instrutormodalidade)
    {
        if (instrutormodalidade is null)
        {
            return Results.BadRequest();
        }

        instrutormodalidade.Instrutor = null;
        instrutormodalidade.Modalidade = null;
        
        var mapper = _mapper.Map<Models.InstrutorModalidadeDto, Entities.InstrutorModalidade>(instrutormodalidade);

        mapper.CreatedAt = DateTime.Now;
        mapper.UpdatedAt = DateTime.Now;
        
        var instrutormodalidades = _fitControlDbContext.InstrutorModalidades;

        if (instrutormodalidades is not null)
        {
            instrutormodalidades.Add(mapper);
            await _fitControlDbContext.SaveChangesAsync();
            return Results.Ok("Modalidade do instrutor adicionado com sucesso!");
        }
        return Results.Empty;
    }
    
    [HttpPut("/instrutormodalidade")]
    public async Task<IResult> UpdateInstrutorModalidade ([FromBody] InstrutorModalidadeDto? instrutormodalidadeDto)
    {
        if (instrutormodalidadeDto is null)
        {
            return Results.BadRequest();
        }
        
        instrutormodalidadeDto.Instrutor = null;
        instrutormodalidadeDto.Modalidade = null;

        if (_fitControlDbContext.InstrutorModalidades is null)
        {
            return Results.NotFound();
        }
        
        var oldInstrutormodalidade = await _fitControlDbContext.InstrutorModalidades.FirstOrDefaultAsync(p => p.Id == instrutormodalidadeDto.Id);

        if (oldInstrutormodalidade is null)
        {
            return Results.NotFound("Modalidade do instrutor não foi encontrado!");
        }

        instrutormodalidadeDto.Adapt(oldInstrutormodalidade);
        
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

        return Results.Ok(instrutormodalidadeDto);
    }
    
    [HttpDelete("/instrutormodalidade/softdelete/{id}")]
    public async Task<IResult> SoftDeleteInstrutorModalidade(int id)
    {
        if (_fitControlDbContext.InstrutorModalidades is not null)
        {
            var instrutormodalidade = await _fitControlDbContext.InstrutorModalidades.FirstOrDefaultAsync(i => i.Id == id);
            if (instrutormodalidade is null)
            {
                return Results.NotFound("Modalidade do instrutor não encontrado");
            }
            
            instrutormodalidade.IsDeleted = true;
            instrutormodalidade.UpdatedAt = DateTime.Now;
            
            await _fitControlDbContext.SaveChangesAsync();
            return Results.Ok("Modalidade do instrutor apagado com sucesso!");
        }
        return Results.Empty; 
    }
    
    // [HttpGet("/instrutormodalidade")]
    // public async Task<IActionResult> GetInstrutor()
    // {
    //     if (_fitControlDbContext.Instrutors is not null)
    //     {
    //         var instrutors = await _fitControlDbContext.Instrutors.ToListAsync();
    //
    //         if (instrutors.Any())
    //         {
    //             return Ok(instrutors);
    //         }
    //     }
    //     return NotFound();
    // }
    // [HttpGet("/modalidade")]
    // public async Task<IActionResult> GetModalidade()
    // {
    //     if (_fitControlDbContext.Modalidades is not null)
    //     {
    //         var modalidades = await _fitControlDbContext.Modalidades.ToListAsync();
    //
    //         if (modalidades.Any())
    //         {
    //             return Ok(modalidades);
    //         }
    //     }
    //     return NotFound();
    // }

}

