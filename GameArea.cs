using Godot;
using System;

public partial class GameArea : Panel
{
	PackedScene cardScene = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cardScene = ResourceLoader.Load<PackedScene>("res://card.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Reset()
	{
		foreach(var child in this.GetChildren())
		{
			child.QueueFree();
		}
	}

	public override bool _CanDropData(Vector2 atPosition, Variant data)
	{
		bool isCardData = (data.VariantType == Variant.Type.Dictionary) && (data.AsGodotDictionary().ContainsKey("suite")) && (data.AsGodotDictionary().ContainsKey("value"));
		return isCardData;
	}

	public override void _DropData(Vector2 atPosition, Variant data)
	{
		var _data = data.AsGodotDictionary<string, uint>();
		var card = (card)cardScene.Instantiate();
		card.Position = atPosition;
		card.Data = new DiceGame.CardData(_data["suite"], _data["value"]);
		card.isFaceDown = false;
		this.AddChild(card);
	}
}
