using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalLaps = 3;
    public FinishLine finishLine;
    public float timer;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }



    private void Update()
    {
        timer += Time.deltaTime;
        if (finishLine.currentLap > totalLaps)
        {
            print("You finished!");
        }
    }
}
