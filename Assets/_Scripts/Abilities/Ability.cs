using UnityEngine;

public struct AbiltyStats
{
    public int flatAttackDamage;
    [Range(0, 1)]
    public float baseAttackDamageScaling;

    public int flatMagicDamage;
    [Range(0, 1)]
    public float baseMagicDamageScaling;
}
public abstract class Ability : MonoBehaviour
{
    #region Fields
    [SerializeField]
    protected AttackCollider attackCollider;
    [SerializeField]
    protected string animationName;

    [Header("Ability Stats")]
    [SerializeField]
    protected AbiltyStats stats;
    protected float cooldownTimer;
    public float CooldownTimer => cooldownTimer;
    [SerializeField]
    protected float abilityCooldown;
    public float Cooldown => abilityCooldown;
    [SerializeField]
    protected int manaCost;

    protected bool isActive;
    public bool IsActive => isActive;
    public bool IsOnCooldown => cooldownTimer > 0;

    protected CharacterStats ownerStats;
    protected Animator anim;
    #endregion

    #region Init & Tick
    public void Init(Animator anim, CharacterStats stats)
    {
        this.anim = anim;
        ownerStats = stats;
        if (attackCollider != null)
        {
            attackCollider.Init(stats, this);
        }
    }
    public virtual void Tick()
    {
        CooldownHandler();
    }
    #endregion

    #region Execute
    public abstract void Execute();
    #endregion

    #region Calculate Damage
    public Vector2 ReturnAbitityDamage()
    {
        // x = physical damage & y = magic damage
        Vector2 damages = new Vector2();
        damages.x = stats.flatAttackDamage + (int)(ownerStats.AttackDamage * stats.baseAttackDamageScaling);
        damages.y = stats.flatMagicDamage + (int)(ownerStats.MagicDamage * stats.baseMagicDamageScaling);

        return damages;
    }
    #endregion

    #region Animation
    protected void HandleAnimation()
    {
        float attackSpeed = Mathf.Clamp(ownerStats.AttackSpeed, 0.3f, 3f);
        anim.SetFloat("attackSpeed", attackSpeed);

        anim.SetTrigger(animationName);
    }
    #endregion

    #region Cooldown Handler
    /// <summary>
    /// Set true, if ability cooldown should scale directly off attack speed.
    /// </summary>
    /// <param name="attackSpeedScaling"></param>
    protected void StartCooldown(bool attackSpeedScaling = false)
    {
        if (attackSpeedScaling)
        {
            cooldownTimer = GetModifiedCooldown();
        }
        else
        {
            cooldownTimer = abilityCooldown;
        }
    }
    protected void CooldownHandler()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
    protected float GetModifiedCooldown()
    {
        // Get the attack speed from the character's stats, clamping it within the range of 0.3 to 3 attacks per second
        float attackSpeed = Mathf.Clamp(ownerStats.AttackSpeed, 0.3f, 3f);

        float modifiedCooldown = abilityCooldown / attackSpeed;
        return modifiedCooldown;
    }
    #endregion

    #region Cost Handler
    protected bool HasMana()
    {
        if (ownerStats.HasMana(manaCost))
            return true;

        return false;
    }
    #endregion
}
