using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI timerSTR;
    public TextMeshProUGUI speedSTR;
    public TextMeshProUGUI countdownSTR;
    public TextMeshProUGUI lapsSTR;

    public Slider powerSlider;

    public bool timer = false;
    public GameObject pauseCanvas;
    public GameObject gameCanvas;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        if (GameManager.instance.formatedTime != "")
        {
            timerSTR.text = GameManager.instance.formatedTime;
        }
        else
        {
            timerSTR.text = "00:00:00";
        }

        if (powerSlider != null)
        {
            powerSlider.maxValue = CarController.instance.maxFirePower;
            powerSlider.value = CarController.instance.firePower;
        }

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
            lapsSTR.text = "Lap 1/" + GameManager.instance.totalLaps.ToString();
        }
        else
        {
            lapsSTR.text = "Lap " + FinishLine.instance.currentLap.ToString() + "/" + GameManager.instance.totalLaps.ToString();
        }
        countdownSTR.text = GameManager.instance.countdownStr;
    }
}
