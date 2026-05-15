using UnityEngine;

public class MoveChildren : MonoBehaviour
{
    public Transform childA;
    public Transform childB;

    public float speed = 2f;
    public float closeDistance = 0.1f;
    public float farDistance = 3f;

    private Vector3 startPosA;
    private Vector3 startPosB;
    private bool movingTogether = true;

    void Start()
    {
        startPosA = childA.localPosition;
        startPosB = childB.localPosition;
    }

    void Update()
    {
        Vector3 center = Vector3.zero;

        if (movingTogether)
        {
            // Move toward center
            childA.localPosition = Vector3.MoveTowards(
                childA.localPosition,
                center,
                speed * Time.deltaTime
            );

            childB.localPosition = Vector3.MoveTowards(
                childB.localPosition,
                center,
                speed * Time.deltaTime
            );

            // Check if close enough
            if (Vector3.Distance(childA.localPosition, childB.localPosition) <= closeDistance)
            {
                movingTogether = false;
            }
        }
        else
        {
            // Move back to original positions
            childA.localPosition = Vector3.MoveTowards(
                childA.localPosition,
                startPosA,
                speed * Time.deltaTime
            );

            childB.localPosition = Vector3.MoveTowards(
                childB.localPosition,
                startPosB,
                speed * Time.deltaTime
            );

            // Check if far enough
            if (Vector3.Distance(childA.localPosition, startPosA) < 0.01f &&
                Vector3.Distance(childB.localPosition, startPosB) < 0.01f)
            {
                movingTogether = true;
            }
        }
    }
}