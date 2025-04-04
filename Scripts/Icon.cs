using Godot;
using System;

namespace ElephantCrossing;
public enum RiverItem
{
	Trash,
	Eel,
	Bottle,
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

	private Vector2 position = Vector2.Zero;
	public override void _Ready()
	{

	}
	public override void _Process(double delta)
	{
		if (!isMoving)
			return;

		Progress += (float)delta * speed;
		if (Type == RiverItem.None)
		{
			if(GlobalPosition.X < position.X)
			{
				GetNode<Sprite2D>("StaticBody2D/Sprite2D").FlipV = true;
			} else
			{
				GetNode<Sprite2D>("StaticBody2D/Sprite2D").FlipV = false;
			}
			position = GlobalPosition;
		}
		if (ProgressRatio == 1)
		{
			if (Type != RiverItem.None)
			{
				// Level.Current.Score -= 1;
				Level.Current.TrashInPlay -= 1;
			}

			QueueFree();
		}
	}
}

