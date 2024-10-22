public class BasicAttack : Ability
{
    public override void Execute()
    {
        if (!IsOnCooldown && HasMana())
        {
            HandleAnimation();
            StartCooldown();

            EnableAttackCollider();
            Invoke("DisableAttackCollider", 1f);
        }
    }

    private void EnableAttackCollider()
    {
        attackCollider.gameObject.SetActive(true);
    }
    private void DisableAttackCollider()
    {
        attackCollider.gameObject.SetActive(false);
    }
}
