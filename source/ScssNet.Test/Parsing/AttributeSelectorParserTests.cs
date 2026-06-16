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
		var source = Selectors.AttributeSelector;

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
		var source = $"[{Selectors.AttributeName}{operatorString}{Selectors.AttributeValue}]";

		var attributeSelector = ShouldParseAttributeSelector(source);
		attributeSelector.Operator?.Symbol.ShouldBe(operatorSymbol);
		attributeSelector.AssertAttributeValue();
		attributeSelector.Modifier?.ShouldBeNull();
	}

	[TestMethod]
	[DataRow("i")]
	[DataRow("I")]
	[DataRow("s")]
	[DataRow("S")]
	public void ShouldParseAttributeSelectorWithValue(string modifier)
	{
		var source = $"[{Selectors.AttributeName}={Selectors.AttributeValue} {modifier}]";

		var attributeSelector = ShouldParseAttributeSelector(source);
		attributeSelector.Operator?.Symbol.ShouldBe(Symbol.Equals);
		attributeSelector.AssertAttributeValue();
		attributeSelector.Modifier?.Text.ShouldBe(modifier);
	}

	private static AttributeSelector ShouldParseAttributeSelector(string source)
	{
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var attributeSelectorParser = provider.GetRequiredService<AttributeSelectorParser>();

		var attributeSelector = attributeSelectorParser.Parse(tokenReader);
		attributeSelector.ShouldNotBeNull();
		attributeSelector.AssertAttributeName();
		attributeSelector.Qualifier.ShouldBeNull();
		attributeSelector.Issues.ShouldBeEmpty();

		tokenReader.End.ShouldBeTrue();

		return attributeSelector;
	}
}
