using System;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    #region Fields
    [Header("Player Settings")]
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float turnSpeed;

    [Header("Camera Settings")]
    public Transform cameraTransform;
    public float cameraSensitivity = 3f;

    [Header("Target Lock Settings")]
    public float lockOnTurnSpeed = 5f;
    private TargetDetection targetDetection;
    private Transform lockedTarget;
    public Transform LockedTarget => lockedTarget;
    private bool isTargetLocked;
    public bool IsTargetLocked => isTargetLocked;

    private CharacterController controller;
    private CharacterStats stats;
    private PlayerCombat combat;
    private PlayerInputManager inputManager;
    private Animator anim;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private float turnSmoothVelocity;

    private bool isDead = false;
    public bool IsDead => isDead;

    public Action OnPlayerKilled;
    #endregion

    #region Init & Update
    private void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
        controller = GetComponent<CharacterController>();
        stats = GetComponent<CharacterStats>();
        combat = GetComponent<PlayerCombat>();
        anim = GetComponentInChildren<Animator>();
        cameraTransform = Camera.main.transform;
        targetDetection = GetComponent<TargetDetection>();
    }
    private void Update()
    {
        if (stats.IsDead() && !isDead)
        {
            isDead = true;
            Death();
            return;
        }

        HandleTargetLocking();
        Movement();
        RotatePlayerWithCamera();
        ApplyGravity();

        JumpHandler();
    }
    #endregion

    #region Target Locking
    private void HandleTargetLocking()
    {
        if (inputManager.IsTargetLocked)
        {
            if (!isTargetLocked)
            {
                // Lock onto the nearest target
                lockedTarget = targetDetection.GetCurrentTarget();
                if (lockedTarget != null)
                {
                    isTargetLocked = true;
                }
            }
        }
        else
        {
            isTargetLocked = false;
            targetDetection.ClearTarget();
            lockedTarget = null;
        }
    }
    #endregion

    #region Movement
    private void Movement()
    {
        // Get input for movement
        float horizontal = inputManager.MovementInput.x;
        float vertical = inputManager.MovementInput.y;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        anim.SetFloat(StringData.MoveSpeedParam, direction.magnitude);

        if (direction.magnitude >= 0.1f)
        {
            if (isTargetLocked && lockedTarget != null)
            {
                // Strafe relative to the locked target
                Vector3 targetDirection = (lockedTarget.position - transform.position).normalized;
                targetDirection.y = 0; // Ensure the target direction is flat on the XZ plane

                // Strafing direction relative to the locked target
                Vector3 right = Quaternion.Euler(0, 90, 0) * targetDirection; // Right vector relative to the target
                Vector3 moveDirection = (right * horizontal + targetDirection * vertical).normalized;

                // Rotate towards the locked target
                RotateTowardsTarget(lockedTarget.position);

                controller.Move(moveDirection * stats.MovementSpeed * Time.deltaTime);
            }
            else
            {
                // Regular movement without target lock
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * Vector3.forward;
                controller.Move(moveDirection.normalized * stats.MovementSpeed * Time.deltaTime);
            }
        }
        else if (isTargetLocked && lockedTarget != null)
        {
            // Rotate towards the locked target even when the player is idle
            RotateTowardsTarget(lockedTarget.position);
        }
    }
    #endregion

    #region Rotation
    private void RotatePlayerWithCamera()
    {
        float mouseX = inputManager.LookInput.x * cameraSensitivity;
        float mouseY = inputManager.LookInput.y * cameraSensitivity;

        cameraTransform.Rotate(Vector3.up * mouseX);
        cameraTransform.Rotate(Vector3.right * -mouseY);
    }
    private void RotateTowardsTarget(Vector3 targetPosition)
    {
        Vector3 targetDirection = (targetPosition - transform.position).normalized;
        float targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
        float smoothAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, lockOnTurnSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
    }
    #endregion

    #region Gravity
    private void ApplyGravity()
    {
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;     // keep player grounded
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    #endregion

    #region Jump
    private void JumpHandler()
    {
        if (inputManager.IsJumpPressed)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    #endregion

    #region Death
    private void Death()
    {
        OnPlayerKilled?.Invoke();

        Destroy(gameObject);
    }
    #endregion
}
