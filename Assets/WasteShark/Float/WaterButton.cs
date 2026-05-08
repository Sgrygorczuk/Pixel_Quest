using System.Collections.Generic;
using UnityEngine;

public class ObjectSequencer : MonoBehaviour
{
    public List<GameObject> objectsToManage;
    private int currentIndex = 0;
    private bool isWaitingForPhysics = false;

    void Start()
    {
        // Initialize all objects in the list
        foreach (GameObject obj in objectsToManage)
        {
            if (obj != null)
            {
                // Disable physics simulation first
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.simulated = false;
                }

                // Deactivate the object
                obj.SetActive(false);
            }
        }
    }
    
    public void OnButtonClick() {
        HandleInteraction();
    }

    void HandleInteraction()
    {
        // Safety check to ensure the list isn't empty or finished
        if (currentIndex >= objectsToManage.Count) return;

        GameObject currentObj = objectsToManage[currentIndex];

        if (!isWaitingForPhysics)
        {
            // First click: Set object active
            currentObj.SetActive(true);
            isWaitingForPhysics = true;
        }
        else
        {
            // Second click: Enable Rigidbody2D
            Rigidbody2D rb = currentObj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.simulated = true; // Or rb.enabled = true;
            }

            // Reset state and move to next object
            isWaitingForPhysics = false;
            currentIndex++;
        }
    }
}