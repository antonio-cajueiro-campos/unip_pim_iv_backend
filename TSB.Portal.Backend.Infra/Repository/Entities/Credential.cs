using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repository.Entities;
public class Credential
{
	[Key]
	public long Id { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
	public string Role { get; set; }
}