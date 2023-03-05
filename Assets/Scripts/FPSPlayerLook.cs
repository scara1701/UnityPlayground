using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class FPSPlayerLook : MonoBehaviour
{
    private UnityDefaultPlayerInput input;
    private PlayerInput playerInput;

    [SerializeField]
    private GameObject playerObject;

    [SerializeField]
    private float mouseSensitivity = 5f;

    private Transform playerBody;

    private float xRotation = 0f;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
                input = playerObject.GetComponent<FPSPlayer>().GetInput;

        playerBody = playerObject.transform;
        playerInput = playerObject.GetComponent<PlayerInput>();

    }

    void Update()
    {
        Vector2 mousePos = input.Player.Look.ReadValue<Vector2>();
        float mouseX = mousePos.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mousePos.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }


}
