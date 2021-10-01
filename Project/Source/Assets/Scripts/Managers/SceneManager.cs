using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;

        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);

        scenes.Clear();

        foreach (InspectorLevel level in levels)
        {
            scenes.Add(level.level, level.scene);
        }
    }

    private static Dictionary<Level, string> scenes = new Dictionary<Level, string>();
    public InspectorLevel[] levels;

    public static void LoadLevel(Level level)
    {
        if (!scenes.TryGetValue(level, out string scene))
        {
            Debug.LogError($"Tried to load {level}, but there is not scene assigned to that level!");
            return;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}

[System.Serializable]
public class InspectorLevel
{
    // Used to assign scenes to the enums in the inspector
    public string name; // Just for inspector
    public Level level; // The level enum
    [Scene] public string scene; // The actual scene
}
