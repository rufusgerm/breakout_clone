using Godot;
using System;

public class Main : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var player = GetNode<Player>("Player");
        var board = GetNode<GameBoard>("GameBoard");
        var playerStartPos = GetNode<Position2D>("StartPosition");
        var brickStartPos = GetNode<Position2D>("BrickPosition");

        board.Start(brickStartPos.Position);
        player.Start(playerStartPos.Position);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
