using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    // movement variables
    [Header("Movement Settings")]
    public bool canMove = true;
    public CharacterController controller;
    public Transform cam;
    public CinemachineCamera freeLookCam;
    public Transform targetForCam;
    private bool isMouseActive = false;

    [HideInInspector]
    public bool isTalkingWithNPC = false;

    public float moveSpeed;
    public float speed = 6f;
    public float sprintSpeed = 12f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [Header("Jump Check")]
    private Vector3 velocity;
    private bool isGrounded;
    public bool isJumping = false;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Dash
    [Header("Dash Settings")]
    public float dashDistance = 8f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.3f;
    public bool isDashing = false;
    private float dashTimer = 0f;
    private Vector3 dashDirection;

    // Sprint
    public bool isSprinting = false;

    // Stamina
    [Header("Stamina Settings")]
    public PlayerStaminaBar staminaBar;
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDrainRate = 15f;
    public float staminaRegenRate = 10f;

    // Attack
    [Header("Attack Settings")]
    public bool isAttacking = false;

    [Header("Source")]
    private SourceCharacter sourceCharacter;
    private bool isRunSoundPlaying = false;

    private void Start()
    {
        sourceCharacter = GetComponentInChildren<SourceCharacter>();

        currentStamina = maxStamina;

        canMove = true;

        Cursor.lockState = CursorLockMode.Locked;

        cam  = Camera.main.transform;

        GameObject freeLookObj = GameObject.Find("PlayerFreeLookCam");
        if (freeLookObj != null)
        {
            freeLookCam = freeLookObj.GetComponent<CinemachineCamera>();           
            freeLookCam.Follow = targetForCam;
            freeLookCam.LookAt = targetForCam;
            freeLookCam.Priority = 10;
        }
        else
        {
            Debug.LogError("Không tìm thấy PlayerFreeLookCam trong Scene.");
        }
        staminaBar = FindAnyObjectByType<PlayerStaminaBar>();

        if (freeLookCam != null)
        {
            freeLookCam.Follow = targetForCam;
            freeLookCam.LookAt = targetForCam;
        }

        isAttacking = false;
    }

    private void Update()
    {
        Dash();
        HandleStamina();
        MouseController();
        if (!isDashing)
        {
            if (isTalkingWithNPC) return;
            Jump();
            if (isAttacking) return;
            Movement();
        }
    }

    private void MouseController()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (!isMouseActive)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isMouseActive = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isMouseActive = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.RightAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isMouseActive = false;
        }
    }

    private void Movement()
    {
        float horizontal;
        float vertical;
        if (!canMove)
        {
            horizontal = 0f;
            vertical = 0f;
        }
        else
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        animator.SetFloat("Speed", direction.magnitude);

        

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            moveSpeed = isSprinting && currentStamina > 0f ? sprintSpeed : speed;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);

            if (!isRunSoundPlaying && sourceCharacter != null)
            {
                sourceCharacter.PlayRunSound();
                isRunSoundPlaying = true;
            }

            if (isSprinting)
            {
                currentStamina -= staminaDrainRate * Time.deltaTime;
                if (currentStamina <= 0f)
                {
                    currentStamina = 0f;
                    animator.SetBool("RunWhichWeapon", false);
                    isSprinting = false;
                }
            }
        }
        else
        {
            isSprinting = false;
            animator.SetBool("RunWhichWeapon", false);
            if (isRunSoundPlaying)
            {
                isRunSoundPlaying = false;
                sourceCharacter.StopRunSound();
            }
        }

        staminaBar.UpdateBar((int)currentStamina, (int)maxStamina);
    }

    private void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
            animator.SetBool("IsGrounded", true);
            isJumping = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            if (!canMove || isTalkingWithNPC) return;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsGrounded", false);
            isJumping = true;
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        animator.SetBool("IsFalling", !isGrounded && velocity.y < 0);
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && currentStamina >= 10f)
        {
            isDashing = true;
            dashTimer = dashDuration;
            dashDirection = transform.forward;
            currentStamina -= 10f;
            animator.SetTrigger("Dashing");
            isAttacking = false;
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