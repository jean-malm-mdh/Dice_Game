using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static DiceGame.CardData;
using Godot;

namespace DiceGame
{
	public class CardData
	{
		public enum SuiteVal { Club=1, Diamond=2, Spade=3, Heart=4};
		public enum ValueVal { Ace=1, Two=2, Three=3, Four=4, Five=5, Six=6,Seven=7, Eight=8,Nine=9,Ten=10,Jack=11,Queen=12,King=13 };
		
		public delegate int ScoreFunc(
			SuiteVal suite, 
			ValueVal val, 
			bool isFaceDown);
		
		public const int NUMBER_OF_SUITES = 4;
		public const int NUMBER_OF_VALUES = 13;
	}
	
	public class Card
	{
		public SuiteVal Suite { get; private set; }
		public ValueVal Value { get; private set; }
		public bool IsFaceDown { get; set; }

		private static ScoreFunc scoreFunc = null;
		private static string ScoreExplanation = null;
		public static void SetScoreFunc(uint index)
		{
			var score_explanation = ScoreFunctions.ScoringFuncCollection[index];
			scoreFunc = score_explanation.Item1;
			ScoreExplanation = score_explanation.Item2;
		}
		
		public void Flip()
		{
			IsFaceDown = !IsFaceDown;
		}
		
		public int GetScore()
		{
			return scoreFunc(this.Suite, this.Value, this.IsFaceDown);
		}

		public Card(CardData.SuiteVal suite, CardData.ValueVal val)
		{
			Suite = suite;
			Value = val;
			if(scoreFunc == null)
				SetScoreFunc(GD.Randi() % (uint)ScoreFunctions.ScoringFuncCollection.Length);
		}
		public Card(uint suite_i, uint val_i) : this((CardData.SuiteVal)suite_i, (CardData.ValueVal)val_i) 
		{ }

		public Card(uint canonical_zero_value) : this(
			(CardData.SuiteVal)(canonical_zero_value / NUMBER_OF_VALUES + 1), 
			(CardData.ValueVal)(canonical_zero_value % NUMBER_OF_VALUES + 1))
		{ }

		public Card(IDictionary<string,uint> suite_value_ints) : this(suite_value_ints["suite"], suite_value_ints["value"])
		{}

		public IDictionary<string, uint> ToDictionary()
		{
			return new Dictionary<string, uint>
			{
				["suite"] = (uint)Suite,
				["value"] = (uint)Value
			};
		}

		public static Stack<Card> GetCards(Func<Card, uint> orderFunc)
		{
			var _cards = new List<Card>();
			for (uint i = 1; i <= NUMBER_OF_SUITES; ++i)
				for (uint j = 1; j <= NUMBER_OF_VALUES; ++j)
					_cards.Add(new Card(i, j));
			return new Stack<Card>(_cards.OrderBy(orderFunc));
		}
		public static Stack<Card> GetCardsCanonicalOrder(Func<Card, uint> orderFunc)
		{
			var res = new Stack<Card>();
			for (uint i = NUMBER_OF_SUITES; i > 0; --i)
				for (uint j = NUMBER_OF_VALUES; j > 0; --j)
					res.Push(new Card(i, j));
			return res;
		}

		public uint GetZeroBasedCanonicalOrder()
		{
			uint suite_i0 = ((uint)Suite) - 1;
			uint val_i0 = ((uint)Value) - 1;
			return suite_i0 * NUMBER_OF_VALUES + val_i0;
		}

		public override string ToString()
		{
			return $"{Value} of {Suite}s";
		}
	}
	public class ScoreFunctions
	{
		public static (CardData.ScoreFunc, string)[] ScoringFuncCollection = new (ScoreFunc, string)[] {
			// Count All Hearts Only
			(delegate (SuiteVal suite, ValueVal val, bool isFaceDown)
				{ return isFaceDown ? 0 : suite == SuiteVal.Heart ? (int)val : 0; }, 
				"Count only Heart cards (Ace = 1) that are facing up"),
			// Count All Spades Only
			(delegate (SuiteVal suite, ValueVal val, bool isFaceDown)
				{ return isFaceDown ? 0 : suite == SuiteVal.Spade ? (int)val : 0; }, 
				"Count only Spade cards (Ace = 1) that are facing up"),
			// Count All Diamonds Only
			(delegate (SuiteVal suite, ValueVal val, bool isFaceDown)
				{ return isFaceDown ? 0 : suite == SuiteVal.Diamond ? (int)val : 0; }, 
				"Count only Diamond cards (Ace = 1) that are facing up"),
			// Count All Clubs Only
			(delegate (SuiteVal suite, ValueVal val, bool isFaceDown)
				{ return isFaceDown ? 0 : suite == SuiteVal.Club ? (int)val : 0; }, 
				"Count only Club cards (Ace = 1) that are facing up")
		};
	}
}
