using Godot;

public partial class CustomSlider : TextureRect
{
	public enum StickSide
	{
		Left,
		Right
	}

	[Export] public StickSide ControlledBy = StickSide.Left;

	private float _slideArea = 0;
	private float _sliderPosition = 0;
	private float _sliderHeight = 0;
	private float _handleHeight = 0;
	private float _handleWidth = 0;
	private Vector2 dragOffset = Vector2.Zero;
	private int touchIndex = -1;
	private float controllerSpeed = 1000f;
	private float deadZone = 0.2f;

	private bool isTouchActive = false;
	private bool isUsingStick = false;

	public float SliderPosition
	{
		get => _sliderPosition;
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

		float axisValue = 0f;

		if (ControlledBy == StickSide.Left)
			axisValue = Input.GetJoyAxis(0, JoyAxis.LeftY);
		else if (ControlledBy == StickSide.Right)
			axisValue = Input.GetJoyAxis(0, JoyAxis.RightY);

		if (Mathf.Abs(axisValue) < deadZone)
		{
			axisValue = 0;
		}

		if (axisValue != 0 && !isTouchActive)
		{
			isUsingStick = true;
			isTouchActive = false;
			float movement = controllerSpeed * (float)delta * axisValue;
			Vector2 newPosition = Position;
			newPosition.Y = Mathf.Clamp(Position.Y + movement, 0, _slideArea);
			newPosition.X = _handleWidth;
			Position = newPosition;
		}
		else if (isUsingStick && axisValue == 0)
		{
			isUsingStick = false;
			SliderPosition = 0;
		}
	}

	public override void _Input(InputEvent @event)
	{
		switch (@event)
		{
			case InputEventScreenTouch screenTouch:
				if (screenTouch.Pressed && GetGlobalRect().HasPoint(screenTouch.Position))
				{
					dragOffset = Position - screenTouch.Position;
					touchIndex = screenTouch.Index;
					isTouchActive = true;
					isUsingStick = false;
				}
				else if (touchIndex == screenTouch.Index && !screenTouch.Pressed)
				{
					touchIndex = -1;
					isTouchActive = false;
					SliderPosition = 0;
				}
				break;

			case InputEventScreenDrag screenDrag:
				if (touchIndex < 0 && GetGlobalRect().HasPoint(screenDrag.Position))
				{
					touchIndex = screenDrag.Index;
					dragOffset = Position - screenDrag.Position;
					isTouchActive = true;
					isUsingStick = false;
				}

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
