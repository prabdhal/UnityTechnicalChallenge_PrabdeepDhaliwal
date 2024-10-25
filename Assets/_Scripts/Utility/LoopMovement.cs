using UnityEngine;

public class LoopMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    private Vector3 pointA; 
    [SerializeField]
    private Vector3 pointB;  
    [SerializeField]
    private float speed = 1f; 

    private float lerpTime;

    private void Start()
    {
        pointA = transform.position;
        pointB = pointA + pointB;
    }
    private void Update()
    {
        lerpTime += speed * Time.deltaTime;

        float t = Mathf.PingPong(lerpTime, 1f);

        transform.position = Vector3.Lerp(pointA, pointB, t);
    }
}
