using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;

    public float LevelDiameter = 7.5f;

    public float SpawnInterval = 5;

    private float SpawnIntervalCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        SpawnIntervalCounter = SpawnInterval;
        SpawnEnemy(Enemy);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnIntervalCounter -= Time.deltaTime;
        if (SpawnIntervalCounter <= 0)
        {
            SpawnIntervalCounter = SpawnInterval;
            SpawnEnemy(Enemy);
        }
    }

    private void SpawnEnemy(GameObject enemyObject)
    {
        GameObject.Instantiate(enemyObject,GenerateRandomSpawnPoint(),Quaternion.identity);
    }

    private Vector3 GenerateRandomSpawnPoint()
    {
        return new Vector3(Random.Range(-LevelDiameter, LevelDiameter), transform.position.y, 0);
    }
}
