using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewScoreManager : MonoBehaviour
{
    public TMP_InputField iF;

    public void OnNameEntered()
    {
        GameManager.instance.NewScoreEntered(iF.text);
    }
}
