using UnityEngine;

public abstract class Combat : MonoBehaviour
{
    #region Fields
    [SerializeField]
    protected Ability[] abilities;
    public Ability[] Abilities => abilities;

    protected Animator anim;
    protected CharacterStats stats;
    protected SoundManager soundManager;

    protected bool canAttack;
    #endregion

    #region Init & Update
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStats>();
        soundManager = GetComponent<SoundManager>();

        foreach (Ability att in abilities)
        {
            att.Init(anim, stats, soundManager);
        }
    }

    protected virtual void Update()
    {
        if (stats.IsDead()) return;

        canAttack = !anim.GetBool(StringData.IsAttackingAnimatorParam);
        HandleAbilityTicks();
    }
    #endregion

    #region Ability Handler
    private void HandleAbilityTicks()
    {
        foreach (Ability att in abilities)
        {
            att.Tick();
        }
    }
    protected abstract void HandleCombatInputs();
    #endregion
}
