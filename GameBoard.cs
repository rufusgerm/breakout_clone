using Godot;
using System;

public class GameBoard : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Signal]
    public delegate void SignalGameOver();

    private int windowWidth;
    private PackedScene brick = GD.Load<PackedScene>("res://Brick.tscn");
    private Node2D brickArea;
    private Vector2 brickSizeVector;
    private int INITIAL_OFFSET;
    private int ROW_COUNT = 10;
    private int COL_COUNT;
    private int BRICK_COUNT;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        windowWidth = (int)GetViewport().Size.x;
        brickSizeVector = (brick.Instance() as StaticBody2D).GetNode<Sprite>("Sprite").Texture.GetSize();
        brickArea = GetNode<Node2D>("BrickArea");
        COL_COUNT = DetermineColumnCount();
        BRICK_COUNT = ROW_COUNT * COL_COUNT;
        Hide();
    }

    public void DrawGameBoard()
    {
        Random random = new Random();
        int i = 0;
        while (i < BRICK_COUNT)
        {
            // instance new brick
            StaticBody2D brickNode = brick.Instance() as StaticBody2D;
            brickArea.AddChild(brickNode);
            //position brick relative to previous
            brickNode.Modulate = new Color(
                (float)random.NextDouble(),
                (float)random.NextDouble(),
                (float)random.NextDouble(),
                1f
                );
            brickNode.Position = new Vector2(
                (i % COL_COUNT * brickSizeVector.x) + INITIAL_OFFSET,
                (i / COL_COUNT) * brickSizeVector.y
            );

            i++;
        }
    }

    public int DetermineColumnCount()
    {
        int brickWidth = (int)brickSizeVector.x;
        int columnCount = windowWidth / brickWidth;
        INITIAL_OFFSET = ((windowWidth % brickWidth) / 2) + brickWidth / 2;

        return columnCount;
    }

    public void Start(Vector2 pos)
    {
        GetNode<Node2D>("BrickArea").Position = pos;
        DrawGameBoard();
        GetNode<CollisionShape2D>("GameOverArea/CollisionShape2D").SetDeferred("disabled", false);
        GetNode<CollisionShape2D>("Borders/PreGameBorder").SetDeferred("disabled", true);
        Show();
    }

    public void Reset()
    {
        GetNode<Node2D>("BrickArea").Position = new Vector2(0, 0);
        GetNode<CollisionShape2D>("GameOverArea/CollisionShape2D").SetDeferred("disabled", true);
        GetNode<CollisionShape2D>("Borders/PreGameBorder").SetDeferred("disabled", false);
        Hide();
        GetTree().CallGroup("bricks", "queue_free");
    }

    public void OnGameOverAreaEntered(Node body)
    {
        EmitSignal("SignalGameOver");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
