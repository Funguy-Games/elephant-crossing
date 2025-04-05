using Godot;
using System;

namespace ElephantCrossing;
public partial class SceneButton : BaseButton
{
    [Export]
    private string _sceneName = "";
    [Export]
    private SceneManager _sceneManager = null;

    public override void _Ready()
    {
        base._Ready();
        Pressed += Press;
    }

    private void Press() =>
            _sceneManager.LoadScene(_sceneName);






}
