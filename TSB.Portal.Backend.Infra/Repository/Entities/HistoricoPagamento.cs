using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repository.Entities;
public class HistoricoPagamento
{
    [Key]    
    public long Id { get; set; }
    public Apolice Apolice { get; set; }
    public string IDNotaFiscal { get; set; }
    public DateTime Data { get; set; }
}