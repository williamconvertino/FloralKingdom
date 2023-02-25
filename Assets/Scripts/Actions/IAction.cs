using UnityEngine;

public interface IAction
{
    public void StartAction(GameObject source);
    public void StopAction(GameObject source);
}