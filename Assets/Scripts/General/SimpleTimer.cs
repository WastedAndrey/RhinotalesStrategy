using System;

public class SimpleTimer
{
    public Action Elapsed;

    public float CurrentTime = 0;
    public bool IsActive = false;

    public void Start(float time)
    {
        CurrentTime = time;
        IsActive = true;
        TimersManager.Instance.SubscribeTimer(this);
    }
    public void Update(float deltaTime)
    {
        if (IsActive)
        {
            CurrentTime -= deltaTime;
            if (CurrentTime <= 0)
            {
                Elapsed?.Invoke();
                Stop();
            }    
                
        }
    }
   
    public void Pause(bool pauseValue)
    {
        IsActive = pauseValue;
        if (pauseValue)
            TimersManager.Instance.SubscribeTimer(this);
        else
            TimersManager.Instance.UnsubscribeTimer(this);
    }
    public void Stop()
    {
        CurrentTime = 0;
        IsActive = false;
        TimersManager.Instance.UnsubscribeTimer(this);
    }

    public void Clear()
    {
        Elapsed = null;
        TimersManager.Instance.UnsubscribeTimer(this);
    }
}