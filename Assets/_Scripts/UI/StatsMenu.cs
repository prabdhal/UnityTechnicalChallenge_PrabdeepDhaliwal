using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsMenu : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private GameObject statsDisplayPrefab;
    [SerializeField]
    private Transform parent;

    private CharacterStats stats;
    private List<GameObject> statsDisplays = new List<GameObject>();
    #endregion

    #region Init
    public void Setup(GameObject player)
    {
        stats = player.GetComponent<CharacterStats>();
    }
    private void OnEnable()
    {
        if (stats == null)
        {
            Debug.LogWarning("Players character stats not found.");
            return;
        }

        InstantiateStatsDisplay("Health: ", stats.CurrentHealth + "", StatType.Health);
        InstantiateStatsDisplay("Mana: ", stats.CurrentMana + "", StatType.Mana);
        InstantiateStatsDisplay("Attack Damage: ", stats.AttackDamage + "", StatType.AttackDamage);
        InstantiateStatsDisplay("Magic Damage: ", stats.MagicDamage + "", StatType.MagicDamage);
        InstantiateStatsDisplay("Armour: ", stats.Armour + "", StatType.Armour);
        InstantiateStatsDisplay("Magic Resist: ", stats.MagicResistance + "", StatType.MagicResistance);
        InstantiateStatsDisplay("Movement Speed: ", stats.MovementSpeed + "", StatType.MovementSpeed);
        InstantiateStatsDisplay("Attack Speed: ", stats.AttackSpeed + "", StatType.AttackSpeed);
    }
    private void OnDisable()
    {
        if (statsDisplays.Count == 0) return;
        
        for (int i = 0; i < statsDisplays.Count; i++)
        {
            Destroy(statsDisplays[i]);
        }
        statsDisplays.Clear();
    }
    #endregion

    #region Instantiate UI
    private void InstantiateStatsDisplay(string statName, string statValue, StatType statType)
    {
        GameObject go = Instantiate(statsDisplayPrefab, parent);
        StatsDisplay stat = go.GetComponent<StatsDisplay>();
        stat.Init(statName, statValue, statType);
        statsDisplays.Add(go);
    }
    #endregion
}
