using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public float speed = 2f;
    public float height = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float y = Mathf.PingPong(Time.time * speed, height * 2) - height;
        transform.position = startPos + new Vector3(0, y, 0);
    }
}
