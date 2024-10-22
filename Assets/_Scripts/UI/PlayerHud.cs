using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : BasicHud
{
    #region Fields
    [SerializeField]
    protected TextMeshProUGUI healthText;

    [SerializeField]
    private Image manaFill;
    [SerializeField]
    private TextMeshProUGUI manaText;

    [SerializeField]
    private Image basicAttackFill;
    [SerializeField]
    private TextMeshProUGUI basicAttackCooldownText;
    [SerializeField]
    private Image specialAbilityFill;
    [SerializeField]
    private TextMeshProUGUI specialAttackCooldownText;

    private PlayerCombat combat;
    #endregion

    #region Init & Update
    private void Start()
    {
        GameManager.Instance.OnPlayerSpawn += Init;
    }
    public void Init(GameObject playerObj)
    {
        target = playerObj;
        combat = target.GetComponent<PlayerCombat>();
        stats = target.GetComponent<CharacterStats>();

        stats.OnHealthChange += UpdateHealthUI;
        stats.OnManaChange += UpdateManaUI;
    }

    private void Update()
    {
        if (combat == null) return;
        if (combat.Abilities.Length < 1) Debug.LogWarning($"Missing abilities for {combat.name}");

        UpdateAbilityCooldownUI();
    }
    #endregion

    #region UI Events
    protected override void UpdateHealthUI(float curHP, float maxHp)
    {
        healthText.text = $"{curHP}/{maxHp}";

        healthFill.fillAmount = curHP / maxHp;
    }

    private void UpdateManaUI(float curMana, float maxMana)
    {
        manaText.text = $"{curMana}/{maxMana}";

        manaFill.fillAmount = curMana / maxMana;
    }

    private void UpdateAbilityCooldownUI()
    {
        Ability basicAttack = combat.Abilities[0];
        Ability specialAttack = combat.Abilities[1];

        float basicAttackCooldown = Mathf.Ceil(basicAttack.CooldownTimer);
        float specialAttackCooldown = Mathf.Ceil(specialAttack.CooldownTimer);

        basicAttackCooldownText.text = basicAttackCooldown <= 0 ? "" : basicAttackCooldown + "";
        basicAttackFill.fillAmount = basicAttack.CooldownTimer / basicAttack.Cooldown;

        specialAttackCooldownText.text = specialAttackCooldown <= 0 ? "" : specialAttackCooldown + "";
        specialAbilityFill.fillAmount = specialAttack.CooldownTimer / specialAttack.Cooldown;
    }
    #endregion
}
