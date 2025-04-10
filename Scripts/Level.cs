using Godot;
using System;
using ElephantCrossing.UI;

namespace ElephantCrossing;
public partial class Level : Node2D
{
	[Export] private ProgressViewer _progressViewer = null; // visualizes gotten points
	[Export] private Label _scoreBroad = null;
	[Export] private EndScreen _endScreen = null;
	[Export] private FadeCanvas _fade = null;

	// this is used to see calculate when the game should end
	// TODO: Get this value by couting the trash amount instead of placing value by hand
	[Export] private int _trashInPlay = 0;

	public int TrashInPlay
	{
		get {return _trashInPlay;}
		set
		{
			_trashInPlay = value;
			UpdateProgressIndicators();
			if (_trashInPlay <= 0)
			{
				End();
			}
		}
	}

	private int _trashInTotal;
	private int _score = 0;
	public int Score
	{
		get {return _score;}
		set
		{
			_score = value;
			UpdateScoreBoard();
			UpdateProgressIndicators();
		}
	}

	public static Level Current {get; private set;}

	Level()
	{
		Current = this;
	}

	public override void _Ready()
    {
		if (_progressViewer != null)
		{
			_progressViewer.SetMax(_trashInPlay);
			_progressViewer.Reset();
			float pointsForStar = _trashInPlay / 3;
			_progressViewer.SetStarIndicator((int) pointsForStar);
			_progressViewer.SetStarIndicator((int) pointsForStar * 2);
			_progressViewer.SetStarIndicator((int) pointsForStar * 3);
		}

		_trashInTotal = _trashInPlay;

		_fade.FadeIn();
		_fade.FadedIn += Start;

        UpdateScoreBoard();

    }

    private void UpdateScoreBoard()
	{
		_scoreBroad.Text = $"{_score}";
	}

	private void UpdateProgressIndicators()
	{
		_progressViewer.SetHits(_score);
		_progressViewer.SetMisses(_trashInTotal - _trashInPlay);
	}

	private void Start()
	{
		GetNode<SpawnChild>("Spawn").Start();
	}

	private void End()
	{
		int trashPoints = _trashInTotal - (_trashInTotal - _score);
		float pointsForStar = _trashInTotal / 3;
		float stars = trashPoints / pointsForStar;

		Timer timer = new Timer();
		timer.OneShot = true;
		timer.Timeout += () =>
			_endScreen.ShowStars(stars);

		AddChild(timer);
		timer.Start(1.25f);
	}



}
