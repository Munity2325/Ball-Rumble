using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject bluePlayerPrefab;
    public GameObject spawnRedPlayer;

    public bool isRedPlayer = false;

    // public NetworkManager networkManager;

    
    public void ConnectAsBluePlayer()
    {
        // networkManager.playerPrefab = bluePlayerPrefab;
        // networkManager.StartClient();
    }

    // public void ConnectAsRedPlayer()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    //     SceneManager.LoadScene("SampleScene1");
    //     Debug.Log("1");
    // }

    // private void OnDestroy()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     if (scene.name == "SampleScene1")
    //     {
    //         NetworkServer.Listen(7777); // Указываем порт, который будет прослушивать сервер
    //         Debug.Log("2"); 
    //         CmdSpawnRedPlayer();
    //     }
    // }

    // [Command]
    // private void CmdSpawnRedPlayer()
    // {
    //     GameObject redPlayer = Instantiate(redPlayerPrefab, Vector3.zero, Quaternion.identity);
    //     NetworkServer.Spawn(redPlayer);
    // }


    public void ConnectAsRedPlayer()
    {
        Debug.Log("aaaaaaaa");
        isRedPlayer = true;

        int isRed = isRedPlayer ? 1 : 0;
        PlayerPrefs.SetInt("SpawnRedPlayer", isRed);

    }
}
