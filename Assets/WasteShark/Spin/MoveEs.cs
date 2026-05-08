using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class PathController : MonoBehaviour
{
    public static PathController Instance { get; private set; }
    
    public Toggle activeToggle;
    public List<int> numberList = new List<int>();
    public int currentIndex = 0;
    public float interval = 2.0f;
    private List<FollowPathLockedRotation> cachedScripts = new List<FollowPathLockedRotation>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            var script = child.GetComponent<FollowPathLockedRotation>();
    
            if (script != null)
            {
                cachedScripts.Add(script);
                script.myIndex = i;
                script.enabled = false;
            }
        }
        // Add listener to handle the bulk ON/OFF when clicking the toggle
        if (activeToggle != null)
        {
            activeToggle.onValueChanged.AddListener(OnToggleChanged);
        }
        
        StartCoroutine(AddToListRoutine());
    }

    // This handles the bulk updates when you click the UI
    void OnToggleChanged(bool isOn)
    {
        foreach (int index in numberList)
        {
            if (index >= 0 && index < cachedScripts.Count)
            {
                cachedScripts[index].enabled = isOn;
            }
        }
    }

    IEnumerator AddToListRoutine()
    {
        while (true)
        {
            if (activeToggle != null && activeToggle.isOn)
            {
                // Ensure index is valid before adding
                if (currentIndex >= 0 && currentIndex < cachedScripts.Count)
                {
                    cachedScripts[currentIndex].enabled = true;
                    numberList.Add(currentIndex);
                    currentIndex++;
                    if (currentIndex >= cachedScripts.Count) {
                        currentIndex = 0;
                    }
                    Debug.Log($"Added and enabled index {currentIndex}");
                }
            }

            yield return new WaitForSeconds(interval);
        }
    }
    
    public void RemoveIndexFromList(int index)
    {
        if (numberList.Contains(index))
        {
            numberList.Remove(index);
            cachedScripts[index].enabled = false;
            Debug.Log($"Index {index} removed and disabled.");
        }
    }

    private void OnDestroy()
    {
        if (activeToggle != null)
        {
            activeToggle.onValueChanged.RemoveListener(OnToggleChanged);
        }
    }
}