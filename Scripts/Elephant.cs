using Godot;

public partial class Elephant : Node2D
{
	[Export]
	private float _turnSpeed = 1;
	[Export]
	private float _trunkSpeed = 4000;

	//Min and max values are reversed due to the trunk expanding left
	private float _trunkLengthMin = -100;
	private float _trunkLengthMax = -1500;

	private Vector2 _trunkEndPosition = Vector2.Zero;

	private Sprite2D _trunkHead = null;
	private Line2D _trunk = null;
	private Icon _carryable = null;

	private CustomSlider _rotationSlider = null;
	private CustomSlider _trunkSlider = null;

    public override void _Ready()
    {
		_trunkHead = GetNode<Sprite2D>("TrunkHead");
		_trunk = GetNode<Line2D>("Line2D");
		_rotationSlider = GetNode<CustomSlider>("CanvasLayer/Control/ColorRect/ColorRect2/ColorRect/TouchScreenButton");
		_trunkSlider = GetNode<CustomSlider>("CanvasLayer/Control/ColorRect2/ColorRect2/ColorRect/TouchScreenButton");
    }
    public override void _Process(double delta)
    {
		float rotation = _rotationSlider.SliderPosition;
		float scale = _trunkSlider.SliderPosition;
		Rotation += rotation * _turnSpeed * (float) delta;

		_trunkEndPosition += Vector2.Left * _trunkSpeed * scale * (float) delta;
		_trunkEndPosition.X = Mathf.Clamp(_trunkEndPosition.X , _trunkLengthMax, _trunkLengthMin); // max and min reversed

		_trunk.SetPointPosition(1, _trunkEndPosition);
		_trunkHead.Position = new Vector2(_trunkEndPosition.X + -500, 0);

		if (_carryable != null)
			_carryable.GlobalPosition = _trunkHead.GlobalPosition;
    }

	public void On2DBodyEntered(Node2D body)
	{
		if (body.IsInGroup("Pickupable"))
		{
			PickupItem(body);
		}
		else if (body.IsInGroup("Basket"))
		{
			Basket basket = (Basket) body.Owner;
			PutDownItem(basket);
		}

	}

	private void PickupItem(Node2D item)
	{
		if (_carryable == null)
		{
			_carryable = item.GetParent<Icon>();
			_carryable.isMoving = false;
		}
	}

	private void PutDownItem(Basket basket)
	{
		if(_carryable == null)
			return;

		if (basket.AddToBasket(_carryable))
		{
			_carryable.QueueFree();
			_carryable = null;
		}
	}

}
