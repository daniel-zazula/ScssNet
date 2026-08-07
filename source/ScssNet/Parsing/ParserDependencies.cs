using Microsoft.Extensions.DependencyInjection;

namespace ScssNet.Parsing;

internal static class ParserDependencies
{
	internal static void AddParsers(this IServiceCollection services)
	{
		services.AddLazySingleton<TagSelectorParser>();
		services.AddLazySingleton<ClassSelectorParser>();
		services.AddLazySingleton<IdSelectorParser>();
		services.AddLazySingleton<AttributeSelectorParser>();
		services.AddLazySingleton<UniversalSelectorParser>();
		services.AddLazySingleton<PseudoClassSelectorParser>();
		services.AddLazySingleton<PseudoElementSelectorParser>();
		services.AddLazySingleton<SelectorListParser>();
		services.AddLazySingleton<SelectorParser>();
		services.AddLazySingleton<ValueParser>();
		services.AddLazySingleton<RuleParser>();
		services.AddLazySingleton<RuleSetParser>();
		services.AddLazySingleton<StatementParser>();
		services.AddLazySingleton<AtRuleParser>();
		services.AddLazySingleton<FunctionCallParser>();
	}
}
