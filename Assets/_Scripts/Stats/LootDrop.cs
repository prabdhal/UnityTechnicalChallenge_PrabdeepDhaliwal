using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    #region Fields
    [Header("Loot Drop Settings")]
    [SerializeField]
    private List<LootItem> lootTable = new List<LootItem>();

    private int totalWeight;
    #endregion

    #region Init
    private void Start()
    {
        CalculateTotalWeight();
    }
    #endregion

    #region Loot Calculation
    private void CalculateTotalWeight()
    {
        // Calculate the total weight of all items in the loot table
        totalWeight = 0;
        foreach (LootItem item in lootTable)
        {
            totalWeight += item.weight;
        }
    }

    private LootItem GetRandomLootItem()
    {
        // Randomly select an item based on its weight
        int randomValue = Random.Range(0, totalWeight);
        int currentWeight = 0;

        foreach (LootItem item in lootTable)
        {
            currentWeight += item.weight;
            if (randomValue < currentWeight)
            {
                return item;
            }
        }
        return null; // In case something goes wrong, return null
    }
    #endregion

    #region Loot Drop
    public void DropLoot(BaseEnemyController enemy)
    {
        LootItem selectedItem = GetRandomLootItem();
        if (selectedItem != null && selectedItem.itemPrefab != null)
        {
            // Instantiate the selected item's prefab at the drop position
            Instantiate(selectedItem.itemPrefab, enemy.transform.position, Quaternion.identity);
        }
    }
    #endregion
}