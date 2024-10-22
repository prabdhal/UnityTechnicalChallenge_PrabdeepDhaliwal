using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasicHud : MonoBehaviour
{
    #region Fields
    [SerializeField]
    protected GameObject target;

    [SerializeField]
    protected Image healthFill;

    private Camera cam;
    protected CharacterStats stats;
    #endregion

    #region Init & Update
    private void Start()
    {
        cam = Camera.main;
        stats = target.GetComponent<CharacterStats>();

        stats.OnHealthChange += UpdateHealthUI;
    }

    private void Update()
    {
        LookAtCamera();
    }
    #endregion

    #region UI Events
    protected virtual void UpdateHealthUI(float curHP, float maxHp)
    {
        healthFill.fillAmount = curHP / maxHp;
    }
    private void LookAtCamera()
    {
        if (cam == null || Time.timeScale == 0) return;

        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
                                     cam.transform.rotation * Vector3.up);
    }
    #endregion
}
