using Godot;
using System;

public partial class MenuProgress : PathFollow2D
{
	[Export] private float _progressSpeed = 1000f;
	[Export] public float StartingRatio = 0f;
	[Export] public float StoppingPoint = 0f;
	[Export] private TextureButton _button;

	private bool _hasStopped = false;
	public bool _isMoving = false;

	/// <summary>
	/// Initializes the progress state.
	/// </summary>
	public override void _Ready()
	{
		ProgressRatio = StartingRatio;
		_button.Disabled = true;
	}

	/// <summary>
	/// Updates progress movement and checks for stopping conditions.
	/// </summary>
	/// <param name="delta">Time since last frame.</param>
	public override void _Process(double delta)
	{
		if (!_isMoving)
			return;

		_button.Disabled = true;

		Progress += (float)delta * _progressSpeed;
		_progressSpeed += (float)delta * 100f;

		if (ProgressRatio >= StoppingPoint && !_hasStopped)
		{
			_isMoving = false;
			_hasStopped = true;
			_button.Disabled = false;
			_progressSpeed = 1000f;
		}

		if (ProgressRatio >= 1f)
		{
			_isMoving = false;
			_hasStopped = false;
			_progressSpeed = 1000f;
		}
	}

	/// <summary>
	/// Starts or toggles the progress movement.
	/// </summary>
	private void StartProgress()
	{
		_isMoving = !_isMoving;
	}
}