using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{

    private Rigidbody rigidbody;
    private PlayerInput playerInput;
    private UnityDefaultPlayerInput playerInputActions;

    [SerializeField]
    float speed = 1f;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();


        playerInputActions = new UnityDefaultPlayerInput();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Move.performed += Move_performed;
        playerInputActions.Player.Look.performed += Look_performed;
        playerInputActions.Player.Fire.performed += Fire_performed;
        playerInputActions.Player.Jump.performed += Jump_performed;

    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        //throw new System.NotImplementedException();
        rigidbody.AddForce(Vector3.up * 4f, ForceMode.Impulse);
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
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        rigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
