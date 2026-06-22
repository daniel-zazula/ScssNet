using ScssNet.Parsing;
using ScssNet.Tokens;

namespace ScssNet.Structures;

public class UniversalSelector
(
	SymbolToken asterisk, ISelectorQualifier? qualifier
) : SourceElement, ISelectorQualifier
{
	public SymbolToken Asterisk => asterisk;
	public ISelectorQualifier? Qualifier => qualifier;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(asterisk, qualifier);

	public SourceCoordinates Start => asterisk.Start;
	public SourceCoordinates End => LastEnd(asterisk, qualifier);
}
