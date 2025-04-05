using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioMixerGroup insideaudioMixerGroup;
    public AudioMixerGroup outsideaudioMixerGroup;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    [ContextMenu("Play Audio Outside")]
    public void PlayAudioOutside()
    {
        audioSource.outputAudioMixerGroup = outsideaudioMixerGroup;
    }

    [ContextMenu("Play Audio Inside")]
    public void PlayAudioInside()
    {
        audioSource.outputAudioMixerGroup = insideaudioMixerGroup;
    }
}