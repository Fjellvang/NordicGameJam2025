using DotsShooter;
using UnityEngine;
using UnityEngine.Events;

public class SleepManager : Singleton<SleepManager>
{
    public UnityEvent OnSleep;
    [SerializeField]
    private float TimeToSleep = 60f; // Time in seconds before the sleep event is triggered
    private float _sleepTimer;
    private bool isGameOver = false;
    
    public float TimeToSleepNormalized => _sleepTimer / TimeToSleep;
    
    void Start()
    {
        _sleepTimer = TimeToSleep;
    }
    
    // Update is called once per frame
    void Update()
    {
        _sleepTimer -= Time.deltaTime;
        if (_sleepTimer <= 0 && !isGameOver)
        {
            isGameOver = true;
            OnSleep?.Invoke(); // Trigger the sleep event
        }
    }
    
    public void AddSleepTime(float time)
    {
        _sleepTimer += time;
    }
}
