using Godot;
using System;

public partial class SpawnChild : Node2D
{
	[Export] PackedScene trash1 = null;
	[Export] PackedScene trash2 = null;
	[Export] PackedScene roar = null;
	[Export] float minSpawnTime = 10.0f;
	[Export] float maxSpawnTime = 30.0f;
	private Random random = new Random();

	public override void _Ready()
	{
		SetRandomSpawnTime();
	}

	private void spawnObjects()
	{
		Path2D path = GetTree().Root.GetNode<Path2D>("Main/Path2D");

		PackedScene scene = roar;
		int randomScene = random.Next(0, 3);

		switch (randomScene)
		{
			case 0: scene = trash1; break;
			case 1: scene = trash2; break;
			case 2: scene = roar; break;
		}

		//PackedScene scene = (random.Next(2) == 0) ? trash1 : trash2;
		PathFollow2D instance = scene.Instantiate<PathFollow2D>();

		path.AddChild(instance);
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
