using Godot;
using System;
using System.Drawing;

public partial class CustomSlider : TextureRect
{
	private float _slideArea = 0;
	private float _sliderPosition = 0;
	private float _sliderHeight = 0;
	private float _handleHeight = 0;
	private float _handleWidth = 0;
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
		_handleWidth = Position.X;
		_slideArea = GetParent<ColorRect>().Size.Y;
		_sliderHeight = 50; // hard coded temporarily
		SetSliderPosition(0);
		// Released += Reset;
	}

	private void Reset()
	{
		isDragged = false;
		touchIndex = -1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	Vector2 dragOffset = Vector2.Zero;
	bool isDragged = false;
	int touchIndex = -1;
    public override void _Input(InputEvent @event)
    {
		// if(IsPressed())
		switch (@event)
		{
			case InputEventScreenTouch screenTouch:
					dragOffset = GlobalPosition - screenTouch.Position;
					GD.Print("Touch: ", dragOffset);
					touchIndex = -1;
					isDragged = false;

				break;
			case InputEventScreenDrag screenDrag:

					if(GetGlobalRect().HasPoint(screenDrag.Position)){
						touchIndex = screenDrag.Index;
					}
					if (!isDragged)
					{
						isDragged = true;
					}
					GD.Print(touchIndex);

					if(screenDrag.Index != touchIndex)
						return;

					GetViewport().SetInputAsHandled();

					Vector2 newPosition = screenDrag.Position + dragOffset;
					newPosition.Y = Mathf.Clamp(newPosition.Y, 0, _slideArea - _sliderHeight);
					newPosition.X = _handleWidth;
					Position = newPosition;
				break;
		}
    }
	private void SetSliderPosition(float value)
	{
		float offset = _slideArea / 2 * value;
		Position = new Vector2(_handleWidth, _slideArea / 2 - offset);
	}
}
