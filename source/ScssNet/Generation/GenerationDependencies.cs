using Microsoft.Extensions.DependencyInjection;

namespace ScssNet.Generation;

internal static class GenerationDependencies
{
	internal static void AddGenerators(this IServiceCollection services)
	{
		services.AddLazySingleton<RuleSetGenerator>();
		services.AddLazySingleton<RuleGenerator>();
		services.AddLazySingleton<SelectorListGenerator>();
		services.AddLazySingleton<BlockGenerator>();
		services.AddLazySingleton<SelectorGenerator>();
		services.AddLazySingleton<ClassSelectorGenerator>();
		services.AddLazySingleton<UniversalSelectorGenerator>();
		services.AddLazySingleton<IdSelectorGenerator>();
		services.AddLazySingleton<TagSelectorGenerator>();
		services.AddLazySingleton<AttributeSelectorGenerator>();
		services.AddLazySingleton<StatementGenerator>();
		services.AddLazySingleton<AtRuleGenerator>();
		services.AddLazySingleton<ValueGenerator>();
		services.AddLazySingleton<FunctionCallGenerator>();
		services.AddLazySingleton<AtCharsetGenerator>();
		services.AddLazySingleton<AtImportGenerator>();
	}
}
