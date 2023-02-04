using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using Fusion.Sockets;
using System.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class NetworkRunnerHandler : MonoBehaviour
{
    [SerializeField] private NetworkRunner networkRunnerPrefab;

    private NetworkRunner _networkRunner;
    
    private void Start()
    {
        _networkRunner = Instantiate(networkRunnerPrefab);
        _networkRunner.name = "Network Runner";

        InitializeNetworkRunner(_networkRunner, GameMode.AutoHostOrClient, NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);
        
        Debug.Log("Started NetworkRunner");
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, NetAddress netAddress, SceneRef scene, Action<NetworkRunner> initialized)
    {
        var sceneManager = runner.GetComponents((typeof(MonoBehaviour))).OfType<INetworkSceneManager>().FirstOrDefault();

        if (sceneManager == null)
        {
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        runner.ProvideInput = true;

        return runner.StartGame(new StartGameArgs()
        {
            GameMode = gameMode,
            Address = netAddress,
            Scene = scene,
            SessionName = "Test Session",
            Initialized = initialized,
            SceneManager = sceneManager
        });
    }

}
