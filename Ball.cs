using Godot;
using System;

public class Ball : KinematicBody2D
{
    private Vector2 _velocity = new Vector2(125, 200);
    [Export]
    float Scalar = 1;
    [Export]
    float MAX_SCALAR = 1.35f;
    Vector2 _screenSize;
    Vector2 _startPos = new Vector2(360, 540);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _screenSize = GetViewport().Size;
        Hide();
        Start(_startPos);
    }

    public override void _PhysicsProcess(float delta)
    {
        var collisionInfo = MoveAndCollide(_velocity * delta);
        if (collisionInfo != null)
        {
            string colliderType = collisionInfo.Collider.GetType().ToString();
            _velocity = _velocity.Bounce((collisionInfo.Normal));
            if (colliderType == "Brick")
                collisionInfo.Collider.Call("Hit");
            if (Scalar < MAX_SCALAR && colliderType == "Player")
            {
                Scalar += 0.05f;
                _velocity *= Scalar;
            }

        }
    }

    public void Start(Vector2 pos)
    {
        Position = pos;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;

    }
}