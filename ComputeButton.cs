using Godot;
using System;
using System.Collections.Generic;
using System.Text;

public static class GameLog
{
	public static int Tries { get; private set; }
	static List<string> states = new List<string>();
	static List<int> results = new List<int>();
	static int cheatEnabledAtTry = -1;
	public static IEnumerable<int> GetResults()
	{
		return results;
	}

	public static void AddTry(int result, string state, bool cheatEnabled=false)
	{
		Tries++;
		states.Add(state);
		results.Add(result);
		if (cheatEnabled && cheatEnabledAtTry == -1) cheatEnabledAtTry = Tries;
	}

	public static void Reset()
	{
		Tries = 0;
		results.Clear();
		results = new List<int>();
		states.Clear();
		states = new List<string>();
		cheatEnabledAtTry = -1;
    }

}

public partial class ComputeButton : Button
{
	[Export]
	GameArea gameArea;

	[Export]
	Log logger;

	[Export]
	TextEdit Score;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		var parent = GetParent<Node>();

		gameArea = parent.GetNode<GameArea>("GameArea");
		logger = parent.GetNode<Log>("Log");

	}
	private void _on_ComputeButton_pressed()
	{
		int? score = null;
		string resultText = "Error";
		StringBuilder state_sb = new StringBuilder();
		if (gameArea != null && (gameArea is GameArea))
		{
			var cards = (gameArea as GameArea).GetChildren();
			if (cards.Count == 0) resultText = "No Info";
			else
			{
				score = 0;
				foreach (var card in cards)
				{
					if(!(card is CardObject)) continue;
					var _card = card as CardObject;
					state_sb.AppendLine(_card.ToString());
					score += _card.GetScore();
				}

				resultText = score.ToString();
				bool hasCheatBeenEnabled = (GetParent().GetNode<Deck>("Deck")?.CanCheat).GetValueOrDefault();
				GameLog.AddTry(score.Value, state_sb.ToString(), hasCheatBeenEnabled);
				logger.AddLine($"Try nr {GameLog.Tries}: " + resultText + (hasCheatBeenEnabled ? " - cheat enabled" : ""));
			}
		}
		else
		{
			resultText = "Game Area not recognised!";
		}

		var info_dialog = new AcceptDialog();
		info_dialog.DialogText = "The score is : " + resultText;
		info_dialog.WindowTitle = "Computed Score";
		AddChild(info_dialog);
		info_dialog.PopupCentered(new Vector2(200,200));
	}
}
