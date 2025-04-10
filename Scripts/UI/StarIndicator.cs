using Godot;

namespace ElephantCrossing.UI;
public partial class StarIndicator : Control
{
    [Export] private TextureRect _starSprite = null;
    [Export] private Texture2D _activeStar = null;
    [Export] private Texture2D _deactiveStar = null;
    private Tween _tween;

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
        Animate();
    }

    private void Animate()
    {
        if(_tween != null)
            _tween.Kill();

        _tween = CreateTween();
        _tween.TweenProperty(_starSprite, "scale", new Vector2(1.5f,1.5f), 0.55f)
            .SetEase(Tween.EaseType.Out)
            .SetTrans(Tween.TransitionType.Quart);
        _tween.TweenProperty(_starSprite, "scale", new Vector2(1,1), 1.10f)
            .SetTrans(Tween.TransitionType.Elastic)
            .SetEase(Tween.EaseType.Out);
    }

}
