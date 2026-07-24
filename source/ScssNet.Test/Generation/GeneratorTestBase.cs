using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

public abstract class GeneratorTestBase
{
	protected static ServiceProvider BuildServiceProvider()
	{
		var services = new ServiceCollection();
		services.AddScoped<StringWriter>();
		services.AddScoped<TextWriter>(p => p.GetRequiredService<StringWriter>());
		services.AddScoped<CssWriter>();
		services.AddGenerators();

		return services.BuildServiceProvider();
	}

	protected static SymbolToken CreateSymbolToken(Symbol symbol, int lineNumber = 1, int columnNumber = 1)
	{
		var length = symbol.ToChars().Length;
		var (start, end) = CreateTokenCoordinates(lineNumber, columnNumber, length);

		return new SymbolToken(symbol, start, end, Separator.Empty, Separator.Empty);
	}

	protected static IdentifierToken CreateIdentifierToken(string identifier, int lineNumber = 1, int columnNumber = 1)
	{
		var length = identifier.Length;
		var (start, end) = CreateTokenCoordinates(lineNumber, columnNumber, length);

		return new IdentifierToken(identifier, start, end, Separator.Empty, Separator.Empty);
	}

	protected static StringToken CreateStringToken(string value, int lineNumber = 1, int columnNumber = 1)
	{
		var startChar = value[0];
		var endChar = value[^1];

		startChar.ShouldBeOneOf('"', '\'');
		endChar.ShouldBe(startChar);

		var length = value.Length;
		var (start, end) = CreateTokenCoordinates(lineNumber, columnNumber, length);

		return new StringToken(value, start, end, Separator.Empty, Separator.Empty);
	}

	protected static HashValueToken CreateHashValueToken(string value, int lineNumber = 1, int columnNumber = 1)
	{
		value.ShouldStartWith("#");

		var length = value.Length;
		var (start, end) = CreateTokenCoordinates(lineNumber, columnNumber, length);

		return new HashValueToken(value, start, end, Separator.Empty, Separator.Empty);
	}

	protected static KeywordToken CreateKeywordToken
	(
		Keyword keyword, string? value = null, int lineNumber = 1, int columnNumber = 1
	)
	{
		if (value is not null)
			value.ShouldBe(keyword.ToString(), StringCompareShould.IgnoreCase);
		else
			value = keyword.ToString();

		var length = value.Length;
		var (start, end) = CreateTokenCoordinates(lineNumber, columnNumber, length);

		return new KeywordToken(keyword, value, start, end, Separator.Empty, Separator.Empty);
	}

	private static (SourceCoordinates start, SourceCoordinates end) CreateTokenCoordinates
	(
		int lineNumber = 1, int columnNumber = 1, int length = 1
	)
	{
		var start = new SourceCoordinates(lineNumber, columnNumber);
		var end = new SourceCoordinates(lineNumber, columnNumber + length - 1);
		return (start, end);
	}
}
