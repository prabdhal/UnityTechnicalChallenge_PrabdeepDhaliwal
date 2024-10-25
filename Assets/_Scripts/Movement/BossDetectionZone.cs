using System;
using UnityEngine;

public class BossDetectionZone : MonoBehaviour
{
    public Action<GameObject> OnBossZoneEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringData.PlayerTag))
        {
            OnBossZoneEnter?.Invoke(other.gameObject);
        }
    }
}
