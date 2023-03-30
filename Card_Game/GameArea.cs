using Godot;
using System;

public partial class GameArea : Panel
{
	PackedScene cardScene = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cardScene = ResourceLoader.Load<PackedScene>("res://Card.tscn");
	}


	public void Reset()
	{
		foreach(Node child in this.GetChildren())
		{
			child.QueueFree();
		}
	}

	public override bool CanDropData(Vector2 atPosition, object data)
	{
		if (!(data is Godot.Collections.Dictionary)) {
			return false;
		}
		var _data = (data as Godot.Collections.Dictionary);
		bool hasSuiteData = _data.Contains("suite");
		bool hasValueData = _data.Contains("value");
		return hasSuiteData && hasValueData;
	}

	public override void DropData(Vector2 atPosition, object data)
	{
		var _data = (data as Godot.Collections.Dictionary);
		var card = (card)cardScene.Instance();
		card.RectPosition = atPosition;
		card.Data = new DiceGame.CardData((DiceGame.CardData.SuiteVal)_data["suite"], (DiceGame.CardData.ValueVal)_data["value"]);
		card.isFaceDown = _data.Contains("faceDown");
		this.AddChild(card);
	}
}
