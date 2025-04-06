using UnityEngine;
using UnityEngine.Playables;

public class SkipTimeline : MonoBehaviour
{
    
    PlayableDirector playableDirector;
    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playableDirector.state == PlayState.Playing)
        {
            playableDirector.time = playableDirector.duration;
            playableDirector.Evaluate();
            playableDirector.Stop();
        }
    }
}