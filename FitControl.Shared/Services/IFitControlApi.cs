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
    Task<Aula> GetAula(int id);

    [Put("/aula")]
    Task<ApiResponse<Aula>> UpdateAula([Body] Aula aula);

    [Delete("/aula/{id}")]
    Task<ApiResponse<string>> DeleteAula(int id);

    [Get("/inscricaos")]
    Task<ApiResponse<List<Inscricao>>> GetInscricaos();

    [Post("/inscricao")]
    Task<ApiResponse<Inscricao>> AddInscricao([Body] Inscricao inscricao);

    [Get("/inscricao/{id}")]
    Task<Inscricao> GetInscricao(int id);

    [Put("/inscricao")]
    Task<ApiResponse<Inscricao>> UpdateInscricao([Body] Inscricao inscricao);

    [Delete("/inscricao/{id}")]
    Task<ApiResponse<string>> DeleteInscricao(int id);

    [Get("/instrutors")]
    Task<ApiResponse<List<Instrutor>>> GetInstrutors();

    [Post("/instrutor")]
    Task<ApiResponse<Instrutor>> AddInstrutor([Body] Instrutor instrutor);

    [Get("/instrutor/{id}")]
    Task<Instrutor> GetInstrutor(int id);

    [Put("/instrutor")]
    Task<ApiResponse<Instrutor>> UpdateInstrutor([Body] Instrutor instrutor);

    [Delete("/instrutor/{id}")]
    Task<ApiResponse<string>> DeleteInstrutor(int id);

    [Get("/instrutormodalidades")]
    Task<ApiResponse<List<InstrutorModalidade>>> GetInstrutorModalidades();

    [Post("/instrutormodalidade")]
    Task<ApiResponse<InstrutorModalidade>> AddInstrutorModalidade([Body] InstrutorModalidade instrutormodalidade);

    [Get("/instrutormodalidade/{id}")]
    Task<InstrutorModalidade> GetInstrutorModalidade(int id);

    [Put("/instrutormodalidade")]
    Task<ApiResponse<InstrutorModalidade>> UpdateInstrutorModalidade([Body] InstrutorModalidade instrutormodalidade);

    [Delete("/instrutormodalidade/{id}")]
    Task<ApiResponse<string>> DeleteInstrutorModalidade(int id);

    [Get("/modalidades")]
    Task<ApiResponse<List<Modalidade>>> GetModalidades();

    [Post("/modalidade")]
    Task<ApiResponse<Modalidade>> AddModalidade([Body] Modalidade modalidade);

    [Get("/modalidade/{id}")]
    Task<Modalidade> GetModalidade(int id);

    [Put("/modalidade")]
    Task<ApiResponse<Modalidade>> UpdateModalidade([Body] Modalidade modalidade);

    [Delete("/modalidade/{id}")]
    Task<ApiResponse<string>> DeleteModalidade(int id);

    [Get("/niveldificuldades")]
    Task<ApiResponse<List<NivelDificuldade>>> GetNivelDificuldades();

    [Post("/niveldificuldade")]
    Task<ApiResponse<NivelDificuldade>> AddNivelDificuldade([Body] NivelDificuldade niveldificuldade);

    [Get("/niveldificuldade/{id}")]
    Task<NivelDificuldade> GetNivelDificuldade(int id);

    [Put("/niveldificuldade")]
    Task<ApiResponse<NivelDificuldade>> UpdateNivelDificuldade([Body] NivelDificuldade niveldificuldade);

    [Delete("/niveldificuldade/{id}")]
    Task<ApiResponse<string>> DeleteNivelDificuldade(int id);

    [Get("/salas")]
    Task<ApiResponse<List<Sala>>> GetSalas();

    [Post("/sala")]
    Task<ApiResponse<Sala>> AddSala([Body] Sala sala);

    [Get("/sala/{id}")]
    Task<Sala> GetSala(int id);

    [Put("/sala")]
    Task<ApiResponse<Sala>> UpdateSala([Body] Sala sala);

    [Delete("/sala/{id}")]
    Task<ApiResponse<string>> DeleteSala(int id);

    [Get("/socios")]
    Task<ApiResponse<List<Socio>>> GetSocios();

    [Post("/socio")]
    Task<ApiResponse<Socio>> AddSocio([Body] Socio socio);

    [Get("/socio/{id}")]
    Task<Socio> GetSocio(int id);

    [Put("/socio")]
    Task<ApiResponse<Socio>> UpdateSocio([Body] Socio socio);

    [Delete("/socio/{id}")]
    Task<ApiResponse<string>> DeleteSocio(int id);

    [Get("/tipoplanos")]
    Task<ApiResponse<List<TipoPlano>>> GetTipoPlanos();

    [Post("/tipoplano")]
    Task<ApiResponse<TipoPlano>> AddTipoPlano([Body] TipoPlano tipoplano);

    [Get("/tipoplano/{id}")]
    Task<TipoPlano> GetTipoPlano(int id);

    [Put("/tipoplano")]
    Task<ApiResponse<TipoPlano>> UpdateTipoPlano([Body] TipoPlano tipoplano);

    [Delete("/tipoplano/{id}")]
    Task<ApiResponse<string>> DeleteTipoPlano(int id);
}