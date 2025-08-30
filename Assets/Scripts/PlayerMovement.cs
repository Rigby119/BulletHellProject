 using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Velocity of the player
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    private Vector2 movement;

    private SpriteRenderer spriteRenderer;

    public Sprite spriteIdle;
    public Sprite spriteUp;
    public Sprite spriteDown;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if (movement.y > 0)
        {
            spriteRenderer.sprite = spriteUp;
        }
        else if (movement.y < 0)
        {
            spriteRenderer.sprite = spriteDown;
        }
        else
        {
            spriteRenderer.sprite = spriteIdle;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
