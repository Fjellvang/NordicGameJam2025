using UnityEngine;

public class VariantAudio : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField]
    private float pitchVariance = 0.1f; // Adjust this value to change the pitch variance
    private float originalPitch;
    
    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        originalPitch = audioSource.pitch;
    }
    
    public void PlaySoundPitched()
    {
        // Randomize the pitch slightly for each footstep
        audioSource.pitch = originalPitch + Random.Range(-pitchVariance, pitchVariance);
        audioSource.Play();
    }
}