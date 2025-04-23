using Godot;
using System;
using System.Collections.Generic;

namespace ElephantCrossing
{
	public partial class SpawnChild : Node2D
	{
		[Export] private Path2D _path = null;
		[Export] private PackedScene _crocodile = null;
		[Export] private float _minSpawnTime = 1.0f;
		[Export] private float _maxSpawnTime = 4.0f;
		[Export] private int _crocodileDelay = 5;
		[Export] private float _chanceIncrease = 0.5f;
		[Export] private Trash[] _trashArray;
		[Export] private float _riverSpeed = 100;

		private float _crocodileChance = 0;
		[Export] private float _startChance = 0.3f;

		private Random _random = new Random();
		private List<Trash> _trashList = new List<Trash>();

		/// <summary>
		/// Starts the spawning process by initializing the trash list and setting the initial spawn time.
		/// </summary>
		public void Start()
		{
			foreach (Trash trash in _trashArray)
			{
				GD.Print(trash.TrashAmount);
				if (trash.TrashAmount > 0 && trash.trashType != null)
				{
					_trashList.Add((Trash)trash.Duplicate());
				}
			}

			_crocodileChance = _startChance;
			SetRandomSpawnTime();
		}

		/// <summary>
		/// Spawns an object based on the spawn conditions.
		/// </summary>
		private void SpawnObjects()
		{
			GD.Print(_crocodileChance);

			if (SpawnCrocodile())
			{
				return;
			}

			PackedScene scene = null;
			int randomScene = _random.Next(0, _trashList.Count);
			GD.Print("Delay: " + _crocodileDelay);

			scene = _trashList[randomScene].trashType;
			_trashList[randomScene].TrashAmount--;

			if (_trashList[randomScene].TrashAmount <= 0)
			{
				_trashList.RemoveAt(randomScene);
			}

			if (_crocodileDelay > 0)
			{
				_crocodileDelay--;
			}

			GD.Print("Spawned: " + randomScene);

			if (scene != null)
			{
				Icon instance = scene.Instantiate<Icon>();
				instance.Speed = _riverSpeed;
				_path.AddChild(instance);
			}
		}

		/// <summary>
		/// Attempts to spawn a crocodile based on a random chance.
		/// </summary>
		/// <returns>True if a crocodile was spawned, otherwise false.</returns>
		private bool SpawnCrocodile()
		{
			float crocodileRandom = GD.Randf();
			GD.Print(crocodileRandom);
			if (crocodileRandom > _crocodileChance)
			{
				_crocodileChance += _chanceIncrease;
				return false;
			}

			Icon instance = _crocodile.Instantiate<Icon>();
			instance.Speed = _riverSpeed;
			_path.AddChild(instance);
			_crocodileChance = _startChance;
			return true;
		}

		/// <summary>
		/// Sets a random spawn time for the next object to be spawned.
		/// </summary>
		private void SetRandomSpawnTime()
		{
			if (_trashList.Count <= 0)
			{
				return;
			}

			float randomTime = (float)(_random.NextDouble() * (_maxSpawnTime - _minSpawnTime) + _minSpawnTime);
			Timer timer = new Timer();
			AddChild(timer);
			timer.WaitTime = randomTime;
			timer.OneShot = true;
			timer.Timeout += () =>
			{
				SpawnObjects();
				SetRandomSpawnTime();
				timer.QueueFree();
			};
			timer.Start();
		}
	}
}
