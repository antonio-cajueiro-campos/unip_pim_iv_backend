using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repository.Entities;
public class Cliente : User
{
    public string Telefone { get; set; }
    public string CEP { get; set; }
    public string NumeroCasa { get; set; }
    public string NumeroComplemento { get; set; }
    public Apolice Apolice { get; set; }
    public string ChavePIX { get; set; }
}