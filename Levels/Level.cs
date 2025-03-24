using Godot;
using System;

public partial class Level : Node2D
{
	[Export]
	private RichTextLabel _scoreBroad = null;
	[Export]
	private EndScreen _endScreen = null;
	[Export] private FadeCanvas _fade = null;

	// this is used to see calculate when the game should end
	// TODO: Get this value by couting the trash amount instead of placing value by hand
	[Export]
	private int _trashInPlay = 0;
	public int TrashInPlay
	{
		get {return _trashInPlay;}
		set
		{
			_trashInPlay = value;
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
		}
	}
	public static Level Current {get; private set;}

	Level()
	{
		Current = this;
	}

	public override void _Ready()
    {
		_trashInTotal = _trashInPlay;
		_fade.FadeIn();
		_fade.FadedIn += Start;
		_endScreen.Visible = false;
        UpdateScoreBoard();

		float pointsForStar = 30 / 3;
		int stars = (int) (30 / pointsForStar);
		GD.Print("for star: " + pointsForStar);
		GD.Print("stars: " + stars);
		_endScreen.Visible = true;

		_endScreen.ShowStars(3);

    }

    private void UpdateScoreBoard()
	{
		_scoreBroad.Text = $"[center]{_score}";
	}

	private void Start()
	{
		GetNode<SpawnChild>("Spawn").Start();
	}

	private void End()
	{
		_endScreen.Visible = true;
		int trashPoints = _trashInTotal - (_trashInTotal - _score);
		float pointsForStar = _trashInTotal / 3;
		int stars = (int) (trashPoints / pointsForStar);
		GD.Print(stars);
		_endScreen.ShowStars(stars);
	}



}
