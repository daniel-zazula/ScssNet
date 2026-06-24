using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class IdentifierParserTests
{
	private static readonly string[] Identifiers = 
	[
		"table", "CamelCase", "h4", "custom-class", "-experimental-property", "--custom-var"
	];
	public static IEnumerable<object[]> IdentifierParams => Identifiers.ToParams();

	[TestMethod]
	[DynamicData(nameof(IdentifierParams))]
	public void ShouldParseIdentifier(string source)
	{
		var sourceReader = new SourceReaderMock(source);
		var identifierParser = new IdentifierParser();

		var identifierToken = identifierParser.Parse(sourceReader, Separator.Empty, () => Separator.Empty);

		identifierToken.ShouldNotBeNull();
		identifierToken!.Text.ShouldBe(source);
		identifierToken.LeadingSeparator.ShouldBe(Separator.Empty);
		identifierToken.TrailingSeparator.ShouldBe(Separator.Empty);
		sourceReader.End.ShouldBeTrue();
	}

	public static IEnumerable<object[]> NonIdentifiers => CommentParserTests.CommentParams
		.Concat(HashValueParserTests.HashValueParams)
		.Concat(StringParserTests.StringParams)
		.Concat(SymbolParserTests.SymbolParams)
		.Concat(UnitValueParserTests.UnitValueParams);

	[TestMethod]
	[DynamicData(nameof(NonIdentifiers))]
	public void ShouldNotParseNonIdentifiers(string source)
	{
		var sourceReader = new SourceReaderMock(source);
		var identifierParser = new IdentifierParser();

		var identifier = identifierParser.Parse(sourceReader, Separator.Empty, () => Separator.Empty);

		identifier.ShouldBeNull();
		sourceReader.End.ShouldBeFalse();
	}

}
