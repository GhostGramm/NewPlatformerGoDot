using Godot;
using Godot.Collections;
using System;

public class EnemyArcher : Node2D
{
    AnimatedSprite animatedSprite;
    private PlayerController player;
    private bool Active = false;
    private bool ableToShoot = false;
    private float shootTimer = 1f;
    private float shootTimerReset = 1f;
    private Position2D spawnPoint;
    [Export]
    public PackedScene ArrowInstance;
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        spawnPoint = GetNode<Position2D>("spawnProjectile");
        animatedSprite =  GetNode<AnimatedSprite>("AnimatedSprite");    //assigning the child animated sprite
    }

    private void _on_Range_body_entered(object body)
    {
        if(body is PlayerController)       //setting object body as player
        {
            player = body as PlayerController;
            GD.Print("Player in Sight" + body);
            Active = true;
        }
    }

    private void _on_Range_body_exited(object body)
    {
        if(body is PlayerController)
        {
            GD.Print("Lost Sight of Player" + body);
            Active = false;
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(Active)
        {
            var spaceState = GetWorld2d().DirectSpaceState;
            Dictionary result = spaceState.IntersectRay(this.Position,player.Position, new Godot.Collections.Array{this});
            if(result != null)
            {
                if(result.Contains("collider"))
                {
                    if(result["collider"] == player)
                    {
                        ableToShoot = true;
                    }
                    else
                    {
                        animatedSprite.Play("idle");
                    }
                }
            }
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
                Arrow arrow = ArrowInstance.Instance() as Arrow;
                Owner.AddChild(arrow);
                arrow.Position = spawnPoint.Position;
                animatedSprite.Play("attack");
                GD.Print("shooting");
                ableToShoot = false;
                shootTimer = shootTimerReset;
            }
        }
    }
}
