using UnityEngine;
using UnityEngine.Audio;

// automaticly require Character Controller
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // Variables

    // player speed
    [Header("Player Speed")]
    [SerializeField] private float walkingSpeed = 5.0f;

    // Player Sprint speed
    [Header("Player Sprint Speed")]
    [SerializeField] private float runningSpeed = 8.5f;
    // Jump Speed
    [Header("Player Jump Speed")]
    [SerializeField] private float jumpSpeed = 6.0f;
    // Graavity basics
    [Header("Player Gravity")]
    [SerializeField] private float gravity = 20.0f;
    // Player camera
    [Header("Player Camera")]
    [SerializeField] private Camera playerCamera;
    // Look Speed
    [SerializeField] private float lookSpeed = 2.0f;
    // Camera X limitation
    [SerializeField] private float lookXLimit = 45.0f;
    
    // Character Controller
    CharacterController characterController;
    
    // Vector motion
    Vector3 moveDirection = Vector3.zero;
    // player rotation
    float rotationX = 0;

    // can player move
    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        // Get Character controller
        characterController = GetComponent<CharacterController>();

        // Lock and hide cursor
        HideCursor();
    }

    void Update()
    {
        // detect player motion
        DetectMotion();

    }

    void DetectMotion()
    {
        // When grounded  are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);



        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;

        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    

}