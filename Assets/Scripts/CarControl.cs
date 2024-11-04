using UnityEngine;
using UnityEngine.InputSystem;

public class CarControl : MonoBehaviour
{
    [Header("Speed Control")]
    public float currentSpeed;
    public float accelerationRate;
    public float brakingForce;
    public float maxSpeed;

    [Header("Steering Control")]
    public float steeringSensitivity;
    public float maxTurningRadiuns;

    [Header("Important Configuration")]
    public InputActionReference accelerate;
    public Rigidbody rb;

    [Header("Other Configuration")]
    public float carHealth;
    public float carDamage;
    public float carFuelCapacity;
    public float carFuel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        accelerate.action.started += Accelerate;
    }

    void Accelerate(InputAction.CallbackContext obj)
    {
        print("sus");
    }
}
