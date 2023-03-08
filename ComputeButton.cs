using Godot;
using System;

public partial class ComputeButton : Button
{
	[Export]
	Node gameArea;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}	
	
	private void ComputeButtonOnPress()
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
		info_dialog.Title = "Computed Score";
		AddChild(info_dialog);
		info_dialog.PopupCentered(new Vector2I(200,200));
	}
}


