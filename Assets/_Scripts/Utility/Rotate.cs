using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]    
    private EnemyController controller;

    [SerializeField] 
    private float rotationSpeed;

    private void Update()
    {
        if (controller.CanAttack)
        {
            // Rotate along the Y-axis
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}