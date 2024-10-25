using UnityEngine;

public class ProjectileCollider : AttackCollider
{
    #region Fields
    [Header("Projectile Stats")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float deathTimer;
    [SerializeField]
    private bool deathOnHit = true;

    private Vector3 startPos;
    #endregion

    #region Init & Update
    private void Start()
    {
        startPos = transform.position;

        if (deathOnHit)
        {
            OnHitTarget += Death;
        }
    }

    public void Update()
    {
        ProjectileMovement();
        DeathHandler();
    }
    #endregion

    #region OnTrigger
    protected override void OnTriggerEnter(Collider other)
    {
        // Destroy upon wall collision
        if (other.CompareTag(StringData.WallTag))
        {
            Death();
        }

        base.OnTriggerEnter(other);
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
