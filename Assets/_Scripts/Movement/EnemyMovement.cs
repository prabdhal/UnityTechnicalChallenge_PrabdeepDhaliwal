using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    #region Fields

    private NavMeshAgent agent;
    private CharacterStats stats;
    private Animator anim;
    #endregion

    #region Init & Update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<CharacterStats>();
        anim = GetComponent<Animator>();
    }
    #endregion
}
