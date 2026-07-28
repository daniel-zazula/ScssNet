using ScssNet.Tokens;

namespace ScssNet.Structures;

public class ImportantValue: SourceElement, ISourceElement
{
	public SymbolToken Exclamation { get; }
	public KeywordToken Important { get; }

	public SourceCoordinates Start => Exclamation.Start;
	public SourceCoordinates End => Important.End;
	public Separator LeadingSeparator => Exclamation.LeadingSeparator;
	public Separator TrailingSeparator => Important.TrailingSeparator;
	public IEnumerable<Issue> Issues => Exclamation.Issues.Concat(Important.Issues);

	internal ImportantValue(SymbolToken exclamation, KeywordToken important)
	{
		Exclamation = exclamation;
		Important = important;
	}
}
