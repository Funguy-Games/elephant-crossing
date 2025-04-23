using Godot;
using System;

namespace ElephantCrossing;
public partial class SettingsMenu : Control
{
	[Export] private SaveSystem _saveSystem = null;

	private int _sfxBusIndex = AudioServer.GetBusIndex("SFX");
	private int _musicBusIndex = AudioServer.GetBusIndex("Music");

	/// <summary>
	/// Initializes the settings menu with saved preferences.
	/// </summary>
	public override void _Ready()
	{
		Visible = false;

		var (savedLanguage, savedSfxVolume, savedMusicVolume) = _saveSystem.LoadSettings();

		TranslationServer.SetLocale(savedLanguage);
		AudioServer.SetBusVolumeDb(_sfxBusIndex, savedSfxVolume);
		AudioServer.SetBusVolumeDb(_musicBusIndex, savedMusicVolume);

		GetNode<HSlider>("VBoxContainer/Volume/VolumeSlider").Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(_sfxBusIndex));
		GetNode<HSlider>("VBoxContainer/Music/MusicSlider").Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(_musicBusIndex));

		string buttonPath = $"Flags/{savedLanguage}";
		if (HasNode(buttonPath))
		{
			GetNode<TextureButton>(buttonPath).ButtonPressed = true;
		}
	}

	/// <summary>
	/// Toggles the visibility of the settings menu.
	/// </summary>
	private void OpenSettingsMenu()
	{
		Visible = !Visible;
	}

	/// <summary>
	/// Saves current settings and closes the menu.
	/// </summary>
	private void SaveSettingsAndClose()
	{
		Visible = !Visible;

		string currentLanguage = TranslationServer.GetLocale();
		float sfxVolume = AudioServer.GetBusVolumeDb(_sfxBusIndex);
		float musicVolume = AudioServer.GetBusVolumeDb(_musicBusIndex);

		_saveSystem.SaveSettings(currentLanguage, sfxVolume, musicVolume);
	}

	/// <summary>
	/// Sets the application language if toggle is on.
	/// </summary>
	/// <param name="toggleOn">True if selected.</param>
	/// <param name="languageCode">Language code to switch to.</param>
	private void SetLanguage(bool toggleOn, string languageCode)
	{
		if (toggleOn)
		{
			TranslationServer.SetLocale(languageCode);
		}
	}
}
