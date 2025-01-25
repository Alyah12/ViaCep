using APICep.Models;
using Microsoft.AspNetCore.Mvc;

namespace APICep.Controllers;

[ApiController]
[Route("[controller]")]
public class CepController : ControllerBase
{
    private readonly HttpClient _httpClient;

    private readonly ILogger<CepController> _logger;

    public CepController(ILogger<CepController> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    [HttpGet("{cep}")]
    public async Task<ActionResult<CepDTO>> GetCep(string cep)
    {
        try
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://viacep.com.br")
            };

            var response = await httpClient.GetAsync($"/ws/{cep}/json/");

            response.EnsureSuccessStatusCode();
            var json = await response?.Content.ReadFromJsonAsync<CepDTO>();
            return Ok(json);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro ao Obter CEP, e.InnerException: {e}");
        }

        return BadRequest();
    }
}