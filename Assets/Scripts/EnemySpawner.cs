using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject borderEnemyPrefab;
    public GameObject hardEnemyPrefab;
    public GameObject bossPrefab;

    public bool spawnEnabled = true;
    public int round = 1;

    // starting time
    private float startTime;
    // time between rounds
    private float roundTime = 3f;
    // time at the end of the last round
    private float lastRoundEndTime;

    // boolean if active round
    private bool activeRound = false;

    // boolean if boss spawned
    private bool bossSpawned = false;

    // Positions to spawn enemies: (6.5, 0), (8, -3), (8, 3)
    public Vector3[] spawnPositions = new Vector3[]
    {
        new Vector3(6.5f, 0f, 0f),
        new Vector3(8f, -3f, 0f),
        new Vector3(8f, 3f, 0f)
    };

    // Hard Enemy position (8, 0)
    public Vector3 hardEnemyPosition = new Vector3(8f, 0f, 0f);

    // Boss position (7, 0)
    public Vector3 bossPosition = new Vector3(7f, 0f, 0f);

    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // If 60 seconds have passed since startTime, enable boss spawn and disable enemy spawns
        if (startTime + 60f < Time.time && spawnEnabled && !bossSpawned)
        {
            SpawnBoss();
            bossSpawned = true;
            spawnEnabled = false;
            return;
        }
        
        if (bossSpawned)
        {
            return;
        }

        if (spawnEnabled)
        {
            if (round % 2 == 0)
            {
                // Spawn a hard enemy every 5 rounds
                Instantiate(hardEnemyPrefab, hardEnemyPosition, Quaternion.Euler(0, 0, -90));
            }
            else
            {
                // Spawn regular enemies
                SpawnEnemies();
            }
            spawnEnabled = false;
            activeRound = true;
            round++;
        }

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && activeRound)
        {
            lastRoundEndTime = Time.time;
            activeRound = false;
        }

        if (!activeRound && !spawnEnabled && lastRoundEndTime + roundTime < Time.time)
        {
            spawnEnabled = true;
        }
    }

    void SpawnBoss()
    {
        Instantiate(bossPrefab, bossPosition, Quaternion.Euler(0, 0, -90));
    }

    void SpawnEnemies()
    {
        foreach (var position in spawnPositions)
        {
            if (position.y != 0)
            {
                Instantiate(borderEnemyPrefab, position, Quaternion.Euler(0, 0, -90));
            }
            else{
                Instantiate(enemyPrefab, position, Quaternion.Euler(0, 0, -90));
            }
        }
    }
}
