using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Speed and life time of the bullet
    public float speed = 10f;
    public float lifeTime = 1f;
    public Vector3 direction = Vector3.up;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.Rotate(0, 0, -90);
        
        // Notify the BulletManager that a new bullet has been created
        BulletManager.Instance.NewBullet();

        // Destroy the bullet after its life time expires
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the bullet upwards
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnDestroy()
    {
        // Notify the BulletManager that this bullet has been destroyed
        BulletManager.Instance.BulletDestroyed();
    }
}
