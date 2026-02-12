using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class AttributeSelectorParserTests: ParserTestBase
{
	[TestMethod]
	public void ShouldParseAttributeSelector()
	{
		var source = "[attr]";
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var attributeSelectorParser = provider.GetRequiredService<AttributeSelectorParser>();

		var attributeSelector = attributeSelectorParser.Parse(tokenReader);
		attributeSelector.ShouldNotBeNull();
		attributeSelector!.Attribute.Text.ShouldBe("attr");
		attributeSelector.Operator?.ShouldBeNull();
		attributeSelector.Value?.ShouldBeNull();
		attributeSelector.Modifier?.ShouldBeNull();
		attributeSelector.Qualifier.ShouldBeNull();
		attributeSelector.Issues.ShouldBeEmpty();

		tokenReader.End.ShouldBeTrue();
	}

	[DataTestMethod]
	[DataRow(["=", Symbol.Equals])]
	[DataRow(["~=", Symbol.ContainsWord])]
	[DataRow(["|=", Symbol.StartsWithWord])]
	[DataRow(["^=", Symbol.StartsWith])]
	[DataRow(["$=", Symbol.EndsWith])]
	[DataRow(["*=", Symbol.Contains])]
	public void ShouldParseAttributeSelectorWithValue(string operatorString, Symbol operatorSymbol)
	{
		var source = $"[attr{operatorString}\"value\"]";
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var attributeSelectorParser = provider.GetRequiredService<AttributeSelectorParser>();

		var attributeSelector = attributeSelectorParser.Parse(tokenReader);
		attributeSelector.ShouldNotBeNull();
		attributeSelector!.Attribute.Text.ShouldBe("attr");
		attributeSelector.Operator?.Symbol.ShouldBe(operatorSymbol);
		attributeSelector.Value?.Text.ShouldBe("\"value\"");
		attributeSelector.Modifier?.ShouldBeNull();
		attributeSelector.Qualifier.ShouldBeNull();
		attributeSelector.Issues.ShouldBeEmpty();

		tokenReader.End.ShouldBeTrue();
	}

	[DataTestMethod]
	[DataRow("i")]
	[DataRow("I")]
	[DataRow("s")]
	[DataRow("S")]
	public void ShouldParseAttributeSelectorWithValue(string modifier)
	{
		var source = $"[attr=\"value\" {modifier}]";
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var attributeSelectorParser = provider.GetRequiredService<AttributeSelectorParser>();

		var attributeSelector = attributeSelectorParser.Parse(tokenReader);
		attributeSelector.ShouldNotBeNull();
		attributeSelector!.Attribute.Text.ShouldBe("attr");
		attributeSelector.Operator?.Symbol.ShouldBe(Symbol.Equals);
		attributeSelector.Value?.Text.ShouldBe("\"value\"");
		attributeSelector.Modifier?.Text.ShouldBe(modifier);
		attributeSelector.Qualifier?.ShouldBeNull();
		attributeSelector.Issues.ShouldBeEmpty();

		tokenReader.End.ShouldBeTrue();
	}
}
