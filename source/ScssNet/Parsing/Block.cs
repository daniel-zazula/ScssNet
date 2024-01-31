using System;
using System.Collections.Generic;
using System.Text;
using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class Block(ICollection<Property> properties)
	{
		public ICollection<Property> Properties { get; } = properties;
	}

	internal class BlockParser(Lazy<PropertyParser> PropertyParser)
	{
		internal Block? Parse(TokenReader tokenReader)
		{
			var property = PropertyParser.Value.Parse(tokenReader);
			if(property is null)
				return null;

			var properties = new List<Property>();
			while(property != null)
			{
				properties.Add(property);
				property = PropertyParser.Value.Parse(tokenReader);
			}

			return new Block(properties);
		}
	}
}
