using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset actions; // Przypisz swój plik Input Actions tutaj
    private InputAction moveAction;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float friction = 6f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController controller;
    private Vector2 movementInput;
    private Vector3 velocity;
    Vector3 targetVelocity;

   
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        moveAction = actions.FindAction("Move");
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
    }

    private void Update()
    {
        ApplyFriction();
        ApplyMovement();
        ApplyGravity();
        MovePlayer();
    }

    private void ApplyFriction()
    {
        if (controller.isGrounded)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0f, friction * Time.deltaTime);
            velocity.z = Mathf.MoveTowards(velocity.z, 0f, friction * Time.deltaTime);
        }
    }

    private void ApplyMovement()
    {
        targetVelocity = new Vector3(movementInput.x, 0f, movementInput.y) * maxSpeed;
        velocity.x = Mathf.MoveTowards(velocity.x, targetVelocity.x, acceleration * Time.deltaTime);
        velocity.z = Mathf.MoveTowards(velocity.z, targetVelocity.z, acceleration * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
    }

    private void MovePlayer()
    {
        controller.Move(velocity * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }


}