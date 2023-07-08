using UnityEngine;

public class PlayerThrowBall : MonoBehaviour
{
    public GameObject Player
    {
        get { return player; }
        set { player = value; }
    }
    


    [SerializeField] private float maxPower;
    [SerializeField] private float maxDistance;
    [SerializeField] private float throwAngle;
    [SerializeField] private GameObject player;

    [SerializeField] private AudioSource hitBallAudio;

    private bool isThrowing = false;

    private float currentPower = 0f;
    private float currentDistance = 0f;

    private int redScore;
    private int blueScore;
    
    private void Update()
    {
        // if (!isServer) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            isThrowing = true;
            currentPower = 0f;
            currentDistance = 0f;
        }

        if (Input.GetMouseButton(0) && isThrowing && player != null)
        {
            currentPower += Time.deltaTime * maxPower;
            currentPower = Mathf.Clamp(currentPower, 0f, maxPower);
            currentDistance = currentPower * maxDistance / maxPower;

            Quaternion throwRotation = Quaternion.LookRotation(player.transform.forward, player.transform.up);
            transform.rotation = throwRotation * Quaternion.Euler(throwAngle, 0f, 0f);
        }

        if (Input.GetMouseButtonUp(0) && isThrowing && player != null)
        {
            isThrowing = false;
            Vector3 throwDirection = transform.forward;
            Vector3 throwForce = throwDirection * currentPower;
            GetComponent<Rigidbody>().AddForce(throwForce, ForceMode.Impulse);
            player.GetComponent<PlayerCatchBall>().IsCatched = false;

            hitBallAudio.Play();
        }
    }
    public void AssignPlayer(GameObject player_)
    {
        player = player_;
    }
}
