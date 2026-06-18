using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Generation;

internal class ValueGenerator
{
	public void Generate(IValue value, CssWriter writer)
	{
		switch (value)
		{
			case HashValueToken hexValueToken:
				writer.Write(hexValueToken);
				break;
			case IdentifierToken identifierToken:
				writer.Write(identifierToken);
				break;
			default:
				throw new InvalidOperationException($"Unsupported value type: {value.GetType().FullName}");
		}
	}
}
