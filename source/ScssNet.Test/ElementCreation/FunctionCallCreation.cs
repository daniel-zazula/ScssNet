using System;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Test.ElementCreation;

internal static class FunctionCallCreation
{
	extension(FunctionCall)
	{
		internal static FunctionCall Create(Func<ISourceElement, ICollection<ValueListItem>> createArguments)
		{
			var identifierToken = IdentifierToken.Create("someFunc");
			var openParenthesisToken = SymbolToken.Create(Symbol.OpenParenthesis, predecessor: identifierToken);
			var valueList = new ValueList(createArguments(openParenthesisToken));
			var closeParenthesisToken = SymbolToken.Create(Symbol.CloseParenthesis, predecessor: (ISourceElement?)valueList ?? openParenthesisToken);

			return new FunctionCall(identifierToken, openParenthesisToken, valueList, closeParenthesisToken);
		}
	}
}
internal static class ValueListCreation
{
	extension(ValueList)
	{
		internal static ValueList Create(ICollection<ValueListItem> items)
		{
			return new ValueList(items);
		}

		internal static ValueListItem CreateItem(IValue value, bool withComma)
		{
			var commaToken = withComma ? SymbolToken.Create(Symbol.Comma, predecessor: value) : null;
			return new ValueListItem(value, commaToken);
		}
	}
}
