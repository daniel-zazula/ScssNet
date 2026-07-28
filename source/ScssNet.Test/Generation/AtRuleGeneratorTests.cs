using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class AtRuleGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldWriteAtCharset()
	{
		var at = CreateSymbolToken(Symbol.At);
		var keyword = CreateKeywordToken(Keyword.Charset, columnNumber: at.End.ColumnNumber + 1);
		var name = CreateStringToken("\"utf-8\"", columnNumber: keyword.End.ColumnNumber + 1);
		var semiColon = CreateSymbolToken(Symbol.SemiColon, columnNumber: name.End.ColumnNumber + 1);

		var atCharset = new AtCharset(at, keyword, name, semiColon);

		var provider = BuildServiceProvider();
		var atRuleGenerator = provider.GetRequiredService<AtRuleGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		atRuleGenerator.Generate(atCharset, writer);

		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe("@charset \"utf-8\";", StringCompareShould.IgnoreCase);
	}
}
