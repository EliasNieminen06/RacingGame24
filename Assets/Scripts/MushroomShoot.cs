using UnityEngine;

public class MushroomShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootingPoint;
    public float cooldownTime = 2f;
    private float lastShootTime;

    private void Update()
    {
        if (Time.time - lastShootTime >= cooldownTime)
        {
            ShootProjectile();
            lastShootTime = Time.time;
        }
    }

    private void ShootProjectile()
    {
        Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
    }
}
