using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repository.Entities;
public class Cliente
{
    [Key]
	public long Id { get; set; }
    public User User { get; set; }
    public Endereco Endereco { get; set; }
    public string Telefone { get; set; }
    public string ChavePIX { get; set; }
}