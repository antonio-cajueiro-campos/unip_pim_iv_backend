using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TSB.Portal.Backend.Infra.Repository;

public static class DataExtensions {
	public static IServiceCollection AddDbServices(this IServiceCollection self)
	{
		self.AddScoped<IDataContext, DataContext>();
		return self;
	}

	public static IServiceCollection AddDataContext(this IServiceCollection self, IConfiguration configuration) {
		string connectionString = configuration.GetConnectionString("DefaultConnection");
		self.AddDbContext<DataContext>(options => {
			options.UseSqlServer(connectionString);
		});
		return self;
	}

	public static DbContextOptionsBuilder AddDefaultConnection(this DbContextOptionsBuilder  self) {
		self.UseSqlServer("Server=bancodedadosunip.ddns.net;Initial Catalog=tsb;User Id=lucas;Password=123;");
		return self;
	}
}