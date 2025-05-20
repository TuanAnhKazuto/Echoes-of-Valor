using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    // movement variables
    [Header("Movement Settings")]
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float sprintSpeed = 12f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [Header("Jump Check")]
    private Vector3 velocity;
    private bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Dash
    [Header("Dash Settings")]
    public float dashDistance = 8f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private Vector3 dashDirection;

    // Sprint
    private bool isSprinting = false;

    // Stamina
    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDrainRate = 15f;
    public float staminaRegenRate = 10f;

    private void Start()
    {
        currentStamina = maxStamina;
    }

    private void Update()
    {
        if (!isDashing)
        {
            Movement();
            Jump();
        }

        Dash();
        HandleStamina();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        animator.SetFloat("Speed", direction.magnitude);


        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            float moveSpeed = isSprinting && currentStamina > 0f ? sprintSpeed : speed;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);

            if (isSprinting)
            {
                currentStamina -= staminaDrainRate * Time.deltaTime;
                if (currentStamina <= 0f)
                {
                    currentStamina = 0f;
                    isSprinting = false;
                }
            }
        }
        else
        {
            isSprinting = false;
            animator.SetBool("RunWhichWeapon", false);
        }
    }

    private void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Dash()
    {
        if (Input.GetMouseButtonDown(1) && !isDashing && currentStamina >= 10f)
        {
            isDashing = true;
            dashTimer = dashDuration;
            dashDirection = transform.forward;
            currentStamina -= 10f;
        }

        if (isDashing)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                isDashing = false;
                isSprinting = true;
                animator.SetBool("RunWhichWeapon", true);
            }
        }

        if (isSprinting && Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            isSprinting = false;
            animator.SetBool("RunWhichWeapon", false);
        }
    }

    private void HandleStamina()
    {
        if (!isSprinting && !isDashing && currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }
    }
}