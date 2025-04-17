using Godot;
using System;

public partial class Tutorial : CanvasLayer
{
	[Export] CanvasLayer Buttons = null;
	[Export] CanvasLayer Tutorial1 = null;
	[Export] CanvasLayer Tutorial2 = null;
	[Export] CanvasLayer Tutorial3 = null;
	[Export] CanvasLayer Tutorial4 = null;
	[Export] CanvasLayer Tutorial5 = null;
	[Export] CanvasLayer Tutorial6 = null;
	[Export] CanvasLayer Tutorial7 = null;

	private int currentIndex = 0;
	private CanvasLayer[] tutorials;

	public override void _Ready()
	{
		Visible = true;
		Buttons.Visible = true;

		Engine.TimeScale = 0;

		tutorials = new CanvasLayer[] { Tutorial1, Tutorial2, Tutorial3, Tutorial4, Tutorial5, Tutorial6, Tutorial7 };

		ShowCurrentTutorial();
	}

	private void ShowCurrentTutorial()
	{
		for (int i = 0; i < tutorials.Length; i++)
		{
			tutorials[i].Visible = (i == currentIndex);
		}
	}

	private void GoForward()
	{
		if (currentIndex < tutorials.Length - 1)
		{
			currentIndex++;
			ShowCurrentTutorial();
		}
	}

	private void GoBack()
	{
		if (currentIndex > 0)
		{
			currentIndex--;
			ShowCurrentTutorial();
		}
	}

	private void ExitTutorial()
	{
		Engine.TimeScale = 1;
		Visible = false;
		tutorials[currentIndex].Visible = false;
		Buttons.Visible = false;
	}
}