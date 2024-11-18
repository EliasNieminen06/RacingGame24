using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalLaps = 3;
    public FinishLine finishLine;
    public float timer;
    public string formatedTime;
    public bool finished;

    private void Awake()
    {
        instance = this;
    }
    
    private void Update()
    {
        timer += Time.deltaTime;

        if (finishLine.currentLap > totalLaps && !finished)
        {
            finished = true;
            Finish();
        }

        float time = GameManager.instance.timer;
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        float millidseconds = Mathf.FloorToInt((time * 1000) % 1000);
        formatedTime = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, millidseconds);
    }

    private void Finish()
    {
        print("You finished in: " + formatedTime);
        LeaderBoardManager.instance.AddPlayerStats(Random.Range(1, 1000).ToString(), timer);
    }
}
