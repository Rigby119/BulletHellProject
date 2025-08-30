using UnityEngine;
using TMPro;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance;

    private int activeBullets = 0;
    public TextMeshProUGUI bulletCounterText;
    
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        bulletCounterText.text = $"BULLETS {activeBullets}";

        if (activeBullets < 50)
            bulletCounterText.color = Color.green;
        else if (activeBullets < 100)
            bulletCounterText.color = Color.yellow;
        else
            bulletCounterText.color = Color.red;
    }

    public void NewBullet()
    {
        activeBullets++;
    }

    public void BulletDestroyed()
    {
        activeBullets--;
    }
}
