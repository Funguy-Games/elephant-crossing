using Godot;
using System;

public partial class Elephant : Node2D
{

    public override void _Process(double delta)
    {
		float rotation = (float) GetTree().Root.GetNode<VSlider>("Main/CanvasLayer/Control/VSlider").Value;
		GetNode<Sprite2D>("Sprite2D").Rotation += rotation * (float) delta;
		float scale = (float) GetTree().Root.GetNode<VSlider>("Main/CanvasLayer/Control/VSlider2").Value;
		GetNode<Sprite2D>("Sprite2D").Scale += new Vector2(1,0) * scale * (float) delta;

    }



}
