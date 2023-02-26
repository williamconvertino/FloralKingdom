using UnityEngine;

public abstract class Action : MonoBehaviour
{
    [HideInInspector] public bool DebugMode;
    public abstract void StartAction(GameObject source);
    public abstract void StopAction(GameObject source);
}