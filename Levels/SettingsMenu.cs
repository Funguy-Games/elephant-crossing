using Godot;
using System;

public partial class SettingsMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Visible = false;

		var locale = TranslationServer.GetLocale();
		var buttonPath = $"Flags/{locale}";

		if (HasNode(buttonPath))
		{
			GetNode<TextureButton>(buttonPath).ButtonPressed = true;
		}
	}

	private void OpenSettingsMenu()
	{
		Visible = !Visible;
	}

	private void SetLanguage(bool toggleOn, string languageCode)
	{
		if (toggleOn)
		{
			TranslationServer.SetLocale(languageCode);
		}
	}
}
