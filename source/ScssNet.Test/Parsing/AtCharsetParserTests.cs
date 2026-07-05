using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class AtCharsetParserTests : ParserTestBase
{
	[TestMethod]
	public void ShouldParseCharset()
	{
		var source = "@charset \"UTF-8\";";
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var stylesheetParser = provider.GetRequiredService<StatementParser>();

		var element = stylesheetParser.Parse(tokenReader);
		element.ShouldNotBeNull();
		element.ShouldBeOfType<AtCharset>();

		element.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}
}
