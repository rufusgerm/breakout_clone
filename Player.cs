
using Godot;
using System;

public class Player : StaticBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";\
    [Export]
    int Speed = 100;
    float speedAccumulator = 0;
    int prevVelocity = 0;
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
    }

    public override void _Process(float delta)
    {
        var velocity = new Vector2();

        if (Input.IsActionPressed("ui_right"))
        {
            speedAccumulator += 66;
            velocity.x += 1;
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            speedAccumulator -= 66;
            velocity.x -= 1;
        }
        else
        {
            speedAccumulator *= 0.7f;
        }

        velocity = (velocity.Normalized() * Speed);
        velocity.x += speedAccumulator;

        prevVelocity = (int)velocity.x;

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
