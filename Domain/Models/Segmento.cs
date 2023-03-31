namespace Domain.Models;
public class Segmento
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public DateTime DataCriada { get; set; }
    public DateTime DataModificada { get; set; }

    public ICollection<Cliente> Clientes { get; set; }
}
