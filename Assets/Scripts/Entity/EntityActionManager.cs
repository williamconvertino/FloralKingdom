using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

public class EntityActionManager : MonoBehaviour
{
    [SerializeField] private GameObject[] actions;

    private Dictionary<String, GameObject> _actionMap;
    private Dictionary<String, IAction> _activeActions;

    private void Awake()
    {
        _actionMap = actions.ToDictionary(prefab => prefab.name);
        _activeActions = new Dictionary<string, IAction>();
    }

    public void StartAction(String actionName)
    {
        if (!_actionMap.TryGetValue(actionName, out GameObject actionPrefab)) Debug.LogError("Could not find action: " + actionName);
        
        GameObject actionObject = Instantiate(actionPrefab, transform.position, Quaternion.identity);
        IAction actionComponent = actionObject.GetComponent<IAction>();
        
        actionComponent.StartAction(gameObject);
        // if (_activeActions.ContainsKey(actionName)) StopAction(actionName);
        // _activeActions.Add(actionName, actionComponent);
    }
    //
    // public void StopAction(String actionName)
    // {
    //     if (!_activeActions.TryGetValue(actionName, out IAction actionComponent)) return;
    //     actionComponent.StopAction(gameObject);
    //     _activeActions.Remove(actionName);
    // }
    
}