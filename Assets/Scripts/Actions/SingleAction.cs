using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;

public abstract class SingleAction : MonoBehaviour, IAction
{
    [Header("Action Info")]
    [SerializeField] private float delay;

    [SerializeField] private bool followSource;

    [Header("Target Info")]
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] [Range(1,AbsoluteMaxTargets)] private int maxTargets = 1;
    [SerializeField] private bool allowTargetSelf;

    private const int AbsoluteMaxTargets = 20;
    
    private Collider2D _collider;
    private Coroutine _currentCoroutine;
    private GameObject _source;
    private Vector2 _position;
    private void Initialize()
    {
        _collider = GetComponent<Collider2D>();
        if (followSource) transform.parent = _source.transform;
        _position = transform.position;
    }
    
    public void StartAction(GameObject source)
    {
        _source = source;
        Initialize();
        _currentCoroutine = StartCoroutine(ActionEnumerator());
    }

    public void StopAction(GameObject source)
    {
        StopCoroutine(_currentCoroutine);
        Destroy(gameObject);
    }

    private IEnumerator ActionEnumerator()
    {
        yield return new WaitForSeconds(delay);
        GameObject[] targets = GenerateTargets();
        ExecuteAction(targets);
        Destroy(gameObject);
    }

    private GameObject[] GenerateTargets()
    {
        Collider2D[] hits = new Collider2D[AbsoluteMaxTargets];
        int numTargets = PhysicsScene2D.OverlapCollider(_collider, hits, targetLayers);

        GameObject[] targets;
        
        if (numTargets <= maxTargets)
        {
            targets = hits.Take(numTargets).Select(col => col.gameObject).Where(target => allowTargetSelf || target != _source).ToArray();
        }
        else
        {
            targets = hits.Take(numTargets).Select(col => col.gameObject).Where(target => allowTargetSelf || target != _source).OrderBy(target => Vector2.Distance(target.transform.position, _position)).Take(maxTargets).ToArray();
        }

        return targets;
    }

    protected abstract void ExecuteAction(GameObject[] targets);
}