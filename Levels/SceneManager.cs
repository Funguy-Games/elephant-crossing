using Godot;
using System;

public partial class SceneManager : Node
{
    public void LoadLevel1()
    {
        GetTree().ChangeSceneToFile("res://Levels/main.tscn");
    }
}
