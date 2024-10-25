using UnityEngine;

public class CrusherDevice : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float attackCooldown;
    
    private float readyTimer;
    private bool isReady = false;
    #endregion

    #region Init & Update
    private void Start()
    {
        readyTimer = attackCooldown;
    }

    private void Update()
    {
        if (readyTimer <= 0)
        {
            readyTimer = attackCooldown;
            isReady = true;
            anim.SetBool("isReady", true);
        }
        else
        {
            readyTimer -= Time.deltaTime;
            isReady = false;
            anim.SetBool("isReady", false);
        }
    }
    #endregion

}
