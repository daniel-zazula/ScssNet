using System;
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

	protected static SymbolToken CreateSymbolToken(Symbol symbol, ISourceElement? predecessor = null)
	{
		var length = symbol.ToChars().Length;
		var span = CreateSpan(predecessor, length);

		return new SymbolToken(symbol, span, Separator.Empty, Separator.Empty);
	}

	protected static IdentifierToken CreateIdentifierToken(string identifier, ISourceElement? predecessor = null)
	{
		var length = identifier.Length;
		var span = CreateSpan(predecessor, length);

		return new IdentifierToken(identifier, span, Separator.Empty, Separator.Empty);
	}

	protected static StringToken CreateStringToken(string value, ISourceElement? predecessor = null)
	{
		var startChar = value[0];
		var endChar = value[^1];

		startChar.ShouldBeOneOf('"', '\'');
		endChar.ShouldBe(startChar);

		var length = value.Length;
		var span = CreateSpan(predecessor, length);

		return new StringToken(value, span, Separator.Empty, Separator.Empty);
	}

	protected static HashValueToken CreateHashValueToken(string value, ISourceElement? predecessor = null)
	{
		value.ShouldStartWith("#");

		var length = value.Length;
		var span = CreateSpan(predecessor, length);

		return new HashValueToken(value, span, Separator.Empty, Separator.Empty);
	}

	protected static UnitValueToken CreateUnitValueToken
	(
		decimal amount, string unit, ISourceElement? predecessor = null
	)
	{
		var valueString = $"{amount}{unit}";
		var length = valueString.Length;
		var span = CreateSpan(predecessor, length);

		return new UnitValueToken(amount, unit, span, Separator.Empty, Separator.Empty);
	}

	protected static AtKeywordToken CreateAtKeywordToken
	(
		AtKeyword atKeyword, string? value = null, ISourceElement? predecessor = null
	)
	{
		value = PrepareKeywordValue(atKeyword, value);
		var length = value.Length;
		var span = CreateSpan(predecessor, length);

		return new AtKeywordToken(atKeyword, value, span, Separator.Empty, Separator.Empty);
	}

	protected static ValueKeywordToken CreateValueKeywordToken
	(
		ValueKeyword valueKeyword, string? value = null, ISourceElement? predecessor = null
	)
	{
		value = PrepareKeywordValue(valueKeyword, value);
		var length = value.Length;
		var span = CreateSpan(predecessor, length);

		return new ValueKeywordToken(valueKeyword, value, span, Separator.Empty, Separator.Empty);
	}

	private static SourceSpan CreateSpan(ISourceElement? predecessor = null, int length = 1)
	{
		var predecessorEnd = predecessor?.Span.Start;
		var startingLine = predecessorEnd?.LineNumber ?? 1;
		var startingColumn = predecessorEnd?.ColumnNumber + 1 ?? 1;
		var start = new SourceCoordinates(startingLine, startingColumn);
		var end = new SourceCoordinates(startingLine, startingColumn + length - 1);
		return new SourceSpan(start, end);
	}

	private static string PrepareKeywordValue<T>(T keyword, string? value = null) where T : Enum
	{
		if(value is null)
			return keyword.ToString();

		value.ShouldBe(keyword.ToString(), StringCompareShould.IgnoreCase);
		return value;
	}
}
