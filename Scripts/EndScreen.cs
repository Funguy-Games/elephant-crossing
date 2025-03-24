using Godot;
using System;

public partial class EndScreen : CanvasLayer
{
	[Export]
	private TextureRect[] _stars;
	[Export]
	private Texture2D _filledStar;

	public void ShowStars(int amount)
	{
		GD.Print("Showing Stars");
		// _stars[0].Material.Set("shader_parameter/f", 1f);
		// for(int i = 0; i < amount; i++)
		// {
		// GD.Print(i);
		// _stars[i].Texture = _filledStar;
		Tween tween = GetTree().CreateTween();
		tween.TweenMethod(
			Callable.From<float>((value) => SetFill(value, i)),
			0f,
			1f,
			1f
		);
		// }
	}

	private void SetFill(float amount, int star)
	{
		// _stars[star - 1].Material.Set("shader_parameter/f", amount);
	}
}
