using ElephantCrossing;
using Godot;
using System;

[GlobalClass]
public partial class LevelSceneResource : Resource
{
    [Export] public LevelID LevelID;
    [Export] public string LevelPath;
    [Export] public string LevelName;
}
