using Godot;
using System;

namespace ElephantCrossing;
public partial class SceneButton : BaseButton
{
    [Export] protected string _sceneName = "";
    [Export] protected SceneManager _sceneManager = null;

    public string SceneName
    {
        get => _sceneName;
        set => _sceneName = value;
    }

    public SceneManager SceneManager
    {
        get => _sceneManager;
        set => _sceneManager = value;
    }

    public override void _Ready()
    {
        base._Ready();
        Pressed += Press;
    }

    private void Press() =>
            _sceneManager.LoadScene(_sceneName);






}
