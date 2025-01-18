using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ScssNet.Lexing;
using ScssNet.Parsing;

namespace ScssNet
{
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
			services.AddLazy<TagSelectorParser>();
			services.AddLazy<ClassSelectorParser>();
			services.AddLazy<IdSelectorParser>();
			services.AddLazy<AttributteSelectorParser>();
			services.AddLazy<CompoundSelectorParser>();
			services.AddLazy<SubSelectorParser>();
			services.AddLazy<SelectorListParser>();
			services.AddLazy<ValueParser>();
			services.AddLazy<RuleParser>();
			services.AddLazy<BlockParser>();
			services.AddLazy<RuleSetParser>();
		}

		private static void AddLazy<TService>(this IServiceCollection services) where TService : class
		{
			services.AddSingleton<TService>();
			services.AddSingleton(sp => new Lazy<TService>(() => sp.GetRequiredService<TService>()));
		}
	}
}
