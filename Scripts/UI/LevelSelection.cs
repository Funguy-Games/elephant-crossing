using Godot;
using System;

namespace ElephantCrossing.UI;
public partial class LevelSelection : Control
{
    [Export] private Control _buttonContainer;
    [Export] private SceneManager _sceneManager;
    [Export] private PackedScene _levelButtonScene;
    [Export] private SaveSystem _saveSystem;
    [Export] private LevelSceneResource[] _levels;


    public override void _Ready()
    {
        foreach(LevelSceneResource level in _levels)
        {
            LevelButton button = _levelButtonScene.Instantiate<LevelButton>();
            button.SceneName = level.LevelPath;
            button.SceneManager = _sceneManager;
            button.GetNode<Label>("Label").Text = level.LevelName; // I don't really like how I'm doing this here
            int score = _saveSystem.LoadLevelData(level.LevelID.ToString());
            button.FillStars(score);
            _buttonContainer.AddChild(button);
        }
    }

}
