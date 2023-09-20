using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public GameObject heartPrefab;

    private float spawnRange = 20;
    public int enemyCount;
    public int powerupCount;
    public int waveNumber = 1;

    public bool isWaveStart = false;

    public TextMeshProUGUI waveText;

    private GameObject player;
    private ParticleManager particleManager;

    public ParticleSystem detonatorParticle;
    public ParticleSystem heartParticle;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        particleManager = player.GetComponent<ParticleManager>();

        if (!PlayerController.instance.GameOver)
        {
            SpawnEnemyWave(waveNumber);
            waveText.text = "Wave " + waveNumber;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.instance.GameOver)
        {
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            powerupCount = GameObject.FindGameObjectsWithTag("Powerup").Length;

            

            if (enemyCount == 0 && !isWaveStart)
            {
                isWaveStart = true;
                waveNumber++;
                StartCoroutine(WaitEnemyWave());
            }
        }
    }

    IEnumerator WaitEnemyWave()
    {
        yield return new WaitForSeconds(2);
        waveText.text = "Wave " + waveNumber;
        SpawnEnemyWave(waveNumber);
        if (powerupCount < 5 && waveNumber % 5 == 0)
        {
            SpawnPowerup();
        }
        if (waveNumber % 7 == 0)
        {
            SpawnHeart();
        }
        isWaveStart = false;

    }

    void SpawnHeart()
    {
        Vector3 heartPos = GenerateSpawnPos() + new Vector3(0, (float)0.6, 0);
        Instantiate(heartPrefab, heartPos, powerupPrefab.transform.rotation);
        heartParticle.transform.position = heartPos;
        heartParticle.Play();
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for(int i=0; i<enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateEnemySpawnPos(), enemyPrefab.transform.rotation);
            
        }
    }

    void SpawnPowerup()
    {
        Vector3 powerupPos = GenerateSpawnPos();
        Instantiate(powerupPrefab, powerupPos, powerupPrefab.transform.rotation);
        detonatorParticle.transform.position = powerupPos;
        detonatorParticle.Play();
    }

    private Vector3 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, (float)1.2, spawnPosZ);

        return randomPos;
    }

    private Vector3 GenerateEnemySpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 1, spawnPosZ);

        return randomPos;
    }

}
