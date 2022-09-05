using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repository.Entities;
public class Sinistro
{
    [Key]
    public long Id { get; set; }
    public string Tipo { get; set; }
    public Decimal ValorSinistro { get; set; }
}
