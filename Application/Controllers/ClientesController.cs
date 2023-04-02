using Domain.Interfaces.Service;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Produces("Application/json")]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _service;
    private readonly ILogger<ClientesController> _logger;

    public ClientesController(IClienteService service, ILogger<ClientesController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteDTO>> Obter(string id)
    {
        try
        {
            _logger.LogInformation($"Procurando Cliente de id: {id}");
            var result =await _service.GetCliente(id);
            if (result is null)
            {
                _logger.LogInformation($"Procurando com id: {id} inexistente");
                return BadRequest();
            }
            _logger.LogInformation("Cliente encontrado");
            return Ok(result);
        }
        catch (System.Exception)
        {
            
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDTO>>> Listar()
    {
        try
        {
            _logger.LogInformation("Monstando lista de Clientes");
            var result = await _service.GetClientes();
            if(result is null)
            {
                _logger.LogInformation("Não existe Clientes para listar");
                ModelState.AddModelError("Info","Lista sem dados para listar");
                return BadRequest();
            }
            return Ok(result);
        }
        catch (System.Exception)
        {
            
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] ClienteDTO cliente)
    {
        try
        {
            _logger.LogInformation("Iniciando serviço de registro de Cliente");
            cliente.Id = Guid.NewGuid().ToString();
            await _service.AddCliente(cliente);
            _logger.LogInformation($"Sucesso na operação de registro do Cliente:{cliente.Nome}");
            return CreatedAtAction(nameof(Obter), new {id = cliente.Id},cliente);
        }
        catch (FluentValidation.ValidationException)
        {
            _logger.LogError($"Erro de validação do Cliente");
            ModelState.AddModelError("Condições","Formulário com uma ou mais informações incorretas");
            return BadRequest();
        }
    }

    [HttpPut]
    public async Task<IActionResult> Atualizar([FromBody] ClienteDTO cliente){
        try
        {
            _logger.LogInformation($"Atualizando cliente de id: {cliente.Id}");
            await _service.UpdateCliente(cliente);
            _logger.LogInformation($"Sucesso na atualização do cliente");
            return Ok();
        }
        catch (System.Exception)
        {
            _logger.LogError($"Erro na atualização do cliente");
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(string id)
    {
        try
        {
            _logger.LogInformation($"Deletando cliente de id: {id}");
            await _service.DeleteCliente(id);
            _logger.LogInformation("Sucesso em deletar o cliente");
            return Ok();
        }
        catch (System.Exception)
        {
            _logger.LogError($"Erro deletando cliente");
            return BadRequest();
        }
    }
}
