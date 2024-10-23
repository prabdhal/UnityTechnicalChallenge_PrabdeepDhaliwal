using UnityEngine;

public class EnemyBossController : BaseEnemyController
{
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

        if (distanceFromPlayer <= stopppingDistance)
        {
            agent.speed = 0f;
        }
        else
        {
            agent.speed = chaseSpeed;
        }
        agent.SetDestination(player.transform.position);

        canAttack = true;
    }
    #endregion
}
