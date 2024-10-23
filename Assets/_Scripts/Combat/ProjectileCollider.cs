using UnityEngine;

public class ProjectileCollider : AttackCollider
{
    #region Fields
    [Header("Projectile Stats")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float deathTimer;

    private Vector3 startPos;
    #endregion

    #region Init & Update
    private void Start()
    {
        startPos = transform.position;

        OnHitTarget += Death;
    }

    public void Update()
    {
        ProjectileMovement();
        DeathHandler();
    }
    #endregion

    #region Projectile Movement
    private void ProjectileMovement()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    #endregion
    
    #region Death Handler
    private void DeathHandler()
    {
        float distanceFromStart = Vector3.Distance(startPos, transform.position);

        if (deathTimer <= 0 || distanceFromStart > ability.AbilityStats.range)
            Destroy(gameObject);
        else
            deathTimer -= Time.deltaTime;
    }
    private void Death()
    {
        Destroy(gameObject);
    }
    #endregion
}
