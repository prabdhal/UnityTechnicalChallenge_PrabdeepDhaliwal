using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    [SerializeField]
    private float deathTimer = 5f;

    private void Update()
    {
        if (deathTimer <= 0)
            Destroy(gameObject);
        else
            deathTimer -= Time.deltaTime;
    }
}
