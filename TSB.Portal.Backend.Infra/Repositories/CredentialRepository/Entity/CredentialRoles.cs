using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repositories.CredentialRepository.Entity;
public class CredentialRoles
{
	[Key]
	public long Id { get; set; }
	public long CredentialId { get; set; }
	public long RoleId { get; set; }
}