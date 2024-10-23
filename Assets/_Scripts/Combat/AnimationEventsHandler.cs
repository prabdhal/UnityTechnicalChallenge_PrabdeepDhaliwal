using UnityEngine;
using UnityEngine.Events;

public class AnimationEventsHandler : MonoBehaviour
{
    #region Fields
    // Events ONLY for ability one
    [SerializeField, Tooltip("Ability one melee events ONLY")]
    private UnityEvent OnEnableColliderAbilityOne;
    [SerializeField, Tooltip("Ability one melee events ONLY")]
    private UnityEvent OnDisableColliderAbilityOne;
    [SerializeField, Tooltip("Ability one projectile events ONLY")]
    private UnityEvent OnProjectileFireAbilityOne;

    // Events ONLY for ability TWO
    [SerializeField, Tooltip("Ability two melee events ONLY")]
    private UnityEvent OnEnableColliderAbilityTwo;
    [SerializeField, Tooltip("Ability two melee events ONLY")]
    private UnityEvent OnDisableColliderAbilityTwo;
    [SerializeField, Tooltip("Ability two projectile events ONLY")]
    private UnityEvent OnProjectileFireAbilityTwo;


    // Events ONLY for ability THREE
    [SerializeField, Tooltip("Ability two melee events ONLY")]
    private UnityEvent OnEnableColliderAbilityThree;
    [SerializeField, Tooltip("Ability two melee events ONLY")]
    private UnityEvent OnDisableColliderAbilityThree;
    [SerializeField, Tooltip("Ability two projectile events ONLY")]
    private UnityEvent OnProjectileFireAbilityThree;
    #endregion

    #region Events
    public void AbilityOne_OnEnableCollider()
    {
        OnEnableColliderAbilityOne?.Invoke();
    }
    public void AbilityOne_OnDisableCollider()
    {
        OnDisableColliderAbilityOne?.Invoke();
    }
    public void AbilityOne_OnProjectileFire()
    {
        OnProjectileFireAbilityOne?.Invoke();
    }

    public void AbilityTwo_OnEnableCollider()
    {
        OnEnableColliderAbilityTwo?.Invoke();
    }
    public void AbilityTwo_OnDisableCollider()
    {
        OnDisableColliderAbilityTwo?.Invoke();
    }
    public void AbilityTwo_OnProjectileFire()
    {
        OnProjectileFireAbilityTwo?.Invoke();
    }


    public void AbilityThree_OnEnableCollider()
    {
        OnEnableColliderAbilityThree?.Invoke();
    }
    public void AbilityThree_OnDisableCollider()
    {
        OnDisableColliderAbilityThree?.Invoke();
    }
    public void AbilityThree_OnProjectileFire()
    {
        OnProjectileFireAbilityThree?.Invoke();
    }
    #endregion
}
