using Godot;
using System;

public partial class EndScreen : CanvasLayer
{
	[Export]
	private TextureRect[] _stars;
	[Export]
	private Texture2D _filledStar;


    public override void _Ready()
    {
        base._Ready();
		Visible = false;
		foreach(TextureRect star in _stars)
		{
			star.Material.Set("shader_parameter/fillAmount", 0);
		}
    }

    public void ShowStars(float amount)
	{
		GD.Print("Showing Stars");
		Visible = true;
		FillStar(amount, -1);
	}

	private void FillStar(float amount, int currentStar)
	{
		float fillAmount = 0;
		currentStar += 1;
		if(amount > 1)
		{
			fillAmount = 1;
			amount -= 1;
		} else if (amount > 0)
		{
			fillAmount = amount;
			amount = 0;
		} else {
			return;
		}
		Tween tween = GetTree().CreateTween().BindNode(_stars[currentStar]);
		tween.TweenMethod(
			Callable.From<float>((value) => SetFill(value, currentStar)),
			0f,
			fillAmount,
			1f
		);
		tween.TweenCallback(Callable.From(() => FillStar(amount, currentStar)));
		if(fillAmount >= 1)
		{
			tween.TweenCallback(Callable.From(() => ShakeStar(currentStar)));
		}

	}

	private void SetFill(float amount, int star)
	{
		if(_stars[star] == null)
			return;
		_stars[star].Material.Set("shader_parameter/fillAmount", amount);
	}

	private void ShakeStar(int star)
	{
		if(_stars[star] == null)
			return;
		_stars[star].PivotOffset = new Vector2(150, 250);

		Tween tween = GetTree().CreateTween().BindNode(_stars[star]);
		tween.TweenProperty(_stars[star], "rotation_degrees", 25, 0.15f)
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(_stars[star], "rotation_degrees", -25, 0.15f)
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(_stars[star], "rotation_degrees", 25, 0.15f)
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(_stars[star], "rotation", 0, 0.5f)
			.SetTrans(Tween.TransitionType.Back)
			.SetEase(Tween.EaseType.Out);
		tween.TweenCallback(Callable.From(() => ExplodeStar(star)));
	}

	private void ExplodeStar(int star)
	{
		_stars[star].GetNode<GpuParticles2D>("GPUParticles2D").Restart();
	}
}
