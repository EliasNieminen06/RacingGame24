using System.Collections;
using UnityEngine;

public class MushroomShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootingPoint;
    public float cooldownTime = 2f;

    private void Start()
    {
        Shoot();
    }

    private void Shoot()
    {
        StartCoroutine(ShootProjectile());
    }

    private IEnumerator ShootProjectile()
    {
        Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
        yield return new WaitForSeconds(cooldownTime);
        Shoot();
    }
}
