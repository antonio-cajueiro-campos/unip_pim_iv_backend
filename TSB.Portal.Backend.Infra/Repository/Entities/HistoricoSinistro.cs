using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repository.Entities;
public class HistoricoSinistro
{
    [Key]
    public long Id { get; set; }
    public User Cliente { get; set; }
    public Sinistro Sinistro { get; set; }
    public DateTime Ocorrencia { get; set; }
    public Decimal Valor { get; set; }
}