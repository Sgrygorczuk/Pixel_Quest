using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUIHandler : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform logoUI;
    public Transform logoButtonContainer;
    public Transform levelSelectUI; // The object with the GridLayoutGroup

    private void Start() {
        CreateCategoryButtons();
    }

    private void CreateCategoryButtons() {
        for (int i = 0; i < LevelManager.Instance.folderPath.Count; i++) {
            int index = i; // Closure fix
            GameObject btn = Instantiate(buttonPrefab, logoButtonContainer);

            // Get last part of path (e.g., "Assets/Scenes/Forest" -> "Forest")
            string folderName = Path.GetFileName(LevelManager.Instance.folderPath[i]);

            TMP_Text txt = btn.GetComponentInChildren<TMP_Text>();
            if (txt != null) txt.text = folderName;

            // When clicked, show levels in this folder
            btn.GetComponent<Button>().onClick.AddListener(() => SelectGroup(index));
        }
    }

    private void SelectGroup(int index) {
        logoUI.gameObject.SetActive(false);
        levelSelectUI.gameObject.SetActive(true);
        PopulateLevels(index);
    }

    private void PopulateLevels(int index) {
        
        ClearContainer();
        
        // Validate the index to avoid "Index Out of Range" errors
        if (index < 0 || index >= LevelManager.Instance.levelData.Count)
        {
            Debug.LogError("Invalid level index: " + index);
            return;
        }

        // Access the specific folder using the index
        LevelData folder = LevelManager.Instance.levelData[index];

        foreach (string sceneName in folder.levelNames)
        {
            GameObject newButton = Instantiate(buttonPrefab, levelSelectUI);
        
            TMP_Text btnText = newButton.GetComponentInChildren<TMP_Text>();
            if (btnText != null)
            {
                btnText.text = sceneName;
            }

            string nameCopy = sceneName; 
            newButton.GetComponent<Button>().onClick.AddListener(() => LoadLevel(nameCopy));
        }
        
        //Sets Up the back button 
        GameObject backButton = Instantiate(buttonPrefab, levelSelectUI);
        
        TMP_Text backText = backButton.GetComponentInChildren<TMP_Text>();
        if (backText != null)
        {
            backText.text = "Back";
        }
        
        backButton.GetComponent<Button>().onClick.AddListener(BackToMenu);
    }
    
    void ClearContainer()
    {
        // container is the Transform of the GameObject with the GridLayoutGroup
        foreach (Transform child in levelSelectUI)
        {
            Destroy(child.gameObject);
        }
    }

    private void LoadLevel(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    private void BackToMenu() {
        logoUI.gameObject.SetActive(true);
        levelSelectUI.gameObject.SetActive(false);
    }
}