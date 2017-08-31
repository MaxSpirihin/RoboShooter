using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Управляет вводом
/// </summary>
public static class InputManager {

    public static float GetLookAxisHorizontal()
    {
        return Input.GetAxis("Mouse X");
    }

    public static float GetLookAxisVertical()
    {
        return Input.GetAxis("Mouse Y");
    }

    public static float GetMoveAxisHorizontal()
    {
        return Input.GetAxis("Horizontal");
    }

    public static float GetMoveAxisVertical()
    {
        return Input.GetAxis("Vertical");
    }

    public static bool GetJump()
    {
        return Input.GetButton("Jump");
    }

    public static bool GetJumpDown()
    {
        return Input.GetButtonDown("Jump");
    }

    public static bool GetShootDown()
    {
        return Input.GetButtonDown("Fire1");
    }

    public static bool GetReloadDown()
    {
        return Input.GetButtonDown("Reload");
    }
    
    public static bool GetAim()
    {
        return Input.GetButton("Fire2");
    }

    public static bool GetSprint()
    {
        return Input.GetButton("Sprint");
    }
}
