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
    public float currentSteeringAngle;
    public float steeringSensitivity;
    public float maxSteeringAngle;

    [Header("Important Configuration")]
    public Rigidbody rb;

    [Header("Other Configuration")]
    public float carHealth;
    public float carDamage;
    public float carFuelCapacity;
    public float carFuel;

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
        UpdateSpeed();
        UpdateSteer();
    }

    void UpdateSpeed()
    {
        currentSpeed = rb.linearVelocity.magnitude;
        if (GetAccelerationDirection() > 0 || GetAccelerationDirection() < 0 && rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * GetAccelerationDirection() * accelerationRate * Time.fixedDeltaTime);
        }
        else
        {
            if (rb.linearVelocity.magnitude > 0.1f)
            {
                rb.linearVelocity -= rb.linearVelocity.normalized * Mathf.Clamp01(1 - (rb.linearVelocity.magnitude / maxSpeed)) * Time.deltaTime * brakingForce;
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
            }
        }
    }

    void UpdateSteer()
    {
        
    }

    float GetAccelerationDirection()
    {
        return carControls.Car.Accelerate.ReadValue<float>();
    }

    float GetSteeringDirection()
    {
        return carControls.Car.Steer.ReadValue<float>();
    }
}
