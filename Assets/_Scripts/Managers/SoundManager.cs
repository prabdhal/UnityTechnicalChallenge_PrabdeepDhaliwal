using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private AudioClip basicAttackAudio;
    [SerializeField]
    private AudioClip specialAbilityAudio;

    private AudioSource audioSource;
    #endregion

    #region Init
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    #endregion

    public void PlayBasicAbilitySound()
    {
        audioSource.clip = basicAttackAudio;
        audioSource.Play();
    }

    public void PlaySpecialAbilitySound()
    {
        audioSource.clip = specialAbilityAudio;
        audioSource.Play();
    }
}
