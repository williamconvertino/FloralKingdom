using UnityEngine;

public static class PlayerInput
{
    #region Move Direction

    public static Vector2 GetMoveDirection()
    {
        return new Vector2(GetMoveDirHorizontal(), GetMoveDirVertical());
    }

    public static float GetMoveDirHorizontal()
    {
        float moveDir = 0.0f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDir += 1.0f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir += -1.0f;
        }
        return moveDir;
    }
    
    public static float GetMoveDirVertical()
    {
        float moveDir = 0.0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveDir += 1.0f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveDir += -1.0f;
        }
        return moveDir;
    }


    #endregion

    #region Attacks

    public static bool GetAttackPrimary()
    {
        return Input.GetKey(KeyCode.Space);
    }

    #endregion
}
