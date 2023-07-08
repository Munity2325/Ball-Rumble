using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

public class BlueBotMovement : MonoBehaviour
{

    [SerializeField] private GameObject ball;

    private NavMeshAgent AI_Agent;


    public bool isBot = true;

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

        if (isBot)
        {
            AI_Agent.SetDestination(ball.transform.position);
            animator.SetFloat("speed", 0.4f);
            GetComponent<PlayerMovement>().enabled = false;
        }
        else
        {
            GetComponent<PlayerMovement>().enabled = true;
            GetComponent<BlueBotMovement>().enabled = false;
        }
    }
}
