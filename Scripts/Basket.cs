using Godot;
using System;

public partial class Basket : Node2D
{
    [Export]
    private Sprite2D _displayIcon = null;
    [Export]
    private Texture2D _displayTexture = null;
    [Export]
    private RiverItem _inputType = RiverItem.None;

    private int _points = 0;

    public override void _Ready()
    {
        _displayIcon.Texture = _displayTexture;
    }


    public bool AddToBasket(Icon item)
    {
        if (item.Type == _inputType)
        {
            _points++;
            return true;
        }

        return false;
    }
}
