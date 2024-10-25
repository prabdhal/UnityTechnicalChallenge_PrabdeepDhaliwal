using Cinemachine;
using UnityEngine;

public class TargetDetection : MonoBehaviour
{
    #region Fields
    [Header("Target Detection Settings")]
    [SerializeField]
    private float detectionRadius = 10f;
    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private CinemachineTargetGroup targetGroup;

    private Transform currentTarget;
    private PlayerController controller;
    #endregion

    #region Init & Update
    private void Start()
    {
        controller = GetComponent<PlayerController>();
        targetGroup = FindObjectOfType<CinemachineTargetGroup>();
    }
    private void Update()
    {
        if (currentTarget == null)
        {
            FindNearestTarget();
        }

        if (controller.IsTargetLocked && controller.LockedTarget != null)
        {
            // Add the locked target to the Cinemachine Target Group if not already there
            if (!IsTargetInGroup(controller.LockedTarget))
            {
                targetGroup.AddMember(controller.LockedTarget, 1f, 10f);
            }
        }

        // Always ensure the player remains in the target group
        if (!IsTargetInGroup(controller.transform))
        {
            targetGroup.AddMember(controller.transform, 1f, 2f);
        }
    }
    #endregion

    #region Target Helpers
    public Transform GetCurrentTarget()
    {
        return currentTarget;
    }

    public void ClearTarget()
    {
        // Remove the locked target if no longer locked
        if (IsTargetInGroup(controller.LockedTarget))
        {
            targetGroup.RemoveMember(controller.LockedTarget);
        }
        currentTarget = null;
    }

    private void FindNearestTarget()
    {
        Collider[] targetsInRange = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);
        float closestDistance = detectionRadius;
        Transform nearestTarget = null;

        foreach (Collider target in targetsInRange)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                nearestTarget = target.transform;
            }
        }

        currentTarget = nearestTarget;
    }

    private bool IsTargetInGroup(Transform target)
    {
        foreach (var member in targetGroup.m_Targets)
        {
            if (member.target == target)
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    #endregion
}
