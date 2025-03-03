using Godot;
using System;

public partial class Elephant : Node2D
{
	Vector2 pointPos = Vector2.Zero;
    public override void _Process(double delta)
    {
		float rotation = (float) GetNode<VSlider>("CanvasLayer/Control/LeftSliderArea/ControlSlider").Value;
		Rotation += rotation * 0.1f * (float) delta;
		float scale = (float) GetNode<VSlider>("CanvasLayer/Control/VSlider2").Value;
		Line2D loin =  GetNode<Line2D>("Line2D");
		pointPos += new Vector2(30,0) * -scale * (float) delta;
		pointPos.X = Mathf.Clamp(pointPos.X , -1500, 0);
		loin.SetPointPosition(1,pointPos);

    }



}
