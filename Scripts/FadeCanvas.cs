using Godot;
using System;

namespace ElephantCrossing;
public partial class FadeCanvas : CanvasLayer
{
	[Signal]
	public delegate void FadedInEventHandler();

	[Signal]
	public delegate void FadedOutEventHandler();

	[Export] private float _fadeSpeed = 1;
	[Export] private bool _autoFadeIn = false;

	private ColorRect _fadeRect = null;
	private bool _fadeIn = false;
	private bool _fadeOut = false;
	private float _alpha = 0;
	private Color _modulate = new Color();

	public override void _Ready()
	{
		_fadeRect = GetNode<ColorRect>("ColorRect");
		_modulate = _fadeRect.Modulate;

		if(_autoFadeIn)
		{
			FadeIn();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_fadeIn)
		{
			_alpha -= (float) delta * _fadeSpeed;
			_modulate.A = _alpha;
			_fadeRect.Modulate = _modulate;
			if (_alpha <= 0) {
				Visible = false;
				_fadeIn = false;
				EmitSignal(SignalName.FadedIn);
			}
		}
		else if (_fadeOut)
		{
			_alpha += (float) delta * _fadeSpeed;
			_modulate.A = _alpha;
			_fadeRect.Modulate = _modulate;
			if (_alpha >= 1) {
				_fadeOut = false;
				EmitSignal(SignalName.FadedOut);
			}
		}
	}
	public void FadeIn()
	{
		_alpha = 1;
		_modulate.A = _alpha;
		_fadeRect.Modulate = _modulate;
		_fadeIn = true;
		Visible = true;
	}

	public void FadeOut()
	{
		_alpha = 0;
		_modulate.A = _alpha;
		_fadeRect.Modulate = _modulate;
		_fadeOut = true;
		Visible = true;

	}

}
