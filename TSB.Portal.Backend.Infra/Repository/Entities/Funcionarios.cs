using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repository.Entities;
public class Funcionario
{
    [Key]
	public long Id { get; set; }
    public User User { get; set; }
    public string Cargo { get; set; }
}