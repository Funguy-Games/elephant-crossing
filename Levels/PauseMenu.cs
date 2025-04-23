using Godot;
using System;

public partial class PauseMenu : Control
{
	[Export] private MenuProgress _button1;
	[Export] private MenuProgress _button2;
	[Export] private MenuProgress _button3;
	[Export] private River _river;

	private bool _isPaused = false;
	private Timer _timer;

	/// <summary>
	/// Hides the pause menu when the node enters the scene tree.
	/// </summary>
	public override void _Ready()
	{
		Visible = false;
	}

	/// <summary>
	/// Called every frame to handle game logic.
	/// Moves the river background down at a specific ProgressRatio.
	/// </summary>
	/// <param name="delta">Time elapsed since last frame.</param>
	public override void _Process(double delta)
	{
		if (_button1.ProgressRatio >= 0.8f)
		{
			_river.MoveDown();
		}
	}

	/// <summary>
	/// Toggles the pause menu and pauses the game.
	/// </summary>
	private void OpenPauseMenu()
	{
		_isPaused = !_isPaused;
		Visible = _isPaused;
		GetTree().Paused = true;

		_button1._isMoving = true;
		_button2._isMoving = true;
		_button3._isMoving = true;

		_river.MoveToCenter();
	}

	/// <summary>
	/// Starts a delay before resuming the game.
	/// </summary>
	private void Resume()
	{
		_timer = new Timer();
		AddChild(_timer);
		_timer.WaitTime = 1.1f;
		_timer.OneShot = true;
		_timer.Timeout += OnResumeTimerTimeout;
		_timer.Start();
	}

	/// <summary>
	/// Called when resume delay is over. Unpauses game and resets button progress.
	/// </summary>
	private void OnResumeTimerTimeout()
	{
		_isPaused = !_isPaused;
		Visible = _isPaused;
		GetTree().Paused = false;

		_button1.ProgressRatio = _button1.StartingRatio;
		_button2.ProgressRatio = _button2.StartingRatio;
		_button3.ProgressRatio = _button3.StartingRatio;

		_river.MoveToStartPosition();

		_timer.QueueFree();
	}

	/// <summary>
	/// Exits to main menu after a delay.
	/// </summary>
	private void Exit()
	{
		_timer = new Timer();
		AddChild(_timer);
		_timer.WaitTime = 1.1f;
		_timer.OneShot = true;
		_timer.Timeout += () =>
		{
			GetTree().Paused = false;
			GetTree().ChangeSceneToFile("res://Levels/menu.tscn");
			_timer.QueueFree();
		};
		_timer.Start();
	}
}
