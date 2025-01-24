using FilmesCatalago.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesCatalago.Controllers;


[ApiController]
[Route("[controller]")]
public class CepCotroller : ControllerBase
{

    private readonly ILogger<CepCotroller> _logger;
    private readonly HttpClient _httpClient;

    public CepCotroller(ILogger<CepCotroller> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    [HttpGet("{cep}")]
    public async Task<ActionResult<CepDTO>> GetCep(string cep)
    {
        try
        {
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://viacep.com.br")
            };

            var response = await httpClient.GetAsync($"/ws/{cep}/json/");

            response.EnsureSuccessStatusCode();
            CepDTO json = await response?.Content.ReadFromJsonAsync<CepDTO>();
            return Ok(json);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro ao Obter CEP, e.InnerException: {e}");
        }
        return BadRequest();
    }
}