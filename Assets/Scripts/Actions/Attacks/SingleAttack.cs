using UnityEngine;

public class SingleAttack : SingleAction
{
    [SerializeField] private int damage;
    protected override void ExecuteAction(GameObject[] targets)
    {
        foreach (GameObject target in targets)
        {
            if (!target.TryGetComponent(out EntityHealth targetHealth)) Debug.LogError("Error: Attempted to damage an object without a health field.");
            targetHealth.Hit(damage);
        }
    }
}