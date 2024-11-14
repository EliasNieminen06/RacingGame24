using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject checkMid;
    public GameObject checkBack;
    public int currentLap = 0;
    bool backFirst = false;
    bool midFirst = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool midCollided = checkMid.GetComponent<FLCheck>().isCollided;
        bool backCollided = checkBack.GetComponent<FLCheck>().isCollided;
        if (backCollided && !midCollided && !midFirst)
        {
            backFirst = true;
        }
        if (backFirst && midCollided)
        {
            checkBack.GetComponent<FLCheck>().isCollided = false;
            checkMid.GetComponent<FLCheck>().isCollided = false;
            backFirst = false;
            currentLap += 1;
        }
        if(!backCollided && midCollided && !backFirst)
        {
            midFirst = true;
        }
        if (midFirst && backCollided)
        {
            checkBack.GetComponent<FLCheck>().isCollided = false;
            checkMid.GetComponent<FLCheck>().isCollided = false;
            midFirst = false;
            currentLap -= 1;
        }
    }
}
