using ScssNet.Tokens;

namespace ScssNet.Structures;

public class ImportantValue(SymbolToken exclamation, ValueKeywordToken important) : ISyntaxStructure
{
	public SymbolToken Exclamation => exclamation;
	public ValueKeywordToken Important => important;

	public SourceSpan Span => SourceSpan.From(exclamation, important);

	public Issues Issues => Exclamation.Issues.Concat(Important.Issues);
}
