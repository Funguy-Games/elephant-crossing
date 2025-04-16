using Godot;
using System;

namespace ElephantCrossing;
public static class Config
{
    public const string SaveFolder = "user://Save";
    public const string SaveFile = "save.json";
}

public static class SaveKeys
{
    public const string Language = "Language";
    public const string SFXVolume = "SFXVolume";
    public const string MusicVolume = "MusicVolume";
}

public enum LevelID
{
    Tutorial,
    Level1,
    Level2,
    Level3,
    Level4,
    Level5,
    Level6,
    Level7,
    Level8,
    Level9,
    Level10,
    Level11,
    Level12,
    Level13,
    Level14,
}