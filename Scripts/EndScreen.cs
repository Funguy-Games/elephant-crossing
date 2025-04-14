using Godot;

namespace ElephantCrossing.UI;
public partial class EndScreen : CanvasLayer
{
	[Export]
	private TextureRect[] _stars;
	[Export]
	private SceneManager _sceneManager = null;
	private RichTextLabel _endText = null;
	private int _maxStars = 3; // these could be gotten from level, for now we are assuming the value.
	private string[] _endMessages =
	{
		"END_TXT_LOST",
		"END_TXT_1",
		"END_TXT_2",
		"END_TXT_3",
	};

    public override void _Ready()
    {
        base._Ready();
		Visible = false;
		foreach(TextureRect star in _stars)
		{
			star.Material.Set("shader_parameter/fillAmount", 0);
		}
    }
	/// <summary>
	/// Opens the games end screen and starts filling the end screens stars.
	/// </summary>
	/// <param name="amount">amount of stars to fill, amounts > 1 spill to the next star</param>
    public void ShowStars(float amount)
	{
		GD.Print($"Showing {amount} Stars");
		Visible = true;
		FillStar(amount, -1);
		SetEndText(amount);
	}

	public void GoNext()
	{
		_sceneManager.LoadMainMenu();
	}

	private void SetEndText(float amount)
	{
		if(_endText == null)
		{
			_endText = GetNode<RichTextLabel>("ColorRect/RichTextLabel");
		}

		int messageIndex = 0;

		if (amount >= 1.0f)
		{
			// Skipping the first message
			messageIndex = (int) (amount / (float) _maxStars * _endMessages.Length - 2) + 1;
		}
		string text = _endMessages[messageIndex];

		if (amount >= _maxStars)
		{
			_endText.Text = $"[center][color=gold]{TranslationServer.Translate(text)}";
		} else
		{
			_endText.Text = $"[center]{TranslationServer.Translate(text)}";
		}

	}

	// Automatically goes through each star filling them one by one.
	// After fully filling a star this triggers the stars animations and effects.
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

	// Fills a single star
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
