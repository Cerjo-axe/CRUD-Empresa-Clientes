namespace Domain.Models;
public class Segmento : Base
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    
    public ICollection<Cliente> Clientes { get; set; }
}
