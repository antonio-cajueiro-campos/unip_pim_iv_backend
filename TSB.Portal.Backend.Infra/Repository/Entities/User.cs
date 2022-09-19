using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSB.Portal.Backend.Infra.Repository.Entities;
public class User
{
	[Key]
	public long Id { get; set; }
	public string Name { get; set; }
	public string Document { get; set; }
	public Credential Credential { get; set; }
	public DateTime RegistrationDate { get; set; }
}