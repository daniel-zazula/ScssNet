using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class ClassSelectorParserTests: ParserTestBase
{
	[TestMethod]
	public void ShouldParseClassSelector()
	{
		var source = Selectors.ClassSelector;
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var classSelectorParser = provider.GetRequiredService<ClassSelectorParser>();

		var classSelector = classSelectorParser.Parse(tokenReader);
		classSelector.ShouldNotBeNull();
		classSelector.AssertClassText();
		classSelector.Qualifier.ShouldBeNull();
		classSelector.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}
}
