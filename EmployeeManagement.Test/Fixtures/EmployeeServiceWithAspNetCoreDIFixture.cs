using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Services.Test;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Test.Fixtures
{
	public class EmployeeServiceWithAspNetCoreDIFixture : IDisposable
	{
		private readonly ServiceProvider _serviceProvider;
		public EmployeeServiceWithAspNetCoreDIFixture()
		{
			var services = new ServiceCollection();
			services.AddScoped<EmployeeFactory>();
			services.AddScoped<IEmployeeManagementRepository,
				EmployeeManagementTestDataRepository>();
			services.AddScoped<IEmployeeService, EmployeeService>();

			_serviceProvider = services.BuildServiceProvider();

		}

		public IEmployeeManagementRepository EmployeeManagementTestDataRepository
		{

			get
			{
				return _serviceProvider.GetRequiredService<IEmployeeManagementRepository>();
			}
		}

		public IEmployeeService EmployeeService
		{
			get
			{
				return _serviceProvider.GetRequiredService<IEmployeeService>();
			}
		}

		public void Dispose()
		{
			//clean code here
		}
	}
}
