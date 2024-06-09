﻿using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public interface IValue: ISourceElement
	{
	}

	public class MissingValue: IValue
	{
		public SourceCoordinates Start { get; }
		public SourceCoordinates End { get; }
		public IEnumerable<Issue> Issues { get; }

		internal MissingValue(SourceCoordinates start)
		{
			Start = start;
			End = start;
			Issues = [new Issue(IssueType.Error, "Missing value (measure unit, string or function)")];
		}
	}

	internal class ValueParser : ParserBase
	{
		internal IValue? Parse(TokenReader tokenReader)
		{
			var peeked = tokenReader.Peek();
			if (peeked is not IValue)
				return null;

			return (IValue)tokenReader.Read()!;
		}
	}
}
