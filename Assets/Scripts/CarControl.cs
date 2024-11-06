using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
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
    public Rigidbody rb;

    [Header("Other Configuration")]
    public float carHealth;
    public float carDamage;
    public float carFuelCapacity;
    public float carFuel;

    private bool accelerating = false;
    private bool braking = false;
    private CarControls carControls;

    private void Awake()
    {
        carControls = new CarControls();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        carControls.Enable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward * GetAccelerationDirection() * accelerationRate * Time.fixedDeltaTime);
        rb.
    }

    float GetAccelerationDirection()
    {
        return carControls.Car.Accelerate.ReadValue<float>();
    }
}
