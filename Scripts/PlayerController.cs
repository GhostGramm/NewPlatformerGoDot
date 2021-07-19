using Godot;
using System;

public class PlayerController : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Vector2 direction = new Vector2();
        if(Input.IsActionJustPressed("ui_left"))
        {
            direction.x += 50;
        }
        if(Input.IsActionJustPressed("ui_right"))
        {
            direction.x -= 500;
        }
    
    }
}
