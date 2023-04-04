using DiceGame;
using Godot;
using System;
using static DiceGame.Card;
public class CardList : ItemList
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Visible = false;

        Items = new Godot.Collections.Array();
        for (int i = 0; i < 52; ++i)
        {
            Items.Add(new Card((uint)i / 13 + 1, (uint)i % 13 + 1));
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
