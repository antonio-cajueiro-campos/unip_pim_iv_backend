using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repository.Entities;
public class Apolice
{
    [Key]
    public long Id { get; set; }
    public Cobertura Cobertura { get; set; }
    public DateTime Vigencia { get; set; }
    public DateTime Emissao { get; set; }
}