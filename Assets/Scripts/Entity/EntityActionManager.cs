using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityActionManager : MonoBehaviour
{
    [Header("Actions")]
    [SerializeField] private GameObject[] _actions;

    [Header("Debug Tools")]
    [SerializeField] private bool _debugMode;
    
    private Dictionary<String, GameObject> _actionMap;
    private Dictionary<String, Action> _activeActions;

    private void Awake()
    {
        _actionMap = _actions.ToDictionary(prefab => prefab.name);
        _activeActions = new Dictionary<string, Action>();
    }

    public void StartAction(String actionName)
    {
        if (!_actionMap.TryGetValue(actionName, out GameObject actionPrefab)) Debug.LogError("Could not find action: " + actionName);
        
        GameObject actionObject = Instantiate(actionPrefab, transform.position, Quaternion.identity);
        Action actionComponent = actionObject.GetComponent<Action>();
        
        actionComponent.DebugMode = _debugMode;
        
        actionComponent.StartAction(gameObject);
    }

}