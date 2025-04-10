using Godot;
using System;

public partial class soundEffect : TextureButton
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += OnPressed;
	}

	private void OnPressed()
	{
		var clickSound = GetNode<AudioStreamPlayer>("Cereal");
		clickSound.Play();
	}
}
