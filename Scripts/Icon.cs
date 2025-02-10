using Godot;
using System;

public partial class Icon : PathFollow2D
{
	[Export] private float speed = 100;
	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public override void _Ready()
	{

	}
	public override void _Process(double delta)
	{
		Progress += (float)delta * speed;
		if (ProgressRatio == 1)
		{
			QueueFree();
		}
	}
}
