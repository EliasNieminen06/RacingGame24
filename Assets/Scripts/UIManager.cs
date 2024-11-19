using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timerSTR;
    public TextMeshProUGUI speedSTR;

    public bool timer = false;
    public GameObject pauseCanvas;
    public GameObject gameCanvas;

    // Update is called once per frame
    void Update()
    {
        timerSTR.text = GameManager.instance.formatedTime;

        int carSpeed = (int)CarController.instance.currentCarSpeed;
        speedSTR.text = carSpeed.ToString() + " km/h";

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
