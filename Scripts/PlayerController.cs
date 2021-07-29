using Godot;
using System;

public class PlayerController : KinematicBody2D
{
    private int speed = 100;
    private int jumpSpeed = 200;
    private int gravity = 600;
    private float friction = .1f;
    private float acceleration = .5f;
    private int RayCastSpeed = 100;
    private int dashSpeed = 400;
    private bool isDashing = false;
    private float dashTimer = 0.2f;
    private float dashTimerReset = 0.2f;
    private bool isRayCasting = false;
    private float RayCastTimer = .4f;
    private float RayCastTimerReset = .4f;
    Vector2 direction = new Vector2();
    


    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
        if(!isDashing && !isRayCasting)
        {
            CharacterMovement(delta);
        }
        
        if(Input.IsActionJustPressed("jump"))
        {
            if(IsOnFloor())
            {
                direction.y = -jumpSpeed;
                // player can only jump when its on the floor
            }
        }

        ProcessRayCast(delta);

        ProcessDash();
        
        if(isDashing)
        {
            dashTimer -= delta;     //counts down the dashTimer from delta
            if(dashTimer <= 0)
            {
                isDashing = false;
                direction = new Vector2(0,0); //resets the direction after dashing for smooth stop
            }
        }
        else
        {
            direction.y += gravity * delta; 
        }

        if(isRayCasting)
        {
            RayCastTimer -= delta;
            if(RayCastTimer <=0)
            {
                isRayCasting = false;
                RayCastTimer = RayCastTimerReset;
            }
        }
        
        MoveAndSlide(direction, Vector2.Up);
    }

    public void CharacterMovement(float delta)
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

    public void ProcessDash()
    {
        if(Input.IsActionJustPressed("dash"))
        {
            if(IsOnFloor())
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
                if(Input.IsActionPressed("ui_up"))
                {
                    direction.y = -dashSpeed;
                    isDashing = true;
                }

                //Diagonal Dashing
                if(Input.IsActionPressed("ui_right") && Input.IsActionPressed("ui_up"))
                {
                    direction.x = dashSpeed;
                    direction.y = -dashSpeed;
                    isDashing = true;
                }
                if(Input.IsActionPressed("ui_left") && Input.IsActionPressed("ui_up"))
                {
                    direction.x = -dashSpeed;
                    direction.y = -dashSpeed;
                    isDashing = true;
                }
            }
            
            //dashing

            dashTimer = dashTimerReset;   //resets the dashTimer to 0.2f once more
        }
    }
    public void ProcessRayCast(float delta)
    {
        //RayCast enables character to bounce inbetween walls
        if(Input.IsActionJustPressed("jump") && GetNode<RayCast2D>("RayCast2D_Left").IsColliding())
        {
            direction.y = -jumpSpeed;
            direction.x = RayCastSpeed;
            isRayCasting = true;
        }
        else if(Input.IsActionJustPressed("jump") && GetNode<RayCast2D>("RayCast2D_Right").IsColliding())
        {
            direction.y = -jumpSpeed;
            direction.x = -RayCastSpeed;
            isRayCasting = true;
        }
    }
}
