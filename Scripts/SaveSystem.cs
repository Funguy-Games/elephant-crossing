using Godot;
using System;
using System.IO;
using Godot.Collections;

namespace ElephantCrossing;
public partial class SaveSystem : Node
{
    public void Save(int score, string levelID)
	{
		Dictionary saveData = new Dictionary();
        if(Load(out Dictionary data))
        {
            saveData = data;
        }

        if(saveData.ContainsKey(levelID))
        {
            saveData[levelID] = score;
        }
        else
        {
		    saveData.Add(levelID, score);
        }

		string json = Json.Stringify(saveData);

		string savePath = ProjectSettings.GlobalizePath(Config.SaveFolder);

		if (SaveToFile(savePath, Config.SaveFile, json))
		{
			GD.Print("Game saved");
		}
		else
		{
			GD.PrintErr("Game saving failed");
		}
	}

    public int LoadLevelData(string levelID)
    {
        if(Load(out Dictionary data))
        {
            if(data.ContainsKey(levelID))
                return (int) data[levelID];
        }
        return 0;
    }

	public bool Load(out Dictionary data)
	{
		string savePath = ProjectSettings.GlobalizePath(Config.SaveFolder);
		string loadedJson = LoadFromFile(savePath, Config.SaveFile);

		Json jsonLoader = new Json();
		Error loadError = jsonLoader.Parse(loadedJson);
		if (loadError != Error.Ok)
		{
			GD.PrintErr($"Error loading the save: {loadError}");
            data = null;
			return false;
		}

		Dictionary saveData = (Dictionary)jsonLoader.Data;
        data = saveData;
		return true;
	}


	private bool SaveToFile(string path, string fileName, string json)
	{
		if (!Directory.Exists(path))
		{
			try
			{
				Directory.CreateDirectory(path);
			}
			catch (Exception e)
			{
				GD.PrintErr($"Error saving file: {e.Message}");
				return false;
			}
		}

		path = Path.Combine(path, fileName);

		try
		{
			File.WriteAllText(path, json);
		}
		catch (Exception e)
		{
			GD.PrintErr($"Error saving file: {e.Message}");
			return false;
		}

		return true;
	}

	private string LoadFromFile(string path, string fileName)
	{
		path = Path.Combine(path, fileName);

		if (!File.Exists(path))
		{
			GD.PrintErr($"File {path} not found.");
			return null;
		}

		try
		{
			return File.ReadAllText(path);
		}
		catch (Exception e)
		{
			GD.PrintErr($"Error loading file: {e.Message}");
			return null;
		}
	}
}
