using UnityEngine;
using UnityEngine.UI; // Required for Toggle

public class PhysicsTankController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;
    public float spinSpeed = 300f;

    [Header("UI References")]
    public Toggle leftToggle;
    public Toggle rightToggle;

    [Header("Visuals")]
    public GameObject leftSprite;
    public GameObject rightSprite;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        // Initialize all toggles to off
        leftToggle.isOn = false;
        rightToggle.isOn = false;
    }

    void Update()
    {
        HandleSpin();
    }

    void FixedUpdate()
    {
        ApplyPhysicsMovement();
    }

    void ApplyPhysicsMovement()
    {
        // Check state from UI Toggles
        bool leftActive = leftToggle.isOn;
        bool rightActive = rightToggle.isOn;
        bool isMovingBackwards = true;

        float moveDir = isMovingBackwards ? -1f : 1f;
        Vector2 velocity = Vector2.zero;
        float rotationAmount = 0f;

        if (leftActive && rightActive)
        {
            velocity = transform.up * moveDir * moveSpeed;
        }
        else if (leftActive)
        {
            // Only Left: Turn Right (CW)
            velocity = transform.up * moveDir * (moveSpeed * 0.5f);
            rotationAmount = isMovingBackwards ? turnSpeed : -turnSpeed;
        }
        else if (rightActive)
        {
            // Only Right: Turn Left (CCW)
            velocity = transform.up * moveDir * (moveSpeed * 0.5f);
            rotationAmount = isMovingBackwards ? -turnSpeed : turnSpeed;
        }
        else
        {
            // Neither: Stop movement
            velocity = Vector2.zero;
            rotationAmount = 0f;
        }

        rb.linearVelocity = velocity;
        float nextRotation = rb.rotation + (rotationAmount * Time.fixedDeltaTime);
        rb.MoveRotation(nextRotation);
    }

    void HandleSpin()
    {
        if (leftToggle.isOn) 
            leftSprite.transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
        
        if (rightToggle.isOn) 
            rightSprite.transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }
}