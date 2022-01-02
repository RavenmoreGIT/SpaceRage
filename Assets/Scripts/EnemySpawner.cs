using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;

    public float LevelDiameter = 7.5f;

    public float SpawnInterval = 10;

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
        Vector3 SpawnPoint = GenerateRandomSpawnPoint();
        Vector3 Distance = new Vector3(0, 1.5f,0);
        GameObject.Instantiate(enemyObject,SpawnPoint,Quaternion.identity);
        GameObject.Instantiate(enemyObject, SpawnPoint+Distance, Quaternion.identity);
        GameObject.Instantiate(enemyObject, SpawnPoint + Distance+Distance, Quaternion.identity);
    }

    private Vector3 GenerateRandomSpawnPoint()
    {
        return new Vector3(Random.Range(-LevelDiameter, LevelDiameter), transform.position.y, 0);
    }
}
