using UnityEngine;

public class SpreadProjectileAttack : ProjectileAttack
{
    [Header("Spread Settings")]
    [SerializeField]
    private int numberOfProjectiles = 3; 
    [SerializeField]
    private float spreadArc = 60f; 

    public override void Execute()
    {
        if (!IsOnCooldown && HasMana())
        {
            HandleAnimation();
            StartCooldown();
        }
    }

    public override void SpawnProjectile()
    {
        // Ensure projectile Y-axis remains level (ignoring animation)
        Vector3 firePos = new Vector3(spawnPos.position.x, yPositionOfProjectile.position.y, spawnPos.position.z);

        // Calculate the starting angle
        float angleStep = spreadArc / (numberOfProjectiles - 1);
        float startAngle = -spreadArc / 2;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            // Calculate the rotation for each projectile
            float currentAngle = startAngle + i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, currentAngle, 0) * spawnPos.rotation;

            InstantiateProjectile(firePos, rotation);
            PlayFireSound();
        }
    }

    private void InstantiateProjectile(Vector3 position, Quaternion rotation)
    {
        GameObject go = Instantiate(projectilePrefab, position, rotation);
        ProjectileCollider proj = go.GetComponent<ProjectileCollider>();
        proj.Init(ownerStats, this);
    }
}
