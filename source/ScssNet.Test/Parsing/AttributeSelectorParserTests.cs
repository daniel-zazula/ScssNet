using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class AttributeSelectorParserTests: ParserTestBase
{
	[TestMethod]
	public void ShouldParseAttributeSelector()
	{
		var source = TestSelectors.AttributeSelector;

		var attributeSelector = ShouldParseAttributeSelector(source);
		attributeSelector.Operator?.ShouldBeNull();
		attributeSelector.Value?.ShouldBeNull();
		attributeSelector.Modifier?.ShouldBeNull();
	}

	[TestMethod]
	[DataRow(["=", Symbol.Equals])]
	[DataRow(["~=", Symbol.ContainsWord])]
	[DataRow(["|=", Symbol.StartsWithWord])]
	[DataRow(["^=", Symbol.StartsWith])]
	[DataRow(["$=", Symbol.EndsWith])]
	[DataRow(["*=", Symbol.Contains])]
	public void ShouldParseAttributeSelectorWithValue(string operatorString, Symbol operatorSymbol)
	{
		var source = $"[{TestSelectors.AttributeName}{operatorString}{TestSelectors.AttributeValue}]";

		var attributeSelector = ShouldParseAttributeSelector(source);
		attributeSelector.Operator?.Symbol.ShouldBe(operatorSymbol);
		attributeSelector.AssertValueText();
		attributeSelector.Modifier?.ShouldBeNull();
	}

	[TestMethod]
	[DataRow("i")]
	[DataRow("I")]
	[DataRow("s")]
	[DataRow("S")]
	public void ShouldParseAttributeSelectorWithValue(string modifier)
	{
		var source = $"[{TestSelectors.AttributeName}={TestSelectors.AttributeValue} {modifier}]";

		var attributeSelector = ShouldParseAttributeSelector(source);
		attributeSelector.Operator?.Symbol.ShouldBe(Symbol.Equals);
		attributeSelector.AssertValueText();
		attributeSelector.Modifier?.Text.ShouldBe(modifier);
	}

	private static AttributeSelector ShouldParseAttributeSelector(string source)
	{
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var attributeSelectorParser = provider.GetRequiredService<AttributeSelectorParser>();

		var attributeSelector = attributeSelectorParser.Parse(tokenReader);
		attributeSelector.ShouldNotBeNull();
		attributeSelector.AssertAttributeText();
		attributeSelector.Qualifier.ShouldBeNull();
		attributeSelector.Issues.ShouldBeEmpty();

		tokenReader.End.ShouldBeTrue();

		return attributeSelector;
	}
}
