using Godot;
using System;

public partial class Sound : HSlider
{
	private const float MIN_DECIBELS = -80f;

	[Export] private string _busName = null;
	private int _busIndex = 0;

	/// <summary>
	/// Initializes the bus index and sets the initial slider value.
	/// </summary>
	public override void _Ready()
	{
		_busIndex = AudioServer.GetBusIndex(_busName);
		ValueChanged += OnValueChanged;

		Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(_busIndex));
	}

	/// <summary>
	/// Called when the slider value changes.
	/// Converts the value to decibels and updates the audio bus.
	/// </summary>
	/// <param name="value">The new slider value.</param>
	private void OnValueChanged(double value)
	{
		float dbValue = Mathf.LinearToDb((float)value);

		// Avoid negative infinity when volume is zero
		if (float.IsNegativeInfinity(dbValue))
			dbValue = MIN_DECIBELS;

		AudioServer.SetBusVolumeDb(_busIndex, dbValue);
		GD.Print(AudioServer.GetBusVolumeDb(_busIndex));
	}
}
