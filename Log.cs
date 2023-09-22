using Godot;
using System;

public class Log : RichTextLabel
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BbcodeEnabled = true;
	}

	public void AddLine(String line)
	{
		AppendBbcode(line);
		Newline();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
