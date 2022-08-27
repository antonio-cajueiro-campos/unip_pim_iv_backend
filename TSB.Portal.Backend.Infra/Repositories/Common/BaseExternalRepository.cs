using Microsoft.EntityFrameworkCore;

namespace TSB.Portal.Backend.Infra.Repositories.Common;
public class BaseExternalRepository : DbContext
{
	public BaseExternalRepository(DbContextOptions<BaseExternalRepository> opt) : base(opt) { }
}
