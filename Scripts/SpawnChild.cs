using Godot;
using System;

public partial class SpawnChild : Node2D
{
	// Called when the node enters the scene tree for the first time.

	// Called every frame. 'delta' is the elapsed time since the previous frame.


	public override void _Ready()
	{
		GetNode<Timer>("Timer").Timeout += spawn;
	}
	private void spawn()
	{
		PackedScene scene = GD.Load<PackedScene>("res://icon.tscn");
		PathFollow2D s = scene.Instantiate<PathFollow2D>();
		GetTree().Root.GetNode<Path2D>("Main/Path2D").AddChild(s);
	}
}
