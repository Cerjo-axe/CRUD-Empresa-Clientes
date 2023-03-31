namespace Domain.Models;

public class Cliente
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Site { get; set; }
    public DateTime DataCriada { get; set; }
    public DateTime DataModificada { get; set; }

    //references
    public Guid SegmentoId { get; set; }
}
