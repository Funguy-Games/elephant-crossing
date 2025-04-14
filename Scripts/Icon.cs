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
	[Export] public float Speed = 100;

	// Called every frame. 'delta' is the elapsed time since the previous frame
	[Export]
	public RiverItem Type
	{
		get; private set;
	}

	public bool isMoving = true;
	private Sprite2D _trashSprite = null;
	[Export] private Texture2D _trashState = null;
	private Vector2 position = Vector2.Zero;

	public override void _Ready()
	{
		_trashSprite = GetNode<Sprite2D>("Sprite2D");
	}
	public override void _Process(double delta)
	{
		if (!isMoving)
		{
			_trashSprite.Texture = _trashState;
			return;
		}

		Progress += (float)delta * Speed;
		if (Type == RiverItem.None)
		{
			if (GlobalPosition.X < position.X)
			{
				GetNode<Sprite2D>("Sprite2D").FlipV = true;
			}
			else
			{
				GetNode<Sprite2D>("Sprite2D").FlipV = false;
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

