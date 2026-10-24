using Microsoft.Extensions.DependencyInjection;
using ScssNet.Structures;

namespace ScssNet.Generation;

internal static class GenerationDependencies
{
	internal static void AddGenerators(this IServiceCollection services)
	{
		services.AddSingleton<GeneratorContext>();
		services.AddSingleton<IGenerator<AtCharset>, AtCharsetGenerator>();
		services.AddSingleton<IGenerator<AtImport>, AtImportGenerator>();
		services.AddSingleton<IGenerator<AtMedia>, AtMediaGenerator>();
		services.AddSingleton<IGenerator<AtNamespace>, AtNamespaceGenerator>();
		services.AddSingleton<IGenerator<IAtRule>, AtRuleGenerator>();
		services.AddSingleton<IGenerator<AttributeSelector>, AttributeSelectorGenerator>();
		services.AddSingleton<IGenerator<Block>, BlockGenerator>();
		services.AddSingleton<IGenerator<ClassSelector>, ClassSelectorGenerator>();
		services.AddSingleton<IGenerator<FunctionCall>, FunctionCallGenerator>();
		services.AddSingleton<IGenerator<IdSelector>, IdSelectorGenerator>();
		services.AddSingleton<IGenerator<IMediaQuery>, MediaQueryGenerator>();
		services.AddSingleton<IGenerator<Rule>, RuleGenerator>();
		services.AddSingleton<IGenerator<RuleSet>, RuleSetGenerator>();
		services.AddSingleton<IGenerator<ISelector>, SelectorGenerator>();
		services.AddSingleton<IGenerator<SelectorList>, SelectorListGenerator>();
		services.AddSingleton<IGenerator<IStatement>, StatementGenerator>();
		services.AddSingleton<IGenerator<TagSelector>, TagSelectorGenerator>();
		services.AddSingleton<IGenerator<UniversalSelector>, UniversalSelectorGenerator>();
		services.AddSingleton<IGenerator<IUrl>, UrlGenerator>();
		services.AddSingleton<IGenerator<IValue>, ValueGenerator>();
	}
}
