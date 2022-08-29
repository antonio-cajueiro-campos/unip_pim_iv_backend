using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repositories.CredentialRepository.Entity;
public class Role
{
	[Key]
	public long Id { get; set; }
	public string Name { get; set; }
}