using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float staminaMax;
    [SerializeField] private float stamina;
    [SerializeField] private float staminaCooldown;
    [SerializeField] private int redScore;
    [SerializeField] private int blueScore;
    
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject gameManager;

    //[SerializeField] private Image staminaLevel;
    private bool canHeal;
    
    private float ySpeed;
    private float timeSinceSprint;

    private CharacterController characterController;
    private Animator animator;

    private bool canAdd = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator= GetComponent<Animator>();
        ball = GameObject.FindGameObjectWithTag("Ball");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void Update()
    {

        // if (!isLocalPlayer) return;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(-horizontalInput, 0, -verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * movementSpeed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime * 3;

        if (characterController.isGrounded)
        {
            ySpeed = -0.5f;

            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
            }
        }

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        Sprint();
        Animation(movementDirection);
        if (stamina <= 0)
        {
            movementSpeed = walkSpeed;
        }
        if (stamina > staminaMax)
        {
            stamina = staminaMax;
        }
        RefillStamina();

    }

    private void Sprint()
    {
        if (Input.GetKey("left shift") && stamina > 0)
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
        if(timeSinceSprint >= staminaCooldown)
        {
            canHeal = true;
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
    private void RefillStamina()
    {
        if(canHeal == true)
        {
            if(stamina < staminaMax)
            {
                //staminaLevel.fillAmount = Mathf.MoveTowards(staminaLevel.fillAmount, 1f, Time.deltaTime * 0.25f);
                stamina = Mathf.MoveTowards(stamina / staminaMax, 1f, Time.deltaTime * 0.25f) * staminaMax;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("RedTrigger") && GetComponent<CatchBall>().isCatched == true && gameObject.tag == "BluePlayer")
        {
            if(canAdd)
            {
                gameManager.GetComponent<GoalSystem>().BlueScore += 5;
                canAdd = false;
                StartCoroutine(DoAddPoints());
            }
        }
        else if(other.CompareTag("BlueTrigger") && GetComponent<CatchBall>().isCatched == true && gameObject.tag == "RedPlayer")
        {
            if(canAdd)
            {
                gameManager.GetComponent<GoalSystem>().RedScore += 5;
                canAdd = false;
                StartCoroutine(DoAddPoints());
            }
        }
    }
    IEnumerator DoAddPoints()
    {
        yield return new WaitForSeconds(2f);
        canAdd = true;
    }



}
