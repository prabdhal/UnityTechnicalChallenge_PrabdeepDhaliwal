using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] 
    private float rotationSpeed;

    private void Update()
    {
        // Rotate along the Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}