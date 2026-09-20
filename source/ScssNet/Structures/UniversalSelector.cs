using ScssNet.Tokens;

namespace ScssNet.Structures;

public class UniversalSelector
(
	SymbolToken asterisk, ISelectorQualifier? qualifier
) : ISyntaxStructure, ISelectorQualifier
{
	public SymbolToken Asterisk => asterisk;
	public ISelectorQualifier? Qualifier => qualifier;

	public SourceSpan Span => SourceSpan.From(asterisk, qualifier);

	public Issues Issues => Issues.ConcatFrom(asterisk, qualifier);
}
