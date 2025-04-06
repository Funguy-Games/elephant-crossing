using Godot;
using System;

public partial class Joki : Sprite2D
{
	private Vector2 _targetPosition;
	private float _moveSpeed = 5f;
	public bool _isMoving = false;

	public override void _Ready()
	{
		Position = new Vector2(0, -1000);
		_targetPosition = new Vector2(0, 0);
	}

	public override void _Process(double delta)
	{
		if (!_isMoving)
			return;

		Position = Position.Lerp(_targetPosition, (float)delta * _moveSpeed);

		if (Position.DistanceTo(_targetPosition) < 1f)
		{
			Position = _targetPosition;
			_isMoving = false;
		}
	}

	public void MoveToStartPosition()
	{
		Position = new Vector2(0, -1000);
		_isMoving = false;
	}

	public void MoveToCenter()
	{
		if (!_isMoving && Position != new Vector2(0, 0))
		{
			_targetPosition = new Vector2(0, 0);
			_isMoving = true;
		}
	}

	public void MoveDown()
	{
		if (!_isMoving && Position == new Vector2(0, 0))
		{
			_targetPosition = new Vector2(0, 1000);
			_isMoving = true;
		}
	}
}
