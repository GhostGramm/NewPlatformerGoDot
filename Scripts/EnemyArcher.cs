using Godot;
using System;

public class EnemyArcher : Node2D
{
    AnimatedSprite animatedSprite;
    private PlayerController player;
    private bool Active = false;
    private bool ableToShoot = false;
    private float shootTimer = 1f;
    private float shootTimerReset = 1f;
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animatedSprite =  GetNode<AnimatedSprite>("AnimatedSprite");    //assigning the child animated sprite
    }

    private void _on_Range_body_entered(object body)
    {
        if(body is PlayerController)       //setting object body as player
        {
            GD.Print("Player in Sight" + body);
            Active = true;
        }
    }

    private void _on_Range_body_exited(object body)
    {
        GD.Print("Lost Sight of Player" + body);
        Active = false;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(Active)
        {
            ableToShoot = true;
            animatedSprite.Play("attack");
        }
        else
        {
            animatedSprite.Play("idle");
            ableToShoot = false;
        }

        if(ableToShoot)     //shooting timer
        {
            shootTimer -= delta;
            if(shootTimer <= 0)
            {
                GD.Print("shooting");
                ableToShoot = false;
                shootTimer = shootTimerReset;
            }
        }
    }
}
