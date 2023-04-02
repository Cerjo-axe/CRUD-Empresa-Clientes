using System.ComponentModel.DataAnnotations;

namespace DTO;
public class SegmentoDTO
{
    public string Id { get; set; }

    [Required]
    public string Nome { get; set; }
    public string? Descricao { get; set; }

}
