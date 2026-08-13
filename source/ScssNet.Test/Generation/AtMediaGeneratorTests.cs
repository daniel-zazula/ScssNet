using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class AtMediaGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldGenerateFromAtRuleGenerator()
	{
		var atMedia = CreateAtMedia();

		var provider = BuildServiceProvider();
		var atRuleGenerator = provider.GetRequiredService<AtRuleGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		atRuleGenerator.Generate(atMedia, writer);

		AssertAtMedia(provider);
	}

	[TestMethod]
	public void ShouldGenerateFromAtMediaGenerator()
	{
		var atMedia = CreateAtMedia();

		var provider = BuildServiceProvider();
		var atMediaGenerator = provider.GetRequiredService<AtMediaGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		atMediaGenerator.Generate(atMedia, writer);

		AssertAtMedia(provider);
	}

	internal static AtMedia CreateAtMedia()
	{
		var at = CreateSymbolToken(Symbol.At);
		var keyword = CreateKeywordToken(Keyword.Media, columnNumber: at.End.ColumnNumber + 1);

		var mediaQuery = CreateIdentifierToken("screen", columnNumber: keyword.End.ColumnNumber + 1);

		var block = BlockGeneratorTests.CreateBlock(mediaQuery.End.ColumnNumber + 1);

		return new AtMedia(at, keyword, mediaQuery, block);
	}

	internal static void AssertAtMedia(ServiceProvider provider)
	{
		var expected = "@media screen " + BlockGeneratorTests.ExpectedBlock;

		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(expected, StringCompareShould.IgnoreCase);
	}
}
