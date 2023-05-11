using UnityEngine;

public class EntityLink : MonoBehaviour
{
    [SerializeField]
    private EntityFactoryBase _entityFactory;

    private Contexts _contexts;
    private GameEntity _entity;

    public GameEntity Entity { get => _entity; }

    public void Init()
    {
        _contexts = Contexts.sharedInstance;
    
        _entity = _entityFactory.CreateEntity(this.gameObject);
    }
}