using Godot;
using System;

public partial class ScrollContainerFix : ScrollContainer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
		if (@event is InputEventScreenDrag)
		{
			AcceptEvent();
			return;
		}
    }
}
