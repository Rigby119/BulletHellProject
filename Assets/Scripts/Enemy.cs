using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int health = 25;
    
    public float amplitude = 4f;
    private Vector3 startPosition;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator DamageFlash()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.025f);
        spriteRenderer.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
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
        if (collision.CompareTag("Player_Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }
}
