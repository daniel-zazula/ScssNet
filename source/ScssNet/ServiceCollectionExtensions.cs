using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Lexing;
using ScssNet.Parsing;

namespace ScssNet;

internal static class ServiceCollectionExtensions
{
	internal static void AddTokenParsers(this IServiceCollection services)
	{
		services.AddSingleton<CommentParser>();
		services.AddSingleton<HexValueParser>();
		services.AddSingleton<IdentifierParser>();
		services.AddSingleton<StringParser>();
		services.AddSingleton<SymbolParser>();
		services.AddSingleton<UnitValueParser>();
		services.AddSingleton<WhiteSpaceParser>();
	}

	internal static void AddReaders(this IServiceCollection services)
	{
		services.AddSingleton<SourceReader>();
		services.AddSingleton<TokenReader>();
	}

	internal static void AddParsers(this IServiceCollection services)
	{
		services.AddLazySingleton<TagSelectorParser>();
		services.AddLazySingleton<ClassSelectorParser>();
		services.AddLazySingleton<IdSelectorParser>();
		services.AddLazySingleton<AttributteSelectorParser>();
		services.AddLazySingleton<CompoundSelectorParser>();
		services.AddLazySingleton<SubSelectorParser>();
		services.AddLazySingleton<SelectorListParser>();
		services.AddLazySingleton<ValueParser>();
		services.AddLazySingleton<RuleParser>();
		services.AddLazySingleton<BlockParser>();
		services.AddLazySingleton<RuleSetParser>();
	}

	internal static void AddGenerators(this IServiceCollection services)
	{
		services.AddLazySingleton<RuleSetGenerator>();
		services.AddLazySingleton<SelectorListGenerator>();
		services.AddLazySingleton<BlockGenerator>();
		services.AddLazySingleton<SelectorGenerator>();
		services.AddLazySingleton<ClassSelectorGenerator>();
		services.AddLazySingleton<CompoundSelectorGenerator>();
		services.AddLazySingleton<IdSelectorGenerator>();
		services.AddLazySingleton<TagSelectorGenerator>();
		services.AddLazySingleton<AttributteSelectorGenerator>();
	}

	private static void AddLazySingleton<TService>(this IServiceCollection services) where TService : class
	{
		services.AddSingleton<TService>();
		services.AddSingleton(sp => new Lazy<TService>(() => sp.GetRequiredService<TService>()));
	}
}
