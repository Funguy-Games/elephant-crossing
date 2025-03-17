using Godot;
using System;

public partial class SpawnChild : Node2D
{
	[Export] PackedScene trash1 = null;
	[Export] PackedScene trash2 = null;
	[Export] PackedScene roar = null;
	[Export] float minSpawnTime = 10.0f;
	[Export] float maxSpawnTime = 30.0f;
	[Export] private int _trash1Amount = 10;
	[Export] private int _trash2Amount = 20;

	private Random random = new Random();

	public int trash1Amount
	{
		get { return _trash1Amount; }
		set { _trash1Amount = value; }
	}

	public int trash2Amount
	{
		get { return _trash2Amount; }
		set { _trash2Amount = value; }
	}

	public int crocodileDelay = 5;

	public override void _Ready()
	{
		SetRandomSpawnTime();
	}

	private void spawnObjects()
	{
		Path2D path = GetTree().Root.GetNode<Path2D>("Main/Path2D");

		PackedScene scene = null;
		int randomScene = random.Next(0, 3);
		//GD.Print("Delay: " +  crocodileDelay);

		switch (randomScene)
		{
			case 0:
				if (trash1Amount > 0)
				{
					scene = trash1;
					trash1Amount--;
					if (crocodileDelay > 0) crocodileDelay--;
				}
				break;
			case 1:
				if (trash2Amount > 0)
				{
					scene = trash2;
					trash2Amount--;
					if (crocodileDelay > 0) crocodileDelay--;
				}
				break;
			case 2:
				if (crocodileDelay == 0 && trash1Amount > 0 && trash2Amount > 0)
				{
					scene = roar;
					crocodileDelay = 5;
				}
				break;
		}

		//GD.Print("Spawned: " + randomScene);

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
