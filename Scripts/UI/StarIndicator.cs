using Godot;

namespace ElephantCrossing.UI;
public partial class StarIndicator : Control
{
    [Export] private TextureRect _starSprite = null;
    [Export] private Texture2D _activeStar = null;
    [Export] private Texture2D _deactiveStar = null;

    public override void _Ready()
    {
        base._Ready();
        _starSprite.Texture = _deactiveStar;
    }
    /// <summary>
    /// Sets the star indicators status to active.
    /// </summary>
    public void Activate()
    {
        _starSprite.Texture = _activeStar;
    }

}
