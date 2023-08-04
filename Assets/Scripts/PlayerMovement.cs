using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float staminaMax;
    [SerializeField] private float stamina;
    [SerializeField] private float staminaCooldown;

    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject gameManager;

    private bool canHeal;
    private bool isGrounded;
    private Rigidbody rb;
    private Animator animator;
    private float horizontalInput;
    private float verticalInput;
    private float timeSinceSprint;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Input handling
        horizontalInput = -Input.GetAxis("Horizontal");
        verticalInput = -Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Ground Check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        // Rotation
        Rotate(movementDirection);

        // Sprinting
        Sprint();

        // Stamina management
        RefillStamina();

        // Animation
        Animation(movementDirection);

        // Jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // Movement
        Move();
    }

    private void Move()
    {
        if (isGrounded)
        {
            Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
            Vector3 moveVelocity = moveDirection * (Input.GetKey(KeyCode.LeftShift) && stamina > 0 ? sprintSpeed : movementSpeed);

            rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            timeSinceSprint = 0;
            canHeal = false;
            movementSpeed = sprintSpeed;
            stamina -= Time.deltaTime;
        }
        else
        {
            movementSpeed = walkSpeed;
            timeSinceSprint += Time.deltaTime;
        }

        if (timeSinceSprint >= staminaCooldown)
        {
            canHeal = true;
        }

        if (stamina <= 0)
        {
            movementSpeed = walkSpeed;
        }
    }

    private void RefillStamina()
    {
        if (canHeal && stamina < staminaMax)
        {
            stamina += Time.deltaTime * staminaCooldown;
            stamina = Mathf.Clamp(stamina, 0, staminaMax);
        }
    }

    private void Animation(Vector3 mD)
    {
        if (mD == Vector3.zero)
        {
            animator.SetFloat("speed", 0);
        }
        else if (movementSpeed == sprintSpeed)
        {
            animator.SetFloat("speed", 0.6f);
        }
        else if (movementSpeed == walkSpeed)
        {
            animator.SetFloat("speed", 0.4f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Handle score triggering logic here if needed.
        if (other.CompareTag("RedTrigger") && GetComponent<CatchBall>().isCatched == true && gameObject.tag == "BluePlayer")
        {
            if (canAdd)
            {
                gameManager.GetComponent<GoalSystem>().BlueScore += 5;
                canAdd = false;
                StartCoroutine(DoAddPoints());
            }
        }
        else if (other.CompareTag("BlueTrigger") && GetComponent<CatchBall>().isCatched == true && gameObject.tag == "RedPlayer")
        {
            if (canAdd)
            {
                gameManager.GetComponent<GoalSystem>().RedScore += 5;
                canAdd = false;
                StartCoroutine(DoAddPoints());
            }
        }
    }

    private bool canAdd = true;

    IEnumerator DoAddPoints()
    {
        yield return new WaitForSeconds(2f);
        canAdd = true;
    }
}
