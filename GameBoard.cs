using Godot;
using System;

public class GameBoard : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private int windowWidth;
    private PackedScene brick = GD.Load<PackedScene>("res://Brick.tscn");
    private Vector2 brickSizeVector;
    private int INITIAL_OFFSET;
    private int ROW_COUNT = 4;
    private int COL_COUNT;
    private int BRICK_COUNT;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        windowWidth = (int)GetViewport().Size.x;
        brickSizeVector = (brick.Instance() as StaticBody2D).GetNode<Sprite>("Sprite").Texture.GetSize();
        COL_COUNT = DetermineColumnCount();
        BRICK_COUNT = ROW_COUNT * COL_COUNT;
        Hide();
        DrawGameBoard();
    }

    public void DrawGameBoard()
    {
        GD.Print("Initial Offset: " + INITIAL_OFFSET);
        int i = 0;
        while (i < BRICK_COUNT)
        {
            // instance new brick
            StaticBody2D brickNode = brick.Instance() as StaticBody2D;
            AddChild(brickNode);
            //position brick relative to previous
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
        GD.Print("Temp Col Count: " + columnCount);
        GD.Print("Offset: " + windowWidth % brickWidth);
        INITIAL_OFFSET = ((windowWidth % brickWidth) / 2) + brickWidth / 2;

        return columnCount;
    }

    public void Start(Vector2 pos)
    {
        Position = pos;
        Show();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
