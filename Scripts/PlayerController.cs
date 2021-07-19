using Godot;
using System;

public class PlayerController : KinematicBody2D
{
    private int speed = 100;
    private int jumpSpeed = 2000;
    private int gravity = 3000;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Vector2 direction = new Vector2();
        if(Input.IsActionPressed("ui_left"))
        {
            direction.x -= speed;
        }
        if(Input.IsActionPressed("ui_right"))
        {
            direction.x += speed;
        }
        if(Input.IsActionJustPressed("ui_up"))
        {
            if(IsOnFloor())
            {
                direction.y -= jumpSpeed;
            }
        }
        direction.y += gravity * delta;
        MoveAndSlide(direction, Vector2.Up);
    }
}
