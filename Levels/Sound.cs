using Godot;
using System;

public partial class Sound : HSlider
{
	// Called when the node enters the scene tree for the first time.

	[Export] public string _busName = null;
	private int _busIndex = 0;
	public override void _Ready()
	{
		_busIndex = AudioServer.GetBusIndex(_busName);
		ValueChanged += OnValueChanged;

		Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(_busIndex));
	}

	private void OnValueChanged(double value)
	{
		float dbValue = Mathf.LinearToDb((float)value);

		if(float.IsNegativeInfinity(dbValue))
			dbValue = -80f;

		AudioServer.SetBusVolumeDb(_busIndex, dbValue);
		GD.Print(AudioServer.GetBusVolumeDb(_busIndex));
	}
}
