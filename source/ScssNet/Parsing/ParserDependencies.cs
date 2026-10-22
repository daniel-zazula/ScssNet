using Microsoft.Extensions.DependencyInjection;
using ScssNet.Structures;

namespace ScssNet.Parsing;

internal static class ParserDependencies
{
	internal static void AddParsers(this IServiceCollection services)
	{
		services.AddScoped<ParserContext>();
		services.AddSingleton<IAtCharsetParser, AtCharsetParser>();
		services.AddSingleton<IAtImportParser, AtImportParser>();
		services.AddSingleton<IAtMediaParser, AtMediaParser>();
		services.AddSingleton<IAtNamespaceParser, AtNamespaceParser>();
		services.AddSingleton<IAtRuleParser, AtRuleParser>();
		services.AddSingleton<IAttributeSelectorParser, AttributeSelectorParser>();
		services.AddSingleton<IBlockParser, BlockParser>();
		services.AddSingleton<IClassSelectorParser, ClassSelectorParser>();
		services.AddSingleton<IFunctionCallParser, FunctionCallParser>();
		services.AddSingleton<IIdSelectorParser, IdSelectorParser>();
		services.AddSingleton<IMediaQueryParser, MediaQueryParser>();
		services.AddSingleton<IPseudoClassSelectorParser, PseudoClassSelectorParser>();
		services.AddSingleton<IPseudoElementSelectorParser, PseudoElementSelectorParser>();
		services.AddSingleton<IRuleParser, RuleParser>();
		services.AddSingleton<IRuleSetParser, RuleSetParser>();
		services.AddSingleton<ISelectorListParser, SelectorListParser>();
		services.AddSingleton<ISelectorParser, SelectorParser>();
		services.AddSingleton<ISelectorQualifierParser, SelectorQualifierParser>();
		services.AddSingleton<IStatementParser, StatementParser>();
		services.AddSingleton<ITagSelectorParser, TagSelectorParser>();
		services.AddSingleton<IUniversalSelectorParser, UniversalSelectorParser>();
		services.AddSingleton<IUrlParser, UrlParser>();
		services.AddSingleton<IValueParser, ValueParser>();
	}
}
