using Godot;
using System;

public partial class SceneManager : Node
{
    private FadeCanvas _fade = null;
    public override void _Ready()
    {
        _fade = GetParent().GetNode<FadeCanvas>("FadeCanvas");
    }
    public void LoadLevel1()
    {
        _fade.FadedOut += ChangeScene;
        _fade.FadeOut();
    }

    public void LoadMainMenu()
    {
        GetTree().ChangeSceneToFile("res://Levels/menu.tscn");
    }

    private void ChangeScene()
    {
        GetTree().ChangeSceneToFile("res://Levels/main.tscn");
    }
}
