using System;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class TokenReaderKeywordTests : TokenReaderTestBase
{
	[TestMethod]
	[DataRow(nameof(AtKeywordToken))]
	[DataRow(nameof(MediaQueryOperatorKeywordToken))]
	[DataRow(nameof(MediaQueryTypeKeywordToken))]
	[DataRow(nameof(ValueKeywordToken))]
	public void ShouldThrowWhenTryingToMatchKeywordToken(string typeName)
	{
		var tokenReader = SetupTokenReader("someIdentifier");

		Action match = typeName switch
		{
			nameof(AtKeywordToken) => () => tokenReader.Match<AtKeywordToken>(),
			nameof(MediaQueryOperatorKeywordToken) => () => tokenReader.Match<MediaQueryOperatorKeywordToken>(),
			nameof(MediaQueryTypeKeywordToken) => () => tokenReader.Match<MediaQueryTypeKeywordToken>(),
			nameof(ValueKeywordToken) => () => tokenReader.Match<ValueKeywordToken>(),
			_ => throw new ArgumentException($"Unknown type name: {typeName}", nameof(typeName))
		};

		Should.Throw<InvalidOperationException>(match);

		tokenReader.End.ShouldBeFalse();
	}
}
