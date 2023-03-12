using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class OldFPSPlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    UnityDefaultPlayerInput input;
    CharacterController controller;

    [SerializeField]
    float speed = 12f;

    [SerializeField]
    Vector3 velocity;

    [SerializeField]
    float jumpHeight = 3f;

    [SerializeField]
    float gravity = -9.81f;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    float groundDistance=0.4f;

    [SerializeField]
    LayerMask groundMask;

    bool isGrounded;

    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        input = GetComponent<OldFPSPlayer>().GetInput;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector2 movePos = input.Player.Move.ReadValue<Vector2>();

        float x = movePos.x;
        float z = movePos.y;
        
        Vector3 move = transform.right *x + transform.forward *z;

        controller.Move(move*speed* Time.deltaTime);


        float jump = input.Player.Jump.ReadValue<float>();

        if (jump == 1 & isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity*Time.deltaTime);
    }
}
