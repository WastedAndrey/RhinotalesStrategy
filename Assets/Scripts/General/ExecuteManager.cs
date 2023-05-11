
using UnityEngine;

public class ExecuteManager : MonoBehaviour
{
    private static ExecuteManager _instance;
    public static ExecuteManager Instance { get => _instance; private set => _instance = value; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddToQueue(IInitializable objectToInit, ExecuteOrderType order)
    { 
    
    }

    private void Update()
    {
        
    }
}