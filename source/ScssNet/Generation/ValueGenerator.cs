using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Generation;

internal class ValueGenerator(Lazy<FunctionCallGenerator> functionCallGenerator)
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
			case StringToken stringToken:
				writer.Write(stringToken);
				break;
			case UnitValueToken unitValueToken:
				writer.Write(unitValueToken);
				break;
			case FunctionCall functionCallToken:
				functionCallGenerator.Value.Generate(functionCallToken, writer);
				break;
			case ValueList valueList:
				foreach(var item in valueList.Items)
				{
					Generate(item.Value, writer);
					if (item.Comma is not null)
						writer.Write(item.Comma);
				}
				break;
			default:
				throw new InvalidOperationException($"Unsupported value type: {value.GetType().FullName}");
		}
	}
}
