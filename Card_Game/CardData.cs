using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DiceGame
{
	public class CardData
	{
		public enum SuiteVal { Club=1, Diamond=2, Spade=3, Heart=4};
		public enum ValueVal { Ace=1, Two=2, Three=3, Four=4, Five=5, Six=6,Seven=7, Eight=8,Nine=9,Ten=10,Jack=11,Queen=12,King=13 };

		public SuiteVal Suite { get; private set; }
		public ValueVal Value { get; private set; }

		public delegate int ScoreFunc(CardData data);
		private static ScoreFunc scoreFunc = delegate (CardData d){ return d.Suite == SuiteVal.Heart ? (int)d.Value : 0; };
		public int GetScore()
		{
			return scoreFunc(this);
		}

		public CardData(SuiteVal suite, ValueVal value)
		{
			Suite = suite;
			Value = value;
		}
		public CardData(uint suite_i, uint val_i) : this((SuiteVal)suite_i, (ValueVal)val_i) 
		{}
		public CardData(IDictionary<string,uint> suite_value_ints) : this(suite_value_ints["suite"], suite_value_ints["value"])
		{}

		public IDictionary<string, uint> ToDictionary()
		{
			return new Dictionary<string, uint>
			{
				["suite"] = (uint)Suite,
				["value"] = (uint)Value
			};
		}

		public static Stack<CardData> GetCards(Func<CardData, uint> orderFunc)
		{
			var _cards = new List<CardData>();
			for (uint i = 1; i <= 4; ++i)
				for (uint j = 1; j <= 13; ++j)
					_cards.Add(new CardData(i, j));
			return new Stack<CardData>(_cards.OrderBy(orderFunc));
		}
		public static Stack<CardData> GetCardsCanonicalOrder(Func<CardData, uint> orderFunc)
		{
			var res = new Stack<CardData>();
			for (uint i = 4; i > 0; --i)
				for (uint j = 13; j > 0; --j)
					res.Push(new CardData(i, j));
			return res;
		}

		public uint GetZeroBasedCanonicalOrder()
		{
			uint suite_iz = ((uint)Suite) - 1;
			uint val_iz = ((uint)Value) - 1;
			return suite_iz * 13 + val_iz;
		}

		public override string ToString()
		{
			return $"{Value} of {Suite}s";
		}
	}
}
