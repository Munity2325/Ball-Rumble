using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public Text timerText;
    public float startTime = 180f;

    private float currentTime;

    public Text blueTeamName;
    public Text redTeamName;
    public Text score;

    public GameObject scoreManager;
    public GameObject namesManager;

    private void Start()
    {
        currentTime = startTime;

        scoreManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0f)
        {
            currentTime = 0f;
        }

        int minutes = Mathf.FloorToInt(currentTime / 60f);

        int seconds = Mathf.FloorToInt(currentTime % 60f);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

        timerText.text = timeString;

        score.text = scoreManager.GetComponent<GoalSystem>().RedScore.ToString() + ":" + scoreManager.GetComponent<GoalSystem>().BlueScore.ToString();


        if (PlayerPrefs.HasKey("BlueName"))
        {
            blueTeamName.text = PlayerPrefs.GetString("BlueName");
        }
        if (PlayerPrefs.HasKey("RedName"))
        {
            redTeamName.text = PlayerPrefs.GetString("RedName");
        }
    }
}
