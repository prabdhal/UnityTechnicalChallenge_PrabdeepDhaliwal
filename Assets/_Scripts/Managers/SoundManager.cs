using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private AudioClip basicAttackAudio;
    [SerializeField]
    private AudioClip basicAttackHitAudio;
    [SerializeField]
    private AudioClip specialAbilityAudio;
    [SerializeField]
    private AudioClip specialAbilityHitAudio;
    [SerializeField]
    private AudioClip bossEarthquakeAudio;

    private AudioSource audioSource;
    #endregion

    #region Init
    private void Start()
    {
        if (TryGetComponent<AudioSource>(out AudioSource audio))
        {
            audioSource = audio;
        }
    }
    #endregion

    public void PlayBasicAbilityFireSound()
    {
        if (audioSource == null) return;

        audioSource.clip = basicAttackAudio;
        audioSource.Play();
    }
    public void PlayBasicAttackHitSound(Vector3 position)
    {
        if (basicAttackHitAudio == null)
        {
            Debug.LogWarning("Hit sound is not assigned.");
            return;
        }

        // Create a temporary audio object
        GameObject audioObject = new GameObject("HitSound");
        audioObject.transform.position = position;

        // Add an AudioSource component and configure it
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = basicAttackHitAudio;
        audioSource.Play();

        // Destroy the audio object after the sound finishes playing
        Destroy(audioObject, basicAttackHitAudio.length);
    }

    public void PlaySpecialAbilityFireSound()
    {
        if (audioSource == null) return;

        audioSource.clip = specialAbilityAudio;
        audioSource.Play();
    }
    public void PlaySpecialAbilityHitSound(Vector3 position)
    {
        if (specialAbilityHitAudio == null)
        {
            Debug.LogWarning("Hit sound is not assigned.");
            return;
        }

        // Create a temporary audio object
        GameObject audioObject = new GameObject("HitSound");
        audioObject.transform.position = position;

        // Add an AudioSource component and configure it
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = specialAbilityHitAudio;
        audioSource.Play();

        // Destroy the audio object after the sound finishes playing
        Destroy(audioObject, specialAbilityHitAudio.length);
    }

    public void PlayBossEarthquakeFireSound()
    {
        if (audioSource == null) return;

        audioSource.clip = bossEarthquakeAudio;
        audioSource.Play();
    }
}
