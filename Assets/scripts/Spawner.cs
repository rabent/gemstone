using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spwanPoint;
    public SpawnData[] spawnData;
    int level;
    float timer;

    void Awake() 
    {
        spwanPoint = GetComponentsInChildren<Transform>();    
    }

    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(gamemanager.instance.gameTime / 10f), spawnData.Length - 1); // 10초마다 spawner elements 넘어감

        if(timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }
    void Spawn()
    {
        GameObject Enemy = gamemanager.instance.pool.pulling(2);
        Enemy.transform.position = spwanPoint[Random.Range(1, spwanPoint.Length)].position;
        Enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}
