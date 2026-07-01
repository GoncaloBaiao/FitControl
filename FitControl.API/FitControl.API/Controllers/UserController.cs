using FitControl.API.Data;
using FitControl.API.Entities;
using FitControl.API.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitControl.API.Controllers;

public class UserController : ControllerBase
{
    private readonly IFitControlDbContext _fitControlDbContext;
    private readonly IMapper _mapper;
    
    public UserController(IFitControlDbContext fitControlDbContext, IMapper mapper)
    {
        _fitControlDbContext = fitControlDbContext;
        _mapper = mapper;
    }
    
    [HttpGet("/users")]
    public async Task<List<User>> GetUsers()
    {
        if (_fitControlDbContext.Users is not null)
        {
            var users = _fitControlDbContext.Users
                .Where(u => u.IsDeleted ==  false);
            
            if (users.Any())
            {
                return await users.ToListAsync();
            }
        }

        return new List<User>();
    }
    
    [HttpGet("/user")]
    public async Task<IActionResult> GetUser([FromBody] UserDto userDto)
    {
        if (_fitControlDbContext.Users != null)
        {
            var checkUser = await _fitControlDbContext.Users.AnyAsync(x => x.Username.Equals(userDto.Username) && x.Password == userDto.Password);

            if (checkUser)
            {
                return Ok(checkUser);
            }
        }
        return NotFound("Username or password is incorrect");
    }
    
    [HttpPost("user")]
    public async Task<IResult> AddUser([FromBody] UserDto? user)
    {
        if (user is null)
        {
            return Results.BadRequest();   
        }
        
        var mapper = _mapper.Map<Models.UserDto, Entities.User>(user);

        mapper.CreatedAt = DateTime.Now;
        mapper.UpdatedAt = DateTime.Now;
        
        var users = _fitControlDbContext.Users;

        if (users is not null)
        {
            users.Add(mapper);
            await _fitControlDbContext.SaveChangesAsync();
            return Results.Ok("User adicionado com sucesso!");
        }
        return Results.Empty;
    }
    
    [HttpPut("/user")]
    public async Task<IActionResult> UpdateUser ([FromBody] UserDto? userDto)
    {
        if (userDto is null)
        {
            return BadRequest();
        }

        if (_fitControlDbContext.Users is null)
        {
            return NotFound();
        }
        
        var oldUser = await _fitControlDbContext.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);

        if (oldUser is null)
        {
            return NotFound("User não foi encontrado!");
        }

        userDto.Adapt(oldUser);
        
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

        return Ok(userDto);
    }
    
    [HttpDelete("/user/softdelete/{id}")]
    public async Task<IResult> SoftDeleteUser(int id)
    {
        if (_fitControlDbContext.Users is not null)
        {
            var user = await _fitControlDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
            {
                return Results.NotFound("User não encontrado");
            }
            
            user.IsDeleted = true;
            user.UpdatedAt = DateTime.Now;
            
            await _fitControlDbContext.SaveChangesAsync();
            return Results.Ok("User apagado com sucesso!");
        }
        return Results.Empty; 
    }
}