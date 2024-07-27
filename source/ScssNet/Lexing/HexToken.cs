﻿using System.Text;
using ScssNet.Parsing;

namespace ScssNet.Lexing
{
	public class HexToken: IToken, IValue
	{
		public string Value { get; }

		public SourceCoordinates Start { get; }
		public SourceCoordinates End { get; }
		public IEnumerable<Issue> Issues => [];

		internal HexToken(string value, SourceCoordinates start, SourceCoordinates end)
		{
			Value = value;
			Start = start;
			End = end;
		}
	}

	internal class HexParser
	{
		public HexToken? Parse(ISourceReader reader)
		{
			if(reader.Peek() != '#')
				return null;

			var startCoordinates = reader.GetCoordinates();

			var stringBuilder = new StringBuilder();
			stringBuilder.Append(reader.Read());
			while(IsHexDigit(reader.Peek()))
				stringBuilder.Append(reader.Read());

			return new HexToken(stringBuilder.ToString(), startCoordinates, reader.GetCoordinates());
		}

		private static bool IsHexDigit(char peeked)
		{
			const int lowerCaseA = 'a';
			const int lowerCaseF = 'f';
			const int upperCaseA = 'A';
			const int upperCaseF = 'F';

			return char.IsDigit(peeked) || (lowerCaseA <= peeked && peeked <= lowerCaseF) || (upperCaseA <= peeked && peeked <= upperCaseF);
		}
	}
}
