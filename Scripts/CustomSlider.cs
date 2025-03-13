using Godot;

public partial class CustomSlider : TextureRect
{
	private float _slideArea = 0;
	private float _sliderPosition = 0;
	private float _sliderHeight = 0;
	private float _handleHeight = 0;
	private float _handleWidth = 0;
	private Vector2 dragOffset = Vector2.Zero;
	private int touchIndex = -1;
	public float SliderPosition
	{
		get { return _sliderPosition; }
		set
		{
			_sliderPosition = value;
			SetSliderPosition(value);
		}
	}
	public override void _Ready()
	{
		_handleWidth = Position.X;
		_sliderHeight = Size.Y;
		_slideArea = GetParent<ColorRect>().Size.Y - (_sliderHeight / 2);
		SetSliderPosition(0);
	}

	public override void _Process(double delta)
	{
		// Calculating the position the slider is in
		// by taking half the slide area and removing the sliders current position
		// to get the slide handles relative position on the slide area
		// then dividing that by the slide area to get a range from -1 to 1
		_sliderPosition = (_slideArea / 2 - Position.Y) / (_slideArea / 2);
		GD.Print(_sliderPosition);
	}


	public override void _Input(InputEvent @event)
	{
		switch (@event)
		{
			case InputEventScreenTouch screenTouch:
				if (GetGlobalRect().HasPoint(screenTouch.Position) && screenTouch.Pressed)
				{
					dragOffset = GlobalPosition - screenTouch.Position;
					touchIndex = screenTouch.Index;
				}
				else if (touchIndex == screenTouch.Index)
				{
					GD.Print("Touch: ", screenTouch.Pressed, GetGlobalRect().HasPoint(screenTouch.Position));
					touchIndex = -1;
					SliderPosition = 0;
				}
				break;
			case InputEventScreenDrag screenDrag:
				if (screenDrag.Index != touchIndex)
					return;

				GetViewport().SetInputAsHandled();

				Vector2 newPosition = screenDrag.Position + dragOffset;
				newPosition.Y = Mathf.Clamp(newPosition.Y, 0, _slideArea);
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
