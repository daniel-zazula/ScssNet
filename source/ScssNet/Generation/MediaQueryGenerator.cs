using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Generation;

internal class MediaQueryGenerator
{
	public void Generate(IMediaQuery mediaQuery, CssWriter writer)
	{
		switch(mediaQuery)
		{
			case MediaQueryList mediaQueryList:
				GenerateList(mediaQueryList, writer);
				break;
			case IMediaQueryExpression mediaQueryExpression:
				GenerateExpression(mediaQueryExpression, writer);
				break;
			default:
				throw new NotImplementedException("Can't generate unknown media query type");
		};
	}

	private void GenerateList(MediaQueryList mediaQueryList, CssWriter writer)
	{
		foreach(var listItem in mediaQueryList.Items)
		{
			GenerateListItem(listItem, writer);
		}
	}

	private void GenerateListItem(MediaQueryListItem mediaQueryListItem, CssWriter writer)
	{
		GenerateExpression(mediaQueryListItem.Value, writer);
		if(mediaQueryListItem.Comma is not null)
		{
			writer.Write(mediaQueryListItem.Comma);
		}
	}

	private void GenerateExpression(IMediaQueryExpression mediaQueryExpression, CssWriter writer)
	{
		switch(mediaQueryExpression)
		{
			case MediaQueryUnaryExpression mediaQueryUnaryExpression:
				GenerateUnaryExpression(mediaQueryUnaryExpression, writer);
				break;
			case MediaQueryTypeKeywordToken mediaQueryValueKeywordToken:
				GenerateValue(mediaQueryValueKeywordToken, writer);
				break;
			default:
				throw new NotImplementedException("Can't generate unknown media query expression type");
		}
	}

	private void GenerateUnaryExpression(MediaQueryUnaryExpression mediaQueryUnaryExpression, CssWriter writer)
	{
		writer.Write(mediaQueryUnaryExpression.OperatorKeyword.Text + " ");
		GenerateValue(mediaQueryUnaryExpression.Value, writer);
	}

	private void GenerateValue(MediaQueryTypeKeywordToken mediaQueryValueKeywordToken, CssWriter writer)
	{
		writer.Write(mediaQueryValueKeywordToken.Text);
	}
}
