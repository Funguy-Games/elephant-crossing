using Godot;
using System;

public partial class Joki : TextureRect
{
	private Vector2 _targetPosition;
	private float _moveSpeed = 2000f;
	public bool _isMoving = false;

	public override void _Ready()
	{
		Position = new Vector2(Position.X, -1000);
		_targetPosition = new Vector2(Position.X, -152.5f);
	}

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

	public void MoveToStartPosition()
	{
		Position = new Vector2(Position.X, -1000);
		_isMoving = false;
	}

	public void MoveToCenter()
	{
		if (!_isMoving && Position != new Vector2(Position.X, -152.5f))
		{
			_targetPosition = new Vector2(Position.X, -152.5f);
			_isMoving = true;
		}
	}

	public void MoveDown()
	{
		if (!_isMoving && Position <= new Vector2(Position.X, -152.5f))
		{
			_targetPosition = new Vector2(Position.X, 1000);
			_isMoving = true;
		}
	}
}
