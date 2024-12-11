using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewScoreManager : MonoBehaviour
{
    public TMP_InputField iF;
    public TextMeshProUGUI time;

    private void Start()
    {
        time.text = GameManager.instance.formatedTime;
    }

    public void OnNameEntered()
    {
        GameManager.instance.NewScoreEntered(iF.text);
    }
}
