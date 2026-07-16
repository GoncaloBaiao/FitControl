using FitControl.Shared.Models;
using Refit;
namespace FitControl.Shared.Services;

public interface IFitControlApi
{
    [Get("/aulas")]
    Task<ApiResponse<List<Aula>>> GetAulas();

    [Post("/aula")]
    Task<ApiResponse<Aula>> AddAula([Body] Aula aula);

    [Get("/aula/{id}")]
    Task<ApiResponse<Aula>> GetAula(int id);

    [Put("/aula")]
    Task<ApiResponse<Aula>> UpdateAula([Body] Aula? aula);

    [Delete("/aula/softdelete/{id}")]
    Task<ApiResponse<string>> DeleteAula(int id);
    
    [Get("/generos")]
    Task<ApiResponse<List<Genero>>> GetGeneros();
    
    [Post("/genero")]
    Task<ApiResponse<Genero>> AddGenero([Body] Genero genero);
    
    [Get("/genero/{id}")]
    Task<ApiResponse<Genero>> GetGenero(int id);
    
    [Put("/genero")]
    Task<ApiResponse<Genero>> UpdateGenero([Body] Genero? genero);
    
    [Delete("/genero/softdelete/{id}")]
    Task<ApiResponse<string>> DeleteGenero(int id);

    [Get("/inscricaos")]
    Task<ApiResponse<List<Inscricao>>> GetInscricaos();

    [Post("/inscricao")]
    Task<ApiResponse<Inscricao>> AddInscricao([Body] Inscricao inscricao);

    [Get("/inscricao/{id}")]
    Task<ApiResponse<Inscricao>> GetInscricao(int id);

    [Put("/inscricao")]
    Task<ApiResponse<Inscricao>> UpdateInscricao([Body] Inscricao inscricao);

    [Delete("/inscricao/softdelete/{id}")]
    Task<ApiResponse<string>> DeleteInscricao(int id);

    [Get("/instrutors")]
    Task<ApiResponse<List<Instrutor>>> GetInstrutors();

    [Post("/instrutor")]
    Task<ApiResponse<Instrutor>> AddInstrutor([Body] Instrutor instrutor);

    [Get("/instrutor/{id}")]
    Task<ApiResponse<Instrutor>> GetInstrutor(int id);

    [Put("/instrutor")]
    Task<ApiResponse<Instrutor>> UpdateInstrutor([Body] Instrutor instrutor);

    [Delete("/instrutor/softdelete/{id}")]
    Task<ApiResponse<string>> DeleteInstrutor(int id);

    [Get("/instrutormodalidades")]
    Task<ApiResponse<List<InstrutorModalidade>>> GetInstrutorModalidades();

    [Post("/instrutormodalidade")]
    Task<ApiResponse<InstrutorModalidade>> AddInstrutorModalidade([Body] InstrutorModalidade instrutormodalidade);

    [Get("/instrutormodalidade/{id}")]
    Task<ApiResponse<InstrutorModalidade>> GetInstrutorModalidade(int id);

    [Put("/instrutormodalidade")]
    Task<ApiResponse<InstrutorModalidade>> UpdateInstrutorModalidade([Body] InstrutorModalidade instrutormodalidade);

    [Delete("/instrutormodalidade/softdelete/{id}")]
    Task<ApiResponse<string>> DeleteInstrutorModalidade(int id);

    [Get("/modalidades")]
    Task<ApiResponse<List<Modalidade>>> GetModalidades();

    [Post("/modalidade")]
    Task<ApiResponse<Modalidade>> AddModalidade([Body] Modalidade modalidade);

    [Get("/modalidade/{id}")]
    Task<ApiResponse<Modalidade>> GetModalidade(int id);

    [Put("/modalidade")]
    Task<ApiResponse<Modalidade>> UpdateModalidade([Body] Modalidade modalidade);

    [Delete("/modalidade/softdelete/{id}")]
    Task<ApiResponse<string>> DeleteModalidade(int id);

    [Get("/niveldificuldades")]
    Task<ApiResponse<List<NivelDificuldade>>> GetNivelDificuldades();

    [Post("/niveldificuldade")]
    Task<ApiResponse<NivelDificuldade>> AddNivelDificuldade([Body] NivelDificuldade niveldificuldade);

    [Get("/niveldificuldade/{id}")]
    Task<ApiResponse<NivelDificuldade>> GetNivelDificuldade(int id);

    [Put("/niveldificuldade")]
    Task<ApiResponse<NivelDificuldade>> UpdateNivelDificuldade([Body] NivelDificuldade niveldificuldade);

    [Delete("/niveldificuldade/softdelete/{id}")]
    Task<ApiResponse<string>> DeleteNivelDificuldade(int id);

    [Get("/salas")]
    Task<ApiResponse<List<Sala>>> GetSalas();

    [Post("/sala")]
    Task<ApiResponse<Sala>> AddSala([Body] Sala sala);

    [Get("/sala/{id}")]
    Task<ApiResponse<Sala>> GetSala(int id);

    [Put("/sala")]
    Task<ApiResponse<Sala>> UpdateSala([Body] Sala sala);

    [Delete("/sala/softdelete/{id}")]
    Task<ApiResponse<string>> DeleteSala(int id);

    [Get("/socios")]
    Task<ApiResponse<List<Socio>>> GetSocios();

    [Post("/socio")]
    Task<ApiResponse<Socio>> AddSocio([Body] Socio socio);

    [Get("/socio/{id}")]
    Task<ApiResponse<Socio>> GetSocio(int id);

    [Put("/socio")]
    Task<ApiResponse<Socio>> UpdateSocio([Body] Socio socio);

    [Delete("/socio/softdelete/{id}")]
    Task<ApiResponse<string>> DeleteSocio(int id);

    [Get("/tipoplanos")]
    Task<ApiResponse<List<TipoPlano>>> GetTipoPlanos();

    [Post("/tipoplano")]
    Task<ApiResponse<TipoPlano>> AddTipoPlano([Body] TipoPlano tipoplano);

    [Get("/tipoplano/{id}")]
    Task<ApiResponse<TipoPlano>> GetTipoPlano(int id);

    [Put("/tipoplano")]
    Task<ApiResponse<TipoPlano>> UpdateTipoPlano([Body] TipoPlano tipoplano);

    [Delete("/tipoplano/softdelete/{id}")]
    Task<ApiResponse<string>> DeleteTipoPlano(int id);
    
    [Get("/users")]
    Task<ApiResponse<List<User>>> GetUsers();

    [Post("/user")]
    Task<ApiResponse<User>> AddUser([Body] User user);

    [Get("/user/{id}")]
    Task<ApiResponse<User>> GetUser(int id);

    [Put("/user")]
    Task<ApiResponse<User>> UpdateUser([Body] User user);

    [Delete("/user/softdelete/{id}")]
    Task<ApiResponse<string>> DeleteUser(int id);
}