using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    #region Fields
    private float currentHealth;
    public float CurrentHealth => currentHealth;
    [SerializeField]
    private float maxHealth;
    public float MaxHealth => maxHealth;

    private float currentMana;
    public float Mana => currentMana;
    [SerializeField]
    private float maxMana;
    public float MaxMana => maxMana;

    [SerializeField]
    private float attackDamage;
    public float AttackDamage => attackDamage;

    [SerializeField]
    private float magicDamage;
    public float MagicDamage => magicDamage;

    [SerializeField]
    private float armour;
    public float Armour => armour;

    [SerializeField]
    private float magicResistance;
    public float MagicResistance => magicResistance;

    [SerializeField]
    private float movementSpeed;
    public float MovementSpeed => movementSpeed;

    [SerializeField, Range(0.3f, 3f)]
    private float attackSpeed;
    public float AttackSpeed => attackSpeed;

    // Events
    public Action<float, float> OnHealthChange;
    public Action<float, float> OnManaChange;
    #endregion

    #region Init
    private void Awake()
    {
        currentHealth = maxHealth;
        currentMana = MaxMana;
    }
    private void Start()
    {
        OnHealthChange?.Invoke(currentHealth, maxHealth);
        OnManaChange?.Invoke(currentMana, maxMana);
    }
    #endregion

    #region Health
    public void InflictDamage(float damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChange?.Invoke(currentHealth, maxHealth);
    }
    public void HealHealth(float damage)
    {
        currentHealth += damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChange?.Invoke(currentHealth, maxHealth);
    }
    #endregion

    #region Mana
    public bool HasMana(float mana)
    {
        if (currentMana < mana)
            return false;

        ConsumeMana(mana);
        return true;
    }
    private void ConsumeMana(float mana)
    {
        currentMana -= mana;

        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        OnManaChange?.Invoke(currentMana, maxMana);
    }
    public void HealMana(float mana)
    {
        currentMana += mana;

        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        OnManaChange?.Invoke(currentMana, maxMana);
    }
    #endregion

    #region Other Stat Updates
    public void AddAttackDamage(float amount)
    {
        attackDamage += amount;
        attackDamage = Mathf.Clamp(attackDamage, 0, 100);
    }
    public void AddMagicDamage(float amount)
    {
        magicDamage += amount;
        magicDamage = Mathf.Clamp(magicDamage, 0, 100);
    }
    public void AddArmor(float amount)
    {
        armour += amount;
        armour = Mathf.Clamp(armour, 0, 100);
    }
    public void AddMagicResistance(float amount)
    {
        magicResistance += amount;
        magicResistance = Mathf.Clamp(magicResistance, 0, 100);
    }
    public void AddMovementSpeed(float amount)
    {
        movementSpeed += amount;
        movementSpeed = Mathf.Clamp(movementSpeed, 0, 10);
    }
    public void AddAttackSpeed(float amount)
    {
        attackSpeed += amount;
        attackSpeed = Mathf.Clamp(attackSpeed, 0, 5);
    }
    #endregion

    #region Death
    public bool IsDead()
    {
        return currentHealth <= 0;
    }
    #endregion
}
