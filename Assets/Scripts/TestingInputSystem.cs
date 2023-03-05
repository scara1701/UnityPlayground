using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class TestingInputSystem : MonoBehaviour
{

    private Rigidbody sphereRigidbody;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        sphereRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        


        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.UI.Submit.performed += Submit;

        playerInputActions.Player.Movement.performed += Movement_performed;

        //when using C# events instead of unity events
        //playerInput.onActionTriggered += PlayerInput_onActionTriggered;
    }

    private void Movement_performed(CallbackContext context)
    {
        Debug.Log(context);
    }

    //when using C# events instead of unity events
    //private void PlayerInput_onActionTriggered(InputAction.CallbackContext obj)
    //{
    //    Debug.Log($"Jump! {obj.phase}");
    //}

    public void Jump(CallbackContext context)
    {
        Debug.Log(context);
        if (!context.performed) return;
        Debug.Log($"Jump! {context.phase}");
        sphereRigidbody.AddForce(Vector3.up * 4f, ForceMode.Impulse);
    }

    public void Submit(CallbackContext context){
        Debug.Log("Submit " +context);
    }


    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        
        float speed = 1f;
        sphereRigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }

    private void Update()
    {
        if (playerInput == null) return;
        if (Keyboard.current.tKey.wasPressedThisFrame) {
            //playerInput.SwitchCurrentActionMap("UI");
            playerInputActions.Player.Disable();
            playerInputActions.UI.Enable();
        }
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            //playerInput.SwitchCurrentActionMap("Player");
            playerInputActions.UI.Disable();
            playerInputActions.Player.Enable();
        }
    }
}
