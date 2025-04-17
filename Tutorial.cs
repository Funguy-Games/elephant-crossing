using Godot;
using System;

public partial class Tutorial : CanvasLayer
{
	[Export] private CanvasLayer _buttons = null;
	[Export] private CanvasLayer _tutorial1 = null;
	[Export] private CanvasLayer _tutorial2 = null;
	[Export] private CanvasLayer _tutorial3 = null;
	[Export] private CanvasLayer _tutorial4 = null;
	[Export] private CanvasLayer _tutorial5 = null;
	[Export] private CanvasLayer _tutorial6 = null;
	[Export] private CanvasLayer _tutorial7 = null;

	private int _currentIndex = 0;
	private CanvasLayer[] _tutorials;

	/// <summary>
	/// Initializes the tutorial system and pauses the game.
	/// </summary>
	public override void _Ready()
	{
		Visible = true;
		_buttons.Visible = true;

		Engine.TimeScale = 0;

		_tutorials = new CanvasLayer[]
		{
			_tutorial1,
			_tutorial2,
			_tutorial3,
			_tutorial4,
			_tutorial5,
			_tutorial6,
			_tutorial7
		};

		ShowCurrentTutorial();
	}

	/// <summary>
	/// Displays the currently selected tutorial screen.
	/// </summary>
	private void ShowCurrentTutorial()
	{
		for (int i = 0; i < _tutorials.Length; i++)
		{
			_tutorials[i].Visible = (i == _currentIndex);
		}
	}

	/// <summary>
	/// Moves forward to the next tutorial screen.
	/// </summary>
	private void GoForward()
	{
		if (_currentIndex < _tutorials.Length - 1)
		{
			_currentIndex++;
			ShowCurrentTutorial();
		}
	}

	/// <summary>
	/// Moves back to the previous tutorial screen.
	/// </summary>
	private void GoBack()
	{
		if (_currentIndex > 0)
		{
			_currentIndex--;
			ShowCurrentTutorial();
		}
	}

	/// <summary>
	/// Exits the tutorial and starts the game.
	/// </summary>
	private void ExitTutorial()
	{
		Engine.TimeScale = 1;
		Visible = false;
		_tutorials[_currentIndex].Visible = false;
		_buttons.Visible = false;
	}
}