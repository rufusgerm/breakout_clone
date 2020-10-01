using Godot;
using System;

public class GameBoard : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
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
        int i = 0;
        while (i < BRICK_COUNT)
        {
            // instance new brick
            StaticBody2D brickNode = brick.Instance() as StaticBody2D;
            brickArea.AddChild(brickNode);
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
        INITIAL_OFFSET = ((windowWidth % brickWidth) / 2) + brickWidth / 2;

        return columnCount;
    }

    public void Start(Vector2 pos)
    {
        GetNode<Node2D>("BrickArea").Position = pos;
        GetNode<CollisionShape2D>("Borders/Bottom").Position = new Vector2(360, 1024);
        GetNode<CollisionShape2D>("Borders/Top").Position = new Vector2(360, 86);
        DrawGameBoard();
        GetNode<CollisionShape2D>("GameOverArea/CollisionShape2D").Disabled = false;
        Show();
    }

    public void OnGameOverAreaEntered(Node body)
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
