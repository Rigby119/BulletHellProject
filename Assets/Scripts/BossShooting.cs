using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    private float fireRate = 0.35f;
    public float bulletSpacing = 0.5f;

    public float patternChangeInterval = 5f;
    public int spiralBulletCount = 10;
    public float spiralRadius = 2f;
    public float waveAmplitude = 2f;
    public int waveBulletCount = 5;

    private float fireCooldown = 0f;
    private float patternTimer = 0f;
    private int currentPattern = 0;
    private float spiralAngle = 0f;

    private enum ShootingPattern
    {
        Circle,
        Shower,
        Diamond,
        Spiral
    }

    private ShootingPattern[] patterns = {
        ShootingPattern.Circle,
        ShootingPattern.Shower,
        ShootingPattern.Diamond,
        ShootingPattern.Spiral
    };
    
    // Update is called once per frame
    void Update()
    {
        patternTimer += Time.deltaTime;
        if (patternTimer >= patternChangeInterval)
        {
            patternTimer = 0f;
            currentPattern = (currentPattern + 1) % patterns.Length;
        }

        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            switch (patterns[currentPattern])
            {
                case ShootingPattern.Circle:
                    fireRate = 0.35f;
                    CircleShot();
                    break;
                case ShootingPattern.Shower:
                    int index = 0;
                    spiralAngle = 0f;
                    fireRate = 0.075f;
                    if (spiralAngle >= 90f) {
                        index = 1;
                    }
                    if (spiralAngle <= -90f) {
                        index = 0;
                    }
                    ShowerShot(index);
                    break;
                case ShootingPattern.Diamond:
                    fireRate = 0.35f;
                    DiamondShot();
                    break;
                case ShootingPattern.Spiral:
                    fireRate = 0.04f;
                    SpiralShot();
                    spiralAngle += 10f; // Increment angle for spiral effect
                    break;
            }
            fireCooldown = fireRate;
        }
    }

    void CircleShot()
    {
        for (int i = 0; i < spiralBulletCount; i++)
        {
            float angle = spiralAngle + (360f / spiralBulletCount) * i;
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            Instantiate(bulletPrefab, transform.position + direction, Quaternion.Euler(0, 0, angle));
        }
    }

    void ShowerShot(int index)
    {
        float angle = spiralAngle;
        
        Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);

        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));

        if (index == 0) {
            spiralAngle += 20f;
            spiralAngle += Time.time * 0.01f;
        } else {
            spiralAngle -= 20f;
            spiralAngle -= Time.time * 0.01f;
        }
    }

    void DiamondShot()
    {
        Vector3[] offsets = new Vector3[]
        {
            new Vector3(0, bulletSpacing, 0),
            new Vector3(0, bulletSpacing/2, 0),

            new Vector3(bulletSpacing, 0, 0),
            new Vector3(bulletSpacing/2, 0, 0),

            new Vector3(0, 0, 0),

            new Vector3(0, -bulletSpacing, 0),
            new Vector3(0, -bulletSpacing/2, 0),

            new Vector3(-bulletSpacing, 0, 0),
            new Vector3(-bulletSpacing/2, 0, 0)
        };

        foreach (var offset in offsets)
        {
            Instantiate(bulletPrefab, transform.position + offset, Quaternion.identity);
        }
    }

    void SpiralShot()
    {
        float angle = spiralAngle;
        
        Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);

        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));

        spiralAngle += 15f;

        spiralAngle += Time.time * 0.01f;
    }
}
