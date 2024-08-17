using FluentAssertions;
using ScssNet.Lexing;

namespace ScssNet.Test.Lexing
{
	[TestClass]
	public class StringParserTests
	{
		private static readonly string[] strings = [ "\"Some string\"", "'Other string'" ];
		public static IEnumerable<object[]> StringParams => strings.ToParams();

		[DataTestMethod]
		[DynamicData(nameof(StringParams))]
		public void ShouldParseString(string source)
		{
			var sourceReader = new SourceReaderMock(source);
			var stringParser = new StringParser();

			var stringToken = stringParser.Parse(sourceReader);

			stringToken.Should().NotBeNull();
			stringToken!.Text.Should().Be(source);
			sourceReader.End.Should().BeTrue();
		}

		public static IEnumerable<object[]> NonStrings => CommentParserTests.CommentParams
			.Concat(HexValueParserTests.HexValueParams)
			.Concat(IdentifierParserTests.IdentifierParams)
			.Concat(SymbolParserTests.SymbolParams)
			.Concat(UnitValueParserTests.UnitValueParams);

		[DataTestMethod]
		[DynamicData(nameof(NonStrings))]
		public void ShouldNotParseNonStrings(string source)
		{
			var sourceReader = new SourceReaderMock(source);
			var stringParser = new StringParser();

			var @string = stringParser.Parse(sourceReader);

			@string.Should().BeNull();
			sourceReader.End.Should().BeFalse();
		}
	}
}
