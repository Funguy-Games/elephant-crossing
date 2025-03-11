using Godot;
using System;

public partial class Elephant : Node2D
{
	Vector2 _trunkEndPosition = Vector2.Zero;

	//Min and max values are reversed due to the trunk expanding left
	private float _trunkLengthMin = -100;
	private float _trunkLengthMax = -1500;

	private Sprite2D _trunkHead = null;
	private Line2D _trunk = null;
	private Icon _carryable = null;
    public override void _Ready()
    {
		_trunkHead = GetNode<Sprite2D>("TrunkHead");
		_trunk = GetNode<Line2D>("Line2D");
    }
    public override void _Process(double delta)
    {
		float rotation = (float) GetNode<VSlider>("CanvasLayer/Control/LeftSliderArea/ControlSlider").Value;
		Rotation += rotation * 0.1f * (float) delta;
		float scale = (float) GetNode<VSlider>("CanvasLayer/Control/VSlider2").Value;
		_trunkEndPosition += new Vector2(30,0) * -scale * (float) delta;
		_trunkEndPosition.X = Mathf.Clamp(_trunkEndPosition.X , -1500, -100);
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
			PutDownItem();
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

	private void PutDownItem()
	{
		if(_carryable == null)
			return;

		// TODO: Check which basket it is
		// TODO: add points to basket
		_carryable.QueueFree();
		_carryable = null;
	}

}
