using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private FPSPlayerInputActions playerInput;
    private FPSPlayerInputActions.OnFootActions onFoot;

    private PlayerMotor playerMotor;
    private PlayerLook playerLook;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new FPSPlayerInputActions();
        onFoot = playerInput.OnFoot;
        playerMotor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => playerMotor.Jump();
        onFoot.Crouch.performed += ctx => playerMotor.Crouch();
        onFoot.Sprint.performed += ctx => playerMotor.Sprint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //vraag aan de playermotor om de input te verwerken
        playerMotor.ProcessMovement(onFoot.Movement.ReadValue<Vector2>());
    }
    //acties op de camera best in lateupdate
    private void LateUpdate()
    {
        playerLook.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
