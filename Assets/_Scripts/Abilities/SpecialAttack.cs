using UnityEngine;

public class SpecialAttack : Ability
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
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        GameObject go = Instantiate(projectilePrefab, spawnPos.position, spawnPos.rotation);
        ProjectileCollider proj = go.GetComponent<ProjectileCollider>();
        proj.Init(ownerStats, this);
    }
}
