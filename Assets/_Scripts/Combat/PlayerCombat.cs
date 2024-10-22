using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Ability[] abilities;
    public Ability[] Abilities => abilities;

    private Animator anim;
    private CharacterStats stats;
    private PlayerInputManager inputManager;

    private bool canAttack;
    #endregion

    #region Init & Update
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStats>();
        inputManager = GetComponent<PlayerInputManager>();

        foreach (Ability att in abilities)
        {
            att.Init(anim, stats);
        }
    }

    private void Update()
    {
        if (stats.IsDead()) return;

        HandleAbilityTicks();
        HandleCombatInputs();
    }
    #endregion

    #region Ability Handler
    private void HandleAbilityTicks()
    {
        foreach (Ability att in abilities)
        {
            att.Tick();
        }
    }
    private void HandleCombatInputs()
    {
        canAttack = !anim.GetBool("IsAttacking");

        if (!canAttack) return;

        for (int i = 0; i < abilities.Length; i++)
        {
            if (inputManager.BasicAttackPressed)
            {
                abilities[0].Execute();
            }
            else if (inputManager.SpecialAbilityPressed)
            {
                abilities[1].Execute();
            }
        }
    }
    #endregion
}
