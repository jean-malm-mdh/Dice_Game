using Godot;
using System;

public partial class ComputeButton2 : Button
{
	[Export]
	Node gameArea;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		gameArea = GetParent<Node>().GetNode<Panel>("GameArea");
	}
	private void _on_ComputeButton_pressed()
	{
		int? score = null;
		string resultText = "Error";

		if (gameArea != null && (gameArea is GameArea))
		{
			var cards = (gameArea as GameArea).GetChildren();
			if (cards.Count == 0) resultText = "No Info";
			else
			{
				score = 0;
				foreach (var card in cards)
				{
					var _card = card as card;
					score += _card.Data.GetScore();
				}

				resultText = score.ToString();
			}
		}
		else
		{
			resultText = "Game Area not recognised!";
		}

		var info_dialog = new AcceptDialog();
		info_dialog.DialogText = resultText;
		info_dialog.WindowTitle = "Computed Score";
		AddChild(info_dialog);
		info_dialog.PopupCentered(new Vector2(200,200));
	}
}
