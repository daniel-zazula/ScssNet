using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class RuleParser(Lazy<ValueParser> valueParser)
{
	internal Rule? Parse(ITokenReader tokenReader)
	{
		var property = tokenReader.Match<IdentifierToken>();
		if(property is null)
			return null;

		var colon = tokenReader.Require(Symbol.Colon);
		var value = valueParser.Value.Parse(tokenReader) ?? throw new NotImplementedException("Handle missing value");
		var important = ParseImportant(tokenReader);
		var semiColon = tokenReader.Match(Symbol.SemiColon);

		return new Rule(property, colon, value, important, semiColon);
	}

	private ImportantValue? ParseImportant(ITokenReader tokenReader)
	{
		var exclamation = tokenReader.Match(Symbol.Exclamation);
		if (exclamation is null)
			return null;

		var important = tokenReader.RequireIdentifier();
		if (!string.Equals(important.Text, "important", StringComparison.OrdinalIgnoreCase))
		{
			var issue = new Issue(IssueType.Error, $"Expected 'important' after '!', but found '{important.Text}'.");
			important = new IdentifierToken
			(
				important.Text, important.Start, important.End, important.LeadingSeparator, important.TrailingSeparator,
				[issue]
			);
		}

		return new ImportantValue(exclamation, important);
	}
}
