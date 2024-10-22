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
    private InputAction basicAttack;
    private InputAction specialAbility;

    public Vector2 MovementInput => movementAction.ReadValue<Vector2>();
    public Vector2 LookInput => lookAction.ReadValue<Vector2>();
    public bool IsJumpPressed => jumpAction.triggered;
    public bool IsDivePressed => diveAction.triggered;
    public bool IsInteractPressed => interactAction.triggered;
    public bool BasicAttackPressed => basicAttack.triggered;
    public bool SpecialAbilityPressed => specialAbility.triggered;
    #endregion

    #region Init
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        movementAction = playerInputActions.FindAction("Move");
        lookAction = playerInputActions.FindAction("Look");
        jumpAction = playerInputActions.FindAction("Jump");
        diveAction = playerInputActions.FindAction("Dive");
        interactAction = playerInputActions.FindAction("Interact");
        basicAttack = playerInputActions.FindAction("BasicAttack"); ;
        specialAbility = playerInputActions.FindAction("SpecialAbility");

        Debug.Log("Awake inputs");
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
        basicAttack.Enable();
        specialAbility.Enable();
        Debug.Log("Enabled inputs");
    }
    private void OnDisable()
    {
        movementAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        diveAction.Disable();
        interactAction.Disable();
        basicAttack.Disable();
        specialAbility.Disable();
        Debug.Log("Disabled inputs");
    }
    #endregion
}
