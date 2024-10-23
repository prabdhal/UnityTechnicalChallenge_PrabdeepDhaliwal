using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Fields
    [Header("Player Settings")]
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Camera Settings")]
    public Transform cameraTransform;
    public float cameraSensitivity = 3f;

    private CharacterController controller;
    private CharacterStats stats;
    private PlayerCombat combat;
    private PlayerInputManager inputManager;
    private Animator anim;
    private Vector3 playerVelocity;
    private bool isGrounded;

    private float turnSmoothVelocity;
    public float turnSpeed;

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

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (stats.IsDead() && !isDead)
        {
            isDead = true;
            Death();
            return;
        }

        Movement();
        RotatePlayerWithCamera();
        ApplyGravity();

        JumpHandler();
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
            // Calculate the target angle based on camera direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            // Instantly turn towards the target angle with some smoothing 
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);

            // Apply the calculated angle for rotation
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move the player
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * stats.MovementSpeed * Time.deltaTime);
        }
    }
    #endregion

    #region Rotation
    private void RotatePlayerWithCamera()
    {
        // Camera rotation
        float mouseX = inputManager.LookInput.x * cameraSensitivity;
        float mouseY = inputManager.LookInput.y * cameraSensitivity;

        // Apply camera rotation to the camera rig (parent object of the camera)
        cameraTransform.Rotate(Vector3.up * mouseX);
        cameraTransform.Rotate(Vector3.right * -mouseY);
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
