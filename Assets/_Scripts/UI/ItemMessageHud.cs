using UnityEngine;

public class ItemMessageHud : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private GameObject itemDescriptionPrefab;
    [SerializeField]
    private Transform parent;
    #endregion

    #region Init
    public void SpawnMessage(string message)
    {
        GameObject go = Instantiate(itemDescriptionPrefab, parent);
        go.GetComponent<ItemDescriptionUI>().Init(message);
    }
    #endregion
}
