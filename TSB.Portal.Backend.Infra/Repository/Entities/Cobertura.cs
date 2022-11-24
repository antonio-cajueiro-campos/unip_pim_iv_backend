using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repository.Entities;
public class Cobertura
{
    [Key]
    public long Id { get; set; }
    public IEnumerable<Sinistro> Sinistros { get; set; }
    public decimal ValorCobertura { get; set; }
}