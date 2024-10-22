using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    #region Fields
    [Header("Detection Settings")]
    [SerializeField]
    private Transform head;
    [SerializeField]
    private float detectionRadius = 10f;
    [SerializeField]
    private LayerMask detectionLayers; // Layers the enemy can detect (e.g., Player layer)
    [SerializeField]
    private LayerMask obstacleLayers; // Layers for obstacles (e.g., Walls)

    [Header("Patrol Settings")]
    [SerializeField]
    private Transform[] patrolPoints;
    [SerializeField]
    private float waypointAccuracy = 1f;
    [SerializeField, Range(0.5f, 1f)]
    private float patrolSpeedRatio;         // percentage of movement speed stat
    private float patrolSpeed => stats.MovementSpeed * patrolSpeedRatio;
    private float chaseSpeed => stats.MovementSpeed;

    private NavMeshAgent agent;
    private int currentPatrolIndex = 0;
    private GameObject player;
    private bool isPlayerDetected;
    private bool isChasingPlayer;

    private Animator anim;
    private CharacterStats stats;

    public Action<EnemyController> OnEnemyKilled;
    #endregion

    #region Init & Update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<CharacterStats>();
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag(StringData.PlayerTag);

        InitPatrol();
    }
    private void InitPatrol()
    {
        if (patrolPoints.Length > 0)
        {
            agent.destination = patrolPoints[currentPatrolIndex].position;
            agent.speed = patrolSpeed;
        }
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

        // Check if the player is within detection range and has a clear line of sight
        isPlayerDetected = IsPlayerDetected(distanceFromPlayer);

        if (isPlayerDetected)
        {
            // If the player is detected and in sight, chase the player
            isChasingPlayer = true;
            agent.speed = chaseSpeed;
            agent.SetDestination(player.transform.position);
        }
        else if (isChasingPlayer)
        {
            // If the player was recently detected but is now out of sight, keep chasing for a short duration
            isChasingPlayer = false;
            Patrol();
        }
        else
        {
            Patrol();
        }
    }
    #endregion

    #region Patrol & Detection
    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        // If close enough to the current patrol point, move to the next one
        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position) <= waypointAccuracy)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        agent.speed = patrolSpeed;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    private bool IsPlayerDetected(float distanceFromPlayer)
    {
        if (distanceFromPlayer > detectionRadius) return false;

        // Perform a raycast to see if there is a clear line of sight to the player
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        Debug.Log("IsPlayerDetected:?");
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRadius, detectionLayers | obstacleLayers))
        {
            Debug.Log("IsPlayerDetected -- hitting: " + hit.collider.name);
            // Player is in sight 
            if (hit.collider.CompareTag(StringData.PlayerTag))
            {
                Debug.Log("Player Detected! -- " + hit.collider.name);
                return true;
            }
        }

        return false;
    }
    #endregion

    #region Death
    private void Death()
    {
        OnEnemyKilled?.Invoke(this);

        Destroy(gameObject);
    }
    public void Kill()
    {
        stats.InflictDamage(10000);
    }
    #endregion
}