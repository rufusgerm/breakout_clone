using Godot;
using System;

public class HUD : CanvasLayer
{
    [Signal]
    public delegate void StartGame();

    private int readyCount = 3;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public void NewGameSetup()
    {
        UpdateScore(0);
        GetNode<Timer>("ReadyTimer").Start();
    }

    public void UpdateScore(int score)
    {
        GetNode<Label>("GameTitleScore").Text = "Score: " + score;
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
            ChangeReadyMessage();
            readyCount--;
        }
        else
        {
            GetNode<Label>("GameMessageTimer").Hide();
            GetNode<Timer>("ReadyTimer").Stop();
        }
    }

    public void ChangeReadyMessage()
    {
        GetNode<Label>("GameMessageTimer").Text = "Get Ready!\n" + readyCount;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
