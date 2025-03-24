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
		for(int i = 0; i < amount; i++)
		{
			_stars[i].Texture = _filledStar;
		}
	}
}
