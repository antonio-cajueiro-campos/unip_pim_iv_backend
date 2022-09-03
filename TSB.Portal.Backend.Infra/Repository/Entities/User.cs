using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repository.Entities;
public class User
{
	[Key]
	public long Id { get; set; }
	public string Name { get; set; }
	public string Document { get; set; }
	public DateTime RegistrationDate { get; set; }
}