using Godot;
using System;

public enum RiverItem
{
	Trash,
	Eel,
	None
}

public partial class Icon : PathFollow2D
{
	[Export] private float speed = 100;
	public bool isMoving = true;
	// Called every frame. 'delta' is the elapsed time since the previous frame
	[Export]
	public RiverItem Type
	{
		get; private set;
	}

	public override void _Ready()
	{

	}
	public override void _Process(double delta)
	{
		if (!isMoving)
			return;

		Progress += (float)delta * speed;
		if (ProgressRatio == 1)
		{
			QueueFree();
		}
	}
}

