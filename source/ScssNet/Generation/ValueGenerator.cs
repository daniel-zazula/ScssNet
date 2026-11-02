using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Generation;

internal class ValueGenerator(GeneratorContext generatorContext): IGenerator<IValue>
{
	private IGenerator<FunctionCall> FunctionCallGenerator => generatorContext.GetGenerator<FunctionCall>();
	
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
				FunctionCallGenerator.Generate(functionCallToken, writer);
				break;
			case ValueList valueList:
				Generate(valueList, writer);
				break;
			default:
				throw new InvalidOperationException($"Unsupported value type: {value.GetType().FullName}");
		}
	}

	private void Generate(ValueList list, CssWriter writer)
	{
		var firstItem = list.Items.First();
		var isCommaList = firstItem.Comma is not null;
		foreach(var item in list.Items)
		{
			if(!isCommaList && item != firstItem)
				writer.Write(" ");

			Generate(item.Value, writer);
			if (item.Comma is not null)
				writer.Write(item.Comma);
		}
	}
}
