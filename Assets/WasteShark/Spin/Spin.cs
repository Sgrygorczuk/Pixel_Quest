using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required for TextMeshPro

public class ToggleSpriteController : MonoBehaviour
{
    [Header("References")]
    public Toggle toggle;
    public Image targetImage;
    public TextMeshProUGUI statusText;
    public Sprite onSprite;
    public Sprite offSprite;

    [Header("Rotation Settings")]
    public RectTransform rotatingPart;
    public float maxRotationSpeed = 300f;
    public float acceleration = 150f;
    public float deceleration = 200f;

    private float currentSpeed = 0f;

    void Start()
    {
        // Force the toggle to off state at start
        toggle.isOn = false;
        
        toggle.onValueChanged.AddListener(HandleToggle);
        
        // Initialize UI elements manually for the start state
        UpdateUI(false);
    }

    void Update()
    {
        if (toggle.isOn)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxRotationSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
        }

        rotatingPart.Rotate(0, 0, currentSpeed * Time.deltaTime);
    }

    void HandleToggle(bool isOn)
    {
        UpdateUI(isOn);
    }

    void UpdateUI(bool isOn)
    {
        // Switch Sprite
        targetImage.sprite = isOn ? onSprite : offSprite;
        
        // Switch Text
        statusText.text = isOn ? "On" : "Off";
    }
}