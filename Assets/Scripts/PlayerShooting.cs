using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    // Bullet prefab to be instantiated when shooting
    public GameObject bulletPrefab;
    // Point from which the bullet will be fired
    public Transform firePoint;

    private SpriteRenderer spriteRenderer;
    
     // Time between shots
    public float fireRate = 0.2f;
    // Cooldown timer to ensure fire rate with deltaTime
    private float fireCooldown = 0f;

    // Boolean to auto fire
    public bool autoFire = false;

    // Player health
    public int health = 100;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator DamageFlash()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.025f);
        spriteRenderer.color = Color.white;
    }

    // Method to handle shooting
    void Shoot()
    {
        // Instantiate a bullet at the fire point's position
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        StartCoroutine(DamageFlash());

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy_Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Boss_Bullet"))
        {
            TakeDamage(2);
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        // Update the cooldown timer
        fireCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E))
        {
            autoFire = !autoFire;
        }

        // Check for shooting input and fire if cooldown has expired
        if (Input.GetKey(KeyCode.Space) && fireCooldown <= 0f || autoFire && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate;
        }
    }
}
