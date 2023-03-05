using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class FPSPlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private UnityDefaultPlayerInput input;
    private CharacterController controller;

    [SerializeField]
    private float speed = 12f;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        input = GetComponent<FPSPlayer>().GetInput;
        controller = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movePos = input.Player.Move.ReadValue<Vector2>();

        float x = movePos.x;
        float z = movePos.y;
        
        Vector3 move = transform.right *x + transform.forward *z;

        controller.Move(move*speed* Time.deltaTime);
    }
}
