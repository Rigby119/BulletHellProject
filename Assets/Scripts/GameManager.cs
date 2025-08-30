using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject winPanel;
    private bool gameEnded = false;
    private bool bossSpawned;

    public TextMeshProUGUI bossHealthText;
    public TextMeshProUGUI playerHealthText;
    
    void Start()
    {
        bossSpawned = true;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void WinGame()
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }

    void Update()
    {
        if (FindFirstObjectByType<Boss>() != null)
        {
            if (!bossSpawned)
            {
                bossSpawned = true;
            }

            int health = FindFirstObjectByType<Boss>().health;

            float healthPercentage = Mathf.Clamp01(health / 250f);

            // get the color based on health percentage
            int hex = Mathf.RoundToInt(255f * healthPercentage);

            Color32 bossColor = new Color32((byte)(255 - hex), (byte)hex, 0, 255);
            // Smooth transition between colors based on health
            bossHealthText.color = bossColor;

            bossHealthText.text = $"BOSS HEALTH {health}";
        } else if (bossSpawned)
        {
            bossHealthText.text = "";
        }
        else
            bossHealthText.text = "Incoming Boss...";

        if (FindFirstObjectByType<PlayerShooting>() != null)
        {
            int health = FindFirstObjectByType<PlayerShooting>().health;

            float healthPercentage = Mathf.Clamp01(health / 100f);

            // get the color based on health percentage
            int hex = Mathf.RoundToInt(255f * healthPercentage);
            Color32 playerColor = new Color32((byte)(255 - hex), (byte)hex, 0, 255);

            playerHealthText.color = playerColor;
            
            playerHealthText.text = $"PLAYER HEALTH {FindFirstObjectByType<PlayerShooting>().health}";
        }
        else
            playerHealthText.text = "GAME OVER";
    }
}
