using Godot;
using System;

public class PlayerController : KinematicBody2D
{
    private int speed = 100;
    private int jumpSpeed = 3000;
    private int gravity = 6000;
    private float friction = .1f;
    private float acceleration = .5f;
    private int dashSpeed = 1000;
    private bool isDashing = false;
    private float dashTimer = 0.2f;
    private float dashTimerReset = 0.2f;


    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Vector2 direction = new Vector2();
        if(!isDashing)
        {
            int control = 0;
            if(Input.IsActionPressed("ui_left"))
            {
                control -= 1;
            }
            if(Input.IsActionPressed("ui_right"))
            {
                control += 1;
            }
            if(control != 0)
            {
                direction.x = Mathf.Lerp(direction.x,control * speed,acceleration);  //interpolates between the direction and acceleration for smooth movement
            }
            else
            {
                direction.x = Mathf.Lerp(direction.x,0,friction);
            }
        }
        
        if(Input.IsActionJustPressed("ui_up"))
        {
            if(IsOnFloor())
            {
                direction.y -= jumpSpeed;
                // player can only jump when its on the floor
            }
        }
        if(Input.IsActionJustPressed("dash"))
        {
            if(Input.IsActionPressed("ui_left"))
            {
                direction.x = -dashSpeed;
                isDashing = true;
            }
            if(Input.IsActionPressed("ui_right"))
            {
                direction.x = dashSpeed;
                isDashing = true;
            }
            //dashing

            dashTimer = dashTimerReset;   //resets the dashTimer to 0.2f once more
        }
        if(isDashing)
        {
            dashTimer -= delta;     //counts down the dashTimer from delta
            if(dashTimer <= 0)
            {
                isDashing = false;
            }
        }
        
        direction.y += gravity * delta;
        MoveAndSlide(direction, Vector2.Up);
    }
}
