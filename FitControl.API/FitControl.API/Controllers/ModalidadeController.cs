using FitControl.API.Data;
using FitControl.API.Entities;
using FitControl.API.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitControl.API.Controllers;

public class ModalidadeController : ControllerBase
{
    private readonly IFitControlDbContext _fitControlDbContext;
    private readonly IMapper _mapper;
    
    public ModalidadeController(IFitControlDbContext fitControlDbContext, IMapper mapper)
    {
        _fitControlDbContext = fitControlDbContext;
        _mapper = mapper;
    }
    
    [HttpGet("/modalidades")]
    public async Task<List<Modalidade>> GetModalidades()
    {
        if (_fitControlDbContext.Modalidades is not null)
        {
            var modalidades = _fitControlDbContext.Modalidades
                .Include(x => x.NivelDificuldade)
                .Where(m => m.IsDeleted == false && m.NivelDificuldade.IsDeleted == false);
            
            if (modalidades.Any())
            {
                return await modalidades.ToListAsync();
            }
        }

        return new List<Modalidade>();
    }
    
    [HttpGet("/modalidade/{id}")]
    public async Task<Modalidade> GetModalidade(int id)
    {
        if (_fitControlDbContext.Modalidades is not null)
        {
            var modalidade = await _fitControlDbContext.Modalidades
                .Include(x => x.NivelDificuldade)
                .FirstOrDefaultAsync(m => (m.IsDeleted == false || m.NivelDificuldade.IsDeleted == false) && m.Id == id);
            
            if (modalidade is not null)
            {
                return modalidade;
            }
        }
        return new Modalidade();
    }
    
    [HttpPost("modalidade")]
    public async Task<IResult> AddModalidade([FromBody] ModalidadeDto? modalidade)
    {
        if (modalidade is null)
        {
            return Results.BadRequest();   
        }
        
        modalidade.NivelDificuldade = null;
        
        var mapper = _mapper.Map<Models.ModalidadeDto, Entities.Modalidade>(modalidade);

        mapper.CreatedAt = DateTime.Now;
        mapper.UpdatedAt = DateTime.Now;
        
        var modalidades = _fitControlDbContext.Modalidades;

        if (modalidades is not null)
        {
            modalidades.Add(mapper);
            await _fitControlDbContext.SaveChangesAsync();
            return Results.Ok("Modalidade adicionada com sucesso!");
        }
        return Results.Empty;
    }
    
    [HttpPut("/modalidade")]
    public async Task<IActionResult> UpdateModalidade ([FromBody] ModalidadeDto? modalidadeDto)
    {
        if (modalidadeDto is null)
        {
            return BadRequest();
        }
        
        modalidadeDto.NivelDificuldade = null;

        if (_fitControlDbContext.Modalidades is null)
        {
            return NotFound();
        }
        
        var oldModalidade = await _fitControlDbContext.Modalidades.FirstOrDefaultAsync(m => m.Id == modalidadeDto.Id);

        if (oldModalidade is null)
        {
            return NotFound("Modalidade não foi encontrada!");
        }

        modalidadeDto.Adapt(oldModalidade);
        
        try
        {
            var result = await _fitControlDbContext.SaveChangesAsync();

            if (result <= 0)
            {
                return NotFound("Não foi possível guardar os dados");
            }
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }

        return Ok(modalidadeDto);
    }
    
    [HttpDelete("/modalidade/softdelete/{id}")]
    public async Task<IResult> SoftDeleteModalidade(int id)
    {
        if (_fitControlDbContext.Modalidades is not null)
        {
            var modalidade = await _fitControlDbContext.Modalidades.FirstOrDefaultAsync(m => m.Id == id);
            if (modalidade is null)
            {
                return Results.NotFound("Modalidade não encontrada");
            }
            
            modalidade.IsDeleted = true;
            modalidade.UpdatedAt = DateTime.Now;
            
            await _fitControlDbContext.SaveChangesAsync();
            return Results.Ok("Modalidade apagada com sucesso!");
        }
        return Results.Empty; 
    }
}