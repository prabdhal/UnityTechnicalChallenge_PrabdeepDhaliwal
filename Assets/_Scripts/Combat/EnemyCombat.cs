using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Ability[] attacks;

    private Animator anim;
    private CharacterStats stats;
    #endregion

    #region Init & Update
    private void Start()
    {
        anim = GetComponent<Animator>();

        foreach (Ability att in attacks) 
        {
            att.Init(anim, stats);
        }
    }

    private void Update()
    {
        foreach (Ability att in attacks)
        {
            att.Tick();
        }
    }
    #endregion
}
