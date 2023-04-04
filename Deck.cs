using Godot;
using System;
using DiceGame;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

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
		if (cards.Count == 0) Reset();
		if (cards.Count > 0) return cards.Pop();
		return null;
	}

	public override object GetDragData(Vector2 atPosition)
	{
		Card data = GetTopCard();
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
	private void _on_Deck_gui_input(object @event)
	{
		if (!(@event is InputEventMouseButton)) return;
		var _event = @event as InputEventMouseButton;
		if (_event.ButtonIndex != (int)ButtonList.Right || !_event.Pressed) return;
        GD.PrintS(_event.AsText());
    }


}

