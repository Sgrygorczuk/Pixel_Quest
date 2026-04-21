using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class InGameFadeOut : MonoBehaviour
{
    public CanvasGroup blackFadePanel;
    public CanvasGroup namePanel;

    public float blackFadeDuration = 0.5f;
    public float nameFadeDuration = 1.0f;

    private void Start() {
        TextMeshProUGUI text = namePanel.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        if (LevelManager.Instance) { text.text = LevelManager.Instance.CurrentLevel; }
        StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        // 1. Fade out the black panel quickly
        yield return StartCoroutine(FadeCanvasGroup(blackFadePanel, 1, 0, blackFadeDuration));

        // 2. Wait or trigger immediately? 
        // We trigger the second fade now that the first is transparent.
        yield return StartCoroutine(FadeCanvasGroup(namePanel, 1, 0, nameFadeDuration));
    }

    public void StartFadeIn(Action callback) {
        StartCoroutine(FadeCanvasGroup(blackFadePanel, 0 ,1, blackFadeDuration, callback));
    }
    
    private static IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        cg.alpha = end;
    }
    
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration, Action callback)
    {
        if (callback == null) throw new ArgumentNullException(nameof(callback));
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        cg.alpha = end;
        
        callback.Invoke();
    }
}
