using ElephantCrossing;
using Godot;
using System;

public partial class SettingsMenu : Control
{
	[Export] SaveSystem _saveSystem = null;

	private int _sfxBusIndex = AudioServer.GetBusIndex("SFX");
	private int _musicBusIndex = AudioServer.GetBusIndex("Music");
	
	public override void _Ready()
	{
		Visible = false;

		var (savedLanguage, savedSfxVolume, savedMusicVolume) = _saveSystem.LoadSettings();

		TranslationServer.SetLocale(savedLanguage);
		AudioServer.SetBusVolumeDb(_sfxBusIndex, savedSfxVolume);
		AudioServer.SetBusVolumeDb(_musicBusIndex, savedMusicVolume);

		GetNode<HSlider>("VBoxContainer/Volume/VolumeSlider").Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(_sfxBusIndex));
		GetNode<HSlider>("VBoxContainer/Music/MusicSlider").Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(_musicBusIndex));

		var buttonPath = $"Flags/{savedLanguage}";
		if (HasNode(buttonPath))
		{
			GetNode<TextureButton>(buttonPath).ButtonPressed = true;
		}
	}

	private void OpenSettingsMenu()
	{
		Visible = !Visible;
	}

	private void SaveSettingsAndClose()
	{
		Visible = !Visible;
		_saveSystem.SaveSettings(TranslationServer.GetLocale(), AudioServer.GetBusVolumeDb(_sfxBusIndex), AudioServer.GetBusVolumeDb(_musicBusIndex));
	}

	private void SetLanguage(bool toggleOn, string languageCode)
	{
		if (toggleOn)
		{
			TranslationServer.SetLocale(languageCode);
		}
	}
}
