using UnityEngine;
using UnityEngine.AI;

public class RedBotMovement : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    private NavMeshAgent AI_Agent;
    public bool isBot = true;
    // [SyncVar] public bool isBot = true;
    private Animator animator;

    void Start()
    {
        AI_Agent = gameObject.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    void Update()
    {
        if (gameObject.GetComponent<CatchBall>().isCatched)
        {
            isBot = false;
        }
        else if (gameObject.GetComponent<CatchBall>().activePlayer != null && gameObject.GetComponent<CatchBall>().activePlayer.tag == "RedPlayer")
        {
            if (gameObject.GetComponent<CatchBall>().isCatched == false)
            {
                isBot = true;
            }
        }

        if (isBot)
        {
            AI_Agent.SetDestination(ball.transform.position);
            animator.SetFloat("speed", 0.4f);
            GetComponent<PlayerMovement>().enabled = false;
        }
        else
        {
            GetComponent<PlayerMovement>().enabled = true;
            GetComponent<RedBotMovement>().enabled = false;
        }
    }
}
