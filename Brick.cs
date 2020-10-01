using Godot;
using System;

public class Brick : StaticBody2D
{
    public override void _Ready()
    {
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

    public void Hit()
    {
        QueueFree();
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
