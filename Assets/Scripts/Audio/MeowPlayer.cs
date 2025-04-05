using UnityEngine;

public class MeowPlayer : MonoBehaviour
{
    public AudioSource meowAudioSource;
    public float meowPitchVariance = 0.1f;
    private float originalPitch; 
    
    public PlayerInteractionCheck playerInteraction;
    
    public void PlayMeow()
    {
        if(meowAudioSource.isPlaying) return;
        
        meowAudioSource.pitch = originalPitch + Random.Range(-meowPitchVariance, meowPitchVariance);
        meowAudioSource.Play();
    }
    
    void Awake()
    {
        originalPitch = meowAudioSource.pitch;
        playerInteraction.OnInteractFailed += PlayMeow;
    }
}