using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class IdentifierParserTests
{
	private static readonly string[] Identifiers = ["table", "CamelCase", "custom-class", "-experimental-property"];
	public static IEnumerable<object[]> IdentifierParams => Identifiers.ToParams();
	private static readonly Separator? LeadingSeparator = null;
	private static readonly Separator TrailingSeparator = new([]);

	[DataTestMethod]
	[DynamicData(nameof(IdentifierParams))]
	public void ShouldParseIdentifier(string source)
	{
		var sourceReader = new SourceReaderMock(source);
		var identifierParser = new IdentifierParser();

		var identifierToken = identifierParser.Parse(sourceReader, LeadingSeparator, () => TrailingSeparator);

		identifierToken.ShouldNotBeNull();
		identifierToken!.Text.ShouldBe(source);
		identifierToken.LeadingSeparator.ShouldBe(LeadingSeparator);
		identifierToken.TrailingSeparator.ShouldBe(TrailingSeparator);
		sourceReader.End.ShouldBeTrue();
	}

	public static IEnumerable<object[]> NonIdentifiers => CommentParserTests.CommentParams
		.Concat(HexValueParserTests.HexValueParams)
		.Concat(StringParserTests.StringParams)
		.Concat(SymbolParserTests.SymbolParams)
		.Concat(UnitValueParserTests.UnitValueParams);

	[DataTestMethod]
	[DynamicData(nameof(NonIdentifiers))]
	public void ShouldNotParseNonIdentifiers(string source)
	{
		var sourceReader = new SourceReaderMock(source);
		var identifierParser = new IdentifierParser();

		var identifier = identifierParser.Parse(sourceReader, LeadingSeparator, () => TrailingSeparator);

		identifier.ShouldBeNull();
		sourceReader.End.ShouldBeFalse();
	}

}
