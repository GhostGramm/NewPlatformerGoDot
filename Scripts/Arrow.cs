using Godot;
using System;

public class Arrow : Node2D
{
   
    private float speed = 100f;
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        GD.Print("arrow movinig");
        Position = Transform.x * delta * speed;
    }
}
