using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{
    private PlayerRef _playerRef;

    public void Init(PlayerRef playerRef)
    {
        _playerRef = playerRef;
    }
}
