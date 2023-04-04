using Godot;
using System;
using DiceGame;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

public partial class Deck : ColorRect
{
	Stack<Card> cards = new Stack<Card>();

    [Export]
    HSlider suiteSelector, valueSelector;

	[Export]
	Label lblValue, lblSuite;

    [Signal]
    public delegate void CheatEnabledEventHandler();

    public bool CanCheat { get; private set; }

	public void EnableCheating() 
	{ 
		CanCheat = true;
		suiteSelector.Visible = true;
        valueSelector.Visible = true;
        lblValue.Visible = true;
        lblSuite.Visible = true;
    }
	public void DisableCheating()
	{
		CanCheat = false;
		suiteSelector.Visible = false;
		valueSelector.Visible = false;
        lblValue.Visible = false;
        lblSuite.Visible = false;
    }



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        suiteSelector = GetParent<Node>().GetNode<HSlider>("SuiteSelector");
        valueSelector = GetParent<Node>().GetNode<HSlider>("ValueSelector");
        lblSuite = GetParent<Node>().GetNode<Label>("lblSuiteValue");
        lblValue = GetParent<Node>().GetNode<Label>("lblValueValue");
        Reset();
	}

	public void Reset()
	{
		GD.Randomize();
		cards = Card.GetCards(_ => GD.Randi());
		DisableCheating();
	}

	public Card GetTopCard()
	{
		if (cards.Count == 0) Reset();
		if (CanCheat) return new Card((CardData.SuiteVal)suiteSelector.Value, (CardData.ValueVal)valueSelector.Value);
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
		if (_event.Doubleclick)
		{
			EnableCheating();
			EmitSignal(nameof(CheatEnabledEventHandler));
		}
		GD.PrintS(_event.AsText());
	}


	private void _on_CardList_gui_input(object @event)
	{
		
	}
}



