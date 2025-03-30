using Godot;
using System;

public partial class PauseMenu : Control
{
	[Export] private MenuProgress button1;
	[Export] private MenuProgress button2;
	[Export] private MenuProgress button3;
	private bool isPaused = false;
	private Timer _timer;

	public override void _Ready()
	{
		Visible = false;
	}

	public override void _Process(double delta)
	{
	}

	private void OpenPauseMenu()
	{
		isPaused = !isPaused;
		Visible = isPaused;
	}

	private void Resume()
	{
		_timer = new Timer();
		AddChild(_timer);
		_timer.WaitTime = 1f;
		_timer.OneShot = true;
		_timer.Timeout += TimerTimeout;
		_timer.Start();
	}

	private void TimerTimeout()
	{
		isPaused = !isPaused;
		Visible = isPaused;
		button1.ProgressRatio = button1._startingRatio;
		button2.ProgressRatio = button2._startingRatio;
		button3.ProgressRatio = button3._startingRatio;

		_timer.QueueFree();
	}
}
