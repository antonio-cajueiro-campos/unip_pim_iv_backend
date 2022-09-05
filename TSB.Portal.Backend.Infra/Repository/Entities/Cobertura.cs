using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repository.Entities;
public class Cobertura
{
    [Key]
    public long Id { get; set; }
    public Sinistro Sinistro { get; set; }
    public Decimal ValorCobertura { get; set; }
}