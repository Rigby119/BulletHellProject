using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public float speedY = 1.5f;
    public float speedX = 0.5f;
    public int health = 150;

    public float amplitude = 3f;
    public float frequency = 1f;

    private SpriteRenderer spriteRenderer;

    private Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * speedY * frequency) * amplitude;
        float newX = startPosition.x + Mathf.Sin(Time.time * speedX * frequency) * amplitude * 0.5f;
        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    private IEnumerator DamageFlash()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.025f);
        spriteRenderer.color = Color.white;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        StartCoroutine(DamageFlash());        

        if (health <= 0)
        {
            Destroy(gameObject);
        } else if (health < 100)
        {
            amplitude = 3f; // Increase amplitude for more erratic movement
            frequency = 3f; // Increase frequency for faster movement
            speedY = 2f;   // Increase vertical speed
            speedX = 1f;   // Increase horizontal speed
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player_Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.WinGame();
        }
    }
}
