using Godot;
using System;

namespace ElephantCrossing;
public partial class SceneManager : Node
{
    private FadeCanvas _fade = null;
    private string _nextScene = "";

    public override void _Ready()
    {
        _fade = GetParent().GetNode<FadeCanvas>("FadeCanvas");
    }

    public void LoadLevel1()
    {
        _nextScene = "res://Levels/main.tscn";
        _fade.FadedOut += ChangeScene;
        _fade.FadeOut();
    }

    public void LoadLevelSelector()
    {
        _nextScene = "res://Levels/LevelSelection.tscn";
        _fade.FadedOut += ChangeScene;
        _fade.FadeOut();
    }

    public void LoadMainMenu()
    {
        _nextScene = "res://Levels/menu.tscn";
        _fade.FadedOut += ChangeScene;
        _fade.FadeOut();
    }

    /// <summary>
    /// Changes the current scene based on a given scene name.
    /// Before the scene changes this method attempts to fade out the current scene.
    /// </summary>
    /// <param name="scene">Name of the next scene</param>
    public void LoadScene(string scene)
    {
        _nextScene = scene;

        if(_fade == null)
        {
            ChangeScene();
            return;
        }

        _fade.FadedOut += ChangeScene;
        _fade.FadeOut();
    }

    private void ChangeScene()
    {
        GetTree().ChangeSceneToFile(_nextScene);
    }
}
