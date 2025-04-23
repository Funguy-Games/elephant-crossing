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


	/// <summary>
	/// The type of river item this icon represents.
	/// </summary>
	[Export]
	public RiverItem Type { get; private set; }

	public bool isMoving = true;

	private Sprite2D _trashSprite = null;
	[Export] private Texture2D _trashState = null;
	private Vector2 _position = Vector2.Zero;

	/// <summary>
	/// Called when the node is added to the scene.
	/// </summary>
	public override void _Ready()
	{
		_trashSprite = GetNode<Sprite2D>("Sprite2D");
	}

	/// <summary>
	/// Updates the position and behavior of the icon each frame.
	/// </summary>
	/// <param name="delta">Time elapsed since the previous frame.</param>
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
			// Flip based on movement direction
			if (GlobalPosition.X < _position.X)
			{
				GetNode<Sprite2D>("Sprite2D").FlipV = true;
			}
			else
			{
				GetNode<Sprite2D>("Sprite2D").FlipV = false;
			}
			_position = GlobalPosition;
		}
		
		if (ProgressRatio == 1)
		{
			if (Type != RiverItem.None)
			{
				Level.Current.TrashInPlay -= 1;
			}

			QueueFree();
		}
	}
}

