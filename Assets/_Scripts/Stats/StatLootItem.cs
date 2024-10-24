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
    #region Fields
    public StatType statType;
    public float statBuffAmount;
    
    private ItemMessageHud messagesHud;
    #endregion

    #region Init
    public void Init(ItemMessageHud messageHud)
    {
        this.messagesHud = messageHud;
    }
    #endregion

    #region Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringData.PlayerTag))
        {
            if (!other.TryGetComponent(out CharacterStats playerStats))
                return;

            if (playerStats != null)
            {
                string message = $"+{statBuffAmount} ";
                switch (statType)
                {
                    case StatType.Health:
                        playerStats.HealHealth(statBuffAmount);
                        messagesHud.SpawnMessage(message + " Health");
                        break;
                    case StatType.Mana:
                        playerStats.HealMana(statBuffAmount);
                        messagesHud.SpawnMessage(message + " Mana");
                        break;
                    case StatType.AttackDamage:
                        playerStats.AddAttackDamage(statBuffAmount);
                        messagesHud.SpawnMessage(message + " Attack Damage");
                        break;
                    case StatType.MagicDamage:
                        playerStats.AddMagicDamage(statBuffAmount);
                        messagesHud.SpawnMessage(message + " Magic Damage");
                        break;
                    case StatType.Armour:
                        playerStats.AddArmor(statBuffAmount);
                        messagesHud.SpawnMessage(message + " Armor");
                        break;
                    case StatType.MagicResistance:
                        playerStats.AddMagicResistance(statBuffAmount);
                        messagesHud.SpawnMessage(message + " Magic Resist");
                        break;
                    case StatType.MovementSpeed:
                        playerStats.AddMovementSpeed(statBuffAmount);
                        messagesHud.SpawnMessage(message + " MovementSpeed");
                        break;
                    case StatType.AttackSpeed:
                        playerStats.AddAttackSpeed(statBuffAmount);
                        messagesHud.SpawnMessage(message + " Attack Speed");
                        break;
                    default:
                        Debug.LogWarning($"{statType.ToString()} does not exist");
                        break;
                }

                Destroy(gameObject);
            }
        }
    }
    #endregion
}


