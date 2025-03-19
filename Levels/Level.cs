using Godot;
using System;

public partial class Level : Node2D
{
	[Export]
	private RichTextLabel _scoreBroad = null;
	[Export]
	private CanvasLayer _endScreen = null;

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
				_endScreen.Visible = true;
			}
		}
	}

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
		_endScreen.Visible = false;
        UpdateScoreBoard();
    }


}
