using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float groundDrag = 5f;
    public float jumpForce = 10f;
    public float jumpCooldown = 0.5f;
    public float airMultiplier = 0.5f;
    private bool readyToJump = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;
    private bool grounded;

    public Transform orientation;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rb;

    [Header("Flashlight")]
    public GameObject flashlight;
    public bool lightEnabled;
    public float flashlightCooldown = 0;
    public float maxCooldown;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Visualize the ground check raycast in the editor
        Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.2f), Color.red);

        // Corrected Ground Check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        PlayerInput();
        SpeedControl();
        if (flashlightCooldown <= 0)
        {
            if (Input.GetAxis("Flashlight") > 0)
            {
                if (lightEnabled == true)
                {
                    lightEnabled = false;
                    flashlight.gameObject.SetActive(false);
                    flashlightCooldown = maxCooldown;
                }
                else
                {
                    lightEnabled = true;
                    flashlight.gameObject.SetActive(true);
                    flashlightCooldown = maxCooldown;
                }
            }
        } else {
            if (flashlightCooldown > 0)
                flashlightCooldown -= 1;
        }
        //Corrected Rigidbody Drag
        rb.linearDamping = grounded ? groundDrag : 0f;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKey("f"))
        {
            enableFlashlight();
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    public void Jump()
    {
        // Reset Y velocity only before applying force
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }



    public void enableFlashlight()
    {
        if (lightEnabled)
        {
            flashlight.gameObject.SetActive(false);
        }
        else
        {
            flashlight.gameObject.SetActive(true);
        }
    }

    
    
    //public void onKeyRelease()
}
