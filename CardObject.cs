using DiceGame;
using Godot;
using Godot.Collections;
using System;
public partial class CardObject : Control
{
	public Card Data { get; set; }
	
	public int GetScore()
	{
		return Data.GetScore(); 
	}

	AnimatedSprite animSprite = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		if (Data == null)
		{
			Data = new Card(GD.Randi() % CardData.NUMBER_OF_SUITES + 1, GD.Randi() % CardData.NUMBER_OF_VALUES + 1);
		}
	}

	void set_animation()
	{
		animSprite.Animation = Data.IsFaceDown ? "Back" : "Front";
		animSprite.Frame = Data.IsFaceDown ? 0 : (int)Data.GetZeroBasedCanonicalOrder();
		animSprite.Play();
		animSprite.Stop();
	}

	void flip()
	{
		Data.Flip();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		set_animation();
	}

	Dictionary<string, uint> make_drag_data()
	{
		var move_data = new Dictionary<string, uint>(Data.ToDictionary());
		
		if (Data.IsFaceDown)
		{
			var value_does_not_matter = 1u;
			move_data["faceDown"] = value_does_not_matter;
		}
		return move_data;
	}

	public override object GetDragData(Vector2 position)
	{
		var dragData = make_drag_data();
		var preview = new Label();
		preview.RectSize = new Vector2(50, 50);
		preview.Text = Data.IsFaceDown ? "Unknown - Face Down" : Data.ToString();
		SetDragPreview(preview);
		QueueFree();
		return dragData;
	}
	private void _on_Card_gui_input(object @event)
	{
		if (@event == null) return;
		if (!(@event is InputEventMouseButton)) return;
		var input = @event as InputEventMouseButton;
		if(input.ButtonIndex != (int)ButtonList.Right) return;
		if (!input.Pressed) return;
		flip();
	}
}



