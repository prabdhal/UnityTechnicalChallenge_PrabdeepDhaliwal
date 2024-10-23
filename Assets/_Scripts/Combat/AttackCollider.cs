using System;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    #region Fields
    [SerializeField]
    protected string targetTag;

    protected CharacterStats ownerStats;
    protected Ability ability;

    public Action OnHitTarget;
    #endregion

    #region Init
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public virtual void Init(CharacterStats stats, Ability ability)
    {
        ownerStats = stats;
        this.ability = ability;
    }
    #endregion

    #region Damage 
    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            CharacterStats stats = other.GetComponent<CharacterStats>();
            
            // Get ability damage
            Vector2 abilityDmg = ability.ReturnAbitityDamage();
            Debug.Log($"ability attack damage: " + abilityDmg.x + " and magic damage: " + abilityDmg.y);
            // Apply damage to target, taking into account armor and magic resistance
            ApplyDamage(abilityDmg, stats);

            OnHitTarget?.Invoke();
        }
    }

    protected void ApplyDamage(Vector2 damages, CharacterStats stats)
    {
        float physicalDamage = Mathf.Clamp(damages.x - stats.Armor, 0, Mathf.Infinity);
        float magicDamage = Mathf.Clamp(damages.y - stats.MagicResistance, 0, Mathf.Infinity);

        float totalDamage = physicalDamage + magicDamage;
        Debug.Log($"Apply damage after defence - attack damage: " + physicalDamage + " and magic damage: " + magicDamage + " total damage: " + totalDamage);
        
        stats.InflictDamage((int)totalDamage);
    }
    #endregion
}
