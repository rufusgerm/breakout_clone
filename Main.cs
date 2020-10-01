using Godot;
using System;

public class Main : Node2D
{
    private int _score;

    [Signal]
    private delegate void BeginRound();
    public override void _Ready()
    {

    }
    // Called when the node enters the scene tree for the first time.
    public void NewGame()
    {
        _score = 0;

        var player = GetNode<Player>("Player");
        var board = GetNode<GameBoard>("GameBoard");
        var playerStartPos = GetNode<Position2D>("StartPosition");
        var brickStartPos = GetNode<Position2D>("BrickPosition");

        board.Start(brickStartPos.Position);
        player.Start(playerStartPos.Position);

        GetNode<Timer>("CountdownTimer").Start();
        var hud = GetNode<HUD>("HUD");
        hud.NewGameSetup();
    }

    public void OnCountdownTimerTimeout()
    {
        EmitSignal("BeginRound");
    }

    public void IncrementScore()
    {
        _score++;
        GetNode<HUD>("HUD").UpdateScore(_score);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
