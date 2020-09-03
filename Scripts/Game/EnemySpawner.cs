using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public Transform[] spawnPoints;
    public GameObject enemy;

    private Transform player;
    private Transform currentSpawnPoint;

    public TextMeshProUGUI waveCounter;

    public Canvas winScreen;

    [SerializeField]
    private List<GameObject> activeEnemies = new List<GameObject>();

    [SerializeField]
    private int waveCount = 0;

    [SerializeField]
    private float enemyNumber = 1, enemyWaveMultipler = 2f, enemyWaveIncrement = 2, waveTimer = 10, waveWinNumber = 20;

    public int currentEnemyCount = 0;

    bool spawning = true;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        activeEnemies.Add(null);

        InvokeRepeating("AdvanceWave", 0, waveTimer);
        winScreen.enabled = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

        player = EvolutionSystem.instance.GetCurrentPlayerTransform();

        

        if(waveCount > waveWinNumber)
        {
            CancelInvoke("SpawnEnemy");
            CancelInvoke("AdvanceWave");

            if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
            {
                WinGame();
            }

        } else
        {
            waveCounter.text = "WAVE: " + waveCount + "/" + waveWinNumber;
        }

        if (EvolutionSystem.instance.isEvolving)
        {
            CancelInvoke("SpawnEnemy");
            CancelInvoke("AdvanceWave");
            spawning = false;
        } else if (!spawning)
        {
            InvokeRepeating("AdvanceWave", 0, waveTimer);
            spawning = true;
        }

        print(currentEnemyCount);
    }

    void StartSpawningWave()
    {
        for(int i = 0; i < enemyNumber; i++)
        {


            //activeEnemies.Add(Instantiate(enemy, spawnPoint.position, spawnPoint.rotation));

            Invoke("SpawnEnemy", (waveTimer / enemyNumber) * i);
        }

    }

    void SpawnEnemy()
    {
        currentSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, currentSpawnPoint.position, currentSpawnPoint.rotation);
        currentEnemyCount++;
    }

    void AdvanceWave()
    {
        enemyNumber += enemyWaveIncrement;
        waveCount++;
        StartSpawningWave();
    }

    void WinGame()
    {
        winScreen.enabled = true;
        Time.timeScale = 0;
    }
}
