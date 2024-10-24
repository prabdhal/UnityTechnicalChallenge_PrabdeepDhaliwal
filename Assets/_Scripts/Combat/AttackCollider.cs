using System;
using System.Collections;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    #region Fields
    [SerializeField]
    protected string targetTag;
    [SerializeField, Tooltip("Applies continuous damage overtime")]
    private bool damageOnStay = false;

    protected CharacterStats ownerStats;
    protected Ability ability;
    private Coroutine damageCoroutine;

    public Action OnHitTarget;
    #endregion

    #region Init
    private void Start()
    {
        // Keep attack collider on if attack is OnTriggerStay based
        if (!damageOnStay)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
    public virtual void Init(CharacterStats stats, Ability ability)
    {
        ownerStats = stats;
        this.ability = ability;
    }
    #endregion

    #region OnTrigger
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            CharacterStats stats = other.GetComponent<CharacterStats>();

            // Get ability damage
            Vector2 abilityDmg = ability.ReturnAbitityDamage();

            // Apply damage to target, taking into account armor and magic resistance
            ApplyDamage(abilityDmg, stats);

            OnHitTarget?.Invoke();

            // Start damage over time if applicable
            if (damageOnStay)
            {
                if (damageCoroutine == null)
                {
                    damageCoroutine = StartCoroutine(ApplyDamageOverTime(stats, abilityDmg));
                }
            }
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // Stop the coroutine when the target exits
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null; // Reset the coroutine reference
            }
        }
    }
    #endregion

    #region Damage
    protected void ApplyDamage(Vector2 damages, CharacterStats stats)
    {
        float physicalDamage = Mathf.Clamp(damages.x - stats.Armour, 0, Mathf.Infinity);
        float magicDamage = Mathf.Clamp(damages.y - stats.MagicResistance, 0, Mathf.Infinity);

        float totalDamage = physicalDamage + magicDamage;
        
        stats.InflictDamage((int)totalDamage);
    }

    private IEnumerator ApplyDamageOverTime(CharacterStats stats, Vector2 damages)
    {
        while (true)
        {
            // Apply damage to target every second
            ApplyDamage(damages, stats);
            yield return new WaitForSeconds(1f);
        }
    }
    #endregion
}
