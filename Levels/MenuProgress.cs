using Godot;
using System;

public partial class MenuProgress : PathFollow2D
{
	[Export] private float _progressSpeed = 200f;
	[Export] public float _startingRatio = 0f;
	public bool _isMoving = false;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (!_isMoving) return;

		Progress += (float)delta * _progressSpeed;
		_progressSpeed += 10f;

		if (ProgressRatio == 1)
		{
			_isMoving = !_isMoving;
			_progressSpeed = 200f;
		}
	}

	private void StarProgress()
	{
		_isMoving = !_isMoving;
	}
}
