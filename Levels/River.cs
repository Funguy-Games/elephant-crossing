using Godot;
using System;

public partial class River : TextureRect
{
	private Vector2 _targetPosition;
	private float _moveSpeed = 2000f;
	private bool _isMoving = false;

	/// <summary>
	/// Initializes the river's starting and target position.
	/// </summary>
	public override void _Ready()
	{
		Position = new Vector2(Position.X, -1000);
		_targetPosition = new Vector2(Position.X, -152.5f);
	}

	/// <summary>
	/// Moves the river toward the target position if in motion.
	/// </summary>
	/// <param name="delta">Frame time step.</param>
	public override void _Process(double delta)
	{
		if (!_isMoving)
			return;

		Position = Position.MoveToward(_targetPosition, (float)delta * _moveSpeed);

		if (Position.DistanceTo(_targetPosition) < 1f)
		{
			Position = _targetPosition;
			_isMoving = false;
		}
	}

	/// <summary>
	/// Resets the river to its initial off-screen position.
	/// </summary>
	public void MoveToStartPosition()
	{
		Position = new Vector2(Position.X, -1000);
		_isMoving = false;
	}

	/// <summary>
	/// Moves the river to center view if not already in motion.
	/// </summary>
	public void MoveToCenter()
	{
		Vector2 centerPosition = new Vector2(Position.X, -152.5f);

		if (!_isMoving && Position != centerPosition)
		{
			_targetPosition = centerPosition;
			_isMoving = true;
		}
	}

	/// <summary>
	/// Moves the river downward out of view if not already moving.
	/// </summary>
	public void MoveDown()
	{
		if (!_isMoving && Position <= new Vector2(Position.X, -152.5f))
		{
			_targetPosition = new Vector2(Position.X, 1000);
			_isMoving = true;
		}
	}
}
