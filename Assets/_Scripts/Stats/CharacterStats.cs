using System;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    #region Fields
    private int currentHealth;
    public int CurrentHealth => currentHealth;
    [SerializeField]
    private int maxHealth;
    public int MaxHealth => maxHealth;

    private int currentMana;
    public int Mana => currentMana;
    [SerializeField]
    private int maxMana;
    public int MaxMana => maxMana;

    [SerializeField]
    private int attackDamage;
    public int AttackDamage => attackDamage;

    [SerializeField]
    private int magicDamage;
    public int MagicDamage => magicDamage;

    [SerializeField]
    private int armor;
    public int Armor => armor;

    [SerializeField]
    private int magicResistance;
    public int MagicResistance => magicResistance;

    [SerializeField]
    private float movementSpeed;
    public float MovementSpeed => movementSpeed;

    [SerializeField, Range(0.3f, 3f)]
    private float attackSpeed;
    public float AttackSpeed => attackSpeed;

    // Events
    public Action<int, int> OnHealthChange;
    public Action<int, int> OnManaChange;
    #endregion

    #region Init
    private void Awake()
    {
        currentHealth = maxHealth;
        currentMana = MaxMana;
    }
    #endregion

    #region Health
    public void InflictDamage(int damage)
    {
        currentHealth -= damage;

        OnHealthChange?.Invoke(currentHealth, maxHealth);
    }
    public void HealHealth(int damage)
    {
        currentHealth += damage;

        OnHealthChange?.Invoke(currentHealth, maxHealth);
    }
    #endregion

    #region Mana
    public bool HasMana(int mana)
    {
        if (currentMana < mana)
            return false;

        ConsumeMana(mana);
        return true;
    }
    private void ConsumeMana(int mana)
    {
        currentMana -= mana;

        OnManaChange?.Invoke(currentMana, maxMana);
    }
    #endregion

    #region Death
    public bool IsDead()
    {
        return currentHealth <= 0;
    }
    #endregion
}
