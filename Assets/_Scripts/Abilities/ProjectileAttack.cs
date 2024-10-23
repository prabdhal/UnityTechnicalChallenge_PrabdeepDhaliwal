using UnityEngine;

public class ProjectileAttack : Ability
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform spawnPos;

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
        GameObject go = Instantiate(projectilePrefab, spawnPos.position, spawnPos.rotation);
        ProjectileCollider proj = go.GetComponent<ProjectileCollider>();
        proj.Init(ownerStats, this);
        soundManager.PlaySpecialAbilitySound();
    }
}
