using Godot;
using System;

public partial class ResetButton : Button
{
	private void _on_ResetButton_pressed()
	{
		GetParent().GetNode<GameArea>("GameArea").Reset();
        GetParent().GetNode<Deck>("Deck").DisableCheating();
    }
}




