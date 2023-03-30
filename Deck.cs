using Godot;
using System;
using DiceGame;
using System.Collections.Generic;
using System.Linq;

public partial class Deck : ColorRect
{
	Stack<Card> cards = new Stack<Card>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Reset();
	}

	public void Reset()
	{
		GD.Randomize();
		cards = Card.GetCards(_ => GD.Randi());
	}

	public Card GetTopCard()
	{
		if (cards.Count > 0) return cards.Pop();
		return null;
	}

	public override object GetDragData(Vector2 atPosition)
	{
		Card data = null;
		data = GetTopCard();
		var previewData = new Label
		{
			RectSize = new Vector2(50, 50),
			Text = data?.ToString()
		};
		SetDragPreview(previewData);
		Godot.Collections.Dictionary<string, uint> res = new Godot.Collections.Dictionary<string, uint>();
		res["suite"] = (uint)data.Suite;
		res["value"] = (uint)data.Value;
		return res;
	}

}
