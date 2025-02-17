using Godot;
using System;

public partial class ControlSlider : VSlider
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        this.DragEnded += Reset;
	}

	private void Reset(bool valueChanged)
	{
		Value = 0;
	}
}
