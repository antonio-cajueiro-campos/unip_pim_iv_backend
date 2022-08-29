using Microsoft.EntityFrameworkCore;
using TSB.Portal.Backend.Infra.Repositories.CredentialRepository.Entity;
using TSB.Portal.Backend.Infra.Repositories.CredentialRepository;

namespace TSB.Portal.Backend.Infra.Repositories.AuthenticateRepository;
public class CredentialRepository : DbContext, ICredentialRepository
{
	public DbSet<Credential> Credentials { get; set; }
	public DbSet<Role> Roles { get; set; }
	public CredentialRepository(DbContextOptions<CredentialRepository> opt) : base(opt) { }
}
