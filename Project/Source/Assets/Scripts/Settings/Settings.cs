using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Settings
{
    public static void WriteInputs(string fileName)
    {
        InputProfile profile = Inputs.inputProfile;
        if (profile == null) profile = new InputProfile();

        string inputJson = JsonUtility.ToJson(profile);
        File.WriteAllText(Path.Combine(Application.dataPath, fileName), inputJson);
    }

    public static void ReadInputs(string fileName)
    {
        string inputJson = File.ReadAllText(Path.Combine(Application.dataPath, fileName));

        InputProfile profile = JsonUtility.FromJson<InputProfile>(inputJson);
        if (profile == null)
        {
            profile = new InputProfile();
            Debug.LogWarning("Could not read Input Profile, creating new...");
        }

        Inputs.inputProfile = profile;
    }
}
