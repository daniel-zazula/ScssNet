using Microsoft.Extensions.DependencyInjection;

namespace ScssNet;

internal static class ServiceCollectionExtensions
{
	internal static void AddLazySingleton<TService>(this IServiceCollection services) where TService : class
	{
		services.AddSingleton<TService>();
		services.AddSingleton(sp => new Lazy<TService>(() => sp.GetRequiredService<TService>()));
	}
}
