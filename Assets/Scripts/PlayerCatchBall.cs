using UnityEngine;

public class PlayerCatchBall : MonoBehaviour
{
    public bool IsCatched
    {
        get { return isCatched; }
        set { isCatched = value; }
    }


    [SerializeField] private GameObject ball;
    [SerializeField] private Transform handsPosition;
    private bool isCatched = false;

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
    }
    private void Update()
    {
        // if(!isLocalPlayer) return;
        CatchBall();

    }
    private void OnTriggerEnter(Collider collider)
    {
        // if (!isServer)
        // {
        //     return;
        // }

        // NetworkIdentity networkIdentity = collider.GetComponent<NetworkIdentity>();
        // if(networkIdentity != null && collider.CompareTag("Ball"))
        // {
        //     isCatched = true;
        //     ball.transform.position = handsPosition.position;
        //     ball.GetComponent<PlayerThrowBall>().AssignPlayer(gameObject);
        // }
    }
    private void OnTriggerExit(Collider collider)
    {
        // if (!isServer)
        // {
        //     return;
        // }


        // NetworkIdentity networkIdentity = collider.GetComponent<NetworkIdentity>();
        // if(networkIdentity != null && collider.CompareTag("Ball"))
        // {
        //     isCatched = false;
        //     ball.GetComponent<PlayerThrowBall>().AssignPlayer(null);
        // }
    }
    private void CatchBall()
    {
        // if (!isServer)
        // {
        //     return;
        // }

        if(isCatched)
        {
            ball.transform.position = handsPosition.position;
        }
    }
}
