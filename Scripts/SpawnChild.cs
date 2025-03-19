using Godot;
using System;

public partial class SpawnChild : Node2D
{
	[Export] PackedScene trash1 = null;
	[Export] PackedScene trash2 = null;
	[Export] PackedScene roar = null;
	[Export] float minSpawnTime = 1.0f;
	[Export] float maxSpawnTime = 4.0f;
	[Export] private int _trash1Amount = 10;
	[Export] private int _trash2Amount = 20;
	[Export] private int _crocodileDelay = 5;

	private Random random = new Random();

	public override void _Ready()
	{
		SetRandomSpawnTime();
	}

	private void spawnObjects()
	{
		Path2D path = GetTree().Root.GetNode<Path2D>("Main/Path2D");

		PackedScene scene = null;
		int randomScene = random.Next(0, 3);
		GD.Print("Delay: " + _crocodileDelay);

		switch (randomScene)
		{
			case 0:
				if (_trash1Amount > 0)
				{
					scene = trash1;
					_trash1Amount--;
					if (_crocodileDelay > 0) _crocodileDelay--;
				}
				break;
			case 1:
				if (_trash2Amount > 0)
				{
					scene = trash2;
					_trash2Amount--;
					if (_crocodileDelay > 0) _crocodileDelay--;
				}
				break;
			case 2:
				if (_crocodileDelay == 0 && (_trash1Amount > 0 || _trash2Amount > 0))
				{
					scene = roar;
					_crocodileDelay = 5;
				}
				break;
		}

		GD.Print("Spawned: " + randomScene);

		if (scene != null)
		{
			PathFollow2D instance = scene.Instantiate<PathFollow2D>();
			path.AddChild(instance);
		}
	}

	private void SetRandomSpawnTime()
	{
		float randomTime = (float)(random.NextDouble() * (maxSpawnTime - minSpawnTime) + minSpawnTime);
		Timer timer = new Timer();
		AddChild(timer);
		timer.WaitTime = randomTime;
		timer.OneShot = true;
		timer.Timeout += () =>
		{
			spawnObjects();
			SetRandomSpawnTime();
			timer.QueueFree();
		};
		timer.Start();
	}
}
