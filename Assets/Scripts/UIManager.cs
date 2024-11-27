using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timerSTR;
    public TextMeshProUGUI speedSTR;
    public TextMeshProUGUI countdownSTR;
    public TextMeshProUGUI lapsSTR;

    public Slider powerSlider;

    public bool timer = false;
    public GameObject pauseCanvas;
    public GameObject gameCanvas;

    // Update is called once per frame
    void Update()
    {
        timerSTR.text = GameManager.instance.formatedTime;

        powerSlider.maxValue = CarController.instance.maxFirePower;
        powerSlider.value = CarController.instance.firePower;

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
        if (FinishLine.instance.currentLap == 0)
        {
            lapsSTR.text = "Current Lap 1/" + GameManager.instance.totalLaps.ToString();
        }
        else
        {
            lapsSTR.text = "Current Lap " + FinishLine.instance.currentLap.ToString() + "/" + GameManager.instance.totalLaps.ToString();
        }
        
    }
}
