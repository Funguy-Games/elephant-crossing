using Godot;
using System;

public partial class Level : Node2D
{
	[Export]
	private RichTextLabel _scoreBroad = null;
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

    private void UpdateScoreBoard()
	{
		_scoreBroad.Text = $"[center]{_score}";
	}

    public override void _Ready()
    {
        UpdateScoreBoard();
    }


}
