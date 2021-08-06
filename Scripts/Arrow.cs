using Godot;
using System;

public class Arrow : Node2D
{

    private float speed = 100f;
    private float lifeSpan = 10f;
    [Export]
    private PackedScene PlayerControllerInstance;
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
        Position -= Transform.x * delta * speed;
        lifeSpan -= delta;
        if (lifeSpan <= 0)
        {
            destroyArrow();
        }
    }

    public void _on_Area2D_body_entered(object body)
    {
        if (body is PlayerController)
        {
            PlayerController player = body as PlayerController;
            player.arrowDamage();
            destroyArrow();
        }
    }

    public void destroyArrow()
    {
        QueueFree();
    }

}
