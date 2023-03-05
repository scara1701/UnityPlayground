using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class FPSPlayer : MonoBehaviour
{
    private UnityDefaultPlayerInput input;

    public UnityDefaultPlayerInput GetInput
    {
        get
        {
            if(input == null)
            {
                input = new UnityDefaultPlayerInput();
                input.Player.Enable();
            }
            return input;
        }
    }

}
