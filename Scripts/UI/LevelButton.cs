using ElephantCrossing;
using Godot;
using System;
using System.Linq;

public partial class LevelButton : SceneButton
{
    [Export] private Texture2D _filledStar = null;
    [Export] private TextureRect[] _stars = null;

    public void FillStars(int amount)
    {
        if(amount > _stars.Count())
            amount = _stars.Count();

        for(int i = 0; i < amount; i++)
        {
            _stars[i].Texture = _filledStar;
        }
    }
}
