using TMPro;
using UnityEngine;

public class ItemDescriptionUI : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private TextMeshProUGUI description;
    #endregion

    #region Init
    public void Init(string description)
    {
        this.description.text = description;
    }
    #endregion
}
