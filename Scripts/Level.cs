using Godot;
using System;
using System.IO;
using ElephantCrossing.UI;
using Godot.Collections;

namespace ElephantCrossing;
public partial class Level : Node2D
{
	[Export] private ProgressViewer _progressViewer = null; // visualizes gotten points
	[Export] private Label _scoreBroad = null;
	[Export] private EndScreen _endScreen = null;
	[Export] private FadeCanvas _fade = null;
	[Export] private SaveSystem _saveSystem = null;
	[Export] private LevelID _levelID = LevelID.Tutorial;

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
	private int _oldScore = 0;
	private int _score = 0;
	public int Score
	{
		get {return _score;}
		set
		{
			_score = value;
			// UpdateScoreBoard();
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

			_progressViewer.SetStarIndicator((int)pointsForStar);
			_progressViewer.SetStarIndicator((int)pointsForStar * 2);
			_progressViewer.SetStarIndicator((int)pointsForStar * 3);
		}

		_trashInTotal = _trashInPlay;

		_fade.FadeIn();
		_fade.FadedIn += Start;

		// UpdateScoreBoard();
		_oldScore = _saveSystem.LoadLevelData(_levelID.ToString());
		GD.Print(_oldScore);
	}

	#region FileHandling
	#endregion

	#region UI
	// private void UpdateScoreBoard()
	// {
	// 	_scoreBroad.Text = $"{_score}";
	// }

	private void UpdateProgressIndicators()
	{
		_progressViewer.SetHits(_score);
		_progressViewer.SetMisses(_trashInTotal - _trashInPlay);
	}
	#endregion

	#region GameStates
	private void Start()
	{
		if (HasNode("CrocodileSpawn"))
		{
			GetNode<SpawnChild>("CrocodileSpawn").Start();
		}
		GetNode<SpawnChild>("TrashSpawn").Start();
	}

	private void End()
	{
		float stars = CalculateFinalScore();
		GD.Print(stars);
		if (stars > _oldScore)
			_saveSystem.Save((int) stars, _levelID.ToString());

		Timer timer = new Timer();
		timer.OneShot = true;
		timer.Timeout += () =>
			_endScreen.ShowStars(stars);

		AddChild(timer);
		timer.Start(1.25f);
	}
	#endregion

	private float CalculateFinalScore()
	{
		int trashPoints = _trashInTotal - (_trashInTotal - _score);
		float pointsForStar = _trashInTotal / 3.0f;
		GD.Print("Points for a star: " + pointsForStar);
		GD.Print("trashPoints: " + trashPoints);
		return trashPoints / pointsForStar;
	}
}