using Api.Interfaces;
using Api.Repositories;
using Api.Services;
using Api.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
      // Register DatabaseHelper
      services.AddSingleton<DatabaseHelper>();

      // Register the IStudentRepository interface and StudentRepository implementation
      services.AddTransient<IStudentRepository, StudentRepository>();

      // Register StudentService
      services.AddTransient<StudentService>();

      return services;
    }
  }
}
