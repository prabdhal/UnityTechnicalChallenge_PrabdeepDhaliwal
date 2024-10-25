
public class MeleeAttack : Ability
{
    public override void Execute()
    {
        if (!IsOnCooldown && HasMana())
        {
            HandleAnimation();
            StartCooldown();

            // Using animation events handler to enable/disabel attack colliders
            //EnableAttackCollider();
            //Invoke("DisableAttackCollider", 0.3f);
            //soundManager.PlayBasicAbilitySound();
        } 
    }

    public void EnableAttackCollider()
    {
        if (attackCollider == null) return;
        attackCollider.gameObject.SetActive(true);
    }
    public void DisableAttackCollider()
    {
        if (attackCollider == null) return;
        attackCollider.gameObject.SetActive(false);
    }
}
