using UnityEngine;

public class CreatePlayers : MonoBehaviour
{

    // public NetworkManager networkManager;
    public GameObject red;
    public bool isRed = false;
    private bool canSpawn = true; 

    private void Start()
    {
        // networkManager = FindObjectOfType<NetworkManager>();
    }
    private void Update()
    {
        if (PlayerPrefs.HasKey("SpawnRedPlayer"))
        {
            int boolValue = PlayerPrefs.GetInt("SpawnRedPlayer");
            isRed = boolValue == 1 ? true : false;
        }

        if (isRed == true && canSpawn == false)
        {
            // networkManager.playerPrefab = red;
            // networkManager.StartClient();
            canSpawn = false;
        }
    }
}
