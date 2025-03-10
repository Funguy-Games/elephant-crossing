using Godot;
using System;

public partial class CustomSlider : TouchScreenButton
{
	private float _slideArea = 0;
	private float _sliderPosition = 0;
	private float _sliderHeight = 0;
	public float SliderPosition
	{
		get { return _sliderPosition; }
		set {
			_sliderPosition = value;
			SetSliderPosition(value);
		}
	}
	public override void _Ready()
	{
		_slideArea = GetParent<ColorRect>().Size.Y;
		_sliderHeight = 50; // hard coded temporarily
		SetSliderPosition(0);
		GD.Print(_slideArea, Position);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}


    public override void _Input(InputEvent @event)
    {
		if(IsPressed())
		switch (@event)
		{
			case InputEventScreenTouch screenTouch:
					GD.Print(screenTouch.Index);
				break;
			case InputEventScreenDrag screenDrag:
					GD.Print(screenDrag.Index);
					Vector2 newPosition = screenDrag.Position;
					newPosition.Y = Mathf.Clamp(newPosition.Y, 0, _slideArea - _sliderHeight);
					newPosition.X = 0;
					Position = newPosition;
				break;
		}
    }
	private void SetSliderPosition(float value)
	{
		float offset = _slideArea / 2 * value;
		Position = new Vector2(0, _slideArea / 2 - offset);
	}
}
