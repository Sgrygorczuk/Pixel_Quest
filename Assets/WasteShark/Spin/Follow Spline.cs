using UnityEngine;
using UnityEngine.Splines;

public class FollowPathLockedRotation : MonoBehaviour 
{
    public SplineContainer spline;
    public float speed = 0.1f;
    public int myIndex = 0; 
    private float distance = 0f;
    private Vector3 originalPosition;

    void Start()
    {
        // Store the original position to return to later
        originalPosition = transform.position;
    }

    void Update() 
    {
        // Increment distance (normalized t)
        distance += speed * Time.deltaTime;

        // 1. Check if we reached or passed the end (t >= 1.0)
        if (distance >= 1.0f)
        {
            // Optional: Clamp to 1.0 to ensure it stops exactly at the end
            distance = 1.0f; 
            transform.position = spline.EvaluatePosition(distance);
        
            ResetAndRemove();
            return;
        }

        // 2. Check for "Approximately Zero" (if moving backward)
        if (speed < 0 && distance <= Mathf.Epsilon)
        {
            ResetAndRemove();
            return;
        }

        // Update position and lock rotation
        transform.position = spline.EvaluatePosition(distance);
        transform.rotation = Quaternion.identity; 
    }
    
    private void ResetAndRemove()
    {
        distance = 0f;
        transform.position = originalPosition;
        RequestSelfRemoval();
    }

    public void RequestSelfRemoval()
    {
        // Check Instance to avoid errors during scene transitions
        if (PathController.Instance != null)
        {
            PathController.Instance.RemoveIndexFromList(myIndex);
        }
    }
}