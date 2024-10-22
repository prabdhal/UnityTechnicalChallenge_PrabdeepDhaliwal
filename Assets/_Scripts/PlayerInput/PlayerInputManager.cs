using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    #region Fields
    private PlayerInputActions playerInputActions;

    private InputAction movementAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction diveAction;
    private InputAction interactAction;
    private InputAction basicAttackAction;
    private InputAction specialAbilityAction;
    private InputAction pauseAction;

    public event Action OnPauseAction;

    public Vector2 MovementInput => movementAction.ReadValue<Vector2>();
    public Vector2 LookInput => lookAction.ReadValue<Vector2>();
    public bool IsJumpPressed => jumpAction.triggered;
    public bool IsDivePressed => diveAction.triggered;
    public bool IsInteractPressed => interactAction.triggered;
    public bool BasicAttackPressed => basicAttackAction.triggered;
    public bool SpecialAbilityPressed => specialAbilityAction.triggered;
    public bool IsPaused => pauseAction.triggered;
    #endregion

    #region Init & Update
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        movementAction = playerInputActions.FindAction("Move");
        lookAction = playerInputActions.FindAction("Look");
        jumpAction = playerInputActions.FindAction("Jump");
        diveAction = playerInputActions.FindAction("Dive");
        interactAction = playerInputActions.FindAction("Interact");
        basicAttackAction = playerInputActions.FindAction("BasicAttack"); ;
        specialAbilityAction = playerInputActions.FindAction("SpecialAbility");
        pauseAction = playerInputActions.FindAction("Pause");

        Debug.Log("Awake inputs");
    }
    private void Update()
    {
        if (IsPaused)
            OnPauseAction?.Invoke();
    }
    #endregion

    #region OnEnable & Disable
    private void OnEnable()
    {
        movementAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        diveAction.Enable();
        interactAction.Enable();
        basicAttackAction.Enable();
        specialAbilityAction.Enable();
        pauseAction.Enable();
        Debug.Log("Enabled inputs");
    }
    private void OnDisable()
    {
        movementAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        diveAction.Disable();
        interactAction.Disable();
        basicAttackAction.Disable();
        specialAbilityAction.Disable();
        pauseAction.Disable();
        Debug.Log("Disabled inputs");
    }
    #endregion
}
