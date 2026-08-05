using Microsoft.Extensions.DependencyInjection;

namespace ScssNet.Lexing;

internal static class LexingDependencies
{
	internal static void AddTokenParsers(this IServiceCollection services)
	{
		services.AddSingleton<CommentParser>();
		services.AddSingleton<HashValueParser>();
		services.AddSingleton<IdentifierParser>();
		services.AddSingleton<StringParser>();
		services.AddSingleton<SymbolParser>();
		services.AddSingleton<UnitValueParser>();
		services.AddSingleton<WhiteSpaceParser>();
	}

	internal static void AddReaders(this IServiceCollection services)
	{
		services.AddSingleton<ISourceReader, SourceReader>();
		services.AddSingleton<TokenReader>();
	}
}
