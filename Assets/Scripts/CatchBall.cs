using UnityEngine;

public class CatchBall : MonoBehaviour
{
    [SerializeField] private GameObject ballTrigger;
    [SerializeField] private Transform handsPosition;
    [SerializeField] private GameObject ball;
    [SerializeField] private float throwForce;
    [SerializeField] private float throwAngle;
    [SerializeField] private float kickForce;
    [SerializeField] private float kickAngle;
    [SerializeField] private bool isRed;
    public bool isCatched = false;

    //[SyncVar(hook = nameof(OnIsCatchedChanged))] public bool isCatched = false;
	
	public GameObject activePlayer;
    private Animator animator;
    private Rigidbody ballRigidbody;
    private float previousRotationAngle;
    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        animator= GetComponent<Animator>();
        ballRigidbody = ball.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float currentRotationAngle = transform.eulerAngles.y;

        if(isRed)
        {
            if (currentRotationAngle >= 205f && currentRotationAngle <= 325f)
            {
                if (Input.GetKeyDown(KeyCode.E) && isCatched)
                {
                    Debug.Log("Вперёд бросать нельзя");
                    return;
                }
            }
        }
        else if(!isRed)
        {
            if (currentRotationAngle >= 35f && currentRotationAngle <= 155f)
            {
                if (Input.GetKeyDown(KeyCode.E) && isCatched)
                {
                    Debug.Log("Вперёд бросать нельзя");
                    return;
                }
            }
        }

        previousRotationAngle = currentRotationAngle;

        if (isCatched)
        {
            ball.transform.position = handsPosition.position;
            activePlayer = gameObject;
        }

        if (Input.GetKeyDown(KeyCode.E) && isCatched)
        {
            ThrowBall();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && isCatched)
        {
            KickBall();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ball"))
        {
            isCatched = true;
        }
    }

    private void ThrowBall()
    {
        isCatched = false;
        

        // Применение фиксированной силы и угла броска к мячу
        Vector3 throwDirection = Quaternion.Euler(-throwAngle, transform.eulerAngles.y, 0f) * Vector3.forward;
        ballRigidbody.AddForce(throwDirection * throwForce);
    }
    private void KickBall()
    {
        isCatched = false;
        

        // Применение фиксированной силы и угла броска к мячу
        Vector3 throwDirection = Quaternion.Euler(-kickAngle, transform.eulerAngles.y, 0f) * Vector3.forward;
        ballRigidbody.AddForce(throwDirection * kickForce);
    }


    // private void OnIsCatchedChanged(bool oldValue, bool newValue)
    // {
    //     if (newValue)
    //     {
    //         RpcUpdateBallPosition(handsPosition.position);
    //     }
    //     else
    //     {
    //         RpcResetBallPosition();
    //     }
    // }

    //[ClientRpc]
    // private void RpcUpdateBallPosition(Vector3 position)
    // {
    //     // if (isLocalPlayer)
    //     // {
    //     //     ball.transform.position = position;
    //     // }
    //     ball.transform.position = position;
    // }

    //[ClientRpc]
    // private void RpcResetBallPosition()
    // {
    //     // if (isLocalPlayer)
    //     // {
    //     //     ball.transform.position = Vector3.zero;
    //     // }
    //     ball.transform.position = Vector3.zero;
    // }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "BluePlayer" && gameObject.tag == "RedPlayer")
        {
            animator.Play("Knocked");
            isCatched = false;
            ball.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 5f, 0f), ForceMode.Force);
        }
        if (col.gameObject.tag == "RedPlayer" && gameObject.tag == "BluePlayer")
        {
            animator.Play("Knocked");
            isCatched = false;
            ball.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 5f, 0f), ForceMode.Force);
        }
    }
}
