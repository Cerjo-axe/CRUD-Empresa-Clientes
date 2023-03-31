namespace Domain.Models;

public class Cliente : Base
{
    public string? Nome { get; set; }
    public string? Site { get; set; }

    //references
    public string SegmentoId { get; set; }
    public Segmento Segmento { get; set; }
}
