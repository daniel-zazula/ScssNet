using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class AtCharsetGeneratorTests : GeneratorTestBase
{
	[TestMethod]
	public void ShouldGenerateFromAtRuleGenerator()
	{
		var atCharset = CreateAtCharset();

		var provider = BuildServiceProvider();
		var atRuleGenerator = provider.GetRequiredService<AtRuleGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		atRuleGenerator.Generate(atCharset, writer);

		AssertAtCharset(provider);
	}

	[TestMethod]
	public void ShouldGenerateFromAtCharsetGenerator()
	{
		var atCharset = CreateAtCharset();

		var provider = BuildServiceProvider();
		var atCharsetGenerator = provider.GetRequiredService<AtCharsetGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		atCharsetGenerator.Generate(atCharset, writer);

		AssertAtCharset(provider);
	}

	internal static AtCharset CreateAtCharset()
	{
		var at = CreateSymbolToken(Symbol.At);
		var keyword = CreateKeywordToken(Keyword.Charset, columnNumber: at.End.ColumnNumber + 1);
		var name = CreateStringToken("\"utf-8\"", columnNumber: keyword.End.ColumnNumber + 1);
		var semiColon = CreateSymbolToken(Symbol.SemiColon, columnNumber: name.End.ColumnNumber + 1);

		return new AtCharset(at, keyword, name, semiColon);
	}

	internal static void AssertAtCharset(ServiceProvider provider)
	{
		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe("@charset \"utf-8\";", StringCompareShould.IgnoreCase);
	}
}
