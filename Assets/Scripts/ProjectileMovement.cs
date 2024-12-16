using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;

    private Rigidbody rb;
    private float timeAlive;
    public GameObject destroyParticle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive >= lifetime)
        {
            GameObject newParticle = Instantiate(destroyParticle);
            newParticle.transform.position = transform.position;
            Destroy(gameObject);
            Destroy(newParticle, 1);
        }
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * speed;
        }
        else
        {
            Destroy(gameObject, lifetime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject newParticle = Instantiate(destroyParticle);
            newParticle.transform.position = transform.position;
            Destroy(gameObject);
            Destroy(newParticle, 1);
            GameManager.instance.Fail();
        }
        else
        {
            GameObject newParticle = Instantiate(destroyParticle);
            newParticle.transform.position = transform.position;
            Destroy(gameObject);
            Destroy(newParticle, 1);
        }
    }
}
