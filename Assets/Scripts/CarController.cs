using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public static CarController instance;

    private CarInputActions carInputActions;

    Rigidbody rb;
    public Transform[] rayPoints;
    public LayerMask ground;
    public Transform accelerationPoint;

    public float springStiffness;
    public float damperStiffness;
    public float restLength;
    public float springTravel;
    public float wheelRadius;

    int[] wheelsIsGrounded = new int[4];
    bool isGrounded = false;

    float moveInput = 0;
    float steerInput = 0;

    bool canJump = false;
    bool jumped = false;

    public float acceleration = 25f;
    public float maxSpeed = 100f;
    public float deceleration = 10f;
    public float steerStrength = 15f;
    public AnimationCurve turningCurve;
    public float dragCoefficient = 1f;
    public float jumpForce = 20;

    Vector3 currentCarLocalVelocity = Vector3.zero;
    float carVelocityRatio = 0;

    public float currentCarSpeed;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        carInputActions = new CarInputActions();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        carInputActions.Enable();
    }

    private void Update()
    {
        GetPlayerInput();
        currentCarSpeed = rb.linearVelocity.magnitude * 3.6f;
    }

    private void FixedUpdate()
    {
        Suspension();
        GroundCheck();
        CalculateCarVelocity();
        Movement();
        Jump();
    }

    private void Suspension()
    {
        for (int i = 0; i < rayPoints.Length; i++)
        {
            RaycastHit hit;
            float maxLen = restLength + springTravel;
            
            if (Physics.Raycast(rayPoints[i].position, -rayPoints[i].up, out hit, maxLen + wheelRadius, ground))
            {
                wheelsIsGrounded[i] = 1;

                float currentSpringLen = hit.distance - wheelRadius;
                float springCompression = (restLength - currentSpringLen) / springTravel;

                float springVelocity = Vector3.Dot(rb.GetPointVelocity(rayPoints[i].position), rayPoints[i].up);
                float dampForce = damperStiffness * springVelocity;

                float springForce = springStiffness * springCompression;

                float netForce = springForce - dampForce;

                rb.AddForceAtPosition(netForce * rayPoints[i].up, rayPoints[i].position);
            }
            else
            {
                wheelsIsGrounded[i] = 0;
            }
        }
    }

    private void GetPlayerInput()
    {
        moveInput = carInputActions.Drive.Accelerate.ReadValue<float>();
        steerInput = carInputActions.Drive.Steer.ReadValue<float>();
    }

    private void GroundCheck()
    {
        int tempGroundWheels = 0;

        for (int i = 0; i < wheelsIsGrounded.Length; i++)
        {
            tempGroundWheels += wheelsIsGrounded[i];
        }

        if(tempGroundWheels > 1)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void CalculateCarVelocity()
    {
        currentCarLocalVelocity = transform.InverseTransformDirection(rb.linearVelocity);
        carVelocityRatio = currentCarLocalVelocity.z / maxSpeed;
    }

    private void Movement()
    {
        if (isGrounded)
        {
            Acceleration();
            Deceleration();
            Turn();
            SidewaysDrag();
        }
    }

    private void Acceleration()
    {
        rb.AddForceAtPosition(acceleration * moveInput * transform.forward, accelerationPoint.position, ForceMode.Acceleration);
    }

    private void Deceleration()
    {
        rb.AddForceAtPosition(deceleration * moveInput * -transform.forward, accelerationPoint.position, ForceMode.Acceleration);
    }

    private void Turn()
    {
        rb.AddRelativeTorque(steerStrength * steerInput * turningCurve.Evaluate(Mathf.Abs(carVelocityRatio)) * Mathf.Sign(carVelocityRatio) * rb.transform.up, ForceMode.Acceleration);
    }

    private void SidewaysDrag()
    {
        float currentSidewaysSpeed = currentCarLocalVelocity.x;

        float dragMagnitude = -currentSidewaysSpeed * dragCoefficient;

        Vector3 dragForce = transform.right * dragMagnitude;

        rb.AddForceAtPosition(dragForce, rb.worldCenterOfMass, ForceMode.Acceleration);
    }

    private void Jump()
    {
        
        if (carInputActions.Drive.Jump.IsPressed() && isGrounded)
        {
            rb.AddRelativeForce(transform.up * jumpForce * 100, ForceMode.Force);
        }
    }
}
