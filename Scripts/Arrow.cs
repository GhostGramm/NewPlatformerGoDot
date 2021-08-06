using Godot;
using System;

public class Arrow : Node2D
{
   
    private float speed = 100f;
    private float lifeSpan =3f;
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        moveArrow(delta);
    }

    public void moveArrow(float delta)
    {
        GD.Print("arrow movinig");
        Position += Transform.x * delta * speed;
        lifeSpan -= delta;
        if(lifeSpan <= 0)
        {
            destroyArrow();
        }
    }

    public void destroyArrow()
    {
        QueueFree();
    }
}
