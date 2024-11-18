using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timerSTR;

    public bool timer = false;
    public GameObject pauseCanvas;
    public GameObject gameCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerSTR.text = GameManager.instance.formatedTime;
        if (GameManager.instance.paused)
        {
            gameCanvas.SetActive(false);
            pauseCanvas.SetActive(true);
        }
        else
        {
            pauseCanvas.SetActive(false);
            gameCanvas.SetActive(true);
        }
    }
}
