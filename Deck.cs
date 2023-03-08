using Godot;
using System;
using DiceGame;
using System.Collections.Generic;
using System.Linq;
using Godot.Collections;

public partial class Deck : ColorRect
{
	Stack<CardData> cards = new Stack<CardData>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Reset();
	}

	public void Reset()
	{
		cards = CardData.GetCards(_ => GD.Randi());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override Variant _GetDragData(Vector2 atPosition)
	{
		CardData data = null;
		cards.TryPop(out data);
		var previewData = new Label
		{
			Size = new Vector2(50, 50),
			Text = data?.ToString()
		};
		SetDragPreview(previewData);
		var res = new Godot.Collections.Dictionary<string, int>();
		res["suite"] = (int)data.Suite;
		res["value"] = (int)data.Value;
		return res;
	}

}
