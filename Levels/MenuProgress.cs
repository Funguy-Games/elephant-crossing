using Godot;
using System;

public partial class MenuProgress : PathFollow2D
{
	[Export] private float _progressSpeed = 200f;
	[Export] public float _startingRatio = 0f;
	[Export] public float _stoppingPoint = 0f;
	[Export] TextureButton button;
	public bool _isMoving = false;
	public bool _hasStopped = false;
	public override void _Ready()
	{
		ProgressRatio = _startingRatio;
		button.Disabled = true;
	}

	public override void _Process(double delta)
	{
		if (!_isMoving) return;

		button.Disabled = true;

		Progress += (float)delta * _progressSpeed;
		_progressSpeed += 10f;

		if (ProgressRatio >= _stoppingPoint && !_hasStopped)
		{
			_isMoving = false;
			_hasStopped = true;
			button.Disabled = false;
			_progressSpeed = 200f;
		}

		if (ProgressRatio == 1)
		{
			_isMoving = !_isMoving;
			_hasStopped = !_hasStopped;
			_progressSpeed = 200f;
		}
	}

	private void StarProgress()
	{
		_isMoving = !_isMoving;
	}
}
