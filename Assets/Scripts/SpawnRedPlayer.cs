using UnityEngine;

public class SpawnRedPlayer : MonoBehaviour
{
    public GameObject redPlayerPrefab;
    void Start()
    {
        // if(isServer)
        // {
        //     CmdSpawnRedPlayer();
        // }
    }

    // [Command]
    private void CmdSpawnRedPlayer()
    {
        GameObject redPlayer = Instantiate(redPlayerPrefab, Vector3.zero, Quaternion.identity);
        // NetworkServer.Spawn(redPlayer);
    }
}
