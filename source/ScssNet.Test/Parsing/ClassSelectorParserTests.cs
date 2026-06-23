using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class ClassSelectorParserTests: ParserTestBase
{
	[TestMethod]
	[DataRow(Selectors.ClassSelector)]
	[DataRow(".-name-with-dash")]
	public void ShouldParseClassSelector(string source)
	{
		var text = source[1..]; // Remove the leading dot for comparison
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var classSelectorParser = provider.GetRequiredService<ClassSelectorParser>();

		var classSelector = classSelectorParser.Parse(tokenReader);
		classSelector.ShouldNotBeNull();
		classSelector.AssertClassText(text);
		classSelector.Qualifier.ShouldBeNull();
		classSelector.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}
}
