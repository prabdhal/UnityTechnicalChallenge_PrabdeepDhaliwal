using UnityEngine;

public class EnemyBossController : BaseEnemyController
{

    #region Init
    protected override void Start()
    {
        base.Start();
        agent.updateRotation = false;
        canAttack = true;
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

        // Manually rotate to player
        RotationHandler();
    }
    #endregion
}
