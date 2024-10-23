using UnityEngine;

public class ProjectileAttack : Ability
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform spawnPos;
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

    public void SpawnProjectile()
    {
        // Ensure projectile Y-axis remains level (ignoring animation)
        Vector3 firePos = new Vector3(spawnPos.position.x, yPositionOfProjectile.position.y, spawnPos.position.z);

        GameObject go = Instantiate(projectilePrefab, firePos, spawnPos.rotation);
        ProjectileCollider proj = go.GetComponent<ProjectileCollider>();
        proj.Init(ownerStats, this);
        soundManager.PlaySpecialAbilitySound();
    }
}
