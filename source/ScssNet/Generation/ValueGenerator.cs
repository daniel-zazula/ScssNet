using ScssNet.SourceElements;
using ScssNet.Tokens;

namespace ScssNet.Generation;

internal class ValueGenerator
{
	public void Generate(IValue value, CssWriter writer)
	{
			switch (value)
			{
				case HexValueToken hexValueToken:
					writer.Write(hexValueToken);
					break;
				case IdentifierToken identifierToken:
					writer.Write(identifierToken);
					break;
				case MissingValue missingValue:
					break;
			}
	}
}
