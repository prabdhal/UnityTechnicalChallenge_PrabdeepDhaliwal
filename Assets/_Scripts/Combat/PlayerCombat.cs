using UnityEngine;

public class PlayerCombat : Combat
{
    #region Fields
    private PlayerInputManager inputManager;
    #endregion

    #region Init & Update
    protected override void Start()
    {
        base.Start();
        inputManager = GetComponent<PlayerInputManager>();
    }

    protected override void Update()
    {
        base.Update();

        HandleCombatInputs();
    }
    #endregion

    #region Ability Handler
    protected override void HandleCombatInputs()
    {
        canAttack = !anim.GetBool(StringData.IsAttackingAnimatorParam);

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
