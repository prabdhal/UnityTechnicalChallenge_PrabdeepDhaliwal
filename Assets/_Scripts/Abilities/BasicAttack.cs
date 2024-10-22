public class BasicAttack : Ability
{
    public override void Execute()
    {
        if (!IsOnCooldown && HasMana())
        {
            HandleAnimation();
            StartCooldown();

            EnableAttackCollider();
            Invoke("DisableAttackCollider", 0.5f);
        }
    }

    private void EnableAttackCollider()
    {
        if (attackCollider == null) return;
        attackCollider.gameObject.SetActive(true);
    }
    private void DisableAttackCollider()
    {
        if (attackCollider == null) return;
        attackCollider.gameObject.SetActive(false);
    }
}
