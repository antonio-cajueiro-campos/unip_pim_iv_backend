using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repository.Entities;
public class Cliente
{
    [Key]
	public long Id { get; set; }
    public string Telefone { get; set; }
    public Endereco Endereco { get; set; } = null!;
    public Apolice Apolice { get; set; } = null!;
    public string ChavePIX { get; set; }
}