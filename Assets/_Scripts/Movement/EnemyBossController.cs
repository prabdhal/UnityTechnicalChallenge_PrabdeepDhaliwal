using UnityEngine;
using UnityEngine.AI;

public class EnemyBossController : BaseEnemyController
{
    #region Fields
    [SerializeField]
    private BossDetectionZone detectionZone;
    #endregion

    #region Init
    protected override void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<CharacterStats>();
        anim = GetComponentInChildren<Animator>();
        agent.updateRotation = false;
        detectionZone.OnBossZoneEnter += Setup;
        OnEnemyKilled += GameUIManager.Instance.OnVictory;
        canAttack = true;
    }
    private void Setup(GameObject player)
    {
        this.player = player;
    }
    #endregion

    #region Update
    private void Update()
    {
        if (stats.IsDead())
        {
            Death();
            return;
        }
        if (player == null) return;

        float distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
        anim.SetFloat(StringData.MoveSpeedParam, agent.speed);

        if (distanceFromPlayer <= stoppingDistance)
        {
            agent.speed = 0f;
        }
        else
        {
            agent.speed = chaseSpeed;
        }
        agent.SetDestination(player.transform.position);

        RotationHandler();  // Manually rotate to player
    }
    #endregion
}
