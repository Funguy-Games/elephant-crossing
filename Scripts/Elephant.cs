using Godot;

namespace ElephantCrossing;
public partial class Elephant : Node2D
{
	[Export]
	private ShaderMaterial _elephantMaterial = null;

	[Export] private float _stunTime = 2f;
	private float _mercyTime = 3f;
	private bool _inStun = false;
	private bool _invincible = false;

	private Vector2 _trunkEndPosition = Vector2.Zero;
	private Sprite2D _trunkHead = null;
	private Line2D _trunk = null;
	private Icon _carryable = null;

	#region Animation
	[Export]
	private Sprite2D _elephantSprite = null;
	[Export]
	private Sprite2D _headSprite = null;

	// Total amount of frames
	private int _frameCount = 12;
	// Starting frame offset.
	private	int frameOffset = 3;
	#endregion

	#region Controls
	[Export]
	private float _turnSpeed = 1;
	[Export]
	private float _trunkSpeed = 4000;

	//Min and max values are reversed due to the trunk expanding left
	private float _trunkLengthMin = -200;
	private float _trunkLengthMax = -1500;
	private CustomSlider _rotationSlider = null;
	private CustomSlider _trunkSlider = null;
	#endregion

	public override void _Ready()
	{
		_trunkHead = GetNode<Sprite2D>("TrunkHead");
		_trunk = GetNode<Line2D>("Line2D");
		_rotationSlider = GetNode<CustomSlider>("CanvasLayer/Control/ColorRect/ColorRect2/ColorRect/TouchScreenButton");
		_trunkSlider = GetNode<CustomSlider>("CanvasLayer/Control/ColorRect2/ColorRect2/ColorRect/TouchScreenButton");

		// Automatically getting frame count
		_frameCount = _elephantSprite.Hframes * _elephantSprite.Vframes;
		_elephantMaterial.SetShaderParameter("isBlinking", false);
	}

	public override void _Process(double delta)
	{
		if (!_inStun)
		{
			float rotation = _rotationSlider.SliderPosition;
			float scale = _trunkSlider.SliderPosition;
			Rotation += rotation * _turnSpeed * (float)delta;

			_trunkEndPosition += Vector2.Left * _trunkSpeed * scale * (float)delta;
			_trunkEndPosition.X = Mathf.Clamp(_trunkEndPosition.X, _trunkLengthMax, _trunkLengthMin); // max and min reversed

			_trunk.SetPointPosition(1, _trunkEndPosition);
			_trunkHead.Position = new Vector2(_trunkEndPosition.X + _trunk.Position.X, 0);

			RotateElephantBody();

			if (_carryable != null)
			{
				_carryable.GlobalPosition = _trunkHead.GlobalPosition;
				_carryable.GlobalRotationDegrees = _trunkHead.GlobalRotationDegrees + 90;
			}
		}
	}

	public void On2DBodyEntered(Node2D body)
	{
		if (body.IsInGroup("Pickupable"))
		{
			PickupItem(body);
		}
		else if (body.IsInGroup("Basket"))
		{
			Basket basket = (Basket)body.Owner;
			PutDownItem(basket);
		}
		else if (body.IsInGroup("Hazard"))
		{
			if (!_invincible)
			{
				HitCrocodile();
			}

		}

	}

	private void RotateElephantBody()
	{


		// Starting rotational offset.
		float rotationOffset = (360 / _frameCount) * 0;
		// Float [0,framecount] gives the correct rotation the sprite should be in
		float spriteRotation = ((RotationDegrees + rotationOffset) % 360) / (360 / _frameCount);
		float shiftedRotation = (spriteRotation < 0) ? spriteRotation - 0.5f : spriteRotation + 0.5f;

		// GD.Print($"Sprite rotation: {spriteRotation}, rotation: {tempRot}");
		// GD.Print($"Current Frame: {currentFrame}");
		// _elephantSprite.RotationDegrees = -((360 / _frameCount) * (int) (shiftedRotation)) + rotationOffset;
		_elephantSprite.RotationDegrees = -RotationDegrees;
		_headSprite.RotationDegrees = -((360 / _frameCount) * (int) (shiftedRotation)) + rotationOffset;

		ChangeSprite((int) -shiftedRotation);

	}

	private void RotateBody()
	{

		// We rotate the sprite backwards to counter the spritesheets rotation.

	}

	private void ChangeSprite(int frame)
	{

		// Correct frame on the elephants rotation sheet
		int currentFrame = _frameCount - (int) frame;

		_elephantSprite.Frame = (currentFrame + frameOffset) % _frameCount;
		_headSprite.Frame = (currentFrame + frameOffset) % _frameCount;
	}

	private void HitCrocodile()
	{
		GD.Print("Hit");

		_inStun = true;
		_elephantMaterial.SetShaderParameter("isBlinking", true);
		Timer stuntimer = new Timer();
		AddChild(stuntimer);
		stuntimer.WaitTime = _stunTime;
		stuntimer.OneShot = true;
		stuntimer.Timeout += () =>
		{
			_inStun = false;
			stuntimer.QueueFree();
			MercyTimer();
		};
		stuntimer.Start();
	}

	private void MercyTimer()
	{
		_invincible = true;
		Timer mercytimer = new Timer();
		AddChild(mercytimer);
		mercytimer.WaitTime = _mercyTime;
		mercytimer.OneShot = true;
		mercytimer.Timeout += () =>
		{
			_invincible = false;
			mercytimer.QueueFree();
			_elephantMaterial.SetShaderParameter("isBlinking", false);

		};
		mercytimer.Start();
	}

	private void PickupItem(Node2D item)
	{
		if (_carryable == null)
		{
			GetNode<AudioStreamPlayer2D>("PickupAudio").Play();
			_carryable = item.GetParent<Icon>();
			_carryable.isMoving = false;
		}
	}

	private void PutDownItem(Basket basket)
	{
		if (_carryable == null)
			return;

		if (basket.AddToBasket(_carryable))
		{
			_carryable.QueueFree();
			_carryable = null;
		}
	}

}
