
using Godot;
using System;

public class Player : StaticBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";\
    [Export]
    int Speed = 900;
    Vector2 _screenSize;
    // Vector2 _startPos = new Vector2(360, 900);
    float _playerWidth;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _screenSize = GetViewport().Size;
        Hide();
        var sprite = GetNode<Sprite>("Sprite");
        var size = sprite.Texture.GetSize();
        _playerWidth = size.x;
        // Start(_startPos);
    }

    public override void _Process(float delta)
    {
        var velocity = new Vector2();

        if (Input.IsActionPressed("ui_right"))
        {
            velocity.x += 1;
        }
        if (Input.IsActionPressed("ui_left"))
        {
            velocity.x -= 1;
        }

        velocity = velocity.Normalized() * Speed;

        Position += velocity * delta;

        Position = new Vector2(
            x: Mathf.Clamp(Position.x, 0 + _playerWidth * 2, _screenSize.x - _playerWidth * 2),
            y: Mathf.Clamp(Position.y, 0, _screenSize.y)
        );
    }

    public void Start(Vector2 pos)
    {
        Position = pos;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;

    }
}
