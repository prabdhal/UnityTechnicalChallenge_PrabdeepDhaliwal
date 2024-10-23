using UnityEngine;

public class EnemyCombat : Combat
{
    #region Fields
    private EnemyController controller;
    private GameObject player;
    #endregion

    #region Init & Update
    protected override void Start()
    {
        base.Start();
        controller = GetComponent<EnemyController>();
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

        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities.Length < 2)
            {
                Debug.LogWarning("Need minimum of two abilities");
                return;
            }
            
            if (controller.CanAttack)
            {
                // use basic attack if within range
                if (playerDistance < abilities[0].AbilityStats.range)
                {
                    abilities[0].Execute();
                }
                // Use special attack if within range
                if (playerDistance < abilities[1].AbilityStats.range)
                {
                    abilities[1].Execute();
                }
            }
        }
    }
    #endregion
}
