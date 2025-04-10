using Godot;
using System.Collections.Generic;

namespace ElephantCrossing.UI;
public partial class ProgressViewer : Control
{
    [Export] private ProgressBar _hitBar;
    [Export] private ProgressBar _missBar;
    [Export] private PackedScene _starIndicatorScene = null;
    private Dictionary<int, StarIndicator> _starIndicators = new Dictionary<int, StarIndicator>();

    /// <summary>
    /// Sets the progressviewers progress bars' max values
    /// </summary>
    /// <param name="maxValue">maximum value</param>
    public void SetMax(int maxValue)
    {
        _hitBar.MaxValue = maxValue;
        _missBar.MaxValue = maxValue;

    }

    /// <summary>
    /// Resets the viewers progressbars to 0.
    /// </summary>
    public void Reset()
    {
        _hitBar.Value = 0;
        _missBar.Value = 0;
    }

    /// <summary>
    /// Sets ProgressViewers hit progress bar's value
    /// </summary>
    /// <param name="hits">given value</param>
    public void SetHits(int hits = 0)
    {
        _hitBar.Value = hits;
        if(_starIndicators.ContainsKey(hits))
        {
            _starIndicators[hits].Activate();
        }
    }

    /// <summary>
    /// Sets ProgressViewers miss progress bar's value
    /// </summary>
    /// <param name="misses">given value</param>
    public void SetMisses(int misses = 0) => _missBar.Value = misses;

    /// <summary>
    /// Spawns an indicator on the progress bar marking a star threshold.
    /// </summary>
    /// <param name="place">Amount of points for the threshold to be reached</param>
    public void SetStarIndicator(int place)
    {
        StarIndicator indicator = null;
        if (_starIndicatorScene != null)
        {
            indicator = _starIndicatorScene.Instantiate<StarIndicator>();
        }

        if (indicator == null)
        {
            return;
        }

        AddChild(indicator);

        _starIndicators.Add(place, indicator);

        // Getting the position from the amount of points the hit bar has
        float x = (Size.X / (float) _hitBar.MaxValue) * place;
        indicator.Position = new Vector2(x, 0);
    }
}
