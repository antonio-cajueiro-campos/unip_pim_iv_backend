using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TSB.Portal.Backend.Infra.Repositories;

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
		self.UseSqlServer("Server=127.0.0.1;Database=tsb;User Id=sqlserver;Password=0XamP45nCjHy;");
		return self;
	}
}