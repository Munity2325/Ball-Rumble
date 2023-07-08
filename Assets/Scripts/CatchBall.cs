using UnityEngine;

public class CatchBall : MonoBehaviour
{
    [SerializeField] private GameObject ballTrigger;
    [SerializeField] private Transform handsPosition;
    [SerializeField] private GameObject ball;
    public bool isCatched = false;

    //[SyncVar(hook = nameof(OnIsCatchedChanged))] public bool isCatched = false;
	
	public GameObject activePlayer;
    private Animator animator;


    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        animator= GetComponent<Animator>();
    }

    private void Update()
    {
        if (isCatched)
        {
            ball.transform.position = handsPosition.position;
			activePlayer = gameObject;
        }
        if (Input.GetKeyDown(KeyCode.E) && isCatched)
        {
            CmdThrowBall();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ball"))
        {
            isCatched = true;
        }
    }

    private void CmdThrowBall()
    {
        Debug.Log("Throwing ball");
        isCatched = false;
        ball.GetComponent<Rigidbody>().isKinematic = false;

        float currentPower = 25;
        float arcHeight = 10;

        Quaternion throwRotation = Quaternion.LookRotation(transform.forward, transform.up);
        transform.rotation = throwRotation * Quaternion.Euler(-55, 0f, 0f);
        Vector3 throwDirection = transform.forward;
        Vector3 throwForce = throwDirection * currentPower;
        ball.GetComponent<Rigidbody>().AddForce(throwForce, ForceMode.Impulse);
		activePlayer = null;

        RpcResetBallPosition();

    }

    private void OnIsCatchedChanged(bool oldValue, bool newValue)
    {
        if (newValue)
        {
            RpcUpdateBallPosition(handsPosition.position);
        }
        else
        {
            RpcResetBallPosition();
        }
    }

    //[ClientRpc]
    private void RpcUpdateBallPosition(Vector3 position)
    {
        // if (isLocalPlayer)
        // {
        //     ball.transform.position = position;
        // }
        ball.transform.position = position;
    }

    //[ClientRpc]
    private void RpcResetBallPosition()
    {
        // if (isLocalPlayer)
        // {
        //     ball.transform.position = Vector3.zero;
        // }
        ball.transform.position = Vector3.zero;
    }
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
