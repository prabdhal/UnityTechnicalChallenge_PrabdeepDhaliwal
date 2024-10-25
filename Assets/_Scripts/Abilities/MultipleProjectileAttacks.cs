using UnityEngine;

public class MultipleProjectileAttacks : Ability
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform[] spawnPos;
    [SerializeField]
    private Transform yPositionOfProjectile;        // Ensures Y-axis of projectile remains level

    public override void Execute()
    {
        if (!IsOnCooldown && HasMana())
        {
            HandleAnimation();
            StartCooldown();

            // Using animation events handler to fire projectile
            //SpawnProjectile();
        }
    }

    public void SpawnProjectiles()
    {
        foreach (var transform in spawnPos)
        {
            // Ensure projectile Y-axis remains level (ignoring animation)
            Vector3 firePos = new Vector3(transform.position.x, yPositionOfProjectile.position.y, transform.position.z);

            GameObject go = Instantiate(projectilePrefab, firePos, transform.rotation);
            ProjectileCollider proj = go.GetComponent<ProjectileCollider>();
            proj.Init(ownerStats, this);
            PlayFireSound();
        }
    }
}
