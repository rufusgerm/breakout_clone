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

    [Signal]
    public delegate void OnBrickBroken();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _screenSize = GetViewport().Size;
        Hide();
        Start();
    }

    public override void _PhysicsProcess(float delta)
    {
        var collisionInfo = MoveAndCollide(_velocity * delta);
        if (collisionInfo != null)
        {
            string colliderType = collisionInfo.Collider.GetType().ToString();

            if (colliderType == "Player")
            {
                float playerVelocity = (int)collisionInfo.Collider.Get("prevVelocity");

                GD.Print("Player Velocity: " + playerVelocity);

                if (playerVelocity != 0)
                    _velocity.x = _velocity.x + (playerVelocity * 0.25f);
                else if (Math.Abs(_velocity.x) > 125 || Math.Abs(_velocity.y) > 200)
                {
                    _velocity.x *= 0.85f;
                    _velocity.y *= 0.85f;
                    Scalar -= (Scalar >= 1.015f) ? 0.015f : 0f;
                }
                GetNode<AudioStreamPlayer>("Pop").Play();
                if (Scalar < MAX_SCALAR)
                {
                    Scalar += 0.02f;
                    _velocity *= Scalar;
                }
            }

            _velocity = _velocity.Bounce((collisionInfo.Normal));


            if (colliderType == "Brick")
            {
                GetNode<AudioStreamPlayer>("Break").Play();
                collisionInfo.Collider.Call("Hit");
                EmitSignal("OnBrickBroken");
            }
            else if (colliderType == "Player")
            {

            }

        }
    }
    public void Start()
    {
        Position = _startPos;
        _velocity = new Vector2(125, 200);
        Scalar = 1;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", false);
    }

    public void ClearBall()
    {
        Hide();
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
    }
}
