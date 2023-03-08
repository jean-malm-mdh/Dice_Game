using DiceGame;
using Godot;
using Godot.Collections;
using System;
public partial class card : Control
{
	public CardData Data { get; set; }

	public bool isFaceDown = true;

	AnimatedSprite2D animSprite = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
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
		animSprite.Pause();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		set_animation();
	}

	public override Variant _GetDragData(Vector2 atPosition)
	{
		var data = new Dictionary<string, uint>(Data.ToDictionary());
		var preview = new Label();
		preview.Size = new Vector2(50, 50);
		preview.Text = Data.ToString();
		SetDragPreview(preview);
		QueueFree();
		return data;
	}
}
