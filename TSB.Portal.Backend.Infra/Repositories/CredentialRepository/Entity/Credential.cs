using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repositories.CredentialRepository.Entity;
public class Credential
{
	[Key]
	public long Id { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
	public ICollection<CredentialRoles> Roles { get; set; }
}