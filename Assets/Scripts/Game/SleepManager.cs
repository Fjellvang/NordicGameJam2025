using UnityEngine;

public class SleepManager : MonoBehaviour
{
    public event System.Action OnSleep;

    public float SleepTimer = 5f;
    
    // Update is called once per frame
    void Update()
    {
        SleepTimer -= Time.deltaTime;
        if (SleepTimer <= 0)
        {
            OnSleep?.Invoke(); // Trigger the sleep event
        }
    }
}
