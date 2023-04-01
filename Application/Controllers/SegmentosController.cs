using Domain.Interfaces.Service;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Produces("Application/json")]
[Route("api/[controller]")]
public class SegmentosController : ControllerBase
{
    private readonly ISegmentoService _service;
    private readonly ILogger<SegmentosController> _logger;

    public SegmentosController(ISegmentoService service, ILogger<SegmentosController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SegmentoDTO>> Obter([FromQuery] string id)
    {
        try
        {
            _logger.LogInformation($"Procurando segmento de id: {id}");
            var result =await _service.GetSegmento(id);
            if (result is null)
            {
                _logger.LogInformation($"Procurando com id: {id} inexistente");
                return BadRequest();
            }
            _logger.LogInformation("Segmento encontrado");
            return Ok(result);
        }
        catch (System.Exception)
        {
            
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SegmentoDTO>>> Listar()
    {
        try
        {
            _logger.LogInformation("Monstando lista de segmentos");
            var result = await _service.GetSegmentos();
            if(result is null)
            {
                _logger.LogInformation("Não existe segmentos para listar");
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
    public async Task<IActionResult> Criar([FromBody] SegmentoDTO segmento)
    {
        try
        {
            _logger.LogInformation("Iniciando serviço de registro de segmento");
            segmento.Id = new Guid().ToString();
            await _service.AddSegment(segmento);
            _logger.LogInformation($"Sucesso na operação de registro do segmento:{segmento.Nome}");
            return CreatedAtAction(nameof(Obter), new {id = segmento.Id},segmento);
        }
        catch (FluentValidation.ValidationException)
        {
            _logger.LogError($"Erro de validação do segmento");
            ModelState.AddModelError("Condições","Formulário com uma ou mais informações incorretas");
            return BadRequest();
        }
    }
    [HttpPut]
    public async Task<IActionResult> Atualizar([FromBody] SegmentoDTO segmento){
        try
        {
            _logger.LogInformation($"Atualizando segmento de id: {segmento.Id}");
            await _service.UpdateSegment(segmento);
            _logger.LogInformation($"Sucesso na atualização do segmento");
            return Ok();
        }
        catch (System.Exception)
        {
            _logger.LogError($"Erro na atualização do segmento");
            return BadRequest();
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Deletar([FromBody]SegmentoDTO segmento)
    {
        try
        {
            _logger.LogInformation($"Deletando segmento de id: {segmento.Id}");
            await _service.DeleteSegment(segmento);
            _logger.LogInformation("Sucesso em deletar o segmento");
            return Ok();
        }
        catch (System.Exception)
        {
            _logger.LogError($"Erro deletando segmento");
            return BadRequest();
        }
    }
}
