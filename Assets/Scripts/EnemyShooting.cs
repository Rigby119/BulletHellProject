using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    // Bullet prefab to be instantiated when shooting
    public GameObject bulletPrefab;

    // Point from which the bullet will be fired
    public Transform firePoint;
    
     // Time between shots
    public float fireRate = 0.35f;
    // Cooldown timer to ensure fire rate with deltaTime
    private float fireCooldown = 0f;

    // Method to handle shooting
    void Shoot()
    {
        // Instantiate a bullet at the fire point's position
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }

    void Update()
    {
        // Update the cooldown timer
        fireCooldown -= Time.deltaTime;

        // Check for shooting input and fire if cooldown has expired
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate;
        }
    }
}
