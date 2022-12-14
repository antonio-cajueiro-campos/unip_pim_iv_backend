using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repository.Entities;
public class HistoricoSinistro
{
    [Key]
    public long Id { get; set; }
    public Cliente Cliente { get; set; }
    public Sinistro Sinistro { get; set; }
    public DateTime Ocorrencia { get; set; }
    public decimal Valor { get; set; }
}