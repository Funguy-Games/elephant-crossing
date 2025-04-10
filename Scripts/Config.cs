using Godot;
using System;

namespace ElephantCrossing;
public static class Config
{
    public const string SaveFolder = "user://Save";
    public const string SaveFile = "save.json";
}

public enum LevelID
{
    Tutorial,
    Level1,
    Level2,
    Level3
}