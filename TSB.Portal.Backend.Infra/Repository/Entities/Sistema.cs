using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repository.Entities;
public class Sistema
{
    public Decimal ValorDisponivelDaSeguradora { get; set; }
    public string PixDaSeguradora { get; set; }
}