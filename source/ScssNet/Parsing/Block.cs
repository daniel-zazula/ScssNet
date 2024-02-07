using System;
using System.Collections.Generic;
using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class Block(SymbolToken openBrace, ICollection<Property> properties, SymbolToken closeBrace)
	{
		public SymbolToken OpenBrace { get; } = openBrace;
		public ICollection<Property> Properties { get; } = properties;
		public SymbolToken CloseBrace { get; } = closeBrace;
	}

	internal class BlockParser(Lazy<PropertyParser> PropertyParser): ParserBase
	{
		internal Block? Parse(TokenReader tokenReader)
		{
			var openBrace = Match(tokenReader, Symbol.OpenBrace);
			if(openBrace is null)
				return null;

			var property = PropertyParser.Value.Parse(tokenReader);
			var properties = new List<Property>();
			while(property != null)
			{
				properties.Add(property);
				property = PropertyParser.Value.Parse(tokenReader);
			}

			var closeBrace = Require(tokenReader, Symbol.CloseBrace);
			return new Block(openBrace, properties, closeBrace);
		}
	}
}
