using UnityEngine;

public class ObstacleAttackCollider : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private float damage;
    #endregion

    #region OnTrigger
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringData.PlayerTag))
        {
            CharacterStats stats = other.GetComponent<CharacterStats>();

            // Apply damage to target, taking into account armor and magic resistance
            ApplyDamage(damage, stats);
        }
    }
    #endregion

    #region Damage
    protected void ApplyDamage(float damage, CharacterStats stats)
    {
        stats.InflictDamage((int)damage);
    }
    #endregion
}
