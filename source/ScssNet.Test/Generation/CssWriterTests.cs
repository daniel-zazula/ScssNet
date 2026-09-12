using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Test.ElementCreation;
using ScssNet.Tokens;

namespace ScssNet.Test.Generation;

[TestClass]
public class CssWriterTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldWriteSymbol()
	{
		const Symbol symbol = Symbol.OpenBracket;

		var symbolToken = SymbolToken.Create(symbol);

		var provider = BuildServiceProvider();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		cssWriter.Write(symbolToken);

		provider.GetStringWriter().ShouldContain(symbol.ToChars());
	}

	[TestMethod]
	public void ShouldWriteIdentifier()
	{
		const string identifier = "foo";

		var identifierToken = IdentifierToken.Create(identifier);

		var provider = BuildServiceProvider();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		cssWriter.Write(identifierToken);

		provider.GetStringWriter().ShouldContain(identifier);
	}

	[TestMethod]
	public void ShouldWriteString()
	{
		const string str = @"""some string""";

		var stringToken = StringToken.Create(str);

		var provider = BuildServiceProvider();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		cssWriter.Write(stringToken);

		provider.GetStringWriter().ShouldContain(str);
	}

	[TestMethod]
	public void ShouldWriteHashValue()
	{
		const string hashValue = "#ff0000";

		var hashToken = HashValueToken.Create(hashValue);

		var provider = BuildServiceProvider();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		cssWriter.Write(hashToken);

		provider.GetStringWriter().ShouldContain(hashValue);
	}

	[TestMethod]
	public void ShouldWriteKeyword()
	{
		const string keyword = "import";

		var keywordToken = AtKeywordToken.Create(AtKeyword.Import, keyword);

		var provider = BuildServiceProvider();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		cssWriter.Write(keywordToken);

		provider.GetStringWriter().ShouldContain(keyword);
	}

	[TestMethod]
	public void ShouldWriteUnitValue()
	{
		var numberToken = UnitValueToken.Create(1.5m, "em");

		var provider = BuildServiceProvider();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		cssWriter.Write(numberToken);

		provider.GetStringWriter().ShouldContain("1.5em");
	}
}
