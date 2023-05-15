using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimersManager : MonoBehaviour
{
    private static TimersManager _instance;
    public static TimersManager Instance { get => _instance; private set => _instance = value; }

    private HashSet<SimpleTimer> _timers = new HashSet<SimpleTimer>();
    private List<SimpleTimer> _timersRemoveQueue = new List<SimpleTimer>();

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
    }

    public void SubscribeTimer(SimpleTimer timer)
    {
        if (_timers.Contains(timer) == false)
            _timers.Add(timer);
    }

    public void UnsubscribeTimer(SimpleTimer timer)
    {
        if (_timers.Contains(timer) == true && _timersRemoveQueue.Contains(timer) == false)
            _timersRemoveQueue.Add(timer);
    }

    private void Update()
    {
        for (int i = 0; i < _timersRemoveQueue.Count; i++)
        {
            _timers.Remove(_timersRemoveQueue[i]);
        }
        _timersRemoveQueue.Clear();

        foreach (var timer in _timers)
        {
            timer.Update(Time.deltaTime);
        }
    }
}
