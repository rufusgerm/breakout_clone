using Godot;
using System;

public class HUD : CanvasLayer
{
    [Signal]
    public delegate void StartGame();

    private int readyCount = 3;
    private Node2D header;
    private Label centerMessage;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        header = GetNode<Node2D>("Header");
        centerMessage = GetNode<Label>("CenterMessage");
    }

    public void NewGameSetup()
    {
        UpdateScore(0);
        GetNode<Timer>("ReadyTimer").Start();
    }

    public void Reset()
    {
        centerMessage.Text = "Game Over!\n Play Again?";
        centerMessage.Show();
    }

    public void UpdateScore(int score)
    {
        header.GetNode<Label>("GameTitleScore").Text = "Score: " + score;
    }
    public void OnStartButtonPressed()
    {
        GetNode<Button>("StartButton").Hide();
        EmitSignal("StartGame");
    }

    public void OnReadyTimerTimeout()
    {
        if (readyCount > 0)
        {
            centerMessage.Text = "Get Ready\n" + readyCount;
            readyCount--;
        }
        else
        {
            centerMessage.Hide();
            GetNode<Timer>("ReadyTimer").Stop();
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
