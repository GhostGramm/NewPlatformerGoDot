using Godot;
using System;

public class GameManager : Node2D
{
    [Export]
    public Position2D RespawnPoint;
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
       
    }

    public void RespawnPlayer()
    {
        GD.Print(RespawnPoint.GlobalPosition);
        GD.Print("game manager called");
        PlayerController player =  GetNode<PlayerController>("Player");
        player.Position = RespawnPoint.Position;    //assign the position of the Respawn point to the player
        GD.Print(RespawnPoint.Position);
        GD.Print(player.Position);
        player.Respawn();   //call the player respawn function
    }

    public void _on_Player_RespawnPlayer()
    {
        RespawnPlayer();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}