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
    private readonly IFitControlDbContext _fitcontrolDbContext;
    private readonly IMapper _mapper;

    [HttpGet("/instrutormodalidade")]
    public async Task<List<InstrutorModalidade>> GetInstrutorModalidade()
    {
        if (_fitcontrolDbContext is not null)
        {
            var instrutormodalidade=_fitcontrolDbContext.InstrutorModalidades
                .Include(x=>x.Instrutor)
                .Include(x=>x.Modalidade)
                .Where(i=>i.IsDeleted ==  false || i.Instrutor.IsDeleted == false || i.Modalidade.IsDeleted == false);
            if (instrutormodalidade.Any())
            {
                return await instrutormodalidade.ToListAsync();
            }
        }

        return new List<InstrutorModalidade>();
    }

    [HttpGet("/instrutormodalidades/{id}")]
    public async Task<InstrutorModalidade> GetInstrutorModalidade(int id)
    {
        if (_fitcontrolDbContext is not null)
        {
            var instrutormodalidade= await _fitcontrolDbContext.InstrutorModalidades
                .Include(x=>x.Instrutor)
                .Include(x=>x.Modalidade)
                .FirstOrDefaultAsync(i => (i.IsDeleted == false || i.Instrutor.IsDeleted == false || i.Modalidade.IsDeleted == false) && i.Id == id);
            if (instrutormodalidade is not null)
            {
                return instrutormodalidade;
            }
        }
        return new InstrutorModalidade();
    }

    [HttpPost("/instrutormodalidades")]
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
        
        var instrutormodalidades = _fitcontrolDbContext.InstrutorModalidades;

        if (instrutormodalidades is not null)
        {
            instrutormodalidades.Add(mapper);
            await _fitcontrolDbContext.SaveChangesAsync();
            return Results.Ok("Modalidade do instrutor adicionado com sucesso!");
        }
        return Results.Empty;
    }
    [HttpPut("/instrutormodalidade")]
    public async Task<IActionResult> UpdateInstrutorModalidade ([FromBody] InstrutorModalidadeDto? instrutormodalidadeDto)
    {
        if (instrutormodalidadeDto is null)
        {
            return BadRequest();
        }
        
        instrutormodalidadeDto.Instrutor = null;
        instrutormodalidadeDto.Modalidade = null;

        if (_fitcontrolDbContext.InstrutorModalidades is null)
        {
            return NotFound();
        }
        
        var oldInstrutormodalidade = await _fitcontrolDbContext.InstrutorModalidades.FirstOrDefaultAsync(p => p.Id == instrutormodalidadeDto.Id);

        if (oldInstrutormodalidade is null)
        {
            return NotFound("Modalidade do instrutor não foi encontrado!");
        }

        instrutormodalidadeDto.Adapt(oldInstrutormodalidade);
        
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

        return Ok(instrutormodalidadeDto);
    }
    
    [HttpDelete("/instrutormodalidade/softdelete/{id}")]
    public async Task<IResult> SoftDeleteInstrutorModalidade(int id)
    {
        if (_fitcontrolDbContext.InstrutorModalidades is not null)
        {
            var instrutormodalidade = await _fitcontrolDbContext.InstrutorModalidades.FirstOrDefaultAsync(i => i.Id == id);
            if (instrutormodalidade is null)
            {
                return Results.NotFound("Modalidade do instrutor não encontrado");
            }
            
            instrutormodalidade.IsDeleted = true;
            instrutormodalidade.UpdatedAt = DateTime.Now;
            
            await _fitcontrolDbContext.SaveChangesAsync();
            return Results.Ok("Modalidade do instrutor apagado com sucesso!");
        }
        return Results.Empty; 
    }
    
    [HttpGet("/instrutormodalidade")]
    public async Task<IActionResult> GetInstrutor()
    {
        if (_fitcontrolDbContext.Instrutors is not null)
        {
            var instrutors = await _fitcontrolDbContext.Instrutors.ToListAsync();

            if (instrutors.Any())
            {
                return Ok(instrutors);
            }
        }
        return NotFound();
    }
    [HttpGet("/modalidade")]
    public async Task<IActionResult> GetModalidade()
    {
        if (_fitcontrolDbContext.Modalidades is not null)
        {
            var modalidades = await _fitcontrolDbContext.Modalidades.ToListAsync();

            if (modalidades.Any())
            {
                return Ok(modalidades);
            }
        }
        return NotFound();
    }

}

