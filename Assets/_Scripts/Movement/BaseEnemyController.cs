using System;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyController : MonoBehaviour
{
    #region Fields
    [SerializeField]
    protected GameObject container;       // Root gameobject of enemy
    [SerializeField]
    protected float stoppingDistance = 2f;
    [SerializeField]
    private float attackRotSpeed;       // Rotation speed during attacks

    protected NavMeshAgent agent;
    protected GameObject player;

    protected Animator anim;
    protected CharacterStats stats;

    protected float chaseSpeed => stats.MovementSpeed;
    protected bool canAttack = false;
    public bool CanAttack => canAttack;
    public bool IsAttacking => anim.GetBool(StringData.IsAttackingAnimatorParam);

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

        if (distanceFromPlayer <= stoppingDistance)
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

    #region Rotate Handler
    protected void RotationHandler()
    {
        if (!IsAttacking)
        {
            RotateTowards(player.transform.position, agent.angularSpeed);
        }
        else
        {
            RotateTowards(player.transform.position, attackRotSpeed);
        }
    }
    protected void RotateTowards(Vector3 targetPosition, float rotSpeed)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0; // Keep the rotation on the horizontal plane

        if (direction.magnitude >= 0.1f)
        {
            // Calculate the target rotation
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            // Smoothly rotate towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
        }
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
