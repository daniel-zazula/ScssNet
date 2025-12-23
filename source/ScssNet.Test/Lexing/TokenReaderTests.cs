using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class TokenReaderTests
{
	public static IEnumerable<object[]> SymbolsSource => AddSpacing(".");

	[DataTestMethod]
	[DynamicData(nameof(SymbolsSource))]
	public void ShouldMatchSymbolToken(string source)
	{
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();

		var symbolToken = tokenReader.Match(Symbol.Dot).ShouldNotBeNull();
		symbolToken!.Symbol.ShouldBe(Symbol.Dot);

		if (source.StartsWith(' '))
		{
			AssertSingleSpaceSeparator(symbolToken.LeadingSeparator);
		}

		if(source.EndsWith(' '))
		{
			AssertSingleSpaceSeparator(symbolToken.TrailingSeparator);
		}

		tokenReader.End.ShouldBeTrue();
	}

	private const string IdentifierText = "identifier";
	public static IEnumerable<object[]> IdentifiersSource => AddSpacing(IdentifierText);

	[DataTestMethod]
	[DynamicData(nameof(IdentifiersSource))]
	public void ShouldMatchIdentifierToken(string source)
	{
		var identifierToken = TestTokenMatch<IdentifierToken>(source);
		identifierToken.Text.ShouldBe(IdentifierText);
	}

	private const string StringText = @"""some string""";
	public static IEnumerable<object[]> StringsSource => AddSpacing(StringText);

	[DataTestMethod]
	[DynamicData(nameof(StringsSource))]
	public void ShouldMatchStringToken(string source)
	{
		var stringToken = TestTokenMatch<StringToken>(source);
		stringToken.Text.ShouldBe(StringText);
	}

	private static T TestTokenMatch<T>(string source) where T : class, IToken, ISeparatedToken
	{
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();

		var token = tokenReader.Match<T>().ShouldNotBeNull();

		if(source.StartsWith(' '))
		{
			AssertSingleSpaceSeparator(token.LeadingSeparator);
		}

		if(source.EndsWith(' '))
		{
			AssertSingleSpaceSeparator(token.TrailingSeparator);
		}

		tokenReader.End.ShouldBeTrue();

		return token;
	}

	private static ServiceProvider BuildServiceProvider(string source)
	{
		var reader = new StringReader(source);

		var services = new ServiceCollection();
		services.AddSingleton<TextReader>(reader);
		services.AddReaders();
		services.AddTokenParsers();

		return services.BuildServiceProvider();
	}

	private static void AssertSingleSpaceSeparator(Separator separator)
	{
		separator.ShouldNotBeNull();
		var token = separator!.Tokens.ShouldHaveSingleItem();
		var whiteSpaceToken = token.ShouldBeOfType<WhiteSpaceToken>();
		whiteSpaceToken.Text.ShouldBe(" ");
	}

	private static IEnumerable<object[]> AddSpacing(string source)
	{
		yield return new object[] { source };
		yield return new object[] { " " + source };
		yield return new object[] { source + " " };
		yield return new object[] { " " + source + " " };
	}
}
