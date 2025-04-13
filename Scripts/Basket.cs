using Godot;
using System;

namespace ElephantCrossing;
public partial class Basket : Node2D
{
    [Export]
    private Sprite2D _displayIcon = null;
    [Export]
    private Texture2D _displayTexture = null;
    [Export]
    private RiverItem _inputType = RiverItem.None;
    [Export]
    private Texture2D[] _basketStates = null;

    private int _points = 0;
    private int _neededPoints = 10;
    private Sprite2D _basketSprite = null;

    public override void _Ready()
    {
        _basketSprite = GetNode<Sprite2D>("BasketSprite");
        _displayIcon.Texture = _displayTexture;
        UpdatePoints();
    }

    public bool AddToBasket(Icon item)
    {
        if (item.Type != _inputType || _points >= _neededPoints)
        {
            return false;
        }

        GetNode<AudioStreamPlayer>("sfx").Playing = true;
        _points++;
        Level.Current.Score++;
        Level.Current.TrashInPlay -= 1;

        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(GetNode("Score"), "scale", new Vector2(1.5f,1.5f), 0.55f)
            .SetEase(Tween.EaseType.Out)
            .SetTrans(Tween.TransitionType.Quart);
        tween.TweenProperty(GetNode("Score"), "scale", new Vector2(1,1), 1.60f)
            .SetTrans(Tween.TransitionType.Elastic)
            .SetEase(Tween.EaseType.Out);


        UpdatePoints();
        UpdateBasketSprite();
        return true;
    }

    private void UpdatePoints()
    {
        GetNode<RichTextLabel>("Score").Text = $"[center]{_points} / {_neededPoints}";
    }

    private void UpdateBasketSprite()
    {
        if(_basketStates.Length <= 0)
        {
            return;
        }

        float spriteIndex = (_basketStates.Length - 1f) / _neededPoints * _points;
        GD.Print(spriteIndex);
        _basketSprite.Texture = _basketStates[(int) spriteIndex];
    }
}
