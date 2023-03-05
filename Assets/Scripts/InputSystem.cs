using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class InputSystem : MonoBehaviour
{


    private PlayerInput playerInput;
    private UnityDefaultPlayerInput _input;
    private CharacterController characterController;

    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    float acceleration = 1f;

    [SerializeField]
    float minViewDistance = 25f;

    [SerializeField]
    float mouseSensitivity = 100f;

    //player
    private float _speed;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        


        _input = new UnityDefaultPlayerInput();
        _input.Player.Enable();

        _input.Player.Move.performed += Move_performed;
        _input.Player.Look.performed += Look_performed;
        _input.Player.Fire.performed += Fire_performed;
        _input.Player.Jump.performed += Jump_performed;

    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        //throw new System.NotImplementedException();
        //rigidbody.AddForce(Vector3.up * 4f, ForceMode.Impulse);
    }

    private void Fire_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Pew! Pew! Pew!");
        //throw new System.NotImplementedException();
    }

    private void Look_performed(InputAction.CallbackContext obj)
    {
        //throw new System.NotImplementedException();
    }

    private void Move_performed(InputAction.CallbackContext obj)
    {
        //throw new System.NotImplementedException();
    }

    private void FixedUpdate()
    {
        Vector2 moveVector = _input.Player.Move.ReadValue<Vector2>();
        Vector2 mousePos = _input.Player.Look.ReadValue<Vector2>();


        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = moveVector.x;
        moveDirection.z = moveVector.y;


        characterController.Move(transform.TransformDirection(moveDirection) * acceleration * Time.deltaTime);
        transform.Rotate(Vector3.up * (mousePos.x * Time.deltaTime) * mouseSensitivity);

        //enkel toe te passen op de camera?
        float xRotation = 0f;
        xRotation -= (mousePos.y * Time.deltaTime) * 8f;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void Move()
    {
        float targetSpeed = acceleration;
        Vector2 move = _input.Player.Move.ReadValue<Vector2>();
        if (move == Vector2.zero) targetSpeed = 0f;

        float currentHorizontalSpeed = new Vector3(characterController.velocity.x, 0.0f, characterController.velocity.z).magnitude;
        float speedOffset = 0.1f;


        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed , Time.deltaTime * acceleration);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }


        // normalise input direction
        Vector3 inputDirection = new Vector3(move.x, 0.0f, move.y).normalized;

        if (move != Vector2.zero)
        {
            // move
            inputDirection = transform.right * move.x + transform.forward * move.y;
        }

        characterController.Move(inputDirection.normalized * (_speed * Time.deltaTime)  * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        //lock cursor on screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
