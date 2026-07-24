using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class CssWriterTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldWriteSymbol()
	{
		const Symbol symbol = Symbol.OpenBracket;

		var symbolToken = CreateSymbolToken(symbol);

		var provider = BuildServiceProvider();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		cssWriter.Write(symbolToken);

		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(symbol.ToChars());
	}

	[TestMethod]
	public void ShouldWriteIdentifier()
	{
		const string identifier = "foo";

		var identifierToken = CreateIdentifierToken(identifier);

		var provider = BuildServiceProvider();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		cssWriter.Write(identifierToken);

		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(identifier);
	}

	[TestMethod]
	public void ShouldWriteString()
	{
		const string str = @"""some string""";

		var stringToken = CreateStringToken(str);

		var provider = BuildServiceProvider();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		cssWriter.Write(stringToken);

		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(str);
	}

	[TestMethod]
	public void ShouldWriteHashValue()
	{
		const string hashValue = "#ff0000";

		var hashToken = CreateHashValueToken(hashValue);

		var provider = BuildServiceProvider();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		cssWriter.Write(hashToken);

		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(hashValue);
	}

	[TestMethod]
	public void ShouldWriteKeyword()
	{
		const string keyword = "import";

		var keywordToken = CreateKeywordToken(Keyword.Import, keyword);

		var provider = BuildServiceProvider();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		cssWriter.Write(keywordToken);

		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(keyword);
	}
}
