public class EnemyCombat : Combat
{
    #region Fields

    #endregion

    #region Init & Update
    protected override void Start()
    {
        base.Start();
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
            abilities[0].Execute();
            abilities[1].Execute();
        }
    }
    #endregion
}
