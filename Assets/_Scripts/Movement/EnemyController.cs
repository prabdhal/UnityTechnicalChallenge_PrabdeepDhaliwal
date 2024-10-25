using System;
using UnityEngine;

public class EnemyController : BaseEnemyController
{
    #region Fields

    [Header("Detection Settings")]
    [SerializeField]
    private float detectionRadius = 10f;
    //[SerializeField]
    //private Transform head;
    //[SerializeField]
    //private LayerMask detectionLayers; 
    //[SerializeField]
    //private LayerMask obstacleLayers; 

    [Header("Patrol Settings")]
    [SerializeField]
    private Transform[] patrolPoints;
    [SerializeField]
    private float waypointAccuracy = 1f;
    [SerializeField, Range(0.5f, 1f)]
    private float patrolSpeedRatio;                 // percentage of movement speed stat
    private float patrolSpeed => stats.MovementSpeed * patrolSpeedRatio;
    private int currentPatrolIndex = 0;

    [Header("Stuck Detection Settings")]
    [SerializeField]
    private float stuckDetectionTime = 3f;          // Time to detect if stuck at patrol point
    [SerializeField]
    private float stuckDistanceThreshold = 0.5f;    // Minimum distance to consider movement

    private float stuckTimer = 0f;
    private Vector3 lastPosition;

    private bool isPlayerDetected;
    private bool isChasingPlayer;
    #endregion

    #region Init & Update
    protected override void Start()
    {
        base.Start();

        InitPatrol();
    }
    private void InitPatrol()
    {
        if (patrolPoints.Length > 0)
        {
            agent.destination = patrolPoints[currentPatrolIndex].position;
            agent.speed = patrolSpeed;
        }
        lastPosition = transform.position;
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

        // Check if the player is within detection range and has a clear line of sight
        isPlayerDetected = IsPlayerDetected(distanceFromPlayer);

        if (isPlayerDetected)
        {
            agent.updateRotation = false;
            if (distanceFromPlayer <= stoppingDistance)
            {
                agent.speed = 0f;
            }
            else
            {
                // If the player is detected and in sight, chase the player
                isChasingPlayer = true;
                agent.speed = chaseSpeed;
            }
            agent.SetDestination(player.transform.position);

            // Manually rotate to player
            RotationHandler();
        }
        else if (isChasingPlayer)
        {
            isChasingPlayer = false;
            Patrol();
        }
        else
        {
            agent.updateRotation = true;
            Patrol();

            CheckIfStuck();
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
            agent.speed = 0;
        }

        agent.speed = patrolSpeed;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    private void CheckIfStuck()
    {
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);

        // Check if the enemy has moved less than the stuck distance threshold
        if (distanceMoved < stuckDistanceThreshold)
        {
            stuckTimer += Time.deltaTime;

            // If stuck for longer than the detection time, update the patrol point
            if (stuckTimer >= stuckDetectionTime)
            {
                UpdatePatrolPoint();
                ResetStuckDetection();
            }
        }
        else
        {
            ResetStuckDetection();
        }

        // Update the last known position
        lastPosition = transform.position;
    }
    private void UpdatePatrolPoint()
    {
        // Move to the next patrol point in the array
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }
    private void ResetStuckDetection()
    {
        stuckTimer = 0f;
        lastPosition = transform.position;
    }

    private bool IsPlayerDetected(float distanceFromPlayer)
    {
        if (distanceFromPlayer > detectionRadius) return false;

        canAttack = true;
        return true;

        // Uncomment for raycast detection 
        // Perform a raycast to see if there is a clear line of sight to the player
        //Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        //Debug.DrawRay(head.position, directionToPlayer * detectionRadius, Color.red, 1f); 

        //if (Physics.Raycast(head.position, directionToPlayer, out RaycastHit hit, detectionRadius, detectionLayers | obstacleLayers))
        //{
        //    // Player is in sight 
        //    if (hit.collider.CompareTag(StringData.PlayerTag))
        //    {
        //        return true;
        //    }
        //}
        //return false;
    }
    #endregion
}
