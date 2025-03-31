using Godot;
using System;
using System.Collections.Generic;

public partial class SpawnChild : Node2D
{
	[Export] PackedScene trash1 = null;
	[Export] PackedScene trash2 = null;
	[Export] PackedScene roar = null;
	[Export] float minSpawnTime = 1.0f;
	[Export] float maxSpawnTime = 4.0f;
	[Export] private int _crocodileDelay = 5;
	[Export] private Trash[] trashArray;

	private Random random = new Random();

	private List<Trash> trashList = new List<Trash>();
	public void Start()
	{
		foreach (Trash trash in trashArray)
		{
			if (trash.TrashAmount > 0 && trash.trashType != null)
				trashList.Add(trash);
		}
		SetRandomSpawnTime();
	}

	private void spawnObjects()
	{
		Path2D path = GetTree().Root.GetNode<Path2D>("Main/Path2D");

		PackedScene scene = null;
		int randomScene = random.Next(0, trashList.Count);
		GD.Print("Delay: " + _crocodileDelay);

		scene = trashList[randomScene].trashType;
		trashList[randomScene].TrashAmount--;

		if (trashList[randomScene].TrashAmount <= 0)
		{
			trashList.RemoveAt(randomScene);
		}

		if (_crocodileDelay > 0) _crocodileDelay--;

		GD.Print("Spawned: " + randomScene);

		if (scene != null)
		{
			PathFollow2D instance = scene.Instantiate<PathFollow2D>();
			path.AddChild(instance);
		}
	}

	private void SetRandomSpawnTime()
	{
		if (trashList.Count <= 0)
		{
			return;
		}

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
