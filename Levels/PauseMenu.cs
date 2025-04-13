using Godot;
using System;

public partial class PauseMenu : Control
{
	[Export] private MenuProgress button1;
	[Export] private MenuProgress button2;
	[Export] private MenuProgress button3;
	[Export] private Joki joki;
	private bool isPaused = false;
	private Timer _timer;

	public override void _Ready()
	{
		Visible = false;
	}

	public override void _Process(double delta)
	{

		if (button1.ProgressRatio >= 0.8f)
		{
			joki.MoveDown();
		}
	}
	private void OpenPauseMenu()
	{
		isPaused = !isPaused;
		Visible = isPaused;
		GetTree().Paused = true;
		button1._isMoving = true;
		button2._isMoving = true;
		button3._isMoving = true;
		joki.MoveToCenter();
	}

	private void Resume()
	{
		_timer = new Timer();
		AddChild(_timer);
		_timer.WaitTime = 1.1f;
		_timer.OneShot = true;
		_timer.Timeout += TimerTimeout;
		_timer.Start();
	}

	private void TimerTimeout()
	{
		isPaused = !isPaused;
		Visible = isPaused;
		GetTree().Paused = false;
		button1.ProgressRatio = button1._startingRatio;
		button2.ProgressRatio = button2._startingRatio;
		button3.ProgressRatio = button3._startingRatio;

		joki.MoveToStartPosition();

		_timer.QueueFree();
	}

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
