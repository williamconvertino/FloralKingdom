using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviour
{
    public static NetworkPlayerSpawner Instance;
    
    #region Initialization
    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
    }

    #endregion

    #region Player Tracking

    [SerializeField] private GameObject networkPlayerPrefab;
    
    private Dictionary<PlayerRef, NetworkObject> _playerMap = new Dictionary<PlayerRef, NetworkObject>();

    public void SpawnPlayer(NetworkRunner runner, PlayerRef playerRef)
    {
        NetworkObject playerObject = runner.Spawn(networkPlayerPrefab, GetSpawnpoint(), Quaternion.identity, playerRef);
        _playerMap.Add(playerRef, playerObject);
    }

    public void DespawnPlayer(NetworkRunner runner, PlayerRef playerRef)
    {
        if (_playerMap.TryGetValue(playerRef, out NetworkObject playerObject))
        {
            runner.Despawn(playerObject);
            _playerMap.Remove(playerRef);
        }
    }

    public List<NetworkObject> GetPlayers()
    {
        return new List<NetworkObject>(_playerMap.Values);
    }
    
    public List<PlayerRef> GetPlayerRefs()
    {
        return new List<PlayerRef>(_playerMap.Keys);
    }
    
    #endregion

    #region Spawnpoints

    [SerializeField] private Transform[] spawnpoints;

    private int _lastIndex = -1;
    
    private Vector3 GetSpawnpoint()
    {
        _lastIndex = (_lastIndex + 1) % spawnpoints.Length;
        return spawnpoints[_lastIndex].position;
    }

    #endregion
}
