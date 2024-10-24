using UnityEngine;


public enum StatType
{
    Health,
    Mana,
    AttackDamage,
    MagicDamage,
    Armour,
    MagicResistance,
    MovementSpeed,
    AttackSpeed,
}
public class StatLootItem : MonoBehaviour
{
    public StatType statType;
    public float statBuffAmount; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringData.PlayerTag))
        {
            if (!other.TryGetComponent(out CharacterStats playerStats))
                return;

            if (playerStats != null)
            {
                switch (statType)
                {
                    case StatType.Health:
                        playerStats.HealHealth(statBuffAmount);
                        break;
                    case StatType.Mana:
                        playerStats.HealMana(statBuffAmount);
                        break;
                    case StatType.AttackDamage:
                        playerStats.AddAttackDamage(statBuffAmount);
                        break;
                    case StatType.MagicDamage:
                        playerStats.AddMagicDamage(statBuffAmount);
                        break;
                    case StatType.Armour:
                        playerStats.AddArmor(statBuffAmount);
                        break;
                    case StatType.MagicResistance:
                        playerStats.AddMagicResistance(statBuffAmount);
                        break;
                    case StatType.MovementSpeed:
                        playerStats.AddMovementSpeed(statBuffAmount);
                        break;
                    case StatType.AttackSpeed:
                        playerStats.AddAttackSpeed(statBuffAmount);
                        break;
                    default:
                        Debug.LogWarning($"{statType.ToString()} does not exist");
                        break;
                }

                Destroy(gameObject);
            }
        }
    }
}


