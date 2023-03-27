using DiceGame;
using Godot;
using Godot.Collections;
using System;
public partial class card : Control
{
	public CardData Data { get; set; }

	public bool isFaceDown = true;

	AnimatedSprite animSprite = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		if (Data == null)
		{
			Data = new CardData(GD.Randi() % 4 + 1, GD.Randi() % 13 + 1);
		}
	}

	void set_animation()
	{
		animSprite.Animation = isFaceDown ? "Back" : "Front";
		animSprite.Frame = isFaceDown ? 0 : (int)Data.GetZeroBasedCanonicalOrder();
		animSprite.Play();
		animSprite.Stop();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		set_animation();
	}

	public override object GetDragData(Vector2 position)
	{
		var data = new Dictionary<string, uint>(Data.ToDictionary());
		var preview = new Label();
		preview.RectSize = new Vector2(50, 50);
		preview.Text = Data.ToString();
		SetDragPreview(preview);
		QueueFree();
		return data;
	}

}
