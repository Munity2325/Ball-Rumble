using UnityEngine;

public class GoalSystem : MonoBehaviour
{
    public int RedScore
    {
        get { return redScore; }
        set { redScore = value; }
    }
    public int BlueScore
    {
        get { return blueScore; }
        set { blueScore = value; }
    }
    
    [SerializeField] private int blueScore;
    [SerializeField] private int redScore;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
