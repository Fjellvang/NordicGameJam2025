using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Collider))]
public class MuffledAudioPlayer : MonoBehaviour
{
    public AudioMixerGroup insideaudioMixerGroup;
    public AudioMixerGroup outsideaudioMixerGroup;
    
    [SerializeField]
    private float PlayForSeconds = 5f;
    private AudioSource audioSource;
    [SerializeField]
    DeliveryScript deliveryScript;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = outsideaudioMixerGroup;
        if (deliveryScript)
        {
            deliveryScript.OnDeliveryCompleted += Play;
        }
        else
        {
            Debug.LogWarning("DeliveryScript is not assigned. Audio will not play.");
        }
    }

    [ContextMenu("Play Audio")]
    public void Play()
    {
        StartCoroutine(PlayAudioForSeconds());
    }
    
    IEnumerator PlayAudioForSeconds()
    {
        audioSource.Play();
        yield return new WaitForSeconds(PlayForSeconds);
        audioSource.Stop();
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayAudioInside();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayAudioOutside();
        }
    }
}