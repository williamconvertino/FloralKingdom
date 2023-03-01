
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class NetworkInputManager : MonoBehaviour
{
    public static NetworkInputManager Instance;

    #region Initialization
    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
    }

    #endregion

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        NetworkInputData networkInputData = new NetworkInputData()
        {
            Direction = GetDirection(),
            ActionButtons = GetActionButtons()
        };
        
        input.Set(networkInputData);
    }

    #region Direction

    public static Vector2 GetDirection()
        {
            Vector2 direction = Vector2.zero;
     
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) direction.x += 1;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) direction.x -= 1; 
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) direction.y += 1; 
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) direction.y -= 1; 
            
            return direction;
        }
    #endregion

    #region Action Buttons
    
    public static NetworkButtons GetActionButtons()
    {
        NetworkButtons activeButtons = new NetworkButtons();
        
        
        
        return activeButtons;
    }

    #endregion
    

}