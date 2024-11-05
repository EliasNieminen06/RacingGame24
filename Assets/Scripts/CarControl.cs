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

    private CarControls controls;

    private bool accelerating = false;
    private bool braking = false;

    private void Awake()
    {
        controls = new CarControls();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        controls.Car.Accelerate.performed += OnAccelerate;
        controls.Car.Accelerate.canceled += OnAccelerateCanceled;
        controls.Enable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (accelerating && rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * accelerationRate * Time.fixedDeltaTime);
        }
        else if (!accelerating && rb.linearVelocity.magnitude > 0)
        {
            rb.linearVelocity = Vector3.MoveTowards(rb.linearVelocity, Vector3.zero, (brakingForce / 5) * Time.fixedDeltaTime);
        }
    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        
        if (context.performed)
        {
            accelerating = true;
            
        }
    }

    private void OnAccelerateCanceled(InputAction.CallbackContext context)
    {
        accelerating = false;
    }
}
