using ScssNet.Tokens;

namespace ScssNet.Structures;

public class ImportantValue(SymbolToken exclamation, ValueKeywordToken important) : SourceElement, ISyntaxStructure
{
	public SymbolToken Exclamation => exclamation;
	public ValueKeywordToken Important => important;

	public SourceSpan Span => SourceSpan.From(exclamation, important);

	public IEnumerable<Issue> Issues => Exclamation.Issues.Concat(Important.Issues);
}
