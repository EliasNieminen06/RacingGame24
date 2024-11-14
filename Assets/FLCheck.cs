using UnityEngine;

public class FLCheck : MonoBehaviour
{
    public bool isCollided;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("flc"))
        {
            isCollided = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("flc"))
        {
            
        }
    }
}
