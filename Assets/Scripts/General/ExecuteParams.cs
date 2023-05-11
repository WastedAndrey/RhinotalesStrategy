using UnityEngine;

[System.Serializable]
public enum ExecuteOrderType
{ 
    BattlefieldEntityLink = 200,
    UnitEntityLink = 201
}


[System.Serializable]
public class ExecuteParams
{
    [SerializeField]
    private ExecuteOrderType _executeOrder;
    public ExecuteOrderType ExecuteOrder { get => _executeOrder; }

    public void Init(IInitializable objectToInit)
    {
        ExecuteManager.Instance.AddToQueue(objectToInit, _executeOrder);
    }
}