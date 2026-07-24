using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class AttributeSelectorGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldWriteAttributeSelector()
	{
		var openBracket = CreateSymbolToken(Symbol.OpenBracket);
		var attributeIdentifier = CreateIdentifierToken("attr", columnNumber: openBracket.End.ColumnNumber + 1);
		var equalSign = CreateSymbolToken(Symbol.Equals, columnNumber: attributeIdentifier.End.ColumnNumber + 1);
		var value = CreateStringToken(@"""some-value""", columnNumber: equalSign.End.ColumnNumber + 1);
		var closeBracket = CreateSymbolToken(Symbol.CloseBracket, columnNumber: value.End.ColumnNumber + 1);

		var attributeSelector = new AttributeSelector(openBracket, attributeIdentifier, equalSign, value, null, closeBracket, null);

		var provider = BuildServiceProvider();
		var generator = provider.GetRequiredService<SelectorGenerator>();
		var cssWriter = provider.GetRequiredService<CssWriter>();
		generator.Generate(attributeSelector, cssWriter);

		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe("[attr=\"some-value\"]");
	}
}
