using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public List<string> folderPath = new List<string>();
    public List<LevelData> levelData = new List<LevelData>();
    public string CurrentLevel { get; set; } = "Level1";
    
    private void Awake() {
        // Ensure only one instance exists
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
    
    private void Start() {
        foreach (string t in folderPath) {
            List<string> levels = GetSceneNamesFromFolder(t);
            LevelData tempLevelData = new LevelData { levelNames = levels };
            levelData.Add(tempLevelData);
        }
    }

    private List<string> GetSceneNamesFromFolder(string path)
    {
        List<string> names = new List<string>();

        // Verify the directory exists
        if (Directory.Exists(path))
        {
            // Get all files ending in .unity
            string[] files = Directory.GetFiles(path, "*.unity");

            foreach (string file in files)
            {
                // Get filename without extension
                string fileName = Path.GetFileNameWithoutExtension(file);

                // Only add if the filename itself has no digits
                if (!fileName.Any(char.IsDigit)) 
                {
                    names.Add(fileName);
                }
            }
        }
        else {
            Debug.LogError("Directory not found: " + path);
        }

        return names;
    }
}

[System.Serializable]
public class LevelData
{
    public List<string> levelNames = new List<string>();
}
