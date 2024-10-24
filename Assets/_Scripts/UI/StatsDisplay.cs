using TMPro;
using UnityEngine;

public class StatsDisplay : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private TextMeshProUGUI statName;
    [SerializeField]
    private TextMeshProUGUI statValue;

    private StatType statType;
    public StatType Type => statType;
    #endregion

    #region Init
    public void Init(string name, string value, StatType type)
    {
        statName.text = name;
        statValue.text = value;
        statType = type;
    }
    #endregion
}
