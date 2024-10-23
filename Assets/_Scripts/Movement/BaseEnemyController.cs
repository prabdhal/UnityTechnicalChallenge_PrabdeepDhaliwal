using System;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyController : MonoBehaviour
{
    #region Fields
    [SerializeField]
    protected GameObject container;       // Root gameobject of enemy
    [SerializeField]
    protected float stopppingDistance = 2f;

    protected NavMeshAgent agent;
    protected GameObject player;

    protected Animator anim;
    protected CharacterStats stats;

    protected bool canAttack = false;
    public bool CanAttack => canAttack;
    protected float chaseSpeed => stats.MovementSpeed;


    public Action<BaseEnemyController> OnEnemyKilled;
    #endregion

    #region Init & Update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<CharacterStats>();
        anim = GetComponentInChildren<Animator>();

        player = GameObject.FindGameObjectWithTag(StringData.PlayerTag);
    }

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
    }
    #endregion

    #region Death
    protected virtual void Death()
    {
        OnEnemyKilled?.Invoke(this);
        Destroy(container);
    }
    public void Kill()
    {
        stats.InflictDamage(10000);
    }
    #endregion
}
