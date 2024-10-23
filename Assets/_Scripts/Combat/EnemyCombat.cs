using UnityEngine;

public class EnemyCombat : Combat
{
    #region Fields
    private BaseEnemyController controller;
    private GameObject player;
    #endregion

    #region Init & Update
    protected override void Start()
    {
        base.Start();
        controller = GetComponent<BaseEnemyController>();
        player = GameObject.FindGameObjectWithTag(StringData.PlayerTag);
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
        if (player == null) return;

        float playerDistance = Vector3.Distance(transform.position, player.transform.position);

        if (!canAttack) return;
        if (abilities.Length == 0)
        {
            Debug.LogError("Need to assign abilities");
            return;
        }

        for (int i = 0; i < abilities.Length; i++)
        {
            if (controller.CanAttack)
            {
                // use basic attack if within range
                if (playerDistance < abilities[i].AbilityStats.range)
                {
                    abilities[i].Execute();
                }
            }
        }
    }
    #endregion
}
