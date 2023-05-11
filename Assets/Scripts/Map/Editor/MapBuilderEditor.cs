using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapBuilder))]
public class MapBuilderEditor : Editor
{
    private MapBuilder _mapBuilder;
    private Tool _previousTool;

    private SerializedProperty _prefabUnitWeak;
    private SerializedProperty _prefabUnitStrong;
    private SerializedProperty _prefabWall;

    private SerializedProperty _map;
    private SerializedProperty _mapCollider;
    private SerializedProperty _isEditing;
    private SerializedProperty _unitType;
    private SerializedProperty _playerTeam;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(_prefabUnitWeak);
        EditorGUILayout.PropertyField(_prefabUnitStrong);
        EditorGUILayout.PropertyField(_prefabWall);

        EditorGUILayout.PropertyField(_map);
        EditorGUILayout.PropertyField(_mapCollider);
      
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(_isEditing);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_mapBuilder, "Change Is Editing");
            OnIsEditingChanged(_isEditing.boolValue);
        }

        EditorGUILayout.PropertyField(_unitType);
        EditorGUILayout.PropertyField(_playerTeam);
        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void OnSceneGUI()
    {
        LockSelectionToMapBuilder();

        MapBuilder mapBuilder = target as MapBuilder;
        MapCollider mapCollider = mapBuilder.MapCollider;

        if (mapCollider == null)
            return;

        Handles.color = Color.red;
        Handles.DrawWireCube(mapCollider.transform.position + mapCollider.Position, mapCollider.Scale);

        if (_mapBuilder.IsEditing == false)
            return;

        if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            var hits = Physics.RaycastAll(ray);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject == mapCollider.gameObject)
                {
                    mapCollider.RegisterHit(hits[i]);
                    break;
                }
            }
        }
    }

    

    private void OnEnable()
    {
        _mapBuilder = (MapBuilder)target;

        _prefabUnitWeak = serializedObject.FindProperty("_prefabUnitWeak");
        _prefabUnitStrong = serializedObject.FindProperty("_prefabUnitStrong");
        _prefabWall = serializedObject.FindProperty("_prefabWall");

        _map = serializedObject.FindProperty("_map");
        _mapCollider = serializedObject.FindProperty("_mapCollider");
        _isEditing = serializedObject.FindProperty("_isEditing");
        _unitType = serializedObject.FindProperty("_unitType");
        _playerTeam = serializedObject.FindProperty("_playerTeam");
        _previousTool = Tools.current;
    }

    private void OnDisable()
    {
        if (_mapBuilder.IsEditing)
        {
            _mapBuilder.IsEditing = false;
            OnIsEditingChanged(_mapBuilder.IsEditing);
        }
    }

    private void OnIsEditingChanged(bool isEditing)
    {
        if (isEditing == true)
        {
            _previousTool = Tools.current;
            Tools.current = Tool.None;
        }
        else
        {
            Tools.current = _previousTool;
        }
    }

    private void LockSelectionToMapBuilder()
    {
        if (_mapBuilder.IsEditing && Selection.activeGameObject != _mapBuilder.gameObject)
        {
            Selection.activeGameObject = _mapBuilder.gameObject;
        }
    }
}
